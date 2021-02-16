module ts2fable.fsFileTests

open Fable.Core
open Fable.Core.JsInterop

open ts2fable.Naming
open TypeScript
open TypeScript.Ts
open Node.Api
open ts2fable.Read
open ts2fable.node.Write
open ts2fable.Transform
open ts2fable.Print
open System.Collections.Generic
open ts2fable.Keywords

let [<Global>] describe (msg: string) (f: unit->unit): unit = jsNative
let [<Global>] it (msg: string) (f: unit->unit): unit = jsNative

// use only to debug single test
let [<Emit("it.only($0,$1)")>] only (msg: string) (f: unit->unit): unit = jsNative
let [<Emit("this.timeout($0)")>] timeout (duration: int): unit = jsNative
let inline equal (expected: 'T) (actual: 'T): unit =
    Testing.Assert.AreEqual(expected, actual)
let inline notEqual (expected: 'T) (actual: 'T): unit =
    Testing.Assert.NotEqual(expected, actual)

let testFsFilesWithExports tsPaths fsPath exports (f: FsFile list -> unit) =

    let fsFileOut = getFsFileOut fsPath tsPaths exports
    emitFsFileOut fsPath fsFileOut 
    f fsFileOut.Files

let testFsFiles tsPaths fsPath (f: FsFile list -> unit) =
    testFsFilesWithExports tsPaths fsPath [] f
    

let testFsFileLines tsPaths fsPath (f: string list -> unit) =

    let fsFileOut = getFsFileOut fsPath tsPaths []
    emitFsFileOutAsLines fsPath fsFileOut 
    |> f


