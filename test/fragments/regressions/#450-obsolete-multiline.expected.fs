// ts2fable 0.0.0
module rec ``#450-obsolete-multiline``

#nowarn "0044" // disable warnings for `Obsolete` usage

open System
open Fable.Core
open Fable.Core.JS


module Ns1 =

    module Ns2 =

        type [<AllowNullLiteral>] SomeInterface =
            /// Some Value
            [<Obsolete("Value is deprecated
 
The reason spans multiple lines,  
which might lead to incorrect indentation  
when last line is shorter than indentation

!"          )>]
            abstract Alpha: float with get, set
            /// Some Value
            [<Obsolete("Value is deprecated
 
A long last line doesn't need any additional spaces")>]
            abstract Beta: float with get, set
            /// Some Value
            [<Obsolete("Value is deprecated. And a single line does never need spaces")>]
            abstract Gamma: float with get, set

