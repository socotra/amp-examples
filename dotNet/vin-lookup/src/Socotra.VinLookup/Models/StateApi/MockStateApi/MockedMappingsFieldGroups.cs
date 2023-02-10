using System.Collections.Generic;
public static class MockedMappingsFieldGroups {
    public const string productName = "personal-auto";
    public static readonly Dictionary<string, string> fields = new Dictionary<string, string>() {
        {"vin", "$[?(@.name=='personal-auto')].policyConfiguration.fields[?(@.name=='vinFG')].fields[?(@.name=='vin')]"},
        {"make", "$[?(@.name=='personal-auto')].policyConfiguration.fields[?(@.name=='vinFG')].fields[?(@.name=='make')]"},
        {"year", "$[?(@.name=='personal-auto')].policyConfiguration.fields[?(@.name=='vinFG')].fields[?(@.name=='year')]"},
        {"model", "$[?(@.name=='personal-auto')].policyConfiguration.fields[?(@.name=='vinFG')].fields[?(@.name=='model')]"},
        {"cylinderNumbers", "$[?(@.name=='personal-auto')].policyConfiguration.fields[?(@.name=='vinFG')].fields[?(@.name=='cylinderNumbers')]"},
        {"engineModel", "$[?(@.name=='personal-auto')].policyConfiguration.fields[?(@.name=='vinFG')].fields[?(@.name=='engineModel')]"},
        {"enginePower", "$[?(@.name=='personal-auto')].policyConfiguration.fields[?(@.name=='vinFG')].fields[?(@.name=='enginePower')]"},
        {"primaryFuelType", "$[?(@.name=='personal-auto')].policyConfiguration.fields[?(@.name=='vinFG')].fields[?(@.name=='primaryFuelType')]"},
        {"frontAirBagLocations", "$[?(@.name=='personal-auto')].policyConfiguration.fields[?(@.name=='vinFG')].fields[?(@.name=='frontAirBagLocations')]"},
        {"sideAirBagLocations", "$[?(@.name=='personal-auto')].policyConfiguration.fields[?(@.name=='vinFG')].fields[?(@.name=='sideAirBagLocations')]"}
    };
}