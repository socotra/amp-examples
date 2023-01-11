using System.Collections.Generic;
using System;
namespace Socotra.VinLookup.Models;

public class AutofillResponse
{
    // required
    public Dictionary<string, string[]> fieldValues { get; set; } = new Dictionary<string, string[]> { };

    // optional

    //timestamp
    public string endorsementEffectiveTimestamp { get; set; } = string.Empty;
    public string policyEndTimestamp { get; set; } = string.Empty;
    public string policyStartTimestamp { get; set; } = string.Empty;

    public AutofillFieldGroupCreateRequest[] addFieldGroups { get; set; } = Array.Empty<AutofillFieldGroupCreateRequest>();
    public AutofillFieldGroupUpdateRequest[] updateFieldGroups { get; set; } = Array.Empty<AutofillFieldGroupUpdateRequest>();
    public AutofillExposureUpdateRequest[] updateExposures { get; set; } = Array.Empty<AutofillExposureUpdateRequest>();
    public AutofillExposureCreateRequest[] addExposures { get; set; } = Array.Empty<AutofillExposureCreateRequest>();
}