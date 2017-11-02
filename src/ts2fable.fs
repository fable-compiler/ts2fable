module rec ts2fable.App

// This app has 3 main functions.
// 1. Read a TypeScript file into a syntax tree.
// 2. Fix the syntax tree.
// 3. Print the syntax tree to a F# file.

open Fable.Core
open Fable.Import
open Fable.Import.Node
open Fable.Import.typescript
open Fable.Import.typescript.ts
open System.Collections.Generic
open System

// our simplified syntax tree
// some names inspired by the actual F# AST:
// https://github.com/fsharp/FSharp.Compiler.Service/blob/master/src/fsharp/ast.fs

type FsInterface =
    {
        IsStatic: bool // contains only static functions
        Name: string
        TypeParameters: FsType list
        Inherits: FsType list
        Members: FsType list
    }

[<RequireQualifiedAccess>]
type FsEnumCaseType =
    | Numeric
    | String
    | Unknown

type FsEnumCase =
    {
        Name: string
        Type: FsEnumCaseType
        Value: string option
    }

type FsEnum =
    {
        Name: string
        Cases: FsEnumCase list
    }
    with
        member x.Type
            with get() =
                if x.Cases |> List.exists (fun c -> c.Type = FsEnumCaseType.Unknown) then
                    FsEnumCaseType.Unknown
                else if x.Cases |> List.exists (fun c -> c.Type = FsEnumCaseType.String) then
                    FsEnumCaseType.String
                else
                    FsEnumCaseType.Numeric

type FsParam =
    {
        Name: string
        Optional: bool
        ParamArray: bool
        Type: FsType
    }

type FsFunction =
    {
        Emit: string option
        IsStatic: bool
        Name: string option // declarations have them, signatures do not
        TypeParameters: FsType list
        Params: FsParam list
        ReturnType: FsType
    }

type FsProperty =
    {
        Emit: string option
        Name: string
        Option: bool
        Type: FsType
    }

type FsGenericType =
    {
        Type: FsType
        TypeParameters: FsType list
    }

type FsUnion =
    {
        Option: bool
        Types: FsType list
    }

type FsAlias =
    {
        Name: string
        Type: FsType
        TypeParameters: FsType list
    }

type FsTuple =
    {
        Types: FsType list
    }

type FsVariable =
    {
        HasDeclare: bool
        Name: string
        Type: FsType
    }

type FsImport =
    {
        Namespace: string list
        Variable: string
        Type: string
    }

[<RequireQualifiedAccess>]
type FsType =
    | Interface of FsInterface
    | Enum of FsEnum
    | Property of FsProperty
    | Param of FsParam
    | Array of FsType
    | TODO
    | None // when it is not set
    | Mapped of string
    | Function of FsFunction
    | Union of FsUnion
    | Alias of FsAlias
    | Generic of FsGenericType
    | Tuple of FsTuple
    | Module of FsModule
    | File of FsFile
    | Variable of FsVariable
    | StringLiteral of string
    | Import of FsImport
    | This

type FsModule =
    {
        Name: string
        Types: FsType list
    }

type FsFile =
    {
        Name: string
        Opens: string list
        Modules: FsModule list
    }

type Node with
    member x.ForEachChild (cbNode: Node -> unit) =
        x.forEachChild<unit> (fun nd -> cbNode nd; None) |> ignore

let getPropertyName(pn: PropertyName): string =
    match pn with
    | U4.Case1 id -> id.text
    | U4.Case2 sl -> sl.text
    | U4.Case3 nl -> nl.text
    | U4.Case4 cpn -> cpn.getText()

let getBindingName(bn: BindingName): string =
    match bn with
    | U2.Case1 id -> id.text
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
            // | _ -> None // TODO TypeScript string based enums #17
            // https://github.com/fable-compiler/ts2fable/issues/17
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
            tp.name.text |> FsType.Mapped
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
        Name = id.name.text 
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
            | Some id -> id.text
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
        Name = ed.name.text
        Cases = ed.members |> List.ofSeq |> List.map readEnumCase
    }

