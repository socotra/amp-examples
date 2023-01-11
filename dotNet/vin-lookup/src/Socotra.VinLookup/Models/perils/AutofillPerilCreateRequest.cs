using System.Collections.Generic;

namespace Socotra.VinLookup.Models;

public class AutofillPerilCreateRequest
{
    // required
    public string? name { get; set; }

    public FieldGroupCreateRequest? fieldGroups { get; set; }

    // optional
    public Dictionary<string, string[]>? fieldValues { get; set; }
}