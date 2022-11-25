module rec ts2fable.Read

open Fable.Core
open Fable.Core.JsInterop
open TypeScript
open TypeScript.Ts
open System.Collections.Generic
open System
open ts2fable.Naming

type Node with
    member x.ForEachChild (cbNode: Node -> unit) =
        x.forEachChild<unit> (fun nd -> cbNode nd; None) |> ignore

let getAccessibility (modifiersOpt: ModifiersArray option) : FsAccessibility option =
    match modifiersOpt with
    | Some modifiers ->
        if modifiers |> Seq.exists (fun m -> m.kind = SyntaxKind.PublicKeyword) then
            Some FsAccessibility.Public
        else if modifiers |> Seq.exists (fun m -> m.kind = SyntaxKind.ProtectedKeyword) then
            Some FsAccessibility.Protected
        else if modifiers |> Seq.exists (fun m -> m.kind = SyntaxKind.PrivateKeyword) then
            Some FsAccessibility.Private
        else
            None
    | None -> None

let getPropertyName(pn: PropertyName): string =
    !!pn?getText() |> removeQuotes
    // match pn with
    // | U4.Case1 id -> id.getText() |> removeQuotes
    // | U4.Case2 sl -> sl.getText()
    // | U4.Case3 nl -> nl.getText()
    // | U4.Case4 cpn -> cpn.getText()

let getBindingName(bn: BindingName): string option =
    // Note: matching on Un does not actually work at runtime.
    // So need to manually check the .kind
    let syntaxNode : Node = !! bn
    match syntaxNode.kind with
    | SyntaxKind.Identifier ->
        let id : Identifier = !! bn
        Some (id.getText())
    | SyntaxKind.ObjectBindingPattern
    | SyntaxKind.ArrayBindingPattern -> None
    | _ -> failwithf "unknown Binding Name kind %A" syntaxNode.kind

type JS.NumberConstructor with
    [<Emit("$0.isSafeInteger($1)")>]
    member __.isSafeInteger (_: float) : bool = jsNative

let readLiteral (node: Node) : FsLiteral option =
    match node.kind with
    | SyntaxKind.StringLiteral ->
        Some (FsLiteral.String ((node :?> Ts.StringLiteral).text))
    | SyntaxKind.TrueKeyword -> Some (FsLiteral.Bool true)
    | SyntaxKind.FalseKeyword -> Some (FsLiteral.Bool false)
    | _ ->
        let text = node.getText()
        let parsedAsInt, intValue = System.Int32.TryParse text
        let parsedAsFloat, floatValue = System.Double.TryParse text
        if parsedAsInt then Some (FsLiteral.Int intValue)
        else if parsedAsFloat then Some (FsLiteral.Float floatValue)
        else None

let readEnumCase (checker: TypeChecker) (em: EnumMember): FsEnumCase =
    let name = em.name |> getPropertyName
    let tryGetConstantValue onFail =
        match checker.getConstantValue(!^em) with
        | Some (U2.Case1 s) -> FsLiteral.String s |> Some
        | Some (U2.Case2 f) ->
            if JS.Constructors.Number.isSafeInteger f then FsLiteral.Int (int f) |> Some
            else FsLiteral.Float f |> Some
        | None -> onFail ()
    let value =
        match em.initializer with
        | None -> tryGetConstantValue (fun () -> None)
        | Some ep ->
            match readLiteral ep with
            | Some (FsLiteral.String _ | FsLiteral.Int _ | FsLiteral.Float _) as value -> value
            | _ ->
                tryGetConstantValue (fun () -> failwithf "EnumCase type not supported %A %A" ep.kind name)
    {
        Attributes = []
        Comments = readCommentsAtLocation checker (!!em.name)
        Name = name
        Value = value
    }

let readTypeParameters (checker: TypeChecker) (tps: List<TypeParameterDeclaration> option): FsType list =
    match tps with
    | None -> []
    | Some tps ->
        tps |> List.ofSeq |> List.map (fun tp ->
            {
                Name = tp.name.getText()
                Constraint = tp.``constraint`` |> Option.map (readTypeNode checker)
                Default = tp.``default`` |> Option.map (readTypeNode checker)
            }
            |> FsType.GenericTypeParameter
        )

let readInherits (checker: TypeChecker) (hcs: List<HeritageClause> option): FsType list =
    match hcs with
    | None -> []
    | Some hcs ->
        hcs |> List.ofSeq |> List.collect (fun hc ->
            hc.types |> List.ofSeq |> List.map (fun eta ->
                let tp = readTypeNode checker eta
                let prms =
                    match eta.typeArguments with
                    | None -> []
                    | Some tps ->
                        tps |> List.ofSeq |> List.map (readTypeNode checker)
                if prms.Length = 0 then
                    tp
                else
                    { Type = tp; TypeParameters = prms } |> FsType.Generic
            )
        )

let readCommentLines (text: string): FsCommentContent =
    text.Replace("\r\n","\n").Replace("\r","\n").Split [|'\n'|] |> List.ofArray

