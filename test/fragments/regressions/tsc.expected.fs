// ts2fable 0.0.0
module rec tsc

#nowarn "3390" // disable warnings for invalid XML comments

open System
open Fable.Core
open Fable.Core.JS


module Ts =

    type [<RequireQualifiedAccess>] SyntaxKind =
        | Unknown = 0
        | ObjectLiteralExpression = 207
        | JsxAttributes = 289

    type [<AllowNullLiteral>] CustomTransformers =
        abstract afterDeclarations: ResizeArray<U3<TransformerFactory<Bundle>, TransformerFactory<SourceFile>, CustomTransformerFactory>> option with get, set

    /// <remarks>
    /// Changed inherit!
    /// 
    /// Original:
    /// <code lang="fsharp">
    /// inherit ObjectLiteralExpression&lt;ObjectLiteralElementLike&gt;
    /// </code>
    /// Changed <c>ObjectLiteralElementLike</c> to common base type <c>ObjectLiteralElement</c>:
    /// <code lang="fsharp">
    /// inherit ObjectLiteralExpression&lt;ObjectLiteralElement&gt;`
    /// </code>
    /// </remarks>
    type [<AllowNullLiteral>] ObjectLiteralExpression =
        inherit ObjectLiteralExpressionBase<ObjectLiteralElement>
        inherit JSDocContainer
        abstract kind: SyntaxKind

    /// <remarks>
    /// Changed inherit!
    /// 
    /// Original:
    /// <code lang="fsharp">
    /// inherit ObjectLiteralExpression&lt;JsxAttributeLike&gt;
    /// </code>
    /// Changed <c>JsxAttributeLike</c> to common base type <c>ObjectLiteralElement</c>:
    /// <code lang="fsharp">
    /// inherit ObjectLiteralExpression&lt;ObjectLiteralElement&gt;`
    /// </code>
    /// </remarks>
    type [<AllowNullLiteral>] JsxAttributes =
        inherit ObjectLiteralExpressionBase<ObjectLiteralElement>
        abstract kind: SyntaxKind
        abstract parent: JsxOpeningLikeElement