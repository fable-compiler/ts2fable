// ts2fable 0.0.0
module rec ``#400-type-literal-in-union-type``
open System
open Fable.Core
open Fable.Core.JS


/// simple Union without anything special
type Union =
    U2<string, float>

/// Type Alias -> interface, NOT anonymous record
type [<AllowNullLiteral>] NoUnion =
    abstract v1: string with get, set

/// Anonymous Record
type Union1 =
    U2<string, {| v1: string |}>

/// Anonymous Record
type Union2 =
    U2<string, {| v1: string; v2: float |}>

/// Anonymous Record
type Union3 =
    U2<string, {| v1: string; v2: float; v3: float |}>

/// Anonymous Record
type Union4 =
    U2<string, {| v1: string; v2: float; v3: float; v4: string |}>

/// Interface
type Union5 =
    U2<string, Union5Case2>

type [<AllowNullLiteral>] Union5Case2 =
    abstract v1: string with get, set
    abstract v2: float with get, set
    abstract v3: float with get, set
    abstract v4: string with get, set
    abstract v5: string with get, set

/// 2 Interfaces
type Union5_6 =
    U2<Union5_6Case1, Union5_6Case2>

type [<AllowNullLiteral>] Union5_6Case1 =
    abstract v1: string with get, set
    abstract v2: float with get, set
    abstract v3: float with get, set
    abstract v4: string with get, set
    abstract v5: string with get, set

type [<AllowNullLiteral>] Union5_6Case2 =
    abstract v1: float with get, set
    abstract v2: float with get, set
    abstract v3: float with get, set
    abstract v4: float with get, set
    abstract v5: float with get, set
    abstract v6: string with get, set

/// 3 Anonymous Records
type Union1_2_4 =
    U3<{| v1: string |}, {| v1: float; v2: string |}, {| v1: string; v2: float; v3: float; v4: string |}>

/// 2 Anonymous Records, 1 Interface
type Union1_5_3 =
    U3<{| v1: string |}, Union1_5_3Case2, {| v1: string; v2: float; v3: float |}>

type [<AllowNullLiteral>] Union1_5_3Case2 =
    abstract v1: string with get, set
    abstract v2: float with get, set
    abstract v3: float with get, set
    abstract v4: string with get, set
    abstract v5: string with get, set

/// Anonymous Records, v2 & v3 optional
type UnionOptional3 =
    U2<string, {| v1: string; v2: float option; v3: float option |}>

/// Interface, v2 & v3 optional
type UnionOptional5 =
    U2<string, UnionOptional5Case2>

type [<AllowNullLiteral>] UnionOptional5Case2 =
    abstract v1: string with get, set
    abstract v2: float option with get, set
    abstract v3: float option with get, set
    abstract v4: string with get, set
    abstract v5: string with get, set

/// Anonymous Records in Anonymous Record
type Union1Union1 =
    U2<string, {| v1: {| nested1: string |} |}>

/// Anonymous Record in Interface
type Union5Union1 =
    U2<string, Union5Union1Case2>

type [<AllowNullLiteral>] Union5Union1Case2 =
    abstract v1: string with get, set
    abstract v2: float with get, set
    abstract v3: {| nested1: string; nested2: float |} with get, set
    abstract v4: string with get, set
    abstract v5: string with get, set

/// Interface in Anonymous Record
type Union2Union5 =
    U2<string, {| v1: string; v2: Union2Union5V2 |}>

/// Empty interface
type Union0 =
    U2<string, Union0Case2>

type [<AllowNullLiteral>] Union0Case2 =
    interface end

/// <summary>
/// Source: (React)[<see href="https://github.com/DefinitelyTyped/DefinitelyTyped/blob/master/types/react/index.d.ts#L87]" />
/// 
/// Similar to <c>fragments/react/f1</c> (but not generic)
/// </summary>
type UnionBivarianceHack =
    U2<string, (string option -> unit)>

type [<AllowNullLiteral>] Union2Union5V2 =
    abstract v1: string with get, set
    abstract v2: float with get, set
    abstract v3: float with get, set
    abstract v4: string with get, set
    abstract v5: string with get, set
