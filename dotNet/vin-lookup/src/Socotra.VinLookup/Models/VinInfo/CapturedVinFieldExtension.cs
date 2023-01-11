namespace Socotra.VinLookup.Models;

public static class CapturedVinFieldExtension
{

    public static string MapField(this CapturedVinFieldValues data, string fieldToGet)
    {
        if (data is null)
            throw new ArgumentNullException(nameof(CapturedVinFieldValues), $"Request object is null, can't map the details.");

        if (string.IsNullOrWhiteSpace(fieldToGet))
            throw new ArgumentNullException(nameof(fieldToGet), $"FieldToGet is null or empty, can't map the details.");

        var result = fieldToGet switch
        {
            MappingValues.make => data.make?.ToString() ?? string.Empty,
            MappingValues.model => data.model?.ToString() ?? string.Empty,
            MappingValues.year => data.year?.ToString() ?? string.Empty,
            _ => string.Empty
        };

        return result;
    }


}