// ts2fable 0.0.0
module rec f1
open System
open Fable.Core
open Fable.Import.JS

let [<Import("*","test")>] config: obj,obj,obj,obj,obj = jsNative

type RaceTimerResponse<'T> =
    obj,obj
