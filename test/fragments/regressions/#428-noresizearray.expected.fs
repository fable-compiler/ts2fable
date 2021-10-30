// ts2fable 0.0.0
module rec ``#428-noresizearray``
open System
open Fable.Core
open Fable.Core.JS


type [<AllowNullLiteral>] IExports =
    abstract Appwrite: AppwriteStatic

type [<AllowNullLiteral>] Appwrite =
    abstract account: AppwriteAccount with get, set

type [<AllowNullLiteral>] AppwriteStatic =
    [<EmitConstructor>] abstract Create: unit -> Appwrite

type [<AllowNullLiteral>] AppwriteAccount =
    abstract createDocument: string[] with get, set