#r "paket: groupref netcorebuild //"
#load "./.fake/build.fsx/intellisense.fsx"
open System
open Fake.Core
open Microsoft.FSharp.Core.Printf
open Fake.IO.FileSystemOperators
open Fake.IO
open Fake.Core.TargetOperators
open Fake.DotNet
open Fake.Tools.Git
open Fake.Tools.Git.Merge
open Fake.Tools.Git.Branches
open Fake.Tools.Git.Repository
open Fake.Tools.Git.Staging
open Fake.Windows

let versionFromGlobalJson (o: DotNet.CliInstallOptions): DotNet.CliInstallOptions =
    { o with Version = DotNet.Version (DotNet.getSDKVersionFromGlobalJson()) }

let run cmd dir args =
    let result =
        CreateProcess.fromRawCommandLine cmd args
        |> CreateProcess.withWorkingDirectory dir
        |> Proc.run
    if result.ExitCode <> 0 then
        failwithf "Error while running '%s' with args: %s " cmd args

let platformTool tool =
    ProcessUtils.tryFindFileOnPath tool
    |> function Some t -> t | _ -> failwithf "%s not found" tool

let npmTool = platformTool "npm"
let npxTool = platformTool "npx"
let nodeTool = platformTool "node"
let gitTool = platformTool "git"
let toolDir = "./tools"
let testCompileDir = "./test-compile"
let buildDir = "./build"
/// Tests compiled by fable (-> JS Modules)
let testBuildDir = buildDir </> "test"
/// CLI compiled by fable (-> JS Modules)
let cliBuildDir = buildDir </> "cli"
/// CLI release (-> no JS Modules)
let distDir = "./dist"
let cliDir = "./src"
let testDir = "./test"
let appDir = "./web-app"
let appTempOutDir = appDir </> "temp"
let appOutDir = appDir </> "output"
let version = Environment.environVarOrDefault "APPVEYOR_REPO_TAG_NAME" BuildServer.buildVersion
let isAppveyor = String.isNotNullOrEmpty BuildServer.appVeyorBuildVersion

let node args = run nodeTool "./" args
let npm args = run npmTool "./" args
let npx args = run npxTool "./" args
let git args = run gitTool "./" args

let execDotNet cmd args =
    let result = DotNet.exec id cmd args
    if not result.OK then
        failwithf "Error while running 'dotnet %s %s'" cmd args

let fable args = execDotNet "fable" args

