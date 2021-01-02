module rec ts2fable.TransformComments
open ts2fable.Transform

let transform (f: FsFile): FsFile =

    //todo: implement
    let fix ns =
        function
        | t -> t

    f
    |> fixFile fix