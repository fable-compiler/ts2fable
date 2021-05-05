// ts2fable 0.0.0
module rec obsolete

#nowarn "0044" // disable warnings for `Obsolete` usage

open System
open Fable.Core
open Fable.Core.JS

/// Summary: SomeNamespace
[<Obsolete("SomeNamespace is deprecated")>]
let [<Import("SomeNamespace","obsolete")>] someNamespace: SomeNamespace.IExports = jsNative
/// Summary: SomeModule
[<Obsolete("SomeModule is deprecated")>]
let [<Import("SomeModule","obsolete")>] someModule: SomeModule.IExports = jsNative
/// Summary: SomeConst
[<Obsolete("SomeConst is deprecated")>]
let [<Import("SomeConst","obsolete")>] SomeConst: float = jsNative
/// Summary: SomeVariable
[<Obsolete("SomeVariable is deprecated")>]
let [<Import("SomeVariable","obsolete")>] SomeVariable: float = jsNative

type [<AllowNullLiteral>] IExports =
    /// Summary: SomeFunction
    [<Obsolete("SomeFunction is deprecated")>]
    abstract SomeFunction: unit -> unit
    /// Summary: SomeClass
    [<Obsolete("SomeClass is deprecated")>]
    abstract SomeClass: SomeClassStatic
    /// Summary: SomeClassWithStaticFunction
    [<Obsolete("SomeClassWithStaticFunction is deprecated")>]
    abstract SomeClassWithFunction: SomeClassWithStaticFunctionStatic

/// Escape quotation marks
[<Obsolete("Hello \"World\"!")>]
type [<AllowNullLiteral>] A =
    interface end

/// Multiline
[<Obsolete("Deprecated because
of stuff")>]
type [<AllowNullLiteral>] B =
    interface end

/// Multiple deprecated tags
[<Obsolete("Message 1
Reason 2")>]
type [<AllowNullLiteral>] C =
    interface end

/// Deprecated interface with default type parameter
/// 
/// Both Interfaces with generic and with default should be deprecated
[<Obsolete("Interface with default is deprecated")>]
type D =
    D<string>

/// Deprecated interface with default type parameter
/// 
/// Both Interfaces with generic and with default should be deprecated
[<Obsolete("Interface with default is deprecated")>]
type [<AllowNullLiteral>] D<'T> =
    interface end

/// Summary: SomeInterface
[<Obsolete("SomeInterface is deprecated")>]
type [<AllowNullLiteral>] SomeInterface =
    /// Summary: SomeValue
    [<Obsolete("SomeValue is deprecated")>]
    abstract SomeValue: float with get, set
    /// Summary: SomeFunction
    [<Obsolete("SomeFunction is deprecated")>]
    abstract SomeFunction: unit -> unit

/// Summary: SomeClass
[<Obsolete("SomeClass is deprecated")>]
type [<AllowNullLiteral>] SomeClass =
    /// Summary: SomeValue
    [<Obsolete("SomeValue is deprecated")>]
    abstract SomeValue: float with get, set
    /// Summary: SomeFunction
    [<Obsolete("SomeFunction is deprecated")>]
    abstract SomeFunction: unit -> unit

/// Summary: SomeClass
[<Obsolete("SomeClass is deprecated")>]
type [<AllowNullLiteral>] SomeClassStatic =
    /// Summary: constructor
    [<Obsolete("ctor is deprecated")>]
    [<EmitConstructor>] abstract Create: value: string -> SomeClass

/// Summary: SomeType
[<Obsolete("SomeType is deprecated")>]
type [<AllowNullLiteral>] SomeType =
    /// Summary: SomeValue
    [<Obsolete("SomeValue is deprecated")>]
    abstract SomeValue: float with get, set
    /// Summary: SomeFunction
    [<Obsolete("SomeFunction is deprecated")>]
    abstract SomeFunction: unit -> unit

/// Summary: SomeFunctionType
[<Obsolete("SomeFunctionType is deprecated")>]
type [<AllowNullLiteral>] SomeFunctionType =
    /// Summary: SomeFunctionType
    [<Obsolete("SomeFunctionType is deprecated")>]
    [<Emit "$0($1...)">] abstract Invoke: a: float * b: string -> string

/// Summary: SomeAlias
[<Obsolete("SomeAlias is deprecated")>]
type SomeAlias =
    string

/// Summary: SomeUnion
[<Obsolete("SomeUnion is deprecated")>]
type SomeUnion =
    U2<string, float>

/// Summary: SomeLiteral
type [<StringEnum>] [<RequireQualifiedAccess>] SomeLiteral =
    | [<CompiledName "A">] A
    | [<CompiledName "B">] B

/// Summary: SomeIntersectionType
[<Obsolete("SomeIntersectionType is deprecated")>]
type [<AllowNullLiteral>] SomeIntersectionType =
    interface end

/// Summary: SomeEnum
[<Obsolete("SomeEnum is deprecated")>]
type [<RequireQualifiedAccess>] SomeEnum =
    /// <summary>Summary: <c>A = 0</c></summary>
    [<Obsolete("A is deprecated")>]
    | A = 0
    /// <summary>Summary: <c>B = 1</c></summary>
    [<Obsolete("B is deprecated")>]
    | B = 1

/// Summary: SomeStringEnum
[<Obsolete("SomeStringEnum is deprecated")>]
type [<StringEnum>] [<RequireQualifiedAccess>] SomeStringEnum =
    /// <summary>Summary: <c>A = "A"</c></summary>
    [<Obsolete("A is deprecated")>]
    | [<CompiledName "A">] A
    /// <summary>Summary: <c>B = "B"</c></summary>
    [<Obsolete("B is deprecated")>]
    | [<CompiledName "B">] B

/// Summary: SomeNamespace
[<Obsolete("SomeNamespace is deprecated")>]
module SomeNamespace =

    type [<AllowNullLiteral>] IExports =
        /// Summary: SomeFunction
        [<Obsolete("SomeFunction is deprecated")>]
        abstract SomeFunction: unit -> unit

/// Summary: SomeModule
[<Obsolete("SomeModule is deprecated")>]
module SomeModule =

    type [<AllowNullLiteral>] IExports =
        /// Summary: SomeFunction
        [<Obsolete("SomeFunction is deprecated")>]
        abstract SomeFunction: unit -> unit

/// Summary: SomeGenericType
[<Obsolete("SomeGenericType is deprecated")>]
type [<AllowNullLiteral>] SomeGenericType<'A> =
    interface end

/// Summary: SomeClassWithStaticFunction
[<Obsolete("SomeClassWithStaticFunction is deprecated")>]
type [<AllowNullLiteral>] SomeClassWithStaticFunction =
    interface end

/// Summary: SomeClassWithStaticFunction
[<Obsolete("SomeClassWithStaticFunction is deprecated")>]
type [<AllowNullLiteral>] SomeClassWithStaticFunctionStatic =
    [<EmitConstructor>] abstract Create: unit -> SomeClassWithStaticFunction
    /// Summary: SomeStaticFunction
    [<Obsolete("SomeStaticFunction is deprecated")>]
    abstract SomeStaticFunction: unit -> unit