let readTypeReference(tr: TypeReferenceNode): FsType =
    match tr.typeArguments with
    | None ->
        let txt = tr.getText()
        if txt.Contains "." then
            txt.Substring(0, txt.IndexOf ".") |> FsType.Mapped
        else
            txt |> FsType.Mapped
    | Some tas ->
        {
            Type =
                match tr.typeName with
                | U2.Case1 id ->
                    id.text
                | U2.Case2 qn ->
                    qn.getText()
                |> FsType.Mapped
            TypeParameters =
                tas |> List.ofSeq |> List.map readTypeNode
        }
        |>
        FsType.Generic
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
            Types = typs |> List.filter (isOption >> not) |> List.map readTypeNode
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
        printfn "TODO mapped types"
        FsType.TODO
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
        Name = fd.name |> Option.map (fun id -> id.text)
        TypeParameters = readTypeParameters fd.typeParameters
        Params = fd.parameters |> List.ofSeq |> List.map readParameterDeclaration
        ReturnType =
            match fd.``type`` with
            | Some t -> readTypeNode t
            | None -> FsType.Mapped "unit"
    }

let readIndexSignature(ps: IndexSignatureDeclaration): FsProperty =
    {
        Emit = Some "$0[$1]{{=$2}}"
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
    let name = d.name.text
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
        id.text
    | SyntaxKind.PropertyAccessExpression ->
        let pa = ep :?> PropertyAccessExpression
        pa.getText()
    | _ ->
        printfn "readExpressionText kind not yet supported: %A" ep.kind
        ep.getText()

let readExportAssignment(ea: ExportAssignment): FsType =
    let var = readExpressionText ea.expression
    {
        Namespace = []
        Variable = var
        Type = sprintf "%s.IExports" var
    }
    |> FsType.Import

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
        printfn "TODO import statements"
        FsType.TODO
    | SyntaxKind.NamespaceExportDeclaration ->
        printfn "TODO namespace exports"
        FsType.TODO
    | _ -> printfn "unsupported Statement kind: %A" sd.kind; FsType.TODO

let mergeTypes(tps: FsType list): FsType list =
    let index = Dictionary<string,int>()
    let list = List<FsType>()
    for b in tps do
        match b with
        | FsType.Interface bi ->
            if index.ContainsKey bi.Name then
                let i = index.[bi.Name]
                let a = list.[i]
                match a with
                | FsType.Interface ai ->
                    list.[i] <-
                        { ai with
                            Inherits = List.append ai.Inherits bi.Inherits
                            Members = List.append ai.Members bi.Members
                        }
                        |> FsType.Interface
                | _ ->
                    list.Add b
                    index.Add(bi.Name, list.Count-1)

            else
                list.Add b
                index.Add(bi.Name, list.Count-1)
        | _ -> 
            list.Add b
    list |> List.ofSeq

let mergeModules(mds: FsModule list): FsModule list =
    let index = Dictionary<string,int>()
    let list = List<FsModule>()
    for b in mds do
        if index.ContainsKey b.Name then
            let i = index.[b.Name]
            let a = list.[i]
            list.[i] <-
                {
                    Name = a.Name
                    Types = List.append a.Types b.Types |> mergeTypes
                }
        else
            list.Add b
            index.Add(b.Name, list.Count-1)
    list |> List.ofSeq

let asFunction (tp: FsType) = match tp with | FsType.Function v -> Some v | _ -> None
let asInterface (tp: FsType) = match tp with | FsType.Interface v -> Some v | _ -> None
let asGeneric (tp: FsType) = match tp with | FsType.Generic v -> Some v | _ -> None
let asStringLiteral (tp: FsType): string option = match tp with | FsType.StringLiteral v -> Some v | _ -> None

let isFunction tp = match tp with | FsType.Function _ -> true | _ -> false
let isStringLiteral tp = match tp with | FsType.StringLiteral _ -> true | _ -> false

let createIExports (f: FsFile): FsFile =
    f |> fixFile (fun tp ->
        match tp with
        | FsType.Module md ->

            let tps = md.Types |> List.collect(fun t ->
                match t with
                | FsType.Variable _ -> [t]
                | FsType.Function _ -> [t]
                | FsType.Interface it ->
                    if it.IsStatic then
                        [
                            // add a property for accessing the static class
                            {
                                Emit = None
                                Name = it.Name.Replace("Static","")
                                Option = false
                                Type = it.Name |> FsType.Mapped
                            }
                            |> FsType.Property
                        ]
                    else []
                | _ -> []
            )
            if tps.Length = 0 then
                tp
            else
                let cl: FsInterface =
                    {
                        IsStatic = false
                        Name = "IExports"
                        Inherits = []
                        TypeParameters = []
                        Members = tps
                    }
                { md with
                    Types = [ FsType.Interface cl ] @ md.Types
                }
                |> FsType.Module
        | _ -> tp
    )

let fixModule (fix: FsType -> FsType) (a: FsModule) =
    let b =
        { a with
            Types = a.Types |> List.map (fixType fix)
        }
        |> FsType.Module |> fix
    match b with
    | FsType.Module c -> c
    | _ -> failwithf "module must be mapped to module"

let fixParam (fix: FsType -> FsType) (a: FsParam) =
    let b =
        { a with
            Type = fixType fix a.Type
        }
        |> FsType.Param |> fix
    match b with
    | FsType.Param c -> c
    | _ -> failwithf "param must be mapped to param"

let rec fixType (fix: FsType -> FsType) (tp: FsType): FsType =
    // fix children first, then curren type
    match tp with
    | FsType.Interface it ->
        { it with
            TypeParameters = it.TypeParameters |> List.map (fixType fix)
            Inherits = it.Inherits |> List.map (fixType fix)
            Members = it.Members |> List.map (fixType fix)
        }
        |> FsType.Interface
    | FsType.Property pr ->
        { pr with
            Type = fixType fix pr.Type
        }
        |> FsType.Property 
    | FsType.Param pr ->
        { pr with
            Type = fixType fix pr.Type
        }
        |> FsType.Param
    | FsType.Array ar ->
        fixType fix ar |> FsType.Array
    | FsType.Function fn ->
        { fn with
            TypeParameters = fn.TypeParameters |> List.map (fixType fix)
            Params = fn.Params |> List.map (fixParam fix)
            ReturnType = fixType fix fn.ReturnType
        }
        |> FsType.Function
    | FsType.Union un ->
        { un with
            Types = un.Types |> List.map (fixType fix)
        }
        |> FsType.Union
    | FsType.Alias al ->
        { al with
            Type = fixType fix al.Type
            TypeParameters = al.TypeParameters |> List.map (fixType fix)
        }
        |> FsType.Alias
    | FsType.Generic gn ->
        { gn with
            Type = fixType fix gn.Type
            TypeParameters = gn.TypeParameters |> List.map (fixType fix)
        }
        |> FsType.Generic
    | FsType.Tuple tp ->
        { tp with
            Types = tp.Types |> List.map (fixType fix)
        }
        |> FsType.Tuple
    | FsType.Module md ->
        fixModule fix md |> FsType.Module
     | FsType.File f ->
        { f with
            Modules = f.Modules |> List.map (fixModule fix)
        }
        |> FsType.File
    | FsType.Variable vb ->
        { vb with
            Type = fixType fix vb.Type
        }
        |> FsType.Variable
    
    // | _ -> tp
    | FsType.Enum _ -> tp
    | FsType.Mapped _ -> tp
    | FsType.None _ -> tp
    | FsType.TODO _ -> tp
    | FsType.StringLiteral _ -> tp
    | FsType.Import _ -> tp
    | FsType.This -> tp

    |> fix // current type

let fixTic (typeParameters: FsType list) (tp: FsType) =
    if typeParameters.Length = 0 then
        tp
    else
        let list = typeParameters |> List.map printType
        let set = list |> Set.ofList
        let fix (t: FsType): FsType =
            match t with
            | FsType.Mapped s ->
                if set.Contains s then
                    sprintf "'%s" s |> FsType.Mapped
                else t
            | _ -> t
        fixType fix tp

let addTicForGenericFunctions(f: FsFile): FsFile =
    f |> fixFile (fun tp ->
        match tp with
        | FsType.Interface it ->
            { it with
                Members = it.Members |> List.map (fun mbr ->
                    match asFunction mbr with
                    | None -> mbr
                    | Some fn -> fixTic fn.TypeParameters mbr
                )
            }
            |> FsType.Interface
        | _ -> tp
    )

let fixNodeArray(md: FsModule): FsModule =

    let fix(tp: FsType): FsType =
        match asGeneric tp with
        | None -> tp
        | Some gn ->
            match gn.Type with
            | FsType.Mapped s ->
                if s.Equals "NodeArray" && gn.TypeParameters.Length = 1 then
                    gn.TypeParameters.[0] |> FsType.Array
                else tp
            | _ -> tp

    { md with Types = md.Types |> List.map (fixType fix) }

let fixEscapeWords(md: FsModule): FsModule =

    let fix(tp: FsType): FsType =
        match tp with
        | FsType.Mapped s ->
            Keywords.escapeWord s |> FsType.Mapped
        | FsType.Param pm ->
            { pm with Name = Keywords.escapeWord pm.Name } |> FsType.Param
        | FsType.Function fn ->
            { fn with Name = fn.Name |> Option.map Keywords.escapeWord } |> FsType.Function
        | FsType.Property pr ->
            { pr with Name = Keywords.escapeWord pr.Name } |> FsType.Property
        | _ -> tp

    { md with Types = md.Types |> List.map (fixType fix) }

let fixDateTime(md: FsModule): FsModule =

    let replaceName name =
        if String.Equals("Date", name) then "DateTime" else name

    let fix(tp: FsType): FsType =
        match tp with
        | FsType.Mapped s ->
            replaceName s |> FsType.Mapped
        | _ -> tp

    { md with Types = md.Types |> List.map (fixType fix) }

let fixDuplicatesInUnion(md: FsModule): FsModule =

    let fix(tp: FsType): FsType =
        match tp with
        | FsType.Union un ->
            let set = HashSet<_>()
            let tps = un.Types |> List.choose (fun tp -> 
                    if set.Contains tp then
                        None
                    else
                        set.Add tp |> ignore
                        Some tp
                )
            if tps.Length > 6 then
                // add U7 and U8 union types https://github.com/fable-compiler/Fable/issues/1211
                FsType.Mapped "obj"
            else 
                { un with Types = tps } |> FsType.Union
        | _ -> tp

    { md with Types = md.Types |> List.map (fixType fix) }

let addTicForGenericTypes(md: FsModule): FsModule =
    { md with
        Types =
            md.Types |> List.map (fun tp ->
                match tp with
                | FsType.Interface it -> fixTic it.TypeParameters tp
                | FsType.Alias al -> fixTic al.TypeParameters tp
                | _ -> tp
            )
    }

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
        | _ -> printfn "unknown kind in ModuleDeclaration: %A" nd.kind
    )
    {
        Name =
            match md.name with
            | U2.Case1 id -> id.text
            | U2.Case2 sl -> sl.text
        Types = types |> List.ofSeq
    }