let private readCommentSummary (documentationComment: ResizeArray<SymbolDisplayPart>): FsComment option =
    if documentationComment.Count = 0 then None
    else
        documentationComment
        |> List.ofSeq
        |> List.filter (fun dp -> dp.kind = "text")  // | SymbolDisplayPartKind.Text -> // TODO how to use the enum
        |> List.collect (fun dp -> dp.text |> readCommentLines)
        |> function | [] -> None | lines -> Some (FsComment.Summary lines)

let private readCommentTag (jsDocTag: JSDocTag): FsComment =
    let rec getQualifiedName (name: QualifiedName) =
        sprintf "%s.%s"
            (getEntityName name.left)
            (name.right.text)
    and getEntityName (name: EntityName) =
        // Fable can't match on EntityName
        // > error FABLE: Cannot type test: interface "TypeScript.Ts.QualifiedName"
        match name?kind with
        | SyntaxKind.Identifier ->
            let identifier: Identifier = unbox name
            identifier.text
        | SyntaxKind.QualifiedName ->
            getQualifiedName (unbox name)
        | kind -> failwithf "Invalid Syntaxkind: %A" kind

    let tag (jsDocTag: JSDocTag) = jsDocTag.tagName.text.ToLower()

    let (|Tag|_|) (kind: SyntaxKind) (jsDocTag: JSDocTag) =
        if jsDocTag.kind = kind then
            Some ()
        else
            None
    let (|TagNodeOf|_|) (names: string list) (jsDocTag: JSDocTag): string option =
        match jsDocTag with
        | Tag SyntaxKind.FirstJSDocTagNode when names |> List.contains (jsDocTag |> tag) ->
            Some (jsDocTag |> tag)
        | _ -> None

    let comment =
        jsDocTag.comment
        |> Option.map readCommentLines
        |> Option.defaultValue []

    let mkTag name =
        { Name = name; Content = comment }
        |> FsComment.Tag

    match jsDocTag with
    | TagNodeOf ["summary"; "description"; "desc"] _ ->
        comment |> FsComment.Summary
    | Tag SyntaxKind.JSDocParameterTag ->
        let p: JSDocParameterTag = downcast jsDocTag
        { Name = getEntityName p.name; Content = comment }
        |> FsComment.Param
    | Tag SyntaxKind.JSDocReturnTag ->
        comment |> FsComment.Returns
    | TagNodeOf ["example"] _ ->
        comment |> FsComment.Example
    | TagNodeOf ["remark"; "remarks"; "note"] _ ->
        comment |> FsComment.Remarks
    | TagNodeOf ["default"; "defaultvalue"] _ ->
        comment |> FsComment.Default
    | TagNodeOf ["version"] _ ->
        comment |> FsComment.Version
    // requires some extracting (like leading name)
    // -> handled in transform
    | TagNodeOf ["typeparam"; "tparam"] _ ->
        mkTag "typeparam"
    | TagNodeOf ["see"] _ ->
        mkTag "see"
    | TagNodeOf ["throws"; "throw"; "exception"] _ ->
        mkTag "throws"
    | TagNodeOf ["deprecated"] _ ->
        mkTag "deprecated"

    | Tag SyntaxKind.FirstJSDocTagNode ->
        { Name = jsDocTag |> tag; Content = comment }
        |> FsComment.UnknownTag
    | _ ->
        comment |> FsComment.Unknown

let private readCommentTags (jsDocTags: ResizeArray<JSDocTag>): FsComment list =
    if jsDocTags.Count = 0 then []
    else
        jsDocTags
        |> List.ofSeq
        |> List.map readCommentTag

let readComments (documentationComment: ResizeArray<SymbolDisplayPart>) (jsDocTags: ResizeArray<JSDocTag>): FsComment list =
    // documentationComment contains root comment
    // jsDocTags all other tags
    [
        match readCommentSummary documentationComment with
        | Some s -> yield s
        | None -> ()

        yield! readCommentTags jsDocTags
    ]


let readCommentsForSignatureDeclaration (checker: TypeChecker) (declaration: SignatureDeclaration): FsComment list =
    try
        match checker.getSignatureFromDeclaration declaration with
        | None -> []
        | Some signature ->
            readComments (signature.getDocumentationComment (Some checker)) (ts.getJSDocTags (!!declaration))
    with _ ->
        []

let readCommentsAtLocation (checker: TypeChecker) (nd: Node): FsComment list =
    match checker.getSymbolAtLocation nd with
    | None -> []
    | Some symbol ->
        // difference between `symbol.getJsDocTags()` && `ts.getJSDocTags id.parent`
        // * on symbol: gets unparsed tags
        // * on ts: gets parsed tags
        readComments (symbol.getDocumentationComment (Some checker)) (ts.getJSDocTags nd.parent)

let readInterface (checker: TypeChecker) (id: InterfaceDeclaration): FsInterface =
    {
        Attributes = []
        Comments = readCommentsAtLocation checker id.name
        IsStatic = false
        IsClass = false
        Name = id.name.getText()
        FullName = getFullName checker id.name
        Inherits = readInherits checker id.heritageClauses
        Members = id.members |> List.ofSeq |> List.map (readNamedDeclaration checker)
        TypeParameters = readTypeParameters checker id.typeParameters
        Accessibility = getAccessibility id.modifiers
    }

