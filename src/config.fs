module Config

let mutable EmitResizeArray = true
let mutable ConvertPropertyFunctions = false

let Reset() =
    EmitResizeArray <- true
    ConvertPropertyFunctions <- false

module OptionNames =
    let NoEmitResizeArray = "--noresizearray"
    let ConvertPropertyFunctions = "--convertpropfns"
    let Exports = "--export"
    let Help = "--help"
    let Version = "--version"

let Options =
    [
        (OptionNames.Exports,
         "Collect from multiple TS files" )

        (OptionNames.NoEmitResizeArray,
         "Use 'T[] instead of ResizeArray<'T>")

        (OptionNames.ConvertPropertyFunctions,
         "Convert lambda properties to member functions")

        (OptionNames.Version,
         "Show tool version" )

        (OptionNames.Help,
         "Show command usage" )

         ]

let Usage() =
    Options
        |> List.map (fun (opt,desc) -> sprintf "%-20s %s\n" opt desc)
        |> List.fold (+) ""