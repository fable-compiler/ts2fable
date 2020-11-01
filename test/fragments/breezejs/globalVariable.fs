// ts2fable 0.0.0
module rec globalVariable
open System
open Fable.Core
open Fable.Core.JS

let [<Import("FilterQueryOp","globalVariable")>] FilterQueryOp: FilterQueryOp = jsNative

type [<AllowNullLiteral>] FilterQueryOp =
    inherit Core.IEnum
    abstract Contains: FilterQueryOpSymbol with get, set
    abstract EndsWith: FilterQueryOpSymbol with get, set
    abstract Equals: FilterQueryOpSymbol with get, set
    abstract GreaterThan: FilterQueryOpSymbol with get, set
    abstract GreaterThanOrEqual: FilterQueryOpSymbol with get, set
    abstract IsTypeOf: FilterQueryOpSymbol with get, set
    abstract LessThan: FilterQueryOpSymbol with get, set
    abstract LessThanOrEqual: FilterQueryOpSymbol with get, set
    abstract NotEquals: FilterQueryOpSymbol with get, set
    abstract StartsWith: FilterQueryOpSymbol with get, set
    abstract Any: FilterQueryOpSymbol with get, set
    abstract All: FilterQueryOpSymbol with get, set
