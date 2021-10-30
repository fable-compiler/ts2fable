module Config

let mutable EmitResizeArray = true
let mutable ConvertPropertyFunctions = false

let Reset() =
    EmitResizeArray <- true
    ConvertPropertyFunctions <- false

module OptionNames =
    let NoEmitResizeArray = "--noresizearray"
    let ConvertPropertyFunctions = "--convertpropfns"