module Scripts =
    // previously `scripts` in `package.json` (-> npm)

    /// Fable cannot handle referenced project with EntryPoint
    /// -> define special symbol to emit EntryPoint when CLI build
    // see https://github.com/fable-compiler/Fable/issues/236
    [<Literal>]
    let private CLI_BUILD_SYMBOL = "TS2FABLE_STANDALONE"

    /// Build `./src` in release mode, no bundle, with sourcemaps,  into `./build/cli`, with entry `ts2fable.js`
    /// 
    /// Start with `node --require esm ./build/cli/ts2fable.js`
    let buildCli () =
        fable $"{cliDir} --outDir {cliBuildDir} --define {CLI_BUILD_SYMBOL} --sourceMaps"
    /// Watch `./src` in debug mode, no bundle, with sourcemaps, into `./build/cli`, with entry `ts2fable.js`
    /// 
    /// Start with `node --require esm ./build/cli/ts2fable.js`
    let watchCli () =
        fable $"watch {cliDir} --outDir {cliBuildDir} --define {CLI_BUILD_SYMBOL} --sourceMaps --define DEBUG"

    /// Build `./test` in release mode, no bundle, with sourcemaps, into `./build/test`, with entry `test.js`
    /// 
    /// Start with `npx mocha --require esm ./build/test/test.js`
    let buildTest () =
        fable $"{testDir} --outDir {testBuildDir} --sourceMaps"

    /// Watch `./test` in debug mode, no bundle, with sourcemaps, into `./build/test`, with entry `test.js`.
    /// 
    /// Start with `npx mocha --require esm ./build/test/test.js`
    /// 
    /// Unlike `watchAndRunTest` this doesn't run tests after compilation.
    let watchTest () =
        fable $" watch {testDir} --outDir {testBuildDir} --sourceMaps --define DEBUG"

    /// Run mocha tests with entry `./build/test/test.js`.
    /// 
    /// Requires building test before via `buildTest`
    let runTest () =
        npx $"mocha --require esm {testBuildDir}/test.js"
    let runTestWithReporter (reporter: string) =
        npx $"mocha --require esm --reporter {reporter} {testBuildDir}/test.js"

    /// Watch `./test` in debug mode, no bundle, with sourcemaps, into `./build/test`, with entry `test.js` and run tests with mocha after each change
    let watchAndRunTest () =
        fable $"watch {testDir} --outDir {testBuildDir} --sourceMaps --define DEBUG --runWatch mocha --require esm {testBuildDir}/test.js"

    /// Build `web-app` in release mode, bundled, with sourcemap into `./web-app/output/` dir.
    /// 
    /// First: Fable in release mode, with sourcemaps into `./web-app/temp` with entry `App.js`.
    /// Then: Bundling with webpack into `./web-app/output/` with `./web-app/webpack.config.js`.
    let buildWebapp () =
        fable $"{appDir} --outDir {appTempOutDir} --sourceMaps --run webpack --mode production --config {appDir}/webpack.config.js"
    
    /// Watch `web-app` in debug mode, with sourcemaps, served via `localhost:8080`.
    /// 
    /// First: Fable in debug mode, with sourcemaps into `./web-app/temp` with entry `App.js`.
    /// Then: Serving via `localhost:8080` with `webpack serve` (-> webpack-dev-server) and `./web-app/webpack.config.js`
    let watchWebapp () =
        fable $"watch {appDir} --outDir {appTempOutDir} --sourceMaps --define DEBUG --run webpack serve --watch-content-base --mode development --config {appDir}/webpack.config.js"

    /// Bundle existing CLI (output of `buildCli`, in `./build/cli` with entry `ts2fable.js`) into `./dist/ts2fable.js` with rollup
    let bundleCli () =
        // umd: Universal Module Definition
        npx $"rollup --file {distDir}/ts2fable.js --format umd --name ts2fable {cliBuildDir}/ts2fable.js"

    
Target.create "Clean" <| fun _ ->
    execDotNet "clean" ""
    Shell.cleanDirs [
        cliBuildDir
        testBuildDir
        distDir
        appOutDir
        appTempOutDir
    ]

Target.create "NpmInstall" <| fun _ ->
    npm "install"

Target.create "Restore" <| fun _ ->
    DotNet.restore id testDir
    DotNet.restore id cliDir
    DotNet.restore id appDir

Target.create "Prepare" ignore

Target.create "BuildCli" <| fun _ ->
    Scripts.buildCli ()

Target.create "WatchCli" <| fun _ ->
    Scripts.watchCli ()

