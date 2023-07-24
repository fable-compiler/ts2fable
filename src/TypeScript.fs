// ts2fable 0.0.0
module rec TypeScript

#nowarn "3390" // disable warnings for invalid XML comments
#nowarn "0044" // disable warnings for `Obsolete` usage

open System
open Fable.Core
open Fable.Core.JS

type Array<'T> = System.Collections.Generic.IList<'T>
type ReadonlyArray<'T> = System.Collections.Generic.IReadOnlyList<'T>
type ReadonlyMap<'K, 'V> = Map<'K, 'V>
type Symbol = obj

let [<ImportDefault("typescript")>] ts: Ts.IExports = jsNative

module Ts =
    let [<Import("ScriptSnapshot","typescript/ts")>] scriptSnapshot: ScriptSnapshot.IExports = jsNative

    type [<AllowNullLiteral>] IExports =
        abstract versionMajorMinor: obj
        /// The version of the TypeScript compiler release
        abstract version: string
        abstract OperationCanceledException: OperationCanceledExceptionStatic
        abstract sys: System with get, set
        abstract tokenToString: t: SyntaxKind -> string option
        abstract getPositionOfLineAndCharacter: sourceFile: SourceFileLike * line: float * character: float -> float
        abstract getLineAndCharacterOfPosition: sourceFile: SourceFileLike * position: float -> LineAndCharacter
        abstract isWhiteSpaceLike: ch: float -> bool
        /// Does not include line breaks. For that, see isWhiteSpaceLike.
        abstract isWhiteSpaceSingleLine: ch: float -> bool
        abstract isLineBreak: ch: float -> bool
        abstract couldStartTrivia: text: string * pos: float -> bool
        abstract forEachLeadingCommentRange: text: string * pos: float * cb: (float -> float -> CommentKind -> bool -> 'U) -> 'U option
        abstract forEachLeadingCommentRange: text: string * pos: float * cb: (float -> float -> CommentKind -> bool -> 'T -> 'U) * state: 'T -> 'U option
        abstract forEachTrailingCommentRange: text: string * pos: float * cb: (float -> float -> CommentKind -> bool -> 'U) -> 'U option
        abstract forEachTrailingCommentRange: text: string * pos: float * cb: (float -> float -> CommentKind -> bool -> 'T -> 'U) * state: 'T -> 'U option
        abstract reduceEachLeadingCommentRange: text: string * pos: float * cb: (float -> float -> CommentKind -> bool -> 'T -> 'U) * state: 'T * initial: 'U -> 'U option
        abstract reduceEachTrailingCommentRange: text: string * pos: float * cb: (float -> float -> CommentKind -> bool -> 'T -> 'U) * state: 'T * initial: 'U -> 'U option
        abstract getLeadingCommentRanges: text: string * pos: float -> CommentRange[] option
        abstract getTrailingCommentRanges: text: string * pos: float -> CommentRange[] option
        /// Optionally, get the shebang
        abstract getShebang: text: string -> string option
        abstract isIdentifierStart: ch: float * languageVersion: ScriptTarget option -> bool
        abstract isIdentifierPart: ch: float * languageVersion: ScriptTarget option * ?identifierVariant: LanguageVariant -> bool
        abstract createScanner: languageVersion: ScriptTarget * skipTrivia: bool * ?languageVariant: LanguageVariant * ?textInitial: string * ?onError: ErrorCallback * ?start: float * ?length: float -> Scanner
        abstract isExternalModuleNameRelative: moduleName: string -> bool
        abstract sortAndDeduplicateDiagnostics: diagnostics: 'T[] -> SortedReadonlyArray<'T> when 'T :> Diagnostic
        abstract getDefaultLibFileName: options: CompilerOptions -> string
        abstract textSpanEnd: span: TextSpan -> float
        abstract textSpanIsEmpty: span: TextSpan -> bool
        abstract textSpanContainsPosition: span: TextSpan * position: float -> bool
        abstract textSpanContainsTextSpan: span: TextSpan * other: TextSpan -> bool
        abstract textSpanOverlapsWith: span: TextSpan * other: TextSpan -> bool
        abstract textSpanOverlap: span1: TextSpan * span2: TextSpan -> TextSpan option
        abstract textSpanIntersectsWithTextSpan: span: TextSpan * other: TextSpan -> bool
        abstract textSpanIntersectsWith: span: TextSpan * start: float * length: float -> bool
        abstract decodedTextSpanIntersectsWith: start1: float * length1: float * start2: float * length2: float -> bool
        abstract textSpanIntersectsWithPosition: span: TextSpan * position: float -> bool
        abstract textSpanIntersection: span1: TextSpan * span2: TextSpan -> TextSpan option
        abstract createTextSpan: start: float * length: float -> TextSpan
        abstract createTextSpanFromBounds: start: float * ``end``: float -> TextSpan
        abstract textChangeRangeNewSpan: range: TextChangeRange -> TextSpan
        abstract textChangeRangeIsUnchanged: range: TextChangeRange -> bool
        abstract createTextChangeRange: span: TextSpan * newLength: float -> TextChangeRange
        /// Called to merge all the changes that occurred across several versions of a script snapshot
        /// into a single change.  i.e. if a user keeps making successive edits to a script we will
        /// have a text change from V1 to V2, V2 to V3, ..., Vn.
        /// 
        /// This function will then merge those changes into a single change range valid between V1 and
        /// Vn.
        abstract collapseTextChangeRangesAcrossMultipleVersions: changes: TextChangeRange[] -> TextChangeRange
        abstract getTypeParameterOwner: d: Declaration -> Declaration option
        abstract isParameterPropertyDeclaration: node: Node * parent: Node -> bool
        abstract isEmptyBindingPattern: node: BindingName -> bool
        abstract isEmptyBindingElement: node: U2<BindingElement, ArrayBindingElement> -> bool
        abstract walkUpBindingElementsAndPatterns: binding: BindingElement -> U2<VariableDeclaration, ParameterDeclaration>
        abstract getCombinedModifierFlags: node: Declaration -> ModifierFlags
        abstract getCombinedNodeFlags: node: Node -> NodeFlags
        /// Checks to see if the locale is in the appropriate format,
        /// and if it is, attempts to set the appropriate language.
        abstract validateLocaleAndSetLanguage: locale: string * sys: {| getExecutingFilePath: unit -> string; resolvePath: string -> string; fileExists: string -> bool; readFile: string -> string option |} * ?errors: Diagnostic[] -> unit
        abstract getOriginalNode: node: Node -> Node
        abstract getOriginalNode: node: Node * nodeTest: (Node -> bool) -> 'T when 'T :> Node
        abstract getOriginalNode: node: Node option -> Node option
        abstract getOriginalNode: node: Node option * nodeTest: (Node -> bool) -> 'T option when 'T :> Node
        /// Iterates through the parent chain of a node and performs the callback on each parent until the callback
        /// returns a truthy value, then returns that value.
        /// If no such value is found, it applies the callback until the parent pointer is undefined or the callback returns "quit"
        /// At that point findAncestor returns undefined.
        abstract findAncestor: node: Node option * callback: (Node -> bool) -> 'T option when 'T :> Node
        abstract findAncestor: node: Node option * callback: (Node -> U2<bool, string>) -> Node option
        /// <summary>Gets a value indicating whether a node originated in the parse tree.</summary>
        /// <param name="node">The node to test.</param>
        abstract isParseTreeNode: node: Node -> bool
        /// <summary>Gets the original parse tree node for a node.</summary>
        /// <param name="node">The original node.</param>
        /// <returns>The original parse tree node if found; otherwise, undefined.</returns>
        abstract getParseTreeNode: node: Node option -> Node option
        /// <summary>Gets the original parse tree node for a node.</summary>
        /// <param name="node">The original node.</param>
        /// <param name="nodeTest">A callback used to ensure the correct type of parse tree node is returned.</param>
        /// <returns>The original parse tree node if found; otherwise, undefined.</returns>
        abstract getParseTreeNode: node: 'T option * ?nodeTest: (Node -> bool) -> 'T option when 'T :> Node
        /// Add an extra underscore to identifiers that start with two underscores to avoid issues with magic names like '__proto__'
        abstract escapeLeadingUnderscores: identifier: string -> __String
        /// <summary>Remove extra underscore from escaped identifier text content.</summary>
        /// <param name="identifier">The escaped identifier text.</param>
        /// <returns>The unescaped identifier text.</returns>
        abstract unescapeLeadingUnderscores: identifier: __String -> string
        abstract idText: identifierOrPrivateName: U2<Identifier, PrivateIdentifier> -> string
        /// If the text of an Identifier matches a keyword (including contextual and TypeScript-specific keywords), returns the
        /// SyntaxKind for the matching keyword.
        abstract identifierToKeywordKind: node: Identifier -> KeywordSyntaxKind option
        abstract symbolName: symbol: Symbol -> string
        abstract getNameOfJSDocTypedef: declaration: JSDocTypedefTag -> U2<Identifier, PrivateIdentifier> option
        abstract getNameOfDeclaration: declaration: U2<Declaration, Expression> option -> DeclarationName option
        abstract getDecorators: node: HasDecorators -> Decorator[] option
        abstract getModifiers: node: HasModifiers -> Modifier[] option
        /// <summary>Gets the JSDoc parameter tags for the node if present.</summary>
        /// <remarks>
        /// Returns any JSDoc param tag whose name matches the provided
        /// parameter, whether a param tag on a containing function
        /// expression, or a param tag on a variable declaration whose
        /// initializer is the containing function. The tags closest to the
        /// node are returned first, so in the previous example, the param
        /// tag on the containing function expression would be first.
        /// 
        /// For binding patterns, parameter tags are matched by position.
        /// </remarks>
        abstract getJSDocParameterTags: param: ParameterDeclaration -> JSDocParameterTag[]
        /// <summary>Gets the JSDoc type parameter tags for the node if present.</summary>
        /// <remarks>
        /// Returns any JSDoc template tag whose names match the provided
        /// parameter, whether a template tag on a containing function
        /// expression, or a template tag on a variable declaration whose
        /// initializer is the containing function. The tags closest to the
        /// node are returned first, so in the previous example, the template
        /// tag on the containing function expression would be first.
        /// </remarks>
        abstract getJSDocTypeParameterTags: param: TypeParameterDeclaration -> JSDocTemplateTag[]
        /// <summary>Return true if the node has JSDoc parameter tags.</summary>
        /// <remarks>
        /// Includes parameter tags that are not directly on the node,
        /// for example on a variable declaration whose initializer is a function expression.
        /// </remarks>
        abstract hasJSDocParameterTags: node: U2<FunctionLikeDeclaration, SignatureDeclaration> -> bool
        /// Gets the JSDoc augments tag for the node if present
        abstract getJSDocAugmentsTag: node: Node -> JSDocAugmentsTag option
        /// Gets the JSDoc implements tags for the node if present
        abstract getJSDocImplementsTags: node: Node -> JSDocImplementsTag[]
        /// Gets the JSDoc class tag for the node if present
        abstract getJSDocClassTag: node: Node -> JSDocClassTag option
        /// Gets the JSDoc public tag for the node if present
        abstract getJSDocPublicTag: node: Node -> JSDocPublicTag option
        /// Gets the JSDoc private tag for the node if present
        abstract getJSDocPrivateTag: node: Node -> JSDocPrivateTag option
        /// Gets the JSDoc protected tag for the node if present
        abstract getJSDocProtectedTag: node: Node -> JSDocProtectedTag option
        /// Gets the JSDoc protected tag for the node if present
        abstract getJSDocReadonlyTag: node: Node -> JSDocReadonlyTag option
        abstract getJSDocOverrideTagNoCache: node: Node -> JSDocOverrideTag option
        /// Gets the JSDoc deprecated tag for the node if present
        abstract getJSDocDeprecatedTag: node: Node -> JSDocDeprecatedTag option
        /// Gets the JSDoc enum tag for the node if present
        abstract getJSDocEnumTag: node: Node -> JSDocEnumTag option
        /// Gets the JSDoc this tag for the node if present
        abstract getJSDocThisTag: node: Node -> JSDocThisTag option
        /// Gets the JSDoc return tag for the node if present
        abstract getJSDocReturnTag: node: Node -> JSDocReturnTag option
        /// Gets the JSDoc template tag for the node if present
        abstract getJSDocTemplateTag: node: Node -> JSDocTemplateTag option
        abstract getJSDocSatisfiesTag: node: Node -> JSDocSatisfiesTag option
        /// Gets the JSDoc type tag for the node if present and valid
        abstract getJSDocTypeTag: node: Node -> JSDocTypeTag option
        /// <summary>Gets the type node for the node if provided via JSDoc.</summary>
        /// <remarks>
        /// The search includes any JSDoc param tag that relates
        /// to the provided parameter, for example a type tag on the
        /// parameter itself, or a param tag on a containing function
        /// expression, or a param tag on a variable declaration whose
        /// initializer is the containing function. The tags closest to the
        /// node are examined first, so in the previous example, the type
        /// tag directly on the node would be returned.
        /// </remarks>
        abstract getJSDocType: node: Node -> TypeNode option
        /// <summary>Gets the return type node for the node if provided via JSDoc return tag or type tag.</summary>
        /// <remarks>
        /// <c>getJSDocReturnTag</c> just gets the whole JSDoc tag. This function
        /// gets the type from inside the braces, after the fat arrow, etc.
        /// </remarks>
        abstract getJSDocReturnType: node: Node -> TypeNode option
        /// Get all JSDoc tags related to a node, including those on parent nodes.
        abstract getJSDocTags: node: Node -> JSDocTag[]
        /// Gets all JSDoc tags that match a specified predicate
        abstract getAllJSDocTags: node: Node * predicate: (JSDocTag -> bool) -> 'T[] when 'T :> JSDocTag
        /// Gets all JSDoc tags of a specified kind
        abstract getAllJSDocTagsOfKind: node: Node * kind: SyntaxKind -> JSDocTag[]
        /// Gets the text of a jsdoc comment, flattening links to their text.
        abstract getTextOfJSDocComment: ?comment: U2<string, JSDocComment[]> -> string option
        /// <summary>
        /// Gets the effective type parameters. If the node was parsed in a
        /// JavaScript file, gets the type parameters from the <c>@template</c> tag from JSDoc.
        /// 
        /// This does *not* return type parameters from a jsdoc reference to a generic type, eg
        /// 
        /// type Id = &lt;T&gt;(x: T) =&gt; T
        /// /** @type {Id} /
        /// function id(x) { return x }
        /// </summary>
        abstract getEffectiveTypeParameterDeclarations: node: DeclarationWithTypeParameters -> TypeParameterDeclaration[]
        abstract getEffectiveConstraintOfTypeParameter: node: TypeParameterDeclaration -> TypeNode option
        abstract isMemberName: node: Node -> bool
        abstract isPropertyAccessChain: node: Node -> bool
        abstract isElementAccessChain: node: Node -> bool
        abstract isCallChain: node: Node -> bool
        abstract isOptionalChain: node: Node -> bool
        abstract isNullishCoalesce: node: Node -> bool
        abstract isConstTypeReference: node: Node -> bool
        abstract skipPartiallyEmittedExpressions: node: Expression -> Expression
        abstract skipPartiallyEmittedExpressions: node: Node -> Node
        abstract isNonNullChain: node: Node -> bool
        abstract isBreakOrContinueStatement: node: Node -> bool
        abstract isNamedExportBindings: node: Node -> bool
        abstract isJSDocPropertyLikeTag: node: Node -> bool
        /// True if kind is of some token syntax kind.
        /// For example, this is true for an IfKeyword but not for an IfStatement.
        /// Literals are considered tokens, except TemplateLiteral, but does include TemplateHead/Middle/Tail.
        abstract isTokenKind: kind: SyntaxKind -> bool
        /// True if node is of some token syntax kind.
        /// For example, this is true for an IfKeyword but not for an IfStatement.
        /// Literals are considered tokens, except TemplateLiteral, but does include TemplateHead/Middle/Tail.
        abstract isToken: n: Node -> bool
        abstract isLiteralExpression: node: Node -> bool
        abstract isTemplateLiteralToken: node: Node -> bool
        abstract isTemplateMiddleOrTemplateTail: node: Node -> bool
        abstract isImportOrExportSpecifier: node: Node -> bool
        abstract isTypeOnlyImportDeclaration: node: Node -> bool
        abstract isTypeOnlyExportDeclaration: node: Node -> bool
        abstract isTypeOnlyImportOrExportDeclaration: node: Node -> bool
        abstract isAssertionKey: node: Node -> bool
        abstract isStringTextContainingNode: node: Node -> bool
        abstract isModifier: node: Node -> bool
        abstract isEntityName: node: Node -> bool
        abstract isPropertyName: node: Node -> bool
        abstract isBindingName: node: Node -> bool
        abstract isFunctionLike: node: Node option -> bool
        abstract isClassElement: node: Node -> bool
        abstract isClassLike: node: Node -> bool
        abstract isAccessor: node: Node -> bool
        abstract isAutoAccessorPropertyDeclaration: node: Node -> bool
        abstract isModifierLike: node: Node -> bool
        abstract isTypeElement: node: Node -> bool
        abstract isClassOrTypeElement: node: Node -> bool
        abstract isObjectLiteralElementLike: node: Node -> bool
        /// <summary>
        /// Node test that determines whether a node is a valid type node.
        /// This differs from the <c>isPartOfTypeNode</c> function which determines whether a node is *part*
        /// of a TypeNode.
        /// </summary>
        abstract isTypeNode: node: Node -> bool
        abstract isFunctionOrConstructorTypeNode: node: Node -> bool
        abstract isArrayBindingElement: node: Node -> bool
        abstract isPropertyAccessOrQualifiedName: node: Node -> bool
        abstract isCallLikeExpression: node: Node -> bool
        abstract isCallOrNewExpression: node: Node -> bool
        abstract isTemplateLiteral: node: Node -> bool
        abstract isLeftHandSideExpression: node: Node -> bool
        abstract isLiteralTypeLiteral: node: Node -> bool
        /// Determines whether a node is an expression based only on its kind.
        abstract isExpression: node: Node -> bool
        abstract isAssertionExpression: node: Node -> bool
        [<Emit("$0.isIterationStatement($1,false)")>] abstract isIterationStatement_false: node: Node -> bool
        abstract isIterationStatement: node: Node * lookInLabeledStatements: bool -> bool
        abstract isConciseBody: node: Node -> bool
        abstract isForInitializer: node: Node -> bool
        abstract isModuleBody: node: Node -> bool
        abstract isNamedImportBindings: node: Node -> bool
        abstract isStatement: node: Node -> bool
        abstract isModuleReference: node: Node -> bool
        abstract isJsxTagNameExpression: node: Node -> bool
        abstract isJsxChild: node: Node -> bool
        abstract isJsxAttributeLike: node: Node -> bool
        abstract isStringLiteralOrJsxExpression: node: Node -> bool
        abstract isJsxOpeningLikeElement: node: Node -> bool
        abstract isCaseOrDefaultClause: node: Node -> bool
        /// True if node is of a kind that may contain comment text.
        abstract isJSDocCommentContainingNode: node: Node -> bool
        abstract isSetAccessor: node: Node -> bool
        abstract isGetAccessor: node: Node -> bool
        /// True if has initializer node attached to it.
        abstract hasOnlyExpressionInitializer: node: Node -> bool
        abstract isObjectLiteralElement: node: Node -> bool
        abstract isStringLiteralLike: node: U2<Node, FileReference> -> bool
        abstract isJSDocLinkLike: node: Node -> bool
        abstract hasRestParameter: s: U2<SignatureDeclaration, JSDocSignature> -> bool
        abstract isRestParameter: node: U2<ParameterDeclaration, JSDocParameterTag> -> bool
        abstract unchangedTextChangeRange: TextChangeRange with get, set
        /// <summary>
        /// This function checks multiple locations for JSDoc comments that apply to a host node.
        /// At each location, the whole comment may apply to the node, or only a specific tag in
        /// the comment. In the first case, location adds the entire <see cref="JSDoc" /> object. In the
        /// second case, it adds the applicable <see cref="JSDocTag" />.
        /// 
        /// For example, a JSDoc comment before a parameter adds the entire <see cref="JSDoc" />. But a
        /// <c>@param</c> tag on the parent function only adds the <see cref="JSDocTag" /> for the <c>@param</c>.
        /// 
        /// <code lang="ts">
        /// /** JSDoc will be returned for `a` *\/
        /// const a = 0
        /// /**
        ///  * Entire JSDoc will be returned for `b`
        ///  * @param c JSDocTag will be returned for `c`
        ///  *\/
        /// function b(/** JSDoc will be returned for `c` *\/ c) {}
        /// </code>
        /// </summary>
        abstract getJSDocCommentsAndTags: hostNode: Node -> U2<JSDoc, JSDocTag>[]
        /// Create an external source map source file reference
        abstract createSourceMapSource: fileName: string * text: string * ?skipTrivia: (float -> float) -> SourceMapSource
        abstract setOriginalNode: node: 'T * original: Node option -> 'T when 'T :> Node
        abstract factory: NodeFactory
        /// <summary>Clears any <c>EmitNode</c> entries from parse-tree nodes.</summary>
        /// <param name="sourceFile">A source file.</param>
        abstract disposeEmitNodes: sourceFile: SourceFile option -> unit
        /// Sets flags that control emit behavior of a node.
        abstract setEmitFlags: node: 'T * emitFlags: EmitFlags -> 'T when 'T :> Node
        /// Gets a custom text range to use when emitting source maps.
        abstract getSourceMapRange: node: Node -> SourceMapRange
        /// Sets a custom text range to use when emitting source maps.
        abstract setSourceMapRange: node: 'T * range: SourceMapRange option -> 'T when 'T :> Node
        /// Gets the TextRange to use for source maps for a token of a node.
        abstract getTokenSourceMapRange: node: Node * token: SyntaxKind -> SourceMapRange option
        /// Sets the TextRange to use for source maps for a token of a node.
        abstract setTokenSourceMapRange: node: 'T * token: SyntaxKind * range: SourceMapRange option -> 'T when 'T :> Node
        /// Gets a custom text range to use when emitting comments.
        abstract getCommentRange: node: Node -> TextRange
        /// Sets a custom text range to use when emitting comments.
        abstract setCommentRange: node: 'T * range: TextRange -> 'T when 'T :> Node
        abstract getSyntheticLeadingComments: node: Node -> SynthesizedComment[] option
        abstract setSyntheticLeadingComments: node: 'T * comments: SynthesizedComment[] option -> 'T when 'T :> Node
        abstract addSyntheticLeadingComment: node: 'T * kind: SyntaxKind * text: string * ?hasTrailingNewLine: bool -> 'T when 'T :> Node
        abstract getSyntheticTrailingComments: node: Node -> SynthesizedComment[] option
        abstract setSyntheticTrailingComments: node: 'T * comments: SynthesizedComment[] option -> 'T when 'T :> Node
        abstract addSyntheticTrailingComment: node: 'T * kind: SyntaxKind * text: string * ?hasTrailingNewLine: bool -> 'T when 'T :> Node
        abstract moveSyntheticComments: node: 'T * original: Node -> 'T when 'T :> Node
        /// Gets the constant value to emit for an expression representing an enum.
        abstract getConstantValue: node: AccessExpression -> U2<string, float> option
        /// Sets the constant value to emit for an expression.
        abstract setConstantValue: node: AccessExpression * value: U2<string, float> -> AccessExpression
        /// Adds an EmitHelper to a node.
        abstract addEmitHelper: node: 'T * helper: EmitHelper -> 'T when 'T :> Node
        /// Add EmitHelpers to a node.
        abstract addEmitHelpers: node: 'T * helpers: EmitHelper[] option -> 'T when 'T :> Node
        /// Removes an EmitHelper from a node.
        abstract removeEmitHelper: node: Node * helper: EmitHelper -> bool
        /// Gets the EmitHelpers of a node.
        abstract getEmitHelpers: node: Node -> EmitHelper[] option
        /// Moves matching emit helpers from a source node to a target node.
        abstract moveEmitHelpers: source: Node * target: Node * predicate: (EmitHelper -> bool) -> unit
        abstract isNumericLiteral: node: Node -> bool
        abstract isBigIntLiteral: node: Node -> bool
        abstract isStringLiteral: node: Node -> bool
        abstract isJsxText: node: Node -> bool
        abstract isRegularExpressionLiteral: node: Node -> bool
        abstract isNoSubstitutionTemplateLiteral: node: Node -> bool
        abstract isTemplateHead: node: Node -> bool
        abstract isTemplateMiddle: node: Node -> bool
        abstract isTemplateTail: node: Node -> bool
        abstract isDotDotDotToken: node: Node -> bool
        abstract isPlusToken: node: Node -> bool
        abstract isMinusToken: node: Node -> bool
        abstract isAsteriskToken: node: Node -> bool
        abstract isExclamationToken: node: Node -> bool
        abstract isQuestionToken: node: Node -> bool
        abstract isColonToken: node: Node -> bool
        abstract isQuestionDotToken: node: Node -> bool
        abstract isEqualsGreaterThanToken: node: Node -> bool
        abstract isIdentifier: node: Node -> bool
        abstract isPrivateIdentifier: node: Node -> bool
        abstract isAssertsKeyword: node: Node -> bool
        abstract isAwaitKeyword: node: Node -> bool
        abstract isQualifiedName: node: Node -> bool
        abstract isComputedPropertyName: node: Node -> bool
        abstract isTypeParameterDeclaration: node: Node -> bool
        abstract isParameter: node: Node -> bool
        abstract isDecorator: node: Node -> bool
        abstract isPropertySignature: node: Node -> bool
        abstract isPropertyDeclaration: node: Node -> bool
        abstract isMethodSignature: node: Node -> bool
        abstract isMethodDeclaration: node: Node -> bool
        abstract isClassStaticBlockDeclaration: node: Node -> bool
        abstract isConstructorDeclaration: node: Node -> bool
        abstract isGetAccessorDeclaration: node: Node -> bool
        abstract isSetAccessorDeclaration: node: Node -> bool
        abstract isCallSignatureDeclaration: node: Node -> bool
        abstract isConstructSignatureDeclaration: node: Node -> bool
        abstract isIndexSignatureDeclaration: node: Node -> bool
        abstract isTypePredicateNode: node: Node -> bool
        abstract isTypeReferenceNode: node: Node -> bool
        abstract isFunctionTypeNode: node: Node -> bool
        abstract isConstructorTypeNode: node: Node -> bool
        abstract isTypeQueryNode: node: Node -> bool
        abstract isTypeLiteralNode: node: Node -> bool
        abstract isArrayTypeNode: node: Node -> bool
        abstract isTupleTypeNode: node: Node -> bool
        abstract isNamedTupleMember: node: Node -> bool
        abstract isOptionalTypeNode: node: Node -> bool
        abstract isRestTypeNode: node: Node -> bool
        abstract isUnionTypeNode: node: Node -> bool
        abstract isIntersectionTypeNode: node: Node -> bool
        abstract isConditionalTypeNode: node: Node -> bool
        abstract isInferTypeNode: node: Node -> bool
        abstract isParenthesizedTypeNode: node: Node -> bool
        abstract isThisTypeNode: node: Node -> bool
        abstract isTypeOperatorNode: node: Node -> bool
        abstract isIndexedAccessTypeNode: node: Node -> bool
        abstract isMappedTypeNode: node: Node -> bool
        abstract isLiteralTypeNode: node: Node -> bool
        abstract isImportTypeNode: node: Node -> bool
        abstract isTemplateLiteralTypeSpan: node: Node -> bool
        abstract isTemplateLiteralTypeNode: node: Node -> bool
        abstract isObjectBindingPattern: node: Node -> bool
        abstract isArrayBindingPattern: node: Node -> bool
        abstract isBindingElement: node: Node -> bool
        abstract isArrayLiteralExpression: node: Node -> bool
        abstract isObjectLiteralExpression: node: Node -> bool
        abstract isPropertyAccessExpression: node: Node -> bool
        abstract isElementAccessExpression: node: Node -> bool
        abstract isCallExpression: node: Node -> bool
        abstract isNewExpression: node: Node -> bool
        abstract isTaggedTemplateExpression: node: Node -> bool
        abstract isTypeAssertionExpression: node: Node -> bool
        abstract isParenthesizedExpression: node: Node -> bool
        abstract isFunctionExpression: node: Node -> bool
        abstract isArrowFunction: node: Node -> bool
        abstract isDeleteExpression: node: Node -> bool
        abstract isTypeOfExpression: node: Node -> bool
        abstract isVoidExpression: node: Node -> bool
        abstract isAwaitExpression: node: Node -> bool
        abstract isPrefixUnaryExpression: node: Node -> bool
        abstract isPostfixUnaryExpression: node: Node -> bool
        abstract isBinaryExpression: node: Node -> bool
        abstract isConditionalExpression: node: Node -> bool
        abstract isTemplateExpression: node: Node -> bool
        abstract isYieldExpression: node: Node -> bool
        abstract isSpreadElement: node: Node -> bool
        abstract isClassExpression: node: Node -> bool
        abstract isOmittedExpression: node: Node -> bool
        abstract isExpressionWithTypeArguments: node: Node -> bool
        abstract isAsExpression: node: Node -> bool
        abstract isSatisfiesExpression: node: Node -> bool
        abstract isNonNullExpression: node: Node -> bool
        abstract isMetaProperty: node: Node -> bool
        abstract isSyntheticExpression: node: Node -> bool
        abstract isPartiallyEmittedExpression: node: Node -> bool
        abstract isCommaListExpression: node: Node -> bool
        abstract isTemplateSpan: node: Node -> bool
        abstract isSemicolonClassElement: node: Node -> bool
        abstract isBlock: node: Node -> bool
        abstract isVariableStatement: node: Node -> bool
        abstract isEmptyStatement: node: Node -> bool
        abstract isExpressionStatement: node: Node -> bool
        abstract isIfStatement: node: Node -> bool
        abstract isDoStatement: node: Node -> bool
        abstract isWhileStatement: node: Node -> bool
        abstract isForStatement: node: Node -> bool
        abstract isForInStatement: node: Node -> bool
        abstract isForOfStatement: node: Node -> bool
        abstract isContinueStatement: node: Node -> bool
        abstract isBreakStatement: node: Node -> bool
        abstract isReturnStatement: node: Node -> bool
        abstract isWithStatement: node: Node -> bool
        abstract isSwitchStatement: node: Node -> bool
        abstract isLabeledStatement: node: Node -> bool
        abstract isThrowStatement: node: Node -> bool
        abstract isTryStatement: node: Node -> bool
        abstract isDebuggerStatement: node: Node -> bool
        abstract isVariableDeclaration: node: Node -> bool
        abstract isVariableDeclarationList: node: Node -> bool
        abstract isFunctionDeclaration: node: Node -> bool
        abstract isClassDeclaration: node: Node -> bool
        abstract isInterfaceDeclaration: node: Node -> bool
        abstract isTypeAliasDeclaration: node: Node -> bool
        abstract isEnumDeclaration: node: Node -> bool
        abstract isModuleDeclaration: node: Node -> bool
        abstract isModuleBlock: node: Node -> bool
        abstract isCaseBlock: node: Node -> bool
        abstract isNamespaceExportDeclaration: node: Node -> bool
        abstract isImportEqualsDeclaration: node: Node -> bool
        abstract isImportDeclaration: node: Node -> bool
        abstract isImportClause: node: Node -> bool
        abstract isImportTypeAssertionContainer: node: Node -> bool
        abstract isAssertClause: node: Node -> bool
        abstract isAssertEntry: node: Node -> bool
        abstract isNamespaceImport: node: Node -> bool
        abstract isNamespaceExport: node: Node -> bool
        abstract isNamedImports: node: Node -> bool
        abstract isImportSpecifier: node: Node -> bool
        abstract isExportAssignment: node: Node -> bool
        abstract isExportDeclaration: node: Node -> bool
        abstract isNamedExports: node: Node -> bool
        abstract isExportSpecifier: node: Node -> bool
        abstract isMissingDeclaration: node: Node -> bool
        abstract isNotEmittedStatement: node: Node -> bool
        abstract isExternalModuleReference: node: Node -> bool
        abstract isJsxElement: node: Node -> bool
        abstract isJsxSelfClosingElement: node: Node -> bool
        abstract isJsxOpeningElement: node: Node -> bool
        abstract isJsxClosingElement: node: Node -> bool
        abstract isJsxFragment: node: Node -> bool
        abstract isJsxOpeningFragment: node: Node -> bool
        abstract isJsxClosingFragment: node: Node -> bool
        abstract isJsxAttribute: node: Node -> bool
        abstract isJsxAttributes: node: Node -> bool
        abstract isJsxSpreadAttribute: node: Node -> bool
        abstract isJsxExpression: node: Node -> bool
        abstract isJsxNamespacedName: node: Node -> bool
        abstract isCaseClause: node: Node -> bool
        abstract isDefaultClause: node: Node -> bool
        abstract isHeritageClause: node: Node -> bool
        abstract isCatchClause: node: Node -> bool
        abstract isPropertyAssignment: node: Node -> bool
        abstract isShorthandPropertyAssignment: node: Node -> bool
        abstract isSpreadAssignment: node: Node -> bool
        abstract isEnumMember: node: Node -> bool
        abstract isSourceFile: node: Node -> bool
        abstract isBundle: node: Node -> bool
        abstract isJSDocTypeExpression: node: Node -> bool
        abstract isJSDocNameReference: node: Node -> bool
        abstract isJSDocMemberName: node: Node -> bool
        abstract isJSDocLink: node: Node -> bool
        abstract isJSDocLinkCode: node: Node -> bool
        abstract isJSDocLinkPlain: node: Node -> bool
        abstract isJSDocAllType: node: Node -> bool
        abstract isJSDocUnknownType: node: Node -> bool
        abstract isJSDocNullableType: node: Node -> bool
        abstract isJSDocNonNullableType: node: Node -> bool
        abstract isJSDocOptionalType: node: Node -> bool
        abstract isJSDocFunctionType: node: Node -> bool
        abstract isJSDocVariadicType: node: Node -> bool
        abstract isJSDocNamepathType: node: Node -> bool
        abstract isJSDoc: node: Node -> bool
        abstract isJSDocTypeLiteral: node: Node -> bool
        abstract isJSDocSignature: node: Node -> bool
        abstract isJSDocAugmentsTag: node: Node -> bool
        abstract isJSDocAuthorTag: node: Node -> bool
        abstract isJSDocClassTag: node: Node -> bool
        abstract isJSDocCallbackTag: node: Node -> bool
        abstract isJSDocPublicTag: node: Node -> bool
        abstract isJSDocPrivateTag: node: Node -> bool
        abstract isJSDocProtectedTag: node: Node -> bool
        abstract isJSDocReadonlyTag: node: Node -> bool
        abstract isJSDocOverrideTag: node: Node -> bool
        abstract isJSDocOverloadTag: node: Node -> bool
        abstract isJSDocDeprecatedTag: node: Node -> bool
        abstract isJSDocSeeTag: node: Node -> bool
        abstract isJSDocEnumTag: node: Node -> bool
        abstract isJSDocParameterTag: node: Node -> bool
        abstract isJSDocReturnTag: node: Node -> bool
        abstract isJSDocThisTag: node: Node -> bool
        abstract isJSDocTypeTag: node: Node -> bool
        abstract isJSDocTemplateTag: node: Node -> bool
        abstract isJSDocTypedefTag: node: Node -> bool
        abstract isJSDocUnknownTag: node: Node -> bool
        abstract isJSDocPropertyTag: node: Node -> bool
        abstract isJSDocImplementsTag: node: Node -> bool
        abstract isJSDocSatisfiesTag: node: Node -> bool
        abstract isJSDocThrowsTag: node: Node -> bool
        abstract isQuestionOrExclamationToken: node: Node -> bool
        abstract isIdentifierOrThisTypeNode: node: Node -> bool
        abstract isReadonlyKeywordOrPlusOrMinusToken: node: Node -> bool
        abstract isQuestionOrPlusOrMinusToken: node: Node -> bool
        abstract isModuleName: node: Node -> bool
        abstract isBinaryOperatorToken: node: Node -> bool
        abstract setTextRange: range: 'T * location: TextRange option -> 'T when 'T :> TextRange
        abstract canHaveModifiers: node: Node -> bool
        abstract canHaveDecorators: node: Node -> bool
        /// <summary>
        /// Invokes a callback for each child of the given node. The 'cbNode' callback is invoked for all child nodes
        /// stored in properties. If a 'cbNodes' callback is specified, it is invoked for embedded arrays; otherwise,
        /// embedded arrays are flattened and the 'cbNode' callback is invoked for each element. If a callback returns
        /// a truthy value, iteration stops and that value is returned. Otherwise, undefined is returned.
        /// </summary>
        /// <param name="node">a given node to visit its children</param>
        /// <param name="cbNode">a callback to be invoked for all child nodes</param>
        /// <param name="cbNodes">a callback to be invoked for embedded array</param>
        /// <remarks>
        /// <c>forEachChild</c> must visit the children of a node in the order
        /// that they appear in the source code. The language service depends on this property to locate nodes by position.
        /// </remarks>
        abstract forEachChild: node: Node * cbNode: (Node -> 'T option) * ?cbNodes: (Node[] -> 'T option) -> 'T option
        abstract createSourceFile: fileName: string * sourceText: string * languageVersionOrOptions: U2<ScriptTarget, CreateSourceFileOptions> * ?setParentNodes: bool * ?scriptKind: ScriptKind -> SourceFile
        abstract parseIsolatedEntityName: text: string * languageVersion: ScriptTarget -> EntityName option
        /// <summary>Parse json text into SyntaxTree and return node and parse errors if any</summary>
        /// <param name="fileName" />
        /// <param name="sourceText" />
        abstract parseJsonText: fileName: string * sourceText: string -> JsonSourceFile
        abstract isExternalModule: file: SourceFile -> bool
        abstract updateSourceFile: sourceFile: SourceFile * newText: string * textChangeRange: TextChangeRange * ?aggressiveChecks: bool -> SourceFile
        abstract parseCommandLine: commandLine: string[] * ?readFile: (string -> string option) -> ParsedCommandLine
        /// Reads the config file, reports errors if any and exits if the config file cannot be found
        abstract getParsedCommandLineOfConfigFile: configFileName: string * optionsToExtend: CompilerOptions option * host: ParseConfigFileHost * ?extendedConfigCache: Map<string, ExtendedConfigCacheEntry> * ?watchOptionsToExtend: WatchOptions * ?extraFileExtensions: FileExtensionInfo[] -> ParsedCommandLine option
        /// <summary>Read tsconfig.json file</summary>
        /// <param name="fileName">The path to the config file</param>
        abstract readConfigFile: fileName: string * readFile: (string -> string option) -> {| config: obj option; error: Diagnostic option |}
        /// <summary>Parse the text of the tsconfig.json file</summary>
        /// <param name="fileName">The path to the config file</param>
        /// <param name="jsonText">The text of the config file</param>
        abstract parseConfigFileTextToJson: fileName: string * jsonText: string -> {| config: obj option; error: Diagnostic option |}
        /// <summary>Read tsconfig.json file</summary>
        /// <param name="fileName">The path to the config file</param>
        abstract readJsonConfigFile: fileName: string * readFile: (string -> string option) -> TsConfigSourceFile
        /// Convert the json syntax tree into the json value
        abstract convertToObject: sourceFile: JsonSourceFile * errors: Diagnostic[] -> obj option
        /// <summary>Parse the contents of a config file (tsconfig.json).</summary>
        /// <param name="json">The contents of the config file to parse</param>
        /// <param name="host">Instance of ParseConfigHost used to enumerate files in folder.</param>
        /// <param name="basePath">
        /// A root directory to resolve relative path entries in the config
        /// file to. e.g. outDir
        /// </param>
        abstract parseJsonConfigFileContent: json: obj option * host: ParseConfigHost * basePath: string * ?existingOptions: CompilerOptions * ?configFileName: string * ?resolutionStack: Path[] * ?extraFileExtensions: FileExtensionInfo[] * ?extendedConfigCache: Map<string, ExtendedConfigCacheEntry> * ?existingWatchOptions: WatchOptions -> ParsedCommandLine
        /// <summary>Parse the contents of a config file (tsconfig.json).</summary>
        /// <param name="jsonNode">The contents of the config file to parse</param>
        /// <param name="host">Instance of ParseConfigHost used to enumerate files in folder.</param>
        /// <param name="basePath">
        /// A root directory to resolve relative path entries in the config
        /// file to. e.g. outDir
        /// </param>
        abstract parseJsonSourceFileConfigFileContent: sourceFile: TsConfigSourceFile * host: ParseConfigHost * basePath: string * ?existingOptions: CompilerOptions * ?configFileName: string * ?resolutionStack: Path[] * ?extraFileExtensions: FileExtensionInfo[] * ?extendedConfigCache: Map<string, ExtendedConfigCacheEntry> * ?existingWatchOptions: WatchOptions -> ParsedCommandLine
        abstract convertCompilerOptionsFromJson: jsonOptions: obj option * basePath: string * ?configFileName: string -> {| options: CompilerOptions; errors: Diagnostic[] |}
        abstract convertTypeAcquisitionFromJson: jsonOptions: obj option * basePath: string * ?configFileName: string -> {| options: TypeAcquisition; errors: Diagnostic[] |}
        abstract getEffectiveTypeRoots: options: CompilerOptions * host: GetEffectiveTypeRootsHost -> string[] option
        /// <param name="containingFile">
        /// file that contains type reference directive, can be undefined if containing file is unknown.
        /// This is possible in case if resolution is performed for directives specified via 'types' parameter. In this case initial path for secondary lookups
        /// is assumed to be the same as root directory of the project.
        /// </param>
        abstract resolveTypeReferenceDirective: typeReferenceDirectiveName: string * containingFile: string option * options: CompilerOptions * host: ModuleResolutionHost * ?redirectedReference: ResolvedProjectReference * ?cache: TypeReferenceDirectiveResolutionCache * ?resolutionMode: ResolutionMode -> ResolvedTypeReferenceDirectiveWithFailedLookupLocations
        /// Given a set of options, returns the set of type directive names
        ///   that should be included for this program automatically.
        /// This list could either come from the config file,
        ///   or from enumerating the types root + initial secondary types lookup location.
        /// More type directives might appear in the program later as a result of loading actual source files;
        ///   this list is only the set of defaults that are implicitly included.
        abstract getAutomaticTypeDirectiveNames: options: CompilerOptions * host: ModuleResolutionHost -> string[]
        abstract createModuleResolutionCache: currentDirectory: string * getCanonicalFileName: (string -> string) * ?options: CompilerOptions * ?packageJsonInfoCache: PackageJsonInfoCache -> ModuleResolutionCache
        abstract createTypeReferenceDirectiveResolutionCache: currentDirectory: string * getCanonicalFileName: (string -> string) * ?options: CompilerOptions * ?packageJsonInfoCache: PackageJsonInfoCache -> TypeReferenceDirectiveResolutionCache
        abstract resolveModuleNameFromCache: moduleName: string * containingFile: string * cache: ModuleResolutionCache * ?mode: ResolutionMode -> ResolvedModuleWithFailedLookupLocations option
        abstract resolveModuleName: moduleName: string * containingFile: string * compilerOptions: CompilerOptions * host: ModuleResolutionHost * ?cache: ModuleResolutionCache * ?redirectedReference: ResolvedProjectReference * ?resolutionMode: ResolutionMode -> ResolvedModuleWithFailedLookupLocations
        abstract bundlerModuleNameResolver: moduleName: string * containingFile: string * compilerOptions: CompilerOptions * host: ModuleResolutionHost * ?cache: ModuleResolutionCache * ?redirectedReference: ResolvedProjectReference -> ResolvedModuleWithFailedLookupLocations
        abstract nodeModuleNameResolver: moduleName: string * containingFile: string * compilerOptions: CompilerOptions * host: ModuleResolutionHost * ?cache: ModuleResolutionCache * ?redirectedReference: ResolvedProjectReference -> ResolvedModuleWithFailedLookupLocations
        abstract classicNameResolver: moduleName: string * containingFile: string * compilerOptions: CompilerOptions * host: ModuleResolutionHost * ?cache: NonRelativeModuleNameResolutionCache * ?redirectedReference: ResolvedProjectReference -> ResolvedModuleWithFailedLookupLocations
        /// <summary>
        /// Visits a Node using the supplied visitor, possibly returning a new Node in its place.
        /// 
        /// - If the input node is undefined, then the output is undefined.
        /// - If the visitor returns undefined, then the output is undefined.
        /// - If the output node is not undefined, then it will satisfy the test function.
        /// - In order to obtain a return type that is more specific than <c>Node</c>, a test
        ///   function _must_ be provided, and that function must be a type predicate.
        /// </summary>
        /// <param name="node">The Node to visit.</param>
        /// <param name="visitor">The callback used to visit the Node.</param>
        /// <param name="test">A callback to execute to verify the Node is valid.</param>
        /// <param name="lift">An optional callback to execute to lift a NodeArray into a valid Node.</param>
        abstract visitNode: node: 'TIn * visitor: Visitor<'TIn, 'TVisited> * test: (Node -> bool) * ?lift: (Node[] -> Node) -> U2<'TOut, obj> when 'TOut :> Node
        /// <summary>
        /// Visits a Node using the supplied visitor, possibly returning a new Node in its place.
        /// 
        /// - If the input node is undefined, then the output is undefined.
        /// - If the visitor returns undefined, then the output is undefined.
        /// - If the output node is not undefined, then it will satisfy the test function.
        /// - In order to obtain a return type that is more specific than <c>Node</c>, a test
        ///   function _must_ be provided, and that function must be a type predicate.
        /// </summary>
        /// <param name="node">The Node to visit.</param>
        /// <param name="visitor">The callback used to visit the Node.</param>
        /// <param name="test">A callback to execute to verify the Node is valid.</param>
        /// <param name="lift">An optional callback to execute to lift a NodeArray into a valid Node.</param>
        abstract visitNode: node: 'TIn * visitor: Visitor<'TIn, 'TVisited> * ?test: (Node -> bool) * ?lift: (Node[] -> Node) -> U2<Node, obj>
        /// <summary>
        /// Visits a NodeArray using the supplied visitor, possibly returning a new NodeArray in its place.
        /// 
        /// - If the input node array is undefined, the output is undefined.
        /// - If the visitor can return undefined, the node it visits in the array will be reused.
        /// - If the output node array is not undefined, then its contents will satisfy the test.
        /// - In order to obtain a return type that is more specific than <c>NodeArray&lt;Node&gt;</c>, a test
        ///   function _must_ be provided, and that function must be a type predicate.
        /// </summary>
        /// <param name="nodes">The NodeArray to visit.</param>
        /// <param name="visitor">The callback used to visit a Node.</param>
        /// <param name="test">A node test to execute for each node.</param>
        /// <param name="start">An optional value indicating the starting offset at which to start visiting.</param>
        /// <param name="count">An optional value indicating the maximum number of nodes to visit.</param>
        abstract visitNodes: nodes: 'TInArray * visitor: Visitor<'TIn, Node option> * test: (Node -> bool) * ?start: float * ?count: float -> U2<'TOut[], obj> when 'TIn :> Node and 'TOut :> Node
        /// <summary>
        /// Visits a NodeArray using the supplied visitor, possibly returning a new NodeArray in its place.
        /// 
        /// - If the input node array is undefined, the output is undefined.
        /// - If the visitor can return undefined, the node it visits in the array will be reused.
        /// - If the output node array is not undefined, then its contents will satisfy the test.
        /// - In order to obtain a return type that is more specific than <c>NodeArray&lt;Node&gt;</c>, a test
        ///   function _must_ be provided, and that function must be a type predicate.
        /// </summary>
        /// <param name="nodes">The NodeArray to visit.</param>
        /// <param name="visitor">The callback used to visit a Node.</param>
        /// <param name="test">A node test to execute for each node.</param>
        /// <param name="start">An optional value indicating the starting offset at which to start visiting.</param>
        /// <param name="count">An optional value indicating the maximum number of nodes to visit.</param>
        abstract visitNodes: nodes: 'TInArray * visitor: Visitor<'TIn, Node option> * ?test: (Node -> bool) * ?start: float * ?count: float -> U2<Node[], obj> when 'TIn :> Node
        /// Starts a new lexical environment and visits a statement list, ending the lexical environment
        /// and merging hoisted declarations upon completion.
        abstract visitLexicalEnvironment: statements: Statement[] * visitor: Visitor * context: TransformationContext * ?start: float * ?ensureUseStrict: bool * ?nodesVisitor: NodesVisitor -> Statement[]
        /// Starts a new lexical environment and visits a parameter list, suspending the lexical
        /// environment upon completion.
        abstract visitParameterList: nodes: ParameterDeclaration[] * visitor: Visitor * context: TransformationContext * ?nodesVisitor: NodesVisitor -> ParameterDeclaration[]
        abstract visitParameterList: nodes: ParameterDeclaration[] option * visitor: Visitor * context: TransformationContext * ?nodesVisitor: NodesVisitor -> ParameterDeclaration[] option
        /// Resumes a suspended lexical environment and visits a function body, ending the lexical
        /// environment and merging hoisted declarations upon completion.
        abstract visitFunctionBody: node: FunctionBody * visitor: Visitor * context: TransformationContext -> FunctionBody
        /// Resumes a suspended lexical environment and visits a function body, ending the lexical
        /// environment and merging hoisted declarations upon completion.
        abstract visitFunctionBody: node: FunctionBody option * visitor: Visitor * context: TransformationContext -> FunctionBody option
        /// Resumes a suspended lexical environment and visits a concise body, ending the lexical
        /// environment and merging hoisted declarations upon completion.
        abstract visitFunctionBody: node: ConciseBody * visitor: Visitor * context: TransformationContext -> ConciseBody
        /// Visits an iteration body, adding any block-scoped variables required by the transformation.
        abstract visitIterationBody: body: Statement * visitor: Visitor * context: TransformationContext -> Statement
        /// <summary>Visits the elements of a <see cref="CommaListExpression" />.</summary>
        /// <param name="visitor">The visitor to use when visiting expressions whose result will not be discarded at runtime.</param>
        /// <param name="discardVisitor">The visitor to use when visiting expressions whose result will be discarded at runtime. Defaults to <see cref="visitor" />.</param>
        abstract visitCommaListElements: elements: Expression[] * visitor: Visitor * ?discardVisitor: Visitor -> Expression[]
        /// <summary>Visits each child of a Node using the supplied visitor, possibly returning a new Node of the same kind in its place.</summary>
        /// <param name="node">The Node whose children will be visited.</param>
        /// <param name="visitor">The callback used to visit each child.</param>
        /// <param name="context">A lexical environment context for the visitor.</param>
        abstract visitEachChild: node: 'T * visitor: Visitor * context: TransformationContext -> 'T when 'T :> Node
        /// <summary>Visits each child of a Node using the supplied visitor, possibly returning a new Node of the same kind in its place.</summary>
        /// <param name="node">The Node whose children will be visited.</param>
        /// <param name="visitor">The callback used to visit each child.</param>
        /// <param name="context">A lexical environment context for the visitor.</param>
        abstract visitEachChild: node: 'T option * visitor: Visitor * context: TransformationContext * ?nodesVisitor: obj * ?tokenVisitor: Visitor -> 'T option when 'T :> Node
        abstract getTsBuildInfoEmitOutputFilePath: options: CompilerOptions -> string option
        abstract getOutputFileNames: commandLine: ParsedCommandLine * inputFileName: string * ignoreCase: bool -> string[]
        abstract createPrinter: ?printerOptions: PrinterOptions * ?handlers: PrintHandlers -> Printer
        abstract findConfigFile: searchPath: string * fileExists: (string -> bool) * ?configName: string -> string option
        abstract resolveTripleslashReference: moduleName: string * containingFile: string -> string
        abstract createCompilerHost: options: CompilerOptions * ?setParentNodes: bool -> CompilerHost
        abstract getPreEmitDiagnostics: program: Program * ?sourceFile: SourceFile * ?cancellationToken: CancellationToken -> Diagnostic[]
        abstract formatDiagnostics: diagnostics: Diagnostic[] * host: FormatDiagnosticsHost -> string
        abstract formatDiagnostic: diagnostic: Diagnostic * host: FormatDiagnosticsHost -> string
        abstract formatDiagnosticsWithColorAndContext: diagnostics: Diagnostic[] * host: FormatDiagnosticsHost -> string
        abstract flattenDiagnosticMessageText: diag: U2<string, DiagnosticMessageChain> option * newLine: string * ?indent: float -> string
        /// Calculates the resulting resolution mode for some reference in some file - this is generally the explicitly
        /// provided resolution mode in the reference, unless one is not present, in which case it is the mode of the containing file.
        abstract getModeForFileReference: ref: U2<FileReference, string> * containingFileMode: ResolutionMode -> ResolutionMode
        /// <summary>
        /// Calculates the final resolution mode for an import at some index within a file's imports list. This is generally the explicitly
        /// defined mode of the import if provided, or, if not, the mode of the containing file (with some exceptions: import=require is always commonjs, dynamic import is always esm).
        /// If you have an actual import node, prefer using getModeForUsageLocation on the reference string node.
        /// </summary>
        /// <param name="file">File to fetch the resolution mode within</param>
        /// <param name="index">Index into the file's complete resolution list to get the resolution of - this is a concatenation of the file's imports and module augmentations</param>
        abstract getModeForResolutionAtIndex: file: SourceFile * index: float -> ResolutionMode
        /// <summary>
        /// Calculates the final resolution mode for a given module reference node. This is generally the explicitly provided resolution mode, if
        /// one exists, or the mode of the containing source file. (Excepting import=require, which is always commonjs, and dynamic import, which is always esm).
        /// Notably, this function always returns <c>undefined</c> if the containing file has an <c>undefined</c> <c>impliedNodeFormat</c> - this field is only set when
        /// <c>moduleResolution</c> is <c>node16</c>+.
        /// </summary>
        /// <param name="file">The file the import or import-like reference is contained within</param>
        /// <param name="usage">The module reference string</param>
        /// <returns>The final resolution mode of the import</returns>
        abstract getModeForUsageLocation: file: {| impliedNodeFormat: ResolutionMode option |} * usage: StringLiteralLike -> ModuleKind option
        abstract getConfigFileParsingDiagnostics: configFileParseResult: ParsedCommandLine -> Diagnostic[]
        /// <summary>
        /// A function for determining if a given file is esm or cjs format, assuming modern node module resolution rules, as configured by the
        /// <c>options</c> parameter.
        /// </summary>
        /// <param name="fileName">The normalized absolute path to check the format of (it need not exist on disk)</param>
        /// <param name="packageJsonInfoCache">A cache for package file lookups - it's best to have a cache when this function is called often</param>
        /// <param name="host">The ModuleResolutionHost which can perform the filesystem lookups for package json data</param>
        /// <param name="options">The compiler options to perform the analysis under - relevant options are <c>moduleResolution</c> and <c>traceResolution</c></param>
        /// <returns><c>undefined</c> if the path has no relevant implied format, <c>ModuleKind.ESNext</c> for esm format, and <c>ModuleKind.CommonJS</c> for cjs format</returns>
        abstract getImpliedNodeFormatForFile: fileName: Path * packageJsonInfoCache: PackageJsonInfoCache option * host: ModuleResolutionHost * options: CompilerOptions -> ResolutionMode
        /// <summary>
        /// Create a new 'Program' instance. A Program is an immutable collection of 'SourceFile's and a 'CompilerOptions'
        /// that represent a compilation unit.
        /// 
        /// Creating a program proceeds from a set of root files, expanding the set of inputs by following imports and
        /// triple-slash-reference-path directives transitively. '@types' and triple-slash-reference-types are also pulled in.
        /// </summary>
        /// <param name="createProgramOptions">The options for creating a program.</param>
        /// <returns>A 'Program' object.</returns>
        abstract createProgram: createProgramOptions: CreateProgramOptions -> Program
        /// <summary>
        /// Create a new 'Program' instance. A Program is an immutable collection of 'SourceFile's and a 'CompilerOptions'
        /// that represent a compilation unit.
        /// 
        /// Creating a program proceeds from a set of root files, expanding the set of inputs by following imports and
        /// triple-slash-reference-path directives transitively. '@types' and triple-slash-reference-types are also pulled in.
        /// </summary>
        /// <param name="rootNames">A set of root files.</param>
        /// <param name="options">The compiler options which should be used.</param>
        /// <param name="host">The host interacts with the underlying file system.</param>
        /// <param name="oldProgram">Reuses an old program structure.</param>
        /// <param name="configFileParsingDiagnostics">error during config file parsing</param>
        /// <returns>A 'Program' object.</returns>
        abstract createProgram: rootNames: string[] * options: CompilerOptions * ?host: CompilerHost * ?oldProgram: Program * ?configFileParsingDiagnostics: Diagnostic[] -> Program
        /// Returns the target config filename of a project reference.
        /// Note: The file might not exist.
        abstract resolveProjectReferencePath: ref: ProjectReference -> ResolvedConfigFileName
        /// Create the builder to manage semantic diagnostics and cache them
        abstract createSemanticDiagnosticsBuilderProgram: newProgram: Program * host: BuilderProgramHost * ?oldProgram: SemanticDiagnosticsBuilderProgram * ?configFileParsingDiagnostics: Diagnostic[] -> SemanticDiagnosticsBuilderProgram
        abstract createSemanticDiagnosticsBuilderProgram: rootNames: string[] option * options: CompilerOptions option * ?host: CompilerHost * ?oldProgram: SemanticDiagnosticsBuilderProgram * ?configFileParsingDiagnostics: Diagnostic[] * ?projectReferences: ProjectReference[] -> SemanticDiagnosticsBuilderProgram
        /// Create the builder that can handle the changes in program and iterate through changed files
        /// to emit the those files and manage semantic diagnostics cache as well
        abstract createEmitAndSemanticDiagnosticsBuilderProgram: newProgram: Program * host: BuilderProgramHost * ?oldProgram: EmitAndSemanticDiagnosticsBuilderProgram * ?configFileParsingDiagnostics: Diagnostic[] -> EmitAndSemanticDiagnosticsBuilderProgram
        abstract createEmitAndSemanticDiagnosticsBuilderProgram: rootNames: string[] option * options: CompilerOptions option * ?host: CompilerHost * ?oldProgram: EmitAndSemanticDiagnosticsBuilderProgram * ?configFileParsingDiagnostics: Diagnostic[] * ?projectReferences: ProjectReference[] -> EmitAndSemanticDiagnosticsBuilderProgram
        /// Creates a builder thats just abstraction over program and can be used with watch
        abstract createAbstractBuilder: newProgram: Program * host: BuilderProgramHost * ?oldProgram: BuilderProgram * ?configFileParsingDiagnostics: Diagnostic[] -> BuilderProgram
        abstract createAbstractBuilder: rootNames: string[] option * options: CompilerOptions option * ?host: CompilerHost * ?oldProgram: BuilderProgram * ?configFileParsingDiagnostics: Diagnostic[] * ?projectReferences: ProjectReference[] -> BuilderProgram
        abstract readBuilderProgram: compilerOptions: CompilerOptions * host: ReadBuildProgramHost -> EmitAndSemanticDiagnosticsBuilderProgram option
        abstract createIncrementalCompilerHost: options: CompilerOptions * ?system: System -> CompilerHost
        abstract createIncrementalProgram: p0: IncrementalProgramOptions<'T> -> 'T when 'T :> BuilderProgram
        /// Create the watch compiler host for either configFile or fileNames and its options
        abstract createWatchCompilerHost: configFileName: string * optionsToExtend: CompilerOptions option * system: System * ?createProgram: CreateProgram<'T> * ?reportDiagnostic: DiagnosticReporter * ?reportWatchStatus: WatchStatusReporter * ?watchOptionsToExtend: WatchOptions * ?extraFileExtensions: FileExtensionInfo[] -> WatchCompilerHostOfConfigFile<'T> when 'T :> BuilderProgram
        abstract createWatchCompilerHost: rootFiles: string[] * options: CompilerOptions * system: System * ?createProgram: CreateProgram<'T> * ?reportDiagnostic: DiagnosticReporter * ?reportWatchStatus: WatchStatusReporter * ?projectReferences: ProjectReference[] * ?watchOptions: WatchOptions -> WatchCompilerHostOfFilesAndCompilerOptions<'T> when 'T :> BuilderProgram
        /// Creates the watch from the host for root files and compiler options
        abstract createWatchProgram: host: WatchCompilerHostOfFilesAndCompilerOptions<'T> -> WatchOfFilesAndCompilerOptions<'T> when 'T :> BuilderProgram
        /// Creates the watch from the host for config file
        abstract createWatchProgram: host: WatchCompilerHostOfConfigFile<'T> -> WatchOfConfigFile<'T> when 'T :> BuilderProgram
        /// Create a function that reports watch status by writing to the system and handles the formating of the diagnostic
        abstract createBuilderStatusReporter: system: System * ?pretty: bool -> DiagnosticReporter
        abstract createSolutionBuilderHost: ?system: System * ?createProgram: CreateProgram<'T> * ?reportDiagnostic: DiagnosticReporter * ?reportSolutionBuilderStatus: DiagnosticReporter * ?reportErrorSummary: ReportEmitErrorSummary -> SolutionBuilderHost<'T> when 'T :> BuilderProgram
        abstract createSolutionBuilderWithWatchHost: ?system: System * ?createProgram: CreateProgram<'T> * ?reportDiagnostic: DiagnosticReporter * ?reportSolutionBuilderStatus: DiagnosticReporter * ?reportWatchStatus: WatchStatusReporter -> SolutionBuilderWithWatchHost<'T> when 'T :> BuilderProgram
        abstract createSolutionBuilder: host: SolutionBuilderHost<'T> * rootNames: string[] * defaultOptions: BuildOptions -> SolutionBuilder<'T> when 'T :> BuilderProgram
        abstract createSolutionBuilderWithWatch: host: SolutionBuilderWithWatchHost<'T> * rootNames: string[] * defaultOptions: BuildOptions * ?baseWatchOptions: WatchOptions -> SolutionBuilder<'T> when 'T :> BuilderProgram
        abstract getDefaultFormatCodeSettings: ?newLineCharacter: string -> FormatCodeSettings
        /// The classifier is used for syntactic highlighting in editors via the TSServer
        abstract createClassifier: unit -> Classifier
        abstract createDocumentRegistry: ?useCaseSensitiveFileNames: bool * ?currentDirectory: string -> DocumentRegistry
        abstract preProcessFile: sourceText: string * ?readImportFiles: bool * ?detectJavaScriptImports: bool -> PreProcessedFileInfo
        abstract transpileModule: input: string * transpileOptions: TranspileOptions -> TranspileOutput
        abstract transpile: input: string * ?compilerOptions: CompilerOptions * ?fileName: string * ?diagnostics: Diagnostic[] * ?moduleName: string -> string
        abstract toEditorSettings: options: U2<EditorOptions, EditorSettings> -> EditorSettings
        abstract displayPartsToString: displayParts: SymbolDisplayPart[] option -> string
        abstract getDefaultCompilerOptions: unit -> CompilerOptions
        abstract getSupportedCodeFixes: unit -> string[]
        abstract createLanguageServiceSourceFile: fileName: string * scriptSnapshot: IScriptSnapshot * scriptTargetOrOptions: U2<ScriptTarget, CreateSourceFileOptions> * version: string * setNodeParents: bool * ?scriptKind: ScriptKind -> SourceFile
        abstract updateLanguageServiceSourceFile: sourceFile: SourceFile * scriptSnapshot: IScriptSnapshot * version: string * textChangeRange: TextChangeRange option * ?aggressiveChecks: bool -> SourceFile
        abstract createLanguageService: host: LanguageServiceHost * ?documentRegistry: DocumentRegistry * ?syntaxOnlyOrLanguageServiceMode: U2<bool, LanguageServiceMode> -> LanguageService
        /// Get the path of the default library files (lib.d.ts) as distributed with the typescript
        /// node package.
        /// The functionality is not supported if the ts module is consumed outside of a node module.
        abstract getDefaultLibFilePath: options: CompilerOptions -> string
        /// The version of the language service API
        abstract servicesVersion: obj
        /// <summary>Transform one or more nodes using the supplied transformers.</summary>
        /// <param name="source">A single <c>Node</c> or an array of <c>Node</c> objects.</param>
        /// <param name="transformers">An array of <c>TransformerFactory</c> callbacks used to process the transformation.</param>
        /// <param name="compilerOptions">Optional compiler options.</param>
        abstract transform: source: U2<'T, 'T[]> * transformers: TransformerFactory<'T>[] * ?compilerOptions: CompilerOptions -> TransformationResult<'T> when 'T :> Node

    /// <summary>
    /// Type of objects whose values are all of the same type.
    /// The <c>in</c> and <c>for-in</c> operators can *not* be safely used,
    /// since <c>Object.prototype</c> may be modified by outside code.
    /// </summary>
    type [<AllowNullLiteral>] MapLike<'T> =
        [<EmitIndexer>] abstract Item: index: string -> 'T with get, set

    type [<AllowNullLiteral>] SortedReadonlyArray<'T> =
        inherit ReadonlyArray<'T>
        abstract `` __sortedArrayBrand``: obj option with get, set

    type [<AllowNullLiteral>] SortedArray<'T> =
        inherit Array<'T>
        abstract `` __sortedArrayBrand``: obj option with get, set

    type [<AllowNullLiteral>] Path =
        interface end

    type [<AllowNullLiteral>] TextRange =
        abstract pos: float with get, set
        abstract ``end``: float with get, set

    type [<AllowNullLiteral>] ReadonlyTextRange =
        abstract pos: float
        abstract ``end``: float

    type [<RequireQualifiedAccess>] SyntaxKind =
        | Unknown = 0
        | EndOfFileToken = 1
        | SingleLineCommentTrivia = 2
        | MultiLineCommentTrivia = 3
        | NewLineTrivia = 4
        | WhitespaceTrivia = 5
        | ShebangTrivia = 6
        | ConflictMarkerTrivia = 7
        | NonTextFileMarkerTrivia = 8
        | NumericLiteral = 9
        | BigIntLiteral = 10
        | StringLiteral = 11
        | JsxText = 12
        | JsxTextAllWhiteSpaces = 13
        | RegularExpressionLiteral = 14
        | NoSubstitutionTemplateLiteral = 15
        | TemplateHead = 16
        | TemplateMiddle = 17
        | TemplateTail = 18
        | OpenBraceToken = 19
        | CloseBraceToken = 20
        | OpenParenToken = 21
        | CloseParenToken = 22
        | OpenBracketToken = 23
        | CloseBracketToken = 24
        | DotToken = 25
        | DotDotDotToken = 26
        | SemicolonToken = 27
        | CommaToken = 28
        | QuestionDotToken = 29
        | LessThanToken = 30
        | LessThanSlashToken = 31
        | GreaterThanToken = 32
        | LessThanEqualsToken = 33
        | GreaterThanEqualsToken = 34
        | EqualsEqualsToken = 35
        | ExclamationEqualsToken = 36
        | EqualsEqualsEqualsToken = 37
        | ExclamationEqualsEqualsToken = 38
        | EqualsGreaterThanToken = 39
        | PlusToken = 40
        | MinusToken = 41
        | AsteriskToken = 42
        | AsteriskAsteriskToken = 43
        | SlashToken = 44
        | PercentToken = 45
        | PlusPlusToken = 46
        | MinusMinusToken = 47
        | LessThanLessThanToken = 48
        | GreaterThanGreaterThanToken = 49
        | GreaterThanGreaterThanGreaterThanToken = 50
        | AmpersandToken = 51
        | BarToken = 52
        | CaretToken = 53
        | ExclamationToken = 54
        | TildeToken = 55
        | AmpersandAmpersandToken = 56
        | BarBarToken = 57
        | QuestionToken = 58
        | ColonToken = 59
        | AtToken = 60
        | QuestionQuestionToken = 61
        /// Only the JSDoc scanner produces BacktickToken. The normal scanner produces NoSubstitutionTemplateLiteral and related kinds.
        | BacktickToken = 62
        /// Only the JSDoc scanner produces HashToken. The normal scanner produces PrivateIdentifier.
        | HashToken = 63
        | EqualsToken = 64
        | PlusEqualsToken = 65
        | MinusEqualsToken = 66
        | AsteriskEqualsToken = 67
        | AsteriskAsteriskEqualsToken = 68
        | SlashEqualsToken = 69
        | PercentEqualsToken = 70
        | LessThanLessThanEqualsToken = 71
        | GreaterThanGreaterThanEqualsToken = 72
        | GreaterThanGreaterThanGreaterThanEqualsToken = 73
        | AmpersandEqualsToken = 74
        | BarEqualsToken = 75
        | BarBarEqualsToken = 76
        | AmpersandAmpersandEqualsToken = 77
        | QuestionQuestionEqualsToken = 78
        | CaretEqualsToken = 79
        | Identifier = 80
        | PrivateIdentifier = 81
        | BreakKeyword = 83
        | CaseKeyword = 84
        | CatchKeyword = 85
        | ClassKeyword = 86
        | ConstKeyword = 87
        | ContinueKeyword = 88
        | DebuggerKeyword = 89
        | DefaultKeyword = 90
        | DeleteKeyword = 91
        | DoKeyword = 92
        | ElseKeyword = 93
        | EnumKeyword = 94
        | ExportKeyword = 95
        | ExtendsKeyword = 96
        | FalseKeyword = 97
        | FinallyKeyword = 98
        | ForKeyword = 99
        | FunctionKeyword = 100
        | IfKeyword = 101
        | ImportKeyword = 102
        | InKeyword = 103
        | InstanceOfKeyword = 104
        | NewKeyword = 105
        | NullKeyword = 106
        | ReturnKeyword = 107
        | SuperKeyword = 108
        | SwitchKeyword = 109
        | ThisKeyword = 110
        | ThrowKeyword = 111
        | TrueKeyword = 112
        | TryKeyword = 113
        | TypeOfKeyword = 114
        | VarKeyword = 115
        | VoidKeyword = 116
        | WhileKeyword = 117
        | WithKeyword = 118
        | ImplementsKeyword = 119
        | InterfaceKeyword = 120
        | LetKeyword = 121
        | PackageKeyword = 122
        | PrivateKeyword = 123
        | ProtectedKeyword = 124
        | PublicKeyword = 125
        | StaticKeyword = 126
        | YieldKeyword = 127
        | AbstractKeyword = 128
        | AccessorKeyword = 129
        | AsKeyword = 130
        | AssertsKeyword = 131
        | AssertKeyword = 132
        | AnyKeyword = 133
        | AsyncKeyword = 134
        | AwaitKeyword = 135
        | BooleanKeyword = 136
        | ConstructorKeyword = 137
        | DeclareKeyword = 138
        | GetKeyword = 139
        | InferKeyword = 140
        | IntrinsicKeyword = 141
        | IsKeyword = 142
        | KeyOfKeyword = 143
        | ModuleKeyword = 144
        | NamespaceKeyword = 145
        | NeverKeyword = 146
        | OutKeyword = 147
        | ReadonlyKeyword = 148
        | RequireKeyword = 149
        | NumberKeyword = 150
        | ObjectKeyword = 151
        | SatisfiesKeyword = 152
        | SetKeyword = 153
        | StringKeyword = 154
        | SymbolKeyword = 155
        | TypeKeyword = 156
        | UndefinedKeyword = 157
        | UniqueKeyword = 158
        | UnknownKeyword = 159
        | FromKeyword = 160
        | GlobalKeyword = 161
        | BigIntKeyword = 162
        | OverrideKeyword = 163
        | OfKeyword = 164
        | QualifiedName = 165
        | ComputedPropertyName = 166
        | TypeParameter = 167
        | Parameter = 168
        | Decorator = 169
        | PropertySignature = 170
        | PropertyDeclaration = 171
        | MethodSignature = 172
        | MethodDeclaration = 173
        | ClassStaticBlockDeclaration = 174
        | Constructor = 175
        | GetAccessor = 176
        | SetAccessor = 177
        | CallSignature = 178
        | ConstructSignature = 179
        | IndexSignature = 180
        | TypePredicate = 181
        | TypeReference = 182
        | FunctionType = 183
        | ConstructorType = 184
        | TypeQuery = 185
        | TypeLiteral = 186
        | ArrayType = 187
        | TupleType = 188
        | OptionalType = 189
        | RestType = 190
        | UnionType = 191
        | IntersectionType = 192
        | ConditionalType = 193
        | InferType = 194
        | ParenthesizedType = 195
        | ThisType = 196
        | TypeOperator = 197
        | IndexedAccessType = 198
        | MappedType = 199
        | LiteralType = 200
        | NamedTupleMember = 201
        | TemplateLiteralType = 202
        | TemplateLiteralTypeSpan = 203
        | ImportType = 204
        | ObjectBindingPattern = 205
        | ArrayBindingPattern = 206
        | BindingElement = 207
        | ArrayLiteralExpression = 208
        | ObjectLiteralExpression = 209
        | PropertyAccessExpression = 210
        | ElementAccessExpression = 211
        | CallExpression = 212
        | NewExpression = 213
        | TaggedTemplateExpression = 214
        | TypeAssertionExpression = 215
        | ParenthesizedExpression = 216
        | FunctionExpression = 217
        | ArrowFunction = 218
        | DeleteExpression = 219
        | TypeOfExpression = 220
        | VoidExpression = 221
        | AwaitExpression = 222
        | PrefixUnaryExpression = 223
        | PostfixUnaryExpression = 224
        | BinaryExpression = 225
        | ConditionalExpression = 226
        | TemplateExpression = 227
        | YieldExpression = 228
        | SpreadElement = 229
        | ClassExpression = 230
        | OmittedExpression = 231
        | ExpressionWithTypeArguments = 232
        | AsExpression = 233
        | NonNullExpression = 234
        | MetaProperty = 235
        | SyntheticExpression = 236
        | SatisfiesExpression = 237
        | TemplateSpan = 238
        | SemicolonClassElement = 239
        | Block = 240
        | EmptyStatement = 241
        | VariableStatement = 242
        | ExpressionStatement = 243
        | IfStatement = 244
        | DoStatement = 245
        | WhileStatement = 246
        | ForStatement = 247
        | ForInStatement = 248
        | ForOfStatement = 249
        | ContinueStatement = 250
        | BreakStatement = 251
        | ReturnStatement = 252
        | WithStatement = 253
        | SwitchStatement = 254
        | LabeledStatement = 255
        | ThrowStatement = 256
        | TryStatement = 257
        | DebuggerStatement = 258
        | VariableDeclaration = 259
        | VariableDeclarationList = 260
        | FunctionDeclaration = 261
        | ClassDeclaration = 262
        | InterfaceDeclaration = 263
        | TypeAliasDeclaration = 264
        | EnumDeclaration = 265
        | ModuleDeclaration = 266
        | ModuleBlock = 267
        | CaseBlock = 268
        | NamespaceExportDeclaration = 269
        | ImportEqualsDeclaration = 270
        | ImportDeclaration = 271
        | ImportClause = 272
        | NamespaceImport = 273
        | NamedImports = 274
        | ImportSpecifier = 275
        | ExportAssignment = 276
        | ExportDeclaration = 277
        | NamedExports = 278
        | NamespaceExport = 279
        | ExportSpecifier = 280
        | MissingDeclaration = 281
        | ExternalModuleReference = 282
        | JsxElement = 283
        | JsxSelfClosingElement = 284
        | JsxOpeningElement = 285
        | JsxClosingElement = 286
        | JsxFragment = 287
        | JsxOpeningFragment = 288
        | JsxClosingFragment = 289
        | JsxAttribute = 290
        | JsxAttributes = 291
        | JsxSpreadAttribute = 292
        | JsxExpression = 293
        | JsxNamespacedName = 294
        | CaseClause = 295
        | DefaultClause = 296
        | HeritageClause = 297
        | CatchClause = 298
        | AssertClause = 299
        | AssertEntry = 300
        | ImportTypeAssertionContainer = 301
        | PropertyAssignment = 302
        | ShorthandPropertyAssignment = 303
        | SpreadAssignment = 304
        | EnumMember = 305
        /// <deprecated />
        | UnparsedPrologue = 306
        /// <deprecated />
        | UnparsedPrepend = 307
        /// <deprecated />
        | UnparsedText = 308
        /// <deprecated />
        | UnparsedInternalText = 309
        /// <deprecated />
        | UnparsedSyntheticReference = 310
        | SourceFile = 311
        | Bundle = 312
        /// <deprecated />
        | UnparsedSource = 313
        /// <deprecated />
        | InputFiles = 314
        | JSDocTypeExpression = 315
        | JSDocNameReference = 316
        | JSDocMemberName = 317
        | JSDocAllType = 318
        | JSDocUnknownType = 319
        | JSDocNullableType = 320
        | JSDocNonNullableType = 321
        | JSDocOptionalType = 322
        | JSDocFunctionType = 323
        | JSDocVariadicType = 324
        | JSDocNamepathType = 325
        | JSDoc = 326
        /// <deprecated>Use SyntaxKind.JSDoc</deprecated>
        | JSDocComment = 326
        | JSDocText = 327
        | JSDocTypeLiteral = 328
        | JSDocSignature = 329
        | JSDocLink = 330
        | JSDocLinkCode = 331
        | JSDocLinkPlain = 332
        | JSDocTag = 333
        | JSDocAugmentsTag = 334
        | JSDocImplementsTag = 335
        | JSDocAuthorTag = 336
        | JSDocDeprecatedTag = 337
        | JSDocClassTag = 338
        | JSDocPublicTag = 339
        | JSDocPrivateTag = 340
        | JSDocProtectedTag = 341
        | JSDocReadonlyTag = 342
        | JSDocOverrideTag = 343
        | JSDocCallbackTag = 344
        | JSDocOverloadTag = 345
        | JSDocEnumTag = 346
        | JSDocParameterTag = 347
        | JSDocReturnTag = 348
        | JSDocThisTag = 349
        | JSDocTypeTag = 350
        | JSDocTemplateTag = 351
        | JSDocTypedefTag = 352
        | JSDocSeeTag = 353
        | JSDocPropertyTag = 354
        | JSDocThrowsTag = 355
        | JSDocSatisfiesTag = 356
        | SyntaxList = 357
        | NotEmittedStatement = 358
        | PartiallyEmittedExpression = 359
        | CommaListExpression = 360
        | SyntheticReferenceExpression = 361
        | Count = 362
        | FirstAssignment = 64
        | LastAssignment = 79
        | FirstCompoundAssignment = 65
        | LastCompoundAssignment = 79
        | FirstReservedWord = 83
        | LastReservedWord = 118
        | FirstKeyword = 83
        | LastKeyword = 164
        | FirstFutureReservedWord = 119
        | LastFutureReservedWord = 127
        | FirstTypeNode = 181
        | LastTypeNode = 204
        | FirstPunctuation = 19
        | LastPunctuation = 79
        | FirstToken = 0
        | LastToken = 164
        | FirstTriviaToken = 2
        | LastTriviaToken = 7
        | FirstLiteralToken = 9
        | LastLiteralToken = 15
        | FirstTemplateToken = 15
        | LastTemplateToken = 18
        | FirstBinaryOperator = 30
        | LastBinaryOperator = 79
        | FirstStatement = 242
        | LastStatement = 258
        | FirstNode = 165
        | FirstJSDocNode = 315
        | LastJSDocNode = 356
        | FirstJSDocTagNode = 333
        | LastJSDocTagNode = 356

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// SyntaxKind.SingleLineCommentTrivia | SyntaxKind.MultiLineCommentTrivia | SyntaxKind.NewLineTrivia | SyntaxKind.WhitespaceTrivia | SyntaxKind.ShebangTrivia | SyntaxKind.ConflictMarkerTrivia
    /// </code>
    /// </remarks>
    type TriviaSyntaxKind =
        SyntaxKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// SyntaxKind.NumericLiteral | SyntaxKind.BigIntLiteral | SyntaxKind.StringLiteral | SyntaxKind.JsxText | SyntaxKind.JsxTextAllWhiteSpaces | SyntaxKind.RegularExpressionLiteral | SyntaxKind.NoSubstitutionTemplateLiteral
    /// </code>
    /// </remarks>
    type LiteralSyntaxKind =
        SyntaxKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// SyntaxKind.TemplateHead | SyntaxKind.TemplateMiddle | SyntaxKind.TemplateTail
    /// </code>
    /// </remarks>
    type PseudoLiteralSyntaxKind =
        SyntaxKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// SyntaxKind.OpenBraceToken | SyntaxKind.CloseBraceToken | SyntaxKind.OpenParenToken | SyntaxKind.CloseParenToken | SyntaxKind.OpenBracketToken | SyntaxKind.CloseBracketToken | SyntaxKind.DotToken | SyntaxKind.DotDotDotToken | SyntaxKind.SemicolonToken | SyntaxKind.CommaToken | SyntaxKind.QuestionDotToken | SyntaxKind.LessThanToken | SyntaxKind.LessThanSlashToken | SyntaxKind.GreaterThanToken | SyntaxKind.LessThanEqualsToken | SyntaxKind.GreaterThanEqualsToken | SyntaxKind.EqualsEqualsToken | SyntaxKind.ExclamationEqualsToken | SyntaxKind.EqualsEqualsEqualsToken | SyntaxKind.ExclamationEqualsEqualsToken | SyntaxKind.EqualsGreaterThanToken | SyntaxKind.PlusToken | SyntaxKind.MinusToken | SyntaxKind.AsteriskToken | SyntaxKind.AsteriskAsteriskToken | SyntaxKind.SlashToken | SyntaxKind.PercentToken | SyntaxKind.PlusPlusToken | SyntaxKind.MinusMinusToken | SyntaxKind.LessThanLessThanToken | SyntaxKind.GreaterThanGreaterThanToken | SyntaxKind.GreaterThanGreaterThanGreaterThanToken | SyntaxKind.AmpersandToken | SyntaxKind.BarToken | SyntaxKind.CaretToken | SyntaxKind.ExclamationToken | SyntaxKind.TildeToken | SyntaxKind.AmpersandAmpersandToken | SyntaxKind.AmpersandAmpersandEqualsToken | SyntaxKind.BarBarToken | SyntaxKind.BarBarEqualsToken | SyntaxKind.QuestionQuestionToken | SyntaxKind.QuestionQuestionEqualsToken | SyntaxKind.QuestionToken | SyntaxKind.ColonToken | SyntaxKind.AtToken | SyntaxKind.BacktickToken | SyntaxKind.HashToken | SyntaxKind.EqualsToken | SyntaxKind.PlusEqualsToken | SyntaxKind.MinusEqualsToken | SyntaxKind.AsteriskEqualsToken | SyntaxKind.AsteriskAsteriskEqualsToken | SyntaxKind.SlashEqualsToken | SyntaxKind.PercentEqualsToken | SyntaxKind.LessThanLessThanEqualsToken | SyntaxKind.GreaterThanGreaterThanEqualsToken | SyntaxKind.GreaterThanGreaterThanGreaterThanEqualsToken | SyntaxKind.AmpersandEqualsToken | SyntaxKind.BarEqualsToken | SyntaxKind.CaretEqualsToken
    /// </code>
    /// </remarks>
    type PunctuationSyntaxKind =
        SyntaxKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// SyntaxKind.AbstractKeyword | SyntaxKind.AccessorKeyword | SyntaxKind.AnyKeyword | SyntaxKind.AsKeyword | SyntaxKind.AssertsKeyword | SyntaxKind.AssertKeyword | SyntaxKind.AsyncKeyword | SyntaxKind.AwaitKeyword | SyntaxKind.BigIntKeyword | SyntaxKind.BooleanKeyword | SyntaxKind.BreakKeyword | SyntaxKind.CaseKeyword | SyntaxKind.CatchKeyword | SyntaxKind.ClassKeyword | SyntaxKind.ConstKeyword | SyntaxKind.ConstructorKeyword | SyntaxKind.ContinueKeyword | SyntaxKind.DebuggerKeyword | SyntaxKind.DeclareKeyword | SyntaxKind.DefaultKeyword | SyntaxKind.DeleteKeyword | SyntaxKind.DoKeyword | SyntaxKind.ElseKeyword | SyntaxKind.EnumKeyword | SyntaxKind.ExportKeyword | SyntaxKind.ExtendsKeyword | SyntaxKind.FalseKeyword | SyntaxKind.FinallyKeyword | SyntaxKind.ForKeyword | SyntaxKind.FromKeyword | SyntaxKind.FunctionKeyword | SyntaxKind.GetKeyword | SyntaxKind.GlobalKeyword | SyntaxKind.IfKeyword | SyntaxKind.ImplementsKeyword | SyntaxKind.ImportKeyword | SyntaxKind.InferKeyword | SyntaxKind.InKeyword | SyntaxKind.InstanceOfKeyword | SyntaxKind.InterfaceKeyword | SyntaxKind.IntrinsicKeyword | SyntaxKind.IsKeyword | SyntaxKind.KeyOfKeyword | SyntaxKind.LetKeyword | SyntaxKind.ModuleKeyword | SyntaxKind.NamespaceKeyword | SyntaxKind.NeverKeyword | SyntaxKind.NewKeyword | SyntaxKind.NullKeyword | SyntaxKind.NumberKeyword | SyntaxKind.ObjectKeyword | SyntaxKind.OfKeyword | SyntaxKind.PackageKeyword | SyntaxKind.PrivateKeyword | SyntaxKind.ProtectedKeyword | SyntaxKind.PublicKeyword | SyntaxKind.ReadonlyKeyword | SyntaxKind.OutKeyword | SyntaxKind.OverrideKeyword | SyntaxKind.RequireKeyword | SyntaxKind.ReturnKeyword | SyntaxKind.SatisfiesKeyword | SyntaxKind.SetKeyword | SyntaxKind.StaticKeyword | SyntaxKind.StringKeyword | SyntaxKind.SuperKeyword | SyntaxKind.SwitchKeyword | SyntaxKind.SymbolKeyword | SyntaxKind.ThisKeyword | SyntaxKind.ThrowKeyword | SyntaxKind.TrueKeyword | SyntaxKind.TryKeyword | SyntaxKind.TypeKeyword | SyntaxKind.TypeOfKeyword | SyntaxKind.UndefinedKeyword | SyntaxKind.UniqueKeyword | SyntaxKind.UnknownKeyword | SyntaxKind.VarKeyword | SyntaxKind.VoidKeyword | SyntaxKind.WhileKeyword | SyntaxKind.WithKeyword | SyntaxKind.YieldKeyword
    /// </code>
    /// </remarks>
    type KeywordSyntaxKind =
        SyntaxKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// SyntaxKind.AbstractKeyword | SyntaxKind.AccessorKeyword | SyntaxKind.AsyncKeyword | SyntaxKind.ConstKeyword | SyntaxKind.DeclareKeyword | SyntaxKind.DefaultKeyword | SyntaxKind.ExportKeyword | SyntaxKind.InKeyword | SyntaxKind.PrivateKeyword | SyntaxKind.ProtectedKeyword | SyntaxKind.PublicKeyword | SyntaxKind.ReadonlyKeyword | SyntaxKind.OutKeyword | SyntaxKind.OverrideKeyword | SyntaxKind.StaticKeyword
    /// </code>
    /// </remarks>
    type ModifierSyntaxKind =
        SyntaxKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// SyntaxKind.AnyKeyword | SyntaxKind.BigIntKeyword | SyntaxKind.BooleanKeyword | SyntaxKind.IntrinsicKeyword | SyntaxKind.NeverKeyword | SyntaxKind.NumberKeyword | SyntaxKind.ObjectKeyword | SyntaxKind.StringKeyword | SyntaxKind.SymbolKeyword | SyntaxKind.UndefinedKeyword | SyntaxKind.UnknownKeyword | SyntaxKind.VoidKeyword
    /// </code>
    /// </remarks>
    type KeywordTypeSyntaxKind =
        SyntaxKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// SyntaxKind.Unknown | SyntaxKind.EndOfFileToken | TriviaSyntaxKind | LiteralSyntaxKind | PseudoLiteralSyntaxKind | PunctuationSyntaxKind | SyntaxKind.Identifier | KeywordSyntaxKind
    /// </code>
    /// </remarks>
    type TokenSyntaxKind =
        SyntaxKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// SyntaxKind.LessThanSlashToken | SyntaxKind.EndOfFileToken | SyntaxKind.ConflictMarkerTrivia | SyntaxKind.JsxText | SyntaxKind.JsxTextAllWhiteSpaces | SyntaxKind.OpenBraceToken | SyntaxKind.LessThanToken
    /// </code>
    /// </remarks>
    type JsxTokenSyntaxKind =
        SyntaxKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// SyntaxKind.EndOfFileToken | SyntaxKind.WhitespaceTrivia | SyntaxKind.AtToken | SyntaxKind.NewLineTrivia | SyntaxKind.AsteriskToken | SyntaxKind.OpenBraceToken | SyntaxKind.CloseBraceToken | SyntaxKind.LessThanToken | SyntaxKind.GreaterThanToken | SyntaxKind.OpenBracketToken | SyntaxKind.CloseBracketToken | SyntaxKind.EqualsToken | SyntaxKind.CommaToken | SyntaxKind.DotToken | SyntaxKind.Identifier | SyntaxKind.BacktickToken | SyntaxKind.HashToken | SyntaxKind.Unknown | KeywordSyntaxKind
    /// </code>
    /// </remarks>
    type JSDocSyntaxKind =
        SyntaxKind

    type [<RequireQualifiedAccess>] NodeFlags =
        | None = 0
        | Let = 1
        | Const = 2
        | NestedNamespace = 4
        | Synthesized = 8
        | Namespace = 16
        | OptionalChain = 32
        | ExportContext = 64
        | ContainsThis = 128
        | HasImplicitReturn = 256
        | HasExplicitReturn = 512
        | GlobalAugmentation = 1024
        | HasAsyncFunctions = 2048
        | DisallowInContext = 4096
        | YieldContext = 8192
        | DecoratorContext = 16384
        | AwaitContext = 32768
        | DisallowConditionalTypesContext = 65536
        | ThisNodeHasError = 131072
        | JavaScriptFile = 262144
        | ThisNodeOrAnySubNodesHasError = 524288
        | HasAggregatedChildData = 1048576
        | JSDoc = 8388608
        | JsonFile = 67108864
        | BlockScoped = 3
        | ReachabilityCheckFlags = 768
        | ReachabilityAndEmitFlags = 2816
        | ContextFlags = 50720768
        | TypeExcludesFlags = 40960

    type [<RequireQualifiedAccess>] ModifierFlags =
        | None = 0
        | Export = 1
        | Ambient = 2
        | Public = 4
        | Private = 8
        | Protected = 16
        | Static = 32
        | Readonly = 64
        | Accessor = 128
        | Abstract = 256
        | Async = 512
        | Default = 1024
        | Const = 2048
        | HasComputedJSDocModifiers = 4096
        | Deprecated = 8192
        | Override = 16384
        | In = 32768
        | Out = 65536
        | Decorator = 131072
        | HasComputedFlags = 536870912
        | AccessibilityModifier = 28
        | ParameterPropertyModifier = 16476
        | NonPublicAccessibilityModifier = 24
        | TypeScriptModifier = 117086
        | ExportDefault = 1025
        | All = 258047
        | Modifier = 126975

    type [<RequireQualifiedAccess>] JsxFlags =
        | None = 0
        /// An element from a named property of the JSX.IntrinsicElements interface
        | IntrinsicNamedElement = 1
        /// An element inferred from the string index signature of the JSX.IntrinsicElements interface
        | IntrinsicIndexedElement = 2
        | IntrinsicElement = 3

    type [<AllowNullLiteral>] Node =
        inherit ReadonlyTextRange
        abstract kind: SyntaxKind
        abstract flags: NodeFlags
        abstract parent: Node
        abstract getSourceFile: unit -> SourceFile
        abstract getChildCount: ?sourceFile: SourceFile -> float
        abstract getChildAt: index: float * ?sourceFile: SourceFile -> Node
        abstract getChildren: ?sourceFile: SourceFile -> Node[]
        abstract getStart: ?sourceFile: SourceFile * ?includeJsDocComment: bool -> float
        abstract getFullStart: unit -> float
        abstract getEnd: unit -> float
        abstract getWidth: ?sourceFile: SourceFileLike -> float
        abstract getFullWidth: unit -> float
        abstract getLeadingTriviaWidth: ?sourceFile: SourceFile -> float
        abstract getFullText: ?sourceFile: SourceFile -> string
        abstract getText: ?sourceFile: SourceFile -> string
        abstract getFirstToken: ?sourceFile: SourceFile -> Node option
        abstract getLastToken: ?sourceFile: SourceFile -> Node option
        abstract forEachChild: cbNode: (Node -> 'T option) * ?cbNodeArray: (Node[] -> 'T option) -> 'T option

    type [<AllowNullLiteral>] JSDocContainer =
        inherit Node
        abstract _jsdocContainerBrand: obj option with get, set

    type [<AllowNullLiteral>] LocalsContainer =
        inherit Node
        abstract _localsContainerBrand: obj option with get, set

    type [<AllowNullLiteral>] FlowContainer =
        inherit Node
        abstract _flowContainerBrand: obj option with get, set

    type HasJSDoc =
        obj

    type HasType =
        obj

    type HasTypeArguments =
        U5<CallExpression, NewExpression, TaggedTemplateExpression, JsxOpeningElement, JsxSelfClosingElement>

    type HasInitializer =
        U5<HasExpressionInitializer, ForStatement, ForInStatement, ForOfStatement, JsxAttribute>

    type HasExpressionInitializer =
        U6<VariableDeclaration, ParameterDeclaration, BindingElement, PropertyDeclaration, PropertyAssignment, EnumMember>

    type HasDecorators =
        U7<ParameterDeclaration, PropertyDeclaration, MethodDeclaration, GetAccessorDeclaration, SetAccessorDeclaration, ClassExpression, ClassDeclaration>

    type HasModifiers =
        obj

    type [<AllowNullLiteral>] NodeArray<'T when 'T :> Node> =
        inherit ReadonlyArray<'T>
        inherit ReadonlyTextRange
        abstract hasTrailingComma: bool

    type [<AllowNullLiteral>] Token<'TKind> =
        inherit Node
        abstract kind: 'TKind

    type [<AllowNullLiteral>] EndOfFileToken =
        interface end

    type [<AllowNullLiteral>] PunctuationToken<'TKind> =
        inherit Token<'TKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// PunctuationToken&lt;SyntaxKind.DotToken&gt;
    /// </code>
    /// </remarks>
    type DotToken =
        PunctuationToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// PunctuationToken&lt;SyntaxKind.DotDotDotToken&gt;
    /// </code>
    /// </remarks>
    type DotDotDotToken =
        PunctuationToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// PunctuationToken&lt;SyntaxKind.QuestionToken&gt;
    /// </code>
    /// </remarks>
    type QuestionToken =
        PunctuationToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// PunctuationToken&lt;SyntaxKind.ExclamationToken&gt;
    /// </code>
    /// </remarks>
    type ExclamationToken =
        PunctuationToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// PunctuationToken&lt;SyntaxKind.ColonToken&gt;
    /// </code>
    /// </remarks>
    type ColonToken =
        PunctuationToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// PunctuationToken&lt;SyntaxKind.EqualsToken&gt;
    /// </code>
    /// </remarks>
    type EqualsToken =
        PunctuationToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// PunctuationToken&lt;SyntaxKind.AmpersandAmpersandEqualsToken&gt;
    /// </code>
    /// </remarks>
    type AmpersandAmpersandEqualsToken =
        PunctuationToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// PunctuationToken&lt;SyntaxKind.BarBarEqualsToken&gt;
    /// </code>
    /// </remarks>
    type BarBarEqualsToken =
        PunctuationToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// PunctuationToken&lt;SyntaxKind.QuestionQuestionEqualsToken&gt;
    /// </code>
    /// </remarks>
    type QuestionQuestionEqualsToken =
        PunctuationToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// PunctuationToken&lt;SyntaxKind.AsteriskToken&gt;
    /// </code>
    /// </remarks>
    type AsteriskToken =
        PunctuationToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// PunctuationToken&lt;SyntaxKind.EqualsGreaterThanToken&gt;
    /// </code>
    /// </remarks>
    type EqualsGreaterThanToken =
        PunctuationToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// PunctuationToken&lt;SyntaxKind.PlusToken&gt;
    /// </code>
    /// </remarks>
    type PlusToken =
        PunctuationToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// PunctuationToken&lt;SyntaxKind.MinusToken&gt;
    /// </code>
    /// </remarks>
    type MinusToken =
        PunctuationToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// PunctuationToken&lt;SyntaxKind.QuestionDotToken&gt;
    /// </code>
    /// </remarks>
    type QuestionDotToken =
        PunctuationToken<SyntaxKind>

    type [<AllowNullLiteral>] KeywordToken<'TKind> =
        inherit Token<'TKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// KeywordToken&lt;SyntaxKind.AssertsKeyword&gt;
    /// </code>
    /// </remarks>
    type AssertsKeyword =
        KeywordToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// KeywordToken&lt;SyntaxKind.AssertKeyword&gt;
    /// </code>
    /// </remarks>
    type AssertKeyword =
        KeywordToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// KeywordToken&lt;SyntaxKind.AwaitKeyword&gt;
    /// </code>
    /// </remarks>
    type AwaitKeyword =
        KeywordToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// KeywordToken&lt;SyntaxKind.CaseKeyword&gt;
    /// </code>
    /// </remarks>
    type CaseKeyword =
        KeywordToken<SyntaxKind>

    type [<AllowNullLiteral>] ModifierToken<'TKind> =
        inherit KeywordToken<'TKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// ModifierToken&lt;SyntaxKind.AbstractKeyword&gt;
    /// </code>
    /// </remarks>
    type AbstractKeyword =
        ModifierToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// ModifierToken&lt;SyntaxKind.AccessorKeyword&gt;
    /// </code>
    /// </remarks>
    type AccessorKeyword =
        ModifierToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// ModifierToken&lt;SyntaxKind.AsyncKeyword&gt;
    /// </code>
    /// </remarks>
    type AsyncKeyword =
        ModifierToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// ModifierToken&lt;SyntaxKind.ConstKeyword&gt;
    /// </code>
    /// </remarks>
    type ConstKeyword =
        ModifierToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// ModifierToken&lt;SyntaxKind.DeclareKeyword&gt;
    /// </code>
    /// </remarks>
    type DeclareKeyword =
        ModifierToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// ModifierToken&lt;SyntaxKind.DefaultKeyword&gt;
    /// </code>
    /// </remarks>
    type DefaultKeyword =
        ModifierToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// ModifierToken&lt;SyntaxKind.ExportKeyword&gt;
    /// </code>
    /// </remarks>
    type ExportKeyword =
        ModifierToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// ModifierToken&lt;SyntaxKind.InKeyword&gt;
    /// </code>
    /// </remarks>
    type InKeyword =
        ModifierToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// ModifierToken&lt;SyntaxKind.PrivateKeyword&gt;
    /// </code>
    /// </remarks>
    type PrivateKeyword =
        ModifierToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// ModifierToken&lt;SyntaxKind.ProtectedKeyword&gt;
    /// </code>
    /// </remarks>
    type ProtectedKeyword =
        ModifierToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// ModifierToken&lt;SyntaxKind.PublicKeyword&gt;
    /// </code>
    /// </remarks>
    type PublicKeyword =
        ModifierToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// ModifierToken&lt;SyntaxKind.ReadonlyKeyword&gt;
    /// </code>
    /// </remarks>
    type ReadonlyKeyword =
        ModifierToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// ModifierToken&lt;SyntaxKind.OutKeyword&gt;
    /// </code>
    /// </remarks>
    type OutKeyword =
        ModifierToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// ModifierToken&lt;SyntaxKind.OverrideKeyword&gt;
    /// </code>
    /// </remarks>
    type OverrideKeyword =
        ModifierToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// ModifierToken&lt;SyntaxKind.StaticKeyword&gt;
    /// </code>
    /// </remarks>
    type StaticKeyword =
        ModifierToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// AbstractKeyword | AccessorKeyword | AsyncKeyword | ConstKeyword | DeclareKeyword | DefaultKeyword | ExportKeyword | InKeyword | PrivateKeyword | ProtectedKeyword | PublicKeyword | OutKeyword | OverrideKeyword | ReadonlyKeyword | StaticKeyword
    /// </code>
    /// </remarks>
    type Modifier =
        ModifierToken<SyntaxKind>

    type ModifierLike =
        U2<Modifier, Decorator>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// PublicKeyword | PrivateKeyword | ProtectedKeyword
    /// </code>
    /// </remarks>
    type AccessibilityModifier =
        ModifierToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// AccessibilityModifier | ReadonlyKeyword
    /// </code>
    /// </remarks>
    type ParameterPropertyModifier =
        ModifierToken<SyntaxKind>

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// AccessibilityModifier | ReadonlyKeyword | StaticKeyword | AccessorKeyword
    /// </code>
    /// </remarks>
    type ClassMemberModifier =
        ModifierToken<SyntaxKind>

    type ModifiersArray =
        Modifier[]

    type [<RequireQualifiedAccess>] GeneratedIdentifierFlags =
        | None = 0
        | ReservedInNestedScopes = 8
        | Optimistic = 16
        | FileLevel = 32
        | AllowNameSubstitution = 64

    type [<AllowNullLiteral>] Identifier =
        inherit PrimaryExpression
        inherit Declaration
        inherit JSDocContainer
        inherit FlowContainer
        abstract kind: SyntaxKind
        /// <summary>
        /// Prefer to use <c>id.unescapedText</c>. (Note: This is available only in services, not internally to the TypeScript compiler.)
        /// Text of identifier, but if the identifier begins with two underscores, this will begin with three.
        /// </summary>
        abstract escapedText: __String
        abstract text: string

    type [<AllowNullLiteral>] TransientIdentifier =
        inherit Identifier
        abstract resolvedSymbol: Symbol with get, set

    type [<AllowNullLiteral>] QualifiedName =
        inherit Node
        inherit FlowContainer
        abstract kind: SyntaxKind
        abstract left: EntityName
        abstract right: Identifier

    type EntityName =
        U2<Identifier, QualifiedName>

    type PropertyName =
        U5<Identifier, StringLiteral, NumericLiteral, ComputedPropertyName, PrivateIdentifier>

    type MemberName =
        U2<Identifier, PrivateIdentifier>

    type DeclarationName =
        U6<PropertyName, JsxAttributeName, StringLiteralLike, ElementAccessExpression, BindingPattern, EntityNameExpression>

    type [<AllowNullLiteral>] Declaration =
        inherit Node
        abstract _declarationBrand: obj option with get, set

    type [<AllowNullLiteral>] NamedDeclaration =
        inherit Declaration
        abstract name: DeclarationName option

    type [<AllowNullLiteral>] DeclarationStatement =
        inherit NamedDeclaration
        inherit Statement
        abstract name: U3<Identifier, StringLiteral, NumericLiteral> option

    type [<AllowNullLiteral>] ComputedPropertyName =
        inherit Node
        abstract kind: SyntaxKind
        abstract parent: Declaration
        abstract expression: Expression

    type [<AllowNullLiteral>] PrivateIdentifier =
        inherit PrimaryExpression
        abstract kind: SyntaxKind
        abstract escapedText: __String
        abstract text: string

    type [<AllowNullLiteral>] Decorator =
        inherit Node
        abstract kind: SyntaxKind
        abstract parent: NamedDeclaration
        abstract expression: LeftHandSideExpression

    type [<AllowNullLiteral>] TypeParameterDeclaration =
        inherit NamedDeclaration
        inherit JSDocContainer
        abstract kind: SyntaxKind
        abstract parent: U2<DeclarationWithTypeParameterChildren, InferTypeNode>
        abstract modifiers: Modifier[] option
        abstract name: Identifier
        /// <summary>Note: Consider calling <c>getEffectiveConstraintOfTypeParameter</c></summary>
        abstract ``constraint``: TypeNode option
        abstract ``default``: TypeNode option
        abstract expression: Expression option with get, set

    type [<AllowNullLiteral>] SignatureDeclarationBase =
        inherit NamedDeclaration
        inherit JSDocContainer
        abstract kind: obj
        abstract name: PropertyName option
        abstract typeParameters: TypeParameterDeclaration[] option
        abstract parameters: ParameterDeclaration[]
        abstract ``type``: TypeNode option

    type SignatureDeclaration =
        obj

    type [<AllowNullLiteral>] CallSignatureDeclaration =
        inherit SignatureDeclarationBase
        inherit TypeElement
        inherit LocalsContainer
        abstract kind: SyntaxKind

    type [<AllowNullLiteral>] ConstructSignatureDeclaration =
        inherit SignatureDeclarationBase
        inherit TypeElement
        inherit LocalsContainer
        abstract kind: SyntaxKind

    type BindingName =
        U2<Identifier, BindingPattern>

    type [<AllowNullLiteral>] VariableDeclaration =
        inherit NamedDeclaration
        inherit JSDocContainer
        abstract kind: SyntaxKind
        abstract parent: U2<VariableDeclarationList, CatchClause>
        abstract name: BindingName
        abstract exclamationToken: ExclamationToken option
        abstract ``type``: TypeNode option
        abstract initializer: Expression option

    type [<AllowNullLiteral>] VariableDeclarationList =
        inherit Node
        abstract kind: SyntaxKind
        abstract parent: U4<VariableStatement, ForStatement, ForOfStatement, ForInStatement>
        abstract declarations: VariableDeclaration[]

    type [<AllowNullLiteral>] ParameterDeclaration =
        inherit NamedDeclaration
        inherit JSDocContainer
        abstract kind: SyntaxKind
        abstract parent: SignatureDeclaration
        abstract modifiers: ModifierLike[] option
        abstract dotDotDotToken: DotDotDotToken option
        abstract name: BindingName
        abstract questionToken: QuestionToken option
        abstract ``type``: TypeNode option
        abstract initializer: Expression option

    type [<AllowNullLiteral>] BindingElement =
        inherit NamedDeclaration
        inherit FlowContainer
        abstract kind: SyntaxKind
        abstract parent: BindingPattern
        abstract propertyName: PropertyName option
        abstract dotDotDotToken: DotDotDotToken option
        abstract name: BindingName
        abstract initializer: Expression option

    type [<AllowNullLiteral>] PropertySignature =
        inherit TypeElement
        inherit JSDocContainer
        abstract kind: SyntaxKind
        abstract parent: U2<TypeLiteralNode, InterfaceDeclaration>
        abstract modifiers: Modifier[] option
        abstract name: PropertyName
        abstract questionToken: QuestionToken option
        abstract ``type``: TypeNode option

    type [<AllowNullLiteral>] PropertyDeclaration =
        inherit ClassElement
        inherit JSDocContainer
        abstract kind: SyntaxKind
        abstract parent: ClassLikeDeclaration
        abstract modifiers: ModifierLike[] option
        abstract name: PropertyName
        abstract questionToken: QuestionToken option
        abstract exclamationToken: ExclamationToken option
        abstract ``type``: TypeNode option
        abstract initializer: Expression option

    type [<AllowNullLiteral>] AutoAccessorPropertyDeclaration =
        inherit PropertyDeclaration
        abstract _autoAccessorBrand: obj option with get, set

    type [<AllowNullLiteral>] ObjectLiteralElement =
        inherit NamedDeclaration
        abstract _objectLiteralBrand: obj option with get, set
        abstract name: PropertyName option

    /// Unlike ObjectLiteralElement, excludes JSXAttribute and JSXSpreadAttribute.
    type ObjectLiteralElementLike =
        U5<PropertyAssignment, ShorthandPropertyAssignment, SpreadAssignment, MethodDeclaration, AccessorDeclaration>

    type [<AllowNullLiteral>] PropertyAssignment =
        inherit ObjectLiteralElement
        inherit JSDocContainer
        abstract kind: SyntaxKind
        abstract parent: ObjectLiteralExpression
        abstract name: PropertyName
        abstract initializer: Expression

    type [<AllowNullLiteral>] ShorthandPropertyAssignment =
        inherit ObjectLiteralElement
        inherit JSDocContainer
        abstract kind: SyntaxKind
        abstract parent: ObjectLiteralExpression
        abstract name: Identifier
        abstract equalsToken: EqualsToken option
        abstract objectAssignmentInitializer: Expression option

    type [<AllowNullLiteral>] SpreadAssignment =
        inherit ObjectLiteralElement
        inherit JSDocContainer
        abstract kind: SyntaxKind
        abstract parent: ObjectLiteralExpression
        abstract expression: Expression

    type VariableLikeDeclaration =
        obj

    type [<AllowNullLiteral>] ObjectBindingPattern =
        inherit Node
        abstract kind: SyntaxKind
        abstract parent: U3<VariableDeclaration, ParameterDeclaration, BindingElement>
        abstract elements: BindingElement[]

    type [<AllowNullLiteral>] ArrayBindingPattern =
        inherit Node
        abstract kind: SyntaxKind
        abstract parent: U3<VariableDeclaration, ParameterDeclaration, BindingElement>
        abstract elements: ArrayBindingElement[]

    type BindingPattern =
        U2<ObjectBindingPattern, ArrayBindingPattern>

    type ArrayBindingElement =
        U2<BindingElement, OmittedExpression>

    /// Several node kinds share function-like features such as a signature,
    /// a name, and a body. These nodes should extend FunctionLikeDeclarationBase.
    /// Examples:
    /// - FunctionDeclaration
    /// - MethodDeclaration
    /// - AccessorDeclaration
    type [<AllowNullLiteral>] FunctionLikeDeclarationBase =
        inherit SignatureDeclarationBase
        abstract _functionLikeDeclarationBrand: obj option with get, set
        abstract asteriskToken: AsteriskToken option
        abstract questionToken: QuestionToken option
        abstract exclamationToken: ExclamationToken option
        abstract body: U2<Block, Expression> option

    type FunctionLikeDeclaration =
        U7<FunctionDeclaration, MethodDeclaration, GetAccessorDeclaration, SetAccessorDeclaration, ConstructorDeclaration, FunctionExpression, ArrowFunction>

    [<Obsolete("Use SignatureDeclaration")>]
    type FunctionLike =
        SignatureDeclaration

    type [<AllowNullLiteral>] FunctionDeclaration =
        inherit FunctionLikeDeclarationBase
        inherit DeclarationStatement
        inherit LocalsContainer
        abstract kind: SyntaxKind
        abstract modifiers: ModifierLike[] option
        abstract name: Identifier option
        abstract body: FunctionBody option

    type [<AllowNullLiteral>] MethodSignature =
        inherit SignatureDeclarationBase
        inherit TypeElement
        inherit LocalsContainer
        abstract kind: SyntaxKind
        abstract parent: U2<TypeLiteralNode, InterfaceDeclaration>
        abstract modifiers: Modifier[] option
        abstract name: PropertyName

    type [<AllowNullLiteral>] MethodDeclaration =
        inherit FunctionLikeDeclarationBase
        inherit ClassElement
        inherit ObjectLiteralElement
        inherit JSDocContainer
        inherit LocalsContainer
        inherit FlowContainer
        abstract kind: SyntaxKind
        abstract parent: U2<ClassLikeDeclaration, ObjectLiteralExpression>
        abstract modifiers: ModifierLike[] option
        abstract name: PropertyName
        abstract body: FunctionBody option

    type [<AllowNullLiteral>] ConstructorDeclaration =
        inherit FunctionLikeDeclarationBase
        inherit ClassElement
        inherit JSDocContainer
        inherit LocalsContainer
        abstract kind: SyntaxKind
        abstract parent: ClassLikeDeclaration
        abstract modifiers: ModifierLike[] option
        abstract body: FunctionBody option

    /// For when we encounter a semicolon in a class declaration. ES6 allows these as class elements.
    type [<AllowNullLiteral>] SemicolonClassElement =
        inherit ClassElement
        inherit JSDocContainer
        abstract kind: SyntaxKind
        abstract parent: ClassLikeDeclaration

    type [<AllowNullLiteral>] GetAccessorDeclaration =
        inherit FunctionLikeDeclarationBase
        inherit ClassElement
        inherit TypeElement
        inherit ObjectLiteralElement
        inherit JSDocContainer
        inherit LocalsContainer
        inherit FlowContainer
        abstract kind: SyntaxKind
        abstract parent: U4<ClassLikeDeclaration, ObjectLiteralExpression, TypeLiteralNode, InterfaceDeclaration>
        abstract modifiers: ModifierLike[] option
        abstract name: PropertyName
        abstract body: FunctionBody option

    type [<AllowNullLiteral>] SetAccessorDeclaration =
        inherit FunctionLikeDeclarationBase
        inherit ClassElement
        inherit TypeElement
        inherit ObjectLiteralElement
        inherit JSDocContainer
        inherit LocalsContainer
        inherit FlowContainer
        abstract kind: SyntaxKind
        abstract parent: U4<ClassLikeDeclaration, ObjectLiteralExpression, TypeLiteralNode, InterfaceDeclaration>
        abstract modifiers: ModifierLike[] option
        abstract name: PropertyName
        abstract body: FunctionBody option

    type AccessorDeclaration =
        U2<GetAccessorDeclaration, SetAccessorDeclaration>

    type [<AllowNullLiteral>] IndexSignatureDeclaration =
        inherit SignatureDeclarationBase
        inherit ClassElement
        inherit TypeElement
        inherit LocalsContainer
        abstract kind: SyntaxKind
        abstract parent: ObjectTypeDeclaration
        abstract modifiers: ModifierLike[] option
        abstract ``type``: TypeNode

    type [<AllowNullLiteral>] ClassStaticBlockDeclaration =
        inherit ClassElement
        inherit JSDocContainer
        inherit LocalsContainer
        abstract kind: SyntaxKind
        abstract parent: U2<ClassDeclaration, ClassExpression>
        abstract body: Block

    type [<AllowNullLiteral>] TypeNode =
        inherit Node
        abstract _typeNodeBrand: obj option with get, set

    type KeywordTypeNode =
        KeywordTypeNode<KeywordTypeSyntaxKind>

    type [<AllowNullLiteral>] KeywordTypeNode<'TKind> =
        inherit KeywordToken<'TKind>
        inherit TypeNode
        abstract kind: 'TKind

    type [<AllowNullLiteral>] ImportTypeAssertionContainer =
        inherit Node
        abstract kind: SyntaxKind
        abstract parent: ImportTypeNode
        abstract assertClause: AssertClause
        abstract multiLine: bool option

    type [<AllowNullLiteral>] ImportTypeNode =
        inherit NodeWithTypeArguments
        abstract kind: SyntaxKind
        abstract isTypeOf: bool
        abstract argument: TypeNode
        abstract assertions: ImportTypeAssertionContainer option
        abstract qualifier: EntityName option

    type [<AllowNullLiteral>] ThisTypeNode =
        inherit TypeNode
        abstract kind: SyntaxKind

    type FunctionOrConstructorTypeNode =
        U2<FunctionTypeNode, ConstructorTypeNode>

    type [<AllowNullLiteral>] FunctionOrConstructorTypeNodeBase =
        inherit TypeNode
        inherit SignatureDeclarationBase
        abstract kind: SyntaxKind
        abstract ``type``: TypeNode

    type [<AllowNullLiteral>] FunctionTypeNode =
        inherit FunctionOrConstructorTypeNodeBase
        inherit LocalsContainer
        abstract kind: SyntaxKind

    type [<AllowNullLiteral>] ConstructorTypeNode =
        inherit FunctionOrConstructorTypeNodeBase
        inherit LocalsContainer
        abstract kind: SyntaxKind
        abstract modifiers: Modifier[] option

    type [<AllowNullLiteral>] NodeWithTypeArguments =
        inherit TypeNode
        abstract typeArguments: TypeNode[] option

    type TypeReferenceType =
        U2<TypeReferenceNode, ExpressionWithTypeArguments>

    type [<AllowNullLiteral>] TypeReferenceNode =
        inherit NodeWithTypeArguments
        abstract kind: SyntaxKind
        abstract typeName: EntityName

    type [<AllowNullLiteral>] TypePredicateNode =
        inherit TypeNode
        abstract kind: SyntaxKind
        abstract parent: U2<SignatureDeclaration, JSDocTypeExpression>
        abstract assertsModifier: AssertsKeyword option
        abstract parameterName: U2<Identifier, ThisTypeNode>
        abstract ``type``: TypeNode option

    type [<AllowNullLiteral>] TypeQueryNode =
        inherit NodeWithTypeArguments
        abstract kind: SyntaxKind
        abstract exprName: EntityName

    type [<AllowNullLiteral>] TypeLiteralNode =
        inherit TypeNode
        inherit Declaration
        abstract kind: SyntaxKind
        abstract members: TypeElement[]

    type [<AllowNullLiteral>] ArrayTypeNode =
        inherit TypeNode
        abstract kind: SyntaxKind
        abstract elementType: TypeNode

    type [<AllowNullLiteral>] TupleTypeNode =
        inherit TypeNode
        abstract kind: SyntaxKind
        abstract elements: U2<TypeNode, NamedTupleMember>[]

    type [<AllowNullLiteral>] NamedTupleMember =
        inherit TypeNode
        inherit Declaration
        inherit JSDocContainer
        abstract kind: SyntaxKind
        abstract dotDotDotToken: Token<SyntaxKind> option
        abstract name: Identifier
        abstract questionToken: Token<SyntaxKind> option
        abstract ``type``: TypeNode

    type [<AllowNullLiteral>] OptionalTypeNode =
        inherit TypeNode
        abstract kind: SyntaxKind
        abstract ``type``: TypeNode

    type [<AllowNullLiteral>] RestTypeNode =
        inherit TypeNode
        abstract kind: SyntaxKind
        abstract ``type``: TypeNode

    type UnionOrIntersectionTypeNode =
        U2<UnionTypeNode, IntersectionTypeNode>

    type [<AllowNullLiteral>] UnionTypeNode =
        inherit TypeNode
        abstract kind: SyntaxKind
        abstract types: TypeNode[]

    type [<AllowNullLiteral>] IntersectionTypeNode =
        inherit TypeNode
        abstract kind: SyntaxKind
        abstract types: TypeNode[]

    type [<AllowNullLiteral>] ConditionalTypeNode =
        inherit TypeNode
        inherit LocalsContainer
        abstract kind: SyntaxKind
        abstract checkType: TypeNode
        abstract extendsType: TypeNode
        abstract trueType: TypeNode
        abstract falseType: TypeNode

    type [<AllowNullLiteral>] InferTypeNode =
        inherit TypeNode
        abstract kind: SyntaxKind
        abstract typeParameter: TypeParameterDeclaration

    type [<AllowNullLiteral>] ParenthesizedTypeNode =
        inherit TypeNode
        abstract kind: SyntaxKind
        abstract ``type``: TypeNode

    type [<AllowNullLiteral>] TypeOperatorNode =
        inherit TypeNode
        abstract kind: SyntaxKind
        abstract operator: SyntaxKind
        abstract ``type``: TypeNode

    type [<AllowNullLiteral>] IndexedAccessTypeNode =
        inherit TypeNode
        abstract kind: SyntaxKind
        abstract objectType: TypeNode
        abstract indexType: TypeNode

    type [<AllowNullLiteral>] MappedTypeNode =
        inherit TypeNode
        inherit Declaration
        inherit LocalsContainer
        abstract kind: SyntaxKind
        abstract readonlyToken: U3<ReadonlyKeyword, PlusToken, MinusToken> option
        abstract typeParameter: TypeParameterDeclaration
        abstract nameType: TypeNode option
        abstract questionToken: U3<QuestionToken, PlusToken, MinusToken> option
        abstract ``type``: TypeNode option
        /// Used only to produce grammar errors
        abstract members: TypeElement[] option

    type [<AllowNullLiteral>] LiteralTypeNode =
        inherit TypeNode
        abstract kind: SyntaxKind
        abstract literal: U4<NullLiteral, BooleanLiteral, LiteralExpression, PrefixUnaryExpression>

    type [<AllowNullLiteral>] StringLiteral =
        inherit LiteralExpression
        inherit Declaration
        abstract kind: SyntaxKind

    type StringLiteralLike =
        U2<StringLiteral, NoSubstitutionTemplateLiteral>

    type PropertyNameLiteral =
        U4<Identifier, StringLiteralLike, NumericLiteral, JsxNamespacedName>

    type [<AllowNullLiteral>] TemplateLiteralTypeNode =
        inherit TypeNode
        abstract kind: SyntaxKind with get, set
        abstract head: TemplateHead
        abstract templateSpans: TemplateLiteralTypeSpan[]

    type [<AllowNullLiteral>] TemplateLiteralTypeSpan =
        inherit TypeNode
        abstract kind: SyntaxKind
        abstract parent: TemplateLiteralTypeNode
        abstract ``type``: TypeNode
        abstract literal: U2<TemplateMiddle, TemplateTail>

    type [<AllowNullLiteral>] Expression =
        inherit Node
        abstract _expressionBrand: obj option with get, set

    type [<AllowNullLiteral>] OmittedExpression =
        inherit Expression
        abstract kind: SyntaxKind

    type [<AllowNullLiteral>] PartiallyEmittedExpression =
        inherit LeftHandSideExpression
        abstract kind: SyntaxKind
        abstract expression: Expression

    type [<AllowNullLiteral>] UnaryExpression =
        inherit Expression
        abstract _unaryExpressionBrand: obj option with get, set

    /// Deprecated, please use UpdateExpression
    type IncrementExpression =
        UpdateExpression

    type [<AllowNullLiteral>] UpdateExpression =
        inherit UnaryExpression
        abstract _updateExpressionBrand: obj option with get, set

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// SyntaxKind.PlusPlusToken | SyntaxKind.MinusMinusToken | SyntaxKind.PlusToken | SyntaxKind.MinusToken | SyntaxKind.TildeToken | SyntaxKind.ExclamationToken
    /// </code>
    /// </remarks>
    type PrefixUnaryOperator =
        SyntaxKind

    type [<AllowNullLiteral>] PrefixUnaryExpression =
        inherit UpdateExpression
        abstract kind: SyntaxKind
        abstract operator: PrefixUnaryOperator
        abstract operand: UnaryExpression

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// SyntaxKind.PlusPlusToken | SyntaxKind.MinusMinusToken
    /// </code>
    /// </remarks>
    type PostfixUnaryOperator =
        SyntaxKind

    type [<AllowNullLiteral>] PostfixUnaryExpression =
        inherit UpdateExpression
        abstract kind: SyntaxKind
        abstract operand: LeftHandSideExpression
        abstract operator: PostfixUnaryOperator

    type [<AllowNullLiteral>] LeftHandSideExpression =
        inherit UpdateExpression
        abstract _leftHandSideExpressionBrand: obj option with get, set

    type [<AllowNullLiteral>] MemberExpression =
        inherit LeftHandSideExpression
        abstract _memberExpressionBrand: obj option with get, set

    type [<AllowNullLiteral>] PrimaryExpression =
        inherit MemberExpression
        abstract _primaryExpressionBrand: obj option with get, set

    type [<AllowNullLiteral>] NullLiteral =
        inherit PrimaryExpression
        abstract kind: SyntaxKind

    type [<AllowNullLiteral>] TrueLiteral =
        inherit PrimaryExpression
        abstract kind: SyntaxKind

    type [<AllowNullLiteral>] FalseLiteral =
        inherit PrimaryExpression
        abstract kind: SyntaxKind

    type BooleanLiteral =
        U2<TrueLiteral, FalseLiteral>

    type [<AllowNullLiteral>] ThisExpression =
        inherit PrimaryExpression
        inherit FlowContainer
        abstract kind: SyntaxKind

    type [<AllowNullLiteral>] SuperExpression =
        inherit PrimaryExpression
        inherit FlowContainer
        abstract kind: SyntaxKind

    type [<AllowNullLiteral>] ImportExpression =
        inherit PrimaryExpression
        abstract kind: SyntaxKind

    type [<AllowNullLiteral>] DeleteExpression =
        inherit UnaryExpression
        abstract kind: SyntaxKind
        abstract expression: UnaryExpression

    type [<AllowNullLiteral>] TypeOfExpression =
        inherit UnaryExpression
        abstract kind: SyntaxKind
        abstract expression: UnaryExpression

    type [<AllowNullLiteral>] VoidExpression =
        inherit UnaryExpression
        abstract kind: SyntaxKind
        abstract expression: UnaryExpression

    type [<AllowNullLiteral>] AwaitExpression =
        inherit UnaryExpression
        abstract kind: SyntaxKind
        abstract expression: UnaryExpression

    type [<AllowNullLiteral>] YieldExpression =
        inherit Expression
        abstract kind: SyntaxKind
        abstract asteriskToken: AsteriskToken option
        abstract expression: Expression option

    type [<AllowNullLiteral>] SyntheticExpression =
        inherit Expression
        abstract kind: SyntaxKind
        abstract isSpread: bool
        abstract ``type``: Type
        abstract tupleNameSource: U2<ParameterDeclaration, NamedTupleMember> option

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// SyntaxKind.AsteriskAsteriskToken
    /// </code>
    /// </remarks>
    type ExponentiationOperator =
        SyntaxKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// SyntaxKind.AsteriskToken | SyntaxKind.SlashToken | SyntaxKind.PercentToken
    /// </code>
    /// </remarks>
    type MultiplicativeOperator =
        SyntaxKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// ExponentiationOperator | MultiplicativeOperator
    /// </code>
    /// </remarks>
    type MultiplicativeOperatorOrHigher =
        SyntaxKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// SyntaxKind.PlusToken | SyntaxKind.MinusToken
    /// </code>
    /// </remarks>
    type AdditiveOperator =
        SyntaxKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// MultiplicativeOperatorOrHigher | AdditiveOperator
    /// </code>
    /// </remarks>
    type AdditiveOperatorOrHigher =
        SyntaxKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// SyntaxKind.LessThanLessThanToken | SyntaxKind.GreaterThanGreaterThanToken | SyntaxKind.GreaterThanGreaterThanGreaterThanToken
    /// </code>
    /// </remarks>
    type ShiftOperator =
        SyntaxKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// AdditiveOperatorOrHigher | ShiftOperator
    /// </code>
    /// </remarks>
    type ShiftOperatorOrHigher =
        SyntaxKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// SyntaxKind.LessThanToken | SyntaxKind.LessThanEqualsToken | SyntaxKind.GreaterThanToken | SyntaxKind.GreaterThanEqualsToken | SyntaxKind.InstanceOfKeyword | SyntaxKind.InKeyword
    /// </code>
    /// </remarks>
    type RelationalOperator =
        SyntaxKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// ShiftOperatorOrHigher | RelationalOperator
    /// </code>
    /// </remarks>
    type RelationalOperatorOrHigher =
        SyntaxKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// SyntaxKind.EqualsEqualsToken | SyntaxKind.EqualsEqualsEqualsToken | SyntaxKind.ExclamationEqualsEqualsToken | SyntaxKind.ExclamationEqualsToken
    /// </code>
    /// </remarks>
    type EqualityOperator =
        SyntaxKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// RelationalOperatorOrHigher | EqualityOperator
    /// </code>
    /// </remarks>
    type EqualityOperatorOrHigher =
        SyntaxKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// SyntaxKind.AmpersandToken | SyntaxKind.BarToken | SyntaxKind.CaretToken
    /// </code>
    /// </remarks>
    type BitwiseOperator =
        SyntaxKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// EqualityOperatorOrHigher | BitwiseOperator
    /// </code>
    /// </remarks>
    type BitwiseOperatorOrHigher =
        SyntaxKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// SyntaxKind.AmpersandAmpersandToken | SyntaxKind.BarBarToken
    /// </code>
    /// </remarks>
    type LogicalOperator =
        SyntaxKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// BitwiseOperatorOrHigher | LogicalOperator
    /// </code>
    /// </remarks>
    type LogicalOperatorOrHigher =
        SyntaxKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// SyntaxKind.PlusEqualsToken | SyntaxKind.MinusEqualsToken | SyntaxKind.AsteriskAsteriskEqualsToken | SyntaxKind.AsteriskEqualsToken | SyntaxKind.SlashEqualsToken | SyntaxKind.PercentEqualsToken | SyntaxKind.AmpersandEqualsToken | SyntaxKind.BarEqualsToken | SyntaxKind.CaretEqualsToken | SyntaxKind.LessThanLessThanEqualsToken | SyntaxKind.GreaterThanGreaterThanGreaterThanEqualsToken | SyntaxKind.GreaterThanGreaterThanEqualsToken | SyntaxKind.BarBarEqualsToken | SyntaxKind.AmpersandAmpersandEqualsToken | SyntaxKind.QuestionQuestionEqualsToken
    /// </code>
    /// </remarks>
    type CompoundAssignmentOperator =
        SyntaxKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// SyntaxKind.EqualsToken | CompoundAssignmentOperator
    /// </code>
    /// </remarks>
    type AssignmentOperator =
        SyntaxKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// SyntaxKind.QuestionQuestionToken | LogicalOperatorOrHigher | AssignmentOperator
    /// </code>
    /// </remarks>
    type AssignmentOperatorOrHigher =
        SyntaxKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// AssignmentOperatorOrHigher | SyntaxKind.CommaToken
    /// </code>
    /// </remarks>
    type BinaryOperator =
        SyntaxKind

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// SyntaxKind.AmpersandAmpersandEqualsToken | SyntaxKind.BarBarEqualsToken | SyntaxKind.QuestionQuestionEqualsToken
    /// </code>
    /// </remarks>
    type LogicalOrCoalescingAssignmentOperator =
        SyntaxKind

    type BinaryOperatorToken =
        Token<BinaryOperator>

    type [<AllowNullLiteral>] BinaryExpression =
        inherit Expression
        inherit Declaration
        inherit JSDocContainer
        abstract kind: SyntaxKind
        abstract left: Expression
        abstract operatorToken: BinaryOperatorToken
        abstract right: Expression

    type AssignmentOperatorToken =
        Token<AssignmentOperator>

    type [<AllowNullLiteral>] AssignmentExpression<'TOperator when 'TOperator :> AssignmentOperatorToken> =
        inherit BinaryExpression
        abstract left: LeftHandSideExpression
        abstract operatorToken: 'TOperator

    type [<AllowNullLiteral>] ObjectDestructuringAssignment =
        inherit AssignmentExpression<EqualsToken>
        abstract left: ObjectLiteralExpression

    type [<AllowNullLiteral>] ArrayDestructuringAssignment =
        inherit AssignmentExpression<EqualsToken>
        abstract left: ArrayLiteralExpression

    type DestructuringAssignment =
        U2<ObjectDestructuringAssignment, ArrayDestructuringAssignment>

    type BindingOrAssignmentElement =
        U4<VariableDeclaration, ParameterDeclaration, ObjectBindingOrAssignmentElement, ArrayBindingOrAssignmentElement>

    type ObjectBindingOrAssignmentElement =
        U4<BindingElement, PropertyAssignment, ShorthandPropertyAssignment, SpreadAssignment>

    type ArrayBindingOrAssignmentElement =
        obj

    type BindingOrAssignmentElementRestIndicator =
        U3<DotDotDotToken, SpreadElement, SpreadAssignment>

    type BindingOrAssignmentElementTarget =
        U5<BindingOrAssignmentPattern, Identifier, PropertyAccessExpression, ElementAccessExpression, OmittedExpression>

    type ObjectBindingOrAssignmentPattern =
        U2<ObjectBindingPattern, ObjectLiteralExpression>

    type ArrayBindingOrAssignmentPattern =
        U2<ArrayBindingPattern, ArrayLiteralExpression>

    type AssignmentPattern =
        U2<ObjectLiteralExpression, ArrayLiteralExpression>

    type BindingOrAssignmentPattern =
        U2<ObjectBindingOrAssignmentPattern, ArrayBindingOrAssignmentPattern>

    type [<AllowNullLiteral>] ConditionalExpression =
        inherit Expression
        abstract kind: SyntaxKind
        abstract condition: Expression
        abstract questionToken: QuestionToken
        abstract whenTrue: Expression
        abstract colonToken: ColonToken
        abstract whenFalse: Expression

    type FunctionBody =
        Block

    type ConciseBody =
        U2<FunctionBody, Expression>

    type [<AllowNullLiteral>] FunctionExpression =
        inherit PrimaryExpression
        inherit FunctionLikeDeclarationBase
        inherit JSDocContainer
        inherit LocalsContainer
        inherit FlowContainer
        abstract kind: SyntaxKind
        abstract modifiers: Modifier[] option
        abstract name: Identifier option
        abstract body: FunctionBody

    type [<AllowNullLiteral>] ArrowFunction =
        inherit Expression
        inherit FunctionLikeDeclarationBase
        inherit JSDocContainer
        inherit LocalsContainer
        inherit FlowContainer
        abstract kind: SyntaxKind
        abstract modifiers: Modifier[] option
        abstract equalsGreaterThanToken: EqualsGreaterThanToken
        abstract body: ConciseBody
        abstract name: obj

    type [<AllowNullLiteral>] LiteralLikeNode =
        inherit Node
        abstract text: string with get, set
        abstract isUnterminated: bool option with get, set
        abstract hasExtendedUnicodeEscape: bool option with get, set

    type [<AllowNullLiteral>] TemplateLiteralLikeNode =
        inherit LiteralLikeNode
        abstract rawText: string option with get, set

    type [<AllowNullLiteral>] LiteralExpression =
        inherit LiteralLikeNode
        inherit PrimaryExpression
        abstract _literalExpressionBrand: obj option with get, set

    type [<AllowNullLiteral>] RegularExpressionLiteral =
        inherit LiteralExpression
        abstract kind: SyntaxKind

    type [<AllowNullLiteral>] NoSubstitutionTemplateLiteral =
        inherit LiteralExpression
        inherit TemplateLiteralLikeNode
        inherit Declaration
        abstract kind: SyntaxKind

    type [<RequireQualifiedAccess>] TokenFlags =
        | None = 0
        | Scientific = 16
        | Octal = 32
        | HexSpecifier = 64
        | BinarySpecifier = 128
        | OctalSpecifier = 256

    type [<AllowNullLiteral>] NumericLiteral =
        inherit LiteralExpression
        inherit Declaration
        abstract kind: SyntaxKind

    type [<AllowNullLiteral>] BigIntLiteral =
        inherit LiteralExpression
        abstract kind: SyntaxKind

    type LiteralToken =
        U6<NumericLiteral, BigIntLiteral, StringLiteral, JsxText, RegularExpressionLiteral, NoSubstitutionTemplateLiteral>

    type [<AllowNullLiteral>] TemplateHead =
        inherit TemplateLiteralLikeNode
        abstract kind: SyntaxKind
        abstract parent: U2<TemplateExpression, TemplateLiteralTypeNode>

    type [<AllowNullLiteral>] TemplateMiddle =
        inherit TemplateLiteralLikeNode
        abstract kind: SyntaxKind
        abstract parent: U2<TemplateSpan, TemplateLiteralTypeSpan>

    type [<AllowNullLiteral>] TemplateTail =
        inherit TemplateLiteralLikeNode
        abstract kind: SyntaxKind
        abstract parent: U2<TemplateSpan, TemplateLiteralTypeSpan>

    type PseudoLiteralToken =
        U3<TemplateHead, TemplateMiddle, TemplateTail>

    type TemplateLiteralToken =
        U2<NoSubstitutionTemplateLiteral, PseudoLiteralToken>

    type [<AllowNullLiteral>] TemplateExpression =
        inherit PrimaryExpression
        abstract kind: SyntaxKind
        abstract head: TemplateHead
        abstract templateSpans: TemplateSpan[]

    type TemplateLiteral =
        U2<TemplateExpression, NoSubstitutionTemplateLiteral>

    type [<AllowNullLiteral>] TemplateSpan =
        inherit Node
        abstract kind: SyntaxKind
        abstract parent: TemplateExpression
        abstract expression: Expression
        abstract literal: U2<TemplateMiddle, TemplateTail>

    type [<AllowNullLiteral>] ParenthesizedExpression =
        inherit PrimaryExpression
        inherit JSDocContainer
        abstract kind: SyntaxKind
        abstract expression: Expression

    type [<AllowNullLiteral>] ArrayLiteralExpression =
        inherit PrimaryExpression
        abstract kind: SyntaxKind
        abstract elements: Expression[]

    type [<AllowNullLiteral>] SpreadElement =
        inherit Expression
        abstract kind: SyntaxKind
        abstract parent: U3<ArrayLiteralExpression, CallExpression, NewExpression>
        abstract expression: Expression

    /// This interface is a base interface for ObjectLiteralExpression and JSXAttributes to extend from. JSXAttributes is similar to
    /// ObjectLiteralExpression in that it contains array of properties; however, JSXAttributes' properties can only be
    /// JSXAttribute or JSXSpreadAttribute. ObjectLiteralExpression, on the other hand, can only have properties of type
    /// ObjectLiteralElement (e.g. PropertyAssignment, ShorthandPropertyAssignment etc.)
    type [<AllowNullLiteral>] ObjectLiteralExpressionBase<'T when 'T :> ObjectLiteralElement> =
        inherit PrimaryExpression
        inherit Declaration
        abstract properties: 'T[]

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

    type EntityNameExpression =
        U2<Identifier, PropertyAccessEntityNameExpression>

    type EntityNameOrEntityNameExpression =
        U2<EntityName, EntityNameExpression>

    type AccessExpression =
        U2<PropertyAccessExpression, ElementAccessExpression>

    type [<AllowNullLiteral>] PropertyAccessExpression =
        inherit MemberExpression
        inherit NamedDeclaration
        inherit JSDocContainer
        inherit FlowContainer
        abstract kind: SyntaxKind
        abstract expression: LeftHandSideExpression
        abstract questionDotToken: QuestionDotToken option
        abstract name: MemberName

    type [<AllowNullLiteral>] PropertyAccessChain =
        inherit PropertyAccessExpression
        abstract _optionalChainBrand: obj option with get, set
        abstract name: MemberName

    type [<AllowNullLiteral>] SuperPropertyAccessExpression =
        inherit PropertyAccessExpression
        abstract expression: SuperExpression

    /// Brand for a PropertyAccessExpression which, like a QualifiedName, consists of a sequence of identifiers separated by dots.
    type [<AllowNullLiteral>] PropertyAccessEntityNameExpression =
        inherit PropertyAccessExpression
        abstract _propertyAccessExpressionLikeQualifiedNameBrand: obj option with get, set
        abstract expression: EntityNameExpression
        abstract name: Identifier

    type [<AllowNullLiteral>] ElementAccessExpression =
        inherit MemberExpression
        inherit Declaration
        inherit JSDocContainer
        inherit FlowContainer
        abstract kind: SyntaxKind
        abstract expression: LeftHandSideExpression
        abstract questionDotToken: QuestionDotToken option
        abstract argumentExpression: Expression

    type [<AllowNullLiteral>] ElementAccessChain =
        inherit ElementAccessExpression
        abstract _optionalChainBrand: obj option with get, set

    type [<AllowNullLiteral>] SuperElementAccessExpression =
        inherit ElementAccessExpression
        abstract expression: SuperExpression

    type SuperProperty =
        U2<SuperPropertyAccessExpression, SuperElementAccessExpression>

    type [<AllowNullLiteral>] CallExpression =
        inherit LeftHandSideExpression
        inherit Declaration
        abstract kind: SyntaxKind
        abstract expression: LeftHandSideExpression
        abstract questionDotToken: QuestionDotToken option
        abstract typeArguments: TypeNode[] option
        abstract arguments: Expression[]

    type [<AllowNullLiteral>] CallChain =
        inherit CallExpression
        abstract _optionalChainBrand: obj option with get, set

    type OptionalChain =
        U4<PropertyAccessChain, ElementAccessChain, CallChain, NonNullChain>

    type [<AllowNullLiteral>] SuperCall =
        inherit CallExpression
        abstract expression: SuperExpression

    type [<AllowNullLiteral>] ImportCall =
        inherit CallExpression
        abstract expression: ImportExpression

    type [<AllowNullLiteral>] ExpressionWithTypeArguments =
        inherit MemberExpression
        inherit NodeWithTypeArguments
        abstract kind: SyntaxKind
        abstract expression: LeftHandSideExpression

    type [<AllowNullLiteral>] NewExpression =
        inherit PrimaryExpression
        inherit Declaration
        abstract kind: SyntaxKind
        abstract expression: LeftHandSideExpression
        abstract typeArguments: TypeNode[] option
        abstract arguments: Expression[] option

    type [<AllowNullLiteral>] TaggedTemplateExpression =
        inherit MemberExpression
        abstract kind: SyntaxKind
        abstract tag: LeftHandSideExpression
        abstract typeArguments: TypeNode[] option
        abstract template: TemplateLiteral

    type CallLikeExpression =
        U5<CallExpression, NewExpression, TaggedTemplateExpression, Decorator, JsxOpeningLikeElement>

    type [<AllowNullLiteral>] AsExpression =
        inherit Expression
        abstract kind: SyntaxKind
        abstract expression: Expression
        abstract ``type``: TypeNode

    type [<AllowNullLiteral>] TypeAssertion =
        inherit UnaryExpression
        abstract kind: SyntaxKind
        abstract ``type``: TypeNode
        abstract expression: UnaryExpression

    type [<AllowNullLiteral>] SatisfiesExpression =
        inherit Expression
        abstract kind: SyntaxKind
        abstract expression: Expression
        abstract ``type``: TypeNode

    type AssertionExpression =
        U2<TypeAssertion, AsExpression>

    type [<AllowNullLiteral>] NonNullExpression =
        inherit LeftHandSideExpression
        abstract kind: SyntaxKind
        abstract expression: Expression

    type [<AllowNullLiteral>] NonNullChain =
        inherit NonNullExpression
        abstract _optionalChainBrand: obj option with get, set

    type [<AllowNullLiteral>] MetaProperty =
        inherit PrimaryExpression
        inherit FlowContainer
        abstract kind: SyntaxKind
        abstract keywordToken: SyntaxKind
        abstract name: Identifier

    type [<AllowNullLiteral>] JsxElement =
        inherit PrimaryExpression
        abstract kind: SyntaxKind
        abstract openingElement: JsxOpeningElement
        abstract children: JsxChild[]
        abstract closingElement: JsxClosingElement

    type JsxOpeningLikeElement =
        U2<JsxSelfClosingElement, JsxOpeningElement>

    type JsxAttributeLike =
        U2<JsxAttribute, JsxSpreadAttribute>

    type JsxAttributeName =
        U2<Identifier, JsxNamespacedName>

    type JsxTagNameExpression =
        U4<Identifier, ThisExpression, JsxTagNamePropertyAccess, JsxNamespacedName>

    type [<AllowNullLiteral>] JsxTagNamePropertyAccess =
        inherit PropertyAccessExpression
        abstract expression: U3<Identifier, ThisExpression, JsxTagNamePropertyAccess>

    type [<AllowNullLiteral>] JsxAttributes =
        inherit PrimaryExpression
        inherit Declaration
        abstract properties: JsxAttributeLike[]
        abstract kind: SyntaxKind
        abstract parent: JsxOpeningLikeElement

    type [<AllowNullLiteral>] JsxNamespacedName =
        inherit Node
        abstract kind: SyntaxKind
        abstract name: Identifier
        abstract ``namespace``: Identifier

    type [<AllowNullLiteral>] JsxOpeningElement =
        inherit Expression
        abstract kind: SyntaxKind
        abstract parent: JsxElement
        abstract tagName: JsxTagNameExpression
        abstract typeArguments: TypeNode[] option
        abstract attributes: JsxAttributes

    type [<AllowNullLiteral>] JsxSelfClosingElement =
        inherit PrimaryExpression
        abstract kind: SyntaxKind
        abstract tagName: JsxTagNameExpression
        abstract typeArguments: TypeNode[] option
        abstract attributes: JsxAttributes

    type [<AllowNullLiteral>] JsxFragment =
        inherit PrimaryExpression
        abstract kind: SyntaxKind
        abstract openingFragment: JsxOpeningFragment
        abstract children: JsxChild[]
        abstract closingFragment: JsxClosingFragment

    type [<AllowNullLiteral>] JsxOpeningFragment =
        inherit Expression
        abstract kind: SyntaxKind
        abstract parent: JsxFragment

    type [<AllowNullLiteral>] JsxClosingFragment =
        inherit Expression
        abstract kind: SyntaxKind
        abstract parent: JsxFragment

    type [<AllowNullLiteral>] JsxAttribute =
        inherit Declaration
        abstract kind: SyntaxKind
        abstract parent: JsxAttributes
        abstract name: JsxAttributeName
        abstract initializer: JsxAttributeValue option

    type JsxAttributeValue =
        U5<StringLiteral, JsxExpression, JsxElement, JsxSelfClosingElement, JsxFragment>

    type [<AllowNullLiteral>] JsxSpreadAttribute =
        inherit ObjectLiteralElement
        abstract kind: SyntaxKind
        abstract parent: JsxAttributes
        abstract expression: Expression

    type [<AllowNullLiteral>] JsxClosingElement =
        inherit Node
        abstract kind: SyntaxKind
        abstract parent: JsxElement
        abstract tagName: JsxTagNameExpression

    type [<AllowNullLiteral>] JsxExpression =
        inherit Expression
        abstract kind: SyntaxKind
        abstract parent: U3<JsxElement, JsxFragment, JsxAttributeLike>
        abstract dotDotDotToken: Token<SyntaxKind> option
        abstract expression: Expression option

    type [<AllowNullLiteral>] JsxText =
        inherit LiteralLikeNode
        abstract kind: SyntaxKind
        abstract parent: U2<JsxElement, JsxFragment>
        abstract containsOnlyTriviaWhiteSpaces: bool

    type JsxChild =
        U5<JsxText, JsxExpression, JsxElement, JsxSelfClosingElement, JsxFragment>

    type [<AllowNullLiteral>] Statement =
        inherit Node
        inherit JSDocContainer
        abstract _statementBrand: obj option with get, set

    type [<AllowNullLiteral>] NotEmittedStatement =
        inherit Statement
        abstract kind: SyntaxKind

    /// A list of comma-separated expressions. This node is only created by transformations.
    type [<AllowNullLiteral>] CommaListExpression =
        inherit Expression
        abstract kind: SyntaxKind
        abstract elements: Expression[]

    type [<AllowNullLiteral>] EmptyStatement =
        inherit Statement
        abstract kind: SyntaxKind

    type [<AllowNullLiteral>] DebuggerStatement =
        inherit Statement
        inherit FlowContainer
        abstract kind: SyntaxKind

    type [<AllowNullLiteral>] MissingDeclaration =
        inherit DeclarationStatement
        inherit PrimaryExpression
        abstract kind: SyntaxKind
        abstract name: Identifier option

    type BlockLike =
        U4<SourceFile, Block, ModuleBlock, CaseOrDefaultClause>

    type [<AllowNullLiteral>] Block =
        inherit Statement
        inherit LocalsContainer
        abstract kind: SyntaxKind
        abstract statements: Statement[]

    type [<AllowNullLiteral>] VariableStatement =
        inherit Statement
        inherit FlowContainer
        abstract kind: SyntaxKind
        abstract modifiers: ModifierLike[] option
        abstract declarationList: VariableDeclarationList

    type [<AllowNullLiteral>] ExpressionStatement =
        inherit Statement
        inherit FlowContainer
        abstract kind: SyntaxKind
        abstract expression: Expression

    type [<AllowNullLiteral>] IfStatement =
        inherit Statement
        inherit FlowContainer
        abstract kind: SyntaxKind
        abstract expression: Expression
        abstract thenStatement: Statement
        abstract elseStatement: Statement option

    type [<AllowNullLiteral>] IterationStatement =
        inherit Statement
        abstract statement: Statement

    type [<AllowNullLiteral>] DoStatement =
        inherit IterationStatement
        inherit FlowContainer
        abstract kind: SyntaxKind
        abstract expression: Expression

    type [<AllowNullLiteral>] WhileStatement =
        inherit IterationStatement
        inherit FlowContainer
        abstract kind: SyntaxKind
        abstract expression: Expression

    type ForInitializer =
        U2<VariableDeclarationList, Expression>

    type [<AllowNullLiteral>] ForStatement =
        inherit IterationStatement
        inherit LocalsContainer
        inherit FlowContainer
        abstract kind: SyntaxKind
        abstract initializer: ForInitializer option
        abstract condition: Expression option
        abstract incrementor: Expression option

    type ForInOrOfStatement =
        U2<ForInStatement, ForOfStatement>

    type [<AllowNullLiteral>] ForInStatement =
        inherit IterationStatement
        inherit LocalsContainer
        inherit FlowContainer
        abstract kind: SyntaxKind
        abstract initializer: ForInitializer
        abstract expression: Expression

    type [<AllowNullLiteral>] ForOfStatement =
        inherit IterationStatement
        inherit LocalsContainer
        inherit FlowContainer
        abstract kind: SyntaxKind
        abstract awaitModifier: AwaitKeyword option
        abstract initializer: ForInitializer
        abstract expression: Expression

    type [<AllowNullLiteral>] BreakStatement =
        inherit Statement
        inherit FlowContainer
        abstract kind: SyntaxKind
        abstract label: Identifier option

    type [<AllowNullLiteral>] ContinueStatement =
        inherit Statement
        inherit FlowContainer
        abstract kind: SyntaxKind
        abstract label: Identifier option

    type BreakOrContinueStatement =
        U2<BreakStatement, ContinueStatement>

    type [<AllowNullLiteral>] ReturnStatement =
        inherit Statement
        inherit FlowContainer
        abstract kind: SyntaxKind
        abstract expression: Expression option

    type [<AllowNullLiteral>] WithStatement =
        inherit Statement
        inherit FlowContainer
        abstract kind: SyntaxKind
        abstract expression: Expression
        abstract statement: Statement

    type [<AllowNullLiteral>] SwitchStatement =
        inherit Statement
        inherit FlowContainer
        abstract kind: SyntaxKind
        abstract expression: Expression
        abstract caseBlock: CaseBlock
        abstract possiblyExhaustive: bool option with get, set

    type [<AllowNullLiteral>] CaseBlock =
        inherit Node
        inherit LocalsContainer
        abstract kind: SyntaxKind
        abstract parent: SwitchStatement
        abstract clauses: CaseOrDefaultClause[]

    type [<AllowNullLiteral>] CaseClause =
        inherit Node
        inherit JSDocContainer
        abstract kind: SyntaxKind
        abstract parent: CaseBlock
        abstract expression: Expression
        abstract statements: Statement[]

    type [<AllowNullLiteral>] DefaultClause =
        inherit Node
        abstract kind: SyntaxKind
        abstract parent: CaseBlock
        abstract statements: Statement[]

    type CaseOrDefaultClause =
        U2<CaseClause, DefaultClause>

    type [<AllowNullLiteral>] LabeledStatement =
        inherit Statement
        inherit FlowContainer
        abstract kind: SyntaxKind
        abstract label: Identifier
        abstract statement: Statement

    type [<AllowNullLiteral>] ThrowStatement =
        inherit Statement
        inherit FlowContainer
        abstract kind: SyntaxKind
        abstract expression: Expression

    type [<AllowNullLiteral>] TryStatement =
        inherit Statement
        inherit FlowContainer
        abstract kind: SyntaxKind
        abstract tryBlock: Block
        abstract catchClause: CatchClause option
        abstract finallyBlock: Block option

    type [<AllowNullLiteral>] CatchClause =
        inherit Node
        inherit LocalsContainer
        abstract kind: SyntaxKind
        abstract parent: TryStatement
        abstract variableDeclaration: VariableDeclaration option
        abstract block: Block

    type ObjectTypeDeclaration =
        U3<ClassLikeDeclaration, InterfaceDeclaration, TypeLiteralNode>

    type DeclarationWithTypeParameters =
        U4<DeclarationWithTypeParameterChildren, JSDocTypedefTag, JSDocCallbackTag, JSDocSignature>

    type DeclarationWithTypeParameterChildren =
        U5<SignatureDeclaration, ClassLikeDeclaration, InterfaceDeclaration, TypeAliasDeclaration, JSDocTemplateTag>

    type [<AllowNullLiteral>] ClassLikeDeclarationBase =
        inherit NamedDeclaration
        inherit JSDocContainer
        abstract kind: SyntaxKind
        abstract name: Identifier option
        abstract typeParameters: TypeParameterDeclaration[] option
        abstract heritageClauses: HeritageClause[] option
        abstract members: ClassElement[]

    type [<AllowNullLiteral>] ClassDeclaration =
        inherit ClassLikeDeclarationBase
        inherit DeclarationStatement
        abstract kind: SyntaxKind
        abstract modifiers: ModifierLike[] option
        /// <summary>May be undefined in <c>export default class { ... }</c>.</summary>
        abstract name: Identifier option

    type [<AllowNullLiteral>] ClassExpression =
        inherit ClassLikeDeclarationBase
        inherit PrimaryExpression
        abstract kind: SyntaxKind
        abstract modifiers: ModifierLike[] option

    type ClassLikeDeclaration =
        U2<ClassDeclaration, ClassExpression>

    type [<AllowNullLiteral>] ClassElement =
        inherit NamedDeclaration
        abstract _classElementBrand: obj option with get, set
        abstract name: PropertyName option

    type [<AllowNullLiteral>] TypeElement =
        inherit NamedDeclaration
        abstract _typeElementBrand: obj option with get, set
        abstract name: PropertyName option
        abstract questionToken: QuestionToken option

    type [<AllowNullLiteral>] InterfaceDeclaration =
        inherit DeclarationStatement
        inherit JSDocContainer
        abstract kind: SyntaxKind
        abstract modifiers: ModifierLike[] option
        abstract name: Identifier
        abstract typeParameters: TypeParameterDeclaration[] option
        abstract heritageClauses: HeritageClause[] option
        abstract members: TypeElement[]

    type [<AllowNullLiteral>] HeritageClause =
        inherit Node
        abstract kind: SyntaxKind
        abstract parent: U2<InterfaceDeclaration, ClassLikeDeclaration>
        abstract token: SyntaxKind
        abstract types: ExpressionWithTypeArguments[]

    type [<AllowNullLiteral>] TypeAliasDeclaration =
        inherit DeclarationStatement
        inherit JSDocContainer
        inherit LocalsContainer
        abstract kind: SyntaxKind
        abstract modifiers: ModifierLike[] option
        abstract name: Identifier
        abstract typeParameters: TypeParameterDeclaration[] option
        abstract ``type``: TypeNode

    type [<AllowNullLiteral>] EnumMember =
        inherit NamedDeclaration
        inherit JSDocContainer
        abstract kind: SyntaxKind
        abstract parent: EnumDeclaration
        abstract name: PropertyName
        abstract initializer: Expression option

    type [<AllowNullLiteral>] EnumDeclaration =
        inherit DeclarationStatement
        inherit JSDocContainer
        abstract kind: SyntaxKind
        abstract modifiers: ModifierLike[] option
        abstract name: Identifier
        abstract members: EnumMember[]

    type ModuleName =
        U2<Identifier, StringLiteral>

    type ModuleBody =
        U2<NamespaceBody, JSDocNamespaceBody>

    type [<AllowNullLiteral>] ModuleDeclaration =
        inherit DeclarationStatement
        inherit JSDocContainer
        inherit LocalsContainer
        abstract kind: SyntaxKind
        abstract parent: U2<ModuleBody, SourceFile>
        abstract modifiers: ModifierLike[] option
        abstract name: ModuleName
        abstract body: U2<ModuleBody, JSDocNamespaceDeclaration> option

    type NamespaceBody =
        U2<ModuleBlock, NamespaceDeclaration>

    type [<AllowNullLiteral>] NamespaceDeclaration =
        inherit ModuleDeclaration
        abstract name: Identifier
        abstract body: NamespaceBody

    type JSDocNamespaceBody =
        U2<Identifier, JSDocNamespaceDeclaration>

    type [<AllowNullLiteral>] JSDocNamespaceDeclaration =
        inherit ModuleDeclaration
        abstract name: Identifier
        abstract body: JSDocNamespaceBody option

    type [<AllowNullLiteral>] ModuleBlock =
        inherit Node
        inherit Statement
        abstract kind: SyntaxKind
        abstract parent: ModuleDeclaration
        abstract statements: Statement[]

    type ModuleReference =
        U2<EntityName, ExternalModuleReference>

    /// One of:
    /// - import x = require("mod");
    /// - import x = M.x;
    type [<AllowNullLiteral>] ImportEqualsDeclaration =
        inherit DeclarationStatement
        inherit JSDocContainer
        abstract kind: SyntaxKind
        abstract parent: U2<SourceFile, ModuleBlock>
        abstract modifiers: ModifierLike[] option
        abstract name: Identifier
        abstract isTypeOnly: bool
        abstract moduleReference: ModuleReference

    type [<AllowNullLiteral>] ExternalModuleReference =
        inherit Node
        abstract kind: SyntaxKind
        abstract parent: ImportEqualsDeclaration
        abstract expression: Expression

    type [<AllowNullLiteral>] ImportDeclaration =
        inherit Statement
        abstract kind: SyntaxKind
        abstract parent: U2<SourceFile, ModuleBlock>
        abstract modifiers: ModifierLike[] option
        abstract importClause: ImportClause option
        /// If this is not a StringLiteral it will be a grammar error.
        abstract moduleSpecifier: Expression
        abstract assertClause: AssertClause option

    type NamedImportBindings =
        U2<NamespaceImport, NamedImports>

    type NamedExportBindings =
        U2<NamespaceExport, NamedExports>

    type [<AllowNullLiteral>] ImportClause =
        inherit NamedDeclaration
        abstract kind: SyntaxKind
        abstract parent: ImportDeclaration
        abstract isTypeOnly: bool
        abstract name: Identifier option
        abstract namedBindings: NamedImportBindings option

    type AssertionKey =
        U2<Identifier, StringLiteral>

    type [<AllowNullLiteral>] AssertEntry =
        inherit Node
        abstract kind: SyntaxKind
        abstract parent: AssertClause
        abstract name: AssertionKey
        abstract value: Expression

    type [<AllowNullLiteral>] AssertClause =
        inherit Node
        abstract kind: SyntaxKind
        abstract parent: U2<ImportDeclaration, ExportDeclaration>
        abstract elements: AssertEntry[]
        abstract multiLine: bool option

    type [<AllowNullLiteral>] NamespaceImport =
        inherit NamedDeclaration
        abstract kind: SyntaxKind
        abstract parent: ImportClause
        abstract name: Identifier

    type [<AllowNullLiteral>] NamespaceExport =
        inherit NamedDeclaration
        abstract kind: SyntaxKind
        abstract parent: ExportDeclaration
        abstract name: Identifier

    type [<AllowNullLiteral>] NamespaceExportDeclaration =
        inherit DeclarationStatement
        inherit JSDocContainer
        abstract kind: SyntaxKind
        abstract name: Identifier

    type [<AllowNullLiteral>] ExportDeclaration =
        inherit DeclarationStatement
        inherit JSDocContainer
        abstract kind: SyntaxKind
        abstract parent: U2<SourceFile, ModuleBlock>
        abstract modifiers: ModifierLike[] option
        abstract isTypeOnly: bool
        /// <summary>Will not be assigned in the case of <c>export * from "foo";</c></summary>
        abstract exportClause: NamedExportBindings option
        /// If this is not a StringLiteral it will be a grammar error.
        abstract moduleSpecifier: Expression option
        abstract assertClause: AssertClause option

    type [<AllowNullLiteral>] NamedImports =
        inherit Node
        abstract kind: SyntaxKind
        abstract parent: ImportClause
        abstract elements: ImportSpecifier[]

    type [<AllowNullLiteral>] NamedExports =
        inherit Node
        abstract kind: SyntaxKind
        abstract parent: ExportDeclaration
        abstract elements: ExportSpecifier[]

    type NamedImportsOrExports =
        U2<NamedImports, NamedExports>

    type [<AllowNullLiteral>] ImportSpecifier =
        inherit NamedDeclaration
        abstract kind: SyntaxKind
        abstract parent: NamedImports
        abstract propertyName: Identifier option
        abstract name: Identifier
        abstract isTypeOnly: bool

    type [<AllowNullLiteral>] ExportSpecifier =
        inherit NamedDeclaration
        inherit JSDocContainer
        abstract kind: SyntaxKind
        abstract parent: NamedExports
        abstract isTypeOnly: bool
        abstract propertyName: Identifier option
        abstract name: Identifier

    type ImportOrExportSpecifier =
        U2<ImportSpecifier, ExportSpecifier>

    type TypeOnlyCompatibleAliasDeclaration =
        U6<ImportClause, ImportEqualsDeclaration, NamespaceImport, ImportOrExportSpecifier, ExportDeclaration, NamespaceExport>

    type TypeOnlyImportDeclaration =
        obj

    type TypeOnlyExportDeclaration =
        obj

    type TypeOnlyAliasDeclaration =
        U2<TypeOnlyImportDeclaration, TypeOnlyExportDeclaration>

    /// <summary>
    /// This is either an <c>export =</c> or an <c>export default</c> declaration.
    /// Unless <c>isExportEquals</c> is set, this node was parsed as an <c>export default</c>.
    /// </summary>
    type [<AllowNullLiteral>] ExportAssignment =
        inherit DeclarationStatement
        inherit JSDocContainer
        abstract kind: SyntaxKind
        abstract parent: SourceFile
        abstract modifiers: ModifierLike[] option
        abstract isExportEquals: bool option
        abstract expression: Expression

    type [<AllowNullLiteral>] FileReference =
        inherit TextRange
        abstract fileName: string with get, set
        abstract resolutionMode: ResolutionMode option with get, set

    type [<AllowNullLiteral>] CheckJsDirective =
        inherit TextRange
        abstract enabled: bool with get, set

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// SyntaxKind.SingleLineCommentTrivia | SyntaxKind.MultiLineCommentTrivia
    /// </code>
    /// </remarks>
    type CommentKind =
        SyntaxKind

    type [<AllowNullLiteral>] CommentRange =
        inherit TextRange
        abstract hasTrailingNewLine: bool option with get, set
        abstract kind: CommentKind with get, set

    type [<AllowNullLiteral>] SynthesizedComment =
        inherit CommentRange
        abstract text: string with get, set
        abstract pos: int with get, set
        abstract ``end``: int with get, set
        abstract hasLeadingNewline: bool option with get, set

    type [<AllowNullLiteral>] JSDocTypeExpression =
        inherit TypeNode
        abstract kind: SyntaxKind
        abstract ``type``: TypeNode

    type [<AllowNullLiteral>] JSDocNameReference =
        inherit Node
        abstract kind: SyntaxKind
        abstract name: U2<EntityName, JSDocMemberName>

    /// Class#method reference in JSDoc
    type [<AllowNullLiteral>] JSDocMemberName =
        inherit Node
        abstract kind: SyntaxKind
        abstract left: U2<EntityName, JSDocMemberName>
        abstract right: Identifier

    type [<AllowNullLiteral>] JSDocType =
        inherit TypeNode
        abstract _jsDocTypeBrand: obj option with get, set

    type [<AllowNullLiteral>] JSDocAllType =
        inherit JSDocType
        abstract kind: SyntaxKind

    type [<AllowNullLiteral>] JSDocUnknownType =
        inherit JSDocType
        abstract kind: SyntaxKind

    type [<AllowNullLiteral>] JSDocNonNullableType =
        inherit JSDocType
        abstract kind: SyntaxKind
        abstract ``type``: TypeNode
        abstract postfix: bool

    type [<AllowNullLiteral>] JSDocNullableType =
        inherit JSDocType
        abstract kind: SyntaxKind
        abstract ``type``: TypeNode
        abstract postfix: bool

    type [<AllowNullLiteral>] JSDocOptionalType =
        inherit JSDocType
        abstract kind: SyntaxKind
        abstract ``type``: TypeNode

    type [<AllowNullLiteral>] JSDocFunctionType =
        inherit JSDocType
        inherit SignatureDeclarationBase
        inherit LocalsContainer
        abstract kind: SyntaxKind

    type [<AllowNullLiteral>] JSDocVariadicType =
        inherit JSDocType
        abstract kind: SyntaxKind
        abstract ``type``: TypeNode

    type [<AllowNullLiteral>] JSDocNamepathType =
        inherit JSDocType
        abstract kind: SyntaxKind
        abstract ``type``: TypeNode

    type JSDocTypeReferencingNode =
        U4<JSDocVariadicType, JSDocOptionalType, JSDocNullableType, JSDocNonNullableType>

    type [<AllowNullLiteral>] JSDoc =
        inherit Node
        abstract kind: SyntaxKind
        abstract parent: HasJSDoc
        abstract tags: JSDocTag[] option
        abstract comment: U2<string, JSDocComment[]> option

    type [<AllowNullLiteral>] JSDocTag =
        inherit Node
        abstract parent: U2<JSDoc, JSDocTypeLiteral>
        abstract tagName: Identifier
        abstract comment: U2<string, JSDocComment[]> option

    type [<AllowNullLiteral>] JSDocLink =
        inherit Node
        abstract kind: SyntaxKind
        abstract name: U2<EntityName, JSDocMemberName> option
        abstract text: string with get, set

    type [<AllowNullLiteral>] JSDocLinkCode =
        inherit Node
        abstract kind: SyntaxKind
        abstract name: U2<EntityName, JSDocMemberName> option
        abstract text: string with get, set

    type [<AllowNullLiteral>] JSDocLinkPlain =
        inherit Node
        abstract kind: SyntaxKind
        abstract name: U2<EntityName, JSDocMemberName> option
        abstract text: string with get, set

    type JSDocComment =
        U4<JSDocText, JSDocLink, JSDocLinkCode, JSDocLinkPlain>

    type [<AllowNullLiteral>] JSDocText =
        inherit Node
        abstract kind: SyntaxKind
        abstract text: string with get, set

    type [<AllowNullLiteral>] JSDocUnknownTag =
        inherit JSDocTag
        abstract kind: SyntaxKind

    /// <summary>
    /// Note that <c>@extends</c> is a synonym of <c>@augments</c>.
    /// Both tags are represented by this interface.
    /// </summary>
    type [<AllowNullLiteral>] JSDocAugmentsTag =
        inherit JSDocTag
        abstract kind: SyntaxKind
        abstract ``class``: obj

    type [<AllowNullLiteral>] JSDocImplementsTag =
        inherit JSDocTag
        abstract kind: SyntaxKind
        abstract ``class``: obj

    type [<AllowNullLiteral>] JSDocAuthorTag =
        inherit JSDocTag
        abstract kind: SyntaxKind

    type [<AllowNullLiteral>] JSDocDeprecatedTag =
        inherit JSDocTag
        abstract kind: SyntaxKind with get, set

    type [<AllowNullLiteral>] JSDocClassTag =
        inherit JSDocTag
        abstract kind: SyntaxKind

    type [<AllowNullLiteral>] JSDocPublicTag =
        inherit JSDocTag
        abstract kind: SyntaxKind

    type [<AllowNullLiteral>] JSDocPrivateTag =
        inherit JSDocTag
        abstract kind: SyntaxKind

    type [<AllowNullLiteral>] JSDocProtectedTag =
        inherit JSDocTag
        abstract kind: SyntaxKind

    type [<AllowNullLiteral>] JSDocReadonlyTag =
        inherit JSDocTag
        abstract kind: SyntaxKind

    type [<AllowNullLiteral>] JSDocOverrideTag =
        inherit JSDocTag
        abstract kind: SyntaxKind

    type [<AllowNullLiteral>] JSDocEnumTag =
        inherit JSDocTag
        inherit Declaration
        inherit LocalsContainer
        abstract kind: SyntaxKind
        abstract parent: JSDoc
        abstract typeExpression: JSDocTypeExpression

    type [<AllowNullLiteral>] JSDocThisTag =
        inherit JSDocTag
        abstract kind: SyntaxKind
        abstract typeExpression: JSDocTypeExpression

    type [<AllowNullLiteral>] JSDocTemplateTag =
        inherit JSDocTag
        abstract kind: SyntaxKind
        abstract ``constraint``: JSDocTypeExpression option
        abstract typeParameters: TypeParameterDeclaration[]

    type [<AllowNullLiteral>] JSDocSeeTag =
        inherit JSDocTag
        abstract kind: SyntaxKind
        abstract name: JSDocNameReference option

    type [<AllowNullLiteral>] JSDocReturnTag =
        inherit JSDocTag
        abstract kind: SyntaxKind
        abstract typeExpression: JSDocTypeExpression option

    type [<AllowNullLiteral>] JSDocTypeTag =
        inherit JSDocTag
        abstract kind: SyntaxKind
        abstract typeExpression: JSDocTypeExpression

    type [<AllowNullLiteral>] JSDocTypedefTag =
        inherit JSDocTag
        inherit NamedDeclaration
        inherit LocalsContainer
        abstract kind: SyntaxKind
        abstract parent: JSDoc
        abstract fullName: U2<JSDocNamespaceDeclaration, Identifier> option
        abstract name: Identifier option
        abstract typeExpression: U2<JSDocTypeExpression, JSDocTypeLiteral> option

    type [<AllowNullLiteral>] JSDocCallbackTag =
        inherit JSDocTag
        inherit NamedDeclaration
        inherit LocalsContainer
        abstract kind: SyntaxKind
        abstract parent: JSDoc
        abstract fullName: U2<JSDocNamespaceDeclaration, Identifier> option
        abstract name: Identifier option
        abstract typeExpression: JSDocSignature

    type [<AllowNullLiteral>] JSDocOverloadTag =
        inherit JSDocTag
        abstract kind: SyntaxKind
        abstract parent: JSDoc
        abstract typeExpression: JSDocSignature

    type [<AllowNullLiteral>] JSDocThrowsTag =
        inherit JSDocTag
        abstract kind: SyntaxKind
        abstract typeExpression: JSDocTypeExpression option

    type [<AllowNullLiteral>] JSDocSignature =
        inherit JSDocType
        inherit Declaration
        inherit JSDocContainer
        inherit LocalsContainer
        abstract kind: SyntaxKind
        abstract typeParameters: JSDocTemplateTag[] option
        abstract parameters: JSDocParameterTag[]
        abstract ``type``: JSDocReturnTag option

    type [<AllowNullLiteral>] JSDocPropertyLikeTag =
        inherit JSDocTag
        inherit Declaration
        abstract parent: JSDoc
        abstract name: EntityName
        abstract typeExpression: JSDocTypeExpression option
        /// Whether the property name came before the type -- non-standard for JSDoc, but Typescript-like
        abstract isNameFirst: bool
        abstract isBracketed: bool

    type [<AllowNullLiteral>] JSDocPropertyTag =
        inherit JSDocPropertyLikeTag
        abstract kind: SyntaxKind

    type [<AllowNullLiteral>] JSDocParameterTag =
        inherit JSDocPropertyLikeTag
        abstract kind: SyntaxKind

    type [<AllowNullLiteral>] JSDocTypeLiteral =
        inherit JSDocType
        inherit Declaration
        abstract kind: SyntaxKind
        abstract jsDocPropertyTags: JSDocPropertyLikeTag[] option
        /// If true, then this type literal represents an *array* of its type.
        abstract isArrayType: bool

    type [<AllowNullLiteral>] JSDocSatisfiesTag =
        inherit JSDocTag
        abstract kind: SyntaxKind
        abstract typeExpression: JSDocTypeExpression

    type [<RequireQualifiedAccess>] FlowFlags =
        | Unreachable = 1
        | Start = 2
        | BranchLabel = 4
        | LoopLabel = 8
        | Assignment = 16
        | TrueCondition = 32
        | FalseCondition = 64
        | SwitchClause = 128
        | ArrayMutation = 256
        | Call = 512
        | ReduceLabel = 1024
        | Referenced = 2048
        | Shared = 4096
        | Label = 12
        | Condition = 96

    type FlowNode =
        U8<FlowStart, FlowLabel, FlowAssignment, FlowCondition, FlowSwitchClause, FlowArrayMutation, FlowCall, FlowReduceLabel>

    type [<AllowNullLiteral>] FlowNodeBase =
        abstract flags: FlowFlags with get, set
        abstract id: float option with get, set

    type [<AllowNullLiteral>] FlowStart =
        inherit FlowNodeBase
        abstract node: U5<FunctionExpression, ArrowFunction, MethodDeclaration, GetAccessorDeclaration, SetAccessorDeclaration> option with get, set

    type [<AllowNullLiteral>] FlowLabel =
        inherit FlowNodeBase
        abstract antecedents: FlowNode[] option with get, set

    type [<AllowNullLiteral>] FlowAssignment =
        inherit FlowNodeBase
        abstract node: U3<Expression, VariableDeclaration, BindingElement> with get, set
        abstract antecedent: FlowNode with get, set

    type [<AllowNullLiteral>] FlowCall =
        inherit FlowNodeBase
        abstract node: CallExpression with get, set
        abstract antecedent: FlowNode with get, set

    type [<AllowNullLiteral>] FlowCondition =
        inherit FlowNodeBase
        abstract node: Expression with get, set
        abstract antecedent: FlowNode with get, set

    type [<AllowNullLiteral>] FlowSwitchClause =
        inherit FlowNodeBase
        abstract switchStatement: SwitchStatement with get, set
        abstract clauseStart: float with get, set
        abstract clauseEnd: float with get, set
        abstract antecedent: FlowNode with get, set

    type [<AllowNullLiteral>] FlowArrayMutation =
        inherit FlowNodeBase
        abstract node: U2<CallExpression, BinaryExpression> with get, set
        abstract antecedent: FlowNode with get, set

    type [<AllowNullLiteral>] FlowReduceLabel =
        inherit FlowNodeBase
        abstract target: FlowLabel with get, set
        abstract antecedents: FlowNode[] with get, set
        abstract antecedent: FlowNode with get, set

    type FlowType =
        U2<Type, IncompleteType>

    type [<AllowNullLiteral>] IncompleteType =
        abstract flags: U2<TypeFlags, float> with get, set
        abstract ``type``: Type with get, set

    type [<AllowNullLiteral>] AmdDependency =
        abstract path: string with get, set
        abstract name: string option with get, set

    /// Subset of properties from SourceFile that are used in multiple utility functions
    type [<AllowNullLiteral>] SourceFileLike =
        abstract text: string
        abstract getLineAndCharacterOfPosition: pos: float -> LineAndCharacter

    /// <remarks>
    /// Original in TypeScript:  
    /// <code lang="typescript">
    /// ModuleKind.ESNext | ModuleKind.CommonJS | undefined
    /// </code>
    /// </remarks>
    type ResolutionMode =
        ModuleKind

    type [<AllowNullLiteral>] SourceFile =
        inherit Declaration
        inherit LocalsContainer
        abstract kind: SyntaxKind
        abstract statements: Statement[]
        abstract endOfFileToken: Token<SyntaxKind>
        abstract fileName: string with get, set
        abstract text: string with get, set
        abstract amdDependencies: AmdDependency[] with get, set
        abstract moduleName: string option with get, set
        abstract referencedFiles: FileReference[] with get, set
        abstract typeReferenceDirectives: FileReference[] with get, set
        abstract libReferenceDirectives: FileReference[] with get, set
        abstract languageVariant: LanguageVariant with get, set
        abstract isDeclarationFile: bool with get, set
        /// lib.d.ts should have a reference comment like
        /// 
        ///  /// <reference no-default-lib="true"/>
        /// 
        /// If any other file has this comment, it signals not to include lib.d.ts
        /// because this containing file is intended to act as a default library.
        abstract hasNoDefaultLib: bool with get, set
        abstract languageVersion: ScriptTarget with get, set
        /// <summary>
        /// When <c>module</c> is <c>Node16</c> or <c>NodeNext</c>, this field controls whether the
        /// source file in question is an ESNext-output-format file, or a CommonJS-output-format
        /// module. This is derived by the module resolver as it looks up the file, since
        /// it is derived from either the file extension of the module, or the containing
        /// <c>package.json</c> context, and affects both checking and emit.
        /// 
        /// It is _public_ so that (pre)transformers can set this field,
        /// since it switches the builtin <c>node</c> module transform. Generally speaking, if unset,
        /// the field is treated as though it is <c>ModuleKind.CommonJS</c>.
        /// 
        /// Note that this field is only set by the module resolution process when
        /// <c>moduleResolution</c> is <c>Node16</c> or <c>NodeNext</c>, which is implied by the <c>module</c> setting
        /// of <c>Node16</c> or <c>NodeNext</c>, respectively, but may be overriden (eg, by a <c>moduleResolution</c>
        /// of <c>node</c>). If so, this field will be unset and source files will be considered to be
        /// CommonJS-output-format by the node module transformer and type checker, regardless of extension or context.
        /// </summary>
        abstract impliedNodeFormat: ResolutionMode option with get, set
        abstract getLineAndCharacterOfPosition: pos: float -> LineAndCharacter
        abstract getLineEndOfPosition: pos: float -> float
        abstract getLineStarts: unit -> float[]
        abstract getPositionOfLineAndCharacter: line: float * character: float -> float
        abstract update: newText: string * textChangeRange: TextChangeRange -> SourceFile

    type [<AllowNullLiteral>] Bundle =
        inherit Node
        abstract kind: SyntaxKind
        abstract sourceFiles: SourceFile[]

    [<Obsolete("")>]
    type [<AllowNullLiteral>] InputFiles =
        inherit Node
        abstract kind: SyntaxKind
        abstract javascriptPath: string option with get, set
        abstract javascriptText: string with get, set
        abstract javascriptMapPath: string option with get, set
        abstract javascriptMapText: string option with get, set
        abstract declarationPath: string option with get, set
        abstract declarationText: string with get, set
        abstract declarationMapPath: string option with get, set
        abstract declarationMapText: string option with get, set

    [<Obsolete("")>]
    type [<AllowNullLiteral>] UnparsedSource =
        inherit Node
        abstract kind: SyntaxKind
        abstract fileName: string with get, set
        abstract text: string with get, set
        abstract prologues: UnparsedPrologue[]
        abstract helpers: UnscopedEmitHelper[] option with get, set
        abstract referencedFiles: FileReference[] with get, set
        abstract typeReferenceDirectives: FileReference[] option with get, set
        abstract libReferenceDirectives: FileReference[] with get, set
        abstract hasNoDefaultLib: bool option with get, set
        abstract sourceMapPath: string option with get, set
        abstract sourceMapText: string option with get, set
        abstract syntheticReferences: UnparsedSyntheticReference[] option
        abstract texts: UnparsedSourceText[]

    [<Obsolete("")>]
    type UnparsedSourceText =
        U2<UnparsedPrepend, UnparsedTextLike>

    [<Obsolete("")>]
    type UnparsedNode =
        U3<UnparsedPrologue, UnparsedSourceText, UnparsedSyntheticReference>

    [<Obsolete("")>]
    type [<AllowNullLiteral>] UnparsedSection =
        inherit Node
        abstract kind: SyntaxKind
        abstract parent: UnparsedSource
        abstract data: string option

    [<Obsolete("")>]
    type [<AllowNullLiteral>] UnparsedPrologue =
        inherit UnparsedSection
        abstract kind: SyntaxKind
        abstract parent: UnparsedSource
        abstract data: string

    [<Obsolete("")>]
    type [<AllowNullLiteral>] UnparsedPrepend =
        inherit UnparsedSection
        abstract kind: SyntaxKind
        abstract parent: UnparsedSource
        abstract data: string
        abstract texts: UnparsedTextLike[]

    [<Obsolete("")>]
    type [<AllowNullLiteral>] UnparsedTextLike =
        inherit UnparsedSection
        abstract kind: SyntaxKind
        abstract parent: UnparsedSource

    [<Obsolete("")>]
    type [<AllowNullLiteral>] UnparsedSyntheticReference =
        inherit UnparsedSection
        abstract kind: SyntaxKind
        abstract parent: UnparsedSource

    type [<AllowNullLiteral>] JsonSourceFile =
        inherit SourceFile
        abstract statements: JsonObjectExpressionStatement[]

    type [<AllowNullLiteral>] TsConfigSourceFile =
        inherit JsonSourceFile
        abstract extendedSourceFiles: string[] option with get, set

    type [<AllowNullLiteral>] JsonMinusNumericLiteral =
        inherit PrefixUnaryExpression
        abstract kind: SyntaxKind
        abstract operator: SyntaxKind
        abstract operand: NumericLiteral

    type JsonObjectExpression =
        U7<ObjectLiteralExpression, ArrayLiteralExpression, JsonMinusNumericLiteral, NumericLiteral, StringLiteral, BooleanLiteral, NullLiteral>

    type [<AllowNullLiteral>] JsonObjectExpressionStatement =
        inherit ExpressionStatement
        abstract expression: JsonObjectExpression

    type [<AllowNullLiteral>] ScriptReferenceHost =
        abstract getCompilerOptions: unit -> CompilerOptions
        abstract getSourceFile: fileName: string -> SourceFile option
        abstract getSourceFileByPath: path: Path -> SourceFile option
        abstract getCurrentDirectory: unit -> string

    type [<AllowNullLiteral>] ParseConfigHost =
        abstract useCaseSensitiveFileNames: bool with get, set
        abstract readDirectory: rootDir: string * extensions: string[] * excludes: string[] option * includes: string[] * ?depth: float -> string[]
        /// <summary>Gets a value indicating whether the specified path exists and is a file.</summary>
        /// <param name="path">The path to test.</param>
        abstract fileExists: path: string -> bool
        abstract readFile: path: string -> string option
        abstract trace: s: string -> unit

    /// Branded string for keeping track of when we've turned an ambiguous path
    /// specified like "./blah" to an absolute path to an actual
    /// tsconfig file, e.g. "/root/blah/tsconfig.json"
    type [<AllowNullLiteral>] ResolvedConfigFileName =
        interface end

    type [<AllowNullLiteral>] WriteFileCallbackData =
        interface end

    type [<AllowNullLiteral>] WriteFileCallback =
        [<Emit("$0($1...)")>] abstract Invoke: fileName: string * text: string * writeByteOrderMark: bool * ?onError: (string -> unit) * ?sourceFiles: SourceFile[] * ?data: WriteFileCallbackData -> unit

    type [<AllowNullLiteral>] OperationCanceledException =
        interface end

    type [<AllowNullLiteral>] OperationCanceledExceptionStatic =
        [<EmitConstructor>] abstract Create: unit -> OperationCanceledException

    type [<AllowNullLiteral>] CancellationToken =
        abstract isCancellationRequested: unit -> bool
        /// <exception cref="">OperationCanceledException if isCancellationRequested is true</exception>
        abstract throwIfCancellationRequested: unit -> unit

    type [<AllowNullLiteral>] Program =
        inherit ScriptReferenceHost
        abstract getCurrentDirectory: unit -> string
        /// Get a list of root file names that were passed to a 'createProgram'
        abstract getRootFileNames: unit -> string[]
        /// Get a list of files in the program
        abstract getSourceFiles: unit -> SourceFile[]
        /// Emits the JavaScript and declaration files.  If targetSourceFile is not specified, then
        /// the JavaScript and declaration files will be produced for all the files in this program.
        /// If targetSourceFile is specified, then only the JavaScript and declaration for that
        /// specific file will be generated.
        /// 
        /// If writeFile is not specified then the writeFile callback from the compiler host will be
        /// used for writing the JavaScript and declaration files.  Otherwise, the writeFile parameter
        /// will be invoked when writing the JavaScript and declaration files.
        abstract emit: ?targetSourceFile: SourceFile * ?writeFile: WriteFileCallback * ?cancellationToken: CancellationToken * ?emitOnlyDtsFiles: bool * ?customTransformers: CustomTransformers -> EmitResult
        abstract getOptionsDiagnostics: ?cancellationToken: CancellationToken -> Diagnostic[]
        abstract getGlobalDiagnostics: ?cancellationToken: CancellationToken -> Diagnostic[]
        abstract getSyntacticDiagnostics: ?sourceFile: SourceFile * ?cancellationToken: CancellationToken -> DiagnosticWithLocation[]
        /// The first time this is called, it will return global diagnostics (no location).
        abstract getSemanticDiagnostics: ?sourceFile: SourceFile * ?cancellationToken: CancellationToken -> Diagnostic[]
        abstract getDeclarationDiagnostics: ?sourceFile: SourceFile * ?cancellationToken: CancellationToken -> DiagnosticWithLocation[]
        abstract getConfigFileParsingDiagnostics: unit -> Diagnostic[]
        /// Gets a type checker that can be used to semantically analyze source files in the program.
        abstract getTypeChecker: unit -> TypeChecker
        abstract getNodeCount: unit -> float
        abstract getIdentifierCount: unit -> float
        abstract getSymbolCount: unit -> float
        abstract getTypeCount: unit -> float
        abstract getInstantiationCount: unit -> float
        abstract getRelationCacheSizes: unit -> {| assignable: float; identity: float; subtype: float; strictSubtype: float |}
        abstract isSourceFileFromExternalLibrary: file: SourceFile -> bool
        abstract isSourceFileDefaultLibrary: file: SourceFile -> bool
        abstract getProjectReferences: unit -> ProjectReference[] option
        abstract getResolvedProjectReferences: unit -> ResolvedProjectReference option[] option

    type [<AllowNullLiteral>] ResolvedProjectReference =
        abstract commandLine: ParsedCommandLine with get, set
        abstract sourceFile: SourceFile with get, set
        abstract references: ResolvedProjectReference option[] option with get, set

    type [<AllowNullLiteral>] CustomTransformerFactory =
        [<Emit("$0($1...)")>] abstract Invoke: context: TransformationContext -> CustomTransformer

    type [<AllowNullLiteral>] CustomTransformer =
        abstract transformSourceFile: node: SourceFile -> SourceFile
        abstract transformBundle: node: Bundle -> Bundle

    type [<AllowNullLiteral>] CustomTransformers =
        /// Custom transformers to evaluate before built-in .js transformations.
        abstract before: U2<TransformerFactory<SourceFile>, CustomTransformerFactory>[] option with get, set
        /// Custom transformers to evaluate after built-in .js transformations.
        abstract after: U2<TransformerFactory<SourceFile>, CustomTransformerFactory>[] option with get, set
        /// Custom transformers to evaluate after built-in .d.ts transformations.
        abstract afterDeclarations: U3<TransformerFactory<Bundle>, TransformerFactory<SourceFile>, CustomTransformerFactory>[] option with get, set

    type [<AllowNullLiteral>] SourceMapSpan =
        /// Line number in the .js file.
        abstract emittedLine: float with get, set
        /// Column number in the .js file.
        abstract emittedColumn: float with get, set
        /// Line number in the .ts file.
        abstract sourceLine: float with get, set
        /// Column number in the .ts file.
        abstract sourceColumn: float with get, set
        /// Optional name (index into names array) associated with this span.
        abstract nameIndex: float option with get, set
        /// .ts file (index into sources array) associated with this span
        abstract sourceIndex: float with get, set

    /// Return code used by getEmitOutput function to indicate status of the function
    type [<RequireQualifiedAccess>] ExitStatus =
        | Success = 0
        | DiagnosticsPresent_OutputsSkipped = 1
        | DiagnosticsPresent_OutputsGenerated = 2
        | InvalidProject_OutputsSkipped = 3
        | ProjectReferenceCycle_OutputsSkipped = 4

    type [<AllowNullLiteral>] EmitResult =
        abstract emitSkipped: bool with get, set
        /// Contains declaration emit diagnostics
        abstract diagnostics: Diagnostic[] with get, set
        abstract emittedFiles: string[] option with get, set

    type [<AllowNullLiteral>] TypeChecker =
        abstract getTypeOfSymbolAtLocation: symbol: Symbol * node: Node -> Type
        abstract getTypeOfSymbol: symbol: Symbol -> Type
        abstract getDeclaredTypeOfSymbol: symbol: Symbol -> Type
        abstract getPropertiesOfType: ``type``: Type -> Symbol[]
        abstract getPropertyOfType: ``type``: Type * propertyName: string -> Symbol option
        abstract getPrivateIdentifierPropertyOfType: leftType: Type * name: string * location: Node -> Symbol option
        abstract getIndexInfoOfType: ``type``: Type * kind: IndexKind -> IndexInfo option
        abstract getIndexInfosOfType: ``type``: Type -> IndexInfo[]
        abstract getIndexInfosOfIndexSymbol: indexSymbol: Symbol -> IndexInfo[]
        abstract getSignaturesOfType: ``type``: Type * kind: SignatureKind -> Signature[]
        abstract getIndexTypeOfType: ``type``: Type * kind: IndexKind -> Type option
        abstract getBaseTypes: ``type``: InterfaceType -> BaseType[]
        abstract getBaseTypeOfLiteralType: ``type``: Type -> Type
        abstract getWidenedType: ``type``: Type -> Type
        abstract getReturnTypeOfSignature: signature: Signature -> Type
        abstract getNullableType: ``type``: Type * flags: TypeFlags -> Type
        abstract getNonNullableType: ``type``: Type -> Type
        abstract getTypeArguments: ``type``: TypeReference -> Type[]
        /// Note that the resulting nodes cannot be checked.
        abstract typeToTypeNode: ``type``: Type * enclosingDeclaration: Node option * flags: NodeBuilderFlags option -> TypeNode option
        /// Note that the resulting nodes cannot be checked.
        abstract signatureToSignatureDeclaration: signature: Signature * kind: SyntaxKind * enclosingDeclaration: Node option * flags: NodeBuilderFlags option -> obj option
        /// Note that the resulting nodes cannot be checked.
        abstract indexInfoToIndexSignatureDeclaration: indexInfo: IndexInfo * enclosingDeclaration: Node option * flags: NodeBuilderFlags option -> IndexSignatureDeclaration option
        /// Note that the resulting nodes cannot be checked.
        abstract symbolToEntityName: symbol: Symbol * meaning: SymbolFlags * enclosingDeclaration: Node option * flags: NodeBuilderFlags option -> EntityName option
        /// Note that the resulting nodes cannot be checked.
        abstract symbolToExpression: symbol: Symbol * meaning: SymbolFlags * enclosingDeclaration: Node option * flags: NodeBuilderFlags option -> Expression option
        /// Note that the resulting nodes cannot be checked.
        abstract symbolToTypeParameterDeclarations: symbol: Symbol * enclosingDeclaration: Node option * flags: NodeBuilderFlags option -> TypeParameterDeclaration[] option
        /// Note that the resulting nodes cannot be checked.
        abstract symbolToParameterDeclaration: symbol: Symbol * enclosingDeclaration: Node option * flags: NodeBuilderFlags option -> ParameterDeclaration option
        /// Note that the resulting nodes cannot be checked.
        abstract typeParameterToDeclaration: parameter: TypeParameter * enclosingDeclaration: Node option * flags: NodeBuilderFlags option -> TypeParameterDeclaration option
        abstract getSymbolsInScope: location: Node * meaning: SymbolFlags -> Symbol[]
        abstract getSymbolAtLocation: node: Node -> Symbol option
        abstract getSymbolsOfParameterPropertyDeclaration: parameter: ParameterDeclaration * parameterName: string -> Symbol[]
        /// The function returns the value (local variable) symbol of an identifier in the short-hand property assignment.
        /// This is necessary as an identifier in short-hand property assignment can contains two meaning: property name and property value.
        abstract getShorthandAssignmentValueSymbol: location: Node option -> Symbol option
        abstract getExportSpecifierLocalTargetSymbol: location: U2<ExportSpecifier, Identifier> -> Symbol option
        /// <summary>
        /// If a symbol is a local symbol with an associated exported symbol, returns the exported symbol.
        /// Otherwise returns its input.
        /// For example, at <c>export type T = number;</c>:
        ///     - <c>getSymbolAtLocation</c> at the location <c>T</c> will return the exported symbol for <c>T</c>.
        ///     - But the result of <c>getSymbolsInScope</c> will contain the *local* symbol for <c>T</c>, not the exported symbol.
        ///     - Calling <c>getExportSymbolOfSymbol</c> on that local symbol will return the exported symbol.
        /// </summary>
        abstract getExportSymbolOfSymbol: symbol: Symbol -> Symbol
        abstract getPropertySymbolOfDestructuringAssignment: location: Identifier -> Symbol option
        abstract getTypeOfAssignmentPattern: pattern: AssignmentPattern -> Type
        abstract getTypeAtLocation: node: Node -> Type
        abstract getTypeFromTypeNode: node: TypeNode -> Type
        abstract signatureToString: signature: Signature * ?enclosingDeclaration: Node * ?flags: TypeFormatFlags * ?kind: SignatureKind -> string
        abstract typeToString: ``type``: Type * ?enclosingDeclaration: Node * ?flags: TypeFormatFlags -> string
        abstract symbolToString: symbol: Symbol * ?enclosingDeclaration: Node * ?meaning: SymbolFlags * ?flags: SymbolFormatFlags -> string
        abstract typePredicateToString: predicate: TypePredicate * ?enclosingDeclaration: Node * ?flags: TypeFormatFlags -> string
        abstract getFullyQualifiedName: symbol: Symbol -> string
        abstract getAugmentedPropertiesOfType: ``type``: Type -> Symbol[]
        abstract getRootSymbols: symbol: Symbol -> Symbol[]
        abstract getSymbolOfExpando: node: Node * allowDeclaration: bool -> Symbol option
        abstract getContextualType: node: Expression -> Type option
        /// <summary>
        /// returns unknownSignature in the case of an error.
        /// returns undefined if the node is not valid.
        /// </summary>
        /// <param name="argumentCount">Apparent number of arguments, passed in case of a possibly incomplete call. This should come from an ArgumentListInfo. See <c>signatureHelp.ts</c>.</param>
        abstract getResolvedSignature: node: CallLikeExpression * ?candidatesOutArray: Signature[] * ?argumentCount: float -> Signature option
        abstract getSignatureFromDeclaration: declaration: SignatureDeclaration -> Signature option
        abstract isImplementationOfOverload: node: SignatureDeclaration -> bool option
        abstract isUndefinedSymbol: symbol: Symbol -> bool
        abstract isArgumentsSymbol: symbol: Symbol -> bool
        abstract isUnknownSymbol: symbol: Symbol -> bool
        abstract getConstantValue: node: U3<EnumMember, PropertyAccessExpression, ElementAccessExpression> -> U2<string, float> option
        abstract isValidPropertyAccess: node: U3<PropertyAccessExpression, QualifiedName, ImportTypeNode> * propertyName: string -> bool
        /// Follow all aliases to get the original symbol.
        abstract getAliasedSymbol: symbol: Symbol -> Symbol
        /// Follow a *single* alias to get the immediately aliased symbol.
        abstract getImmediateAliasedSymbol: symbol: Symbol -> Symbol option
        abstract getExportsOfModule: moduleSymbol: Symbol -> Symbol[]
        abstract getJsxIntrinsicTagNamesAt: location: Node -> Symbol[]
        abstract isOptionalParameter: node: ParameterDeclaration -> bool
        abstract getAmbientModules: unit -> Symbol[]
        abstract tryGetMemberInModuleExports: memberName: string * moduleSymbol: Symbol -> Symbol option
        abstract getApparentType: ``type``: Type -> Type
        abstract getBaseConstraintOfType: ``type``: Type -> Type option
        abstract getDefaultFromTypeParameter: ``type``: Type -> Type option
        /// <summary>
        /// Gets the intrinsic <c>any</c> type. There are multiple types that act as <c>any</c> used internally in the compiler,
        /// so the type returned by this function should not be used in equality checks to determine if another type
        /// is <c>any</c>. Instead, use <c>type.flags &amp; TypeFlags.Any</c>.
        /// </summary>
        abstract getAnyType: unit -> Type
        abstract getStringType: unit -> Type
        abstract getStringLiteralType: value: string -> StringLiteralType
        abstract getNumberType: unit -> Type
        abstract getNumberLiteralType: value: float -> NumberLiteralType
        abstract getBigIntType: unit -> Type
        abstract getBooleanType: unit -> Type
        abstract getFalseType: unit -> Type
        abstract getTrueType: unit -> Type
        abstract getVoidType: unit -> Type
        /// <summary>
        /// Gets the intrinsic <c>undefined</c> type. There are multiple types that act as <c>undefined</c> used internally in the compiler
        /// depending on compiler options, so the type returned by this function should not be used in equality checks to determine
        /// if another type is <c>undefined</c>. Instead, use <c>type.flags &amp; TypeFlags.Undefined</c>.
        /// </summary>
        abstract getUndefinedType: unit -> Type
        /// <summary>
        /// Gets the intrinsic <c>null</c> type. There are multiple types that act as <c>null</c> used internally in the compiler,
        /// so the type returned by this function should not be used in equality checks to determine if another type
        /// is <c>null</c>. Instead, use <c>type.flags &amp; TypeFlags.Null</c>.
        /// </summary>
        abstract getNullType: unit -> Type
        abstract getESSymbolType: unit -> Type
        /// <summary>
        /// Gets the intrinsic <c>never</c> type. There are multiple types that act as <c>never</c> used internally in the compiler,
        /// so the type returned by this function should not be used in equality checks to determine if another type
        /// is <c>never</c>. Instead, use <c>type.flags &amp; TypeFlags.Never</c>.
        /// </summary>
        abstract getNeverType: unit -> Type
        /// <summary>
        /// True if this type is the <c>Array</c> or <c>ReadonlyArray</c> type from lib.d.ts.
        /// This function will _not_ return true if passed a type which
        /// extends <c>Array</c> (for example, the TypeScript AST's <c>NodeArray</c> type).
        /// </summary>
        abstract isArrayType: ``type``: Type -> bool
        /// True if this type is a tuple type. This function will _not_ return true if
        /// passed a type which extends from a tuple.
        abstract isTupleType: ``type``: Type -> bool
        /// <summary>True if this type is assignable to <c>ReadonlyArray&lt;any&gt;</c>.</summary>
        abstract isArrayLikeType: ``type``: Type -> bool
        abstract getTypePredicateOfSignature: signature: Signature -> TypePredicate option
        /// Depending on the operation performed, it may be appropriate to throw away the checker
        /// if the cancellation token is triggered. Typically, if it is used for error checking
        /// and the operation is cancelled, then it should be discarded, otherwise it is safe to keep.
        abstract runWithCancellationToken: token: CancellationToken * cb: (TypeChecker -> 'T) -> 'T

    type [<RequireQualifiedAccess>] NodeBuilderFlags =
        | None = 0
        | NoTruncation = 1
        | WriteArrayAsGenericType = 2
        | GenerateNamesForShadowedTypeParams = 4
        | UseStructuralFallback = 8
        | ForbidIndexedAccessSymbolReferences = 16
        | WriteTypeArgumentsOfSignature = 32
        | UseFullyQualifiedType = 64
        | UseOnlyExternalAliasing = 128
        | SuppressAnyReturnType = 256
        | WriteTypeParametersInQualifiedName = 512
        | MultilineObjectLiterals = 1024
        | WriteClassExpressionAsTypeLiteral = 2048
        | UseTypeOfFunction = 4096
        | OmitParameterModifiers = 8192
        | UseAliasDefinedOutsideCurrentScope = 16384
        | UseSingleQuotesForStringLiteralType = 268435456
        | NoTypeReduction = 536870912
        | OmitThisParameter = 33554432
        | AllowThisInObjectLiteral = 32768
        | AllowQualifiedNameInPlaceOfIdentifier = 65536
        | AllowAnonymousIdentifier = 131072
        | AllowEmptyUnionOrIntersection = 262144
        | AllowEmptyTuple = 524288
        | AllowUniqueESSymbolType = 1048576
        | AllowEmptyIndexInfoType = 2097152
        | AllowNodeModulesRelativePaths = 67108864
        | IgnoreErrors = 70221824
        | InObjectTypeLiteral = 4194304
        | InTypeAlias = 8388608
        | InInitialEntityName = 16777216

    type [<RequireQualifiedAccess>] TypeFormatFlags =
        | None = 0
        | NoTruncation = 1
        | WriteArrayAsGenericType = 2
        | UseStructuralFallback = 8
        | WriteTypeArgumentsOfSignature = 32
        | UseFullyQualifiedType = 64
        | SuppressAnyReturnType = 256
        | MultilineObjectLiterals = 1024
        | WriteClassExpressionAsTypeLiteral = 2048
        | UseTypeOfFunction = 4096
        | OmitParameterModifiers = 8192
        | UseAliasDefinedOutsideCurrentScope = 16384
        | UseSingleQuotesForStringLiteralType = 268435456
        | NoTypeReduction = 536870912
        | OmitThisParameter = 33554432
        | AllowUniqueESSymbolType = 1048576
        | AddUndefined = 131072
        | WriteArrowStyleSignature = 262144
        | InArrayType = 524288
        | InElementType = 2097152
        | InFirstTypeArgument = 4194304
        | InTypeAlias = 8388608
        | NodeBuilderFlagsMask = 848330091

    type [<RequireQualifiedAccess>] SymbolFormatFlags =
        | None = 0
        | WriteTypeParametersOrArguments = 1
        | UseOnlyExternalAliasing = 2
        | AllowAnyNodeKind = 4
        | UseAliasDefinedOutsideCurrentScope = 8

    type [<RequireQualifiedAccess>] TypePredicateKind =
        | This = 0
        | Identifier = 1
        | AssertsThis = 2
        | AssertsIdentifier = 3

    type [<AllowNullLiteral>] TypePredicateBase =
        abstract kind: TypePredicateKind with get, set
        abstract ``type``: Type option with get, set

    type [<AllowNullLiteral>] ThisTypePredicate =
        inherit TypePredicateBase
        abstract kind: TypePredicateKind with get, set
        abstract parameterName: obj with get, set
        abstract parameterIndex: obj with get, set
        abstract ``type``: Type with get, set

    type [<AllowNullLiteral>] IdentifierTypePredicate =
        inherit TypePredicateBase
        abstract kind: TypePredicateKind with get, set
        abstract parameterName: string with get, set
        abstract parameterIndex: float with get, set
        abstract ``type``: Type with get, set

    type [<AllowNullLiteral>] AssertsThisTypePredicate =
        inherit TypePredicateBase
        abstract kind: TypePredicateKind with get, set
        abstract parameterName: obj with get, set
        abstract parameterIndex: obj with get, set
        abstract ``type``: Type option with get, set

    type [<AllowNullLiteral>] AssertsIdentifierTypePredicate =
        inherit TypePredicateBase
        abstract kind: TypePredicateKind with get, set
        abstract parameterName: string with get, set
        abstract parameterIndex: float with get, set
        abstract ``type``: Type option with get, set

    type TypePredicate =
        U4<ThisTypePredicate, IdentifierTypePredicate, AssertsThisTypePredicate, AssertsIdentifierTypePredicate>

    type [<RequireQualifiedAccess>] SymbolFlags =
        | None = 0
        | FunctionScopedVariable = 1
        | BlockScopedVariable = 2
        | Property = 4
        | EnumMember = 8
        | Function = 16
        | Class = 32
        | Interface = 64
        | ConstEnum = 128
        | RegularEnum = 256
        | ValueModule = 512
        | NamespaceModule = 1024
        | TypeLiteral = 2048
        | ObjectLiteral = 4096
        | Method = 8192
        | Constructor = 16384
        | GetAccessor = 32768
        | SetAccessor = 65536
        | Signature = 131072
        | TypeParameter = 262144
        | TypeAlias = 524288
        | ExportValue = 1048576
        | Alias = 2097152
        | Prototype = 4194304
        | ExportStar = 8388608
        | Optional = 16777216
        | Transient = 33554432
        | Assignment = 67108864
        | ModuleExports = 134217728
        | Enum = 384
        | Variable = 3
        | Value = 111551
        | Type = 788968
        | Namespace = 1920
        | Module = 1536
        | Accessor = 98304
        | FunctionScopedVariableExcludes = 111550
        | BlockScopedVariableExcludes = 111551
        | ParameterExcludes = 111551
        | PropertyExcludes = 0
        | EnumMemberExcludes = 900095
        | FunctionExcludes = 110991
        | ClassExcludes = 899503
        | InterfaceExcludes = 788872
        | RegularEnumExcludes = 899327
        | ConstEnumExcludes = 899967
        | ValueModuleExcludes = 110735
        | NamespaceModuleExcludes = 0
        | MethodExcludes = 103359
        | GetAccessorExcludes = 46015
        | SetAccessorExcludes = 78783
        | AccessorExcludes = 13247
        | TypeParameterExcludes = 526824
        | TypeAliasExcludes = 788968
        | AliasExcludes = 2097152
        | ModuleMember = 2623475
        | ExportHasLocal = 944
        | BlockScoped = 418
        | PropertyOrAccessor = 98308
        | ClassMember = 106500

    type [<AllowNullLiteral>] Symbol =
        abstract flags: SymbolFlags with get, set
        abstract escapedName: __String with get, set
        abstract declarations: Declaration[] option with get, set
        abstract valueDeclaration: Declaration option with get, set
        abstract members: SymbolTable option with get, set
        abstract exports: SymbolTable option with get, set
        abstract globalExports: SymbolTable option with get, set
        abstract name: string
        abstract getFlags: unit -> SymbolFlags
        abstract getEscapedName: unit -> __String
        abstract getName: unit -> string
        abstract getDeclarations: unit -> Declaration[] option
        abstract getDocumentationComment: typeChecker: TypeChecker option -> SymbolDisplayPart[]
        abstract getJsDocTags: ?checker: TypeChecker -> JSDocTagInfo[]

    type [<StringEnum>] [<RequireQualifiedAccess>] InternalSymbolName =
        | [<CompiledName("__call")>] Call
        | [<CompiledName("__constructor")>] Constructor
        | [<CompiledName("__new")>] New
        | [<CompiledName("__index")>] Index
        | [<CompiledName("__export")>] ExportStar
        | [<CompiledName("__global")>] Global
        | [<CompiledName("__missing")>] Missing
        | [<CompiledName("__type")>] Type
        | [<CompiledName("__object")>] Object
        | [<CompiledName("__jsxAttributes")>] JSXAttributes
        | [<CompiledName("__class")>] Class
        | [<CompiledName("__function")>] Function
        | [<CompiledName("__computed")>] Computed
        | [<CompiledName("__resolving__")>] Resolving
        | [<CompiledName("export=")>] ExportEquals
        | Default
        | This

    /// This represents a string whose leading underscore have been escaped by adding extra leading underscores.
    /// The shape of this brand is rather unique compared to others we've used.
    /// Instead of just an intersection of a string and an object, it is that union-ed
    /// with an intersection of void and an object. This makes it wholly incompatible
    /// with a normal string (which is good, it cannot be misused on assignment or on usage),
    /// while still being comparable with a normal string via === (also good) and castable from a string.
    type __String =
        U2<obj, InternalSymbolName>

    [<Obsolete("Use ReadonlyMap<__String, T> instead.")>]
    type ReadonlyUnderscoreEscapedMap<'T> =
        ReadonlyMap<__String, 'T>

    [<Obsolete("Use Map<__String, T> instead.")>]
    type UnderscoreEscapedMap<'T> =
        Map<__String, 'T>

    /// SymbolTable based on ES6 Map interface.
    type SymbolTable =
        Map<__String, Symbol>

    type [<RequireQualifiedAccess>] TypeFlags =
        | Any = 1
        | Unknown = 2
        | String = 4
        | Number = 8
        | Boolean = 16
        | Enum = 32
        | BigInt = 64
        | StringLiteral = 128
        | NumberLiteral = 256
        | BooleanLiteral = 512
        | EnumLiteral = 1024
        | BigIntLiteral = 2048
        | ESSymbol = 4096
        | UniqueESSymbol = 8192
        | Void = 16384
        | Undefined = 32768
        | Null = 65536
        | Never = 131072
        | TypeParameter = 262144
        | Object = 524288
        | Union = 1048576
        | Intersection = 2097152
        | Index = 4194304
        | IndexedAccess = 8388608
        | Conditional = 16777216
        | Substitution = 33554432
        | NonPrimitive = 67108864
        | TemplateLiteral = 134217728
        | StringMapping = 268435456
        | Literal = 2944
        | Unit = 109472
        | Freshable = 2976
        | StringOrNumberLiteral = 384
        | PossiblyFalsy = 117724
        | StringLike = 402653316
        | NumberLike = 296
        | BigIntLike = 2112
        | BooleanLike = 528
        | EnumLike = 1056
        | ESSymbolLike = 12288
        | VoidLike = 49152
        | UnionOrIntersection = 3145728
        | StructuredType = 3670016
        | TypeVariable = 8650752
        | InstantiableNonPrimitive = 58982400
        | InstantiablePrimitive = 406847488
        | Instantiable = 465829888
        | StructuredOrInstantiable = 469499904
        | Narrowable = 536624127

    type DestructuringPattern =
        U3<BindingPattern, ObjectLiteralExpression, ArrayLiteralExpression>

    type [<AllowNullLiteral>] Type =
        abstract flags: TypeFlags with get, set
        abstract symbol: Symbol with get, set
        abstract pattern: DestructuringPattern option with get, set
        abstract aliasSymbol: Symbol option with get, set
        abstract aliasTypeArguments: Type[] option with get, set
        abstract getFlags: unit -> TypeFlags
        abstract getSymbol: unit -> Symbol option
        abstract getProperties: unit -> Symbol[]
        abstract getProperty: propertyName: string -> Symbol option
        abstract getApparentProperties: unit -> Symbol[]
        abstract getCallSignatures: unit -> Signature[]
        abstract getConstructSignatures: unit -> Signature[]
        abstract getStringIndexType: unit -> Type option
        abstract getNumberIndexType: unit -> Type option
        abstract getBaseTypes: unit -> BaseType[] option
        abstract getNonNullableType: unit -> Type
        abstract getConstraint: unit -> Type option
        abstract getDefault: unit -> Type option
        abstract isUnion: unit -> bool
        abstract isIntersection: unit -> bool
        abstract isUnionOrIntersection: unit -> bool
        abstract isLiteral: unit -> bool
        abstract isStringLiteral: unit -> bool
        abstract isNumberLiteral: unit -> bool
        abstract isTypeParameter: unit -> bool
        abstract isClassOrInterface: unit -> bool
        abstract isClass: unit -> bool
        abstract isIndexType: unit -> bool

    type [<AllowNullLiteral>] FreshableType =
        inherit Type
        abstract freshType: FreshableType with get, set
        abstract regularType: FreshableType with get, set

    type [<AllowNullLiteral>] LiteralType =
        inherit FreshableType
        abstract value: U3<string, float, PseudoBigInt> with get, set

    type [<AllowNullLiteral>] UniqueESSymbolType =
        inherit Type
        abstract symbol: Symbol with get, set
        abstract escapedName: __String with get, set

    type [<AllowNullLiteral>] StringLiteralType =
        inherit LiteralType
        abstract value: string with get, set

    type [<AllowNullLiteral>] NumberLiteralType =
        inherit LiteralType
        abstract value: float with get, set

    type [<AllowNullLiteral>] BigIntLiteralType =
        inherit LiteralType
        abstract value: PseudoBigInt with get, set

    type [<AllowNullLiteral>] EnumType =
        inherit FreshableType

    type [<RequireQualifiedAccess>] ObjectFlags =
        | None = 0
        | Class = 1
        | Interface = 2
        | Reference = 4
        | Tuple = 8
        | Anonymous = 16
        | Mapped = 32
        | Instantiated = 64
        | ObjectLiteral = 128
        | EvolvingArray = 256
        | ObjectLiteralPatternWithComputedProperties = 512
        | ReverseMapped = 1024
        | JsxAttributes = 2048
        | JSLiteral = 4096
        | FreshLiteral = 8192
        | ArrayLiteral = 16384
        | ClassOrInterface = 3
        | ContainsSpread = 2097152
        | ObjectRestType = 4194304
        | InstantiationExpressionType = 8388608

    type [<AllowNullLiteral>] ObjectType =
        inherit Type
        abstract objectFlags: ObjectFlags with get, set

    /// Class and interface types (ObjectFlags.Class and ObjectFlags.Interface).
    type [<AllowNullLiteral>] InterfaceType =
        inherit ObjectType
        abstract typeParameters: TypeParameter[] option with get, set
        abstract outerTypeParameters: TypeParameter[] option with get, set
        abstract localTypeParameters: TypeParameter[] option with get, set
        abstract thisType: TypeParameter option with get, set

    type BaseType =
        U3<ObjectType, IntersectionType, TypeVariable>

    type [<AllowNullLiteral>] InterfaceTypeWithDeclaredMembers =
        inherit InterfaceType
        abstract declaredProperties: Symbol[] with get, set
        abstract declaredCallSignatures: Signature[] with get, set
        abstract declaredConstructSignatures: Signature[] with get, set
        abstract declaredIndexInfos: IndexInfo[] with get, set

    /// Type references (ObjectFlags.Reference). When a class or interface has type parameters or
    /// a "this" type, references to the class or interface are made using type references. The
    /// typeArguments property specifies the types to substitute for the type parameters of the
    /// class or interface and optionally includes an extra element that specifies the type to
    /// substitute for "this" in the resulting instantiation. When no extra argument is present,
    /// the type reference itself is substituted for "this". The typeArguments property is undefined
    /// if the class or interface has no type parameters and the reference isn't specifying an
    /// explicit "this" argument.
    type [<AllowNullLiteral>] TypeReference =
        inherit ObjectType
        abstract target: GenericType with get, set
        abstract node: U3<TypeReferenceNode, ArrayTypeNode, TupleTypeNode> option with get, set
        abstract typeArguments: Type[] option with get, set

    type [<AllowNullLiteral>] DeferredTypeReference =
        inherit TypeReference

    type [<AllowNullLiteral>] GenericType =
        inherit InterfaceType
        inherit TypeReference

    type [<RequireQualifiedAccess>] ElementFlags =
        | Required = 1
        | Optional = 2
        | Rest = 4
        | Variadic = 8
        | Fixed = 3
        | Variable = 12
        | NonRequired = 14
        | NonRest = 11

    type [<AllowNullLiteral>] TupleType =
        inherit GenericType
        abstract elementFlags: ElementFlags[] with get, set
        /// Number of required or variadic elements
        abstract minLength: float with get, set
        /// Number of initial required or optional elements
        abstract fixedLength: float with get, set
        /// True if tuple has any rest or variadic elements
        abstract hasRestElement: bool with get, set
        abstract combinedFlags: ElementFlags with get, set
        abstract readonly: bool with get, set
        abstract labeledElementDeclarations: U2<NamedTupleMember, ParameterDeclaration>[] option with get, set

    type [<AllowNullLiteral>] TupleTypeReference =
        inherit TypeReference
        abstract target: TupleType with get, set

    type [<AllowNullLiteral>] UnionOrIntersectionType =
        inherit Type
        abstract types: Type[] with get, set

    type [<AllowNullLiteral>] UnionType =
        inherit UnionOrIntersectionType

    type [<AllowNullLiteral>] IntersectionType =
        inherit UnionOrIntersectionType

    type StructuredType =
        U3<ObjectType, UnionType, IntersectionType>

    type [<AllowNullLiteral>] EvolvingArrayType =
        inherit ObjectType
        abstract elementType: Type with get, set
        abstract finalArrayType: Type option with get, set

    type [<AllowNullLiteral>] InstantiableType =
        inherit Type

    type [<AllowNullLiteral>] TypeParameter =
        inherit InstantiableType

    type [<AllowNullLiteral>] IndexedAccessType =
        inherit InstantiableType
        abstract objectType: Type with get, set
        abstract indexType: Type with get, set
        abstract ``constraint``: Type option with get, set
        abstract simplifiedForReading: Type option with get, set
        abstract simplifiedForWriting: Type option with get, set

    type TypeVariable =
        U2<TypeParameter, IndexedAccessType>

    type [<AllowNullLiteral>] IndexType =
        inherit InstantiableType
        abstract ``type``: U2<InstantiableType, UnionOrIntersectionType> with get, set

    type [<AllowNullLiteral>] ConditionalRoot =
        abstract node: ConditionalTypeNode with get, set
        abstract checkType: Type with get, set
        abstract extendsType: Type with get, set
        abstract isDistributive: bool with get, set
        abstract inferTypeParameters: TypeParameter[] option with get, set
        abstract outerTypeParameters: TypeParameter[] option with get, set
        abstract instantiations: Map<string, Type> option with get, set
        abstract aliasSymbol: Symbol option with get, set
        abstract aliasTypeArguments: Type[] option with get, set

    type [<AllowNullLiteral>] ConditionalType =
        inherit InstantiableType
        abstract root: ConditionalRoot with get, set
        abstract checkType: Type with get, set
        abstract extendsType: Type with get, set
        abstract resolvedTrueType: Type option with get, set
        abstract resolvedFalseType: Type option with get, set

    type [<AllowNullLiteral>] TemplateLiteralType =
        inherit InstantiableType
        abstract texts: string[] with get, set
        abstract types: Type[] with get, set

    type [<AllowNullLiteral>] StringMappingType =
        inherit InstantiableType
        abstract symbol: Symbol with get, set
        abstract ``type``: Type with get, set

    type [<AllowNullLiteral>] SubstitutionType =
        inherit InstantiableType
        abstract objectFlags: ObjectFlags with get, set
        abstract baseType: Type with get, set
        abstract ``constraint``: Type with get, set

    type [<RequireQualifiedAccess>] SignatureKind =
        | Call = 0
        | Construct = 1

    type [<AllowNullLiteral>] Signature =
        abstract declaration: U2<SignatureDeclaration, JSDocSignature> option with get, set
        abstract typeParameters: TypeParameter[] option with get, set
        abstract parameters: Symbol[] with get, set
        abstract getDeclaration: unit -> SignatureDeclaration
        abstract getTypeParameters: unit -> TypeParameter[] option
        abstract getParameters: unit -> Symbol[]
        abstract getTypeParameterAtPosition: pos: float -> Type
        abstract getReturnType: unit -> Type
        abstract getDocumentationComment: typeChecker: TypeChecker option -> SymbolDisplayPart[]
        abstract getJsDocTags: unit -> JSDocTagInfo[]

    type [<RequireQualifiedAccess>] IndexKind =
        | String = 0
        | Number = 1

    type [<AllowNullLiteral>] IndexInfo =
        abstract keyType: Type with get, set
        abstract ``type``: Type with get, set
        abstract isReadonly: bool with get, set
        abstract declaration: IndexSignatureDeclaration option with get, set

    type [<RequireQualifiedAccess>] InferencePriority =
        | None = 0
        | NakedTypeVariable = 1
        | SpeculativeTuple = 2
        | SubstituteSource = 4
        | HomomorphicMappedType = 8
        | PartialHomomorphicMappedType = 16
        | MappedTypeConstraint = 32
        | ContravariantConditional = 64
        | ReturnType = 128
        | LiteralKeyof = 256
        | NoConstraints = 512
        | AlwaysStrict = 1024
        | MaxValue = 2048
        | PriorityImpliesCombination = 416
        | Circularity = -1

    type [<AllowNullLiteral>] FileExtensionInfo =
        abstract extension: string with get, set
        abstract isMixedContent: bool with get, set
        abstract scriptKind: ScriptKind option with get, set

    type [<AllowNullLiteral>] DiagnosticMessage =
        abstract key: string with get, set
        abstract category: DiagnosticCategory with get, set
        abstract code: float with get, set
        abstract message: string with get, set
        abstract reportsUnnecessary: DiagnosticMessageReportsUnnecessary option with get, set
        abstract reportsDeprecated: DiagnosticMessageReportsUnnecessary option with get, set

    /// A linked list of formatted diagnostic messages to be used as part of a multiline message.
    /// It is built from the bottom up, leaving the head to be the "main" diagnostic.
    /// While it seems that DiagnosticMessageChain is structurally similar to DiagnosticMessage,
    /// the difference is that messages are all preformatted in DMC.
    type [<AllowNullLiteral>] DiagnosticMessageChain =
        abstract messageText: string with get, set
        abstract category: DiagnosticCategory with get, set
        abstract code: float with get, set
        abstract next: DiagnosticMessageChain[] option with get, set

    type [<AllowNullLiteral>] Diagnostic =
        inherit DiagnosticRelatedInformation
        /// <summary>May store more in future. For now, this will simply be <c>true</c> to indicate when a diagnostic is an unused-identifier diagnostic.</summary>
        abstract reportsUnnecessary: DiagnosticMessageReportsUnnecessary option with get, set
        abstract reportsDeprecated: DiagnosticMessageReportsUnnecessary option with get, set
        abstract source: string option with get, set
        abstract relatedInformation: DiagnosticRelatedInformation[] option with get, set

    type [<AllowNullLiteral>] DiagnosticRelatedInformation =
        abstract category: DiagnosticCategory with get, set
        abstract code: float with get, set
        abstract file: SourceFile option with get, set
        abstract start: float option with get, set
        abstract length: float option with get, set
        abstract messageText: U2<string, DiagnosticMessageChain> with get, set

    type [<AllowNullLiteral>] DiagnosticWithLocation =
        inherit Diagnostic
        abstract file: SourceFile with get, set
        abstract start: float with get, set
        abstract length: float with get, set

    type [<RequireQualifiedAccess>] DiagnosticCategory =
        | Warning = 0
        | Error = 1
        | Suggestion = 2
        | Message = 3

    type [<RequireQualifiedAccess>] ModuleResolutionKind =
        | Classic = 1
        /// <deprecated>
        /// <c>NodeJs</c> was renamed to <c>Node10</c> to better reflect the version of Node that it targets.
        /// Use the new name or consider switching to a modern module resolution target.
        /// </deprecated>
        | NodeJs = 2
        | Node10 = 2
        | Node16 = 3
        | NodeNext = 99
        | Bundler = 100

    type [<RequireQualifiedAccess>] ModuleDetectionKind =
        /// Files with imports, exports and/or import.meta are considered modules
        | Legacy = 1
        /// Legacy, but also files with jsx under react-jsx or react-jsxdev and esm mode files under moduleResolution: node16+
        | Auto = 2
        /// Consider all non-declaration files modules, regardless of present syntax
        | Force = 3

    type [<AllowNullLiteral>] PluginImport =
        abstract name: string with get, set

    type [<AllowNullLiteral>] ProjectReference =
        /// A normalized path on disk
        abstract path: string with get, set
        /// The path as the user originally wrote it
        abstract originalPath: string option with get, set
        /// True if the output of this reference should be prepended to the output of this project. Only valid for --outFile compilations
        abstract prepend: bool option with get, set
        /// True if it is intended that this reference form a circularity
        abstract circular: bool option with get, set

    type [<RequireQualifiedAccess>] WatchFileKind =
        | FixedPollingInterval = 0
        | PriorityPollingInterval = 1
        | DynamicPriorityPolling = 2
        | FixedChunkSizePolling = 3
        | UseFsEvents = 4
        | UseFsEventsOnParentDirectory = 5

    type [<RequireQualifiedAccess>] WatchDirectoryKind =
        | UseFsEvents = 0
        | FixedPollingInterval = 1
        | DynamicPriorityPolling = 2
        | FixedChunkSizePolling = 3

    type [<RequireQualifiedAccess>] PollingWatchKind =
        | FixedInterval = 0
        | PriorityInterval = 1
        | DynamicPriority = 2
        | FixedChunkSize = 3

    type CompilerOptionsValue =
        U8<string, float, bool, U2<string, float>[], string[], MapLike<string[]>, PluginImport[], ProjectReference[]> option

    type [<AllowNullLiteral>] CompilerOptions =
        abstract allowImportingTsExtensions: bool option with get, set
        abstract allowJs: bool option with get, set
        abstract allowArbitraryExtensions: bool option with get, set
        abstract allowSyntheticDefaultImports: bool option with get, set
        abstract allowUmdGlobalAccess: bool option with get, set
        abstract allowUnreachableCode: bool option with get, set
        abstract allowUnusedLabels: bool option with get, set
        abstract alwaysStrict: bool option with get, set
        abstract baseUrl: string option with get, set
        abstract charset: string option with get, set
        abstract checkJs: bool option with get, set
        abstract customConditions: string[] option with get, set
        abstract declaration: bool option with get, set
        abstract declarationMap: bool option with get, set
        abstract emitDeclarationOnly: bool option with get, set
        abstract declarationDir: string option with get, set
        abstract disableSizeLimit: bool option with get, set
        abstract disableSourceOfProjectReferenceRedirect: bool option with get, set
        abstract disableSolutionSearching: bool option with get, set
        abstract disableReferencedProjectLoad: bool option with get, set
        abstract downlevelIteration: bool option with get, set
        abstract emitBOM: bool option with get, set
        abstract emitDecoratorMetadata: bool option with get, set
        abstract exactOptionalPropertyTypes: bool option with get, set
        abstract experimentalDecorators: bool option with get, set
        abstract forceConsistentCasingInFileNames: bool option with get, set
        abstract ignoreDeprecations: string option with get, set
        abstract importHelpers: bool option with get, set
        abstract importsNotUsedAsValues: ImportsNotUsedAsValues option with get, set
        abstract inlineSourceMap: bool option with get, set
        abstract inlineSources: bool option with get, set
        abstract isolatedModules: bool option with get, set
        abstract jsx: JsxEmit option with get, set
        abstract keyofStringsOnly: bool option with get, set
        abstract lib: string[] option with get, set
        abstract locale: string option with get, set
        abstract mapRoot: string option with get, set
        abstract maxNodeModuleJsDepth: float option with get, set
        abstract ``module``: ModuleKind option with get, set
        abstract moduleResolution: ModuleResolutionKind option with get, set
        abstract moduleSuffixes: string[] option with get, set
        abstract moduleDetection: ModuleDetectionKind option with get, set
        abstract newLine: NewLineKind option with get, set
        abstract noEmit: bool option with get, set
        abstract noEmitHelpers: bool option with get, set
        abstract noEmitOnError: bool option with get, set
        abstract noErrorTruncation: bool option with get, set
        abstract noFallthroughCasesInSwitch: bool option with get, set
        abstract noImplicitAny: bool option with get, set
        abstract noImplicitReturns: bool option with get, set
        abstract noImplicitThis: bool option with get, set
        abstract noStrictGenericChecks: bool option with get, set
        abstract noUnusedLocals: bool option with get, set
        abstract noUnusedParameters: bool option with get, set
        abstract noImplicitUseStrict: bool option with get, set
        abstract noPropertyAccessFromIndexSignature: bool option with get, set
        abstract assumeChangesOnlyAffectDirectDependencies: bool option with get, set
        abstract noLib: bool option with get, set
        abstract noResolve: bool option with get, set
        abstract noUncheckedIndexedAccess: bool option with get, set
        abstract out: string option with get, set
        abstract outDir: string option with get, set
        abstract outFile: string option with get, set
        abstract paths: MapLike<string[]> option with get, set
        abstract preserveConstEnums: bool option with get, set
        abstract noImplicitOverride: bool option with get, set
        abstract preserveSymlinks: bool option with get, set
        abstract preserveValueImports: bool option with get, set
        abstract project: string option with get, set
        abstract reactNamespace: string option with get, set
        abstract jsxFactory: string option with get, set
        abstract jsxFragmentFactory: string option with get, set
        abstract jsxImportSource: string option with get, set
        abstract composite: bool option with get, set
        abstract incremental: bool option with get, set
        abstract tsBuildInfoFile: string option with get, set
        abstract removeComments: bool option with get, set
        abstract resolvePackageJsonExports: bool option with get, set
        abstract resolvePackageJsonImports: bool option with get, set
        abstract rootDir: string option with get, set
        abstract rootDirs: string[] option with get, set
        abstract skipLibCheck: bool option with get, set
        abstract skipDefaultLibCheck: bool option with get, set
        abstract sourceMap: bool option with get, set
        abstract sourceRoot: string option with get, set
        abstract strict: bool option with get, set
        abstract strictFunctionTypes: bool option with get, set
        abstract strictBindCallApply: bool option with get, set
        abstract strictNullChecks: bool option with get, set
        abstract strictPropertyInitialization: bool option with get, set
        abstract stripInternal: bool option with get, set
        abstract suppressExcessPropertyErrors: bool option with get, set
        abstract suppressImplicitAnyIndexErrors: bool option with get, set
        abstract target: ScriptTarget option with get, set
        abstract traceResolution: bool option with get, set
        abstract useUnknownInCatchVariables: bool option with get, set
        abstract resolveJsonModule: bool option with get, set
        abstract types: string[] option with get, set
        /// Paths used to compute primary types search locations
        abstract typeRoots: string[] option with get, set
        abstract verbatimModuleSyntax: bool option with get, set
        abstract esModuleInterop: bool option with get, set
        abstract useDefineForClassFields: bool option with get, set
        [<EmitIndexer>] abstract Item: option: string -> U2<CompilerOptionsValue, TsConfigSourceFile> option with get, set

    type [<AllowNullLiteral>] WatchOptions =
        abstract watchFile: WatchFileKind option with get, set
        abstract watchDirectory: WatchDirectoryKind option with get, set
        abstract fallbackPolling: PollingWatchKind option with get, set
        abstract synchronousWatchDirectory: bool option with get, set
        abstract excludeDirectories: string[] option with get, set
        abstract excludeFiles: string[] option with get, set
        [<EmitIndexer>] abstract Item: option: string -> CompilerOptionsValue option with get, set

    type [<AllowNullLiteral>] TypeAcquisition =
        abstract enable: bool option with get, set
        abstract ``include``: string[] option with get, set
        abstract exclude: string[] option with get, set
        abstract disableFilenameBasedTypeAcquisition: bool option with get, set
        [<EmitIndexer>] abstract Item: option: string -> CompilerOptionsValue option with get, set

    type [<RequireQualifiedAccess>] ModuleKind =
        | None = 0
        | CommonJS = 1
        | AMD = 2
        | UMD = 3
        | System = 4
        | ES2015 = 5
        | ES2020 = 6
        | ES2022 = 7
        | ESNext = 99
        | Node16 = 100
        | NodeNext = 199

    type [<RequireQualifiedAccess>] JsxEmit =
        | None = 0
        | Preserve = 1
        | React = 2
        | ReactNative = 3
        | ReactJSX = 4
        | ReactJSXDev = 5

    type [<RequireQualifiedAccess>] ImportsNotUsedAsValues =
        | Remove = 0
        | Preserve = 1
        | Error = 2

    type [<RequireQualifiedAccess>] NewLineKind =
        | CarriageReturnLineFeed = 0
        | LineFeed = 1

    type [<AllowNullLiteral>] LineAndCharacter =
        /// 0-based.
        abstract line: float with get, set
        abstract character: float with get, set

    type [<RequireQualifiedAccess>] ScriptKind =
        | Unknown = 0
        | JS = 1
        | JSX = 2
        | TS = 3
        | TSX = 4
        | External = 5
        | JSON = 6
        /// Used on extensions that doesn't define the ScriptKind but the content defines it.
        /// Deferred extensions are going to be included in all project contexts.
        | Deferred = 7

    type [<RequireQualifiedAccess>] ScriptTarget =
        | ES3 = 0
        | ES5 = 1
        | ES2015 = 2
        | ES2016 = 3
        | ES2017 = 4
        | ES2018 = 5
        | ES2019 = 6
        | ES2020 = 7
        | ES2021 = 8
        | ES2022 = 9
        | ESNext = 99
        | JSON = 100
        | Latest = 99

    type [<RequireQualifiedAccess>] LanguageVariant =
        | Standard = 0
        | JSX = 1

    /// Either a parsed command line or a parsed tsconfig.json
    type [<AllowNullLiteral>] ParsedCommandLine =
        abstract options: CompilerOptions with get, set
        abstract typeAcquisition: TypeAcquisition option with get, set
        abstract fileNames: string[] with get, set
        abstract projectReferences: ProjectReference[] option with get, set
        abstract watchOptions: WatchOptions option with get, set
        abstract raw: obj option with get, set
        abstract errors: Diagnostic[] with get, set
        abstract wildcardDirectories: MapLike<WatchDirectoryFlags> option with get, set
        abstract compileOnSave: bool option with get, set

    type [<RequireQualifiedAccess>] WatchDirectoryFlags =
        | None = 0
        | Recursive = 1

    type [<AllowNullLiteral>] CreateProgramOptions =
        abstract rootNames: string[] with get, set
        abstract options: CompilerOptions with get, set
        abstract projectReferences: ProjectReference[] option with get, set
        abstract host: CompilerHost option with get, set
        abstract oldProgram: Program option with get, set
        abstract configFileParsingDiagnostics: Diagnostic[] option with get, set

    type [<AllowNullLiteral>] ModuleResolutionHost =
        abstract fileExists: fileName: string -> bool
        abstract readFile: fileName: string -> string option
        abstract trace: s: string -> unit
        abstract directoryExists: directoryName: string -> bool
        /// <summary>Resolve a symbolic link.</summary>
        /// <seealso href="https://nodejs.org/api/fs.html#fs_fs_realpathsync_path_options" />
        abstract realpath: path: string -> string
        abstract getCurrentDirectory: unit -> string
        abstract getDirectories: path: string -> string[]
        abstract useCaseSensitiveFileNames: U2<bool, (unit -> bool)> option with get, set

    /// Used by services to specify the minimum host area required to set up source files under any compilation settings
    type [<AllowNullLiteral>] MinimalResolutionCacheHost =
        inherit ModuleResolutionHost
        abstract getCompilationSettings: unit -> CompilerOptions
        abstract getCompilerHost: unit -> CompilerHost option

    /// <summary>
    /// Represents the result of module resolution.
    /// Module resolution will pick up tsx/jsx/js files even if '--jsx' and '--allowJs' are turned off.
    /// The Program will then filter results based on these flags.
    /// 
    /// Prefer to return a <c>ResolvedModuleFull</c> so that the file type does not have to be inferred.
    /// </summary>
    type [<AllowNullLiteral>] ResolvedModule =
        /// Path of the file the module was resolved to.
        abstract resolvedFileName: string with get, set
        /// <summary>True if <c>resolvedFileName</c> comes from <c>node_modules</c>.</summary>
        abstract isExternalLibraryImport: bool option with get, set
        /// True if the original module reference used a .ts extension to refer directly to a .ts file,
        /// which should produce an error during checking if emit is enabled.
        abstract resolvedUsingTsExtension: bool option with get, set

    /// <summary>
    /// ResolvedModule with an explicitly provided <c>extension</c> property.
    /// Prefer this over <c>ResolvedModule</c>.
    /// If changing this, remember to change <c>moduleResolutionIsEqualTo</c>.
    /// </summary>
    type [<AllowNullLiteral>] ResolvedModuleFull =
        inherit ResolvedModule
        /// Extension of resolvedFileName. This must match what's at the end of resolvedFileName.
        /// This is optional for backwards-compatibility, but will be added if not provided.
        abstract extension: string with get, set
        abstract packageId: PackageId option with get, set

    /// <summary>
    /// Unique identifier with a package name and version.
    /// If changing this, remember to change <c>packageIdIsEqual</c>.
    /// </summary>
    type [<AllowNullLiteral>] PackageId =
        /// <summary>
        /// Name of the package.
        /// Should not include <c>@types</c>.
        /// If accessing a non-index file, this should include its name e.g. "foo/bar".
        /// </summary>
        abstract name: string with get, set
        /// Name of a submodule within this package.
        /// May be "".
        abstract subModuleName: string with get, set
        /// Version of the package, e.g. "1.2.3"
        abstract version: string with get, set

    type [<StringEnum>] [<RequireQualifiedAccess>] Extension =
        | [<CompiledName(".ts")>] Ts
        | [<CompiledName(".tsx")>] Tsx
        | [<CompiledName(".d.ts")>] Dts
        | [<CompiledName(".js")>] Js
        | [<CompiledName(".jsx")>] Jsx
        | [<CompiledName(".json")>] Json
        | [<CompiledName(".tsbuildinfo")>] TsBuildInfo
        | [<CompiledName(".mjs")>] Mjs
        | [<CompiledName(".mts")>] Mts
        | [<CompiledName(".d.mts")>] Dmts
        | [<CompiledName(".cjs")>] Cjs
        | [<CompiledName(".cts")>] Cts
        | [<CompiledName(".d.cts")>] Dcts

    type [<AllowNullLiteral>] ResolvedModuleWithFailedLookupLocations =
        abstract resolvedModule: ResolvedModuleFull option

    type [<AllowNullLiteral>] ResolvedTypeReferenceDirective =
        abstract primary: bool with get, set
        abstract resolvedFileName: string option with get, set
        abstract packageId: PackageId option with get, set
        /// <summary>True if <c>resolvedFileName</c> comes from <c>node_modules</c>.</summary>
        abstract isExternalLibraryImport: bool option with get, set

    type [<AllowNullLiteral>] ResolvedTypeReferenceDirectiveWithFailedLookupLocations =
        abstract resolvedTypeReferenceDirective: ResolvedTypeReferenceDirective option

    type [<AllowNullLiteral>] CompilerHost =
        inherit ModuleResolutionHost
        abstract getSourceFile: fileName: string * languageVersionOrOptions: U2<ScriptTarget, CreateSourceFileOptions> * ?onError: (string -> unit) * ?shouldCreateNewSourceFile: bool -> SourceFile option
        abstract getSourceFileByPath: fileName: string * path: Path * languageVersionOrOptions: U2<ScriptTarget, CreateSourceFileOptions> * ?onError: (string -> unit) * ?shouldCreateNewSourceFile: bool -> SourceFile option
        abstract getCancellationToken: unit -> CancellationToken
        abstract getDefaultLibFileName: options: CompilerOptions -> string
        abstract getDefaultLibLocation: unit -> string
        abstract writeFile: WriteFileCallback with get, set
        abstract getCurrentDirectory: unit -> string
        abstract getCanonicalFileName: fileName: string -> string
        abstract useCaseSensitiveFileNames: unit -> bool
        abstract getNewLine: unit -> string
        abstract readDirectory: rootDir: string * extensions: string[] * excludes: string[] option * includes: string[] * ?depth: float -> string[]
        /// <summary>Returns the module resolution cache used by a provided <c>resolveModuleNames</c> implementation so that any non-name module resolution operations (eg, package.json lookup) can reuse it</summary>
        abstract getModuleResolutionCache: unit -> ModuleResolutionCache option
        abstract resolveModuleNameLiterals: moduleLiterals: StringLiteralLike[] * containingFile: string * redirectedReference: ResolvedProjectReference option * options: CompilerOptions * containingSourceFile: SourceFile * reusedNames: StringLiteralLike[] option -> ResolvedModuleWithFailedLookupLocations[]
        abstract resolveTypeReferenceDirectiveReferences: typeDirectiveReferences: 'T[] * containingFile: string * redirectedReference: ResolvedProjectReference option * options: CompilerOptions * containingSourceFile: SourceFile option * reusedNames: 'T[] option -> ResolvedTypeReferenceDirectiveWithFailedLookupLocations[]
        abstract getEnvironmentVariable: name: string -> string option
        /// If provided along with custom resolveModuleNames or resolveTypeReferenceDirectives, used to determine if unchanged file path needs to re-resolve modules/type reference directives
        abstract hasInvalidatedResolutions: filePath: Path -> bool
        abstract createHash: data: string -> string
        abstract getParsedCommandLine: fileName: string -> ParsedCommandLine option

    type [<AllowNullLiteral>] SourceMapRange =
        inherit TextRange
        abstract source: SourceMapSource option with get, set

    type [<AllowNullLiteral>] SourceMapSource =
        abstract fileName: string with get, set
        abstract text: string with get, set
        abstract skipTrivia: pos: float -> float
        abstract getLineAndCharacterOfPosition: pos: float -> LineAndCharacter

    type [<RequireQualifiedAccess>] EmitFlags =
        | None = 0
        | SingleLine = 1
        | MultiLine = 2
        | AdviseOnEmitNode = 4
        | NoSubstitution = 8
        | CapturesThis = 16
        | NoLeadingSourceMap = 32
        | NoTrailingSourceMap = 64
        | NoSourceMap = 96
        | NoNestedSourceMaps = 128
        | NoTokenLeadingSourceMaps = 256
        | NoTokenTrailingSourceMaps = 512
        | NoTokenSourceMaps = 768
        | NoLeadingComments = 1024
        | NoTrailingComments = 2048
        | NoComments = 3072
        | NoNestedComments = 4096
        | HelperName = 8192
        | ExportName = 16384
        | LocalName = 32768
        | InternalName = 65536
        | Indented = 131072
        | NoIndentation = 262144
        | AsyncFunctionBody = 524288
        | ReuseTempVariableScope = 1048576
        | CustomPrologue = 2097152
        | NoHoisting = 4194304
        | Iterator = 8388608
        | NoAsciiEscaping = 16777216

    type [<AllowNullLiteral>] EmitHelperBase =
        abstract name: string
        abstract scoped: bool
        abstract text: U2<string, (EmitHelperUniqueNameCallback -> string)>
        abstract priority: float option
        abstract dependencies: EmitHelper[] option

    type [<AllowNullLiteral>] ScopedEmitHelper =
        inherit EmitHelperBase
        abstract scoped: bool

    type [<AllowNullLiteral>] UnscopedEmitHelper =
        inherit EmitHelperBase
        abstract scoped: bool
        abstract text: string

    type EmitHelper =
        U2<ScopedEmitHelper, UnscopedEmitHelper>

    type [<AllowNullLiteral>] EmitHelperUniqueNameCallback =
        [<Emit("$0($1...)")>] abstract Invoke: name: string -> string

    type [<RequireQualifiedAccess>] EmitHint =
        | SourceFile = 0
        | Expression = 1
        | IdentifierName = 2
        | MappedTypeParameter = 3
        | Unspecified = 4
        | EmbeddedStatement = 5
        | JsxAttributeValue = 6

    type [<RequireQualifiedAccess>] OuterExpressionKinds =
        | Parentheses = 1
        | TypeAssertions = 2
        | NonNullAssertions = 4
        | PartiallyEmittedExpressions = 8
        | Assertions = 6
        | All = 15
        | ExcludeJSDocTypeAssertion = 16

    type [<StringEnum>] [<RequireQualifiedAccess>] TypeOfTag =
        | Undefined
        | Number
        | Bigint
        | Boolean
        | String
        | Symbol
        | Object
        | Function

    type [<AllowNullLiteral>] NodeFactory =
        abstract createNodeArray: ?elements: 'T[] * ?hasTrailingComma: bool -> 'T[] when 'T :> Node
        abstract createNumericLiteral: value: U2<string, float> * ?numericLiteralFlags: TokenFlags -> NumericLiteral
        abstract createBigIntLiteral: value: U2<string, PseudoBigInt> -> BigIntLiteral
        abstract createStringLiteral: text: string * ?isSingleQuote: bool -> StringLiteral
        abstract createStringLiteralFromNode: sourceNode: U2<PropertyNameLiteral, PrivateIdentifier> * ?isSingleQuote: bool -> StringLiteral
        abstract createRegularExpressionLiteral: text: string -> RegularExpressionLiteral
        abstract createIdentifier: text: string -> Identifier
        /// <summary>Create a unique temporary variable.</summary>
        /// <param name="recordTempVariable">
        /// An optional callback used to record the temporary variable name. This
        /// should usually be a reference to <c>hoistVariableDeclaration</c> from a <c>TransformationContext</c>, but
        /// can be <c>undefined</c> if you plan to record the temporary variable manually.
        /// </param>
        /// <param name="reservedInNestedScopes">
        /// When <c>true</c>, reserves the temporary variable name in all nested scopes
        /// during emit so that the variable can be referenced in a nested function body. This is an alternative to
        /// setting <c>EmitFlags.ReuseTempVariableScope</c> on the nested function itself.
        /// </param>
        abstract createTempVariable: recordTempVariable: (Identifier -> unit) option * ?reservedInNestedScopes: bool -> Identifier
        /// <summary>Create a unique temporary variable for use in a loop.</summary>
        /// <param name="reservedInNestedScopes">
        /// When <c>true</c>, reserves the temporary variable name in all nested scopes
        /// during emit so that the variable can be referenced in a nested function body. This is an alternative to
        /// setting <c>EmitFlags.ReuseTempVariableScope</c> on the nested function itself.
        /// </param>
        abstract createLoopVariable: ?reservedInNestedScopes: bool -> Identifier
        /// Create a unique name based on the supplied text.
        abstract createUniqueName: text: string * ?flags: GeneratedIdentifierFlags -> Identifier
        /// Create a unique name generated for a node.
        abstract getGeneratedNameForNode: node: Node option * ?flags: GeneratedIdentifierFlags -> Identifier
        abstract createPrivateIdentifier: text: string -> PrivateIdentifier
        abstract createUniquePrivateName: ?text: string -> PrivateIdentifier
        abstract getGeneratedPrivateNameForNode: node: Node -> PrivateIdentifier
        abstract createToken: token: SyntaxKind -> SuperExpression
        abstract createToken: token: 'TKind -> PunctuationToken<'TKind>
        abstract createSuper: unit -> SuperExpression
        abstract createThis: unit -> ThisExpression
        abstract createNull: unit -> NullLiteral
        abstract createTrue: unit -> TrueLiteral
        abstract createFalse: unit -> FalseLiteral
        abstract createModifier: kind: 'T -> ModifierToken<'T>
        abstract createModifiersFromModifierFlags: flags: ModifierFlags -> Modifier[] option
        abstract createQualifiedName: left: EntityName * right: U2<string, Identifier> -> QualifiedName
        abstract updateQualifiedName: node: QualifiedName * left: EntityName * right: Identifier -> QualifiedName
        abstract createComputedPropertyName: expression: Expression -> ComputedPropertyName
        abstract updateComputedPropertyName: node: ComputedPropertyName * expression: Expression -> ComputedPropertyName
        abstract createTypeParameterDeclaration: modifiers: Modifier[] option * name: U2<string, Identifier> * ?``constraint``: TypeNode * ?defaultType: TypeNode -> TypeParameterDeclaration
        abstract updateTypeParameterDeclaration: node: TypeParameterDeclaration * modifiers: Modifier[] option * name: Identifier * ``constraint``: TypeNode option * defaultType: TypeNode option -> TypeParameterDeclaration
        abstract createParameterDeclaration: modifiers: ModifierLike[] option * dotDotDotToken: DotDotDotToken option * name: U2<string, BindingName> * ?questionToken: QuestionToken * ?``type``: TypeNode * ?initializer: Expression -> ParameterDeclaration
        abstract updateParameterDeclaration: node: ParameterDeclaration * modifiers: ModifierLike[] option * dotDotDotToken: DotDotDotToken option * name: U2<string, BindingName> * questionToken: QuestionToken option * ``type``: TypeNode option * initializer: Expression option -> ParameterDeclaration
        abstract createDecorator: expression: Expression -> Decorator
        abstract updateDecorator: node: Decorator * expression: Expression -> Decorator
        abstract createPropertySignature: modifiers: Modifier[] option * name: U2<PropertyName, string> * questionToken: QuestionToken option * ``type``: TypeNode option -> PropertySignature
        abstract updatePropertySignature: node: PropertySignature * modifiers: Modifier[] option * name: PropertyName * questionToken: QuestionToken option * ``type``: TypeNode option -> PropertySignature
        abstract createPropertyDeclaration: modifiers: ModifierLike[] option * name: U2<string, PropertyName> * questionOrExclamationToken: U2<QuestionToken, ExclamationToken> option * ``type``: TypeNode option * initializer: Expression option -> PropertyDeclaration
        abstract updatePropertyDeclaration: node: PropertyDeclaration * modifiers: ModifierLike[] option * name: U2<string, PropertyName> * questionOrExclamationToken: U2<QuestionToken, ExclamationToken> option * ``type``: TypeNode option * initializer: Expression option -> PropertyDeclaration
        abstract createMethodSignature: modifiers: Modifier[] option * name: U2<string, PropertyName> * questionToken: QuestionToken option * typeParameters: TypeParameterDeclaration[] option * parameters: ParameterDeclaration[] * ``type``: TypeNode option -> MethodSignature
        abstract updateMethodSignature: node: MethodSignature * modifiers: Modifier[] option * name: PropertyName * questionToken: QuestionToken option * typeParameters: TypeParameterDeclaration[] option * parameters: ParameterDeclaration[] * ``type``: TypeNode option -> MethodSignature
        abstract createMethodDeclaration: modifiers: ModifierLike[] option * asteriskToken: AsteriskToken option * name: U2<string, PropertyName> * questionToken: QuestionToken option * typeParameters: TypeParameterDeclaration[] option * parameters: ParameterDeclaration[] * ``type``: TypeNode option * body: Block option -> MethodDeclaration
        abstract updateMethodDeclaration: node: MethodDeclaration * modifiers: ModifierLike[] option * asteriskToken: AsteriskToken option * name: PropertyName * questionToken: QuestionToken option * typeParameters: TypeParameterDeclaration[] option * parameters: ParameterDeclaration[] * ``type``: TypeNode option * body: Block option -> MethodDeclaration
        abstract createConstructorDeclaration: modifiers: ModifierLike[] option * parameters: ParameterDeclaration[] * body: Block option -> ConstructorDeclaration
        abstract updateConstructorDeclaration: node: ConstructorDeclaration * modifiers: ModifierLike[] option * parameters: ParameterDeclaration[] * body: Block option -> ConstructorDeclaration
        abstract createGetAccessorDeclaration: modifiers: ModifierLike[] option * name: U2<string, PropertyName> * parameters: ParameterDeclaration[] * ``type``: TypeNode option * body: Block option -> GetAccessorDeclaration
        abstract updateGetAccessorDeclaration: node: GetAccessorDeclaration * modifiers: ModifierLike[] option * name: PropertyName * parameters: ParameterDeclaration[] * ``type``: TypeNode option * body: Block option -> GetAccessorDeclaration
        abstract createSetAccessorDeclaration: modifiers: ModifierLike[] option * name: U2<string, PropertyName> * parameters: ParameterDeclaration[] * body: Block option -> SetAccessorDeclaration
        abstract updateSetAccessorDeclaration: node: SetAccessorDeclaration * modifiers: ModifierLike[] option * name: PropertyName * parameters: ParameterDeclaration[] * body: Block option -> SetAccessorDeclaration
        abstract createCallSignature: typeParameters: TypeParameterDeclaration[] option * parameters: ParameterDeclaration[] * ``type``: TypeNode option -> CallSignatureDeclaration
        abstract updateCallSignature: node: CallSignatureDeclaration * typeParameters: TypeParameterDeclaration[] option * parameters: ParameterDeclaration[] * ``type``: TypeNode option -> CallSignatureDeclaration
        abstract createConstructSignature: typeParameters: TypeParameterDeclaration[] option * parameters: ParameterDeclaration[] * ``type``: TypeNode option -> ConstructSignatureDeclaration
        abstract updateConstructSignature: node: ConstructSignatureDeclaration * typeParameters: TypeParameterDeclaration[] option * parameters: ParameterDeclaration[] * ``type``: TypeNode option -> ConstructSignatureDeclaration
        abstract createIndexSignature: modifiers: ModifierLike[] option * parameters: ParameterDeclaration[] * ``type``: TypeNode -> IndexSignatureDeclaration
        abstract updateIndexSignature: node: IndexSignatureDeclaration * modifiers: ModifierLike[] option * parameters: ParameterDeclaration[] * ``type``: TypeNode -> IndexSignatureDeclaration
        abstract createTemplateLiteralTypeSpan: ``type``: TypeNode * literal: U2<TemplateMiddle, TemplateTail> -> TemplateLiteralTypeSpan
        abstract updateTemplateLiteralTypeSpan: node: TemplateLiteralTypeSpan * ``type``: TypeNode * literal: U2<TemplateMiddle, TemplateTail> -> TemplateLiteralTypeSpan
        abstract createClassStaticBlockDeclaration: body: Block -> ClassStaticBlockDeclaration
        abstract updateClassStaticBlockDeclaration: node: ClassStaticBlockDeclaration * body: Block -> ClassStaticBlockDeclaration
        abstract createKeywordTypeNode: kind: 'TKind -> KeywordTypeNode<'TKind>
        abstract createTypePredicateNode: assertsModifier: AssertsKeyword option * parameterName: U3<Identifier, ThisTypeNode, string> * ``type``: TypeNode option -> TypePredicateNode
        abstract updateTypePredicateNode: node: TypePredicateNode * assertsModifier: AssertsKeyword option * parameterName: U2<Identifier, ThisTypeNode> * ``type``: TypeNode option -> TypePredicateNode
        abstract createTypeReferenceNode: typeName: U2<string, EntityName> * ?typeArguments: TypeNode[] -> TypeReferenceNode
        abstract updateTypeReferenceNode: node: TypeReferenceNode * typeName: EntityName * typeArguments: TypeNode[] option -> TypeReferenceNode
        abstract createFunctionTypeNode: typeParameters: TypeParameterDeclaration[] option * parameters: ParameterDeclaration[] * ``type``: TypeNode -> FunctionTypeNode
        abstract updateFunctionTypeNode: node: FunctionTypeNode * typeParameters: TypeParameterDeclaration[] option * parameters: ParameterDeclaration[] * ``type``: TypeNode -> FunctionTypeNode
        abstract createConstructorTypeNode: modifiers: Modifier[] option * typeParameters: TypeParameterDeclaration[] option * parameters: ParameterDeclaration[] * ``type``: TypeNode -> ConstructorTypeNode
        abstract updateConstructorTypeNode: node: ConstructorTypeNode * modifiers: Modifier[] option * typeParameters: TypeParameterDeclaration[] option * parameters: ParameterDeclaration[] * ``type``: TypeNode -> ConstructorTypeNode
        abstract createTypeQueryNode: exprName: EntityName * ?typeArguments: TypeNode[] -> TypeQueryNode
        abstract updateTypeQueryNode: node: TypeQueryNode * exprName: EntityName * ?typeArguments: TypeNode[] -> TypeQueryNode
        abstract createTypeLiteralNode: members: TypeElement[] option -> TypeLiteralNode
        abstract updateTypeLiteralNode: node: TypeLiteralNode * members: TypeElement[] -> TypeLiteralNode
        abstract createArrayTypeNode: elementType: TypeNode -> ArrayTypeNode
        abstract updateArrayTypeNode: node: ArrayTypeNode * elementType: TypeNode -> ArrayTypeNode
        abstract createTupleTypeNode: elements: U2<TypeNode, NamedTupleMember>[] -> TupleTypeNode
        abstract updateTupleTypeNode: node: TupleTypeNode * elements: U2<TypeNode, NamedTupleMember>[] -> TupleTypeNode
        abstract createNamedTupleMember: dotDotDotToken: DotDotDotToken option * name: Identifier * questionToken: QuestionToken option * ``type``: TypeNode -> NamedTupleMember
        abstract updateNamedTupleMember: node: NamedTupleMember * dotDotDotToken: DotDotDotToken option * name: Identifier * questionToken: QuestionToken option * ``type``: TypeNode -> NamedTupleMember
        abstract createOptionalTypeNode: ``type``: TypeNode -> OptionalTypeNode
        abstract updateOptionalTypeNode: node: OptionalTypeNode * ``type``: TypeNode -> OptionalTypeNode
        abstract createRestTypeNode: ``type``: TypeNode -> RestTypeNode
        abstract updateRestTypeNode: node: RestTypeNode * ``type``: TypeNode -> RestTypeNode
        abstract createUnionTypeNode: types: TypeNode[] -> UnionTypeNode
        abstract updateUnionTypeNode: node: UnionTypeNode * types: TypeNode[] -> UnionTypeNode
        abstract createIntersectionTypeNode: types: TypeNode[] -> IntersectionTypeNode
        abstract updateIntersectionTypeNode: node: IntersectionTypeNode * types: TypeNode[] -> IntersectionTypeNode
        abstract createConditionalTypeNode: checkType: TypeNode * extendsType: TypeNode * trueType: TypeNode * falseType: TypeNode -> ConditionalTypeNode
        abstract updateConditionalTypeNode: node: ConditionalTypeNode * checkType: TypeNode * extendsType: TypeNode * trueType: TypeNode * falseType: TypeNode -> ConditionalTypeNode
        abstract createInferTypeNode: typeParameter: TypeParameterDeclaration -> InferTypeNode
        abstract updateInferTypeNode: node: InferTypeNode * typeParameter: TypeParameterDeclaration -> InferTypeNode
        abstract createImportTypeNode: argument: TypeNode * ?assertions: ImportTypeAssertionContainer * ?qualifier: EntityName * ?typeArguments: TypeNode[] * ?isTypeOf: bool -> ImportTypeNode
        abstract updateImportTypeNode: node: ImportTypeNode * argument: TypeNode * assertions: ImportTypeAssertionContainer option * qualifier: EntityName option * typeArguments: TypeNode[] option * ?isTypeOf: bool -> ImportTypeNode
        abstract createParenthesizedType: ``type``: TypeNode -> ParenthesizedTypeNode
        abstract updateParenthesizedType: node: ParenthesizedTypeNode * ``type``: TypeNode -> ParenthesizedTypeNode
        abstract createThisTypeNode: unit -> ThisTypeNode
        abstract createTypeOperatorNode: operator: SyntaxKind * ``type``: TypeNode -> TypeOperatorNode
        abstract updateTypeOperatorNode: node: TypeOperatorNode * ``type``: TypeNode -> TypeOperatorNode
        abstract createIndexedAccessTypeNode: objectType: TypeNode * indexType: TypeNode -> IndexedAccessTypeNode
        abstract updateIndexedAccessTypeNode: node: IndexedAccessTypeNode * objectType: TypeNode * indexType: TypeNode -> IndexedAccessTypeNode
        abstract createMappedTypeNode: readonlyToken: U3<ReadonlyKeyword, PlusToken, MinusToken> option * typeParameter: TypeParameterDeclaration * nameType: TypeNode option * questionToken: U3<QuestionToken, PlusToken, MinusToken> option * ``type``: TypeNode option * members: TypeElement[] option -> MappedTypeNode
        abstract updateMappedTypeNode: node: MappedTypeNode * readonlyToken: U3<ReadonlyKeyword, PlusToken, MinusToken> option * typeParameter: TypeParameterDeclaration * nameType: TypeNode option * questionToken: U3<QuestionToken, PlusToken, MinusToken> option * ``type``: TypeNode option * members: TypeElement[] option -> MappedTypeNode
        abstract createLiteralTypeNode: literal: obj -> LiteralTypeNode
        abstract updateLiteralTypeNode: node: LiteralTypeNode * literal: obj -> LiteralTypeNode
        abstract createTemplateLiteralType: head: TemplateHead * templateSpans: TemplateLiteralTypeSpan[] -> TemplateLiteralTypeNode
        abstract updateTemplateLiteralType: node: TemplateLiteralTypeNode * head: TemplateHead * templateSpans: TemplateLiteralTypeSpan[] -> TemplateLiteralTypeNode
        abstract createObjectBindingPattern: elements: BindingElement[] -> ObjectBindingPattern
        abstract updateObjectBindingPattern: node: ObjectBindingPattern * elements: BindingElement[] -> ObjectBindingPattern
        abstract createArrayBindingPattern: elements: ArrayBindingElement[] -> ArrayBindingPattern
        abstract updateArrayBindingPattern: node: ArrayBindingPattern * elements: ArrayBindingElement[] -> ArrayBindingPattern
        abstract createBindingElement: dotDotDotToken: DotDotDotToken option * propertyName: U2<string, PropertyName> option * name: U2<string, BindingName> * ?initializer: Expression -> BindingElement
        abstract updateBindingElement: node: BindingElement * dotDotDotToken: DotDotDotToken option * propertyName: PropertyName option * name: BindingName * initializer: Expression option -> BindingElement
        abstract createArrayLiteralExpression: ?elements: Expression[] * ?multiLine: bool -> ArrayLiteralExpression
        abstract updateArrayLiteralExpression: node: ArrayLiteralExpression * elements: Expression[] -> ArrayLiteralExpression
        abstract createObjectLiteralExpression: ?properties: ObjectLiteralElementLike[] * ?multiLine: bool -> ObjectLiteralExpression
        abstract updateObjectLiteralExpression: node: ObjectLiteralExpression * properties: ObjectLiteralElementLike[] -> ObjectLiteralExpression
        abstract createPropertyAccessExpression: expression: Expression * name: U2<string, MemberName> -> PropertyAccessExpression
        abstract updatePropertyAccessExpression: node: PropertyAccessExpression * expression: Expression * name: MemberName -> PropertyAccessExpression
        abstract createPropertyAccessChain: expression: Expression * questionDotToken: QuestionDotToken option * name: U2<string, MemberName> -> PropertyAccessChain
        abstract updatePropertyAccessChain: node: PropertyAccessChain * expression: Expression * questionDotToken: QuestionDotToken option * name: MemberName -> PropertyAccessChain
        abstract createElementAccessExpression: expression: Expression * index: U2<float, Expression> -> ElementAccessExpression
        abstract updateElementAccessExpression: node: ElementAccessExpression * expression: Expression * argumentExpression: Expression -> ElementAccessExpression
        abstract createElementAccessChain: expression: Expression * questionDotToken: QuestionDotToken option * index: U2<float, Expression> -> ElementAccessChain
        abstract updateElementAccessChain: node: ElementAccessChain * expression: Expression * questionDotToken: QuestionDotToken option * argumentExpression: Expression -> ElementAccessChain
        abstract createCallExpression: expression: Expression * typeArguments: TypeNode[] option * argumentsArray: Expression[] option -> CallExpression
        abstract updateCallExpression: node: CallExpression * expression: Expression * typeArguments: TypeNode[] option * argumentsArray: Expression[] -> CallExpression
        abstract createCallChain: expression: Expression * questionDotToken: QuestionDotToken option * typeArguments: TypeNode[] option * argumentsArray: Expression[] option -> CallChain
        abstract updateCallChain: node: CallChain * expression: Expression * questionDotToken: QuestionDotToken option * typeArguments: TypeNode[] option * argumentsArray: Expression[] -> CallChain
        abstract createNewExpression: expression: Expression * typeArguments: TypeNode[] option * argumentsArray: Expression[] option -> NewExpression
        abstract updateNewExpression: node: NewExpression * expression: Expression * typeArguments: TypeNode[] option * argumentsArray: Expression[] option -> NewExpression
        abstract createTaggedTemplateExpression: tag: Expression * typeArguments: TypeNode[] option * template: TemplateLiteral -> TaggedTemplateExpression
        abstract updateTaggedTemplateExpression: node: TaggedTemplateExpression * tag: Expression * typeArguments: TypeNode[] option * template: TemplateLiteral -> TaggedTemplateExpression
        abstract createTypeAssertion: ``type``: TypeNode * expression: Expression -> TypeAssertion
        abstract updateTypeAssertion: node: TypeAssertion * ``type``: TypeNode * expression: Expression -> TypeAssertion
        abstract createParenthesizedExpression: expression: Expression -> ParenthesizedExpression
        abstract updateParenthesizedExpression: node: ParenthesizedExpression * expression: Expression -> ParenthesizedExpression
        abstract createFunctionExpression: modifiers: Modifier[] option * asteriskToken: AsteriskToken option * name: U2<string, Identifier> option * typeParameters: TypeParameterDeclaration[] option * parameters: ParameterDeclaration[] option * ``type``: TypeNode option * body: Block -> FunctionExpression
        abstract updateFunctionExpression: node: FunctionExpression * modifiers: Modifier[] option * asteriskToken: AsteriskToken option * name: Identifier option * typeParameters: TypeParameterDeclaration[] option * parameters: ParameterDeclaration[] * ``type``: TypeNode option * body: Block -> FunctionExpression
        abstract createArrowFunction: modifiers: Modifier[] option * typeParameters: TypeParameterDeclaration[] option * parameters: ParameterDeclaration[] * ``type``: TypeNode option * equalsGreaterThanToken: EqualsGreaterThanToken option * body: ConciseBody -> ArrowFunction
        abstract updateArrowFunction: node: ArrowFunction * modifiers: Modifier[] option * typeParameters: TypeParameterDeclaration[] option * parameters: ParameterDeclaration[] * ``type``: TypeNode option * equalsGreaterThanToken: EqualsGreaterThanToken * body: ConciseBody -> ArrowFunction
        abstract createDeleteExpression: expression: Expression -> DeleteExpression
        abstract updateDeleteExpression: node: DeleteExpression * expression: Expression -> DeleteExpression
        abstract createTypeOfExpression: expression: Expression -> TypeOfExpression
        abstract updateTypeOfExpression: node: TypeOfExpression * expression: Expression -> TypeOfExpression
        abstract createVoidExpression: expression: Expression -> VoidExpression
        abstract updateVoidExpression: node: VoidExpression * expression: Expression -> VoidExpression
        abstract createAwaitExpression: expression: Expression -> AwaitExpression
        abstract updateAwaitExpression: node: AwaitExpression * expression: Expression -> AwaitExpression
        abstract createPrefixUnaryExpression: operator: PrefixUnaryOperator * operand: Expression -> PrefixUnaryExpression
        abstract updatePrefixUnaryExpression: node: PrefixUnaryExpression * operand: Expression -> PrefixUnaryExpression
        abstract createPostfixUnaryExpression: operand: Expression * operator: PostfixUnaryOperator -> PostfixUnaryExpression
        abstract updatePostfixUnaryExpression: node: PostfixUnaryExpression * operand: Expression -> PostfixUnaryExpression
        abstract createBinaryExpression: left: Expression * operator: U2<BinaryOperator, BinaryOperatorToken> * right: Expression -> BinaryExpression
        abstract updateBinaryExpression: node: BinaryExpression * left: Expression * operator: U2<BinaryOperator, BinaryOperatorToken> * right: Expression -> BinaryExpression
        abstract createConditionalExpression: condition: Expression * questionToken: QuestionToken option * whenTrue: Expression * colonToken: ColonToken option * whenFalse: Expression -> ConditionalExpression
        abstract updateConditionalExpression: node: ConditionalExpression * condition: Expression * questionToken: QuestionToken * whenTrue: Expression * colonToken: ColonToken * whenFalse: Expression -> ConditionalExpression
        abstract createTemplateExpression: head: TemplateHead * templateSpans: TemplateSpan[] -> TemplateExpression
        abstract updateTemplateExpression: node: TemplateExpression * head: TemplateHead * templateSpans: TemplateSpan[] -> TemplateExpression
        abstract createTemplateHead: text: string * ?rawText: string * ?templateFlags: TokenFlags -> TemplateHead
        abstract createTemplateHead: text: string option * rawText: string * ?templateFlags: TokenFlags -> TemplateHead
        abstract createTemplateMiddle: text: string * ?rawText: string * ?templateFlags: TokenFlags -> TemplateMiddle
        abstract createTemplateMiddle: text: string option * rawText: string * ?templateFlags: TokenFlags -> TemplateMiddle
        abstract createTemplateTail: text: string * ?rawText: string * ?templateFlags: TokenFlags -> TemplateTail
        abstract createTemplateTail: text: string option * rawText: string * ?templateFlags: TokenFlags -> TemplateTail
        abstract createNoSubstitutionTemplateLiteral: text: string * ?rawText: string -> NoSubstitutionTemplateLiteral
        abstract createNoSubstitutionTemplateLiteral: text: string option * rawText: string -> NoSubstitutionTemplateLiteral
        abstract createYieldExpression: asteriskToken: AsteriskToken * expression: Expression -> YieldExpression
        abstract createYieldExpression: asteriskToken: obj * expression: Expression option -> YieldExpression
        abstract updateYieldExpression: node: YieldExpression * asteriskToken: AsteriskToken option * expression: Expression option -> YieldExpression
        abstract createSpreadElement: expression: Expression -> SpreadElement
        abstract updateSpreadElement: node: SpreadElement * expression: Expression -> SpreadElement
        abstract createClassExpression: modifiers: ModifierLike[] option * name: U2<string, Identifier> option * typeParameters: TypeParameterDeclaration[] option * heritageClauses: HeritageClause[] option * members: ClassElement[] -> ClassExpression
        abstract updateClassExpression: node: ClassExpression * modifiers: ModifierLike[] option * name: Identifier option * typeParameters: TypeParameterDeclaration[] option * heritageClauses: HeritageClause[] option * members: ClassElement[] -> ClassExpression
        abstract createOmittedExpression: unit -> OmittedExpression
        abstract createExpressionWithTypeArguments: expression: Expression * typeArguments: TypeNode[] option -> ExpressionWithTypeArguments
        abstract updateExpressionWithTypeArguments: node: ExpressionWithTypeArguments * expression: Expression * typeArguments: TypeNode[] option -> ExpressionWithTypeArguments
        abstract createAsExpression: expression: Expression * ``type``: TypeNode -> AsExpression
        abstract updateAsExpression: node: AsExpression * expression: Expression * ``type``: TypeNode -> AsExpression
        abstract createNonNullExpression: expression: Expression -> NonNullExpression
        abstract updateNonNullExpression: node: NonNullExpression * expression: Expression -> NonNullExpression
        abstract createNonNullChain: expression: Expression -> NonNullChain
        abstract updateNonNullChain: node: NonNullChain * expression: Expression -> NonNullChain
        abstract createMetaProperty: keywordToken: obj * name: Identifier -> MetaProperty
        abstract updateMetaProperty: node: MetaProperty * name: Identifier -> MetaProperty
        abstract createSatisfiesExpression: expression: Expression * ``type``: TypeNode -> SatisfiesExpression
        abstract updateSatisfiesExpression: node: SatisfiesExpression * expression: Expression * ``type``: TypeNode -> SatisfiesExpression
        abstract createTemplateSpan: expression: Expression * literal: U2<TemplateMiddle, TemplateTail> -> TemplateSpan
        abstract updateTemplateSpan: node: TemplateSpan * expression: Expression * literal: U2<TemplateMiddle, TemplateTail> -> TemplateSpan
        abstract createSemicolonClassElement: unit -> SemicolonClassElement
        abstract createBlock: statements: Statement[] * ?multiLine: bool -> Block
        abstract updateBlock: node: Block * statements: Statement[] -> Block
        abstract createVariableStatement: modifiers: ModifierLike[] option * declarationList: U2<VariableDeclarationList, VariableDeclaration[]> -> VariableStatement
        abstract updateVariableStatement: node: VariableStatement * modifiers: ModifierLike[] option * declarationList: VariableDeclarationList -> VariableStatement
        abstract createEmptyStatement: unit -> EmptyStatement
        abstract createExpressionStatement: expression: Expression -> ExpressionStatement
        abstract updateExpressionStatement: node: ExpressionStatement * expression: Expression -> ExpressionStatement
        abstract createIfStatement: expression: Expression * thenStatement: Statement * ?elseStatement: Statement -> IfStatement
        abstract updateIfStatement: node: IfStatement * expression: Expression * thenStatement: Statement * elseStatement: Statement option -> IfStatement
        abstract createDoStatement: statement: Statement * expression: Expression -> DoStatement
        abstract updateDoStatement: node: DoStatement * statement: Statement * expression: Expression -> DoStatement
        abstract createWhileStatement: expression: Expression * statement: Statement -> WhileStatement
        abstract updateWhileStatement: node: WhileStatement * expression: Expression * statement: Statement -> WhileStatement
        abstract createForStatement: initializer: ForInitializer option * condition: Expression option * incrementor: Expression option * statement: Statement -> ForStatement
        abstract updateForStatement: node: ForStatement * initializer: ForInitializer option * condition: Expression option * incrementor: Expression option * statement: Statement -> ForStatement
        abstract createForInStatement: initializer: ForInitializer * expression: Expression * statement: Statement -> ForInStatement
        abstract updateForInStatement: node: ForInStatement * initializer: ForInitializer * expression: Expression * statement: Statement -> ForInStatement
        abstract createForOfStatement: awaitModifier: AwaitKeyword option * initializer: ForInitializer * expression: Expression * statement: Statement -> ForOfStatement
        abstract updateForOfStatement: node: ForOfStatement * awaitModifier: AwaitKeyword option * initializer: ForInitializer * expression: Expression * statement: Statement -> ForOfStatement
        abstract createContinueStatement: ?label: U2<string, Identifier> -> ContinueStatement
        abstract updateContinueStatement: node: ContinueStatement * label: Identifier option -> ContinueStatement
        abstract createBreakStatement: ?label: U2<string, Identifier> -> BreakStatement
        abstract updateBreakStatement: node: BreakStatement * label: Identifier option -> BreakStatement
        abstract createReturnStatement: ?expression: Expression -> ReturnStatement
        abstract updateReturnStatement: node: ReturnStatement * expression: Expression option -> ReturnStatement
        abstract createWithStatement: expression: Expression * statement: Statement -> WithStatement
        abstract updateWithStatement: node: WithStatement * expression: Expression * statement: Statement -> WithStatement
        abstract createSwitchStatement: expression: Expression * caseBlock: CaseBlock -> SwitchStatement
        abstract updateSwitchStatement: node: SwitchStatement * expression: Expression * caseBlock: CaseBlock -> SwitchStatement
        abstract createLabeledStatement: label: U2<string, Identifier> * statement: Statement -> LabeledStatement
        abstract updateLabeledStatement: node: LabeledStatement * label: Identifier * statement: Statement -> LabeledStatement
        abstract createThrowStatement: expression: Expression -> ThrowStatement
        abstract updateThrowStatement: node: ThrowStatement * expression: Expression -> ThrowStatement
        abstract createTryStatement: tryBlock: Block * catchClause: CatchClause option * finallyBlock: Block option -> TryStatement
        abstract updateTryStatement: node: TryStatement * tryBlock: Block * catchClause: CatchClause option * finallyBlock: Block option -> TryStatement
        abstract createDebuggerStatement: unit -> DebuggerStatement
        abstract createVariableDeclaration: name: U2<string, BindingName> * ?exclamationToken: ExclamationToken * ?``type``: TypeNode * ?initializer: Expression -> VariableDeclaration
        abstract updateVariableDeclaration: node: VariableDeclaration * name: BindingName * exclamationToken: ExclamationToken option * ``type``: TypeNode option * initializer: Expression option -> VariableDeclaration
        abstract createVariableDeclarationList: declarations: VariableDeclaration[] * ?flags: NodeFlags -> VariableDeclarationList
        abstract updateVariableDeclarationList: node: VariableDeclarationList * declarations: VariableDeclaration[] -> VariableDeclarationList
        abstract createFunctionDeclaration: modifiers: ModifierLike[] option * asteriskToken: AsteriskToken option * name: U2<string, Identifier> option * typeParameters: TypeParameterDeclaration[] option * parameters: ParameterDeclaration[] * ``type``: TypeNode option * body: Block option -> FunctionDeclaration
        abstract updateFunctionDeclaration: node: FunctionDeclaration * modifiers: ModifierLike[] option * asteriskToken: AsteriskToken option * name: Identifier option * typeParameters: TypeParameterDeclaration[] option * parameters: ParameterDeclaration[] * ``type``: TypeNode option * body: Block option -> FunctionDeclaration
        abstract createClassDeclaration: modifiers: ModifierLike[] option * name: U2<string, Identifier> option * typeParameters: TypeParameterDeclaration[] option * heritageClauses: HeritageClause[] option * members: ClassElement[] -> ClassDeclaration
        abstract updateClassDeclaration: node: ClassDeclaration * modifiers: ModifierLike[] option * name: Identifier option * typeParameters: TypeParameterDeclaration[] option * heritageClauses: HeritageClause[] option * members: ClassElement[] -> ClassDeclaration
        abstract createInterfaceDeclaration: modifiers: ModifierLike[] option * name: U2<string, Identifier> * typeParameters: TypeParameterDeclaration[] option * heritageClauses: HeritageClause[] option * members: TypeElement[] -> InterfaceDeclaration
        abstract updateInterfaceDeclaration: node: InterfaceDeclaration * modifiers: ModifierLike[] option * name: Identifier * typeParameters: TypeParameterDeclaration[] option * heritageClauses: HeritageClause[] option * members: TypeElement[] -> InterfaceDeclaration
        abstract createTypeAliasDeclaration: modifiers: ModifierLike[] option * name: U2<string, Identifier> * typeParameters: TypeParameterDeclaration[] option * ``type``: TypeNode -> TypeAliasDeclaration
        abstract updateTypeAliasDeclaration: node: TypeAliasDeclaration * modifiers: ModifierLike[] option * name: Identifier * typeParameters: TypeParameterDeclaration[] option * ``type``: TypeNode -> TypeAliasDeclaration
        abstract createEnumDeclaration: modifiers: ModifierLike[] option * name: U2<string, Identifier> * members: EnumMember[] -> EnumDeclaration
        abstract updateEnumDeclaration: node: EnumDeclaration * modifiers: ModifierLike[] option * name: Identifier * members: EnumMember[] -> EnumDeclaration
        abstract createModuleDeclaration: modifiers: ModifierLike[] option * name: ModuleName * body: ModuleBody option * ?flags: NodeFlags -> ModuleDeclaration
        abstract updateModuleDeclaration: node: ModuleDeclaration * modifiers: ModifierLike[] option * name: ModuleName * body: ModuleBody option -> ModuleDeclaration
        abstract createModuleBlock: statements: Statement[] -> ModuleBlock
        abstract updateModuleBlock: node: ModuleBlock * statements: Statement[] -> ModuleBlock
        abstract createCaseBlock: clauses: CaseOrDefaultClause[] -> CaseBlock
        abstract updateCaseBlock: node: CaseBlock * clauses: CaseOrDefaultClause[] -> CaseBlock
        abstract createNamespaceExportDeclaration: name: U2<string, Identifier> -> NamespaceExportDeclaration
        abstract updateNamespaceExportDeclaration: node: NamespaceExportDeclaration * name: Identifier -> NamespaceExportDeclaration
        abstract createImportEqualsDeclaration: modifiers: ModifierLike[] option * isTypeOnly: bool * name: U2<string, Identifier> * moduleReference: ModuleReference -> ImportEqualsDeclaration
        abstract updateImportEqualsDeclaration: node: ImportEqualsDeclaration * modifiers: ModifierLike[] option * isTypeOnly: bool * name: Identifier * moduleReference: ModuleReference -> ImportEqualsDeclaration
        abstract createImportDeclaration: modifiers: ModifierLike[] option * importClause: ImportClause option * moduleSpecifier: Expression * ?assertClause: AssertClause -> ImportDeclaration
        abstract updateImportDeclaration: node: ImportDeclaration * modifiers: ModifierLike[] option * importClause: ImportClause option * moduleSpecifier: Expression * assertClause: AssertClause option -> ImportDeclaration
        abstract createImportClause: isTypeOnly: bool * name: Identifier option * namedBindings: NamedImportBindings option -> ImportClause
        abstract updateImportClause: node: ImportClause * isTypeOnly: bool * name: Identifier option * namedBindings: NamedImportBindings option -> ImportClause
        abstract createAssertClause: elements: AssertEntry[] * ?multiLine: bool -> AssertClause
        abstract updateAssertClause: node: AssertClause * elements: AssertEntry[] * ?multiLine: bool -> AssertClause
        abstract createAssertEntry: name: AssertionKey * value: Expression -> AssertEntry
        abstract updateAssertEntry: node: AssertEntry * name: AssertionKey * value: Expression -> AssertEntry
        abstract createImportTypeAssertionContainer: clause: AssertClause * ?multiLine: bool -> ImportTypeAssertionContainer
        abstract updateImportTypeAssertionContainer: node: ImportTypeAssertionContainer * clause: AssertClause * ?multiLine: bool -> ImportTypeAssertionContainer
        abstract createNamespaceImport: name: Identifier -> NamespaceImport
        abstract updateNamespaceImport: node: NamespaceImport * name: Identifier -> NamespaceImport
        abstract createNamespaceExport: name: Identifier -> NamespaceExport
        abstract updateNamespaceExport: node: NamespaceExport * name: Identifier -> NamespaceExport
        abstract createNamedImports: elements: ImportSpecifier[] -> NamedImports
        abstract updateNamedImports: node: NamedImports * elements: ImportSpecifier[] -> NamedImports
        abstract createImportSpecifier: isTypeOnly: bool * propertyName: Identifier option * name: Identifier -> ImportSpecifier
        abstract updateImportSpecifier: node: ImportSpecifier * isTypeOnly: bool * propertyName: Identifier option * name: Identifier -> ImportSpecifier
        abstract createExportAssignment: modifiers: ModifierLike[] option * isExportEquals: bool option * expression: Expression -> ExportAssignment
        abstract updateExportAssignment: node: ExportAssignment * modifiers: ModifierLike[] option * expression: Expression -> ExportAssignment
        abstract createExportDeclaration: modifiers: ModifierLike[] option * isTypeOnly: bool * exportClause: NamedExportBindings option * ?moduleSpecifier: Expression * ?assertClause: AssertClause -> ExportDeclaration
        abstract updateExportDeclaration: node: ExportDeclaration * modifiers: ModifierLike[] option * isTypeOnly: bool * exportClause: NamedExportBindings option * moduleSpecifier: Expression option * assertClause: AssertClause option -> ExportDeclaration
        abstract createNamedExports: elements: ExportSpecifier[] -> NamedExports
        abstract updateNamedExports: node: NamedExports * elements: ExportSpecifier[] -> NamedExports
        abstract createExportSpecifier: isTypeOnly: bool * propertyName: U2<string, Identifier> option * name: U2<string, Identifier> -> ExportSpecifier
        abstract updateExportSpecifier: node: ExportSpecifier * isTypeOnly: bool * propertyName: Identifier option * name: Identifier -> ExportSpecifier
        abstract createExternalModuleReference: expression: Expression -> ExternalModuleReference
        abstract updateExternalModuleReference: node: ExternalModuleReference * expression: Expression -> ExternalModuleReference
        abstract createJSDocAllType: unit -> JSDocAllType
        abstract createJSDocUnknownType: unit -> JSDocUnknownType
        abstract createJSDocNonNullableType: ``type``: TypeNode * ?postfix: bool -> JSDocNonNullableType
        abstract updateJSDocNonNullableType: node: JSDocNonNullableType * ``type``: TypeNode -> JSDocNonNullableType
        abstract createJSDocNullableType: ``type``: TypeNode * ?postfix: bool -> JSDocNullableType
        abstract updateJSDocNullableType: node: JSDocNullableType * ``type``: TypeNode -> JSDocNullableType
        abstract createJSDocOptionalType: ``type``: TypeNode -> JSDocOptionalType
        abstract updateJSDocOptionalType: node: JSDocOptionalType * ``type``: TypeNode -> JSDocOptionalType
        abstract createJSDocFunctionType: parameters: ParameterDeclaration[] * ``type``: TypeNode option -> JSDocFunctionType
        abstract updateJSDocFunctionType: node: JSDocFunctionType * parameters: ParameterDeclaration[] * ``type``: TypeNode option -> JSDocFunctionType
        abstract createJSDocVariadicType: ``type``: TypeNode -> JSDocVariadicType
        abstract updateJSDocVariadicType: node: JSDocVariadicType * ``type``: TypeNode -> JSDocVariadicType
        abstract createJSDocNamepathType: ``type``: TypeNode -> JSDocNamepathType
        abstract updateJSDocNamepathType: node: JSDocNamepathType * ``type``: TypeNode -> JSDocNamepathType
        abstract createJSDocTypeExpression: ``type``: TypeNode -> JSDocTypeExpression
        abstract updateJSDocTypeExpression: node: JSDocTypeExpression * ``type``: TypeNode -> JSDocTypeExpression
        abstract createJSDocNameReference: name: U2<EntityName, JSDocMemberName> -> JSDocNameReference
        abstract updateJSDocNameReference: node: JSDocNameReference * name: U2<EntityName, JSDocMemberName> -> JSDocNameReference
        abstract createJSDocMemberName: left: U2<EntityName, JSDocMemberName> * right: Identifier -> JSDocMemberName
        abstract updateJSDocMemberName: node: JSDocMemberName * left: U2<EntityName, JSDocMemberName> * right: Identifier -> JSDocMemberName
        abstract createJSDocLink: name: U2<EntityName, JSDocMemberName> option * text: string -> JSDocLink
        abstract updateJSDocLink: node: JSDocLink * name: U2<EntityName, JSDocMemberName> option * text: string -> JSDocLink
        abstract createJSDocLinkCode: name: U2<EntityName, JSDocMemberName> option * text: string -> JSDocLinkCode
        abstract updateJSDocLinkCode: node: JSDocLinkCode * name: U2<EntityName, JSDocMemberName> option * text: string -> JSDocLinkCode
        abstract createJSDocLinkPlain: name: U2<EntityName, JSDocMemberName> option * text: string -> JSDocLinkPlain
        abstract updateJSDocLinkPlain: node: JSDocLinkPlain * name: U2<EntityName, JSDocMemberName> option * text: string -> JSDocLinkPlain
        abstract createJSDocTypeLiteral: ?jsDocPropertyTags: JSDocPropertyLikeTag[] * ?isArrayType: bool -> JSDocTypeLiteral
        abstract updateJSDocTypeLiteral: node: JSDocTypeLiteral * jsDocPropertyTags: JSDocPropertyLikeTag[] option * isArrayType: bool option -> JSDocTypeLiteral
        abstract createJSDocSignature: typeParameters: JSDocTemplateTag[] option * parameters: JSDocParameterTag[] * ?``type``: JSDocReturnTag -> JSDocSignature
        abstract updateJSDocSignature: node: JSDocSignature * typeParameters: JSDocTemplateTag[] option * parameters: JSDocParameterTag[] * ``type``: JSDocReturnTag option -> JSDocSignature
        abstract createJSDocTemplateTag: tagName: Identifier option * ``constraint``: JSDocTypeExpression option * typeParameters: TypeParameterDeclaration[] * ?comment: U2<string, JSDocComment[]> -> JSDocTemplateTag
        abstract updateJSDocTemplateTag: node: JSDocTemplateTag * tagName: Identifier option * ``constraint``: JSDocTypeExpression option * typeParameters: TypeParameterDeclaration[] * comment: U2<string, JSDocComment[]> option -> JSDocTemplateTag
        abstract createJSDocTypedefTag: tagName: Identifier option * ?typeExpression: U2<JSDocTypeExpression, JSDocTypeLiteral> * ?fullName: U2<Identifier, JSDocNamespaceDeclaration> * ?comment: U2<string, JSDocComment[]> -> JSDocTypedefTag
        abstract updateJSDocTypedefTag: node: JSDocTypedefTag * tagName: Identifier option * typeExpression: U2<JSDocTypeExpression, JSDocTypeLiteral> option * fullName: U2<Identifier, JSDocNamespaceDeclaration> option * comment: U2<string, JSDocComment[]> option -> JSDocTypedefTag
        abstract createJSDocParameterTag: tagName: Identifier option * name: EntityName * isBracketed: bool * ?typeExpression: JSDocTypeExpression * ?isNameFirst: bool * ?comment: U2<string, JSDocComment[]> -> JSDocParameterTag
        abstract updateJSDocParameterTag: node: JSDocParameterTag * tagName: Identifier option * name: EntityName * isBracketed: bool * typeExpression: JSDocTypeExpression option * isNameFirst: bool * comment: U2<string, JSDocComment[]> option -> JSDocParameterTag
        abstract createJSDocPropertyTag: tagName: Identifier option * name: EntityName * isBracketed: bool * ?typeExpression: JSDocTypeExpression * ?isNameFirst: bool * ?comment: U2<string, JSDocComment[]> -> JSDocPropertyTag
        abstract updateJSDocPropertyTag: node: JSDocPropertyTag * tagName: Identifier option * name: EntityName * isBracketed: bool * typeExpression: JSDocTypeExpression option * isNameFirst: bool * comment: U2<string, JSDocComment[]> option -> JSDocPropertyTag
        abstract createJSDocTypeTag: tagName: Identifier option * typeExpression: JSDocTypeExpression * ?comment: U2<string, JSDocComment[]> -> JSDocTypeTag
        abstract updateJSDocTypeTag: node: JSDocTypeTag * tagName: Identifier option * typeExpression: JSDocTypeExpression * comment: U2<string, JSDocComment[]> option -> JSDocTypeTag
        abstract createJSDocSeeTag: tagName: Identifier option * nameExpression: JSDocNameReference option * ?comment: U2<string, JSDocComment[]> -> JSDocSeeTag
        abstract updateJSDocSeeTag: node: JSDocSeeTag * tagName: Identifier option * nameExpression: JSDocNameReference option * ?comment: U2<string, JSDocComment[]> -> JSDocSeeTag
        abstract createJSDocReturnTag: tagName: Identifier option * ?typeExpression: JSDocTypeExpression * ?comment: U2<string, JSDocComment[]> -> JSDocReturnTag
        abstract updateJSDocReturnTag: node: JSDocReturnTag * tagName: Identifier option * typeExpression: JSDocTypeExpression option * comment: U2<string, JSDocComment[]> option -> JSDocReturnTag
        abstract createJSDocThisTag: tagName: Identifier option * typeExpression: JSDocTypeExpression * ?comment: U2<string, JSDocComment[]> -> JSDocThisTag
        abstract updateJSDocThisTag: node: JSDocThisTag * tagName: Identifier option * typeExpression: JSDocTypeExpression option * comment: U2<string, JSDocComment[]> option -> JSDocThisTag
        abstract createJSDocEnumTag: tagName: Identifier option * typeExpression: JSDocTypeExpression * ?comment: U2<string, JSDocComment[]> -> JSDocEnumTag
        abstract updateJSDocEnumTag: node: JSDocEnumTag * tagName: Identifier option * typeExpression: JSDocTypeExpression * comment: U2<string, JSDocComment[]> option -> JSDocEnumTag
        abstract createJSDocCallbackTag: tagName: Identifier option * typeExpression: JSDocSignature * ?fullName: U2<Identifier, JSDocNamespaceDeclaration> * ?comment: U2<string, JSDocComment[]> -> JSDocCallbackTag
        abstract updateJSDocCallbackTag: node: JSDocCallbackTag * tagName: Identifier option * typeExpression: JSDocSignature * fullName: U2<Identifier, JSDocNamespaceDeclaration> option * comment: U2<string, JSDocComment[]> option -> JSDocCallbackTag
        abstract createJSDocOverloadTag: tagName: Identifier option * typeExpression: JSDocSignature * ?comment: U2<string, JSDocComment[]> -> JSDocOverloadTag
        abstract updateJSDocOverloadTag: node: JSDocOverloadTag * tagName: Identifier option * typeExpression: JSDocSignature * comment: U2<string, JSDocComment[]> option -> JSDocOverloadTag
        abstract createJSDocAugmentsTag: tagName: Identifier option * className: obj * ?comment: U2<string, JSDocComment[]> -> JSDocAugmentsTag
        abstract updateJSDocAugmentsTag: node: JSDocAugmentsTag * tagName: Identifier option * className: obj * comment: U2<string, JSDocComment[]> option -> JSDocAugmentsTag
        abstract createJSDocImplementsTag: tagName: Identifier option * className: obj * ?comment: U2<string, JSDocComment[]> -> JSDocImplementsTag
        abstract updateJSDocImplementsTag: node: JSDocImplementsTag * tagName: Identifier option * className: obj * comment: U2<string, JSDocComment[]> option -> JSDocImplementsTag
        abstract createJSDocAuthorTag: tagName: Identifier option * ?comment: U2<string, JSDocComment[]> -> JSDocAuthorTag
        abstract updateJSDocAuthorTag: node: JSDocAuthorTag * tagName: Identifier option * comment: U2<string, JSDocComment[]> option -> JSDocAuthorTag
        abstract createJSDocClassTag: tagName: Identifier option * ?comment: U2<string, JSDocComment[]> -> JSDocClassTag
        abstract updateJSDocClassTag: node: JSDocClassTag * tagName: Identifier option * comment: U2<string, JSDocComment[]> option -> JSDocClassTag
        abstract createJSDocPublicTag: tagName: Identifier option * ?comment: U2<string, JSDocComment[]> -> JSDocPublicTag
        abstract updateJSDocPublicTag: node: JSDocPublicTag * tagName: Identifier option * comment: U2<string, JSDocComment[]> option -> JSDocPublicTag
        abstract createJSDocPrivateTag: tagName: Identifier option * ?comment: U2<string, JSDocComment[]> -> JSDocPrivateTag
        abstract updateJSDocPrivateTag: node: JSDocPrivateTag * tagName: Identifier option * comment: U2<string, JSDocComment[]> option -> JSDocPrivateTag
        abstract createJSDocProtectedTag: tagName: Identifier option * ?comment: U2<string, JSDocComment[]> -> JSDocProtectedTag
        abstract updateJSDocProtectedTag: node: JSDocProtectedTag * tagName: Identifier option * comment: U2<string, JSDocComment[]> option -> JSDocProtectedTag
        abstract createJSDocReadonlyTag: tagName: Identifier option * ?comment: U2<string, JSDocComment[]> -> JSDocReadonlyTag
        abstract updateJSDocReadonlyTag: node: JSDocReadonlyTag * tagName: Identifier option * comment: U2<string, JSDocComment[]> option -> JSDocReadonlyTag
        abstract createJSDocUnknownTag: tagName: Identifier * ?comment: U2<string, JSDocComment[]> -> JSDocUnknownTag
        abstract updateJSDocUnknownTag: node: JSDocUnknownTag * tagName: Identifier * comment: U2<string, JSDocComment[]> option -> JSDocUnknownTag
        abstract createJSDocDeprecatedTag: tagName: Identifier option * ?comment: U2<string, JSDocComment[]> -> JSDocDeprecatedTag
        abstract updateJSDocDeprecatedTag: node: JSDocDeprecatedTag * tagName: Identifier option * ?comment: U2<string, JSDocComment[]> -> JSDocDeprecatedTag
        abstract createJSDocOverrideTag: tagName: Identifier option * ?comment: U2<string, JSDocComment[]> -> JSDocOverrideTag
        abstract updateJSDocOverrideTag: node: JSDocOverrideTag * tagName: Identifier option * ?comment: U2<string, JSDocComment[]> -> JSDocOverrideTag
        abstract createJSDocThrowsTag: tagName: Identifier * typeExpression: JSDocTypeExpression option * ?comment: U2<string, JSDocComment[]> -> JSDocThrowsTag
        abstract updateJSDocThrowsTag: node: JSDocThrowsTag * tagName: Identifier option * typeExpression: JSDocTypeExpression option * ?comment: U2<string, JSDocComment[]> -> JSDocThrowsTag
        abstract createJSDocSatisfiesTag: tagName: Identifier option * typeExpression: JSDocTypeExpression * ?comment: U2<string, JSDocComment[]> -> JSDocSatisfiesTag
        abstract updateJSDocSatisfiesTag: node: JSDocSatisfiesTag * tagName: Identifier option * typeExpression: JSDocTypeExpression * comment: U2<string, JSDocComment[]> option -> JSDocSatisfiesTag
        abstract createJSDocText: text: string -> JSDocText
        abstract updateJSDocText: node: JSDocText * text: string -> JSDocText
        abstract createJSDocComment: ?comment: U2<string, JSDocComment[]> * ?tags: JSDocTag[] -> JSDoc
        abstract updateJSDocComment: node: JSDoc * comment: U2<string, JSDocComment[]> option * tags: JSDocTag[] option -> JSDoc
        abstract createJsxElement: openingElement: JsxOpeningElement * children: JsxChild[] * closingElement: JsxClosingElement -> JsxElement
        abstract updateJsxElement: node: JsxElement * openingElement: JsxOpeningElement * children: JsxChild[] * closingElement: JsxClosingElement -> JsxElement
        abstract createJsxSelfClosingElement: tagName: JsxTagNameExpression * typeArguments: TypeNode[] option * attributes: JsxAttributes -> JsxSelfClosingElement
        abstract updateJsxSelfClosingElement: node: JsxSelfClosingElement * tagName: JsxTagNameExpression * typeArguments: TypeNode[] option * attributes: JsxAttributes -> JsxSelfClosingElement
        abstract createJsxOpeningElement: tagName: JsxTagNameExpression * typeArguments: TypeNode[] option * attributes: JsxAttributes -> JsxOpeningElement
        abstract updateJsxOpeningElement: node: JsxOpeningElement * tagName: JsxTagNameExpression * typeArguments: TypeNode[] option * attributes: JsxAttributes -> JsxOpeningElement
        abstract createJsxClosingElement: tagName: JsxTagNameExpression -> JsxClosingElement
        abstract updateJsxClosingElement: node: JsxClosingElement * tagName: JsxTagNameExpression -> JsxClosingElement
        abstract createJsxFragment: openingFragment: JsxOpeningFragment * children: JsxChild[] * closingFragment: JsxClosingFragment -> JsxFragment
        abstract createJsxText: text: string * ?containsOnlyTriviaWhiteSpaces: bool -> JsxText
        abstract updateJsxText: node: JsxText * text: string * ?containsOnlyTriviaWhiteSpaces: bool -> JsxText
        abstract createJsxOpeningFragment: unit -> JsxOpeningFragment
        abstract createJsxJsxClosingFragment: unit -> JsxClosingFragment
        abstract updateJsxFragment: node: JsxFragment * openingFragment: JsxOpeningFragment * children: JsxChild[] * closingFragment: JsxClosingFragment -> JsxFragment
        abstract createJsxAttribute: name: JsxAttributeName * initializer: JsxAttributeValue option -> JsxAttribute
        abstract updateJsxAttribute: node: JsxAttribute * name: JsxAttributeName * initializer: JsxAttributeValue option -> JsxAttribute
        abstract createJsxAttributes: properties: JsxAttributeLike[] -> JsxAttributes
        abstract updateJsxAttributes: node: JsxAttributes * properties: JsxAttributeLike[] -> JsxAttributes
        abstract createJsxSpreadAttribute: expression: Expression -> JsxSpreadAttribute
        abstract updateJsxSpreadAttribute: node: JsxSpreadAttribute * expression: Expression -> JsxSpreadAttribute
        abstract createJsxExpression: dotDotDotToken: DotDotDotToken option * expression: Expression option -> JsxExpression
        abstract updateJsxExpression: node: JsxExpression * expression: Expression option -> JsxExpression
        abstract createJsxNamespacedName: ``namespace``: Identifier * name: Identifier -> JsxNamespacedName
        abstract updateJsxNamespacedName: node: JsxNamespacedName * ``namespace``: Identifier * name: Identifier -> JsxNamespacedName
        abstract createCaseClause: expression: Expression * statements: Statement[] -> CaseClause
        abstract updateCaseClause: node: CaseClause * expression: Expression * statements: Statement[] -> CaseClause
        abstract createDefaultClause: statements: Statement[] -> DefaultClause
        abstract updateDefaultClause: node: DefaultClause * statements: Statement[] -> DefaultClause
        abstract createHeritageClause: token: obj * types: ExpressionWithTypeArguments[] -> HeritageClause
        abstract updateHeritageClause: node: HeritageClause * types: ExpressionWithTypeArguments[] -> HeritageClause
        abstract createCatchClause: variableDeclaration: U3<string, BindingName, VariableDeclaration> option * block: Block -> CatchClause
        abstract updateCatchClause: node: CatchClause * variableDeclaration: VariableDeclaration option * block: Block -> CatchClause
        abstract createPropertyAssignment: name: U2<string, PropertyName> * initializer: Expression -> PropertyAssignment
        abstract updatePropertyAssignment: node: PropertyAssignment * name: PropertyName * initializer: Expression -> PropertyAssignment
        abstract createShorthandPropertyAssignment: name: U2<string, Identifier> * ?objectAssignmentInitializer: Expression -> ShorthandPropertyAssignment
        abstract updateShorthandPropertyAssignment: node: ShorthandPropertyAssignment * name: Identifier * objectAssignmentInitializer: Expression option -> ShorthandPropertyAssignment
        abstract createSpreadAssignment: expression: Expression -> SpreadAssignment
        abstract updateSpreadAssignment: node: SpreadAssignment * expression: Expression -> SpreadAssignment
        abstract createEnumMember: name: U2<string, PropertyName> * ?initializer: Expression -> EnumMember
        abstract updateEnumMember: node: EnumMember * name: PropertyName * initializer: Expression option -> EnumMember
        abstract createSourceFile: statements: Statement[] * endOfFileToken: EndOfFileToken * flags: NodeFlags -> SourceFile
        abstract updateSourceFile: node: SourceFile * statements: Statement[] * ?isDeclarationFile: bool * ?referencedFiles: FileReference[] * ?typeReferences: FileReference[] * ?hasNoDefaultLib: bool * ?libReferences: FileReference[] -> SourceFile
        abstract createNotEmittedStatement: original: Node -> NotEmittedStatement
        abstract createPartiallyEmittedExpression: expression: Expression * ?original: Node -> PartiallyEmittedExpression
        abstract updatePartiallyEmittedExpression: node: PartiallyEmittedExpression * expression: Expression -> PartiallyEmittedExpression
        abstract createCommaListExpression: elements: Expression[] -> CommaListExpression
        abstract updateCommaListExpression: node: CommaListExpression * elements: Expression[] -> CommaListExpression
        abstract createBundle: sourceFiles: SourceFile[] -> Bundle
        abstract updateBundle: node: Bundle * sourceFiles: SourceFile[] -> Bundle
        abstract createComma: left: Expression * right: Expression -> BinaryExpression
        abstract createAssignment: left: U2<ObjectLiteralExpression, ArrayLiteralExpression> * right: Expression -> DestructuringAssignment
        abstract createAssignment: left: Expression * right: Expression -> AssignmentExpression<EqualsToken>
        abstract createLogicalOr: left: Expression * right: Expression -> BinaryExpression
        abstract createLogicalAnd: left: Expression * right: Expression -> BinaryExpression
        abstract createBitwiseOr: left: Expression * right: Expression -> BinaryExpression
        abstract createBitwiseXor: left: Expression * right: Expression -> BinaryExpression
        abstract createBitwiseAnd: left: Expression * right: Expression -> BinaryExpression
        abstract createStrictEquality: left: Expression * right: Expression -> BinaryExpression
        abstract createStrictInequality: left: Expression * right: Expression -> BinaryExpression
        abstract createEquality: left: Expression * right: Expression -> BinaryExpression
        abstract createInequality: left: Expression * right: Expression -> BinaryExpression
        abstract createLessThan: left: Expression * right: Expression -> BinaryExpression
        abstract createLessThanEquals: left: Expression * right: Expression -> BinaryExpression
        abstract createGreaterThan: left: Expression * right: Expression -> BinaryExpression
        abstract createGreaterThanEquals: left: Expression * right: Expression -> BinaryExpression
        abstract createLeftShift: left: Expression * right: Expression -> BinaryExpression
        abstract createRightShift: left: Expression * right: Expression -> BinaryExpression
        abstract createUnsignedRightShift: left: Expression * right: Expression -> BinaryExpression
        abstract createAdd: left: Expression * right: Expression -> BinaryExpression
        abstract createSubtract: left: Expression * right: Expression -> BinaryExpression
        abstract createMultiply: left: Expression * right: Expression -> BinaryExpression
        abstract createDivide: left: Expression * right: Expression -> BinaryExpression
        abstract createModulo: left: Expression * right: Expression -> BinaryExpression
        abstract createExponent: left: Expression * right: Expression -> BinaryExpression
        abstract createPrefixPlus: operand: Expression -> PrefixUnaryExpression
        abstract createPrefixMinus: operand: Expression -> PrefixUnaryExpression
        abstract createPrefixIncrement: operand: Expression -> PrefixUnaryExpression
        abstract createPrefixDecrement: operand: Expression -> PrefixUnaryExpression
        abstract createBitwiseNot: operand: Expression -> PrefixUnaryExpression
        abstract createLogicalNot: operand: Expression -> PrefixUnaryExpression
        abstract createPostfixIncrement: operand: Expression -> PostfixUnaryExpression
        abstract createPostfixDecrement: operand: Expression -> PostfixUnaryExpression
        abstract createImmediatelyInvokedFunctionExpression: statements: Statement[] -> CallExpression
        abstract createImmediatelyInvokedFunctionExpression: statements: Statement[] * param: ParameterDeclaration * paramValue: Expression -> CallExpression
        abstract createImmediatelyInvokedArrowFunction: statements: Statement[] -> CallExpression
        abstract createImmediatelyInvokedArrowFunction: statements: Statement[] * param: ParameterDeclaration * paramValue: Expression -> CallExpression
        abstract createVoidZero: unit -> VoidExpression
        abstract createExportDefault: expression: Expression -> ExportAssignment
        abstract createExternalModuleExport: exportName: Identifier -> ExportDeclaration
        abstract restoreOuterExpressions: outerExpression: Expression option * innerExpression: Expression * ?kinds: OuterExpressionKinds -> Expression

    type [<AllowNullLiteral>] CoreTransformationContext =
        abstract factory: NodeFactory
        /// Gets the compiler options supplied to the transformer.
        abstract getCompilerOptions: unit -> CompilerOptions
        /// Starts a new lexical environment.
        abstract startLexicalEnvironment: unit -> unit
        /// Suspends the current lexical environment, usually after visiting a parameter list.
        abstract suspendLexicalEnvironment: unit -> unit
        /// Resumes a suspended lexical environment, usually before visiting a function body.
        abstract resumeLexicalEnvironment: unit -> unit
        /// Ends a lexical environment, returning any declarations.
        abstract endLexicalEnvironment: unit -> Statement[] option
        /// Hoists a function declaration to the containing scope.
        abstract hoistFunctionDeclaration: node: FunctionDeclaration -> unit
        /// Hoists a variable declaration to the containing scope.
        abstract hoistVariableDeclaration: node: Identifier -> unit

    type [<AllowNullLiteral>] TransformationContext =
        inherit CoreTransformationContext
        /// Records a request for a non-scoped emit helper in the current context.
        abstract requestEmitHelper: helper: EmitHelper -> unit
        /// Gets and resets the requested non-scoped emit helpers.
        abstract readEmitHelpers: unit -> EmitHelper[] option
        /// Enables expression substitutions in the pretty printer for the provided SyntaxKind.
        abstract enableSubstitution: kind: SyntaxKind -> unit
        /// Determines whether expression substitutions are enabled for the provided node.
        abstract isSubstitutionEnabled: node: Node -> bool
        /// <summary>
        /// Hook used by transformers to substitute expressions just before they
        /// are emitted by the pretty printer.
        /// 
        /// NOTE: Transformation hooks should only be modified during <c>Transformer</c> initialization,
        /// before returning the <c>NodeTransformer</c> callback.
        /// </summary>
        abstract onSubstituteNode: hint: EmitHint * node: Node -> Node
        /// Enables before/after emit notifications in the pretty printer for the provided
        /// SyntaxKind.
        abstract enableEmitNotification: kind: SyntaxKind -> unit
        /// Determines whether before/after emit notifications should be raised in the pretty
        /// printer when it emits a node.
        abstract isEmitNotificationEnabled: node: Node -> bool
        /// <summary>
        /// Hook used to allow transformers to capture state before or after
        /// the printer emits a node.
        /// 
        /// NOTE: Transformation hooks should only be modified during <c>Transformer</c> initialization,
        /// before returning the <c>NodeTransformer</c> callback.
        /// </summary>
        abstract onEmitNode: hint: EmitHint * node: Node * emitCallback: (EmitHint -> Node -> unit) -> unit

    type [<AllowNullLiteral>] TransformationResult<'T when 'T :> Node> =
        /// Gets the transformed source files.
        abstract transformed: 'T[] with get, set
        /// Gets diagnostics for the transformation.
        abstract diagnostics: DiagnosticWithLocation[] option with get, set
        /// <summary>Gets a substitute for a node, if one is available; otherwise, returns the original node.</summary>
        /// <param name="hint">A hint as to the intended usage of the node.</param>
        /// <param name="node">The node to substitute.</param>
        abstract substituteNode: hint: EmitHint * node: Node -> Node
        /// <summary>Emits a node with possible notification.</summary>
        /// <param name="hint">A hint as to the intended usage of the node.</param>
        /// <param name="node">The node to emit.</param>
        /// <param name="emitCallback">A callback used to emit the node.</param>
        abstract emitNodeWithNotification: hint: EmitHint * node: Node * emitCallback: (EmitHint -> Node -> unit) -> unit
        /// <summary>Indicates if a given node needs an emit notification</summary>
        /// <param name="node">The node to emit.</param>
        abstract isEmitNotificationEnabled: node: Node -> bool
        /// Clean up EmitNode entries on any parse-tree nodes.
        abstract dispose: unit -> unit

    /// <summary>
    /// A function that is used to initialize and return a <c>Transformer</c> callback, which in turn
    /// will be used to transform one or more nodes.
    /// </summary>
    type [<AllowNullLiteral>] TransformerFactory<'T when 'T :> Node> =
        /// <summary>
        /// A function that is used to initialize and return a <c>Transformer</c> callback, which in turn
        /// will be used to transform one or more nodes.
        /// </summary>
        [<Emit("$0($1...)")>] abstract Invoke: context: TransformationContext -> Transformer<'T>

    /// A function that transforms a node.
    type [<AllowNullLiteral>] Transformer<'T when 'T :> Node> =
        /// A function that transforms a node.
        [<Emit("$0($1...)")>] abstract Invoke: node: 'T -> 'T

    /// A function that accepts and possibly transforms a node.
    type Visitor =
        Visitor<Node, Node option>

    /// A function that accepts and possibly transforms a node.
    type Visitor<'TIn when 'TIn :> Node> =
        Visitor<'TIn, 'TIn option>

    /// A function that accepts and possibly transforms a node.
    type [<AllowNullLiteral>] Visitor<'TIn, 'TOut when 'TIn :> Node> =
        /// A function that accepts and possibly transforms a node.
        [<Emit("$0($1...)")>] abstract Invoke: node: 'TIn -> VisitResult<'TOut>

    /// <summary>
    /// A function that walks a node using the given visitor, lifting node arrays into single nodes,
    /// returning an node which satisfies the test.
    /// 
    /// - If the input node is undefined, then the output is undefined.
    /// - If the visitor returns undefined, then the output is undefined.
    /// - If the output node is not undefined, then it will satisfy the test function.
    /// - In order to obtain a return type that is more specific than <c>Node</c>, a test
    ///   function _must_ be provided, and that function must be a type predicate.
    /// 
    /// For the canonical implementation of this type, @see {visitNode}.
    /// </summary>
    type [<AllowNullLiteral>] NodeVisitor =
        [<Emit("$0($1...)")>] abstract Invoke: node: 'TIn * visitor: Visitor<'TIn, 'TVisited> * test: (Node -> bool) * ?lift: (Node[] -> Node) -> U2<'TOut, obj> when 'TOut :> Node
        [<Emit("$0($1...)")>] abstract Invoke: node: 'TIn * visitor: Visitor<'TIn, 'TVisited> * ?test: (Node -> bool) * ?lift: (Node[] -> Node) -> U2<Node, obj>

    /// <summary>
    /// A function that walks a node array using the given visitor, returning an array whose contents satisfy the test.
    /// 
    /// - If the input node array is undefined, the output is undefined.
    /// - If the visitor can return undefined, the node it visits in the array will be reused.
    /// - If the output node array is not undefined, then its contents will satisfy the test.
    /// - In order to obtain a return type that is more specific than <c>NodeArray&lt;Node&gt;</c>, a test
    ///   function _must_ be provided, and that function must be a type predicate.
    /// 
    /// For the canonical implementation of this type, @see {visitNodes}.
    /// </summary>
    type [<AllowNullLiteral>] NodesVisitor =
        [<Emit("$0($1...)")>] abstract Invoke: nodes: 'TInArray * visitor: Visitor<'TIn, Node option> * test: (Node -> bool) * ?start: float * ?count: float -> U2<'TOut[], obj> when 'TIn :> Node and 'TOut :> Node
        [<Emit("$0($1...)")>] abstract Invoke: nodes: 'TInArray * visitor: Visitor<'TIn, Node option> * ?test: (Node -> bool) * ?start: float * ?count: float -> U2<Node[], obj> when 'TIn :> Node

    type VisitResult<'T> =
        U2<'T, Node[]>

    type [<AllowNullLiteral>] Printer =
        /// <summary>Print a node and its subtree as-is, without any emit transformations.</summary>
        /// <param name="hint">
        /// A value indicating the purpose of a node. This is primarily used to
        /// distinguish between an <c>Identifier</c> used in an expression position, versus an
        /// <c>Identifier</c> used as an <c>IdentifierName</c> as part of a declaration. For most nodes you
        /// should just pass <c>Unspecified</c>.
        /// </param>
        /// <param name="node">
        /// The node to print. The node and its subtree are printed as-is, without any
        /// emit transformations.
        /// </param>
        /// <param name="sourceFile">
        /// A source file that provides context for the node. The source text of
        /// the file is used to emit the original source content for literals and identifiers, while
        /// the identifiers of the source file are used when generating unique names to avoid
        /// collisions.
        /// </param>
        abstract printNode: hint: EmitHint * node: Node * sourceFile: SourceFile -> string
        /// Prints a list of nodes using the given format flags
        abstract printList: format: ListFormat * list: 'T[] * sourceFile: SourceFile -> string when 'T :> Node
        /// Prints a source file as-is, without any emit transformations.
        abstract printFile: sourceFile: SourceFile -> string
        /// Prints a bundle of source files as-is, without any emit transformations.
        abstract printBundle: bundle: Bundle -> string

    type [<AllowNullLiteral>] PrintHandlers =
        /// A hook used by the Printer when generating unique names to avoid collisions with
        /// globally defined names that exist outside of the current source file.
        abstract hasGlobalName: name: string -> bool
        /// <summary>
        /// A hook used by the Printer to provide notifications prior to emitting a node. A
        /// compatible implementation **must** invoke <c>emitCallback</c> with the provided <c>hint</c> and
        /// <c>node</c> values.
        /// </summary>
        /// <param name="hint">A hint indicating the intended purpose of the node.</param>
        /// <param name="node">The node to emit.</param>
        /// <param name="emitCallback">A callback that, when invoked, will emit the node.</param>
        /// <example>
        /// <code lang="ts">
        /// var printer = createPrinter(printerOptions, {
        ///   onEmitNode(hint, node, emitCallback) {
        ///     // set up or track state prior to emitting the node...
        ///     emitCallback(hint, node);
        ///     // restore state after emitting the node...
        ///   }
        /// });
        /// </code>
        /// </example>
        abstract onEmitNode: hint: EmitHint * node: Node * emitCallback: (EmitHint -> Node -> unit) -> unit
        /// <summary>A hook used to check if an emit notification is required for a node.</summary>
        /// <param name="node">The node to emit.</param>
        abstract isEmitNotificationEnabled: node: Node -> bool
        /// <summary>
        /// A hook used by the Printer to perform just-in-time substitution of a node. This is
        /// primarily used by node transformations that need to substitute one node for another,
        /// such as replacing <c>myExportedVar</c> with <c>exports.myExportedVar</c>.
        /// </summary>
        /// <param name="hint">A hint indicating the intended purpose of the node.</param>
        /// <param name="node">The node to emit.</param>
        /// <example>
        /// <code lang="ts">
        /// var printer = createPrinter(printerOptions, {
        ///   substituteNode(hint, node) {
        ///     // perform substitution if necessary...
        ///     return node;
        ///   }
        /// });
        /// </code>
        /// </example>
        abstract substituteNode: hint: EmitHint * node: Node -> Node

    type [<AllowNullLiteral>] PrinterOptions =
        abstract removeComments: bool option with get, set
        abstract newLine: NewLineKind option with get, set
        abstract omitTrailingSemicolon: bool option with get, set
        abstract noEmitHelpers: bool option with get, set

    type [<AllowNullLiteral>] GetEffectiveTypeRootsHost =
        abstract getCurrentDirectory: unit -> string

    type [<AllowNullLiteral>] TextSpan =
        abstract start: float with get, set
        abstract length: float with get, set

    type [<AllowNullLiteral>] TextChangeRange =
        abstract span: TextSpan with get, set
        abstract newLength: float with get, set

    type [<AllowNullLiteral>] SyntaxList =
        inherit Node
        abstract kind: SyntaxKind with get, set
        abstract _children: Node[] with get, set

    type [<RequireQualifiedAccess>] ListFormat =
        | None = 0
        | SingleLine = 0
        | MultiLine = 1
        | PreserveLines = 2
        | LinesMask = 3
        | NotDelimited = 0
        | BarDelimited = 4
        | AmpersandDelimited = 8
        | CommaDelimited = 16
        | AsteriskDelimited = 32
        | DelimitersMask = 60
        | AllowTrailingComma = 64
        | Indented = 128
        | SpaceBetweenBraces = 256
        | SpaceBetweenSiblings = 512
        | Braces = 1024
        | Parenthesis = 2048
        | AngleBrackets = 4096
        | SquareBrackets = 8192
        | BracketsMask = 15360
        | OptionalIfUndefined = 16384
        | OptionalIfEmpty = 32768
        | Optional = 49152
        | PreferNewLine = 65536
        | NoTrailingNewLine = 131072
        | NoInterveningComments = 262144
        | NoSpaceIfEmpty = 524288
        | SingleElement = 1048576
        | SpaceAfterList = 2097152
        | Modifiers = 2359808
        | HeritageClauses = 512
        | SingleLineTypeLiteralMembers = 768
        | MultiLineTypeLiteralMembers = 32897
        | SingleLineTupleTypeElements = 528
        | MultiLineTupleTypeElements = 657
        | UnionTypeConstituents = 516
        | IntersectionTypeConstituents = 520
        | ObjectBindingPatternElements = 525136
        | ArrayBindingPatternElements = 524880
        | ObjectLiteralExpressionProperties = 526226
        | ImportClauseEntries = 526226
        | ArrayLiteralExpressionElements = 8914
        | CommaListElements = 528
        | CallExpressionArguments = 2576
        | NewExpressionArguments = 18960
        | TemplateExpressionSpans = 262144
        | SingleLineBlockStatements = 768
        | MultiLineBlockStatements = 129
        | VariableDeclarationList = 528
        | SingleLineFunctionBodyStatements = 768
        | MultiLineFunctionBodyStatements = 1
        | ClassHeritageClauses = 0
        | ClassMembers = 129
        | InterfaceMembers = 129
        | EnumMembers = 145
        | CaseBlockClauses = 129
        | NamedImportsOrExportsElements = 525136
        | JsxElementOrFragmentChildren = 262144
        | JsxElementAttributes = 262656
        | CaseOrDefaultClauseStatements = 163969
        | HeritageClauseTypes = 528
        | SourceFileStatements = 131073
        | Decorators = 2146305
        | TypeArguments = 53776
        | TypeParameters = 53776
        | Parameters = 2576
        | IndexSignatureParameters = 8848
        | JSDocComment = 33

    type [<AllowNullLiteral>] UserPreferences =
        abstract disableSuggestions: bool option
        abstract quotePreference: UserPreferencesQuotePreference option
        abstract includeCompletionsForModuleExports: bool option
        abstract includeCompletionsForImportStatements: bool option
        abstract includeCompletionsWithSnippetText: bool option
        abstract includeAutomaticOptionalChainCompletions: bool option
        abstract includeCompletionsWithInsertText: bool option
        abstract includeCompletionsWithClassMemberSnippets: bool option
        abstract includeCompletionsWithObjectLiteralMethodSnippets: bool option
        abstract useLabelDetailsInCompletionEntries: bool option
        abstract allowIncompleteCompletions: bool option
        abstract importModuleSpecifierPreference: UserPreferencesImportModuleSpecifierPreference option
        /// <summary>Determines whether we import <c>foo/index.ts</c> as "foo", "foo/index", or "foo/index.js"</summary>
        abstract importModuleSpecifierEnding: UserPreferencesImportModuleSpecifierEnding option
        abstract allowTextChangesInNewFiles: bool option
        abstract providePrefixAndSuffixTextForRename: bool option
        abstract includePackageJsonAutoImports: UserPreferencesIncludePackageJsonAutoImports option
        abstract provideRefactorNotApplicableReason: bool option
        abstract jsxAttributeCompletionStyle: UserPreferencesJsxAttributeCompletionStyle option
        abstract includeInlayParameterNameHints: UserPreferencesIncludeInlayParameterNameHints option
        abstract includeInlayParameterNameHintsWhenArgumentMatchesName: bool option
        abstract includeInlayFunctionParameterTypeHints: bool option
        abstract includeInlayVariableTypeHints: bool option
        abstract includeInlayVariableTypeHintsWhenTypeMatchesName: bool option
        abstract includeInlayPropertyDeclarationTypeHints: bool option
        abstract includeInlayFunctionLikeReturnTypeHints: bool option
        abstract includeInlayEnumMemberValueHints: bool option
        abstract allowRenameOfImportPath: bool option
        abstract autoImportFileExcludePatterns: string[] option
        abstract organizeImportsIgnoreCase: U2<bool, string> option
        abstract organizeImportsCollation: UserPreferencesOrganizeImportsCollation option
        abstract organizeImportsLocale: string option
        abstract organizeImportsNumericCollation: bool option
        abstract organizeImportsAccentCollation: bool option
        abstract organizeImportsCaseFirst: UserPreferencesOrganizeImportsCaseFirst option

    /// Represents a bigint literal value without requiring bigint support
    type [<AllowNullLiteral>] PseudoBigInt =
        abstract negative: bool with get, set
        abstract base10Value: string with get, set

    type [<RequireQualifiedAccess>] FileWatcherEventKind =
        | Created = 0
        | Changed = 1
        | Deleted = 2

    type [<AllowNullLiteral>] FileWatcherCallback =
        [<Emit("$0($1...)")>] abstract Invoke: fileName: string * eventKind: FileWatcherEventKind * ?modifiedTime: DateTime -> unit

    type [<AllowNullLiteral>] DirectoryWatcherCallback =
        [<Emit("$0($1...)")>] abstract Invoke: fileName: string -> unit

    type [<StringEnum>] [<RequireQualifiedAccess>] BufferEncoding =
        | Ascii
        | Utf8
        | [<CompiledName("utf-8")>] ``utf-8``
        | Utf16le
        | Ucs2
        | [<CompiledName("ucs-2")>] ``ucs-2``
        | Base64
        | Latin1
        | Binary
        | Hex

    type [<AllowNullLiteral>] System =
        abstract args: string[] with get, set
        abstract newLine: string with get, set
        abstract useCaseSensitiveFileNames: bool with get, set
        abstract write: s: string -> unit
        abstract writeOutputIsTTY: unit -> bool
        abstract getWidthOfTerminal: unit -> float
        abstract readFile: path: string * ?encoding: string -> string option
        abstract getFileSize: path: string -> float
        abstract writeFile: path: string * data: string * ?writeByteOrderMark: bool -> unit
        abstract watchFile: path: string * callback: FileWatcherCallback * ?pollingInterval: float * ?options: WatchOptions -> FileWatcher
        abstract watchDirectory: path: string * callback: DirectoryWatcherCallback * ?recursive: bool * ?options: WatchOptions -> FileWatcher
        abstract resolvePath: path: string -> string
        abstract fileExists: path: string -> bool
        abstract directoryExists: path: string -> bool
        abstract createDirectory: path: string -> unit
        abstract getExecutingFilePath: unit -> string
        abstract getCurrentDirectory: unit -> string
        abstract getDirectories: path: string -> string[]
        abstract readDirectory: path: string * ?extensions: string[] * ?exclude: string[] * ?``include``: string[] * ?depth: float -> string[]
        abstract getModifiedTime: path: string -> DateTime option
        abstract setModifiedTime: path: string * time: DateTime -> unit
        abstract deleteFile: path: string -> unit
        /// <summary>A good implementation is node.js' <c>crypto.createHash</c>. (<see href="https://nodejs.org/api/crypto.html#crypto_crypto_createhash_algorithm)" /></summary>
        abstract createHash: data: string -> string
        /// <summary>This must be cryptographically secure. Only implement this method using <c>crypto.createHash("sha256")</c>.</summary>
        abstract createSHA256Hash: data: string -> string
        abstract getMemoryUsage: unit -> float
        abstract exit: ?exitCode: float -> unit
        abstract realpath: path: string -> string
        abstract setTimeout: callback: (obj option[] -> unit) * ms: float * [<ParamArray>] args: obj option[] -> obj option
        abstract clearTimeout: timeoutId: obj option -> unit
        abstract clearScreen: unit -> unit
        abstract base64decode: input: string -> string
        abstract base64encode: input: string -> string

    type [<AllowNullLiteral>] FileWatcher =
        abstract close: unit -> unit

    type [<AllowNullLiteral>] ErrorCallback =
        [<Emit("$0($1...)")>] abstract Invoke: message: DiagnosticMessage * length: float * ?arg0: obj -> unit

    type [<AllowNullLiteral>] Scanner =
        abstract getToken: unit -> SyntaxKind
        abstract getTokenFullStart: unit -> float
        abstract getTokenStart: unit -> float
        abstract getTokenEnd: unit -> float
        abstract getTokenText: unit -> string
        abstract getTokenValue: unit -> string
        abstract hasUnicodeEscape: unit -> bool
        abstract hasExtendedUnicodeEscape: unit -> bool
        abstract hasPrecedingLineBreak: unit -> bool
        abstract isIdentifier: unit -> bool
        abstract isReservedWord: unit -> bool
        abstract isUnterminated: unit -> bool
        abstract reScanGreaterToken: unit -> SyntaxKind
        abstract reScanSlashToken: unit -> SyntaxKind
        abstract reScanAsteriskEqualsToken: unit -> SyntaxKind
        abstract reScanTemplateToken: isTaggedTemplate: bool -> SyntaxKind
        abstract scanJsxIdentifier: unit -> SyntaxKind
        abstract scanJsxAttributeValue: unit -> SyntaxKind
        abstract reScanJsxAttributeValue: unit -> SyntaxKind
        abstract reScanJsxToken: ?allowMultilineJsxText: bool -> JsxTokenSyntaxKind
        abstract reScanLessThanToken: unit -> SyntaxKind
        abstract reScanHashToken: unit -> SyntaxKind
        abstract reScanQuestionToken: unit -> SyntaxKind
        abstract reScanInvalidIdentifier: unit -> SyntaxKind
        abstract scanJsxToken: unit -> JsxTokenSyntaxKind
        abstract scanJsDocToken: unit -> JSDocSyntaxKind
        abstract scan: unit -> SyntaxKind
        abstract getText: unit -> string
        abstract setText: text: string option * ?start: float * ?length: float -> unit
        abstract setOnError: onError: ErrorCallback option -> unit
        abstract setScriptTarget: scriptTarget: ScriptTarget -> unit
        abstract setLanguageVariant: variant: LanguageVariant -> unit
        abstract resetTokenState: pos: float -> unit
        abstract lookAhead: callback: (unit -> 'T) -> 'T
        abstract scanRange: start: float * length: float * callback: (unit -> 'T) -> 'T
        abstract tryScan: callback: (unit -> 'T) -> 'T

    type [<AllowNullLiteral>] ParameterPropertyDeclaration =
        interface end

    type [<AllowNullLiteral>] CreateSourceFileOptions =
        abstract languageVersion: ScriptTarget with get, set
        /// <summary>
        /// Controls the format the file is detected as - this can be derived from only the path
        /// and files on disk, but needs to be done with a module resolution cache in scope to be performant.
        /// This is usually <c>undefined</c> for compilations that do not have <c>moduleResolution</c> values of <c>node16</c> or <c>nodenext</c>.
        /// </summary>
        abstract impliedNodeFormat: ResolutionMode option with get, set
        /// <summary>
        /// Controls how module-y-ness is set for the given file. Usually the result of calling
        /// <c>getSetExternalModuleIndicator</c> on a valid <c>CompilerOptions</c> object. If not present, the default
        /// check specified by <c>isFileProbablyExternalModule</c> will be used to set the field.
        /// </summary>
        abstract setExternalModuleIndicator: file: SourceFile -> unit

    type [<AllowNullLiteral>] DiagnosticReporter =
        [<Emit("$0($1...)")>] abstract Invoke: diagnostic: Diagnostic -> unit

    /// Reports config file diagnostics
    type [<AllowNullLiteral>] ConfigFileDiagnosticsReporter =
        /// Reports unrecoverable error when parsing config file
        abstract onUnRecoverableConfigFileDiagnostic: DiagnosticReporter with get, set

    /// Interface extending ParseConfigHost to support ParseConfigFile that reads config file and reports errors
    type [<AllowNullLiteral>] ParseConfigFileHost =
        inherit ParseConfigHost
        inherit ConfigFileDiagnosticsReporter
        abstract getCurrentDirectory: unit -> string

    type [<AllowNullLiteral>] ParsedTsconfig =
        abstract raw: obj option with get, set
        abstract options: CompilerOptions option with get, set
        abstract watchOptions: WatchOptions option with get, set
        abstract typeAcquisition: TypeAcquisition option with get, set
        /// Note that the case of the config path has not yet been normalized, as no files have been imported into the project yet
        abstract extendedConfigPath: U2<string, string[]> option with get, set

    type [<AllowNullLiteral>] ExtendedConfigCacheEntry =
        abstract extendedResult: TsConfigSourceFile with get, set
        abstract extendedConfig: ParsedTsconfig option with get, set

    type [<AllowNullLiteral>] TypeReferenceDirectiveResolutionCache =
        inherit PerDirectoryResolutionCache<ResolvedTypeReferenceDirectiveWithFailedLookupLocations>
        inherit NonRelativeNameResolutionCache<ResolvedTypeReferenceDirectiveWithFailedLookupLocations>
        inherit PackageJsonInfoCache

    type [<AllowNullLiteral>] ModeAwareCache<'T> =
        abstract get: key: string * mode: ResolutionMode -> 'T option
        abstract set: key: string * mode: ResolutionMode * value: 'T -> ModeAwareCache<'T>
        abstract delete: key: string * mode: ResolutionMode -> ModeAwareCache<'T>
        abstract has: key: string * mode: ResolutionMode -> bool
        abstract forEach: cb: ('T -> string -> ResolutionMode -> unit) -> unit
        abstract size: unit -> float

    /// Cached resolutions per containing directory.
    /// This assumes that any module id will have the same resolution for sibling files located in the same folder.
    type [<AllowNullLiteral>] PerDirectoryResolutionCache<'T> =
        abstract getFromDirectoryCache: name: string * mode: ResolutionMode * directoryName: string * redirectedReference: ResolvedProjectReference option -> 'T option
        abstract getOrCreateCacheForDirectory: directoryName: string * ?redirectedReference: ResolvedProjectReference -> ModeAwareCache<'T>
        abstract clear: unit -> unit
        /// Updates with the current compilerOptions the cache will operate with.
        /// This updates the redirects map as well if needed so module resolutions are cached if they can across the projects
        abstract update: options: CompilerOptions -> unit

    type [<AllowNullLiteral>] NonRelativeNameResolutionCache<'T> =
        abstract getFromNonRelativeNameCache: nonRelativeName: string * mode: ResolutionMode * directoryName: string * redirectedReference: ResolvedProjectReference option -> 'T option
        abstract getOrCreateCacheForNonRelativeName: nonRelativeName: string * mode: ResolutionMode * ?redirectedReference: ResolvedProjectReference -> PerNonRelativeNameCache<'T>
        abstract clear: unit -> unit
        /// Updates with the current compilerOptions the cache will operate with.
        /// This updates the redirects map as well if needed so module resolutions are cached if they can across the projects
        abstract update: options: CompilerOptions -> unit

    type [<AllowNullLiteral>] PerNonRelativeNameCache<'T> =
        abstract get: directory: string -> 'T option
        abstract set: directory: string * result: 'T -> unit

    type [<AllowNullLiteral>] ModuleResolutionCache =
        inherit PerDirectoryResolutionCache<ResolvedModuleWithFailedLookupLocations>
        inherit NonRelativeModuleNameResolutionCache
        inherit PackageJsonInfoCache
        abstract getPackageJsonInfoCache: unit -> PackageJsonInfoCache

    /// Stored map from non-relative module name to a table: directory -> result of module lookup in this directory
    /// We support only non-relative module names because resolution of relative module names is usually more deterministic and thus less expensive.
    type [<AllowNullLiteral>] NonRelativeModuleNameResolutionCache =
        inherit NonRelativeNameResolutionCache<ResolvedModuleWithFailedLookupLocations>
        inherit PackageJsonInfoCache

    type [<AllowNullLiteral>] PackageJsonInfoCache =
        abstract clear: unit -> unit

    type PerModuleNameCache =
        PerNonRelativeNameCache<ResolvedModuleWithFailedLookupLocations>

    type [<AllowNullLiteral>] FormatDiagnosticsHost =
        abstract getCurrentDirectory: unit -> string
        abstract getCanonicalFileName: fileName: string -> string
        abstract getNewLine: unit -> string

    type [<AllowNullLiteral>] EmitOutput =
        abstract outputFiles: OutputFile[] with get, set
        abstract emitSkipped: bool with get, set

    type [<AllowNullLiteral>] OutputFile =
        abstract name: string with get, set
        abstract writeByteOrderMark: bool with get, set
        abstract text: string with get, set

    type AffectedFileResult<'T> =
        {| result: 'T; affected: U2<SourceFile, Program> |} option

    type [<AllowNullLiteral>] BuilderProgramHost =
        /// If provided this would be used this hash instead of actual file shape text for detecting changes
        abstract createHash: data: string -> string
        /// When emit or emitNextAffectedFile are called without writeFile,
        /// this callback if present would be used to write files
        abstract writeFile: WriteFileCallback option with get, set

    /// Builder to manage the program state changes
    type [<AllowNullLiteral>] BuilderProgram =
        /// Returns current program
        abstract getProgram: unit -> Program
        /// Get compiler options of the program
        abstract getCompilerOptions: unit -> CompilerOptions
        /// Get the source file in the program with file name
        abstract getSourceFile: fileName: string -> SourceFile option
        /// Get a list of files in the program
        abstract getSourceFiles: unit -> SourceFile[]
        /// Get the diagnostics for compiler options
        abstract getOptionsDiagnostics: ?cancellationToken: CancellationToken -> Diagnostic[]
        /// Get the diagnostics that dont belong to any file
        abstract getGlobalDiagnostics: ?cancellationToken: CancellationToken -> Diagnostic[]
        /// Get the diagnostics from config file parsing
        abstract getConfigFileParsingDiagnostics: unit -> Diagnostic[]
        /// Get the syntax diagnostics, for all source files if source file is not supplied
        abstract getSyntacticDiagnostics: ?sourceFile: SourceFile * ?cancellationToken: CancellationToken -> Diagnostic[]
        /// Get the declaration diagnostics, for all source files if source file is not supplied
        abstract getDeclarationDiagnostics: ?sourceFile: SourceFile * ?cancellationToken: CancellationToken -> DiagnosticWithLocation[]
        /// Get all the dependencies of the file
        abstract getAllDependencies: sourceFile: SourceFile -> string[]
        /// Gets the semantic diagnostics from the program corresponding to this state of file (if provided) or whole program
        /// The semantic diagnostics are cached and managed here
        /// Note that it is assumed that when asked about semantic diagnostics through this API,
        /// the file has been taken out of affected files so it is safe to use cache or get from program and cache the diagnostics
        /// In case of SemanticDiagnosticsBuilderProgram if the source file is not provided,
        /// it will iterate through all the affected files, to ensure that cache stays valid and yet provide a way to get all semantic diagnostics
        abstract getSemanticDiagnostics: ?sourceFile: SourceFile * ?cancellationToken: CancellationToken -> Diagnostic[]
        /// Emits the JavaScript and declaration files.
        /// When targetSource file is specified, emits the files corresponding to that source file,
        /// otherwise for the whole program.
        /// In case of EmitAndSemanticDiagnosticsBuilderProgram, when targetSourceFile is specified,
        /// it is assumed that that file is handled from affected file list. If targetSourceFile is not specified,
        /// it will only emit all the affected files instead of whole program
        /// 
        /// The first of writeFile if provided, writeFile of BuilderProgramHost if provided, writeFile of compiler host
        /// in that order would be used to write the files
        abstract emit: ?targetSourceFile: SourceFile * ?writeFile: WriteFileCallback * ?cancellationToken: CancellationToken * ?emitOnlyDtsFiles: bool * ?customTransformers: CustomTransformers -> EmitResult
        /// Get the current directory of the program
        abstract getCurrentDirectory: unit -> string

    /// The builder that caches the semantic diagnostics for the program and handles the changed files and affected files
    type [<AllowNullLiteral>] SemanticDiagnosticsBuilderProgram =
        inherit BuilderProgram
        /// Gets the semantic diagnostics from the program for the next affected file and caches it
        /// Returns undefined if the iteration is complete
        abstract getSemanticDiagnosticsOfNextAffectedFile: ?cancellationToken: CancellationToken * ?ignoreSourceFile: (SourceFile -> bool) -> AffectedFileResult<Diagnostic[]>

    /// The builder that can handle the changes in program and iterate through changed file to emit the files
    /// The semantic diagnostics are cached per file and managed by clearing for the changed/affected files
    type [<AllowNullLiteral>] EmitAndSemanticDiagnosticsBuilderProgram =
        inherit SemanticDiagnosticsBuilderProgram
        /// Emits the next affected file's emit result (EmitResult and sourceFiles emitted) or returns undefined if iteration is complete
        /// The first of writeFile if provided, writeFile of BuilderProgramHost if provided, writeFile of compiler host
        /// in that order would be used to write the files
        abstract emitNextAffectedFile: ?writeFile: WriteFileCallback * ?cancellationToken: CancellationToken * ?emitOnlyDtsFiles: bool * ?customTransformers: CustomTransformers -> AffectedFileResult<EmitResult>

    type [<AllowNullLiteral>] ReadBuildProgramHost =
        abstract useCaseSensitiveFileNames: unit -> bool
        abstract getCurrentDirectory: unit -> string
        abstract readFile: fileName: string -> string option

    type [<AllowNullLiteral>] IncrementalProgramOptions<'T when 'T :> BuilderProgram> =
        abstract rootNames: string[] with get, set
        abstract options: CompilerOptions with get, set
        abstract configFileParsingDiagnostics: Diagnostic[] option with get, set
        abstract projectReferences: ProjectReference[] option with get, set
        abstract host: CompilerHost option with get, set
        abstract createProgram: CreateProgram<'T> option with get, set

    type [<AllowNullLiteral>] WatchStatusReporter =
        [<Emit("$0($1...)")>] abstract Invoke: diagnostic: Diagnostic * newLine: string * options: CompilerOptions * ?errorCount: float -> unit

    /// Create the program with rootNames and options, if they are undefined, oldProgram and new configFile diagnostics create new program
    type [<AllowNullLiteral>] CreateProgram<'T when 'T :> BuilderProgram> =
        /// Create the program with rootNames and options, if they are undefined, oldProgram and new configFile diagnostics create new program
        [<Emit("$0($1...)")>] abstract Invoke: rootNames: string[] option * options: CompilerOptions option * ?host: CompilerHost * ?oldProgram: 'T * ?configFileParsingDiagnostics: Diagnostic[] * ?projectReferences: ProjectReference[] -> 'T

    /// Host that has watch functionality used in --watch mode
    type [<AllowNullLiteral>] WatchHost =
        /// If provided, called with Diagnostic message that informs about change in watch status
        abstract onWatchStatusChange: diagnostic: Diagnostic * newLine: string * options: CompilerOptions * ?errorCount: float -> unit
        /// Used to watch changes in source files, missing files needed to update the program or config file
        abstract watchFile: path: string * callback: FileWatcherCallback * ?pollingInterval: float * ?options: WatchOptions -> FileWatcher
        /// Used to watch resolved module's failed lookup locations, config file specs, type roots where auto type reference directives are added
        abstract watchDirectory: path: string * callback: DirectoryWatcherCallback * ?recursive: bool * ?options: WatchOptions -> FileWatcher
        /// If provided, will be used to set delayed compilation, so that multiple changes in short span are compiled together
        abstract setTimeout: callback: (obj option[] -> unit) * ms: float * [<ParamArray>] args: obj option[] -> obj option
        /// If provided, will be used to reset existing delayed compilation
        abstract clearTimeout: timeoutId: obj option -> unit

    type [<AllowNullLiteral>] ProgramHost<'T when 'T :> BuilderProgram> =
        /// Used to create the program when need for program creation or recreation detected
        abstract createProgram: CreateProgram<'T> with get, set
        abstract useCaseSensitiveFileNames: unit -> bool
        abstract getNewLine: unit -> string
        abstract getCurrentDirectory: unit -> string
        abstract getDefaultLibFileName: options: CompilerOptions -> string
        abstract getDefaultLibLocation: unit -> string
        abstract createHash: data: string -> string
        /// Use to check file presence for source files and
        /// if resolveModuleNames is not provided (complier is in charge of module resolution) then module files as well
        abstract fileExists: path: string -> bool
        /// Use to read file text for source files and
        /// if resolveModuleNames is not provided (complier is in charge of module resolution) then module files as well
        abstract readFile: path: string * ?encoding: string -> string option
        /// If provided, used for module resolution as well as to handle directory structure
        abstract directoryExists: path: string -> bool
        /// If provided, used in resolutions as well as handling directory structure
        abstract getDirectories: path: string -> string[]
        /// If provided, used to cache and handle directory structure modifications
        abstract readDirectory: path: string * ?extensions: string[] * ?exclude: string[] * ?``include``: string[] * ?depth: float -> string[]
        /// Symbol links resolution
        abstract realpath: path: string -> string
        /// If provided would be used to write log about compilation
        abstract trace: s: string -> unit
        /// If provided is used to get the environment variable
        abstract getEnvironmentVariable: name: string -> string option
        abstract resolveModuleNameLiterals: moduleLiterals: StringLiteralLike[] * containingFile: string * redirectedReference: ResolvedProjectReference option * options: CompilerOptions * containingSourceFile: SourceFile * reusedNames: StringLiteralLike[] option -> ResolvedModuleWithFailedLookupLocations[]
        abstract resolveTypeReferenceDirectiveReferences: typeDirectiveReferences: 'T[] * containingFile: string * redirectedReference: ResolvedProjectReference option * options: CompilerOptions * containingSourceFile: SourceFile option * reusedNames: 'T[] option -> ResolvedTypeReferenceDirectiveWithFailedLookupLocations[]
        /// If provided along with custom resolveModuleNames or resolveTypeReferenceDirectives, used to determine if unchanged file path needs to re-resolve modules/type reference directives
        abstract hasInvalidatedResolutions: filePath: Path -> bool
        /// <summary>Returns the module resolution cache used by a provided <c>resolveModuleNames</c> implementation so that any non-name module resolution operations (eg, package.json lookup) can reuse it</summary>
        abstract getModuleResolutionCache: unit -> ModuleResolutionCache option

    type [<AllowNullLiteral>] WatchCompilerHost<'T when 'T :> BuilderProgram> =
        inherit ProgramHost<'T>
        inherit WatchHost
        /// Instead of using output d.ts file from project reference, use its source file
        abstract useSourceOfProjectReferenceRedirect: unit -> bool
        /// If provided, use this method to get parsed command lines for referenced projects
        abstract getParsedCommandLine: fileName: string -> ParsedCommandLine option
        /// If provided, callback to invoke after every new program creation
        abstract afterProgramCreate: program: 'T -> unit

    /// Host to create watch with root files and options
    type [<AllowNullLiteral>] WatchCompilerHostOfFilesAndCompilerOptions<'T when 'T :> BuilderProgram> =
        inherit WatchCompilerHost<'T>
        /// root files to use to generate program
        abstract rootFiles: string[] with get, set
        /// Compiler options
        abstract options: CompilerOptions with get, set
        abstract watchOptions: WatchOptions option with get, set
        /// Project References
        abstract projectReferences: ProjectReference[] option with get, set

    /// Host to create watch with config file
    type [<AllowNullLiteral>] WatchCompilerHostOfConfigFile<'T when 'T :> BuilderProgram> =
        inherit WatchCompilerHost<'T>
        inherit ConfigFileDiagnosticsReporter
        /// Name of the config file to compile
        abstract configFileName: string with get, set
        /// Options to extend
        abstract optionsToExtend: CompilerOptions option with get, set
        abstract watchOptionsToExtend: WatchOptions option with get, set
        abstract extraFileExtensions: FileExtensionInfo[] option with get, set
        /// Used to generate source file names from the config file and its include, exclude, files rules
        /// and also to cache the directory stucture
        abstract readDirectory: path: string * ?extensions: string[] * ?exclude: string[] * ?``include``: string[] * ?depth: float -> string[]

    type [<AllowNullLiteral>] Watch<'T> =
        /// Synchronize with host and get updated program
        abstract getProgram: unit -> 'T
        /// Closes the watch
        abstract close: unit -> unit

    /// Creates the watch what generates program using the config file
    type [<AllowNullLiteral>] WatchOfConfigFile<'T> =
        inherit Watch<'T>

    /// Creates the watch that generates program using the root files and compiler options
    type [<AllowNullLiteral>] WatchOfFilesAndCompilerOptions<'T> =
        inherit Watch<'T>
        /// Updates the root files in the program, only if this is not config file compilation
        abstract updateRootFileNames: fileNames: string[] -> unit

    type [<AllowNullLiteral>] BuildOptions =
        abstract dry: bool option with get, set
        abstract force: bool option with get, set
        abstract verbose: bool option with get, set
        abstract incremental: bool option with get, set
        abstract assumeChangesOnlyAffectDirectDependencies: bool option with get, set
        abstract declaration: bool option with get, set
        abstract declarationMap: bool option with get, set
        abstract emitDeclarationOnly: bool option with get, set
        abstract sourceMap: bool option with get, set
        abstract inlineSourceMap: bool option with get, set
        abstract traceResolution: bool option with get, set
        [<EmitIndexer>] abstract Item: option: string -> CompilerOptionsValue option with get, set

    type [<AllowNullLiteral>] ReportEmitErrorSummary =
        [<Emit("$0($1...)")>] abstract Invoke: errorCount: float * filesInError: ReportFileInError option[] -> unit

    type [<AllowNullLiteral>] ReportFileInError =
        abstract fileName: string with get, set
        abstract line: float with get, set

    type [<AllowNullLiteral>] SolutionBuilderHostBase<'T when 'T :> BuilderProgram> =
        inherit ProgramHost<'T>
        abstract createDirectory: path: string -> unit
        /// Should provide create directory and writeFile if done of invalidatedProjects is not invoked with
        /// writeFileCallback
        abstract writeFile: path: string * data: string * ?writeByteOrderMark: bool -> unit
        abstract getCustomTransformers: project: string -> CustomTransformers option
        abstract getModifiedTime: fileName: string -> DateTime option
        abstract setModifiedTime: fileName: string * date: DateTime -> unit
        abstract deleteFile: fileName: string -> unit
        abstract getParsedCommandLine: fileName: string -> ParsedCommandLine option
        abstract reportDiagnostic: DiagnosticReporter with get, set
        abstract reportSolutionBuilderStatus: DiagnosticReporter with get, set
        abstract afterProgramEmitAndDiagnostics: program: 'T -> unit

    type [<AllowNullLiteral>] SolutionBuilderHost<'T when 'T :> BuilderProgram> =
        inherit SolutionBuilderHostBase<'T>
        abstract reportErrorSummary: ReportEmitErrorSummary option with get, set

    type [<AllowNullLiteral>] SolutionBuilderWithWatchHost<'T when 'T :> BuilderProgram> =
        inherit SolutionBuilderHostBase<'T>
        inherit WatchHost

    type [<AllowNullLiteral>] SolutionBuilder<'T when 'T :> BuilderProgram> =
        abstract build: ?project: string * ?cancellationToken: CancellationToken * ?writeFile: WriteFileCallback * ?getCustomTransformers: (string -> CustomTransformers) -> ExitStatus
        abstract clean: ?project: string -> ExitStatus
        abstract buildReferences: project: string * ?cancellationToken: CancellationToken * ?writeFile: WriteFileCallback * ?getCustomTransformers: (string -> CustomTransformers) -> ExitStatus
        abstract cleanReferences: ?project: string -> ExitStatus
        abstract getNextInvalidatedProject: ?cancellationToken: CancellationToken -> InvalidatedProject<'T> option

    type [<RequireQualifiedAccess>] InvalidatedProjectKind =
        | Build = 0
        /// <deprecated />
        | UpdateBundle = 1
        | UpdateOutputFileStamps = 2

    type [<AllowNullLiteral>] InvalidatedProjectBase =
        abstract kind: InvalidatedProjectKind
        abstract project: ResolvedConfigFileName
        /// To dispose this project and ensure that all the necessary actions are taken and state is updated accordingly
        abstract ``done``: ?cancellationToken: CancellationToken * ?writeFile: WriteFileCallback * ?customTransformers: CustomTransformers -> ExitStatus
        abstract getCompilerOptions: unit -> CompilerOptions
        abstract getCurrentDirectory: unit -> string

    type [<AllowNullLiteral>] UpdateOutputFileStampsProject =
        inherit InvalidatedProjectBase
        abstract kind: InvalidatedProjectKind
        abstract updateOutputFileStatmps: unit -> unit

    type [<AllowNullLiteral>] BuildInvalidedProject<'T when 'T :> BuilderProgram> =
        inherit InvalidatedProjectBase
        abstract kind: InvalidatedProjectKind
        abstract getBuilderProgram: unit -> 'T option
        abstract getProgram: unit -> Program option
        abstract getSourceFile: fileName: string -> SourceFile option
        abstract getSourceFiles: unit -> SourceFile[]
        abstract getOptionsDiagnostics: ?cancellationToken: CancellationToken -> Diagnostic[]
        abstract getGlobalDiagnostics: ?cancellationToken: CancellationToken -> Diagnostic[]
        abstract getConfigFileParsingDiagnostics: unit -> Diagnostic[]
        abstract getSyntacticDiagnostics: ?sourceFile: SourceFile * ?cancellationToken: CancellationToken -> Diagnostic[]
        abstract getAllDependencies: sourceFile: SourceFile -> string[]
        abstract getSemanticDiagnostics: ?sourceFile: SourceFile * ?cancellationToken: CancellationToken -> Diagnostic[]
        abstract getSemanticDiagnosticsOfNextAffectedFile: ?cancellationToken: CancellationToken * ?ignoreSourceFile: (SourceFile -> bool) -> AffectedFileResult<Diagnostic[]>
        abstract emit: ?targetSourceFile: SourceFile * ?writeFile: WriteFileCallback * ?cancellationToken: CancellationToken * ?emitOnlyDtsFiles: bool * ?customTransformers: CustomTransformers -> EmitResult option

    [<Obsolete("")>]
    type [<AllowNullLiteral>] UpdateBundleProject<'T when 'T :> BuilderProgram> =
        inherit InvalidatedProjectBase
        abstract kind: InvalidatedProjectKind
        abstract emit: ?writeFile: WriteFileCallback * ?customTransformers: CustomTransformers -> U2<EmitResult, BuildInvalidedProject<'T>> option

    type InvalidatedProject<'T when 'T :> BuilderProgram> =
        U3<UpdateOutputFileStampsProject, BuildInvalidedProject<'T>, UpdateBundleProject<'T>>

    module JsTyping =

        type [<AllowNullLiteral>] TypingResolutionHost =
            abstract directoryExists: path: string -> bool
            abstract fileExists: fileName: string -> bool
            abstract readFile: path: string * ?encoding: string -> string option
            abstract readDirectory: rootDir: string * extensions: string[] * excludes: string[] option * includes: string[] option * ?depth: float -> string[]

    module Server =

        type ActionSet =
            string

        type ActionInvalidate =
            string

        type ActionPackageInstalled =
            string

        type EventTypesRegistry =
            string

        type EventBeginInstallTypes =
            string

        type EventEndInstallTypes =
            string

        type EventInitializationFailed =
            string

        type ActionWatchTypingLocations =
            string

        type [<AllowNullLiteral>] TypingInstallerResponse =
            abstract kind: U8<ActionSet, ActionInvalidate, EventTypesRegistry, ActionPackageInstalled, EventBeginInstallTypes, EventEndInstallTypes, EventInitializationFailed, ActionWatchTypingLocations>

        type [<AllowNullLiteral>] TypingInstallerRequestWithProjectName =
            abstract projectName: string

        type [<AllowNullLiteral>] DiscoverTypings =
            inherit TypingInstallerRequestWithProjectName
            abstract fileNames: string[]
            abstract projectRootPath: Path
            abstract compilerOptions: CompilerOptions
            abstract typeAcquisition: TypeAcquisition
            abstract unresolvedImports: SortedReadonlyArray<string>
            abstract cachePath: string option
            abstract kind: string

        type [<AllowNullLiteral>] CloseProject =
            inherit TypingInstallerRequestWithProjectName
            abstract kind: string

        type [<AllowNullLiteral>] TypesRegistryRequest =
            abstract kind: string

        type [<AllowNullLiteral>] InstallPackageRequest =
            inherit TypingInstallerRequestWithProjectName
            abstract kind: string
            abstract fileName: Path
            abstract packageName: string
            abstract projectRootPath: Path

        type [<AllowNullLiteral>] PackageInstalledResponse =
            inherit ProjectResponse
            abstract kind: ActionPackageInstalled
            abstract success: bool
            abstract message: string

        type [<AllowNullLiteral>] InitializationFailedResponse =
            inherit TypingInstallerResponse
            abstract kind: EventInitializationFailed
            abstract message: string
            abstract stack: string option

        type [<AllowNullLiteral>] ProjectResponse =
            inherit TypingInstallerResponse
            abstract projectName: string

        type [<AllowNullLiteral>] InvalidateCachedTypings =
            inherit ProjectResponse
            abstract kind: ActionInvalidate

        type [<AllowNullLiteral>] InstallTypes =
            inherit ProjectResponse
            abstract kind: U2<EventBeginInstallTypes, EventEndInstallTypes>
            abstract eventId: float
            abstract typingsInstallerVersion: string
            abstract packagesToInstall: string[]

        type [<AllowNullLiteral>] BeginInstallTypes =
            inherit InstallTypes
            abstract kind: EventBeginInstallTypes

        type [<AllowNullLiteral>] EndInstallTypes =
            inherit InstallTypes
            abstract kind: EventEndInstallTypes
            abstract installSuccess: bool

        type [<AllowNullLiteral>] InstallTypingHost =
            inherit JsTyping.TypingResolutionHost
            abstract useCaseSensitiveFileNames: bool with get, set
            abstract writeFile: path: string * content: string -> unit
            abstract createDirectory: path: string -> unit
            abstract getCurrentDirectory: unit -> string

        type [<AllowNullLiteral>] SetTypings =
            inherit ProjectResponse
            abstract typeAcquisition: TypeAcquisition
            abstract compilerOptions: CompilerOptions
            abstract typings: string[]
            abstract unresolvedImports: SortedReadonlyArray<string>
            abstract kind: ActionSet

        type [<AllowNullLiteral>] WatchTypingLocations =
            inherit ProjectResponse
            /// if files is undefined, retain same set of watchers
            abstract files: string[] option
            abstract kind: ActionWatchTypingLocations

    /// Represents an immutable snapshot of a script at a specified time.Once acquired, the
    /// snapshot is observably immutable. i.e. the same calls with the same parameters will return
    /// the same values.
    type [<AllowNullLiteral>] IScriptSnapshot =
        /// Gets a portion of the script snapshot specified by [start, end).
        abstract getText: start: float * ``end``: float -> string
        /// Gets the length of this script snapshot.
        abstract getLength: unit -> float
        /// Gets the TextChangeRange that describe how the text changed between this text and
        /// an older version.  This information is used by the incremental parser to determine
        /// what sections of the script need to be re-parsed.  'undefined' can be returned if the
        /// change range cannot be determined.  However, in that case, incremental parsing will
        /// not happen and the entire document will be re - parsed.
        abstract getChangeRange: oldSnapshot: IScriptSnapshot -> TextChangeRange option
        /// Releases all resources held by this script snapshot
        abstract dispose: unit -> unit

    module ScriptSnapshot =

        type [<AllowNullLiteral>] IExports =
            abstract fromString: text: string -> IScriptSnapshot

    type [<AllowNullLiteral>] PreProcessedFileInfo =
        abstract referencedFiles: FileReference[] with get, set
        abstract typeReferenceDirectives: FileReference[] with get, set
        abstract libReferenceDirectives: FileReference[] with get, set
        abstract importedFiles: FileReference[] with get, set
        abstract ambientExternalModules: string[] option with get, set
        abstract isLibFile: bool with get, set

    type [<AllowNullLiteral>] HostCancellationToken =
        abstract isCancellationRequested: unit -> bool

    type [<AllowNullLiteral>] InstallPackageOptions =
        abstract fileName: Path with get, set
        abstract packageName: string with get, set

    type [<AllowNullLiteral>] PerformanceEvent =
        abstract kind: PerformanceEventKind with get, set
        abstract durationMs: float with get, set

    type [<RequireQualifiedAccess>] LanguageServiceMode =
        | Semantic = 0
        | PartialSemantic = 1
        | Syntactic = 2

    type [<AllowNullLiteral>] IncompleteCompletionsCache =
        abstract get: unit -> CompletionInfo option
        abstract set: response: CompletionInfo -> unit
        abstract clear: unit -> unit

    type [<AllowNullLiteral>] LanguageServiceHost =
        inherit GetEffectiveTypeRootsHost
        inherit MinimalResolutionCacheHost
        abstract getCompilationSettings: unit -> CompilerOptions
        abstract getNewLine: unit -> string
        abstract getProjectVersion: unit -> string
        abstract getScriptFileNames: unit -> string[]
        abstract getScriptKind: fileName: string -> ScriptKind
        abstract getScriptVersion: fileName: string -> string
        abstract getScriptSnapshot: fileName: string -> IScriptSnapshot option
        abstract getProjectReferences: unit -> ProjectReference[] option
        abstract getLocalizedDiagnosticMessages: unit -> obj option
        abstract getCancellationToken: unit -> HostCancellationToken
        abstract getCurrentDirectory: unit -> string
        abstract getDefaultLibFileName: options: CompilerOptions -> string
        abstract log: s: string -> unit
        abstract trace: s: string -> unit
        abstract error: s: string -> unit
        abstract useCaseSensitiveFileNames: unit -> bool
        abstract readDirectory: path: string * ?extensions: string[] * ?exclude: string[] * ?``include``: string[] * ?depth: float -> string[]
        /// Resolve a symbolic link.
        abstract realpath: path: string -> string
        abstract readFile: path: string * ?encoding: string -> string option
        abstract fileExists: path: string -> bool
        abstract getTypeRootsVersion: unit -> float
        abstract getResolvedModuleWithFailedLookupLocationsFromCache: modulename: string * containingFile: string * ?resolutionMode: ResolutionMode -> ResolvedModuleWithFailedLookupLocations option
        abstract resolveModuleNameLiterals: moduleLiterals: StringLiteralLike[] * containingFile: string * redirectedReference: ResolvedProjectReference option * options: CompilerOptions * containingSourceFile: SourceFile * reusedNames: StringLiteralLike[] option -> ResolvedModuleWithFailedLookupLocations[]
        abstract resolveTypeReferenceDirectiveReferences: typeDirectiveReferences: 'T[] * containingFile: string * redirectedReference: ResolvedProjectReference option * options: CompilerOptions * containingSourceFile: SourceFile option * reusedNames: 'T[] option -> ResolvedTypeReferenceDirectiveWithFailedLookupLocations[]
        abstract getDirectories: directoryName: string -> string[]
        /// Gets a set of custom transformers to use during emit.
        abstract getCustomTransformers: unit -> CustomTransformers option
        abstract isKnownTypesPackageName: name: string -> bool
        abstract installPackage: options: InstallPackageOptions -> Promise<ApplyCodeActionCommandResult>
        abstract writeFile: fileName: string * content: string -> unit
        abstract getParsedCommandLine: fileName: string -> ParsedCommandLine option

    type [<AllowNullLiteral>] WithMetadata<'T> =
        interface end

    type [<StringEnum>] [<RequireQualifiedAccess>] SemanticClassificationFormat =
        | Original
        | [<CompiledName("2020")>] TwentyTwenty

    type [<AllowNullLiteral>] LanguageService =
        /// This is used as a part of restarting the language service.
        abstract cleanupSemanticCache: unit -> unit
        /// <summary>
        /// Gets errors indicating invalid syntax in a file.
        /// 
        /// In English, "this cdeo have, erorrs" is syntactically invalid because it has typos,
        /// grammatical errors, and misplaced punctuation. Likewise, examples of syntax
        /// errors in TypeScript are missing parentheses in an <c>if</c> statement, mismatched
        /// curly braces, and using a reserved keyword as a variable name.
        /// 
        /// These diagnostics are inexpensive to compute and don't require knowledge of
        /// other files. Note that a non-empty result increases the likelihood of false positives
        /// from <c>getSemanticDiagnostics</c>.
        /// 
        /// While these represent the majority of syntax-related diagnostics, there are some
        /// that require the type system, which will be present in <c>getSemanticDiagnostics</c>.
        /// </summary>
        /// <param name="fileName">A path to the file you want syntactic diagnostics for</param>
        abstract getSyntacticDiagnostics: fileName: string -> DiagnosticWithLocation[]
        /// <summary>
        /// Gets warnings or errors indicating type system issues in a given file.
        /// Requesting semantic diagnostics may start up the type system and
        /// run deferred work, so the first call may take longer than subsequent calls.
        /// 
        /// Unlike the other get*Diagnostics functions, these diagnostics can potentially not
        /// include a reference to a source file. Specifically, the first time this is called,
        /// it will return global diagnostics with no associated location.
        /// 
        /// To contrast the differences between semantic and syntactic diagnostics, consider the
        /// sentence: "The sun is green." is syntactically correct; those are real English words with
        /// correct sentence structure. However, it is semantically invalid, because it is not true.
        /// </summary>
        /// <param name="fileName">A path to the file you want semantic diagnostics for</param>
        abstract getSemanticDiagnostics: fileName: string -> Diagnostic[]
        /// <summary>
        /// Gets suggestion diagnostics for a specific file. These diagnostics tend to
        /// proactively suggest refactors, as opposed to diagnostics that indicate
        /// potentially incorrect runtime behavior.
        /// </summary>
        /// <param name="fileName">A path to the file you want semantic diagnostics for</param>
        abstract getSuggestionDiagnostics: fileName: string -> DiagnosticWithLocation[]
        /// Gets global diagnostics related to the program configuration and compiler options.
        abstract getCompilerOptionsDiagnostics: unit -> Diagnostic[]
        abstract getSyntacticClassifications: fileName: string * span: TextSpan * format: SemanticClassificationFormat -> U2<ClassifiedSpan[], ClassifiedSpan2020[]>
        abstract getSemanticClassifications: fileName: string * span: TextSpan * format: SemanticClassificationFormat -> U2<ClassifiedSpan[], ClassifiedSpan2020[]>
        /// Encoded as triples of [start, length, ClassificationType].
        abstract getEncodedSyntacticClassifications: fileName: string * span: TextSpan -> Classifications
        /// <summary>
        /// Gets semantic highlights information for a particular file. Has two formats, an older
        /// version used by VS and a format used by VS Code.
        /// </summary>
        /// <param name="fileName">The path to the file</param>
        /// <param name="position">A text span to return results within</param>
        /// <param name="format">Which format to use, defaults to "original"</param>
        /// <returns>a number array encoded as triples of [start, length, ClassificationType, ...].</returns>
        abstract getEncodedSemanticClassifications: fileName: string * span: TextSpan * ?format: SemanticClassificationFormat -> Classifications
        /// <summary>Gets completion entries at a particular position in a file.</summary>
        /// <param name="fileName">The path to the file</param>
        /// <param name="position">A zero-based index of the character where you want the entries</param>
        /// <param name="options">
        /// An object describing how the request was triggered and what kinds
        /// of code actions can be returned with the completions.
        /// </param>
        /// <param name="formattingSettings">settings needed for calling formatting functions.</param>
        abstract getCompletionsAtPosition: fileName: string * position: float * options: GetCompletionsAtPositionOptions option * ?formattingSettings: FormatCodeSettings -> WithMetadata<CompletionInfo> option
        /// <summary>Gets the extended details for a completion entry retrieved from <c>getCompletionsAtPosition</c>.</summary>
        /// <param name="fileName">The path to the file</param>
        /// <param name="position">A zero based index of the character where you want the entries</param>
        /// <param name="entryName">The <c>name</c> from an existing completion which came from <c>getCompletionsAtPosition</c></param>
        /// <param name="formatOptions">How should code samples in the completions be formatted, can be undefined for backwards compatibility</param>
        /// <param name="source"><c>source</c> property from the completion entry</param>
        /// <param name="preferences">User settings, can be undefined for backwards compatibility</param>
        /// <param name="data"><c>data</c> property from the completion entry</param>
        abstract getCompletionEntryDetails: fileName: string * position: float * entryName: string * formatOptions: U2<FormatCodeOptions, FormatCodeSettings> option * source: string option * preferences: UserPreferences option * data: CompletionEntryData option -> CompletionEntryDetails option
        abstract getCompletionEntrySymbol: fileName: string * position: float * name: string * source: string option -> Symbol option
        /// <summary>
        /// Gets semantic information about the identifier at a particular position in a
        /// file. Quick info is what you typically see when you hover in an editor.
        /// </summary>
        /// <param name="fileName">The path to the file</param>
        /// <param name="position">A zero-based index of the character where you want the quick info</param>
        abstract getQuickInfoAtPosition: fileName: string * position: float -> QuickInfo option
        abstract getNameOrDottedNameSpan: fileName: string * startPos: float * endPos: float -> TextSpan option
        abstract getBreakpointStatementAtPosition: fileName: string * position: float -> TextSpan option
        abstract getSignatureHelpItems: fileName: string * position: float * options: SignatureHelpItemsOptions option -> SignatureHelpItems option
        abstract getRenameInfo: fileName: string * position: float * preferences: UserPreferences -> RenameInfo
        abstract findRenameLocations: fileName: string * position: float * findInStrings: bool * findInComments: bool * preferences: UserPreferences -> RenameLocation[] option
        abstract getSmartSelectionRange: fileName: string * position: float -> SelectionRange
        abstract getDefinitionAtPosition: fileName: string * position: float -> DefinitionInfo[] option
        abstract getDefinitionAndBoundSpan: fileName: string * position: float -> DefinitionInfoAndBoundSpan option
        abstract getTypeDefinitionAtPosition: fileName: string * position: float -> DefinitionInfo[] option
        abstract getImplementationAtPosition: fileName: string * position: float -> ImplementationLocation[] option
        abstract getReferencesAtPosition: fileName: string * position: float -> ReferenceEntry[] option
        abstract findReferences: fileName: string * position: float -> ReferencedSymbol[] option
        abstract getDocumentHighlights: fileName: string * position: float * filesToSearch: string[] -> DocumentHighlights[] option
        abstract getFileReferences: fileName: string -> ReferenceEntry[]
        abstract getNavigateToItems: searchValue: string * ?maxResultCount: float * ?fileName: string * ?excludeDtsFiles: bool -> NavigateToItem[]
        abstract getNavigationBarItems: fileName: string -> NavigationBarItem[]
        abstract getNavigationTree: fileName: string -> NavigationTree
        abstract prepareCallHierarchy: fileName: string * position: float -> U2<CallHierarchyItem, CallHierarchyItem[]> option
        abstract provideCallHierarchyIncomingCalls: fileName: string * position: float -> CallHierarchyIncomingCall[]
        abstract provideCallHierarchyOutgoingCalls: fileName: string * position: float -> CallHierarchyOutgoingCall[]
        abstract provideInlayHints: fileName: string * span: TextSpan * preferences: UserPreferences option -> InlayHint[]
        abstract getOutliningSpans: fileName: string -> OutliningSpan[]
        abstract getTodoComments: fileName: string * descriptors: TodoCommentDescriptor[] -> TodoComment[]
        abstract getBraceMatchingAtPosition: fileName: string * position: float -> TextSpan[]
        abstract getIndentationAtPosition: fileName: string * position: float * options: U2<EditorOptions, EditorSettings> -> float
        abstract getFormattingEditsForRange: fileName: string * start: float * ``end``: float * options: U2<FormatCodeOptions, FormatCodeSettings> -> TextChange[]
        abstract getFormattingEditsForDocument: fileName: string * options: U2<FormatCodeOptions, FormatCodeSettings> -> TextChange[]
        abstract getFormattingEditsAfterKeystroke: fileName: string * position: float * key: string * options: U2<FormatCodeOptions, FormatCodeSettings> -> TextChange[]
        abstract getDocCommentTemplateAtPosition: fileName: string * position: float * ?options: DocCommentTemplateOptions * ?formatOptions: FormatCodeSettings -> TextInsertion option
        abstract isValidBraceCompletionAtPosition: fileName: string * position: float * openingBrace: float -> bool
        /// <summary>
        /// This will return a defined result if the position is after the <c>&gt;</c> of the opening tag, or somewhere in the text, of a JSXElement with no closing tag.
        /// Editors should call this after <c>&gt;</c> is typed.
        /// </summary>
        abstract getJsxClosingTagAtPosition: fileName: string * position: float -> JsxClosingTagInfo option
        abstract getLinkedEditingRangeAtPosition: fileName: string * position: float -> LinkedEditingInfo option
        abstract getSpanOfEnclosingComment: fileName: string * position: float * onlyMultiLine: bool -> TextSpan option
        abstract toLineColumnOffset: fileName: string * position: float -> LineAndCharacter
        abstract getCodeFixesAtPosition: fileName: string * start: float * ``end``: float * errorCodes: float[] * formatOptions: FormatCodeSettings * preferences: UserPreferences -> CodeFixAction[]
        abstract getCombinedCodeFix: scope: CombinedCodeFixScope * fixId: LanguageServiceGetCombinedCodeFixFixId * formatOptions: FormatCodeSettings * preferences: UserPreferences -> CombinedCodeActions
        abstract applyCodeActionCommand: action: CodeActionCommand * ?formatSettings: FormatCodeSettings -> Promise<ApplyCodeActionCommandResult>
        abstract applyCodeActionCommand: action: CodeActionCommand[] * ?formatSettings: FormatCodeSettings -> Promise<ApplyCodeActionCommandResult[]>
        abstract applyCodeActionCommand: action: U2<CodeActionCommand, CodeActionCommand[]> * ?formatSettings: FormatCodeSettings -> Promise<U2<ApplyCodeActionCommandResult, ApplyCodeActionCommandResult[]>>
        /// <param name="includeInteractiveActions">
        /// Include refactor actions that require additional arguments to be
        /// passed when calling <c>getEditsForRefactor</c>. When true, clients should inspect the <c>isInteractive</c>
        /// property of each returned <c>RefactorActionInfo</c> and ensure they are able to collect the appropriate
        /// arguments for any interactive action before offering it.
        /// </param>
        abstract getApplicableRefactors: fileName: string * positionOrRange: U2<float, TextRange> * preferences: UserPreferences option * ?triggerReason: RefactorTriggerReason * ?kind: string * ?includeInteractiveActions: bool -> ApplicableRefactorInfo[]
        abstract getEditsForRefactor: fileName: string * formatOptions: FormatCodeSettings * positionOrRange: U2<float, TextRange> * refactorName: string * actionName: string * preferences: UserPreferences option * ?interactiveRefactorArguments: InteractiveRefactorArguments -> RefactorEditInfo option
        abstract getMoveToRefactoringFileSuggestions: fileName: string * positionOrRange: U2<float, TextRange> * preferences: UserPreferences option * ?triggerReason: RefactorTriggerReason * ?kind: string -> {| newFileName: string; files: string[] |}
        abstract organizeImports: args: OrganizeImportsArgs * formatOptions: FormatCodeSettings * preferences: UserPreferences option -> FileTextChanges[]
        abstract getEditsForFileRename: oldFilePath: string * newFilePath: string * formatOptions: FormatCodeSettings * preferences: UserPreferences option -> FileTextChanges[]
        abstract getEmitOutput: fileName: string * ?emitOnlyDtsFiles: bool * ?forceDtsEmit: bool -> EmitOutput
        abstract getProgram: unit -> Program option
        abstract toggleLineComment: fileName: string * textRange: TextRange -> TextChange[]
        abstract toggleMultilineComment: fileName: string * textRange: TextRange -> TextChange[]
        abstract commentSelection: fileName: string * textRange: TextRange -> TextChange[]
        abstract uncommentSelection: fileName: string * textRange: TextRange -> TextChange[]
        abstract getSupportedCodeFixes: ?fileName: string -> string[]
        abstract dispose: unit -> unit

    type [<AllowNullLiteral>] LanguageServiceGetCombinedCodeFixFixId =
        interface end

    type [<AllowNullLiteral>] JsxClosingTagInfo =
        abstract newText: string

    type [<AllowNullLiteral>] LinkedEditingInfo =
        abstract ranges: TextSpan[]
        abstract wordPattern: string option with get, set

    type [<AllowNullLiteral>] CombinedCodeFixScope =
        abstract ``type``: string with get, set
        abstract fileName: string with get, set

    type [<StringEnum>] [<RequireQualifiedAccess>] OrganizeImportsMode =
        | [<CompiledName("All")>] All
        | [<CompiledName("SortAndCombine")>] SortAndCombine
        | [<CompiledName("RemoveUnused")>] RemoveUnused

    type [<AllowNullLiteral>] OrganizeImportsArgs =
        inherit CombinedCodeFixScope
        abstract mode: OrganizeImportsMode option with get, set

    type [<StringEnum>] [<RequireQualifiedAccess>] CompletionsTriggerCharacter =
        | [<CompiledName(".")>] DOT
        | [<CompiledName("\"")>] QUOTATION
        | [<CompiledName("'")>] ``'``
        | [<CompiledName("`")>] BACKTICK
        | [<CompiledName("/")>] SLASH
        | [<CompiledName("@")>] AT
        | [<CompiledName("<")>] ``<``
        | [<CompiledName("#")>] ``#``
        | [<CompiledName(" ")>] Empty

    type [<RequireQualifiedAccess>] CompletionTriggerKind =
        /// Completion was triggered by typing an identifier, manual invocation (e.g Ctrl+Space) or via API.
        | Invoked = 1
        /// Completion was triggered by a trigger character.
        | TriggerCharacter = 2
        /// Completion was re-triggered as the current completion list is incomplete.
        | TriggerForIncompleteCompletions = 3

    type [<AllowNullLiteral>] GetCompletionsAtPositionOptions =
        inherit UserPreferences
        /// If the editor is asking for completions because a certain character was typed
        /// (as opposed to when the user explicitly requested them) this should be set.
        abstract triggerCharacter: CompletionsTriggerCharacter option with get, set
        abstract triggerKind: CompletionTriggerKind option with get, set
        /// <summary>
        /// Include a <c>symbol</c> property on each completion entry object.
        /// Symbols reference cyclic data structures and sometimes an entire TypeChecker instance,
        /// so use caution when serializing or retaining completion entries retrieved with this option.
        /// </summary>
        /// <default>false</default>
        abstract includeSymbol: bool option with get, set

    type [<StringEnum>] [<RequireQualifiedAccess>] SignatureHelpTriggerCharacter =
        | [<CompiledName(",")>] ``,``
        | [<CompiledName("(")>] ``(``
        | [<CompiledName("<")>] ``<``

    type SignatureHelpRetriggerCharacter =
        U2<SignatureHelpTriggerCharacter, string>

    type [<AllowNullLiteral>] SignatureHelpItemsOptions =
        abstract triggerReason: SignatureHelpTriggerReason option with get, set

    type SignatureHelpTriggerReason =
        U3<SignatureHelpInvokedReason, SignatureHelpCharacterTypedReason, SignatureHelpRetriggeredReason>

    /// Signals that the user manually requested signature help.
    /// The language service will unconditionally attempt to provide a result.
    type [<AllowNullLiteral>] SignatureHelpInvokedReason =
        abstract kind: string with get, set
        abstract triggerCharacter: obj option with get, set

    /// Signals that the signature help request came from a user typing a character.
    /// Depending on the character and the syntactic context, the request may or may not be served a result.
    type [<AllowNullLiteral>] SignatureHelpCharacterTypedReason =
        abstract kind: string with get, set
        /// Character that was responsible for triggering signature help.
        abstract triggerCharacter: SignatureHelpTriggerCharacter with get, set

    /// <summary>
    /// Signals that this signature help request came from typing a character or moving the cursor.
    /// This should only occur if a signature help session was already active and the editor needs to see if it should adjust.
    /// The language service will unconditionally attempt to provide a result.
    /// <c>triggerCharacter</c> can be <c>undefined</c> for a retrigger caused by a cursor move.
    /// </summary>
    type [<AllowNullLiteral>] SignatureHelpRetriggeredReason =
        abstract kind: string with get, set
        /// Character that was responsible for triggering signature help.
        abstract triggerCharacter: SignatureHelpRetriggerCharacter option with get, set

    type [<AllowNullLiteral>] ApplyCodeActionCommandResult =
        abstract successMessage: string with get, set

    type [<AllowNullLiteral>] Classifications =
        abstract spans: float[] with get, set
        abstract endOfLineState: EndOfLineState with get, set

    type [<AllowNullLiteral>] ClassifiedSpan =
        abstract textSpan: TextSpan with get, set
        abstract classificationType: ClassificationTypeNames with get, set

    type [<AllowNullLiteral>] ClassifiedSpan2020 =
        abstract textSpan: TextSpan with get, set
        abstract classificationType: float with get, set

    /// <summary>
    /// Navigation bar interface designed for visual studio's dual-column layout.
    /// This does not form a proper tree.
    /// The navbar is returned as a list of top-level items, each of which has a list of child items.
    /// Child items always have an empty array for their <c>childItems</c>.
    /// </summary>
    type [<AllowNullLiteral>] NavigationBarItem =
        abstract text: string with get, set
        abstract kind: ScriptElementKind with get, set
        abstract kindModifiers: string with get, set
        abstract spans: TextSpan[] with get, set
        abstract childItems: NavigationBarItem[] with get, set
        abstract indent: float with get, set
        abstract bolded: bool with get, set
        abstract grayed: bool with get, set

    /// Node in a tree of nested declarations in a file.
    /// The top node is always a script or module node.
    type [<AllowNullLiteral>] NavigationTree =
        /// Name of the declaration, or a short description, e.g. "<class>".
        abstract text: string with get, set
        abstract kind: ScriptElementKind with get, set
        /// ScriptElementKindModifier separated by commas, e.g. "public,abstract"
        abstract kindModifiers: string with get, set
        /// Spans of the nodes that generated this declaration.
        /// There will be more than one if this is the result of merging.
        abstract spans: TextSpan[] with get, set
        abstract nameSpan: TextSpan option with get, set
        /// Present if non-empty
        abstract childItems: NavigationTree[] option with get, set

    type [<AllowNullLiteral>] CallHierarchyItem =
        abstract name: string with get, set
        abstract kind: ScriptElementKind with get, set
        abstract kindModifiers: string option with get, set
        abstract file: string with get, set
        abstract span: TextSpan with get, set
        abstract selectionSpan: TextSpan with get, set
        abstract containerName: string option with get, set

    type [<AllowNullLiteral>] CallHierarchyIncomingCall =
        abstract from: CallHierarchyItem with get, set
        abstract fromSpans: TextSpan[] with get, set

    type [<AllowNullLiteral>] CallHierarchyOutgoingCall =
        abstract ``to``: CallHierarchyItem with get, set
        abstract fromSpans: TextSpan[] with get, set

    type [<StringEnum>] [<RequireQualifiedAccess>] InlayHintKind =
        | [<CompiledName("Type")>] Type
        | [<CompiledName("Parameter")>] Parameter
        | [<CompiledName("Enum")>] Enum

    type [<AllowNullLiteral>] InlayHint =
        abstract text: string with get, set
        abstract position: float with get, set
        abstract kind: InlayHintKind with get, set
        abstract whitespaceBefore: bool option with get, set
        abstract whitespaceAfter: bool option with get, set

    type [<AllowNullLiteral>] TodoCommentDescriptor =
        abstract text: string with get, set
        abstract priority: float with get, set

    type [<AllowNullLiteral>] TodoComment =
        abstract descriptor: TodoCommentDescriptor with get, set
        abstract message: string with get, set
        abstract position: float with get, set

    type [<AllowNullLiteral>] TextChange =
        abstract span: TextSpan with get, set
        abstract newText: string with get, set

    type [<AllowNullLiteral>] FileTextChanges =
        abstract fileName: string with get, set
        abstract textChanges: TextChange[] with get, set
        abstract isNewFile: bool option with get, set

    type [<AllowNullLiteral>] CodeAction =
        /// Description of the code action to display in the UI of the editor
        abstract description: string with get, set
        /// Text changes to apply to each file as part of the code action
        abstract changes: FileTextChanges[] with get, set
        /// <summary>
        /// If the user accepts the code fix, the editor should send the action back in a <c>applyAction</c> request.
        /// This allows the language service to have side effects (e.g. installing dependencies) upon a code fix.
        /// </summary>
        abstract commands: CodeActionCommand[] option with get, set

    type [<AllowNullLiteral>] CodeFixAction =
        inherit CodeAction
        /// Short name to identify the fix, for use by telemetry.
        abstract fixName: string with get, set
        /// If present, one may call 'getCombinedCodeFix' with this fixId.
        /// This may be omitted to indicate that the code fix can't be applied in a group.
        abstract fixId: DiagnosticMessageReportsUnnecessary option with get, set
        abstract fixAllDescription: string option with get, set

    type [<AllowNullLiteral>] CombinedCodeActions =
        abstract changes: FileTextChanges[] with get, set
        abstract commands: CodeActionCommand[] option with get, set

    type CodeActionCommand =
        InstallPackageAction

    type [<AllowNullLiteral>] InstallPackageAction =
        interface end

    /// A set of one or more available refactoring actions, grouped under a parent refactoring.
    type [<AllowNullLiteral>] ApplicableRefactorInfo =
        /// The programmatic name of the refactoring
        abstract name: string with get, set
        /// A description of this refactoring category to show to the user.
        /// If the refactoring gets inlined (see below), this text will not be visible.
        abstract description: string with get, set
        /// Inlineable refactorings can have their actions hoisted out to the top level
        /// of a context menu. Non-inlineanable refactorings should always be shown inside
        /// their parent grouping.
        /// 
        /// If not specified, this value is assumed to be 'true'
        abstract inlineable: bool option with get, set
        abstract actions: RefactorActionInfo[] with get, set

    /// Represents a single refactoring action - for example, the "Extract Method..." refactor might
    /// offer several actions, each corresponding to a surround class or closure to extract into.
    type [<AllowNullLiteral>] RefactorActionInfo =
        /// The programmatic name of the refactoring action
        abstract name: string with get, set
        /// A description of this refactoring action to show to the user.
        /// If the parent refactoring is inlined away, this will be the only text shown,
        /// so this description should make sense by itself if the parent is inlineable=true
        abstract description: string with get, set
        /// A message to show to the user if the refactoring cannot be applied in
        /// the current context.
        abstract notApplicableReason: string option with get, set
        /// The hierarchical dotted name of the refactor action.
        abstract kind: string option with get, set
        /// <summary>
        /// Indicates that the action requires additional arguments to be passed
        /// when calling <c>getEditsForRefactor</c>.
        /// </summary>
        abstract isInteractive: bool option with get, set

    /// A set of edits to make in response to a refactor action, plus an optional
    /// location where renaming should be invoked from
    type [<AllowNullLiteral>] RefactorEditInfo =
        abstract edits: FileTextChanges[] with get, set
        abstract renameFilename: string option with get, set
        abstract renameLocation: float option with get, set
        abstract commands: CodeActionCommand[] option with get, set
        abstract notApplicableReason: string option with get, set

    type [<StringEnum>] [<RequireQualifiedAccess>] RefactorTriggerReason =
        | Implicit
        | Invoked

    type [<AllowNullLiteral>] TextInsertion =
        abstract newText: string with get, set
        /// The position in newText the caret should point to after the insertion.
        abstract caretOffset: float with get, set

    type [<AllowNullLiteral>] DocumentSpan =
        abstract textSpan: TextSpan with get, set
        abstract fileName: string with get, set
        /// If the span represents a location that was remapped (e.g. via a .d.ts.map file),
        /// then the original filename and span will be specified here
        abstract originalTextSpan: TextSpan option with get, set
        abstract originalFileName: string option with get, set
        /// If DocumentSpan.textSpan is the span for name of the declaration,
        /// then this is the span for relevant declaration
        abstract contextSpan: TextSpan option with get, set
        abstract originalContextSpan: TextSpan option with get, set

    type [<AllowNullLiteral>] RenameLocation =
        inherit DocumentSpan
        abstract prefixText: string option
        abstract suffixText: string option

    type [<AllowNullLiteral>] ReferenceEntry =
        inherit DocumentSpan
        abstract isWriteAccess: bool with get, set
        abstract isInString: bool option with get, set

    type [<AllowNullLiteral>] ImplementationLocation =
        inherit DocumentSpan
        abstract kind: ScriptElementKind with get, set
        abstract displayParts: SymbolDisplayPart[] with get, set

    type [<StringEnum>] [<RequireQualifiedAccess>] HighlightSpanKind =
        | None
        | Definition
        | Reference
        | WrittenReference

    type [<AllowNullLiteral>] HighlightSpan =
        abstract fileName: string option with get, set
        abstract isInString: bool option with get, set
        abstract textSpan: TextSpan with get, set
        abstract contextSpan: TextSpan option with get, set
        abstract kind: HighlightSpanKind with get, set

    type [<AllowNullLiteral>] NavigateToItem =
        abstract name: string with get, set
        abstract kind: ScriptElementKind with get, set
        abstract kindModifiers: string with get, set
        abstract matchKind: NavigateToItemMatchKind with get, set
        abstract isCaseSensitive: bool with get, set
        abstract fileName: string with get, set
        abstract textSpan: TextSpan with get, set
        abstract containerName: string with get, set
        abstract containerKind: ScriptElementKind with get, set

    type [<RequireQualifiedAccess>] IndentStyle =
        | None = 0
        | Block = 1
        | Smart = 2

    type [<StringEnum>] [<RequireQualifiedAccess>] SemicolonPreference =
        | Ignore
        | Insert
        | Remove

    [<Obsolete("- consider using EditorSettings instead")>]
    type [<AllowNullLiteral>] EditorOptions =
        abstract BaseIndentSize: float option with get, set
        abstract IndentSize: float with get, set
        abstract TabSize: float with get, set
        abstract NewLineCharacter: string with get, set
        abstract ConvertTabsToSpaces: bool with get, set
        abstract IndentStyle: IndentStyle with get, set

    type [<AllowNullLiteral>] EditorSettings =
        abstract baseIndentSize: float option with get, set
        abstract indentSize: float option with get, set
        abstract tabSize: float option with get, set
        abstract newLineCharacter: string option with get, set
        abstract convertTabsToSpaces: bool option with get, set
        abstract indentStyle: IndentStyle option with get, set
        abstract trimTrailingWhitespace: bool option with get, set

    [<Obsolete("- consider using FormatCodeSettings instead")>]
    type [<AllowNullLiteral>] FormatCodeOptions =
        inherit EditorOptions
        abstract InsertSpaceAfterCommaDelimiter: bool with get, set
        abstract InsertSpaceAfterSemicolonInForStatements: bool with get, set
        abstract InsertSpaceBeforeAndAfterBinaryOperators: bool with get, set
        abstract InsertSpaceAfterConstructor: bool option with get, set
        abstract InsertSpaceAfterKeywordsInControlFlowStatements: bool with get, set
        abstract InsertSpaceAfterFunctionKeywordForAnonymousFunctions: bool with get, set
        abstract InsertSpaceAfterOpeningAndBeforeClosingNonemptyParenthesis: bool with get, set
        abstract InsertSpaceAfterOpeningAndBeforeClosingNonemptyBrackets: bool with get, set
        abstract InsertSpaceAfterOpeningAndBeforeClosingNonemptyBraces: bool option with get, set
        abstract InsertSpaceAfterOpeningAndBeforeClosingTemplateStringBraces: bool with get, set
        abstract InsertSpaceAfterOpeningAndBeforeClosingJsxExpressionBraces: bool option with get, set
        abstract InsertSpaceAfterTypeAssertion: bool option with get, set
        abstract InsertSpaceBeforeFunctionParenthesis: bool option with get, set
        abstract PlaceOpenBraceOnNewLineForFunctions: bool with get, set
        abstract PlaceOpenBraceOnNewLineForControlBlocks: bool with get, set
        abstract insertSpaceBeforeTypeAnnotation: bool option with get, set

    type [<AllowNullLiteral>] FormatCodeSettings =
        inherit EditorSettings
        abstract insertSpaceAfterCommaDelimiter: bool option
        abstract insertSpaceAfterSemicolonInForStatements: bool option
        abstract insertSpaceBeforeAndAfterBinaryOperators: bool option
        abstract insertSpaceAfterConstructor: bool option
        abstract insertSpaceAfterKeywordsInControlFlowStatements: bool option
        abstract insertSpaceAfterFunctionKeywordForAnonymousFunctions: bool option
        abstract insertSpaceAfterOpeningAndBeforeClosingNonemptyParenthesis: bool option
        abstract insertSpaceAfterOpeningAndBeforeClosingNonemptyBrackets: bool option
        abstract insertSpaceAfterOpeningAndBeforeClosingNonemptyBraces: bool option
        abstract insertSpaceAfterOpeningAndBeforeClosingEmptyBraces: bool option
        abstract insertSpaceAfterOpeningAndBeforeClosingTemplateStringBraces: bool option
        abstract insertSpaceAfterOpeningAndBeforeClosingJsxExpressionBraces: bool option
        abstract insertSpaceAfterTypeAssertion: bool option
        abstract insertSpaceBeforeFunctionParenthesis: bool option
        abstract placeOpenBraceOnNewLineForFunctions: bool option
        abstract placeOpenBraceOnNewLineForControlBlocks: bool option
        abstract insertSpaceBeforeTypeAnnotation: bool option
        abstract indentMultiLineObjectLiteralBeginningOnBlankLine: bool option
        abstract semicolons: SemicolonPreference option
        abstract indentSwitchCase: bool option

    type [<AllowNullLiteral>] DefinitionInfo =
        inherit DocumentSpan
        abstract kind: ScriptElementKind with get, set
        abstract name: string with get, set
        abstract containerKind: ScriptElementKind with get, set
        abstract containerName: string with get, set
        abstract unverified: bool option with get, set

    type [<AllowNullLiteral>] DefinitionInfoAndBoundSpan =
        abstract definitions: DefinitionInfo[] option with get, set
        abstract textSpan: TextSpan with get, set

    type [<AllowNullLiteral>] ReferencedSymbolDefinitionInfo =
        inherit DefinitionInfo
        abstract displayParts: SymbolDisplayPart[] with get, set

    type [<AllowNullLiteral>] ReferencedSymbol =
        abstract definition: ReferencedSymbolDefinitionInfo with get, set
        abstract references: ReferencedSymbolEntry[] with get, set

    type [<AllowNullLiteral>] ReferencedSymbolEntry =
        inherit ReferenceEntry
        abstract isDefinition: bool option with get, set

    type [<RequireQualifiedAccess>] SymbolDisplayPartKind =
        | AliasName = 0
        | ClassName = 1
        | EnumName = 2
        | FieldName = 3
        | InterfaceName = 4
        | Keyword = 5
        | LineBreak = 6
        | NumericLiteral = 7
        | StringLiteral = 8
        | LocalName = 9
        | MethodName = 10
        | ModuleName = 11
        | Operator = 12
        | ParameterName = 13
        | PropertyName = 14
        | Punctuation = 15
        | Space = 16
        | Text = 17
        | TypeParameterName = 18
        | EnumMemberName = 19
        | FunctionName = 20
        | RegularExpressionLiteral = 21
        | Link = 22
        | LinkName = 23
        | LinkText = 24

    type [<AllowNullLiteral>] SymbolDisplayPart =
        abstract text: string with get, set
        abstract kind: string with get, set

    type [<AllowNullLiteral>] JSDocLinkDisplayPart =
        inherit SymbolDisplayPart
        abstract target: DocumentSpan with get, set

    type [<AllowNullLiteral>] JSDocTagInfo =
        abstract name: string with get, set
        abstract text: SymbolDisplayPart[] option with get, set

    type [<AllowNullLiteral>] QuickInfo =
        abstract kind: ScriptElementKind with get, set
        abstract kindModifiers: string with get, set
        abstract textSpan: TextSpan with get, set
        abstract displayParts: SymbolDisplayPart[] option with get, set
        abstract documentation: SymbolDisplayPart[] option with get, set
        abstract tags: JSDocTagInfo[] option with get, set

    type RenameInfo =
        U2<RenameInfoSuccess, RenameInfoFailure>

    type [<AllowNullLiteral>] RenameInfoSuccess =
        abstract canRename: bool with get, set
        /// <summary>
        /// File or directory to rename.
        /// If set, <c>getEditsForFileRename</c> should be called instead of <c>findRenameLocations</c>.
        /// </summary>
        abstract fileToRename: string option with get, set
        abstract displayName: string with get, set
        abstract fullDisplayName: string with get, set
        abstract kind: ScriptElementKind with get, set
        abstract kindModifiers: string with get, set
        abstract triggerSpan: TextSpan with get, set

    type [<AllowNullLiteral>] RenameInfoFailure =
        abstract canRename: bool with get, set
        abstract localizedErrorMessage: string with get, set

    [<Obsolete("Use `UserPreferences` instead.")>]
    type [<AllowNullLiteral>] RenameInfoOptions =
        abstract allowRenameOfImportPath: bool option

    type [<AllowNullLiteral>] DocCommentTemplateOptions =
        abstract generateReturnInDocTemplate: bool option

    type [<AllowNullLiteral>] InteractiveRefactorArguments =
        abstract targetFile: string with get, set

    type [<AllowNullLiteral>] SignatureHelpParameter =
        abstract name: string with get, set
        abstract documentation: SymbolDisplayPart[] with get, set
        abstract displayParts: SymbolDisplayPart[] with get, set
        abstract isOptional: bool with get, set
        abstract isRest: bool option with get, set

    type [<AllowNullLiteral>] SelectionRange =
        abstract textSpan: TextSpan with get, set
        abstract parent: SelectionRange option with get, set

    /// Represents a single signature to show in signature help.
    /// The id is used for subsequent calls into the language service to ask questions about the
    /// signature help item in the context of any documents that have been updated.  i.e. after
    /// an edit has happened, while signature help is still active, the host can ask important
    /// questions like 'what parameter is the user currently contained within?'.
    type [<AllowNullLiteral>] SignatureHelpItem =
        abstract isVariadic: bool with get, set
        abstract prefixDisplayParts: SymbolDisplayPart[] with get, set
        abstract suffixDisplayParts: SymbolDisplayPart[] with get, set
        abstract separatorDisplayParts: SymbolDisplayPart[] with get, set
        abstract parameters: SignatureHelpParameter[] with get, set
        abstract documentation: SymbolDisplayPart[] with get, set
        abstract tags: JSDocTagInfo[] with get, set

    /// Represents a set of signature help items, and the preferred item that should be selected.
    type [<AllowNullLiteral>] SignatureHelpItems =
        abstract items: SignatureHelpItem[] with get, set
        abstract applicableSpan: TextSpan with get, set
        abstract selectedItemIndex: float with get, set
        abstract argumentIndex: float with get, set
        abstract argumentCount: float with get, set

    type [<RequireQualifiedAccess>] CompletionInfoFlags =
        | None = 0
        | MayIncludeAutoImports = 1
        | IsImportStatementCompletion = 2
        | IsContinuation = 4
        | ResolvedModuleSpecifiers = 8
        | ResolvedModuleSpecifiersBeyondLimit = 16
        | MayIncludeMethodSnippets = 32

    type [<AllowNullLiteral>] CompletionInfo =
        /// For performance telemetry.
        abstract flags: CompletionInfoFlags option with get, set
        /// <summary>Not true for all global completions. This will be true if the enclosing scope matches a few syntax kinds. See <c>isSnippetScope</c>.</summary>
        abstract isGlobalCompletion: bool with get, set
        abstract isMemberCompletion: bool with get, set
        /// <summary>
        /// In the absence of <c>CompletionEntry["replacementSpan"]</c>, the editor may choose whether to use
        /// this span or its default one. If <c>CompletionEntry["replacementSpan"]</c> is defined, that span
        /// must be used to commit that completion entry.
        /// </summary>
        abstract optionalReplacementSpan: TextSpan option with get, set
        /// true when the current location also allows for a new identifier
        abstract isNewIdentifierLocation: bool with get, set
        /// Indicates to client to continue requesting completions on subsequent keystrokes.
        abstract isIncomplete: bool option with get, set
        abstract entries: CompletionEntry[] with get, set

    type [<AllowNullLiteral>] CompletionEntryDataAutoImport =
        /// The name of the property or export in the module's symbol table. Differs from the completion name
        /// in the case of InternalSymbolName.ExportEquals and InternalSymbolName.Default.
        abstract exportName: string with get, set
        abstract exportMapKey: string option with get, set
        abstract moduleSpecifier: string option with get, set
        /// The file name declaring the export's module symbol, if it was an external module
        abstract fileName: string option with get, set
        /// The module name (with quotes stripped) of the export's module symbol, if it was an ambient module
        abstract ambientModuleName: string option with get, set
        /// True if the export was found in the package.json AutoImportProvider
        abstract isPackageJsonImport: bool option with get, set

    type [<AllowNullLiteral>] CompletionEntryDataUnresolved =
        inherit CompletionEntryDataAutoImport
        abstract exportMapKey: string with get, set

    type [<AllowNullLiteral>] CompletionEntryDataResolved =
        inherit CompletionEntryDataAutoImport
        abstract moduleSpecifier: string with get, set

    type CompletionEntryData =
        U2<CompletionEntryDataUnresolved, CompletionEntryDataResolved>

    type [<AllowNullLiteral>] CompletionEntry =
        abstract name: string with get, set
        abstract kind: ScriptElementKind with get, set
        abstract kindModifiers: string option with get, set
        abstract sortText: string with get, set
        abstract insertText: string option with get, set
        abstract isSnippet: bool option with get, set
        /// An optional span that indicates the text to be replaced by this completion item.
        /// If present, this span should be used instead of the default one.
        /// It will be set if the required span differs from the one generated by the default replacement behavior.
        abstract replacementSpan: TextSpan option with get, set
        abstract hasAction: bool option with get, set
        abstract source: string option with get, set
        abstract sourceDisplay: SymbolDisplayPart[] option with get, set
        abstract labelDetails: CompletionEntryLabelDetails option with get, set
        abstract isRecommended: bool option with get, set
        abstract isFromUncheckedFile: bool option with get, set
        abstract isPackageJsonImport: bool option with get, set
        abstract isImportStatementCompletion: bool option with get, set
        /// <summary>
        /// For API purposes.
        /// Included for non-string completions only when <c>includeSymbol: true</c> option is passed to <c>getCompletionsAtPosition</c>.
        /// </summary>
        /// <example>Get declaration of completion: <c>symbol.valueDeclaration</c></example>
        abstract symbol: Symbol option with get, set
        /// <summary>
        /// A property to be sent back to TS Server in the CompletionDetailsRequest, along with <c>name</c>,
        /// that allows TS Server to look up the symbol represented by the completion item, disambiguating
        /// items with the same name. Currently only defined for auto-import completions, but the type is
        /// <c>unknown</c> in the protocol, so it can be changed as needed to support other kinds of completions.
        /// The presence of this property should generally not be used to assume that this completion entry
        /// is an auto-import.
        /// </summary>
        abstract data: CompletionEntryData option with get, set

    type [<AllowNullLiteral>] CompletionEntryLabelDetails =
        abstract detail: string option with get, set
        abstract description: string option with get, set

    type [<AllowNullLiteral>] CompletionEntryDetails =
        abstract name: string with get, set
        abstract kind: ScriptElementKind with get, set
        abstract kindModifiers: string with get, set
        abstract displayParts: SymbolDisplayPart[] with get, set
        abstract documentation: SymbolDisplayPart[] option with get, set
        abstract tags: JSDocTagInfo[] option with get, set
        abstract codeActions: CodeAction[] option with get, set
        abstract sourceDisplay: SymbolDisplayPart[] option with get, set

    type [<AllowNullLiteral>] OutliningSpan =
        /// The span of the document to actually collapse.
        abstract textSpan: TextSpan with get, set
        /// The span of the document to display when the user hovers over the collapsed span.
        abstract hintSpan: TextSpan with get, set
        /// The text to display in the editor for the collapsed region.
        abstract bannerText: string with get, set
        /// Whether or not this region should be automatically collapsed when
        /// the 'Collapse to Definitions' command is invoked.
        abstract autoCollapse: bool with get, set
        /// Classification of the contents of the span
        abstract kind: OutliningSpanKind with get, set

    type [<StringEnum>] [<RequireQualifiedAccess>] OutliningSpanKind =
        /// Single or multi-line comments
        | Comment
        /// Sections marked by '// #region' and '// #endregion' comments
        | Region
        /// Declarations and expressions
        | Code
        /// Contiguous blocks of import declarations
        | Imports

    type [<RequireQualifiedAccess>] OutputFileType =
        | JavaScript = 0
        | SourceMap = 1
        | Declaration = 2

    type [<RequireQualifiedAccess>] EndOfLineState =
        | None = 0
        | InMultiLineCommentTrivia = 1
        | InSingleQuoteStringLiteral = 2
        | InDoubleQuoteStringLiteral = 3
        | InTemplateHeadOrNoSubstitutionTemplate = 4
        | InTemplateMiddleOrTail = 5
        | InTemplateSubstitutionPosition = 6

    type [<RequireQualifiedAccess>] TokenClass =
        | Punctuation = 0
        | Keyword = 1
        | Operator = 2
        | Comment = 3
        | Whitespace = 4
        | Identifier = 5
        | NumberLiteral = 6
        | BigIntLiteral = 7
        | StringLiteral = 8
        | RegExpLiteral = 9

    type [<AllowNullLiteral>] ClassificationResult =
        abstract finalLexState: EndOfLineState with get, set
        abstract entries: ClassificationInfo[] with get, set

    type [<AllowNullLiteral>] ClassificationInfo =
        abstract length: float with get, set
        abstract classification: TokenClass with get, set

    type [<AllowNullLiteral>] Classifier =
        abstract getEncodedLexicalClassifications: text: string * endOfLineState: EndOfLineState * syntacticClassifierAbsent: bool -> Classifications

    type [<StringEnum>] [<RequireQualifiedAccess>] ScriptElementKind =
        | [<CompiledName("")>] Unknown
        | Warning
        /// predefined type (void) or keyword (class)
        | Keyword
        /// top level script node
        | [<CompiledName("script")>] ScriptElement
        /// module foo {}
        | [<CompiledName("module")>] ModuleElement
        /// class X {}
        | [<CompiledName("class")>] ClassElement
        /// var x = class X {}
        | [<CompiledName("local class")>] LocalClassElement
        /// interface Y {}
        | [<CompiledName("interface")>] InterfaceElement
        /// type T = ...
        | [<CompiledName("type")>] TypeElement
        /// enum E
        | [<CompiledName("enum")>] EnumElement
        | [<CompiledName("enum member")>] EnumMemberElement
        /// Inside module and script only
        /// const v = ..
        | [<CompiledName("var")>] VariableElement
        /// Inside function
        | [<CompiledName("local var")>] LocalVariableElement
        /// Inside module and script only
        /// function f() { }
        | [<CompiledName("function")>] FunctionElement
        /// Inside function
        | [<CompiledName("local function")>] LocalFunctionElement
        /// class X { [public|private]* foo() {} }
        | [<CompiledName("method")>] MemberFunctionElement
        /// class X { [public|private]* [get|set] foo:number; }
        | [<CompiledName("getter")>] MemberGetAccessorElement
        | [<CompiledName("setter")>] MemberSetAccessorElement
        /// class X { [public|private]* foo:number; }
        /// interface Y { foo:number; }
        | [<CompiledName("property")>] MemberVariableElement
        /// class X { [public|private]* accessor foo: number; }
        | [<CompiledName("accessor")>] MemberAccessorVariableElement
        /// class X { constructor() { } }
        /// class X { static { } }
        | [<CompiledName("constructor")>] ConstructorImplementationElement
        /// interface Y { ():number; }
        | [<CompiledName("call")>] CallSignatureElement
        /// interface Y { []:number; }
        | [<CompiledName("index")>] IndexSignatureElement
        /// interface Y { new():Y; }
        | [<CompiledName("construct")>] ConstructSignatureElement
        /// function foo(*Y*: string)
        | [<CompiledName("parameter")>] ParameterElement
        | [<CompiledName("type parameter")>] TypeParameterElement
        | [<CompiledName("primitive type")>] PrimitiveType
        | Label
        | Alias
        | [<CompiledName("const")>] ConstElement
        | [<CompiledName("let")>] LetElement
        | Directory
        | [<CompiledName("external module name")>] ExternalModuleName
        /// <summary>&lt;JsxTagName attribute1 attribute2={0} /&gt;</summary>
        /// <deprecated />
        | [<CompiledName("JSX attribute")>] JsxAttribute
        /// String literal
        | String
        /// <summary>Jsdoc @link: in <c>{@link C link text}</c>, the before and after text "<see cref=""">and "</see>"</summary>
        | Link
        /// <summary>Jsdoc @link: in <c>{@link C link text}</c>, the entity name "C"</summary>
        | [<CompiledName("link name")>] LinkName
        /// <summary>Jsdoc @link: in <c>{@link C link text}</c>, the link text "link text"</summary>
        | [<CompiledName("link text")>] LinkText

    type [<StringEnum>] [<RequireQualifiedAccess>] ScriptElementKindModifier =
        | [<CompiledName("")>] None
        | [<CompiledName("public")>] PublicMemberModifier
        | [<CompiledName("private")>] PrivateMemberModifier
        | [<CompiledName("protected")>] ProtectedMemberModifier
        | [<CompiledName("export")>] ExportedModifier
        | [<CompiledName("declare")>] AmbientModifier
        | [<CompiledName("static")>] StaticModifier
        | [<CompiledName("abstract")>] AbstractModifier
        | [<CompiledName("optional")>] OptionalModifier
        | [<CompiledName("deprecated")>] DeprecatedModifier
        | [<CompiledName(".d.ts")>] DtsModifier
        | [<CompiledName(".ts")>] TsModifier
        | [<CompiledName(".tsx")>] TsxModifier
        | [<CompiledName(".js")>] JsModifier
        | [<CompiledName(".jsx")>] JsxModifier
        | [<CompiledName(".json")>] JsonModifier
        | [<CompiledName(".d.mts")>] DmtsModifier
        | [<CompiledName(".mts")>] MtsModifier
        | [<CompiledName(".mjs")>] MjsModifier
        | [<CompiledName(".d.cts")>] DctsModifier
        | [<CompiledName(".cts")>] CtsModifier
        | [<CompiledName(".cjs")>] CjsModifier

    type [<StringEnum>] [<RequireQualifiedAccess>] ClassificationTypeNames =
        | Comment
        | Identifier
        | Keyword
        | [<CompiledName("number")>] NumericLiteral
        | [<CompiledName("bigint")>] BigintLiteral
        | Operator
        | [<CompiledName("string")>] StringLiteral
        | [<CompiledName("whitespace")>] WhiteSpace
        | Text
        | Punctuation
        | [<CompiledName("class name")>] ClassName
        | [<CompiledName("enum name")>] EnumName
        | [<CompiledName("interface name")>] InterfaceName
        | [<CompiledName("module name")>] ModuleName
        | [<CompiledName("type parameter name")>] TypeParameterName
        | [<CompiledName("type alias name")>] TypeAliasName
        | [<CompiledName("parameter name")>] ParameterName
        | [<CompiledName("doc comment tag name")>] DocCommentTagName
        | [<CompiledName("jsx open tag name")>] JsxOpenTagName
        | [<CompiledName("jsx close tag name")>] JsxCloseTagName
        | [<CompiledName("jsx self closing tag name")>] JsxSelfClosingTagName
        | [<CompiledName("jsx attribute")>] JsxAttribute
        | [<CompiledName("jsx text")>] JsxText
        | [<CompiledName("jsx attribute string literal value")>] JsxAttributeStringLiteralValue

    type [<RequireQualifiedAccess>] ClassificationType =
        | Comment = 1
        | Identifier = 2
        | Keyword = 3
        | NumericLiteral = 4
        | Operator = 5
        | StringLiteral = 6
        | RegularExpressionLiteral = 7
        | WhiteSpace = 8
        | Text = 9
        | Punctuation = 10
        | ClassName = 11
        | EnumName = 12
        | InterfaceName = 13
        | ModuleName = 14
        | TypeParameterName = 15
        | TypeAliasName = 16
        | ParameterName = 17
        | DocCommentTagName = 18
        | JsxOpenTagName = 19
        | JsxCloseTagName = 20
        | JsxSelfClosingTagName = 21
        | JsxAttribute = 22
        | JsxText = 23
        | JsxAttributeStringLiteralValue = 24
        | BigintLiteral = 25

    type [<AllowNullLiteral>] InlayHintsContext =
        abstract file: SourceFile with get, set
        abstract program: Program with get, set
        abstract cancellationToken: CancellationToken with get, set
        abstract host: LanguageServiceHost with get, set
        abstract span: TextSpan with get, set
        abstract preferences: UserPreferences with get, set

    type [<AllowNullLiteral>] DocumentHighlights =
        abstract fileName: string with get, set
        abstract highlightSpans: HighlightSpan[] with get, set

    /// The document registry represents a store of SourceFile objects that can be shared between
    /// multiple LanguageService instances. A LanguageService instance holds on the SourceFile (AST)
    /// of files in the context.
    /// SourceFile objects account for most of the memory usage by the language service. Sharing
    /// the same DocumentRegistry instance between different instances of LanguageService allow
    /// for more efficient memory utilization since all projects will share at least the library
    /// file (lib.d.ts).
    /// 
    /// A more advanced use of the document registry is to serialize sourceFile objects to disk
    /// and re-hydrate them when needed.
    /// 
    /// To create a default DocumentRegistry, use createDocumentRegistry to create one, and pass it
    /// to all subsequent createLanguageService calls.
    type [<AllowNullLiteral>] DocumentRegistry =
        /// <summary>
        /// Request a stored SourceFile with a given fileName and compilationSettings.
        /// The first call to acquire will call createLanguageServiceSourceFile to generate
        /// the SourceFile if was not found in the registry.
        /// </summary>
        /// <param name="fileName">The name of the file requested</param>
        /// <param name="compilationSettingsOrHost">
        /// Some compilation settings like target affects the
        /// shape of a the resulting SourceFile. This allows the DocumentRegistry to store
        /// multiple copies of the same file for different compilation settings. A minimal
        /// resolution cache is needed to fully define a source file's shape when
        /// the compilation settings include <c>module: node16</c>+, so providing a cache host
        /// object should be preferred. A common host is a language service <c>ConfiguredProject</c>.
        /// </param>
        /// <param name="scriptSnapshot">
        /// Text of the file. Only used if the file was not found
        /// in the registry and a new one was created.
        /// </param>
        /// <param name="version">
        /// Current version of the file. Only used if the file was not found
        /// in the registry and a new one was created.
        /// </param>
        abstract acquireDocument: fileName: string * compilationSettingsOrHost: U2<CompilerOptions, MinimalResolutionCacheHost> * scriptSnapshot: IScriptSnapshot * version: string * ?scriptKind: ScriptKind * ?sourceFileOptions: U2<CreateSourceFileOptions, ScriptTarget> -> SourceFile
        abstract acquireDocumentWithKey: fileName: string * path: Path * compilationSettingsOrHost: U2<CompilerOptions, MinimalResolutionCacheHost> * key: DocumentRegistryBucketKey * scriptSnapshot: IScriptSnapshot * version: string * ?scriptKind: ScriptKind * ?sourceFileOptions: U2<CreateSourceFileOptions, ScriptTarget> -> SourceFile
        /// <summary>
        /// Request an updated version of an already existing SourceFile with a given fileName
        /// and compilationSettings. The update will in-turn call updateLanguageServiceSourceFile
        /// to get an updated SourceFile.
        /// </summary>
        /// <param name="fileName">The name of the file requested</param>
        /// <param name="compilationSettingsOrHost">
        /// Some compilation settings like target affects the
        /// shape of a the resulting SourceFile. This allows the DocumentRegistry to store
        /// multiple copies of the same file for different compilation settings. A minimal
        /// resolution cache is needed to fully define a source file's shape when
        /// the compilation settings include <c>module: node16</c>+, so providing a cache host
        /// object should be preferred. A common host is a language service <c>ConfiguredProject</c>.
        /// </param>
        /// <param name="scriptSnapshot">Text of the file.</param>
        /// <param name="version">Current version of the file.</param>
        abstract updateDocument: fileName: string * compilationSettingsOrHost: U2<CompilerOptions, MinimalResolutionCacheHost> * scriptSnapshot: IScriptSnapshot * version: string * ?scriptKind: ScriptKind * ?sourceFileOptions: U2<CreateSourceFileOptions, ScriptTarget> -> SourceFile
        abstract updateDocumentWithKey: fileName: string * path: Path * compilationSettingsOrHost: U2<CompilerOptions, MinimalResolutionCacheHost> * key: DocumentRegistryBucketKey * scriptSnapshot: IScriptSnapshot * version: string * ?scriptKind: ScriptKind * ?sourceFileOptions: U2<CreateSourceFileOptions, ScriptTarget> -> SourceFile
        abstract getKeyForCompilationSettings: settings: CompilerOptions -> DocumentRegistryBucketKey
        /// <summary>
        /// Informs the DocumentRegistry that a file is not needed any longer.
        /// 
        /// Note: It is not allowed to call release on a SourceFile that was not acquired from
        /// this registry originally.
        /// </summary>
        /// <param name="fileName">The name of the file to be released</param>
        /// <param name="compilationSettings">The compilation settings used to acquire the file</param>
        /// <param name="scriptKind">The script kind of the file to be released</param>
        /// <param name="impliedNodeFormat">The implied source file format of the file to be released</param>
        abstract releaseDocument: fileName: string * compilationSettings: CompilerOptions * scriptKind: ScriptKind * impliedNodeFormat: ResolutionMode -> unit
        abstract releaseDocumentWithKey: path: Path * key: DocumentRegistryBucketKey * scriptKind: ScriptKind * impliedNodeFormat: ResolutionMode -> unit
        abstract reportStats: unit -> string

    type [<AllowNullLiteral>] DocumentRegistryBucketKey =
        interface end

    type [<AllowNullLiteral>] TranspileOptions =
        abstract compilerOptions: CompilerOptions option with get, set
        abstract fileName: string option with get, set
        abstract reportDiagnostics: bool option with get, set
        abstract moduleName: string option with get, set
        abstract renamedDependencies: MapLike<string> option with get, set
        abstract transformers: CustomTransformers option with get, set

    type [<AllowNullLiteral>] TranspileOutput =
        abstract outputText: string with get, set
        abstract diagnostics: Diagnostic[] option with get, set
        abstract sourceMapText: string option with get, set

    type [<AllowNullLiteral>] DiagnosticMessageReportsUnnecessary =
        interface end

    type [<StringEnum>] [<RequireQualifiedAccess>] UserPreferencesQuotePreference =
        | Auto
        | Double
        | Single

    type [<StringEnum>] [<RequireQualifiedAccess>] UserPreferencesImportModuleSpecifierPreference =
        | Shortest
        | [<CompiledName("project-relative")>] ProjectRelative
        | Relative
        | [<CompiledName("non-relative")>] NonRelative

    type [<StringEnum>] [<RequireQualifiedAccess>] UserPreferencesImportModuleSpecifierEnding =
        | Auto
        | Minimal
        | Index
        | Js

    type [<StringEnum>] [<RequireQualifiedAccess>] UserPreferencesIncludePackageJsonAutoImports =
        | Auto
        | On
        | Off

    type [<StringEnum>] [<RequireQualifiedAccess>] UserPreferencesJsxAttributeCompletionStyle =
        | Auto
        | Braces
        | None

    type [<StringEnum>] [<RequireQualifiedAccess>] UserPreferencesIncludeInlayParameterNameHints =
        | None
        | Literals
        | All

    type [<StringEnum>] [<RequireQualifiedAccess>] UserPreferencesOrganizeImportsCollation =
        | Ordinal
        | Unicode

    type [<StringEnum>] [<RequireQualifiedAccess>] UserPreferencesOrganizeImportsCaseFirst =
        | Upper
        | Lower

    type [<StringEnum>] [<RequireQualifiedAccess>] PerformanceEventKind =
        | [<CompiledName("UpdateGraph")>] UpdateGraph
        | [<CompiledName("CreatePackageJsonAutoImportProvider")>] CreatePackageJsonAutoImportProvider

    type [<StringEnum>] [<RequireQualifiedAccess>] NavigateToItemMatchKind =
        | Exact
        | Prefix
        | Substring
        | CamelCase