let readTypeLiteral (checker: TypeChecker) (tl: TypeLiteralNode): FsTypeLiteral =
    {
        Members = tl.members |> List.ofSeq |> List.map (readNamedDeclaration checker)
    }

let getFullName (checker: TypeChecker) (nd: Node) : string =
    match checker.getSymbolAtLocation nd with
    | None -> ""
    | Some smb -> checker.getFullyQualifiedName smb

let readDeclarationOfSymbol (checker: TypeChecker) (decl: Declaration) : FsMappedDeclaration list =
    if decl.kind = SyntaxKind.EnumMember then
        let em: EnumMember = !!decl
        let en: EnumDeclaration = em.parent
        [FsMappedDeclaration.EnumCase (readEnum checker en, readEnumCase checker em)]
    else
        match tryReadNamedDeclaration checker !!decl with
        | Some t -> [FsMappedDeclaration.Type t]
        | None ->
            match tryReadStatement checker !!decl with
            | Some ts -> ts |> List.map FsMappedDeclaration.Type
            | None -> []

let getDeclarationsOfTypeNode (checker: TypeChecker) (nd: Node) : Lazy<FsMappedDeclaration list> =
    let symbol =
        if ts.isTypeNode nd then
            let tp = checker.getTypeFromTypeNode !!nd
            if not (isNull tp) && not (isNullOrUndefined tp.symbol) then tp.symbol |> Some
            else checker.getSymbolAtLocation nd
        else checker.getSymbolAtLocation nd
    let fail () = Lazy<_>.CreateFromValue []
    match symbol with
    | Some symbol ->
        match symbol.getDeclarations() with
        | None -> fail () // no declaration found
        | Some decls ->
            lazy (decls |> Seq.collect (fun decl -> readDeclarationOfSymbol checker decl) |> Seq.toList)
    | None -> fail () // symbol undefined

let readClass (checker: TypeChecker) (cd: ClassDeclaration): FsInterface =
    let fullName = getFullName checker !!cd.name
    {
        Attributes = []
        Comments = cd.name |> Option.map (readCommentsAtLocation checker) |> Option.defaultValue []
        IsStatic = false
        IsClass = true
        Name =
            match cd.name with
            | None -> fullName
            | Some id -> id.getText()
        FullName = fullName
        Inherits = readInherits checker cd.heritageClauses
        Members = cd.members |> List.ofSeq |> List.map (readNamedDeclaration checker)
        TypeParameters = readTypeParameters checker cd.typeParameters
        Accessibility = getAccessibility cd.modifiers
    }

let hasModifier (kind: SyntaxKind) (modifiers: ModifiersArray option) =
    match modifiers with
    | None -> false
    | Some mds -> mds |> Seq.exists (fun md -> md.kind = kind)

let isConst (nd: Node): bool =
    nd.flags.HasFlag NodeFlags.Const

let isNamespace (nd: Node): bool =
    nd.getChildren() |> Seq.exists (fun nd -> nd.kind = SyntaxKind.NamespaceKeyword)

let readVariable (checker: TypeChecker) (vb: VariableStatement) =
    vb.declarationList.declarations |> List.ofSeq |> List.map (fun vd ->
        {
            Attributes = []
            Comments = readCommentsAtLocation checker (!!vd.name)
            Export = None
            HasDeclare = hasModifier SyntaxKind.DeclareKeyword vb.modifiers || hasModifier SyntaxKind.ExportKeyword vb.modifiers
            Name = vd.name |> getBindingName |> Option.defaultValue "unsupported_pattern"
            Type = vd.``type`` |> Option.map (readTypeNode checker) |> Option.defaultValue (simpleType "obj")
            IsConst = isConst (vb.declarationList)  // const is specified before all declarations -> part of list
            IsStatic = hasModifier SyntaxKind.StaticKeyword vd.modifiers
            Accessibility = getAccessibility vb.modifiers
        }
        |> FsType.Variable
    )

let readEnum (checker: TypeChecker) (ed: EnumDeclaration): FsEnum =
    {
        Attributes = []
        Comments = readCommentsAtLocation checker ed.name
        Name = ed.name.getText()
        FullName = getFullName checker ed.name
        Cases = ed.members |> List.ofSeq |> List.map (readEnumCase checker)
    }

let readTypeReference (checker: TypeChecker) (tr: TypeReferenceNode): FsType =
    match tr.typeArguments with
    | None ->
        {
            Name = tr.getText()
            FullName = getFullName checker !!tr.typeName
            Declarations = getDeclarationsOfTypeNode checker !!tr.typeName
        }
        |> FsType.Mapped
    | Some tas ->
        {
            Type =
                let typeName = !!tr.typeName?getText()
                    // match tr.typeName with
                    // | U2.Case1 id -> id.getText()
                    // | U2.Case2 qn -> qn.getText()
                if isNull typeName then
                    failwithf "readTypeReference type name is null: %s" (tr.getText())
                {
                    Name = typeName
                    FullName = getFullName checker !!tr.typeName
                    Declarations = getDeclarationsOfTypeNode checker !!tr.typeName
                }
                |> FsType.Mapped
            TypeParameters =
                tas |> List.ofSeq |> List.map (readTypeNode checker)
        }
        |> FsType.Generic

