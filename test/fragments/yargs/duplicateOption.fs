// ts2fable 0.0.0
module rec duplicatedOption
open System
open Fable.Core
open Fable.Import.JS


type [<AllowNullLiteral>] Options =
    abstract ``default``: obj option with get, set
