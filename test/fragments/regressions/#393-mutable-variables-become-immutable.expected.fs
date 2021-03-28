// ts2fable 0.0.0
module rec ``#393-mutable-variables-become-immutable``
open System
open Fable.Core
open Fable.Core.JS

let [<Import("M","#393-mutable-variables-become-immutable")>] m: M.IExports = jsNative
let [<Import("N","#393-mutable-variables-become-immutable")>] n: N.IExports = jsNative
/// <summary>
/// <c>var</c> -> mutable 
/// 
/// BUT: 
/// > error FS0874: Mutable 'let' bindings can't be recursive or defined in recursive modules or namespaces
/// -> keep immutable
/// </summary>
let [<Import("v2","#393-mutable-variables-become-immutable")>] v2: float = jsNative
/// <summary>
/// <c>let</c> -> mutable 
/// 
/// BUT: 
/// > error FS0874: Mutable 'let' bindings can't be recursive or defined in recursive modules or namespaces
/// -> keep immutable
/// </summary>
let [<Import("l1","#393-mutable-variables-become-immutable")>] l1: float = jsNative
/// <summary><c>const</c> -> immutable</summary>
let [<Import("c1","#393-mutable-variables-become-immutable")>] c1: float = jsNative


module M =

    type [<AllowNullLiteral>] IExports =
        /// <summary><c>var</c> -> mutable</summary>
        abstract v2: float with get, set
        /// <summary><c>let</c> -> mutable</summary>
        abstract l1: float with get, set
        /// <summary><c>const</c> -> immutable</summary>
        abstract c1: float

module N =

    type [<AllowNullLiteral>] IExports =
        /// <summary><c>var</c> -> mutable</summary>
        abstract v2: float with get, set
        /// <summary><c>let</c> -> mutable</summary>
        abstract l1: float with get, set
        /// <summary><c>const</c> -> immutable</summary>
        abstract c1: float
