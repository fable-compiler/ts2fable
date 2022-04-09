// ts2fable 0.8.0
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
        | [<CompiledName "square_">] Square of Square

module NumberKind =

    type [<AllowNullLiteral>] Circle =
        abstract kind: int with get, set
        abstract radius: float with get, set

    type [<AllowNullLiteral>] Square =
        abstract kind: float with get, set
        abstract sideLength: float with get, set

    type [<TypeScriptTaggedUnion("kind")>] [<RequireQualifiedAccess>] Shape =
        | [<CompiledValue 1>] Circle of Circle
        | [<CompiledValue 4.2>] Square of Square

module MixedKind =

    type [<AllowNullLiteral>] Circle =
        abstract kind: int with get, set
        abstract radius: float with get, set

    type [<AllowNullLiteral>] Square =
        abstract kind: string with get, set
        abstract sideLength: float with get, set

    type [<TypeScriptTaggedUnion("kind")>] [<RequireQualifiedAccess>] Shape =
        | [<CompiledName "square!">] Square of Square
        | [<CompiledValue 0>] Circle of Circle

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