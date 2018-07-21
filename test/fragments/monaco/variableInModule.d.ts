/*!-----------------------------------------------------------
 * Copyright (c) Microsoft Corporation. All rights reserved.
 * Type definitions for monaco-editor v0.10.1
 * Released under the MIT license
*-----------------------------------------------------------*/
/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Microsoft Corporation. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

declare module monaco {

    export var EditorType: {
        ICodeEditor: string;
        IDiffEditor: string;
    };

    export interface IDisposable {
        dispose(): void;
    }

    export interface IEvent<T> {
        (listener: (e: T) => any, thisArg?: any): IDisposable;
    }

}