module ts2fable.node.Transform

open Fable.Core
open Fable.Core.JsInterop
open TypeScript
open TypeScript.Ts
open System.Collections.Generic
open System
open ts2fable.Naming
open System.Collections
open System.Collections.Generic
open ts2fable.Syntax
open ts2fable.Keywords
open Fable
open Node.Api

module Common = ts2fable.Transform

let fixNamespace (f: FsFile): FsFile =
    Common.fixNamespace f |> fun f ->
        match f.Kind with
        | FsFileKind.Index -> f
        | FsFileKind.Extra extra ->
        f |> Common.fixFile (fun ns tp ->
            match tp with
            | FsType.Import im ->
                match im with
                | FsImport.Type imtp ->
                    { imtp with
                        SpecifiedModule =
                            match imtp.SpecifiedModule with
                                | ModuleName.Parts _ ->
                                    let dir = path.dirname extra
                                    let joinedPath = path.join [| dir; imtp.SpecifiedModule |]
                                    joinedPath |> ModuleName.normalize
                                | _ -> imtp.SpecifiedModule
                                |> fixModuleName
                    }
                    |> FsImport.Type
                    |> FsType.Import
                | _ -> tp
            | _ -> tp
        )
