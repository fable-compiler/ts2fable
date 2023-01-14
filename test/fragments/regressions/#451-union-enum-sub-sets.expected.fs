// ts2fable 0.0.0
module rec ``#451-union-enum-sub-sets``

#nowarn "3390" // disable warnings for invalid XML comments

open System
open Fable.Core
open Fable.Core.JS


module Alpha =

    type [<RequireQualifiedAccess>] RootKind =
        | Alpha = 1
        | Beta = 2
        | Gamma = 3
        | Delta = 4

    type AliasKind =
        RootKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// RootKind.Beta
    /// </code>
    /// </remarks>
    type SingleKind =
        RootKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// RootKind.Alpha | RootKind.Gamma
    /// </code>
    /// </remarks>
    type Sub1Kind =
        RootKind

    /// <summary>Should keep <c>Sub1Kind</c> and not replace with source enum!</summary>
    type Sub1KindAlias =
        Sub1Kind

    type Sub1AddKind =
        Sub1Kind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// RootKind.Alpha | Sub1AddKind
    /// </code>
    /// </remarks>
    type Sub11Kind =
        RootKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// Sub1Kind | Sub1Kind
    /// </code>
    /// </remarks>
    type Sub12Kind =
        RootKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// Sub1Kind | RootKind.Beta
    /// </code>
    /// </remarks>
    type Sub13Kind =
        RootKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// RootKind.Alpha | RootKind.Beta
    /// </code>
    /// </remarks>
    type Sub3Kind =
        RootKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// Sub1Kind | Sub3Kind
    /// </code>
    /// </remarks>
    type Sub4Kind =
        RootKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// RootKind.Delta | Sub3Kind
    /// </code>
    /// </remarks>
    type Sub5Kind =
        RootKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// Sub4Kind | Sub5Kind
    /// </code>
    /// </remarks>
    type Sub6Kind =
        RootKind

    type [<RequireQualifiedAccess>] OtherKind =
        | Foo = 1
        | Bar = 2
        | Baz = 3

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// OtherKind.Foo | OtherKind.Bar
    /// </code>
    /// </remarks>
    type OtherSub1Kind =
        OtherKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// OtherKind.Foo | OtherKind.Baz
    /// </code>
    /// </remarks>
    type OtherSub2Kind =
        OtherKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// OtherSub1Kind | OtherSub2Kind
    /// </code>
    /// </remarks>
    type OtherSub12Kind =
        OtherKind

    /// <summary>Two different enums -&gt; keep Union</summary>
    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// RootKind.Alpha | OtherKind.Bar
    /// </code>
    /// </remarks>
    type MixedKind =
        U2<RootKind, OtherKind>

    /// Two different enums -> keep Union
    type MixedSubKind =
        U2<Sub1Kind, OtherSub1Kind>

module Beta =

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// alpha.Sub1Kind | alpha.Sub3Kind
    /// </code>
    /// </remarks>
    type AlphaSub1 =
        Alpha.RootKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// alpha.Sub4Kind | alpha.Sub3Kind
    /// </code>
    /// </remarks>
    type AlphaSub2 =
        Alpha.RootKind

module Gamma =

    /// <summary>must reference <c>RootKind</c> with correct namespace (<c>alpha</c>)</summary>
    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// beta.AlphaSub1 | beta.AlphaSub2
    /// </code>
    /// </remarks>
    type BetaSub =
        Alpha.RootKind
