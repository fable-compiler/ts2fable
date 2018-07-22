// ts2fable 0.0.0
module rec wrapperModuleForExtraFile
open System
open Fable.Core
open Fable.Import.JS

module ReactXP = __web_ReactXP

module __web_ReactXP =
    let [<Import("*","test/web/ReactXP")>] reactXP: ReactXP.IExports = jsNative

    module ReactXP =

        type [<AllowNullLiteral>] IExports =
            abstract __spread: obj option
