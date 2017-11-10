module rec ts2fable.Read

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import.Node
open Fable.Import.TypeScript
open Fable.Import.TypeScript.ts
open System.Collections.Generic
open System

type Node with
    member x.ForEachChild (cbNode: Node -> unit) =
        x.forEachChild<unit> (fun nd -> cbNode nd; None) |> ignore

let getPropertyName(pn: PropertyName): string =
    match pn with
    | U4.Case1 id -> id.getText().Replace("\"","")
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
            | SyntaxKind.PrefixUnaryExpression -> // TODO TypeScript Ternary
                FsEnumCaseType.Unknown, None
            | _ -> failwithf "EnumCase type not supported %A %A" ep.kind name
    {
        Name = name
        Type = tp
        Value = value
    }

let readTypeParameters(tps: ResizeArray<TypeParameterDeclaration> option): FsType list =
    match tps with
    | None -> []
    | Some tps ->
        tps |> List.ofSeq |> List.map (fun tp ->
            tp.name.getText() |> FsType.Mapped
        )

let readInherits(hcs: ResizeArray<HeritageClause> option): FsType list =
    match hcs with
    | None -> []
    | Some hcs ->
        hcs |> List.ofSeq |> List.collect (fun hc ->
            hc.types |> List.ofSeq |> List.map (fun eta ->
                {
                    Type = readTypeNode eta
                    TypeParameters =
                        match eta.typeArguments with
                        | None -> []
                        | Some tps ->
                            tps |> List.ofSeq |> List.map readTypeNode
                }
                |> FsType.Generic
            )
        )

let readInterface(id: InterfaceDeclaration): FsInterface =
    {
        IsStatic = false
        Name = id.name.getText()
        Inherits = readInherits id.heritageClauses
        Members = id.members |> List.ofSeq |> List.map readNamedDeclaration
        TypeParameters = readTypeParameters id.typeParameters
    }

let readClass(cd: ClassDeclaration): FsInterface =
    {
        IsStatic = false
        Name =
            match cd.name with
            | None -> "TODO_NoClassName"
            | Some id -> id.getText()
        Inherits = readInherits cd.heritageClauses
        Members = cd.members |> List.ofSeq |> List.map readNamedDeclaration
        TypeParameters = readTypeParameters cd.typeParameters
    }

let hasModifier (kind: SyntaxKind) (modifiers: ModifiersArray option) =
    match modifiers with
    | None -> false
    | Some mds -> mds |> Seq.exists (fun md -> md.kind = kind)

let readVariable(vb: VariableStatement): FsVariable =
    let vd = vb.declarationList.declarations.[0] // TODO more than 1
    {
        HasDeclare = hasModifier SyntaxKind.DeclareKeyword vb.modifiers
        Name = vd.name |> getBindingName
        Type = vd.``type`` |> Option.map readTypeNode |> Option.defaultValue (FsType.Mapped "obj")
    }

let readEnum(ed: EnumDeclaration): FsEnum =
    {
        Name = ed.name.getText()
        Cases = ed.members |> List.ofSeq |> List.map readEnumCase
    }

let readTypeReference(tr: TypeReferenceNode): FsType =
    match tr.typeArguments with
    | None ->
        tr.getText() |> FsType.Mapped
    | Some tas ->
        {
            Type =
                let typeName =
                    match tr.typeName with
                    | U2.Case1 id -> id.getText()
                    | U2.Case2 qn -> qn.getText()
                if isNull typeName then
                    failwithf "readTypeReference type name is null: %s" (tr.getText())
                typeName |> FsType.Mapped
            TypeParameters =
                tas |> List.ofSeq |> List.map readTypeNode
        }
        |> FsType.Generic
let readFunctionType(ft: FunctionTypeNode): FsFunction =
    {
        Emit = None
        IsStatic = hasModifier SyntaxKind.StaticKeyword ft.modifiers
        Name = ft.name |> Option.map getPropertyName
        TypeParameters = readTypeParameters ft.typeParameters
        Params =  ft.parameters |> List.ofSeq |> List.map readParameterDeclaration
        ReturnType =
            match ft.``type`` with
            | Some t -> readTypeNode t
            | None -> FsType.Mapped "unit"
    }

