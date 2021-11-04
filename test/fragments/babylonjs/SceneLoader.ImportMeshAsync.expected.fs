// ts2fable 0.0.0
module rec SceneLoader.ImportMeshAsync
open System
open Fable.Core
open Fable.Core.JS

let [<ImportAll("SceneLoader.ImportMeshAsync")>] babylon: BABYLON.IExports = jsNative

module BABYLON =

    type [<AllowNullLiteral>] IExports =
        abstract SceneLoader: SceneLoaderStatic

    /// Alias type for value that can be null
    type Nullable<'T> =
        'T option

    type [<AllowNullLiteral>] Scene =
        interface end

    type [<AllowNullLiteral>] SceneLoaderProgressEvent =
        interface end

    type [<AllowNullLiteral>] AbstractMesh =
        interface end

    type [<AllowNullLiteral>] IParticleSystem =
        interface end

    type [<AllowNullLiteral>] Skeleton =
        interface end

    type [<AllowNullLiteral>] AnimationGroup =
        interface end

    type [<AllowNullLiteral>] SceneLoader =
        interface end

    type [<AllowNullLiteral>] SceneLoaderStatic =
        [<EmitConstructor>] abstract Create: unit -> SceneLoader
        /// Test:
        /// 1) the callback 'onProgress' should not be Option<Option<_>>. Instead it should be an optional parameter of type function.
        /// 2) the return value should be Promise<Interface>, not Promise<obj>. (Currently it generates 'TypeLiteral_01', would be great if it generated a better-named interface like 'SceneLoaderImportMeshAsyncReturn')
        abstract ImportMeshAsync: meshNames: obj option * rootUrl: string * ?sceneFilename: string * ?scene: Scene * ?onProgress: (SceneLoaderProgressEvent -> unit) * ?pluginExtension: string -> Promise<SceneLoaderStaticImportMeshAsyncPromise>

    type [<AllowNullLiteral>] SceneLoaderStaticImportMeshAsyncPromise =
        abstract meshes: ResizeArray<AbstractMesh> with get, set
        abstract particleSystems: ResizeArray<IParticleSystem> with get, set
        abstract skeletons: ResizeArray<Skeleton> with get, set
        abstract animationGroups: ResizeArray<AnimationGroup> with get, set
