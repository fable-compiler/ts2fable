module rec ts2fable.TransformComments
open ts2fable.Transform

module private Text =
    open Fable.Core.JsInterop
    open System.Text.RegularExpressions
    open Fable.Core.DynamicExtensions

    module private Regex =
        open Fable.Core

        [<Emit("$0.replace($1, (...args) => $2(args))")>]
        let private nativeReplace (text: string) (regex: Regex) (replacer: obj array -> string): string = jsNative

        let private toMatch (args: obj array): Match =
            // args: https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String/replace#Specifying_a_function_as_a_parameter
            // match: matched substring
            // p1, p2, ...: nth capture group string
            // offset: offset of matched substring
            // string: whole string examined
            // groups: named capturing groups
            let (m: string, ps: string array, offset: int, input: string, groups) =
                (
                    !!args.[0],
                    !!args.[1 .. (args.Length - 4)],
                    !!args.[args.Length - 3],
                    !!args.[args.Length - 2],
                    args.[args.Length - 1]
                )

            // create match object...
            let m = 
                let fields = 
                    [
                        "input" ==> input
                        "index" ==> offset
                        "length" ==> m.Length
                        "groups" ==> groups
                    ]
                    @
                    (
                        ps
                        |> Array.toList
                        |> List.mapi (fun i m -> (sprintf "%i" i) ==> m)
                    )
                createObj fields

            !!m

        let replaceAll (regex: Regex) (replacer: Match -> string) (input: string) =
            // Match in replacer in `regex.Replace(input, replacer)` doesn't get groups
            // -> use custom function & ignore all good F# stuff...
            nativeReplace input regex (Regex.toMatch >> replacer)


    // groups cannot occur multiple times -> prepend group name

    /// markdown link: `[title](link)`
    let [<Literal>] private MdLink = "\[(?<mdLink_title>[^\)]*)\]\((?<mdLink_link>[^\]]*)\)"
    // jsDoc Link
    // https://jsdoc.app/tags-inline-link.html
    // Syntax:
    // * `{@link namepathOrURL}`
    // * `[link text]{@link namepathOrURL}` -> currently not handled
    // * `{@link namepathOrURL|link text}`
    // * `{@link namepathOrURL link text (after first space)}`
    let [<Literal>] private JsDocLink = "\{\@link (?<jsDocLink_link>[^ \|\}]+)(?:[ \|](?<jsDocLink_title>[^\}]*))?\}"
    /// Markdown inline code: between two backticks
    let [<Literal>] private MdInlineCode = "`(?<mdInlineCode_code>[^`]*)`"
    /// Simple url pattern: Url is basically something that contains `://`
    let [<Literal>] private Url = "(?<url_link>\w+\:\/\/\S+)"

    /// simple discrimination between href and cref: href contains `://`
    let isHref (possibleUrl: string) = possibleUrl.Contains "://"

    let private jsDocLinkRegex = JsDocLink |> Regex

    /// Fable doesn't support regex named groups
    /// -> use js workaround
    let private tryNamedGroup (name: string) (m: Match): string option =
        if m |> isNullOrUndefined then
            None
        else
            let value = m?groups.[name]
            if value |> isNullOrUndefined then
                None
            else
                Some <| unbox value

    /// `None` if group doesn't exist or is empty (empty string)
    let private tryNotEmptyGroup (name: string) (m: Match): string option =
        m |> tryNamedGroup name |> Option.bind (function "" -> None | v -> Some v)

    /// Requires group `name` to exist!
    /// -> check with tryNamed before
    ///
    /// Unlike `tryNotEmptyGroup`, this returns empty string too
    let private group (name: string) (m: Match): string =
        (m |> tryNamedGroup name).Value

    /// Active pattern to check if group `name` exists (empty string is considered unmatched)
    let private (|NotEmptyGroup|_|) name (m: Match) =
        m |> tryNotEmptyGroup name

    let transformLineToMarkdown (line: string) =
        // only transform jsDoc links
        let replace (m: Match) =
            let link = m |> group "jsDocLink_link"
            match m |> tryNotEmptyGroup "jsDocLink_title" with
            | Some title ->
                sprintf "[%s](%s)" title link
            | None -> link
        
        printfn "Transforming line %s" line
        line |> Regex.replaceAll jsDocLinkRegex replace

    let private mdInlineCodeRegex =
        sprintf "(?<mdInlineCode>%s)" MdInlineCode
        |> Regex
    let transformInlineCodeToXml (input: string) =
        let replace (m: Match) =
            match m with
            | NotEmptyGroup "mdInlineCode" _ ->
                group "mdInlineCode_code" m
                |> sprintf "<c>%s</c>"
            | _ -> m.Value

        input |> Regex.replaceAll mdInlineCodeRegex replace

    let private allRegex =
        [ 
          sprintf "(?<mdLink>%s)" MdLink
          sprintf "(?<jsDocLink>%s)" JsDocLink
          sprintf "(?<mdInlineCode>%s)" MdInlineCode
          sprintf "(?<url>%s)" Url
        ]
        |> String.concat "|"
        |> Regex

    let private mkXmlLink (link: string) (title: string option) =
        let start = 
            sprintf "<see %cref=\"%s\""
                (if link |> isHref then 'h' else 'c')
                link
        match title with
        | None -> sprintf "%s />" start
        | Some title -> 
            // title might contain code -> parse for inline code
            let title = title |> transformInlineCodeToXml
            sprintf "%s>%s</see>" start title

    let transformLineToXml (line: string) =
        let replace (m: Match) =
            match m with
            | NotEmptyGroup "mdLink" _ ->
                let (title, link) = (group "mdLink_title" m, group "mdLink_link" m)
                mkXmlLink link (Some title)
            | NotEmptyGroup "jsDocLink" _ ->
                let (title, link) = (tryNotEmptyGroup "jsDocLink_title" m, group "jsDocLink_link" m)
                mkXmlLink link title
            | NotEmptyGroup "mdInlineCode" _ ->
                let code = group "mdInlineCode_code" m
                sprintf "<c>%s</c>" code
            | NotEmptyGroup "url" _ ->
                let link = group "url_link" m
                mkXmlLink link None
            | _ -> m.Value

        line |> Regex.replaceAll allRegex replace

