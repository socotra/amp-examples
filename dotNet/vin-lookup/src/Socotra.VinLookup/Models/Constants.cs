using System.Collections.Generic;

namespace Socotra.VinLookup.Models;
public static class VinApiValues
{
    public const string make = "Make";
    public const string model = "Model";
    public const string year = "Model Year";

    public const string cylinderNumbers = "Engine Number of Cylinders";
    public const string engineModel = "Engine Model";
    public const string enginePower = "Engine Power (kW)";
    public const string primaryFuelType = "Fuel Type - Primary";
    public const string frontAirBagLocations = "Front Air Bag Locations";
    public const string sideAirBagLocations = "Side Air Bag Locations";
}


public static class PossibleStateApiMappings {
    public static string [] MappingValuesList = new string[9] 
    { MappingValues.make, MappingValues.model, MappingValues.year, MappingValues.cylinderNumbers, MappingValues.frontAirBagLocations, MappingValues.sideAirBagLocations, MappingValues.enginePower, MappingValues.primaryFuelType, MappingValues.engineModel };
}
public static class MappingValues
{
    public const string vin = "vin";
    public const string make = "make";
    public const string model = "model";
    public const string year = "year";

    public const string cylinderNumbers = "cylinderNumbers";
    public const string engineModel = "engineModel";
    public const string enginePower = "enginePower";
    public const string primaryFuelType = "primaryFuelType";
    public const string frontAirBagLocations = "frontAirBagLocations";
    public const string sideAirBagLocations = "sideAirBagLocations";
}

public static class mockApi
{
    public static Dictionary<string, string>? settings { get; set; }

    public const string productName = "personal-auto";
    public static Dictionary<string, string>? fields = new Dictionary<string, string>()
    {
        {"vin", "$[?(@.name=='personal-auto')].policyConfiguration.exposures[?(@.name=='vehicle')].fields[?(@.name=='vehicleIdNum')]"},
        {"make", "$[?(@.name=='personal-auto')].policyConfiguration.exposures[?(@.name=='vehicle')].fields[?(@.name=='mCDONALSDA')]"},
        {"year", "$[?(@.name=='personal-auto')].policyConfiguration.fields[?(@.name=='drivers')].fields[?(@.name=='year')]"},
        {"model", "$[?(@.name=='personal-auto')].policyConfiguration.exposures[?(@.name=='vehicle')].fields[?(@.name=='model')]"}
    };

    public const string socotraApiUrl = "https://example/url/here";

    public const string tenantHostName = "socotra";

    public const string token = "abcdefgh";
}

public static class PolicyConstants
{
    public const string exposures = "exposures";

    public const string fields = "fields";

    public const string perils = "perils";

    public const string policyConfiguration = "policyConfiguration";

    public static class fieldType
    {

        public const string isFieldValueGroup = "isFieldValueGroup";

        public const string isExposureGroup = "isExposureGroup";

        public const string isExposurePerilGroup = "isExposurePerilGroup";

        public const string isFieldValueField = "isFieldValueField";

        public const string isExposureField = "isExposureField";
        public const string isExposurePerilField = "isExposurePerilField";

    }
}