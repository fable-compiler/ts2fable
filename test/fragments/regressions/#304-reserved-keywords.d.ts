/** 
 * Escape all reserved keywords.
 * 
 * Some previously reserved keywords, aren't reserved any more.
 * See [RFC](https://github.com/fsharp/fslang-design/blob/master/FSharp-4.1/FS-1016-unreserve-keywords.md), [PR](https://github.com/dotnet/fsharp/pull/1279)
 */
export interface I {
    // OCAML keywords

    asr: string
    land: string
    lor: string
    lsl: string
    lsr: string
    lxor: string
    mod: string
    sig: string

    // F# reserved keywords

    break: string
    checked: string
    component: string
    const: string
    constraint: string
    continue: string
    event: string
    external: string
    include: string
    mixin: string
    parallel: string
    process: string
    protected: string
    pure: string
    sealed: string
    tailcall: string
    trait: string
    virtual: string

    // not reserved any more
    /** Not reserved any more */
    atomic: string
    /** Not reserved any more */
    constructor: string
    /** Not reserved any more */
    eager: string
    /** Not reserved any more */
    functor: string
    /** Not reserved any more */
    measure: string
    /** Not reserved any more */
    method: string
    /** Not reserved any more */
    object: string
    /** Not reserved any more */
    recursive: string
    /** Not reserved any more */
    volatile: string
}