/// Inline XML tags are better supported in VS, but not so well in Ionide (VSCode).
/// Markdown comments are better supported in Ionide, but not at all in VS.
///
/// -> might be better to emit markdown instead of XML when using VSCode
let [<Literal>] ToMarkdown = false
let private (|Xml|Markdown|) _ = if ToMarkdown then Markdown else Xml

let private (|TransformMultilineCode|_|) (lines: string list) =
    match lines with
    | l :: ls when l.TrimStart().StartsWith "```" ->
        // take lines until ``` again
        let (code, remaining) =
            let length = ls |> List.tryFindIndex (fun l -> l.TrimStart().StartsWith "```")
            match length with
            | None -> 
                // no code block end -> invalid markdown
                // -> just end code block
                (ls, [])
            | Some i ->
                let (code, rest) = ls |> List.splitAt i
                // first line of rest is ```
                (code, rest |> List.skip 1)

        let codeLines =
            match () with
            | Markdown ->
                (l :: code @ ["```"])
            | Xml ->
                // block code start
                // might contain language
                let lang = 
                    match l.TrimStart().Substring(3).Trim() with
                    | "" -> None
                    | lang -> Some lang
                let startTag = 
                    // sandcastle understands language attribute
                    // https://ewsoftware.github.io/SHFB/html/7f03ba39-09f8-4320-bdbd-ed31a3bd885f.htm
                    match lang with
                    | None -> "<code>"
                    | Some lang ->
                        sprintf "<code language=\"%s\">" lang

                startTag :: code @ ["</code>"]

        Some (codeLines, remaining)
    | _ -> None

let private transformLine (line: string) =
    match () with
    | Xml ->
        [ Text.transformLineToXml line ]
    | Markdown ->
        [ Text.transformLineToMarkdown line ]


let private transformContent (comments: FsComment list) =

    let transform (content: FsCommentContent) =
        // detect block code
        // transform jsdoc links & markdown stuff into xml
        let rec transformLines (transformed: string list) (lines: string list) =
            match lines with
            | [] -> transformed
            | TransformMultilineCode (code, remaining) ->
                transformLines (transformed @ code) remaining
            | l :: ls ->
                let l = transformLine l
                transformLines (transformed @ l) ls

        transformLines [] content

    comments
    |> List.map (
        function
        | FsComment.Summary c ->
            transform c
            |> FsComment.Summary
        | FsComment.Param ({Content = c } as p) -> 
            { p with Content = transform c }
            |> FsComment.Param
        | FsComment.TypeParam ({Content = c } as tp) -> 
            { tp with Content = transform c }
            |> FsComment.TypeParam
        | FsComment.Returns c -> 
            transform c 
            |> FsComment.Returns
        | FsComment.Remarks c -> 
            transform c 
            |> FsComment.Remarks
        | FsComment.SeeAlso ({ Title = c } as sa) ->
            { sa with Title = transform c }
            |> FsComment.SeeAlso
        | FsComment.Example c -> 
            transform c 
            |> FsComment.Example
        | FsComment.Exception ({ Content = c } as e) ->
            { e with Content = transform c }
            |> FsComment.Exception
        | FsComment.Version c -> 
            transform c 
            |> FsComment.Version
        | FsComment.Default c -> 
            transform c 
            |> FsComment.Default
        | FsComment.Tag ({ Content = c } as t) -> 
            { t with Content = transform c }
            |> FsComment.Tag
        | FsComment.UnknownTag _ as u -> u
        | FsComment.Unknown _ as u -> u
    )

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
        |> transformContent

    //todo: add other types
    //todo: parse typeparam, exception, etc. for leading name
    //todo: introduce attributes for deprecated/obsolete
    let fix ns =
        function
        | FsType.Interface i ->
            { i with Comments = transformComments i.Comments }
            |> FsType.Interface
        | FsType.Function f ->
            { f with Comments = transformComments f.Comments }
            |> FsType.Function
        | t -> t

    f
    |> fixFile fix