/// add a namespace to import
/// and convert `declare` variables to imports
let rec fixImport (ns: string list) (md: FsModule): FsModule =
    let newNs = if String.IsNullOrEmpty md.Name then ns else ns @ [md.Name]
    { md with
        Types = md.Types |> List.map (fun tp ->
            match tp with
            | FsType.Module submd ->
                fixImport newNs submd |> FsType.Module
            | FsType.Import ep ->
                { ep with Namespace = newNs } |> FsType.Import
            | FsType.Variable vb ->
                if vb.HasDeclare then
                    {
                        Namespace = newNs
                        Variable = vb.Name
                        Type = printType vb.Type
                    }
                    |> FsType.Import
                else
                    vb  |> FsType.Variable
            | _ -> tp
        )
    }

/// replaces `this` with a reference to the interface type
let fixThis(md: FsModule): FsModule =

    let fix(tp: FsType): FsType =
        match tp with
        | FsType.Interface it ->
            { it with
                Members = it.Members |> List.map (fun mbr -> 
                    match mbr with
                    | FsType.Function f ->
                        { f with
                            ReturnType =
                                match f.ReturnType with
                                | FsType.This ->
                                    {
                                        Type = FsType.Mapped it.Name
                                        TypeParameters = it.TypeParameters
                                    }
                                    |> FsType.Generic
                                | _ -> f.ReturnType
                        }
                        |> FsType.Function
                    | _ -> mbr
                )
            }
            |> FsType.Interface
        | _ -> tp

    { md with Types = md.Types |> List.map (fixType fix) }

