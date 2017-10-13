module FableApp

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Fable.Import.Node
open Fable.Import.ts

printfn "hello"

printfn "version: %s" ts.version

let node = ts.createNode SyntaxKind.NumericLiteral
printfn "node: %A" node