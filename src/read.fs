module rec ts2fable.Read

open Fable.Core
open Fable.Core.JsInterop
open Node
open TypeScript
open TypeScript.Ts
open System.Collections.Generic
open System
open ts2fable.Naming
open System.Collections.Generic
open ts2fable.Syntax
open System.Collections.Generic
open Fable

type Node with
    member x.ForEachChild (cbNode: Node -> unit) =
        x.forEachChild<unit> (fun nd -> cbNode nd; None) |> ignore

let getPropertyName(pn: PropertyName): string =
    match pn with
    | U4.Case1 id -> id.getText() |> removeQuotes
    | U4.Case2 sl -> sl.getText()
    | U4.Case3 nl -> nl.getText()
    | U4.Case4 cpn -> cpn.getText()

let getBindingName(bn: BindingName): string =
    match bn with
    | U2.Case1 id -> id.getText()
    | U2.Case2 bp -> // BindingPattern
        match bp with
        | U2.Case1 obp -> obp.getText()
        | U2.Case2 abp -> abp.getText()

let readEnumCase(em: EnumMember): FsEnumCase =
    let name = em.name |> getPropertyName
    let tp, value =
        match em.initializer with
        | None -> FsEnumCaseType.Unknown, None
        | Some ep ->
            match ep.kind with
            | SyntaxKind.NumericLiteral ->
                let nl = ep :?> NumericLiteral
                FsEnumCaseType.Numeric, Some nl.text
            | SyntaxKind.StringLiteral ->
                let sl = ep :?> StringLiteral
                FsEnumCaseType.String, Some sl.text
            | SyntaxKind.PrefixUnaryExpression ->
                let pue = ep :?> PrefixUnaryExpression
                // a negative number such as -1 is the usual case
                let txt = pue.getText()
                let parsed, _ = Int32.TryParse txt
                if parsed then
                    FsEnumCaseType.Numeric, Some txt
                else
                    FsEnumCaseType.Unknown, None
            | _ -> failwithf "EnumCase type not supported %A %A" ep.kind name
    {
        Name = name
        Type = tp
        Value = value
    }

