// ts2fable 0.0.0
module rec types

#nowarn "3390" // disable warnings for invalid XML comments

open System
open Fable.Core
open Fable.Core.JS

/// <summary>Summary: SomeNamespace</summary>
/// <remarks>
/// Remarks:
/// SomeNamespace 
/// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
/// </remarks>
let [<Import("SomeNamespace","types")>] someNamespace: SomeNamespace.IExports = jsNative
/// <summary>Summary: SomeModule</summary>
/// <remarks>
/// Remarks:
/// SomeModule 
/// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
/// </remarks>
let [<Import("SomeModule","types")>] someModule: SomeModule.IExports = jsNative
/// <summary>Summary: SomeConst</summary>
/// <remarks>
/// Remarks:
/// SomeConst 
/// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
/// </remarks>
let [<Import("SomeConst","types")>] SomeConst: float = jsNative
/// <summary>Summary: SomeVariable</summary>
/// <remarks>
/// Remarks:
/// SomeVariable 
/// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
/// </remarks>
let [<Import("SomeVariable","types")>] SomeVariable: float = jsNative

type [<AllowNullLiteral>] IExports =
    /// <summary>Summary: SomeFunction</summary>
    /// <remarks>
    /// Remarks:
    /// SomeFunction 
    /// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
    /// </remarks>
    abstract SomeFunction: unit -> unit
    /// <summary>Summary: SomeClass</summary>
    /// <remarks>
    /// Remarks:
    /// SomeClass 
    /// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
    /// </remarks>
    abstract SomeClass: SomeClassStatic
    /// <summary>Summary: SomeClassWithStaticFunction</summary>
    /// <remarks>
    /// Remarks:
    /// SomeClassWithStaticFunction 
    /// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
    /// </remarks>
    abstract SomeClassWithFunction: SomeClassWithStaticFunctionStatic

/// <summary>Summary: SomeInterface</summary>
/// <remarks>
/// Remarks:
/// SomeInterface 
/// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
/// </remarks>
type [<AllowNullLiteral>] SomeInterface =
    /// <summary>Summary: SomeValue</summary>
    /// <remarks>
    /// Remarks:
    /// SomeValue 
    /// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
    /// </remarks>
    abstract SomeValue: float with get, set
    /// <summary>Summary: SomeFunction</summary>
    /// <remarks>
    /// Remarks:
    /// SomeFunction 
    /// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
    /// </remarks>
    abstract SomeFunction: unit -> unit

/// <summary>Summary: SomeClass</summary>
/// <remarks>
/// Remarks:
/// SomeClass 
/// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
/// </remarks>
type [<AllowNullLiteral>] SomeClass =
    /// <summary>Summary: SomeValue</summary>
    /// <remarks>
    /// Remarks:
    /// SomeValue 
    /// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
    /// </remarks>
    abstract SomeValue: float with get, set
    /// <summary>Summary: SomeFunction</summary>
    /// <remarks>
    /// Remarks:
    /// SomeFunction 
    /// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
    /// </remarks>
    abstract SomeFunction: unit -> unit

/// <summary>Summary: SomeClass</summary>
/// <remarks>
/// Remarks:
/// SomeClass 
/// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
/// </remarks>
type [<AllowNullLiteral>] SomeClassStatic =
    /// <summary>Summary: constructor</summary>
    /// <remarks>
    /// Remarks:
    /// constructor 
    /// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
    /// </remarks>
    [<EmitConstructor>] abstract Create: value: string -> SomeClass

/// <summary>Summary: SomeType</summary>
/// <remarks>
/// Remarks:
/// SomeType 
/// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
/// </remarks>
type [<AllowNullLiteral>] SomeType =
    /// <summary>Summary: SomeValue</summary>
    /// <remarks>
    /// Remarks:
    /// SomeValue 
    /// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
    /// </remarks>
    abstract SomeValue: float with get, set
    /// <summary>Summary: SomeFunction</summary>
    /// <remarks>
    /// Remarks:
    /// SomeFunction 
    /// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
    /// </remarks>
    abstract SomeFunction: unit -> unit

/// <summary>Summary: SomeFunctionType</summary>
/// <remarks>
/// Remarks:
/// SomeFunctionType 
/// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
/// </remarks>
type [<AllowNullLiteral>] SomeFunctionType =
    /// <summary>Summary: SomeFunctionType</summary>
    /// <remarks>
    /// Remarks:
    /// SomeFunctionType 
    /// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
    /// </remarks>
    [<Emit "$0($1...)">] abstract Invoke: a: float * b: string -> string

