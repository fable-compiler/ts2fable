// ts2fable 0.0.0
module rec f8
open System
open Fable.Core
open Fable.Import.JS

type Validator = React.Validator

type [<AllowNullLiteral>] ValidationMap<'T> =
    interface end
