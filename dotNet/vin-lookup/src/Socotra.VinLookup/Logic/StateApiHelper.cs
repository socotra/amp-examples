using System.Net.Http.Headers;
using System.Text.Json;
using Socotra.VinLookup.Models;

namespace Socotra.VinLookup.Logic;
public class StateApiHelper
{
    private readonly Validator _validator = new Validator();
    async internal Task<StateApiResponse> FetchState(String xSmpKey)
    {
        var stateApiBaseAddress = Environment.GetEnvironmentVariable("SMP_STATE_API");

        if (stateApiBaseAddress is null)
        {
            var response = new StateApiResponse();
            // NOTE: To test field-value payload, use "Models/StateApi/MockStateApi/MockedMappingsFieldVals"
            // NOTE: To test field-groups paylod, "Models/StateApi/MockStateApi/MockedMappingsFieldGroups"
            // NOTE: To test field-groups paylod, "Models/StateApi/MockStateApi/MockedMappingsExposures"
            response.mappings[0].productName = MockedMappingsFieldGroups.productName;
            response.mappings[0].fields = MockedMappingsFieldGroups.fields;
            return response;
        }

        var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var httpResponse = await client.GetAsync($"{stateApiBaseAddress}/state/{xSmpKey}");
        httpResponse.EnsureSuccessStatusCode();

        string responseJson = await httpResponse.Content.ReadAsStringAsync();
        StateApiResponse StateApiResp = JsonSerializer.Deserialize<StateApiResponse>(responseJson) ?? new StateApiResponse();

        return StateApiResp;
    }

    public string getGroupName (string path) {
        if (_validator.ValueIsPresent(path))
        {
            string[] pathName = path.Split("].");
            int initialIndx = pathName[^1].IndexOf("'");
            string val = pathName[pathName.Length - 2];
            string firstParse = val.Substring(
                initialIndx + 1
            );
            int secondIndx = firstParse.LastIndexOf("'");
            string secondName = firstParse.Substring(
                0, secondIndx
            );
            int thirdIndx = secondName.IndexOf("'");

            string groupName = secondName.Substring(
                thirdIndx + 1
            );
            
            return groupName;
        }
        return "";
    }

    public string GetFieldName(string path)
    {
        if (_validator.ValueIsPresent(path))
        {
            string[] pathName = path.Split("].");
            int initialIndx = pathName[^1].IndexOf("'");
            string val = pathName[pathName.Length - 1];
            string firstParse = val.Substring(
                initialIndx + 1
            );
            int secondIndx = firstParse.LastIndexOf("'");
            string fieldName = firstParse.Substring(
                0, secondIndx
            );
            return fieldName;
        }
        return "";
    }

    // function that if given a mapping-path, return's path for the mappings from the state-api
    public string GetMappingPath(StateApiResponse StateApiResp, string name)
    {
        if (StateApiResp.mappings[0].fields.ContainsKey(name))
        {
            return StateApiResp.mappings[0].fields?[name] ?? "";
        }
        return "";
    }

    // function that tells if value is a in top-level field-Values, field-group, exposure, and etc.,
    public string FindPathType(string path)
    {
        string type = "";
        if (_validator.ValueIsPresent(path))
        {
            string[] pathPieces = path.Split("].");
            int fieldCount = 0;
            int exposureCount = 0;
            int perilCount = 0;
            foreach (string name in pathPieces)
            {
                fieldCount += name.Contains(PolicyConstants.fields) ? 1 : 0;
                exposureCount += name.Contains(PolicyConstants.exposures) ? 1 : 0;
                perilCount += name.Contains(PolicyConstants.perils) ? 1 : 0;
            }

            switch (fieldCount, exposureCount, perilCount)
            {
                case (2, 0, 0):
                    return PolicyConstants.fieldType.isFieldValueGroup;
                case (2, 1, 0):
                    return PolicyConstants.fieldType.isExposureGroup;
                case (2, 1, 1):
                    return PolicyConstants.fieldType.isExposurePerilGroup;
                case (1, 1, 0):
                    return PolicyConstants.fieldType.isExposureField;
                case (1, 1, 1):
                    return PolicyConstants.fieldType.isExposurePerilField;
                case (1, 0, 0):
                    return PolicyConstants.fieldType.isFieldValueField;

            }
        }
        return type;
    }
}