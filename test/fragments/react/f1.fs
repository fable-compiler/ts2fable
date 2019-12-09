// ts2fable 0.0.0
module rec f1
open System
open Fable.Core
open Fable.Core.JS


module React =

    type Ref<'T> =
        U2<string, ('T option -> obj option)>
