using System.Collections.Generic;

namespace Socotra.VinLookup.Models;

public class AutofillFieldGroupCreateRequest
{
    // required
    public string? fieldName { get; set; }

    // optional
    public Dictionary<string, string[]>? fieldValues { get; set; }
}