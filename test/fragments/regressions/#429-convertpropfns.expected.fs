// ts2fable 0.8.0
module rec ``#429-convertpropfns``
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
    abstract createDocument: name: string -> Document