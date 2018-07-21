// ts2fable 0.0.0
module rec incorrentImportGenerated
open System
open Fable.Core
open Fable.Import.JS
open Fable.Import.Browser


module Monaco =
    let [<Import("editor","monaco")>] editor: Editor.IExports = jsNative

    module Editor =

        type [<AllowNullLiteral>] IExports =
            /// Create a new editor under `domElement`.
            /// `domElement` should be empty (not contain other dom nodes).
            /// The editor will read the size of `domElement`.
            abstract create: domElement: HTMLElement * ?options: IEditorConstructionOptions * ?``override``: IEditorOverrideServices -> IStandaloneCodeEditor
