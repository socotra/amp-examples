using System.Net.Http.Headers;
using System.Text.Json;
using Socotra.VinLookup.Models;

namespace Socotra.VinLookup.Logic;

public class AutofillHelper
{
    private readonly Validator _validator = new Validator();

    private readonly StateApiHelper _stateApiHelper = new StateApiHelper();
    public AutofillResponse BuildAutoFillResponse(AutofillResponse resp, StateApiResponse StateApiResp, VinsInfo[] vins, string fieldMappingType)
    {
        string fieldMappedPath = _stateApiHelper.GetMappingPath(StateApiResp, fieldMappingType);
        string fieldLocation = _stateApiHelper.FindPathType(fieldMappedPath);
        string fieldName = _stateApiHelper.GetFieldName(fieldMappedPath);

        if (_validator.ValueIsPresent(fieldLocation))
        {
            switch (fieldLocation)
            {
                case PolicyConstants.fieldType.isFieldValueField:
                    if (_validator.AutofillRequestContainsFieldValues(resp.fieldValues))
                    {
                        string[] fieldValPolicy = { vins?[0]?.values[fieldMappingType] ?? "" };
                        resp.fieldValues[fieldName] = fieldValPolicy;
                    }
                    break;
                case PolicyConstants.fieldType.isExposureField:
                    if (resp.updateExposures.Any())
                    {
                        foreach (VinsInfo vin in vins)
                        {
                            foreach (AutofillExposureUpdateRequest exposure in resp.updateExposures)
                            {
                                if (_validator.MatchingValues(exposure.exposureLocator ?? "", vin.exposureLocator))
                                {
                                    string[] fieldVal = { vin.values[fieldMappingType] ?? "" };
                                    if (resp.updateExposures.Any())
                                    {
                                        exposure.fieldValues[fieldName] = fieldVal;
                                    }

                                }
                            }
                        }
                    }
                    break;
            }
        }
        return resp;
    }

    public AutofillResponse BuildInitialAutofillResponse(AutofillRequest request)
    {
        var resp = new AutofillResponse();
        if (_validator.AutofillRequestContainsUpdates(request))
        {

            resp.fieldValues = new Dictionary<string, string[]>() { };

            if (_validator.AutofillRequestContainsFieldValues(request.updates.fieldValues))
            {
                resp.fieldValues = request.updates.fieldValues;
            }
            if (request.updates.updateExposures.Any())
            {
                resp.updateExposures = request.updates.updateExposures;
            }
            if (request.updates.addExposures.Any())
            {
                resp.addExposures = request.updates.addExposures;
            }
        }
        return resp;
    }
}