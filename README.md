# **ts2fable**  [![npm version](https://badge.fury.io/js/ts2fable.svg)](https://www.npmjs.com/package/ts2fable)

[Fable](https://github.com/fable-compiler/Fable) parser for [TypeScript declaration files](https://www.typescriptlang.org/docs/handbook/writing-declaration-files.html).
## Usage

Install it with yarn or npm. With yarn it is:
```
yarn global add ts2fable
```

With npm it is:
```
npm install -g ts2fable
```
Run the `ts2fable` command on a TypeScript file and also specify the F# output file. The F# namespace in taken from the output filename. In this example, it is `Yargs`.

```
yarn add @types/yargs --dev
ts2fable node_modules/@types/yargs/index.d.ts src/Yargs.fs
```

You can also use `--export`(or `-e`) option to collect from multiple `tsfiles` 

In below sample: All the **related ts files** in npm packages **uifabric** and **office-ui-fabric-react** will be compiled to `OfficeReact.fs` as a [bundle](https://github.com/fable-compiler/ts2fable-exports/blob/master/OfficeReact.fs)
```
ts2fable node_modules/office-ui-fabric-react/lib/index.d.ts test-compile/OfficeReact.fs -e uifabric office-ui-fabric-react
```

You can find more information about how to interact with JavaScript
from F# [here](https://fable.io/docs/communicate/js-from-fable.html).
Please note the parser is not perfect and some tweaking by hand may be needed. Please submit bugs as [issues on GitHub](https://github.com/fable-compiler/ts2fable/issues).

### Online Version
You can also try an in-browser version [here](http://fable.io/ts2fable/)

The online version will be updated automatically when commits is merged


## Contributing
Succesfull [builds](https://ci.appveyor.com/project/fable-compiler/ts2fable/history) on the master branch are uploaded and tagged as `next`. You can help us test these builds by installing them with:
```
yarn global add ts2fable@next
```

or build directly from source:
- `git clone https://github.com/fable-compiler/ts2fable`
- `cd ts2fable`
- Compile:
  - Windows: `./fake.cmd build`
  - Linux: `./fake.sh build`
- Run: 
  - `node -require esm ./build/cli/ts2fable.js Path/to/Declaration.d.ts Path/to/Output.fs`
  - or: `npm ts2fable Path/to/Declaration.d.ts Path/to/Output.fs`

`-require esm` is needed, because fable outputs code with ES modules.  
If you want to run ts2fable directly without esm (ECMAScript module loader): 
* build:  `./fake.cmd build -t Cli.BuildRelease`
* call: `node ./dist/ts2fable.js ...`


### Test Suites
* `./test`: Mocha tests
    * Run: `./fake.cmd build -t RunTest`
    * Watch: `./fake.cmd build -t WatchAndRunTest`
    * `functionTests.fs`: Test ts2fable functions
    * `fsFileTests.fs`: Test file translation using small snippets and compare output against expected results or content
* `./test-compile`: Translate actual TypeScript declaration files with ts2fable into F#. Then compile with F# compiler to ensure they are valid F#.
    * Run: `./fake.cmd build -t BuildTestCompile`
    * Setup for translation of .ts files: `./build.fsx` > Target `RunCliOnTestCompile`
    * Setup for .NET compilation: `./test-compile/test-compile.fsproj`

Run both test suites: `./fake.cmd build -t CliTest`

**Debug Test**:
* In VS Code: `Ctrl+Shift+P` > Run Task > WatchTest
* Add your test to `./test/fsFileTests.fs` and prefix with mocha `only` (See below sample)

Sample Test:
```fsharp
only "duplicated variable exports" <| fun _ ->
    let tsPaths = ["node_modules/reactxp/dist/web/ReactXP.d.ts"]
    let fsPath = "test-compile/ReactXP.fs"
    testFsFiles tsPaths fsPath  <| fun fsFiles ->
        fsFiles
        |> getTopVariables
        |> List.countBy(fun vb -> vb.Name)
        |> List.forall(fun (_,l) -> l = 1)
        |> equal true
```

* Press F5 (or launch `Run Mocha Tests`) to debug this test

### Web-App
* Start with: ` ./fake.cmd run -t WebApp.Watch`
* Launch in Browser: `localhost:8080`


## Conventions

Some JavaScript/TypeScript features have no direct translation to F#. Here is
a list of common workarounds adopted by the parser to solve these problems:

* **Erased unions**: TypeScript union types work differently from F# and its only
purpose is to specify the types allowed for a function argument. In F# they are
translated as _erased unions_: they're checked at compiled time but they'll be
removed from the generated JS code.

```fsharp
type CanvasRenderingContext2D =
    abstract fillStyle: U3<string, CanvasGradient, CanvasPattern> with get, set

let ctx: CanvasRenderingContext2D = failwith "dummy"
ctx.fillStyle <- U3.Case1 "#FF0000"
```

* **Constructor functions**: In JS any function can become a constructor just by
calling it with the `new` keyword. In the parsed files, interfaces with this
capability will have a `Create` method attached:

```fsharp
type CanvasRenderingContext2DType =
    abstract prototype: CanvasRenderingContext2D with get, set
    [<Emit("new $0($1...)")>] abstract Create: unit -> CanvasRenderingContext2D
```

* **Callable interfaces**: In the same way, JS functions are just objects which
means applying arguments directly to any object is legal in JS. To convey, the
parser attaches an `Invoke` method to callable interfaces:

```fsharp
type Express =
    inherit Application
    abstract version: string with get, set
    abstract application: obj with get, set
    [<Emit("$0($1...)")>] abstract Invoke: unit -> Application
```
