module rec ts2fable.App

// This app has 3 main functions.
// 1. Read a TypeScript file into a syntax tree.
// 2. Fix the syntax tree.
// 3. Print the syntax tree to a F# file.

open Fable.Core
open Fable.Import
open Fable.Import.Node
open Fable.Import.ts
open System.Collections.Generic
open System

// our simplified syntax tree
// some names inspired by the actual F# AST:
// https://github.com/fsharp/FSharp.Compiler.Service/blob/master/src/fsharp/ast.fs

let [<Import("*","typescript")>] ts: ts.Globals = jsNative

type FsInterface =
    {
        Name: string
        TypeParameters: FsType list
        Inherits: FsType list
        Members: FsType list
    }

type FsClass =
    {
        ClassName: string option
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
        Name: string
        Type: FsType
    }

[<RequireQualifiedAccess>]
type FsType =
    | Interface of FsInterface
    | Class of FsClass
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

type FsModule =
    {
        Name: string
        Types: FsType list
    }

type FsFile =
    {
        Modules: FsModule list
    }

type Node with
    member x.ForEachChild cbNode =
        let func = Func<_,_>(fun (node:Node) -> cbNode node; None)
        x.forEachChild<unit>(func) |> ignore

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
        Name = id.name.text 
        Inherits = readInherits id.heritageClauses
        Members = id.members |> List.ofSeq |> List.map readTypeElement
        TypeParameters = readTypeParameters id.typeParameters
    }

let readClass(cd: ClassDeclaration): FsClass =
    {
        ClassName = cd.name |> Option.map (fun nm -> nm.text) 
        Inherits = readInherits cd.heritageClauses
        Members = [] // TODO cd.members |> List.ofSeq |> List.map readTypeElement
        TypeParameters = readTypeParameters cd.typeParameters
    }

let readVariable(vb: VariableStatement): FsVariable =
    let vd = vb.declarationList.declarations.[0] // TODO more than 1
    {
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
        Name = ft.name |> Option.map getPropertyName
        TypeParameters = readTypeParameters ft.typeParameters
        Params =  ft.parameters |> List.ofSeq |> List.map readParameterDeclaration
        ReturnType =
            match ft.``type`` with
            | Some t -> readTypeNode t
            | None -> FsType.Mapped "unit"
    }
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
    | SyntaxKind.ThisType ->
        // TODO map to the actual type of this
        FsType.Mapped "obj"
    | SyntaxKind.TypePredicate -> FsType.Mapped "bool"
    | SyntaxKind.TypeLiteral ->
        // let tl = t :?> TypeLiteralNode
        // printfn "TypeLiteral %A" tl
        FsType.Mapped "obj"
    | SyntaxKind.IntersectionType -> FsType.Mapped "obj"
    | SyntaxKind.IndexedAccessType ->
        // function createKeywordTypeNode(kind: KeywordTypeNode["kind"]): KeywordTypeNode;
        FsType.Mapped "obj" // TODO
    | SyntaxKind.TypeQuery ->
        // let tq = t :?> TypeQueryNode
        FsType.Mapped "obj"
    | SyntaxKind.LiteralType ->
        let lt = t :?> LiteralTypeNode
        match lt.literal.kind with
        | SyntaxKind.StringLiteral ->
            FsType.StringLiteral (lt.literal.getText().Replace("\"","").Replace("'",""))
        | _ ->
            FsType.Mapped "obj"
    | SyntaxKind.ExpressionWithTypeArguments ->
        let eta = t :?> ExpressionWithTypeArguments
        match eta.expression.kind with
        | SyntaxKind.Identifier ->
            let id = eta.expression :?> Identifier
            FsType.Mapped id.text
        | _ -> printfn "unsupported TypeNode ExpressionWithTypeArguments kind: %A" eta.expression.kind; FsType.TODO
    | SyntaxKind.ParenthesizedType -> FsType.Mapped "obj"
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

