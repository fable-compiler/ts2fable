module rec Fable.Import.Mocha
open System
open Fable.Core
open Fable.Import.JS

type [<AllowNullLiteral>] IExports =
    abstract mocha: Mocha with get, set
    [<Global>] abstract describe: Mocha.IContextDefinition with get, set
    abstract xdescribe: Mocha.IContextDefinition with get, set
    abstract context: Mocha.IContextDefinition with get, set
    abstract suite: Mocha.IContextDefinition with get, set
    [<Global>] abstract it: Mocha.ITestDefinition with get, set
    [<Global>] abstract xit: Mocha.ITestDefinition with get, set
    abstract test: Mocha.ITestDefinition with get, set
    abstract specify: Mocha.ITestDefinition with get, set
    abstract run: unit -> unit
    abstract setup: callback: (Mocha.IBeforeAndAfterContext -> MochaDone -> obj) -> unit
    abstract teardown: callback: (Mocha.IBeforeAndAfterContext -> MochaDone -> obj) -> unit
    abstract suiteSetup: callback: (Mocha.IHookCallbackContext -> MochaDone -> obj) -> unit
    abstract suiteTeardown: callback: (Mocha.IHookCallbackContext -> MochaDone -> obj) -> unit
    abstract before: callback: (Mocha -> MochaDone -> obj) -> unit
    abstract before: description: string * callback: (Mocha -> MochaDone -> obj) -> unit
    abstract after: callback: (Mocha -> MochaDone -> obj) -> unit
    abstract after: description: string * callback: (Mocha -> MochaDone -> obj) -> unit
    abstract beforeEach: callback: (Mocha -> MochaDone -> obj) -> unit
    abstract beforeEach: description: string * callback: (Mocha -> MochaDone -> obj) -> unit
    abstract afterEach: callback: (Mocha -> MochaDone -> obj) -> unit
    abstract afterEach: description: string * callback: (Mocha -> MochaDone -> obj) -> unit
    abstract ReporterConstructor: ReporterConstructorStatic with get, set
    abstract Mocha: MochaStatic with get, set

type [<AllowNullLiteral>] MochaSetupOptions =
    abstract slow: float option with get, set
    abstract timeout: float option with get, set
    abstract ui: string option with get, set
    abstract globals: ResizeArray<obj> option with get, set
    abstract reporter: U2<string, ReporterConstructor> option with get, set
    abstract bail: bool option with get, set
    abstract ignoreLeaks: bool option with get, set
    abstract grep: obj option with get, set
    abstract require: ResizeArray<string> option with get, set

type [<AllowNullLiteral>] MochaDone =
    [<Emit "$0($1...)">] abstract Invoke: ?error: obj -> obj

type [<AllowNullLiteral>] ReporterConstructor =
    interface end

type [<AllowNullLiteral>] ReporterConstructorStatic =
    [<Emit "new $0($1...)">] abstract Create: runner: Mocha * options: obj -> ReporterConstructor

type [<AllowNullLiteral>] Mocha =
    abstract currentTest: Mocha with get, set
    abstract setup: options: MochaSetupOptions -> Mocha
    abstract bail: ?value: bool -> Mocha
    abstract addFile: file: string -> Mocha
    abstract reporter: name: string -> Mocha
    abstract reporter: reporter: ReporterConstructor -> Mocha
    abstract ui: value: string -> Mocha
    abstract grep: value: string -> Mocha
    abstract grep: value: RegExp -> Mocha
    abstract invert: unit -> Mocha
    abstract ignoreLeaks: value: bool -> Mocha
    abstract checkLeaks: unit -> Mocha
    abstract throwError: error: Error -> unit
    abstract growl: unit -> Mocha
    abstract globals: value: string -> Mocha
    abstract globals: values: ResizeArray<string> -> Mocha
    abstract useColors: value: bool -> Mocha
    abstract useInlineDiffs: value: bool -> Mocha
    abstract timeout: value: float -> Mocha
    abstract slow: value: float -> Mocha
    abstract enableTimeouts: value: bool -> Mocha
    abstract asyncOnly: value: bool -> Mocha
    abstract noHighlighting: value: bool -> Mocha
    abstract run: ?onComplete: (float -> unit) -> Mocha

type [<AllowNullLiteral>] MochaStatic =
    [<Emit "new $0($1...)">] abstract Create: ?options: obj -> Mocha

