module ts2fable.Tests

open Fable.Core.JsInterop
open Fable.Import

let mocha: Mocha.IExports = importDefault "Mocha"
let assertt: Chai.Chai.Assert = import "assert" "Chai"

mocha.describe.Invoke("my suite", fun _ ->
    mocha.it.Invoke("my test", fun donee -> 
        assertt.equal(1, 1)
        donee.Invoke()
    )|> ignore
    
) |> ignore