let removeQuotes (s:string): string =
    if isNull s then ""
    else s.Replace("\"","").Replace("'","")

let rec readTypeNode(t: TypeNode): FsType =
    match t.kind with
    | SyntaxKind.StringKeyword -> FsType.Mapped "string"
    | SyntaxKind.FunctionType ->
        readFunctionType (t :?> FunctionTypeNode) |> FsType.Function
    | SyntaxKind.TypeReference ->
        readTypeReference (t :?> TypeReferenceNode)
    | SyntaxKind.ArrayType ->
        let at = t :?> ArrayTypeNode
        FsType.Array (readTypeNode at.elementType)
    | SyntaxKind.NumberKeyword -> FsType.Mapped "float"
    | SyntaxKind.BooleanKeyword -> FsType.Mapped "bool"
    | SyntaxKind.UnionType ->
        let un = t :?> UnionTypeNode
        let typs = un.types |> List.ofSeq
        let isOption (t:TypeNode) = t.kind = SyntaxKind.UndefinedKeyword || t.kind = SyntaxKind.NullKeyword
        {
            Option = typs |> List.exists isOption
            Types =
                let ts = typs |> List.filter (isOption >> not) |> List.map readTypeNode
                if ts.Length = 0 then [FsType.Mapped "obj"]
                else ts
        }
        |> FsType.Union
    | SyntaxKind.AnyKeyword -> FsType.Mapped "obj"
    | SyntaxKind.VoidKeyword -> FsType.Mapped "unit"
    | SyntaxKind.TupleType ->
        let tp = t :?> TupleTypeNode
        {
            Types = tp.elementTypes |> List.ofSeq |> List.map readTypeNode
        }
        |> FsType.Tuple
    | SyntaxKind.SymbolKeyword -> FsType.Mapped "Symbol"
    | SyntaxKind.ThisType -> FsType.This
    | SyntaxKind.TypePredicate -> FsType.Mapped "bool"
    | SyntaxKind.TypeLiteral ->
        let tl = t :?> TypeLiteralNode
        // printfn "TypeLiteral %A" tl
        // printfn "  %s" (tl.getText())
        FsType.Mapped "obj"
    | SyntaxKind.IntersectionType -> FsType.Mapped "obj"
    | SyntaxKind.IndexedAccessType ->
        // function createKeywordTypeNode(kind: KeywordTypeNode["kind"]): KeywordTypeNode;
        FsType.Mapped "obj" // TODO?
    | SyntaxKind.TypeQuery ->
        // let tq = t :?> TypeQueryNode
        FsType.Mapped "obj"
    | SyntaxKind.LiteralType ->
        let lt = t :?> LiteralTypeNode
        match lt.literal.kind with
        | SyntaxKind.StringLiteral ->
            FsType.StringLiteral (lt.literal.getText() |> removeQuotes)
        | _ ->
            FsType.Mapped "obj"
    | SyntaxKind.ExpressionWithTypeArguments ->
        let eta = t :?> ExpressionWithTypeArguments
        readExpressionText eta.expression |> FsType.Mapped
    | SyntaxKind.ParenthesizedType -> FsType.Mapped "obj"
    | SyntaxKind.MappedType ->
        let mt = t :?> MappedTypeNode
        // TODO map mapped types https://github.com/fable-compiler/ts2fable/issues/44
        // printfn "TODO mapped types %s" (mt.getText())
        FsType.Mapped "obj"
    | SyntaxKind.NeverKeyword -> FsType.Mapped "unit"
    | SyntaxKind.UndefinedKeyword -> FsType.Mapped "obj"
    | SyntaxKind.NullKeyword -> FsType.TODO // It should be an option
    | SyntaxKind.ObjectKeyword -> FsType.Mapped "obj"
    | SyntaxKind.TypeOperator -> FsType.TODO // jQuery
    | _ -> printfn "unsupported TypeNode kind: %A" t.kind; FsType.TODO

let readParameterDeclaration(pd: ParameterDeclaration): FsParam =
    {
        Name = pd.name |> getBindingName
        Optional = pd.questionToken.IsSome
        ParamArray = pd.dotDotDotToken.IsSome
        Type = 
            match pd.``type`` with
            | Some t -> readTypeNode t
            | None -> FsType.Mapped "obj"
    }

