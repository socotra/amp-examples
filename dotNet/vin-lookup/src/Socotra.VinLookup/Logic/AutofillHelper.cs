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
                    if (_validator.EnsureVinFieldIsContained(vins[0]) && _validator.AutofillRequestContainsFieldValues(resp.fieldValues))
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
                            if (_validator.EnsureVinFieldIsContained(vin))
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
                    }
                    if (resp.addExposures.Any())
                    {
                        foreach (VinsInfo vin in vins)
                        {
                            if (_validator.EnsureVinFieldIsContained(vin))
                            {   
                                int index = 0;
                                foreach (AutofillExposureCreateRequest exposure in resp.addExposures)
                                {
                                    if (_validator.MatchingValues(index.ToString() ?? "", vin.exposureLocator))
                                    {

                                        string[] fieldVal = { vin.values[fieldMappingType] ?? "" };
                                        if (resp.addExposures.Any())
                                        {
                                            exposure.fieldValues[fieldName] = fieldVal;
                                        }

                                    }
                                    index++;
                                }
                            }
                        }
                    }
                    break;
                case PolicyConstants.fieldType.isFieldValueGroup:
                    if (resp.updateFieldGroups.Any())
                    {
                        foreach (VinsInfo vin in vins)
                        {
                            if (vin is not null && _validator.EnsureVinFieldIsContained(vin))
                            {
                                foreach (AutofillFieldGroupUpdateRequest fieldGroup in resp.updateFieldGroups)
                                {
                                    if (_validator.MatchingValues(fieldGroup.fieldGroupLocator ?? "", vin.fieldGroupLocator))
                                    {
                                        string[] fieldVal = { vin.values[fieldMappingType] ?? "" };
                                        if (resp.updateFieldGroups.Any())
                                        {
                                            fieldGroup.fieldValues[fieldName] = fieldVal;
                                        }

                                    }
                                }
                            }
                        }
                    }
                    if (resp.addFieldGroups.Any())
                    {
                        foreach (VinsInfo vin in vins)
                        {
                            if (vin is not null && _validator.EnsureVinFieldIsContained(vin))
                            {
                                int index = 0;
                                foreach (AutofillFieldGroupCreateRequest fieldGroup in resp.addFieldGroups)
                                {
                                    if (_validator.MatchingValues(index.ToString() ?? "", vin.fieldGroupLocator))
                                    {
                                        string[] fieldVal = { vin.values[fieldMappingType] ?? "" };
                                        if (resp.addFieldGroups.Any())
                                        {
                                            fieldGroup.fieldValues[fieldName] = fieldVal;
                                        }

                                    }
                                    index++;
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
            if (request.updates.updateFieldGroups.Any())
            {
                resp.updateFieldGroups = request.updates.updateFieldGroups;
            }
            if (request.updates.addFieldGroups.Any())
            {
                resp.addFieldGroups = request.updates.addFieldGroups;
            }
        }
        return resp;
    }
}