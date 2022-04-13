// ts2fable 0.0.0
module rec ``#438-tagged-union``
open System
open Fable.Core
open Fable.Core.JS


module StringKind =

    type [<AllowNullLiteral>] Circle =
        abstract kind: string with get, set
        abstract radius: float with get, set

    type [<AllowNullLiteral>] Square =
        abstract kind: string with get, set
        abstract sideLength: float with get, set

    type [<TypeScriptTaggedUnion("kind")>] [<RequireQualifiedAccess>] Shape =
        | Circle of Circle
        | [<CompiledName "square!">] Square of Square

module NumberKind =

    type [<AllowNullLiteral>] Circle =
        abstract kind: int with get, set
        abstract radius: float with get, set

    type [<AllowNullLiteral>] Square =
        abstract kind: int with get, set
        abstract sideLength: float with get, set

    type [<TypeScriptTaggedUnion("kind")>] [<RequireQualifiedAccess>] Shape =
        | [<CompiledValue 1>] Circle of Circle
        | [<CompiledValue 2>] Square of Square

module MixedKind =

    type [<AllowNullLiteral>] Circle =
        abstract kind: int with get, set
        abstract radius: float with get, set

    type [<AllowNullLiteral>] Square =
        abstract kind: string with get, set
        abstract sideLength: float with get, set

    type [<TypeScriptTaggedUnion("kind")>] [<RequireQualifiedAccess>] Shape =
        | [<CompiledName "square!">] Square of Square
        | [<CompiledValue 1>] Circle of Circle

module EnumKind =

    type [<RequireQualifiedAccess>] ShapeKind =
        | Circle = 1
        | Square = 2

    type [<AllowNullLiteral>] Circle =
        abstract kind: ShapeKind with get, set
        abstract radius: float with get, set

    type [<AllowNullLiteral>] Square =
        abstract kind: ShapeKind with get, set
        abstract sideLength: float with get, set

    type [<TypeScriptTaggedUnion("kind")>] [<RequireQualifiedAccess>] Shape =
        | [<CompiledValue 1>] Circle of Circle
        | [<CompiledValue 2>] Square of Square

type [<TypeScriptTaggedUnion("kind")>] [<RequireQualifiedAccess>] S1 =
    | Circle of {| kind: string; radius: float |}
    | Square of {| kind: string; sideLength: float |}

type [<TypeScriptTaggedUnion("kind")>] [<RequireQualifiedAccess>] S2 =
    | [<CompiledValue 1>] Case1 of {| kind: int; radius: float |}
    | [<CompiledValue 2>] Case2 of {| kind: int; sideLength: float |}

type [<TypeScriptTaggedUnion("kind")>] [<RequireQualifiedAccess>] S3 =
    | [<CompiledValue 1>] Circle of {| kind: EnumKind.ShapeKind; radius: float |}
    | [<CompiledValue 2>] Square of {| kind: EnumKind.ShapeKind; sideLength: float |}
