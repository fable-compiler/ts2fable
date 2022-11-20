// ts2fable 0.0.0
module rec ``#454-extends-enum``

#nowarn "3390" // disable warnings for invalid XML comments

open System
open Fable.Core
open Fable.Core.JS


/// <summary>
/// extends int enum
/// -&gt; <c>: enum&lt;int&gt;</c> constraint
/// </summary>
module N =

    type [<AllowNullLiteral>] IExports =
        abstract fRootKind: value: 'TKind -> 'TKind when 'TKind : enum<int>
        abstract fSub1Kind: value: 'TKind -> 'TKind when 'TKind : enum<int>
        abstract fSubValue: value: 'TKind -> 'TKind when 'TKind : enum<int>
        abstract fSubSet: value: 'TKind -> 'TKind when 'TKind : enum<int>

    type [<RequireQualifiedAccess>] RootKind =
        | Alpha = 1
        | Beta = 2
        | Gamma = 3
        | Delta = 4

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// RootKind.Alpha | RootKind.Gamma
    /// </code>
    /// </remarks>
    type SubKind =
        RootKind

    type [<AllowNullLiteral>] InterfaceRootKind<'TKind when 'TKind : enum<int>> =
        interface end

    type [<AllowNullLiteral>] InterfaceSubKind<'TKind when 'TKind : enum<int>> =
        interface end

    type [<AllowNullLiteral>] InterfaceSubValue<'TKind when 'TKind : enum<int>> =
        interface end

    type [<AllowNullLiteral>] InterfaceSubSet<'TKind when 'TKind : enum<int>> =
        interface end

/// extends string enum
/// -> remove constraint
module S =

    type [<AllowNullLiteral>] IExports =
        abstract fMyKind: value: 'TKind -> 'TKind
        abstract fSub1Kind: value: 'TKind -> 'TKind
        abstract fSubValue: value: 'TKind -> 'TKind
        abstract fSubSet: value: 'TKind -> 'TKind

    type [<StringEnum>] [<RequireQualifiedAccess>] MyKind =
        | [<CompiledName("foo")>] Alpha
        | [<CompiledName("bar")>] Beta
        | [<CompiledName("baz")>] Gamma

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// MyKind.Alpha | MyKind.Gamma
    /// </code>
    /// </remarks>
    type SubKind =
        MyKind

    type [<AllowNullLiteral>] InterfaceMyKind<'TKind> =
        interface end

    type [<AllowNullLiteral>] InterfaceSubKind<'TKind> =
        interface end

    type [<AllowNullLiteral>] InterfaceSubValue<'TKind> =
        interface end

    type [<AllowNullLiteral>] InterfaceSubSet<'TKind> =
        interface end
