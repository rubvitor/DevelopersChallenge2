export class ValidationResultModel {
    isValid: boolean;
    errors: ValidationFailureModel[];
    ruleSetsExecuted: string[];
}

export class ValidationFailureModel {
    propertyName: string;
    errorMessage: string;
    attemptedValue: any;
    customState: any;
    severity: Severity;
    errorCode: string;
    formattedMessageArguments: any[];
    formattedMessagePlaceholderValues: any;
}

export enum Severity {
    error = 0,
    warning = 1,
    info = 2
}
