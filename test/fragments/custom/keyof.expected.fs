// ts2fable 0.8.0
module rec keyof
open System
open Fable.Core
open Fable.Core.JS

[<Erase>] type KeyOf<'T> = Key of string


type [<AllowNullLiteral>] IExports =
    abstract f1: k: KeyOf<float> -> KeyOf<ResizeArray<float>>
    abstract f2: k: KeyOf<float> -> KeyOf<float>
    abstract f3: k: KeyOf<'T> -> KeyOf<'T>
    abstract f4: unit -> KeyOf<float> * KeyOf<string>
    abstract f5: v: 'T * KeyOf<'T> -> 'T * KeyOf<'T>
    abstract C1: C1Static
    abstract C2: C2Static
    abstract C3: C3Static

type [<AllowNullLiteral>] T1 =
    interface end

type [<AllowNullLiteral>] T2<'T> =
    interface end

type [<AllowNullLiteral>] I1 =
    interface end

type [<AllowNullLiteral>] I2<'T> =
    interface end

type [<AllowNullLiteral>] C1 =
    interface end

type [<AllowNullLiteral>] C1Static =
    [<Emit "new $0($1...)">] abstract Create: unit -> C1

type [<AllowNullLiteral>] C2<'T> =
    interface end

type [<AllowNullLiteral>] C2Static =
    [<Emit "new $0($1...)">] abstract Create: unit -> C2<'T>

type [<AllowNullLiteral>] I3<'T> =
    abstract p1: KeyOf<'T> with get, set
    abstract f1: t: 'T * k: KeyOf<'T> -> 'T * KeyOf<'T>
    abstract f2: t: 'T * k: KeyOf<'T> -> 'T * KeyOf<'T>
    abstract f3: l: KeyOf<'T> -> KeyOf<'T>
    abstract f4: l: KeyOf<float> -> KeyOf<float>

type [<AllowNullLiteral>] T3<'T> =
    abstract p1: KeyOf<'T> with get, set
    abstract f1: t: 'T * k: KeyOf<'T> -> 'T * KeyOf<'T>
    abstract f2: t: 'T * k: KeyOf<'T> -> 'T * KeyOf<'T>
    abstract f3: l: KeyOf<'T> -> KeyOf<'T>
    abstract f4: l: KeyOf<float> -> KeyOf<float>

type [<AllowNullLiteral>] C3<'T> =
    abstract p1: KeyOf<'T> with get, set
    abstract f1: t: 'T * k: KeyOf<'T> -> 'T * KeyOf<'T>
    abstract f2: t: 'T * k: KeyOf<'T> -> 'T * KeyOf<'T>
    abstract f3: l: KeyOf<'T> -> KeyOf<'T>
    abstract f4: l: KeyOf<float> -> KeyOf<float>

type [<AllowNullLiteral>] C3Static =
    [<Emit "new $0($1...)">] abstract Create: k: KeyOf<'T> -> C3<'T>