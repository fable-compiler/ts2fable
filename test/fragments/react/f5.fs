// ts2fable 0.0.0
module rec f5
open System
open Fable.Core
open Fable.Import.JS


type [<AllowNullLiteral>] Validator<'T> =
    abstract invoke: ``object``: 'T * key: string * componentName: string * [<ParamArray>] rest: ResizeArray<obj option> -> Error option
