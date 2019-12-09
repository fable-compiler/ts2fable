// ts2fable 0.0.0
module rec Yargs
open System
open Fable.Core
open Fable.Core.JS

let [<Import("*","yargs")>] yargs: Yargs.Argv = jsNative

type Array<'T> = 'T[]
type ReadonlyArray<'T> = 'T[]
type RegExp = obj
exception Error

type [<AllowNullLiteral>] Arguments =
    /// Non-option arguments
    abstract ``_``: ResizeArray<string> with get, set
    /// The script name or node command
    abstract ``$0``: string with get, set
    /// All remaining options
    [<Emit "$0[$1]{{=$2}}">] abstract Item: argName: string -> obj option with get, set

type [<AllowNullLiteral>] DetailedArguments =
    /// An object representing the parsed value of `args`
    abstract argv: Arguments with get, set
    /// Populated with an error object if an exception occurred during parsing.
    abstract error: Error option with get, set
    /// The inferred list of aliases built by combining lists in opts.alias.
    abstract aliases: TypeLiteral_03 with get, set
    /// Any new aliases added via camel-case expansion.
    abstract newAliases: TypeLiteral_04 with get, set
    /// The configuration loaded from the yargs stanza in package.json.
    abstract configuration: Configuration with get, set

type [<AllowNullLiteral>] Configuration =
    /// Should variables prefixed with --no be treated as negations? Default is `true`
    abstract ``boolean-negation``: bool with get, set
    /// Should hyphenated arguments be expanded into camel-case aliases? Default is `true`
    abstract ``camel-case-expansion``: bool with get, set
    /// Should arrays be combined when provided by both command line arguments and a configuration file. Default is `false`
    abstract ``combine-arrays``: bool with get, set
    /// Should keys that contain . be treated as objects? Default is `true`
    abstract ``dot-notation``: bool with get, set
    /// Should arguments be coerced into an array when duplicated. Default is `true`
    abstract ``duplicate-arguments-array``: bool with get, set
    /// Should array arguments be coerced into a single array when duplicated. Default is `true`
    abstract ``flatten-duplicate-arrays``: bool with get, set
    /// Should parsing stop at the first text argument? This is similar to how e.g. ssh parses its command line. Default is `false`
    abstract ``halt-at-non-option``: bool with get, set
    /// The prefix to use for negated boolean variables. Default is `'no-'`
    abstract ``negation-prefix``: string with get, set
    /// Should keys that look like numbers be treated as such? Default is `true`
    abstract ``parse-numbers``: bool with get, set
    /// Should unparsed flags be stored in -- or _. Default is `false`
    abstract ``populate--``: bool with get, set
    /// Should a placeholder be added for keys not set via the corresponding CLI argument? Default is `false`
    abstract ``set-placeholder-key``: bool with get, set
    /// Should a group of short-options be treated as boolean flags? Default is `true`
    abstract ``short-option-groups``: bool with get, set
    /// Should aliases be removed before returning results? Default is `false`
    abstract ``strip-aliased``: bool with get, set
    /// Should dashed keys be removed before returning results? This option has no effect if camel-case-expansion is disabled. Default is `false`
    abstract ``strip-dashed``: bool with get, set

