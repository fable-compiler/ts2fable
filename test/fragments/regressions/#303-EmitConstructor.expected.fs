// ts2fable 0.8.0
module rec ``#303-EmitConstructor``
open System
open Fable.Core
open Fable.Core.JS


type [<AllowNullLiteral>] IExports =
    abstract A: AStatic
    abstract B: BStatic
    abstract C: CStatic

type [<AllowNullLiteral>] A =
    interface end

type [<AllowNullLiteral>] AStatic =
    [<EmitConstructor>] abstract Create: unit -> A

type [<AllowNullLiteral>] B =
    interface end

type [<AllowNullLiteral>] BStatic =
    [<EmitConstructor>] abstract Create: v: string -> B

type [<AllowNullLiteral>] C =
    interface end

type [<AllowNullLiteral>] CStatic =
    [<EmitConstructor>] abstract Create: ?v: string -> C