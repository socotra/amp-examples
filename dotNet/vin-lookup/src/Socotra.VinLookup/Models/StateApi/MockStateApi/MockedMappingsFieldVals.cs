using System.Collections.Generic;
public static class MockedMappingsFieldVals {
    public const string productName = "personal-auto";
    public static readonly Dictionary<string, string> fields = new Dictionary<string, string>() {
        {"vin", "$[?(@.name=='personal-auto')].policyConfiguration.fields[?(@.name=='vehicleIdNum')]"},
        {"make", "$[?(@.name=='personal-auto')].policyConfiguration.fields[?(@.name=='MakeMcdonalds')]"},
        {"year", "$[?(@.name=='personal-auto')].policyConfiguration.fields[?(@.name=='year')]"},
        {"model", "$[?(@.name=='personal-auto')].policyConfiguration.fields[?(@.name=='model')]"},
        {"cylinderNumbers", "$[?(@.name=='personal-auto')].policyConfiguration.fields[?(@.name=='cylinderNumbers')]"},
        {"engineModel", "$[?(@.name=='personal-auto')].policyConfiguration.fields[?(@.name=='engineModel')]"},
        {"enginePower", "$[?(@.name=='personal-auto')].policyConfiguration.fields[?(@.name=='enginePower')]"},
        {"primaryFuelType", "$[?(@.name=='personal-auto')].policyConfiguration.fields[?(@.name=='primaryFuelType')]"},
        {"frontAirBagLocations", "$[?(@.name=='personal-auto')].policyConfiguration.fields[?(@.name=='frontAirBagLocations')]"},
        {"sideAirBagLocations", "$[?(@.name=='personal-auto')].policyConfiguration.fields[?(@.name=='sideAirBagLocations')]"}
    };
}