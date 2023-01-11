using System.Net.Http.Headers;
using System.Text.Json;
using Socotra.VinLookup.Models;

namespace Socotra.VinLookup.Logic;

public class VinApiHelper
{
    private readonly Validator _validator = new Validator();

    async internal Task<VinsInfo> AddFieldValueVinInfo(AutofillRequest request, string vinNameSpace)
    {
        var result = new VinsInfo
        {
            vin = string.Empty
        };
        
        if (_validator.CheckPathInAutofillRequest(request, vinNameSpace, false))
        {
            var vinToLookup = request.updates.fieldValues[vinNameSpace].First();
            result = await GetVinsInfo(vinToLookup);
        }

        return result;
    }

    async internal Task<VinsInfo[]> AddExposuresVinInfo(AutofillRequest request, VinsInfo[] vins, string vinNameSpace)
    {
        var vinsWithInfo = vins;
        if (_validator.AutofillRequestContainsUpdates(request) &&
                request.updates.updateExposures.Any())
        {
            for (int i = 0; i < request.updates.updateExposures.Length; i++)
            {
                vinsWithInfo[i] = new VinsInfo()
                {
                    vin = string.Empty,
                    exposureLocator = request.updates.updateExposures[i].exposureLocator?.ToString() ?? ""
                };

                if (_validator.CheckPathInAutofillRequest(request, vinNameSpace, true, i))
                {
                    var vinToLook = request.updates.updateExposures[i].fieldValues?[vinNameSpace]?[0]?.ToString() ?? "";
                    var vinRetrieved = await GetVinsInfo(vinToLook);
                    vinRetrieved.exposureLocator = request.updates.updateExposures[i].exposureLocator?.ToString() ?? "";
                    vinsWithInfo[i] = vinRetrieved;
                }

            }
        }
        return vinsWithInfo;
    }
    async internal Task<VinsInfo> GetVinsInfo(string vinToLookup)
    {
        CapturedVinFieldValues actualVinResp = await RetrieveVIN(vinToLookup);

        var result = new VinsInfo()
        {
            vin = vinToLookup
        };

        //var result = AutoMapper.Map<CapturedVinFieldValues,VinsInfo>(actualVinResp);
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

    internal int CountAllVins(string vinLoc, AutofillResponse resp) => vinLoc != PolicyConstants.fieldType.isFieldValueField ? resp.updateExposures.Length : 1;
}