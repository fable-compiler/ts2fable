
#r "paket:
nuget Fake.DotNet.Cli
nuget Fake.IO.FileSystem
nuget Fake.Core.Environment
nuget Fake.Core.BuildServer
nuget Fake.Core.Target //"
#load "./.fake/deploy.fsx/intellisense.fsx"
open System
open Fake.Core
open Microsoft.FSharp.Core.Printf
open Fake.IO.FileSystemOperators
open Fake.IO
open Fake.Core.Target
open Fake.Core.TargetOperators
open Fake.DotNet.Cli
open Fake.Core.BuildServer
open Fake.Core.Environment
open Fake.SystemHelper.Environment

let run' timeout (cmd:string) dir args  =
    if Process.ExecProcess (fun info ->
        { info with 
            FileName = cmd
            WorkingDirectory = 
                if not (String.IsNullOrWhiteSpace dir) then dir else info.WorkingDirectory  
            Arguments = args
        }
    ) timeout <> 0 then
        failwithf "Error while running '%s' with args: %s " cmd args

let run = run' System.TimeSpan.MaxValue

let platformTool tool = 
    Process.tryFindFileOnPath tool
    |> function Some t -> t | _ -> failwithf "%s not found" tool
    
let yarnTool = platformTool "yarn"
let npmTool = platformTool "npm"
let nodeTool = platformTool "node"
let mochaPath = "./node_modules/mocha/bin/_mocha" |> Path.getFullName 
let mutable dotnetExePath = "dotnet"
let toolDir = "./tools" 
let testCompileTool = "./test-compile" 
let buildDir = "./build"
let distDir = "./dist"
let cliDir = "./src"
let testDir = "./test"
let cliProj = cliDir</>"ts2fable.fsproj"
let runDotnet workingDir args =
    let result =
        Process.ExecProcess (fun info ->
        { info with 
            FileName = dotnetExePath
            WorkingDirectory = workingDir 
            Arguments =  args }) TimeSpan.MaxValue
    if result <> 0 then failwithf "dotnet %s failed" args 

let yarn args = 
    run yarnTool "./" args
let node args = 
    run nodeTool "./" args   

let npm args = 
    run npmTool "./" args   


Target.Create "InstallDotNetCore" (fun _ ->
    DotNetCliInstall Release_2_1_4
    dotnetExePath <- DotNetInfoOptions.Create().Common.DotNetCliPath
)

Target.Create "YarnInstall" (fun _ ->
    yarn "install"
)

Target.Create "Restore" (fun _ ->
    DotNetRestore(id) (toolDir</>"DotnetCLI.fsproj")
    DotNetRestore(id) (testDir</>"test.fsproj")
    DotNetRestore(id) cliProj
)

Target.Create "BuildCli" (fun _ ->
    runDotnet toolDir <| sprintf "fable yarn-fable-splitter -- %s  --config %s" cliProj (toolDir</>"splitter.config.js")
)

Target.Create "RunCli" (fun _ ->
    node <| sprintf "%s cliTest" (distDir</>"ts2fable.js")
)

Target.Create "RestoreTestCompile" (fun _ ->
    DotNetRestore(id) (testCompileTool</>"test-compile.fsproj")
)

Target.Create "BuildTest" (fun _ ->
    runDotnet toolDir "fable webpack -- --config webpack.config.test.js"
)

Target.Create "RunTest" (fun _ ->
    match buildServer with 
    | AppVeyor -> node <| sprintf "%s --reporter mocha-appveyor-reporter %s" mochaPath (buildDir</>"test.js" |> Path.getFullName)
    | _ -> node <| sprintf "%s %s" mochaPath (buildDir</>"test.js" |> Path.getFullName)
)

Target.Create "Publish" (fun _ ->
    match buildServer with 
    | AppVeyor -> 
        node (toolDir</>"build-update.package.js")
        let version = sprintf "0.6.0-build.%s" buildVersion
        yarn <| sprintf "version --new-version %s --no-git-tag-version" version
        npm "pack"
        
        let repoName = environVar "appveyor_repo_name"
        let repoBranch = environVar "appveyor_repo_branch"
        
        if repoName = "fable-compiler/ts2fable" && repoBranch = "master" then
            let line = sprintf "//registry.npmjs.org/:_authToken=%s\n" <| environVar "npmauthtoken`n"
            let npmrc = (GetFolderPath UserProfile)</>".npmrc"
            printfn "Auth token is %s" line
            printfn "npmrc path is %s" npmrc
            File.writeNew npmrc [line]
            npm "whoami"
            yarn <| sprintf "publish ts2fable-%s.tgz --new-version %s --tag next" version version
    | _ ->  ()
)

Target.Create "Deploy" Target.DoNothing    

"Deploy"
    <== [ "InstallDotNetCore"
          "YarnInstall"
          "Restore"
          "BuildTest"
          "RunTest" 
          "BuildCli"
          "RunCli"
          "RestoreTestCompile"
          "Publish" ]

Target.RunOrDefault "Deploy"
