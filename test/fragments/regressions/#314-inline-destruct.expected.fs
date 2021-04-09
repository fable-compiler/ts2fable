// ts2fable 0.0.0
module rec ``#314-inline-destruct``
open System
open Fable.Core
open Fable.Core.JS


type [<AllowNullLiteral>] IExports =
    abstract DoubleKeyMap: DoubleKeyMapStatic

type [<AllowNullLiteral>] DoubleKeyMap<'TKey1, 'TKey2, 'TValue> =
    abstract set: p0: {| key1: 'TKey1; key2: 'TKey2 |} * value: 'TValue -> unit
    abstract get: p0: {| key1: 'TKey1; key2: 'TKey2 |} -> 'TValue option
    abstract delete: p0: {| key1: 'TKey1; key2: 'TKey2 |} -> unit

type [<AllowNullLiteral>] DoubleKeyMapStatic =
    [<EmitConstructor>] abstract Create: unit -> DoubleKeyMap<'TKey1, 'TKey2, 'TValue>