Target.create "RunCliOnTestCompile" <| fun _ ->
    let ts2fable args =
        let args = args |> String.concat " "
        async {
            node <| $"--require esm {cliBuildDir}/ts2fable.js {args}"
        }

    [
        // used by ts2fable
        ts2fable ["node_modules/typescript/lib/typescript.d.ts";"test-compile/TypeScript.fs"]
        ts2fable ["node_modules/@types/node/index.d.ts";"test-compile/Node.fs"]
        ts2fable ["node_modules/@types/yargs/index.d.ts";"test-compile/Yargs.fs"]
        ts2fable ["node_modules/breeze-client/index.d.ts";"test-compile/Breeze.fs"]

        // for test-compile
        ts2fable ["node_modules/vscode/vscode.d.ts";"test-compile/VSCode.fs"]
        // ts2fable ["node_modules/izitoast/dist/izitoast/izitoast.d.ts"] "test-compile/IziToast.fs"
        ts2fable ["node_modules/izitoast/types/index.d.ts";"test-compile/IziToast.fs"]
        ts2fable ["node_modules/electron/electron.d.ts";"test-compile/Electron.fs"]
        ts2fable ["node_modules/@types/react/index.d.ts";"test-compile/React.fs"]
        ts2fable ["node_modules/@types/react-native/index.d.ts";"test-compile/ReactNative.fs"]
        ts2fable ["node_modules/@types/mocha/index.d.ts";"test-compile/Mocha.fs"]
        ts2fable ["node_modules/@types/chai/index.d.ts";"test-compile/Chai.fs"]
        ts2fable ["node_modules/chalk/types/index.d.ts";"test-compile/Chalk.fs"]
        ts2fable ["node_modules/monaco-editor/monaco.d.ts";"test-compile/Monaco.fs"]
        ts2fable
            [   "node_modules/@types/google-protobuf/index.d.ts"
                "node_modules/@types/google-protobuf/google/protobuf/empty_pb.d.ts"
                "test-compile/Protobuf.fs"
            ]
        ts2fable ["node_modules/synctasks/dist/SyncTasks.d.ts";"test-compile/SyncTasks.fs"]
        ts2fable ["node_modules/subscribableevent/dist-types/SubscribableEvent.d.ts";"test-compile/SubscribableEvent.fs"]
        ts2fable
            [
                "node_modules/office-ui-fabric-react/lib/index.d.ts"
                "test-compile/OfficeReact.fs"
                "-e"
                "uifabric"
                "office-ui-fabric-react"
            ]
        ts2fable
            [
                "node_modules/reactxp/dist/ReactXP.d.ts"
                "test-compile/ReactXP.fs"
                "-e"
                "reactxp"
            ]
    ]
    |> Async.Parallel
    |> Async.RunSynchronously
    |> ignore
    // files that have too many TODOs
    // ts2fable ["node_modules/@types/jquery/index.d.ts"] "test-compile/JQuery.fs"
    // ts2fable ["node_modules/typescript/lib/lib.es2015.promise.d.ts"] "test-compile/Promise.fs"

    printfn "done writing test-compile files"

Target.create "BuildTestCompile" <| fun _ ->
    DotNet.build(id) (testCompileDir</>"test-compile.fsproj")

Target.create "BuildTest" <| fun _ ->
    Scripts.buildTest ()

Target.create "WatchTest" <| fun _ ->
    Scripts.watchTest ()

Target.create "RunTest" <| fun _ ->
    Scripts.runTest ()

Target.create "WatchAndRunTest" <| fun _ ->
    Scripts.watchAndRunTest ()

Target.create "Watch" <| fun _ ->
    // combines WatchCli & WatchTest
    let watchCli =
        async {
            // into `./build/cli`
            Scripts.watchCli ()
        }
    let watchTest =
        async {
            // into `./build/test`
            Scripts.watchTest ()
        }
    [watchCli;watchTest]
    |> Async.Parallel
    |> Async.RunSynchronously
    |> ignore

