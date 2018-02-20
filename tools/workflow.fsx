#r "paket:
nuget Fake.DotNet.Cli
nuget Fake.IO.FileSystem
nuget Fake.Core.Environment
nuget Fake.Core.Target //"
#load "./.fake/workflow.fsx/intellisense.fsx"
open System
open Fake.Core
open Fake.IO.FileSystemOperators
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
let mutable dotnetExePath = "dotnet"
let toolDir = "./tools" 
let runDotnet workingDir args =
    let result =
        Process.ExecProcess (fun info ->
        { info with 
            FileName = dotnetExePath
            WorkingDirectory = workingDir 
            Arguments =  args }) TimeSpan.MaxValue
    if result <> 0 then failwithf "dotnet %s failed" args 


Target.Create "WatchTest" (fun _ ->
    runDotnet toolDir "fable webpack -- --config webpack.config.test.js -w"
)

Target.RunOrDefault "WatchTest"
