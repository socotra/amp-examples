using System.Collections.Generic; 
using System;

namespace Socotra.VinLookup.Models;

public class VinApiResponse
{
    public int? Count { get; set; } = null;
    public string? Message { get; set; } = string.Empty;
    public string? SearchCriteria { get; set; } = string.Empty;

    public VinValueResults[] Results { get; set; } = Array.Empty<VinValueResults>();
}

public class VinValueResults
{
   public string? Value { get; set; } = string.Empty;
    public string? ValueId { get; set; } = string.Empty;
    public string? Variable { get; set; } = string.Empty;
    public int? VariableId { get; set; } = null;
}
public class sortedResults
{
    public string? make { get; set; } = string.Empty;
    public string? model { get; set; } = string.Empty;
    public string? year { get; set; } = string.Empty;

    public string? cylinderNumbers { get; set; } = string.Empty;
    public string? engineModel { get; set; } = string.Empty;
    public string? enginePower { get; set; } = string.Empty;

    public string? primaryFuelType { get; set; } = string.Empty;
    public string? frontAirBagLocations { get; set; } = string.Empty;
    public string? sideAirBagLocations { get; set; } = string.Empty;
    
}