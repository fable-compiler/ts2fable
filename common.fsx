#load "./.fake/build.fsx/intellisense.fsx"
#if !FAKE
#r "Facades/netstandard"
#r "netstandard"
#endif
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
open Fake.JavaScript

let versionFromGlobalJson : DotNet.CliInstallOptions -> DotNet.CliInstallOptions = (fun o ->
        { o with Version = DotNet.Version (DotNet.getSDKVersionFromGlobalJson()) }
    )

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
let nodeTool = platformTool "node"
let gitTool = platformTool "git"
let mochaPath = "./node_modules/mocha/bin/_mocha" |> Path.getFullName
let mutable dotnetExePath = "dotnet"
let toolDir = "./tools"
let testCompileDir = "./test-compile"
let buildDir = "./build"
let distDir = "./dist"
let cliDir = "./src"
let testDir = "./test"
let cliProj = cliDir</>"ts2fable.fsproj"
let version = Environment.environVarOrDefault "APPVEYOR_REPO_TAG_NAME" BuildServer.buildVersion
let isAppveyor = String.isNotNullOrEmpty BuildServer.appVeyorBuildVersion

let node args = run nodeTool "./" args
let npm args = run npmTool "./" args
let git args = run gitTool "./" args

Target.create "NpmInstall" (fun _ ->
    npm "install"
)

Target.create "Restore" (fun _ ->
    DotNet.restore(id) (toolDir</>"DotnetCLI.fsproj")
    DotNet.restore(id) (testDir</>"test.fsproj")
    DotNet.restore(id) cliProj
    DotNet.restore(id) ("web-app" </> "ts2fable-web-app.fsproj")
)

Target.create "BuildCli" (fun _ ->
    npm "run build-cli"
)


Target.create "RunCli" (fun _ ->
    let ts2fable args =
        let args = args |> String.concat " "
        async {
            node <| sprintf "%s %s" (buildDir</>"ts2fable.js") args
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
)

Target.create "BuildTestCompile" (fun _ ->
    DotNet.build(id) (testCompileDir</>"test-compile.fsproj")
)

Target.create "BuildTest" (fun _ ->
    npm "run build-test"
)

Target.create "RunTest" (fun _ ->
    // match BuildServer.buildServer with
    // | AppVeyor -> node <| sprintf "%s --reporter mocha-appveyor-reporter %s" mochaPath (buildDir</>"test.js" |> Path.getFullName)
    // | _ -> node <| sprintf "%s %s" mochaPath (buildDir</>"test.js" |> Path.getFullName)
    if (isAppveyor) then
        node <| sprintf "%s --reporter mocha-appveyor-reporter %s" mochaPath (buildDir</>"test.js" |> Path.getFullName)
    else
        node <| sprintf "%s %s" mochaPath (buildDir</>"test.js" |> Path.getFullName)
)

Target.create "PushToExports" (fun _ ->

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
)

// you have to fork ts2fable-export repo to your computer first 
Target.create "PushForComparision" (fun _ ->

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
)

Target.create "Cli.Publish" (fun _ ->
    if (isAppveyor) then
        npm "run build"

        node (toolDir</>"build-update.package.js")
        node (toolDir</>"add-shebang.js")

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
)

Target.create "WatchTest" (fun _ ->
    npm "run watch-test"
)

Target.create "WatchCli" (fun _ ->
    npm "run watch-cli"
)

Target.create "CliTest" ignore
Target.create "Deploy" ignore
Target.create "BuildAll" ignore

// Web App
let jsLibsOutput = "web-app" </> "public" </> "libs"

Target.create "CopyMonacoModules" (fun _ ->
    let requireJsOutput = jsLibsOutput </> "requirejs"
    let vsOutput = jsLibsOutput </> "vs"
    Directory.create requireJsOutput
    Directory.create vsOutput
    Shell.cp ("./node_modules" </> "requirejs" </> "require.js") requireJsOutput
    Shell.cp_r ("./node_modules" </> "monaco-editor" </> "min" </> "vs") vsOutput
)

Target.create "WebApp.Build" (fun _ ->
    npm "run webapp-build"
)

Target.create "WebApp.Watch" (fun _ ->
    npm "run webapp-watch"
)

Target.create "WebApp.Publish" (fun _ ->
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
)

Target.create "WebApp.Setup" ignore

Target.create "Watch" (fun _ ->
    let watchCli =
        async {
            npm "run watch-cli"
        }
    let watchTest =
        async {
            npm "run watch-test"
        }
    [watchCli;watchTest]
    |> Async.Parallel
    |> Async.RunSynchronously
    |> ignore

)

"NpmInstall"
    ==> "Restore"
    ==> "BuildCli"
    ==> "RunCli"
    // ==> "BuildTestCompile"
    ==> "CliTest"

// "CliTest"
//     <== [ "BuildCli"
//           "RunCli"
//           "BuildTestCompile" ]

"NpmInstall"
    ==> "Restore"
    ==> "BuildTest"
    ==> "RunTest"
    ==> "CliTest"
    ==> "BuildAll"

// "BuildAll"
//     <== [ "NpmInstall"
//           "Restore"
//           "BuildTest"
//           "RunTest"
//           "CliTest" ]

"BuildAll"
    ==> "PushToExports"
    ==> "WebApp.Publish"
    ==> "Cli.Publish"
    ==> "Deploy"

// "Deploy"
//     <== [ "BuildAll"
//           "PushToExports"   //https://github.com/fable-compiler/ts2fable-exports
//           "WebApp.Publish"
//           "Cli.Publish" ]

"NpmInstall"
    ==> "Restore"
    ==> "CopyMonacoModules"
    ==> "WebApp.Setup"

// "WebApp.Setup"
//     <== [ "NpmInstall"
//           "Restore"
//           "CopyMonacoModules" ]

"WebApp.Setup"
    ==> "WebApp.Build"
    ==> "WebApp.Publish"

"WebApp.Setup"
    ==> "WebApp.Watch"

"PushForComparision"
    <== [ "RunCli"
          "BuildTestCompile"
          "RunTest" ]
