import { Injectable } from '@nestjs/common';
import axios from 'axios';
import * as jsonpath from 'jsonpath';
import { Fields } from '../state/state.service';
import {
  AutofillPayload,
  ExposureUpdateRequest,
  PerilUpdateRequest,
} from './autofill';
import { PolicyResponse } from './policy';

@Injectable()
export class SocotraService {
  static async getPolicy(
    socotraApi: string,
    token: string,
    policyLocator: string,
  ): Promise<PolicyResponse> {
    console.log('getPolicy');
    try {
      const response = await axios.get(
        `${socotraApi}/policy/${policyLocator}`,
        {
          headers: { Authorization: 'Bearer ' + token },
        },
      );
      return response.data;
    } catch (err) {
      console.error('Error retrieving the policy');
      return undefined;
    }
  }

  static mergeData(
    payload: AutofillPayload,
    policy: PolicyResponse,
    fieldNames: Fields,
  ) {
    console.log('mergeData');

    const policyVinPaths = jsonpath.nodes(
      policy,
      `$..characteristics[?(!@.replaced)].fieldValues.${fieldNames['vin']}`,
    );
    policyVinPaths.forEach((policyVinPath) => {
      const updatesPath = '$.updates';
      const updates = jsonpath.nodes(payload, updatesPath).pop();
      if (policyVinPath.path.includes('exposures')) {
        const exposureInfo = this.getExposureInfo(
          policy,
          payload,
          policyVinPath.path,
          updatesPath,
        );

        if (policyVinPath.path.includes('perils')) {
          const perilInfo = this.getPerilInfo(
            policy,
            payload,
            policyVinPath.path,
            exposureInfo.path,
          );

          if (perilInfo.data) {
            this.addVinToPayload(
              fieldNames,
              policyVinPath.value,
              payload,
              perilInfo.path,
            );
          }
        } else if (exposureInfo.data.value) {
          this.addVinToPayload(
            fieldNames,
            policyVinPath.value,
            payload,
            exposureInfo.path,
          );
        }
      } else if (updates.value) {
        this.addVinToPayload(
          fieldNames,
          policyVinPath.value,
          payload,
          updatesPath,
        );
      }
    });
  }

  static addVinToPayload(
    fieldNames: Fields,
    value,
    payload: AutofillPayload,
    path: string,
  ) {
    const fieldPath = `${path}.fieldValues`;
    var fieldValues = jsonpath.query(payload, fieldPath).pop();
    if (!fieldValues) {
      fieldValues = {};
    }
    if (!fieldValues[fieldNames['vin']]) {
      fieldValues[fieldNames['vin']] = value;
      jsonpath.value(payload, fieldPath, fieldValues);
    }
  }

  static getPerilInfo(
    policy: PolicyResponse,
    payload: AutofillPayload,
    path: jsonpath.PathComponent[],
    exposurePath: string,
  ) {
    var [peril, perilLocator, perilPath]: [Node, string, string] =
      this.getExposureOrPeril(policy, payload, path, 'peril', exposurePath);

    if (!peril) {
      const newPeril: PerilUpdateRequest = {
        addFieldGroups: [],
        updateFieldGroups: [],
        removeFieldGroups: [],
        perilLocator: perilLocator,
        fieldValues: {},
      };

      const updateObjectsPath = `${exposurePath}.updatePerils`;
      peril = this.addNewExposureOrPeril<PerilUpdateRequest>(
        payload,
        newPeril,
        updateObjectsPath,
        perilPath,
      );
    }

    return { data: peril, path: perilPath };
  }

  static getExposureInfo(
    policy: PolicyResponse,
    payload: AutofillPayload,
    path: jsonpath.PathComponent[],
    updatesPath: string,
  ) {
    var [exposure, exposureLocator, exposurePath]: [Node, string, string] =
      this.getExposureOrPeril(policy, payload, path, 'exposure', updatesPath);
    if (!exposure) {
      const newExposure: ExposureUpdateRequest = {
        addFieldGroups: [],
        updateFieldGroups: [],
        removeFieldGroups: [],
        addPerils: [],
        updatePerils: [],
        exposureLocator: exposureLocator,
        removePerils: [],
        fieldValues: {},
      };

      const updateObjectsPath = `${updatesPath}.updateExposures`;
      exposure = this.addNewExposureOrPeril<ExposureUpdateRequest>(
        payload,
        newExposure,
        updateObjectsPath,
        exposurePath,
      );
    }

    return { data: exposure, path: exposurePath };
  }

  static getExposureOrPeril(
    policy: PolicyResponse,
    payload: AutofillPayload,
    path: jsonpath.PathComponent[],
    type: 'exposure' | 'peril',
    parentPath: string,
  ): [Node, string, string] {
    var exposureOrPerilPath = jsonpath.stringify(
      path.slice(0, path.indexOf(`${type}s`) + 2),
    );
    const exposureOrPerilLocator = jsonpath
      .query(policy, `${exposureOrPerilPath}.locator`)
      .pop();
    exposureOrPerilPath = `${parentPath}.update${type
      .charAt(0)
      .toUpperCase()}${type.slice(
      1,
    )}s[?(@.${type}Locator=="${exposureOrPerilLocator}")]`;
    const exposureOrPeril = jsonpath.nodes(payload, exposureOrPerilPath).pop();
    return [exposureOrPeril, exposureOrPerilLocator, exposureOrPerilPath];
  }

  static addNewExposureOrPeril<T>(
    payload: AutofillPayload,
    newExposureOrPeril: T,
    updatePath: string,
    path: string,
  ) {
    const updateExposuresOrPerils: T[] =
      jsonpath.query(payload, updatePath).pop() || [];
    updateExposuresOrPerils.push(newExposureOrPeril);
    jsonpath.value(payload, updatePath, updateExposuresOrPerils);
    return jsonpath.nodes(payload, path).pop();
  }
}

export type Node = {
  path: jsonpath.PathComponent[];
  value: any;
};
