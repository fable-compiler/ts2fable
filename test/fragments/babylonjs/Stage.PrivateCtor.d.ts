

declare module 'babylonjs' {
    export = BABYLON;
}

declare module BABYLON {
    // dummy
    interface ISceneComponent { }
}


declare module BABYLON {

    /** this was copy-pasted from babylonjs.
     * **********
     * Expected FS output: 'Stage' with 'registerStep' and 'clear', 'StageStatic' with the static Create method
     * **********
     * */
    class Stage<T extends Function> extends Array<{
        index: number;
        component: ISceneComponent;
        action: T;
    }> {
        /**
         * Hide ctor from the rest of the world.
         * @param items The items to add.
         */
        private constructor();
        /**
         * Creates a new Stage.
         * @returns A new instance of a Stage
         */
        static Create<T extends Function>(): Stage<T>;
        /**
         * Registers a step in an ordered way in the targeted stage.
         * @param index Defines the position to register the step in
         * @param component Defines the component attached to the step
         * @param action Defines the action to launch during the step
         */
        registerStep(index: number, component: ISceneComponent, action: T): void;
        /**
         * Clears all the steps from the stage.
         */
        clear(): void;
    }
}
