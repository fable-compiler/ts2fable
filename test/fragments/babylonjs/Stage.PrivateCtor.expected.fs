// ts2fable 0.0.0
module rec Stage.PrivateCtor

#nowarn "3390" // disable warnings for invalid XML comments

open System
open Fable.Core
open Fable.Core.JS

type Array<'T> = System.Collections.Generic.IList<'T>

let [<Import("*","Stage.PrivateCtor")>] babylon: BABYLON.IExports = jsNative

module BABYLON =

    type [<AllowNullLiteral>] IExports =
        /// this was copy-pasted from babylonjs.
        /// **********
        /// Expected FS output: 'Stage' with 'registerStep' and 'clear', 'StageStatic' with the static Create method
        /// **********
        abstract Stage: StageStatic

    type [<AllowNullLiteral>] ISceneComponent =
        interface end

    /// this was copy-pasted from babylonjs.
    /// **********
    /// Expected FS output: 'Stage' with 'registerStep' and 'clear', 'StageStatic' with the static Create method
    /// **********
    type [<AllowNullLiteral>] Stage<'T> =
        inherit Array<StageArray<'T>>
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
        /// <summary>Creates a new Stage.</summary>
        /// <returns>A new instance of a Stage</returns>
        abstract Create: unit -> Stage<'T>

    type [<AllowNullLiteral>] StageArray<'T> =
        abstract index: float with get, set
        abstract ``component``: ISceneComponent with get, set
        abstract action: 'T with get, set
