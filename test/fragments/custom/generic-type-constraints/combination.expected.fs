// ts2fable 0.8.0
module rec combination
open System
open Fable.Core
open Fable.Core.JS


type [<AllowNullLiteral>] A =
    abstract alpha: float

type [<AllowNullLiteral>] B =
    abstract beta: string

type [<AllowNullLiteral>] C<'T> =
    abstract value: 'T

type [<AllowNullLiteral>] Comb1<'T when 'T :> A> =
    interface end

type [<AllowNullLiteral>] Comb2<'T> =
    interface end

type [<AllowNullLiteral>] Comb3<'T> =
    interface end