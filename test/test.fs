module ts2fable.Tests

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import

// let mocha: Mocha.IExports = importDefault "Mocha"
// let assertt: Chai.Chai.Assert = import "assert" "Chai"

[<Global>]
let describe (msg: string) (f: unit->unit): unit = jsNative

[<Global>]
let it (msg: string) (f: unit->unit): unit = jsNative

let inline equal (expected: 'T) (actual: 'T): unit =
    Testing.Assert.AreEqual(expected, actual)

describe "my tests" <| fun _ ->

    it "addition" <| fun _ ->
        1 + 2
        |> equal 4 // failing on purpose to test AppVeyor


describe "keyword tests" <| fun _ ->

    it "done is escaped" <| fun _ ->
        Keywords.escapeWord "done"
        |> equal "``done``"