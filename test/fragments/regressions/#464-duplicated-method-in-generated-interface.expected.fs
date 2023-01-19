// ts2fable 0.0.0
module rec ``#464-duplicated-method-in-generated-interface``

#nowarn "3390" // disable warnings for invalid XML comments

open System
open Fable.Core
open Fable.Core.JS


module Ns =

    type [<AllowNullLiteral>] IExports =
        abstract node: IExportsNode
        /// <summary>Source: <see href="https://github.com/microsoft/TypeScript/blob/e2868216f637e875a74c675845625eb15dcfe9a2/lib/typescript.d.ts#L7232-L7236" /></summary>
        abstract createImportTypeNode: IExportsCreateImportTypeNode

    type [<AllowNullLiteral>] IExportsNode =
        [<Emit("$0($1...)")>] abstract Invoke: arg: string -> bool

    type [<AllowNullLiteral>] IExportsCreateImportTypeNode =
        [<Emit("$0($1...)")>] abstract Invoke: argument: TypeNode * ?assertions: ImportTypeAssertionContainer * ?qualifier: EntityName * ?typeArguments: ResizeArray<TypeNode> * ?isTypeOf: bool -> ImportTypeNode
        [<Emit("$0($1...)")>] abstract Invoke: argument: TypeNode * ?qualifier: EntityName * ?typeArguments: ResizeArray<TypeNode> * ?isTypeOf: bool -> ImportTypeNode
