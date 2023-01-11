using System.Collections.Generic;
public static class MockedMappingsExposures {
    public const string productName = "personal-auto";
    public static readonly Dictionary<string, string> fields = new Dictionary<string, string>() {
        {"vin", "$[?(@.name=='personal-auto')].policyConfiguration.exposures[?(@.name=='vehicle')].fields[?(@.name=='vehicleIdNum')]"},
        {"make", "$[?(@.name=='personal-auto')].policyConfiguration.exposures[?(@.name=='vehicle')].fields[?(@.name=='MakeMcdonalds')]"},
        {"year", "$[?(@.name=='personal-auto')].policyConfiguration.exposures[?(@.name=='drivers')].fields[?(@.name=='year')]"},
        {"model", "$[?(@.name=='personal-auto')].policyConfiguration.exposures[?(@.name=='vehicle')].fields[?(@.name=='model')]"},
        {"cylinderNumbers", "$[?(@.name=='personal-auto')].policyConfiguration.exposures[?(@.name=='vehicle')].fields[?(@.name=='cylinderNumbers')]"},
        {"engineModel", "$[?(@.name=='personal-auto')].policyConfiguration.exposures[?(@.name=='vehicle')].fields[?(@.name=='engineModel')]"},
        {"enginePower", "$[?(@.name=='personal-auto')].policyConfiguration.exposures[?(@.name=='vehicle')].fields[?(@.name=='enginePower')]"},
        {"primaryFuelType", "$[?(@.name=='personal-auto')].policyConfiguration.exposures[?(@.name=='vehicle')].fields[?(@.name=='primaryFuelType')]"},
        {"frontAirBagLocations", "$[?(@.name=='personal-auto')].policyConfiguration.exposures[?(@.name=='vehicle')].fields[?(@.name=='frontAirBagLocations')]"},
        {"sideAirBagLocations", "$[?(@.name=='personal-auto')].policyConfiguration.exposures[?(@.name=='vehicle')].fields[?(@.name=='sideAirBagLocations')]"}
    };
}