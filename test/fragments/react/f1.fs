// ts2fable 0.0.0
module rec f1
open System
open Fable.Core
open Fable.Import.JS


module React =

    type Ref<'T> =
        U2<string, ('T option -> obj option)>

    [<RequireQualifiedAccess; CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module Ref =
        let ofString v: Ref<'T> = v |> U2.Case1
        let isString (v: Ref<'T>) = match v with U2.Case1 _ -> true | _ -> false
        let asString (v: Ref<'T>) = match v with U2.Case1 o -> Some o | _ -> None
        let ofBivarianceHack v: Ref<'T> = v |> U2.Case2
        let isBivarianceHack (v: Ref<'T>) = match v with U2.Case2 _ -> true | _ -> false
        let asBivarianceHack (v: Ref<'T>) = match v with U2.Case2 o -> Some o | _ -> None
