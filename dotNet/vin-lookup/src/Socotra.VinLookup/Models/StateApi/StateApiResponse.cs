using System.Collections.Generic;
using System;

namespace Socotra.VinLookup.Models;
public class StateApiResponse
{

    public Dictionary<string, string>? settings { get; set; } = new Dictionary<string, string> { };

    public Mappings[] mappings { get; set; } = new Mappings[]{
         new Mappings()
    };

    // public Mappings[] mappings { get; set; } = Array.Empty<Mappings>();

    public string? socotraApiUrl { get; set; } = "https://example/url/here";

    public string? tenantHostName { get; set; } = "socotra";

    public string? token { get; set; } = "abcdefgh";

}