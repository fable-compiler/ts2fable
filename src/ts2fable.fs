module rec ts2fable.App

open Fable.Core
open Fable.Import
open Fable.Import.Node
open Fable.Import.ts

// our simplified F# AST
// some names inspired by the actual F# AST:
// https://github.com/fsharp/FSharp.Compiler.Service/blob/master/src/fsharp/ast.fs

type FsInterface =
    {
        Name: string
        Inherits: FsType list
        Members: FsType list
    }

type FsEnumCase =
    {
        Name: string
        Value: string option
    }

type FsEnum =
    {
        Name: string
        Cases: FsEnumCase list
    }

type FsParam =
    {
        Name: string
        Optional: bool
        Type: FsType
    }

type FsFunction =
    {
        Name: string
        Params: FsParam list
        ReturnType: FsType
    }

type FsProperty =
    {
        Name: string
        Option: bool
        Type: FsType
    }

[<RequireQualifiedAccess>]
type FsType =
    | Interface of FsInterface
    | Enum of FsEnum
    | Method of FsFunction
    | Property of FsProperty
    | Param of FsParam
    | Array of FsType
    | TODO
    | None // when it is not set
    | Mapped of string
    | Function of FsFunction

type FsModule =
    {
        Name: string
        Types: FsType list
    }

type FsFile =
    {
        Modules: FsModule list
    }


type Globals with
    member x.forEachChildNode (node: Node) cbNode =
        let func = System.Func<_,_>(fun (node:Node) -> cbNode node; None)
        x.forEachChild<unit>(node, func) |> ignore

    member x.forEachChildNodeRec (root: Node) cbNode =
        cbNode root
        x.forEachChildNode root (fun node -> x.forEachChildNodeRec node cbNode)

    member x.isLiteralExpressionAs(node: Node): LiteralExpression option =
        if x.isLiteralExpression node then Some(node :?> LiteralExpression) else None

let getModules(root: Node) =
    let nodes = ResizeArray<ModuleBlock>()
    ts.forEachChildNodeRec root (fun node -> 
        if node.kind = SyntaxKind.ModuleBlock then
            nodes.Add (node :?> ModuleBlock) )
    nodes |> List.ofSeq

let getModuleName(mb: ModuleBlock): string =
    match mb.parent with
    | Some md ->
        match md.name with
        | U2.Case1 id -> id.getText()
        | U2.Case2 sl -> sl.text
    | None -> "NoModuleName"

let getPropertyName(pn: PropertyName): string =
    match pn with
    | U4.Case1 id -> id.text
    | U4.Case2 sl -> sl.text
    | U4.Case3 nl -> nl.text
    | U4.Case4 cpn -> cpn.getText()

let getBindingyName(bn: BindingName): string =
    match bn with
    | U2.Case1 id -> id.text
    | U2.Case2 bp -> // BindingPattern
        match bp with
        | U2.Case1 obp -> obp.getText()
        | U2.Case2 abp -> abp.getText()

let visitEnumCase(em: EnumMember): FsEnumCase =
    {
        Name = em.name |> getPropertyName
        Value = 
            match em.initializer with
            | None -> None
            | Some ep ->
                match ep.kind with
                | SyntaxKind.NumericLiteral ->
                    let nl = ep :?> NumericLiteral
                    Some nl.text
                | _ -> None // TODO TypeScript string based enums #17
                // https://github.com/fable-compiler/ts2fable/issues/17
    }

let visitInterface(id: InterfaceDeclaration): FsInterface =
    {
        Name = id.name.text 
        Inherits =
            match id.heritageClauses with
            | None -> []
            | Some hcs ->
                hcs |> List.ofArray |> List.map (fun hc ->
                    hc.types |> List.ofArray |> List.map (fun eta ->
                        visitTypeNode eta
                    )
                )
                |> List.concat
        Members = id.members |> List.ofArray |> List.map visitTypeElement
    }

let visitEnum(ed: EnumDeclaration): FsEnum =
    {
        Name = ed.name.text
        Cases = ed.members |> List.ofArray |> List.map visitEnumCase
    }

