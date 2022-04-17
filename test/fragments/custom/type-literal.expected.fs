// ts2fable 0.0.0
module rec ``type-literal``

#nowarn "3390" // disable warnings for invalid XML comments

open System
open Fable.Core
open Fable.Core.JS

let [<Import("N1","type-literal")>] n1: N1.IExports = jsNative
/// Anon record
let [<Import("c1","type-literal")>] c1: {| v1: string |} = jsNative
/// Anon record
let [<Import("c4","type-literal")>] c4: {| v1: string; v2: float; v3: float; v4: string |} = jsNative
/// interface
let [<Import("c5","type-literal")>] c5: C5 = jsNative
/// Anon record
let [<Import("l1","type-literal")>] l1: {| v1: string |} = jsNative
/// Anon record
let [<Import("l4","type-literal")>] l4: {| v1: string; v2: float; v3: float; v4: string |} = jsNative
/// interface
let [<Import("l5","type-literal")>] l5: C5 = jsNative
/// Nested Anon records
let [<Import("cn1","type-literal")>] cn1: {| v1: string; v2: {| v2_1: string; v2_2: {| v2_2_1: string; v2_2_2: float; v2_2_3: string |}; v2_3: {| v2_3_1: string; v2_3_2: string |}; v2_4: string |}; v3: float |} = jsNative
/// Nested Anon records & Interfaces
let [<Import("cn2","type-literal")>] cn2: {| v1: string; v2: {| v2_1: string; v2_2: Cn2V2V2_2; v2_3: {| v2_3_1: string; v2_3_2: string |}; v2_4: string |}; v3: Cn2V3 |} = jsNative
/// <summary>
/// NOT a Anonymous Record, instead Union type (read as TypeLiteral)
/// 
/// source: <see href="https://github.com/DefinitelyTyped/DefinitelyTyped/blob/ab5329620abcfa7ffc990d93d45165b8e51a55ca/types/mocha/index.d.ts#L165">Mocha</see>
/// </summary>
let [<Import("state","type-literal")>] state: State = jsNative

type [<AllowNullLiteral>] IExports =
    /// neither interface nor anon record
    abstract f0_0: v: string -> float
    /// Input: Anon record; Return: Anon record
    abstract f1_1: v: {| v1: string |} -> {| v1: float |}
    /// Input: Anon record; Return: Anon record
    abstract f2_2: v: {| v1: string; v2: float |} -> {| v1: float; v2: string |}
    /// Input: Anon record; Return: Anon record
    abstract f3_3: v: {| v1: string; v2: float; v3: string |} -> {| v1: float; v2: string; v3: string |}
    /// Input: Anon record; Return: Anon record
    abstract f4_4: v: {| v1: string; v2: float; v3: string; v4: string |} -> {| v1: float; v2: string; v3: string; v4: float |}
    /// Input: interface; Return: interface
    abstract f5_5: v: F5_5V -> F5_5Return
    /// Input: anon record, interface; Return: anon record
    abstract f2_5_3: v1: {| v1: string; v2: float |} * v2: F2_5_3V2 -> {| v1: float; v2: string; v3: string |}
    /// Input: anon record; Return: anon record
    abstract fOptional2_Optional3: v1: {| name: string; birthday: float option |} -> {| name: string; age: float option; id: float |}
    /// Input: anon record; Return: anon record
    abstract ff2_2: v1: {| value: string; f: string -> string |} -> {| result: string; f: string -> float -> string |}
    /// Input: interface; Return: anon interface
    abstract ff2_2: v1: Ff2_2V1 -> Ff2_2Return
    abstract C1: C1Static
    /// read as Type Literal 
    /// BUT: Input & Output must be extracted into Union
    abstract e1: v: IExportsE1 -> IExportsE12

type [<AllowNullLiteral>] F5_5V =
    abstract v1: string with get, set
    abstract v2: float with get, set
    abstract v3: string with get, set
    abstract v4: string with get, set
    abstract v5: string with get, set

