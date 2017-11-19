module ts2fable.Tests

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import

open ts2fable.Transform
open ts2fable.Naming


let [<Global>] describe (msg: string) (f: unit->unit): unit = jsNative
let [<Global>] it (msg: string) (f: unit->unit): unit = jsNative
let inline equal (expected: 'T) (actual: 'T): unit =
    Testing.Assert.AreEqual(expected, actual)

describe "my tests" <| fun _ ->

    it "addition" <| fun _ ->
        1 + 2
        |> equal 3


describe "escapeWord tests" <| fun _ ->

    it "done" <| fun _ ->
        escapeWord "done"
        |> equal "``done``"

    it "accessibility-support-changed" <| fun _ ->
        escapeWord "accessibility-support-changed"
        |> equal "``accessibility-support-changed``"

    it "accessibility-support-changed" <| fun _ ->
        escapeWord "2d_canvas"
        |> equal "``2d_canvas``"

    it ".json" <| fun _ ->
        escapeWord ".json"
        |> equal "``.json``"

    // do not escape
    it "NodeJS.ReadWriteStream" <| fun _ ->
        escapeWord "NodeJS.ReadWriteStream"
        |> equal "NodeJS.ReadWriteStream"

    it "_" <| fun _ ->
        escapeWord "_"
        |> equal "``_``"

describe "createEnumNameParts tests" <| fun _ ->

    it "simpleCase" <| fun _ ->
        createEnumNameParts "simpleCase"
        |> equal ["simple"; "Case"]

    it "simple-kebab-case" <| fun _ ->
        createEnumNameParts "simple-kebab-case" 
        |> equal ["simple"; "kebab"; "case"]

    it "Other-Cases" <| fun _ ->
        createEnumNameParts "Other-Cases" 
        |> equal ["Other"; "Cases"]

    it "helloThereItsMe" <| fun _ ->
        createEnumNameParts "helloThereItsMe" 
        |> equal ["hello"; "There"; "Its"; "Me"]

    it "OtherSimpleCase" <| fun _ ->
        createEnumNameParts "OtherSimpleCase" 
        |> equal ["Other";"Simple"; "Case"]

    it "2d-shadow" <| fun _ ->
        createEnumNameParts "2d-shadow" 
        |> equal ["N2d"; "shadow"]

    it "shadow-2d" <| fun _ ->
        createEnumNameParts "shadow-2d" 
        |> equal ["shadow"; "2d"]

    it "shadow-2d-more" <| fun _ ->
        createEnumNameParts "shadow-2d-more" 
        |> equal ["shadow"; "2d"; "more"]


describe "createModuleNameParts tests" <| fun _ ->

    it "jquery/dist/jquery.slim" <| fun _ ->
        createModuleNameParts "jquery/dist/jquery.slim"
        |> equal ["jquery"; "dist"; "jquery"; "slim"]

    it "JQuery" <| fun _ ->
        createModuleNameParts "JQuery"
        |> equal ["JQuery"]


describe "nameEqualsDefaultFableValue tests" <| fun _ ->

    it "none" <| fun _ ->
        nameEqualsDefaultFableValue "None" "none"
        |> equal true