let readFunctionDeclaration(fd: FunctionDeclaration): FsFunction =
    {
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

let readTypeElement(te: TypeElement): FsType =
    match te.kind with
    | SyntaxKind.IndexSignature ->
        readIndexSignature (te :?> IndexSignatureDeclaration) |> FsType.Property
    | SyntaxKind.MethodSignature ->
        readMethodSignature (te :?> MethodSignature) |> FsType.Function
    | SyntaxKind.PropertySignature ->
        readPropertySignature (te :?> PropertySignature) |> FsType.Property
    | SyntaxKind.CallSignature ->
        // member = getMethod(node, { name: "Invoke" });
        // member.emit = "$0($1...)";
        // ifc.methods.push(member);
        FsType.TODO
    | SyntaxKind.ConstructSignature ->
        // member = getMethod(node, { name: "Create" });
        // member.emit = "new $0($1...)";
        // ifc.methods.push(member);
         FsType.TODO
    | _ -> printfn "unsupported TypeElement kind: %A" te.kind; FsType.TODO

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

let readStatement(sd: Statement): FsType =
    match sd.kind with
    | SyntaxKind.InterfaceDeclaration ->
        readInterface (sd :?> InterfaceDeclaration) |> FsType.Interface
    | SyntaxKind.EnumDeclaration ->
        readEnum (sd :?> EnumDeclaration) |> FsType.Enum
    | SyntaxKind.TypeAliasDeclaration ->
        readAliasDeclaration (sd :?> TypeAliasDeclaration)
    | SyntaxKind.ClassDeclaration ->
        readClass (sd :?> ClassDeclaration) |> FsType.Class
    | SyntaxKind.VariableStatement ->
        readVariable (sd :?> VariableStatement) |> FsType.Variable
    | SyntaxKind.FunctionDeclaration ->
        readFunctionDeclaration (sd :?> FunctionDeclaration) |> FsType.Function
    | SyntaxKind.ModuleDeclaration ->
        readModuleDeclaration (sd :?> ModuleDeclaration) |> FsType.Module
    | SyntaxKind.ExportAssignment ->
        FsType.TODO
    | SyntaxKind.ImportDeclaration ->
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
                        {
                            Name = ai.Name
                            TypeParameters = ai.TypeParameters
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
let asClass (tp: FsType) = match tp with | FsType.Class v -> Some v | _ -> None
let asGeneric (tp: FsType) = match tp with | FsType.Generic v -> Some v | _ -> None
let asStringLiteral (tp: FsType): string option = match tp with | FsType.StringLiteral v -> Some v | _ -> None

let isFunction tp = match tp with | FsType.Function _ -> true | _ -> false
let isStringLiteral tp = match tp with | FsType.StringLiteral _ -> true | _ -> false

let createGlobals(md: FsModule): FsModule =
    let tps = md.Types |> List.filter (fun t ->
        match t with
        | FsType.Variable _ -> true
        | FsType.Function _ -> true
        | _ -> false
    )
    if tps.Length = 0 then
        md
    else
        let cl: FsClass =
            {
                ClassName = Some "[<Erase>] Globals" // TODO attributes
                Inherits = []
                TypeParameters = []
                Members = tps
            }
        { md with
            Types = [ FsType.Class cl ] @ md.Types
        }

let rec fixType (fix: FsType -> FsType) (tp: FsType): FsType =
    // fix children first, then curren type

    let fixModule (a: FsModule) =
        let b =
            { a with
                Types = a.Types |> List.map (fixType fix)
            }
            |> FsType.Module |> fix
        match b with
        | FsType.Module c -> c
        | _ -> failwithf "module must be mapped to module"

    let fixParam (a: FsParam) =
        let b =
            { a with
                Type = fixType fix a.Type
            }
            |> FsType.Param |> fix
        match b with
        | FsType.Param c -> c
        | _ -> failwithf "param must be mapped to param"
    
    match tp with
    | FsType.Interface it ->
        { it with
            TypeParameters = it.TypeParameters |> List.map (fixType fix)
            Inherits = it.Inherits |> List.map (fixType fix)
            Members = it.Members |> List.map (fixType fix)
        }
        |> FsType.Interface
    | FsType.Class cl ->
        { cl with
            TypeParameters = cl.TypeParameters |> List.map (fixType fix)
            Members = cl.Members |> List.map (fixType fix)
        }
        |> FsType.Class
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
            Params = fn.Params |> List.map fixParam
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
        fixModule md |> FsType.Module
     | FsType.File f ->
        { f with
            Modules = f.Modules |> List.map fixModule
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

let addTicForGenericFunctions(md: FsModule): FsModule =
    { md with
        Types =
            md.Types |> List.map (fun tp ->
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
                | FsType.Class cl ->
                    { cl with
                        Members = cl.Members |> List.map (fun mbr ->
                            match asFunction mbr with
                            | None -> mbr
                            | Some fn -> fixTic fn.TypeParameters mbr
                        )
                    }
                    |> FsType.Class
                | _ -> tp
            )
    }

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

let addTicForGenericTypes(md: FsModule): FsModule =
    { md with
        Types =
            md.Types |> List.map (fun tp ->
                match tp with
                | FsType.Interface it -> fixTic it.TypeParameters tp
                | FsType.Class cl -> fixTic cl.TypeParameters tp
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
            | U2.Case1 id -> id.getText()
            | U2.Case2 sl -> sl.text
        Types = types |> List.ofSeq
    }

let readSourceFile(sf: SourceFile): FsFile =
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
    {
        Modules =
            modules
            |> List.ofSeq
            |> mergeModules
            |> List.map createGlobals
            |> List.map addTicForGenericFunctions
            |> List.map addTicForGenericTypes
            |> List.map fixNodeArray
            |> List.map fixEscapeWords
            |> List.map fixDateTime
    }

let printType (tp: FsType): string =
    match tp with
    | FsType.Mapped s -> s
    | FsType.TODO -> "TODO"
    | FsType.Array at ->
        sprintf "ResizeArray<%s>" (printType at)
    | FsType.Union un ->
        if un.Types.Length > 6 then
            "obj"
        else if un.Types.Length = 1 then
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
        "Func<" |> line.Add
        typs |> List.map printType |> String.concat ", " |> line.Add
        ">"|> line.Add
        line |> String.concat ""
    | FsType.Tuple tp ->
        let line = ResizeArray()
        tp.Types |> List.map printType |> String.concat " * " |> line.Add
        line |> String.concat ""
    | FsType.Variable vb ->
        let vtp = vb.Type |> printType
        sprintf "[<Global>] static member %s with get(): %s = jsNative and set(v: %s): unit = jsNative"
            vb.Name vtp vtp
    | FsType.StringLiteral _ -> "string"
    | _ -> printfn "unsupported printType %A" tp; "TODO"

let printFunction (f: FsFunction): string =
    let line = ResizeArray()
    // TODO can interface methods not have generics?
    // sprintf "abstract %s%s" f.Name.Value (printTypeParameters f.TypeParameters) |> line.Add
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

let printClassFunction (f: FsFunction): string =
    let line = ResizeArray()
    sprintf "member __.%s%s" f.Name.Value (printTypeParameters f.TypeParameters) |> line.Add
    let prms = 
        f.Params |> List.map(fun p ->
            sprintf "%s%s%s: %s"
                (if p.ParamArray then "[<ParamArray>] " else "")
                (if p.Optional then "?" else "") p.Name (printType p.Type)
        )
    if prms.Length = 0 then
        sprintf "(" |> line.Add
    else
        sprintf "(%s" (prms |> String.concat ", ") |> line.Add
    sprintf "): %s = jsNative" (printType f.ReturnType) |> line.Add
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
                | _ -> ()
            if !nLines = 0 then
                sprintf "%s    interface end" indent |> lines.Add
        | FsType.Class cl ->
            sprintf "" |> lines.Add
            sprintf "%stype %s%s =" indent cl.ClassName.Value (printTypeParameters cl.TypeParameters) |> lines.Add
            let nLines = ref 0
            for mbr in cl.Members do
                match mbr with
                | FsType.Function f ->
                    sprintf "%s    %s" indent (printClassFunction f) |> lines.Add
                    incr nLines
                | _ ->
                    sprintf "%s    %s" indent (printType mbr) |> lines.Add
                    incr nLines
            if !nLines = 0 then
                sprintf "%s    class end" indent |> lines.Add
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
        | _ -> ()

let printFsFile (file: FsFile): ResizeArray<string> =
    let lines = ResizeArray<string>()

    // TODO specify namespace
    // TODO customize open statements
    sprintf "namespace rec Fable.Import" |> lines.Add
    sprintf "open System" |> lines.Add
    sprintf "open System.Text.RegularExpressions" |> lines.Add // TODO why
    sprintf "open Fable.Core" |> lines.Add
    sprintf "open Fable.Import.JS" |> lines.Add

    file.Modules
        |> List.filter (fun md -> md.Types.Length > 0)
        |> List.iter (printModule lines "")
    lines

let printFile tsPath: unit =
    let code = Fs.readFileSync(tsPath).toString()
    let tsFile = ts.createSourceFile(tsPath, code, ScriptTarget.ES2015, true)
    let fsFile = readSourceFile tsFile
    for line in printFsFile fsFile do
        printfn "%s" line

let writeFile tsPath (fsPath: string): unit =
    let code = Fs.readFileSync(tsPath).toString()
    let tsFile = ts.createSourceFile(tsPath, code, ScriptTarget.ES2015, true)
    let fsFile = readSourceFile tsFile
    let file = Fs.createWriteStream fsPath
    for line in printFsFile fsFile do
        file.write(sprintf "%s%c" line '\n') |> ignore
    file.``end``()

let p = Node.Globals.``process``
let argv = p.argv |> List.ofSeq
// printfn "%A" argv

if argv |> List.exists (fun s -> s = "splitter.config.js") then // run from build
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