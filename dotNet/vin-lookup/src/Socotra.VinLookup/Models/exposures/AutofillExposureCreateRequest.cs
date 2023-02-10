using System.Collections.Generic;
using System;

namespace Socotra.VinLookup.Models;

public class AutofillExposureCreateRequest
{
    // requires
    public string? exposureName { get; set; }

    public PerilCreateRequest[] perils { get; set; } = Array.Empty<PerilCreateRequest>();
    public AutofillFieldGroupUpdateRequest[] updateFieldGroups { get; set; } = Array.Empty<AutofillFieldGroupUpdateRequest>();

    public FieldGroupCreateRequest[] fieldGroups { get; set; } = Array.Empty<FieldGroupCreateRequest>();

    // optional
    public Dictionary<string, string[]?> fieldValues { get; set; } = new Dictionary<string, string[]?>() { };
}