let readFunctionType (checker: TypeChecker) (ft: FunctionTypeNode): FsFunction =
    {
        // TODO https://github.com/fable-compiler/ts2fable/issues/68
        Attributes = []
        Comments = []//ft.name |> Option.map (readPropertyNameComments checker) |> Option.defaultValue []
        Kind = FsFunctionKind.Regular
        IsStatic = hasModifier SyntaxKind.StaticKeyword ft.modifiers
        Name = ft.name |> Option.map getPropertyName
        TypeParameters = readTypeParameters checker ft.typeParameters
        Params =  ft.parameters |> List.ofSeq |> List.mapi (readParameterDeclaration checker)
        ReturnType = readTypeNode checker ft.``type``
            // match ft.``type`` with
            // | Some t -> readTypeNode checker t
            // | None -> simpleType "unit"
        Accessibility = getAccessibility ft.modifiers
    }

let rec readTypeNode (checker: TypeChecker) (t: TypeNode): FsType =
    match t.kind with
    | SyntaxKind.StringKeyword -> simpleType "string"
    | SyntaxKind.FunctionType ->
        readFunctionType checker (t :?> FunctionTypeNode) |> FsType.Function
    | SyntaxKind.TypeReference ->
        readTypeReference checker (t :?> TypeReferenceNode)
    | SyntaxKind.ArrayType ->
        let at = t :?> ArrayTypeNode
        FsType.Array (readTypeNode checker at.elementType)
    | SyntaxKind.NumberKeyword -> simpleType "float"
    | SyntaxKind.BooleanKeyword -> simpleType "bool"
    | SyntaxKind.UnionType ->
        readUnionType checker (t :?> UnionTypeNode)
    | SyntaxKind.AnyKeyword ->
        {
            Option = true // any allows null or undefined
            Types = [simpleType "obj"]
        }
        |> FsType.Union
    | SyntaxKind.VoidKeyword -> simpleType "unit"
    | SyntaxKind.TupleType ->
        let tp = t :?> TupleTypeNode
        {
            Types = tp.elementTypes |> List.ofSeq |> List.map (readTypeNode checker)
            Kind = FsTupleKind.Tuple
        }
        |> FsType.Tuple
    | SyntaxKind.SymbolKeyword -> simpleType "Symbol"
    | SyntaxKind.ThisType -> FsType.This
    | SyntaxKind.TypePredicate -> simpleType "bool"
    | SyntaxKind.TypeLiteral ->
        let tl = t :?> TypeLiteralNode
        readTypeLiteral checker tl |> FsType.TypeLiteral
    | SyntaxKind.IntersectionType ->
        let itp = t :?> IntersectionTypeNode
        {
            Types = itp.types |> List.ofSeq |> List.map (readTypeNode checker)
            Kind = FsTupleKind.Intersection
        }
        |> FsType.Tuple
    | SyntaxKind.IndexedAccessType ->
        // `{ bivarianceHack(instance: T | null): void }["bivarianceHack"]` (React)
        // -> get type of `bivarianceHack`
        let ia = t :?> IndexedAccessTypeNode
        let indexType = readTypeNode checker ia.indexType
        match indexType with
        | FsType.Literal (FsLiteral.String name) ->
            let inner = readTypeNode checker ia.objectType
            match inner with
            | FsType.TypeLiteral tl ->
                tl.Members
                |> List.tryPick (
                    function
                    | FsType.Function f when f.Name = Some name ->
                        FsType.Function f
                        |> Some
                    | _ -> None
                )
                |> Option.defaultValue FsType.TODO
            | _ -> FsType.TODO
        | _ -> FsType.TODO
    | SyntaxKind.TypeQuery ->
        // let tq = t :?> TypeQueryNode
        simpleType "obj"
    | SyntaxKind.LiteralType ->
        let lt = t :?> LiteralTypeNode
        readLiteral !!lt.literal
        |> Option.map FsType.Literal
        |> Option.defaultValue (simpleType "obj")
    | SyntaxKind.ExpressionWithTypeArguments ->
        let eta = t :?> ExpressionWithTypeArguments
        {
            Name = readExpressionText eta.expression
            FullName = getFullName checker eta.expression
            Declarations = getDeclarationsOfTypeNode checker eta.expression
        }
        |> FsType.Mapped

    | SyntaxKind.ParenthesizedType ->
        let pt = t :?> ParenthesizedTypeNode
        // just get the type in parenthesis
        readTypeNode checker pt.``type``
    | SyntaxKind.MappedType ->
        {
            Types = [simpleType "obj"]
            Kind = FsTupleKind.Mapped
        }
        |> FsType.Tuple
        // TODO map mapped types https://github.com/fable-compiler/ts2fable/issues/44

    | SyntaxKind.NeverKeyword ->
        // printfn "unsupported TypeNode NeverKeyword: %A" t
        // simpleType "obj"
        FsType.TODO
    | SyntaxKind.UndefinedKeyword -> simpleType "obj"
    | SyntaxKind.NullKeyword -> FsType.TODO // It should be an option
    | SyntaxKind.ObjectKeyword -> simpleType "obj"
    // | ConditionalType -> FsType.TODO
    // | ConstructorType -> FsType.TODO
    | SyntaxKind.TypeOperator ->
        let pt = t :?> TypeOperatorNode
        match pt.operator with
        | SyntaxKind.KeyOfKeyword ->
            {
                Type = readTypeNode checker pt.``type``
            }
            |> FsType.KeyOf
        | _ -> readTypeNode checker pt.``type``
    | _ ->
        printfn "unsupported TypeNode kind: %A" t.kind
        FsType.TODO

