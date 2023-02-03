// ts2fable 0.0.0
module rec ``#466-generic-type-param-default-refs-other-type-param``

#nowarn "3390" // disable warnings for invalid XML comments

open System
open Fable.Core
open Fable.Core.JS


type [<AllowNullLiteral>] Node =
    abstract kind: string

type VisitResult<'T> =
    U2<'T, ResizeArray<Node>>

/// <summary>in F#: 3 <c>Visitor</c>s: with 2 generic type params, with 1 type param, without type param</summary>
type Visitor =
    Visitor<Node, Node option>

/// <summary>in F#: 3 <c>Visitor</c>s: with 2 generic type params, with 1 type param, without type param</summary>
type Visitor<'TIn when 'TIn :> Node> =
    Visitor<'TIn, 'TIn option>

/// <summary>in F#: 3 <c>Visitor</c>s: with 2 generic type params, with 1 type param, without type param</summary>
type [<AllowNullLiteral>] Visitor<'TIn, 'TOut when 'TIn :> Node> =
    /// <summary>in F#: 3 <c>Visitor</c>s: with 2 generic type params, with 1 type param, without type param</summary>
    [<Emit("$0($1...)")>] abstract Invoke: node: 'TIn -> VisitResult<'TOut>

type [<AllowNullLiteral>] W<'T> =
    interface end

type A =
    A<Node, W<Node>>

type A<'TA when 'TA :> Node> =
    A<'TA, W<'TA>>

type [<AllowNullLiteral>] A<'TA, 'TB when 'TA :> Node and 'TB :> W<'TA>> =
    interface end

/// <summary>
/// Note: This isn't valid in F#: <c>'TB :&gt; 'TA</c> is not allowed!:
/// <code lang="fsharp">
/// type E&lt;'TA, 'TB when 'TA :&gt; Node and 'TB :&gt; 'TA&gt; = interface end
/// //                                   ^^^^^^^^^^ Invalid constraint: the type used for the constraint is sealed, which means the constraint could only be satisfied by at most one solution
/// //                                   ^^^^^^^^^^ This construct causes code to be less generic than indicated by the type annotations. The type variable 'TB has been constrained to be type ''TA'.
/// //          ^^^ This type parameter has been used in a way that constrains it to always be '#Node'
/// </code>
/// </summary>
type B =
    B<Node, Node>

/// <summary>
/// Note: This isn't valid in F#: <c>'TB :&gt; 'TA</c> is not allowed!:
/// <code lang="fsharp">
/// type E&lt;'TA, 'TB when 'TA :&gt; Node and 'TB :&gt; 'TA&gt; = interface end
/// //                                   ^^^^^^^^^^ Invalid constraint: the type used for the constraint is sealed, which means the constraint could only be satisfied by at most one solution
/// //                                   ^^^^^^^^^^ This construct causes code to be less generic than indicated by the type annotations. The type variable 'TB has been constrained to be type ''TA'.
/// //          ^^^ This type parameter has been used in a way that constrains it to always be '#Node'
/// </code>
/// </summary>
type B<'TA when 'TA :> Node> =
    B<'TA, 'TA>

/// <summary>
/// Note: This isn't valid in F#: <c>'TB :&gt; 'TA</c> is not allowed!:
/// <code lang="fsharp">
/// type E&lt;'TA, 'TB when 'TA :&gt; Node and 'TB :&gt; 'TA&gt; = interface end
/// //                                   ^^^^^^^^^^ Invalid constraint: the type used for the constraint is sealed, which means the constraint could only be satisfied by at most one solution
/// //                                   ^^^^^^^^^^ This construct causes code to be less generic than indicated by the type annotations. The type variable 'TB has been constrained to be type ''TA'.
/// //          ^^^ This type parameter has been used in a way that constrains it to always be '#Node'
/// </code>
/// </summary>
type [<AllowNullLiteral>] B<'TA, 'TB when 'TA :> Node and 'TB :> 'TA> =
    interface end

type C =
    C<Node, W<Node>, W<W<Node>>, W<W<Node>>>

type C<'TA when 'TA :> Node> =
    C<'TA, W<'TA>, W<W<'TA>>, W<W<'TA>>>

type C<'TA, 'TB when 'TA :> Node and 'TB :> W<'TA>> =
    C<'TA, 'TB, W<'TB>, W<'TB>>

type C<'TA, 'TB, 'TC when 'TA :> Node and 'TB :> W<'TA> and 'TC :> W<'TB>> =
    C<'TA, 'TB, 'TC, W<'TB>>

type [<AllowNullLiteral>] C<'TA, 'TB, 'TC, 'TD when 'TA :> Node and 'TB :> W<'TA> and 'TC :> W<'TB> and 'TD :> W<'TB>> =
    interface end

type D =
    D<Node, ResizeArray<Node>, W<ResizeArray<Node>>>

type D<'TA when 'TA :> Node> =
    D<'TA, ResizeArray<'TA>, W<ResizeArray<'TA>>>

type D<'TA, 'TB when 'TA :> Node> =
    D<'TA, 'TB, W<'TB>>

type [<AllowNullLiteral>] D<'TA, 'TB, 'TC when 'TA :> Node> =
    interface end