module Mocha =

    type [<AllowNullLiteral>] ISuiteCallbackContext =
        abstract timeout: ms: float -> ISuiteCallbackContext
        abstract retries: n: float -> ISuiteCallbackContext
        abstract slow: ms: float -> ISuiteCallbackContext

    type [<AllowNullLiteral>] IHookCallbackContext =
        abstract skip: unit -> IHookCallbackContext
        abstract timeout: ms: float -> IHookCallbackContext
        [<Emit "$0[$1]{{=$2}}">] abstract Item: index: string -> obj with get, set

    type [<AllowNullLiteral>] ITestCallbackContext =
        abstract skip: unit -> ITestCallbackContext
        abstract timeout: ms: float -> ITestCallbackContext
        abstract retries: n: float -> ITestCallbackContext
        abstract slow: ms: float -> ITestCallbackContext
        [<Emit "$0[$1]{{=$2}}">] abstract Item: index: string -> obj with get, set

    type [<AllowNullLiteral>] IRunnable =
        abstract title: string with get, set
        abstract fn: Function with get, set
        abstract async: bool with get, set
        abstract sync: bool with get, set
        abstract timedOut: bool with get, set
        abstract timeout: n: float -> IRunnable

    type [<AllowNullLiteral>] ISuite =
        abstract parent: ISuite with get, set
        abstract title: string with get, set
        abstract fullTitle: unit -> string

    type [<AllowNullLiteral>] ITest =
        inherit IRunnable
        abstract parent: ISuite with get, set
        abstract pending: bool with get, set
        abstract state: U2<string, string> option with get, set
        abstract fullTitle: unit -> string

    type [<AllowNullLiteral>] IBeforeAndAfterContext =
        inherit IHookCallbackContext
        abstract currentTest: ITest with get, set

    type [<AllowNullLiteral>] IRunner =
        interface end

    type [<AllowNullLiteral>] IContextDefinition =
        [<Emit "$0($1...)">] abstract Invoke: description: string * callback: (ISuiteCallbackContext -> unit) -> ISuite
        abstract only: description: string * callback: (ISuiteCallbackContext -> unit) -> ISuite
        abstract skip: description: string * callback: (ISuiteCallbackContext -> unit) -> unit
        abstract timeout: ms: float -> unit

    type [<AllowNullLiteral>] ITestDefinition =
        [<Emit "$0($1...)">] abstract Invoke: expectation: string * ?callback: (MochaDone -> obj) -> ITest
        abstract only: expectation: string * ?callback: (ITestCallbackContext -> MochaDone -> obj) -> ITest
        abstract skip: expectation: string * ?callback: (ITestCallbackContext -> MochaDone -> obj) -> unit
        abstract timeout: ms: float -> unit
        abstract state: U2<string, string> with get, set

    module reporters =

        type [<AllowNullLiteral>] IExports =
            abstract Base: BaseStatic with get, set
            abstract Progress: ProgressStatic with get, set
            abstract XUnit: XUnitStatic with get, set

        type [<AllowNullLiteral>] Base =
            abstract stats: obj with get, set

        type [<AllowNullLiteral>] BaseStatic =
            [<Emit "new $0($1...)">] abstract Create: runner: IRunner -> Base

        type [<AllowNullLiteral>] Doc =
            inherit Base

        type [<AllowNullLiteral>] Dot =
            inherit Base

        type [<AllowNullLiteral>] HTML =
            inherit Base

        type [<AllowNullLiteral>] HTMLCov =
            inherit Base

        type [<AllowNullLiteral>] JSON =
            inherit Base

        type [<AllowNullLiteral>] JSONCov =
            inherit Base

        type [<AllowNullLiteral>] JSONStream =
            inherit Base

        type [<AllowNullLiteral>] Landing =
            inherit Base

        type [<AllowNullLiteral>] List =
            inherit Base

        type [<AllowNullLiteral>] Markdown =
            inherit Base

        type [<AllowNullLiteral>] Min =
            inherit Base

        type [<AllowNullLiteral>] Nyan =
            inherit Base

        type [<AllowNullLiteral>] Progress =
            inherit Base

        type [<AllowNullLiteral>] ProgressStatic =
            [<Emit "new $0($1...)">] abstract Create: runner: IRunner * ?options: obj -> Progress

        type [<AllowNullLiteral>] Spec =
            inherit Base

        type [<AllowNullLiteral>] TAP =
            inherit Base

        type [<AllowNullLiteral>] XUnit =
            inherit Base

        type [<AllowNullLiteral>] XUnitStatic =
            [<Emit "new $0($1...)">] abstract Create: runner: IRunner * ?options: obj -> XUnit

// module mocha =