and readUnionType (checker: TypeChecker) (un: UnionTypeNode): FsType =
    let rec removeParens (t: TypeNode) =
        if t.kind = SyntaxKind.ParenthesizedType then
            let t = t :?> ParenthesizedTypeNode
            removeParens t.``type``
        else
            t
    let unTypes =
        un.types
        |> Seq.map removeParens
        |> List.ofSeq
    let isOptionType (t: TypeNode) = t.kind = SyntaxKind.UndefinedKeyword || t.kind = SyntaxKind.NullKeyword
    let isLiteralType (t: TypeNode) = t.kind = SyntaxKind.LiteralType
    let isOptional = unTypes |> List.exists isOptionType
    let makeEnumCase (t: LiteralTypeNode) =
        let name = !!t.literal?getText() |> removeQuotes
        {
            Attributes = []
            // comments aren't really supported for Literal Types in TS -> not available in node
            Comments = []
            Name = name
            Value = readLiteral !!t.literal
        }
    let makeEnum name cases =
        {
            Attributes = []
            Comments = []
            Name = name
            FullName = name
            Cases = cases
        }
        |> FsType.Enum
    let isKindOf kind (t: TypeNode) =
        if isLiteralType t then
            let lt = t :?> LiteralTypeNode
            if !!lt.literal?kind = kind then Some (makeEnumCase lt)
            else None
        else None
    let nonOptionalTypes = unTypes |> List.filter (isOptionType >> not)
    let enumCases, restCases = nonOptionalTypes |> List.partition isLiteralType
    let stringCases = enumCases |> List.choose (isKindOf SyntaxKind.StringLiteral)
    let numericCases = enumCases |> List.choose (isKindOf SyntaxKind.NumericLiteral)
    let restTypes = restCases |> List.map (readTypeNode checker)
    let stringTypes =
        if List.isEmpty stringCases then []
        else [ makeEnum "StringEnum" stringCases ]
    let numericTypes =
        if List.isEmpty numericCases then []
        else [ makeEnum "NumericEnum" numericCases ]
    let unionTypes = List.concat [restTypes; stringTypes; numericTypes]
    match unionTypes with
    | [] -> simpleType "obj"
    | [FsType.Enum _] -> FsType.TypeLiteral { Members = unionTypes } // wrapped in type literal so it can be extracted
    | _ -> FsType.Union { Option = isOptional; Types = unionTypes }

let readParameterDeclaration (checker: TypeChecker) (iParam:int) (pd: ParameterDeclaration): FsParam =
    let stringLiteral =
        pd.getChildren() |> List.ofSeq |> List.tryPick (fun ch ->
            ch.getChildren() |> List.ofSeq |> List.tryFind (fun gch -> gch.kind = SyntaxKind.StringLiteral)
        )
    let tp =
        match stringLiteral with
        | Some sl -> FsType.Literal (FsLiteral.String (sl.getText() |> removeQuotes))
        | None ->
            match pd.``type`` with
            | Some t -> readTypeNode checker t
            | None -> simpleType "obj"

    let name = pd.name |> getBindingName |> Option.defaultValue ("p" + string iParam)
    {
        Name = name
        Optional = pd.questionToken.IsSome
        ParamArray = pd.dotDotDotToken.IsSome
        Type = tp
    }

let readMethodSignature (checker: TypeChecker) (ms: MethodSignature): FsFunction =
    {
        Attributes = []
        Comments = readCommentsForSignatureDeclaration checker ms
        Kind = FsFunctionKind.Regular
        IsStatic = hasModifier SyntaxKind.StaticKeyword ms.modifiers
        Name = ms.name |> getPropertyName |> Some
        TypeParameters = readTypeParameters checker ms.typeParameters
        Params = ms.parameters |> List.ofSeq |> List.mapi (readParameterDeclaration checker)
        ReturnType =
            match ms.``type`` with
            | Some t -> readTypeNode checker t
            | None -> simpleType "unit"
        Accessibility = getAccessibility ms.modifiers
    }

let readMethodDeclaration checker (md: MethodDeclaration): FsFunction =
    {
        Attributes = []
        Comments = readCommentsForSignatureDeclaration checker md
        Kind = FsFunctionKind.Regular
        IsStatic = hasModifier SyntaxKind.StaticKeyword md.modifiers
        Name = md.name |> getPropertyName |> Some
        TypeParameters = readTypeParameters checker md.typeParameters
        Params = md.parameters |> List.ofSeq |> List.mapi (readParameterDeclaration checker)
        ReturnType =
            match md.``type`` with
            | Some t -> readTypeNode checker t
            | None -> simpleType "unit"
        Accessibility = getAccessibility md.modifiers
    }

