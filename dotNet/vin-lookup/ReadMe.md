## Description
---
C# (.NET Core) application that hosts an endpoint that takes in a Socotra AutofillPayload, uses NHTSA api to lookup for the provided VIN, then builds and returns an AutofillResponse.

- Using the mapping path of a Socotra product, dictated in the app configuration, we are able to find a VIN located in the AutofillPayload.

- The vin field may reside within the policy's top-level 'fieldValues' or within a policy's 'exposures'.

- Wherever the vin is, the mapping-values (make, model, year, etc.,) must also reside.

- Current mapped values supported are make, model, year, cylinderNumbers, sideAirBagLocations, frontAirBagLocations, enginePower, engineModel, and primaryFuelType.

- Sample Socotra AutofillPayloads provided in `src/Socotra.VinLookup/Models/AutofillPayloadExamples.json`

## Create App Listing 
---
 * Visit https://marketplace.socotra.com/publish
   * Click **Publish New App**
     * Proceed to create your app-listing
       * Replace `"name"` in *socotra-app.json* with your `{organizationName}/{app-name}`
 * This is the listing that we will publish docker container to

## Installation
---
```bash
# note: client-id + secret-api key can be configured from socotra marketplace dashboard
$ npm i @socotra/app-cli
$ socotra-app login
```

## Running the app locally
---
```bash
# build project and its dependencies
$ dotnet build
# start and build docker container 
$ make dr-start
# test locally 
$ curl -d "@src/Socotra.VinLookup/Models/AutofillPayloadExamples/FieldValues.json" -H "Content-Type: application/json" -X POST http://127.0.0.1:10101/vehicleLookup
```

## Deploying the app
---
```bash
# build project and its dependencies
$ dotnet build
# start and build docker container
$ make dr-start
# publish docker container
# note: you must increment the version number in `socotra-app.json`
# note: deploys updated version of the app to socotra marketplace
$ socotra-app publish docker vin-lookup:latest
```

## Test 
```bash
# test suite
$ dotnet test
```
## Support
---
https://socotra.atlassian.net/wiki/spaces/MD/pages/2415952742/Publishing+on+App+MarketPlace

## Stay in touch
---
* Author - Socotra
* Website - https://marketplace.socotra.com/