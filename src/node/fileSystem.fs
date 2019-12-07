module ts2fable.node.FileSystem
open Node.Api
open Fable.Core

let readText (path: string) =
    fs.readFileSync (path, "utf-8")
let readLines =
    readText >> fun text -> text.Split('\n') |> Seq.ofArray
let readLine num file =
    readLines file |> Seq.item (num - 1)

let enumerateFileSystemEntries dir =
    fs.readdirSync (dir |> U2.Case1)
    |> Seq.map (fun str -> path.join [| dir; str |])
let isFile path =
    let stats = path |> U2.Case1 |> fs.lstatSync
    stats.isFile()
let isDirectory path =
    let stats = path |> U2.Case1 |> fs.lstatSync
    stats.isDirectory()

// get all files from a directory
let rec enumerateFiles dirs =
    if Seq.isEmpty dirs then Seq.empty else
        seq { yield! dirs |> Seq.collect (enumerateFileSystemEntries >> Seq.filter isFile)
              yield! dirs |> Seq.collect (enumerateFileSystemEntries >> Seq.filter isDirectory) |> enumerateFiles }
