import { FieldValues } from './autofill';

export interface PolicyResponse {
  //Required
  locator: string;
  policyholderLocator: string;
  productLocator: string;
  productName: string;
  createdTimestamp: string;
  updatedTimestamp: string;
  originalContractStartTimestamp?: string;
  originalContractEndTimestamp?: string;
  effectiveContractEndTimestamp?: string;
  characteristics:
    | PolicyCharacteristicsResponse[]
    | PolicyCharacteristicsResponse;
  modifications?: PolicyModificationResponse[];
  exposures: ExposureResponse[];
  documents: PolicyDocumentResponse[];
  invoices: PolicyInvoiceResponse[];
  grossFees: number;
  currency?: string;
  fees: FeeResponse[];
  configVersion?: number;

  //Optional
  issuedTimestamp?: string;
  cancellation?: CancellationInfo;
  grossFeesCurrency?: string;
  paymentScheduleName?: string;
  premiumReportName?: string;
  quoteLocator?: string;
  quoteSummary?: QuoteSummaryResponse;
  selected?: boolean;
  state?: 'draft';
  name?: string;
  contractStartTimestamp?: string;
  contractEndTimestamp?: string;
  policyLocator?: string;
}

export interface PolicyCharacteristicsResponse {
  //Required
  locator: string;
  policyholderLocator: string;
  policyLocator: string;
  productLocator: string;
  createdTimestamp: string;
  updatedTimestamp: string;
  startTimestamp: string;
  endTimestamp: string;
  policyEndTimestamp: string;
  policyStartTimestamp: string;
  fieldValues: FieldValues;
  fieldGroupsByLocator: FieldGroupsByLocator;
  mediaByLocator: MediaByLocator;
  taxGroups: TaxGroupResponse[];

  //Optional
  issuedTimestamp?: string;
  replacedTimestamp?: string;
  grossPremium?: number;
  grossTaxes?: number;
  grossPremiumCurrency?: string;
  grossTaxesCurrency?: string;
}

export interface FieldGroupsByLocator {
  [propertyName: string]: FieldGroup;
}

export interface FieldGroup {
  // Required
  fieldGroupLocator: string;
  fieldName: string;

  // Optional
  fieldValues?: FieldValues;
}

export interface MediaByLocator {
  [propertyName: string]: Media;
}

export interface Media {
  //Required
  locator: string;
  expiresTimestamp: string;
  mimeType: string;
  url: string;

  //Optional
  fileName?: string;
}

export interface TaxGroupResponse {
  //Required
  name: string;
  amount: number;

  //Optional
  amountCurrency?: string;
}

export interface PolicyModificationResponse {
  //Required
  locator: string;
  name: string;
  displayId: string;
  policyholderLocator: string;
  policyLocator: string;
  productLocator: string;
  createdTimestamp: string;
  updatedTimestamp: string;
  exposureModifications: ExposureModificationResponse[];
  mediaByLocator: MediaByLocator;
  newPolicyCharacteristicsLocators: string[];
  number: number;

  //Optional
  issuedTimestamp?: string;
  effectiveTimestamp?: string;
  policyEndTimestamp?: string;
  premiumChange?: number;
  automatedUnderwritingResult?: AutomatedUnderwritingResultResponse;
  cancellationName?: string;
  endorsementLocator?: string;
  newPolicyCharacteristicsLocator?: string;
  premiumChangeCurrency?: string;
  renewalLocator?: string;
}

export interface ExposureModificationResponse {
  //Required
  locator: string;
  policyholderLocator: string;
  policyLocator: string;
  productLocator: string;
  exposureLocator: string;
  perilModifications: PerilModificationResponse[];
  policyModificationLocator: string;

  //Optional
  newExposureCharacteristicsLocator?: string;
}

export interface PerilModificationResponse {
  //Required
  locator: string;
  policyholderLocator: string;
  policyLocator: string;
  productLocator: string;
  exposureModificationLocator: string;
  perilLocator: string;

  //Optional
  premiumChange?: number;
  newPerilCharacteristicsLocator?: string;
  premiumChangeCurrency?: string;
  replacedPerilCharacteristicsLocator?: string;
}

export interface AutomatedUnderwritingResultResponse {
  //Required
  decisionTimestamp: string;
  decision: 'accept' | 'reject' | 'none';
  notes: string[];
}

export interface ExposureResponse {
  //Required
  locator: string;
  name: string;
  displayId: string;
  policyholderLocator: string;
  policyLocator: string;
  productLocator: string;
  createdTimestamp: string;
  updatedTimestamp: string;
  characteristics: ExposureCharacteristicsResponse[];
  perils: PerilResponse[];
}

