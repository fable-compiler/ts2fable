module ts2fable.node.FileSystem
open Node
open Node.Fs
open Fable.Core
let readText file =
    let path: U2<PathLike, float> =U2.Case1 (file |> PathLike.ofString)
    let encoding:U2<obj, string> = U2.Case2 "utf-8"
    fs.readFileSync (path,encoding)
let readLines = 
    readText >> fun text ->
        text.Split('\n') |> Seq.ofArray
let readLine num file = 
    readLines file |> Seq.item (num - 1)