let isStatic (tp: FsType) =
    match tp with
    | FsType.Function fn -> fn.IsStatic
    | FsType.Interface it -> it.IsStatic
    | _ -> false

type FsInterface with
    member x.HasStaticMembers with get() = x.Members |> List.exists isStatic
    member x.StaticMembers with get() = x.Members |> List.filter isStatic
    member x.NonStaticMembers with get() = x.Members |> List.filter (not << isStatic)

let fixStatic(f: FsFile): FsFile =
    f |> fixFile (fun tp ->
        match tp with
        | FsType.Module md ->
            { md with
                Types = md.Types |> List.collect (fun tp2 ->
                    match tp2 with
                    | FsType.Interface it ->
                        if it.HasStaticMembers then
                            [
                                { it with
                                    Members = it.NonStaticMembers
                                }
                                |> FsType.Interface

                                { it with
                                    IsStatic = true
                                    Name = sprintf "%sStatic" it.Name
                                    Inherits = []
                                    Members = it.StaticMembers
                                }
                                |> FsType.Interface
                            ]
                        else
                            [tp2]
                    | _ -> [tp2]
                )
            }
            |> FsType.Module
        | _ -> tp
    )

let fixFile (fix: FsType -> FsType) (f: FsFile): FsFile =
    { f with Modules = f.Modules |> List.map (fixModule fix) }

