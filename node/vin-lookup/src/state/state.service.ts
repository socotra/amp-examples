import { Injectable } from '@nestjs/common';
import axios from 'axios';
import * as jsonpath from 'jsonpath';
import { AutofillPayload } from '../socotra/autofill';

Injectable();
export class StateService {
  static async getState(
    stateApi: string | undefined,
    key: string | string[] | undefined,
  ): Promise<StateResponse | undefined> {
    console.log('getState');
    try {
      const response = await axios.get(`${stateApi}/state/${key}`);
      return response.data;
    } catch (err) {
      console.error('Error retrieving the app config');
      return undefined;
    }
  }

  static getFieldNames(state: StateResponse, payload: AutofillPayload): Fields {
    console.log('getFieldNames');

    const response = {};

    const mappings = state.mappings.find((mapping: Mappings) => {
      return mapping.productName === payload.productName;
    });

    const fields = mappings.fields;
    Object.entries(fields).forEach((field: [string, string]) => {
      const path = jsonpath.parse(field[1]);
      const fieldNameExpression = path.pop().expression.value;
      response[field[0]] = fieldNameExpression.slice(
        fieldNameExpression.indexOf("'") + 1,
        fieldNameExpression.lastIndexOf("'"),
      );
    });

    return response;
  }
}

export type StateResponse = {
  socotraApiUrl: string;
  tenantHostName: string;
  token: string;
  settings: any;
  mappings: Mappings[];
};

type Mappings = {
  productName: string;
  fields: Fields;
};

export type Fields = {
  [propertyName: string]: string;
};