let readMethodSignature(ms: MethodSignature): FsFunction =
    {
        Emit = None
        IsStatic = hasModifier SyntaxKind.StaticKeyword ms.modifiers
        Name = ms.name |> getPropertyName |> Some
        TypeParameters = readTypeParameters ms.typeParameters
        Params = ms.parameters |> List.ofSeq |> List.map readParameterDeclaration
        ReturnType =
            match ms.``type`` with
            | Some t -> readTypeNode t
            | None -> FsType.Mapped "unit"
    }

let readMethodDeclaration(ms: MethodDeclaration): FsFunction =
    {
        Emit = None
        IsStatic = hasModifier SyntaxKind.StaticKeyword ms.modifiers
        Name = ms.name |> getPropertyName |> Some
        TypeParameters = readTypeParameters ms.typeParameters
        Params = ms.parameters |> List.ofSeq |> List.map readParameterDeclaration
        ReturnType =
            match ms.``type`` with
            | Some t -> readTypeNode t
            | None -> FsType.Mapped "unit"
    }
let readPropertySignature(ps: PropertySignature): FsProperty =
    {
        Emit = None
        Index = None
        Name = ps.name |> getPropertyName
        Option = ps.questionToken.IsSome
        Type = 
            match ps.``type`` with
            | None -> FsType.None 
            | Some tp -> readTypeNode tp
    }

let readPropertyDeclaration(pd: PropertyDeclaration): FsProperty =
    {
        Emit = None
        Index = None
        Name = pd.name |> getPropertyName
        Option = pd.questionToken.IsSome
        Type = 
            match pd.``type`` with
            | None -> FsType.None 
            | Some tp -> readTypeNode tp
    }

let readFunctionDeclaration(fd: FunctionDeclaration): FsFunction =
    {
        Emit = None
        IsStatic = hasModifier SyntaxKind.StaticKeyword fd.modifiers
        Name = fd.name |> Option.map (fun id -> id.getText())
        TypeParameters = readTypeParameters fd.typeParameters
        Params = fd.parameters |> List.ofSeq |> List.map readParameterDeclaration
        ReturnType =
            match fd.``type`` with
            | Some t -> readTypeNode t
            | None -> FsType.Mapped "unit"
    }

let readIndexSignature(ps: IndexSignatureDeclaration): FsProperty =
    let pm = readParameterDeclaration ps.parameters.[0]
    {
        Emit = Some "$0[$1]{{=$2}}"
        Index = Some pm
        Name = "Item"
        Option = ps.questionToken.IsSome
        Type = 
            match ps.``type`` with
            | None -> FsType.None 
            | Some tp -> readTypeNode tp
    }

let readCallSignature(cs: CallSignatureDeclaration): FsFunction =
    {
        Emit = Some "$0($1...)"
        IsStatic = false // TODO ?
        Name = Some "Invoke"
        TypeParameters = []
        Params = cs.parameters |> List.ofSeq |> List.map readParameterDeclaration
        ReturnType =
            match cs.``type`` with
            | Some t -> readTypeNode t
            | None -> FsType.Mapped "unit"
    }

let readConstructSignatureDeclaration(cs: ConstructSignatureDeclaration): FsFunction =
    {
        Emit = Some "new $0($1...)"
        IsStatic = true
        Name = Some "Create"
        TypeParameters = cs.typeParameters |> readTypeParameters
        Params = cs.parameters |> List.ofSeq |> List.map readParameterDeclaration
        ReturnType = FsType.This
    }

let readConstructorDeclaration(cs: ConstructorDeclaration): FsFunction =
    {
        Emit = Some "new $0($1...)"
        IsStatic = true
        Name = Some "Create"
        TypeParameters = cs.typeParameters |> readTypeParameters
        Params = cs.parameters |> List.ofSeq |> List.map readParameterDeclaration
        ReturnType = FsType.This
    }