let readTypeParameters (checker: TypeChecker) (tps: List<TypeParameterDeclaration> option): FsType list =
    match tps with
    | None -> []
    | Some tps ->
        tps |> List.ofSeq |> List.map (fun tp ->
            {
                Name = tp.name.getText()
                FullName = getFullNodeName checker tp
            }
            |> FsType.Mapped
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

let readCommentLines (text: string): string list =
    text.Replace("\r\n","\n").Replace("\r","\n").Split [|'\n'|] |> List.ofArray

let readComments (comments: List<SymbolDisplayPart>): FsComment list =
    if comments.Count = 0 then []
    else
        comments |> List.ofSeq |> List.collect (fun dp ->
            match dp.kind with
            // | SymbolDisplayPartKind.Text -> // TODO how to use the enum
            | "text" -> dp.text |> readCommentLines |> List.map FsComment.SummaryLine
            | _ -> []
        )

let readCommentTags (nd: Node) =
    match ts.getJSDocTags nd with
    | None -> List.empty
    | Some tags ->
        tags |> List.ofSeq |> List.collect (fun tag ->
            match tag.kind with
            | SyntaxKind.JSDocParameterTag ->
                match tag.comment with
                | None -> []
                | Some comment -> [{ Name = ""; Description = readCommentLines comment} |> FsComment.Param]
            | _ ->
                // printfn "uknown comment kind tag kind %A %A" tag.kind tag.comment
                [tag.comment |> FsComment.Unknown]
        )

let readCommentsForSignatureDeclaration (checker: TypeChecker) (declaration: SignatureDeclaration): FsComment list =
    match checker.getSignatureFromDeclaration declaration with
    | None -> []
    | Some signature ->
        signature.getDocumentationComment() |> readComments

let readCommentsAtLocation (checker: TypeChecker) (nd: Node): FsComment list =
    match checker.getSymbolAtLocation nd with
    | None -> []
    | Some symbol ->
        symbol.getDocumentationComment() |> readComments

let readInterface (checker: TypeChecker) (id: InterfaceDeclaration): FsType list=
    let name = id.name.getText()
    let typeParameters = readTypeParameters checker id.typeParameters
    
    // typescript optional generic type 
    let ops =
        match id.typeParameters with 
        | Some tps -> 
            let als = List<FsAlias>()
            tps 
            |> List.ofSeq
            |> List.iteri(fun i tp ->
                match tp.``default`` with 
                | Some _ -> 
                    { Name = name
                      Type = 
                        { 
                            Type = simpleType name
                            TypeParameters = 
                                (typeParameters.[0 .. i] |> List.map(fun _ -> simpleType "obj"))
                                @ typeParameters.[i+1 ..]
                        } |> FsType.Generic
                      TypeParameters = typeParameters.[i+1 ..]
                    } 
                    |> als.Add
                | None -> ()
            )
            als |> List.ofSeq |> List.map FsType.Alias  
        | None -> []

    let current = 
        {
            Comments = readCommentsAtLocation checker id.name
            IsStatic = false
            IsClass = false
            Name = name
            FullName = getFullNodeName checker id
            Inherits = readInherits checker id.heritageClauses
            Members = id.members |> List.ofSeq |> List.map (readNamedDeclaration checker)
            TypeParameters = readTypeParameters checker id.typeParameters
        } |> FsType.Interface
    current :: ops     

let readTypeLiteral (checker: TypeChecker) (tl: TypeLiteralNode): FsTypeLiteral =
    {
        Members = tl.members |> List.ofSeq |> List.map (readNamedDeclaration checker)
    }

let getFullTypeName (checker: TypeChecker) (tp: Ts.Type) =
    match tp.symbol with
    | None -> ""
    | Some smb -> checker.getFullyQualifiedName smb

let getFullNodeName (checker: TypeChecker) (nd: Node) =
    // getFullTypeName checker (checker.getTypeAtLocation nd)
    match checker.getSymbolAtLocation nd with
    | None -> ""
    | Some smb -> checker.getFullyQualifiedName smb

let readClass (checker: TypeChecker) (cd: ClassDeclaration): FsInterface =
    let fullName = getFullNodeName checker cd
    {
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
    }

let hasModifier (kind: SyntaxKind) (modifiers: ModifiersArray option) =
    match modifiers with
    | None -> false
    | Some mds -> mds |> Seq.exists (fun md -> md.kind = kind)

let isConst (nd: Node): bool =
    (int nd.flags) ||| (int NodeFlags.Const) <> 0

let isNamespace (nd: Node): bool =
    nd.getChildren() |> Seq.exists (fun nd -> nd.kind = SyntaxKind.NamespaceKeyword)

let readVariable (checker: TypeChecker) (vb: VariableStatement) =
    vb.declarationList.declarations |> List.ofSeq |> List.map (fun vd ->
        {
            Export = None
            HasDeclare = hasModifier SyntaxKind.DeclareKeyword vb.modifiers
            Name = vd.name |> getBindingName
            Type = vd.``type`` |> Option.map (readTypeNode checker) |> Option.defaultValue (simpleType "obj")
            IsConst = isConst vd
        }
        |> FsType.Variable
    )

let readEnum(ed: EnumDeclaration): FsEnum =
    {
        Name = ed.name.getText()
        Cases = ed.members |> List.ofSeq |> List.map readEnumCase
    }

let readTypeReference (checker: TypeChecker) (tr: TypeReferenceNode): FsType =

    match tr.typeArguments with
    | None ->
        {
            Name = tr.getText()
            FullName = getFullNodeName checker tr
        }
        |> FsType.Mapped
    | Some tas ->
        {
            Type =
                let typeName =
                    match tr.typeName with
                    | U2.Case1 id -> id.getText()
                    | U2.Case2 qn -> qn.getText()
                if isNull typeName then
                    failwithf "readTypeReference type name is null: %s" (tr.getText())
                {
                    Name = typeName
                    FullName = getFullNodeName checker tr
                }
                |> FsType.Mapped
            TypeParameters =
                tas |> List.ofSeq |> List.map (readTypeNode checker)
        }
        |> FsType.Generic
let readFunctionType (checker: TypeChecker) (ft: FunctionTypeNode): FsFunction =
    {
        // TODO https://github.com/fable-compiler/ts2fable/issues/68
        Comments = []//ft.name |> Option.map (readPropertyNameComments checker) |> Option.defaultValue []
        Kind = FsFunctionKind.Regular
        IsStatic = hasModifier SyntaxKind.StaticKeyword ft.modifiers
        Name = ft.name |> Option.map getPropertyName
        TypeParameters = readTypeParameters checker ft.typeParameters
        Params =  ft.parameters |> List.ofSeq |> List.map (readParameterDeclaration checker)
        ReturnType =
            match ft.``type`` with
            | Some t -> readTypeNode checker t
            | None -> simpleType "unit"
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
        let un = t :?> UnionTypeNode
        let typs = un.types |> List.ofSeq
        let isOption (t:TypeNode) = t.kind = SyntaxKind.UndefinedKeyword || t.kind = SyntaxKind.NullKeyword
        {
            Option = typs |> List.exists isOption
            Types =
                let ts = typs |> List.filter (isOption >> not) |> List.map (readTypeNode checker)
                if ts.Length = 0 then [simpleType "obj"]
                else ts
        }
        |> FsType.Union
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
            IsIntersection = false
            IsMapped = false
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
            IsIntersection = true
            IsMapped = false
        }
        |> FsType.Tuple        
        // simpleType "obj"
    | SyntaxKind.IndexedAccessType ->
        let ia = t :?> IndexedAccessTypeNode
        readTypeNode checker ia.objectType
        // function createKeywordTypeNode(kind: KeywordTypeNode["kind"]): KeywordTypeNode;
        // simpleType "obj" // TODO?
    | SyntaxKind.TypeQuery ->
        // let tq = t :?> TypeQueryNode
        simpleType "obj"
    | SyntaxKind.LiteralType -> 
        let lt = t :?> LiteralTypeNode
        let readLiteralKind (kind: SyntaxKind) text: FsType =
            match kind with
            | SyntaxKind.StringLiteral ->
                FsType.StringLiteral (text |> removeQuotes)
            | _ -> simpleType "obj"
        match lt.literal with
        | U3.Case1 bl -> readLiteralKind bl.kind (bl.getText())
        | U3.Case2 le -> readLiteralKind le.kind (le.getText())
        | U3.Case3 pue -> readLiteralKind pue.kind (pue.getText())
    | SyntaxKind.ExpressionWithTypeArguments ->
        let eta = t :?> ExpressionWithTypeArguments
        let tp = checker.getTypeFromTypeNode eta
        {
            Name = readExpressionText eta.expression
            FullName =  getFullTypeName checker tp
        }
        |> FsType.Mapped
    | SyntaxKind.ParenthesizedType ->
        let pt = t :?> ParenthesizedTypeNode
        // just get the type in parenthesis
        readTypeNode checker pt.``type``
    | SyntaxKind.MappedType ->
        let mt = t :?> MappedTypeNode
        {
            Types = []
            IsIntersection = false
            IsMapped = true
        }
        |> FsType.Tuple   
        // TODO map mapped types https://github.com/fable-compiler/ts2fable/issues/44
        // printfn "TODO mapped types %s" (mt.getText())
        // simpleType "obj"
    | SyntaxKind.NeverKeyword ->
        // printfn "unsupported TypeNode NeverKeyword: %A" t
        // simpleType "obj"
        FsType.TODO
    | SyntaxKind.UndefinedKeyword -> simpleType "obj"
    | SyntaxKind.NullKeyword -> FsType.TODO // It should be an option
    | SyntaxKind.ObjectKeyword -> simpleType "obj"
    | SyntaxKind.TypeOperator ->
        printfn "unsupported TypeNode TypeOperator: %A" t
        FsType.TODO // jQuery
    | _ ->
        printfn "unsupported TypeNode kind: %A" t.kind
        FsType.TODO

let readParameterDeclaration (checker: TypeChecker) (pd: ParameterDeclaration): FsParam =
    let stringLiteral =
        pd.getChildren() |> List.ofSeq |> List.tryPick (fun ch ->
            ch.getChildren() |> List.ofSeq |> List.tryFind (fun gch -> gch.kind = SyntaxKind.StringLiteral)
        )
    let tp =     
        match stringLiteral with
        | Some sl -> FsType.StringLiteral (sl.getText() |> removeQuotes)
        | None ->
            match pd.``type`` with
            | Some t -> readTypeNode checker t
            | None -> simpleType "obj"
    
    let name = pd.name |> getBindingName
    {
        Comment =
            readCommentTags pd
            |> List.tryFind FsComment.isParam
            |> Option.map (fun comment ->
                match comment with
                | FsComment.Param c -> { c with Name = name} |> FsComment.Param
                | _ -> comment
            )
        Name = name
        Optional = pd.questionToken.IsSome
        ParamArray = pd.dotDotDotToken.IsSome
        Type = tp
    }

let readMethodSignature (checker: TypeChecker) (ms: MethodSignature): FsFunction =
    {
        Comments = readCommentsForSignatureDeclaration checker ms
        Kind = FsFunctionKind.Regular
        IsStatic = hasModifier SyntaxKind.StaticKeyword ms.modifiers
        Name = ms.name |> getPropertyName |> Some
        TypeParameters = readTypeParameters checker ms.typeParameters
        Params = ms.parameters |> List.ofSeq |> List.map (readParameterDeclaration checker)
        ReturnType =
            match ms.``type`` with
            | Some t -> readTypeNode checker t
            | None -> simpleType "unit"
    }

let readMethodDeclaration checker (md: MethodDeclaration): FsFunction =
    {
        Comments = readCommentsForSignatureDeclaration checker md
        Kind = FsFunctionKind.Regular
        IsStatic = hasModifier SyntaxKind.StaticKeyword md.modifiers
        Name = md.name |> getPropertyName |> Some
        TypeParameters = readTypeParameters checker md.typeParameters
        Params = md.parameters |> List.ofSeq |> List.map (readParameterDeclaration checker)
        ReturnType =
            match md.``type`` with
            | Some t -> readTypeNode checker t
            | None -> simpleType "unit"
    }

let isReadOnly (modifiers: ModifiersArray option) =
    hasModifier SyntaxKind.ReadonlyKeyword modifiers

let readPropertySignature (checker: TypeChecker) (ps: PropertySignature): FsProperty =
    {
        Comments = readPropertyNameComments checker ps.name
        Kind = FsPropertyKind.Regular
        Index = None
        Name = ps.name |> getPropertyName
        Option = ps.questionToken.IsSome
        Type = 
            match ps.``type`` with
            | None -> FsType.None 
            | Some tp -> readTypeNode checker tp
        IsReadonly = isReadOnly ps.modifiers
    }

let readPropertyNameComments (checker: TypeChecker) (pn: PropertyName): FsComment list =
    match pn with
    | U4.Case1 id -> readCommentsAtLocation checker id
    | U4.Case2 sl -> readCommentsAtLocation checker sl
    | U4.Case3 nl -> readCommentsAtLocation checker nl
    | U4.Case4 cpn -> readCommentsAtLocation checker cpn

let readPropertyDeclaration (checker: TypeChecker) (pd: PropertyDeclaration): FsProperty =
    {
        Comments = readPropertyNameComments checker pd.name
        Kind = FsPropertyKind.Regular
        Index = None
        Name = pd.name |> getPropertyName
        Option = pd.questionToken.IsSome
        Type = 
            match pd.``type`` with
            | None -> FsType.None 
            | Some tp -> readTypeNode checker tp
        IsReadonly = isReadOnly pd.modifiers
    }

let readFunctionDeclaration (checker: TypeChecker) (fd: FunctionDeclaration): FsFunction =
    {     
        Comments = readCommentsForSignatureDeclaration checker fd
        Kind = FsFunctionKind.Regular
        IsStatic = hasModifier SyntaxKind.StaticKeyword fd.modifiers
        Name = fd.name |> Option.map (fun id -> id.getText())
        TypeParameters = readTypeParameters checker fd.typeParameters
        Params = fd.parameters |> List.ofSeq |> List.map (readParameterDeclaration checker)
        ReturnType =
            match fd.``type`` with
            | Some t -> readTypeNode checker t
            | None -> simpleType "unit"
    }

let readIndexSignature (checker: TypeChecker) (ps: IndexSignatureDeclaration): FsProperty =
    let pm = readParameterDeclaration checker ps.parameters.[0]
    {
        Comments = readCommentsForSignatureDeclaration checker ps
        Kind = FsPropertyKind.Index
        Index = Some pm
        Name = "Item"
        Option = ps.questionToken.IsSome
        Type = 
            match ps.``type`` with
            | None -> FsType.None 
            | Some tp -> readTypeNode checker tp
        IsReadonly = isReadOnly ps.modifiers
    }

let readCallSignature (checker: TypeChecker) (cs: CallSignatureDeclaration): FsFunction =
    {
        Comments = readCommentsForSignatureDeclaration checker cs
        Kind = FsFunctionKind.Call
        IsStatic = false // TODO ?
        Name = Some "Invoke"
        TypeParameters = []
        Params = cs.parameters |> List.ofSeq |> List.map (readParameterDeclaration checker)
        ReturnType =
            match cs.``type`` with
            | Some t -> readTypeNode checker t
            | None -> simpleType "unit"
    }

let readConstructSignatureDeclaration (checker: TypeChecker) (cs: ConstructSignatureDeclaration): FsFunction =
    {
        Comments = readCommentsForSignatureDeclaration checker cs
        Kind = FsFunctionKind.Constructor
        IsStatic = true
        Name = Some "Create"
        TypeParameters = cs.typeParameters |> (readTypeParameters checker)
        Params = cs.parameters |> List.ofSeq |> List.map (readParameterDeclaration checker)
        ReturnType = FsType.This
    }

let readConstructorDeclaration (checker: TypeChecker) (cs: ConstructorDeclaration): FsFunction =
    {
        Comments = readCommentsForSignatureDeclaration checker cs
        Kind = FsFunctionKind.Constructor
        IsStatic = true
        Name = Some "Create"
        TypeParameters = cs.typeParameters |> (readTypeParameters checker)
        Params = cs.parameters |> List.ofSeq |> List.map (readParameterDeclaration checker)
        ReturnType = FsType.This
    }

let readNamedDeclaration (checker: TypeChecker) (te: NamedDeclaration): FsType =
    match te.kind with
    | SyntaxKind.IndexSignature ->
        readIndexSignature checker (te :?> IndexSignatureDeclaration) |> FsType.Property
    | SyntaxKind.MethodSignature ->
        readMethodSignature checker (te :?> MethodSignature) |> FsType.Function
    | SyntaxKind.PropertySignature ->
        readPropertySignature checker (te :?> PropertySignature) |> FsType.Property
    | SyntaxKind.CallSignature ->
        readCallSignature checker (te :?> CallSignatureDeclaration) |> FsType.Function
    | SyntaxKind.ConstructSignature ->
        readConstructSignatureDeclaration checker (te :?> ConstructSignatureDeclaration) |> FsType.Function
    | SyntaxKind.Constructor ->
        readConstructorDeclaration checker (te :?> ConstructorDeclaration) |> FsType.Function
    | SyntaxKind.PropertyDeclaration ->
        readPropertyDeclaration checker (te :?> PropertyDeclaration) |> FsType.Property
    | SyntaxKind.MethodDeclaration ->
        readMethodDeclaration checker (te :?> MethodDeclaration) |> FsType.Function
    | _ -> printfn "unsupported NamedDeclaration kind: %A" te.kind; FsType.TODO

let readAliasDeclaration (checker: TypeChecker) (d: TypeAliasDeclaration): FsType list =
    let tp = d.``type`` |> (readTypeNode checker)
    let name = d.name.getText()
    let asAlias() =
        let current = 
            {
                Name = name
                Type = tp
                TypeParameters = readTypeParameters checker d.typeParameters
            }
            |> FsType.Alias
        
        //typescript optional generic type
        let op =
            match d.typeParameters with 
            | Some tps -> 
                match tps.[0].``default`` with 
                | Some _ -> 
                    {
                        Name = name
                        Type = simpleType "obj"
                        TypeParameters = []
                    } |> FsType.Alias |> List.singleton               
                | _ -> []
            | None -> []
        current :: op    
               
    match tp with
    | FsType.Union un ->
        let sls = un.Types |> List.choose FsType.asStringLiteral
        if un.Types.Length = sls.Length then
            // It is a string literal type. Map it is a string enum.
            {
                Name = name
                Cases = sls |> List.map (fun sl ->
                    {
                        Name = sl
                        Type = FsEnumCaseType.String
                        Value = None
                    }
                )
            }
            |> FsType.Enum |> List.singleton
        else asAlias()
    | FsType.Function f ->
        let f = { f with Name = Some "invoke" }
        {
            Comments = f.Comments
            IsStatic = false
            IsClass = false
            Name = name
            FullName = name
            Inherits = []
            Members = f |> FsType.Function |> List.singleton
            TypeParameters = readTypeParameters checker d.typeParameters
        } |> FsType.Interface |> List.singleton
   
    | FsType.Tuple tl when tl.IsIntersection ->  
        {
            Comments = []
            IsStatic = false
            IsClass = false
            Name = name
            FullName = name
            Inherits = tl.Types |> List.ofSeq |> List.filter FsType.isGeneric
            Members = []
            TypeParameters = readTypeParameters checker d.typeParameters
        } |> FsType.Interface |> List.singleton  
    
    | FsType.Tuple tl when tl.IsMapped -> 
        {
            Comments = []
            IsStatic = false
            IsClass = false
            Name = name
            FullName = name
            Inherits = tl.Types |> List.ofSeq |> List.filter FsType.isGeneric
            Members = []
            TypeParameters = readTypeParameters checker d.typeParameters
        } |> FsType.Interface |> List.singleton      
    | FsType.TypeLiteral lt -> 
        if lt.Members.Length = 1 then
            match lt.Members.[0] with 
            | FsType.Function f -> 
                let f ={ f with Name = Some "invoke" }
                {
                    Comments = f.Comments
                    IsStatic = false
                    IsClass = false
                    Name = name
                    FullName = name
                    Inherits = []
                    Members = f |> FsType.Function |> List.singleton
                    TypeParameters = readTypeParameters checker d.typeParameters
                } |> FsType.Interface |> List.singleton      
            | _ -> asAlias()
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
            match namedBindings with
            | U2.Case1 namespaceImport ->
                if isNull namespaceImport.name = false then
                    [
                        { Module = namespaceImport.name.getText(); SpecifiedModule = moduleSpecifier; ResolvedModule = None; Role = FsModuleImportRole.CurrentPackage }
                        |> FsImport.Module |> FsType.Import
                    ]
                else
                    namespaceImport.getChildren() |> List.ofSeq |> List.collect (fun ch ->
                        match ch.kind with
                        | SyntaxKind.SyntaxList  ->
                            let sl = ch :?> SyntaxList
                            sl.getChildren() |> List.ofSeq |> List.choose (fun slch ->
                                match slch.kind with
                                | SyntaxKind.ImportSpecifier ->
                                    let imp = slch :?> ImportSpecifier
                                    { Type = imp.getText(); SpecifiedModule = moduleSpecifier; ResolvedModule = None }
                                    |> FsImport.Type |> FsType.Import |> Some
                                | _ -> None
                            )
                        | _ -> []
                    )
            | U2.Case2 namedImports -> []

let readImportEqualsDeclaration(im: ImportEqualsDeclaration): FsType list =
    match im.moduleReference with
        | U2.Case1 entityName ->
              match entityName with
              | U2.Case1 id -> 
                    { Module = im.name.getText()
                      SpecifiedModule = id.getText().Replace("require","") |> removeParentheses |> removeQuotes
                      ResolvedModule = None
                      Role = id.getText()|> FsModuleImportRole.getRoleFromSpecifiedName}
                    |> FsImport.Module |> FsType.Import |> List.singleton
              | U2.Case2 _ -> []
        | U2.Case2 _ -> []
let readStatement (checker: TypeChecker) (sd: Statement): FsType list =
    match sd.kind with
    | SyntaxKind.InterfaceDeclaration ->
        readInterface checker (sd :?> InterfaceDeclaration)
    | SyntaxKind.EnumDeclaration ->
        [readEnum (sd :?> EnumDeclaration) |> FsType.Enum]
    | SyntaxKind.TypeAliasDeclaration ->
        readAliasDeclaration checker (sd :?> TypeAliasDeclaration)
    | SyntaxKind.ClassDeclaration ->
        [readClass checker (sd :?> ClassDeclaration) |> FsType.Interface]
    | SyntaxKind.VariableStatement ->
        readVariable checker (sd :?> VariableStatement)
    | SyntaxKind.FunctionDeclaration ->
        [readFunctionDeclaration checker (sd :?> FunctionDeclaration) |> FsType.Function]
    | SyntaxKind.ModuleDeclaration ->
        [readModuleDeclaration checker (sd :?> ModuleDeclaration) |> FsType.Module]
    | SyntaxKind.ExportAssignment ->
        [readExportAssignment(sd :?> ExportAssignment)]
    | SyntaxKind.ImportDeclaration ->
        readImportDeclaration(sd :?> ImportDeclaration)
    | SyntaxKind.NamespaceExportDeclaration ->
        // let ns = sd :?> NamespaceExportDeclaration
        []
    | SyntaxKind.ExportDeclaration ->
        // printfn "TODO export statements"
        []
    | SyntaxKind.ImportEqualsDeclaration ->
        readImportEqualsDeclaration(sd :?> ImportEqualsDeclaration)
        // let ime = sd :?> ImportEqualsDeclaration
        // printfn "import equals decl %s" (ime.getText())
        // []
    | _ -> printfn "unsupported Statement kind: %A" sd.kind; []

let readModuleName(mn: ModuleName): string =
    match mn with
    | U2.Case1 id -> id.getText().Replace("\"","")
    | U2.Case2 sl -> sl.getText()

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
        HasDeclare = hasModifier SyntaxKind.DeclareKeyword md.modifiers
        IsNamespace = isNamespace md
        Name = readModuleName md.name
        Types = types |> List.ofSeq
        HelperLines = []
        Attributes = []
    }

