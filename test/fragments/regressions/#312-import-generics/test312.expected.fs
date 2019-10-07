// ts2fable 0.0.0
module rec test312.actual
open System
open Fable.Core
open Fable.Import.JS

type IGenericInterface<'T> = __core_other.IGenericInterface<'T>

type [<AllowNullLiteral>] IExports =
    abstract f: a: IGenericInterface<float> * b: IGenericInterfaceAlias<float> -> unit

type IGenericInterfaceAlias<'T> =
    IGenericInterface<'T>

module __core_other =

    type [<AllowNullLiteral>] IGenericInterface<'T> =
        abstract value: 'T with get, set
