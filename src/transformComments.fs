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
    let linkType (url: string) = if isHref url then HRef else CRef

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

    /// Transform JS Doc namepath to ~ .Net valid path.
    /// Change `#` (instance) & `~` (inner member) to `.`
    /// 
    /// HRefs are ignored and not changed.
    let private transformCref (link: string) =
        // https://jsdoc.app/about-namepaths.html
        // namepath might contain a path with `#` (instance) or `~` (inner member)
        // -> change all to use `.`

        // special case: `#myFunction` -> `this#myFunction`
        // -> transform to just `myFunction`

        if link |> isHref then
            // don't change href (might contain `#`)
            link
        else
            let link =
                if link.StartsWith "#" || link.StartsWith "~" then
                    link.Substring 1
                else
                    link
            link.Replace('#', '.').Replace('~', '.')

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
        let link = link |> transformCref
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

    let [<Literal>] private LeadingType = "^\s*(?:\{(?<type>[^\}]*)\})(?<description>.*)$"
    let private leadingTypeRegex = LeadingType |> Regex
    /// Parses leading type in curly braces
    /// * `{Type} Description`
    /// * `{Type}`
    ///
    /// Returns `Some (Type, rest option)`
    let parseLeadingType (input: string) =
        // used in https://jsdoc.app/tags-throws.html
        let m = leadingTypeRegex.Match input
        if m.Success then
            Some (group "type" m, tryNotEmptyGroup "description" m)
        else
            None

    let [<Literal>] private LeadingName = "^\s*(?<name>\w+)\s*(?:[-:])?\s*(?<description>.*)$"
    let private leadingNameRegex = LeadingName |> Regex
    /// Parses leading name
    /// * `Name description`
    /// * `Name: description`
    /// * `Name - description`
    ///
    /// Returns `Some (Name, rest option)`
    let parseLeadingName (input: string) =
        // used in `@typeparam`
        let m = leadingNameRegex.Match input
        if m.Success then
            Some (group "name" m, tryNotEmptyGroup "description" m)
        else
            None

    let (*[<Literal>]*) private LeadingLink = "^\s*((?<jsDocLink>" + JsDocLink + ")|(?<mdLink>" + MdLink + ")|(?<path>[^\s]+))\s*(?<description>.*)$"
    let private leadingLinkRegex = LeadingLink |> Regex
    /// Parses leading link
    /// * just one term or jsDocLink
    /// * text with leading jsDocLink
    /// * `namepath` (ref to function, class, ... -> cref)
    /// * `https://www.github.com`
    /// * `{@link bar}`
    /// * `{@link bar} for further info`
    /// * `{@link bar|for further info}`
    /// * `{@link bar for further info}`
    /// * `namepath some description`
    /// * `[Some description](link)`
    /// * `[Some description](link) and more description`
    /// 
    /// Returns `Some (target, title option, rest option)`
    /// 
    /// Note: No diffentiation between href & cref
    let parseLeadingLink (input: string) =
        // used in https://jsdoc.app/tags-see.html
        let m = leadingLinkRegex.Match input
        if m.Success then
            match m with
            | NotEmptyGroup "jsDocLink" _ ->
                let (title, link) = (tryNotEmptyGroup "jsDocLink_title" m, group "jsDocLink_link" m)
                let description = tryNotEmptyGroup "description" m
                Some (link |> transformCref, title, description)
            | NotEmptyGroup "mdLink" _ ->
                let (title, link) = (group "mdLink_title" m, group "mdLink_link" m)
                let description = tryNotEmptyGroup "description" m
                Some (link |> transformCref, Some title, description)
            | NotEmptyGroup "path" link ->
                let description = tryNotEmptyGroup "description" m
                Some (link |> transformCref, None, description)
            | _ -> None
        else
            // empty input
            None


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
        | FsComment.SeeAlso ({ Content = c } as sa) ->
            { sa with Content = transform c }
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


