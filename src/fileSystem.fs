module rec ts2fable.fileSystem
open Fable.PowerPack
open Node
open Node.Fs
open Fable.Core
open Mkdirp
open Fable.Core.JsInterop

//get all files from a dirctory
let enumerateFiles dir=
    promise {
        let rec loop (array:ResizeArray<string>) dir = 
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

            promise {
                let paths = enumerateFileSystemEntries dir
                let files = paths |> Seq.filter isFile
                array.AddRange files
                let dirs = paths |> Seq.filter isDirectory 
                printfn "%A" dirs
                for dir in dirs do
                    return! loop array dir
            }
        let resizeArray = ResizeArray<string>()    
        do! loop resizeArray dir 
        return resizeArray |> Seq.toList
    }

let readLines file = 
    let path: U2<PathLike, float> =U2.Case1 (file |> PathLike.ofString)
    let encoding:U2<obj, string> = U2.Case2 "utf-8"
    let file =  fs.readFileSync (path,encoding)
    file.Split('\n') |> Seq.toList
let readLine num file = 
    let path: U2<PathLike, float> =U2.Case1 (file |> PathLike.ofString)
    let encoding:U2<obj, string> = U2.Case2 "utf-8"
    let file =  fs.readFileSync (path,encoding)
    file.Split('\n').[num - 1]
