## Description
---
Typescript application that hosts an endpoint that takes in a Socotra AutofillPayload, uses NHTSA api to lookup for the provided VIN, then builds and returns an AutofillResponse.

- The vin field may reside anywhere within the policy.

- Wherever the vin is, the mapping-values (make, model, year, etc.,) must also reside.

- Current mapped values supported are make, model, year, cylinderNumbers, sideAirBagLocations, frontAirBagLocations, enginePower, engineModel, and primaryFuelType.

## Installation
---
```bash
# note: client-id + secret-api key can be configured from socotra marketplace dashboard
$ npm i @socotra/app-cli
$ socotra-app login
```

## Running the app
---
```bash
# start and build docker container 
$ npm run docker:build
# test locally 
$ nest start
# publish docker container
# note: you must increment the version number in `socotra-app.json`
# note: deploys updated version of the app to socotra marketplace
$ npm run socotra:publish
```

## Test 
```bash
# test suite
$ npm run test
```
## Support
---
https://socotra.atlassian.net/wiki/spaces/MD/pages/2415952742/Publishing+on+App+MarketPlace

## Stay in touch
---
* Author - Socotra
* Website - https://marketplace.socotra.com/
