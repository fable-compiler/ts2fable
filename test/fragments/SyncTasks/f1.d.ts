export declare const config: {
    exceptionsToConsole: boolean;
    catchExceptions: boolean;
    traceEnabled: boolean;
    exceptionHandler: ((ex: Error) => void) | undefined;
    unhandledErrorHandler: (err: any) => void;
};
