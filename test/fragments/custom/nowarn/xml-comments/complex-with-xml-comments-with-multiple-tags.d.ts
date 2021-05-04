/**
 * Some interface
 */
export interface I1 {
    /** function in interface */
    f1(v1: string): void;
    /** another function in interface
     * 
     * And some code: `1+1`
     * 
     */
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
        /** 
         * another function in interface
         * 
         * @remarks some notes
         */
        f2(v1: string): void;
    }
    /** function in namespace */
    function f1(v1: string): void;
    /** nested namespace */
    namespace N2 {
        /** 
         * deep interface
         * 
         * And a link: [ts2fable](https://github.com/fable-compiler/ts2fable)
         */
        interface I1 {
            /** 
             * function in interface
             * 
             * @param value a value
             */
            f1(v1: string): void;
            /** another function in interface */
            f2(v1: string): void;
        }
        /** deep function */
        function f1(v1: string): void;
    }
}