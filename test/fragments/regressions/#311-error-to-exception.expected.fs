// ts2fable 0.0.0
module rec ``#311-error-to-exception``
open System
open Fable.Core
open Fable.Import.JS

let [<Import("ErrorExceptionTest","test")>] errorExceptionTest: ErrorExceptionTest.IExports = jsNative

module ErrorExceptionTest =

    type [<AllowNullLiteral>] IExports =
        abstract instanceErrorProperty: System.Exception
        abstract instanceInheritFromErrorProperty: InheritFromError

    type InheritFromError =
        System.Exception
