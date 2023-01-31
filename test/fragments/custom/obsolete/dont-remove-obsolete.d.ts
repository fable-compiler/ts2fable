// Same as `remove-obsolete.d.ts`, but without `--remove-obsolete`
// -> all deprecated/obsolete should remain!

// Note: auto-copied from `remove-obsolete.d.ts`
// -> edit `remove-obsolete.d.ts` instead of this file here!

/**
 * ns should stay
 */
declare namespace ns {
    /**
     * remove
     * 
     * @deprecated reason
     */
    function doStuff(v: number): boolean;
    /**
     * Not obsolete -> `I` MUST stay
     */
    interface I {
        /**
         * remove
         * 
         * @deprecated reason
         */
        doStuff(v: number): boolean;
        /**
         * remove
         * 
         * @deprecated reason
         */
        value: number;
    }

    
    /**
     * Should not stay: deprecated, and not used
     * 
     * @deprecated reason
     */
    interface UnusedInterface {}

    /**
     * Should stay: deprecated, but used in function
     * 
     * @deprecated reason
     */
    interface UsedInterface {}
    function onUsedClass(uc: UsedInterface): void

    /**
     * Should not stay: deprecated, and used in deprecated function
     * 
     * @deprecated reason
     */
    interface UsedInOtherDeprecatedInterface {}
    function onUsedInOtherDeprecatedClass(uc: UsedInOtherDeprecatedInterface): void

    /**
     * remove
     * 
     * @deprecated reason
     */
    const myValue: number

    /**
     * `J` and anything inside `J` should be removed
     * 
     * @deprecated reason
     */
    interface J {
        doStuff(v: number): boolean
        value: number
    }
}
/**
 * remove
 * 
 * @deprecated reason
 */
declare function doStuff(v: number): boolean

/**
 * remove
 * 
 * @deprecated reason
 */
declare const myValue = 42

/**
 * `nsd` and everything inside should be removed!
 * 
 * @deprecated reason
 */
declare namespace nsd {
    const myValue: number
    function f(v: any): void
}

