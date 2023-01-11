using System.Collections.Generic;

namespace Socotra.VinLookup.Models;

public class AutofillFieldGroupUpdateRequest
{
    // required
    public string? fieldGroupLocator { get; set; }
    public string? fieldName { get; set; }
    // optional
    public Dictionary<string, string[]>? fieldValues { get; set; }
}