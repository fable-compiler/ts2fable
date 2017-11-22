[<AutoOpen>]
module ts2fable.NodeExt
   open Node.fs

    // union helpers: of, is, & as
    // https://github.com/fable-compiler/ts2fable/issues/105
    [<RequireQualifiedAccess>]
    module PathLike =
        let ofString v = PathLike.Case1 v
        let ofBuffer v = PathLike.Case2 v
        let ofURL v = PathLike.Case3 v
        let isString (v: PathLike) = match v with | PathLike.Case1 _ -> true | _ -> false
        let isBuffer (v: PathLike) = match v with | PathLike.Case2 _ -> true | _ -> false
        let isURL (v: PathLike) = match v with | PathLike.Case3 _ -> true | _ -> false
        let asString (v: PathLike) = match v with | PathLike.Case1 o -> Some o | _ -> None
        let asBuffer(v: PathLike) = match v with | PathLike.Case2 o -> Some o | _ -> None
        let asURL (v: PathLike) = match v with | PathLike.Case3 o -> Some o | _ -> None