import { VinService } from '../vin/vin.service';
import { StateService } from '../state/state.service';
import { PAYLOAD } from './data/autofill.data';
import { STATE } from './data/state.data';

describe('VinApi Service', () => {
  test('setValues', () => {
    const fields = StateService.getFieldNames(STATE, PAYLOAD);
    VinService.getVinData(PAYLOAD, fields);
    console.log(JSON.stringify(PAYLOAD, null, 4));
  });
});
