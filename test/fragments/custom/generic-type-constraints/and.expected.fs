// ts2fable 0.8.0
module rec and
open System
open Fable.Core
open Fable.Core.JS


type [<AllowNullLiteral>] A =
    abstract alpha: float

type [<AllowNullLiteral>] B =
    abstract beta: string

type [<AllowNullLiteral>] C<'T> =
    abstract value: 'T

type [<AllowNullLiteral>] And1<'T when 'T :> A and 'T :> B> =
    interface end

type [<AllowNullLiteral>] And2<'T when 'T :> A and 'T :> B and 'T :> C<A> and 'T :> C<B>> =
    interface end

type [<AllowNullLiteral>] And3<'T, 'U when 'T :> A and 'U :> B and 'U :> C<'T>> =
    interface end

type [<AllowNullLiteral>] And4<'T when 'T :> C<obj>> =
    interface end