// ts2fable 0.8.0
module rec ``#353-generic-type-parameter-default-type``
open System
open Fable.Core
open Fable.Core.JS


type A =
    A<string>

type [<AllowNullLiteral>] A<'T> =
    interface end

type B =
    B<string, float>

type B<'T> =
    B<'T, float>

type [<AllowNullLiteral>] B<'T, 'K> =
    interface end

type C =
    C<obj>

type [<AllowNullLiteral>] C<'T> =
    interface end

type D =
    D<U2<string, float>>

type [<AllowNullLiteral>] D<'T> =
    interface end

type E =
    E<obj>

type [<AllowNullLiteral>] E<'T> =
    interface end

type F =
    F<A<string>>

type [<AllowNullLiteral>] F<'T> =
    interface end