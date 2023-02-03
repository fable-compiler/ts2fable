// ts2fable 0.0.0
module rec ``#467-NonNullable``

#nowarn "3390" // disable warnings for invalid XML comments

open System
open Fable.Core
open Fable.Core.JS

/// <summary><see href="https://www.typescriptlang.org/docs/handbook/utility-types.html#nonnullabletype" /></summary>
let [<Import("NsNonNullable","#467-NonNullable")>] nsNonNullable: NsNonNullable.IExports = jsNative

type [<AllowNullLiteral>] W<'T> =
    abstract value: 'T

/// <summary><see href="https://www.typescriptlang.org/docs/handbook/utility-types.html#nonnullabletype" /></summary>
module NsNonNullable =

    type [<AllowNullLiteral>] IExports =
        /// W<T> -> W<T>
        abstract f0: v: W<'T> -> W<'T>
        /// W<T> -> W<T>
        abstract f1: v: W<'T> -> W<'T>

    /// string | number
    type T0 =
        U2<string, float>

    /// string[]
    type T1 =
        ResizeArray<string>

    /// W<T>
    type T2<'T> =
        W<'T>

    type [<AllowNullLiteral>] I0 =
        /// W<T> -> W<T>
        [<Emit("$0($1...)")>] abstract Invoke: v: W<'T> -> W<'T>
