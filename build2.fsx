#r "paket: groupref netcorebuild //"
#load "common.fsx"
open System
open Fake.Core
open Microsoft.FSharp.Core.Printf
open Fake.IO.FileSystemOperators
open Fake.IO
open Fake.Core.TargetOperators
open Fake.DotNet
open Fake.Tools.Git.Merge
open Fake.Tools.Git.Branches
open Fake.Tools.Git.Repository
open Fake.Tools.Git.Staging
open Fake.Windows
open Fake.JavaScript
open Common
Target.runOrDefault "BuildAll"