let readSourceFile (checker: TypeChecker) (sf: SourceFile) (file: FsFile): FsFile =
    let modules = List()
    
    let gbl: FsModule =
        {
            HasDeclare = false
            IsNamespace = false
            Name = ""
            Types =
                sf.statements
                |> List.ofSeq
                |> List.collect (readStatement checker)
            HelperLines = []
            Attributes = []
        }
    modules.Add gbl

    { file with
        Modules = modules |> List.ofSeq
    }

//recursively get all resolveModuleNames
let readAllResolvedModuleNames tsPath = 
    let accum = Dictionary<string,FsModuleImportRole>()
       
    let rec readResolveModuleNames (program:Program) tsPath = 
        let tsFile = tsPath |> program.getSourceFile
        tsFile.resolvedModules 
        |> 
            function 
            | Some v ->
                v
                |> List.ofSeq 
                |> List.map(|KeyValue|)
                |> List.iter (fun (k,v) ->
                    let name = v.resolvedFileName.Replace("\\","/")
                    if not <| accum.ContainsKey name 
                    then
                        if k.Contains "./" 
                        then 
                            accum.Add (name,FsModuleImportRole.CurrentPackage)
                            name 
                            |> readResolveModuleNames program
                            |> ignore
                        else     
                            accum.Add (name,FsModuleImportRole.NodePackage)
                )
            | None -> ()
         
        accum

    // let workSpaceRoot = ``process``.cwd()
    // let tsPath = path.join(ResizeArray [workSpaceRoot; tsPath])
    accum.Add (tsPath,FsModuleImportRole.CurrentPackage)

    let options = jsOptions<Ts.CompilerOptions>(fun o ->
        o.target <- Some ScriptTarget.ES2015
        o.``module`` <- Some ModuleKind.CommonJS
    )
    let setParentNodes = true
    let host = ts.createCompilerHost(options, setParentNodes)
    let program = ts.createProgram(ResizeArray [tsPath], options, host)

    let tsFile = program.getSourceFile tsPath
    let tsDir = path.dirname tsPath    
    tsFile.referencedFiles 
    |> List.ofSeq
    |> List.iter(fun f -> 
        let name = path.join(ResizeArray [tsDir;f.fileName]).Replace("\\","/")
        accum.Add (name,FsModuleImportRole.CurrentPackage) )  

    readResolveModuleNames (program:Program) tsPath 
    |> Seq.map(|KeyValue|) 
    |> List.ofSeq 
    |> List.partition (fun (_,v) -> FsModuleImportRole.isNodePackage v)
    |> fun (f,s) -> f |> List.map fst, s |> List.map fst  
