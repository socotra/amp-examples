using System.Collections.Generic;

namespace Socotra.VinLookup.Models;

public class PerilCreateRequest
{
    // required
    public string? name { get; set; }

    // optional
    public string? locator { get; set; }
    public Dictionary<string, string[]>? fieldValues { get; set; }
    public float? number { get; set; }

    public float? lumpSumPayment { get; set; }

    // fieldGroups [FieldGroupCreateRequest]

    public string? indemnityInAggregate { get; set; }
    public string? indemnityPerEvent { get; set; }

    public string? indemnityPerItem { get; set; }
}