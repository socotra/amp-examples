using System.Collections.Generic;
using System;
namespace Socotra.VinLookup.Models;

public class AutofillPerilUpdateRequest
{
    // required
    public AutofillFieldGroupCreateRequest[] addFieldGroups { get; set; } = Array.Empty<AutofillFieldGroupCreateRequest>();
    public AutofillFieldGroupUpdateRequest[] updateFieldGroups { get; set; } = Array.Empty<AutofillFieldGroupUpdateRequest>();

    public string? perilLocator { get; set; }

    // optional
    public Dictionary<string, string[]>? fieldValues { get; set; }
}