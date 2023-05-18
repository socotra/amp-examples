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

    public bool EnsureVinFieldIsContained (VinsInfo vinInfo) {
        if (vinInfo is not null && vinInfo.vin == EmptyValues.none) {
            return false;
        }
        return true;
    }
    public bool CheckPathInExposures (AutofillRequest request, string vinNameSpace, int idx = 0, bool isAdd = false) 
    {
        if (isAdd) {
            return request.updates?.addExposures is not null
                                    && idx >= 0
                                    && request.updates.addExposures[idx] is not null
                                    && request.updates.addExposures[idx].fieldValues is not null
                                    && request.updates.addExposures[idx].fieldValues.ContainsKey(vinNameSpace)
                                    && vinNameSpace is not null
                                    && request.updates.addExposures[idx].fieldValues[vinNameSpace]!.Any()
                                    && ValueIsPresent(request.updates.addExposures[idx].fieldValues[vinNameSpace]![0].ToString());
        } 
        return request.updates?.updateExposures is not null
                                    && idx >= 0
                                    && request.updates.updateExposures[idx] is not null
                                    && request.updates.updateExposures[idx].fieldValues is not null
                                    && request.updates.updateExposures[idx].fieldValues.ContainsKey(vinNameSpace)
                                    && vinNameSpace is not null
                                    && request.updates.updateExposures[idx].fieldValues[vinNameSpace]!.Any()
                                    && ValueIsPresent(request.updates.updateExposures[idx].fieldValues[vinNameSpace]![0].ToString());
    }
    
    public bool CheckPathInExposureFG (AutofillRequest request, string vinNameSpace, int idx = 0, bool isAdd = false, int idxFG = 0, bool isAddOnUpdateExposure = false)
    {
        if (isAdd) {
            return request.updates?.addExposures is not null
                                    && idx >= 0
                                    && idxFG >= 0
                                    && request.updates.addExposures[idx] is not null
                                    && request.updates.addExposures[idx].fieldGroups[idxFG] is not null
                                    && request.updates.addExposures[idx].fieldGroups[idxFG].fieldValues.ContainsKey(vinNameSpace)
                                    && vinNameSpace is not null
                                    && request.updates.addExposures[idx].fieldGroups[idxFG].fieldValues[vinNameSpace]!.Any()
                                    && ValueIsPresent(request.updates.addExposures[idx].fieldGroups[idxFG].fieldValues[vinNameSpace]![0].ToString());
        } 
        if (isAddOnUpdateExposure) {
            return request.updates?.updateExposures is not null
                                    && idx >= 0
                                    && idxFG >= 0
                                    && request.updates.updateExposures[idx] is not null
                                    && request.updates.updateExposures[idx].addFieldGroups[idxFG] is not null
                                    && request.updates.updateExposures[idx].addFieldGroups[idxFG].fieldValues.ContainsKey(vinNameSpace)
                                    && vinNameSpace is not null
                                    && request.updates.updateExposures[idx].addFieldGroups[idxFG].fieldValues[vinNameSpace]!.Any()
                                    && ValueIsPresent(request.updates.updateExposures[idx].addFieldGroups[idxFG].fieldValues[vinNameSpace]![0].ToString());
        }
        return request.updates?.updateExposures is not null
                                    && idx >= 0
                                    && idxFG >= 0
                                    && request.updates.updateExposures[idx] is not null
                                    && request.updates.updateExposures[idx].updateFieldGroups[idxFG] is not null
                                    && request.updates.updateExposures[idx].updateFieldGroups[idxFG].fieldValues.ContainsKey(vinNameSpace)
                                    && vinNameSpace is not null
                                    && request.updates.updateExposures[idx].updateFieldGroups[idxFG].fieldValues[vinNameSpace]!.Any()
                                    && ValueIsPresent(request.updates.updateExposures[idx].updateFieldGroups[idxFG].fieldValues[vinNameSpace]![0].ToString());
    }
    public bool CheckPathInFieldGroups (AutofillRequest request, string vinNameSpace, int idx = 0, bool isAdd = false) 
    {
        if (isAdd) {
            return request.updates?.addFieldGroups is not null
                                && idx >= 0
                                && request.updates.addFieldGroups[idx] is not null
                                && request.updates.addFieldGroups[idx].fieldValues is not null
                                && request.updates.addFieldGroups[idx].fieldValues.ContainsKey(vinNameSpace)
                                && vinNameSpace is not null
                                && request.updates.addFieldGroups[idx].fieldValues[vinNameSpace]!.Any()
                                && ValueIsPresent(request.updates.addFieldGroups[idx].fieldValues[vinNameSpace]![0].ToString());
        }
        return request.updates?.updateFieldGroups is not null
                                && idx >= 0
                                && request.updates.updateFieldGroups[idx] is not null
                                && request.updates.updateFieldGroups[idx].fieldValues is not null
                                && request.updates.updateFieldGroups[idx].fieldValues.ContainsKey(vinNameSpace)
                                && vinNameSpace is not null
                                && request.updates.updateFieldGroups[idx].fieldValues[vinNameSpace]!.Any()
                                && ValueIsPresent(request.updates.updateFieldGroups[idx].fieldValues[vinNameSpace]![0].ToString());
    }

     public bool CheckPathInFieldValues (AutofillRequest request, string vinNameSpace) 
    {
        return request.updates?.fieldValues is not null
                                && request.updates.fieldValues.ContainsKey(vinNameSpace)
                                && ((request.updates.fieldValues[vinNameSpace]).Any())
                                && (ValueIsPresent(request.updates.fieldValues[vinNameSpace][0]));
    }

    public bool CheckPathInAutofillRequest(AutofillRequest request, string vinNameSpace, string vinLocation, int idx = 0, bool isAdd = false, int idxFG = 0, bool isAddOnUpdateExposure = false)
    {
        // TODO: Move this logic to individual functions 
        switch (vinLocation)
        {
            case PolicyConstants.fieldType.isFieldValueField:
                return CheckPathInFieldValues(request, vinNameSpace);
            case PolicyConstants.fieldType.isFieldValueGroup:
                return CheckPathInFieldGroups(request, vinNameSpace, idx, isAdd);
            case PolicyConstants.fieldType.isExposureField:
                return CheckPathInExposures(request, vinNameSpace, idx, isAdd);
            case PolicyConstants.fieldType.isExposureGroup:
                return CheckPathInExposureFG(request, vinNameSpace, idx, isAdd, idxFG, false);

        }
        return false;

    }
}