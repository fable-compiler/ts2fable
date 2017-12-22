module rec ts2fable.fileSystem
open Fable.PowerPack
open Node
open Node.Fs
open Fable.Core
open Mkdirp
open Fable.Core.JsInterop

let enumerateFileSystemEntries dir =
    let encoding = BufferEncoding.Utf8 |> U2.Case2 |> Some
    let t= dir |> PathLike.ofString
    fs.readdirSync (t,encoding) |> Seq.map(fun str -> path.join(ResizeArray<string> [dir;str]))       
let isFile path=  
    let stats=
        path |> PathLike.ofString |> fs.lstatSync
    stats.isFile()
let isDirectory path=  
    let stats=
        path |> PathLike.ofString |> fs.lstatSync
    stats.isDirectory()  
//get all files from a dirctory
let rec enumerateFiles dirs =
    if Seq.isEmpty dirs then Seq.empty else
        seq { yield! dirs |> Seq.collect (enumerateFileSystemEntries >> Seq.filter isFile)
              yield! dirs |> Seq.collect (enumerateFileSystemEntries >> Seq.filter isDirectory) |> enumerateFiles }

let readLines file = 
    let path: U2<PathLike, float> =U2.Case1 (file |> PathLike.ofString)
    let encoding:U2<obj, string> = U2.Case2 "utf-8"
    let file =  fs.readFileSync (path,encoding)
    file.Split('\n') |> Seq.ofArray
let readLine num file = 
    readLines file |> Seq.item (num - 1)
