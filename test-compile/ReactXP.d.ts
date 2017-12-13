/// <reference types="react" />
/**
* ReactXP.ts
*
* Copyright (c) Microsoft Corporation. All rights reserved.
* Licensed under the MIT license.
*
* Wrapper for all ReactXP functionality. Users of ReactXP should import just this
* file instead of internals.
*/
import ImportRXInterfaces = require('../common/Interfaces');
import { App as AppType } from './App';

declare module moduleReactXP {

    type TypeWebView = RXInterfaces.WebView;
    var VarWebView: RXInterfaces.WebViewConstructor;
    export import ImportAnimated = AnimatedImpl;

}
export = ExportReactXP;
