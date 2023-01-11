import { StateResponse } from 'src/state/state.service';

export const STATE: StateResponse = {
  settings: {
    token: '787ed466cef84dc89b2cdb70aa6d0ba6',
    confidence_threshold: '0.6',
  },
  mappings: [
    {
      productName: 'newBusiness',
      fields: {
        vin: "$[?(@.name=='businessowners')].policyConfiguration.exposures[?(@.name=='exposure')].fields[?(@.name=='vin')]",
        make: "$[?(@.name=='businessowners')].policyConfiguration.exposures[?(@.name=='exposure')].fields[?(@.name=='make')]",
        model:
          "$[?(@.name=='businessowners')].policyConfiguration.exposures[?(@.name=='exposure'].fields[?(@.name=='fieldGroup')].fields[?(@.name=='model')]",
      },
    },
  ],
  socotraApiUrl: '',
  tenantHostName: '',
  token: '',
};