module Yargs =

    type BuilderCallback<'T, 'R> =
        U2<(Argv<'T> -> Argv<'R>), (Argv<'T> -> unit)>

    type Argv =
        Argv<obj>

    /// The type parameter `T` is the expected shape of the parsed options.
    /// `Arguments<T>` is those options plus `_` and `$0`, and an indexer falling
    /// back to `unknown` for unknown options.
    /// 
    /// For the return type / `argv` property, we create a mapped type over
    /// `Arguments<T>` to simplify the inferred type signature in client code.
    type [<AllowNullLiteral>] Argv<'T> =
        [<Emit "$0($1...)">] abstract Invoke: unit -> obj
        [<Emit "$0($1...)">] abstract Invoke: args: ResizeArray<string> * ?cwd: string -> Argv<'T>
        /// Set key names as equivalent such that updates to a key will propagate to aliases and vice-versa.
        /// 
        /// Optionally `.alias()` can take an object that maps keys to aliases.
        /// Each key of this object should be the canonical version of the option, and each value should be a string or an array of strings.
        abstract alias: shortName: 'K1 * longName: U2<'K2, ResizeArray<'K2>> -> Argv<obj>
        // abstract alias: shortName: 'K2 * longName: U2<'K1, ResizeArray<'K1>> -> Argv<obj>
        abstract alias: shortName: U2<string, ResizeArray<string>> * longName: U2<string, ResizeArray<string>> -> Argv<'T>
        abstract alias: aliases: ArgvAliasAliases -> Argv<'T>
        /// Get the arguments as a plain old object.
        /// 
        /// Arguments without a corresponding flag show up in the `argv._` array.
        /// 
        /// The script name or node command is available at `argv.$0` similarly to how `$0` works in bash or perl.
        /// 
        /// If `yargs` is executed in an environment that embeds node and there's no script name (e.g. Electron or nw.js),
        /// it will ignore the first parameter since it expects it to be the script name. In order to override
        /// this behavior, use `.parse(process.argv.slice(1))` instead of .argv and the first parameter won't be ignored.
        abstract argv: Arguments with get, set
        /// Tell the parser to interpret `key` as an array.
        /// If `.array('foo')` is set, `--foo foo bar` will be parsed as `['foo', 'bar']` rather than as `'foo'`.
        /// Also, if you use the option multiple times all the values will be flattened in one array so `--foo foo --foo bar` will be parsed as `['foo', 'bar']`
        /// 
        /// When the option is used with a positional, use `--` to tell `yargs` to stop adding values to the array.
        abstract array: key: U2<'K, ResizeArray<'K>> -> Argv<obj>
        /// Interpret `key` as a boolean. If a non-flag option follows `key` in `process.argv`, that string won't get set as the value of `key`.
        /// 
        /// `key` will default to `false`, unless a `default(key, undefined)` is explicitly set.
        /// 
        /// If `key` is an array, interpret all the elements as booleans.
        abstract boolean: key: U2<'K, ResizeArray<'K>> -> Argv<obj>
        /// <summary>Check that certain conditions are met in the provided arguments.</summary>
        /// <param name="func">Called with two arguments, the parsed `argv` hash and an array of options and their aliases.
        /// If `func` throws or returns a non-truthy value, show the thrown error, usage information, and exit.</param>
        /// <param name="global">Indicates whether `check()` should be enabled both at the top-level and for each sub-command.</param>
        abstract check: func: (Arguments<'T> -> TypeLiteral_01 -> obj option) * ?``global``: bool -> Argv<'T>
        /// Limit valid values for key to a predefined set of choices, given as an array or as an individual value.
        /// If this method is called multiple times, all enumerated values will be merged together.
        /// Choices are generally strings or numbers, and value matching is case-sensitive.
        /// 
        /// Optionally `.choices()` can take an object that maps multiple keys to their choices.
        /// 
        /// Choices can also be specified as choices in the object given to `option()`.
        abstract choices: key: 'K * values: 'C -> Argv<obj>
        abstract choices: choices: 'C -> Argv<obj>
        /// Provide a synchronous function to coerce or transform the value(s) given on the command line for `key`.
        /// 
        /// The coercion function should accept one argument, representing the parsed value from the command line, and should return a new value or throw an error.
        /// The returned value will be used as the value for `key` (or one of its aliases) in `argv`.
        /// 
        /// If the function throws, the error will be treated as a validation failure, delegating to either a custom `.fail()` handler or printing the error message in the console.
        /// 
        /// Coercion will be applied to a value after all other modifications, such as `.normalize()`.
        /// 
        /// Optionally `.coerce()` can take an object that maps several keys to their respective coercion function.
        /// 
        /// You can also map the same function to several keys at one time. Just pass an array of keys as the first argument to `.coerce()`.
        /// 
        /// If you are using dot-notion or arrays, .e.g., `user.email` and `user.password`, coercion will be applied to the final object that has been parsed
        abstract coerce: key: U2<'K, ResizeArray<'K>> * func: (obj option -> 'V) -> Argv<obj>
        abstract coerce: opts: 'O -> Argv<obj>
        /// <summary>Define the commands exposed by your application.</summary>
        /// <param name="command">Should be a string representing the command or an array of strings representing the command and its aliases.</param>
        /// <param name="description">Use to provide a description for each command your application accepts (the values stored in `argv._`).
        /// Set `description` to false to create a hidden command. Hidden commands don't show up in the help output and aren't available for completion.</param>
        /// <param name="builder">Object to give hints about the options that your command accepts.
        /// Can also be a function. This function is executed with a yargs instance, and can be used to provide advanced command specific help.
        /// 
        /// Note that when `void` is returned, the handler `argv` object type will not include command-specific arguments.</param>
        /// <param name="handler">Function, which will be executed with the parsed `argv` object.</param>
        abstract command: command: U2<string, ResizeArray<string>> * description: string * ?builder: BuilderCallback<'T, 'U> * ?handler: (Arguments<'U> -> unit) -> Argv<'T>
        // abstract command: command: U2<string, ResizeArray<string>> * description: string * ?builder: 'O * ?handler: (Arguments<InferredOptionTypes<'O>> -> unit) -> Argv<'T>
        abstract command: command: U2<string, ResizeArray<string>> * description: string * ``module``: CommandModule<'T, 'U> -> Argv<'U>
        abstract command: command: U2<string, ResizeArray<string>> * showInHelp: obj * ?builder: BuilderCallback<'T, 'U> * ?handler: (Arguments<'U> -> unit) -> Argv<'T>
        // abstract command: command: U2<string, ResizeArray<string>> * showInHelp: obj * ?builder: 'O * ?handler: (Arguments<InferredOptionTypes<'O>> -> unit) -> Argv<'T>
        abstract command: command: U2<string, ResizeArray<string>> * showInHelp: obj * ``module``: CommandModule<'T, 'U> -> Argv<'U>
        abstract command: ``module``: CommandModule<'T, 'U> -> Argv<'U>
        /// Apply command modules from a directory relative to the module calling this method.
        abstract commandDir: dir: string * ?opts: RequireDirectoryOptions -> Argv<'T>
        /// Enable bash/zsh-completion shortcuts for commands and options.
        /// 
        /// If invoked without parameters, `.completion()` will make completion the command to output the completion script.
        abstract completion: unit -> Argv<'T>
        abstract completion: cmd: string * ?func: AsyncCompletionFunction -> Argv<'T>
        abstract completion: cmd: string * ?func: SyncCompletionFunction -> Argv<'T>
        abstract completion: cmd: string * ?func: PromiseCompletionFunction -> Argv<'T>
        abstract completion: cmd: string * ?description: U2<string, obj> * ?func: AsyncCompletionFunction -> Argv<'T>
        abstract completion: cmd: string * ?description: U2<string, obj> * ?func: SyncCompletionFunction -> Argv<'T>
        abstract completion: cmd: string * ?description: U2<string, obj> * ?func: PromiseCompletionFunction -> Argv<'T>
        /// Tells the parser that if the option specified by `key` is passed in, it should be interpreted as a path to a JSON config file.
        /// The file is loaded and parsed, and its properties are set as arguments.
        /// Because the file is loaded using Node's require(), the filename MUST end in `.json` to be interpreted correctly.
        /// 
        /// If invoked without parameters, `.config()` will make --config the option to pass the JSON config file.
        abstract config: unit -> Argv<'T>
        abstract config: key: U2<string, ResizeArray<string>> * ?description: string * ?parseFn: (string -> obj) -> Argv<'T>
        abstract config: key: U2<string, ResizeArray<string>> * parseFn: (string -> obj) -> Argv<'T>
        abstract config: explicitConfigurationObject: obj -> Argv<'T>
        /// Given the key `x` is set, the key `y` must not be set. `y` can either be a single string or an array of argument names that `x` conflicts with.
        /// 
        /// Optionally `.conflicts()` can accept an object specifying multiple conflicting keys.
        abstract conflicts: key: string * value: U2<string, ResizeArray<string>> -> Argv<'T>
        abstract conflicts: conflicts: ArgvConflicts -> Argv<'T>
        /// Interpret `key` as a boolean flag, but set its parsed value to the number of flag occurrences rather than `true` or `false`. Default value is thus `0`.
        abstract count: key: U2<'K, ResizeArray<'K>> -> Argv<obj>
        /// Set `argv[key]` to `value` if no option was specified in `process.argv`.
        /// 
        /// Optionally `.default()` can take an object that maps keys to default values.
        /// 
        /// The default value can be a `function` which returns a value. The name of the function will be used in the usage string.
        /// 
        /// Optionally, `description` can also be provided and will take precedence over displaying the value in the usage instructions.
        abstract ``default``: key: 'K * value: 'V * ?description: string -> Argv<obj>
        abstract ``default``: defaults: 'D * ?description: string -> Argv<obj>
        abstract demand: key: U2<'K, ResizeArray<'K>> * ?msg: U2<string, obj> -> Argv<Defined<'T, 'K>>
        abstract demand: key: U2<string, ResizeArray<string>> * ?required: bool -> Argv<'T>
        abstract demand: positionals: float * msg: string -> Argv<'T>
        abstract demand: positionals: float * ?required: bool -> Argv<'T>
        abstract demand: positionals: float * max: float * ?msg: string -> Argv<'T>
        /// <param name="key">If is a string, show the usage information and exit if key wasn't specified in `process.argv`.
        /// If is an array, demand each element.</param>
        /// <param name="msg">If string is given, it will be printed when the argument is missing, instead of the standard error message.</param>
        abstract demandOption: key: U2<'K, ResizeArray<'K>> * ?msg: U2<string, obj> -> Argv<Defined<'T, 'K>>
        // abstract demandOption: key: U2<'K, ResizeArray<'K>> * ?msg: U2<string, obj> -> Argv<obj>
        abstract demandOption: key: U2<string, ResizeArray<string>> * ?demand: bool -> Argv<'T>
        /// Demand in context of commands.
        /// You can demand a minimum and a maximum number a user can have within your program, as well as provide corresponding error messages if either of the demands is not met.
        abstract demandCommand: unit -> Argv<'T>
        abstract demandCommand: min: float * ?minMsg: string -> Argv<'T>
        abstract demandCommand: min: float * ?max: float * ?minMsg: string * ?maxMsg: string -> Argv<'T>
        /// Describe a `key` for the generated usage information.
        /// 
        /// Optionally `.describe()` can take an object that maps keys to descriptions.
        abstract describe: key: U2<string, ResizeArray<string>> * description: string -> Argv<'T>
        abstract describe: descriptions: ArgvDescribeDescriptions -> Argv<'T>
        /// Should yargs attempt to detect the os' locale? Defaults to `true`.
        abstract detectLocale: detect: bool -> Argv<'T>
        /// Tell yargs to parse environment variables matching the given prefix and apply them to argv as though they were command line arguments.
        /// 
        /// Use the "__" separator in the environment variable to indicate nested options. (e.g. prefix_nested__foo => nested.foo)
        /// 
        /// If this method is called with no argument or with an empty string or with true, then all env vars will be applied to argv.
        /// 
        /// Program arguments are defined in this order of precedence:
        /// 1. Command line args
        /// 2. Env vars
        /// 3. Config file/objects
        /// 4. Configured defaults
        /// 
        /// Env var parsing is disabled by default, but you can also explicitly disable it by calling `.env(false)`, e.g. if you need to undo previous configuration.
        abstract env: unit -> Argv<'T>
        abstract env: prefix: string -> Argv<'T>
        abstract env: enable: bool -> Argv<'T>
        /// A message to print at the end of the usage instructions
        abstract epilog: msg: string -> Argv<'T>
        /// A message to print at the end of the usage instructions
        abstract epilogue: msg: string -> Argv<'T>
        /// Give some example invocations of your program.
        /// Inside `cmd`, the string `$0` will get interpolated to the current script name or node command for the present script similar to how `$0` works in bash or perl.
        /// Examples will be printed out as part of the help message.
        abstract example: command: string * description: string -> Argv<'T>
        /// Manually indicate that the program should exit, and provide context about why we wanted to exit. Follows the behavior set by `.exitProcess().`
        abstract exit: code: float * err: Error -> unit
        /// By default, yargs exits the process when the user passes a help flag, the user uses the `.version` functionality, validation fails, or the command handler fails.
        /// Calling `.exitProcess(false)` disables this behavior, enabling further actions after yargs have been validated.
        abstract exitProcess: enabled: bool -> Argv<'T>
        /// <summary>Method to execute when a failure occurs, rather than printing the failure message.</summary>
        /// <param name="func">Is called with the failure message that would have been printed, the Error instance originally thrown and yargs state when the failure occurred.</param>
        abstract fail: func: (string -> Error -> obj option) -> Argv<'T>
        /// <summary>Allows to programmatically get completion choices for any line.</summary>
        /// <param name="args">An array of the words in the command line to complete.</param>
        /// <param name="done">The callback to be called with the resulting completions.</param>
        abstract getCompletion: args: ResizeArray<string> * ``done``: (ResizeArray<string> -> unit) -> Argv<'T>
        /// Indicate that an option (or group of options) should not be reset when a command is executed
        /// 
        /// Options default to being global.
        abstract ``global``: key: U2<string, ResizeArray<string>> -> Argv<'T>
        /// Given a key, or an array of keys, places options under an alternative heading when displaying usage instructions
        abstract group: key: U2<string, ResizeArray<string>> * groupName: string -> Argv<'T>
        /// Hides a key from the generated usage information. Unless a `--show-hidden` option is also passed with `--help` (see `showHidden()`).
        abstract hide: key: string -> Argv<'T>
        /// Configure an (e.g. `--help`) and implicit command that displays the usage string and exits the process.
        /// By default yargs enables help on the `--help` option.
        /// 
        /// Note that any multi-char aliases (e.g. `help`) used for the help option will also be used for the implicit command.
        /// If there are no multi-char aliases (e.g. `h`), then all single-char aliases will be used for the command.
        /// 
        /// If invoked without parameters, `.help()` will use `--help` as the option and help as the implicit command to trigger help output.
        abstract help: unit -> Argv<'T>
        abstract help: enableExplicit: bool -> Argv<'T>
        abstract help: option: string * enableExplicit: bool -> Argv<'T>
        abstract help: option: string * ?description: string * ?enableExplicit: bool -> Argv<'T>
        /// Given the key `x` is set, it is required that the key `y` is set.
        /// y` can either be the name of an argument to imply, a number indicating the position of an argument or an array of multiple implications to associate with `x`.
        /// 
        /// Optionally `.implies()` can accept an object specifying multiple implications.
        abstract implies: key: string * value: U2<string, ResizeArray<string>> -> Argv<'T>
        abstract implies: implies: ArgvImplies -> Argv<'T>
        /// Return the locale that yargs is currently using.
        /// 
        /// By default, yargs will auto-detect the operating system's locale so that yargs-generated help content will display in the user's language.
        abstract locale: unit -> string
        /// Override the auto-detected locale from the user's operating system with a static locale.
        /// Note that the OS locale can be modified by setting/exporting the `LC_ALL` environment variable.
        abstract locale: loc: string -> Argv<'T>
        /// <summary>Define global middleware functions to be called first, in list order, for all cli command.</summary>
        /// <param name="callbacks">Can be a function or a list of functions. Each callback gets passed a reference to argv.</param>
        /// <param name="applyBeforeValidation">Set to `true` to apply middleware before validation. This will execute the middleware prior to validation checks, but after parsing.</param>
        abstract middleware: callbacks: U2<MiddlewareFunction<'T>, ResizeArray<MiddlewareFunction<'T>>> * ?applyBeforeValidation: bool -> Argv<'T>
        /// The number of arguments that should be consumed after a key. This can be a useful hint to prevent parsing ambiguity.
        /// 
        /// Optionally `.nargs()` can take an object of `key`/`narg` pairs.
        abstract nargs: key: string * count: float -> Argv<'T>
        abstract nargs: nargs: ArgvNargs -> Argv<'T>
        /// The key provided represents a path and should have `path.normalize()` applied.
        abstract normalize: key: U2<'K, ResizeArray<'K>> -> Argv<obj>
        /// Tell the parser to always interpret key as a number.
        /// 
        /// If `key` is an array, all elements will be parsed as numbers.
        /// 
        /// If the option is given on the command line without a value, `argv` will be populated with `undefined`.
        /// 
        /// If the value given on the command line cannot be parsed as a number, `argv` will be populated with `NaN`.
        /// 
        /// Note that decimals, hexadecimals, and scientific notation are all accepted.
        abstract number: key: U2<'K, ResizeArray<'K>> -> Argv<obj>
        /// This method can be used to make yargs aware of options that could exist.
        /// You can also pass an opt object which can hold further customization, like `.alias()`, `.demandOption()` etc. for that option.
        abstract option: key: 'K * options: 'O -> Argv<obj>
        abstract option: options: 'O -> Argv<obj>
        /// This method can be used to make yargs aware of options that could exist.
        /// You can also pass an opt object which can hold further customization, like `.alias()`, `.demandOption()` etc. for that option.
        abstract options: key: 'K * options: 'O -> Argv<obj>
        abstract options: options: 'O -> Argv<obj>
        /// Parse `args` instead of `process.argv`. Returns the `argv` object. `args` may either be a pre-processed argv array, or a raw argument string.
        /// 
        /// Note: Providing a callback to parse() disables the `exitProcess` setting until after the callback is invoked.
        abstract parse: unit -> obj
        abstract parse: arg: U2<string, ResizeArray<string>> * ?context: obj * ?parseCallback: ParseCallback<'T> -> obj
        /// If the arguments have not been parsed, this property is `false`.
        /// 
        /// If the arguments have been parsed, this contain detailed parsed arguments.
        abstract parsed: U2<DetailedArguments, obj> with get, set
        /// Allows to configure advanced yargs features.
        abstract parserConfiguration: configuration: obj -> Argv<'T>
        /// <summary>Similar to `config()`, indicates that yargs should interpret the object from the specified key in package.json as a configuration object.</summary>
        /// <param name="cwd">If provided, the package.json will be read from this location</param>
        abstract pkgConf: key: U2<string, ResizeArray<string>> * ?cwd: string -> Argv<'T>
        /// Allows you to configure a command's positional arguments with an API similar to `.option()`.
        /// `.positional()` should be called in a command's builder function, and is not available on the top-level yargs instance. If so, it will throw an error.
        abstract positional: key: 'K * opt: 'O -> Argv<obj>
        /// Should yargs provide suggestions regarding similar commands if no matching command is found?
        abstract recommendCommands: unit -> Argv<'T>
        abstract require: key: U2<'K, ResizeArray<'K>> * ?msg: U2<string, obj> -> Argv<Defined<'T, 'K>>
        abstract require: key: string * msg: string -> Argv<'T>
        abstract require: key: string * required: bool -> Argv<'T>
        abstract require: keys: ResizeArray<float> * msg: string -> Argv<'T>
        abstract require: keys: ResizeArray<float> * required: bool -> Argv<'T>
        abstract require: positionals: float * required: bool -> Argv<'T>
        abstract require: positionals: float * msg: string -> Argv<'T>
        abstract required: key: U2<'K, ResizeArray<'K>> * ?msg: U2<string, obj> -> Argv<Defined<'T, 'K>>
        abstract required: key: string * msg: string -> Argv<'T>
        abstract required: key: string * required: bool -> Argv<'T>
        abstract required: keys: ResizeArray<float> * msg: string -> Argv<'T>
        abstract required: keys: ResizeArray<float> * required: bool -> Argv<'T>
        abstract required: positionals: float * required: bool -> Argv<'T>
        abstract required: positionals: float * msg: string -> Argv<'T>
        abstract requiresArg: key: U2<string, ResizeArray<string>> -> Argv<'T>
        abstract reset: unit -> Argv<'T>
        /// Set the name of your script ($0). Default is the base filename executed by node (`process.argv[1]`)
        abstract scriptName: ``$0``: string -> Argv<'T>
        /// Generate a bash completion script.
        /// Users of your application can install this script in their `.bashrc`, and yargs will provide completion shortcuts for commands and options.
        abstract showCompletionScript: unit -> Argv<'T>
        /// <summary>Configure the `--show-hidden` option that displays the hidden keys (see `hide()`).</summary>
        /// <param name="option">If `boolean`, it enables/disables this option altogether. i.e. hidden keys will be permanently hidden if first argument is `false`.
        /// If `string` it changes the key name ("--show-hidden").</param>
        abstract showHidden: ?option: U2<string, bool> -> Argv<'T>
        abstract showHidden: option: string * ?description: string -> Argv<'T>
        /// Print the usage data using the console function consoleLevel for printing.
        abstract showHelp: ?consoleLevel: string -> Argv<'T>
        /// <summary>By default, yargs outputs a usage string if any error is detected.
        /// Use the `.showHelpOnFail()` method to customize this behavior.</summary>
        /// <param name="enable">If `false`, the usage string is not output.</param>
        /// <param name="message">Message that is output after the error message.</param>
        abstract showHelpOnFail: enable: bool * ?message: string -> Argv<'T>
        /// Specifies either a single option key (string), or an array of options. If any of the options is present, yargs validation is skipped.
        abstract skipValidation: key: U2<string, ResizeArray<string>> -> Argv<'T>
        /// Any command-line argument given that is not demanded, or does not have a corresponding description, will be reported as an error.
        /// 
        /// Unrecognized commands will also be reported as errors.
        abstract strict: unit -> Argv<'T>
        abstract strict: enabled: bool -> Argv<'T>
        /// Tell the parser logic not to interpret `key` as a number or boolean. This can be useful if you need to preserve leading zeros in an input.
        /// 
        /// If `key` is an array, interpret all the elements as strings.
        /// 
        /// `.string('_')` will result in non-hyphenated arguments being interpreted as strings, regardless of whether they resemble numbers.
        abstract string: key: U2<'K, ResizeArray<'K>> -> Argv<obj>
        abstract terminalWidth: unit -> float
        abstract updateLocale: obj: ArgvUpdateLocaleObj -> Argv<'T>
        /// Override the default strings used by yargs with the key/value pairs provided in obj
        /// 
        /// If you explicitly specify a locale(), you should do so before calling `updateStrings()`.
        abstract updateStrings: obj: ArgvUpdateStringsObj -> Argv<'T>
        /// Set a usage message to show which commands to use.
        /// Inside `message`, the string `$0` will get interpolated to the current script name or node command for the present script similar to how `$0` works in bash or perl.
        /// 
        /// If the optional `description`/`builder`/`handler` are provided, `.usage()` acts an an alias for `.command()`.
        /// This allows you to use `.usage()` to configure the default command that will be run as an entry-point to your application
        /// and allows you to provide configuration for the positional arguments accepted by your program:
        abstract usage: message: string -> Argv<'T>
        abstract usage: command: U2<string, ResizeArray<string>> * description: string * ?builder: (Argv<'T> -> Argv<'U>) * ?handler: (Arguments<'U> -> unit) -> Argv<'T>
        abstract usage: command: U2<string, ResizeArray<string>> * showInHelp: bool * ?builder: (Argv<'T> -> Argv<'U>) * ?handler: (Arguments<'U> -> unit) -> Argv<'T>
        abstract usage: command: U2<string, ResizeArray<string>> * description: string * ?builder: 'O * ?handler: (Arguments<InferredOptionTypes<'O>> -> unit) -> Argv<'T>
        abstract usage: command: U2<string, ResizeArray<string>> * showInHelp: bool * ?builder: 'O * ?handler: (Arguments<InferredOptionTypes<'O>> -> unit) -> Argv<'T>
        /// Add an option (e.g. `--version`) that displays the version number (given by the version parameter) and exits the process.
        /// By default yargs enables version for the `--version` option.
        /// 
        /// If no arguments are passed to version (`.version()`), yargs will parse the package.json of your module and use its version value.
        /// 
        /// If the boolean argument `false` is provided, it will disable `--version`.
        abstract version: unit -> Argv<'T>
        abstract version: version: string -> Argv<'T>
        abstract version: enable: bool -> Argv<'T>
        abstract version: optionKey: string * version: string -> Argv<'T>
        abstract version: optionKey: string * description: string * version: string -> Argv<'T>
        /// Format usage output to wrap at columns many columns.
        /// 
        /// By default wrap will be set to `Math.min(80, windowWidth)`. Use `.wrap(null)` to specify no column limit (no right-align).
        /// Use `.wrap(yargs.terminalWidth())` to maximize the width of yargs' usage instructions.
        abstract wrap: columns: float option -> Argv<'T>

    type [<AllowNullLiteral>] ArgvAliasAliases =
        [<Emit "$0[$1]{{=$2}}">] abstract Item: shortName: string -> U2<string, ResizeArray<string>> with get, set

    type [<AllowNullLiteral>] ArgvConflicts =
        [<Emit "$0[$1]{{=$2}}">] abstract Item: key: string -> U2<string, ResizeArray<string>> with get, set

    type [<AllowNullLiteral>] ArgvDescribeDescriptions =
        [<Emit "$0[$1]{{=$2}}">] abstract Item: key: string -> string with get, set

    type [<AllowNullLiteral>] ArgvImplies =
        [<Emit "$0[$1]{{=$2}}">] abstract Item: key: string -> U2<string, ResizeArray<string>> with get, set

    type [<AllowNullLiteral>] ArgvNargs =
        [<Emit "$0[$1]{{=$2}}">] abstract Item: key: string -> float with get, set

    type [<AllowNullLiteral>] ArgvUpdateLocaleObj =
        [<Emit "$0[$1]{{=$2}}">] abstract Item: key: string -> string with get, set

    type [<AllowNullLiteral>] ArgvUpdateStringsObj =
        [<Emit "$0[$1]{{=$2}}">] abstract Item: key: string -> string with get, set

    // type Arguments =
    //     Arguments<obj>

    type [<AllowNullLiteral>] Arguments<'T> =
        interface end

    type [<AllowNullLiteral>] RequireDirectoryOptions =
        /// Look for command modules in all subdirectories and apply them as a flattened (non-hierarchical) list.
        abstract recurse: bool option with get, set
        /// The types of files to look for when requiring command modules.
        abstract extensions: ReadonlyArray<string> option with get, set
        /// A synchronous function called for each command module encountered.
        /// Accepts `commandObject`, `pathToFile`, and `filename` as arguments.
        /// Returns `commandObject` to include the command; any falsy value to exclude/skip it.
        abstract visit: (obj option -> string -> string -> obj option) option with get, set
        /// Whitelist certain modules
        abstract ``include``: U2<RegExp, (string -> bool)> option with get, set
        /// Blacklist certain modules.
        abstract exclude: U2<RegExp, (string -> bool)> option with get, set

    type [<AllowNullLiteral>] Options =
        /// string or array of strings, alias(es) for the canonical option key, see `alias()`
        abstract alias: U2<string, ReadonlyArray<string>> option with get, set
        /// boolean, interpret option as an array, see `array()`
        abstract array: bool option with get, set
        /// boolean, interpret option as a boolean flag, see `boolean()`
        abstract boolean: bool option with get, set
        /// value or array of values, limit valid option arguments to a predefined set, see `choices()`
        abstract choices: Choices option with get, set
        /// function, coerce or transform parsed command line values into another value, see `coerce()`
        abstract coerce: (obj option -> obj option) option with get, set
        /// boolean, interpret option as a path to a JSON config file, see `config()`
        abstract config: bool option with get, set
        /// function, provide a custom config parsing function, see `config()`
        abstract configParser: (string -> obj) option with get, set
        /// string or object, require certain keys not to be set, see `conflicts()`
        abstract conflicts: U3<string, ReadonlyArray<string>, TypeLiteral_02> option with get, set
        /// boolean, interpret option as a count of boolean flags, see `count()`
        abstract count: bool option with get, set
        /// value, set a default value for the option, see `default()`
        abstract ``default``: obj option with get, set
        /// string, use this description for the default value in help content, see `default()`
        abstract defaultDescription: string option with get, set
        abstract demand: U2<bool, string> option with get, set
        /// boolean or string, demand the option be given, with optional error message, see `demandOption()`
        abstract demandOption: U2<bool, string> option with get, set
        /// string, the option description for help content, see `describe()`
        abstract desc: string option with get, set
        /// string, the option description for help content, see `describe()`
        abstract describe: string option with get, set
        /// string, the option description for help content, see `describe()`
        abstract description: string option with get, set
        /// boolean, indicate that this key should not be reset when a command is invoked, see `global()`
        abstract ``global``: bool option with get, set
        /// string, when displaying usage instructions place the option under an alternative group heading, see `group()`
        abstract group: string option with get, set
        /// don't display option in help output.
        abstract hidden: bool option with get, set
        /// string or object, require certain keys to be set, see `implies()`
        abstract implies: U3<string, ReadonlyArray<string>, TypeLiteral_02> option with get, set
        /// number, specify how many arguments should be consumed for the option, see `nargs()`
        abstract nargs: float option with get, set
        /// boolean, apply path.normalize() to the option, see `normalize()`
        abstract normalize: bool option with get, set
        /// boolean, interpret option as a number, `number()`
        abstract number: bool option with get, set
        abstract require: U2<bool, string> option with get, set
        abstract required: U2<bool, string> option with get, set
        /// boolean, require the option be specified with a value, see `requiresArg()`
        abstract requiresArg: bool option with get, set
        /// boolean, skips validation if the option is present, see `skipValidation()`
        abstract skipValidation: bool option with get, set
        /// boolean, interpret option as a string, see `string()`
        abstract string: bool option with get, set
        abstract ``type``: U3<string, string, PositionalOptionsType> option with get, set

    type [<AllowNullLiteral>] PositionalOptions =
        /// string or array of strings, see `alias()`
        abstract alias: U2<string, ReadonlyArray<string>> option with get, set
        /// value or array of values, limit valid option arguments to a predefined set, see `choices()`
        abstract choices: Choices option with get, set
        /// function, coerce or transform parsed command line values into another value, see `coerce()`
        abstract coerce: (obj option -> obj option) option with get, set
        /// string or object, require certain keys not to be set, see `conflicts()`
        abstract conflicts: U3<string, ReadonlyArray<string>, TypeLiteral_02> option with get, set
        /// value, set a default value for the option, see `default()`
        abstract ``default``: obj option with get, set
        /// string, the option description for help content, see `describe()`
        abstract desc: string option with get, set
        /// string, the option description for help content, see `describe()`
        abstract describe: string option with get, set
        /// string, the option description for help content, see `describe()`
        abstract description: string option with get, set
        /// string or object, require certain keys to be set, see `implies()`
        abstract implies: U3<string, ReadonlyArray<string>, TypeLiteral_02> option with get, set
        /// boolean, apply path.normalize() to the option, see normalize()
        abstract normalize: bool option with get, set
        abstract ``type``: PositionalOptionsType option with get, set

    type [<AllowNullLiteral>] Omit<'T, 'K> =
        interface end

    type [<AllowNullLiteral>] Defined<'T, 'K> =
        interface end

    // type ToArray<'T> =
    //     U2<Array<Exclude<'T, obj>>, Extract<'T, obj>>

    // type ToString<'T> =
    //     U2<obj, Extract<'T, obj>>

    // type ToNumber<'T> =
    //     U2<obj, Extract<'T, obj>>

    // type InferredOptionType<'O> =
    //     obj

    // type RequiredOptionType<'O> =
    //     obj

    type [<AllowNullLiteral>] InferredOptionTypes<'O> =
        interface end

    type CommandModule<'U> =
        CommandModule<obj, 'U>

    type CommandModule =
        CommandModule<obj, obj>

    type [<AllowNullLiteral>] CommandModule<'T, 'U> =
        /// array of strings (or a single string) representing aliases of `exports.command`, positional args defined in an alias are ignored
        abstract aliases: U2<ReadonlyArray<string>, string> option with get, set
        /// object declaring the options the command accepts, or a function accepting and returning a yargs instance
        abstract builder: CommandBuilder<'T, 'U> option with get, set
        /// string (or array of strings) that executes this command when given on the command line, first string may contain positional args
        abstract command: U2<ReadonlyArray<string>, string> option with get, set
        /// string used as the description for the command in help text, use `false` for a hidden command
        abstract describe: U2<string, obj> option with get, set
        /// a function which will be passed the parsed argv.
        abstract handler: (Arguments<'U> -> unit) with get, set

    type ParseCallback =
        ParseCallback<obj>

    type [<AllowNullLiteral>] ParseCallback<'T> =
        [<Emit "$0($1...)">] abstract Invoke: err: Error option * argv: Arguments<'T> * output: string -> unit

    type CommandBuilder<'U> =
        CommandBuilder<obj, 'U>

    type CommandBuilder =
        CommandBuilder<obj, obj>

    type CommandBuilder<'T, 'U> =
        U2<obj, (Argv<'T> -> Argv<'U>)>

    type [<AllowNullLiteral>] SyncCompletionFunction =
        [<Emit "$0($1...)">] abstract Invoke: current: string * argv: obj option -> ResizeArray<string>

    type [<AllowNullLiteral>] AsyncCompletionFunction =
        [<Emit "$0($1...)">] abstract Invoke: current: string * argv: obj option * ``done``: (ResizeArray<string> -> unit) -> unit

    type [<AllowNullLiteral>] PromiseCompletionFunction =
        [<Emit "$0($1...)">] abstract Invoke: current: string * argv: obj option -> Promise<ResizeArray<string>>

    type MiddlewareFunction =
        MiddlewareFunction<obj>

    type [<AllowNullLiteral>] MiddlewareFunction<'T> =
        [<Emit "$0($1...)">] abstract Invoke: args: Arguments<'T> -> unit

    type Choices =
        ReadonlyArray<U3<string, float, obj> option>

    type [<StringEnum>] [<RequireQualifiedAccess>] PositionalOptionsType =
        | Boolean
        | Number
        | String

    type [<AllowNullLiteral>] TypeLiteral_01 =
        [<Emit "$0[$1]{{=$2}}">] abstract Item: alias: string -> string with get, set

    type [<AllowNullLiteral>] TypeLiteral_02 =
        [<Emit "$0[$1]{{=$2}}">] abstract Item: key: string -> U2<string, ReadonlyArray<string>> with get, set

type [<AllowNullLiteral>] TypeLiteral_03 =
    [<Emit "$0[$1]{{=$2}}">] abstract Item: alias: string -> ResizeArray<string> with get, set

type [<AllowNullLiteral>] TypeLiteral_04 =
    [<Emit "$0[$1]{{=$2}}">] abstract Item: alias: string -> bool with get, set
