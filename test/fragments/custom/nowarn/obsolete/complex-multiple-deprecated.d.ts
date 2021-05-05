export interface I1 {
    f1(v1: string): void;
    f2(v1: string): void;
}
/**
 * @deprecated use something else
 */
export function f1(v1: string): void;
export namespace N1 {
    interface I1 {
        /**
         * @deprecated use something else
         */
        f1(v1: string): void;
        f2(v1: string): void;
    }
    function f1(v1: string): void;
    namespace N2 {
        interface I1 {
            /**
             * @deprecated use something else
             */
            f1(v1: string): void;
            f2(v1: string): void;
        }
        function f1(v1: string): void;
    }
}