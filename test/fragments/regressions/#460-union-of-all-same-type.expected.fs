// ts2fable 0.0.0
module rec ``#460-union-of-all-same-type``

#nowarn "3390" // disable warnings for invalid XML comments

open System
open Fable.Core
open Fable.Core.JS


module Ns =

    type [<RequireQualifiedAccess>] Kind =
        | Alpha = 1
        | Beta = 2
        | Gamma = 3
        | Delta = 4

    // `enum` constraint doesn't work in rec module and type alias or inheritance
    // see https://github.com/dotnet/fsharp/issues/14580
    // -> disabled until fixed

    // type [<AllowNullLiteral>] Token<'TKind when 'TKind : enum<int>> =
    //     interface end
    type [<AllowNullLiteral>] Token<'TKind> =
        interface end

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// Token&lt;Kind.Alpha&gt;
    /// </code>
    /// </remarks>
    type TokenAlpha =
        Token<Kind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// Token&lt;Kind.Beta&gt;
    /// </code>
    /// </remarks>
    type TokenBeta =
        Token<Kind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// Token&lt;Kind.Gamma&gt;
    /// </code>
    /// </remarks>
    type TokenGamma =
        Token<Kind>

    /// <summary>Common type</summary>
    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// Token&lt;Kind.Alpha&gt; | Token&lt;Kind.Beta&gt;
    /// </code>
    /// </remarks>
    type TokenABDirect1 =
        Token<Kind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// Token&lt;Kind.Alpha | Kind.Beta&gt;
    /// </code>
    /// </remarks>
    type TokenABDirect2 =
        Token<Kind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// Token&lt;Kind.Beta | Kind.Gamma&gt;
    /// </code>
    /// </remarks>
    type TokenBC1 =
        Token<Kind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// Token&lt;Kind.Beta&gt; | Token&lt;Kind.Gamma&gt;
    /// </code>
    /// </remarks>
    type TokenBC2 =
        Token<Kind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// TokenBeta | TokenGamma
    /// </code>
    /// </remarks>
    type TokenBC =
        Token<Kind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// TokenAlpha | TokenBC | TokenGamma
    /// </code>
    /// </remarks>
    type ABCToken =
        Token<Kind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// TokenAlpha | TokenBC | Token&lt;Kind.Gamma&gt;
    /// </code>
    /// </remarks>
    type ABCToken2 =
        Token<Kind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// TokenAlpha | TokenBC | Token&lt;Kind.Gamma | Kind.Delta&gt;
    /// </code>
    /// </remarks>
    type ABCToken3 =
        Token<Kind>

    type [<RequireQualifiedAccess>] Other =
        | Foo = 1
        | Bar = 2

    // type [<AllowNullLiteral>] OtherToken<'TKind when 'TKind : enum<int>> =
    //     interface end
    type [<AllowNullLiteral>] OtherToken<'TKind> =
        interface end

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// OtherToken&lt;Other.Foo&gt;
    /// </code>
    /// </remarks>
    type OtherTokenFoo =
        OtherToken<Other>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// OtherTokenFoo | OtherToken&lt;Other.Bar&gt;
    /// </code>
    /// </remarks>
    type OtherTokenBoth =
        OtherToken<Other>

    /// No common type
    type MixedToken =
        U2<TokenGamma, OtherTokenFoo>

    type [<AllowNullLiteral>] BasicToken<'TKind> =
        interface end

    /// <summary>No common type</summary>
    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// TokenGamma | BasicToken&lt;Kind.Alpha&gt;
    /// </code>
    /// </remarks>
    type SimpleMixedToken =
        U2<TokenGamma, BasicToken<Kind>>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// BasicToken&lt;Kind.Alpha&gt; | BasicToken&lt;Kind.Beta&gt;
    /// </code>
    /// </remarks>
    type BasicKindToken =
        BasicToken<Kind>

    /// Doesn't need any adjustments or comments
    type Simple =
        Token<Kind>

module Other =

    /// <summary>requires namespace</summary>
    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// ns.TokenBC | ns.Token&lt;ns.Kind.Gamma&gt; | ns.Token&lt;ns.Kind.Delta | ns.Kind.Alpha&gt;
    /// </code>
    /// </remarks>
    type InOther1 =
        Ns.Token<Ns.Kind>

    /// <summary>requires namespace</summary>
    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// ns.Token&lt;ns.Kind.Delta | ns.Kind.Alpha&gt;
    /// </code>
    /// </remarks>
    type InOther2 =
        Ns.Token<Ns.Kind>

    /// Doesn't need any adjustments or comments
    type Simple =
        Ns.Token<Ns.Kind>
