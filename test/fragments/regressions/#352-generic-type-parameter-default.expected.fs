// ts2fable 0.8.0
module rec ``#352-generic-type-parameter-default``
open System
open Fable.Core
open Fable.Core.JS


type A =
    A<obj>

type [<AllowNullLiteral>] A<'K> =
    interface end

type B<'T> =
    B<'T, obj>

type [<AllowNullLiteral>] B<'T, 'K> =
    interface end

type C =
    C<obj, obj>

type C<'K> =
    C<'K, obj>

type [<AllowNullLiteral>] C<'K, 'V> =
    interface end

type E<'S, 'T> =
    E<'S, 'T, obj, obj>

type E<'S, 'T, 'U> =
    E<'S, 'T, 'U, obj>

type [<AllowNullLiteral>] E<'S, 'T, 'U, 'V> =
    interface end