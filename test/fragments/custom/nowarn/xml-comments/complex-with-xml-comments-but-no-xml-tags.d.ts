/**
 * Some interface
 */
export interface I1 {
    /** function in interface */
    f1(v1: string): void;
    /** another function in interface */
    f2(v1: string): void;
}
/** Some function */
export function f1(v1: string): void;
/** A namespace */
export namespace N1 {
    /** interface in namespace */
    interface I1 {
        /** function in interface */
        f1(v1: string): void;
        /** another function in interface */
        f2(v1: string): void;
    }
    /** function in namespace */
    function f1(v1: string): void;
    /** nested namespace */
    namespace N2 {
        /** deep interface */
        interface I1 {
            /** function in interface */
            f1(v1: string): void;
            /** another function in interface */
            f2(v1: string): void;
        }
        /** deep function */
        function f1(v1: string): void;
    }
}