let isReadOnly (modifiers: ModifiersArray option) =
    hasModifier SyntaxKind.ReadonlyKeyword modifiers

let readPropertySignature (checker: TypeChecker) (ps: PropertySignature): FsProperty =
    {
        Attributes = []
        Comments = readCommentsAtLocation checker (!!ps.name)
        Kind = FsPropertyKind.Regular
        Index = None
        Name = ps.name |> getPropertyName
        Option = ps.questionToken.IsSome
        Type =
            match ps.``type`` with
            | None -> FsType.None
            | Some tp -> readTypeNode checker tp
        Accessor = FsAccessor.fromReadonly <| isReadOnly ps.modifiers
        IsStatic = hasModifier SyntaxKind.StaticKeyword ps.modifiers
        Accessibility = getAccessibility ps.modifiers
    }

let readPropertyDeclaration (checker: TypeChecker) (pd: PropertyDeclaration): FsProperty =
    {
        Attributes = []
        Comments = readCommentsAtLocation checker (!!pd.name)
        Kind = FsPropertyKind.Regular
        Index = None
        Name = pd.name |> getPropertyName
        Option = pd.questionToken.IsSome
        Type =
            match pd.``type`` with
            | None -> FsType.None
            | Some tp -> readTypeNode checker tp
        Accessor = FsAccessor.fromReadonly <| isReadOnly pd.modifiers
        IsStatic = hasModifier SyntaxKind.StaticKeyword pd.modifiers
        Accessibility = getAccessibility pd.modifiers
    }

let readGetAccessorDeclaration (checker: TypeChecker) (gad: GetAccessorDeclaration): FsProperty =
    {
        Attributes = []
        Comments = readCommentsAtLocation checker (!!gad.name)
        Kind = FsPropertyKind.Regular
        Index = None
        Name = gad.name |> getPropertyName
        Option = gad.questionToken.IsSome
        Type =
            match gad.``type`` with
            | None -> FsType.None
            | Some tp -> readTypeNode checker tp
        Accessor = ReadOnly
        IsStatic = hasModifier SyntaxKind.StaticKeyword gad.modifiers
        Accessibility = getAccessibility gad.modifiers
    }
let readSetAccessorDeclaration (checker: TypeChecker) (sad: SetAccessorDeclaration): FsProperty =
    {
        Attributes = []
        Comments = readCommentsAtLocation checker (!!sad.name)
        Kind = FsPropertyKind.Regular
        Index = None
        Name = sad.name |> getPropertyName
        Option = sad.questionToken.IsSome
        Type =
            // parameter, not return type
            //      in valid TS: EXACTLY one parameter
            if sad.parameters.Count <> 1 then
                FsType.None
            else
                let p = sad.parameters.[0]
                let p = readParameterDeclaration checker 0 p
                p.Type
        Accessor = WriteOnly
        IsStatic = hasModifier SyntaxKind.StaticKeyword sad.modifiers
        Accessibility = getAccessibility sad.modifiers
    }

let readFunctionDeclaration (checker: TypeChecker) (fd: FunctionDeclaration): FsFunction =
    {
        Attributes = []
        Comments = readCommentsForSignatureDeclaration checker fd
        Kind = FsFunctionKind.Regular
        IsStatic = hasModifier SyntaxKind.StaticKeyword fd.modifiers
        Name = fd.name |> Option.map (fun id -> id.getText())
        TypeParameters = readTypeParameters checker fd.typeParameters
        Params = fd.parameters |> List.ofSeq |> List.mapi (readParameterDeclaration checker)
        ReturnType =
            match fd.``type`` with
            | Some t -> readTypeNode checker t
            | None -> simpleType "unit"
        Accessibility = getAccessibility fd.modifiers
    }

let readIndexSignature (checker: TypeChecker) (ps: IndexSignatureDeclaration): FsProperty =
    let pm = readParameterDeclaration checker 0 ps.parameters.[0]
    {
        Attributes = []
        Comments = readCommentsForSignatureDeclaration checker ps
        Kind = FsPropertyKind.Index
        Index = Some pm
        Name = "Item"
        Option = ps.questionToken.IsSome
        Type =
            match ps.``type`` with
            | None -> FsType.None
            | Some tp -> readTypeNode checker tp
        Accessor = FsAccessor.fromReadonly <| isReadOnly ps.modifiers
        IsStatic = hasModifier SyntaxKind.StaticKeyword ps.modifiers
        Accessibility = getAccessibility ps.modifiers
    }

let readCallSignature (checker: TypeChecker) (cs: CallSignatureDeclaration): FsFunction =
    {
        Attributes = []
        Comments = readCommentsForSignatureDeclaration checker cs
        Kind = FsFunctionKind.Call
        IsStatic = false // TODO ?
        Name = Some "Invoke"
        TypeParameters = cs.typeParameters |> readTypeParameters checker
        Params = cs.parameters |> List.ofSeq |> List.mapi (readParameterDeclaration checker)
        ReturnType =
            match cs.``type`` with
            | Some t -> readTypeNode checker t
            | None -> simpleType "unit"
        Accessibility = getAccessibility cs.modifiers
    }