Target.create "PushToExports" <| fun _ ->

    let configGitAuthorization() =
        Choco.install id "openssl.light"
        Choco.install id "openssh"

        let sshDir = (Environment.GetFolderPath Environment.SpecialFolder.UserProfile)</>".ssh"

        Directory.create sshDir

        let id_rsa = sshDir</>"id_rsa"
        let id_rsa_enc = toolDir</>"id_rsa.enc" |> Path.getFullName

        sprintf "enc -in %s -out %s -d -aes256 -k Michelin" id_rsa_enc id_rsa
        |> run @"C:\Program Files\OpenSSL\bin\openssl.exe" toolDir

        try run @"C:\Program Files\OpenSSH-Win64\ssh.exe" "./" """ -o "StrictHostKeyChecking no" git@github.com """
        with _ -> ()

        git "config --global user.name ts2fable-exports"
        git "config --global user.email ts2fable-exports@outlook.com"

    let repositoryDir = "./ts2fable-exports"

    if (isAppveyor) then
        let repoName = Environment.environVar "appveyor_repo_name"
        let repoBranch = Environment.environVar "appveyor_repo_branch"
        let RepocommitMsg = Environment.environVar "APPVEYOR_REPO_COMMIT_MESSAGE"
        let prHeadRepoName = Environment.environVar "APPVEYOR_PULL_REQUEST_HEAD_REPO_NAME"

        let commit() =
            let descripton =
                [ sprintf "Appveyor https://ci.appveyor.com/project/fable-compiler/ts2fable/build/%s" version
                  RepocommitMsg]
                |> String.concat "\n"
                |> fun s -> sprintf "\"%s\"" s

            sprintf "commit -m %s -m %s" version descripton
            |> run gitTool repositoryDir
            |> ignore

        if  repoName = "fable-compiler/ts2fable" && repoBranch = "master" then
            configGitAuthorization()
            let handle addtionalBehavior =
                Shell.deleteDir repositoryDir
                clone "./" "-b dev git@github.com:fable-compiler/ts2fable-exports.git" repositoryDir
                Shell.copyDir repositoryDir testCompileDir (fun f -> f.EndsWith ".fs")
                stageAll repositoryDir
                try
                    commit()
                    push repositoryDir
                with ex -> printf "%A" ex
                try
                    addtionalBehavior()
                with ex -> printf "%A" ex
            if String.isNullOrEmpty prHeadRepoName then
                handle
                    ( fun () ->
                        checkoutBranch repositoryDir "master"
                        merge repositoryDir "-X theirs" "dev"
                        stageAll repositoryDir
                        push repositoryDir
                        )
            else
                handle (fun () -> ())

// you have to fork ts2fable-export repo to your computer first 
Target.create "PushForComparison" <| fun _ ->
    if (not isAppveyor) then
        let repositoryDir = "../ts2fable-exports"
        let git = run gitTool repositoryDir
        let commit() =
            sprintf "commit -m comparision" 
            |> git
            |> ignore

        git "remote remove upstream"
        git "remote add upstream git@github.com:fable-compiler/ts2fable-exports.git"
        git "fetch upstream"
        git "reset --hard upstream/dev"
        Shell.copyDir repositoryDir testCompileDir (fun f -> f.EndsWith ".fs")
        stageAll repositoryDir
        try 
            commit()
        with ex -> printf "%A" ex
        git "push origin -f"

Target.create "Cli.BuildRelease" <| fun _ -> 
    Scripts.bundleCli ()

    // add `#! node`
    node (toolDir</>"add-shebang.js")

Target.create "Cli.Publish" <| fun _ ->
    if (isAppveyor) then
        // remove `scripts` & `engines` from `package.json`
        node (toolDir</>"build-update.package.js")

        // update version in `package.json`
        npm (sprintf "version %s --allow-same-version --no-git-tag-version" version)

        npm "pack"


        let repoName = Environment.environVar "appveyor_repo_name"
        let repoBranch = Environment.environVar "appveyor_repo_branch"
        let prHeadRepoName = Environment.environVar "APPVEYOR_PULL_REQUEST_HEAD_REPO_NAME"

        if repoName = "fable-compiler/ts2fable" && repoBranch = "master" && String.isNullOrEmpty prHeadRepoName then
            let line = sprintf "//registry.npmjs.org/:_authToken=%s\n" <| Environment.environVar "npmauthtoken"
            let npmrc = (Environment.GetFolderPath Environment.SpecialFolder.UserProfile)</>".npmrc"
            File.writeNew npmrc [line]
            npm "whoami"
            if not <| version.Contains("-build") then
                npm (sprintf "publish ts2fable-%s.tgz" version)
            else
                npm (sprintf "publish ts2fable-%s.tgz --tag next" version)

Target.create "CliTest" ignore
Target.create "Deploy" ignore
Target.create "BuildAll" ignore

// Web App
let jsLibsOutput = appOutDir </> "libs"

Target.create "WebApp.CopyMonacoModules" <| fun _ ->
    let requireJsOutput = jsLibsOutput </> "requirejs"
    let vsOutput = jsLibsOutput </> "vs"
    Directory.create requireJsOutput
    Directory.create vsOutput
    Shell.cp ("./node_modules" </> "requirejs" </> "require.js") requireJsOutput
    Shell.cp_r ("./node_modules" </> "monaco-editor" </> "min" </> "vs") vsOutput

