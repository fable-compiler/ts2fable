// ts2fable 0.0.0
module rec ``#463-literal-parameters``

#nowarn "3390" // disable warnings for invalid XML comments

open System
open Fable.Core
open Fable.Core.JS


/// <summary>
/// <c>myTest1</c> &amp; <c>myTest2</c> &amp; <c>myTest4</c> should get reduced to just one <c>member</c> each
/// 
/// all others should be transformed to unit parameter with value in emit &amp; name
/// </summary>
module Ns =

    type [<AllowNullLiteral>] IExports =
        abstract myTest1: v: obj option -> bool
        [<Emit("$0.myTest2($1,false)")>] abstract myTest2_false: v: obj option -> bool
        abstract myTest2: v: obj option * condition: bool -> bool
        [<Emit("$0.myTest3('hello')")>] abstract myTest3_hello: unit -> bool
        [<Emit("$0.myTest3('world')")>] abstract myTest3_world: unit -> bool
        abstract myTest4: v: obj option -> bool
        [<Emit("$0.myTest5('hello')")>] abstract myTest5_hello: unit -> bool
        [<Emit("$0.myTest5('world')")>] abstract myTest5_world: unit -> bool
        [<Emit("$0.myTest6('hello','world')")>] abstract myTest6_hello_world: unit -> bool
        [<Emit("$0.myTest6('world','hello')")>] abstract myTest6_world_hello: unit -> bool
        [<Emit("$0.myTest7('hello')")>] abstract myTest7_hello: unit -> string
        [<Emit("$0.myTest7('world')")>] abstract myTest7_world: unit -> string
        [<Emit("$0.myTest8(3.14)")>] abstract ``myTest8_3.14``: unit -> unit
        [<Emit("$0.myTest8(2.72)")>] abstract ``myTest8_2.72``: unit -> unit
        [<Emit("$0.myTest9(42)")>] abstract myTest9_42: unit -> unit
        [<Emit("$0.myTest9(123)")>] abstract myTest9_123: unit -> unit
        [<Emit("$0.myTest10(false)")>] abstract myTest10_false: unit -> unit
        [<Emit("$0.myTest10(true)")>] abstract myTest10_true: unit -> unit
        [<Emit("$0.myTest11($1,42,123)")>] abstract myTest11_42_123: a: string -> unit
        [<Emit("$0.myTest11($1,'foo',3.14)")>] abstract ``myTest11_foo_3.14``: a: string -> unit
