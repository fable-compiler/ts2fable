export const templates = {
  file:
  `namespace Fable.Import
open System
open System.Text.RegularExpressions
open Fable.Core
open Fable.Import.JS

`,

  interface:
  `[TYPE_KEYWORD] [<AllowNullLiteral>] [DECORATOR][NAME][CONSTRUCTOR] =
`,

  enum:
  `[TYPE_KEYWORD] [DECORATOR][NAME] =
`,

  alias:
  `[TYPE_KEYWORD] [DECORATOR][NAME] =
`,

  classProperty:
  `[STATIC]member [INSTANCE][NAME] with get(): [TYPE][OPTION] = jsNative and set(v: [TYPE][OPTION]): unit = jsNative`,

  classMethod:
  `[STATIC][MEMBER_KEYWORD] [INSTANCE][NAME]([PARAMETERS]): [TYPE] = jsNative`,

  module:
  `module [NAME] =
`,

  moduleProxy:
  `type [IMPORT]Globals =
`,

  property:
  `abstract [NAME]: [TYPE][OPTION] with get, set`,

  method:
  `abstract [NAME]: [PARAMETERS] -> [TYPE]`,

  enumCase:
  `    | [NAME] = [ID]`
}