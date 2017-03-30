"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.templates = {
    file: "namespace Fable.Import\nopen System\nopen System.Text.RegularExpressions\nopen Fable.Core\nopen Fable.Import.JS\n\n",
    interface: "[TYPE_KEYWORD] [<AllowNullLiteral>] [DECORATOR][NAME][CONSTRUCTOR] =\n",
    enum: "[TYPE_KEYWORD] [DECORATOR][NAME] =\n",
    alias: "[TYPE_KEYWORD] [DECORATOR][NAME] =\n",
    classProperty: "[STATIC]member [INSTANCE][NAME] with get(): [TYPE][OPTION] = jsNative and set(v: [TYPE][OPTION]): unit = jsNative",
    classMethod: "[STATIC][MEMBER_KEYWORD] [INSTANCE][NAME]([PARAMETERS]): [TYPE] = jsNative",
    module: "module [NAME] =\n",
    moduleProxy: "type [IMPORT]Globals =\n",
    property: "abstract [NAME]: [TYPE][OPTION] with get, set",
    method: "abstract [NAME]: [PARAMETERS] -> [TYPE]",
    enumCase: "    | [NAME] = [ID]"
};
//# sourceMappingURL=templates.js.map