type [<AllowNullLiteral>] F5_5Return =
    abstract v1: float with get, set
    abstract v2: string with get, set
    abstract v3: string with get, set
    abstract v4: float with get, set
    abstract v5: string with get, set

type [<AllowNullLiteral>] F2_5_3V2 =
    abstract v1: float with get, set
    abstract v2: string with get, set
    abstract v3: string with get, set
    abstract v4: float with get, set
    abstract v5: string with get, set

type [<AllowNullLiteral>] Ff2_2V1 =
    abstract value: string with get, set
    abstract f: (string -> string) with get, set
    abstract v2: float with get, set
    abstract v3: float with get, set
    abstract v4: string with get, set
    abstract v5: string with get, set

type [<AllowNullLiteral>] Ff2_2Return =
    abstract result: string with get, set
    abstract f: (string -> float -> string) with get, set
    abstract v2: float with get, set
    abstract v3: float with get, set
    abstract v4: string with get, set
    abstract v5: string with get, set

/// Type Alias -> interface, NOT anonymous record
type [<AllowNullLiteral>] NoUnion =
    abstract v1: string with get, set

/// Anonymous Record
type Union1 =
    U2<string, {| v1: string |}>

/// less than 5 members -> Anonymous Record
type Union4 =
    U2<string, {| v1: string; v2: float; v3: float; v4: string |}>

/// more than 4 members -> Interface
type Union5 =
    U2<string, Union5Case2>

type [<AllowNullLiteral>] Union5Case2 =
    abstract v1: string with get, set
    abstract v2: float with get, set
    abstract v3: float with get, set
    abstract v4: string with get, set
    abstract v5: string with get, set

/// <summary>
/// Source: <see href="https://github.com/DefinitelyTyped/DefinitelyTyped/blob/master/types/react/index.d.ts#L87">React</see>
/// 
/// Similar to <c>fragments/react/f1</c> (but not generic)
/// </summary>
type UnionBivarianceHack =
    U2<string, (string option -> unit)>

/// <summary>Source: <see href="https://github.com/DefinitelyTyped/DefinitelyTyped/blob/master/types/react/index.d.ts#L87">React</see></summary>
type [<AllowNullLiteral>] RefCallback<'T> =
    /// <summary>Source: <see href="https://github.com/DefinitelyTyped/DefinitelyTyped/blob/master/types/react/index.d.ts#L87">React</see></summary>
    [<Emit("$0($1...)")>] abstract Invoke: instance: 'T option -> unit

type [<AllowNullLiteral>] I1 =
    /// Anon Record
    abstract l1: {| v1: string |} with get, set
    /// Interface
    abstract l5: C5 with get, set
    /// Anon Record
    abstract c1: {| v1: string |}
    /// Interface
    abstract c5: C5
    /// Anon Record
    abstract f1: v1: {| v1: string |} -> {| v1: string |}
    /// Interface
    abstract f5: v1: I1F5V1 -> I1F5Return

type [<AllowNullLiteral>] I1F5V1 =
    abstract v1: string with get, set
    abstract v2: float with get, set
    abstract v3: float with get, set
    abstract v4: string with get, set
    abstract v5: string with get, set

type [<AllowNullLiteral>] I1F5Return =
    abstract v1: string with get, set
    abstract v2: float with get, set
    abstract v3: float with get, set
    abstract v4: string with get, set
    abstract v5: string with get, set

type [<AllowNullLiteral>] T1 =
    /// Anon Record
    abstract l1: {| v1: string |} with get, set
    /// Interface
    abstract l5: C5 with get, set
    /// Anon Record
    abstract c1: {| v1: string |}
    /// Interface
    abstract c5: C5
    /// Anon Record
    abstract f1: v1: {| v1: string |} -> {| v1: string |}
    /// Interface
    abstract f5: v1: C5 -> C5

