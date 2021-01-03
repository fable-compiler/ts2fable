module rec ts2fable.TransformComments
open ts2fable.Transform

let private mergeSummaries (comments: FsComment list) =
    // there are multiple ways to specify summaries in jsdoc:
    // * normal, top level comment: `/** my summary */`
    //      * same as `@description`
    // * `@description`, `@desc` 
    //      * https://jsdoc.app/tags-description.html
    //      * long version of `@summary`
    // * `@summary`
    //      * https://jsdoc.app/tags-summary.html
    //      * short version of `@description`
    // in c# xml comments, these are all represented as summary
    comments
    |> List.choose (FsComment.asSummary)
    |> function
       | [] -> comments
       | [s] -> 
            // ensure summary is at front
            FsComment.Summary s
            ::
            (comments |> List.filter (not << FsComment.isSummary))
       | ss ->
            //todo: enhancement: `@summary` before `@description` (summary is short version of description)
            (
                ss
                                               // add empty line between two summaries
                                               // todo: enhancement: use paragraphs (`<para>...</para>`)
                |> List.reduce (fun a b -> a @ "" :: b)
                |> FsComment.Summary
            )
            ::
            (comments |> List.filter (not << FsComment.isSummary))



let transform (f: FsFile): FsFile =

    let transformComments (comments: FsComment list) =
        comments
        |> mergeSummaries

    //todo: add other types
    //todo: convert markdown & jsdoc link into xml
    //todo: introduce attributes for deprecated/obsolete
    let fix ns =
        function
        | FsType.Interface i ->
            { i with Comments = transformComments i.Comments }
            |> FsType.Interface
        | t -> t

    f
    |> fixFile fix