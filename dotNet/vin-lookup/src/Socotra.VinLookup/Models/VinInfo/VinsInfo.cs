using System.Collections.Generic;

namespace Socotra.VinLookup.Models;

public class VinsInfo
{
    public string vin { get; set; } = String.Empty;

    public string exposureLocator { get; set; } = String.Empty;

    public string perilLocator { get; set; } = String.Empty;

    public string fieldGroupLocator { get; set; } = String.Empty;

    public Dictionary<string, string> values { get; set; } = new Dictionary<string, string>(){
        {"make", ""},
        {"year", ""},
        {"model", ""},
        {"cylinderNumbers", ""},
        {"engineModel", ""},
        {"enginePower", ""},
        {"primaryFuelType", ""},
        {"frontAirBagLocations", ""},
        {"sideAirBagLocations", ""}
    };
}