let fixOpens(f: FsFile): FsFile =

    let isBrowser (name: string) =
        name.StartsWith "HTML"

    let mutable hasBrowser = false

    let fix(tp: FsType): FsType =
        match tp with
        | FsType.Mapped s ->
            if isBrowser s then
                hasBrowser <- true
            tp
        | _ -> tp

    f |> FsType.File |> fixType fix |> ignore

    { f with
        Opens =
            if hasBrowser then f.Opens @ ["Fable.Import.Browser"]
            else f.Opens
    }

let readSourceFile (tsPath: string) (sf: SourceFile): FsFile =
    let modules = ResizeArray()

    let gbl: FsModule =
        {
            Name = ""
            Types = sf.statements |> List.ofSeq |> List.map readStatement
        }
    modules.Add gbl

    sf.ForEachChild (fun nd ->
        match nd.kind with
        | SyntaxKind.ModuleDeclaration ->
            readModuleDeclaration (nd :?> ModuleDeclaration) |> modules.Add
        // | SyntaxKind.ExportAssignment -> () // TODO
        // | SyntaxKind.EndOfFileToken -> ()
        // | SyntaxKind.FunctionDeclaration -> ()
        // | SyntaxKind.TypeAliasDeclaration -> ()
        // | _ -> failwithf "unknown kind in SourceFile: %A" nd.kind
        | _ -> ()
    )
    let path = Fable.Import.Node.Exports.Path
    let name = path.basename(tsPath, path.extname(tsPath)) // TODO ensure valid name
    let name2 = path.basename(name, path.extname(name)) // twice because of .d.ts

    let opens = 
        [
            "System"
            // "System.Text.RegularExpressions"
            "Fable.Core"
            "Fable.Import.JS"
        ]

    {
        Name = name2
        Opens = opens
        Modules =
            modules
            |> List.ofSeq
            |> mergeModules
            |> List.map (fixImport [name2])
            |> List.map fixThis
            |> List.map addTicForGenericTypes
            |> List.map fixNodeArray
            |> List.map fixEscapeWords
            |> List.map fixDateTime
            |> List.map fixDuplicatesInUnion
    }
    |> fixOpens
    |> fixStatic
    |> createIExports
    |> addTicForGenericFunctions

