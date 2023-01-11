import { AutofillPayload } from 'src/socotra/autofill';

export const PAYLOAD: AutofillPayload = {
  policyholderLocator: '',
  productName: 'newBusiness',
  operation: 'newBusiness',
  operationType: 'create',
  updates: {
    policyEndTimestamp: '',
    policyStartTimestamp: '',
    fieldValues: {
      vin: [],
      make: ['TOYOTA'],
      model: ['CAMRY'],
    },
  },
};
