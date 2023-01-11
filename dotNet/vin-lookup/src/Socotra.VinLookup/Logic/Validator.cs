using Socotra.VinLookup.Models;

namespace Socotra.VinLookup.Logic;

public class Validator
{
    public bool AutofillRequestContainsUpdates(AutofillRequest request)
    {
        return request.updates is not null;
    }

    public bool AutofillRequestContainsFieldValues(Dictionary<string, string[]> fieldValues)
    {
        return fieldValues is not null;
    }

    public bool HasItems<T>(T[] values)
    {
        var hasItems = values is not null && values.Length > 0;
        return hasItems;
    }

    public bool ValueIsPresent(string value)
    {
        var isPresent = value is not null && value.Length > 0;
        return isPresent;
    }

    public bool MatchingValues(string value1, string value2)
    {
        var isMatching = value1 == value2;
        return isMatching;
    }

    public bool CheckPathInAutofillRequest(AutofillRequest request, string vinNameSpace, bool isExposureField = false, int idx = 0)
    {
        if (isExposureField == false) {
            var pathExistsInFieldVals = request.updates?.fieldValues is not null
                                && request.updates.fieldValues.ContainsKey(vinNameSpace)
                                && ((request.updates.fieldValues[vinNameSpace]).Any())
                                && (ValueIsPresent(request.updates.fieldValues[vinNameSpace][0]));
            return pathExistsInFieldVals;
        }
        var pathExistsInExposure = request.updates?.updateExposures is not null
                                && idx >= 0
                                && request.updates.updateExposures[idx] is not null
                                && request.updates.updateExposures[idx].fieldValues.ContainsKey(vinNameSpace)
                                && request.updates.updateExposures[idx].fieldValues is not null
                                && vinNameSpace is not null 
                                && request.updates.updateExposures[idx].fieldValues[vinNameSpace] is not null
                                && request.updates.updateExposures[idx].fieldValues[vinNameSpace].Any()
                                && ValueIsPresent(request.updates.updateExposures[idx].fieldValues[vinNameSpace][0].ToString());
        return pathExistsInExposure;

    }
}