let readConstructSignatureDeclaration (checker: TypeChecker) (cs: ConstructSignatureDeclaration): FsFunction =
    {
        Attributes = []
        Comments = readCommentsForSignatureDeclaration checker cs
        Kind = FsFunctionKind.Constructor
        IsStatic = true
        Name = Some "Create"
        TypeParameters = cs.typeParameters |> (readTypeParameters checker)
        Params = cs.parameters |> List.ofSeq |> List.mapi (readParameterDeclaration checker)
        ReturnType = FsType.This
        Accessibility = getAccessibility cs.modifiers
    }

let readConstructorDeclaration (checker: TypeChecker) (cs: ConstructorDeclaration): FsFunction =
    {
        Attributes = []
        Comments = readCommentsForSignatureDeclaration checker cs
        Kind = FsFunctionKind.Constructor
        IsStatic = true
        Name = Some "Create"
        TypeParameters = cs.typeParameters |> (readTypeParameters checker)
        Params = cs.parameters |> List.ofSeq |> List.mapi (readParameterDeclaration checker)
        ReturnType = FsType.This
        Accessibility = getAccessibility cs.modifiers
    }

let tryReadNamedDeclaration (checker: TypeChecker) (te: NamedDeclaration): FsType option =
    match te.kind with
    | SyntaxKind.IndexSignature ->
        readIndexSignature checker (te :?> IndexSignatureDeclaration) |> FsType.Property |> Some
    | SyntaxKind.MethodSignature ->
        readMethodSignature checker (te :?> MethodSignature) |> FsType.Function |> Some
    | SyntaxKind.PropertySignature ->
        readPropertySignature checker (te :?> PropertySignature) |> FsType.Property |> Some
    | SyntaxKind.CallSignature ->
        readCallSignature checker (te :?> CallSignatureDeclaration) |> FsType.Function |> Some
    | SyntaxKind.ConstructSignature ->
        readConstructSignatureDeclaration checker (te :?> ConstructSignatureDeclaration) |> FsType.Function |> Some
    | SyntaxKind.Constructor ->
        readConstructorDeclaration checker (te :?> ConstructorDeclaration) |> FsType.Function |> Some
    | SyntaxKind.PropertyDeclaration ->
        readPropertyDeclaration checker (te :?> PropertyDeclaration) |> FsType.Property |> Some
    | SyntaxKind.GetAccessor ->
        readGetAccessorDeclaration checker (te :?> GetAccessorDeclaration) |> FsType.Property |> Some
    | SyntaxKind.SetAccessor ->
        readSetAccessorDeclaration checker (te :?> SetAccessorDeclaration) |> FsType.Property |> Some
    | SyntaxKind.MethodDeclaration ->
        readMethodDeclaration checker (te :?> MethodDeclaration) |> FsType.Function |> Some
    | _ -> None

let readNamedDeclaration (checker: TypeChecker) (te: NamedDeclaration): FsType =
    match tryReadNamedDeclaration checker te with
    | Some tp -> tp
    | None -> printfn "unsupported NamedDeclaration kind: %A" te.kind; FsType.TODO

let readAliasDeclaration (checker: TypeChecker) (d: TypeAliasDeclaration): FsType =
    let tp = d.``type`` |> (readTypeNode checker)
    let name = d.name.getText()
    let fullName = getFullName checker d.name
    let asAlias() =
        {
            Attributes = []
            Comments = readCommentsAtLocation checker d.name
            Name = name
            FullName = fullName
            Type = tp
            TypeParameters = readTypeParameters checker d.typeParameters
        }
        |> FsType.Alias
    match tp with
    | FsType.Union un ->
        let sls = un.Types |> List.choose FsType.asStringLiteral
        if un.Types.Length = sls.Length then
            // It is a string literal type. Map it is a string enum.
            {
                Attributes = []
                Comments = readCommentsAtLocation checker d.name
                Name = name
                FullName = fullName
                Cases = sls |> List.map (fun sl ->
                    {
                        Attributes = []
                        Comments = []
                        Name = sl
                        Value = FsLiteral.String sl |> Some
                    }
                )
            }
            |> FsType.Enum
        else asAlias()
    | _ -> asAlias()

let readExpressionText(ep: Expression): string =
    match ep.kind with
    | SyntaxKind.Identifier ->
        let id = ep :?> Identifier
        id.getText()
    | SyntaxKind.PropertyAccessExpression ->
        let pa = ep :?> PropertyAccessExpression
        pa.getText()
    | _ ->
        printfn "readExpressionText kind not yet supported: %A" ep.kind
        ep.getText()

let readExportAssignment(ea: ExportAssignment): FsType =
    match ea.expression.kind with
    | SyntaxKind.Identifier ->
        let path = readExpressionText ea.expression
        FsType.ExportAssignment path
    | _ -> FsType.None

