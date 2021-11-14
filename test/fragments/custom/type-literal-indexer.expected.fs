// ts2fable 0.0.0
module rec ``type-literal-indexer``

#nowarn "3390" // disable warnings for invalid XML comments

open System
open Fable.Core
open Fable.Core.JS


type [<AllowNullLiteral>] IExports =
    abstract fIndexerOut: unit -> FIndexerOutReturn
    abstract fIndexerIn: i: FIndexerInI -> unit
    abstract fIndexerIn2: i: FIndexerIn2I -> unit
    /// <summary>Some description</summary>
    /// <remarks>some remarks</remarks>
    abstract fIndexerIn3WithComments: i: FIndexerIn3WithCommentsI -> unit

type [<AllowNullLiteral>] FIndexerOutReturn =
    [<EmitIndexer>] abstract Item: key: string -> string with get, set

/// <summary>
/// Typescript interface contains an <see href="https://www.typescriptlang.org/docs/handbook/2/objects.html#index-signatures">index signature</see> (like <c>{ [key:string]: string }</c>).  
/// Unlike an indexer in F#, index signatures index over a type's members. 
/// 
/// As such an index signature cannot be implemented via regular F# Indexer (<c>Item</c> property),
/// but instead by just specifying fields.
/// 
/// Easiest way to declare such a type is with an Anonymous Record and force it into the function.  
/// For example:  
/// <code lang="fsharp">
/// type I =
///     [&lt;EmitIndexer&gt;]
///     abstract Item: string -&gt; string
/// let f (i: I) = jsNative
/// 
/// let t = {| Value1 = "foo"; Value2 = "bar" |}
/// f (!! t)
/// </code>
/// </summary>
type [<AllowNullLiteral>] FIndexerInI =
    [<EmitIndexer>] abstract Item: key: string -> string with get, set

/// <summary>
/// Typescript interface contains an <see href="https://www.typescriptlang.org/docs/handbook/2/objects.html#index-signatures">index signature</see> (like <c>{ [key:string]: string }</c>).  
/// Unlike an indexer in F#, index signatures index over a type's members. 
/// 
/// As such an index signature cannot be implemented via regular F# Indexer (<c>Item</c> property),
/// but instead by just specifying fields.
/// 
/// Easiest way to declare such a type is with an Anonymous Record and force it into the function.  
/// For example:  
/// <code lang="fsharp">
/// type I =
///     [&lt;EmitIndexer&gt;]
///     abstract Item: string -&gt; string
/// let f (i: I) = jsNative
/// 
/// let t = {| Value1 = "foo"; Value2 = "bar" |}
/// f (!! t)
/// </code>
/// </summary>
type [<AllowNullLiteral>] FIndexerIn2I =
    [<EmitIndexer>] abstract Item: key: string -> string with get, set
    [<EmitIndexer>] abstract Item: index: float -> string with get, set

/// <summary>
/// Typescript interface contains an <see href="https://www.typescriptlang.org/docs/handbook/2/objects.html#index-signatures">index signature</see> (like <c>{ [key:string]: string }</c>).  
/// Unlike an indexer in F#, index signatures index over a type's members. 
/// 
/// As such an index signature cannot be implemented via regular F# Indexer (<c>Item</c> property),
/// but instead by just specifying fields.
/// 
/// Easiest way to declare such a type is with an Anonymous Record and force it into the function.  
/// For example:  
/// <code lang="fsharp">
/// type I =
///     [&lt;EmitIndexer&gt;]
///     abstract Item: string -&gt; string
/// let f (i: I) = jsNative
/// 
/// let t = {| Value1 = "foo"; Value2 = "bar" |}
/// f (!! t)
/// </code>
/// </summary>
type [<AllowNullLiteral>] FIndexerIn3WithCommentsI =
    [<EmitIndexer>] abstract Item: key: string -> string with get, set