export interface ExposureCharacteristicsResponse {
  //Required
  locator: string;
  policyholderLocator: string;
  policyLocator: string;
  productLocator: string;
  createdTimestamp: string;
  updatedTimestamp: string;
  startTimestamp: string;
  endTimestamp: string;
  fieldValues: FieldValues;
  exposureLocator: string;
  fieldGroupsByLocator: FieldGroupsByLocator;
  mediaByLocator: MediaByLocator;

  //Optional
  issuedTimestamp?: string;
  replacedTimestamp?: string;
}

export interface PerilResponse {
  //Required
  locator: string;
  name: string;
  displayId: string;
  policyholderLocator: string;
  policyLocator: string;
  productLocator: string;
  createdTimestamp: string;
  updatedTimestamp: string;
  characteristics: PerilCharacteristicsResponse[];
  exposureLocator: string;
  renewalGroup: string;
}

export interface PerilCharacteristicsResponse {
  //Required
  locator: string;
  policyholderLocator: string;
  policyLocator: string;
  productLocator: string;
  policyCharacteristicsLocator: string;
  exposureCharacteristicsLocator: string;
  createdTimestamp: string;
  updatedTimestamp: string;
  coverageStartTimestamp: string;
  coverageEndTimestamp: string;
  fieldValues: FieldValues;
  fieldGroupsByLocator: FieldGroupsByLocator;
  mediaByLocator: MediaByLocator;
  perilLocator: string;
  policyModificationLocator: string;

  //Optional
  issuedTimestamp?: string;
  replacedTimestamp?: string;
  deductible?: number;
  lumpSumPayment?: number;
  monthPremium?: number;
  monthTechnicalPremium?: number;
  premium?: number;
  technicalPremium?: number;
  deductibleCurrency?: string;
  indemnityInAggregate?: string;
  indemnityInAggregateCurrency?: string;
  indemnityPerEvent?: string;
  indemnityPerEventCurrency?: string;
  indemnityPerItem?: string;
  indemnityPerItemCurrency?: string;
  lumpSumPaymentCurrency?: string;
  premiumCurrency?: string;
}

export interface PolicyDocumentResponse {
  //Required
  locator: string;
  displayName: string;
  fileName: string;
  type: 'pdf' | 'html';

  //Optional
  createdTimestamp?: string;
  urlExpirationTimestamp?: string;
  policyModificationLocator?: string;
  url?: string;
}

export interface PolicyInvoiceResponse {
  //Required
  locator: string;
  displayId: string;
  policyLocator: string;
  createdTimestamp: string;
  updatedTimestamp: string;
  dueTimestamp: string;
  startTimestamp: string;
  endTimestamp: string;
  documents: PolicyDocumentResponse[];
  totalDue: number;
  invoiceType:
    | 'newBusiness'
    | 'endorsement'
    | 'renewal'
    | 'cancellation'
    | 'reinstatement'
    | 'installment'
    | 'premiumReporting'
    | 'other';
  payments: PaymentResponse[];
  settlementStatus: 'outstanding' | 'settled';
  statuses: PolicyInvoiceStatusAndTimeResponse[];
  totalDueCurrency: string;
  transactionIssued: boolean;

  //Optional
  policyModificationLocator?: string;
  premiumReportName?: string;
  settlementType?:
    | 'paid'
    | 'writtenOff'
    | 'zeroDue'
    | 'invalidated'
    | 'carriedForward';
}

export interface PaymentResponse {
  //Required
  locator: string;
  displayId: string;
  postedTimestamp: string;
  fieldValues: FieldValues;
  amount: number;
  invoiceLocator: string;
  mediaByLocator: MediaByLocator;

  //Optional
  reversedTimestamp?: string;
  amountCurrency?: string;
}

export interface PolicyInvoiceStatusAndTimeResponse {
  //Required
  timestamp: string;
  status:
    | 'unfulfilled'
    | 'paid'
    | 'writtenOff'
    | 'zeroDue'
    | 'invalidated'
    | 'carriedForward';
}

export interface FeeResponse {
  //Required
  locator: string;
  name: string;
  startTimestamp: string;
  endTimestamp: string;
  amount: number;
  description: string;
  reversed: boolean;

  //Optional
  amountCurrency?: string;
}

export interface CancellationInfo {
  //Required
  effectiveTimestamp: string;
  modificationLocator: string;
  modificationName: string;
}

export interface QuoteSummaryResponse {
  //Required
  locator: string;
  name: string;
  state: 'draft' | 'quoted' | 'accepted' | 'declined' | 'discarded';
}