module N1 =

    type [<AllowNullLiteral>] IExports =
        /// Anon Record
        abstract l1: {| v1: string |} with get, set
        /// Interface
        abstract l5: IExportsL5 with get, set
        /// Anon Record
        abstract c1: {| v1: string |}
        /// Interface
        abstract c5: IExportsL5
        /// Anon Record
        abstract f1: v1: {| v1: string |} -> {| v1: string |}
        /// Interface
        abstract f5: v1: F5V1 -> F5Return

    type [<AllowNullLiteral>] F5V1 =
        abstract v1: string with get, set
        abstract v2: float with get, set
        abstract v3: float with get, set
        abstract v4: string with get, set
        abstract v5: string with get, set

    type [<AllowNullLiteral>] F5Return =
        abstract v1: string with get, set
        abstract v2: float with get, set
        abstract v3: float with get, set
        abstract v4: string with get, set
        abstract v5: string with get, set

    type [<AllowNullLiteral>] IExportsL5 =
        abstract v1: string with get, set
        abstract v2: float with get, set
        abstract v3: float with get, set
        abstract v4: string with get, set
        abstract v5: string with get, set

type [<AllowNullLiteral>] C1 =
    /// Anon Record
    abstract l1: {| v1: string |} with get, set
    /// Interface
    abstract l5: C5 with get, set
    /// Anon Record
    abstract c1: {| v1: string |}
    /// Interface
    abstract c5: C5
    /// Anon Record
    abstract f1: v1: {| v1: string |} -> {| v1: string |}
    /// Interface
    abstract f5: v1: C1F5V1 -> C1F5Return

type [<AllowNullLiteral>] C1F5V1 =
    abstract v1: string with get, set
    abstract v2: float with get, set
    abstract v3: float with get, set
    abstract v4: string with get, set
    abstract v5: string with get, set

type [<AllowNullLiteral>] C1F5Return =
    abstract v1: string with get, set
    abstract v2: float with get, set
    abstract v3: float with get, set
    abstract v4: string with get, set
    abstract v5: string with get, set

type [<AllowNullLiteral>] C1Static =
    [<EmitConstructor>] abstract Create: unit -> C1

/// read as Type Literal 
/// BUT: must be printed as Union
type [<StringEnum>] [<RequireQualifiedAccess>] E1 =
    | [<CompiledName("Alpha")>] Alpha
    | [<CompiledName("Beta")>] Beta

type [<AllowNullLiteral>] C5 =
    abstract v1: string with get, set
    abstract v2: float with get, set
    abstract v3: float with get, set
    abstract v4: string with get, set
    abstract v5: string with get, set

type [<AllowNullLiteral>] Cn2V2V2_2 =
    abstract v2_2_1: string with get, set
    abstract v2_2_2: float with get, set
    abstract v2_2_3: string with get, set
    abstract v2_2_4: string with get, set
    abstract v2_2_5: float with get, set

type [<AllowNullLiteral>] Cn2V3V3_3V3_3_2 =
    abstract v3_3_2_1: string with get, set
    abstract v3_3_2_2: float with get, set
    abstract v3_3_2_3: string with get, set
    abstract v3_3_2_4: float with get, set
    abstract v3_3_2_5: string with get, set
    abstract v3_3_2_6: string with get, set

type [<AllowNullLiteral>] Cn2V3 =
    abstract v3_1: string with get, set
    abstract v3_2: float with get, set
    abstract v3_3: {| v3_3_1: string; v3_3_2: Cn2V3V3_3V3_3_2; v3_3_3: string |} with get, set
    abstract v3_4: string with get, set
    abstract v3_5: float with get, set

type [<StringEnum>] [<RequireQualifiedAccess>] State =
    | Failed
    | Passed

type [<StringEnum>] [<RequireQualifiedAccess>] IExportsE1 =
    | [<CompiledName("Alpha")>] Alpha
    | [<CompiledName("Beta")>] Beta

type [<StringEnum>] [<RequireQualifiedAccess>] IExportsE12 =
    | [<CompiledName("Gamma")>] Gamma
    | [<CompiledName("Delta")>] Delta