let private transformTags (comments: FsComment list): FsComment list =
    // extract unparsed tags like typeparam or exception
    // todo: filter out deprecated and put into obsolete attribute 

    let (|AtLeastOneLine|_|) (c: FsCommentContent) =
        c
        |> List.skipWhile (System.String.IsNullOrWhiteSpace)
        |> function
           | l::ls -> Some (l, ls)
           | _ -> None

    let combineWith ls head =
        match head with
        | Some head -> head :: ls
        | None -> ls

    comments
    |> List.choose (
        function
        | FsComment.Tag { Name = "throws"; Content = c } ->
            // https://jsdoc.app/tags-throws.html
            // Syntax:
            // * `@throws free-form description`
            // * `@throws {<type>}`
            // * `@throws {<type>} free-form description`
            match c with
            | AtLeastOneLine (l, ls) ->
                match Text.parseLeadingType l with
                | Some (ty, rest) ->
                    { Type = Some ty; Content = rest |> combineWith ls }
                | None ->
                    { Type = None; Content = c }
                |> FsComment.Exception
                |> Some
            | _ -> None

        | FsComment.Tag { Name = "typeparam"; Content = c } ->
            // not an official jsdoc tag
            // but quite often used in `Definitely Typed` repo

            // Syntax:
            // * `TypeParameterName description`
            // * `TypeParameterName: description`
            // * `TypeParameterName - description`
            match c with
            | AtLeastOneLine (l, ls) ->
                match Text.parseLeadingName l with
                | None -> None
                | Some (name, rest) ->
                    {
                        Name = name
                        Content = rest |> combineWith ls
                    }
                    |> FsComment.TypeParam
                    |> Some
            | _ -> None

        | FsComment.Tag { Name = "see"; Content = c } ->
            // https://jsdoc.app/tags-see.html
            // Syntax:
            // * `@see bar` -> code ref
            // * `@see https://www.github.com`
            // * `@see {@link bar}`
            // * `@see {@link bar} description`
            // * `@see {@link bar|description}`
            // * `@see {@link bar description}`
            // -> first part is link, other parts are description
            //
            // Special cases because often used (in Definitely Typed):
            // * `@see bar some description` -> first part is ref, after space description
            // * `@see [description](ref)` -> markdown link
            //
            // Code refs:
            // * Name: `LineDash`
            // * ~namespaced: `@see com.sun.star.configuration.SetAccess`
            // * Hashed: `Model#create` -> replace with `.`
            // * with parameter: `JsMockito.Integration.importTo(window)` (used to further specify `any`) -> don't handle
            // * whatever: `_.head` (probably intended as "for docs: see any other head method...")
            // * file link: `javascript/RelayFlowGenerator.js` -> don't handle
            //          // todo: change url detection to use `/`?
            //
            // issue: in doc tools `<seealso>` doesn't always support title: (same for `<see>`)
            // * cref: title usually isn't handled: `<seealso cref="bar">title</seealso>` -> `bar`
            // * href: title usally is handled: `<seealso cref="https://github.com">title</seealso>` -> `title`

            match c with
            | AtLeastOneLine (l, ls) ->
                Fable.Core.JS.console.log (Text.parseLeadingLink l)
                match Text.parseLeadingLink l with
                | None -> None
                | Some (target, title, rest) ->
                    {
                        Type = Text.linkType target
                        Target = target
                        Content = 
                            // no differentiation between title and description in xml
                            // -> combine
                            match (title, rest) with
                            | (Some title, Some rest) ->
                                sprintf "%s %s" title rest
                                :: ls
                            | (Some title, None) -> title :: ls
                            | (None, Some rest) -> rest :: ls
                            | (None, None) -> ls
                    }
                    |> FsComment.SeeAlso
                    |> Some
            | _ -> None

        | FsComment.Param ({ Content = AtLeastOneLine (l, ls) } as p) ->
            // param name already extracted, because @param is parsed by TypeScript compiler
            // https://jsdoc.app/tags-param.html
            // BUT: it doesn't remove `-` after param name (which is allowed by specs)
            //      neither does it remove `:` after name (not in specs, but sometimes used)
            if l.StartsWith "-" || l.StartsWith ":" then
                let l = l.Substring(1).TrimStart()
                let content =
                    if System.String.IsNullOrWhiteSpace(l) then
                        // ensure rest isn't empty
                        match ls with
                        | AtLeastOneLine _ -> Some ls
                        | _ -> None
                    else
                        Some <| l :: ls
                
                match content with
                | Some c -> 
                    { p with Content = c }
                    |> FsComment.Param
                    |> Some
                | None -> None
            else
                p
                |> FsComment.Param
                |> Some

        | c -> Some c
    )