/// <summary>Summary: SomeAlias</summary>
/// <remarks>
/// Remarks:
/// SomeAlias 
/// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
/// </remarks>
type SomeAlias =
    string

/// <summary>Summary: SomeUnion</summary>
/// <remarks>
/// Remarks:
/// SomeUnion 
/// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
/// </remarks>
type SomeUnion =
    U2<string, float>

/// <summary>Summary: SomeLiteral</summary>
/// <remarks>
/// Remarks:
/// SomeLiteral 
/// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
/// </remarks>
type [<StringEnum>] [<RequireQualifiedAccess>] SomeLiteral =
    | [<CompiledName "A">] A
    | [<CompiledName "B">] B

/// <summary>Summary: SomeIntersectionType</summary>
/// <remarks>
/// Remarks:
/// SomeIntersectionType 
/// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
/// </remarks>
type [<AllowNullLiteral>] SomeIntersectionType =
    interface end

/// <summary>Summary: SomeEnum</summary>
/// <remarks>
/// Remarks:
/// SomeEnum 
/// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
/// </remarks>
type [<RequireQualifiedAccess>] SomeEnum =
    /// <summary>Summary: <c>A = 0</c></summary>
    /// <remarks>
    /// Remarks:
    /// `A 
    /// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see> = 0`
    /// </remarks>
    | A = 0
    /// <summary>Summary: <c>B = 1</c></summary>
    /// <remarks><c>B = 1</c></remarks>
    | B = 1

/// <summary>Summary: SomeStringEnum</summary>
/// <remarks>
/// Remarks:
/// SomeStringEnum 
/// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
/// </remarks>
type [<StringEnum>] [<RequireQualifiedAccess>] SomeStringEnum =
    /// <summary>Summary: <c>A = "A"</c></summary>
    /// <remarks>
    /// Remarks:
    /// `A 
    /// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see> = "A"`
    /// </remarks>
    | [<CompiledName "A">] A
    /// <summary>Summary: <c>B = "B"</c></summary>
    /// <remarks>
    /// Remarks:
    /// `B 
    /// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see> = "B"`
    /// </remarks>
    | [<CompiledName "B">] B

/// <summary>Summary: SomeNamespace</summary>
/// <remarks>
/// Remarks:
/// SomeNamespace 
/// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
/// </remarks>
module SomeNamespace =

    type [<AllowNullLiteral>] IExports =
        /// <summary>Summary: SomeFunction</summary>
        /// <remarks>
        /// Remarks:
        /// SomeFunction 
        /// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
        /// </remarks>
        abstract SomeFunction: unit -> unit

/// <summary>Summary: SomeModule</summary>
/// <remarks>
/// Remarks:
/// SomeModule 
/// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
/// </remarks>
module SomeModule =

    type [<AllowNullLiteral>] IExports =
        /// <summary>Summary: SomeFunction</summary>
        /// <remarks>
        /// Remarks:
        /// SomeFunction 
        /// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
        /// </remarks>
        abstract SomeFunction: unit -> unit

/// <summary>Summary: SomeGenericType</summary>
/// <remarks>
/// Remarks:
/// SomeGenericType 
/// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
/// </remarks>
type [<AllowNullLiteral>] SomeGenericType<'A> =
    interface end

/// <summary>Summary: SomeClassWithStaticFunction</summary>
/// <remarks>
/// Remarks:
/// SomeClassWithStaticFunction 
/// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
/// </remarks>
type [<AllowNullLiteral>] SomeClassWithStaticFunction =
    interface end

/// <summary>Summary: SomeClassWithStaticFunction</summary>
/// <remarks>
/// Remarks:
/// SomeClassWithStaticFunction 
/// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
/// </remarks>
type [<AllowNullLiteral>] SomeClassWithStaticFunctionStatic =
    [<EmitConstructor>] abstract Create: unit -> SomeClassWithStaticFunction
    /// <summary>Summary: SomeStaticFunction</summary>
    /// <remarks>
    /// Remarks:
    /// SomeStaticFunction 
    /// and a link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
    /// </remarks>
    abstract SomeStaticFunction: unit -> unit
