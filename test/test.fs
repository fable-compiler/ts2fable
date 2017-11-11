module ts2fable.Tests

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import

open ts2fable.Transform
open ts2fable.Enum


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


describe "createNameParts tests" <| fun _ ->

    it "simpleCase" <| fun _ ->
        createNameParts "simpleCase"
        |> equal ["simple"; "Case"]

    it "simple-kebab-case" <| fun _ ->
        createNameParts "simple-kebab-case" 
        |> equal ["simple"; "kebab"; "case"]

    it "Other-Cases" <| fun _ ->
        createNameParts "Other-Cases" 
        |> equal ["Other"; "Cases"]

    it "helloThereItsMe" <| fun _ ->
        createNameParts "helloThereItsMe" 
        |> equal ["hello"; "There"; "Its"; "Me"]

    it "OtherSimpleCase" <| fun _ ->
        createNameParts "OtherSimpleCase" 
        |> equal ["Other";"Simple"; "Case"]

    it "2d-shadow" <| fun _ ->
        createNameParts "2d-shadow" 
        |> equal ["N2d"; "shadow"]

    it "shadow-2d" <| fun _ ->
        createNameParts "shadow-2d" 
        |> equal ["shadow"; "2d"]

    it "shadow-2d-more" <| fun _ ->
        createNameParts "shadow-2d-more" 
        |> equal ["shadow"; "2d"; "more"]

    it "jquery/dist/jquery.slim" <| fun _ ->
        createNameParts "jquery/dist/jquery.slim"
        |> equal ["jquery"; "dist"; "jquery"; "slim"]