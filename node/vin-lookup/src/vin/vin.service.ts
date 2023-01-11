import { Injectable } from '@nestjs/common';
import axios, { AxiosResponse } from 'axios';
import * as jsonpath from 'jsonpath';
import { AutofillPayload } from '../socotra/autofill';
import { Fields } from '../state/state.service';

Injectable();
export class VinService {
  static async getVinData(payload: AutofillPayload, fields: Fields) {
    // gets each instance of a vin field in the autofill payload
    console.log('getVinData');
    const vinLocations = jsonpath.nodes(
      payload,
      `$..fieldValues.${fields['vin']}`,
    );

    // iterates through each part of the payload where a vin is located
    for (const vinLocation of vinLocations) {
      // gets the car information from the api based on the input vin
      const vin = vinLocation.value[0];
      var response: AxiosResponse | undefined;
      if (vin) {
        response = await axios.get(
          `https://vpic.nhtsa.dot.gov/api/vehicles/DecodeVin/${vin}?format=json`,
        );
      } else {
        response = undefined;
      }

      // extracts the output fields data from the api response
      Object.keys(fields).forEach((key) => {
        if (key !== 'vin') {
          this.getVinResponseVariable(
            payload,
            response,
            fields,
            key,
            vinLocation.path,
          );
        }
      });
    }
  }

  static async getVinResponseVariable(
    payload: AutofillPayload,
    response: AxiosResponse | undefined,
    fields: Fields,
    key: string,
    path: jsonpath.PathComponent[],
  ) {
    var value = [];
    if (response) {
      // extracts output field value from api response
      const responseVariable = response.data.Results.find((result) => {
        const keyParts = key.split('_');
        var valid = true;
        keyParts.forEach((part) => {
          if (!result.Variable.includes(part)) {
            valid = false;
          }
        });
        return valid;
      });
      if (responseVariable.Value) {
        value = [responseVariable.Value];
      }
    }

    // creates new jsonpath for output field
    const newPath = path;
    newPath[newPath.length - 1] = fields[key];
    const newPathString = jsonpath.stringify(newPath);

    // Sets new value in autofill payload
    jsonpath.value(payload, newPathString, value);
  }
}