let readNamedDeclaration(te: NamedDeclaration): FsType =
    match te.kind with
    | SyntaxKind.IndexSignature ->
        readIndexSignature (te :?> IndexSignatureDeclaration) |> FsType.Property
    | SyntaxKind.MethodSignature ->
        readMethodSignature (te :?> MethodSignature) |> FsType.Function
    | SyntaxKind.PropertySignature ->
        readPropertySignature (te :?> PropertySignature) |> FsType.Property
    | SyntaxKind.CallSignature ->
        readCallSignature(te :?> CallSignatureDeclaration) |> FsType.Function
    | SyntaxKind.ConstructSignature ->
        readConstructSignatureDeclaration(te :?> ConstructSignatureDeclaration) |> FsType.Function
    | SyntaxKind.Constructor ->
        readConstructorDeclaration(te :?> ConstructorDeclaration) |> FsType.Function
    | SyntaxKind.PropertyDeclaration ->
        readPropertyDeclaration (te :?> PropertyDeclaration) |> FsType.Property
    | SyntaxKind.MethodDeclaration ->
        readMethodDeclaration (te :?> MethodDeclaration) |> FsType.Function
    | _ -> printfn "unsupported NamedDeclaration kind: %A" te.kind; FsType.TODO

let readAliasDeclaration(d: TypeAliasDeclaration): FsType =
    let tp = d.``type`` |> readTypeNode
    let name = d.name.getText()
    let asAlias() =
        {
            Name = name
            Type = tp
            TypeParameters = readTypeParameters d.typeParameters
        }
        |> FsType.Alias
    match tp with
    | FsType.Union un ->
        let sls = un.Types |> List.choose asStringLiteral
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
    // let var = readExpressionText ea.expression
    // {
    //     Namespace = []
    //     Variable = var
    //     Type = sprintf "%s.IExports" var
    // }
    // |> FsType.Import
    FsType.None

let readStatement(sd: Statement): FsType =
    match sd.kind with
    | SyntaxKind.InterfaceDeclaration ->
        readInterface (sd :?> InterfaceDeclaration) |> FsType.Interface
    | SyntaxKind.EnumDeclaration ->
        readEnum (sd :?> EnumDeclaration) |> FsType.Enum
    | SyntaxKind.TypeAliasDeclaration ->
        readAliasDeclaration (sd :?> TypeAliasDeclaration)
    | SyntaxKind.ClassDeclaration ->
        readClass (sd :?> ClassDeclaration) |> FsType.Interface
    | SyntaxKind.VariableStatement ->
        readVariable (sd :?> VariableStatement) |> FsType.Variable
    | SyntaxKind.FunctionDeclaration ->
        readFunctionDeclaration (sd :?> FunctionDeclaration) |> FsType.Function
    | SyntaxKind.ModuleDeclaration ->
        readModuleDeclaration (sd :?> ModuleDeclaration) |> FsType.Module
    | SyntaxKind.ExportAssignment ->
        readExportAssignment(sd :?> ExportAssignment)
    | SyntaxKind.ImportDeclaration ->
        // https://github.com/fable-compiler/ts2fable/issues/21
        // printfn "TODO import statements"
        FsType.TODO
    | SyntaxKind.NamespaceExportDeclaration ->
        // let ns = sd :?> NamespaceExportDeclaration
        FsType.TODO
    | SyntaxKind.ExportDeclaration ->
        // printfn "TODO export statements"
        FsType.TODO
    | _ -> printfn "unsupported Statement kind: %A" sd.kind; FsType.TODO

let readModuleName(mn: ModuleName): string =
    match mn with
    | U2.Case1 id -> id.getText().Replace("\"","")
    | U2.Case2 sl -> sl.getText()

let rec readModuleDeclaration(md: ModuleDeclaration): FsModule =
    let types = ResizeArray()
    md.ForEachChild (fun nd ->
        match nd.kind with
        | SyntaxKind.ModuleBlock ->
            let mb = nd :?> ModuleBlock
            mb.statements |> List.ofSeq |> List.map readStatement |> List.iter types.Add
        | SyntaxKind.DeclareKeyword -> ()
        | SyntaxKind.Identifier -> ()
        | SyntaxKind.ModuleDeclaration ->
            readModuleDeclaration (nd :?> ModuleDeclaration) |> FsType.Module |> types.Add
        | SyntaxKind.StringLiteral -> ()
        | SyntaxKind.ExportKeyword -> ()
        | _ -> printfn "unknown kind in ModuleDeclaration: %A" nd.kind
    )
    {
        Name = readModuleName md.name
        Types = types |> List.ofSeq
    }