/// Extract `@deprecated` JSDoc tag from comments and convert into `Obsolete` Attribute
/// 
/// `None` if no `@deprecated`
let private extractDeprecated (comments: FsComment list): (FsAttribute * FsComment list) option =
    let isDeprecated =
        function
        | FsComment.Tag { Name = "deprecated" } -> true
        | _ -> false
    let asDeprecated =
        function
        | FsComment.Tag { Name = "deprecated"; Content = text } -> Some text
        | _ -> None

    let deprecated =
        comments
        |> List.filter isDeprecated

    match deprecated with
    | [] -> None
    | _ ->
        let commentsWithoutDeprecated =
            comments
            |> List.filter (not << isDeprecated)

        let arg =
            deprecated
               // don't directly choose text above: tag might be specified, but text is empty
            |> List.choose asDeprecated
            |> List.collect id
               // escape quotation marks
            |> List.map (fun l -> l.Replace("\"", "\\\""))
            |> String.concat "\n"

        let attr = 
            { FsAttribute.fromName "Obsolete" with
                // no namespace: `open System` always emitted
                Arguments = [
                    FsArgument.justValue <| sprintf "\"%s\"" arg
                ]
            }

        Some (attr, commentsWithoutDeprecated)

/// Convert `@deprecated` JSDoc tags into `Obsolete` Attributes
let private convertDeprecatedTagIntoObsoleteAttribute _ ty =
    let (|Obsolete|_|) = extractDeprecated

    match ty with
    | FsType.Interface ({ Comments = Obsolete (attr, comments)} as i) ->
        { i with Comments = comments; Attributes = [attr] :: i.Attributes }
        |> FsType.Interface
    | FsType.Function ({ Comments = Obsolete (attr, comments)} as f) ->
        { f with Comments = comments; Attributes = [attr] :: f.Attributes }
        |> FsType.Function
    | FsType.Enum ({ Comments = Obsolete (attr, comments)} as e) ->
        { e with
            Comments = comments
            Attributes = [attr] :: e.Attributes
            Cases = 
                e.Cases 
                |> List.map (
                    function
                    | ({ Comments = Obsolete (attr, comments) } as c) ->
                        { c with Comments = comments; Attributes = [attr] :: c.Attributes }
                    | c -> c
                   )
        }
        |> FsType.Enum
    | FsType.Property ({ Comments = Obsolete (attr, comments)} as p) ->
        { p with Comments = comments; Attributes = [attr] :: p.Attributes }
        |> FsType.Property
    | FsType.Alias ({ Comments = Obsolete (attr, comments)} as a) ->
        { a with Comments = comments; Attributes = [attr] :: a.Attributes }
        |> FsType.Alias
    | FsType.Variable ({ Comments = Obsolete (attr, comments)} as v) ->
        { v with Comments = comments; Attributes = [attr] :: v.Attributes }
        |> FsType.Variable
    | FsType.Module ({ Comments = Obsolete (attr, comments)} as m) ->
        { m with Comments = comments; Attributes = [attr] :: m.Attributes }
        |> FsType.Module
    | t -> t

let transform (f: FsFile): FsFile =

    let transformComments (comments: FsComment list) =
        comments
        |> mergeSummaries
        |> transformTags
        |> transformContent

    let fix ns =
        function
        | FsType.Interface i ->
            { i with Comments = transformComments i.Comments }
            |> FsType.Interface
        | FsType.Function f ->
            { f with Comments = transformComments f.Comments }
            |> FsType.Function
        | FsType.Enum e ->
            { e with
                Comments = transformComments e.Comments
                Cases = e.Cases |> List.map (fun c -> { c with Comments = transformComments c.Comments })
            }
            |> FsType.Enum
        | FsType.Property p ->
            { p with Comments = transformComments p.Comments }
            |> FsType.Property
        | FsType.Alias a ->
            { a with Comments = transformComments a.Comments}
            |> FsType.Alias
        | FsType.Variable v ->
            { v with Comments = transformComments v.Comments }
            |> FsType.Variable
        | FsType.Module m ->
            { m with Comments = transformComments m.Comments }
            |> FsType.Module
        | t -> t

    f
    |> fixFile convertDeprecatedTagIntoObsoleteAttribute
    |> fixFile fix
