using System.Collections.Generic;

namespace Socotra.VinLookup.Models;

public class FieldGroupCreateRequest
{
    // required 
    public string? fieldName { get; set; }

    // optional
    public Dictionary<string, string[]?> fieldValues { get; set; } = new Dictionary<string, string[]?>() { };
}