

declare module 'babylonjs' {
    export = BABYLON;
}

declare module BABYLON {
    /** Alias type for value that can be null */
    type Nullable<T> = T | null;
}

// dummies
declare module BABYLON {
    interface Scene { }
    interface SceneLoaderProgressEvent { }
    interface AbstractMesh { }
    interface IParticleSystem { }
    interface Skeleton { }
    interface AnimationGroup { }
}


declare module BABYLON {

    class SceneLoader {

        /** Test:
         *  1) the callback 'onProgress' should not be Option<Option<_>>. Instead it should be an optional parameter of type function.
         *  2) the return value should be Promise<Interface>, not Promise<obj>. (Currently it generates 'TypeLiteral_01', would be great if it generated a better-named interface like 'SceneLoaderImportMeshAsyncReturn')
        */
        static ImportMeshAsync(meshNames: any, rootUrl: string, sceneFilename?: string, scene?: Nullable<Scene>, onProgress?: Nullable<(event: SceneLoaderProgressEvent) => void>, pluginExtension?: Nullable<string>): Promise<{
            meshes: AbstractMesh[];
            particleSystems: IParticleSystem[];
            skeletons: Skeleton[];
            animationGroups: AnimationGroup[];
        }>;
    }
}