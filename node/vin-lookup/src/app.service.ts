import { Injectable } from '@nestjs/common';
import { Request } from 'express';
import { AutofillPayload, AutofillResponse } from './socotra/autofill';
import { SocotraService } from './socotra/socotra.service';
import { StateService } from './state/state.service';
import { VinService } from './vin/vin.service';

@Injectable()
export class AppService {
  async autofill(
    req: Request,
    body: AutofillPayload,
    stateUrl: string,
  ): Promise<AutofillResponse> {
    console.log('call to service', body);

    const key = req.headers['x-smp-key'];
    const state = await StateService.getState(stateUrl, key);
    const fields = StateService.getFieldNames(state, body);

    const socotraApi = state.socotraApiUrl;
    const token = state.token;
    const policy = await SocotraService.getPolicy(
      socotraApi,
      token,
      body.policyLocator,
    );

    if (policy) {
      SocotraService.mergeData(body, policy, fields);
      console.log(JSON.stringify(body, null, 4));
    }

    await VinService.getVinData(body, fields);

    console.log('return');
    const newAutofillResponse: AutofillResponse = {
      fieldValues: body.updates.fieldValues,
      ...body.updates,
    };
    if (body.operation === 'renewal') {
      delete newAutofillResponse.policyStartTimestamp;
      delete newAutofillResponse.endorsementEffectiveTimestamp;
    }
    if (body.operation === 'endorsement') {
      delete newAutofillResponse.policyStartTimestamp;
      delete newAutofillResponse.policyEndTimestamp;
    }
    console.log(JSON.stringify(newAutofillResponse, null, 4));
    return newAutofillResponse;
  }
}
