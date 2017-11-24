// ts2fable 0.0.0
module rec Yargs
open System
open Fable.Core
open Fable.Import.JS

let [<Import("*","yargs")>] yargs: yargs.Argv = jsNative

module yargs =

    type [<AllowNullLiteral>] Argv =
        abstract argv: Arguments with get, set
        [<Emit "$0($1...)">] abstract Invoke: ?args: ResizeArray<string> * ?cwd: string -> Arguments
        abstract parse: args: U2<string, ResizeArray<string>> * ?context: obj * ?parseCallback: ParseCallback -> Arguments
        abstract reset: unit -> Argv
        abstract locale: unit -> string
        abstract locale: loc: string -> Argv
        abstract detectLocale: detect: bool -> Argv
        abstract terminalWidth: unit -> float
        abstract alias: shortName: string * longName: U2<string, ResizeArray<string>> -> Argv
        abstract alias: shortNames: ResizeArray<string> * longName: string -> Argv
        abstract alias: aliases: obj -> Argv
        abstract array: key: string -> Argv
        abstract array: keys: ResizeArray<string> -> Argv
        abstract ``default``: key: string * value: obj option * ?description: string -> Argv
        abstract ``default``: defaults: obj * ?description: string -> Argv
        abstract demand: key: string * msg: string -> Argv
        abstract demand: key: string * ?required: bool -> Argv
        abstract demand: keys: ResizeArray<string> * msg: string -> Argv
        abstract demand: keys: ResizeArray<string> * ?required: bool -> Argv
        abstract demand: positionals: float * ?required: bool -> Argv
        abstract demand: positionals: float * msg: string -> Argv
        abstract demand: positionals: float * max: float * ?msg: string -> Argv
        abstract demandCommand: min: float * ?minMsg: string -> Argv
        abstract demandCommand: min: float * ?max: float * ?minMsg: string * ?maxMsg: string -> Argv
        abstract demandOption: key: U2<string, ResizeArray<string>> * ?msg: string -> Argv
        abstract demandOption: key: U2<string, ResizeArray<string>> * ?demand: bool -> Argv
        abstract require: key: string * msg: string -> Argv
        abstract require: key: string * required: bool -> Argv
        abstract require: keys: ResizeArray<float> * msg: string -> Argv
        abstract require: keys: ResizeArray<float> * required: bool -> Argv
        abstract require: positionals: float * required: bool -> Argv
        abstract require: positionals: float * msg: string -> Argv
        abstract required: key: string * msg: string -> Argv
        abstract required: key: string * required: bool -> Argv
        abstract required: keys: ResizeArray<float> * msg: string -> Argv
        abstract required: keys: ResizeArray<float> * required: bool -> Argv
        abstract required: positionals: float * required: bool -> Argv
        abstract required: positionals: float * msg: string -> Argv
        abstract requiresArg: key: string -> Argv
        abstract requiresArg: keys: ResizeArray<string> -> Argv
        abstract describe: key: U2<string, ResizeArray<string>> * description: string -> Argv
        abstract describe: descriptions: obj -> Argv
        abstract option: key: string * options: Options -> Argv
        abstract option: options: obj -> Argv
        abstract options: key: string * options: Options -> Argv
        abstract options: options: obj -> Argv
        abstract usage: message: string * ?options: obj -> Argv
        abstract usage: ?options: obj -> Argv
        abstract command: command: U2<string, ResizeArray<string>> * description: string -> Argv
        abstract command: command: U2<string, ResizeArray<string>> * description: string * builder: (Argv -> Argv) -> Argv
        abstract command: command: U2<string, ResizeArray<string>> * description: string * builder: obj -> Argv
        abstract command: command: U2<string, ResizeArray<string>> * description: string * builder: obj * handler: (Arguments -> unit) -> Argv
        abstract command: command: U2<string, ResizeArray<string>> * description: string * builder: (Argv -> Argv) * handler: (Arguments -> unit) -> Argv
        abstract command: command: U2<string, ResizeArray<string>> * description: string * ``module``: CommandModule -> Argv
        abstract command: ``module``: CommandModule -> Argv
        abstract commandDir: dir: string * ?opts: RequireDirectoryOptions -> Argv
        abstract completion: unit -> Argv
        abstract completion: cmd: string * ?fn: AsyncCompletionFunction -> Argv
        abstract completion: cmd: string * ?fn: SyncCompletionFunction -> Argv
        abstract completion: cmd: string * ?description: string * ?fn: AsyncCompletionFunction -> Argv
        abstract completion: cmd: string * ?description: string * ?fn: SyncCompletionFunction -> Argv
        abstract example: command: string * description: string -> Argv
        abstract check: func: (Arguments -> obj -> obj option) * ?``global``: bool -> Argv
        abstract boolean: key: string -> Argv
        abstract boolean: keys: ResizeArray<string> -> Argv
        abstract string: key: string -> Argv
        abstract string: keys: ResizeArray<string> -> Argv
        abstract number: key: string -> Argv
        abstract number: keys: ResizeArray<string> -> Argv
        abstract choices: choices: obj -> Argv
        abstract choices: key: string * values: Choices -> Argv
        abstract config: unit -> Argv
        abstract config: explicitConfigurationObject: obj -> Argv
        abstract config: key: string * ?description: string * ?parseFn: (string -> obj) -> Argv
        abstract config: keys: ResizeArray<string> * ?description: string * ?parseFn: (string -> obj) -> Argv
        abstract config: key: string * parseFn: (string -> obj) -> Argv
        abstract config: keys: ResizeArray<string> * parseFn: (string -> obj) -> Argv
        abstract conflicts: key: string * value: string -> Argv
        abstract conflicts: conflicts: obj -> Argv
        abstract wrap: columns: float -> Argv
        abstract strict: unit -> Argv
        abstract help: unit -> Argv
        abstract help: enableExplicit: bool -> Argv
        abstract help: option: string * enableExplicit: bool -> Argv
        abstract help: option: string * ?description: string * ?enableExplicit: bool -> Argv
        abstract env: ?prefix: string -> Argv
        abstract env: enable: bool -> Argv
        abstract epilog: msg: string -> Argv
        abstract epilogue: msg: string -> Argv
        abstract version: ?version: string * ?option: string * ?description: string -> Argv
        abstract version: version: (unit -> string) * ?option: string * ?description: string -> Argv
        abstract showHelpOnFail: enable: bool * ?message: string -> Argv
        abstract showHelp: ?consoleLevel: string -> Argv
        abstract exitProcess: enabled: bool -> Argv
        abstract ``global``: key: string -> Argv
        abstract ``global``: keys: ResizeArray<string> -> Argv
        abstract group: key: string * groupName: string -> Argv
        abstract group: keys: ResizeArray<string> * groupName: string -> Argv
        abstract nargs: key: string * count: float -> Argv
        abstract nargs: nargs: obj -> Argv
        abstract normalize: key: string -> Argv
        abstract normalize: keys: ResizeArray<string> -> Argv
        abstract implies: key: string * value: string -> Argv
        abstract implies: implies: obj -> Argv
        abstract count: key: string -> Argv
        abstract count: keys: ResizeArray<string> -> Argv
        abstract fail: func: (string -> Error -> obj option) -> Argv
        abstract coerce: key: U2<string, ResizeArray<string>> * func: ('T -> 'U) -> Argv
        abstract coerce: opts: obj -> Argv
        abstract getCompletion: args: ResizeArray<string> * ``done``: (ResizeArray<string> -> unit) -> Argv
        abstract pkgConf: key: string * ?cwd: string -> Argv
        abstract pkgConf: keys: ResizeArray<string> * ?cwd: string -> Argv
        abstract recommendCommands: unit -> Argv
        abstract showCompletionScript: unit -> Argv
        abstract skipValidation: key: string -> Argv
        abstract skipValidation: keys: ResizeArray<string> -> Argv
        abstract updateLocale: obj: obj -> Argv
        abstract updateStrings: obj: obj -> Argv

    type [<AllowNullLiteral>] Arguments =
        /// Non-option arguments 
        abstract ``_``: ResizeArray<string> with get, set
        /// The script name or node command 
        abstract ``$0``: string with get, set
        /// All remaining options 
        [<Emit "$0[$1]{{=$2}}">] abstract Item: argName: string -> obj option with get, set

    type [<AllowNullLiteral>] RequireDirectoryOptions =
        abstract recurse: bool option with get, set
        abstract extensions: ResizeArray<string> option with get, set
        abstract visit: (obj option -> string -> string -> obj option) option with get, set
        abstract ``include``: U2<RegExp, (string -> bool)> option with get, set
        abstract exclude: U2<RegExp, (string -> bool)> option with get, set

    type [<AllowNullLiteral>] Options =
        abstract alias: U2<string, ResizeArray<string>> option with get, set
        abstract array: bool option with get, set
        abstract boolean: bool option with get, set
        abstract choices: Choices option with get, set
        abstract coerce: (obj option -> obj option) option with get, set
        abstract config: bool option with get, set
        abstract configParser: (string -> obj) option with get, set
        abstract conflicts: U2<string, obj> option with get, set
        abstract count: bool option with get, set
        abstract ``default``: obj option option with get, set
        abstract defaultDescription: string option with get, set
        abstract demand: U2<bool, string> option with get, set
        abstract demandOption: U2<bool, string> option with get, set
        abstract desc: string option with get, set
        abstract describe: string option with get, set
        abstract description: string option with get, set
        abstract ``global``: bool option with get, set
        abstract group: string option with get, set
        abstract implies: U2<string, obj> option with get, set
        abstract nargs: float option with get, set
        abstract normalize: bool option with get, set
        abstract number: bool option with get, set
        abstract require: U2<bool, string> option with get, set
        abstract required: U2<bool, string> option with get, set
        abstract requiresArg: U2<bool, string> option with get, set
        abstract skipValidation: bool option with get, set
        abstract string: bool option with get, set
        abstract ``type``: U5<string, string, string, string, string> option with get, set

    type [<AllowNullLiteral>] CommandModule =
        abstract aliases: U2<ResizeArray<string>, string> option with get, set
        abstract builder: CommandBuilder option with get, set
        abstract command: U2<ResizeArray<string>, string> option with get, set
        abstract describe: U2<string, obj> option with get, set
        abstract handler: (obj option -> unit) with get, set

    type ParseCallback =
        (Error option -> Arguments -> string -> unit)

    type CommandBuilder =
        U2<obj, (Argv -> Argv)>

    type SyncCompletionFunction =
        (string -> obj option -> ResizeArray<string>)

    type AsyncCompletionFunction =
        (string -> obj option -> (ResizeArray<string> -> unit) -> unit)

    type Choice =
        U2<string, obj> option

    type Choices =
        ResizeArray<Choice>
