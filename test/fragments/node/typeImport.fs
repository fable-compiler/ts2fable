// ts2fable 0.0.0
module rec typeImport
open System
open Fable.Core
open Fable.Import.JS


module Url =
    type ParsedUrlQuery = Querystring.ParsedUrlQuery

module Http =
    type URL = Url.URL
