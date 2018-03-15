#r "paket: groupref netcorebuild //"
#load "./.fake/build.fsx/intellisense.fsx"
open System
open Fake.Core
open Microsoft.FSharp.Core.Printf
open Fake.IO.FileSystemOperators
open Fake.IO
open Fake.Core.Target
open Fake.Core.TargetOperators
open Fake.DotNet
open Fake.Core.BuildServer
open Fake.Core.Environment
open Fake.SystemHelper.Environment
open Fake.Api
open Fake.Tools.Git.Merge
open Fake.Tools.Git.Commit
open Fake.Tools.Git.Branches
open Fake.Tools.Git.Repository
open Fake.Tools.Git.CommandHelper
open Fake.Tools.Git.Staging
open Fake.IO
open Fake.Windows.Choco
let run' timeout (cmd:string) dir args  =
    if Process.Exec (fun info ->
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
let runDotnet workingDir args =
    let result =
        Process.Exec (fun info ->
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

let git args = 
    run gitTool "./" args  
Target.Create "InstallDotNetCore" (fun _ ->
    DotNet.Install DotNet.Release_2_1_4
    dotnetExePath <- DotNet.InfoOptions.Create().Common.DotNetCliPath
)

Target.Create "YarnInstall" (fun _ ->
    yarn "install"
)

Target.Create "Restore" (fun _ ->
    DotNet.Restore(id) (toolDir</>"DotnetCLI.fsproj")
    DotNet.Restore(id) (testDir</>"test.fsproj")
    DotNet.Restore(id) cliProj
)

Target.Create "BuildCli" (fun _ ->
    runDotnet toolDir <| sprintf "fable yarn-fable-splitter -- %s  --config %s" cliProj (toolDir</>"splitter.config.js")
)

Target.Create "RunCli" (fun _ ->
    node <| sprintf "%s cliTest" (distDir</>"ts2fable.js")
)

Target.Create "BuildTestCompile" (fun _ ->
    DotNet.Build(id) (testCompileDir</>"test-compile.fsproj")
)

Target.Create "BuildTest" (fun _ ->
    runDotnet toolDir "fable webpack -- --config webpack.config.test.js"
)

Target.Create "RunTest" (fun _ ->
    match buildServer with 
    | AppVeyor -> node <| sprintf "%s --reporter mocha-appveyor-reporter %s" mochaPath (buildDir</>"test.js" |> Path.getFullName)
    | _ -> node <| sprintf "%s %s" mochaPath (buildDir</>"test.js" |> Path.getFullName)
)

Target.Create "PushToExports" (fun _ ->
    
    let configGitAuthorization() = 
        Install id "openssl.light"
        Install id "openssh"

        let sshDir = (GetFolderPath UserProfile)</>".ssh"
       
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

    match buildServer with 
    | AppVeyor -> 
        let repoName = environVar "appveyor_repo_name"
        let repoBranch = environVar "appveyor_repo_branch"
        let RepocommitMsg = environVar "APPVEYOR_REPO_COMMIT_MESSAGE"
        let prHeadRepoName = environVar "APPVEYOR_PULL_REQUEST_HEAD_REPO_NAME"

        let commit() =
            let descripton = 
                [ sprintf "Appveyor https://ci.appveyor.com/project/fable-compiler/ts2fable/build/%s" buildVersion
                  RepocommitMsg]
                |> String.concat "\n"
                |> fun s -> sprintf "\"%s\"" s 
        
            sprintf "commit -m %s -m %s" buildVersion descripton 
            |> run gitTool repositoryDir
            |> ignore                  
                                                      
        if  repoName = "fable-compiler/ts2fable" && repoBranch = "master" then
            configGitAuthorization()    
            let handle addtionalBehavior = 
                Shell.DeleteDir repositoryDir
                clone "./" "-b dev git@github.com:fable-compiler/ts2fable-exports.git" repositoryDir
                Shell.CopyDir repositoryDir testCompileDir (fun f -> f.EndsWith ".fs")
                StageAll repositoryDir
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
                        StageAll repositoryDir
                        push repositoryDir
                        )
            else
                handle (fun () -> ())                
    | _ ->  ()
)

Target.Create "Publish" (fun _ ->
    match buildServer with 
    | AppVeyor -> 
        node (toolDir</>"build-update.package.js")
        node (toolDir</>"add-shebang.js")
        
        yarn <| sprintf "version --new-version %s --no-git-tag-version" buildVersion
        npm "pack"
        
        let repoName = environVar "appveyor_repo_name"
        let repoBranch = environVar "appveyor_repo_branch"
        let prHeadRepoName = environVar "APPVEYOR_PULL_REQUEST_HEAD_REPO_NAME"

        if repoName = "fable-compiler/ts2fable" && repoBranch = "master" && String.isNullOrEmpty prHeadRepoName then
            let line = sprintf "//registry.npmjs.org/:_authToken=%s\n" <| environVar "npmauthtoken"
            let npmrc = (GetFolderPath UserProfile)</>".npmrc"
            File.writeNew npmrc [line]
            npm "whoami"
            yarn <| sprintf "publish ts2fable-%s.tgz --new-version %s --tag next" buildVersion buildVersion
    | _ ->  ()
)

Target.Create "WatchTest" (fun _ ->
    runDotnet toolDir "fable webpack -- --config webpack.config.test.js -w"
)

Target.Create "CliTest" Target.DoNothing
Target.Create "Deploy" DoNothing
Target.Create "BuildAll" Target.DoNothing

"CliTest"
    <== [ "BuildCli"
          "RunCli"
          "BuildTestCompile" ]

"BuildAll"
    <== [ "InstallDotNetCore"
          "YarnInstall"
          "Restore"
          "BuildTest"
          "RunTest" 
          "CliTest" ]

"Deploy"
    <== [ "BuildAll"
          "PushToExports"   //https://github.com/fable-compiler/ts2fable-exports
          "Publish" ]

Target.RunOrDefault "BuildAll"
