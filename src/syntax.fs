[<AutoOpen>]
module rec ts2fable.Syntax

// our simplified syntax tree
// some names inspired by the actual F# AST:
// https://github.com/fsharp/FSharp.Compiler.Service/blob/master/src/fsharp/ast.fs

[<RequireQualifiedAccess>]
type FsAccessibility =
| Public
| Protected
| Private

type FsInterface =
    {
        Comments: FsComment list
        IsStatic: bool // contains only static functions
        IsClass: bool
        Name: string
        FullName: string
        TypeParameters: FsType list
        Inherits: FsType list
        Members: FsType list
        Accessibility : FsAccessibility option
    }
with
    member x.HasStaticMembers = x.Members |> List.exists isStatic
    member x.StaticMembers = x.Members |> List.filter isStatic
    member x.NonStaticMembers = x.Members |> List.filter (not << isStatic)
    member x.HasConstructor = x.Members |> List.exists isConstructor
    member x.Constructors = x.Members |> List.filter isConstructor

type FsTypeLiteral =
    {
        Members: FsType list
    }

type FsGenericParameterDefaults = 
    {
        Default: FsType
        Name: string
        FullName: string
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
    member x.Type =
        if x.Cases |> List.exists (fun c -> c.Type = FsEnumCaseType.Unknown) then
            FsEnumCaseType.Unknown
        else if x.Cases |> List.exists (fun c -> c.Type = FsEnumCaseType.String) then
            FsEnumCaseType.String
        else
            FsEnumCaseType.Numeric

type FsParamComment =
    {
        Name: string
        Description: string list
    }

[<RequireQualifiedAccess>]
type FsComment =
    | SummaryLine of string
    | Param of FsParamComment
    | Unknown of string option

[<RequireQualifiedAccess>]
module FsComment =
    let isSummaryLine v = match v with | FsComment.SummaryLine _ -> true | _ -> false
    let asSummaryLine v = match v with | FsComment.SummaryLine o -> Some o | _ -> None
    let isParam v = match v with | FsComment.Param _ -> true | _ -> false
    let asParam v = match v with | FsComment.Param o -> Some o | _ -> None

type FsParam =
    {
        Comment: FsComment option
        Name: string
        Optional: bool
        ParamArray: bool
        Type: FsType
    }

[<RequireQualifiedAccess>]
module FsParam =
    let isStringLiteral (p: FsParam): bool = FsType.isStringLiteral p.Type
    let hasComment (p: FsParam): bool = p.Comment.IsSome
    let getComment (p: FsParam) = p.Comment

[<RequireQualifiedAccess>]
type FsFunctionKind =
    | Regular
    | Constructor
    | Call
    | StringParam of string

type FsFunction =
    {
        Comments: FsComment list
        Kind: FsFunctionKind
        IsStatic: bool
        Name: string option // declarations have them, signatures do not
        TypeParameters: FsType list
        Params: FsParam list
        ReturnType: FsType
        Accessibility : FsAccessibility option
    }
with
    member x.HasStringLiteralParams = x.Params |> List.exists FsParam.isStringLiteral
    member x.StringLiteralParams = x.Params |> List.filter FsParam.isStringLiteral
    member x.NonStringLiteralParams = x.Params |> List.filter (not << FsParam.isStringLiteral)
    member x.HasSummaryComments = x.Comments.Length > 0
    member x.HasParamComments = x.Params |> List.exists FsParam.hasComment
    member x.HasComments = x.HasSummaryComments || x.HasParamComments
    member x.SummaryLineComments = x.Comments |> List.choose FsComment.asSummaryLine
    member x.ParamComments = x.Params |> List.map FsParam.getComment |> List.choose id
    member x.AllComments = x.Comments @ x.ParamComments

[<RequireQualifiedAccess>]
type FsPropertyKind =
    | Regular
    | Index

type FsProperty =
    {
        Comments: FsComment list
        Kind: FsPropertyKind
        Index: FsParam option
        Name: string
        Option: bool
        Type: FsType
        IsReadonly: bool
        Accessibility : FsAccessibility option
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

[<RequireQualifiedAccess>]
type FsTupleKind =
    | Intersection
    | Tuple
    | Mapped

type FsTuple =
    {
        Types: FsType list
        Kind: FsTupleKind
    }
type FsImportSpecifier =
    {
        PropertyName: string option
        Name: string
    }

type FsTypeImport =
    {
        ImportSpecifier: FsImportSpecifier
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
    | Type of FsTypeImport
    | Module of ModuleImport

type FsExport =
    {
        IsGlobal: bool
        Selector: string
        Path: string
    }
[<CustomEquality;CustomComparison>]
type FsVariable =
    {
        Export: FsExport option
        HasDeclare: bool
        Name: string
        Type: FsType
        IsConst: bool
        Accessibility : FsAccessibility option
    }
    
with
    member x.IsGlobal = x.Export.IsSome && x.Export.Value.IsGlobal
    override x.Equals(y) = 
        match y with 
        | :? FsVariable as other -> x.Name = other.Name
        | _ -> false
    override x.GetHashCode() = hash x.Name
    interface System.IComparable with 
        member x.CompareTo y = 
            match y with 
            | :? FsVariable as vb -> compare x.Name vb.Name
            | _ -> invalidArg "y" "cannot compare values of different types"

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
    | GenericParameterDefaults of FsGenericParameterDefaults

[<RequireQualifiedAccess>]
module FsType =
    let isFunction tp = match tp with | FsType.Function _ -> true | _ -> false
    let isInterface tp = match tp with | FsType.Interface _ -> true | _ -> false
    let isStringLiteral tp = match tp with | FsType.StringLiteral _ -> true | _ -> false
    let isModule tp = match tp with | FsType.Module _ -> true | _ -> false
    let isImport tp = match tp with | FsType.Import _ -> true | _ -> false
    let isVariable tp = match tp with | FsType.Variable _ -> true | _ -> false
    let isAlias tp = match tp with | FsType.Alias _ -> true | _ -> false
    let isGeneric tp = match tp with | FsType.Generic _ -> true | _ -> false

    let asMapped (tp: FsType) = match tp with | FsType.Mapped v -> Some v | _ -> None
    let asFunction (tp: FsType) = match tp with | FsType.Function v -> Some v | _ -> None
    let asInterface (tp: FsType) = match tp with | FsType.Interface v -> Some v | _ -> None
    let asGeneric (tp: FsType) = match tp with | FsType.Generic v -> Some v | _ -> None
    let asStringLiteral (tp: FsType): string option = match tp with | FsType.StringLiteral v -> Some v | _ -> None
    let asModule (tp: FsType) = match tp with | FsType.Module v -> Some v | _ -> None
    let asVariable (tp: FsType) = match tp with | FsType.Variable v -> Some v | _ -> None
    let asExportAssignment (tp: FsType) = match tp with | FsType.ExportAssignment v -> Some v | _ -> None
    let asGenericParameterDefaults (tp: FsType) = match tp with | FsType.GenericParameterDefaults v -> Some v | _ -> None

type FsModule =
    {
        HasDeclare: bool
        IsNamespace: bool
        Name: string
        Types: FsType list
        HelperLines: string list
        Attributes: string list
    }
with
    member x.IsHelper = x.HelperLines.Length > 0
    member x.HasAttributes = x.Attributes.Length > 0

[<RequireQualifiedAccess>]
type FsFileKind =
    | Index 
    | Extra of string(*relative path to index file*)
    

type FsFile =
    {
        Kind: FsFileKind
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

let isStatic (tp: FsType) =
    match tp with
    | FsType.Function fn -> fn.IsStatic
    | FsType.Interface it -> it.IsStatic
    | _ -> false

let isConstructor (tp: FsType) =
    match tp with
    | FsType.Function fn -> fn.Kind = FsFunctionKind.Constructor
    | _ -> false

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
    | FsType.Import im ->
        match im with 
        | FsImport.Type imtp -> imtp.ImportSpecifier.Name
        | _ -> "" 
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
  
let getAccessibility (tp: FsType) : FsAccessibility option =
    match tp with
    | FsType.Interface it -> it.Accessibility
    | FsType.Function fn -> fn.Accessibility
    | FsType.Property pr -> pr.Accessibility
    | FsType.Variable vb -> vb.Accessibility
    | FsType.Module _
    | FsType.Enum _
    | FsType.Param _
    | FsType.Alias _
    | FsType.File _
    | FsType.Generic _
    | FsType.Mapped _
    | FsType.Import _
    | FsType.Array _
    | FsType.ExportAssignment _
    | FsType.GenericParameterDefaults _
    | FsType.None
    | FsType.TODO
    | FsType.StringLiteral _
    | FsType.This
    | FsType.Tuple _
    | FsType.TypeLiteral _
    | FsType.Union _
    | FsType.FileOut _ -> None