export const MockedStateRespMappedExposures = {
    "socotraApiUrl": "https://api.sandbox.socotra.com",
    "tenantHostName": "lahrens-socotra-configeditor.co.sandbox.socotra.com",
    "token": "eyJhbGciOiJIUzI1NiJ9.eyJhY2NvdW50LnR5cGUiOiJhY2NvdW50LnRlbmFudC5lbXBsb3llZSIsInRlbmFudC5uYW1lIjoiYjc5MWFkNzMtNmNhZS00ZWU1LWI0ZGMtNGJhNGNkMGZhN2ZjIiwib250b2xvZ3kudXVpZCI6IjExZWQtYTk3OS1lN2EzMDIxNi1hNTY4LTliYmNmMWVkYWE3OSIsInN1YiI6ImEwOTI0NDkzLTI4ZTktNDc3ZC1hOThjLTE3MzU3YjE0Y2VkNiIsImFjY291bnQubmFtZSI6IkFsaWNlIExlZSIsImFjY291bnQudXVpZCI6ImEwOTI0NDkzLTI4ZTktNDc3ZC1hOThjLTE3MzU3YjE0Y2VkNiIsInRlbmFudC51dWlkIjoiYmEwNTljMDQtMWU4ZS00OTYyLTkzZGItNGM3NWMxOWEwMzc3IiwidGVuYW50LnR5cGUiOiJ0ZW5hbnQudGVzdCIsImV4cCI6MTY3NzYyMzQ5NywidGVuYW50LnRpbWV6b25lIjoiQW1lcmljYVwvTG9zX0FuZ2VsZXMiLCJpYXQiOjE2Nzc1MzcwOTd9.XfQF0KQEhiaD4SSfntf4T5l-79e8yYCLUTi3o9U06wA",
    "settings": {},
    "mappings": [
    {
        "productName": "personal-auto",
        "fields": {
        "vin": "$[?(@.name=='personal-auto')].policyConfiguration.exposures[?(@.name=='vehicle')].fields[?(@.name=='vehicleIdNum')]",
        "Make": "$[?(@.name=='personal-auto')].policyConfiguration.exposures[?(@.name=='vehicle')].fields[?(@.name=='MakeMcdonalds')]",
        "Model_Year": "$[?(@.name=='personal-auto')].policyConfiguration.exposures[?(@.name=='drivers')].fields[?(@.name=='year')]",
        "Model": "$[?(@.name=='personal-auto')].policyConfiguration.exposures[?(@.name=='vehicle')].fields[?(@.name=='model')]",
        "Engine_Number_of_Cylinders": "$[?(@.name=='personal-auto')].policyConfiguration.exposures[?(@.name=='vehicle')].fields[?(@.name=='cylinderNumbers')]",
        "Engine_Model": "$[?(@.name=='personal-auto')].policyConfiguration.exposures[?(@.name=='vehicle')].fields[?(@.name=='engineModel')]",
        "Engine_Power_kW": "$[?(@.name=='personal-auto')].policyConfiguration.exposures[?(@.name=='vehicle')].fields[?(@.name=='enginePower')]",
        "Fuel_Type_Primary": "$[?(@.name=='personal-auto')].policyConfiguration.exposures[?(@.name=='vehicle')].fields[?(@.name=='primaryFuelType')]",
        "Front_Air_Bag_Locations": "$[?(@.name=='personal-auto')].policyConfiguration.exposures[?(@.name=='vehicle')].fields[?(@.name=='frontAirBagLocations')]",
        "Side_Air_Bag_Locations": "$[?(@.name== 'personal-auto')].policyConfiguration.exposures[?(@.name=='vehicle')].fields[?(@.name== 'sideAirBagLocations')]"
        }
        
    }
    ]
}