let readImportDeclaration(im: ImportDeclaration): FsType list =
    let moduleSpecifier = im.moduleSpecifier.getText() |> removeQuotes
    match im.importClause with
    | None -> []
    | Some cl ->
        match cl.namedBindings with
        | None -> []
        | Some namedBindings ->
            // match namedBindings with
            // | U2.Case1 namespaceImport ->
                // can't match on erased union, so testing properties instead
                if not (isNull !!namedBindings?name) then
                    [
                        { Module = !!namedBindings?name?getText(); SpecifiedModule = moduleSpecifier; ResolvedModule = None }
                        |> FsImport.Module |> FsType.Import
                    ]
                else
                    let children: ResizeArray<Node> = !!namedBindings?getChildren()
                    children |> List.ofSeq |> List.collect (fun ch ->
                        match ch.kind with
                        | SyntaxKind.SyntaxList  ->
                            let sl = ch :?> SyntaxList
                            sl.getChildren() |> List.ofSeq |> List.choose (fun slch ->
                                match slch.kind with
                                | SyntaxKind.ImportSpecifier ->
                                    let imp = slch :?> ImportSpecifier
                                    { ImportSpecifier =
                                        { Name = imp.name.text
                                          PropertyName = imp.propertyName |> Option.map (fun id ->
                                            id.text) }
                                      SpecifiedModule = moduleSpecifier
                                      ResolvedModule = None }
                                    |> FsImport.Type |> FsType.Import |> Some
                                | _ -> None
                            )
                        | _ -> []
                    )
            // | U2.Case2 namedImports -> []

let tryReadStatement (checker: TypeChecker) (sd: Statement): FsType list option =
    match sd.kind with
    | SyntaxKind.InterfaceDeclaration ->
        Some [readInterface checker (sd :?> InterfaceDeclaration) |> FsType.Interface]
    | SyntaxKind.EnumDeclaration ->
        Some [readEnum checker (sd :?> EnumDeclaration) |> FsType.Enum]
    | SyntaxKind.TypeAliasDeclaration ->
        Some [readAliasDeclaration checker (sd :?> TypeAliasDeclaration)]
    | SyntaxKind.ClassDeclaration ->
        Some [readClass checker (sd :?> ClassDeclaration) |> FsType.Interface]
    | SyntaxKind.VariableStatement ->
        readVariable checker (sd :?> VariableStatement) |> Some
    | SyntaxKind.FunctionDeclaration ->
        Some [readFunctionDeclaration checker (sd :?> FunctionDeclaration) |> FsType.Function]
    | SyntaxKind.ModuleDeclaration ->
        Some [readModuleDeclaration checker (sd :?> ModuleDeclaration) |> FsType.Module]
    | SyntaxKind.ExportAssignment ->
        Some [readExportAssignment(sd :?> ExportAssignment)]
    | SyntaxKind.ImportDeclaration ->
        readImportDeclaration(sd :?> ImportDeclaration) |> Some
    | SyntaxKind.NamespaceExportDeclaration ->
        // let ns = sd :?> NamespaceExportDeclaration
        Some []
    | SyntaxKind.ExportDeclaration ->
        // printfn "TODO export statements"
        Some []
    | SyntaxKind.ImportEqualsDeclaration ->
        let ime = sd :?> ImportEqualsDeclaration
        // printfn "import equals decl %s" (ime.getText())
        Some []
    | _ -> None

let readStatement (checker: TypeChecker) (sd: Statement): FsType list =
    match tryReadStatement checker sd with
    | Some ts -> ts
    | None -> printfn "unsupported Statement kind: %A" sd.kind; []

let readModuleName(mn: ModuleName): string =
    !!mn?getText() |> removeQuotes
    // match mn with
    // | U2.Case1 id -> id.getText().Replace("\"","")
    // | U2.Case2 sl -> sl.getText()

let rec readModuleDeclaration checker (md: ModuleDeclaration): FsModule =
    let types = List()
    md.ForEachChild (fun nd ->
        match nd.kind with
        | SyntaxKind.ModuleBlock ->
            let mb = nd :?> ModuleBlock
            mb.statements |> List.ofSeq |> List.collect (readStatement checker) |> List.iter types.Add
        | SyntaxKind.DeclareKeyword -> ()
        | SyntaxKind.Identifier -> ()
        | SyntaxKind.ModuleDeclaration ->
            readModuleDeclaration checker (nd :?> ModuleDeclaration) |> FsType.Module |> types.Add
        | SyntaxKind.StringLiteral -> ()
        | SyntaxKind.ExportKeyword -> ()
        | _ -> printfn "unknown kind in ModuleDeclaration: %A" nd.kind
    )
    {
        Comments = readCommentsAtLocation checker (!!md.name)
        HasDeclare = hasModifier SyntaxKind.DeclareKeyword md.modifiers
        IsNamespace = isNamespace md
        Name = readModuleName md.name
        Types = types |> List.ofSeq
        HelperLines = []
        Attributes = []
    }

let readSourceFile (checker: TypeChecker) (sf: SourceFile) (file: FsFile): FsFile =
    let md =
        let tps =
            sf.statements
            |> List.ofSeq
            |> List.collect (readStatement checker)
        match file.Kind with
        | FsFileKind.Index ->
            {
                Comments = []
                HasDeclare = false
                IsNamespace = false
                Name = ""
                Types = tps

                HelperLines = []
                Attributes = []
            }
        | FsFileKind.Extra extra ->
            {
                Comments = []
                HasDeclare = true
                IsNamespace = false
                Name = extra |> ModuleName.normalize
                Types = tps
                HelperLines = []
                Attributes = []
            }

    { file with
        Modules = [md]
    }