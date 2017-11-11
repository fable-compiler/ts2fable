module rec Fable.Import.TypeScript
open System
open Fable.Core
open Fable.Import.JS

type [<AllowNullLiteral>] IExports =
    abstract setTimeout: handler: (ResizeArray<obj> -> unit) * timeout: float -> obj
    abstract clearTimeout: handle: obj -> unit

module ts =

    type [<AllowNullLiteral>] IExports =
        abstract versionMajorMinor: obj with get, set
        abstract version: string with get, set
        abstract getNodeMajorVersion: unit -> float
        abstract sys: System with get, set
        abstract tokenToString: t: SyntaxKind -> string option
        abstract getPositionOfLineAndCharacter: sourceFile: SourceFile * line: float * character: float -> float
        abstract getLineAndCharacterOfPosition: sourceFile: SourceFileLike * position: float -> LineAndCharacter
        abstract isWhiteSpaceLike: ch: float -> bool
        /// Does not include line breaks. For that, see isWhiteSpaceLike. 
        abstract isWhiteSpaceSingleLine: ch: float -> bool
        abstract isLineBreak: ch: float -> bool
        abstract couldStartTrivia: text: string * pos: float -> bool
        abstract forEachLeadingCommentRange: text: string * pos: float * cb: (float -> float -> CommentKind -> bool -> 'T -> 'U) * ?state: 'T -> 'U option
        abstract forEachTrailingCommentRange: text: string * pos: float * cb: (float -> float -> CommentKind -> bool -> 'T -> 'U) * ?state: 'T -> 'U option
        abstract reduceEachLeadingCommentRange: text: string * pos: float * cb: (float -> float -> CommentKind -> bool -> 'T -> 'U -> 'U) * state: 'T * initial: 'U -> 'U
        abstract reduceEachTrailingCommentRange: text: string * pos: float * cb: (float -> float -> CommentKind -> bool -> 'T -> 'U -> 'U) * state: 'T * initial: 'U -> 'U
        abstract getLeadingCommentRanges: text: string * pos: float -> ResizeArray<CommentRange> option
        abstract getTrailingCommentRanges: text: string * pos: float -> ResizeArray<CommentRange> option
        /// Optionally, get the shebang 
        abstract getShebang: text: string -> string option
        abstract isIdentifierStart: ch: float * languageVersion: ScriptTarget -> bool
        abstract isIdentifierPart: ch: float * languageVersion: ScriptTarget -> bool
        abstract createScanner: languageVersion: ScriptTarget * skipTrivia: bool * ?languageVariant: LanguageVariant * ?text: string * ?onError: ErrorCallback * ?start: float * ?length: float -> Scanner
        abstract getDefaultLibFileName: options: CompilerOptions -> string
        abstract textSpanEnd: span: TextSpan -> float
        abstract textSpanIsEmpty: span: TextSpan -> bool
        abstract textSpanContainsPosition: span: TextSpan * position: float -> bool
        abstract textSpanContainsTextSpan: span: TextSpan * other: TextSpan -> bool
        abstract textSpanOverlapsWith: span: TextSpan * other: TextSpan -> bool
        abstract textSpanOverlap: span1: TextSpan * span2: TextSpan -> TextSpan
        abstract textSpanIntersectsWithTextSpan: span: TextSpan * other: TextSpan -> bool
        abstract textSpanIntersectsWith: span: TextSpan * start: float * length: float -> bool
        abstract decodedTextSpanIntersectsWith: start1: float * length1: float * start2: float * length2: float -> bool
        abstract textSpanIntersectsWithPosition: span: TextSpan * position: float -> bool
        abstract textSpanIntersection: span1: TextSpan * span2: TextSpan -> TextSpan
        abstract createTextSpan: start: float * length: float -> TextSpan
        abstract createTextSpanFromBounds: start: float * ``end``: float -> TextSpan
        abstract textChangeRangeNewSpan: range: TextChangeRange -> TextSpan
        abstract textChangeRangeIsUnchanged: range: TextChangeRange -> bool
        abstract createTextChangeRange: span: TextSpan * newLength: float -> TextChangeRange
        abstract unchangedTextChangeRange: TextChangeRange with get, set
        /// Called to merge all the changes that occurred across several versions of a script snapshot
        /// into a single change.  i.e. if a user keeps making successive edits to a script we will
        /// have a text change from V1 to V2, V2 to V3, ..., Vn.
        /// 
        /// This function will then merge those changes into a single change range valid between V1 and
        /// Vn.
        abstract collapseTextChangeRangesAcrossMultipleVersions: changes: ReadonlyArray<TextChangeRange> -> TextChangeRange
        abstract getTypeParameterOwner: d: Declaration -> Declaration
        abstract isParameterPropertyDeclaration: node: Node -> bool
        abstract isEmptyBindingPattern: node: BindingName -> bool
        abstract isEmptyBindingElement: node: BindingElement -> bool
        abstract getCombinedModifierFlags: node: Node -> ModifierFlags
        abstract getCombinedNodeFlags: node: Node -> NodeFlags
        /// Checks to see if the locale is in the appropriate format,
        /// and if it is, attempts to set the appropriate language.
        abstract validateLocaleAndSetLanguage: locale: string * sys: obj * ?errors: Push<Diagnostic> -> unit
        abstract getOriginalNode: node: Node -> Node
        abstract getOriginalNode: node: Node * nodeTest: (Node -> bool) -> 'T
        /// Gets a value indicating whether a node originated in the parse tree.
        abstract isParseTreeNode: node: Node -> bool
        /// Gets the original parse tree node for a node.
        abstract getParseTreeNode: node: Node -> Node
        /// Gets the original parse tree node for a node.
        abstract getParseTreeNode: node: Node * ?nodeTest: (Node -> bool) -> 'T
        /// Remove extra underscore from escaped identifier text content.
        abstract unescapeLeadingUnderscores: identifier: __String -> string
        /// Remove extra underscore from escaped identifier text content.
        abstract unescapeIdentifier: id: string -> string
        abstract getNameOfDeclaration: declaration: Declaration -> DeclarationName option
        abstract isNumericLiteral: node: Node -> bool
        abstract isStringLiteral: node: Node -> bool
        abstract isJsxText: node: Node -> bool
        abstract isRegularExpressionLiteral: node: Node -> bool
        abstract isNoSubstitutionTemplateLiteral: node: Node -> bool
        abstract isTemplateHead: node: Node -> bool
        abstract isTemplateMiddle: node: Node -> bool
        abstract isTemplateTail: node: Node -> bool
        abstract isIdentifier: node: Node -> bool
        abstract isQualifiedName: node: Node -> bool
        abstract isComputedPropertyName: node: Node -> bool
        abstract isTypeParameterDeclaration: node: Node -> bool
        abstract isParameter: node: Node -> bool
        abstract isDecorator: node: Node -> bool
        abstract isPropertySignature: node: Node -> bool
        abstract isPropertyDeclaration: node: Node -> bool
        abstract isMethodSignature: node: Node -> bool
        abstract isMethodDeclaration: node: Node -> bool
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
        abstract isUnionTypeNode: node: Node -> bool
        abstract isIntersectionTypeNode: node: Node -> bool
        abstract isParenthesizedTypeNode: node: Node -> bool
        abstract isThisTypeNode: node: Node -> bool
        abstract isTypeOperatorNode: node: Node -> bool
        abstract isIndexedAccessTypeNode: node: Node -> bool
        abstract isMappedTypeNode: node: Node -> bool
        abstract isLiteralTypeNode: node: Node -> bool
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
        abstract isTypeAssertion: node: Node -> bool
        abstract isParenthesizedExpression: node: Node -> bool
        abstract skipPartiallyEmittedExpressions: node: Expression -> Expression
        abstract skipPartiallyEmittedExpressions: node: Node -> Node
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
        abstract isNonNullExpression: node: Node -> bool
        abstract isMetaProperty: node: Node -> bool
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
        abstract isNamespaceImport: node: Node -> bool
        abstract isNamedImports: node: Node -> bool
        abstract isImportSpecifier: node: Node -> bool
        abstract isExportAssignment: node: Node -> bool
        abstract isExportDeclaration: node: Node -> bool
        abstract isNamedExports: node: Node -> bool
        abstract isExportSpecifier: node: Node -> bool
        abstract isMissingDeclaration: node: Node -> bool
        abstract isExternalModuleReference: node: Node -> bool
        abstract isJsxElement: node: Node -> bool
        abstract isJsxSelfClosingElement: node: Node -> bool
        abstract isJsxOpeningElement: node: Node -> bool
        abstract isJsxClosingElement: node: Node -> bool
        abstract isJsxAttribute: node: Node -> bool
        abstract isJsxAttributes: node: Node -> bool
        abstract isJsxSpreadAttribute: node: Node -> bool
        abstract isJsxExpression: node: Node -> bool
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
        abstract isJSDocAllType: node: JSDocAllType -> bool
        abstract isJSDocUnknownType: node: Node -> bool
        abstract isJSDocNullableType: node: Node -> bool
        abstract isJSDocNonNullableType: node: Node -> bool
        abstract isJSDocOptionalType: node: Node -> bool
        abstract isJSDocFunctionType: node: Node -> bool
        abstract isJSDocVariadicType: node: Node -> bool
        abstract isJSDoc: node: Node -> bool
        abstract isJSDocAugmentsTag: node: Node -> bool
        abstract isJSDocParameterTag: node: Node -> bool
        abstract isJSDocReturnTag: node: Node -> bool
        abstract isJSDocTypeTag: node: Node -> bool
        abstract isJSDocTemplateTag: node: Node -> bool
        abstract isJSDocTypedefTag: node: Node -> bool
        abstract isJSDocPropertyTag: node: Node -> bool
        abstract isJSDocPropertyLikeTag: node: Node -> bool
        abstract isJSDocTypeLiteral: node: Node -> bool
        /// True if node is of some token syntax kind.
        /// For example, this is true for an IfKeyword but not for an IfStatement.
        abstract isToken: n: Node -> bool
        abstract isLiteralExpression: node: Node -> bool
        abstract isTemplateMiddleOrTemplateTail: node: Node -> bool
        abstract isStringTextContainingNode: node: Node -> bool
        abstract isModifier: node: Node -> bool
        abstract isEntityName: node: Node -> bool
        abstract isPropertyName: node: Node -> bool
        abstract isBindingName: node: Node -> bool
        abstract isFunctionLike: node: Node -> bool
        abstract isClassElement: node: Node -> bool
        abstract isClassLike: node: Node -> bool
        abstract isAccessor: node: Node -> bool
        abstract isTypeElement: node: Node -> bool
        abstract isObjectLiteralElementLike: node: Node -> bool
        /// Node test that determines whether a node is a valid type node.
        /// This differs from the `isPartOfTypeNode` function which determines whether a node is *part*
        /// of a TypeNode.
        abstract isTypeNode: node: Node -> bool
        abstract isFunctionOrConstructorTypeNode: node: Node -> bool
        abstract isPropertyAccessOrQualifiedName: node: Node -> bool
        abstract isCallLikeExpression: node: Node -> bool
        abstract isCallOrNewExpression: node: Node -> bool
        abstract isTemplateLiteral: node: Node -> bool
        abstract isAssertionExpression: node: Node -> bool
        abstract isIterationStatement: node: Node * lookInLabeledStatements: bool -> bool
        abstract isJsxOpeningLikeElement: node: Node -> bool
        abstract isCaseOrDefaultClause: node: Node -> bool
        /// True if node is of a kind that may contain comment text. 
        abstract isJSDocCommentContainingNode: node: Node -> bool
        abstract createNode: kind: SyntaxKind * ?pos: float * ?``end``: float -> Node
        /// Invokes a callback for each child of the given node. The 'cbNode' callback is invoked for all child nodes
        /// stored in properties. If a 'cbNodes' callback is specified, it is invoked for embedded arrays; otherwise,
        /// embedded arrays are flattened and the 'cbNode' callback is invoked for each element. If a callback returns
        /// a truthy value, iteration stops and that value is returned. Otherwise, undefined is returned.
        abstract forEachChild: node: Node * cbNode: (Node -> 'T option) * ?cbNodes: (ResizeArray<Node> -> 'T option) -> 'T option
        abstract createSourceFile: fileName: string * sourceText: string * languageVersion: ScriptTarget * ?setParentNodes: bool * ?scriptKind: ScriptKind -> SourceFile
        abstract parseIsolatedEntityName: text: string * languageVersion: ScriptTarget -> EntityName
        /// Parse json text into SyntaxTree and return node and parse errors if any
        abstract parseJsonText: fileName: string * sourceText: string -> JsonSourceFile
        abstract isExternalModule: file: SourceFile -> bool
        abstract updateSourceFile: sourceFile: SourceFile * newText: string * textChangeRange: TextChangeRange * ?aggressiveChecks: bool -> SourceFile
        abstract getEffectiveTypeRoots: options: CompilerOptions * host: obj -> ResizeArray<string> option
        abstract resolveTypeReferenceDirective: typeReferenceDirectiveName: string * containingFile: string option * options: CompilerOptions * host: ModuleResolutionHost -> ResolvedTypeReferenceDirectiveWithFailedLookupLocations
        /// Given a set of options, returns the set of type directive names
        ///    that should be included for this program automatically.
        /// This list could either come from the config file,
        ///    or from enumerating the types root + initial secondary types lookup location.
        /// More type directives might appear in the program later as a result of loading actual source files;
        ///    this list is only the set of defaults that are implicitly included.
        abstract getAutomaticTypeDirectiveNames: options: CompilerOptions * host: ModuleResolutionHost -> ResizeArray<string>
        abstract createModuleResolutionCache: currentDirectory: string * getCanonicalFileName: (string -> string) -> ModuleResolutionCache
        abstract resolveModuleName: moduleName: string * containingFile: string * compilerOptions: CompilerOptions * host: ModuleResolutionHost * ?cache: ModuleResolutionCache -> ResolvedModuleWithFailedLookupLocations
        abstract nodeModuleNameResolver: moduleName: string * containingFile: string * compilerOptions: CompilerOptions * host: ModuleResolutionHost * ?cache: ModuleResolutionCache -> ResolvedModuleWithFailedLookupLocations
        abstract classicNameResolver: moduleName: string * containingFile: string * compilerOptions: CompilerOptions * host: ModuleResolutionHost * ?cache: NonRelativeModuleNameResolutionCache -> ResolvedModuleWithFailedLookupLocations
        abstract createNodeArray: ?elements: ReadonlyArray<'T> * ?hasTrailingComma: bool -> ResizeArray<'T>
        /// Create a string literal whose source text is read from a source node during emit. 
        abstract createLiteral: value: string -> StringLiteral
        /// Create a string literal whose source text is read from a source node during emit. 
        abstract createLiteral: value: float -> NumericLiteral
        /// Create a string literal whose source text is read from a source node during emit. 
        abstract createLiteral: value: bool -> BooleanLiteral
        /// Create a string literal whose source text is read from a source node during emit. 
        abstract createLiteral: sourceNode: U3<StringLiteral, NumericLiteral, Identifier> -> StringLiteral
        /// Create a string literal whose source text is read from a source node during emit. 
        abstract createLiteral: value: U3<string, float, bool> -> PrimaryExpression
        abstract createNumericLiteral: value: string -> NumericLiteral
        abstract createIdentifier: text: string -> Identifier
        abstract updateIdentifier: node: Identifier * typeArguments: ResizeArray<TypeNode> option -> Identifier
        /// Create a unique temporary variable. 
        abstract createTempVariable: recordTempVariable: obj option -> Identifier
        /// Create a unique temporary variable for use in a loop. 
        abstract createLoopVariable: unit -> Identifier
        /// Create a unique name based on the supplied text. 
        abstract createUniqueName: text: string -> Identifier
        /// Create a unique name generated for a node. 
        abstract getGeneratedNameForNode: node: Node -> Identifier
        abstract createToken: token: 'TKind -> Token<'TKind>
        abstract createSuper: unit -> SuperExpression
        abstract createThis: unit -> obj
        abstract createNull: unit -> obj
        abstract createTrue: unit -> obj
        abstract createFalse: unit -> obj
        abstract createQualifiedName: left: EntityName * right: U2<string, Identifier> -> QualifiedName
        abstract updateQualifiedName: node: QualifiedName * left: EntityName * right: Identifier -> QualifiedName
        abstract createComputedPropertyName: expression: Expression -> ComputedPropertyName
        abstract updateComputedPropertyName: node: ComputedPropertyName * expression: Expression -> ComputedPropertyName
        abstract createTypeParameterDeclaration: name: U2<string, Identifier> * ?``constraint``: TypeNode * ?defaultType: TypeNode -> TypeParameterDeclaration
        abstract updateTypeParameterDeclaration: node: TypeParameterDeclaration * name: Identifier * ``constraint``: TypeNode option * defaultType: TypeNode option -> TypeParameterDeclaration
        abstract createParameter: decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * dotDotDotToken: DotDotDotToken option * name: U2<string, BindingName> * ?questionToken: QuestionToken * ?``type``: TypeNode * ?initializer: Expression -> ParameterDeclaration
        abstract updateParameter: node: ParameterDeclaration * decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * dotDotDotToken: DotDotDotToken option * name: U2<string, BindingName> * questionToken: QuestionToken option * ``type``: TypeNode option * initializer: Expression option -> ParameterDeclaration
        abstract createDecorator: expression: Expression -> Decorator
        abstract updateDecorator: node: Decorator * expression: Expression -> Decorator
        abstract createPropertySignature: modifiers: ReadonlyArray<Modifier> option * name: U2<PropertyName, string> * questionToken: QuestionToken option * ``type``: TypeNode option * initializer: Expression option -> PropertySignature
        abstract updatePropertySignature: node: PropertySignature * modifiers: ReadonlyArray<Modifier> option * name: PropertyName * questionToken: QuestionToken option * ``type``: TypeNode option * initializer: Expression option -> PropertySignature
        abstract createProperty: decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * name: U2<string, PropertyName> * questionToken: QuestionToken option * ``type``: TypeNode option * initializer: Expression option -> PropertyDeclaration
        abstract updateProperty: node: PropertyDeclaration * decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * name: U2<string, PropertyName> * questionToken: QuestionToken option * ``type``: TypeNode option * initializer: Expression option -> PropertyDeclaration
        abstract createMethodSignature: typeParameters: ReadonlyArray<TypeParameterDeclaration> option * parameters: ReadonlyArray<ParameterDeclaration> * ``type``: TypeNode option * name: U2<string, PropertyName> * questionToken: QuestionToken option -> MethodSignature
        abstract updateMethodSignature: node: MethodSignature * typeParameters: ResizeArray<TypeParameterDeclaration> option * parameters: ResizeArray<ParameterDeclaration> * ``type``: TypeNode option * name: PropertyName * questionToken: QuestionToken option -> MethodSignature
        abstract createMethod: decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * asteriskToken: AsteriskToken option * name: U2<string, PropertyName> * questionToken: QuestionToken option * typeParameters: ReadonlyArray<TypeParameterDeclaration> option * parameters: ReadonlyArray<ParameterDeclaration> * ``type``: TypeNode option * body: Block option -> MethodDeclaration
        abstract updateMethod: node: MethodDeclaration * decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * asteriskToken: AsteriskToken option * name: PropertyName * questionToken: QuestionToken option * typeParameters: ReadonlyArray<TypeParameterDeclaration> option * parameters: ReadonlyArray<ParameterDeclaration> * ``type``: TypeNode option * body: Block option -> MethodDeclaration
        abstract createConstructor: decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * parameters: ReadonlyArray<ParameterDeclaration> * body: Block option -> ConstructorDeclaration
        abstract updateConstructor: node: ConstructorDeclaration * decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * parameters: ReadonlyArray<ParameterDeclaration> * body: Block option -> ConstructorDeclaration
        abstract createGetAccessor: decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * name: U2<string, PropertyName> * parameters: ReadonlyArray<ParameterDeclaration> * ``type``: TypeNode option * body: Block option -> GetAccessorDeclaration
        abstract updateGetAccessor: node: GetAccessorDeclaration * decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * name: PropertyName * parameters: ReadonlyArray<ParameterDeclaration> * ``type``: TypeNode option * body: Block option -> GetAccessorDeclaration
        abstract createSetAccessor: decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * name: U2<string, PropertyName> * parameters: ReadonlyArray<ParameterDeclaration> * body: Block option -> SetAccessorDeclaration
        abstract updateSetAccessor: node: SetAccessorDeclaration * decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * name: PropertyName * parameters: ReadonlyArray<ParameterDeclaration> * body: Block option -> SetAccessorDeclaration
        abstract createCallSignature: typeParameters: ResizeArray<TypeParameterDeclaration> option * parameters: ResizeArray<ParameterDeclaration> * ``type``: TypeNode option -> CallSignatureDeclaration
        abstract updateCallSignature: node: CallSignatureDeclaration * typeParameters: ResizeArray<TypeParameterDeclaration> option * parameters: ResizeArray<ParameterDeclaration> * ``type``: TypeNode option -> CallSignatureDeclaration
        abstract createConstructSignature: typeParameters: ResizeArray<TypeParameterDeclaration> option * parameters: ResizeArray<ParameterDeclaration> * ``type``: TypeNode option -> ConstructSignatureDeclaration
        abstract updateConstructSignature: node: ConstructSignatureDeclaration * typeParameters: ResizeArray<TypeParameterDeclaration> option * parameters: ResizeArray<ParameterDeclaration> * ``type``: TypeNode option -> ConstructSignatureDeclaration
        abstract createIndexSignature: decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * parameters: ReadonlyArray<ParameterDeclaration> * ``type``: TypeNode -> IndexSignatureDeclaration
        abstract updateIndexSignature: node: IndexSignatureDeclaration * decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * parameters: ReadonlyArray<ParameterDeclaration> * ``type``: TypeNode -> IndexSignatureDeclaration
        abstract createKeywordTypeNode: kind: obj -> KeywordTypeNode
        abstract createTypePredicateNode: parameterName: U3<Identifier, ThisTypeNode, string> * ``type``: TypeNode -> TypePredicateNode
        abstract updateTypePredicateNode: node: TypePredicateNode * parameterName: U2<Identifier, ThisTypeNode> * ``type``: TypeNode -> TypePredicateNode
        abstract createTypeReferenceNode: typeName: U2<string, EntityName> * typeArguments: ReadonlyArray<TypeNode> option -> TypeReferenceNode
        abstract updateTypeReferenceNode: node: TypeReferenceNode * typeName: EntityName * typeArguments: ResizeArray<TypeNode> option -> TypeReferenceNode
        abstract createFunctionTypeNode: typeParameters: ResizeArray<TypeParameterDeclaration> option * parameters: ResizeArray<ParameterDeclaration> * ``type``: TypeNode option -> FunctionTypeNode
        abstract updateFunctionTypeNode: node: FunctionTypeNode * typeParameters: ResizeArray<TypeParameterDeclaration> option * parameters: ResizeArray<ParameterDeclaration> * ``type``: TypeNode option -> FunctionTypeNode
        abstract createConstructorTypeNode: typeParameters: ResizeArray<TypeParameterDeclaration> option * parameters: ResizeArray<ParameterDeclaration> * ``type``: TypeNode option -> ConstructorTypeNode
        abstract updateConstructorTypeNode: node: ConstructorTypeNode * typeParameters: ResizeArray<TypeParameterDeclaration> option * parameters: ResizeArray<ParameterDeclaration> * ``type``: TypeNode option -> ConstructorTypeNode
        abstract createTypeQueryNode: exprName: EntityName -> TypeQueryNode
        abstract updateTypeQueryNode: node: TypeQueryNode * exprName: EntityName -> TypeQueryNode
        abstract createTypeLiteralNode: members: ReadonlyArray<TypeElement> -> TypeLiteralNode
        abstract updateTypeLiteralNode: node: TypeLiteralNode * members: ResizeArray<TypeElement> -> TypeLiteralNode
        abstract createArrayTypeNode: elementType: TypeNode -> ArrayTypeNode
        abstract updateArrayTypeNode: node: ArrayTypeNode * elementType: TypeNode -> ArrayTypeNode
        abstract createTupleTypeNode: elementTypes: ReadonlyArray<TypeNode> -> TupleTypeNode
        abstract updateTypleTypeNode: node: TupleTypeNode * elementTypes: ReadonlyArray<TypeNode> -> TupleTypeNode
        abstract createUnionTypeNode: types: ResizeArray<TypeNode> -> UnionTypeNode
        abstract updateUnionTypeNode: node: UnionTypeNode * types: ResizeArray<TypeNode> -> UnionTypeNode
        abstract createIntersectionTypeNode: types: ResizeArray<TypeNode> -> IntersectionTypeNode
        abstract updateIntersectionTypeNode: node: IntersectionTypeNode * types: ResizeArray<TypeNode> -> IntersectionTypeNode
        abstract createUnionOrIntersectionTypeNode: kind: SyntaxKind * types: ReadonlyArray<TypeNode> -> UnionOrIntersectionTypeNode
        abstract createParenthesizedType: ``type``: TypeNode -> ParenthesizedTypeNode
        abstract updateParenthesizedType: node: ParenthesizedTypeNode * ``type``: TypeNode -> ParenthesizedTypeNode
        abstract createThisTypeNode: unit -> ThisTypeNode
        abstract createTypeOperatorNode: ``type``: TypeNode -> TypeOperatorNode
        abstract updateTypeOperatorNode: node: TypeOperatorNode * ``type``: TypeNode -> TypeOperatorNode
        abstract createIndexedAccessTypeNode: objectType: TypeNode * indexType: TypeNode -> IndexedAccessTypeNode
        abstract updateIndexedAccessTypeNode: node: IndexedAccessTypeNode * objectType: TypeNode * indexType: TypeNode -> IndexedAccessTypeNode
        abstract createMappedTypeNode: readonlyToken: ReadonlyToken option * typeParameter: TypeParameterDeclaration * questionToken: QuestionToken option * ``type``: TypeNode option -> MappedTypeNode
        abstract updateMappedTypeNode: node: MappedTypeNode * readonlyToken: ReadonlyToken option * typeParameter: TypeParameterDeclaration * questionToken: QuestionToken option * ``type``: TypeNode option -> MappedTypeNode
        abstract createLiteralTypeNode: literal: Expression -> LiteralTypeNode
        abstract updateLiteralTypeNode: node: LiteralTypeNode * literal: Expression -> LiteralTypeNode
        abstract createObjectBindingPattern: elements: ReadonlyArray<BindingElement> -> ObjectBindingPattern
        abstract updateObjectBindingPattern: node: ObjectBindingPattern * elements: ReadonlyArray<BindingElement> -> ObjectBindingPattern
        abstract createArrayBindingPattern: elements: ReadonlyArray<ArrayBindingElement> -> ArrayBindingPattern
        abstract updateArrayBindingPattern: node: ArrayBindingPattern * elements: ReadonlyArray<ArrayBindingElement> -> ArrayBindingPattern
        abstract createBindingElement: dotDotDotToken: DotDotDotToken option * propertyName: U2<string, PropertyName> option * name: U2<string, BindingName> * ?initializer: Expression -> BindingElement
        abstract updateBindingElement: node: BindingElement * dotDotDotToken: DotDotDotToken option * propertyName: PropertyName option * name: BindingName * initializer: Expression option -> BindingElement
        abstract createArrayLiteral: ?elements: ReadonlyArray<Expression> * ?multiLine: bool -> ArrayLiteralExpression
        abstract updateArrayLiteral: node: ArrayLiteralExpression * elements: ReadonlyArray<Expression> -> ArrayLiteralExpression
        abstract createObjectLiteral: ?properties: ReadonlyArray<ObjectLiteralElementLike> * ?multiLine: bool -> ObjectLiteralExpression
        abstract updateObjectLiteral: node: ObjectLiteralExpression * properties: ReadonlyArray<ObjectLiteralElementLike> -> ObjectLiteralExpression
        abstract createPropertyAccess: expression: Expression * name: U2<string, Identifier> -> PropertyAccessExpression
        abstract updatePropertyAccess: node: PropertyAccessExpression * expression: Expression * name: Identifier -> PropertyAccessExpression
        abstract createElementAccess: expression: Expression * index: U2<float, Expression> -> ElementAccessExpression
        abstract updateElementAccess: node: ElementAccessExpression * expression: Expression * argumentExpression: Expression -> ElementAccessExpression
        abstract createCall: expression: Expression * typeArguments: ReadonlyArray<TypeNode> option * argumentsArray: ReadonlyArray<Expression> -> CallExpression
        abstract updateCall: node: CallExpression * expression: Expression * typeArguments: ReadonlyArray<TypeNode> option * argumentsArray: ReadonlyArray<Expression> -> CallExpression
        abstract createNew: expression: Expression * typeArguments: ReadonlyArray<TypeNode> option * argumentsArray: ReadonlyArray<Expression> option -> NewExpression
        abstract updateNew: node: NewExpression * expression: Expression * typeArguments: ReadonlyArray<TypeNode> option * argumentsArray: ReadonlyArray<Expression> option -> NewExpression
        abstract createTaggedTemplate: tag: Expression * template: TemplateLiteral -> TaggedTemplateExpression
        abstract updateTaggedTemplate: node: TaggedTemplateExpression * tag: Expression * template: TemplateLiteral -> TaggedTemplateExpression
        abstract createTypeAssertion: ``type``: TypeNode * expression: Expression -> TypeAssertion
        abstract updateTypeAssertion: node: TypeAssertion * ``type``: TypeNode * expression: Expression -> TypeAssertion
        abstract createParen: expression: Expression -> ParenthesizedExpression
        abstract updateParen: node: ParenthesizedExpression * expression: Expression -> ParenthesizedExpression
        abstract createFunctionExpression: modifiers: ReadonlyArray<Modifier> option * asteriskToken: AsteriskToken option * name: U2<string, Identifier> option * typeParameters: ReadonlyArray<TypeParameterDeclaration> option * parameters: ReadonlyArray<ParameterDeclaration> * ``type``: TypeNode option * body: Block -> FunctionExpression
        abstract updateFunctionExpression: node: FunctionExpression * modifiers: ReadonlyArray<Modifier> option * asteriskToken: AsteriskToken option * name: Identifier option * typeParameters: ReadonlyArray<TypeParameterDeclaration> option * parameters: ReadonlyArray<ParameterDeclaration> * ``type``: TypeNode option * body: Block -> FunctionExpression
        abstract createArrowFunction: modifiers: ReadonlyArray<Modifier> option * typeParameters: ReadonlyArray<TypeParameterDeclaration> option * parameters: ReadonlyArray<ParameterDeclaration> * ``type``: TypeNode option * equalsGreaterThanToken: EqualsGreaterThanToken option * body: ConciseBody -> ArrowFunction
        abstract updateArrowFunction: node: ArrowFunction * modifiers: ReadonlyArray<Modifier> option * typeParameters: ReadonlyArray<TypeParameterDeclaration> option * parameters: ReadonlyArray<ParameterDeclaration> * ``type``: TypeNode option * body: ConciseBody -> ArrowFunction
        abstract updateArrowFunction: node: ArrowFunction * modifiers: ReadonlyArray<Modifier> option * typeParameters: ReadonlyArray<TypeParameterDeclaration> option * parameters: ReadonlyArray<ParameterDeclaration> * ``type``: TypeNode option * equalsGreaterThanToken: Token<SyntaxKind> * body: ConciseBody -> ArrowFunction
        abstract createDelete: expression: Expression -> DeleteExpression
        abstract updateDelete: node: DeleteExpression * expression: Expression -> DeleteExpression
        abstract createTypeOf: expression: Expression -> TypeOfExpression
        abstract updateTypeOf: node: TypeOfExpression * expression: Expression -> TypeOfExpression
        abstract createVoid: expression: Expression -> VoidExpression
        abstract updateVoid: node: VoidExpression * expression: Expression -> VoidExpression
        abstract createAwait: expression: Expression -> AwaitExpression
        abstract updateAwait: node: AwaitExpression * expression: Expression -> AwaitExpression
        abstract createPrefix: operator: PrefixUnaryOperator * operand: Expression -> PrefixUnaryExpression
        abstract updatePrefix: node: PrefixUnaryExpression * operand: Expression -> PrefixUnaryExpression
        abstract createPostfix: operand: Expression * operator: PostfixUnaryOperator -> PostfixUnaryExpression
        abstract updatePostfix: node: PostfixUnaryExpression * operand: Expression -> PostfixUnaryExpression
        abstract createBinary: left: Expression * operator: U2<BinaryOperator, BinaryOperatorToken> * right: Expression -> BinaryExpression
        abstract updateBinary: node: BinaryExpression * left: Expression * right: Expression * ?operator: U2<BinaryOperator, BinaryOperatorToken> -> BinaryExpression
        abstract createConditional: condition: Expression * whenTrue: Expression * whenFalse: Expression -> ConditionalExpression
        abstract createConditional: condition: Expression * questionToken: QuestionToken * whenTrue: Expression * colonToken: ColonToken * whenFalse: Expression -> ConditionalExpression
        abstract updateConditional: node: ConditionalExpression * condition: Expression * whenTrue: Expression * whenFalse: Expression -> ConditionalExpression
        abstract updateConditional: node: ConditionalExpression * condition: Expression * questionToken: Token<SyntaxKind> * whenTrue: Expression * colonToken: Token<SyntaxKind> * whenFalse: Expression -> ConditionalExpression
        abstract createTemplateExpression: head: TemplateHead * templateSpans: ReadonlyArray<TemplateSpan> -> TemplateExpression
        abstract updateTemplateExpression: node: TemplateExpression * head: TemplateHead * templateSpans: ReadonlyArray<TemplateSpan> -> TemplateExpression
        abstract createYield: ?expression: Expression -> YieldExpression
        abstract createYield: asteriskToken: AsteriskToken * expression: Expression -> YieldExpression
        abstract updateYield: node: YieldExpression * asteriskToken: AsteriskToken option * expression: Expression -> YieldExpression
        abstract createSpread: expression: Expression -> SpreadElement
        abstract updateSpread: node: SpreadElement * expression: Expression -> SpreadElement
        abstract createClassExpression: modifiers: ReadonlyArray<Modifier> option * name: U2<string, Identifier> option * typeParameters: ReadonlyArray<TypeParameterDeclaration> option * heritageClauses: ReadonlyArray<HeritageClause> * members: ReadonlyArray<ClassElement> -> ClassExpression
        abstract updateClassExpression: node: ClassExpression * modifiers: ReadonlyArray<Modifier> option * name: Identifier option * typeParameters: ReadonlyArray<TypeParameterDeclaration> option * heritageClauses: ReadonlyArray<HeritageClause> * members: ReadonlyArray<ClassElement> -> ClassExpression
        abstract createOmittedExpression: unit -> OmittedExpression
        abstract createExpressionWithTypeArguments: typeArguments: ReadonlyArray<TypeNode> * expression: Expression -> ExpressionWithTypeArguments
        abstract updateExpressionWithTypeArguments: node: ExpressionWithTypeArguments * typeArguments: ReadonlyArray<TypeNode> * expression: Expression -> ExpressionWithTypeArguments
        abstract createAsExpression: expression: Expression * ``type``: TypeNode -> AsExpression
        abstract updateAsExpression: node: AsExpression * expression: Expression * ``type``: TypeNode -> AsExpression
        abstract createNonNullExpression: expression: Expression -> NonNullExpression
        abstract updateNonNullExpression: node: NonNullExpression * expression: Expression -> NonNullExpression
        abstract createMetaProperty: keywordToken: obj * name: Identifier -> MetaProperty
        abstract updateMetaProperty: node: MetaProperty * name: Identifier -> MetaProperty
        abstract createTemplateSpan: expression: Expression * literal: U2<TemplateMiddle, TemplateTail> -> TemplateSpan
        abstract updateTemplateSpan: node: TemplateSpan * expression: Expression * literal: U2<TemplateMiddle, TemplateTail> -> TemplateSpan
        abstract createSemicolonClassElement: unit -> SemicolonClassElement
        abstract createBlock: statements: ReadonlyArray<Statement> * ?multiLine: bool -> Block
        abstract updateBlock: node: Block * statements: ReadonlyArray<Statement> -> Block
        abstract createVariableStatement: modifiers: ReadonlyArray<Modifier> option * declarationList: U2<VariableDeclarationList, ReadonlyArray<VariableDeclaration>> -> VariableStatement
        abstract updateVariableStatement: node: VariableStatement * modifiers: ReadonlyArray<Modifier> option * declarationList: VariableDeclarationList -> VariableStatement
        abstract createEmptyStatement: unit -> EmptyStatement
        abstract createStatement: expression: Expression -> ExpressionStatement
        abstract updateStatement: node: ExpressionStatement * expression: Expression -> ExpressionStatement
        abstract createIf: expression: Expression * thenStatement: Statement * ?elseStatement: Statement -> IfStatement
        abstract updateIf: node: IfStatement * expression: Expression * thenStatement: Statement * elseStatement: Statement option -> IfStatement
        abstract createDo: statement: Statement * expression: Expression -> DoStatement
        abstract updateDo: node: DoStatement * statement: Statement * expression: Expression -> DoStatement
        abstract createWhile: expression: Expression * statement: Statement -> WhileStatement
        abstract updateWhile: node: WhileStatement * expression: Expression * statement: Statement -> WhileStatement
        abstract createFor: initializer: ForInitializer option * condition: Expression option * incrementor: Expression option * statement: Statement -> ForStatement
        abstract updateFor: node: ForStatement * initializer: ForInitializer option * condition: Expression option * incrementor: Expression option * statement: Statement -> ForStatement
        abstract createForIn: initializer: ForInitializer * expression: Expression * statement: Statement -> ForInStatement
        abstract updateForIn: node: ForInStatement * initializer: ForInitializer * expression: Expression * statement: Statement -> ForInStatement
        abstract createForOf: awaitModifier: AwaitKeywordToken * initializer: ForInitializer * expression: Expression * statement: Statement -> ForOfStatement
        abstract updateForOf: node: ForOfStatement * awaitModifier: AwaitKeywordToken * initializer: ForInitializer * expression: Expression * statement: Statement -> ForOfStatement
        abstract createContinue: ?label: U2<string, Identifier> -> ContinueStatement
        abstract updateContinue: node: ContinueStatement * label: Identifier option -> ContinueStatement
        abstract createBreak: ?label: U2<string, Identifier> -> BreakStatement
        abstract updateBreak: node: BreakStatement * label: Identifier option -> BreakStatement
        abstract createReturn: ?expression: Expression -> ReturnStatement
        abstract updateReturn: node: ReturnStatement * expression: Expression option -> ReturnStatement
        abstract createWith: expression: Expression * statement: Statement -> WithStatement
        abstract updateWith: node: WithStatement * expression: Expression * statement: Statement -> WithStatement
        abstract createSwitch: expression: Expression * caseBlock: CaseBlock -> SwitchStatement
        abstract updateSwitch: node: SwitchStatement * expression: Expression * caseBlock: CaseBlock -> SwitchStatement
        abstract createLabel: label: U2<string, Identifier> * statement: Statement -> LabeledStatement
        abstract updateLabel: node: LabeledStatement * label: Identifier * statement: Statement -> LabeledStatement
        abstract createThrow: expression: Expression -> ThrowStatement
        abstract updateThrow: node: ThrowStatement * expression: Expression -> ThrowStatement
        abstract createTry: tryBlock: Block * catchClause: CatchClause option * finallyBlock: Block option -> TryStatement
        abstract updateTry: node: TryStatement * tryBlock: Block * catchClause: CatchClause option * finallyBlock: Block option -> TryStatement
        abstract createDebuggerStatement: unit -> DebuggerStatement
        abstract createVariableDeclaration: name: U2<string, BindingName> * ?``type``: TypeNode * ?initializer: Expression -> VariableDeclaration
        abstract updateVariableDeclaration: node: VariableDeclaration * name: BindingName * ``type``: TypeNode option * initializer: Expression option -> VariableDeclaration
        abstract createVariableDeclarationList: declarations: ReadonlyArray<VariableDeclaration> * ?flags: NodeFlags -> VariableDeclarationList
        abstract updateVariableDeclarationList: node: VariableDeclarationList * declarations: ReadonlyArray<VariableDeclaration> -> VariableDeclarationList
        abstract createFunctionDeclaration: decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * asteriskToken: AsteriskToken option * name: U2<string, Identifier> option * typeParameters: ReadonlyArray<TypeParameterDeclaration> option * parameters: ReadonlyArray<ParameterDeclaration> * ``type``: TypeNode option * body: Block option -> FunctionDeclaration
        abstract updateFunctionDeclaration: node: FunctionDeclaration * decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * asteriskToken: AsteriskToken option * name: Identifier option * typeParameters: ReadonlyArray<TypeParameterDeclaration> option * parameters: ReadonlyArray<ParameterDeclaration> * ``type``: TypeNode option * body: Block option -> FunctionDeclaration
        abstract createClassDeclaration: decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * name: U2<string, Identifier> option * typeParameters: ReadonlyArray<TypeParameterDeclaration> option * heritageClauses: ReadonlyArray<HeritageClause> * members: ReadonlyArray<ClassElement> -> ClassDeclaration
        abstract updateClassDeclaration: node: ClassDeclaration * decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * name: Identifier option * typeParameters: ReadonlyArray<TypeParameterDeclaration> option * heritageClauses: ReadonlyArray<HeritageClause> * members: ReadonlyArray<ClassElement> -> ClassDeclaration
        abstract createInterfaceDeclaration: decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * name: U2<string, Identifier> * typeParameters: ReadonlyArray<TypeParameterDeclaration> option * heritageClauses: ReadonlyArray<HeritageClause> option * members: ReadonlyArray<TypeElement> -> InterfaceDeclaration
        abstract updateInterfaceDeclaration: node: InterfaceDeclaration * decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * name: Identifier * typeParameters: ReadonlyArray<TypeParameterDeclaration> option * heritageClauses: ReadonlyArray<HeritageClause> option * members: ReadonlyArray<TypeElement> -> InterfaceDeclaration
        abstract createTypeAliasDeclaration: decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * name: U2<string, Identifier> * typeParameters: ReadonlyArray<TypeParameterDeclaration> option * ``type``: TypeNode -> TypeAliasDeclaration
        abstract updateTypeAliasDeclaration: node: TypeAliasDeclaration * decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * name: Identifier * typeParameters: ReadonlyArray<TypeParameterDeclaration> option * ``type``: TypeNode -> TypeAliasDeclaration
        abstract createEnumDeclaration: decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * name: U2<string, Identifier> * members: ReadonlyArray<EnumMember> -> EnumDeclaration
        abstract updateEnumDeclaration: node: EnumDeclaration * decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * name: Identifier * members: ReadonlyArray<EnumMember> -> EnumDeclaration
        abstract createModuleDeclaration: decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * name: ModuleName * body: ModuleBody option * ?flags: NodeFlags -> ModuleDeclaration
        abstract updateModuleDeclaration: node: ModuleDeclaration * decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * name: ModuleName * body: ModuleBody option -> ModuleDeclaration
        abstract createModuleBlock: statements: ReadonlyArray<Statement> -> ModuleBlock
        abstract updateModuleBlock: node: ModuleBlock * statements: ReadonlyArray<Statement> -> ModuleBlock
        abstract createCaseBlock: clauses: ReadonlyArray<CaseOrDefaultClause> -> CaseBlock
        abstract updateCaseBlock: node: CaseBlock * clauses: ReadonlyArray<CaseOrDefaultClause> -> CaseBlock
        abstract createNamespaceExportDeclaration: name: U2<string, Identifier> -> NamespaceExportDeclaration
        abstract updateNamespaceExportDeclaration: node: NamespaceExportDeclaration * name: Identifier -> NamespaceExportDeclaration
        abstract createImportEqualsDeclaration: decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * name: U2<string, Identifier> * moduleReference: ModuleReference -> ImportEqualsDeclaration
        abstract updateImportEqualsDeclaration: node: ImportEqualsDeclaration * decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * name: Identifier * moduleReference: ModuleReference -> ImportEqualsDeclaration
        abstract createImportDeclaration: decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * importClause: ImportClause option * ?moduleSpecifier: Expression -> ImportDeclaration
        abstract updateImportDeclaration: node: ImportDeclaration * decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * importClause: ImportClause option * moduleSpecifier: Expression option -> ImportDeclaration
        abstract createImportClause: name: Identifier option * namedBindings: NamedImportBindings option -> ImportClause
        abstract updateImportClause: node: ImportClause * name: Identifier option * namedBindings: NamedImportBindings option -> ImportClause
        abstract createNamespaceImport: name: Identifier -> NamespaceImport
        abstract updateNamespaceImport: node: NamespaceImport * name: Identifier -> NamespaceImport
        abstract createNamedImports: elements: ReadonlyArray<ImportSpecifier> -> NamedImports
        abstract updateNamedImports: node: NamedImports * elements: ReadonlyArray<ImportSpecifier> -> NamedImports
        abstract createImportSpecifier: propertyName: Identifier option * name: Identifier -> ImportSpecifier
        abstract updateImportSpecifier: node: ImportSpecifier * propertyName: Identifier option * name: Identifier -> ImportSpecifier
        abstract createExportAssignment: decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * isExportEquals: bool * expression: Expression -> ExportAssignment
        abstract updateExportAssignment: node: ExportAssignment * decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * expression: Expression -> ExportAssignment
        abstract createExportDeclaration: decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * exportClause: NamedExports option * ?moduleSpecifier: Expression -> ExportDeclaration
        abstract updateExportDeclaration: node: ExportDeclaration * decorators: ReadonlyArray<Decorator> option * modifiers: ReadonlyArray<Modifier> option * exportClause: NamedExports option * moduleSpecifier: Expression option -> ExportDeclaration
        abstract createNamedExports: elements: ReadonlyArray<ExportSpecifier> -> NamedExports
        abstract updateNamedExports: node: NamedExports * elements: ReadonlyArray<ExportSpecifier> -> NamedExports
        abstract createExportSpecifier: propertyName: U2<string, Identifier> option * name: U2<string, Identifier> -> ExportSpecifier
        abstract updateExportSpecifier: node: ExportSpecifier * propertyName: Identifier option * name: Identifier -> ExportSpecifier
        abstract createExternalModuleReference: expression: Expression -> ExternalModuleReference
        abstract updateExternalModuleReference: node: ExternalModuleReference * expression: Expression -> ExternalModuleReference
        abstract createJsxElement: openingElement: JsxOpeningElement * children: ReadonlyArray<JsxChild> * closingElement: JsxClosingElement -> JsxElement
        abstract updateJsxElement: node: JsxElement * openingElement: JsxOpeningElement * children: ReadonlyArray<JsxChild> * closingElement: JsxClosingElement -> JsxElement
        abstract createJsxSelfClosingElement: tagName: JsxTagNameExpression * attributes: JsxAttributes -> JsxSelfClosingElement
        abstract updateJsxSelfClosingElement: node: JsxSelfClosingElement * tagName: JsxTagNameExpression * attributes: JsxAttributes -> JsxSelfClosingElement
        abstract createJsxOpeningElement: tagName: JsxTagNameExpression * attributes: JsxAttributes -> JsxOpeningElement
        abstract updateJsxOpeningElement: node: JsxOpeningElement * tagName: JsxTagNameExpression * attributes: JsxAttributes -> JsxOpeningElement
        abstract createJsxClosingElement: tagName: JsxTagNameExpression -> JsxClosingElement
        abstract updateJsxClosingElement: node: JsxClosingElement * tagName: JsxTagNameExpression -> JsxClosingElement
        abstract createJsxAttribute: name: Identifier * initializer: U2<StringLiteral, JsxExpression> -> JsxAttribute
        abstract updateJsxAttribute: node: JsxAttribute * name: Identifier * initializer: U2<StringLiteral, JsxExpression> -> JsxAttribute
        abstract createJsxAttributes: properties: ReadonlyArray<JsxAttributeLike> -> JsxAttributes
        abstract updateJsxAttributes: node: JsxAttributes * properties: ReadonlyArray<JsxAttributeLike> -> JsxAttributes
        abstract createJsxSpreadAttribute: expression: Expression -> JsxSpreadAttribute
        abstract updateJsxSpreadAttribute: node: JsxSpreadAttribute * expression: Expression -> JsxSpreadAttribute
        abstract createJsxExpression: dotDotDotToken: DotDotDotToken option * expression: Expression option -> JsxExpression
        abstract updateJsxExpression: node: JsxExpression * expression: Expression option -> JsxExpression
        abstract createCaseClause: expression: Expression * statements: ReadonlyArray<Statement> -> CaseClause
        abstract updateCaseClause: node: CaseClause * expression: Expression * statements: ReadonlyArray<Statement> -> CaseClause
        abstract createDefaultClause: statements: ReadonlyArray<Statement> -> DefaultClause
        abstract updateDefaultClause: node: DefaultClause * statements: ReadonlyArray<Statement> -> DefaultClause
        abstract createHeritageClause: token: obj * types: ReadonlyArray<ExpressionWithTypeArguments> -> HeritageClause
        abstract updateHeritageClause: node: HeritageClause * types: ReadonlyArray<ExpressionWithTypeArguments> -> HeritageClause
        abstract createCatchClause: variableDeclaration: U2<string, VariableDeclaration> option * block: Block -> CatchClause
        abstract updateCatchClause: node: CatchClause * variableDeclaration: VariableDeclaration option * block: Block -> CatchClause
        abstract createPropertyAssignment: name: U2<string, PropertyName> * initializer: Expression -> PropertyAssignment
        abstract updatePropertyAssignment: node: PropertyAssignment * name: PropertyName * initializer: Expression -> PropertyAssignment
        abstract createShorthandPropertyAssignment: name: U2<string, Identifier> * ?objectAssignmentInitializer: Expression -> ShorthandPropertyAssignment
        abstract updateShorthandPropertyAssignment: node: ShorthandPropertyAssignment * name: Identifier * objectAssignmentInitializer: Expression option -> ShorthandPropertyAssignment
        abstract createSpreadAssignment: expression: Expression -> SpreadAssignment
        abstract updateSpreadAssignment: node: SpreadAssignment * expression: Expression -> SpreadAssignment
        abstract createEnumMember: name: U2<string, PropertyName> * ?initializer: Expression -> EnumMember
        abstract updateEnumMember: node: EnumMember * name: PropertyName * initializer: Expression option -> EnumMember
        abstract updateSourceFileNode: node: SourceFile * statements: ReadonlyArray<Statement> -> SourceFile
        /// Creates a shallow, memberwise clone of a node for mutation.
        abstract getMutableClone: node: 'T -> 'T
        /// Creates a synthetic statement to act as a placeholder for a not-emitted statement in
        /// order to preserve comments.
        abstract createNotEmittedStatement: original: Node -> NotEmittedStatement
        /// Creates a synthetic expression to act as a placeholder for a not-emitted expression in
        /// order to preserve comments or sourcemap positions.
        abstract createPartiallyEmittedExpression: expression: Expression * ?original: Node -> PartiallyEmittedExpression
        abstract updatePartiallyEmittedExpression: node: PartiallyEmittedExpression * expression: Expression -> PartiallyEmittedExpression
        abstract createCommaList: elements: ReadonlyArray<Expression> -> CommaListExpression
        abstract updateCommaList: node: CommaListExpression * elements: ReadonlyArray<Expression> -> CommaListExpression
        abstract createBundle: sourceFiles: ResizeArray<SourceFile> -> Bundle
        abstract updateBundle: node: Bundle * sourceFiles: ResizeArray<SourceFile> -> Bundle
        abstract createImmediatelyInvokedFunctionExpression: statements: ResizeArray<Statement> -> CallExpression
        abstract createImmediatelyInvokedFunctionExpression: statements: ResizeArray<Statement> * param: ParameterDeclaration * paramValue: Expression -> CallExpression
        abstract createImmediatelyInvokedArrowFunction: statements: ResizeArray<Statement> -> CallExpression
        abstract createImmediatelyInvokedArrowFunction: statements: ResizeArray<Statement> * param: ParameterDeclaration * paramValue: Expression -> CallExpression
        abstract createComma: left: Expression * right: Expression -> Expression
        abstract createLessThan: left: Expression * right: Expression -> Expression
        abstract createAssignment: left: U2<ObjectLiteralExpression, ArrayLiteralExpression> * right: Expression -> DestructuringAssignment
        abstract createAssignment: left: Expression * right: Expression -> BinaryExpression
        abstract createStrictEquality: left: Expression * right: Expression -> BinaryExpression
        abstract createStrictInequality: left: Expression * right: Expression -> BinaryExpression
        abstract createAdd: left: Expression * right: Expression -> BinaryExpression
        abstract createSubtract: left: Expression * right: Expression -> BinaryExpression
        abstract createPostfixIncrement: operand: Expression -> PostfixUnaryExpression
        abstract createLogicalAnd: left: Expression * right: Expression -> BinaryExpression
        abstract createLogicalOr: left: Expression * right: Expression -> BinaryExpression
        abstract createLogicalNot: operand: Expression -> PrefixUnaryExpression
        abstract createVoidZero: unit -> VoidExpression
        abstract createExportDefault: expression: Expression -> ExportAssignment
        abstract createExternalModuleExport: exportName: Identifier -> ExportDeclaration
        /// Clears any EmitNode entries from parse-tree nodes.
        abstract disposeEmitNodes: sourceFile: SourceFile -> unit
        abstract setTextRange: range: 'T * location: TextRange option -> 'T
        /// Sets flags that control emit behavior of a node.
        abstract setEmitFlags: node: 'T * emitFlags: EmitFlags -> 'T
        /// Gets a custom text range to use when emitting source maps.
        abstract getSourceMapRange: node: Node -> SourceMapRange
        /// Sets a custom text range to use when emitting source maps.
        abstract setSourceMapRange: node: 'T * range: SourceMapRange option -> 'T
        /// Create an external source map source file reference
        abstract createSourceMapSource: fileName: string * text: string * ?skipTrivia: (float -> float) -> SourceMapSource
        /// Gets the TextRange to use for source maps for a token of a node.
        abstract getTokenSourceMapRange: node: Node * token: SyntaxKind -> SourceMapRange option
        /// Sets the TextRange to use for source maps for a token of a node.
        abstract setTokenSourceMapRange: node: 'T * token: SyntaxKind * range: SourceMapRange option -> 'T
        /// Gets a custom text range to use when emitting comments.
        abstract getCommentRange: node: Node -> TextRange
        /// Sets a custom text range to use when emitting comments.
        abstract setCommentRange: node: 'T * range: TextRange -> 'T
        abstract getSyntheticLeadingComments: node: Node -> ResizeArray<SynthesizedComment> option
        abstract setSyntheticLeadingComments: node: 'T * comments: ResizeArray<SynthesizedComment> -> 'T
        abstract addSyntheticLeadingComment: node: 'T * kind: SyntaxKind * text: string * ?hasTrailingNewLine: bool -> 'T
        abstract getSyntheticTrailingComments: node: Node -> ResizeArray<SynthesizedComment> option
        abstract setSyntheticTrailingComments: node: 'T * comments: ResizeArray<SynthesizedComment> -> 'T
        abstract addSyntheticTrailingComment: node: 'T * kind: SyntaxKind * text: string * ?hasTrailingNewLine: bool -> 'T
        /// Gets the constant value to emit for an expression.
        abstract getConstantValue: node: U2<PropertyAccessExpression, ElementAccessExpression> -> U2<string, float>
        /// Sets the constant value to emit for an expression.
        abstract setConstantValue: node: U2<PropertyAccessExpression, ElementAccessExpression> * value: U2<string, float> -> U2<PropertyAccessExpression, ElementAccessExpression>
        /// Adds an EmitHelper to a node.
        abstract addEmitHelper: node: 'T * helper: EmitHelper -> 'T
        /// Add EmitHelpers to a node.
        abstract addEmitHelpers: node: 'T * helpers: ResizeArray<EmitHelper> option -> 'T
        /// Removes an EmitHelper from a node.
        abstract removeEmitHelper: node: Node * helper: EmitHelper -> bool
        /// Gets the EmitHelpers of a node.
        abstract getEmitHelpers: node: Node -> ResizeArray<EmitHelper> option
        /// Moves matching emit helpers from a source node to a target node.
        abstract moveEmitHelpers: source: Node * target: Node * predicate: (EmitHelper -> bool) -> unit
        abstract setOriginalNode: node: 'T * original: Node option -> 'T
        /// Visits a Node using the supplied visitor, possibly returning a new Node in its place.
        abstract visitNode: node: 'T * visitor: Visitor * ?test: (Node -> bool) * ?lift: (ResizeArray<Node> -> 'T) -> 'T
        /// Visits a Node using the supplied visitor, possibly returning a new Node in its place.
        abstract visitNode: node: 'T option * visitor: Visitor * ?test: (Node -> bool) * ?lift: (ResizeArray<Node> -> 'T) -> 'T option
        /// Visits a NodeArray using the supplied visitor, possibly returning a new NodeArray in its place.
        abstract visitNodes: nodes: ResizeArray<'T> * visitor: Visitor * ?test: (Node -> bool) * ?start: float * ?count: float -> ResizeArray<'T>
        /// Visits a NodeArray using the supplied visitor, possibly returning a new NodeArray in its place.
        abstract visitNodes: nodes: ResizeArray<'T> option * visitor: Visitor * ?test: (Node -> bool) * ?start: float * ?count: float -> ResizeArray<'T> option
        /// Starts a new lexical environment and visits a statement list, ending the lexical environment
        /// and merging hoisted declarations upon completion.
        abstract visitLexicalEnvironment: statements: ResizeArray<Statement> * visitor: Visitor * context: TransformationContext * ?start: float * ?ensureUseStrict: bool -> ResizeArray<Statement>
        /// Starts a new lexical environment and visits a parameter list, suspending the lexical
        /// environment upon completion.
        abstract visitParameterList: nodes: ResizeArray<ParameterDeclaration> * visitor: Visitor * context: TransformationContext * ?nodesVisitor: obj -> ResizeArray<ParameterDeclaration>
        /// Resumes a suspended lexical environment and visits a function body, ending the lexical
        /// environment and merging hoisted declarations upon completion.
        /// Resumes a suspended lexical environment and visits a concise body, ending the lexical
        abstract visitFunctionBody: node: FunctionBody * visitor: Visitor * context: TransformationContext -> FunctionBody
        /// Resumes a suspended lexical environment and visits a function body, ending the lexical
        /// environment and merging hoisted declarations upon completion.
        /// Resumes a suspended lexical environment and visits a concise body, ending the lexical
        abstract visitFunctionBody: node: FunctionBody option * visitor: Visitor * context: TransformationContext -> FunctionBody option
        /// Resumes a suspended lexical environment and visits a function body, ending the lexical
        /// environment and merging hoisted declarations upon completion.
        /// Resumes a suspended lexical environment and visits a concise body, ending the lexical
        abstract visitFunctionBody: node: ConciseBody * visitor: Visitor * context: TransformationContext -> ConciseBody
        /// Visits each child of a Node using the supplied visitor, possibly returning a new Node of the same kind in its place.
        abstract visitEachChild: node: 'T * visitor: Visitor * context: TransformationContext -> 'T
        /// Visits each child of a Node using the supplied visitor, possibly returning a new Node of the same kind in its place.
        abstract visitEachChild: node: 'T option * visitor: Visitor * context: TransformationContext * ?nodesVisitor: obj * ?tokenVisitor: Visitor -> 'T option
        abstract createPrinter: ?printerOptions: PrinterOptions * ?handlers: PrintHandlers -> Printer
        abstract findConfigFile: searchPath: string * fileExists: (string -> bool) * ?configName: string -> string
        abstract resolveTripleslashReference: moduleName: string * containingFile: string -> string
        abstract createCompilerHost: options: CompilerOptions * ?setParentNodes: bool -> CompilerHost
        abstract getPreEmitDiagnostics: program: Program * ?sourceFile: SourceFile * ?cancellationToken: CancellationToken -> ResizeArray<Diagnostic>
        abstract formatDiagnostics: diagnostics: ResizeArray<Diagnostic> * host: FormatDiagnosticsHost -> string
        abstract formatDiagnosticsWithColorAndContext: diagnostics: ResizeArray<Diagnostic> * host: FormatDiagnosticsHost -> string
        abstract flattenDiagnosticMessageText: messageText: U2<string, DiagnosticMessageChain> * newLine: string -> string
        /// Create a new 'Program' instance. A Program is an immutable collection of 'SourceFile's and a 'CompilerOptions'
        /// that represent a compilation unit.
        /// 
        /// Creating a program proceeds from a set of root files, expanding the set of inputs by following imports and
        /// triple-slash-reference-path directives transitively. '@types' and triple-slash-reference-types are also pulled in.
        abstract createProgram: rootNames: ResizeArray<string> * options: CompilerOptions * ?host: CompilerHost * ?oldProgram: Program -> Program
        abstract parseCommandLine: commandLine: ReadonlyArray<string> * ?readFile: (string -> string option) -> ParsedCommandLine
        /// Read tsconfig.json file
        abstract readConfigFile: fileName: string * readFile: (string -> string option) -> obj
        /// Parse the text of the tsconfig.json file
        abstract parseConfigFileTextToJson: fileName: string * jsonText: string -> obj
        /// Read tsconfig.json file
        abstract readJsonConfigFile: fileName: string * readFile: (string -> string option) -> JsonSourceFile
        /// Convert the json syntax tree into the json value
        abstract convertToObject: sourceFile: JsonSourceFile * errors: Push<Diagnostic> -> obj
        /// Parse the contents of a config file (tsconfig.json).
        abstract parseJsonConfigFileContent: json: obj * host: ParseConfigHost * basePath: string * ?existingOptions: CompilerOptions * ?configFileName: string * ?resolutionStack: ResizeArray<Path> * ?extraFileExtensions: ReadonlyArray<JsFileExtensionInfo> -> ParsedCommandLine
        /// Parse the contents of a config file (tsconfig.json).
        abstract parseJsonSourceFileConfigFileContent: sourceFile: JsonSourceFile * host: ParseConfigHost * basePath: string * ?existingOptions: CompilerOptions * ?configFileName: string * ?resolutionStack: ResizeArray<Path> * ?extraFileExtensions: ReadonlyArray<JsFileExtensionInfo> -> ParsedCommandLine
        abstract convertCompilerOptionsFromJson: jsonOptions: obj * basePath: string * ?configFileName: string -> obj
        abstract convertTypeAcquisitionFromJson: jsonOptions: obj * basePath: string * ?configFileName: string -> obj
        abstract createClassifier: unit -> Classifier
        abstract createDocumentRegistry: ?useCaseSensitiveFileNames: bool * ?currentDirectory: string -> DocumentRegistry
        abstract preProcessFile: sourceText: string * ?readImportFiles: bool * ?detectJavaScriptImports: bool -> PreProcessedFileInfo
        abstract transpileModule: input: string * transpileOptions: TranspileOptions -> TranspileOutput
        abstract transpile: input: string * ?compilerOptions: CompilerOptions * ?fileName: string * ?diagnostics: ResizeArray<Diagnostic> * ?moduleName: string -> string
        abstract servicesVersion: obj with get, set
        abstract toEditorSettings: options: U2<EditorOptions, EditorSettings> -> EditorSettings
        abstract displayPartsToString: displayParts: ResizeArray<SymbolDisplayPart> -> string
        abstract getDefaultCompilerOptions: unit -> CompilerOptions
        abstract getSupportedCodeFixes: unit -> ResizeArray<string>
        abstract createLanguageServiceSourceFile: fileName: string * scriptSnapshot: IScriptSnapshot * scriptTarget: ScriptTarget * version: string * setNodeParents: bool * ?scriptKind: ScriptKind -> SourceFile
        abstract disableIncrementalParsing: bool with get, set
        abstract updateLanguageServiceSourceFile: sourceFile: SourceFile * scriptSnapshot: IScriptSnapshot * version: string * textChangeRange: TextChangeRange * ?aggressiveChecks: bool -> SourceFile
        abstract createLanguageService: host: LanguageServiceHost * ?documentRegistry: DocumentRegistry -> LanguageService
        /// Get the path of the default library files (lib.d.ts) as distributed with the typescript
        /// node package.
        /// The functionality is not supported if the ts module is consumed outside of a node module.
        abstract getDefaultLibFilePath: options: CompilerOptions -> string
        /// Transform one or more nodes using the supplied transformers.
        abstract transform: source: U2<'T, ResizeArray<'T>> * transformers: ResizeArray<TransformerFactory<'T>> * ?compilerOptions: CompilerOptions -> TransformationResult<'T>

    /// Type of objects whose values are all of the same type.
    /// The `in` and `for-in` operators can *not* be safely used,
    /// since `Object.prototype` may be modified by outside code.
    type [<AllowNullLiteral>] MapLike<'T> =
        [<Emit "$0[$1]{{=$2}}">] abstract Item: index: string -> 'T with get, set

    /// ES6 Map interface, only read methods included. 
    type [<AllowNullLiteral>] ReadonlyMap<'T> =
        abstract get: key: string -> 'T option
        abstract has: key: string -> bool
        abstract forEach: action: ('T -> string -> unit) -> unit
        abstract size: float with get, set
        abstract keys: unit -> Iterator<string>
        abstract values: unit -> Iterator<'T>
        abstract entries: unit -> Iterator<string * 'T>

    /// ES6 Map interface. 
    type [<AllowNullLiteral>] Map<'T> =
        inherit ReadonlyMap<'T>
        abstract set: key: string * value: 'T -> Map<'T>
        abstract delete: key: string -> bool
        abstract clear: unit -> unit

    /// ES6 Iterator type. 
    type [<AllowNullLiteral>] Iterator<'T> =
        abstract next: unit -> obj

    /// Array that is only intended to be pushed to, never read. 
    type [<AllowNullLiteral>] Push<'T> =
        abstract push: [<ParamArray>] values: 'T -> unit

    type Path =
        obj

    type [<AllowNullLiteral>] TextRange =
        abstract pos: float with get, set
        abstract ``end``: float with get, set

    type [<RequireQualifiedAccess>] SyntaxKind =
        | Unknown = 0
        | EndOfFileToken = 1
        | SingleLineCommentTrivia = 2
        | MultiLineCommentTrivia = 3
        | NewLineTrivia = 4
        | WhitespaceTrivia = 5
        | ShebangTrivia = 6
        | ConflictMarkerTrivia = 7
        | NumericLiteral = 8
        | StringLiteral = 9
        | JsxText = 10
        | JsxTextAllWhiteSpaces = 11
        | RegularExpressionLiteral = 12
        | NoSubstitutionTemplateLiteral = 13
        | TemplateHead = 14
        | TemplateMiddle = 15
        | TemplateTail = 16
        | OpenBraceToken = 17
        | CloseBraceToken = 18
        | OpenParenToken = 19
        | CloseParenToken = 20
        | OpenBracketToken = 21
        | CloseBracketToken = 22
        | DotToken = 23
        | DotDotDotToken = 24
        | SemicolonToken = 25
        | CommaToken = 26
        | LessThanToken = 27
        | LessThanSlashToken = 28
        | GreaterThanToken = 29
        | LessThanEqualsToken = 30
        | GreaterThanEqualsToken = 31
        | EqualsEqualsToken = 32
        | ExclamationEqualsToken = 33
        | EqualsEqualsEqualsToken = 34
        | ExclamationEqualsEqualsToken = 35
        | EqualsGreaterThanToken = 36
        | PlusToken = 37
        | MinusToken = 38
        | AsteriskToken = 39
        | AsteriskAsteriskToken = 40
        | SlashToken = 41
        | PercentToken = 42
        | PlusPlusToken = 43
        | MinusMinusToken = 44
        | LessThanLessThanToken = 45
        | GreaterThanGreaterThanToken = 46
        | GreaterThanGreaterThanGreaterThanToken = 47
        | AmpersandToken = 48
        | BarToken = 49
        | CaretToken = 50
        | ExclamationToken = 51
        | TildeToken = 52
        | AmpersandAmpersandToken = 53
        | BarBarToken = 54
        | QuestionToken = 55
        | ColonToken = 56
        | AtToken = 57
        | EqualsToken = 58
        | PlusEqualsToken = 59
        | MinusEqualsToken = 60
        | AsteriskEqualsToken = 61
        | AsteriskAsteriskEqualsToken = 62
        | SlashEqualsToken = 63
        | PercentEqualsToken = 64
        | LessThanLessThanEqualsToken = 65
        | GreaterThanGreaterThanEqualsToken = 66
        | GreaterThanGreaterThanGreaterThanEqualsToken = 67
        | AmpersandEqualsToken = 68
        | BarEqualsToken = 69
        | CaretEqualsToken = 70
        | Identifier = 71
        | BreakKeyword = 72
        | CaseKeyword = 73
        | CatchKeyword = 74
        | ClassKeyword = 75
        | ConstKeyword = 76
        | ContinueKeyword = 77
        | DebuggerKeyword = 78
        | DefaultKeyword = 79
        | DeleteKeyword = 80
        | DoKeyword = 81
        | ElseKeyword = 82
        | EnumKeyword = 83
        | ExportKeyword = 84
        | ExtendsKeyword = 85
        | FalseKeyword = 86
        | FinallyKeyword = 87
        | ForKeyword = 88
        | FunctionKeyword = 89
        | IfKeyword = 90
        | ImportKeyword = 91
        | InKeyword = 92
        | InstanceOfKeyword = 93
        | NewKeyword = 94
        | NullKeyword = 95
        | ReturnKeyword = 96
        | SuperKeyword = 97
        | SwitchKeyword = 98
        | ThisKeyword = 99
        | ThrowKeyword = 100
        | TrueKeyword = 101
        | TryKeyword = 102
        | TypeOfKeyword = 103
        | VarKeyword = 104
        | VoidKeyword = 105
        | WhileKeyword = 106
        | WithKeyword = 107
        | ImplementsKeyword = 108
        | InterfaceKeyword = 109
        | LetKeyword = 110
        | PackageKeyword = 111
        | PrivateKeyword = 112
        | ProtectedKeyword = 113
        | PublicKeyword = 114
        | StaticKeyword = 115
        | YieldKeyword = 116
        | AbstractKeyword = 117
        | AsKeyword = 118
        | AnyKeyword = 119
        | AsyncKeyword = 120
        | AwaitKeyword = 121
        | BooleanKeyword = 122
        | ConstructorKeyword = 123
        | DeclareKeyword = 124
        | GetKeyword = 125
        | IsKeyword = 126
        | KeyOfKeyword = 127
        | ModuleKeyword = 128
        | NamespaceKeyword = 129
        | NeverKeyword = 130
        | ReadonlyKeyword = 131
        | RequireKeyword = 132
        | NumberKeyword = 133
        | ObjectKeyword = 134
        | SetKeyword = 135
        | StringKeyword = 136
        | SymbolKeyword = 137
        | TypeKeyword = 138
        | UndefinedKeyword = 139
        | FromKeyword = 140
        | GlobalKeyword = 141
        | OfKeyword = 142
        | QualifiedName = 143
        | ComputedPropertyName = 144
        | TypeParameter = 145
        | Parameter = 146
        | Decorator = 147
        | PropertySignature = 148
        | PropertyDeclaration = 149
        | MethodSignature = 150
        | MethodDeclaration = 151
        | Constructor = 152
        | GetAccessor = 153
        | SetAccessor = 154
        | CallSignature = 155
        | ConstructSignature = 156
        | IndexSignature = 157
        | TypePredicate = 158
        | TypeReference = 159
        | FunctionType = 160
        | ConstructorType = 161
        | TypeQuery = 162
        | TypeLiteral = 163
        | ArrayType = 164
        | TupleType = 165
        | UnionType = 166
        | IntersectionType = 167
        | ParenthesizedType = 168
        | ThisType = 169
        | TypeOperator = 170
        | IndexedAccessType = 171
        | MappedType = 172
        | LiteralType = 173
        | ObjectBindingPattern = 174
        | ArrayBindingPattern = 175
        | BindingElement = 176
        | ArrayLiteralExpression = 177
        | ObjectLiteralExpression = 178
        | PropertyAccessExpression = 179
        | ElementAccessExpression = 180
        | CallExpression = 181
        | NewExpression = 182
        | TaggedTemplateExpression = 183
        | TypeAssertionExpression = 184
        | ParenthesizedExpression = 185
        | FunctionExpression = 186
        | ArrowFunction = 187
        | DeleteExpression = 188
        | TypeOfExpression = 189
        | VoidExpression = 190
        | AwaitExpression = 191
        | PrefixUnaryExpression = 192
        | PostfixUnaryExpression = 193
        | BinaryExpression = 194
        | ConditionalExpression = 195
        | TemplateExpression = 196
        | YieldExpression = 197
        | SpreadElement = 198
        | ClassExpression = 199
        | OmittedExpression = 200
        | ExpressionWithTypeArguments = 201
        | AsExpression = 202
        | NonNullExpression = 203
        | MetaProperty = 204
        | TemplateSpan = 205
        | SemicolonClassElement = 206
        | Block = 207
        | VariableStatement = 208
        | EmptyStatement = 209
        | ExpressionStatement = 210
        | IfStatement = 211
        | DoStatement = 212
        | WhileStatement = 213
        | ForStatement = 214
        | ForInStatement = 215
        | ForOfStatement = 216
        | ContinueStatement = 217
        | BreakStatement = 218
        | ReturnStatement = 219
        | WithStatement = 220
        | SwitchStatement = 221
        | LabeledStatement = 222
        | ThrowStatement = 223
        | TryStatement = 224
        | DebuggerStatement = 225
        | VariableDeclaration = 226
        | VariableDeclarationList = 227
        | FunctionDeclaration = 228
        | ClassDeclaration = 229
        | InterfaceDeclaration = 230
        | TypeAliasDeclaration = 231
        | EnumDeclaration = 232
        | ModuleDeclaration = 233
        | ModuleBlock = 234
        | CaseBlock = 235
        | NamespaceExportDeclaration = 236
        | ImportEqualsDeclaration = 237
        | ImportDeclaration = 238
        | ImportClause = 239
        | NamespaceImport = 240
        | NamedImports = 241
        | ImportSpecifier = 242
        | ExportAssignment = 243
        | ExportDeclaration = 244
        | NamedExports = 245
        | ExportSpecifier = 246
        | MissingDeclaration = 247
        | ExternalModuleReference = 248
        | JsxElement = 249
        | JsxSelfClosingElement = 250
        | JsxOpeningElement = 251
        | JsxClosingElement = 252
        | JsxAttribute = 253
        | JsxAttributes = 254
        | JsxSpreadAttribute = 255
        | JsxExpression = 256
        | CaseClause = 257
        | DefaultClause = 258
        | HeritageClause = 259
        | CatchClause = 260
        | PropertyAssignment = 261
        | ShorthandPropertyAssignment = 262
        | SpreadAssignment = 263
        | EnumMember = 264
        | SourceFile = 265
        | Bundle = 266
        | JSDocTypeExpression = 267
        | JSDocAllType = 268
        | JSDocUnknownType = 269
        | JSDocNullableType = 270
        | JSDocNonNullableType = 271
        | JSDocOptionalType = 272
        | JSDocFunctionType = 273
        | JSDocVariadicType = 274
        | JSDocComment = 275
        | JSDocTag = 276
        | JSDocAugmentsTag = 277
        | JSDocClassTag = 278
        | JSDocParameterTag = 279
        | JSDocReturnTag = 280
        | JSDocTypeTag = 281
        | JSDocTemplateTag = 282
        | JSDocTypedefTag = 283
        | JSDocPropertyTag = 284
        | JSDocTypeLiteral = 285
        | SyntaxList = 286
        | NotEmittedStatement = 287
        | PartiallyEmittedExpression = 288
        | CommaListExpression = 289
        | MergeDeclarationMarker = 290
        | EndOfDeclarationMarker = 291
        | Count = 292
        | FirstAssignment = 58
        | LastAssignment = 70
        | FirstCompoundAssignment = 59
        | LastCompoundAssignment = 70
        | FirstReservedWord = 72
        | LastReservedWord = 107
        | FirstKeyword = 72
        | LastKeyword = 142
        | FirstFutureReservedWord = 108
        | LastFutureReservedWord = 116
        | FirstTypeNode = 158
        | LastTypeNode = 173
        | FirstPunctuation = 17
        | LastPunctuation = 70
        | FirstToken = 0
        | LastToken = 142
        | FirstTriviaToken = 2
        | LastTriviaToken = 7
        | FirstLiteralToken = 8
        | LastLiteralToken = 13
        | FirstTemplateToken = 13
        | LastTemplateToken = 16
        | FirstBinaryOperator = 27
        | LastBinaryOperator = 70
        | FirstNode = 143
        | FirstJSDocNode = 267
        | LastJSDocNode = 285
        | FirstJSDocTagNode = 276
        | LastJSDocTagNode = 285

    type [<RequireQualifiedAccess>] NodeFlags =
        | None = 0
        | Let = 1
        | Const = 2
        | NestedNamespace = 4
        | Synthesized = 8
        | Namespace = 16
        | ExportContext = 32
        | ContainsThis = 64
        | HasImplicitReturn = 128
        | HasExplicitReturn = 256
        | GlobalAugmentation = 512
        | HasAsyncFunctions = 1024
        | DisallowInContext = 2048
        | YieldContext = 4096
        | DecoratorContext = 8192
        | AwaitContext = 16384
        | ThisNodeHasError = 32768
        | JavaScriptFile = 65536
        | ThisNodeOrAnySubNodesHasError = 131072
        | HasAggregatedChildData = 262144
        | JSDoc = 1048576
        | BlockScoped = 3
        | ReachabilityCheckFlags = 384
        | ReachabilityAndEmitFlags = 1408
        | ContextFlags = 96256
        | TypeExcludesFlags = 20480

    type [<RequireQualifiedAccess>] ModifierFlags =
        | None = 0
        | Export = 1
        | Ambient = 2
        | Public = 4
        | Private = 8
        | Protected = 16
        | Static = 32
        | Readonly = 64
        | Abstract = 128
        | Async = 256
        | Default = 512
        | Const = 2048
        | HasComputedFlags = 536870912
        | AccessibilityModifier = 28
        | ParameterPropertyModifier = 92
        | NonPublicAccessibilityModifier = 24
        | TypeScriptModifier = 2270
        | ExportDefault = 513

    type [<RequireQualifiedAccess>] JsxFlags =
        | None = 0
        | IntrinsicNamedElement = 1
        | IntrinsicIndexedElement = 2
        | IntrinsicElement = 3

    type [<AllowNullLiteral>] Node =
        inherit TextRange
        abstract kind: SyntaxKind with get, set
        abstract flags: NodeFlags with get, set
        abstract decorators: ResizeArray<Decorator> option with get, set
        abstract modifiers: ModifiersArray option with get, set
        abstract parent: Node option with get, set
        abstract getSourceFile: unit -> SourceFile
        abstract getChildCount: ?sourceFile: SourceFile -> float
        abstract getChildAt: index: float * ?sourceFile: SourceFile -> Node
        abstract getChildren: ?sourceFile: SourceFile -> ResizeArray<Node>
        abstract getStart: ?sourceFile: SourceFile * ?includeJsDocComment: bool -> float
        abstract getFullStart: unit -> float
        abstract getEnd: unit -> float
        abstract getWidth: ?sourceFile: SourceFile -> float
        abstract getFullWidth: unit -> float
        abstract getLeadingTriviaWidth: ?sourceFile: SourceFile -> float
        abstract getFullText: ?sourceFile: SourceFile -> string
        abstract getText: ?sourceFile: SourceFile -> string
        abstract getFirstToken: ?sourceFile: SourceFile -> Node
        abstract getLastToken: ?sourceFile: SourceFile -> Node
        abstract forEachChild: cbNode: (Node -> 'T option) * ?cbNodeArray: (ResizeArray<Node> -> 'T option) -> 'T option

    type [<AllowNullLiteral>] NodeArray<'T> =
        inherit ReadonlyArray<'T>
        inherit TextRange
        abstract hasTrailingComma: bool option with get, set

    type [<AllowNullLiteral>] Token<'TKind> =
        inherit Node
        abstract kind: 'TKind with get, set

    type DotDotDotToken =
        Token<SyntaxKind>

    type QuestionToken =
        Token<SyntaxKind>

    type ColonToken =
        Token<SyntaxKind>

    type EqualsToken =
        Token<SyntaxKind>

    type AsteriskToken =
        Token<SyntaxKind>

    type EqualsGreaterThanToken =
        Token<SyntaxKind>

    type EndOfFileToken =
        Token<SyntaxKind>

    type AtToken =
        Token<SyntaxKind>

    type ReadonlyToken =
        Token<SyntaxKind>

    type AwaitKeywordToken =
        Token<SyntaxKind>

    type Modifier =
        Token<SyntaxKind>

    type ModifiersArray =
        ResizeArray<Modifier>

    type [<AllowNullLiteral>] Identifier =
        inherit PrimaryExpression
        abstract kind: SyntaxKind with get, set
        /// Prefer to use `id.unescapedText`. (Note: This is available only in services, not internally to the TypeScript compiler.)
        /// Text of identifier, but if the identifier begins with two underscores, this will begin with three.
        abstract escapedText: __String with get, set
        abstract originalKeywordKind: SyntaxKind option with get, set
        abstract isInJSDocNamespace: bool option with get, set
        abstract text: string with get, set

    type [<AllowNullLiteral>] TransientIdentifier =
        inherit Identifier
        abstract resolvedSymbol: Symbol with get, set

    type [<AllowNullLiteral>] QualifiedName =
        inherit Node
        abstract kind: SyntaxKind with get, set
        abstract left: EntityName with get, set
        abstract right: Identifier with get, set

    type EntityName =
        U2<Identifier, QualifiedName>

    type PropertyName =
        U4<Identifier, StringLiteral, NumericLiteral, ComputedPropertyName>

    type DeclarationName =
        U5<Identifier, StringLiteral, NumericLiteral, ComputedPropertyName, BindingPattern>

    type [<AllowNullLiteral>] Declaration =
        inherit Node
        abstract _declarationBrand: obj with get, set

    type [<AllowNullLiteral>] NamedDeclaration =
        inherit Declaration
        abstract name: DeclarationName option with get, set

    type [<AllowNullLiteral>] DeclarationStatement =
        inherit NamedDeclaration
        inherit Statement
        abstract name: U3<Identifier, StringLiteral, NumericLiteral> option with get, set

    type [<AllowNullLiteral>] ComputedPropertyName =
        inherit Node
        abstract kind: SyntaxKind with get, set
        abstract expression: Expression with get, set

    type [<AllowNullLiteral>] Decorator =
        inherit Node
        abstract kind: SyntaxKind with get, set
        abstract expression: LeftHandSideExpression with get, set

    type [<AllowNullLiteral>] TypeParameterDeclaration =
        inherit NamedDeclaration
        abstract kind: SyntaxKind with get, set
        abstract parent: DeclarationWithTypeParameters option with get, set
        abstract name: Identifier with get, set
        abstract ``constraint``: TypeNode option with get, set
        abstract ``default``: TypeNode option with get, set
        abstract expression: Expression option with get, set

    type [<AllowNullLiteral>] SignatureDeclaration =
        inherit NamedDeclaration
        abstract name: PropertyName option with get, set
        abstract typeParameters: ResizeArray<TypeParameterDeclaration> option with get, set
        abstract parameters: ResizeArray<ParameterDeclaration> with get, set
        abstract ``type``: TypeNode option with get, set

    type [<AllowNullLiteral>] CallSignatureDeclaration =
        inherit SignatureDeclaration
        inherit TypeElement
        abstract kind: SyntaxKind with get, set

    type [<AllowNullLiteral>] ConstructSignatureDeclaration =
        inherit SignatureDeclaration
        inherit TypeElement
        abstract kind: SyntaxKind with get, set

    type BindingName =
        U2<Identifier, BindingPattern>

    type [<AllowNullLiteral>] VariableDeclaration =
        inherit NamedDeclaration
        abstract kind: SyntaxKind with get, set
        abstract parent: U2<VariableDeclarationList, CatchClause> option with get, set
        abstract name: BindingName with get, set
        abstract ``type``: TypeNode option with get, set
        abstract initializer: Expression option with get, set

    type [<AllowNullLiteral>] VariableDeclarationList =
        inherit Node
        abstract kind: SyntaxKind with get, set
        abstract parent: U4<VariableStatement, ForStatement, ForOfStatement, ForInStatement> option with get, set
        abstract declarations: ResizeArray<VariableDeclaration> with get, set

    type [<AllowNullLiteral>] ParameterDeclaration =
        inherit NamedDeclaration
        abstract kind: SyntaxKind with get, set
        abstract parent: SignatureDeclaration option with get, set
        abstract dotDotDotToken: DotDotDotToken option with get, set
        abstract name: BindingName with get, set
        abstract questionToken: QuestionToken option with get, set
        abstract ``type``: TypeNode option with get, set
        abstract initializer: Expression option with get, set

    type [<AllowNullLiteral>] BindingElement =
        inherit NamedDeclaration
        abstract kind: SyntaxKind with get, set
        abstract parent: BindingPattern option with get, set
        abstract propertyName: PropertyName option with get, set
        abstract dotDotDotToken: DotDotDotToken option with get, set
        abstract name: BindingName with get, set
        abstract initializer: Expression option with get, set

    type [<AllowNullLiteral>] PropertySignature =
        inherit TypeElement
        abstract kind: SyntaxKind with get, set
        abstract name: PropertyName with get, set
        abstract questionToken: QuestionToken option with get, set
        abstract ``type``: TypeNode option with get, set
        abstract initializer: Expression option with get, set

    type [<AllowNullLiteral>] PropertyDeclaration =
        inherit ClassElement
        abstract kind: SyntaxKind with get, set
        abstract questionToken: QuestionToken option with get, set
        abstract name: PropertyName with get, set
        abstract ``type``: TypeNode option with get, set
        abstract initializer: Expression option with get, set

    type [<AllowNullLiteral>] ObjectLiteralElement =
        inherit NamedDeclaration
        abstract _objectLiteralBrandBrand: obj with get, set
        abstract name: PropertyName option with get, set

    type ObjectLiteralElementLike =
        U5<PropertyAssignment, ShorthandPropertyAssignment, SpreadAssignment, MethodDeclaration, AccessorDeclaration>

    type [<AllowNullLiteral>] PropertyAssignment =
        inherit ObjectLiteralElement
        abstract kind: SyntaxKind with get, set
        abstract name: PropertyName with get, set
        abstract questionToken: QuestionToken option with get, set
        abstract initializer: Expression with get, set

    type [<AllowNullLiteral>] ShorthandPropertyAssignment =
        inherit ObjectLiteralElement
        abstract kind: SyntaxKind with get, set
        abstract name: Identifier with get, set
        abstract questionToken: QuestionToken option with get, set
        abstract equalsToken: Token<SyntaxKind> option with get, set
        abstract objectAssignmentInitializer: Expression option with get, set

    type [<AllowNullLiteral>] SpreadAssignment =
        inherit ObjectLiteralElement
        abstract kind: SyntaxKind with get, set
        abstract expression: Expression with get, set

    type [<AllowNullLiteral>] VariableLikeDeclaration =
        inherit NamedDeclaration
        abstract propertyName: PropertyName option with get, set
        abstract dotDotDotToken: DotDotDotToken option with get, set
        abstract name: DeclarationName option with get, set
        abstract questionToken: QuestionToken option with get, set
        abstract ``type``: TypeNode option with get, set
        abstract initializer: Expression option with get, set

    type [<AllowNullLiteral>] PropertyLikeDeclaration =
        inherit NamedDeclaration
        abstract name: PropertyName with get, set

    type [<AllowNullLiteral>] ObjectBindingPattern =
        inherit Node
        abstract kind: SyntaxKind with get, set
        abstract parent: U3<VariableDeclaration, ParameterDeclaration, BindingElement> option with get, set
        abstract elements: ResizeArray<BindingElement> with get, set

    type [<AllowNullLiteral>] ArrayBindingPattern =
        inherit Node
        abstract kind: SyntaxKind with get, set
        abstract parent: U3<VariableDeclaration, ParameterDeclaration, BindingElement> option with get, set
        abstract elements: ResizeArray<ArrayBindingElement> with get, set

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
        inherit SignatureDeclaration
        abstract _functionLikeDeclarationBrand: obj with get, set
        abstract asteriskToken: AsteriskToken option with get, set
        abstract questionToken: QuestionToken option with get, set
        abstract body: U2<Block, Expression> option with get, set

    type FunctionLikeDeclaration =
        obj

    type FunctionLike =
        obj

    type [<AllowNullLiteral>] FunctionDeclaration =
        inherit FunctionLikeDeclarationBase
        inherit DeclarationStatement
        abstract kind: SyntaxKind with get, set
        abstract name: Identifier option with get, set
        abstract body: FunctionBody option with get, set

    type [<AllowNullLiteral>] MethodSignature =
        inherit SignatureDeclaration
        inherit TypeElement
        abstract kind: SyntaxKind with get, set
        abstract name: PropertyName with get, set

    type [<AllowNullLiteral>] MethodDeclaration =
        inherit FunctionLikeDeclarationBase
        inherit ClassElement
        inherit ObjectLiteralElement
        abstract kind: SyntaxKind with get, set
        abstract name: PropertyName with get, set
        abstract body: FunctionBody option with get, set

    type [<AllowNullLiteral>] ConstructorDeclaration =
        inherit FunctionLikeDeclarationBase
        inherit ClassElement
        abstract kind: SyntaxKind with get, set
        abstract parent: U2<ClassDeclaration, ClassExpression> option with get, set
        abstract body: FunctionBody option with get, set

    /// For when we encounter a semicolon in a class declaration. ES6 allows these as class elements. 
    type [<AllowNullLiteral>] SemicolonClassElement =
        inherit ClassElement
        abstract kind: SyntaxKind with get, set
        abstract parent: U2<ClassDeclaration, ClassExpression> option with get, set

    type [<AllowNullLiteral>] GetAccessorDeclaration =
        inherit FunctionLikeDeclarationBase
        inherit ClassElement
        inherit ObjectLiteralElement
        abstract kind: SyntaxKind with get, set
        abstract parent: U3<ClassDeclaration, ClassExpression, ObjectLiteralExpression> option with get, set
        abstract name: PropertyName with get, set
        abstract body: FunctionBody with get, set

    type [<AllowNullLiteral>] SetAccessorDeclaration =
        inherit FunctionLikeDeclarationBase
        inherit ClassElement
        inherit ObjectLiteralElement
        abstract kind: SyntaxKind with get, set
        abstract parent: U3<ClassDeclaration, ClassExpression, ObjectLiteralExpression> option with get, set
        abstract name: PropertyName with get, set
        abstract body: FunctionBody with get, set

    type AccessorDeclaration =
        U2<GetAccessorDeclaration, SetAccessorDeclaration>

    type [<AllowNullLiteral>] IndexSignatureDeclaration =
        inherit SignatureDeclaration
        inherit ClassElement
        inherit TypeElement
        abstract kind: SyntaxKind with get, set
        abstract parent: U4<ClassDeclaration, ClassExpression, InterfaceDeclaration, TypeLiteralNode> option with get, set

    type [<AllowNullLiteral>] TypeNode =
        inherit Node
        abstract _typeNodeBrand: obj with get, set

    type [<AllowNullLiteral>] KeywordTypeNode =
        inherit TypeNode
        abstract kind: SyntaxKind with get, set

    type [<AllowNullLiteral>] ThisTypeNode =
        inherit TypeNode
        abstract kind: SyntaxKind with get, set

    type FunctionOrConstructorTypeNode =
        U2<FunctionTypeNode, ConstructorTypeNode>

    type [<AllowNullLiteral>] FunctionTypeNode =
        inherit TypeNode
        inherit SignatureDeclaration
        abstract kind: SyntaxKind with get, set

    type [<AllowNullLiteral>] ConstructorTypeNode =
        inherit TypeNode
        inherit SignatureDeclaration
        abstract kind: SyntaxKind with get, set

    type TypeReferenceType =
        U2<TypeReferenceNode, ExpressionWithTypeArguments>

    type [<AllowNullLiteral>] TypeReferenceNode =
        inherit TypeNode
        abstract kind: SyntaxKind with get, set
        abstract typeName: EntityName with get, set
        abstract typeArguments: ResizeArray<TypeNode> option with get, set

    type [<AllowNullLiteral>] TypePredicateNode =
        inherit TypeNode
        abstract kind: SyntaxKind with get, set
        abstract parameterName: U2<Identifier, ThisTypeNode> with get, set
        abstract ``type``: TypeNode with get, set

    type [<AllowNullLiteral>] TypeQueryNode =
        inherit TypeNode
        abstract kind: SyntaxKind with get, set
        abstract exprName: EntityName with get, set

    type [<AllowNullLiteral>] TypeLiteralNode =
        inherit TypeNode
        inherit Declaration
        abstract kind: SyntaxKind with get, set
        abstract members: ResizeArray<TypeElement> with get, set

    type [<AllowNullLiteral>] ArrayTypeNode =
        inherit TypeNode
        abstract kind: SyntaxKind with get, set
        abstract elementType: TypeNode with get, set

    type [<AllowNullLiteral>] TupleTypeNode =
        inherit TypeNode
        abstract kind: SyntaxKind with get, set
        abstract elementTypes: ResizeArray<TypeNode> with get, set

    type UnionOrIntersectionTypeNode =
        U2<UnionTypeNode, IntersectionTypeNode>

    type [<AllowNullLiteral>] UnionTypeNode =
        inherit TypeNode
        abstract kind: SyntaxKind with get, set
        abstract types: ResizeArray<TypeNode> with get, set

    type [<AllowNullLiteral>] IntersectionTypeNode =
        inherit TypeNode
        abstract kind: SyntaxKind with get, set
        abstract types: ResizeArray<TypeNode> with get, set

    type [<AllowNullLiteral>] ParenthesizedTypeNode =
        inherit TypeNode
        abstract kind: SyntaxKind with get, set
        abstract ``type``: TypeNode with get, set

    type [<AllowNullLiteral>] TypeOperatorNode =
        inherit TypeNode
        abstract kind: SyntaxKind with get, set
        abstract operator: SyntaxKind with get, set
        abstract ``type``: TypeNode with get, set

    type [<AllowNullLiteral>] IndexedAccessTypeNode =
        inherit TypeNode
        abstract kind: SyntaxKind with get, set
        abstract objectType: TypeNode with get, set
        abstract indexType: TypeNode with get, set

    type [<AllowNullLiteral>] MappedTypeNode =
        inherit TypeNode
        inherit Declaration
        abstract kind: SyntaxKind with get, set
        abstract parent: TypeAliasDeclaration option with get, set
        abstract readonlyToken: ReadonlyToken option with get, set
        abstract typeParameter: TypeParameterDeclaration with get, set
        abstract questionToken: QuestionToken option with get, set
        abstract ``type``: TypeNode option with get, set

    type [<AllowNullLiteral>] LiteralTypeNode =
        inherit TypeNode
        abstract kind: SyntaxKind with get, set
        abstract literal: Expression with get, set

    type [<AllowNullLiteral>] StringLiteral =
        inherit LiteralExpression
        abstract kind: SyntaxKind with get, set

    type [<AllowNullLiteral>] Expression =
        inherit Node
        abstract _expressionBrand: obj with get, set

    type [<AllowNullLiteral>] OmittedExpression =
        inherit Expression
        abstract kind: SyntaxKind with get, set

    type [<AllowNullLiteral>] PartiallyEmittedExpression =
        inherit LeftHandSideExpression
        abstract kind: SyntaxKind with get, set
        abstract expression: Expression with get, set

    type [<AllowNullLiteral>] UnaryExpression =
        inherit Expression
        abstract _unaryExpressionBrand: obj with get, set

    type IncrementExpression =
        UpdateExpression

    type [<AllowNullLiteral>] UpdateExpression =
        inherit UnaryExpression
        abstract _updateExpressionBrand: obj with get, set

    type PrefixUnaryOperator =
        SyntaxKind

    type [<AllowNullLiteral>] PrefixUnaryExpression =
        inherit UpdateExpression
        abstract kind: SyntaxKind with get, set
        abstract operator: PrefixUnaryOperator with get, set
        abstract operand: UnaryExpression with get, set

    type PostfixUnaryOperator =
        SyntaxKind

    type [<AllowNullLiteral>] PostfixUnaryExpression =
        inherit UpdateExpression
        abstract kind: SyntaxKind with get, set
        abstract operand: LeftHandSideExpression with get, set
        abstract operator: PostfixUnaryOperator with get, set

    type [<AllowNullLiteral>] LeftHandSideExpression =
        inherit UpdateExpression
        abstract _leftHandSideExpressionBrand: obj with get, set

    type [<AllowNullLiteral>] MemberExpression =
        inherit LeftHandSideExpression
        abstract _memberExpressionBrand: obj with get, set

    type [<AllowNullLiteral>] PrimaryExpression =
        inherit MemberExpression
        abstract _primaryExpressionBrand: obj with get, set

    type [<AllowNullLiteral>] NullLiteral =
        inherit PrimaryExpression
        inherit TypeNode
        abstract kind: SyntaxKind with get, set

    type [<AllowNullLiteral>] BooleanLiteral =
        inherit PrimaryExpression
        inherit TypeNode
        abstract kind: SyntaxKind with get, set

    type [<AllowNullLiteral>] ThisExpression =
        inherit PrimaryExpression
        inherit KeywordTypeNode
        abstract kind: SyntaxKind with get, set

    type [<AllowNullLiteral>] SuperExpression =
        inherit PrimaryExpression
        abstract kind: SyntaxKind with get, set

    type [<AllowNullLiteral>] ImportExpression =
        inherit PrimaryExpression
        abstract kind: SyntaxKind with get, set

    type [<AllowNullLiteral>] DeleteExpression =
        inherit UnaryExpression
        abstract kind: SyntaxKind with get, set
        abstract expression: UnaryExpression with get, set

    type [<AllowNullLiteral>] TypeOfExpression =
        inherit UnaryExpression
        abstract kind: SyntaxKind with get, set
        abstract expression: UnaryExpression with get, set

    type [<AllowNullLiteral>] VoidExpression =
        inherit UnaryExpression
        abstract kind: SyntaxKind with get, set
        abstract expression: UnaryExpression with get, set

    type [<AllowNullLiteral>] AwaitExpression =
        inherit UnaryExpression
        abstract kind: SyntaxKind with get, set
        abstract expression: UnaryExpression with get, set

    type [<AllowNullLiteral>] YieldExpression =
        inherit Expression
        abstract kind: SyntaxKind with get, set
        abstract asteriskToken: AsteriskToken option with get, set
        abstract expression: Expression option with get, set

    type ExponentiationOperator =
        SyntaxKind

    type MultiplicativeOperator =
        SyntaxKind

    type MultiplicativeOperatorOrHigher =
        U2<ExponentiationOperator, MultiplicativeOperator>

    type AdditiveOperator =
        SyntaxKind

    type AdditiveOperatorOrHigher =
        U2<MultiplicativeOperatorOrHigher, AdditiveOperator>

    type ShiftOperator =
        SyntaxKind

    type ShiftOperatorOrHigher =
        U2<AdditiveOperatorOrHigher, ShiftOperator>

    type RelationalOperator =
        SyntaxKind

    type RelationalOperatorOrHigher =
        U2<ShiftOperatorOrHigher, RelationalOperator>

    type EqualityOperator =
        SyntaxKind

    type EqualityOperatorOrHigher =
        U2<RelationalOperatorOrHigher, EqualityOperator>

    type BitwiseOperator =
        SyntaxKind

    type BitwiseOperatorOrHigher =
        U2<EqualityOperatorOrHigher, BitwiseOperator>

    type LogicalOperator =
        SyntaxKind

    type LogicalOperatorOrHigher =
        U2<BitwiseOperatorOrHigher, LogicalOperator>

    type CompoundAssignmentOperator =
        SyntaxKind

    type AssignmentOperator =
        U2<SyntaxKind, CompoundAssignmentOperator>

    type AssignmentOperatorOrHigher =
        U2<LogicalOperatorOrHigher, AssignmentOperator>

    type BinaryOperator =
        U2<AssignmentOperatorOrHigher, SyntaxKind>

    type BinaryOperatorToken =
        Token<BinaryOperator>

    type [<AllowNullLiteral>] BinaryExpression =
        inherit Expression
        inherit Declaration
        abstract kind: SyntaxKind with get, set
        abstract left: Expression with get, set
        abstract operatorToken: BinaryOperatorToken with get, set
        abstract right: Expression with get, set

    type AssignmentOperatorToken =
        Token<AssignmentOperator>

    type [<AllowNullLiteral>] AssignmentExpression<'TOperator> =
        inherit BinaryExpression
        abstract left: LeftHandSideExpression with get, set
        abstract operatorToken: 'TOperator with get, set

    type [<AllowNullLiteral>] ObjectDestructuringAssignment =
        inherit AssignmentExpression<EqualsToken>
        abstract left: ObjectLiteralExpression with get, set

    type [<AllowNullLiteral>] ArrayDestructuringAssignment =
        inherit AssignmentExpression<EqualsToken>
        abstract left: ArrayLiteralExpression with get, set

    type DestructuringAssignment =
        U2<ObjectDestructuringAssignment, ArrayDestructuringAssignment>

    type BindingOrAssignmentElement =
        obj

    type BindingOrAssignmentElementRestIndicator =
        U3<DotDotDotToken, SpreadElement, SpreadAssignment>

    type BindingOrAssignmentElementTarget =
        U2<BindingOrAssignmentPattern, Expression>

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
        abstract kind: SyntaxKind with get, set
        abstract condition: Expression with get, set
        abstract questionToken: QuestionToken with get, set
        abstract whenTrue: Expression with get, set
        abstract colonToken: ColonToken with get, set
        abstract whenFalse: Expression with get, set

    type FunctionBody =
        Block

    type ConciseBody =
        U2<FunctionBody, Expression>

    type [<AllowNullLiteral>] FunctionExpression =
        inherit PrimaryExpression
        inherit FunctionLikeDeclarationBase
        abstract kind: SyntaxKind with get, set
        abstract name: Identifier option with get, set
        abstract body: FunctionBody with get, set

    type [<AllowNullLiteral>] ArrowFunction =
        inherit Expression
        inherit FunctionLikeDeclarationBase
        abstract kind: SyntaxKind with get, set
        abstract equalsGreaterThanToken: EqualsGreaterThanToken with get, set
        abstract body: ConciseBody with get, set

    type [<AllowNullLiteral>] LiteralLikeNode =
        inherit Node
        abstract text: string with get, set
        abstract isUnterminated: bool option with get, set
        abstract hasExtendedUnicodeEscape: bool option with get, set

    type [<AllowNullLiteral>] LiteralExpression =
        inherit LiteralLikeNode
        inherit PrimaryExpression
        abstract _literalExpressionBrand: obj with get, set

    type [<AllowNullLiteral>] RegularExpressionLiteral =
        inherit LiteralExpression
        abstract kind: SyntaxKind with get, set

    type [<AllowNullLiteral>] NoSubstitutionTemplateLiteral =
        inherit LiteralExpression
        abstract kind: SyntaxKind with get, set

    type [<AllowNullLiteral>] NumericLiteral =
        inherit LiteralExpression
        abstract kind: SyntaxKind with get, set

    type [<AllowNullLiteral>] TemplateHead =
        inherit LiteralLikeNode
        abstract kind: SyntaxKind with get, set
        abstract parent: TemplateExpression option with get, set

    type [<AllowNullLiteral>] TemplateMiddle =
        inherit LiteralLikeNode
        abstract kind: SyntaxKind with get, set
        abstract parent: TemplateSpan option with get, set

    type [<AllowNullLiteral>] TemplateTail =
        inherit LiteralLikeNode
        abstract kind: SyntaxKind with get, set
        abstract parent: TemplateSpan option with get, set

    type TemplateLiteral =
        U2<TemplateExpression, NoSubstitutionTemplateLiteral>

    type [<AllowNullLiteral>] TemplateExpression =
        inherit PrimaryExpression
        abstract kind: SyntaxKind with get, set
        abstract head: TemplateHead with get, set
        abstract templateSpans: ResizeArray<TemplateSpan> with get, set

    type [<AllowNullLiteral>] TemplateSpan =
        inherit Node
        abstract kind: SyntaxKind with get, set
        abstract parent: TemplateExpression option with get, set
        abstract expression: Expression with get, set
        abstract literal: U2<TemplateMiddle, TemplateTail> with get, set

    type [<AllowNullLiteral>] ParenthesizedExpression =
        inherit PrimaryExpression
        abstract kind: SyntaxKind with get, set
        abstract expression: Expression with get, set

    type [<AllowNullLiteral>] ArrayLiteralExpression =
        inherit PrimaryExpression
        abstract kind: SyntaxKind with get, set
        abstract elements: ResizeArray<Expression> with get, set

    type [<AllowNullLiteral>] SpreadElement =
        inherit Expression
        abstract kind: SyntaxKind with get, set
        abstract expression: Expression with get, set

    /// This interface is a base interface for ObjectLiteralExpression and JSXAttributes to extend from. JSXAttributes is similar to
    /// ObjectLiteralExpression in that it contains array of properties; however, JSXAttributes' properties can only be
    /// JSXAttribute or JSXSpreadAttribute. ObjectLiteralExpression, on the other hand, can only have properties of type
    /// ObjectLiteralElement (e.g. PropertyAssignment, ShorthandPropertyAssignment etc.)
    type [<AllowNullLiteral>] ObjectLiteralExpressionBase<'T> =
        inherit PrimaryExpression
        inherit Declaration
        abstract properties: ResizeArray<'T> with get, set

    type [<AllowNullLiteral>] ObjectLiteralExpression =
        inherit ObjectLiteralExpressionBase<ObjectLiteralElementLike>
        abstract kind: SyntaxKind with get, set

    type EntityNameExpression =
        U3<Identifier, PropertyAccessEntityNameExpression, ParenthesizedExpression>

    type EntityNameOrEntityNameExpression =
        U2<EntityName, EntityNameExpression>

    type [<AllowNullLiteral>] PropertyAccessExpression =
        inherit MemberExpression
        inherit NamedDeclaration
        abstract kind: SyntaxKind with get, set
        abstract expression: LeftHandSideExpression with get, set
        abstract name: Identifier with get, set

    type [<AllowNullLiteral>] SuperPropertyAccessExpression =
        inherit PropertyAccessExpression
        abstract expression: SuperExpression with get, set

    /// Brand for a PropertyAccessExpression which, like a QualifiedName, consists of a sequence of identifiers separated by dots. 
    type [<AllowNullLiteral>] PropertyAccessEntityNameExpression =
        inherit PropertyAccessExpression
        abstract _propertyAccessExpressionLikeQualifiedNameBrand: obj option with get, set
        abstract expression: EntityNameExpression with get, set

    type [<AllowNullLiteral>] ElementAccessExpression =
        inherit MemberExpression
        abstract kind: SyntaxKind with get, set
        abstract expression: LeftHandSideExpression with get, set
        abstract argumentExpression: Expression option with get, set

    type [<AllowNullLiteral>] SuperElementAccessExpression =
        inherit ElementAccessExpression
        abstract expression: SuperExpression with get, set

    type SuperProperty =
        U2<SuperPropertyAccessExpression, SuperElementAccessExpression>

    type [<AllowNullLiteral>] CallExpression =
        inherit LeftHandSideExpression
        inherit Declaration
        abstract kind: SyntaxKind with get, set
        abstract expression: LeftHandSideExpression with get, set
        abstract typeArguments: ResizeArray<TypeNode> option with get, set
        abstract arguments: ResizeArray<Expression> with get, set

    type [<AllowNullLiteral>] SuperCall =
        inherit CallExpression
        abstract expression: SuperExpression with get, set

    type [<AllowNullLiteral>] ImportCall =
        inherit CallExpression
        abstract expression: ImportExpression with get, set

    type [<AllowNullLiteral>] ExpressionWithTypeArguments =
        inherit TypeNode
        abstract kind: SyntaxKind with get, set
        abstract parent: HeritageClause option with get, set
        abstract expression: LeftHandSideExpression with get, set
        abstract typeArguments: ResizeArray<TypeNode> option with get, set

    type [<AllowNullLiteral>] NewExpression =
        inherit PrimaryExpression
        inherit Declaration
        abstract kind: SyntaxKind with get, set
        abstract expression: LeftHandSideExpression with get, set
        abstract typeArguments: ResizeArray<TypeNode> option with get, set
        abstract arguments: ResizeArray<Expression> option with get, set

    type [<AllowNullLiteral>] TaggedTemplateExpression =
        inherit MemberExpression
        abstract kind: SyntaxKind with get, set
        abstract tag: LeftHandSideExpression with get, set
        abstract template: TemplateLiteral with get, set

    type CallLikeExpression =
        U5<CallExpression, NewExpression, TaggedTemplateExpression, Decorator, JsxOpeningLikeElement>

    type [<AllowNullLiteral>] AsExpression =
        inherit Expression
        abstract kind: SyntaxKind with get, set
        abstract expression: Expression with get, set
        abstract ``type``: TypeNode with get, set

    type [<AllowNullLiteral>] TypeAssertion =
        inherit UnaryExpression
        abstract kind: SyntaxKind with get, set
        abstract ``type``: TypeNode with get, set
        abstract expression: UnaryExpression with get, set

    type AssertionExpression =
        U2<TypeAssertion, AsExpression>

    type [<AllowNullLiteral>] NonNullExpression =
        inherit LeftHandSideExpression
        abstract kind: SyntaxKind with get, set
        abstract expression: Expression with get, set

    type [<AllowNullLiteral>] MetaProperty =
        inherit PrimaryExpression
        abstract kind: SyntaxKind with get, set
        abstract keywordToken: SyntaxKind with get, set
        abstract name: Identifier with get, set

    type [<AllowNullLiteral>] JsxElement =
        inherit PrimaryExpression
        abstract kind: SyntaxKind with get, set
        abstract openingElement: JsxOpeningElement with get, set
        abstract children: ResizeArray<JsxChild> with get, set
        abstract closingElement: JsxClosingElement with get, set

    type JsxOpeningLikeElement =
        U2<JsxSelfClosingElement, JsxOpeningElement>

    type JsxAttributeLike =
        U2<JsxAttribute, JsxSpreadAttribute>

    type JsxTagNameExpression =
        U2<PrimaryExpression, PropertyAccessExpression>

    type [<AllowNullLiteral>] JsxAttributes =
        inherit ObjectLiteralExpressionBase<JsxAttributeLike>
        abstract parent: JsxOpeningLikeElement option with get, set

    type [<AllowNullLiteral>] JsxOpeningElement =
        inherit Expression
        abstract kind: SyntaxKind with get, set
        abstract parent: JsxElement option with get, set
        abstract tagName: JsxTagNameExpression with get, set
        abstract attributes: JsxAttributes with get, set

    type [<AllowNullLiteral>] JsxSelfClosingElement =
        inherit PrimaryExpression
        abstract kind: SyntaxKind with get, set
        abstract tagName: JsxTagNameExpression with get, set
        abstract attributes: JsxAttributes with get, set

    type [<AllowNullLiteral>] JsxAttribute =
        inherit ObjectLiteralElement
        abstract kind: SyntaxKind with get, set
        abstract parent: JsxAttributes option with get, set
        abstract name: Identifier with get, set
        abstract initializer: U2<StringLiteral, JsxExpression> option with get, set

    type [<AllowNullLiteral>] JsxSpreadAttribute =
        inherit ObjectLiteralElement
        abstract kind: SyntaxKind with get, set
        abstract parent: JsxAttributes option with get, set
        abstract expression: Expression with get, set

    type [<AllowNullLiteral>] JsxClosingElement =
        inherit Node
        abstract kind: SyntaxKind with get, set
        abstract parent: JsxElement option with get, set
        abstract tagName: JsxTagNameExpression with get, set

    type [<AllowNullLiteral>] JsxExpression =
        inherit Expression
        abstract kind: SyntaxKind with get, set
        abstract parent: U2<JsxElement, JsxAttributeLike> option with get, set
        abstract dotDotDotToken: Token<SyntaxKind> option with get, set
        abstract expression: Expression option with get, set

    type [<AllowNullLiteral>] JsxText =
        inherit Node
        abstract kind: SyntaxKind with get, set
        abstract containsOnlyWhiteSpaces: bool with get, set
        abstract parent: JsxElement option with get, set

    type JsxChild =
        U4<JsxText, JsxExpression, JsxElement, JsxSelfClosingElement>

    type [<AllowNullLiteral>] Statement =
        inherit Node
        abstract _statementBrand: obj with get, set

    type [<AllowNullLiteral>] NotEmittedStatement =
        inherit Statement
        abstract kind: SyntaxKind with get, set

    /// A list of comma-seperated expressions. This node is only created by transformations.
    type [<AllowNullLiteral>] CommaListExpression =
        inherit Expression
        abstract kind: SyntaxKind with get, set
        abstract elements: ResizeArray<Expression> with get, set

    type [<AllowNullLiteral>] EmptyStatement =
        inherit Statement
        abstract kind: SyntaxKind with get, set

    type [<AllowNullLiteral>] DebuggerStatement =
        inherit Statement
        abstract kind: SyntaxKind with get, set

    type [<AllowNullLiteral>] MissingDeclaration =
        inherit DeclarationStatement
        inherit ClassElement
        inherit ObjectLiteralElement
        inherit TypeElement
        abstract kind: SyntaxKind with get, set
        abstract name: Identifier option with get, set

    type BlockLike =
        U4<SourceFile, Block, ModuleBlock, CaseOrDefaultClause>

    type [<AllowNullLiteral>] Block =
        inherit Statement
        abstract kind: SyntaxKind with get, set
        abstract statements: ResizeArray<Statement> with get, set

    type [<AllowNullLiteral>] VariableStatement =
        inherit Statement
        abstract kind: SyntaxKind with get, set
        abstract declarationList: VariableDeclarationList with get, set

    type [<AllowNullLiteral>] ExpressionStatement =
        inherit Statement
        abstract kind: SyntaxKind with get, set
        abstract expression: Expression with get, set

    type [<AllowNullLiteral>] IfStatement =
        inherit Statement
        abstract kind: SyntaxKind with get, set
        abstract expression: Expression with get, set
        abstract thenStatement: Statement with get, set
        abstract elseStatement: Statement option with get, set

    type [<AllowNullLiteral>] IterationStatement =
        inherit Statement
        abstract statement: Statement with get, set

    type [<AllowNullLiteral>] DoStatement =
        inherit IterationStatement
        abstract kind: SyntaxKind with get, set
        abstract expression: Expression with get, set

    type [<AllowNullLiteral>] WhileStatement =
        inherit IterationStatement
        abstract kind: SyntaxKind with get, set
        abstract expression: Expression with get, set

    type ForInitializer =
        U2<VariableDeclarationList, Expression>

    type [<AllowNullLiteral>] ForStatement =
        inherit IterationStatement
        abstract kind: SyntaxKind with get, set
        abstract initializer: ForInitializer option with get, set
        abstract condition: Expression option with get, set
        abstract incrementor: Expression option with get, set

    type ForInOrOfStatement =
        U2<ForInStatement, ForOfStatement>

    type [<AllowNullLiteral>] ForInStatement =
        inherit IterationStatement
        abstract kind: SyntaxKind with get, set
        abstract initializer: ForInitializer with get, set
        abstract expression: Expression with get, set

    type [<AllowNullLiteral>] ForOfStatement =
        inherit IterationStatement
        abstract kind: SyntaxKind with get, set
        abstract awaitModifier: AwaitKeywordToken option with get, set
        abstract initializer: ForInitializer with get, set
        abstract expression: Expression with get, set

    type [<AllowNullLiteral>] BreakStatement =
        inherit Statement
        abstract kind: SyntaxKind with get, set
        abstract label: Identifier option with get, set

    type [<AllowNullLiteral>] ContinueStatement =
        inherit Statement
        abstract kind: SyntaxKind with get, set
        abstract label: Identifier option with get, set

    type BreakOrContinueStatement =
        U2<BreakStatement, ContinueStatement>

    type [<AllowNullLiteral>] ReturnStatement =
        inherit Statement
        abstract kind: SyntaxKind with get, set
        abstract expression: Expression option with get, set

    type [<AllowNullLiteral>] WithStatement =
        inherit Statement
        abstract kind: SyntaxKind with get, set
        abstract expression: Expression with get, set
        abstract statement: Statement with get, set

    type [<AllowNullLiteral>] SwitchStatement =
        inherit Statement
        abstract kind: SyntaxKind with get, set
        abstract expression: Expression with get, set
        abstract caseBlock: CaseBlock with get, set
        abstract possiblyExhaustive: bool option with get, set

    type [<AllowNullLiteral>] CaseBlock =
        inherit Node
        abstract kind: SyntaxKind with get, set
        abstract parent: SwitchStatement option with get, set
        abstract clauses: ResizeArray<CaseOrDefaultClause> with get, set

    type [<AllowNullLiteral>] CaseClause =
        inherit Node
        abstract kind: SyntaxKind with get, set
        abstract parent: CaseBlock option with get, set
        abstract expression: Expression with get, set
        abstract statements: ResizeArray<Statement> with get, set

    type [<AllowNullLiteral>] DefaultClause =
        inherit Node
        abstract kind: SyntaxKind with get, set
        abstract parent: CaseBlock option with get, set
        abstract statements: ResizeArray<Statement> with get, set

    type CaseOrDefaultClause =
        U2<CaseClause, DefaultClause>

    type [<AllowNullLiteral>] LabeledStatement =
        inherit Statement
        abstract kind: SyntaxKind with get, set
        abstract label: Identifier with get, set
        abstract statement: Statement with get, set

    type [<AllowNullLiteral>] ThrowStatement =
        inherit Statement
        abstract kind: SyntaxKind with get, set
        abstract expression: Expression with get, set

    type [<AllowNullLiteral>] TryStatement =
        inherit Statement
        abstract kind: SyntaxKind with get, set
        abstract tryBlock: Block with get, set
        abstract catchClause: CatchClause option with get, set
        abstract finallyBlock: Block option with get, set

    type [<AllowNullLiteral>] CatchClause =
        inherit Node
        abstract kind: SyntaxKind with get, set
        abstract parent: TryStatement option with get, set
        abstract variableDeclaration: VariableDeclaration option with get, set
        abstract block: Block with get, set

    type DeclarationWithTypeParameters =
        U5<SignatureDeclaration, ClassLikeDeclaration, InterfaceDeclaration, TypeAliasDeclaration, JSDocTemplateTag>

    type [<AllowNullLiteral>] ClassLikeDeclaration =
        inherit NamedDeclaration
        abstract name: Identifier option with get, set
        abstract typeParameters: ResizeArray<TypeParameterDeclaration> option with get, set
        abstract heritageClauses: ResizeArray<HeritageClause> option with get, set
        abstract members: ResizeArray<ClassElement> with get, set

    type [<AllowNullLiteral>] ClassDeclaration =
        inherit ClassLikeDeclaration
        inherit DeclarationStatement
        abstract kind: SyntaxKind with get, set
        abstract name: Identifier option with get, set

    type [<AllowNullLiteral>] ClassExpression =
        inherit ClassLikeDeclaration
        inherit PrimaryExpression
        abstract kind: SyntaxKind with get, set

    type [<AllowNullLiteral>] ClassElement =
        inherit NamedDeclaration
        abstract _classElementBrand: obj with get, set
        abstract name: PropertyName option with get, set

    type [<AllowNullLiteral>] TypeElement =
        inherit NamedDeclaration
        abstract _typeElementBrand: obj with get, set
        abstract name: PropertyName option with get, set
        abstract questionToken: QuestionToken option with get, set

    type [<AllowNullLiteral>] InterfaceDeclaration =
        inherit DeclarationStatement
        abstract kind: SyntaxKind with get, set
        abstract name: Identifier with get, set
        abstract typeParameters: ResizeArray<TypeParameterDeclaration> option with get, set
        abstract heritageClauses: ResizeArray<HeritageClause> option with get, set
        abstract members: ResizeArray<TypeElement> with get, set

    type [<AllowNullLiteral>] HeritageClause =
        inherit Node
        abstract kind: SyntaxKind with get, set
        abstract parent: U3<InterfaceDeclaration, ClassDeclaration, ClassExpression> option with get, set
        abstract token: SyntaxKind with get, set
        abstract types: ResizeArray<ExpressionWithTypeArguments> with get, set

    type [<AllowNullLiteral>] TypeAliasDeclaration =
        inherit DeclarationStatement
        abstract kind: SyntaxKind with get, set
        abstract name: Identifier with get, set
        abstract typeParameters: ResizeArray<TypeParameterDeclaration> option with get, set
        abstract ``type``: TypeNode with get, set

    type [<AllowNullLiteral>] EnumMember =
        inherit NamedDeclaration
        abstract kind: SyntaxKind with get, set
        abstract parent: EnumDeclaration option with get, set
        abstract name: PropertyName with get, set
        abstract initializer: Expression option with get, set

    type [<AllowNullLiteral>] EnumDeclaration =
        inherit DeclarationStatement
        abstract kind: SyntaxKind with get, set
        abstract name: Identifier with get, set
        abstract members: ResizeArray<EnumMember> with get, set

    type ModuleName =
        U2<Identifier, StringLiteral>

    type ModuleBody =
        U2<NamespaceBody, JSDocNamespaceBody>

    type [<AllowNullLiteral>] ModuleDeclaration =
        inherit DeclarationStatement
        abstract kind: SyntaxKind with get, set
        abstract parent: U2<ModuleBody, SourceFile> option with get, set
        abstract name: ModuleName with get, set
        abstract body: U2<ModuleBody, JSDocNamespaceDeclaration> option with get, set

    type NamespaceBody =
        U2<ModuleBlock, NamespaceDeclaration>

    type [<AllowNullLiteral>] NamespaceDeclaration =
        inherit ModuleDeclaration
        abstract name: Identifier with get, set
        abstract body: NamespaceBody with get, set

    type JSDocNamespaceBody =
        U2<Identifier, JSDocNamespaceDeclaration>

    type [<AllowNullLiteral>] JSDocNamespaceDeclaration =
        inherit ModuleDeclaration
        abstract name: Identifier with get, set
        abstract body: JSDocNamespaceBody with get, set

    type [<AllowNullLiteral>] ModuleBlock =
        inherit Node
        inherit Statement
        abstract kind: SyntaxKind with get, set
        abstract parent: ModuleDeclaration option with get, set
        abstract statements: ResizeArray<Statement> with get, set

    type ModuleReference =
        U2<EntityName, ExternalModuleReference>

    /// One of:
    /// - import x = require("mod");
    /// - import x = M.x;
    type [<AllowNullLiteral>] ImportEqualsDeclaration =
        inherit DeclarationStatement
        abstract kind: SyntaxKind with get, set
        abstract parent: U2<SourceFile, ModuleBlock> option with get, set
        abstract name: Identifier with get, set
        abstract moduleReference: ModuleReference with get, set

    type [<AllowNullLiteral>] ExternalModuleReference =
        inherit Node
        abstract kind: SyntaxKind with get, set
        abstract parent: ImportEqualsDeclaration option with get, set
        abstract expression: Expression option with get, set

    type [<AllowNullLiteral>] ImportDeclaration =
        inherit Statement
        abstract kind: SyntaxKind with get, set
        abstract parent: U2<SourceFile, ModuleBlock> option with get, set
        abstract importClause: ImportClause option with get, set
        /// If this is not a StringLiteral it will be a grammar error. 
        abstract moduleSpecifier: Expression with get, set

    type NamedImportBindings =
        U2<NamespaceImport, NamedImports>

    type [<AllowNullLiteral>] ImportClause =
        inherit NamedDeclaration
        abstract kind: SyntaxKind with get, set
        abstract parent: ImportDeclaration option with get, set
        abstract name: Identifier option with get, set
        abstract namedBindings: NamedImportBindings option with get, set

    type [<AllowNullLiteral>] NamespaceImport =
        inherit NamedDeclaration
        abstract kind: SyntaxKind with get, set
        abstract parent: ImportClause option with get, set
        abstract name: Identifier with get, set

    type [<AllowNullLiteral>] NamespaceExportDeclaration =
        inherit DeclarationStatement
        abstract kind: SyntaxKind with get, set
        abstract name: Identifier with get, set

    type [<AllowNullLiteral>] ExportDeclaration =
        inherit DeclarationStatement
        abstract kind: SyntaxKind with get, set
        abstract parent: U2<SourceFile, ModuleBlock> option with get, set
        abstract exportClause: NamedExports option with get, set
        /// If this is not a StringLiteral it will be a grammar error. 
        abstract moduleSpecifier: Expression option with get, set

    type [<AllowNullLiteral>] NamedImports =
        inherit Node
        abstract kind: SyntaxKind with get, set
        abstract parent: ImportClause option with get, set
        abstract elements: ResizeArray<ImportSpecifier> with get, set

    type [<AllowNullLiteral>] NamedExports =
        inherit Node
        abstract kind: SyntaxKind with get, set
        abstract parent: ExportDeclaration option with get, set
        abstract elements: ResizeArray<ExportSpecifier> with get, set

    type NamedImportsOrExports =
        U2<NamedImports, NamedExports>

    type [<AllowNullLiteral>] ImportSpecifier =
        inherit NamedDeclaration
        abstract kind: SyntaxKind with get, set
        abstract parent: NamedImports option with get, set
        abstract propertyName: Identifier option with get, set
        abstract name: Identifier with get, set

    type [<AllowNullLiteral>] ExportSpecifier =
        inherit NamedDeclaration
        abstract kind: SyntaxKind with get, set
        abstract parent: NamedExports option with get, set
        abstract propertyName: Identifier option with get, set
        abstract name: Identifier with get, set

    type ImportOrExportSpecifier =
        U2<ImportSpecifier, ExportSpecifier>

    type [<AllowNullLiteral>] ExportAssignment =
        inherit DeclarationStatement
        abstract kind: SyntaxKind with get, set
        abstract parent: SourceFile option with get, set
        abstract isExportEquals: bool option with get, set
        abstract expression: Expression with get, set

    type [<AllowNullLiteral>] FileReference =
        inherit TextRange
        abstract fileName: string with get, set

    type [<AllowNullLiteral>] CheckJsDirective =
        inherit TextRange
        abstract enabled: bool with get, set

    type CommentKind =
        SyntaxKind

    type [<AllowNullLiteral>] CommentRange =
        inherit TextRange
        abstract hasTrailingNewLine: bool option with get, set
        abstract kind: CommentKind with get, set

    type [<AllowNullLiteral>] SynthesizedComment =
        inherit CommentRange
        abstract text: string with get, set
        abstract pos: obj with get, set
        abstract ``end``: obj with get, set

    type [<AllowNullLiteral>] JSDocTypeExpression =
        inherit TypeNode
        abstract kind: SyntaxKind with get, set
        abstract ``type``: TypeNode with get, set

    type [<AllowNullLiteral>] JSDocType =
        inherit TypeNode
        abstract _jsDocTypeBrand: obj with get, set

    type [<AllowNullLiteral>] JSDocAllType =
        inherit JSDocType
        abstract kind: SyntaxKind with get, set

    type [<AllowNullLiteral>] JSDocUnknownType =
        inherit JSDocType
        abstract kind: SyntaxKind with get, set

    type [<AllowNullLiteral>] JSDocNonNullableType =
        inherit JSDocType
        abstract kind: SyntaxKind with get, set
        abstract ``type``: TypeNode with get, set

    type [<AllowNullLiteral>] JSDocNullableType =
        inherit JSDocType
        abstract kind: SyntaxKind with get, set
        abstract ``type``: TypeNode with get, set

    type [<AllowNullLiteral>] JSDocOptionalType =
        inherit JSDocType
        abstract kind: SyntaxKind with get, set
        abstract ``type``: TypeNode with get, set

    type [<AllowNullLiteral>] JSDocFunctionType =
        inherit JSDocType
        inherit SignatureDeclaration
        abstract kind: SyntaxKind with get, set

    type [<AllowNullLiteral>] JSDocVariadicType =
        inherit JSDocType
        abstract kind: SyntaxKind with get, set
        abstract ``type``: TypeNode with get, set

    type JSDocTypeReferencingNode =
        U4<JSDocVariadicType, JSDocOptionalType, JSDocNullableType, JSDocNonNullableType>

    type [<AllowNullLiteral>] JSDoc =
        inherit Node
        abstract kind: SyntaxKind with get, set
        abstract tags: ResizeArray<JSDocTag> option with get, set
        abstract comment: string option with get, set

    type [<AllowNullLiteral>] JSDocTag =
        inherit Node
        abstract parent: JSDoc with get, set
        abstract atToken: AtToken with get, set
        abstract tagName: Identifier with get, set
        abstract comment: string option with get, set

    type [<AllowNullLiteral>] JSDocUnknownTag =
        inherit JSDocTag
        abstract kind: SyntaxKind with get, set

    type [<AllowNullLiteral>] JSDocAugmentsTag =
        inherit JSDocTag
        abstract kind: SyntaxKind with get, set
        abstract typeExpression: JSDocTypeExpression with get, set

    type [<AllowNullLiteral>] JSDocClassTag =
        inherit JSDocTag
        abstract kind: SyntaxKind with get, set

    type [<AllowNullLiteral>] JSDocTemplateTag =
        inherit JSDocTag
        abstract kind: SyntaxKind with get, set
        abstract typeParameters: ResizeArray<TypeParameterDeclaration> with get, set

    type [<AllowNullLiteral>] JSDocReturnTag =
        inherit JSDocTag
        abstract kind: SyntaxKind with get, set
        abstract typeExpression: JSDocTypeExpression with get, set

    type [<AllowNullLiteral>] JSDocTypeTag =
        inherit JSDocTag
        abstract kind: SyntaxKind with get, set
        abstract typeExpression: JSDocTypeExpression with get, set

    type [<AllowNullLiteral>] JSDocTypedefTag =
        inherit JSDocTag
        inherit NamedDeclaration
        abstract parent: JSDoc with get, set
        abstract kind: SyntaxKind with get, set
        abstract fullName: U2<JSDocNamespaceDeclaration, Identifier> option with get, set
        abstract name: Identifier option with get, set
        abstract typeExpression: U2<JSDocTypeExpression, JSDocTypeLiteral> option with get, set

    type [<AllowNullLiteral>] JSDocPropertyLikeTag =
        inherit JSDocTag
        inherit Declaration
        abstract parent: JSDoc with get, set
        abstract name: EntityName with get, set
        abstract typeExpression: JSDocTypeExpression with get, set
        /// Whether the property name came before the type -- non-standard for JSDoc, but Typescript-like 
        abstract isNameFirst: bool with get, set
        abstract isBracketed: bool with get, set

    type [<AllowNullLiteral>] JSDocPropertyTag =
        inherit JSDocPropertyLikeTag
        abstract kind: SyntaxKind with get, set

    type [<AllowNullLiteral>] JSDocParameterTag =
        inherit JSDocPropertyLikeTag
        abstract kind: SyntaxKind with get, set

    type [<AllowNullLiteral>] JSDocTypeLiteral =
        inherit JSDocType
        abstract kind: SyntaxKind with get, set
        abstract jsDocPropertyTags: ReadonlyArray<JSDocPropertyLikeTag> option with get, set
        /// If true, then this type literal represents an *array* of its type. 
        abstract isArrayType: bool option with get, set

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
        | Referenced = 512
        | Shared = 1024
        | PreFinally = 2048
        | AfterFinally = 4096
        | Label = 12
        | Condition = 96

    type [<AllowNullLiteral>] FlowLock =
        abstract locked: bool option with get, set

    type [<AllowNullLiteral>] AfterFinallyFlow =
        inherit FlowNodeBase
        inherit FlowLock
        abstract antecedent: FlowNode with get, set

    type [<AllowNullLiteral>] PreFinallyFlow =
        inherit FlowNodeBase
        abstract antecedent: FlowNode with get, set
        abstract lock: FlowLock with get, set

    type FlowNode =
        obj

    type [<AllowNullLiteral>] FlowNodeBase =
        abstract flags: FlowFlags with get, set
        abstract id: float option with get, set

    type [<AllowNullLiteral>] FlowStart =
        inherit FlowNodeBase
        abstract container: U3<FunctionExpression, ArrowFunction, MethodDeclaration> option with get, set

    type [<AllowNullLiteral>] FlowLabel =
        inherit FlowNodeBase
        abstract antecedents: ResizeArray<FlowNode> with get, set

    type [<AllowNullLiteral>] FlowAssignment =
        inherit FlowNodeBase
        abstract node: U3<Expression, VariableDeclaration, BindingElement> with get, set
        abstract antecedent: FlowNode with get, set

    type [<AllowNullLiteral>] FlowCondition =
        inherit FlowNodeBase
        abstract expression: Expression with get, set
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

    type FlowType =
        U2<Type, IncompleteType>

    type [<AllowNullLiteral>] IncompleteType =
        abstract flags: TypeFlags with get, set
        abstract ``type``: Type with get, set

    type [<AllowNullLiteral>] AmdDependency =
        abstract path: string with get, set
        abstract name: string with get, set

    type [<AllowNullLiteral>] SourceFile =
        inherit Declaration
        abstract kind: SyntaxKind with get, set
        abstract statements: ResizeArray<Statement> with get, set
        abstract endOfFileToken: Token<SyntaxKind> with get, set
        abstract fileName: string with get, set
        abstract text: string with get, set
        abstract amdDependencies: ResizeArray<AmdDependency> with get, set
        abstract moduleName: string with get, set
        abstract referencedFiles: ResizeArray<FileReference> with get, set
        abstract typeReferenceDirectives: ResizeArray<FileReference> with get, set
        abstract languageVariant: LanguageVariant with get, set
        abstract isDeclarationFile: bool with get, set
        /// lib.d.ts should have a reference comment like
        /// 
        ///   /// <reference no-default-lib="true"/>
        /// 
        /// If any other file has this comment, it signals not to include lib.d.ts
        /// because this containing file is intended to act as a default library.
        abstract hasNoDefaultLib: bool with get, set
        abstract languageVersion: ScriptTarget with get, set
        abstract getLineAndCharacterOfPosition: pos: float -> LineAndCharacter
        abstract getLineEndOfPosition: pos: float -> float
        abstract getLineStarts: unit -> ResizeArray<float>
        abstract getPositionOfLineAndCharacter: line: float * character: float -> float
        abstract update: newText: string * textChangeRange: TextChangeRange -> SourceFile

    type [<AllowNullLiteral>] Bundle =
        inherit Node
        abstract kind: SyntaxKind with get, set
        abstract sourceFiles: ResizeArray<SourceFile> with get, set

    type [<AllowNullLiteral>] JsonSourceFile =
        inherit SourceFile
        abstract jsonObject: ObjectLiteralExpression option with get, set
        abstract extendedSourceFiles: ResizeArray<string> option with get, set

    type [<AllowNullLiteral>] ScriptReferenceHost =
        abstract getCompilerOptions: unit -> CompilerOptions
        abstract getSourceFile: fileName: string -> SourceFile
        abstract getSourceFileByPath: path: Path -> SourceFile
        abstract getCurrentDirectory: unit -> string

    type [<AllowNullLiteral>] ParseConfigHost =
        abstract useCaseSensitiveFileNames: bool with get, set
        abstract readDirectory: rootDir: string * extensions: ReadonlyArray<string> * excludes: ReadonlyArray<string> * includes: ReadonlyArray<string> * depth: float -> ResizeArray<string>
        /// Gets a value indicating whether the specified path exists and is a file.
        abstract fileExists: path: string -> bool
        abstract readFile: path: string -> string option

    type [<AllowNullLiteral>] WriteFileCallback =
        [<Emit "$0($1...)">] abstract Invoke: fileName: string * data: string * writeByteOrderMark: bool * ?onError: (string -> unit) * ?sourceFiles: ReadonlyArray<SourceFile> -> unit

    type [<AllowNullLiteral>] OperationCanceledException =
        interface end

    type [<AllowNullLiteral>] CancellationToken =
        abstract isCancellationRequested: unit -> bool
        abstract throwIfCancellationRequested: unit -> unit

    type [<AllowNullLiteral>] Program =
        inherit ScriptReferenceHost
        /// Get a list of root file names that were passed to a 'createProgram'
        abstract getRootFileNames: unit -> ResizeArray<string>
        /// Get a list of files in the program
        abstract getSourceFiles: unit -> ResizeArray<SourceFile>
        /// Emits the JavaScript and declaration files.  If targetSourceFile is not specified, then
        /// the JavaScript and declaration files will be produced for all the files in this program.
        /// If targetSourceFile is specified, then only the JavaScript and declaration for that
        /// specific file will be generated.
        /// 
        /// If writeFile is not specified then the writeFile callback from the compiler host will be
        /// used for writing the JavaScript and declaration files.  Otherwise, the writeFile parameter
        /// will be invoked when writing the JavaScript and declaration files.
        abstract emit: ?targetSourceFile: SourceFile * ?writeFile: WriteFileCallback * ?cancellationToken: CancellationToken * ?emitOnlyDtsFiles: bool * ?customTransformers: CustomTransformers -> EmitResult
        abstract getOptionsDiagnostics: ?cancellationToken: CancellationToken -> ResizeArray<Diagnostic>
        abstract getGlobalDiagnostics: ?cancellationToken: CancellationToken -> ResizeArray<Diagnostic>
        abstract getSyntacticDiagnostics: ?sourceFile: SourceFile * ?cancellationToken: CancellationToken -> ResizeArray<Diagnostic>
        abstract getSemanticDiagnostics: ?sourceFile: SourceFile * ?cancellationToken: CancellationToken -> ResizeArray<Diagnostic>
        abstract getDeclarationDiagnostics: ?sourceFile: SourceFile * ?cancellationToken: CancellationToken -> ResizeArray<Diagnostic>
        /// Gets a type checker that can be used to semantically analyze source fils in the program.
        abstract getTypeChecker: unit -> TypeChecker

    type [<AllowNullLiteral>] CustomTransformers =
        /// Custom transformers to evaluate before built-in transformations. 
        abstract before: ResizeArray<TransformerFactory<SourceFile>> option with get, set
        /// Custom transformers to evaluate after built-in transformations. 
        abstract after: ResizeArray<TransformerFactory<SourceFile>> option with get, set

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

    type [<AllowNullLiteral>] SourceMapData =
        abstract sourceMapFilePath: string with get, set
        abstract jsSourceMappingURL: string with get, set
        abstract sourceMapFile: string with get, set
        abstract sourceMapSourceRoot: string with get, set
        abstract sourceMapSources: ResizeArray<string> with get, set
        abstract sourceMapSourcesContent: ResizeArray<string> option with get, set
        abstract inputSourceFileNames: ResizeArray<string> with get, set
        abstract sourceMapNames: ResizeArray<string> option with get, set
        abstract sourceMapMappings: string with get, set
        abstract sourceMapDecodedMappings: ResizeArray<SourceMapSpan> with get, set

    type [<RequireQualifiedAccess>] ExitStatus =
        | Success = 0
        | DiagnosticsPresent_OutputsSkipped = 1
        | DiagnosticsPresent_OutputsGenerated = 2

    type [<AllowNullLiteral>] EmitResult =
        abstract emitSkipped: bool with get, set
        /// Contains declaration emit diagnostics 
        abstract diagnostics: ResizeArray<Diagnostic> with get, set
        abstract emittedFiles: ResizeArray<string> with get, set

    type [<AllowNullLiteral>] TypeChecker =
        abstract getTypeOfSymbolAtLocation: symbol: Symbol * node: Node -> Type
        abstract getDeclaredTypeOfSymbol: symbol: Symbol -> Type
        abstract getPropertiesOfType: ``type``: Type -> ResizeArray<Symbol>
        abstract getPropertyOfType: ``type``: Type * propertyName: string -> Symbol option
        abstract getIndexInfoOfType: ``type``: Type * kind: IndexKind -> IndexInfo option
        abstract getSignaturesOfType: ``type``: Type * kind: SignatureKind -> ResizeArray<Signature>
        abstract getIndexTypeOfType: ``type``: Type * kind: IndexKind -> Type option
        abstract getBaseTypes: ``type``: InterfaceType -> ResizeArray<BaseType>
        abstract getBaseTypeOfLiteralType: ``type``: Type -> Type
        abstract getWidenedType: ``type``: Type -> Type
        abstract getReturnTypeOfSignature: signature: Signature -> Type
        abstract getNullableType: ``type``: Type * flags: TypeFlags -> Type
        abstract getNonNullableType: ``type``: Type -> Type
        /// Note that the resulting nodes cannot be checked. 
        abstract typeToTypeNode: ``type``: Type * ?enclosingDeclaration: Node * ?flags: NodeBuilderFlags -> TypeNode
        /// Note that the resulting nodes cannot be checked. 
        abstract signatureToSignatureDeclaration: signature: Signature * kind: SyntaxKind * ?enclosingDeclaration: Node * ?flags: NodeBuilderFlags -> SignatureDeclaration
        /// Note that the resulting nodes cannot be checked. 
        abstract indexInfoToIndexSignatureDeclaration: indexInfo: IndexInfo * kind: IndexKind * ?enclosingDeclaration: Node * ?flags: NodeBuilderFlags -> IndexSignatureDeclaration
        abstract getSymbolsInScope: location: Node * meaning: SymbolFlags -> ResizeArray<Symbol>
        abstract getSymbolAtLocation: node: Node -> Symbol option
        abstract getSymbolsOfParameterPropertyDeclaration: parameter: ParameterDeclaration * parameterName: string -> ResizeArray<Symbol>
        abstract getShorthandAssignmentValueSymbol: location: Node -> Symbol option
        abstract getExportSpecifierLocalTargetSymbol: location: ExportSpecifier -> Symbol option
        abstract getPropertySymbolOfDestructuringAssignment: location: Identifier -> Symbol option
        abstract getTypeAtLocation: node: Node -> Type
        abstract getTypeFromTypeNode: node: TypeNode -> Type
        abstract signatureToString: signature: Signature * ?enclosingDeclaration: Node * ?flags: TypeFormatFlags * ?kind: SignatureKind -> string
        abstract typeToString: ``type``: Type * ?enclosingDeclaration: Node * ?flags: TypeFormatFlags -> string
        abstract symbolToString: symbol: Symbol * ?enclosingDeclaration: Node * ?meaning: SymbolFlags -> string
        abstract getSymbolDisplayBuilder: unit -> SymbolDisplayBuilder
        abstract getFullyQualifiedName: symbol: Symbol -> string
        abstract getAugmentedPropertiesOfType: ``type``: Type -> ResizeArray<Symbol>
        abstract getRootSymbols: symbol: Symbol -> ResizeArray<Symbol>
        abstract getContextualType: node: Expression -> Type option
        /// returns unknownSignature in the case of an error. Don't know when it returns undefined.
        abstract getResolvedSignature: node: CallLikeExpression * ?candidatesOutArray: ResizeArray<Signature> * ?argumentCount: float -> Signature option
        abstract getSignatureFromDeclaration: declaration: SignatureDeclaration -> Signature option
        abstract isImplementationOfOverload: node: FunctionLike -> bool option
        abstract isUndefinedSymbol: symbol: Symbol -> bool
        abstract isArgumentsSymbol: symbol: Symbol -> bool
        abstract isUnknownSymbol: symbol: Symbol -> bool
        abstract getConstantValue: node: U3<EnumMember, PropertyAccessExpression, ElementAccessExpression> -> U2<string, float> option
        abstract isValidPropertyAccess: node: U2<PropertyAccessExpression, QualifiedName> * propertyName: string -> bool
        /// Follow all aliases to get the original symbol. 
        abstract getAliasedSymbol: symbol: Symbol -> Symbol
        abstract getExportsOfModule: moduleSymbol: Symbol -> ResizeArray<Symbol>
        abstract getAllAttributesTypeFromJsxOpeningLikeElement: elementNode: JsxOpeningLikeElement -> Type option
        abstract getJsxIntrinsicTagNames: unit -> ResizeArray<Symbol>
        abstract isOptionalParameter: node: ParameterDeclaration -> bool
        abstract getAmbientModules: unit -> ResizeArray<Symbol>
        abstract tryGetMemberInModuleExports: memberName: string * moduleSymbol: Symbol -> Symbol option
        abstract getApparentType: ``type``: Type -> Type
        abstract getSuggestionForNonexistentProperty: node: Identifier * containingType: Type -> string option
        abstract getSuggestionForNonexistentSymbol: location: Node * name: string * meaning: SymbolFlags -> string option

    type [<RequireQualifiedAccess>] NodeBuilderFlags =
        | None = 0
        | NoTruncation = 1
        | WriteArrayAsGenericType = 2
        | WriteTypeArgumentsOfSignature = 32
        | UseFullyQualifiedType = 64
        | SuppressAnyReturnType = 256
        | WriteTypeParametersInQualifiedName = 512
        | AllowThisInObjectLiteral = 1024
        | AllowQualifedNameInPlaceOfIdentifier = 2048
        | AllowAnonymousIdentifier = 8192
        | AllowEmptyUnionOrIntersection = 16384
        | AllowEmptyTuple = 32768
        | IgnoreErrors = 60416
        | InObjectTypeLiteral = 1048576
        | InTypeAlias = 8388608

    type [<AllowNullLiteral>] SymbolDisplayBuilder =
        abstract buildTypeDisplay: ``type``: Type * writer: SymbolWriter * ?enclosingDeclaration: Node * ?flags: TypeFormatFlags -> unit
        abstract buildSymbolDisplay: symbol: Symbol * writer: SymbolWriter * ?enclosingDeclaration: Node * ?meaning: SymbolFlags * ?flags: SymbolFormatFlags -> unit
        abstract buildSignatureDisplay: signature: Signature * writer: SymbolWriter * ?enclosingDeclaration: Node * ?flags: TypeFormatFlags * ?kind: SignatureKind -> unit
        abstract buildIndexSignatureDisplay: info: IndexInfo * writer: SymbolWriter * kind: IndexKind * ?enclosingDeclaration: Node * ?globalFlags: TypeFormatFlags * ?symbolStack: ResizeArray<Symbol> -> unit
        abstract buildParameterDisplay: parameter: Symbol * writer: SymbolWriter * ?enclosingDeclaration: Node * ?flags: TypeFormatFlags -> unit
        abstract buildTypeParameterDisplay: tp: TypeParameter * writer: SymbolWriter * ?enclosingDeclaration: Node * ?flags: TypeFormatFlags -> unit
        abstract buildTypePredicateDisplay: predicate: TypePredicate * writer: SymbolWriter * ?enclosingDeclaration: Node * ?flags: TypeFormatFlags -> unit
        abstract buildTypeParameterDisplayFromSymbol: symbol: Symbol * writer: SymbolWriter * ?enclosingDeclaration: Node * ?flags: TypeFormatFlags -> unit
        abstract buildDisplayForParametersAndDelimiters: thisParameter: Symbol * parameters: ResizeArray<Symbol> * writer: SymbolWriter * ?enclosingDeclaration: Node * ?flags: TypeFormatFlags -> unit
        abstract buildDisplayForTypeParametersAndDelimiters: typeParameters: ResizeArray<TypeParameter> * writer: SymbolWriter * ?enclosingDeclaration: Node * ?flags: TypeFormatFlags -> unit
        abstract buildReturnTypeDisplay: signature: Signature * writer: SymbolWriter * ?enclosingDeclaration: Node * ?flags: TypeFormatFlags -> unit

    type [<AllowNullLiteral>] SymbolWriter =
        abstract writeKeyword: text: string -> unit
        abstract writeOperator: text: string -> unit
        abstract writePunctuation: text: string -> unit
        abstract writeSpace: text: string -> unit
        abstract writeStringLiteral: text: string -> unit
        abstract writeParameter: text: string -> unit
        abstract writeProperty: text: string -> unit
        abstract writeSymbol: text: string * symbol: Symbol -> unit
        abstract writeLine: unit -> unit
        abstract increaseIndent: unit -> unit
        abstract decreaseIndent: unit -> unit
        abstract clear: unit -> unit
        abstract trackSymbol: symbol: Symbol * ?enclosingDeclaration: Node * ?meaning: SymbolFlags -> unit
        abstract reportInaccessibleThisError: unit -> unit
        abstract reportPrivateInBaseOfClassExpression: propertyName: string -> unit

    type [<RequireQualifiedAccess>] TypeFormatFlags =
        | None = 0
        | WriteArrayAsGenericType = 1
        | UseTypeOfFunction = 4
        | NoTruncation = 8
        | WriteArrowStyleSignature = 16
        | WriteOwnNameForAnyLike = 32
        | WriteTypeArgumentsOfSignature = 64
        | InElementType = 128
        | UseFullyQualifiedType = 256
        | InFirstTypeArgument = 512
        | InTypeAlias = 1024
        | UseTypeAliasValue = 2048
        | SuppressAnyReturnType = 4096
        | AddUndefined = 8192
        | WriteClassExpressionAsTypeLiteral = 16384
        | InArrayType = 32768
        | UseAliasDefinedOutsideCurrentScope = 65536

    type [<RequireQualifiedAccess>] SymbolFormatFlags =
        | None = 0
        | WriteTypeParametersOrArguments = 1
        | UseOnlyExternalAliasing = 2

    type [<RequireQualifiedAccess>] TypePredicateKind =
        | This = 0
        | Identifier = 1

    type [<AllowNullLiteral>] TypePredicateBase =
        abstract kind: TypePredicateKind with get, set
        abstract ``type``: Type with get, set

    type [<AllowNullLiteral>] ThisTypePredicate =
        inherit TypePredicateBase
        abstract kind: TypePredicateKind with get, set

    type [<AllowNullLiteral>] IdentifierTypePredicate =
        inherit TypePredicateBase
        abstract kind: TypePredicateKind with get, set
        abstract parameterName: string with get, set
        abstract parameterIndex: float with get, set

    type TypePredicate =
        U2<IdentifierTypePredicate, ThisTypePredicate>

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
        | Enum = 384
        | Variable = 3
        | Value = 107455
        | Type = 793064
        | Namespace = 1920
        | Module = 1536
        | Accessor = 98304
        | FunctionScopedVariableExcludes = 107454
        | BlockScopedVariableExcludes = 107455
        | ParameterExcludes = 107455
        | PropertyExcludes = 0
        | EnumMemberExcludes = 900095
        | FunctionExcludes = 106927
        | ClassExcludes = 899519
        | InterfaceExcludes = 792968
        | RegularEnumExcludes = 899327
        | ConstEnumExcludes = 899967
        | ValueModuleExcludes = 106639
        | NamespaceModuleExcludes = 0
        | MethodExcludes = 99263
        | GetAccessorExcludes = 41919
        | SetAccessorExcludes = 74687
        | TypeParameterExcludes = 530920
        | TypeAliasExcludes = 793064
        | AliasExcludes = 2097152
        | ModuleMember = 2623475
        | ExportHasLocal = 944
        | HasExports = 1952
        | HasMembers = 6240
        | BlockScoped = 418
        | PropertyOrAccessor = 98308
        | ClassMember = 106500

    type [<AllowNullLiteral>] Symbol =
        abstract flags: SymbolFlags with get, set
        abstract escapedName: __String with get, set
        abstract declarations: ResizeArray<Declaration> option with get, set
        abstract valueDeclaration: Declaration option with get, set
        abstract members: SymbolTable option with get, set
        abstract exports: SymbolTable option with get, set
        abstract globalExports: SymbolTable option with get, set
        abstract name: string with get, set
        abstract getFlags: unit -> SymbolFlags
        abstract getEscapedName: unit -> __String
        abstract getName: unit -> string
        abstract getDeclarations: unit -> ResizeArray<Declaration> option
        abstract getDocumentationComment: unit -> ResizeArray<SymbolDisplayPart>
        abstract getJsDocTags: unit -> ResizeArray<JSDocTagInfo>

    type [<StringEnum>] [<RequireQualifiedAccess>] InternalSymbolName =
        | Call
        | Constructor
        | New
        | Index
        | ExportStar
        | Global
        | Missing
        | Type
        | Object
        | JSXAttributes
        | Class
        | Function
        | Computed
        | Resolving
        | ExportEquals
        | Default

    type __String =
        U2<obj, InternalSymbolName>

    /// ReadonlyMap where keys are `__String`s. 
    type [<AllowNullLiteral>] ReadonlyUnderscoreEscapedMap<'T> =
        abstract get: key: __String -> 'T option
        abstract has: key: __String -> bool
        abstract forEach: action: ('T -> __String -> unit) -> unit
        abstract size: float with get, set
        abstract keys: unit -> Iterator<__String>
        abstract values: unit -> Iterator<'T>
        abstract entries: unit -> Iterator<__String * 'T>

    /// Map where keys are `__String`s. 
    type [<AllowNullLiteral>] UnderscoreEscapedMap<'T> =
        inherit ReadonlyUnderscoreEscapedMap<'T>
        abstract set: key: __String * value: 'T -> UnderscoreEscapedMap<'T>
        abstract delete: key: __String -> bool
        abstract clear: unit -> unit

    type SymbolTable =
        UnderscoreEscapedMap<Symbol>

    type [<RequireQualifiedAccess>] TypeFlags =
        | Any = 1
        | String = 2
        | Number = 4
        | Boolean = 8
        | Enum = 16
        | StringLiteral = 32
        | NumberLiteral = 64
        | BooleanLiteral = 128
        | EnumLiteral = 256
        | ESSymbol = 512
        | Void = 1024
        | Undefined = 2048
        | Null = 4096
        | Never = 8192
        | TypeParameter = 16384
        | Object = 32768
        | Union = 65536
        | Intersection = 131072
        | Index = 262144
        | IndexedAccess = 524288
        | NonPrimitive = 16777216
        | Literal = 224
        | StringOrNumberLiteral = 96
        | PossiblyFalsy = 7406
        | StringLike = 262178
        | NumberLike = 84
        | BooleanLike = 136
        | EnumLike = 272
        | UnionOrIntersection = 196608
        | StructuredType = 229376
        | StructuredOrTypeVariable = 1032192
        | TypeVariable = 540672
        | Narrowable = 17810175
        | NotUnionOrUnit = 16810497

    type DestructuringPattern =
        U3<BindingPattern, ObjectLiteralExpression, ArrayLiteralExpression>

    type [<AllowNullLiteral>] Type =
        abstract flags: TypeFlags with get, set
        abstract symbol: Symbol option with get, set
        abstract pattern: DestructuringPattern option with get, set
        abstract aliasSymbol: Symbol option with get, set
        abstract aliasTypeArguments: ResizeArray<Type> option with get, set
        abstract getFlags: unit -> TypeFlags
        abstract getSymbol: unit -> Symbol option
        abstract getProperties: unit -> ResizeArray<Symbol>
        abstract getProperty: propertyName: string -> Symbol option
        abstract getApparentProperties: unit -> ResizeArray<Symbol>
        abstract getCallSignatures: unit -> ResizeArray<Signature>
        abstract getConstructSignatures: unit -> ResizeArray<Signature>
        abstract getStringIndexType: unit -> Type option
        abstract getNumberIndexType: unit -> Type option
        abstract getBaseTypes: unit -> ResizeArray<BaseType> option
        abstract getNonNullableType: unit -> Type

    type [<AllowNullLiteral>] LiteralType =
        inherit Type
        abstract value: U2<string, float> with get, set
        abstract freshType: LiteralType option with get, set
        abstract regularType: LiteralType option with get, set

    type [<AllowNullLiteral>] StringLiteralType =
        inherit LiteralType
        abstract value: string with get, set

    type [<AllowNullLiteral>] NumberLiteralType =
        inherit LiteralType
        abstract value: float with get, set

    type [<AllowNullLiteral>] EnumType =
        inherit Type

    type [<RequireQualifiedAccess>] ObjectFlags =
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
        | ClassOrInterface = 3

    type [<AllowNullLiteral>] ObjectType =
        inherit Type
        abstract objectFlags: ObjectFlags with get, set

    /// Class and interface types (ObjectFlags.Class and ObjectFlags.Interface). 
    type [<AllowNullLiteral>] InterfaceType =
        inherit ObjectType
        abstract typeParameters: ResizeArray<TypeParameter> with get, set
        abstract outerTypeParameters: ResizeArray<TypeParameter> with get, set
        abstract localTypeParameters: ResizeArray<TypeParameter> with get, set
        abstract thisType: TypeParameter with get, set

    type BaseType =
        U2<ObjectType, IntersectionType>

    type [<AllowNullLiteral>] InterfaceTypeWithDeclaredMembers =
        inherit InterfaceType
        abstract declaredProperties: ResizeArray<Symbol> with get, set
        abstract declaredCallSignatures: ResizeArray<Signature> with get, set
        abstract declaredConstructSignatures: ResizeArray<Signature> with get, set
        abstract declaredStringIndexInfo: IndexInfo with get, set
        abstract declaredNumberIndexInfo: IndexInfo with get, set

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
        abstract typeArguments: ResizeArray<Type> option with get, set

    type [<AllowNullLiteral>] GenericType =
        inherit InterfaceType
        inherit TypeReference

    type [<AllowNullLiteral>] UnionOrIntersectionType =
        inherit Type
        abstract types: ResizeArray<Type> with get, set

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

    type [<AllowNullLiteral>] TypeVariable =
        inherit Type

    type [<AllowNullLiteral>] TypeParameter =
        inherit TypeVariable
        abstract ``constraint``: Type with get, set
        abstract ``default``: Type option with get, set

    type [<AllowNullLiteral>] IndexedAccessType =
        inherit TypeVariable
        abstract objectType: Type with get, set
        abstract indexType: Type with get, set
        abstract ``constraint``: Type option with get, set

    type [<AllowNullLiteral>] IndexType =
        inherit Type
        abstract ``type``: U2<TypeVariable, UnionOrIntersectionType> with get, set

    type [<RequireQualifiedAccess>] SignatureKind =
        | Call = 0
        | Construct = 1

    type [<AllowNullLiteral>] Signature =
        abstract declaration: SignatureDeclaration with get, set
        abstract typeParameters: ResizeArray<TypeParameter> option with get, set
        abstract parameters: ResizeArray<Symbol> with get, set
        abstract getDeclaration: unit -> SignatureDeclaration
        abstract getTypeParameters: unit -> ResizeArray<TypeParameter> option
        abstract getParameters: unit -> ResizeArray<Symbol>
        abstract getReturnType: unit -> Type
        abstract getDocumentationComment: unit -> ResizeArray<SymbolDisplayPart>
        abstract getJsDocTags: unit -> ResizeArray<JSDocTagInfo>

    type [<RequireQualifiedAccess>] IndexKind =
        | String = 0
        | Number = 1

    type [<AllowNullLiteral>] IndexInfo =
        abstract ``type``: Type with get, set
        abstract isReadonly: bool with get, set
        abstract declaration: SignatureDeclaration option with get, set

    type [<RequireQualifiedAccess>] InferencePriority =
        | NakedTypeVariable = 1
        | MappedType = 2
        | ReturnType = 4

    type [<AllowNullLiteral>] InferenceInfo =
        abstract typeParameter: TypeParameter with get, set
        abstract candidates: ResizeArray<Type> with get, set
        abstract inferredType: Type with get, set
        abstract priority: InferencePriority with get, set
        abstract topLevel: bool with get, set
        abstract isFixed: bool with get, set

    type [<RequireQualifiedAccess>] InferenceFlags =
        | InferUnionTypes = 1
        | NoDefault = 2
        | AnyDefault = 4

    type Ternary =
        obj

    type TypeComparer =
        (Type -> Type -> bool -> Ternary)

    type [<AllowNullLiteral>] JsFileExtensionInfo =
        abstract extension: string with get, set
        abstract isMixedContent: bool with get, set

    type [<AllowNullLiteral>] DiagnosticMessage =
        abstract key: string with get, set
        abstract category: DiagnosticCategory with get, set
        abstract code: float with get, set
        abstract message: string with get, set

    /// A linked list of formatted diagnostic messages to be used as part of a multiline message.
    /// It is built from the bottom up, leaving the head to be the "main" diagnostic.
    /// While it seems that DiagnosticMessageChain is structurally similar to DiagnosticMessage,
    /// the difference is that messages are all preformatted in DMC.
    type [<AllowNullLiteral>] DiagnosticMessageChain =
        abstract messageText: string with get, set
        abstract category: DiagnosticCategory with get, set
        abstract code: float with get, set
        abstract next: DiagnosticMessageChain option with get, set

    type [<AllowNullLiteral>] Diagnostic =
        abstract file: SourceFile option with get, set
        abstract start: float option with get, set
        abstract length: float option with get, set
        abstract messageText: U2<string, DiagnosticMessageChain> with get, set
        abstract category: DiagnosticCategory with get, set
        abstract code: float with get, set
        abstract source: string option with get, set

    type [<RequireQualifiedAccess>] DiagnosticCategory =
        | Warning = 0
        | Error = 1
        | Message = 2

    type [<RequireQualifiedAccess>] ModuleResolutionKind =
        | Classic = 1
        | NodeJs = 2

    type [<AllowNullLiteral>] PluginImport =
        abstract name: string with get, set

    type CompilerOptionsValue =
        obj

    type [<AllowNullLiteral>] CompilerOptions =
        abstract allowJs: bool option with get, set
        abstract allowSyntheticDefaultImports: bool option with get, set
        abstract allowUnreachableCode: bool option with get, set
        abstract allowUnusedLabels: bool option with get, set
        abstract alwaysStrict: bool option with get, set
        abstract baseUrl: string option with get, set
        abstract charset: string option with get, set
        abstract checkJs: bool option with get, set
        abstract declaration: bool option with get, set
        abstract declarationDir: string option with get, set
        abstract disableSizeLimit: bool option with get, set
        abstract downlevelIteration: bool option with get, set
        abstract emitBOM: bool option with get, set
        abstract emitDecoratorMetadata: bool option with get, set
        abstract experimentalDecorators: bool option with get, set
        abstract forceConsistentCasingInFileNames: bool option with get, set
        abstract importHelpers: bool option with get, set
        abstract inlineSourceMap: bool option with get, set
        abstract inlineSources: bool option with get, set
        abstract isolatedModules: bool option with get, set
        abstract jsx: JsxEmit option with get, set
        abstract lib: ResizeArray<string> option with get, set
        abstract locale: string option with get, set
        abstract mapRoot: string option with get, set
        abstract maxNodeModuleJsDepth: float option with get, set
        abstract ``module``: ModuleKind option with get, set
        abstract moduleResolution: ModuleResolutionKind option with get, set
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
        abstract noLib: bool option with get, set
        abstract noResolve: bool option with get, set
        abstract out: string option with get, set
        abstract outDir: string option with get, set
        abstract outFile: string option with get, set
        abstract paths: MapLike<ResizeArray<string>> option with get, set
        abstract preserveConstEnums: bool option with get, set
        abstract preserveSymlinks: bool option with get, set
        abstract project: string option with get, set
        abstract reactNamespace: string option with get, set
        abstract jsxFactory: string option with get, set
        abstract removeComments: bool option with get, set
        abstract rootDir: string option with get, set
        abstract rootDirs: ResizeArray<string> option with get, set
        abstract skipLibCheck: bool option with get, set
        abstract skipDefaultLibCheck: bool option with get, set
        abstract sourceMap: bool option with get, set
        abstract sourceRoot: string option with get, set
        abstract strict: bool option with get, set
        abstract strictNullChecks: bool option with get, set
        abstract suppressExcessPropertyErrors: bool option with get, set
        abstract suppressImplicitAnyIndexErrors: bool option with get, set
        abstract target: ScriptTarget option with get, set
        abstract traceResolution: bool option with get, set
        abstract types: ResizeArray<string> option with get, set
        /// Paths used to compute primary types search locations 
        abstract typeRoots: ResizeArray<string> option with get, set
        [<Emit "$0[$1]{{=$2}}">] abstract Item: option: string -> U2<CompilerOptionsValue, JsonSourceFile> option with get, set

    type [<AllowNullLiteral>] TypeAcquisition =
        abstract enableAutoDiscovery: bool option with get, set
        abstract enable: bool option with get, set
        abstract ``include``: ResizeArray<string> option with get, set
        abstract exclude: ResizeArray<string> option with get, set
        [<Emit "$0[$1]{{=$2}}">] abstract Item: option: string -> U2<ResizeArray<string>, bool> option with get, set

    type [<AllowNullLiteral>] DiscoverTypingsInfo =
        abstract fileNames: ResizeArray<string> with get, set
        abstract projectRootPath: string with get, set
        abstract safeListPath: string with get, set
        abstract packageNameToTypingLocation: Map<string> with get, set
        abstract typeAcquisition: TypeAcquisition with get, set
        abstract compilerOptions: CompilerOptions with get, set
        abstract unresolvedImports: ReadonlyArray<string> with get, set

    type [<RequireQualifiedAccess>] ModuleKind =
        | None = 0
        | CommonJS = 1
        | AMD = 2
        | UMD = 3
        | System = 4
        | ES2015 = 5
        | ESNext = 6

    type [<RequireQualifiedAccess>] JsxEmit =
        | None = 0
        | Preserve = 1
        | React = 2
        | ReactNative = 3

    type [<RequireQualifiedAccess>] NewLineKind =
        | CarriageReturnLineFeed = 0
        | LineFeed = 1

    type [<AllowNullLiteral>] LineAndCharacter =
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

    type [<RequireQualifiedAccess>] ScriptTarget =
        | ES3 = 0
        | ES5 = 1
        | ES2015 = 2
        | ES2016 = 3
        | ES2017 = 4
        | ESNext = 5
        | Latest = 5

    type [<RequireQualifiedAccess>] LanguageVariant =
        | Standard = 0
        | JSX = 1

    /// Either a parsed command line or a parsed tsconfig.json 
    type [<AllowNullLiteral>] ParsedCommandLine =
        abstract options: CompilerOptions with get, set
        abstract typeAcquisition: TypeAcquisition option with get, set
        abstract fileNames: ResizeArray<string> with get, set
        abstract raw: obj option with get, set
        abstract errors: ResizeArray<Diagnostic> with get, set
        abstract wildcardDirectories: MapLike<WatchDirectoryFlags> option with get, set
        abstract compileOnSave: bool option with get, set

    type [<RequireQualifiedAccess>] WatchDirectoryFlags =
        | None = 0
        | Recursive = 1

    type [<AllowNullLiteral>] ExpandResult =
        abstract fileNames: ResizeArray<string> with get, set
        abstract wildcardDirectories: MapLike<WatchDirectoryFlags> with get, set

    type [<AllowNullLiteral>] ModuleResolutionHost =
        abstract fileExists: fileName: string -> bool
        abstract readFile: fileName: string -> string option
        abstract trace: s: string -> unit
        abstract directoryExists: directoryName: string -> bool
        /// Resolve a symbolic link.
        abstract realpath: path: string -> string
        abstract getCurrentDirectory: unit -> string
        abstract getDirectories: path: string -> ResizeArray<string>

    /// Represents the result of module resolution.
    /// Module resolution will pick up tsx/jsx/js files even if '--jsx' and '--allowJs' are turned off.
    /// The Program will then filter results based on these flags.
    /// 
    /// Prefer to return a `ResolvedModuleFull` so that the file type does not have to be inferred.
    type [<AllowNullLiteral>] ResolvedModule =
        /// Path of the file the module was resolved to. 
        abstract resolvedFileName: string with get, set
        /// Denotes if 'resolvedFileName' is isExternalLibraryImport and thus should be a proper external module:
        /// - be a .d.ts file
        /// - use top level imports\exports
        /// - don't use tripleslash references
        abstract isExternalLibraryImport: bool option with get, set

    /// ResolvedModule with an explicitly provided `extension` property.
    /// Prefer this over `ResolvedModule`.
    /// If changing this, remember to change `moduleResolutionIsEqualTo`.
    type [<AllowNullLiteral>] ResolvedModuleFull =
        inherit ResolvedModule
        /// Extension of resolvedFileName. This must match what's at the end of resolvedFileName.
        /// This is optional for backwards-compatibility, but will be added if not provided.
        abstract extension: Extension with get, set
        abstract packageId: PackageId option with get, set

    /// Unique identifier with a package name and version.
    /// If changing this, remember to change `packageIdIsEqual`.
    type [<AllowNullLiteral>] PackageId =
        /// Name of the package.
        /// Should not include `@types`.
        /// If accessing a non-index file, this should include its name e.g. "foo/bar".
        abstract name: string with get, set
        /// Name of a submodule within this package.
        /// May be "".
        abstract subModuleName: string with get, set
        /// Version of the package, e.g. "1.2.3" 
        abstract version: string with get, set

    type [<StringEnum>] [<RequireQualifiedAccess>] Extension =
        | Ts
        | Tsx
        | Dts
        | Js
        | Jsx

    type [<AllowNullLiteral>] ResolvedModuleWithFailedLookupLocations =
        abstract resolvedModule: ResolvedModuleFull option with get, set

    type [<AllowNullLiteral>] ResolvedTypeReferenceDirective =
        abstract primary: bool with get, set
        abstract resolvedFileName: string option with get, set
        abstract packageId: PackageId option with get, set

    type [<AllowNullLiteral>] ResolvedTypeReferenceDirectiveWithFailedLookupLocations =
        abstract resolvedTypeReferenceDirective: ResolvedTypeReferenceDirective with get, set
        abstract failedLookupLocations: ResizeArray<string> with get, set

    type [<AllowNullLiteral>] CompilerHost =
        inherit ModuleResolutionHost
        abstract getSourceFile: fileName: string * languageVersion: ScriptTarget * ?onError: (string -> unit) -> SourceFile
        abstract getSourceFileByPath: fileName: string * path: Path * languageVersion: ScriptTarget * ?onError: (string -> unit) -> SourceFile
        abstract getCancellationToken: unit -> CancellationToken
        abstract getDefaultLibFileName: options: CompilerOptions -> string
        abstract getDefaultLibLocation: unit -> string
        abstract writeFile: WriteFileCallback with get, set
        abstract getCurrentDirectory: unit -> string
        abstract getDirectories: path: string -> ResizeArray<string>
        abstract getCanonicalFileName: fileName: string -> string
        abstract useCaseSensitiveFileNames: unit -> bool
        abstract getNewLine: unit -> string
        abstract resolveModuleNames: moduleNames: ResizeArray<string> * containingFile: string -> ResizeArray<ResolvedModule>
        /// This method is a companion for 'resolveModuleNames' and is used to resolve 'types' references to actual type declaration files
        abstract resolveTypeReferenceDirectives: typeReferenceDirectiveNames: ResizeArray<string> * containingFile: string -> ResizeArray<ResolvedTypeReferenceDirective>
        abstract getEnvironmentVariable: name: string -> string

    type [<AllowNullLiteral>] SourceMapRange =
        inherit TextRange
        abstract source: SourceMapSource option with get, set

    type [<AllowNullLiteral>] SourceMapSource =
        abstract fileName: string with get, set
        abstract text: string with get, set
        abstract skipTrivia: (float -> float) option with get, set
        abstract getLineAndCharacterOfPosition: pos: float -> LineAndCharacter

    type [<RequireQualifiedAccess>] EmitFlags =
        | SingleLine = 1
        | AdviseOnEmitNode = 2
        | NoSubstitution = 4
        | CapturesThis = 8
        | NoLeadingSourceMap = 16
        | NoTrailingSourceMap = 32
        | NoSourceMap = 48
        | NoNestedSourceMaps = 64
        | NoTokenLeadingSourceMaps = 128
        | NoTokenTrailingSourceMaps = 256
        | NoTokenSourceMaps = 384
        | NoLeadingComments = 512
        | NoTrailingComments = 1024
        | NoComments = 1536
        | NoNestedComments = 2048
        | HelperName = 4096
        | ExportName = 8192
        | LocalName = 16384
        | InternalName = 32768
        | Indented = 65536
        | NoIndentation = 131072
        | AsyncFunctionBody = 262144
        | ReuseTempVariableScope = 524288
        | CustomPrologue = 1048576
        | NoHoisting = 2097152
        | HasEndOfDeclarationMarker = 4194304
        | Iterator = 8388608
        | NoAsciiEscaping = 16777216

    type [<AllowNullLiteral>] EmitHelper =
        abstract name: string with get, set
        abstract scoped: bool with get, set
        abstract text: string with get, set
        abstract priority: float option with get, set

    type [<RequireQualifiedAccess>] EmitHint =
        | SourceFile = 0
        | Expression = 1
        | IdentifierName = 2
        | MappedTypeParameter = 3
        | Unspecified = 4

    type [<AllowNullLiteral>] TransformationContext =
        /// Gets the compiler options supplied to the transformer. 
        abstract getCompilerOptions: unit -> CompilerOptions
        /// Starts a new lexical environment. 
        abstract startLexicalEnvironment: unit -> unit
        /// Suspends the current lexical environment, usually after visiting a parameter list. 
        abstract suspendLexicalEnvironment: unit -> unit
        /// Resumes a suspended lexical environment, usually before visiting a function body. 
        abstract resumeLexicalEnvironment: unit -> unit
        /// Ends a lexical environment, returning any declarations. 
        abstract endLexicalEnvironment: unit -> ResizeArray<Statement>
        /// Hoists a function declaration to the containing scope. 
        abstract hoistFunctionDeclaration: node: FunctionDeclaration -> unit
        /// Hoists a variable declaration to the containing scope. 
        abstract hoistVariableDeclaration: node: Identifier -> unit
        /// Records a request for a non-scoped emit helper in the current context. 
        abstract requestEmitHelper: helper: EmitHelper -> unit
        /// Gets and resets the requested non-scoped emit helpers. 
        abstract readEmitHelpers: unit -> ResizeArray<EmitHelper> option
        /// Enables expression substitutions in the pretty printer for the provided SyntaxKind. 
        abstract enableSubstitution: kind: SyntaxKind -> unit
        /// Determines whether expression substitutions are enabled for the provided node. 
        abstract isSubstitutionEnabled: node: Node -> bool
        /// Hook used by transformers to substitute expressions just before they
        /// are emitted by the pretty printer.
        /// 
        /// NOTE: Transformation hooks should only be modified during `Transformer` initialization,
        /// before returning the `NodeTransformer` callback.
        abstract onSubstituteNode: (EmitHint -> Node -> Node) with get, set
        /// Enables before/after emit notifications in the pretty printer for the provided
        /// SyntaxKind.
        abstract enableEmitNotification: kind: SyntaxKind -> unit
        /// Determines whether before/after emit notifications should be raised in the pretty
        /// printer when it emits a node.
        abstract isEmitNotificationEnabled: node: Node -> bool
        /// Hook used to allow transformers to capture state before or after
        /// the printer emits a node.
        /// 
        /// NOTE: Transformation hooks should only be modified during `Transformer` initialization,
        /// before returning the `NodeTransformer` callback.
        abstract onEmitNode: (EmitHint -> Node -> (EmitHint -> Node -> unit) -> unit) with get, set

    type [<AllowNullLiteral>] TransformationResult<'T> =
        /// Gets the transformed source files. 
        abstract transformed: ResizeArray<'T> with get, set
        /// Gets diagnostics for the transformation. 
        abstract diagnostics: ResizeArray<Diagnostic> option with get, set
        /// Gets a substitute for a node, if one is available; otherwise, returns the original node.
        abstract substituteNode: hint: EmitHint * node: Node -> Node
        /// Emits a node with possible notification.
        abstract emitNodeWithNotification: hint: EmitHint * node: Node * emitCallback: (EmitHint -> Node -> unit) -> unit
        /// Clean up EmitNode entries on any parse-tree nodes.
        abstract dispose: unit -> unit

    type TransformerFactory<'T> =
        (TransformationContext -> Transformer<'T>)

    type Transformer<'T> =
        ('T -> 'T)

    type Visitor =
        (Node -> VisitResult<Node>)

    type VisitResult<'T> =
        U2<'T, ResizeArray<'T>> option

    type [<AllowNullLiteral>] Printer =
        /// Print a node and its subtree as-is, without any emit transformations.
        abstract printNode: hint: EmitHint * node: Node * sourceFile: SourceFile -> string
        /// Prints a source file as-is, without any emit transformations.
        abstract printFile: sourceFile: SourceFile -> string
        /// Prints a bundle of source files as-is, without any emit transformations.
        abstract printBundle: bundle: Bundle -> string

    type [<AllowNullLiteral>] PrintHandlers =
        /// A hook used by the Printer when generating unique names to avoid collisions with
        /// globally defined names that exist outside of the current source file.
        abstract hasGlobalName: name: string -> bool
        /// A hook used by the Printer to provide notifications prior to emitting a node. A
        /// compatible implementation **must** invoke `emitCallback` with the provided `hint` and
        /// `node` values.
        abstract onEmitNode: hint: EmitHint * node: Node * emitCallback: (EmitHint -> Node -> unit) -> unit
        /// A hook used by the Printer to perform just-in-time substitution of a node. This is
        /// primarily used by node transformations that need to substitute one node for another,
        /// such as replacing `myExportedVar` with `exports.myExportedVar`.
        abstract substituteNode: hint: EmitHint * node: Node -> Node

    type [<AllowNullLiteral>] PrinterOptions =
        abstract removeComments: bool option with get, set
        abstract newLine: NewLineKind option with get, set

    type [<AllowNullLiteral>] TextSpan =
        abstract start: float with get, set
        abstract length: float with get, set

    type [<AllowNullLiteral>] TextChangeRange =
        abstract span: TextSpan with get, set
        abstract newLength: float with get, set

    type [<AllowNullLiteral>] SyntaxList =
        inherit Node
        abstract _children: ResizeArray<Node> with get, set

    type [<RequireQualifiedAccess>] FileWatcherEventKind =
        | Created = 0
        | Changed = 1
        | Deleted = 2

    type FileWatcherCallback =
        (string -> FileWatcherEventKind -> unit)

    type DirectoryWatcherCallback =
        (string -> unit)

    type [<AllowNullLiteral>] WatchedFile =
        abstract fileName: string with get, set
        abstract callback: FileWatcherCallback with get, set
        abstract mtime: DateTime option with get, set

    type [<AllowNullLiteral>] System =
        abstract args: ResizeArray<string> with get, set
        abstract newLine: string with get, set
        abstract useCaseSensitiveFileNames: bool with get, set
        abstract write: s: string -> unit
        abstract readFile: path: string * ?encoding: string -> string option
        abstract getFileSize: path: string -> float
        abstract writeFile: path: string * data: string * ?writeByteOrderMark: bool -> unit
        abstract watchFile: path: string * callback: FileWatcherCallback * ?pollingInterval: float -> FileWatcher
        abstract watchDirectory: path: string * callback: DirectoryWatcherCallback * ?recursive: bool -> FileWatcher
        abstract resolvePath: path: string -> string
        abstract fileExists: path: string -> bool
        abstract directoryExists: path: string -> bool
        abstract createDirectory: path: string -> unit
        abstract getExecutingFilePath: unit -> string
        abstract getCurrentDirectory: unit -> string
        abstract getDirectories: path: string -> ResizeArray<string>
        abstract readDirectory: path: string * ?extensions: ReadonlyArray<string> * ?exclude: ReadonlyArray<string> * ?``include``: ReadonlyArray<string> * ?depth: float -> ResizeArray<string>
        abstract getModifiedTime: path: string -> DateTime
        /// This should be cryptographically secure.
        /// A good implementation is node.js' `crypto.createHash`. (https://nodejs.org/api/crypto.html#crypto_crypto_createhash_algorithm)
        abstract createHash: data: string -> string
        abstract getMemoryUsage: unit -> float
        abstract exit: ?exitCode: float -> unit
        abstract realpath: path: string -> string
        abstract setTimeout: callback: (ResizeArray<obj> -> unit) * ms: float * [<ParamArray>] args: obj -> obj
        abstract clearTimeout: timeoutId: obj -> unit

    type [<AllowNullLiteral>] FileWatcher =
        abstract close: unit -> unit

    type [<AllowNullLiteral>] DirectoryWatcher =
        inherit FileWatcher
        abstract directoryName: string with get, set
        abstract referenceCount: float with get, set

    type [<AllowNullLiteral>] ErrorCallback =
        [<Emit "$0($1...)">] abstract Invoke: message: DiagnosticMessage * length: float -> unit

    type [<AllowNullLiteral>] Scanner =
        abstract getStartPos: unit -> float
        abstract getToken: unit -> SyntaxKind
        abstract getTextPos: unit -> float
        abstract getTokenPos: unit -> float
        abstract getTokenText: unit -> string
        abstract getTokenValue: unit -> string
        abstract hasExtendedUnicodeEscape: unit -> bool
        abstract hasPrecedingLineBreak: unit -> bool
        abstract isIdentifier: unit -> bool
        abstract isReservedWord: unit -> bool
        abstract isUnterminated: unit -> bool
        abstract reScanGreaterToken: unit -> SyntaxKind
        abstract reScanSlashToken: unit -> SyntaxKind
        abstract reScanTemplateToken: unit -> SyntaxKind
        abstract scanJsxIdentifier: unit -> SyntaxKind
        abstract scanJsxAttributeValue: unit -> SyntaxKind
        abstract reScanJsxToken: unit -> SyntaxKind
        abstract scanJsxToken: unit -> SyntaxKind
        abstract scanJSDocToken: unit -> SyntaxKind
        abstract scan: unit -> SyntaxKind
        abstract getText: unit -> string
        abstract setText: text: string * ?start: float * ?length: float -> unit
        abstract setOnError: onError: ErrorCallback -> unit
        abstract setScriptTarget: scriptTarget: ScriptTarget -> unit
        abstract setLanguageVariant: variant: LanguageVariant -> unit
        abstract setTextPos: textPos: float -> unit
        abstract lookAhead: callback: (unit -> 'T) -> 'T
        abstract scanRange: start: float * length: float * callback: (unit -> 'T) -> 'T
        abstract tryScan: callback: (unit -> 'T) -> 'T

    /// Cached module resolutions per containing directory.
    /// This assumes that any module id will have the same resolution for sibling files located in the same folder.
    type [<AllowNullLiteral>] ModuleResolutionCache =
        inherit NonRelativeModuleNameResolutionCache
        abstract getOrCreateCacheForDirectory: directoryName: string -> Map<ResolvedModuleWithFailedLookupLocations>

    /// Stored map from non-relative module name to a table: directory -> result of module lookup in this directory
    /// We support only non-relative module names because resolution of relative module names is usually more deterministic and thus less expensive.
    type [<AllowNullLiteral>] NonRelativeModuleNameResolutionCache =
        abstract getOrCreateCacheForModuleName: nonRelativeModuleName: string -> PerModuleNameCache

    type [<AllowNullLiteral>] PerModuleNameCache =
        abstract get: directory: string -> ResolvedModuleWithFailedLookupLocations
        abstract set: directory: string * result: ResolvedModuleWithFailedLookupLocations -> unit

    type [<AllowNullLiteral>] FormatDiagnosticsHost =
        abstract getCurrentDirectory: unit -> string
        abstract getCanonicalFileName: fileName: string -> string
        abstract getNewLine: unit -> string

    type [<AllowNullLiteral>] SourceFileLike =
        abstract getLineAndCharacterOfPosition: pos: float -> LineAndCharacter

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

    module Script_Snapshot =

        type [<AllowNullLiteral>] IExports =
            abstract fromString: text: string -> IScriptSnapshot

    type [<AllowNullLiteral>] PreProcessedFileInfo =
        abstract referencedFiles: ResizeArray<FileReference> with get, set
        abstract typeReferenceDirectives: ResizeArray<FileReference> with get, set
        abstract importedFiles: ResizeArray<FileReference> with get, set
        abstract ambientExternalModules: ResizeArray<string> with get, set
        abstract isLibFile: bool with get, set

    type [<AllowNullLiteral>] HostCancellationToken =
        abstract isCancellationRequested: unit -> bool

    type [<AllowNullLiteral>] LanguageServiceHost =
        abstract getCompilationSettings: unit -> CompilerOptions
        abstract getNewLine: unit -> string
        abstract getProjectVersion: unit -> string
        abstract getScriptFileNames: unit -> ResizeArray<string>
        abstract getScriptKind: fileName: string -> ScriptKind
        abstract getScriptVersion: fileName: string -> string
        abstract getScriptSnapshot: fileName: string -> IScriptSnapshot option
        abstract getLocalizedDiagnosticMessages: unit -> obj
        abstract getCancellationToken: unit -> HostCancellationToken
        abstract getCurrentDirectory: unit -> string
        abstract getDefaultLibFileName: options: CompilerOptions -> string
        abstract log: s: string -> unit
        abstract trace: s: string -> unit
        abstract error: s: string -> unit
        abstract useCaseSensitiveFileNames: unit -> bool
        abstract readDirectory: path: string * ?extensions: ReadonlyArray<string> * ?exclude: ReadonlyArray<string> * ?``include``: ReadonlyArray<string> * ?depth: float -> ResizeArray<string>
        abstract readFile: path: string * ?encoding: string -> string option
        abstract fileExists: path: string -> bool
        abstract getTypeRootsVersion: unit -> float
        abstract resolveModuleNames: moduleNames: ResizeArray<string> * containingFile: string -> ResizeArray<ResolvedModule>
        abstract resolveTypeReferenceDirectives: typeDirectiveNames: ResizeArray<string> * containingFile: string -> ResizeArray<ResolvedTypeReferenceDirective>
        abstract directoryExists: directoryName: string -> bool
        abstract getDirectories: directoryName: string -> ResizeArray<string>
        /// Gets a set of custom transformers to use during emit.
        abstract getCustomTransformers: unit -> CustomTransformers option

    type [<AllowNullLiteral>] LanguageService =
        abstract cleanupSemanticCache: unit -> unit
        abstract getSyntacticDiagnostics: fileName: string -> ResizeArray<Diagnostic>
        abstract getSemanticDiagnostics: fileName: string -> ResizeArray<Diagnostic>
        abstract getCompilerOptionsDiagnostics: unit -> ResizeArray<Diagnostic>
        abstract getSyntacticClassifications: fileName: string * span: TextSpan -> ResizeArray<ClassifiedSpan>
        abstract getSemanticClassifications: fileName: string * span: TextSpan -> ResizeArray<ClassifiedSpan>
        abstract getEncodedSyntacticClassifications: fileName: string * span: TextSpan -> Classifications
        abstract getEncodedSemanticClassifications: fileName: string * span: TextSpan -> Classifications
        abstract getCompletionsAtPosition: fileName: string * position: float -> CompletionInfo
        abstract getCompletionEntryDetails: fileName: string * position: float * entryName: string -> CompletionEntryDetails
        abstract getCompletionEntrySymbol: fileName: string * position: float * entryName: string -> Symbol
        abstract getQuickInfoAtPosition: fileName: string * position: float -> QuickInfo
        abstract getNameOrDottedNameSpan: fileName: string * startPos: float * endPos: float -> TextSpan
        abstract getBreakpointStatementAtPosition: fileName: string * position: float -> TextSpan
        abstract getSignatureHelpItems: fileName: string * position: float -> SignatureHelpItems
        abstract getRenameInfo: fileName: string * position: float -> RenameInfo
        abstract findRenameLocations: fileName: string * position: float * findInStrings: bool * findInComments: bool -> ResizeArray<RenameLocation>
        abstract getDefinitionAtPosition: fileName: string * position: float -> ResizeArray<DefinitionInfo>
        abstract getTypeDefinitionAtPosition: fileName: string * position: float -> ResizeArray<DefinitionInfo>
        abstract getImplementationAtPosition: fileName: string * position: float -> ResizeArray<ImplementationLocation>
        abstract getReferencesAtPosition: fileName: string * position: float -> ResizeArray<ReferenceEntry>
        abstract findReferences: fileName: string * position: float -> ResizeArray<ReferencedSymbol>
        abstract getDocumentHighlights: fileName: string * position: float * filesToSearch: ResizeArray<string> -> ResizeArray<DocumentHighlights>
        abstract getOccurrencesAtPosition: fileName: string * position: float -> ResizeArray<ReferenceEntry>
        abstract getNavigateToItems: searchValue: string * ?maxResultCount: float * ?fileName: string * ?excludeDtsFiles: bool -> ResizeArray<NavigateToItem>
        abstract getNavigationBarItems: fileName: string -> ResizeArray<NavigationBarItem>
        abstract getNavigationTree: fileName: string -> NavigationTree
        abstract getOutliningSpans: fileName: string -> ResizeArray<OutliningSpan>
        abstract getTodoComments: fileName: string * descriptors: ResizeArray<TodoCommentDescriptor> -> ResizeArray<TodoComment>
        abstract getBraceMatchingAtPosition: fileName: string * position: float -> ResizeArray<TextSpan>
        abstract getIndentationAtPosition: fileName: string * position: float * options: U2<EditorOptions, EditorSettings> -> float
        abstract getFormattingEditsForRange: fileName: string * start: float * ``end``: float * options: U2<FormatCodeOptions, FormatCodeSettings> -> ResizeArray<TextChange>
        abstract getFormattingEditsForDocument: fileName: string * options: U2<FormatCodeOptions, FormatCodeSettings> -> ResizeArray<TextChange>
        abstract getFormattingEditsAfterKeystroke: fileName: string * position: float * key: string * options: U2<FormatCodeOptions, FormatCodeSettings> -> ResizeArray<TextChange>
        abstract getDocCommentTemplateAtPosition: fileName: string * position: float -> TextInsertion
        abstract isValidBraceCompletionAtPosition: fileName: string * position: float * openingBrace: float -> bool
        abstract getCodeFixesAtPosition: fileName: string * start: float * ``end``: float * errorCodes: ResizeArray<float> * formatOptions: FormatCodeSettings -> ResizeArray<CodeAction>
        abstract getApplicableRefactors: fileName: string * positionOrRaneg: U2<float, TextRange> -> ResizeArray<ApplicableRefactorInfo>
        abstract getEditsForRefactor: fileName: string * formatOptions: FormatCodeSettings * positionOrRange: U2<float, TextRange> * refactorName: string * actionName: string -> RefactorEditInfo option
        abstract getEmitOutput: fileName: string * ?emitOnlyDtsFiles: bool -> EmitOutput
        abstract getProgram: unit -> Program
        abstract dispose: unit -> unit

    type [<AllowNullLiteral>] Classifications =
        abstract spans: ResizeArray<float> with get, set
        abstract endOfLineState: EndOfLineState with get, set

    type [<AllowNullLiteral>] ClassifiedSpan =
        abstract textSpan: TextSpan with get, set
        abstract classificationType: ClassificationTypeNames with get, set

    /// Navigation bar interface designed for visual studio's dual-column layout.
    /// This does not form a proper tree.
    /// The navbar is returned as a list of top-level items, each of which has a list of child items.
    /// Child items always have an empty array for their `childItems`.
    type [<AllowNullLiteral>] NavigationBarItem =
        abstract text: string with get, set
        abstract kind: ScriptElementKind with get, set
        abstract kindModifiers: string with get, set
        abstract spans: ResizeArray<TextSpan> with get, set
        abstract childItems: ResizeArray<NavigationBarItem> with get, set
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
        abstract spans: ResizeArray<TextSpan> with get, set
        /// Present if non-empty 
        abstract childItems: ResizeArray<NavigationTree> option with get, set

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
        abstract textChanges: ResizeArray<TextChange> with get, set

    type [<AllowNullLiteral>] CodeAction =
        /// Description of the code action to display in the UI of the editor 
        abstract description: string with get, set
        /// Text changes to apply to each file as part of the code action 
        abstract changes: ResizeArray<FileTextChanges> with get, set

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
        abstract actions: ResizeArray<RefactorActionInfo> with get, set

    type RefactorActionInfo =
        obj

    type RefactorEditInfo =
        obj

    type [<AllowNullLiteral>] TextInsertion =
        abstract newText: string with get, set
        /// The position in newText the caret should point to after the insertion. 
        abstract caretOffset: float with get, set

    type [<AllowNullLiteral>] DocumentSpan =
        abstract textSpan: TextSpan with get, set
        abstract fileName: string with get, set

    type [<AllowNullLiteral>] RenameLocation =
        inherit DocumentSpan

    type [<AllowNullLiteral>] ReferenceEntry =
        inherit DocumentSpan
        abstract isWriteAccess: bool with get, set
        abstract isDefinition: bool with get, set
        abstract isInString: obj option with get, set

    type [<AllowNullLiteral>] ImplementationLocation =
        inherit DocumentSpan
        abstract kind: ScriptElementKind with get, set
        abstract displayParts: ResizeArray<SymbolDisplayPart> with get, set

    type [<AllowNullLiteral>] DocumentHighlights =
        abstract fileName: string with get, set
        abstract highlightSpans: ResizeArray<HighlightSpan> with get, set

    type [<StringEnum>] [<RequireQualifiedAccess>] HighlightSpanKind =
        | [<CompiledName "none">] None
        | [<CompiledName "definition">] Definition
        | [<CompiledName "reference">] Reference
        | [<CompiledName "writtenReference">] WrittenReference

    type [<AllowNullLiteral>] HighlightSpan =
        abstract fileName: string option with get, set
        abstract isInString: obj option with get, set
        abstract textSpan: TextSpan with get, set
        abstract kind: HighlightSpanKind with get, set

    type [<AllowNullLiteral>] NavigateToItem =
        abstract name: string with get, set
        abstract kind: ScriptElementKind with get, set
        abstract kindModifiers: string with get, set
        abstract matchKind: string with get, set
        abstract isCaseSensitive: bool with get, set
        abstract fileName: string with get, set
        abstract textSpan: TextSpan with get, set
        abstract containerName: string with get, set
        abstract containerKind: ScriptElementKind with get, set

    type [<RequireQualifiedAccess>] IndentStyle =
        | None = 0
        | Block = 1
        | Smart = 2

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

    type [<AllowNullLiteral>] FormatCodeSettings =
        inherit EditorSettings
        abstract insertSpaceAfterCommaDelimiter: bool option with get, set
        abstract insertSpaceAfterSemicolonInForStatements: bool option with get, set
        abstract insertSpaceBeforeAndAfterBinaryOperators: bool option with get, set
        abstract insertSpaceAfterConstructor: bool option with get, set
        abstract insertSpaceAfterKeywordsInControlFlowStatements: bool option with get, set
        abstract insertSpaceAfterFunctionKeywordForAnonymousFunctions: bool option with get, set
        abstract insertSpaceAfterOpeningAndBeforeClosingNonemptyParenthesis: bool option with get, set
        abstract insertSpaceAfterOpeningAndBeforeClosingNonemptyBrackets: bool option with get, set
        abstract insertSpaceAfterOpeningAndBeforeClosingNonemptyBraces: bool option with get, set
        abstract insertSpaceAfterOpeningAndBeforeClosingTemplateStringBraces: bool option with get, set
        abstract insertSpaceAfterOpeningAndBeforeClosingJsxExpressionBraces: bool option with get, set
        abstract insertSpaceAfterTypeAssertion: bool option with get, set
        abstract insertSpaceBeforeFunctionParenthesis: bool option with get, set
        abstract placeOpenBraceOnNewLineForFunctions: bool option with get, set
        abstract placeOpenBraceOnNewLineForControlBlocks: bool option with get, set

    type [<AllowNullLiteral>] DefinitionInfo =
        abstract fileName: string with get, set
        abstract textSpan: TextSpan with get, set
        abstract kind: ScriptElementKind with get, set
        abstract name: string with get, set
        abstract containerKind: ScriptElementKind with get, set
        abstract containerName: string with get, set

    type [<AllowNullLiteral>] ReferencedSymbolDefinitionInfo =
        inherit DefinitionInfo
        abstract displayParts: ResizeArray<SymbolDisplayPart> with get, set

    type [<AllowNullLiteral>] ReferencedSymbol =
        abstract definition: ReferencedSymbolDefinitionInfo with get, set
        abstract references: ResizeArray<ReferenceEntry> with get, set

    type [<RequireQualifiedAccess>] SymbolDisplayPartKind =
        | [<CompiledName "aliasName">] AliasName = 0
        | [<CompiledName "className">] ClassName = 1
        | [<CompiledName "enumName">] EnumName = 2
        | [<CompiledName "fieldName">] FieldName = 3
        | [<CompiledName "interfaceName">] InterfaceName = 4
        | [<CompiledName "keyword">] Keyword = 5
        | [<CompiledName "lineBreak">] LineBreak = 6
        | [<CompiledName "numericLiteral">] NumericLiteral = 7
        | [<CompiledName "stringLiteral">] StringLiteral = 8
        | [<CompiledName "localName">] LocalName = 9
        | [<CompiledName "methodName">] MethodName = 10
        | [<CompiledName "moduleName">] ModuleName = 11
        | [<CompiledName "operator">] Operator = 12
        | [<CompiledName "parameterName">] ParameterName = 13
        | [<CompiledName "propertyName">] PropertyName = 14
        | [<CompiledName "punctuation">] Punctuation = 15
        | [<CompiledName "space">] Space = 16
        | [<CompiledName "text">] Text = 17
        | [<CompiledName "typeParameterName">] TypeParameterName = 18
        | [<CompiledName "enumMemberName">] EnumMemberName = 19
        | [<CompiledName "functionName">] FunctionName = 20
        | [<CompiledName "regularExpressionLiteral">] RegularExpressionLiteral = 21

    type [<AllowNullLiteral>] SymbolDisplayPart =
        abstract text: string with get, set
        abstract kind: string with get, set

    type [<AllowNullLiteral>] JSDocTagInfo =
        abstract name: string with get, set
        abstract text: string option with get, set

    type [<AllowNullLiteral>] QuickInfo =
        abstract kind: ScriptElementKind with get, set
        abstract kindModifiers: string with get, set
        abstract textSpan: TextSpan with get, set
        abstract displayParts: ResizeArray<SymbolDisplayPart> with get, set
        abstract documentation: ResizeArray<SymbolDisplayPart> with get, set
        abstract tags: ResizeArray<JSDocTagInfo> with get, set

    type [<AllowNullLiteral>] RenameInfo =
        abstract canRename: bool with get, set
        abstract localizedErrorMessage: string with get, set
        abstract displayName: string with get, set
        abstract fullDisplayName: string with get, set
        abstract kind: ScriptElementKind with get, set
        abstract kindModifiers: string with get, set
        abstract triggerSpan: TextSpan with get, set

    type [<AllowNullLiteral>] SignatureHelpParameter =
        abstract name: string with get, set
        abstract documentation: ResizeArray<SymbolDisplayPart> with get, set
        abstract displayParts: ResizeArray<SymbolDisplayPart> with get, set
        abstract isOptional: bool with get, set

    /// Represents a single signature to show in signature help.
    /// The id is used for subsequent calls into the language service to ask questions about the
    /// signature help item in the context of any documents that have been updated.  i.e. after
    /// an edit has happened, while signature help is still active, the host can ask important
    /// questions like 'what parameter is the user currently contained within?'.
    type [<AllowNullLiteral>] SignatureHelpItem =
        abstract isVariadic: bool with get, set
        abstract prefixDisplayParts: ResizeArray<SymbolDisplayPart> with get, set
        abstract suffixDisplayParts: ResizeArray<SymbolDisplayPart> with get, set
        abstract separatorDisplayParts: ResizeArray<SymbolDisplayPart> with get, set
        abstract parameters: ResizeArray<SignatureHelpParameter> with get, set
        abstract documentation: ResizeArray<SymbolDisplayPart> with get, set
        abstract tags: ResizeArray<JSDocTagInfo> with get, set

    /// Represents a set of signature help items, and the preferred item that should be selected.
    type [<AllowNullLiteral>] SignatureHelpItems =
        abstract items: ResizeArray<SignatureHelpItem> with get, set
        abstract applicableSpan: TextSpan with get, set
        abstract selectedItemIndex: float with get, set
        abstract argumentIndex: float with get, set
        abstract argumentCount: float with get, set

    type [<AllowNullLiteral>] CompletionInfo =
        abstract isGlobalCompletion: bool with get, set
        abstract isMemberCompletion: bool with get, set
        /// true when the current location also allows for a new identifier
        abstract isNewIdentifierLocation: bool with get, set
        abstract entries: ResizeArray<CompletionEntry> with get, set

    type [<AllowNullLiteral>] CompletionEntry =
        abstract name: string with get, set
        abstract kind: ScriptElementKind with get, set
        abstract kindModifiers: string with get, set
        abstract sortText: string with get, set
        /// An optional span that indicates the text to be replaced by this completion item. It will be
        /// set if the required span differs from the one generated by the default replacement behavior and should
        /// be used in that case
        abstract replacementSpan: TextSpan option with get, set

    type [<AllowNullLiteral>] CompletionEntryDetails =
        abstract name: string with get, set
        abstract kind: ScriptElementKind with get, set
        abstract kindModifiers: string with get, set
        abstract displayParts: ResizeArray<SymbolDisplayPart> with get, set
        abstract documentation: ResizeArray<SymbolDisplayPart> with get, set
        abstract tags: ResizeArray<JSDocTagInfo> with get, set

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

    type [<AllowNullLiteral>] EmitOutput =
        abstract outputFiles: ResizeArray<OutputFile> with get, set
        abstract emitSkipped: bool with get, set

    type [<RequireQualifiedAccess>] OutputFileType =
        | JavaScript = 0
        | SourceMap = 1
        | Declaration = 2

    type [<AllowNullLiteral>] OutputFile =
        abstract name: string with get, set
        abstract writeByteOrderMark: bool with get, set
        abstract text: string with get, set

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
        | StringLiteral = 7
        | RegExpLiteral = 8

    type [<AllowNullLiteral>] ClassificationResult =
        abstract finalLexState: EndOfLineState with get, set
        abstract entries: ResizeArray<ClassificationInfo> with get, set

    type [<AllowNullLiteral>] ClassificationInfo =
        abstract length: float with get, set
        abstract classification: TokenClass with get, set

    type [<AllowNullLiteral>] Classifier =
        /// Gives lexical classifications of tokens on a line without any syntactic context.
        /// For instance, a token consisting of the text 'string' can be either an identifier
        /// named 'string' or the keyword 'string', however, because this classifier is not aware,
        /// it relies on certain heuristics to give acceptable results. For classifications where
        /// speed trumps accuracy, this function is preferable; however, for true accuracy, the
        /// syntactic classifier is ideal. In fact, in certain editing scenarios, combining the
        /// lexical, syntactic, and semantic classifiers may issue the best user experience.
        abstract getClassificationsForLine: text: string * lexState: EndOfLineState * syntacticClassifierAbsent: bool -> ClassificationResult
        abstract getEncodedLexicalClassifications: text: string * endOfLineState: EndOfLineState * syntacticClassifierAbsent: bool -> Classifications

    type [<StringEnum>] [<RequireQualifiedAccess>] ScriptElementKind =
        | [<CompiledName "unknown">] Unknown
        | [<CompiledName "warning">] Warning
        | [<CompiledName "keyword">] Keyword
        | [<CompiledName "scriptElement">] ScriptElement
        | [<CompiledName "moduleElement">] ModuleElement
        | [<CompiledName "classElement">] ClassElement
        | [<CompiledName "localClassElement">] LocalClassElement
        | [<CompiledName "interfaceElement">] InterfaceElement
        | [<CompiledName "typeElement">] TypeElement
        | [<CompiledName "enumElement">] EnumElement
        | [<CompiledName "enumMemberElement">] EnumMemberElement
        | [<CompiledName "variableElement">] VariableElement
        | [<CompiledName "localVariableElement">] LocalVariableElement
        | [<CompiledName "functionElement">] FunctionElement
        | [<CompiledName "localFunctionElement">] LocalFunctionElement
        | [<CompiledName "memberFunctionElement">] MemberFunctionElement
        | [<CompiledName "memberGetAccessorElement">] MemberGetAccessorElement
        | [<CompiledName "memberSetAccessorElement">] MemberSetAccessorElement
        | [<CompiledName "memberVariableElement">] MemberVariableElement
        | [<CompiledName "constructorImplementationElement">] ConstructorImplementationElement
        | [<CompiledName "callSignatureElement">] CallSignatureElement
        | [<CompiledName "indexSignatureElement">] IndexSignatureElement
        | [<CompiledName "constructSignatureElement">] ConstructSignatureElement
        | [<CompiledName "parameterElement">] ParameterElement
        | [<CompiledName "typeParameterElement">] TypeParameterElement
        | [<CompiledName "primitiveType">] PrimitiveType
        | [<CompiledName "label">] Label
        | [<CompiledName "alias">] Alias
        | [<CompiledName "constElement">] ConstElement
        | [<CompiledName "letElement">] LetElement
        | [<CompiledName "directory">] Directory
        | [<CompiledName "externalModuleName">] ExternalModuleName
        | [<CompiledName "jsxAttribute">] JsxAttribute

    type [<StringEnum>] [<RequireQualifiedAccess>] ScriptElementKindModifier =
        | [<CompiledName "none">] None
        | [<CompiledName "publicMemberModifier">] PublicMemberModifier
        | [<CompiledName "privateMemberModifier">] PrivateMemberModifier
        | [<CompiledName "protectedMemberModifier">] ProtectedMemberModifier
        | [<CompiledName "exportedModifier">] ExportedModifier
        | [<CompiledName "ambientModifier">] AmbientModifier
        | [<CompiledName "staticModifier">] StaticModifier
        | [<CompiledName "abstractModifier">] AbstractModifier

    type [<StringEnum>] [<RequireQualifiedAccess>] ClassificationTypeNames =
        | [<CompiledName "comment">] Comment
        | [<CompiledName "identifier">] Identifier
        | [<CompiledName "keyword">] Keyword
        | [<CompiledName "numericLiteral">] NumericLiteral
        | [<CompiledName "operator">] Operator
        | [<CompiledName "stringLiteral">] StringLiteral
        | [<CompiledName "whiteSpace">] WhiteSpace
        | [<CompiledName "text">] Text
        | [<CompiledName "punctuation">] Punctuation
        | [<CompiledName "className">] ClassName
        | [<CompiledName "enumName">] EnumName
        | [<CompiledName "interfaceName">] InterfaceName
        | [<CompiledName "moduleName">] ModuleName
        | [<CompiledName "typeParameterName">] TypeParameterName
        | [<CompiledName "typeAliasName">] TypeAliasName
        | [<CompiledName "parameterName">] ParameterName
        | [<CompiledName "docCommentTagName">] DocCommentTagName
        | [<CompiledName "jsxOpenTagName">] JsxOpenTagName
        | [<CompiledName "jsxCloseTagName">] JsxCloseTagName
        | [<CompiledName "jsxSelfClosingTagName">] JsxSelfClosingTagName
        | [<CompiledName "jsxAttribute">] JsxAttribute
        | [<CompiledName "jsxText">] JsxText
        | [<CompiledName "jsxAttributeStringLiteralValue">] JsxAttributeStringLiteralValue

    type [<RequireQualifiedAccess>] ClassificationType =
        | [<CompiledName "comment">] Comment = 1
        | [<CompiledName "identifier">] Identifier = 2
        | [<CompiledName "keyword">] Keyword = 3
        | [<CompiledName "numericLiteral">] NumericLiteral = 4
        | [<CompiledName "operator">] Operator = 5
        | [<CompiledName "stringLiteral">] StringLiteral = 6
        | [<CompiledName "regularExpressionLiteral">] RegularExpressionLiteral = 7
        | [<CompiledName "whiteSpace">] WhiteSpace = 8
        | [<CompiledName "text">] Text = 9
        | [<CompiledName "punctuation">] Punctuation = 10
        | [<CompiledName "className">] ClassName = 11
        | [<CompiledName "enumName">] EnumName = 12
        | [<CompiledName "interfaceName">] InterfaceName = 13
        | [<CompiledName "moduleName">] ModuleName = 14
        | [<CompiledName "typeParameterName">] TypeParameterName = 15
        | [<CompiledName "typeAliasName">] TypeAliasName = 16
        | [<CompiledName "parameterName">] ParameterName = 17
        | [<CompiledName "docCommentTagName">] DocCommentTagName = 18
        | [<CompiledName "jsxOpenTagName">] JsxOpenTagName = 19
        | [<CompiledName "jsxCloseTagName">] JsxCloseTagName = 20
        | [<CompiledName "jsxSelfClosingTagName">] JsxSelfClosingTagName = 21
        | [<CompiledName "jsxAttribute">] JsxAttribute = 22
        | [<CompiledName "jsxText">] JsxText = 23
        | [<CompiledName "jsxAttributeStringLiteralValue">] JsxAttributeStringLiteralValue = 24

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
        /// Request a stored SourceFile with a given fileName and compilationSettings.
        /// The first call to acquire will call createLanguageServiceSourceFile to generate
        /// the SourceFile if was not found in the registry.
        abstract acquireDocument: fileName: string * compilationSettings: CompilerOptions * scriptSnapshot: IScriptSnapshot * version: string * ?scriptKind: ScriptKind -> SourceFile
        abstract acquireDocumentWithKey: fileName: string * path: Path * compilationSettings: CompilerOptions * key: DocumentRegistryBucketKey * scriptSnapshot: IScriptSnapshot * version: string * ?scriptKind: ScriptKind -> SourceFile
        /// Request an updated version of an already existing SourceFile with a given fileName
        /// and compilationSettings. The update will in-turn call updateLanguageServiceSourceFile
        /// to get an updated SourceFile.
        abstract updateDocument: fileName: string * compilationSettings: CompilerOptions * scriptSnapshot: IScriptSnapshot * version: string * ?scriptKind: ScriptKind -> SourceFile
        abstract updateDocumentWithKey: fileName: string * path: Path * compilationSettings: CompilerOptions * key: DocumentRegistryBucketKey * scriptSnapshot: IScriptSnapshot * version: string * ?scriptKind: ScriptKind -> SourceFile
        abstract getKeyForCompilationSettings: settings: CompilerOptions -> DocumentRegistryBucketKey
        /// Informs the DocumentRegistry that a file is not needed any longer.
        /// 
        /// Note: It is not allowed to call release on a SourceFile that was not acquired from
        /// this registry originally.
        abstract releaseDocument: fileName: string * compilationSettings: CompilerOptions -> unit
        abstract releaseDocumentWithKey: path: Path * key: DocumentRegistryBucketKey -> unit
        abstract reportStats: unit -> string

    type DocumentRegistryBucketKey =
        obj

    type [<AllowNullLiteral>] TranspileOptions =
        abstract compilerOptions: CompilerOptions option with get, set
        abstract fileName: string option with get, set
        abstract reportDiagnostics: bool option with get, set
        abstract moduleName: string option with get, set
        abstract renamedDependencies: MapLike<string> option with get, set
        abstract transformers: CustomTransformers option with get, set

    type [<AllowNullLiteral>] TranspileOutput =
        abstract outputText: string with get, set
        abstract diagnostics: ResizeArray<Diagnostic> option with get, set
        abstract sourceMapText: string option with get, set

    type [<AllowNullLiteral>] DisplayPartsSymbolWriter =
        inherit SymbolWriter
        abstract displayParts: unit -> ResizeArray<SymbolDisplayPart>
