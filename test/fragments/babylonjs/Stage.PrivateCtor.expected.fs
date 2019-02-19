// ts2fable 0.0.0
module rec Stage.PrivateCtor
open System
open Fable.Core
open Fable.Import.JS

let [<Import("*","test")>] bABYLON: BABYLON.IExports = jsNative

module BABYLON =

    type [<AllowNullLiteral>] IExports =
        abstract Stage: StageStatic

    type [<AllowNullLiteral>] ISceneComponent =
        interface end

    /// this was copy-pasted from babylonjs.
    /// **********
    /// Expected FS output: 'Stage' with 'registerStep' and 'clear', 'StageStatic' with the static Create method
    /// **********
    type [<AllowNullLiteral>] Stage<'T> =
        inherit Array<TypeLiteral_01<'T>>
        /// <summary>Registers a step in an ordered way in the targeted stage.</summary>
        /// <param name="index">Defines the position to register the step in</param>
        /// <param name="component">Defines the component attached to the step</param>
        /// <param name="action">Defines the action to launch during the step</param>
        abstract registerStep: index: float * ``component``: ISceneComponent * action: 'T -> unit
        /// Clears all the steps from the stage.
        abstract clear: unit -> unit

    /// this was copy-pasted from babylonjs.
    /// **********
    /// Expected FS output: 'Stage' with 'registerStep' and 'clear', 'StageStatic' with the static Create method
    /// **********
    type [<AllowNullLiteral>] StageStatic =
        /// Creates a new Stage.
        abstract Create: unit -> Stage<'T>

    type [<AllowNullLiteral>] TypeLiteral_01<'T> =
        abstract index: float with get, set
        abstract ``component``: ISceneComponent with get, set
        abstract action: 'T with get, set