let rec visitTypeNode(t: TypeNode): FsType =
    match t.kind with
    | SyntaxKind.StringKeyword -> FsType.Mapped "string"
    | SyntaxKind.FunctionType ->
        // var cbParams = type.parameters.map(function (x) {
        //     return x.dotDotDotToken ? "obj" : getType(x.type);
        // }).join(", ");
        // return "Func<" + (cbParams || "unit") + ", " + getType(type.type) + ">";
        FsType.Mapped "fun" // TODO
    | SyntaxKind.TypeReference -> FsType.Mapped (t.getText())
    | SyntaxKind.ArrayType ->
        // return "ResizeArray<" + getType(type.elementType) + ">";
        let at = t :?> ArrayTypeNode
        FsType.Array (visitTypeNode at.elementType)
    | SyntaxKind.NumberKeyword -> FsType.Mapped "float"
    | SyntaxKind.BooleanKeyword -> FsType.Mapped "bool"
    | SyntaxKind.UnionType ->
        // if (type.types && type.types[0].kind == ts.SyntaxKind.StringLiteralType)
        //     return "(* TODO StringEnum " + type.types.map(x=>x.text).join(" | ") + " *) string";
        // else if (type.types.length <= 4)
        //     return "U" + type.types.length + printTypeArguments(type.types);
        // else
        //     return "obj";
        FsType.Mapped "union" // TODO
    | SyntaxKind.AnyKeyword -> FsType.Mapped "obj"
    | SyntaxKind.VoidKeyword -> FsType.Mapped "unit"
    | SyntaxKind.TupleType ->
        // return type.elementTypes.map(getType).join(" * ");
        FsType.Mapped "tupple" // TODO
    | SyntaxKind.SymbolKeyword -> FsType.Mapped "Symbol"
    | SyntaxKind.ThisType -> FsType.Mapped "TODO_ThisType"
    | SyntaxKind.TypePredicate -> FsType.Mapped "TODO_TypePredicate"
    | SyntaxKind.TypeLiteral -> FsType.Mapped "TODO_TypeLiteral"
    | SyntaxKind.IntersectionType -> FsType.Mapped "TODO_IntersectionType"
    | SyntaxKind.IndexedAccessType -> FsType.Mapped "TODO_IndexedAccessType"
    | SyntaxKind.TypeQuery -> FsType.Mapped "TODO_TypeQuery"
    | SyntaxKind.LiteralType -> FsType.Mapped "TODO_LiteralType"
    | SyntaxKind.ExpressionWithTypeArguments ->
        let eta = t :?> ExpressionWithTypeArguments
        match eta.expression.kind with
        | SyntaxKind.Identifier ->
            let id = eta.expression :?> Identifier
            FsType.Mapped id.text
        | _ -> failwithf "unsupported TypeNode ExpressionWithTypeArguments kind: %A" eta.expression.kind
    | _ -> failwithf "unsupported TypeNode kind: %A" t.kind

let visitParameterDeclaration(pd: ParameterDeclaration): FsParam =
    {
        Name = pd.name |> getBindingyName
        Optional = pd.questionToken.IsSome
        Type = 
            match pd.``type`` with
            | Some t -> visitTypeNode t
            | None -> FsType.Mapped "obj"
    }

let visitMethodSignature(ms: MethodSignature): FsFunction =
    {
        Name = ms.name |> getPropertyName
        Params = ms.parameters |> List.ofArray |> List.map visitParameterDeclaration
        ReturnType =
            match ms.``type`` with
            | Some t -> visitTypeNode t
            | None -> FsType.Mapped "unit"
    }

let visitPropertySignature(ps: PropertySignature): FsProperty =
    {
        Name = ps.name |> getPropertyName
        Option = ps.questionToken.IsSome
        Type = 
            match ps.``type`` with
            | None -> FsType.None 
            | Some tp -> visitTypeNode tp
    }

let visitFunctionDeclaration(fd: FunctionDeclaration): FsFunction =
    {
        Name =
            match fd.name with
            | None -> "TodoNamelessFunction"
            | Some nm -> nm.text
        Params = fd.parameters |> List.ofArray |> List.map visitParameterDeclaration
        ReturnType =
            match fd.``type`` with
            | Some t -> visitTypeNode t
            | None -> FsType.Mapped "unit"
    }

let visitTypeElement(te: TypeElement): FsType =
    match te.kind with
    | SyntaxKind.IndexSignature -> FsType.TODO
    | SyntaxKind.MethodSignature ->
        visitMethodSignature (te :?> MethodSignature) |> FsType.Method
    | SyntaxKind.PropertySignature ->
        visitPropertySignature (te :?> PropertySignature) |> FsType.Property
    | SyntaxKind.CallSignature -> FsType.TODO
    | _ -> failwithf "unsupported TypeElement kind: %A" te.kind

let visitStatement(sd: Statement): FsType =
    match sd.kind with
    | SyntaxKind.InterfaceDeclaration ->
        visitInterface (sd :?> InterfaceDeclaration) |> FsType.Interface
    | SyntaxKind.EnumDeclaration ->
        visitEnum (sd :?> EnumDeclaration) |> FsType.Enum
    | SyntaxKind.TypeAliasDeclaration -> FsType.TODO
    | SyntaxKind.ClassDeclaration -> FsType.TODO
    | SyntaxKind.VariableStatement -> FsType.TODO
    | SyntaxKind.FunctionDeclaration ->
        visitFunctionDeclaration (sd :?> FunctionDeclaration) |> FsType.Function
    | SyntaxKind.ModuleDeclaration -> FsType.TODO
    | _ -> failwithf "unsupported Statement kind: %A" sd.kind