let printType (tp: FsType): string =
    match tp with
    | FsType.Mapped s -> s
    | FsType.TODO -> "TODO"
    | FsType.Array at ->
        sprintf "ResizeArray<%s>" (printType at)
    | FsType.Union un ->
        if un.Types.Length = 1 then
            sprintf "%s%s" (printType un.Types.[0]) (if un.Option then " option" else "")
        else
            let line = ResizeArray()
            sprintf "U%d<" un.Types.Length |> line.Add
            un.Types |> List.map printType |> String.concat ", " |> line.Add
            sprintf ">%s" (if un.Option then " option" else "") |> line.Add
            line |> String.concat ""
    | FsType.Generic g ->
        let line = ResizeArray()
        sprintf "%s" (printType g.Type) |> line.Add
        if g.TypeParameters.Length > 0 then
            "<" |> line.Add
            g.TypeParameters |> List.map printType |> String.concat " * " |> line.Add
            ">" |> line.Add
        line |> String.concat ""
    | FsType.Function ft ->
        let line = ResizeArray()
        let typs =
            if ft.Params.Length = 0 then
                [ FsType.Mapped "unit"; ft.ReturnType ]
            else
                (ft.Params |> List.map (fun p -> p.Type)) @ [ ft.ReturnType ]
        "(" |> line.Add
        typs |> List.map printType |> String.concat " -> " |> line.Add
        ")"|> line.Add
        line |> String.concat ""
    | FsType.Tuple tp ->
        let line = ResizeArray()
        tp.Types |> List.map printType |> String.concat " * " |> line.Add
        line |> String.concat ""
    | FsType.Variable vb ->
        let vtp = vb.Type |> printType
        sprintf "abstract %s: %s with get, set" vb.Name vtp
    | FsType.StringLiteral _ -> "string"
    | _ -> printfn "unsupported printType %A" tp; "TODO"

let printFunction (f: FsFunction): string =
    let line = ResizeArray()
    if f.Emit.IsSome then sprintf "[<Emit \"%s\">] " f.Emit.Value |> line.Add
    sprintf "abstract %s" f.Name.Value |> line.Add
    let prms = 
        f.Params |> List.map(fun p ->
            if p.ParamArray then
                sprintf "[<ParamArray>] %s%s: %s" (if p.Optional then "?" else "") p.Name
                    (match p.Type with
                    | FsType.Array t -> printType t // inner type
                    | _ -> failwithf "function with unsupported param array type: %s" f.Name.Value)
            else
                sprintf "%s%s: %s" (if p.Optional then "?" else "") p.Name (printType p.Type)
        )
    if prms.Length = 0 then
        sprintf ": unit" |> line.Add
    else
        sprintf ": %s" (prms |> String.concat " * ") |> line.Add
    sprintf " -> %s" (printType f.ReturnType) |> line.Add
    line |> String.concat ""
let printProperty (pr: FsProperty): string =
    sprintf "%sabstract %s: %s%s%s with get, set"
        (if pr.Emit.IsSome then sprintf "[<Emit \"%s\">] " pr.Emit.Value else "")
        pr.Name
        (if pr.Emit.IsSome then "index: string -> " else "") // TODO will only work with indexed
        (printType pr.Type)
        (if pr.Option then " option" else "")

let printTypeParameters (tps: FsType list): string =
    if tps.Length = 0 then ""
    else
        let line = ResizeArray()
        line.Add "<"
        tps |> List.map printType |> String.concat ", " |> line.Add
        line.Add ">"
        line |> String.concat ""

