// ts2fable 0.0.0
module rec Mkdirp
open System
open Fable.Core
open Fable.Import.JS
open Node
let [<Import("*","mkdirp")>] mkdirp: Mkdirp.IExports = jsNative

type [<AllowNullLiteral>] IExports =
    abstract mkdirp: dir: string * cb: (NodeJS.ErrnoException -> Mkdirp.Made -> unit) -> unit
    abstract mkdirp: dir: string * opts: U2<Mkdirp.Mode, Mkdirp.Options> * cb: (NodeJS.ErrnoException -> Mkdirp.Made -> unit) -> unit

module Mkdirp =

    type [<AllowNullLiteral>] IExports =
        abstract sync: dir: string * ?opts: U2<Mode, OptionsSync> -> Made

    type Made =
        string option

    type Mode =
        U2<float, string> option

    [<RequireQualifiedAccess; CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module Mode =
        let ofFloatOption v: Mode = v |> Option.map U2.Case1
        let ofFloat v: Mode = v |> U2.Case1 |> Some
        let isFloat (v: Mode) = match v with None -> false | Some o -> match o with U2.Case1 _ -> true | _ -> false
        let asFloat (v: Mode) = match v with None -> None | Some o -> match o with U2.Case1 o -> Some o | _ -> None
        let ofStringOption v: Mode = v |> Option.map U2.Case2
        let ofString v: Mode = v |> U2.Case2 |> Some
        let isString (v: Mode) = match v with None -> false | Some o -> match o with U2.Case2 _ -> true | _ -> false
        let asString (v: Mode) = match v with None -> None | Some o -> match o with U2.Case2 o -> Some o | _ -> None

    type [<AllowNullLiteral>] FsImplementation =
        abstract mkdir: obj with get, set
        abstract stat: obj with get, set

    type [<AllowNullLiteral>] FsImplementationSync =
        abstract mkdirSync: obj with get, set
        abstract statSync: obj with get, set

    type [<AllowNullLiteral>] Options =
        abstract mode: Mode option with get, set
        abstract fs: FsImplementation option with get, set

    type [<AllowNullLiteral>] OptionsSync =
        abstract mode: Mode option with get, set
        abstract fs: FsImplementationSync option with get, set