let visitModuleBlock(mb: ModuleBlock): FsModule =
    {
        Name = mb |> getModuleName
        Types = mb.statements |> List.ofArray |> List.map visitStatement
    }

let visitSourceFile(sf: SourceFile): FsFile =
    {
        Modules = getModules sf |> List.map visitModuleBlock
    }

let printType(tp: FsType): string =
    match tp with
    | FsType.Mapped s -> s
    | FsType.TODO -> "TODO"
    | FsType.Array at ->
        match at with
        | FsType.Mapped s -> sprintf "ResizeArray<%s>" s 
        | _ -> failwithf "unsupported Array ReturnType %A" tp
    | _ -> failwithf "unsupported ReturnType %A" tp

let printFunction(m: FsFunction): string =
    let line = ResizeArray()
    sprintf "abstract %s" (escapeWord m.Name) |> line.Add
    let prms = 
        m.Params |> List.map(fun p ->
            match p.Type with
            | FsType.Mapped t ->
                sprintf "%s%s: %s" (if p.Optional then "?" else "") (escapeWord p.Name) t
            | _ -> sprintf "TODO %A" p.Type
        )
    if prms.Length = 0 then
        sprintf ": unit" |> line.Add
    else
        sprintf ": %s" (prms |> String.concat " * ") |> line.Add
    sprintf " -> %s" (printType m.ReturnType) |> line.Add
    line |> String.concat ""

let printProperty(pr: FsProperty): string =
    sprintf "abstract %s: %s%s with get, set" (escapeWord pr.Name) (printType pr.Type) (if pr.Option then " option" else "")

let printCodeFile (file: FsFile) =
    // TODO namespace
    // TODO open statements
    for md in file.Modules do
        printfn ""
        printfn "module %s =" md.Name
        for tp in md.Types do
            printfn ""
            match tp with
            | FsType.Interface inf ->
                printfn "    and [<AllowNullLiteral>] %s =" inf.Name
                for ih in inf.Inherits do
                    printfn "        inherit %s" (printType ih)
                for mbr in inf.Members do
                    match mbr with
                    | FsType.Method m ->
                        printfn "        %s" (printFunction m)
                    | FsType.Property p ->
                        printfn "        %s" (printProperty p)
                    | _ -> ()
                if inf.Members.Length = 0 then
                    printfn "        interface end"
            | FsType.Enum en ->
                printfn "    and %s =" en.Name
                for cs in en.Cases do
                    match cs.Value with
                    | None -> printfn "        | %s" cs.Name
                    | Some v -> printfn "        | %s = %s" cs.Name v
            // | FsType.Function fn ->
                // printfn "    %s" (printFunction fn) // TODO How should we map these?
            | _ -> ()

let reserved =
    [
        "atomic"
        "break"
        "checked"
        "component"
        "const"
        "constraint"
        "constructor"
        "continue"
        "eager"
        "event"
        "external"
        "fixed"
        "functor"
        "include"
        "measure"
        "method"
        "mixin"
        "object"
        "parallel"
        "params"
        "process"
        "protected"
        "pure"
        "sealed"
        "tailcall"
        "trait"
        "virtual"
        "volatile"
        "asr"
        "land"
        "lor"
        "lsl"
        "lsr"
        "lxor"
        "mod"
        "sig"
    ]
    |> Set.ofList

let keywords = 
    [
        "abstract"
        "and"
        "as"
        "assert"
        "base"
        "begin"
        "class"
        "default"
        "delegate"
        "do"
        "done"
        "downcast"
        "downto"
        "elif"
        "else"
        "end"
        "exception"
        "extern"
        "false"
        "finally"
        "for"
        "fun"
        "function"
        "global"
        "if"
        "in"
        "inherit"
        "inline"
        "interface"
        "internal"
        "lazy"
        "let"
        "match"
        "member"
        "module"
        "mutable"
        "namespace"
        "new"
        "null"
        "of"
        "open"
        "or"
        "override"
        "private"
        "public"
        "rec"
        "return"
        "sig"
        "static"
        "struct"
        "then"
        "to"
        "true"
        "try"
        "type"
        "upcast"
        "use"
        "val"
        "void"
        "when"
        "while"
        "with"
        "yield"
    ]
    |> Set.ofList

let escapeWord s =
    if reserved.Contains s || keywords.Contains s then
        sprintf "``%s``" s
    else
        s

let printFile() =
    let filePath = @"c:\Users\camer\fs\ts2fable\node_modules\typescript\lib\typescript.d.ts"
    let code = Fs.readFileSync(filePath).toString()
    let tsFile = ts.createSourceFile(filePath, code, ScriptTarget.ES2015, true)

    let fsFile = visitSourceFile tsFile
    printCodeFile fsFile
    ()

printFile()