// ts2fable 0.0.0
module rec ``#375-optional-tuple-gets-converted-into-optional-second-value``
open System
open Fable.Core
open Fable.Core.JS


type [<AllowNullLiteral>] A =
    abstract value1: float option with get, set
    abstract value2: (float * float) option with get, set
    abstract value3: (float * float * float) option with get, set
