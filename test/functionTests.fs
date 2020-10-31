module ts2fable.functionTests

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

    it "NodeJS.ReadWriteStream" <| fun _ ->
        escapeWord "NodeJS.ReadWriteStream"
        |> equal "NodeJS.ReadWriteStream" // do not escape

    it "_" <| fun _ ->
        escapeWord "_"
        |> equal "``_``"

    it "_jsDocTypeBrand" <| fun _ ->
        escapeWord "_jsDocTypeBrand"
        |> equal "_jsDocTypeBrand" // do not escape

    it "[Symbol.iterator]" <| fun _ ->
        escapeWord "[Symbol.iterator]"
        |> equal "``[Symbol.iterator]``"


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


describe "getJsModuleName tests" <| fun _ ->

    it "chai full path" <| fun _ ->
        getJsModuleName "C:/Users/camer/fs/ts2fable/node_modules/@types/chai/index.d.ts"
        |> equal "chai"
    
    it "chalk relative path" <| fun _ ->
        getJsModuleName "node_modules/chalk/types/index.d.ts"
        |> equal "chalk"       
        
    it "reactxp relative path" <| fun _ ->
        getJsModuleName "node_modules/reactxp/dist/web/ReactXP.d.ts"
        |> equal "reactxp"            

    it "node relative path" <| fun _ ->
        getJsModuleName "node_modules/@types/node/index.d.ts"
        |> equal "node"

    it "typescript" <| fun _ ->
        getJsModuleName "node_modules/typescript/lib/typescript.d.ts"
        |> equal "typescript"
        
    it "izitoast" <| fun _ ->
        getJsModuleName "node_modules/izitoast/dist/izitoast/izitoast.d.ts"
        |> equal "izitoast"

    it "just file name" <| fun _ ->
        getJsModuleName "izitoast.d.ts"
        |> equal "izitoast"

    it "izitoast in parent dir" <| fun _ ->
        getJsModuleName "../izitoast.d.ts"
        |> equal "izitoast"

    it "izitoast in current dir" <| fun _ ->
        getJsModuleName "./izitoast.d.ts"
        |> equal "izitoast"

    let currentDirName = Node.Api.path.basename(Node.Api.``process``.cwd())

    it "just index.d.ts" <| fun _ ->
        getJsModuleName "index.d.ts"
        |> equal currentDirName

    it "index.d.ts in current dir" <| fun _ ->
        getJsModuleName "./index.d.ts"
        |> equal currentDirName

    it "index.d.ts in current dir via parent dir" <| fun _ ->
        getJsModuleName (sprintf "../%s/index.d.ts" currentDirName)
        |> equal currentDirName
    
    it "scoped package" <| fun _ ->
        getJsModuleName "node_modules/@slack/client"
        |> equal "@slack/client"

    it "scoped package in @types" <| fun _ ->
        getJsModuleName "node_modules/@types/slack__client"
        |> equal "@slack/client"


describe "fixModuleName tests" <| fun _ ->

    it "open" <| fun _ ->
        fixModuleName "open"
        |> equal "Open"

    it "module" <| fun _ ->
        fixModuleName "module"
        |> equal "Module"

    it "process" <| fun _ ->
        fixModuleName "process"
        |> equal "Process"

    it "assert" <| fun _ ->
        fixModuleName "assert"
        |> equal "Assert"