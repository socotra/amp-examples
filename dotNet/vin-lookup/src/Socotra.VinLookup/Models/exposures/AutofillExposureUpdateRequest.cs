using System.Collections.Generic;
using System;
namespace Socotra.VinLookup.Models;

public class AutofillExposureUpdateRequest
{
    // required
    public AutofillFieldGroupCreateRequest[] addFieldGroups { get; set; } = Array.Empty<AutofillFieldGroupCreateRequest>();
    public AutofillFieldGroupUpdateRequest[] updateFieldGroups { get; set; } = Array.Empty<AutofillFieldGroupUpdateRequest>();
    public AutofillPerilCreateRequest[] addPerils { get; set; } = Array.Empty<AutofillPerilCreateRequest>();
    public AutofillPerilUpdateRequest[] updatePerils { get; set; } = Array.Empty<AutofillPerilUpdateRequest>();
    public string? exposureLocator { get; set; }

    // optional
    public Dictionary<string, string[]?> fieldValues { get; set; } = new Dictionary<string, string[]?>() { };
}