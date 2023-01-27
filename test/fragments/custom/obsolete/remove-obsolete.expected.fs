// ts2fable 0.0.0
module rec ``remove-obsolete``

#nowarn "3390" // disable warnings for invalid XML comments
#nowarn "0044" // disable warnings for `Obsolete` usage

open System
open Fable.Core
open Fable.Core.JS


/// ns should stay
module Ns =

    type [<AllowNullLiteral>] IExports =
        abstract onUsedClass: uc: UsedInterface -> unit
        abstract onUsedInOtherDeprecatedClass: uc: UsedInOtherDeprecatedInterface -> unit

    /// <summary>Not obsolete -&gt; <c>I</c> MUST stay</summary>
    type [<AllowNullLiteral>] I =
        interface end

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
