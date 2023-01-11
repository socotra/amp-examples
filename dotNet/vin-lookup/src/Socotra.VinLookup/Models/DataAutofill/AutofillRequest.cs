using System.Collections.Generic;
using System;
namespace Socotra.VinLookup.Models;

public class AutofillRequest
{

    // required
    public string policyholderLocator { get; set; } = string.Empty;
    public string productName { get; set; } = string.Empty;
    public string operation { get; set; } = string.Empty;// string newBusiness | endorsement | renewal | reinstatement | cancellation | manual | feeAssessment
    public string operationType { get; set; } = string.Empty;// string create | update

    public AutofillUpdateRequest updates { get; set; } = new AutofillUpdateRequest();



    // optional
    public string policyLocator { get; set; } = string.Empty;
    public string endorsementLocator { get; set; } = string.Empty;
    public string quoteLocator { get; set; } = string.Empty;
    public string renewalLocator { get; set; } = string.Empty;
    // public int configVersion { get; set; } = 0
}

public class AutofillUpdateRequest
{
    // required

    // timestamp
    public string policyEndTimestamp { get; set; } = string.Empty;
    public string policyStartTimestamp { get; set; } = string.Empty;

    // optional
    public string endorsementName { get; set; } = string.Empty;
    // timestamp
    public string endorsementEffectiveTimestamp { get; set; } = string.Empty;

    public Dictionary<string, string[]> fieldValues { get; set; } = new Dictionary<string, string[]>() { };
    public AutofillFieldGroupCreateRequest[] addFieldGroups { get; set; } = Array.Empty<AutofillFieldGroupCreateRequest>();
    public AutofillFieldGroupUpdateRequest[] updateFieldGroups { get; set; } = Array.Empty<AutofillFieldGroupUpdateRequest>();
    public AutofillExposureUpdateRequest[] updateExposures { get; set; } = Array.Empty<AutofillExposureUpdateRequest>();
    public AutofillExposureCreateRequest[] addExposures { get; set; } = Array.Empty<AutofillExposureCreateRequest>();


}