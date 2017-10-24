namespace rec Fable.Import
open System
open System.Text.RegularExpressions
open Fable.Core
open Fable.Import.JS

module ts =

    type Globals =
        member __.getNodeMajorVersion(): float = jsNative
        member __.tokenToString(t: SyntaxKind): string option = jsNative
        member __.getPositionOfLineAndCharacter(sourceFile: SourceFile, line: float, character: float): float = jsNative
        member __.getLineAndCharacterOfPosition(sourceFile: SourceFileLike, position: float): LineAndCharacter = jsNative
        member __.isWhiteSpaceLike(ch: float): bool = jsNative
        member __.isWhiteSpaceSingleLine(ch: float): bool = jsNative
        member __.isLineBreak(ch: float): bool = jsNative
        member __.couldStartTrivia(text: string, pos: float): bool = jsNative
        member __.forEachLeadingCommentRange<'T, 'U>(text: string, pos: float, cb: Func<float, float, CommentKind, bool, 'T, 'U>, ?state: 'T): 'U option = jsNative
        member __.forEachTrailingCommentRange<'T, 'U>(text: string, pos: float, cb: Func<float, float, CommentKind, bool, 'T, 'U>, ?state: 'T): 'U option = jsNative
        member __.reduceEachLeadingCommentRange<'T, 'U>(text: string, pos: float, cb: Func<float, float, CommentKind, bool, 'T, 'U, 'U>, state: 'T, initial: 'U): 'U = jsNative
        member __.reduceEachTrailingCommentRange<'T, 'U>(text: string, pos: float, cb: Func<float, float, CommentKind, bool, 'T, 'U, 'U>, state: 'T, initial: 'U): 'U = jsNative
        member __.getLeadingCommentRanges(text: string, pos: float): ResizeArray<CommentRange> option = jsNative
        member __.getTrailingCommentRanges(text: string, pos: float): ResizeArray<CommentRange> option = jsNative
        member __.getShebang(text: string): string option = jsNative
        member __.isIdentifierStart(ch: float, languageVersion: ScriptTarget): bool = jsNative
        member __.isIdentifierPart(ch: float, languageVersion: ScriptTarget): bool = jsNative
        member __.createScanner(languageVersion: ScriptTarget, skipTrivia: bool, ?languageVariant: LanguageVariant, ?text: string, ?onError: ErrorCallback, ?start: float, ?length: float): Scanner = jsNative
        member __.getDefaultLibFileName(options: CompilerOptions): string = jsNative
        member __.textSpanEnd(span: TextSpan): float = jsNative
        member __.textSpanIsEmpty(span: TextSpan): bool = jsNative
        member __.textSpanContainsPosition(span: TextSpan, position: float): bool = jsNative
        member __.textSpanContainsTextSpan(span: TextSpan, other: TextSpan): bool = jsNative
        member __.textSpanOverlapsWith(span: TextSpan, other: TextSpan): bool = jsNative
        member __.textSpanOverlap(span1: TextSpan, span2: TextSpan): TextSpan = jsNative
        member __.textSpanIntersectsWithTextSpan(span: TextSpan, other: TextSpan): bool = jsNative
        member __.textSpanIntersectsWith(span: TextSpan, start: float, length: float): bool = jsNative
        member __.decodedTextSpanIntersectsWith(start1: float, length1: float, start2: float, length2: float): bool = jsNative
        member __.textSpanIntersectsWithPosition(span: TextSpan, position: float): bool = jsNative
        member __.textSpanIntersection(span1: TextSpan, span2: TextSpan): TextSpan = jsNative
        member __.createTextSpan(start: float, length: float): TextSpan = jsNative
        member __.createTextSpanFromBounds(start: float, ``end``: float): TextSpan = jsNative
        member __.textChangeRangeNewSpan(range: TextChangeRange): TextSpan = jsNative
        member __.textChangeRangeIsUnchanged(range: TextChangeRange): bool = jsNative
        member __.createTextChangeRange(span: TextSpan, newLength: float): TextChangeRange = jsNative
        member __.collapseTextChangeRangesAcrossMultipleVersions(changes: ReadonlyArray<TextChangeRange>): TextChangeRange = jsNative
        member __.getTypeParameterOwner(d: Declaration): Declaration = jsNative
        member __.isParameterPropertyDeclaration(node: Node): bool = jsNative
        member __.isEmptyBindingPattern(node: BindingName): bool = jsNative
        member __.isEmptyBindingElement(node: BindingElement): bool = jsNative
        member __.getCombinedModifierFlags(node: Node): ModifierFlags = jsNative
        member __.getCombinedNodeFlags(node: Node): NodeFlags = jsNative
        member __.validateLocaleAndSetLanguage(locale: string, sys: obj, ?errors: Push<Diagnostic>): unit = jsNative
        member __.getOriginalNode(node: Node): Node = jsNative
        member __.getOriginalNode<'T>(node: Node, nodeTest: Func<Node, bool>): 'T = jsNative
        member __.isParseTreeNode(node: Node): bool = jsNative
        member __.getParseTreeNode(node: Node): Node = jsNative
        member __.getParseTreeNode<'T>(node: Node, ?nodeTest: Func<Node, bool>): 'T = jsNative
        member __.unescapeLeadingUnderscores(identifier: __String): string = jsNative
        member __.unescapeIdentifier(id: string): string = jsNative
        member __.getNameOfDeclaration(declaration: Declaration): DeclarationName option = jsNative
        member __.isNumericLiteral(node: Node): bool = jsNative
        member __.isStringLiteral(node: Node): bool = jsNative
        member __.isJsxText(node: Node): bool = jsNative
        member __.isRegularExpressionLiteral(node: Node): bool = jsNative
        member __.isNoSubstitutionTemplateLiteral(node: Node): bool = jsNative
        member __.isTemplateHead(node: Node): bool = jsNative
        member __.isTemplateMiddle(node: Node): bool = jsNative
        member __.isTemplateTail(node: Node): bool = jsNative
        member __.isIdentifier(node: Node): bool = jsNative
        member __.isQualifiedName(node: Node): bool = jsNative
        member __.isComputedPropertyName(node: Node): bool = jsNative
        member __.isTypeParameterDeclaration(node: Node): bool = jsNative
        member __.isParameter(node: Node): bool = jsNative
        member __.isDecorator(node: Node): bool = jsNative
        member __.isPropertySignature(node: Node): bool = jsNative
        member __.isPropertyDeclaration(node: Node): bool = jsNative
        member __.isMethodSignature(node: Node): bool = jsNative
        member __.isMethodDeclaration(node: Node): bool = jsNative
        member __.isConstructorDeclaration(node: Node): bool = jsNative
        member __.isGetAccessorDeclaration(node: Node): bool = jsNative
        member __.isSetAccessorDeclaration(node: Node): bool = jsNative
        member __.isCallSignatureDeclaration(node: Node): bool = jsNative
        member __.isConstructSignatureDeclaration(node: Node): bool = jsNative
        member __.isIndexSignatureDeclaration(node: Node): bool = jsNative
        member __.isTypePredicateNode(node: Node): bool = jsNative
        member __.isTypeReferenceNode(node: Node): bool = jsNative
        member __.isFunctionTypeNode(node: Node): bool = jsNative
        member __.isConstructorTypeNode(node: Node): bool = jsNative
        member __.isTypeQueryNode(node: Node): bool = jsNative
        member __.isTypeLiteralNode(node: Node): bool = jsNative
        member __.isArrayTypeNode(node: Node): bool = jsNative
        member __.isTupleTypeNode(node: Node): bool = jsNative
        member __.isUnionTypeNode(node: Node): bool = jsNative
        member __.isIntersectionTypeNode(node: Node): bool = jsNative
        member __.isParenthesizedTypeNode(node: Node): bool = jsNative
        member __.isThisTypeNode(node: Node): bool = jsNative
        member __.isTypeOperatorNode(node: Node): bool = jsNative
        member __.isIndexedAccessTypeNode(node: Node): bool = jsNative
        member __.isMappedTypeNode(node: Node): bool = jsNative
        member __.isLiteralTypeNode(node: Node): bool = jsNative
        member __.isObjectBindingPattern(node: Node): bool = jsNative
        member __.isArrayBindingPattern(node: Node): bool = jsNative
        member __.isBindingElement(node: Node): bool = jsNative
        member __.isArrayLiteralExpression(node: Node): bool = jsNative
        member __.isObjectLiteralExpression(node: Node): bool = jsNative
        member __.isPropertyAccessExpression(node: Node): bool = jsNative
        member __.isElementAccessExpression(node: Node): bool = jsNative
        member __.isCallExpression(node: Node): bool = jsNative
        member __.isNewExpression(node: Node): bool = jsNative
        member __.isTaggedTemplateExpression(node: Node): bool = jsNative
        member __.isTypeAssertion(node: Node): bool = jsNative
        member __.isParenthesizedExpression(node: Node): bool = jsNative
        member __.skipPartiallyEmittedExpressions(node: Expression): Expression = jsNative
        member __.skipPartiallyEmittedExpressions(node: Node): Node = jsNative
        member __.isFunctionExpression(node: Node): bool = jsNative
        member __.isArrowFunction(node: Node): bool = jsNative
        member __.isDeleteExpression(node: Node): bool = jsNative
        member __.isTypeOfExpression(node: Node): bool = jsNative
        member __.isVoidExpression(node: Node): bool = jsNative
        member __.isAwaitExpression(node: Node): bool = jsNative
        member __.isPrefixUnaryExpression(node: Node): bool = jsNative
        member __.isPostfixUnaryExpression(node: Node): bool = jsNative
        member __.isBinaryExpression(node: Node): bool = jsNative
        member __.isConditionalExpression(node: Node): bool = jsNative
        member __.isTemplateExpression(node: Node): bool = jsNative
        member __.isYieldExpression(node: Node): bool = jsNative
        member __.isSpreadElement(node: Node): bool = jsNative
        member __.isClassExpression(node: Node): bool = jsNative
        member __.isOmittedExpression(node: Node): bool = jsNative
        member __.isExpressionWithTypeArguments(node: Node): bool = jsNative
        member __.isAsExpression(node: Node): bool = jsNative
        member __.isNonNullExpression(node: Node): bool = jsNative
        member __.isMetaProperty(node: Node): bool = jsNative
        member __.isTemplateSpan(node: Node): bool = jsNative
        member __.isSemicolonClassElement(node: Node): bool = jsNative
        member __.isBlock(node: Node): bool = jsNative
        member __.isVariableStatement(node: Node): bool = jsNative
        member __.isEmptyStatement(node: Node): bool = jsNative
        member __.isExpressionStatement(node: Node): bool = jsNative
        member __.isIfStatement(node: Node): bool = jsNative
        member __.isDoStatement(node: Node): bool = jsNative
        member __.isWhileStatement(node: Node): bool = jsNative
        member __.isForStatement(node: Node): bool = jsNative
        member __.isForInStatement(node: Node): bool = jsNative
        member __.isForOfStatement(node: Node): bool = jsNative
        member __.isContinueStatement(node: Node): bool = jsNative
        member __.isBreakStatement(node: Node): bool = jsNative
        member __.isReturnStatement(node: Node): bool = jsNative
        member __.isWithStatement(node: Node): bool = jsNative
        member __.isSwitchStatement(node: Node): bool = jsNative
        member __.isLabeledStatement(node: Node): bool = jsNative
        member __.isThrowStatement(node: Node): bool = jsNative
        member __.isTryStatement(node: Node): bool = jsNative
        member __.isDebuggerStatement(node: Node): bool = jsNative
        member __.isVariableDeclaration(node: Node): bool = jsNative
        member __.isVariableDeclarationList(node: Node): bool = jsNative
        member __.isFunctionDeclaration(node: Node): bool = jsNative
        member __.isClassDeclaration(node: Node): bool = jsNative
        member __.isInterfaceDeclaration(node: Node): bool = jsNative
        member __.isTypeAliasDeclaration(node: Node): bool = jsNative
        member __.isEnumDeclaration(node: Node): bool = jsNative
        member __.isModuleDeclaration(node: Node): bool = jsNative
        member __.isModuleBlock(node: Node): bool = jsNative
        member __.isCaseBlock(node: Node): bool = jsNative
        member __.isNamespaceExportDeclaration(node: Node): bool = jsNative
        member __.isImportEqualsDeclaration(node: Node): bool = jsNative
        member __.isImportDeclaration(node: Node): bool = jsNative
        member __.isImportClause(node: Node): bool = jsNative
        member __.isNamespaceImport(node: Node): bool = jsNative
        member __.isNamedImports(node: Node): bool = jsNative
        member __.isImportSpecifier(node: Node): bool = jsNative
        member __.isExportAssignment(node: Node): bool = jsNative
        member __.isExportDeclaration(node: Node): bool = jsNative
        member __.isNamedExports(node: Node): bool = jsNative
        member __.isExportSpecifier(node: Node): bool = jsNative
        member __.isMissingDeclaration(node: Node): bool = jsNative
        member __.isExternalModuleReference(node: Node): bool = jsNative
        member __.isJsxElement(node: Node): bool = jsNative
        member __.isJsxSelfClosingElement(node: Node): bool = jsNative
        member __.isJsxOpeningElement(node: Node): bool = jsNative
        member __.isJsxClosingElement(node: Node): bool = jsNative
        member __.isJsxAttribute(node: Node): bool = jsNative
        member __.isJsxAttributes(node: Node): bool = jsNative
        member __.isJsxSpreadAttribute(node: Node): bool = jsNative
        member __.isJsxExpression(node: Node): bool = jsNative
        member __.isCaseClause(node: Node): bool = jsNative
        member __.isDefaultClause(node: Node): bool = jsNative
        member __.isHeritageClause(node: Node): bool = jsNative
        member __.isCatchClause(node: Node): bool = jsNative
        member __.isPropertyAssignment(node: Node): bool = jsNative
        member __.isShorthandPropertyAssignment(node: Node): bool = jsNative
        member __.isSpreadAssignment(node: Node): bool = jsNative
        member __.isEnumMember(node: Node): bool = jsNative
        member __.isSourceFile(node: Node): bool = jsNative
        member __.isBundle(node: Node): bool = jsNative
        member __.isJSDocTypeExpression(node: Node): bool = jsNative
        member __.isJSDocAllType(node: JSDocAllType): bool = jsNative
        member __.isJSDocUnknownType(node: Node): bool = jsNative
        member __.isJSDocNullableType(node: Node): bool = jsNative
        member __.isJSDocNonNullableType(node: Node): bool = jsNative
        member __.isJSDocOptionalType(node: Node): bool = jsNative
        member __.isJSDocFunctionType(node: Node): bool = jsNative
        member __.isJSDocVariadicType(node: Node): bool = jsNative
        member __.isJSDoc(node: Node): bool = jsNative
        member __.isJSDocAugmentsTag(node: Node): bool = jsNative
        member __.isJSDocParameterTag(node: Node): bool = jsNative
        member __.isJSDocReturnTag(node: Node): bool = jsNative
        member __.isJSDocTypeTag(node: Node): bool = jsNative
        member __.isJSDocTemplateTag(node: Node): bool = jsNative
        member __.isJSDocTypedefTag(node: Node): bool = jsNative
        member __.isJSDocPropertyTag(node: Node): bool = jsNative
        member __.isJSDocPropertyLikeTag(node: Node): bool = jsNative
        member __.isJSDocTypeLiteral(node: Node): bool = jsNative
        member __.isToken(n: Node): bool = jsNative
        member __.isLiteralExpression(node: Node): bool = jsNative
        member __.isTemplateMiddleOrTemplateTail(node: Node): bool = jsNative
        member __.isStringTextContainingNode(node: Node): bool = jsNative
        member __.isModifier(node: Node): bool = jsNative
        member __.isEntityName(node: Node): bool = jsNative
        member __.isPropertyName(node: Node): bool = jsNative
        member __.isBindingName(node: Node): bool = jsNative
        member __.isFunctionLike(node: Node): bool = jsNative
        member __.isClassElement(node: Node): bool = jsNative
        member __.isClassLike(node: Node): bool = jsNative
        member __.isAccessor(node: Node): bool = jsNative
        member __.isTypeElement(node: Node): bool = jsNative
        member __.isObjectLiteralElementLike(node: Node): bool = jsNative
        member __.isTypeNode(node: Node): bool = jsNative
        member __.isFunctionOrConstructorTypeNode(node: Node): bool = jsNative
        member __.isPropertyAccessOrQualifiedName(node: Node): bool = jsNative
        member __.isCallLikeExpression(node: Node): bool = jsNative
        member __.isCallOrNewExpression(node: Node): bool = jsNative
        member __.isTemplateLiteral(node: Node): bool = jsNative
        member __.isAssertionExpression(node: Node): bool = jsNative
        member __.isIterationStatement(node: Node, lookInLabeledStatements: bool): bool = jsNative
        member __.isJsxOpeningLikeElement(node: Node): bool = jsNative
        member __.isCaseOrDefaultClause(node: Node): bool = jsNative
        member __.isJSDocCommentContainingNode(node: Node): bool = jsNative
        member __.createNode(kind: SyntaxKind, ?pos: float, ?``end``: float): Node = jsNative
        member __.forEachChild<'T>(node: Node, cbNode: Func<Node, 'T option>, ?cbNodes: Func<NodeArray<Node>, 'T option>): 'T option = jsNative
        member __.createSourceFile(fileName: string, sourceText: string, languageVersion: ScriptTarget, ?setParentNodes: bool, ?scriptKind: ScriptKind): SourceFile = jsNative
        member __.parseIsolatedEntityName(text: string, languageVersion: ScriptTarget): EntityName = jsNative
        member __.parseJsonText(fileName: string, sourceText: string): JsonSourceFile = jsNative
        member __.isExternalModule(file: SourceFile): bool = jsNative
        member __.updateSourceFile(sourceFile: SourceFile, newText: string, textChangeRange: TextChangeRange, ?aggressiveChecks: bool): SourceFile = jsNative
        member __.getEffectiveTypeRoots(options: CompilerOptions, host: obj): ResizeArray<string> option = jsNative
        member __.resolveTypeReferenceDirective(typeReferenceDirectiveName: string, containingFile: string option, options: CompilerOptions, host: ModuleResolutionHost): ResolvedTypeReferenceDirectiveWithFailedLookupLocations = jsNative
        member __.getAutomaticTypeDirectiveNames(options: CompilerOptions, host: ModuleResolutionHost): ResizeArray<string> = jsNative
        member __.createModuleResolutionCache(currentDirectory: string, getCanonicalFileName: Func<string, string>): ModuleResolutionCache = jsNative
        member __.resolveModuleName(moduleName: string, containingFile: string, compilerOptions: CompilerOptions, host: ModuleResolutionHost, ?cache: ModuleResolutionCache): ResolvedModuleWithFailedLookupLocations = jsNative
        member __.nodeModuleNameResolver(moduleName: string, containingFile: string, compilerOptions: CompilerOptions, host: ModuleResolutionHost, ?cache: ModuleResolutionCache): ResolvedModuleWithFailedLookupLocations = jsNative
        member __.classicNameResolver(moduleName: string, containingFile: string, compilerOptions: CompilerOptions, host: ModuleResolutionHost, ?cache: NonRelativeModuleNameResolutionCache): ResolvedModuleWithFailedLookupLocations = jsNative
        member __.createNodeArray<'T>(?elements: ReadonlyArray<'T>, ?hasTrailingComma: bool): NodeArray<'T> = jsNative
        member __.createLiteral(value: string): StringLiteral = jsNative
        member __.createLiteral(value: float): NumericLiteral = jsNative
        member __.createLiteral(value: bool): BooleanLiteral = jsNative
        member __.createLiteral(sourceNode: U3<StringLiteral, NumericLiteral, Identifier>): StringLiteral = jsNative
        member __.createLiteral(value: U3<string, float, bool>): PrimaryExpression = jsNative
        member __.createNumericLiteral(value: string): NumericLiteral = jsNative
        member __.createIdentifier(text: string): Identifier = jsNative
        member __.updateIdentifier(node: Identifier, typeArguments: NodeArray<TypeNode> option): Identifier = jsNative
        member __.createTempVariable(recordTempVariable: obj option): Identifier = jsNative
        member __.createLoopVariable(): Identifier = jsNative
        member __.createUniqueName(text: string): Identifier = jsNative
        member __.getGeneratedNameForNode(node: Node): Identifier = jsNative
        member __.createToken<'TKind>(token: 'TKind): Token<'TKind> = jsNative
        member __.createSuper(): SuperExpression = jsNative
        member __.createThis(): obj = jsNative
        member __.createNull(): obj = jsNative
        member __.createTrue(): obj = jsNative
        member __.createFalse(): obj = jsNative
        member __.createQualifiedName(left: EntityName, right: U2<string, Identifier>): QualifiedName = jsNative
        member __.updateQualifiedName(node: QualifiedName, left: EntityName, right: Identifier): QualifiedName = jsNative
        member __.createComputedPropertyName(expression: Expression): ComputedPropertyName = jsNative
        member __.updateComputedPropertyName(node: ComputedPropertyName, expression: Expression): ComputedPropertyName = jsNative
        member __.createTypeParameterDeclaration(name: U2<string, Identifier>, ?``constraint``: TypeNode, ?defaultType: TypeNode): TypeParameterDeclaration = jsNative
        member __.updateTypeParameterDeclaration(node: TypeParameterDeclaration, name: Identifier, ``constraint``: TypeNode option, defaultType: TypeNode option): TypeParameterDeclaration = jsNative
        member __.createParameter(decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, dotDotDotToken: DotDotDotToken option, name: U2<string, BindingName>, ?questionToken: QuestionToken, ?``type``: TypeNode, ?initializer: Expression): ParameterDeclaration = jsNative
        member __.updateParameter(node: ParameterDeclaration, decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, dotDotDotToken: DotDotDotToken option, name: U2<string, BindingName>, questionToken: QuestionToken option, ``type``: TypeNode option, initializer: Expression option): ParameterDeclaration = jsNative
        member __.createDecorator(expression: Expression): Decorator = jsNative
        member __.updateDecorator(node: Decorator, expression: Expression): Decorator = jsNative
        member __.createPropertySignature(modifiers: ReadonlyArray<Modifier> option, name: U2<PropertyName, string>, questionToken: QuestionToken option, ``type``: TypeNode option, initializer: Expression option): PropertySignature = jsNative
        member __.updatePropertySignature(node: PropertySignature, modifiers: ReadonlyArray<Modifier> option, name: PropertyName, questionToken: QuestionToken option, ``type``: TypeNode option, initializer: Expression option): PropertySignature = jsNative
        member __.createProperty(decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, name: U2<string, PropertyName>, questionToken: QuestionToken option, ``type``: TypeNode option, initializer: Expression option): PropertyDeclaration = jsNative
        member __.updateProperty(node: PropertyDeclaration, decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, name: U2<string, PropertyName>, questionToken: QuestionToken option, ``type``: TypeNode option, initializer: Expression option): PropertyDeclaration = jsNative
        member __.createMethodSignature(typeParameters: ReadonlyArray<TypeParameterDeclaration> option, parameters: ReadonlyArray<ParameterDeclaration>, ``type``: TypeNode option, name: U2<string, PropertyName>, questionToken: QuestionToken option): MethodSignature = jsNative
        member __.updateMethodSignature(node: MethodSignature, typeParameters: NodeArray<TypeParameterDeclaration> option, parameters: NodeArray<ParameterDeclaration>, ``type``: TypeNode option, name: PropertyName, questionToken: QuestionToken option): MethodSignature = jsNative
        member __.createMethod(decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, asteriskToken: AsteriskToken option, name: U2<string, PropertyName>, questionToken: QuestionToken option, typeParameters: ReadonlyArray<TypeParameterDeclaration> option, parameters: ReadonlyArray<ParameterDeclaration>, ``type``: TypeNode option, body: Block option): MethodDeclaration = jsNative
        member __.updateMethod(node: MethodDeclaration, decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, asteriskToken: AsteriskToken option, name: PropertyName, questionToken: QuestionToken option, typeParameters: ReadonlyArray<TypeParameterDeclaration> option, parameters: ReadonlyArray<ParameterDeclaration>, ``type``: TypeNode option, body: Block option): MethodDeclaration = jsNative
        member __.createConstructor(decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, parameters: ReadonlyArray<ParameterDeclaration>, body: Block option): ConstructorDeclaration = jsNative
        member __.updateConstructor(node: ConstructorDeclaration, decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, parameters: ReadonlyArray<ParameterDeclaration>, body: Block option): ConstructorDeclaration = jsNative
        member __.createGetAccessor(decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, name: U2<string, PropertyName>, parameters: ReadonlyArray<ParameterDeclaration>, ``type``: TypeNode option, body: Block option): GetAccessorDeclaration = jsNative
        member __.updateGetAccessor(node: GetAccessorDeclaration, decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, name: PropertyName, parameters: ReadonlyArray<ParameterDeclaration>, ``type``: TypeNode option, body: Block option): GetAccessorDeclaration = jsNative
        member __.createSetAccessor(decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, name: U2<string, PropertyName>, parameters: ReadonlyArray<ParameterDeclaration>, body: Block option): SetAccessorDeclaration = jsNative
        member __.updateSetAccessor(node: SetAccessorDeclaration, decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, name: PropertyName, parameters: ReadonlyArray<ParameterDeclaration>, body: Block option): SetAccessorDeclaration = jsNative
        member __.createCallSignature(typeParameters: ResizeArray<TypeParameterDeclaration> option, parameters: ResizeArray<ParameterDeclaration>, ``type``: TypeNode option): CallSignatureDeclaration = jsNative
        member __.updateCallSignature(node: CallSignatureDeclaration, typeParameters: NodeArray<TypeParameterDeclaration> option, parameters: NodeArray<ParameterDeclaration>, ``type``: TypeNode option): CallSignatureDeclaration = jsNative
        member __.createConstructSignature(typeParameters: ResizeArray<TypeParameterDeclaration> option, parameters: ResizeArray<ParameterDeclaration>, ``type``: TypeNode option): ConstructSignatureDeclaration = jsNative
        member __.updateConstructSignature(node: ConstructSignatureDeclaration, typeParameters: NodeArray<TypeParameterDeclaration> option, parameters: NodeArray<ParameterDeclaration>, ``type``: TypeNode option): ConstructSignatureDeclaration = jsNative
        member __.createIndexSignature(decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, parameters: ReadonlyArray<ParameterDeclaration>, ``type``: TypeNode): IndexSignatureDeclaration = jsNative
        member __.updateIndexSignature(node: IndexSignatureDeclaration, decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, parameters: ReadonlyArray<ParameterDeclaration>, ``type``: TypeNode): IndexSignatureDeclaration = jsNative
        member __.createKeywordTypeNode(kind: obj): KeywordTypeNode = jsNative
        member __.createTypePredicateNode(parameterName: U3<Identifier, ThisTypeNode, string>, ``type``: TypeNode): TypePredicateNode = jsNative
        member __.updateTypePredicateNode(node: TypePredicateNode, parameterName: U2<Identifier, ThisTypeNode>, ``type``: TypeNode): TypePredicateNode = jsNative
        member __.createTypeReferenceNode(typeName: U2<string, EntityName>, typeArguments: ReadonlyArray<TypeNode> option): TypeReferenceNode = jsNative
        member __.updateTypeReferenceNode(node: TypeReferenceNode, typeName: EntityName, typeArguments: NodeArray<TypeNode> option): TypeReferenceNode = jsNative
        member __.createFunctionTypeNode(typeParameters: ResizeArray<TypeParameterDeclaration> option, parameters: ResizeArray<ParameterDeclaration>, ``type``: TypeNode option): FunctionTypeNode = jsNative
        member __.updateFunctionTypeNode(node: FunctionTypeNode, typeParameters: NodeArray<TypeParameterDeclaration> option, parameters: NodeArray<ParameterDeclaration>, ``type``: TypeNode option): FunctionTypeNode = jsNative
        member __.createConstructorTypeNode(typeParameters: ResizeArray<TypeParameterDeclaration> option, parameters: ResizeArray<ParameterDeclaration>, ``type``: TypeNode option): ConstructorTypeNode = jsNative
        member __.updateConstructorTypeNode(node: ConstructorTypeNode, typeParameters: NodeArray<TypeParameterDeclaration> option, parameters: NodeArray<ParameterDeclaration>, ``type``: TypeNode option): ConstructorTypeNode = jsNative
        member __.createTypeQueryNode(exprName: EntityName): TypeQueryNode = jsNative
        member __.updateTypeQueryNode(node: TypeQueryNode, exprName: EntityName): TypeQueryNode = jsNative
        member __.createTypeLiteralNode(members: ReadonlyArray<TypeElement>): TypeLiteralNode = jsNative
        member __.updateTypeLiteralNode(node: TypeLiteralNode, members: NodeArray<TypeElement>): TypeLiteralNode = jsNative
        member __.createArrayTypeNode(elementType: TypeNode): ArrayTypeNode = jsNative
        member __.updateArrayTypeNode(node: ArrayTypeNode, elementType: TypeNode): ArrayTypeNode = jsNative
        member __.createTupleTypeNode(elementTypes: ReadonlyArray<TypeNode>): TupleTypeNode = jsNative
        member __.updateTypleTypeNode(node: TupleTypeNode, elementTypes: ReadonlyArray<TypeNode>): TupleTypeNode = jsNative
        member __.createUnionTypeNode(types: ResizeArray<TypeNode>): UnionTypeNode = jsNative
        member __.updateUnionTypeNode(node: UnionTypeNode, types: NodeArray<TypeNode>): UnionTypeNode = jsNative
        member __.createIntersectionTypeNode(types: ResizeArray<TypeNode>): IntersectionTypeNode = jsNative
        member __.updateIntersectionTypeNode(node: IntersectionTypeNode, types: NodeArray<TypeNode>): IntersectionTypeNode = jsNative
        member __.createUnionOrIntersectionTypeNode(kind: U2<SyntaxKind, SyntaxKind>, types: ReadonlyArray<TypeNode>): UnionOrIntersectionTypeNode = jsNative
        member __.createParenthesizedType(``type``: TypeNode): ParenthesizedTypeNode = jsNative
        member __.updateParenthesizedType(node: ParenthesizedTypeNode, ``type``: TypeNode): ParenthesizedTypeNode = jsNative
        member __.createThisTypeNode(): ThisTypeNode = jsNative
        member __.createTypeOperatorNode(``type``: TypeNode): TypeOperatorNode = jsNative
        member __.updateTypeOperatorNode(node: TypeOperatorNode, ``type``: TypeNode): TypeOperatorNode = jsNative
        member __.createIndexedAccessTypeNode(objectType: TypeNode, indexType: TypeNode): IndexedAccessTypeNode = jsNative
        member __.updateIndexedAccessTypeNode(node: IndexedAccessTypeNode, objectType: TypeNode, indexType: TypeNode): IndexedAccessTypeNode = jsNative
        member __.createMappedTypeNode(readonlyToken: ReadonlyToken option, typeParameter: TypeParameterDeclaration, questionToken: QuestionToken option, ``type``: TypeNode option): MappedTypeNode = jsNative
        member __.updateMappedTypeNode(node: MappedTypeNode, readonlyToken: ReadonlyToken option, typeParameter: TypeParameterDeclaration, questionToken: QuestionToken option, ``type``: TypeNode option): MappedTypeNode = jsNative
        member __.createLiteralTypeNode(literal: Expression): LiteralTypeNode = jsNative
        member __.updateLiteralTypeNode(node: LiteralTypeNode, literal: Expression): LiteralTypeNode = jsNative
        member __.createObjectBindingPattern(elements: ReadonlyArray<BindingElement>): ObjectBindingPattern = jsNative
        member __.updateObjectBindingPattern(node: ObjectBindingPattern, elements: ReadonlyArray<BindingElement>): ObjectBindingPattern = jsNative
        member __.createArrayBindingPattern(elements: ReadonlyArray<ArrayBindingElement>): ArrayBindingPattern = jsNative
        member __.updateArrayBindingPattern(node: ArrayBindingPattern, elements: ReadonlyArray<ArrayBindingElement>): ArrayBindingPattern = jsNative
        member __.createBindingElement(dotDotDotToken: DotDotDotToken option, propertyName: U2<string, PropertyName> option, name: U2<string, BindingName>, ?initializer: Expression): BindingElement = jsNative
        member __.updateBindingElement(node: BindingElement, dotDotDotToken: DotDotDotToken option, propertyName: PropertyName option, name: BindingName, initializer: Expression option): BindingElement = jsNative
        member __.createArrayLiteral(?elements: ReadonlyArray<Expression>, ?multiLine: bool): ArrayLiteralExpression = jsNative
        member __.updateArrayLiteral(node: ArrayLiteralExpression, elements: ReadonlyArray<Expression>): ArrayLiteralExpression = jsNative
        member __.createObjectLiteral(?properties: ReadonlyArray<ObjectLiteralElementLike>, ?multiLine: bool): ObjectLiteralExpression = jsNative
        member __.updateObjectLiteral(node: ObjectLiteralExpression, properties: ReadonlyArray<ObjectLiteralElementLike>): ObjectLiteralExpression = jsNative
        member __.createPropertyAccess(expression: Expression, name: U2<string, Identifier>): PropertyAccessExpression = jsNative
        member __.updatePropertyAccess(node: PropertyAccessExpression, expression: Expression, name: Identifier): PropertyAccessExpression = jsNative
        member __.createElementAccess(expression: Expression, index: U2<float, Expression>): ElementAccessExpression = jsNative
        member __.updateElementAccess(node: ElementAccessExpression, expression: Expression, argumentExpression: Expression): ElementAccessExpression = jsNative
        member __.createCall(expression: Expression, typeArguments: ReadonlyArray<TypeNode> option, argumentsArray: ReadonlyArray<Expression>): CallExpression = jsNative
        member __.updateCall(node: CallExpression, expression: Expression, typeArguments: ReadonlyArray<TypeNode> option, argumentsArray: ReadonlyArray<Expression>): CallExpression = jsNative
        member __.createNew(expression: Expression, typeArguments: ReadonlyArray<TypeNode> option, argumentsArray: ReadonlyArray<Expression> option): NewExpression = jsNative
        member __.updateNew(node: NewExpression, expression: Expression, typeArguments: ReadonlyArray<TypeNode> option, argumentsArray: ReadonlyArray<Expression> option): NewExpression = jsNative
        member __.createTaggedTemplate(tag: Expression, template: TemplateLiteral): TaggedTemplateExpression = jsNative
        member __.updateTaggedTemplate(node: TaggedTemplateExpression, tag: Expression, template: TemplateLiteral): TaggedTemplateExpression = jsNative
        member __.createTypeAssertion(``type``: TypeNode, expression: Expression): TypeAssertion = jsNative
        member __.updateTypeAssertion(node: TypeAssertion, ``type``: TypeNode, expression: Expression): TypeAssertion = jsNative
        member __.createParen(expression: Expression): ParenthesizedExpression = jsNative
        member __.updateParen(node: ParenthesizedExpression, expression: Expression): ParenthesizedExpression = jsNative
        member __.createFunctionExpression(modifiers: ReadonlyArray<Modifier> option, asteriskToken: AsteriskToken option, name: U2<string, Identifier> option, typeParameters: ReadonlyArray<TypeParameterDeclaration> option, parameters: ReadonlyArray<ParameterDeclaration>, ``type``: TypeNode option, body: Block): FunctionExpression = jsNative
        member __.updateFunctionExpression(node: FunctionExpression, modifiers: ReadonlyArray<Modifier> option, asteriskToken: AsteriskToken option, name: Identifier option, typeParameters: ReadonlyArray<TypeParameterDeclaration> option, parameters: ReadonlyArray<ParameterDeclaration>, ``type``: TypeNode option, body: Block): FunctionExpression = jsNative
        member __.createArrowFunction(modifiers: ReadonlyArray<Modifier> option, typeParameters: ReadonlyArray<TypeParameterDeclaration> option, parameters: ReadonlyArray<ParameterDeclaration>, ``type``: TypeNode option, equalsGreaterThanToken: EqualsGreaterThanToken option, body: ConciseBody): ArrowFunction = jsNative
        member __.updateArrowFunction(node: ArrowFunction, modifiers: ReadonlyArray<Modifier> option, typeParameters: ReadonlyArray<TypeParameterDeclaration> option, parameters: ReadonlyArray<ParameterDeclaration>, ``type``: TypeNode option, body: ConciseBody): ArrowFunction = jsNative
        member __.updateArrowFunction(node: ArrowFunction, modifiers: ReadonlyArray<Modifier> option, typeParameters: ReadonlyArray<TypeParameterDeclaration> option, parameters: ReadonlyArray<ParameterDeclaration>, ``type``: TypeNode option, equalsGreaterThanToken: Token<SyntaxKind>, body: ConciseBody): ArrowFunction = jsNative
        member __.createDelete(expression: Expression): DeleteExpression = jsNative
        member __.updateDelete(node: DeleteExpression, expression: Expression): DeleteExpression = jsNative
        member __.createTypeOf(expression: Expression): TypeOfExpression = jsNative
        member __.updateTypeOf(node: TypeOfExpression, expression: Expression): TypeOfExpression = jsNative
        member __.createVoid(expression: Expression): VoidExpression = jsNative
        member __.updateVoid(node: VoidExpression, expression: Expression): VoidExpression = jsNative
        member __.createAwait(expression: Expression): AwaitExpression = jsNative
        member __.updateAwait(node: AwaitExpression, expression: Expression): AwaitExpression = jsNative
        member __.createPrefix(operator: PrefixUnaryOperator, operand: Expression): PrefixUnaryExpression = jsNative
        member __.updatePrefix(node: PrefixUnaryExpression, operand: Expression): PrefixUnaryExpression = jsNative
        member __.createPostfix(operand: Expression, operator: PostfixUnaryOperator): PostfixUnaryExpression = jsNative
        member __.updatePostfix(node: PostfixUnaryExpression, operand: Expression): PostfixUnaryExpression = jsNative
        member __.createBinary(left: Expression, operator: U2<BinaryOperator, BinaryOperatorToken>, right: Expression): BinaryExpression = jsNative
        member __.updateBinary(node: BinaryExpression, left: Expression, right: Expression, ?operator: U2<BinaryOperator, BinaryOperatorToken>): BinaryExpression = jsNative
        member __.createConditional(condition: Expression, whenTrue: Expression, whenFalse: Expression): ConditionalExpression = jsNative
        member __.createConditional(condition: Expression, questionToken: QuestionToken, whenTrue: Expression, colonToken: ColonToken, whenFalse: Expression): ConditionalExpression = jsNative
        member __.updateConditional(node: ConditionalExpression, condition: Expression, whenTrue: Expression, whenFalse: Expression): ConditionalExpression = jsNative
        member __.updateConditional(node: ConditionalExpression, condition: Expression, questionToken: Token<SyntaxKind>, whenTrue: Expression, colonToken: Token<SyntaxKind>, whenFalse: Expression): ConditionalExpression = jsNative
        member __.createTemplateExpression(head: TemplateHead, templateSpans: ReadonlyArray<TemplateSpan>): TemplateExpression = jsNative
        member __.updateTemplateExpression(node: TemplateExpression, head: TemplateHead, templateSpans: ReadonlyArray<TemplateSpan>): TemplateExpression = jsNative
        member __.createYield(?expression: Expression): YieldExpression = jsNative
        member __.createYield(asteriskToken: AsteriskToken, expression: Expression): YieldExpression = jsNative
        member __.updateYield(node: YieldExpression, asteriskToken: AsteriskToken option, expression: Expression): YieldExpression = jsNative
        member __.createSpread(expression: Expression): SpreadElement = jsNative
        member __.updateSpread(node: SpreadElement, expression: Expression): SpreadElement = jsNative
        member __.createClassExpression(modifiers: ReadonlyArray<Modifier> option, name: U2<string, Identifier> option, typeParameters: ReadonlyArray<TypeParameterDeclaration> option, heritageClauses: ReadonlyArray<HeritageClause>, members: ReadonlyArray<ClassElement>): ClassExpression = jsNative
        member __.updateClassExpression(node: ClassExpression, modifiers: ReadonlyArray<Modifier> option, name: Identifier option, typeParameters: ReadonlyArray<TypeParameterDeclaration> option, heritageClauses: ReadonlyArray<HeritageClause>, members: ReadonlyArray<ClassElement>): ClassExpression = jsNative
        member __.createOmittedExpression(): OmittedExpression = jsNative
        member __.createExpressionWithTypeArguments(typeArguments: ReadonlyArray<TypeNode>, expression: Expression): ExpressionWithTypeArguments = jsNative
        member __.updateExpressionWithTypeArguments(node: ExpressionWithTypeArguments, typeArguments: ReadonlyArray<TypeNode>, expression: Expression): ExpressionWithTypeArguments = jsNative
        member __.createAsExpression(expression: Expression, ``type``: TypeNode): AsExpression = jsNative
        member __.updateAsExpression(node: AsExpression, expression: Expression, ``type``: TypeNode): AsExpression = jsNative
        member __.createNonNullExpression(expression: Expression): NonNullExpression = jsNative
        member __.updateNonNullExpression(node: NonNullExpression, expression: Expression): NonNullExpression = jsNative
        member __.createMetaProperty(keywordToken: obj, name: Identifier): MetaProperty = jsNative
        member __.updateMetaProperty(node: MetaProperty, name: Identifier): MetaProperty = jsNative
        member __.createTemplateSpan(expression: Expression, literal: U2<TemplateMiddle, TemplateTail>): TemplateSpan = jsNative
        member __.updateTemplateSpan(node: TemplateSpan, expression: Expression, literal: U2<TemplateMiddle, TemplateTail>): TemplateSpan = jsNative
        member __.createSemicolonClassElement(): SemicolonClassElement = jsNative
        member __.createBlock(statements: ReadonlyArray<Statement>, ?multiLine: bool): Block = jsNative
        member __.updateBlock(node: Block, statements: ReadonlyArray<Statement>): Block = jsNative
        member __.createVariableStatement(modifiers: ReadonlyArray<Modifier> option, declarationList: U2<VariableDeclarationList, ReadonlyArray<VariableDeclaration>>): VariableStatement = jsNative
        member __.updateVariableStatement(node: VariableStatement, modifiers: ReadonlyArray<Modifier> option, declarationList: VariableDeclarationList): VariableStatement = jsNative
        member __.createEmptyStatement(): EmptyStatement = jsNative
        member __.createStatement(expression: Expression): ExpressionStatement = jsNative
        member __.updateStatement(node: ExpressionStatement, expression: Expression): ExpressionStatement = jsNative
        member __.createIf(expression: Expression, thenStatement: Statement, ?elseStatement: Statement): IfStatement = jsNative
        member __.updateIf(node: IfStatement, expression: Expression, thenStatement: Statement, elseStatement: Statement option): IfStatement = jsNative
        member __.createDo(statement: Statement, expression: Expression): DoStatement = jsNative
        member __.updateDo(node: DoStatement, statement: Statement, expression: Expression): DoStatement = jsNative
        member __.createWhile(expression: Expression, statement: Statement): WhileStatement = jsNative
        member __.updateWhile(node: WhileStatement, expression: Expression, statement: Statement): WhileStatement = jsNative
        member __.createFor(initializer: ForInitializer option, condition: Expression option, incrementor: Expression option, statement: Statement): ForStatement = jsNative
        member __.updateFor(node: ForStatement, initializer: ForInitializer option, condition: Expression option, incrementor: Expression option, statement: Statement): ForStatement = jsNative
        member __.createForIn(initializer: ForInitializer, expression: Expression, statement: Statement): ForInStatement = jsNative
        member __.updateForIn(node: ForInStatement, initializer: ForInitializer, expression: Expression, statement: Statement): ForInStatement = jsNative
        member __.createForOf(awaitModifier: AwaitKeywordToken, initializer: ForInitializer, expression: Expression, statement: Statement): ForOfStatement = jsNative
        member __.updateForOf(node: ForOfStatement, awaitModifier: AwaitKeywordToken, initializer: ForInitializer, expression: Expression, statement: Statement): ForOfStatement = jsNative
        member __.createContinue(?label: U2<string, Identifier>): ContinueStatement = jsNative
        member __.updateContinue(node: ContinueStatement, label: Identifier option): ContinueStatement = jsNative
        member __.createBreak(?label: U2<string, Identifier>): BreakStatement = jsNative
        member __.updateBreak(node: BreakStatement, label: Identifier option): BreakStatement = jsNative
        member __.createReturn(?expression: Expression): ReturnStatement = jsNative
        member __.updateReturn(node: ReturnStatement, expression: Expression option): ReturnStatement = jsNative
        member __.createWith(expression: Expression, statement: Statement): WithStatement = jsNative
        member __.updateWith(node: WithStatement, expression: Expression, statement: Statement): WithStatement = jsNative
        member __.createSwitch(expression: Expression, caseBlock: CaseBlock): SwitchStatement = jsNative
        member __.updateSwitch(node: SwitchStatement, expression: Expression, caseBlock: CaseBlock): SwitchStatement = jsNative
        member __.createLabel(label: U2<string, Identifier>, statement: Statement): LabeledStatement = jsNative
        member __.updateLabel(node: LabeledStatement, label: Identifier, statement: Statement): LabeledStatement = jsNative
        member __.createThrow(expression: Expression): ThrowStatement = jsNative
        member __.updateThrow(node: ThrowStatement, expression: Expression): ThrowStatement = jsNative
        member __.createTry(tryBlock: Block, catchClause: CatchClause option, finallyBlock: Block option): TryStatement = jsNative
        member __.updateTry(node: TryStatement, tryBlock: Block, catchClause: CatchClause option, finallyBlock: Block option): TryStatement = jsNative
        member __.createDebuggerStatement(): DebuggerStatement = jsNative
        member __.createVariableDeclaration(name: U2<string, BindingName>, ?``type``: TypeNode, ?initializer: Expression): VariableDeclaration = jsNative
        member __.updateVariableDeclaration(node: VariableDeclaration, name: BindingName, ``type``: TypeNode option, initializer: Expression option): VariableDeclaration = jsNative
        member __.createVariableDeclarationList(declarations: ReadonlyArray<VariableDeclaration>, ?flags: NodeFlags): VariableDeclarationList = jsNative
        member __.updateVariableDeclarationList(node: VariableDeclarationList, declarations: ReadonlyArray<VariableDeclaration>): VariableDeclarationList = jsNative
        member __.createFunctionDeclaration(decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, asteriskToken: AsteriskToken option, name: U2<string, Identifier> option, typeParameters: ReadonlyArray<TypeParameterDeclaration> option, parameters: ReadonlyArray<ParameterDeclaration>, ``type``: TypeNode option, body: Block option): FunctionDeclaration = jsNative
        member __.updateFunctionDeclaration(node: FunctionDeclaration, decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, asteriskToken: AsteriskToken option, name: Identifier option, typeParameters: ReadonlyArray<TypeParameterDeclaration> option, parameters: ReadonlyArray<ParameterDeclaration>, ``type``: TypeNode option, body: Block option): FunctionDeclaration = jsNative
        member __.createClassDeclaration(decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, name: U2<string, Identifier> option, typeParameters: ReadonlyArray<TypeParameterDeclaration> option, heritageClauses: ReadonlyArray<HeritageClause>, members: ReadonlyArray<ClassElement>): ClassDeclaration = jsNative
        member __.updateClassDeclaration(node: ClassDeclaration, decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, name: Identifier option, typeParameters: ReadonlyArray<TypeParameterDeclaration> option, heritageClauses: ReadonlyArray<HeritageClause>, members: ReadonlyArray<ClassElement>): ClassDeclaration = jsNative
        member __.createInterfaceDeclaration(decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, name: U2<string, Identifier>, typeParameters: ReadonlyArray<TypeParameterDeclaration> option, heritageClauses: ReadonlyArray<HeritageClause> option, members: ReadonlyArray<TypeElement>): InterfaceDeclaration = jsNative
        member __.updateInterfaceDeclaration(node: InterfaceDeclaration, decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, name: Identifier, typeParameters: ReadonlyArray<TypeParameterDeclaration> option, heritageClauses: ReadonlyArray<HeritageClause> option, members: ReadonlyArray<TypeElement>): InterfaceDeclaration = jsNative
        member __.createTypeAliasDeclaration(decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, name: U2<string, Identifier>, typeParameters: ReadonlyArray<TypeParameterDeclaration> option, ``type``: TypeNode): TypeAliasDeclaration = jsNative
        member __.updateTypeAliasDeclaration(node: TypeAliasDeclaration, decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, name: Identifier, typeParameters: ReadonlyArray<TypeParameterDeclaration> option, ``type``: TypeNode): TypeAliasDeclaration = jsNative
        member __.createEnumDeclaration(decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, name: U2<string, Identifier>, members: ReadonlyArray<EnumMember>): EnumDeclaration = jsNative
        member __.updateEnumDeclaration(node: EnumDeclaration, decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, name: Identifier, members: ReadonlyArray<EnumMember>): EnumDeclaration = jsNative
        member __.createModuleDeclaration(decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, name: ModuleName, body: ModuleBody option, ?flags: NodeFlags): ModuleDeclaration = jsNative
        member __.updateModuleDeclaration(node: ModuleDeclaration, decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, name: ModuleName, body: ModuleBody option): ModuleDeclaration = jsNative
        member __.createModuleBlock(statements: ReadonlyArray<Statement>): ModuleBlock = jsNative
        member __.updateModuleBlock(node: ModuleBlock, statements: ReadonlyArray<Statement>): ModuleBlock = jsNative
        member __.createCaseBlock(clauses: ReadonlyArray<CaseOrDefaultClause>): CaseBlock = jsNative
        member __.updateCaseBlock(node: CaseBlock, clauses: ReadonlyArray<CaseOrDefaultClause>): CaseBlock = jsNative
        member __.createNamespaceExportDeclaration(name: U2<string, Identifier>): NamespaceExportDeclaration = jsNative
        member __.updateNamespaceExportDeclaration(node: NamespaceExportDeclaration, name: Identifier): NamespaceExportDeclaration = jsNative
        member __.createImportEqualsDeclaration(decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, name: U2<string, Identifier>, moduleReference: ModuleReference): ImportEqualsDeclaration = jsNative
        member __.updateImportEqualsDeclaration(node: ImportEqualsDeclaration, decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, name: Identifier, moduleReference: ModuleReference): ImportEqualsDeclaration = jsNative
        member __.createImportDeclaration(decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, importClause: ImportClause option, ?moduleSpecifier: Expression): ImportDeclaration = jsNative
        member __.updateImportDeclaration(node: ImportDeclaration, decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, importClause: ImportClause option, moduleSpecifier: Expression option): ImportDeclaration = jsNative
        member __.createImportClause(name: Identifier option, namedBindings: NamedImportBindings option): ImportClause = jsNative
        member __.updateImportClause(node: ImportClause, name: Identifier option, namedBindings: NamedImportBindings option): ImportClause = jsNative
        member __.createNamespaceImport(name: Identifier): NamespaceImport = jsNative
        member __.updateNamespaceImport(node: NamespaceImport, name: Identifier): NamespaceImport = jsNative
        member __.createNamedImports(elements: ReadonlyArray<ImportSpecifier>): NamedImports = jsNative
        member __.updateNamedImports(node: NamedImports, elements: ReadonlyArray<ImportSpecifier>): NamedImports = jsNative
        member __.createImportSpecifier(propertyName: Identifier option, name: Identifier): ImportSpecifier = jsNative
        member __.updateImportSpecifier(node: ImportSpecifier, propertyName: Identifier option, name: Identifier): ImportSpecifier = jsNative
        member __.createExportAssignment(decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, isExportEquals: bool, expression: Expression): ExportAssignment = jsNative
        member __.updateExportAssignment(node: ExportAssignment, decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, expression: Expression): ExportAssignment = jsNative
        member __.createExportDeclaration(decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, exportClause: NamedExports option, ?moduleSpecifier: Expression): ExportDeclaration = jsNative
        member __.updateExportDeclaration(node: ExportDeclaration, decorators: ReadonlyArray<Decorator> option, modifiers: ReadonlyArray<Modifier> option, exportClause: NamedExports option, moduleSpecifier: Expression option): ExportDeclaration = jsNative
        member __.createNamedExports(elements: ReadonlyArray<ExportSpecifier>): NamedExports = jsNative
        member __.updateNamedExports(node: NamedExports, elements: ReadonlyArray<ExportSpecifier>): NamedExports = jsNative
        member __.createExportSpecifier(propertyName: U2<string, Identifier> option, name: U2<string, Identifier>): ExportSpecifier = jsNative
        member __.updateExportSpecifier(node: ExportSpecifier, propertyName: Identifier option, name: Identifier): ExportSpecifier = jsNative
        member __.createExternalModuleReference(expression: Expression): ExternalModuleReference = jsNative
        member __.updateExternalModuleReference(node: ExternalModuleReference, expression: Expression): ExternalModuleReference = jsNative
        member __.createJsxElement(openingElement: JsxOpeningElement, children: ReadonlyArray<JsxChild>, closingElement: JsxClosingElement): JsxElement = jsNative
        member __.updateJsxElement(node: JsxElement, openingElement: JsxOpeningElement, children: ReadonlyArray<JsxChild>, closingElement: JsxClosingElement): JsxElement = jsNative
        member __.createJsxSelfClosingElement(tagName: JsxTagNameExpression, attributes: JsxAttributes): JsxSelfClosingElement = jsNative
        member __.updateJsxSelfClosingElement(node: JsxSelfClosingElement, tagName: JsxTagNameExpression, attributes: JsxAttributes): JsxSelfClosingElement = jsNative
        member __.createJsxOpeningElement(tagName: JsxTagNameExpression, attributes: JsxAttributes): JsxOpeningElement = jsNative
        member __.updateJsxOpeningElement(node: JsxOpeningElement, tagName: JsxTagNameExpression, attributes: JsxAttributes): JsxOpeningElement = jsNative
        member __.createJsxClosingElement(tagName: JsxTagNameExpression): JsxClosingElement = jsNative
        member __.updateJsxClosingElement(node: JsxClosingElement, tagName: JsxTagNameExpression): JsxClosingElement = jsNative
        member __.createJsxAttribute(name: Identifier, initializer: U2<StringLiteral, JsxExpression>): JsxAttribute = jsNative
        member __.updateJsxAttribute(node: JsxAttribute, name: Identifier, initializer: U2<StringLiteral, JsxExpression>): JsxAttribute = jsNative
        member __.createJsxAttributes(properties: ReadonlyArray<JsxAttributeLike>): JsxAttributes = jsNative
        member __.updateJsxAttributes(node: JsxAttributes, properties: ReadonlyArray<JsxAttributeLike>): JsxAttributes = jsNative
        member __.createJsxSpreadAttribute(expression: Expression): JsxSpreadAttribute = jsNative
        member __.updateJsxSpreadAttribute(node: JsxSpreadAttribute, expression: Expression): JsxSpreadAttribute = jsNative
        member __.createJsxExpression(dotDotDotToken: DotDotDotToken option, expression: Expression option): JsxExpression = jsNative
        member __.updateJsxExpression(node: JsxExpression, expression: Expression option): JsxExpression = jsNative
        member __.createCaseClause(expression: Expression, statements: ReadonlyArray<Statement>): CaseClause = jsNative
        member __.updateCaseClause(node: CaseClause, expression: Expression, statements: ReadonlyArray<Statement>): CaseClause = jsNative
        member __.createDefaultClause(statements: ReadonlyArray<Statement>): DefaultClause = jsNative
        member __.updateDefaultClause(node: DefaultClause, statements: ReadonlyArray<Statement>): DefaultClause = jsNative
        member __.createHeritageClause(token: obj, types: ReadonlyArray<ExpressionWithTypeArguments>): HeritageClause = jsNative
        member __.updateHeritageClause(node: HeritageClause, types: ReadonlyArray<ExpressionWithTypeArguments>): HeritageClause = jsNative
        member __.createCatchClause(variableDeclaration: U2<string, VariableDeclaration> option, block: Block): CatchClause = jsNative
        member __.updateCatchClause(node: CatchClause, variableDeclaration: VariableDeclaration option, block: Block): CatchClause = jsNative
        member __.createPropertyAssignment(name: U2<string, PropertyName>, initializer: Expression): PropertyAssignment = jsNative
        member __.updatePropertyAssignment(node: PropertyAssignment, name: PropertyName, initializer: Expression): PropertyAssignment = jsNative
        member __.createShorthandPropertyAssignment(name: U2<string, Identifier>, ?objectAssignmentInitializer: Expression): ShorthandPropertyAssignment = jsNative
        member __.updateShorthandPropertyAssignment(node: ShorthandPropertyAssignment, name: Identifier, objectAssignmentInitializer: Expression option): ShorthandPropertyAssignment = jsNative
        member __.createSpreadAssignment(expression: Expression): SpreadAssignment = jsNative
        member __.updateSpreadAssignment(node: SpreadAssignment, expression: Expression): SpreadAssignment = jsNative
        member __.createEnumMember(name: U2<string, PropertyName>, ?initializer: Expression): EnumMember = jsNative
        member __.updateEnumMember(node: EnumMember, name: PropertyName, initializer: Expression option): EnumMember = jsNative
        member __.updateSourceFileNode(node: SourceFile, statements: ReadonlyArray<Statement>): SourceFile = jsNative
        member __.getMutableClone<'T>(node: 'T): 'T = jsNative
        member __.createNotEmittedStatement(original: Node): NotEmittedStatement = jsNative
        member __.createPartiallyEmittedExpression(expression: Expression, ?original: Node): PartiallyEmittedExpression = jsNative
        member __.updatePartiallyEmittedExpression(node: PartiallyEmittedExpression, expression: Expression): PartiallyEmittedExpression = jsNative
        member __.createCommaList(elements: ReadonlyArray<Expression>): CommaListExpression = jsNative
        member __.updateCommaList(node: CommaListExpression, elements: ReadonlyArray<Expression>): CommaListExpression = jsNative
        member __.createBundle(sourceFiles: ResizeArray<SourceFile>): Bundle = jsNative
        member __.updateBundle(node: Bundle, sourceFiles: ResizeArray<SourceFile>): Bundle = jsNative
        member __.createImmediatelyInvokedFunctionExpression(statements: ResizeArray<Statement>): CallExpression = jsNative
        member __.createImmediatelyInvokedFunctionExpression(statements: ResizeArray<Statement>, param: ParameterDeclaration, paramValue: Expression): CallExpression = jsNative
        member __.createImmediatelyInvokedArrowFunction(statements: ResizeArray<Statement>): CallExpression = jsNative
        member __.createImmediatelyInvokedArrowFunction(statements: ResizeArray<Statement>, param: ParameterDeclaration, paramValue: Expression): CallExpression = jsNative
        member __.createComma(left: Expression, right: Expression): Expression = jsNative
        member __.createLessThan(left: Expression, right: Expression): Expression = jsNative
        member __.createAssignment(left: U2<ObjectLiteralExpression, ArrayLiteralExpression>, right: Expression): DestructuringAssignment = jsNative
        member __.createAssignment(left: Expression, right: Expression): BinaryExpression = jsNative
        member __.createStrictEquality(left: Expression, right: Expression): BinaryExpression = jsNative
        member __.createStrictInequality(left: Expression, right: Expression): BinaryExpression = jsNative
        member __.createAdd(left: Expression, right: Expression): BinaryExpression = jsNative
        member __.createSubtract(left: Expression, right: Expression): BinaryExpression = jsNative
        member __.createPostfixIncrement(operand: Expression): PostfixUnaryExpression = jsNative
        member __.createLogicalAnd(left: Expression, right: Expression): BinaryExpression = jsNative
        member __.createLogicalOr(left: Expression, right: Expression): BinaryExpression = jsNative
        member __.createLogicalNot(operand: Expression): PrefixUnaryExpression = jsNative
        member __.createVoidZero(): VoidExpression = jsNative
        member __.createExportDefault(expression: Expression): ExportAssignment = jsNative
        member __.createExternalModuleExport(exportName: Identifier): ExportDeclaration = jsNative
        member __.disposeEmitNodes(sourceFile: SourceFile): unit = jsNative
        member __.setTextRange<'T>(range: 'T, location: TextRange option): 'T = jsNative
        member __.setEmitFlags<'T>(node: 'T, emitFlags: EmitFlags): 'T = jsNative
        member __.getSourceMapRange(node: Node): SourceMapRange = jsNative
        member __.setSourceMapRange<'T>(node: 'T, range: SourceMapRange option): 'T = jsNative
        member __.createSourceMapSource(fileName: string, text: string, ?skipTrivia: Func<float, float>): SourceMapSource = jsNative
        member __.getTokenSourceMapRange(node: Node, token: SyntaxKind): SourceMapRange option = jsNative
        member __.setTokenSourceMapRange<'T>(node: 'T, token: SyntaxKind, range: SourceMapRange option): 'T = jsNative
        member __.getCommentRange(node: Node): TextRange = jsNative
        member __.setCommentRange<'T>(node: 'T, range: TextRange): 'T = jsNative
        member __.getSyntheticLeadingComments(node: Node): ResizeArray<SynthesizedComment> option = jsNative
        member __.setSyntheticLeadingComments<'T>(node: 'T, comments: ResizeArray<SynthesizedComment>): 'T = jsNative
        member __.addSyntheticLeadingComment<'T>(node: 'T, kind: U2<SyntaxKind, SyntaxKind>, text: string, ?hasTrailingNewLine: bool): 'T = jsNative
        member __.getSyntheticTrailingComments(node: Node): ResizeArray<SynthesizedComment> option = jsNative
        member __.setSyntheticTrailingComments<'T>(node: 'T, comments: ResizeArray<SynthesizedComment>): 'T = jsNative
        member __.addSyntheticTrailingComment<'T>(node: 'T, kind: U2<SyntaxKind, SyntaxKind>, text: string, ?hasTrailingNewLine: bool): 'T = jsNative
        member __.getConstantValue(node: U2<PropertyAccessExpression, ElementAccessExpression>): U2<string, float> = jsNative
        member __.setConstantValue(node: U2<PropertyAccessExpression, ElementAccessExpression>, value: U2<string, float>): U2<PropertyAccessExpression, ElementAccessExpression> = jsNative
        member __.addEmitHelper<'T>(node: 'T, helper: EmitHelper): 'T = jsNative
        member __.addEmitHelpers<'T>(node: 'T, helpers: ResizeArray<EmitHelper> option): 'T = jsNative
        member __.removeEmitHelper(node: Node, helper: EmitHelper): bool = jsNative
        member __.getEmitHelpers(node: Node): ResizeArray<EmitHelper> option = jsNative
        member __.moveEmitHelpers(source: Node, target: Node, predicate: Func<EmitHelper, bool>): unit = jsNative
        member __.setOriginalNode<'T>(node: 'T, original: Node option): 'T = jsNative
        member __.visitNode<'T>(node: 'T, visitor: Visitor, ?test: Func<Node, bool>, ?lift: Func<NodeArray<Node>, 'T>): 'T = jsNative
        member __.visitNode<'T>(node: 'T option, visitor: Visitor, ?test: Func<Node, bool>, ?lift: Func<NodeArray<Node>, 'T>): 'T option = jsNative
        member __.visitNodes<'T>(nodes: NodeArray<'T>, visitor: Visitor, ?test: Func<Node, bool>, ?start: float, ?count: float): NodeArray<'T> = jsNative
        member __.visitNodes<'T>(nodes: NodeArray<'T> option, visitor: Visitor, ?test: Func<Node, bool>, ?start: float, ?count: float): NodeArray<'T> option = jsNative
        member __.visitLexicalEnvironment(statements: NodeArray<Statement>, visitor: Visitor, context: TransformationContext, ?start: float, ?ensureUseStrict: bool): NodeArray<Statement> = jsNative
        member __.visitParameterList(nodes: NodeArray<ParameterDeclaration>, visitor: Visitor, context: TransformationContext, ?nodesVisitor: obj): NodeArray<ParameterDeclaration> = jsNative
        member __.visitFunctionBody(node: FunctionBody, visitor: Visitor, context: TransformationContext): FunctionBody = jsNative
        member __.visitFunctionBody(node: FunctionBody option, visitor: Visitor, context: TransformationContext): FunctionBody option = jsNative
        member __.visitFunctionBody(node: ConciseBody, visitor: Visitor, context: TransformationContext): ConciseBody = jsNative
        member __.visitEachChild<'T>(node: 'T, visitor: Visitor, context: TransformationContext): 'T = jsNative
        member __.visitEachChild<'T>(node: 'T option, visitor: Visitor, context: TransformationContext, ?nodesVisitor: obj, ?tokenVisitor: Visitor): 'T option = jsNative
        member __.createPrinter(?printerOptions: PrinterOptions, ?handlers: PrintHandlers): Printer = jsNative
        member __.findConfigFile(searchPath: string, fileExists: Func<string, bool>, ?configName: string): string = jsNative
        member __.resolveTripleslashReference(moduleName: string, containingFile: string): string = jsNative
        member __.createCompilerHost(options: CompilerOptions, ?setParentNodes: bool): CompilerHost = jsNative
        member __.getPreEmitDiagnostics(program: Program, ?sourceFile: SourceFile, ?cancellationToken: CancellationToken): ResizeArray<Diagnostic> = jsNative
        member __.formatDiagnostics(diagnostics: ResizeArray<Diagnostic>, host: FormatDiagnosticsHost): string = jsNative
        member __.formatDiagnosticsWithColorAndContext(diagnostics: ResizeArray<Diagnostic>, host: FormatDiagnosticsHost): string = jsNative
        member __.flattenDiagnosticMessageText(messageText: U2<string, DiagnosticMessageChain>, newLine: string): string = jsNative
        member __.createProgram(rootNames: ResizeArray<string>, options: CompilerOptions, ?host: CompilerHost, ?oldProgram: Program): Program = jsNative
        member __.parseCommandLine(commandLine: ReadonlyArray<string>, ?readFile: Func<string, string option>): ParsedCommandLine = jsNative
        member __.readConfigFile(fileName: string, readFile: Func<string, string option>): obj = jsNative
        member __.parseConfigFileTextToJson(fileName: string, jsonText: string): obj = jsNative
        member __.readJsonConfigFile(fileName: string, readFile: Func<string, string option>): JsonSourceFile = jsNative
        member __.convertToObject(sourceFile: JsonSourceFile, errors: Push<Diagnostic>): obj = jsNative
        member __.parseJsonConfigFileContent(json: obj, host: ParseConfigHost, basePath: string, ?existingOptions: CompilerOptions, ?configFileName: string, ?resolutionStack: ResizeArray<Path>, ?extraFileExtensions: ReadonlyArray<JsFileExtensionInfo>): ParsedCommandLine = jsNative
        member __.parseJsonSourceFileConfigFileContent(sourceFile: JsonSourceFile, host: ParseConfigHost, basePath: string, ?existingOptions: CompilerOptions, ?configFileName: string, ?resolutionStack: ResizeArray<Path>, ?extraFileExtensions: ReadonlyArray<JsFileExtensionInfo>): ParsedCommandLine = jsNative
        member __.convertCompilerOptionsFromJson(jsonOptions: obj, basePath: string, ?configFileName: string): obj = jsNative
        member __.convertTypeAcquisitionFromJson(jsonOptions: obj, basePath: string, ?configFileName: string): obj = jsNative
        member __.createClassifier(): Classifier = jsNative
        member __.createDocumentRegistry(?useCaseSensitiveFileNames: bool, ?currentDirectory: string): DocumentRegistry = jsNative
        member __.preProcessFile(sourceText: string, ?readImportFiles: bool, ?detectJavaScriptImports: bool): PreProcessedFileInfo = jsNative
        member __.transpileModule(input: string, transpileOptions: TranspileOptions): TranspileOutput = jsNative
        member __.transpile(input: string, ?compilerOptions: CompilerOptions, ?fileName: string, ?diagnostics: ResizeArray<Diagnostic>, ?moduleName: string): string = jsNative
        member __.toEditorSettings(options: U2<EditorOptions, EditorSettings>): EditorSettings = jsNative
        member __.displayPartsToString(displayParts: ResizeArray<SymbolDisplayPart>): string = jsNative
        member __.getDefaultCompilerOptions(): CompilerOptions = jsNative
        member __.getSupportedCodeFixes(): ResizeArray<string> = jsNative
        member __.createLanguageServiceSourceFile(fileName: string, scriptSnapshot: IScriptSnapshot, scriptTarget: ScriptTarget, version: string, setNodeParents: bool, ?scriptKind: ScriptKind): SourceFile = jsNative
        member __.updateLanguageServiceSourceFile(sourceFile: SourceFile, scriptSnapshot: IScriptSnapshot, version: string, textChangeRange: TextChangeRange, ?aggressiveChecks: bool): SourceFile = jsNative
        member __.createLanguageService(host: LanguageServiceHost, ?documentRegistry: DocumentRegistry): LanguageService = jsNative
        member __.getDefaultLibFilePath(options: CompilerOptions): string = jsNative
        member __.transform<'T>(source: U2<'T, ResizeArray<'T>>, transformers: ResizeArray<TransformerFactory<'T>>, ?compilerOptions: CompilerOptions): TransformationResult<'T> = jsNative

    and [<AllowNullLiteral>] MapLike<'T> =
        [<Emit "$0[$1]{{=$2}}">] abstract Item: index: string -> 'T with get, set

    and [<AllowNullLiteral>] ReadonlyMap<'T> =
        abstract get: key: string -> 'T option
        abstract has: key: string -> bool
        abstract forEach: action: Func<'T, string, unit> -> unit
        abstract size: float with get, set
        abstract keys: unit -> Iterator<string>
        abstract values: unit -> Iterator<'T>
        abstract entries: unit -> Iterator<string * 'T>

    and [<AllowNullLiteral>] Map<'T> =
        inherit ReadonlyMap<'T>
        abstract set: key: string * value: 'T -> obj
        abstract delete: key: string -> bool
        abstract clear: unit -> unit

    and [<AllowNullLiteral>] Iterator<'T> =
        abstract next: unit -> U2<obj, obj>

    and [<AllowNullLiteral>] Push<'T> =
        abstract push: [<ParamArray>] values: 'T -> unit
        abstract TODO: string with get, set

    and Path =
        obj

    and [<AllowNullLiteral>] TextRange =
        abstract pos: float with get, set
        abstract ``end``: float with get, set

    and SyntaxKind =
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

    and NodeFlags =
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

    and ModifierFlags =
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

    and JsxFlags =
        | None = 0
        | IntrinsicNamedElement = 1
        | IntrinsicIndexedElement = 2
        | IntrinsicElement = 3

    and [<AllowNullLiteral>] Node =
        inherit TextRange
        abstract kind: SyntaxKind with get, set
        abstract flags: NodeFlags with get, set
        abstract decorators: Decorator array option with get, set
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
        abstract forEachChild: cbNode: Func<Node, 'T option> * ?cbNodeArray: Func<ResizeArray<Node>, 'T option> -> 'T option

    and [<AllowNullLiteral>] NodeArray<'T> =
        inherit ReadonlyArray<'T>
        inherit TextRange
        abstract hasTrailingComma: bool option with get, set

    and [<AllowNullLiteral>] Token<'TKind> =
        inherit Node
        abstract kind: 'TKind with get, set

    and DotDotDotToken =
        Token<SyntaxKind>

    and QuestionToken =
        Token<SyntaxKind>

    and ColonToken =
        Token<SyntaxKind>

    and EqualsToken =
        Token<SyntaxKind>

    and AsteriskToken =
        Token<SyntaxKind>

    and EqualsGreaterThanToken =
        Token<SyntaxKind>

    and EndOfFileToken =
        Token<SyntaxKind>

    and AtToken =
        Token<SyntaxKind>

    and ReadonlyToken =
        Token<SyntaxKind>

    and AwaitKeywordToken =
        Token<SyntaxKind>

    and Modifier =
        obj

    and ModifiersArray =
        Modifier array

    and [<AllowNullLiteral>] Identifier =
        inherit PrimaryExpression
        // abstract escapedText: __String with get, set
        abstract originalKeywordKind: SyntaxKind option with get, set
        abstract isInJSDocNamespace: bool option with get, set
        abstract text: string with get, set

    and [<AllowNullLiteral>] TransientIdentifier =
        inherit Identifier
        abstract resolvedSymbol: Symbol with get, set

    and [<AllowNullLiteral>] QualifiedName =
        inherit Node
        abstract left: EntityName with get, set
        abstract right: Identifier with get, set

    and EntityName =
        U2<Identifier, QualifiedName>

    and PropertyName =
        U4<Identifier, StringLiteral, NumericLiteral, ComputedPropertyName>

    and DeclarationName =
        U5<Identifier, StringLiteral, NumericLiteral, ComputedPropertyName, BindingPattern>

    and [<AllowNullLiteral>] Declaration =
        inherit Node
        abstract _declarationBrand: obj with get, set

    and [<AllowNullLiteral>] NamedDeclaration =
        inherit Declaration
        abstract name: DeclarationName option with get, set

    and [<AllowNullLiteral>] DeclarationStatement =
        inherit NamedDeclaration
        inherit Statement
        abstract name: U3<Identifier, StringLiteral, NumericLiteral> option with get, set

    and [<AllowNullLiteral>] ComputedPropertyName =
        inherit Node
        abstract expression: Expression with get, set

    and [<AllowNullLiteral>] Decorator =
        inherit Node
        abstract expression: LeftHandSideExpression with get, set

    and [<AllowNullLiteral>] TypeParameterDeclaration =
        inherit NamedDeclaration
        abstract parent: DeclarationWithTypeParameters option with get, set
        abstract name: Identifier with get, set
        abstract ``constraint``: TypeNode option with get, set
        abstract ``default``: TypeNode option with get, set
        abstract expression: Expression option with get, set

    and [<AllowNullLiteral>] SignatureDeclaration =
        inherit NamedDeclaration
        abstract name: PropertyName option with get, set
        abstract typeParameters: TypeParameterDeclaration array option with get, set
        abstract parameters: ParameterDeclaration array with get, set
        abstract ``type``: TypeNode option with get, set

    and [<AllowNullLiteral>] CallSignatureDeclaration =
        inherit SignatureDeclaration
        inherit TypeElement

    and [<AllowNullLiteral>] ConstructSignatureDeclaration =
        inherit SignatureDeclaration
        inherit TypeElement

    and BindingName =
        U2<Identifier, BindingPattern>

    and [<AllowNullLiteral>] VariableDeclaration =
        inherit NamedDeclaration
        abstract parent: U2<VariableDeclarationList, CatchClause> option with get, set
        abstract name: BindingName with get, set
        abstract ``type``: TypeNode option with get, set
        abstract initializer: Expression option with get, set

    and [<AllowNullLiteral>] VariableDeclarationList =
        inherit Node
        abstract parent: U4<VariableStatement, ForStatement, ForOfStatement, ForInStatement> option with get, set
        abstract declarations: VariableDeclaration array with get, set

    and [<AllowNullLiteral>] ParameterDeclaration =
        inherit NamedDeclaration
        abstract parent: SignatureDeclaration option with get, set
        abstract dotDotDotToken: DotDotDotToken option with get, set
        abstract name: BindingName with get, set
        abstract questionToken: QuestionToken option with get, set
        abstract ``type``: TypeNode option with get, set
        abstract initializer: Expression option with get, set

    and [<AllowNullLiteral>] BindingElement =
        inherit NamedDeclaration
        abstract parent: BindingPattern option with get, set
        abstract propertyName: PropertyName option with get, set
        abstract dotDotDotToken: DotDotDotToken option with get, set
        abstract name: BindingName with get, set
        abstract initializer: Expression option with get, set

    and [<AllowNullLiteral>] PropertySignature =
        inherit TypeElement
        abstract name: PropertyName with get, set
        abstract questionToken: QuestionToken option with get, set
        abstract ``type``: TypeNode option with get, set
        abstract initializer: Expression option with get, set

    and [<AllowNullLiteral>] PropertyDeclaration =
        inherit ClassElement
        abstract questionToken: QuestionToken option with get, set
        abstract name: PropertyName with get, set
        abstract ``type``: TypeNode option with get, set
        abstract initializer: Expression option with get, set

    and [<AllowNullLiteral>] ObjectLiteralElement =
        inherit NamedDeclaration
        abstract _objectLiteralBrandBrand: obj with get, set
        abstract name: PropertyName option with get, set

    and ObjectLiteralElementLike =
        obj

    and [<AllowNullLiteral>] PropertyAssignment =
        inherit ObjectLiteralElement
        abstract name: PropertyName with get, set
        abstract questionToken: QuestionToken option with get, set
        abstract initializer: Expression with get, set

    and [<AllowNullLiteral>] ShorthandPropertyAssignment =
        inherit ObjectLiteralElement
        abstract name: Identifier with get, set
        abstract questionToken: QuestionToken option with get, set
        // abstract equalsToken: Token<SyntaxKind.EqualsToken> option with get, set
        abstract objectAssignmentInitializer: Expression option with get, set

    and [<AllowNullLiteral>] SpreadAssignment =
        inherit ObjectLiteralElement
        abstract expression: Expression with get, set

    and [<AllowNullLiteral>] VariableLikeDeclaration =
        inherit NamedDeclaration
        abstract propertyName: PropertyName option with get, set
        abstract dotDotDotToken: DotDotDotToken option with get, set
        abstract name: DeclarationName option with get, set
        abstract questionToken: QuestionToken option with get, set
        abstract ``type``: TypeNode option with get, set
        abstract initializer: Expression option with get, set

    and [<AllowNullLiteral>] PropertyLikeDeclaration =
        inherit NamedDeclaration
        abstract name: PropertyName with get, set

    and [<AllowNullLiteral>] ObjectBindingPattern =
        inherit Node
        abstract parent: U3<VariableDeclaration, ParameterDeclaration, BindingElement> option with get, set
        abstract elements: BindingElement array with get, set

    and [<AllowNullLiteral>] ArrayBindingPattern =
        inherit Node
        abstract parent: U3<VariableDeclaration, ParameterDeclaration, BindingElement> option with get, set
        abstract elements: ArrayBindingElement array with get, set

    and BindingPattern =
        U2<ObjectBindingPattern, ArrayBindingPattern>

    and ArrayBindingElement =
        U2<BindingElement, OmittedExpression>

    and [<AllowNullLiteral>] FunctionLikeDeclarationBase =
        inherit SignatureDeclaration
        abstract _functionLikeDeclarationBrand: obj with get, set
        abstract asteriskToken: AsteriskToken option with get, set
        abstract questionToken: QuestionToken option with get, set
        abstract body: U2<Block, Expression> option with get, set

    and FunctionLikeDeclaration =
        obj

    and FunctionLike =
        obj

    and [<AllowNullLiteral>] FunctionDeclaration =
        inherit FunctionLikeDeclarationBase
        inherit DeclarationStatement
        abstract name: Identifier option with get, set
        abstract body: FunctionBody option with get, set

    and [<AllowNullLiteral>] MethodSignature =
        inherit SignatureDeclaration
        inherit TypeElement
        abstract name: PropertyName with get, set

    and [<AllowNullLiteral>] MethodDeclaration =
        inherit FunctionLikeDeclarationBase
        inherit ClassElement
        inherit ObjectLiteralElement
        abstract name: PropertyName with get, set
        abstract body: FunctionBody option with get, set

    and [<AllowNullLiteral>] ConstructorDeclaration =
        inherit FunctionLikeDeclarationBase
        inherit ClassElement
        abstract parent: U2<ClassDeclaration, ClassExpression> option with get, set
        abstract body: FunctionBody option with get, set

    and [<AllowNullLiteral>] SemicolonClassElement =
        inherit ClassElement
        abstract parent: U2<ClassDeclaration, ClassExpression> option with get, set

    and [<AllowNullLiteral>] GetAccessorDeclaration =
        inherit FunctionLikeDeclarationBase
        inherit ClassElement
        inherit ObjectLiteralElement
        abstract parent: U3<ClassDeclaration, ClassExpression, ObjectLiteralExpression> option with get, set
        abstract name: PropertyName with get, set
        abstract body: FunctionBody with get, set

    and [<AllowNullLiteral>] SetAccessorDeclaration =
        inherit FunctionLikeDeclarationBase
        inherit ClassElement
        inherit ObjectLiteralElement
        abstract parent: U3<ClassDeclaration, ClassExpression, ObjectLiteralExpression> option with get, set
        abstract name: PropertyName with get, set
        abstract body: FunctionBody with get, set

    and AccessorDeclaration =
        U2<GetAccessorDeclaration, SetAccessorDeclaration>

    and [<AllowNullLiteral>] IndexSignatureDeclaration =
        inherit SignatureDeclaration
        inherit ClassElement
        inherit TypeElement
        abstract parent: U4<ClassDeclaration, ClassExpression, InterfaceDeclaration, TypeLiteralNode> option with get, set

    and [<AllowNullLiteral>] TypeNode =
        inherit Node
        abstract _typeNodeBrand: obj with get, set

    and [<AllowNullLiteral>] KeywordTypeNode =
        inherit TypeNode

    and [<AllowNullLiteral>] ThisTypeNode =
        inherit TypeNode

    and FunctionOrConstructorTypeNode =
        U2<FunctionTypeNode, ConstructorTypeNode>

    and [<AllowNullLiteral>] FunctionTypeNode =
        inherit TypeNode
        inherit SignatureDeclaration

    and [<AllowNullLiteral>] ConstructorTypeNode =
        inherit TypeNode
        inherit SignatureDeclaration

    and TypeReferenceType =
        U2<TypeReferenceNode, ExpressionWithTypeArguments>

    and [<AllowNullLiteral>] TypeReferenceNode =
        inherit TypeNode
        abstract typeName: EntityName with get, set
        abstract typeArguments: TypeNode array option with get, set

    and [<AllowNullLiteral>] TypePredicateNode =
        inherit TypeNode
        abstract parameterName: U2<Identifier, ThisTypeNode> with get, set
        abstract ``type``: TypeNode with get, set

    and [<AllowNullLiteral>] TypeQueryNode =
        inherit TypeNode
        abstract exprName: EntityName with get, set

    and [<AllowNullLiteral>] TypeLiteralNode =
        inherit TypeNode
        inherit Declaration
        abstract members: TypeElement array with get, set

    and [<AllowNullLiteral>] ArrayTypeNode =
        inherit TypeNode
        abstract elementType: TypeNode with get, set

    and [<AllowNullLiteral>] TupleTypeNode =
        inherit TypeNode
        abstract elementTypes: TypeNode array with get, set

    and UnionOrIntersectionTypeNode =
        U2<UnionTypeNode, IntersectionTypeNode>

    and [<AllowNullLiteral>] UnionTypeNode =
        inherit TypeNode
        abstract types: TypeNode array with get, set

    and [<AllowNullLiteral>] IntersectionTypeNode =
        inherit TypeNode
        abstract types: TypeNode array with get, set

    and [<AllowNullLiteral>] ParenthesizedTypeNode =
        inherit TypeNode
        abstract ``type``: TypeNode with get, set

    and [<AllowNullLiteral>] TypeOperatorNode =
        inherit TypeNode
        // abstract operator: SyntaxKind.KeyOfKeyword with get, set
        abstract ``type``: TypeNode with get, set

    and [<AllowNullLiteral>] IndexedAccessTypeNode =
        inherit TypeNode
        abstract objectType: TypeNode with get, set
        abstract indexType: TypeNode with get, set

    and [<AllowNullLiteral>] MappedTypeNode =
        inherit TypeNode
        inherit Declaration
        abstract parent: TypeAliasDeclaration option with get, set
        abstract Token: ReadonlyToken option with get, set
        abstract typeParameter: TypeParameterDeclaration with get, set
        abstract questionToken: QuestionToken option with get, set
        abstract ``type``: TypeNode option with get, set

    and [<AllowNullLiteral>] LiteralTypeNode =
        inherit TypeNode
        abstract literal: Expression with get, set

    and [<AllowNullLiteral>] StringLiteral =
        inherit LiteralExpression

    and [<AllowNullLiteral>] Expression =
        inherit Node
        abstract _expressionBrand: obj with get, set

    and [<AllowNullLiteral>] OmittedExpression =
        inherit Expression

    and [<AllowNullLiteral>] PartiallyEmittedExpression =
        inherit LeftHandSideExpression
        abstract expression: Expression with get, set

    and [<AllowNullLiteral>] UnaryExpression =
        inherit Expression
        abstract _unaryExpressionBrand: obj with get, set

    and IncrementExpression =
        UpdateExpression

    and [<AllowNullLiteral>] UpdateExpression =
        inherit UnaryExpression
        abstract _updateExpressionBrand: obj with get, set

    and PrefixUnaryOperator =
        obj

    and [<AllowNullLiteral>] PrefixUnaryExpression =
        inherit UpdateExpression
        abstract operator: PrefixUnaryOperator with get, set
        abstract operand: UnaryExpression with get, set

    and PostfixUnaryOperator =
        // U2<SyntaxKind.PlusPlusToken, SyntaxKind.MinusMinusToken>
        abstract TODO: string with get, set

    and [<AllowNullLiteral>] PostfixUnaryExpression =
        inherit UpdateExpression
        abstract operand: LeftHandSideExpression with get, set
        abstract operator: PostfixUnaryOperator with get, set

    and [<AllowNullLiteral>] LeftHandSideExpression =
        inherit UpdateExpression
        abstract _leftHandSideExpressionBrand: obj with get, set

    and [<AllowNullLiteral>] MemberExpression =
        inherit LeftHandSideExpression
        abstract _memberExpressionBrand: obj with get, set

    and [<AllowNullLiteral>] PrimaryExpression =
        inherit MemberExpression
        abstract _primaryExpressionBrand: obj with get, set

    and [<AllowNullLiteral>] NullLiteral =
        inherit PrimaryExpression
        inherit TypeNode

    and [<AllowNullLiteral>] BooleanLiteral =
        inherit PrimaryExpression
        inherit TypeNode

    and [<AllowNullLiteral>] ThisExpression =
        inherit PrimaryExpression
        inherit KeywordTypeNode

    and [<AllowNullLiteral>] SuperExpression =
        inherit PrimaryExpression

    and [<AllowNullLiteral>] ImportExpression =
        inherit PrimaryExpression

    and [<AllowNullLiteral>] DeleteExpression =
        inherit UnaryExpression
        abstract expression: UnaryExpression with get, set

    and [<AllowNullLiteral>] TypeOfExpression =
        inherit UnaryExpression
        abstract expression: UnaryExpression with get, set

    and [<AllowNullLiteral>] VoidExpression =
        inherit UnaryExpression
        abstract expression: UnaryExpression with get, set

    and [<AllowNullLiteral>] AwaitExpression =
        inherit UnaryExpression
        abstract expression: UnaryExpression with get, set

    and [<AllowNullLiteral>] YieldExpression =
        inherit Expression
        abstract asteriskToken: AsteriskToken option with get, set
        abstract expression: Expression option with get, set

    and ExponentiationOperator =
        // SyntaxKind.AsteriskAsteriskToken
        abstract TODO: string with get, set

    and MultiplicativeOperator =
        // U3<SyntaxKind.AsteriskToken, SyntaxKind.SlashToken, SyntaxKind.PercentToken>
        abstract TODO: string with get, set

    and MultiplicativeOperatorOrHigher =
        U2<ExponentiationOperator, MultiplicativeOperator>

    and AdditiveOperator =
        // U2<SyntaxKind.PlusToken, SyntaxKind.MinusToken>
        abstract TODO: string with get, set

    and AdditiveOperatorOrHigher =
        U2<MultiplicativeOperatorOrHigher, AdditiveOperator>

    and ShiftOperator =
        // U3<SyntaxKind.LessThanLessThanToken, SyntaxKind.GreaterThanGreaterThanToken, SyntaxKind.GreaterThanGreaterThanGreaterThanToken>
        abstract TODO: string with get, set

    and ShiftOperatorOrHigher =
        U2<AdditiveOperatorOrHigher, ShiftOperator>

    and RelationalOperator =
        obj

    and RelationalOperatorOrHigher =
        U2<ShiftOperatorOrHigher, RelationalOperator>

    and EqualityOperator =
        // U4<SyntaxKind.EqualsEqualsToken, SyntaxKind.EqualsEqualsEqualsToken, SyntaxKind.ExclamationEqualsEqualsToken, SyntaxKind.ExclamationEqualsToken>
        abstract TODO: string with get, set

    and EqualityOperatorOrHigher =
        U2<RelationalOperatorOrHigher, EqualityOperator>

    and BitwiseOperator =
        // U3<SyntaxKind.AmpersandToken, SyntaxKind.BarToken, SyntaxKind.CaretToken>
        abstract TODO: string with get, set

    and BitwiseOperatorOrHigher =
        U2<EqualityOperatorOrHigher, BitwiseOperator>

    and LogicalOperator =
        // U2<SyntaxKind.AmpersandAmpersandToken, SyntaxKind.BarBarToken>
        abstract TODO: string with get, set

    and LogicalOperatorOrHigher =
        U2<BitwiseOperatorOrHigher, LogicalOperator>

    and CompoundAssignmentOperator =
        obj

    and AssignmentOperator =
        // U2<SyntaxKind.EqualsToken, CompoundAssignmentOperator>
        abstract TODO: string with get, set

    and AssignmentOperatorOrHigher =
        U2<LogicalOperatorOrHigher, AssignmentOperator>

    and BinaryOperator =
        // U2<AssignmentOperatorOrHigher, SyntaxKind.CommaToken>
        abstract TODO: string with get, set

    and BinaryOperatorToken =
        Token<BinaryOperator>

    and [<AllowNullLiteral>] BinaryExpression =
        inherit Expression
        inherit Declaration
        abstract left: Expression with get, set
        abstract operatorToken: BinaryOperatorToken with get, set
        abstract right: Expression with get, set

    and AssignmentOperatorToken =
        Token<AssignmentOperator>

    and [<AllowNullLiteral>] AssignmentExpression<'TOperator> =
        inherit BinaryExpression
        abstract left: LeftHandSideExpression with get, set
        abstract operatorToken: 'TOperator with get, set

    and [<AllowNullLiteral>] ObjectDestructuringAssignment =
        inherit AssignmentExpression<EqualsToken>
        abstract left: ObjectLiteralExpression with get, set

    and [<AllowNullLiteral>] ArrayDestructuringAssignment =
        inherit AssignmentExpression<EqualsToken>
        abstract left: ArrayLiteralExpression with get, set

    and DestructuringAssignment =
        U2<ObjectDestructuringAssignment, ArrayDestructuringAssignment>

    and BindingOrAssignmentElement =
        obj

    and BindingOrAssignmentElementRestIndicator =
        U3<DotDotDotToken, SpreadElement, SpreadAssignment>

    and BindingOrAssignmentElementTarget =
        U2<BindingOrAssignmentPattern, Expression>

    and ObjectBindingOrAssignmentPattern =
        U2<ObjectBindingPattern, ObjectLiteralExpression>

    and ArrayBindingOrAssignmentPattern =
        U2<ArrayBindingPattern, ArrayLiteralExpression>

    and AssignmentPattern =
        U2<ObjectLiteralExpression, ArrayLiteralExpression>

    and BindingOrAssignmentPattern =
        U2<ObjectBindingOrAssignmentPattern, ArrayBindingOrAssignmentPattern>

    and [<AllowNullLiteral>] ConditionalExpression =
        inherit Expression
        abstract condition: Expression with get, set
        abstract questionToken: QuestionToken with get, set
        abstract whenTrue: Expression with get, set
        abstract colonToken: ColonToken with get, set
        abstract whenFalse: Expression with get, set

    and FunctionBody =
        Block

    and ConciseBody =
        U2<FunctionBody, Expression>

    and [<AllowNullLiteral>] FunctionExpression =
        inherit PrimaryExpression
        inherit FunctionLikeDeclarationBase
        abstract name: Identifier option with get, set
        abstract body: FunctionBody with get, set

    and [<AllowNullLiteral>] ArrowFunction =
        inherit Expression
        inherit FunctionLikeDeclarationBase
        abstract equalsGreaterThanToken: EqualsGreaterThanToken with get, set
        abstract body: ConciseBody with get, set

    and [<AllowNullLiteral>] LiteralLikeNode =
        inherit Node
        abstract text: string with get, set
        abstract isUnterminated: bool option with get, set
        abstract hasExtendedUnicodeEscape: bool option with get, set

    and [<AllowNullLiteral>] LiteralExpression =
        inherit LiteralLikeNode
        inherit PrimaryExpression

    and [<AllowNullLiteral>] RegularExpressionLiteral =
        inherit LiteralExpression

    and [<AllowNullLiteral>] NoSubstitutionTemplateLiteral =
        inherit LiteralExpression

    and [<AllowNullLiteral>] NumericLiteral =
        inherit LiteralExpression

    and [<AllowNullLiteral>] TemplateHead =
        inherit LiteralLikeNode
        abstract parent: TemplateExpression option with get, set

    and [<AllowNullLiteral>] TemplateMiddle =
        inherit LiteralLikeNode
        abstract parent: TemplateSpan option with get, set

    and [<AllowNullLiteral>] TemplateTail =
        inherit LiteralLikeNode
        abstract parent: TemplateSpan option with get, set

    and TemplateLiteral =
        U2<TemplateExpression, NoSubstitutionTemplateLiteral>

    and [<AllowNullLiteral>] TemplateExpression =
        inherit PrimaryExpression
        abstract head: TemplateHead with get, set
        abstract templateSpans: TemplateSpan array with get, set

    and [<AllowNullLiteral>] TemplateSpan =
        inherit Node
        abstract parent: TemplateExpression option with get, set
        abstract expression: Expression with get, set
        abstract literal: U2<TemplateMiddle, TemplateTail> with get, set

    and [<AllowNullLiteral>] ParenthesizedExpression =
        inherit PrimaryExpression
        abstract expression: Expression with get, set

    and [<AllowNullLiteral>] ArrayLiteralExpression =
        inherit PrimaryExpression
        abstract elements: Expression array with get, set

    and [<AllowNullLiteral>] SpreadElement =
        inherit Expression
        abstract expression: Expression with get, set

    and [<AllowNullLiteral>] ObjectLiteralExpressionBase<'T> =
        inherit PrimaryExpression
        inherit Declaration
        abstract properties: NodeArray<'T> with get, set

    and [<AllowNullLiteral>] ObjectLiteralExpression =
        inherit ObjectLiteralExpressionBase<ObjectLiteralElementLike>

    and EntityNameExpression =
        U3<Identifier, PropertyAccessEntityNameExpression, ParenthesizedExpression>

    and EntityNameOrEntityNameExpression =
        U2<EntityName, EntityNameExpression>

    and [<AllowNullLiteral>] PropertyAccessExpression =
        inherit MemberExpression
        inherit NamedDeclaration
        abstract expression: LeftHandSideExpression with get, set
        abstract name: Identifier with get, set

    and [<AllowNullLiteral>] SuperPropertyAccessExpression =
        inherit PropertyAccessExpression
        abstract expression: SuperExpression with get, set

    and [<AllowNullLiteral>] PropertyAccessEntityNameExpression =
        inherit PropertyAccessExpression
        abstract _propertyAccessExpressionLikeQualifiedNameBrand: obj option with get, set
        abstract expression: EntityNameExpression with get, set

    and [<AllowNullLiteral>] ElementAccessExpression =
        inherit MemberExpression
        abstract expression: LeftHandSideExpression with get, set
        abstract argumentExpression: Expression option with get, set

    and [<AllowNullLiteral>] SuperElementAccessExpression =
        inherit ElementAccessExpression
        abstract expression: SuperExpression with get, set

    and SuperProperty =
        U2<SuperPropertyAccessExpression, SuperElementAccessExpression>

    and [<AllowNullLiteral>] CallExpression =
        inherit LeftHandSideExpression
        inherit Declaration
        abstract expression: LeftHandSideExpression with get, set
        abstract typeArguments: TypeNode array option with get, set
        abstract arguments: Expression array with get, set

    and [<AllowNullLiteral>] SuperCall =
        inherit CallExpression
        abstract expression: SuperExpression with get, set

    and [<AllowNullLiteral>] ImportCall =
        inherit CallExpression
        abstract expression: ImportExpression with get, set

    and [<AllowNullLiteral>] ExpressionWithTypeArguments =
        inherit TypeNode
        abstract parent: HeritageClause option with get, set
        abstract expression: LeftHandSideExpression with get, set
        abstract typeArguments: TypeNode array option with get, set

    and [<AllowNullLiteral>] NewExpression =
        inherit PrimaryExpression
        inherit Declaration
        abstract expression: LeftHandSideExpression with get, set
        abstract typeArguments: TypeNode array option with get, set
        abstract arguments: Expression array option with get, set

    and [<AllowNullLiteral>] TaggedTemplateExpression =
        inherit MemberExpression
        abstract tag: LeftHandSideExpression with get, set
        abstract template: TemplateLiteral with get, set

    and CallLikeExpression =
        obj

    and [<AllowNullLiteral>] AsExpression =
        inherit Expression
        abstract expression: Expression with get, set
        abstract ``type``: TypeNode with get, set

    and [<AllowNullLiteral>] TypeAssertion =
        inherit UnaryExpression
        abstract ``type``: TypeNode with get, set
        abstract expression: UnaryExpression with get, set

    and AssertionExpression =
        U2<TypeAssertion, AsExpression>

    and [<AllowNullLiteral>] NonNullExpression =
        inherit LeftHandSideExpression
        abstract expression: Expression with get, set

    and [<AllowNullLiteral>] MetaProperty =
        inherit PrimaryExpression
        // abstract keywordToken: SyntaxKind.NewKeyword with get, set
        abstract name: Identifier with get, set

    and [<AllowNullLiteral>] JsxElement =
        inherit PrimaryExpression
        abstract openingElement: JsxOpeningElement with get, set
        abstract children: JsxChild array with get, set
        abstract closingElement: JsxClosingElement with get, set

    and JsxOpeningLikeElement =
        U2<JsxSelfClosingElement, JsxOpeningElement>

    and JsxAttributeLike =
        U2<JsxAttribute, JsxSpreadAttribute>

    and JsxTagNameExpression =
        U2<PrimaryExpression, PropertyAccessExpression>

    and [<AllowNullLiteral>] JsxAttributes =
        inherit ObjectLiteralExpressionBase<JsxAttributeLike>
        abstract parent: JsxOpeningLikeElement option with get, set

    and [<AllowNullLiteral>] JsxOpeningElement =
        inherit Expression
        abstract parent: JsxElement option with get, set
        abstract tagName: JsxTagNameExpression with get, set
        abstract attributes: JsxAttributes with get, set

    and [<AllowNullLiteral>] JsxSelfClosingElement =
        inherit PrimaryExpression
        abstract tagName: JsxTagNameExpression with get, set
        abstract attributes: JsxAttributes with get, set

    and [<AllowNullLiteral>] JsxAttribute =
        inherit ObjectLiteralElement
        abstract parent: JsxAttributes option with get, set
        abstract name: Identifier with get, set
        abstract initializer: U2<StringLiteral, JsxExpression> option with get, set

    and [<AllowNullLiteral>] JsxSpreadAttribute =
        inherit ObjectLiteralElement
        abstract parent: JsxAttributes option with get, set
        abstract expression: Expression with get, set

    and [<AllowNullLiteral>] JsxClosingElement =
        inherit Node
        abstract parent: JsxElement option with get, set
        abstract tagName: JsxTagNameExpression with get, set

    and [<AllowNullLiteral>] JsxExpression =
        inherit Expression
        abstract parent: U2<JsxElement, JsxAttributeLike> option with get, set
        // abstract dotDotDotToken: Token<SyntaxKind.DotDotDotToken> option with get, set
        abstract expression: Expression option with get, set

    and [<AllowNullLiteral>] JsxText =
        inherit Node
        abstract containsOnlyWhiteSpaces: bool with get, set
        abstract parent: JsxElement option with get, set

    and JsxChild =
        U4<JsxText, JsxExpression, JsxElement, JsxSelfClosingElement>

    and [<AllowNullLiteral>] Statement =
        inherit Node
        abstract _statementBrand: obj with get, set

    and [<AllowNullLiteral>] NotEmittedStatement =
        inherit Statement

    and [<AllowNullLiteral>] CommaListExpression =
        inherit Expression
        abstract elements: Expression array with get, set

    and [<AllowNullLiteral>] EmptyStatement =
        inherit Statement

    and [<AllowNullLiteral>] DebuggerStatement =
        inherit Statement

    and [<AllowNullLiteral>] MissingDeclaration =
        inherit DeclarationStatement
        inherit ClassElement
        inherit ObjectLiteralElement
        inherit TypeElement
        abstract name: Identifier option with get, set

    and BlockLike =
        U4<SourceFile, Block, ModuleBlock, CaseOrDefaultClause>

    and [<AllowNullLiteral>] Block =
        inherit Statement
        abstract statements: Statement array with get, set

    and [<AllowNullLiteral>] VariableStatement =
        inherit Statement
        abstract declarationList: VariableDeclarationList with get, set

    and [<AllowNullLiteral>] ExpressionStatement =
        inherit Statement
        abstract expression: Expression with get, set

    and [<AllowNullLiteral>] IfStatement =
        inherit Statement
        abstract expression: Expression with get, set
        abstract thenStatement: Statement with get, set
        abstract elseStatement: Statement option with get, set

    and [<AllowNullLiteral>] IterationStatement =
        inherit Statement
        abstract statement: Statement with get, set

    and [<AllowNullLiteral>] DoStatement =
        inherit IterationStatement
        abstract expression: Expression with get, set

    and [<AllowNullLiteral>] WhileStatement =
        inherit IterationStatement
        abstract expression: Expression with get, set

    and ForInitializer =
        U2<VariableDeclarationList, Expression>

    and [<AllowNullLiteral>] ForStatement =
        inherit IterationStatement
        abstract initializer: ForInitializer option with get, set
        abstract condition: Expression option with get, set
        abstract incrementor: Expression option with get, set

    and ForInOrOfStatement =
        U2<ForInStatement, ForOfStatement>

    and [<AllowNullLiteral>] ForInStatement =
        inherit IterationStatement
        abstract initializer: ForInitializer with get, set
        abstract expression: Expression with get, set

    and [<AllowNullLiteral>] ForOfStatement =
        inherit IterationStatement
        abstract awaitModifier: AwaitKeywordToken option with get, set
        abstract initializer: ForInitializer with get, set
        abstract expression: Expression with get, set

    and [<AllowNullLiteral>] BreakStatement =
        inherit Statement
        abstract label: Identifier option with get, set

    and [<AllowNullLiteral>] ContinueStatement =
        inherit Statement
        abstract label: Identifier option with get, set

    and BreakOrContinueStatement =
        U2<BreakStatement, ContinueStatement>

    and [<AllowNullLiteral>] ReturnStatement =
        inherit Statement
        abstract expression: Expression option with get, set

    and [<AllowNullLiteral>] WithStatement =
        inherit Statement
        abstract expression: Expression with get, set
        abstract statement: Statement with get, set

    and [<AllowNullLiteral>] SwitchStatement =
        inherit Statement
        abstract expression: Expression with get, set
        abstract caseBlock: CaseBlock with get, set
        abstract possiblyExhaustive: bool option with get, set

    and [<AllowNullLiteral>] CaseBlock =
        inherit Node
        abstract parent: SwitchStatement option with get, set
        abstract clauses: CaseOrDefaultClause array with get, set

    and [<AllowNullLiteral>] CaseClause =
        inherit Node
        abstract parent: CaseBlock option with get, set
        abstract expression: Expression with get, set
        abstract statements: Statement array with get, set

    and [<AllowNullLiteral>] DefaultClause =
        inherit Node
        abstract parent: CaseBlock option with get, set
        abstract statements: Statement array with get, set

    and CaseOrDefaultClause =
        U2<CaseClause, DefaultClause>

    and [<AllowNullLiteral>] LabeledStatement =
        inherit Statement
        abstract label: Identifier with get, set
        abstract statement: Statement with get, set

    and [<AllowNullLiteral>] ThrowStatement =
        inherit Statement
        abstract expression: Expression with get, set

    and [<AllowNullLiteral>] TryStatement =
        inherit Statement
        abstract tryBlock: Block with get, set
        abstract catchClause: CatchClause option with get, set
        abstract finallyBlock: Block option with get, set

    and [<AllowNullLiteral>] CatchClause =
        inherit Node
        abstract parent: TryStatement option with get, set
        abstract variableDeclaration: VariableDeclaration option with get, set
        abstract block: Block with get, set

    and DeclarationWithTypeParameters =
        obj

    and [<AllowNullLiteral>] ClassLikeDeclaration =
        inherit NamedDeclaration
        abstract name: Identifier option with get, set
        abstract typeParameters: TypeParameterDeclaration array option with get, set
        abstract heritageClauses: HeritageClause array option with get, set
        abstract members: ClassElement array with get, set

    and [<AllowNullLiteral>] ClassDeclaration =
        inherit ClassLikeDeclaration
        inherit DeclarationStatement
        abstract name: Identifier option with get, set

    and [<AllowNullLiteral>] ClassExpression =
        inherit ClassLikeDeclaration
        inherit PrimaryExpression

    and [<AllowNullLiteral>] ClassElement =
        inherit NamedDeclaration
        abstract _classElementBrand: obj with get, set
        abstract name: PropertyName option with get, set

    and [<AllowNullLiteral>] TypeElement =
        inherit NamedDeclaration
        abstract _typeElementBrand: obj with get, set
        abstract name: PropertyName option with get, set
        abstract questionToken: QuestionToken option with get, set

    and [<AllowNullLiteral>] InterfaceDeclaration =
        inherit DeclarationStatement
        abstract name: Identifier with get, set
        abstract typeParameters: TypeParameterDeclaration array option with get, set
        abstract heritageClauses: HeritageClause array option with get, set
        abstract members: TypeElement array with get, set

    and [<AllowNullLiteral>] HeritageClause =
        inherit Node
        abstract parent: U3<InterfaceDeclaration, ClassDeclaration, ClassExpression> option with get, set
        // abstract token: U2<SyntaxKind.ExtendsKeyword, SyntaxKind.ImplementsKeyword> with get, set
        abstract types: ExpressionWithTypeArguments array with get, set

    and [<AllowNullLiteral>] TypeAliasDeclaration =
        inherit DeclarationStatement
        abstract name: Identifier with get, set
        abstract typeParameters: TypeParameterDeclaration array option with get, set
        abstract ``type``: TypeNode with get, set

    and [<AllowNullLiteral>] EnumMember =
        inherit NamedDeclaration
        abstract parent: EnumDeclaration option with get, set
        abstract name: PropertyName with get, set
        abstract initializer: Expression option with get, set

    and [<AllowNullLiteral>] EnumDeclaration =
        inherit DeclarationStatement
        abstract name: Identifier with get, set
        abstract members: EnumMember array with get, set

    and ModuleName =
        U2<Identifier, StringLiteral>

    and ModuleBody =
        U2<NamespaceBody, JSDocNamespaceBody>

    and [<AllowNullLiteral>] ModuleDeclaration =
        inherit DeclarationStatement
        abstract parent: U2<ModuleBody, SourceFile> option with get, set
        abstract name: ModuleName with get, set
        abstract body: U2<ModuleBody, JSDocNamespaceDeclaration> option with get, set

    and NamespaceBody =
        U2<ModuleBlock, NamespaceDeclaration>

    and [<AllowNullLiteral>] NamespaceDeclaration =
        inherit ModuleDeclaration
        abstract name: Identifier with get, set
        abstract body: NamespaceBody with get, set

    and JSDocNamespaceBody =
        U2<Identifier, JSDocNamespaceDeclaration>

    and [<AllowNullLiteral>] JSDocNamespaceDeclaration =
        inherit ModuleDeclaration
        abstract name: Identifier with get, set
        abstract body: JSDocNamespaceBody with get, set

    and [<AllowNullLiteral>] ModuleBlock =
        inherit Node
        inherit Statement
        abstract parent: ModuleDeclaration option with get, set
        abstract statements: Statement array with get, set

    and ModuleReference =
        U2<EntityName, ExternalModuleReference>

    and [<AllowNullLiteral>] ImportEqualsDeclaration =
        inherit DeclarationStatement
        abstract parent: U2<SourceFile, ModuleBlock> option with get, set
        abstract name: Identifier with get, set
        abstract moduleReference: ModuleReference with get, set

    and [<AllowNullLiteral>] ExternalModuleReference =
        inherit Node
        abstract parent: ImportEqualsDeclaration option with get, set
        abstract expression: Expression option with get, set

    and [<AllowNullLiteral>] ImportDeclaration =
        inherit Statement
        abstract parent: U2<SourceFile, ModuleBlock> option with get, set
        abstract importClause: ImportClause option with get, set
        abstract moduleSpecifier: Expression with get, set

    and NamedImportBindings =
        U2<NamespaceImport, NamedImports>

    and [<AllowNullLiteral>] ImportClause =
        inherit NamedDeclaration
        abstract parent: ImportDeclaration option with get, set
        abstract name: Identifier option with get, set
        abstract namedBindings: NamedImportBindings option with get, set

    and [<AllowNullLiteral>] NamespaceImport =
        inherit NamedDeclaration
        abstract parent: ImportClause option with get, set
        abstract name: Identifier with get, set

    and [<AllowNullLiteral>] NamespaceExportDeclaration =
        inherit DeclarationStatement
        abstract name: Identifier with get, set

    and [<AllowNullLiteral>] ExportDeclaration =
        inherit DeclarationStatement
        abstract parent: U2<SourceFile, ModuleBlock> option with get, set
        abstract exportClause: NamedExports option with get, set
        abstract moduleSpecifier: Expression option with get, set

    and [<AllowNullLiteral>] NamedImports =
        inherit Node
        abstract parent: ImportClause option with get, set
        abstract elements: ImportSpecifier array with get, set

    and [<AllowNullLiteral>] NamedExports =
        inherit Node
        abstract parent: ExportDeclaration option with get, set
        abstract elements: ExportSpecifier array with get, set

    and NamedImportsOrExports =
        U2<NamedImports, NamedExports>

    and [<AllowNullLiteral>] ImportSpecifier =
        inherit NamedDeclaration
        abstract parent: NamedImports option with get, set
        abstract propertyName: Identifier option with get, set
        abstract name: Identifier with get, set

    and [<AllowNullLiteral>] ExportSpecifier =
        inherit NamedDeclaration
        abstract parent: NamedExports option with get, set
        abstract propertyName: Identifier option with get, set
        abstract name: Identifier with get, set

    and ImportOrExportSpecifier =
        U2<ImportSpecifier, ExportSpecifier>

    and [<AllowNullLiteral>] ExportAssignment =
        inherit DeclarationStatement
        abstract parent: SourceFile option with get, set
        abstract isExportEquals: bool option with get, set
        abstract expression: Expression with get, set

    and [<AllowNullLiteral>] FileReference =
        inherit TextRange
        abstract fileName: string with get, set

    and [<AllowNullLiteral>] CheckJsDirective =
        inherit TextRange
        abstract enabled: bool with get, set

    and CommentKind =
        // U2<SyntaxKind.SingleLineCommentTrivia, SyntaxKind.MultiLineCommentTrivia>
        abstract TODO: string with get, set

    and [<AllowNullLiteral>] CommentRange =
        inherit TextRange
        abstract hasTrailingNewLine: bool option with get, set
        abstract kind: CommentKind with get, set

    and [<AllowNullLiteral>] SynthesizedComment =
        inherit CommentRange
        abstract text: string with get, set
        abstract pos: obj with get, set
        abstract ``end``: obj with get, set

    and [<AllowNullLiteral>] JSDocTypeExpression =
        inherit TypeNode
        abstract ``type``: TypeNode with get, set

    and [<AllowNullLiteral>] JSDocType =
        inherit TypeNode
        abstract _jsDocTypeBrand: obj with get, set

    and [<AllowNullLiteral>] JSDocAllType =
        inherit JSDocType

    and [<AllowNullLiteral>] JSDocUnknownType =
        inherit JSDocType

    and [<AllowNullLiteral>] JSDocNonNullableType =
        inherit JSDocType
        abstract ``type``: TypeNode with get, set

    and [<AllowNullLiteral>] JSDocNullableType =
        inherit JSDocType
        abstract ``type``: TypeNode with get, set

    and [<AllowNullLiteral>] JSDocOptionalType =
        inherit JSDocType
        abstract ``type``: TypeNode with get, set

    and [<AllowNullLiteral>] JSDocFunctionType =
        inherit JSDocType
        inherit SignatureDeclaration

    and [<AllowNullLiteral>] JSDocVariadicType =
        inherit JSDocType
        abstract ``type``: TypeNode with get, set

    and JSDocTypeReferencingNode =
        U4<JSDocVariadicType, JSDocOptionalType, JSDocNullableType, JSDocNonNullableType>

    and [<AllowNullLiteral>] JSDoc =
        inherit Node
        abstract tags: U2<JSDocTag array, obj> with get, set
        abstract comment: U2<string, obj> with get, set

    and [<AllowNullLiteral>] JSDocTag =
        inherit Node
        abstract parent: JSDoc with get, set
        abstract atToken: AtToken with get, set
        abstract tagName: Identifier with get, set
        abstract comment: U2<string, obj> with get, set

    and [<AllowNullLiteral>] JSDocUnknownTag =
        inherit JSDocTag

    and [<AllowNullLiteral>] JSDocAugmentsTag =
        inherit JSDocTag
        abstract typeExpression: JSDocTypeExpression with get, set

    and [<AllowNullLiteral>] JSDocClassTag =
        inherit JSDocTag

    and [<AllowNullLiteral>] JSDocTemplateTag =
        inherit JSDocTag
        abstract typeParameters: TypeParameterDeclaration array with get, set

    and [<AllowNullLiteral>] JSDocReturnTag =
        inherit JSDocTag
        abstract typeExpression: JSDocTypeExpression with get, set

    and [<AllowNullLiteral>] JSDocTypeTag =
        inherit JSDocTag
        abstract typeExpression: JSDocTypeExpression with get, set

    and [<AllowNullLiteral>] JSDocTypedefTag =
        inherit JSDocTag
        inherit NamedDeclaration
        abstract parent: JSDoc with get, set
        abstract fullName: U2<JSDocNamespaceDeclaration, Identifier> option with get, set
        abstract name: Identifier option with get, set
        abstract typeExpression: U2<JSDocTypeExpression, JSDocTypeLiteral> option with get, set

    and [<AllowNullLiteral>] JSDocPropertyLikeTag =
        inherit JSDocTag
        inherit Declaration
        abstract parent: JSDoc with get, set
        abstract name: EntityName with get, set
        abstract typeExpression: JSDocTypeExpression with get, set
        abstract isNameFirst: bool with get, set
        abstract isBracketed: bool with get, set

    and [<AllowNullLiteral>] JSDocPropertyTag =
        inherit JSDocPropertyLikeTag

    and [<AllowNullLiteral>] JSDocParameterTag =
        inherit JSDocPropertyLikeTag

    and [<AllowNullLiteral>] JSDocTypeLiteral =
        inherit JSDocType
        abstract jsDocPropertyTags: ReadonlyArray<JSDocPropertyLikeTag> option with get, set
        abstract isArrayType: bool option with get, set

    and FlowFlags =
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

    and [<AllowNullLiteral>] FlowLock =
        abstract locked: bool option with get, set

    and [<AllowNullLiteral>] AfterFinallyFlow =
        inherit FlowNodeBase
        inherit FlowLock
        abstract antecedent: FlowNode with get, set

    and [<AllowNullLiteral>] PreFinallyFlow =
        inherit FlowNodeBase
        abstract antecedent: FlowNode with get, set
        abstract lock: FlowLock with get, set

    and FlowNode =
        obj

    and [<AllowNullLiteral>] FlowNodeBase =
        abstract flags: FlowFlags with get, set
        abstract id: float option with get, set

    and [<AllowNullLiteral>] FlowStart =
        inherit FlowNodeBase
        abstract container: U3<FunctionExpression, ArrowFunction, MethodDeclaration> option with get, set

    and [<AllowNullLiteral>] FlowLabel =
        inherit FlowNodeBase
        abstract antecedents: ResizeArray<FlowNode> with get, set

    and [<AllowNullLiteral>] FlowAssignment =
        inherit FlowNodeBase
        abstract node: U3<Expression, VariableDeclaration, BindingElement> with get, set
        abstract antecedent: FlowNode with get, set

    and [<AllowNullLiteral>] FlowCondition =
        inherit FlowNodeBase
        abstract expression: Expression with get, set
        abstract antecedent: FlowNode with get, set

    and [<AllowNullLiteral>] FlowSwitchClause =
        inherit FlowNodeBase
        abstract switchStatement: SwitchStatement with get, set
        abstract clauseStart: float with get, set
        abstract clauseEnd: float with get, set
        abstract antecedent: FlowNode with get, set

    and [<AllowNullLiteral>] FlowArrayMutation =
        inherit FlowNodeBase
        abstract node: U2<CallExpression, BinaryExpression> with get, set
        abstract antecedent: FlowNode with get, set

    and FlowType =
        U2<Type, IncompleteType>

    and [<AllowNullLiteral>] IncompleteType =
        abstract flags: TypeFlags with get, set
        abstract ``type``: Type with get, set
        abstract TODO: string with get, set

    and [<AllowNullLiteral>] AmdDependency =
        abstract path: string with get, set
        abstract name: string with get, set
        abstract TODO: string with get, set

    and [<AllowNullLiteral>] SourceFile =
        inherit Declaration
        abstract statements: Statement array with get, set
        // abstract endOfFileToken: Token<SyntaxKind.EndOfFileToken> with get, set
        abstract fileName: string with get, set
        abstract text: string with get, set
        abstract amdDependencies: ResizeArray<AmdDependency> with get, set
        abstract moduleName: string with get, set
        abstract referencedFiles: ResizeArray<FileReference> with get, set
        abstract typeReferenceDirectives: ResizeArray<FileReference> with get, set
        abstract languageVariant: LanguageVariant with get, set
        abstract isDeclarationFile: bool with get, set
        abstract hasNoDefaultLib: bool with get, set
        abstract languageVersion: ScriptTarget with get, set
        abstract getLineAndCharacterOfPosition: pos: float -> LineAndCharacter
        abstract getLineEndOfPosition: pos: float -> float
        abstract getLineStarts: unit -> ResizeArray<float>
        abstract getPositionOfLineAndCharacter: line: float * character: float -> float
        abstract update: newText: string * textChangeRange: TextChangeRange -> SourceFile

    and [<AllowNullLiteral>] Bundle =
        inherit Node
        abstract sourceFiles: ResizeArray<SourceFile> with get, set

    and [<AllowNullLiteral>] JsonSourceFile =
        inherit SourceFile
        abstract jsonObject: ObjectLiteralExpression option with get, set
        abstract extendedSourceFiles: ResizeArray<string> option with get, set

    and [<AllowNullLiteral>] ScriptReferenceHost =
        abstract getCompilerOptions: unit -> CompilerOptions
        abstract getSourceFile: fileName: string -> SourceFile
        abstract getSourceFileByPath: path: Path -> SourceFile
        abstract getCurrentDirectory: unit -> string

    and [<AllowNullLiteral>] ParseConfigHost =
        abstract useCaseSensitiveFileNames: bool with get, set
        abstract readDirectory: rootDir: string * extensions: ReadonlyArray<string> * excludes: ReadonlyArray<string> * includes: ReadonlyArray<string> * depth: float -> ResizeArray<string>
        abstract fileExists: path: string -> bool
        abstract readFile: path: string -> U2<string, obj>

    and [<AllowNullLiteral>] WriteFileCallback =
        [<Emit("$0($1...)")>] abstract Invoke: fileName: string * data: string * writeByteOrderMark: bool * ?onError: Func<string, unit> * ?sourceFiles: ReadonlyArray<SourceFile> -> unit

    and [<AllowNullLiteral>] [<Import("OperationCanceledException","ts")>] OperationCanceledException() =
        class end

    and [<AllowNullLiteral>] CancellationToken =
        abstract isCancellationRequested: unit -> bool
        abstract throwIfCancellationRequested: unit -> unit

    and [<AllowNullLiteral>] Program =
        inherit ScriptReferenceHost
        abstract getRootFileNames: unit -> ResizeArray<string>
        abstract getSourceFiles: unit -> ResizeArray<SourceFile>
        abstract emit: ?targetSourceFile: SourceFile * ?writeFile: WriteFileCallback * ?cancellationToken: CancellationToken * ?emitOnlyDtsFiles: bool * ?customTransformers: CustomTransformers -> EmitResult
        abstract getOptionsDiagnostics: ?cancellationToken: CancellationToken -> ResizeArray<Diagnostic>
        abstract getGlobalDiagnostics: ?cancellationToken: CancellationToken -> ResizeArray<Diagnostic>
        abstract getSyntacticDiagnostics: ?sourceFile: SourceFile * ?cancellationToken: CancellationToken -> ResizeArray<Diagnostic>
        abstract getSemanticDiagnostics: ?sourceFile: SourceFile * ?cancellationToken: CancellationToken -> ResizeArray<Diagnostic>
        abstract getDeclarationDiagnostics: ?sourceFile: SourceFile * ?cancellationToken: CancellationToken -> ResizeArray<Diagnostic>
        abstract getTypeChecker: unit -> TypeChecker

    and [<AllowNullLiteral>] CustomTransformers =
        abstract before: ResizeArray<TransformerFactory<SourceFile>> option with get, set
        abstract after: ResizeArray<TransformerFactory<SourceFile>> option with get, set

    and [<AllowNullLiteral>] SourceMapSpan =
        abstract emittedLine: float with get, set
        abstract emittedColumn: float with get, set
        abstract sourceLine: float with get, set
        abstract sourceColumn: float with get, set
        abstract nameIndex: float option with get, set
        abstract sourceIndex: float with get, set

    and [<AllowNullLiteral>] SourceMapData =
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

    and ExitStatus =
        | Success = 0
        | DiagnosticsPresent_OutputsSkipped = 1
        | DiagnosticsPresent_OutputsGenerated = 2

    and [<AllowNullLiteral>] EmitResult =
        abstract emitSkipped: bool with get, set
        abstract diagnostics: ResizeArray<Diagnostic> with get, set
        abstract emittedFiles: ResizeArray<string> with get, set
        abstract TODO: string with get, set

    and [<AllowNullLiteral>] TypeChecker =
        abstract getTypeOfSymbolAtLocation: symbol: Symbol * node: Node -> Type
        abstract getDeclaredTypeOfSymbol: symbol: Symbol -> Type
        abstract getPropertiesOfType: ``type``: Type -> ResizeArray<Symbol>
        abstract getPropertyOfType: ``type``: Type * propertyName: string -> U2<Symbol, obj>
        abstract getIndexInfoOfType: ``type``: Type * kind: IndexKind -> U2<IndexInfo, obj>
        abstract getSignaturesOfType: ``type``: Type * kind: SignatureKind -> ResizeArray<Signature>
        abstract getIndexTypeOfType: ``type``: Type * kind: IndexKind -> U2<Type, obj>
        abstract getBaseTypes: ``type``: InterfaceType -> ResizeArray<BaseType>
        abstract getBaseTypeOfLiteralType: ``type``: Type -> Type
        abstract getWidenedType: ``type``: Type -> Type
        abstract getReturnTypeOfSignature: signature: Signature -> Type
        abstract getNullableType: ``type``: Type * flags: TypeFlags -> Type
        abstract getNonNullableType: ``type``: Type -> Type
        abstract typeToTypeNode: ``type``: Type * ?enclosingDeclaration: Node * ?flags: NodeBuilderFlags -> TypeNode
        abstract signatureToSignatureDeclaration: signature: Signature * kind: SyntaxKind * ?enclosingDeclaration: Node * ?flags: NodeBuilderFlags -> SignatureDeclaration
        abstract indexInfoToIndexSignatureDeclaration: indexInfo: IndexInfo * kind: IndexKind * ?enclosingDeclaration: Node * ?flags: NodeBuilderFlags -> IndexSignatureDeclaration
        abstract getSymbolsInScope: location: Node * meaning: SymbolFlags -> ResizeArray<Symbol>
        abstract getSymbolAtLocation: node: Node -> U2<Symbol, obj>
        abstract getSymbolsOfParameterPropertyDeclaration: parameter: ParameterDeclaration * parameterName: string -> ResizeArray<Symbol>
        abstract getShorthandAssignmentValueSymbol: location: Node -> U2<Symbol, obj>
        abstract getExportSpecifierLocalTargetSymbol: location: ExportSpecifier -> U2<Symbol, obj>
        abstract getPropertySymbolOfDestructuringAssignment: location: Identifier -> U2<Symbol, obj>
        abstract getTypeAtLocation: node: Node -> Type
        abstract getTypeFromTypeNode: node: TypeNode -> Type
        abstract signatureToString: signature: Signature * ?enclosingDeclaration: Node * ?flags: TypeFormatFlags * ?kind: SignatureKind -> string
        abstract typeToString: ``type``: Type * ?enclosingDeclaration: Node * ?flags: TypeFormatFlags -> string
        abstract symbolToString: symbol: Symbol * ?enclosingDeclaration: Node * ?meaning: SymbolFlags -> string
        abstract getSymbolDisplayBuilder: unit -> SymbolDisplayBuilder
        abstract getFullyQualifiedName: symbol: Symbol -> string
        abstract getAugmentedPropertiesOfType: ``type``: Type -> ResizeArray<Symbol>
        abstract getRootSymbols: symbol: Symbol -> ResizeArray<Symbol>
        abstract getContextualType: node: Expression -> U2<Type, obj>
        abstract getResolvedSignature: node: CallLikeExpression * ?candidatesOutArray: ResizeArray<Signature> * ?argumentCount: float -> U2<Signature, obj>
        abstract getSignatureFromDeclaration: declaration: SignatureDeclaration -> U2<Signature, obj>
        abstract isImplementationOfOverload: node: FunctionLike -> U2<bool, obj>
        abstract isUndefinedSymbol: symbol: Symbol -> bool
        abstract isArgumentsSymbol: symbol: Symbol -> bool
        abstract isUnknownSymbol: symbol: Symbol -> bool
        abstract getConstantValue: node: U3<EnumMember, PropertyAccessExpression, ElementAccessExpression> -> U3<string, float, obj>
        abstract isValidPropertyAccess: node: U2<PropertyAccessExpression, QualifiedName> * propertyName: string -> bool
        abstract getAliasedSymbol: symbol: Symbol -> Symbol
        abstract getExportsOfModule: moduleSymbol: Symbol -> ResizeArray<Symbol>
        abstract getAllAttributesTypeFromJsxOpeningLikeElement: elementNode: JsxOpeningLikeElement -> U2<Type, obj>
        abstract getJsxIntrinsicTagNames: unit -> ResizeArray<Symbol>
        abstract isOptionalParameter: node: ParameterDeclaration -> bool
        abstract getAmbientModules: unit -> ResizeArray<Symbol>
        abstract tryGetMemberInModuleExports: memberName: string * moduleSymbol: Symbol -> U2<Symbol, obj>
        abstract getApparentType: ``type``: Type -> Type
        abstract getSuggestionForNonexistentProperty: node: Identifier * containingType: Type -> U2<string, obj>
        abstract getSuggestionForNonexistentSymbol: location: Node * name: string * meaning: SymbolFlags -> U2<string, obj>

    and NodeBuilderFlags =
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

    and [<AllowNullLiteral>] SymbolDisplayBuilder =
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

    and [<AllowNullLiteral>] SymbolWriter =
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

    and TypeFormatFlags =
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

    and SymbolFormatFlags =
        | None = 0
        | WriteTypeParametersOrArguments = 1
        | UseOnlyExternalAliasing = 2

    and TypePredicateKind =
        | This = 0
        | Identifier = 1

    and [<AllowNullLiteral>] TypePredicateBase =
        abstract kind: TypePredicateKind with get, set
        abstract ``type``: Type with get, set

    and [<AllowNullLiteral>] ThisTypePredicate =
        inherit TypePredicateBase

    and [<AllowNullLiteral>] IdentifierTypePredicate =
        inherit TypePredicateBase
        abstract parameterName: string with get, set
        abstract parameterIndex: float with get, set

    and TypePredicate =
        U2<IdentifierTypePredicate, ThisTypePredicate>

    and SymbolFlags =
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

    and [<AllowNullLiteral>] Symbol =
        abstract flags: SymbolFlags with get, set
        // abstract escapedName: __String with get, set
        abstract declarations: ResizeArray<Declaration> option with get, set
        abstract valueDeclaration: Declaration option with get, set
        // abstract members: SymbolTable option with get, set
        // abstract exports: SymbolTable option with get, set
        // abstract globalExports: SymbolTable option with get, set
        abstract name: string with get, set
        abstract getFlags: unit -> SymbolFlags
        // abstract getEscapedName: unit -> __String
        abstract getName: unit -> string
        abstract getDeclarations: unit -> U2<ResizeArray<Declaration>, obj>
        abstract getDocumentationComment: unit -> ResizeArray<SymbolDisplayPart>
        abstract getJsDocTags: unit -> ResizeArray<JSDocTagInfo>

    and InternalSymbolName =
        // | Call = __call
        // | Constructor = __constructor
        // | New = __new
        // | Index = __index
        // | ExportStar = __export
        // | Global = __global
        // | Missing = __missing
        // | Type = __type
        // | Object = __object
        // | JSXAttributes = __jsxAttributes
        // | Class = __class
        // | Function = __function
        // | Computed = __computed
        // | Resolving = __resolving__
        // | ExportEquals = export=
        // | Default = default
        abstract TODO: string with get, set

    and __String =
        U3<obj, obj, InternalSymbolName>

    // and [<AllowNullLiteral>] ReadonlyUnderscoreEscapedMap<'T> =
    //     // abstract :  with get, set
    //     // abstract size: float with get, set
    //     // abstract :  ->
    //     abstract get: key: __String -> U2<'T, obj>
    //     abstract has: key: __String -> bool
    //     abstract forEach: action: Func<'T, __String, unit> -> unit
    //     abstract keys: unit -> Iterator<__String>
    //     abstract values: unit -> Iterator<'T>
    //     abstract entries: unit -> Iterator<__String * 'T>

    // and [<AllowNullLiteral>] UnderscoreEscapedMap<'T> =
    //     inherit ReadonlyUnderscoreEscapedMap<'T>
    //     // abstract :  ->
    //     abstract set: key: __String * value: 'T -> obj
    //     abstract delete: key: __String -> bool
    //     abstract clear: unit -> unit

    // and SymbolTable =
    //     UnderscoreEscapedMap<Symbol>

    and TypeFlags =
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

    and DestructuringPattern =
        U3<BindingPattern, ObjectLiteralExpression, ArrayLiteralExpression>

    and [<AllowNullLiteral>] Type =
        abstract flags: TypeFlags with get, set
        abstract symbol: Symbol option with get, set
        abstract pattern: DestructuringPattern option with get, set
        abstract aliasSymbol: Symbol option with get, set
        abstract aliasTypeArguments: ResizeArray<Type> option with get, set
        abstract getFlags: unit -> TypeFlags
        abstract getSymbol: unit -> U2<Symbol, obj>
        abstract getProperties: unit -> ResizeArray<Symbol>
        abstract getProperty: propertyName: string -> U2<Symbol, obj>
        abstract getApparentProperties: unit -> ResizeArray<Symbol>
        abstract getCallSignatures: unit -> ResizeArray<Signature>
        abstract getConstructSignatures: unit -> ResizeArray<Signature>
        abstract getStringIndexType: unit -> U2<Type, obj>
        abstract getNumberIndexType: unit -> U2<Type, obj>
        abstract getBaseTypes: unit -> U2<ResizeArray<BaseType>, obj>
        abstract getNonNullableType: unit -> Type

    and [<AllowNullLiteral>] LiteralType =
        inherit Type
        abstract value: U2<string, float> with get, set
        abstract freshType: LiteralType option with get, set
        abstract regularType: LiteralType option with get, set

    and [<AllowNullLiteral>] StringLiteralType =
        inherit LiteralType
        abstract value: string with get, set

    and [<AllowNullLiteral>] NumberLiteralType =
        inherit LiteralType
        abstract value: float with get, set

    and [<AllowNullLiteral>] EnumType =
        inherit Type

    and ObjectFlags =
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

    and [<AllowNullLiteral>] ObjectType =
        inherit Type
        abstract objectFlags: ObjectFlags with get, set

    and [<AllowNullLiteral>] InterfaceType =
        inherit ObjectType
        abstract typeParameters: ResizeArray<TypeParameter> with get, set
        abstract outerTypeParameters: ResizeArray<TypeParameter> with get, set
        abstract localTypeParameters: ResizeArray<TypeParameter> with get, set
        abstract thisType: TypeParameter with get, set

    and BaseType =
        U2<ObjectType, IntersectionType>

    and [<AllowNullLiteral>] InterfaceTypeWithDeclaredMembers =
        inherit InterfaceType
        abstract declaredProperties: ResizeArray<Symbol> with get, set
        abstract declaredCallSignatures: ResizeArray<Signature> with get, set
        abstract declaredConstructSignatures: ResizeArray<Signature> with get, set
        abstract declaredStringIndexInfo: IndexInfo with get, set
        abstract declaredNumberIndexInfo: IndexInfo with get, set

    and [<AllowNullLiteral>] TypeReference =
        inherit ObjectType
        abstract target: GenericType with get, set
        abstract typeArguments: ResizeArray<Type> option with get, set

    and [<AllowNullLiteral>] GenericType =
        inherit InterfaceType
        inherit TypeReference

    and [<AllowNullLiteral>] UnionOrIntersectionType =
        inherit Type
        abstract types: ResizeArray<Type> with get, set

    and [<AllowNullLiteral>] UnionType =
        inherit UnionOrIntersectionType


    and [<AllowNullLiteral>] IntersectionType =
        inherit UnionOrIntersectionType


    and StructuredType =
        U3<ObjectType, UnionType, IntersectionType>

    and [<AllowNullLiteral>] EvolvingArrayType =
        inherit ObjectType
        abstract elementType: Type with get, set
        abstract finalArrayType: Type option with get, set

    and [<AllowNullLiteral>] TypeVariable =
        inherit Type

    and [<AllowNullLiteral>] TypeParameter =
        inherit TypeVariable
        abstract ``constraint``: Type with get, set
        abstract ``default``: Type option with get, set

    and [<AllowNullLiteral>] IndexedAccessType =
        inherit TypeVariable
        abstract objectType: Type with get, set
        abstract indexType: Type with get, set
        abstract ``constraint``: Type option with get, set

    and [<AllowNullLiteral>] IndexType =
        inherit Type
        abstract ``type``: U2<TypeVariable, UnionOrIntersectionType> with get, set

    and SignatureKind =
        | Call = 0
        | Construct = 1

    and [<AllowNullLiteral>] Signature =
        abstract declaration: SignatureDeclaration with get, set
        abstract typeParameters: ResizeArray<TypeParameter> option with get, set
        abstract parameters: ResizeArray<Symbol> with get, set
        abstract getDeclaration: unit -> SignatureDeclaration
        abstract getTypeParameters: unit -> U2<ResizeArray<TypeParameter>, obj>
        abstract getParameters: unit -> ResizeArray<Symbol>
        abstract getReturnType: unit -> Type
        abstract getDocumentationComment: unit -> ResizeArray<SymbolDisplayPart>
        abstract getJsDocTags: unit -> ResizeArray<JSDocTagInfo>

    and IndexKind =
        | String = 0
        | Number = 1

    and [<AllowNullLiteral>] IndexInfo =
        abstract ``type``: Type with get, set
        abstract isReadonly: bool with get, set
        abstract declaration: SignatureDeclaration option with get, set

    and InferencePriority =
        | NakedTypeVariable = 1
        | MappedType = 2
        | ReturnType = 4

    and [<AllowNullLiteral>] InferenceInfo =
        abstract typeParameter: TypeParameter with get, set
        abstract candidates: ResizeArray<Type> with get, set
        abstract inferredType: Type with get, set
        abstract priority: InferencePriority with get, set
        abstract topLevel: bool with get, set
        abstract isFixed: bool with get, set

    and InferenceFlags =
        | InferUnionTypes = 1
        | NoDefault = 2
        | AnyDefault = 4

    and Ternary =
        // | False = 0
        // | Maybe = 1
        // | True = undefined
        abstract TODO: string with get, set

    and TypeComparer =
        Func<Type, Type, bool, Ternary>

    and [<AllowNullLiteral>] JsFileExtensionInfo =
        abstract extension: string with get, set
        abstract isMixedContent: bool with get, set

    and [<AllowNullLiteral>] DiagnosticMessage =
        abstract key: string with get, set
        abstract category: DiagnosticCategory with get, set
        abstract code: float with get, set
        abstract message: string with get, set

    and [<AllowNullLiteral>] DiagnosticMessageChain =
        abstract messageText: string with get, set
        abstract category: DiagnosticCategory with get, set
        abstract code: float with get, set
        abstract next: DiagnosticMessageChain option with get, set

    and [<AllowNullLiteral>] Diagnostic =
        abstract file: U2<SourceFile, obj> with get, set
        abstract start: U2<float, obj> with get, set
        abstract length: U2<float, obj> with get, set
        abstract messageText: U2<string, DiagnosticMessageChain> with get, set
        abstract category: DiagnosticCategory with get, set
        abstract code: float with get, set
        abstract source: string option with get, set

    and DiagnosticCategory =
        | Warning = 0
        | Error = 1
        | Message = 2

    and ModuleResolutionKind =
        | Classic = 1
        | NodeJs = 2

    and [<AllowNullLiteral>] PluginImport =
        abstract name: string with get, set

    and CompilerOptionsValue =
        obj

    and [<AllowNullLiteral>] CompilerOptions =
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
        abstract typeRoots: ResizeArray<string> option with get, set
        [<Emit "$0[$1]{{=$2}}">] abstract Item: index: string -> U2<CompilerOptionsValue, JsonSourceFile> option with get, set

    and [<AllowNullLiteral>] TypeAcquisition =
        abstract enableAutoDiscovery: bool option with get, set
        abstract enable: bool option with get, set
        abstract ``include``: ResizeArray<string> option with get, set
        abstract exclude: ResizeArray<string> option with get, set
        [<Emit "$0[$1]{{=$2}}">] abstract Item: index: string -> U2<ResizeArray<string>, bool> option with get, set

    and [<AllowNullLiteral>] DiscoverTypingsInfo =
        abstract fileNames: ResizeArray<string> with get, set
        abstract projectRootPath: string with get, set
        abstract safeListPath: string with get, set
        abstract packageNameToTypingLocation: Map<string> with get, set
        abstract typeAcquisition: TypeAcquisition with get, set
        abstract compilerOptions: CompilerOptions with get, set
        abstract unresolvedImports: ReadonlyArray<string> with get, set

    and ModuleKind =
        | None = 0
        | CommonJS = 1
        | AMD = 2
        | UMD = 3
        | System = 4
        | ES2015 = 5
        | ESNext = 6

    and JsxEmit =
        | None = 0
        | Preserve = 1
        | React = 2
        | ReactNative = 3

    and NewLineKind =
        | CarriageReturnLineFeed = 0
        | LineFeed = 1

    and [<AllowNullLiteral>] LineAndCharacter =
        abstract line: float with get, set
        abstract character: float with get, set

    and ScriptKind =
        | Unknown = 0
        | JS = 1
        | JSX = 2
        | TS = 3
        | TSX = 4
        | External = 5
        | JSON = 6

    and ScriptTarget =
        | ES3 = 0
        | ES5 = 1
        | ES2015 = 2
        | ES2016 = 3
        | ES2017 = 4
        | ESNext = 5
        | Latest = 5

    and LanguageVariant =
        | Standard = 0
        | JSX = 1

    and [<AllowNullLiteral>] ParsedCommandLine =
        abstract options: CompilerOptions with get, set
        abstract typeAcquisition: TypeAcquisition option with get, set
        abstract fileNames: ResizeArray<string> with get, set
        abstract raw: obj option with get, set
        abstract errors: ResizeArray<Diagnostic> with get, set
        abstract wildcardDirectories: MapLike<WatchDirectoryFlags> option with get, set
        abstract compileOnSave: bool option with get, set

    and WatchDirectoryFlags =
        | None = 0
        | Recursive = 1

    and [<AllowNullLiteral>] ExpandResult =
        abstract fileNames: ResizeArray<string> with get, set
        abstract wildcardDirectories: MapLike<WatchDirectoryFlags> with get, set

    and [<AllowNullLiteral>] ModuleResolutionHost =
        abstract fileExists: fileName: string -> bool
        abstract readFile: fileName: string -> U2<string, obj>
        abstract trace: s: string -> unit
        abstract directoryExists: directoryName: string -> bool
        abstract realpath: path: string -> string
        abstract getCurrentDirectory: unit -> string
        abstract getDirectories: path: string -> ResizeArray<string>

    and [<AllowNullLiteral>] ResolvedModule =
        abstract resolvedFileName: string with get, set
        abstract isExternalLibraryImport: bool option with get, set

    and [<AllowNullLiteral>] ResolvedModuleFull =
        inherit ResolvedModule
        abstract extension: Extension with get, set
        abstract packageId: PackageId option with get, set

    and [<AllowNullLiteral>] PackageId =
        abstract name: string with get, set
        abstract subModuleName: string with get, set
        abstract version: string with get, set

    and [<StringEnum>] [<RequireQualifiedAccess>] Extension =
        | Ts
        | Tsx
        | Dts
        | Js
        | Jsx

    and [<AllowNullLiteral>] ResolvedModuleWithFailedLookupLocations =
        abstract resolvedModule: ResolvedModuleFull option with get, set

    and [<AllowNullLiteral>] ResolvedTypeReferenceDirective =
        abstract primary: bool with get, set
        abstract resolvedFileName: string option with get, set
        abstract packageId: PackageId option with get, set

    and [<AllowNullLiteral>] ResolvedTypeReferenceDirectiveWithFailedLookupLocations =
        abstract resolvedTypeReferenceDirective: ResolvedTypeReferenceDirective with get, set
        abstract failedLookupLocations: ResizeArray<string> with get, set

    and [<AllowNullLiteral>] CompilerHost =
        inherit ModuleResolutionHost
        abstract getSourceFile: fileName: string * languageVersion: ScriptTarget * ?onError: Func<string, unit> -> SourceFile
        abstract getSourceFileByPath: fileName: string * path: Path * languageVersion: ScriptTarget * ?onError: Func<string, unit> -> SourceFile
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
        abstract resolveTypeReferenceDirectives: typeReferenceDirectiveNames: ResizeArray<string> * containingFile: string -> ResizeArray<ResolvedTypeReferenceDirective>
        abstract getEnvironmentVariable: name: string -> string

    and [<AllowNullLiteral>] SourceMapRange =
        inherit TextRange
        abstract source: SourceMapSource option with get, set

    and [<AllowNullLiteral>] SourceMapSource =
        abstract fileName: string with get, set
        abstract text: string with get, set
        abstract skipTrivia: Func<float, float> option with get, set
        abstract getLineAndCharacterOfPosition: pos: float -> LineAndCharacter

    and EmitFlags =
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

    and [<AllowNullLiteral>] EmitHelper =
        abstract name: string with get, set
        abstract scoped: bool with get, set
        abstract text: string with get, set
        abstract priority: float option with get, set

    and EmitHint =
        | SourceFile = 0
        | Expression = 1
        | IdentifierName = 2
        | MappedTypeParameter = 3
        | Unspecified = 4

    and [<AllowNullLiteral>] TransformationContext =
        abstract onSubstituteNode: Func<EmitHint, Node, Node> with get, set
        abstract onEmitNode: Func<EmitHint, Node, Func<EmitHint, Node, unit>, unit> with get, set
        abstract getCompilerOptions: unit -> CompilerOptions
        abstract startLexicalEnvironment: unit -> unit
        abstract suspendLexicalEnvironment: unit -> unit
        abstract resumeLexicalEnvironment: unit -> unit
        abstract endLexicalEnvironment: unit -> ResizeArray<Statement>
        abstract hoistFunctionDeclaration: node: FunctionDeclaration -> unit
        abstract hoistVariableDeclaration: node: Identifier -> unit
        abstract requestEmitHelper: helper: EmitHelper -> unit
        abstract readEmitHelpers: unit -> U2<ResizeArray<EmitHelper>, obj>
        abstract enableSubstitution: kind: SyntaxKind -> unit
        abstract isSubstitutionEnabled: node: Node -> bool
        abstract enableEmitNotification: kind: SyntaxKind -> unit
        abstract isEmitNotificationEnabled: node: Node -> bool

    and [<AllowNullLiteral>] TransformationResult<'T> =
        abstract transformed: ResizeArray<'T> with get, set
        abstract diagnostics: ResizeArray<Diagnostic> option with get, set
        abstract substituteNode: hint: EmitHint * node: Node -> Node
        abstract emitNodeWithNotification: hint: EmitHint * node: Node * emitCallback: Func<EmitHint, Node, unit> -> unit
        abstract dispose: unit -> unit

    and TransformerFactory<'T> =
        Func<TransformationContext, Transformer<'T>>

    and Transformer<'T> =
        Func<'T, 'T>

    and Visitor =
        Func<Node, VisitResult<Node>>

    and VisitResult<'T> =
        U3<'T, ResizeArray<'T>, obj>

    and [<AllowNullLiteral>] Printer =
        abstract printNode: hint: EmitHint * node: Node * sourceFile: SourceFile -> string
        abstract printFile: sourceFile: SourceFile -> string
        abstract printBundle: bundle: Bundle -> string

    and [<AllowNullLiteral>] PrintHandlers =
        abstract hasGlobalName: name: string -> bool
        abstract onEmitNode: hint: EmitHint * node: Node * emitCallback: Func<EmitHint, Node, unit> -> unit
        abstract substituteNode: hint: EmitHint * node: Node -> Node

    and [<AllowNullLiteral>] PrinterOptions =
        abstract removeComments: bool option with get, set
        abstract newLine: NewLineKind option with get, set

    and [<AllowNullLiteral>] TextSpan =
        abstract start: float with get, set
        abstract length: float with get, set

    and [<AllowNullLiteral>] TextChangeRange =
        abstract span: TextSpan with get, set
        abstract newLength: float with get, set

    and [<AllowNullLiteral>] SyntaxList =
        inherit Node
        abstract _children: ResizeArray<Node> with get, set

    and FileWatcherEventKind =
        | Created = 0
        | Changed = 1
        | Deleted = 2

    and FileWatcherCallback =
        Func<string, FileWatcherEventKind, unit>

    and DirectoryWatcherCallback =
        Func<string, unit>

    and [<AllowNullLiteral>] WatchedFile =
        abstract fileName: string with get, set
        abstract callback: FileWatcherCallback with get, set
        abstract mtime: DateTime option with get, set

    and [<AllowNullLiteral>] System =
        abstract args: ResizeArray<string> with get, set
        abstract newLine: string with get, set
        abstract useCaseSensitiveFileNames: bool with get, set
        abstract write: s: string -> unit
        abstract readFile: path: string * ?encoding: string -> U2<string, obj>
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
        abstract createHash: data: string -> string
        abstract getMemoryUsage: unit -> float
        abstract exit: ?exitCode: float -> unit
        abstract realpath: path: string -> string
        abstract setTimeout: callback: Func<obj, unit> * ms: float * [<ParamArray>] args: obj -> obj
        abstract clearTimeout: timeoutId: obj -> unit

    and [<AllowNullLiteral>] FileWatcher =
        abstract close: unit -> unit

    and [<AllowNullLiteral>] DirectoryWatcher =
        inherit FileWatcher
        abstract directoryName: string with get, set
        abstract referenceCount: float with get, set

    and [<AllowNullLiteral>] ErrorCallback =
        [<Emit("$0($1...)")>] abstract Invoke: message: DiagnosticMessage * length: float -> unit

    and [<AllowNullLiteral>] Scanner =
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
        abstract lookAhead: callback: Func<unit, 'T> -> 'T
        abstract scanRange: start: float * length: float * callback: Func<unit, 'T> -> 'T
        abstract tryScan: callback: Func<unit, 'T> -> 'T

    and [<AllowNullLiteral>] ModuleResolutionCache =
        inherit NonRelativeModuleNameResolutionCache
        abstract getOrCreateCacheForDirectory: directoryName: string -> Map<ResolvedModuleWithFailedLookupLocations>

    and [<AllowNullLiteral>] NonRelativeModuleNameResolutionCache =
        abstract getOrCreateCacheForModuleName: nonRelativeModuleName: string -> PerModuleNameCache

    and [<AllowNullLiteral>] PerModuleNameCache =
        abstract get: directory: string -> ResolvedModuleWithFailedLookupLocations
        abstract set: directory: string * result: ResolvedModuleWithFailedLookupLocations -> unit

    and [<AllowNullLiteral>] FormatDiagnosticsHost =
        abstract getCurrentDirectory: unit -> string
        abstract getCanonicalFileName: fileName: string -> string
        abstract getNewLine: unit -> string

    and [<AllowNullLiteral>] SourceFileLike =
        abstract getLineAndCharacterOfPosition: pos: float -> LineAndCharacter

    and [<AllowNullLiteral>] IScriptSnapshot =
        abstract getText: start: float * ``end``: float -> string
        abstract getLength: unit -> float
        abstract getChangeRange: oldSnapshot: IScriptSnapshot -> U2<TextChangeRange, obj>
        abstract dispose: unit -> unit

    and [<AllowNullLiteral>] PreProcessedFileInfo =
        abstract referencedFiles: ResizeArray<FileReference> with get, set
        abstract typeReferenceDirectives: ResizeArray<FileReference> with get, set
        abstract importedFiles: ResizeArray<FileReference> with get, set
        abstract ambientExternalModules: ResizeArray<string> with get, set
        abstract isLibFile: bool with get, set

    and [<AllowNullLiteral>] HostCancellationToken =
        abstract isCancellationRequested: unit -> bool

    and [<AllowNullLiteral>] LanguageServiceHost =
        abstract getCompilationSettings: unit -> CompilerOptions
        abstract getNewLine: unit -> string
        abstract getProjectVersion: unit -> string
        abstract getScriptFileNames: unit -> ResizeArray<string>
        abstract getScriptKind: fileName: string -> ScriptKind
        abstract getScriptVersion: fileName: string -> string
        abstract getScriptSnapshot: fileName: string -> U2<IScriptSnapshot, obj>
        abstract getLocalizedDiagnosticMessages: unit -> obj
        abstract getCancellationToken: unit -> HostCancellationToken
        abstract getCurrentDirectory: unit -> string
        abstract getDefaultLibFileName: options: CompilerOptions -> string
        abstract log: s: string -> unit
        abstract trace: s: string -> unit
        abstract error: s: string -> unit
        abstract useCaseSensitiveFileNames: unit -> bool
        abstract readDirectory: path: string * ?extensions: ReadonlyArray<string> * ?exclude: ReadonlyArray<string> * ?``include``: ReadonlyArray<string> * ?depth: float -> ResizeArray<string>
        abstract readFile: path: string * ?encoding: string -> U2<string, obj>
        abstract fileExists: path: string -> bool
        abstract getTypeRootsVersion: unit -> float
        abstract resolveModuleNames: moduleNames: ResizeArray<string> * containingFile: string -> ResizeArray<ResolvedModule>
        abstract resolveTypeReferenceDirectives: typeDirectiveNames: ResizeArray<string> * containingFile: string -> ResizeArray<ResolvedTypeReferenceDirective>
        abstract directoryExists: directoryName: string -> bool
        abstract getDirectories: directoryName: string -> ResizeArray<string>
        abstract getCustomTransformers: unit -> U2<CustomTransformers, obj>

    and [<AllowNullLiteral>] LanguageService =
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

    and [<AllowNullLiteral>] Classifications =
        abstract spans: ResizeArray<float> with get, set
        abstract endOfLineState: EndOfLineState with get, set

    and [<AllowNullLiteral>] ClassifiedSpan =
        abstract textSpan: TextSpan with get, set
        abstract classificationType: ClassificationTypeNames with get, set

    and [<AllowNullLiteral>] NavigationBarItem =
        abstract text: string with get, set
        abstract kind: ScriptElementKind with get, set
        abstract kindModifiers: string with get, set
        abstract spans: ResizeArray<TextSpan> with get, set
        abstract childItems: ResizeArray<NavigationBarItem> with get, set
        abstract indent: float with get, set
        abstract bolded: bool with get, set
        abstract grayed: bool with get, set

    and [<AllowNullLiteral>] NavigationTree =
        abstract text: string with get, set
        abstract kind: ScriptElementKind with get, set
        abstract kindModifiers: string with get, set
        abstract spans: ResizeArray<TextSpan> with get, set
        abstract childItems: ResizeArray<NavigationTree> option with get, set

    and [<AllowNullLiteral>] TodoCommentDescriptor =
        abstract text: string with get, set
        abstract priority: float with get, set

    and [<AllowNullLiteral>] TodoComment =
        abstract descriptor: TodoCommentDescriptor with get, set
        abstract message: string with get, set
        abstract position: float with get, set

    and [<AllowNullLiteral>] [<Import("TextChange","ts")>] TextChange() =
        class end

    and [<AllowNullLiteral>] FileTextChanges =
        abstract fileName: string with get, set
        abstract textChanges: ResizeArray<TextChange> with get, set

    and [<AllowNullLiteral>] CodeAction =
        abstract description: string with get, set
        abstract changes: ResizeArray<FileTextChanges> with get, set

    and [<AllowNullLiteral>] ApplicableRefactorInfo =
        abstract name: string with get, set
        abstract description: string with get, set
        abstract inlineable: bool option with get, set
        abstract actions: ResizeArray<RefactorActionInfo> with get, set

    and RefactorActionInfo =
        obj

    and RefactorEditInfo =
        obj

    and [<AllowNullLiteral>] TextInsertion =
        abstract newText: string with get, set
        abstract caretOffset: float with get, set

    and [<AllowNullLiteral>] DocumentSpan =
        abstract textSpan: TextSpan with get, set
        abstract fileName: string with get, set

    and [<AllowNullLiteral>] RenameLocation =
        inherit DocumentSpan

    and [<AllowNullLiteral>] ReferenceEntry =
        inherit DocumentSpan
        abstract isWriteAccess: bool with get, set
        abstract isDefinition: bool with get, set
        abstract isInString: obj option with get, set

    and [<AllowNullLiteral>] ImplementationLocation =
        inherit DocumentSpan
        abstract kind: ScriptElementKind with get, set
        abstract displayParts: ResizeArray<SymbolDisplayPart> with get, set

    and [<AllowNullLiteral>] DocumentHighlights =
        abstract fileName: string with get, set
        abstract highlightSpans: ResizeArray<HighlightSpan> with get, set

    and [<StringEnum>] [<RequireQualifiedAccess>] HighlightSpanKind =
        | [<CompiledName "none">] None
        | [<CompiledName "definition">] Definition
        | [<CompiledName "reference">] Reference
        | [<CompiledName "writtenReference">] WrittenReference

    and [<AllowNullLiteral>] HighlightSpan =
        abstract fileName: string option with get, set
        abstract isInString: obj option with get, set
        abstract textSpan: TextSpan with get, set
        abstract kind: HighlightSpanKind with get, set

    and [<AllowNullLiteral>] NavigateToItem =
        abstract name: string with get, set
        abstract kind: ScriptElementKind with get, set
        abstract kindModifiers: string with get, set
        abstract matchKind: string with get, set
        abstract isCaseSensitive: bool with get, set
        abstract fileName: string with get, set
        abstract textSpan: TextSpan with get, set
        abstract containerName: string with get, set
        abstract containerKind: ScriptElementKind with get, set

    and [<RequireQualifiedAccess>] IndentStyle =
        | None = 0
        | Block = 1
        | Smart = 2

    and [<AllowNullLiteral>] EditorOptions =
        abstract BaseIndentSize: float option with get, set
        abstract IndentSize: float with get, set
        abstract TabSize: float with get, set
        abstract NewLineCharacter: string with get, set
        abstract ConvertTabsToSpaces: bool with get, set
        abstract IndentStyle: IndentStyle with get, set

    and [<AllowNullLiteral>] EditorSettings =
        abstract baseIndentSize: float option with get, set
        abstract indentSize: float option with get, set
        abstract tabSize: float option with get, set
        abstract newLineCharacter: string option with get, set
        abstract convertTabsToSpaces: bool option with get, set
        abstract indentStyle: IndentStyle option with get, set

    and [<AllowNullLiteral>] FormatCodeOptions =
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

    and [<AllowNullLiteral>] FormatCodeSettings =
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

    and [<AllowNullLiteral>] DefinitionInfo =
        abstract fileName: string with get, set
        abstract textSpan: TextSpan with get, set
        abstract kind: ScriptElementKind with get, set
        abstract name: string with get, set
        abstract containerKind: ScriptElementKind with get, set
        abstract containerName: string with get, set

    and [<AllowNullLiteral>] ReferencedSymbolDefinitionInfo =
        inherit DefinitionInfo
        abstract displayParts: ResizeArray<SymbolDisplayPart> with get, set

    and [<AllowNullLiteral>] ReferencedSymbol =
        abstract definition: ReferencedSymbolDefinitionInfo with get, set
        abstract references: ResizeArray<ReferenceEntry> with get, set

    and [<RequireQualifiedAccess>] SymbolDisplayPartKind =
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

    and [<AllowNullLiteral>] SymbolDisplayPart =
        abstract text: string with get, set
        abstract kind: string with get, set

    and [<AllowNullLiteral>] JSDocTagInfo =
        abstract name: string with get, set
        abstract text: string option with get, set

    and [<AllowNullLiteral>] QuickInfo =
        abstract kind: ScriptElementKind with get, set
        abstract kindModifiers: string with get, set
        abstract textSpan: TextSpan with get, set
        abstract displayParts: ResizeArray<SymbolDisplayPart> with get, set
        abstract documentation: ResizeArray<SymbolDisplayPart> with get, set
        abstract tags: ResizeArray<JSDocTagInfo> with get, set

    and [<AllowNullLiteral>] RenameInfo =
        abstract canRename: bool with get, set
        abstract localizedErrorMessage: string with get, set
        abstract displayName: string with get, set
        abstract fullDisplayName: string with get, set
        abstract kind: ScriptElementKind with get, set
        abstract kindModifiers: string with get, set
        abstract triggerSpan: TextSpan with get, set

    and [<AllowNullLiteral>] SignatureHelpParameter =
        abstract name: string with get, set
        abstract documentation: ResizeArray<SymbolDisplayPart> with get, set
        abstract displayParts: ResizeArray<SymbolDisplayPart> with get, set
        abstract isOptional: bool with get, set

    and [<AllowNullLiteral>] SignatureHelpItem =
        abstract isVariadic: bool with get, set
        abstract prefixDisplayParts: ResizeArray<SymbolDisplayPart> with get, set
        abstract suffixDisplayParts: ResizeArray<SymbolDisplayPart> with get, set
        abstract separatorDisplayParts: ResizeArray<SymbolDisplayPart> with get, set
        abstract parameters: ResizeArray<SignatureHelpParameter> with get, set
        abstract documentation: ResizeArray<SymbolDisplayPart> with get, set
        abstract tags: ResizeArray<JSDocTagInfo> with get, set

    and [<AllowNullLiteral>] SignatureHelpItems =
        abstract items: ResizeArray<SignatureHelpItem> with get, set
        abstract applicableSpan: TextSpan with get, set
        abstract selectedItemIndex: float with get, set
        abstract argumentIndex: float with get, set
        abstract argumentCount: float with get, set

    and [<AllowNullLiteral>] CompletionInfo =
        abstract isGlobalCompletion: bool with get, set
        abstract isMemberCompletion: bool with get, set
        abstract isNewIdentifierLocation: bool with get, set
        abstract entries: ResizeArray<CompletionEntry> with get, set

    and [<AllowNullLiteral>] CompletionEntry =
        abstract name: string with get, set
        abstract kind: ScriptElementKind with get, set
        abstract kindModifiers: string with get, set
        abstract sortText: string with get, set
        abstract replacementSpan: TextSpan option with get, set

    and [<AllowNullLiteral>] CompletionEntryDetails =
        abstract name: string with get, set
        abstract kind: ScriptElementKind with get, set
        abstract kindModifiers: string with get, set
        abstract displayParts: ResizeArray<SymbolDisplayPart> with get, set
        abstract documentation: ResizeArray<SymbolDisplayPart> with get, set
        abstract tags: ResizeArray<JSDocTagInfo> with get, set

    and [<AllowNullLiteral>] OutliningSpan =
        abstract textSpan: TextSpan with get, set
        abstract hintSpan: TextSpan with get, set
        abstract bannerText: string with get, set
        abstract autoCollapse: bool with get, set

    and [<AllowNullLiteral>] EmitOutput =
        abstract outputFiles: ResizeArray<OutputFile> with get, set
        abstract emitSkipped: bool with get, set

    and [<RequireQualifiedAccess>] OutputFileType =
        | JavaScript = 0
        | SourceMap = 1
        | Declaration = 2

    and [<AllowNullLiteral>] OutputFile =
        abstract name: string with get, set
        abstract writeByteOrderMark: bool with get, set
        abstract text: string with get, set

    and [<RequireQualifiedAccess>] EndOfLineState =
        | None = 0
        | InMultiLineCommentTrivia = 1
        | InSingleQuoteStringLiteral = 2
        | InDoubleQuoteStringLiteral = 3
        | InTemplateHeadOrNoSubstitutionTemplate = 4
        | InTemplateMiddleOrTail = 5
        | InTemplateSubstitutionPosition = 6

    and [<RequireQualifiedAccess>] TokenClass =
        | Punctuation = 0
        | Keyword = 1
        | Operator = 2
        | Comment = 3
        | Whitespace = 4
        | Identifier = 5
        | NumberLiteral = 6
        | StringLiteral = 7
        | RegExpLiteral = 8

    and [<AllowNullLiteral>] ClassificationResult =
        abstract finalLexState: EndOfLineState with get, set
        abstract entries: ResizeArray<ClassificationInfo> with get, set

    and [<AllowNullLiteral>] ClassificationInfo =
        abstract length: float with get, set
        abstract classification: TokenClass with get, set

    and [<AllowNullLiteral>] Classifier =
        abstract getClassificationsForLine: text: string * lexState: EndOfLineState * syntacticClassifierAbsent: bool -> ClassificationResult
        abstract getEncodedLexicalClassifications: text: string * endOfLineState: EndOfLineState * syntacticClassifierAbsent: bool -> Classifications

    and [<StringEnum>] [<RequireQualifiedAccess>] ScriptElementKind =
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

    and [<StringEnum>] [<RequireQualifiedAccess>] ScriptElementKindModifier =
        | [<CompiledName "none">] None
        | [<CompiledName "publicMemberModifier">] PublicMemberModifier
        | [<CompiledName "privateMemberModifier">] PrivateMemberModifier
        | [<CompiledName "protectedMemberModifier">] ProtectedMemberModifier
        | [<CompiledName "exportedModifier">] ExportedModifier
        | [<CompiledName "ambientModifier">] AmbientModifier
        | [<CompiledName "staticModifier">] StaticModifier
        | [<CompiledName "abstractModifier">] AbstractModifier

    and [<StringEnum>] [<RequireQualifiedAccess>] ClassificationTypeNames =
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

    and [<RequireQualifiedAccess>] ClassificationType =
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

    and [<AllowNullLiteral>] DocumentRegistry =
        abstract acquireDocument: fileName: string * compilationSettings: CompilerOptions * scriptSnapshot: IScriptSnapshot * version: string * ?scriptKind: ScriptKind -> SourceFile
        abstract acquireDocumentWithKey: fileName: string * path: Path * compilationSettings: CompilerOptions * key: DocumentRegistryBucketKey * scriptSnapshot: IScriptSnapshot * version: string * ?scriptKind: ScriptKind -> SourceFile
        abstract updateDocument: fileName: string * compilationSettings: CompilerOptions * scriptSnapshot: IScriptSnapshot * version: string * ?scriptKind: ScriptKind -> SourceFile
        abstract updateDocumentWithKey: fileName: string * path: Path * compilationSettings: CompilerOptions * key: DocumentRegistryBucketKey * scriptSnapshot: IScriptSnapshot * version: string * ?scriptKind: ScriptKind -> SourceFile
        abstract getKeyForCompilationSettings: settings: CompilerOptions -> DocumentRegistryBucketKey
        abstract releaseDocument: fileName: string * compilationSettings: CompilerOptions -> unit
        abstract releaseDocumentWithKey: path: Path * key: DocumentRegistryBucketKey -> unit
        abstract reportStats: unit -> string

    and DocumentRegistryBucketKey =
        obj

    and [<AllowNullLiteral>] TranspileOptions =
        abstract compilerOptions: CompilerOptions option with get, set
        abstract fileName: string option with get, set
        abstract reportDiagnostics: bool option with get, set
        abstract moduleName: string option with get, set
        abstract renamedDependencies: MapLike<string> option with get, set
        abstract transformers: CustomTransformers option with get, set

    and [<AllowNullLiteral>] TranspileOutput =
        abstract outputText: string with get, set
        abstract diagnostics: ResizeArray<Diagnostic> option with get, set
        abstract sourceMapText: string option with get, set

    and [<AllowNullLiteral>] DisplayPartsSymbolWriter =
        inherit SymbolWriter
        abstract displayParts: unit -> ResizeArray<SymbolDisplayPart>
