export interface FieldValues {
  [propertyName: string]: string[];
}

export interface AutofillPayload {
  // Required
  policyholderLocator: string;
  productName: string;
  operation:
    | 'newBusiness'
    | 'endorsement'
    | 'renewal'
    | 'reinstatement'
    | 'cancellation'
    | 'manual'
    | 'feeAssessment';
  operationType: 'create' | 'update';
  updates: UpdateRequest;

  // Optional
  policyLocator?: string;
  endorsementLocator?: string;
  quoteLocator?: string;
  renewalLocator?: string;
  configVersion?: number;
}

export interface UpdateRequest {
  // Required
  policyEndTimestamp: string;
  policyStartTimestamp: string;

  // Optional
  endorsementName?: string;
  endorsementEffectiveTimestamp?: string;
  fieldValues?: FieldValues;
  addFieldGroups?: FieldGroupCreateRequest[];
  updateFieldGroups?: FieldGroupUpdateRequest[];
  removeFieldGroups?: [string];
  addExposures?: ExposureCreateRequest[];
  updateExposures?: ExposureUpdateRequest[];
  removeExposures?: [string];
}

export interface FieldGroupCreateRequest {
  // Required
  fieldName: string;

  // Optional
  fieldValues: FieldValues;
}

export interface FieldGroupUpdateRequest {
  // Required
  fieldGroupLocator: string;
  fieldName: string;

  // Optional
  fieldValues: FieldValues;
}

export interface ExposureCreateRequest {
  // Required
  exposureName: string;
  perils: PerilCreateRequest[];
  fieldGroups: FieldGroupCreateRequest[];

  // Optional
  fieldValues: FieldValues;
}

export interface ExposureUpdateRequest {
  // Required
  addFieldGroups: FieldGroupCreateRequest[];
  updateFieldGroups: FieldGroupUpdateRequest[];
  removeFieldGroups: string[];
  addPerils: PerilCreateRequest[];
  updatePerils: PerilUpdateRequest[];
  exposureLocator: string;
  removePerils: string[];

  // Optional
  fieldValues: FieldValues;
}

export interface PerilCreateRequest {
  // Required
  name: string;

  // Optional
  locator: string;
  fieldValues: FieldValues;
  deductible: number;
  lumpSumPayment: number;
  fieldGroups: FieldGroupCreateRequest[];
  indemnityInAggregate: string;
  indemnityPerEvent: string;
  indemnityPerItem: string;
}

export interface PerilUpdateRequest {
  // Required
  addFieldGroups: FieldGroupCreateRequest[];
  updateFieldGroups: FieldGroupUpdateRequest[];
  removeFieldGroups: string[];
  perilLocator: string;

  // Optional
  fieldValues: FieldValues;
}

export interface AutofillResponse {
  // Required
  fieldValues: FieldValues;

  // Optional
  endorsementEffectiveTimestamp?: string;
  policyEndTimestamp?: string;
  policyStartTimestamp?: string;
  addFieldGroups?: FieldGroupCreateRequest[];
  updateFieldGroups?: FieldGroupUpdateRequest[];
  removeFieldGroups?: string[];
  addExposures?: ExposureCreateRequest[];
  updateExposures?: ExposureUpdateRequest[];
  removeExposures?: string[];
}
