using System.Net.Http.Headers;
using System.Text.Json;
using Socotra.VinLookup.Models;

namespace Socotra.VinLookup.Logic;

public class VinApiHelper
{
    private readonly Validator _validator = new Validator();

    async internal Task<VinsInfo> AddFieldValueVinInfo(AutofillRequest request, string vinNameSpace, string vinLocation)
    {
        var result = new VinsInfo
        {
            vin = string.Empty
        };
        if (request.updates.fieldValues.ContainsKey(vinNameSpace))
        {   
            if (_validator.CheckPathInAutofillRequest(request, vinNameSpace, vinLocation))
            {
                var vinToLookup = request.updates.fieldValues[vinNameSpace].First();
                result = await GetVinsInfo(vinToLookup);
            }
        } else {
            result.vin = EmptyValues.none;
        }

        return result;
    }

    async internal Task<VinsInfo[]> AddExposuresVinInfo(AutofillRequest request, VinsInfo[] vins, string vinNameSpace, string vinLocation)
    {
        VinsInfo[] vinsWithInfoUpdate = new VinsInfo[request.updates.updateExposures.Length];
        if (_validator.AutofillRequestContainsUpdates(request) &&
                request.updates.updateExposures.Any())
        {
            for (int i = 0; i < request.updates.updateExposures.Length; i++)
            {
                if (request.updates.updateExposures[i].fieldValues.ContainsKey(vinNameSpace)) {
                    vinsWithInfoUpdate[i] = new VinsInfo()
                    {
                        vin = string.Empty,
                        exposureLocator = request.updates.updateExposures[i].exposureLocator?.ToString() ?? ""
                    };

                    if (_validator.CheckPathInAutofillRequest(request, vinNameSpace, vinLocation, i, false))
                    {
                        var vinToLook = request.updates.updateExposures[i].fieldValues?[vinNameSpace]?[0]?.ToString() ?? "";
                        var vinRetrieved = await GetVinsInfo(vinToLook);
                        vinRetrieved.exposureLocator = request.updates.updateExposures[i].exposureLocator?.ToString() ?? "";
                        vinsWithInfoUpdate[i] = vinRetrieved;
                    }
                } else {
                    vinsWithInfoUpdate[i] = new VinsInfo()
                    {
                        vin = EmptyValues.none,
                        exposureLocator = request.updates.updateExposures[i].exposureLocator?.ToString() ?? ""
                    };
                }
            }
        }
        VinsInfo[] vinsWithInfoAdd = new VinsInfo[request.updates.addExposures.Length];
        if (_validator.AutofillRequestContainsUpdates(request) &&
                request.updates.addExposures.Any())
        {
            for (int i = 0; i < request.updates.addExposures.Length; i++)
            {
                if (request.updates.addExposures[i] is not null &&
                    request.updates.addExposures[i].fieldValues is not null &&
                    request.updates.addExposures[i].fieldValues.ContainsKey(vinNameSpace)) {
                    vinsWithInfoAdd[i] = new VinsInfo()
                    {
                        vin = string.Empty,
                        exposureLocator = i.ToString()
                };

                    if (_validator.CheckPathInAutofillRequest(request, vinNameSpace, vinLocation, i, true))
                    {
                        var vinToLook = request.updates.addExposures[i].fieldValues?[vinNameSpace]?[0]?.ToString() ?? "";
                        var vinRetrieved = await GetVinsInfo(vinToLook);
                        vinRetrieved.exposureLocator = i.ToString();
                        vinsWithInfoAdd[i] = vinRetrieved;
                    }
                } else {
                    vinsWithInfoAdd[i] = new VinsInfo()
                    {
                        vin = EmptyValues.none,
                        exposureLocator = i.ToString()
                    };
                }
            }
        }
        VinsInfo[] combinedVins = vinsWithInfoUpdate.Concat(vinsWithInfoAdd).ToArray();
        return combinedVins;
    }
    