Target.create "WebApp.Build" <| fun _ ->
    Scripts.buildWebapp ()

Target.create "WebApp.Watch" <| fun _ ->
    Scripts.watchWebapp ()

Target.create "WebApp.Publish" <| fun _ ->
    if (isAppveyor) then
        npm (sprintf "version %s --no-git-tag-version" version)

        let repoName = Environment.environVar "appveyor_repo_name"
        let repoBranch = Environment.environVar "appveyor_repo_branch"
        let prHeadRepoName = Environment.environVar "APPVEYOR_PULL_REQUEST_HEAD_REPO_NAME"

        if repoName = "fable-compiler/ts2fable" && repoBranch = "master" && String.isNullOrEmpty prHeadRepoName then
            /// code adapted from https://github.com/fsharp/FAKE/blob/release/next/build.fsx
            Shell.cleanDir "gh-pages"
            let auth = sprintf "%s:x-oauth-basic@" (Environment.environVar "githubtoken")
            let url = sprintf "https://%sgithub.com/%s/%s.git" auth "fable-compiler" "ts2fable"
            cloneSingleBranch "" url "gh-pages" "gh-pages"

            fullclean "gh-pages"
            Shell.copyRecursive "web-app/output" "gh-pages" true |> printfn "%A"
            stageAll "gh-pages"
            CommandHelper.directRunGitCommandAndFail "gh-pages" "config user.email humhei@outlook.com"
            CommandHelper.directRunGitCommandAndFail "gh-pages" "config user.name \"humhei\""
            let repocommitMsg = Environment.environVar "APPVEYOR_REPO_COMMIT_MESSAGE"
            Commit.exec "gh-pages" (sprintf "%s" repocommitMsg)
            Branches.pushBranch "gh-pages" url "gh-pages"

Target.create "WebApp.Setup" ignore


// Prepare
"Clean" 
    ==> "NpmInstall"
    ==> "Prepare"
"Clean" 
    ==> "Restore"
    ==> "Prepare"

// Cli: Build CLI, compile several Declaration files to F#, ensure these compile in F#
"Prepare"
    ==> "BuildCli"
    ==> "RunCliOnTestCompile"
    ==> "BuildTestCompile"
    ==> "CliTest"
    ==> "BuildAll"

// CLI Watch
"Prepare"
    ==> "WatchCli"

// Run Tests: Build Tests, Run Tests
"Prepare"
    ==> "BuildTest"
    ==> "RunTest"
    ==> "CliTest"
    ==> "BuildAll"

// Watch tests, but don't run after each compilation
"Prepare"
    ==> "WatchTest"

// Watch Tests and run after each compilation
"Prepare"
    ==> "WatchAndRunTest"

// Watch both CLI (-> ./build/cli) & Tests (-> ./build/test)
"Prepare"
    ==> "Watch"

// Cli.BuildRelease
"Prepare"
    ==> "BuildCli"
    ==> "Cli.BuildRelease"

// CLI Publish
"BuildAll"
    ==> "Cli.BuildRelease"
    ==> "Cli.Publish"

// Deploy: Build for release, Deploy
"BuildAll"
    ==> "PushToExports"
    ==> "WebApp.Publish"
    ==> "Cli.Publish"
    ==> "Deploy"

// WebApp.Setup
"Prepare"
    ==> "WebApp.CopyMonacoModules"
    ==> "WebApp.Setup"

// WebApp.Publish: Build & Publish
"WebApp.Setup"
    ==> "WebApp.Build"
    ==> "WebApp.Publish"

// WebApp.Watch: run server
"WebApp.Setup"
    ==> "WebApp.Watch"

"PushForComparison"
    <== [ "RunCliOnTestCompile"
          "BuildTestCompile"
          "RunTest" ]


Target.runOrDefault "BuildCli"
