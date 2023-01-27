// ts2fable 0.0.0
module rec ``dont-remove-obsolete``

#nowarn "3390" // disable warnings for invalid XML comments
#nowarn "0044" // disable warnings for `Obsolete` usage

open System
open Fable.Core
open Fable.Core.JS

/// remove
[<Obsolete("reason")>]
let [<Import("myValue","dont-remove-obsolete")>] myValue: obj = jsNative

type [<AllowNullLiteral>] IExports =
    /// remove
    [<Obsolete("reason")>]
    abstract doStuff: v: float -> bool

/// ns should stay
module Ns =

    type [<AllowNullLiteral>] IExports =
        /// remove
        [<Obsolete("reason")>]
        abstract doStuff: v: float -> bool
        abstract onUsedClass: uc: UsedInterface -> unit
        abstract onUsedInOtherDeprecatedClass: uc: UsedInOtherDeprecatedInterface -> unit
        /// remove
        [<Obsolete("reason")>]
        abstract myValue: float

    /// <summary>Not obsolete -&gt; <c>I</c> MUST stay</summary>
    type [<AllowNullLiteral>] I =
        /// remove
        [<Obsolete("reason")>]
        abstract doStuff: v: float -> bool
        /// remove
        [<Obsolete("reason")>]
        abstract value: float with get, set

    /// Should not stay: deprecated, and not used
    [<Obsolete("reason")>]
    type [<AllowNullLiteral>] UnusedInterface =
        interface end

    /// Should stay: deprecated, but used in function
    [<Obsolete("reason")>]
    type [<AllowNullLiteral>] UsedInterface =
        interface end

    /// Should not stay: deprecated, and used in deprecated function
    [<Obsolete("reason")>]
    type [<AllowNullLiteral>] UsedInOtherDeprecatedInterface =
        interface end

    /// <summary><c>J</c> and anything inside <c>J</c> should be removed</summary>
    [<Obsolete("reason")>]
    type [<AllowNullLiteral>] J =
        abstract doStuff: v: float -> bool
        abstract value: float with get, set

/// <summary><c>nsd</c> and everything inside should be removed!</summary>
[<Obsolete("reason")>]
module Nsd =

    type [<AllowNullLiteral>] IExports =
        abstract myValue: float
        abstract f: v: obj option -> unit

