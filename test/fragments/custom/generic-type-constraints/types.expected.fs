// ts2fable 0.8.0
module rec types
open System
open Fable.Core
open Fable.Core.JS


type [<AllowNullLiteral>] IExports =
    abstract T3: T3Static

type [<AllowNullLiteral>] A =
    abstract alpha: float

type [<AllowNullLiteral>] B =
    abstract beta: string

type [<AllowNullLiteral>] C<'T> =
    abstract value: 'T

type [<AllowNullLiteral>] T1<'T when 'T :> A> =
    interface end

type [<AllowNullLiteral>] T2<'T when 'T :> A> =
    interface end

type [<AllowNullLiteral>] T3<'T when 'T :> A> =
    interface end

type [<AllowNullLiteral>] T3Static =
    [<EmitConstructor>] abstract Create: unit -> T3<'T> when 'T :> A