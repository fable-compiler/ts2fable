/**
* ReactXP.ts
*
* Copyright (c) Microsoft Corporation. All rights reserved.
* Licensed under the MIT license.
*
* Wrapper for all ReactXP functionality. Users of ReactXP should import just this
* file instead of internals.
*/

import React = require('react');
declare module ReactXP {
    export import createElement = React.createElement;
    export import Children = React.Children;
    var __spread: any;
}
export = ReactXP;