// make sure tests are strict
describe "transform tests" <| fun _ ->
    timeout 10000
    
    let getTypeByName name fsFiles =
        getAllTypes fsFiles
        |> List.filter(fun tp -> getName tp = name)

    let existOnlyOneByName name (predicate: FsType -> bool) fsFiles= 
        fsFiles
        |> getTypeByName name
        |> List.filter predicate
        |> fun tp -> tp.Length = 1

    let existLeastOneByName name (predicate: FsType -> bool) fsFiles= 
        fsFiles
        |> getTypeByName name
        |> List.filter predicate
        |> fun tp -> tp.Length > 0     

    let existNone (predicate: FsType -> bool) fsFiles= 
        fsFiles
        |> getAllTypes
        |> List.filter predicate
        |> fun tp -> tp.Length = 0        

    let existNoneByName name (predicate: FsType -> bool) fsFiles= 
        fsFiles
        |> getTypeByName name
        |> List.filter predicate
        |> fun tp -> tp.Length = 0                  

    let existManyByName i name (predicate: FsType -> bool) fsFiles= 
        fsFiles
        |> getTypeByName name
        |> List.filter predicate
        |> fun tp -> tp.Length = i   

    let getTopTypes fsFiles = 
        fsFiles
        |> List.head
        |> fun f ->  f.Modules
        |> List.head
        |> fun md -> md.Types
    
    let getTopVarialbles fsFiles = 
        fsFiles
        |> getTopTypes
        |> List.choose FsType.asVariable 

    let inline (<&&>) (t1:FsFile list -> bool) (t2:FsFile list -> bool) fsFiles =
        fsFiles |> t1 && fsFiles |> t2        

    let importSpecifierHasNames name propertyName =
            (fun tp ->
                match tp with 
                | FsType.Import ti -> 
                    match ti with 
                    | FsImport.Type imtp ->
                        let imsp = imtp.ImportSpecifier
                        imsp.Name = name
                        && imsp.PropertyName = propertyName
                    | _ -> false                    
                | _ -> false
            )
    let containLineHasWord tsPaths fsPath line = testFsFileLines tsPaths fsPath (stringContainsAny line >> equal true)

    // clean up an fs file for a comparison later
    let sanitizeFsFile (lines:#seq<string>) : string array =
        lines
        |> Seq.filter (not << System.String.IsNullOrWhiteSpace)
        |> Seq.filter (fun l ->
            // ignore normal comments, but not xml comments
            (not <| l.TrimStart().StartsWith("//"))
            ||
            l.TrimStart().StartsWith("///")
        )
        |> Seq.map (fun l -> l.TrimEnd())
        |> Seq.toArray
    
    let fileLinesCompare compare expected actual =
        compare (sanitizeFsFile expected) (sanitizeFsFile actual)

    let convertAndCompareAgainstExpectedWithComparison compare tsPaths fsPath expected =
        testFsFileLines tsPaths fsPath <| fun lines ->
            let expected =
                let lines = ts2fable.node.FileSystem.readLines (expected)
                Seq.toArray lines
            fileLinesCompare compare expected lines
    let convertAndCompareAgainstExpected = convertAndCompareAgainstExpectedWithComparison equal

    let runRegressionTestWithComparison compare name =
        let tsPaths = [sprintf "test/fragments/regressions/%s.d.ts" name]
        let fsPath = sprintf "test/fragments/regressions/%s.fs" name
        let expected = sprintf "test/fragments/regressions/%s.expected.fs" name
        convertAndCompareAgainstExpectedWithComparison compare tsPaths fsPath expected
    let runRegressionTest = runRegressionTestWithComparison equal


    // https://github.com/fable-compiler/ts2fable/issues/154
    it "duplicated variable exports" <| fun _ ->
        let tsPaths = ["node_modules/reactxp/dist/web/ReactXP.d.ts"]
        let fsPath = "test/fragments/reactxp/duplicatedVariableExports.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles
            |> getTopVarialbles 
            |> List.countBy(fun vb -> vb.Name)
            |> List.forall(fun (_,l) -> l = 1)
            |> equal true

    // https://github.com/fable-compiler/ts2fable/issues/246
    it "add wrapper module to extra files" <| fun _ ->
        // let tsPaths = ["node_modules/reactxp/dist/ReactXP.d.ts"]
        let tsPaths = ["test/fragments/reactxp/multiple/ReactXP.d.ts"]
        let fsPath = "test/fragments/reactxp/multiple/ReactXP.fs"
        testFsFilesWithExports tsPaths fsPath ["multiple"] <| fun fsFiles ->
            fsFiles
            |> 
                ( 
                    existOnlyOneByName "__web_ReactXP" FsType.isModule 
                    <&&> existOnlyOneByName "__web_Accessibility" FsType.isModule 
                    <&&> existOnlyOneByName "reactXP" (fun tp ->
                        match FsType.asVariable tp with 
                        | Some vb -> 
                            match vb.Export with 
                            | Some ex -> ex.Path = "ReactXP/web/ReactXP"
                            | None -> false
                        | None -> false
                    ) 
                )
            |> equal true

    // https://github.com/fable-compiler/ts2fable/issues/251
    it "typeImport for importSpecifier that has propertyname" <| fun _ ->
        let tsPaths = ["test/fragments/reactxp/multiple/ReactXP.d.ts"]
        let fsPath = "test/fragments/reactxp/multiple/ReactXP.fs"
        testFsFilesWithExports tsPaths fsPath ["multiple"] <| fun fsFiles ->
            fsFiles
            |> existOnlyOneByName 
                "CommonAccessibility" 
                (importSpecifierHasNames "CommonAccessibility" (Some "Accessibility"))
            |> equal true

    // https://github.com/fable-compiler/ts2fable/pull/164
    it "multiple ts inputs should export one time" <| fun _ ->
        let tsPaths =         
            [   
                "node_modules/@types/google-protobuf/index.d.ts"
                "node_modules/@types/google-protobuf/google/protobuf/empty_pb.d.ts"
            ]
        let fsPath = "test-compile/Protobuf.fs"
        testFsFileLines tsPaths fsPath  <| fun lines ->
            lines.Length < 700
            |> equal true
    
    // https://github.com/fable-compiler/ts2fable/issues/175
    it "fix some Option.map to Microsoft.FSharp.Core.Option.map" <| fun _ ->
        let tsPaths = ["node_modules/@types/react/index.d.ts"]
        let fsPath = "test-compile/React.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> existLeastOneByName "ReactNode" FsType.isMapped
            // (fun tp ->
            //     match tp with 
            //     | FsType.Module md -> 
            //         md.HelperLines  |> List.exists(fun l -> l.Contains("Microsoft.FSharp.Core.Option.map"))
            //     | _ -> false
            // )
            |> equal true

    // https://github.com/fable-compiler/ts2fable/pull/170
    it "compile type alias has only function to interface" <| fun _ ->
        let tsPaths = ["test/fragments/react/f2.d.ts"]
        let fsPath = "test/fragments/react/f2.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> existOnlyOneByName "DOMFactory" FsType.isInterface
            |> equal true

    // https://github.com/fable-compiler/ts2fable/pull/167        
    it "extract type literal from union" <| fun _ ->
        let tsPaths = ["test/fragments/react/f1.d.ts"]
        let fsPath = "test/fragments/react/f1.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> existOnlyOneByName "bivarianceHack" FsType.isFunction
            |> equal true

    // https://github.com/fable-compiler/ts2fable/issues/177        
    it "generic parameter defaults" <| fun _ ->
        let tsPaths = ["test/fragments/react/f3.d.ts"]
        let fsPath = "test/fragments/react/f3.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> existManyByName 2 "Component" FsType.isAlias
            |> equal true            

    // https://github.com/fable-compiler/ts2fable/issues/179  
    it "fix types has ESKeywords" <| fun _ ->
        let tsPaths = ["test/fragments/react/f4.d.ts"]
        let fsPath = "test/fragments/react/f4.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> existNone (fun tp -> 
                let name = getName tp
                esKeywords.Contains name
            )
            |> equal true

    // https://github.com/fable-compiler/ts2fable/issues/181 
    it "extract types in global module" <| fun _ ->
        let tsPaths = ["test/fragments/react/f5.d.ts"]
        let fsPath = "test/fragments/react/f5.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> existNoneByName "global" FsType.isModule
            |> equal true            

    // https://github.com/fable-compiler/ts2fable/issues/182               
    it "compile intersection to interface end" <| fun _ ->
        let tsPaths = ["test/fragments/react/f6.d.ts"]
        let fsPath = "test/fragments/react/f6.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> existOnlyOneByName "ClassType" FsType.isInterface
            |> equal true                                    

    // https://github.com/fable-compiler/ts2fable/issues/185               
    it "extract type literal from type alias" <| fun _ ->
        let tsPaths = ["test/fragments/react/f7.d.ts"]
        let fsPath = "test/fragments/react/f7.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |>  (
                    existOnlyOneByName "EventHandler" FsType.isInterface 
                    <&&> existOnlyOneByName "bivarianceHack" FsType.isFunction
                )
            |> equal true                                                

    // https://github.com/fable-compiler/ts2fable/issues/44               
    it "map mapped types" <| fun _ ->
        let tsPaths = ["test/fragments/react/f8.d.ts"]
        let fsPath = "test/fragments/react/f8.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> existLeastOneByName "ValidationMap" FsType.isInterface
            |> equal true                

    // https://github.com/fable-compiler/ts2fable/issues/208
    it "remove duplicate options" <| fun _ ->
        let tsPaths = ["test/fragments/yargs/duplicateOption.d.ts"]
        let fsPath = "test/fragments/yargs/duplicateOption.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> existNone (fun tp ->
                match tp with 
                | FsType.Union un when un.Option -> true
                | _ -> false
            )
            |> equal true         

    // https://github.com/fable-compiler/ts2fable/issues/219
    it "string enum with period" <| fun _ ->
        let tsPaths = ["test/fragments/react-native-fbsdk/stringEnumWithPeriod.d.ts"]
        let fsPath = "test/fragments/react-native-fbsdk/stringEnumWithPeriod.fs"
        testFsFileLines tsPaths fsPath  <| fun lines ->
            lines 
            |> List.exists (fun line ->
                line.Contains "User_actions_books"    
            )
            |> equal true      

    // https://github.com/fable-compiler/ts2fable/issues/219
    it "global variable is not generated" <| fun _ ->
        let tsPaths = ["test/fragments/breezejs/globalVariable.d.ts"]
        let fsPath = "test/fragments/breezejs/globalVariable.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> existOnlyOneByName "FilterQueryOp" (fun tp ->
                tp 
                |> FsType.asVariable
                |> function 
                    | Some vb ->
                        match vb.Export with 
                        | Some ep ->
                            ep.Selector = "FilterQueryOp"
                        | None -> false 
                    | None -> false
            )
            |> equal true  

    it "variable in module should be added to exports" <| fun _ ->
        let tsPaths = ["test/fragments/monaco/variableInModule.d.ts"]
        let fsPath = "test/fragments/monaco/variableInModule.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> existOnlyOneByName "EditorType" FsType.isVariable
            |> equal true  

    it "globalClass" <| fun _ ->
        let tsPaths = ["test/fragments/breezejs/globalClass.d.ts"]
        let fsPath = "test/fragments/breezejs/globalClass.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            // not implemented
            equal true true

    it "typeImport for importSpecifier that has not propertyname" <| fun _ ->
        let tsPaths = ["test/fragments/node/typeImport.d.ts"]
        let fsPath = "test/fragments/node/typeImport.fs"
        containLineHasWord tsPaths fsPath "Url.URL"

    // https://github.com/fable-compiler/ts2fable/issues/272
    it "babylonJS.SceneLoader.ImportMeshAsync" <| fun _ ->
        let tsPaths = ["test/fragments/babylonjs/SceneLoader.ImportMeshAsync.d.ts"]
        let fsPath = "test/fragments/babylonjs/SceneLoader.ImportMeshAsync.fs"
        let expected = "test/fragments/babylonjs/SceneLoader.ImportMeshAsync.expected.fs"
        convertAndCompareAgainstExpected tsPaths fsPath expected

    // https://github.com/fable-compiler/ts2fable/pull/275
    it "babylonJS.PrivateCtor" <| fun _ ->
        let tsPaths = ["test/fragments/babylonjs/Stage.PrivateCtor.d.ts"]
        let fsPath = "test/fragments/babylonjs/Stage.PrivateCtor.fs"
        let expected = "test/fragments/babylonjs/Stage.PrivateCtor.expected.fs"
        convertAndCompareAgainstExpected tsPaths fsPath expected
    
    it "generic-type-constraints/simple" <| fun _ ->
        let tsPaths = ["test/fragments/custom/generic-type-constraints/simple.d.ts"]
        let fsPath = "test/fragments/custom/generic-type-constraints/simple.fs"
        let expected = "test/fragments/custom/generic-type-constraints/simple.expected.fs"
        convertAndCompareAgainstExpected tsPaths fsPath expected
    it "generic-type-constraints/and" <| fun _ ->
        let tsPaths = ["test/fragments/custom/generic-type-constraints/and.d.ts"]
        let fsPath = "test/fragments/custom/generic-type-constraints/and.fs"
        let expected = "test/fragments/custom/generic-type-constraints/and.expected.fs"
        convertAndCompareAgainstExpected tsPaths fsPath expected
    it "generic-type-constraints/or" <| fun _ ->
        let tsPaths = ["test/fragments/custom/generic-type-constraints/or.d.ts"]
        let fsPath = "test/fragments/custom/generic-type-constraints/or.fs"
        let expected = "test/fragments/custom/generic-type-constraints/or.expected.fs"
        convertAndCompareAgainstExpected tsPaths fsPath expected
    it "generic-type-constraints/combination" <| fun _ ->
        let tsPaths = ["test/fragments/custom/generic-type-constraints/combination.d.ts"]
        let fsPath = "test/fragments/custom/generic-type-constraints/combination.fs"
        let expected = "test/fragments/custom/generic-type-constraints/combination.expected.fs"
        convertAndCompareAgainstExpected tsPaths fsPath expected
    it "generic-type-constraints/sealed" <| fun _ ->
        let tsPaths = ["test/fragments/custom/generic-type-constraints/sealed.d.ts"]
        let fsPath = "test/fragments/custom/generic-type-constraints/sealed.fs"
        let expected = "test/fragments/custom/generic-type-constraints/sealed.expected.fs"
        convertAndCompareAgainstExpected tsPaths fsPath expected
    it "generic-type-constraints/types" <| fun _ ->
        let tsPaths = ["test/fragments/custom/generic-type-constraints/types.d.ts"]
        let fsPath = "test/fragments/custom/generic-type-constraints/types.fs"
        let expected = "test/fragments/custom/generic-type-constraints/types.expected.fs"
        convertAndCompareAgainstExpected tsPaths fsPath expected
    it "generic-type-constraints/function" <| fun _ ->
        let tsPaths = ["test/fragments/custom/generic-type-constraints/function.d.ts"]
        let fsPath = "test/fragments/custom/generic-type-constraints/function.fs"
        let expected = "test/fragments/custom/generic-type-constraints/function.expected.fs"
        convertAndCompareAgainstExpected tsPaths fsPath expected
    it "generic-type-constraints/etc" <| fun _ ->
        let tsPaths = ["test/fragments/custom/generic-type-constraints/etc.d.ts"]
        let fsPath = "test/fragments/custom/generic-type-constraints/etc.fs"
        let expected = "test/fragments/custom/generic-type-constraints/etc.expected.fs"
        convertAndCompareAgainstExpected tsPaths fsPath expected
    it "generic-type-constraints/default" <| fun _ ->
        let tsPaths = ["test/fragments/custom/generic-type-constraints/default.d.ts"]
        let fsPath = "test/fragments/custom/generic-type-constraints/default.fs"
        let expected = "test/fragments/custom/generic-type-constraints/default.expected.fs"
        convertAndCompareAgainstExpected tsPaths fsPath expected
    
    // https://github.com/fable-compiler/ts2fable/issues/361
    it "keyof" <| fun _ ->
        let tsPaths = ["test/fragments/custom/keyof.d.ts"]
        let fsPath = "test/fragments/custom/keyof.fs"
        let expected = "test/fragments/custom/keyof.expected.fs"
        convertAndCompareAgainstExpected tsPaths fsPath expected

    it "comments/summary" <| fun _ ->
        let tsPaths = ["test/fragments/custom/comments/summary.d.ts"]
        let fsPath = "test/fragments/custom/comments/summary.fs"
        let expected = "test/fragments/custom/comments/summary.expected.fs"
        convertAndCompareAgainstExpected tsPaths fsPath expected
    it "comments/transformToXml" <| fun _ ->
        let tsPaths = ["test/fragments/custom/comments/transformToXml.d.ts"]
        let fsPath = "test/fragments/custom/comments/transformToXml.fs"
        let expected = "test/fragments/custom/comments/transformToXml.expected.fs"
        convertAndCompareAgainstExpected tsPaths fsPath expected
    it "comments/types" <| fun _ ->
        let tsPaths = ["test/fragments/custom/comments/types.d.ts"]
        let fsPath = "test/fragments/custom/comments/types.fs"
        let expected = "test/fragments/custom/comments/types.expected.fs"
        convertAndCompareAgainstExpected tsPaths fsPath expected
    it "comments/obsolete" <| fun _ ->
        let tsPaths = ["test/fragments/custom/comments/obsolete.d.ts"]
        let fsPath = "test/fragments/custom/comments/obsolete.fs"
        let expected = "test/fragments/custom/comments/obsolete.expected.fs"
        convertAndCompareAgainstExpected tsPaths fsPath expected

    // https://github.com/fable-compiler/ts2fable/pull/275
    it "regression #275 remove private members" <| fun _ ->
        runRegressionTest "#275-private-members"

    // https://github.com/fable-compiler/ts2fable/pull/277
    it "regression #277 unwrap options for optional parameters" <| fun _ ->
        runRegressionTest "#277-unwrap-options"

    // https://github.com/fable-compiler/ts2fable/pull/278
    it "regression #278 type literals in return position" <| fun _ ->
        runRegressionTest "#278-typeliterals-return"

    // https://github.com/fable-compiler/ts2fable/pull/288
    it "regression #288 type alias 'type float = number'" <| fun _ ->
        runRegressionTest "#288-type-alias-float-number"

    // https://github.com/fable-compiler/ts2fable/issues/280
    // https://github.com/fable-compiler/ts2fable/pull/289
    it "regression #289 merging outer modules" <| fun _ ->
        runRegressionTest "#289-recursive-merge-modules"

    // https://github.com/fable-compiler/ts2fable/issues/292
    it "regression #292 static props" <| fun _ ->
        runRegressionTest "#292-static-props"

    // https://github.com/fable-compiler/ts2fable/issues/314
    it "regression #314 inline destruct" <| fun _ ->
        runRegressionTest "#314-inline-destruct"

    // https://github.com/fable-compiler/ts2fable/issues/352
    it "regression #352 generic type parameter default" <| fun _ ->
        runRegressionTest "#352-generic-type-parameter-default"

    // https://github.com/fable-compiler/ts2fable/issues/353
    it "regression #353 generic type parameter default type" <| fun _ ->
        runRegressionTest "#353-generic-type-parameter-default-type"

    // https://github.com/fable-compiler/ts2fable/issues/353
    it "regression #362 array with ParamArray" <| fun _ ->
        runRegressionTest "#362-paramarray"

    // https://github.com/fable-compiler/ts2fable/issues/303
    it "regression #303 EmitConstructor" <| fun _ ->
        runRegressionTest "#303-EmitConstructor"
    it "regression #303 EmitIndexer" <| fun _ ->
        runRegressionTest "#303-EmitIndexer"

    // https://github.com/fable-compiler/ts2fable/pull/368
    it "regression #368 compare xml comments -- pass" <| fun _ ->
        runRegressionTest "#368-compare-xml-comments.pass"
    it "regression #368 compare xml comments -- fail" <| fun _ ->
        runRegressionTestWithComparison notEqual "#368-compare-xml-comments.fail"
    it "regression #368 compare xml comments -- indented fail" <| fun _ ->
        runRegressionTestWithComparison notEqual "#368-compare-xml-comments.indented.fail"
