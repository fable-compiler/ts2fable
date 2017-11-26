[<AutoOpen>]
module rec ts2fable.Syntax

// our simplified syntax tree
// some names inspired by the actual F# AST:
// https://github.com/fsharp/FSharp.Compiler.Service/blob/master/src/fsharp/ast.fs

type FsInterface =
    {
        Comments: string list
        IsStatic: bool // contains only static functions
        IsClass: bool
        Name: string
        FullName: string
        TypeParameters: FsType list
        Inherits: FsType list
        Members: FsType list
    }

type FsTypeLiteral =
    {
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

type FsParam =
    {
        Name: string
        Optional: bool
        ParamArray: bool
        Type: FsType
    }

[<RequireQualifiedAccess>]
type FsFunctionKind =
    | Regular
    | Constructor
    | Call
    | StringParam of string

type FsFunction =
    {
        Comments: string list
        Kind: FsFunctionKind
        IsStatic: bool
        Name: string option // declarations have them, signatures do not
        TypeParameters: FsType list
        Params: FsParam list
        ReturnType: FsType
    }

[<RequireQualifiedAccess>]
type FsPropertyKind =
    | Regular
    | Index

type FsProperty =
    {
        Comments: string list
        Kind: FsPropertyKind
        Index: FsParam option
        Name: string
        Option: bool
        Type: FsType
        IsReadonly: bool
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

type TypeImport =
    {
        Type: string
        SpecifiedModule: string
        ResolvedModule: string option
    }

type ModuleImport =
    {
        Module: string
        SpecifiedModule: string
        ResolvedModule: string option
    }

[<RequireQualifiedAccess>]
type FsImport =
    | Type of TypeImport
    | Module of ModuleImport

type FsExport =
    {
        IsGlobal: bool
        Selector: string
        Path: string
    }

type FsVariable =
    {
        Export: FsExport option
        HasDeclare: bool
        Name: string
        Type: FsType
        IsConst: bool
    }

type FsMapped =
    {
        // Namespace: string list // TODO
        Name: string
        FullName: string
    }

let simpleType name: FsType =
    { 
        // Namespace = []
        Name = name
        FullName = name 
    }
    |> FsType.Mapped

[<RequireQualifiedAccess>]
type FsType =
    | Interface of FsInterface
    | Enum of FsEnum
    | Property of FsProperty
    | Param of FsParam
    | Array of FsType
    | TODO
    | None // when it is not set
    | Mapped of FsMapped
    | Function of FsFunction
    | Union of FsUnion
    | Alias of FsAlias
    | Generic of FsGenericType
    | Tuple of FsTuple
    | Module of FsModule
    | File of FsFile
    | FileOut of FsFileOut
    | Variable of FsVariable
    | StringLiteral of string
    | ExportAssignment of string
    | This
    | Import of FsImport
    | TypeLiteral of FsTypeLiteral

type FsModule =
    {
        HasDeclare: bool
        Name: string
        Types: FsType list
        HelperLines: string list
        Attributes: string list
    }

type FsFile =
    {
        FileName: string
        ModuleName: string
        Modules: FsModule list
    }

type FsFileOut =
    {
        Namespace: string
        Opens: string list
        Files: FsFile list
    }

let isFunction tp = match tp with | FsType.Function _ -> true | _ -> false
let isStringLiteral tp = match tp with | FsType.StringLiteral _ -> true | _ -> false
let isModule tp = match tp with | FsType.Module _ -> true | _ -> false
let isVariable tp = match tp with | FsType.Variable _ -> true | _ -> false

let asFunction (tp: FsType) = match tp with | FsType.Function v -> Some v | _ -> None
let asInterface (tp: FsType) = match tp with | FsType.Interface v -> Some v | _ -> None
let asGeneric (tp: FsType) = match tp with | FsType.Generic v -> Some v | _ -> None
let asStringLiteral (tp: FsType): string option = match tp with | FsType.StringLiteral v -> Some v | _ -> None
let asModule (tp: FsType) = match tp with | FsType.Module v -> Some v | _ -> None
let asVariable (tp: FsType) = match tp with | FsType.Variable v -> Some v | _ -> None
let asExportAssignment (tp: FsType) = match tp with | FsType.ExportAssignment v -> Some v | _ -> None

// type FsModule with
    // member x.Modules = x.Types |> List.filter isModule
    // member x.NonModules = x.Types |> List.filter (not << isModule)
    // member x.Variables = x.Types |> List.choose asVariable

let isStringLiteralParam (p: FsParam): bool = isStringLiteral p.Type

type FsFunction with
    member x.HasStringLiteralParams = x.Params |> List.exists isStringLiteralParam
    member x.StringLiteralParams = x.Params |> List.filter isStringLiteralParam
    member x.NonStringLiteralParams = x.Params |> List.filter (not << isStringLiteralParam)

let isStatic (tp: FsType) =
    match tp with
    | FsType.Function fn -> fn.IsStatic
    | FsType.Interface it -> it.IsStatic
    | _ -> false

let isConstructor (tp: FsType) =
    match tp with
    | FsType.Function fn -> fn.Kind = FsFunctionKind.Constructor
    | _ -> false

type FsInterface with
    member x.HasStaticMembers = x.Members |> List.exists isStatic
    member x.StaticMembers = x.Members |> List.filter isStatic
    member x.NonStaticMembers = x.Members |> List.filter (not << isStatic)
    member x.HasConstructor = x.Members |> List.exists isConstructor
    member x.Constructors = x.Members |> List.filter isConstructor

type FsEnum with
    member x.Type =
        if x.Cases |> List.exists (fun c -> c.Type = FsEnumCaseType.Unknown) then
            FsEnumCaseType.Unknown
        else if x.Cases |> List.exists (fun c -> c.Type = FsEnumCaseType.String) then
            FsEnumCaseType.String
        else
            FsEnumCaseType.Numeric

let rec getName (tp: FsType) =
    match tp with
    | FsType.Interface it -> it.Name
    | FsType.Enum en -> en.Name
    | FsType.Param pm -> pm.Name
    | FsType.Function fn -> fn.Name |> Option.defaultValue ""
    | FsType.Property pr -> pr.Name
    | FsType.Alias al -> al.Name
    | FsType.Variable vb -> vb.Name
    | FsType.Module md -> md.Name
    | FsType.File fl -> fl.ModuleName
    | FsType.Generic gn -> getName gn.Type
    | FsType.Mapped mp -> mp.Name
    | FsType.Array ar ->
        let name = getName ar
        if name = "" then "" else sprintf "%sArray" name
    | _ -> ""

let rec getFullName (tp: FsType) =
    match tp with
    | FsType.Interface it -> it.FullName
    | FsType.Mapped en -> en.FullName
    | FsType.Generic gn -> getFullName gn.Type
    | FsType.File fl -> fl.FileName
    | _ -> getName tp

type FsVariable with
    member x.IsGlobal = x.Export.IsSome && x.Export.Value.IsGlobal

type FsModule with
    member x.IsHelper = x.HelperLines.Length > 0
    member x.HasAttributes = x.Attributes.Length > 0