    async internal Task<VinsInfo[]> AddFieldGroupVinInfo (AutofillRequest request, VinsInfo[] vins, string vinNameSpace, string fieldGroupName, string vinLocation)
    {
        VinsInfo[] vinsWithInfoUpdate = new VinsInfo[request.updates.updateFieldGroups.Length];
        if (_validator.AutofillRequestContainsUpdates(request) &&
                request.updates.updateFieldGroups.Any())
        {
            for (int i = 0; i < request.updates.updateFieldGroups.Length; i++)
            {
                if (request.updates.updateFieldGroups[i] is not null && 
                    request.updates.updateFieldGroups[i].fieldName == fieldGroupName &&
                    request.updates.updateFieldGroups[i].fieldValues is not null &&
                    request.updates.updateFieldGroups[i].fieldValues.ContainsKey(vinNameSpace)) {
                    vinsWithInfoUpdate[i] = new VinsInfo()
                    {
                        vin = string.Empty,
                        fieldGroupLocator = request.updates.updateFieldGroups[i].fieldGroupLocator?.ToString() ?? ""
                    };

                    if (_validator.CheckPathInAutofillRequest(request, vinNameSpace, vinLocation, i, false))
                    {
                        var vinToLook = request.updates.updateFieldGroups[i].fieldValues?[vinNameSpace]?[0]?.ToString() ?? "";
                        var vinRetrieved = await GetVinsInfo(vinToLook);
                        vinRetrieved.fieldGroupLocator = request.updates.updateFieldGroups[i].fieldGroupLocator?.ToString() ?? "";
                        vinsWithInfoUpdate[i] = vinRetrieved;
                    }
                } else {
                    vinsWithInfoUpdate[i] = new VinsInfo()
                    {
                        vin = EmptyValues.none,
                        fieldGroupLocator = request.updates.updateFieldGroups[i].fieldGroupLocator?.ToString() ?? ""
                    };
                }
            }
        }

        VinsInfo[] vinsWithInfoAdd = new VinsInfo[request.updates.addFieldGroups.Length];
        if (_validator.AutofillRequestContainsUpdates(request) &&
                request.updates.addFieldGroups.Any())
        {
            for (int i = 0; i < request.updates.addFieldGroups.Length; i++)
            {
                if (request.updates.addFieldGroups[i] is not null && 
                    request.updates.addFieldGroups[i].fieldName == fieldGroupName &&
                    request.updates.addFieldGroups[i].fieldValues is not null &&
                    request.updates.addFieldGroups[i].fieldValues.ContainsKey(vinNameSpace)) {
                    vinsWithInfoAdd[i] = new VinsInfo()
                    {
                        vin = string.Empty,
                        fieldGroupLocator = i.ToString()
                };

                    if (_validator.CheckPathInAutofillRequest(request, vinNameSpace, vinLocation, i, true))
                    {
                        var vinToLook = request.updates.addFieldGroups[i].fieldValues?[vinNameSpace]?[0]?.ToString() ?? "";
                        var vinRetrieved = await GetVinsInfo(vinToLook);
                        vinRetrieved.fieldGroupLocator = i.ToString();
                        vinsWithInfoAdd[i] = vinRetrieved;
                    }
                } else {
                    vinsWithInfoAdd[i] = new VinsInfo()
                    {
                        vin = EmptyValues.none,
                        fieldGroupLocator = i.ToString()
                    };
                }
            }
        }
        VinsInfo[] combinedVins = vinsWithInfoUpdate.Concat(vinsWithInfoAdd).ToArray();
        return combinedVins;
    }
    async internal Task<VinsInfo> GetVinsInfo(string vinToLookup)
    {
        CapturedVinFieldValues actualVinResp = await RetrieveVIN(vinToLookup);

        var result = new VinsInfo()
        {
            vin = vinToLookup
        };

        result.values[MappingValues.make] = actualVinResp?.make?.ToString() ?? "";
        result.values[MappingValues.model] = actualVinResp?.model?.ToString() ?? "";
        result.values[MappingValues.year] = actualVinResp?.year?.ToString() ?? "";
        result.values[MappingValues.cylinderNumbers] = actualVinResp?.cylinderNumbers?.ToString() ?? "";
        result.values[MappingValues.engineModel] = actualVinResp?.engineModel?.ToString() ?? "";
        result.values[MappingValues.enginePower] = actualVinResp?.enginePower?.ToString() ?? "";
        result.values[MappingValues.primaryFuelType] = actualVinResp?.primaryFuelType?.ToString() ?? "";
        result.values[MappingValues.frontAirBagLocations] = actualVinResp?.frontAirBagLocations?.ToString() ?? "";
        result.values[MappingValues.sideAirBagLocations] = actualVinResp?.sideAirBagLocations?.ToString() ?? "";

        return result;
    }
    async internal Task<CapturedVinFieldValues> RetrieveVIN(string? vin)
    {
        CapturedVinFieldValues results = new CapturedVinFieldValues();
        var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var httpResponse = await client.GetAsync($"https://vpic.nhtsa.dot.gov/api/vehicles/DecodeVin/{vin}?format=json");
        httpResponse.EnsureSuccessStatusCode();
        string responseJson = await httpResponse.Content.ReadAsStringAsync();

        VinApiResponse? stateData = new VinApiResponse();
        stateData = JsonSerializer.Deserialize<VinApiResponse>(responseJson);
        if (stateData is not null)
        {
            foreach (VinValueResults result in stateData.Results)
            {
                switch (result.Variable)
                {
                    case VinApiValues.make:
                        results.make = result.Value?.ToString() ?? "";
                        break;
                    case VinApiValues.model:
                        results.model = result.Value?.ToString() ?? "";
                        break;
                    case VinApiValues.year:
                        results.year = result.Value?.ToString() ?? "";
                        break;
                    case VinApiValues.cylinderNumbers:
                        results.cylinderNumbers = result.Value?.ToString() ?? "";
                        break;
                    case VinApiValues.engineModel:
                        results.engineModel = result.Value?.ToString() ?? "";
                        break;
                    case VinApiValues.enginePower:
                        results.enginePower = result.Value?.ToString() ?? "";
                        break;
                    case VinApiValues.primaryFuelType:
                        results.primaryFuelType = result.Value?.ToString() ?? "";
                        break;
                    case VinApiValues.frontAirBagLocations:
                        results.frontAirBagLocations = result.Value?.ToString() ?? "";
                        break;
                    case VinApiValues.sideAirBagLocations:
                        results.sideAirBagLocations = result.Value?.ToString() ?? "";
                        break;

                    default:
                        break;
                }
            }
        }
        return results;
    }

    public int CountAllVins (string vinLoc, AutofillResponse resp)
    {
        switch (vinLoc) 
        {
            case PolicyConstants.fieldType.isFieldValueField:
                return 1;
            case PolicyConstants.fieldType.isExposureField:
                int totalExposures = resp.updateExposures.Length + resp.addExposures.Length;
                return totalExposures;
            case PolicyConstants.fieldType.isFieldValueGroup:
                int totalFieldGroups = resp.updateFieldGroups.Length + resp.addFieldGroups.Length;
                return totalFieldGroups;
        }
        return 1;
    }
}