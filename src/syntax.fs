[<AutoOpen>]
module rec ts2fable.Syntax

// our simplified syntax tree
// some names inspired by the actual F# AST:
// https://github.com/fsharp/FSharp.Compiler.Service/blob/master/src/fsharp/ast.fs

type FsInterface =
    {
        Comments: string list
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
        Index: FsParam option
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
        Type: FsType
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

let isFunction tp = match tp with | FsType.Function _ -> true | _ -> false
let isStringLiteral tp = match tp with | FsType.StringLiteral _ -> true | _ -> false
let isModule tp = match tp with | FsType.Module _ -> true | _ -> false

let asFunction (tp: FsType) = match tp with | FsType.Function v -> Some v | _ -> None
let asInterface (tp: FsType) = match tp with | FsType.Interface v -> Some v | _ -> None
let asGeneric (tp: FsType) = match tp with | FsType.Generic v -> Some v | _ -> None
let asStringLiteral (tp: FsType): string option = match tp with | FsType.StringLiteral v -> Some v | _ -> None
let asModule (tp: FsType) = match tp with | FsType.Module v -> Some v | _ -> None

type FsModule with
    member x.Modules with get() = x.Types |> List.filter isModule
    member x.NonModules with get() = x.Types |> List.filter (not << isModule)

let isStringLiteralParam (p: FsParam): bool = isStringLiteral p.Type

type FsFunction with
    member x.HasStringLiteralParams with get() = x.Params |> List.exists isStringLiteralParam
    member x.StringLiteralParams with get() = x.Params |> List.filter isStringLiteralParam
    member x.NonStringLiteralParams with get() = x.Params |> List.filter (not << isStringLiteralParam)

let isStatic (tp: FsType) =
    match tp with
    | FsType.Function fn -> fn.IsStatic
    | FsType.Interface it -> it.IsStatic
    | _ -> false

type FsInterface with
    member x.HasStaticMembers with get() = x.Members |> List.exists isStatic
    member x.StaticMembers with get() = x.Members |> List.filter isStatic
    member x.NonStaticMembers with get() = x.Members |> List.filter (not << isStatic)

type FsEnum with
    member x.Type
        with get() =
            if x.Cases |> List.exists (fun c -> c.Type = FsEnumCaseType.Unknown) then
                FsEnumCaseType.Unknown
            else if x.Cases |> List.exists (fun c -> c.Type = FsEnumCaseType.String) then
                FsEnumCaseType.String
            else
                FsEnumCaseType.Numeric