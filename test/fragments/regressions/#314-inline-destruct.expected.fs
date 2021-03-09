// ts2fable 0.0.0
module rec ``#314-inline-destruct``
open System
open Fable.Core
open Fable.Core.JS


type [<AllowNullLiteral>] IExports =
    abstract DoubleKeyMap: DoubleKeyMapStatic

type [<AllowNullLiteral>] DoubleKeyMap<'TKey1, 'TKey2, 'TValue> =
    abstract set: p0: DoubleKeyMapSetP0 * value: 'TValue -> unit
    abstract get: p0: DoubleKeyMapGetP0 -> 'TValue option
    abstract delete: p0: DoubleKeyMapDeleteP0 -> unit

type [<AllowNullLiteral>] DoubleKeyMapSetP0 =
    abstract key1: 'TKey1 with get, set
    abstract key2: 'TKey2 with get, set

type [<AllowNullLiteral>] DoubleKeyMapGetP0 =
    abstract key1: 'TKey1 with get, set
    abstract key2: 'TKey2 with get, set

type [<AllowNullLiteral>] DoubleKeyMapDeleteP0 =
    abstract key1: 'TKey1 with get, set
    abstract key2: 'TKey2 with get, set

type [<AllowNullLiteral>] DoubleKeyMapStatic =
    [<EmitConstructor>] abstract Create: unit -> DoubleKeyMap<'TKey1, 'TKey2, 'TValue>