let printModule (lines: ResizeArray<string>) (indent: string) (md: FsModule): unit =
    let indent =
        if md.Name <> "" then
            "" |> lines.Add
            sprintf "module %s =" md.Name |> lines.Add
            sprintf "%s    " indent 
        else indent
    for tp in md.Types do
        match tp with
        | FsType.Interface inf ->
            sprintf "" |> lines.Add
            sprintf "%stype [<AllowNullLiteral>] %s%s =" indent inf.Name (printTypeParameters inf.TypeParameters) |> lines.Add
            let nLines = ref 0
            for ih in inf.Inherits do
                sprintf "%s    inherit %s" indent (printType ih) |> lines.Add
                incr nLines
            for mbr in inf.Members do
                match mbr with
                | FsType.Function f ->
                    sprintf "%s    %s" indent (printFunction f) |> lines.Add
                    incr nLines
                | FsType.Property p ->
                    sprintf "%s    %s" indent (printProperty p) |> lines.Add
                    incr nLines
                | _ ->
                    sprintf "%s    %s" indent (printType mbr) |> lines.Add
                    incr nLines
            if !nLines = 0 then
                sprintf "%s    interface end" indent |> lines.Add
        | FsType.Enum en ->
            sprintf "" |> lines.Add
            match en.Type with
            | FsEnumCaseType.Numeric ->
                sprintf "%stype [<RequireQualifiedAccess>] %s =" indent en.Name |> lines.Add
                for cs in en.Cases do
                    let nm = cs.Name
                    let unm = Enum.createEnumName nm
                    let line = ResizeArray()
                    if nm.Equals unm then
                        sprintf "    | %s" nm |> line.Add
                    else
                        sprintf "    | [<CompiledName \"%s\">] %s" nm unm |> line.Add
                    if cs.Value.IsSome then
                        sprintf " = %s" cs.Value.Value |> line.Add
                    sprintf "%s%s" indent (line |> String.concat "") |> lines.Add
            | FsEnumCaseType.String ->
                sprintf "%stype [<StringEnum>] [<RequireQualifiedAccess>] %s =" indent en.Name |> lines.Add
                for cs in en.Cases do
                    let nm = cs.Name
                    let unm = Enum.createEnumName nm
                    let line = ResizeArray()
                    if nm.Equals unm then
                        sprintf "    | %s" nm |> line.Add
                    else
                        sprintf "    | [<CompiledName \"%s\">] %s" nm unm |> line.Add
                    sprintf "%s%s" indent (line |> String.concat "") |> lines.Add
            | FsEnumCaseType.Unknown ->
                sprintf "%stype %s =" indent en.Name |> lines.Add
                sprintf "%s    obj" indent |> lines.Add
        | FsType.Alias al ->
            sprintf "" |> lines.Add
            sprintf "%stype %s%s =" indent al.Name (printTypeParameters al.TypeParameters) |> lines.Add
            sprintf "%s    %s" indent (printType al.Type) |> lines.Add
        | FsType.Import ip ->
            sprintf "" |> lines.Add
            let ns = ip.Namespace |> String.concat "."
            sprintf "%slet [<Import(\"*\",\"%s\")>] %s: %s = jsNative" indent ns ip.Variable ip.Type |> lines.Add
        | _ -> ()

let printFsFile (file: FsFile): ResizeArray<string> =
    let lines = ResizeArray<string>()

    sprintf "module rec Fable.Import.%s" file.Name |> lines.Add

    for opn in file.Opens do
        sprintf "open %s" opn |> lines.Add

    file.Modules
        |> List.filter (fun md -> md.Types.Length > 0)
        |> List.iter (printModule lines "")
    lines

let printFile tsPath: unit =
    let code = Fs.readFileSync(tsPath).toString()
    let tsFile = ts.createSourceFile(tsPath, code, ScriptTarget.ES2015, true)
    let fsFile =  readSourceFile tsPath tsFile
    for line in printFsFile fsFile do
        printfn "%s" line

let writeFile tsPath (fsPath: string): unit =
    let code = Fs.readFileSync(tsPath).toString()
    let tsFile = ts.createSourceFile(tsPath, code, ScriptTarget.ES2015, true)
    let fsFile = readSourceFile tsPath tsFile
    let file = Fs.createWriteStream fsPath
    for line in printFsFile fsFile do
        file.write(sprintf "%s%c" line '\n') |> ignore
    file.``end``()

let p = Node.Globals.``process``
let argv = p.argv |> List.ofSeq
// printfn "%A" argv

if argv |> List.exists (fun s -> s = "splitter.config.js") then // run from build
    printfn "ts.version: %s" ts.version
    // printFile "node_modules/izitoast/dist/izitoast/izitoast.d.ts"
    writeFile "node_modules/izitoast/dist/izitoast/izitoast.d.ts" "src/bin/izitoast.fs"
    writeFile "node_modules/typescript/lib/typescript.d.ts" "src/bin/typescript.fs"
    // writeFile "node_modules/@types/electron/index.d.ts" "src/bin/electron.fs"
    // writeFile "node_modules/@types/react/index.d.ts" "src/bin/react.fs"
    // writeFile "node_modules/@types/node/index.d.ts" "src/bin/node.fs"

else
    let tsfile = argv |> List.tryFind (fun s -> s.EndsWith ".ts")
    let fsfile = argv |> List.tryFind (fun s -> s.EndsWith ".fs")
    
    match tsfile with
    | None -> failwithf "Please provide the path to a TypeScript definition file"
    | Some tsf ->
        match fsfile with
        | None -> printFile tsf
        | Some fsf -> writeFile tsf fsf