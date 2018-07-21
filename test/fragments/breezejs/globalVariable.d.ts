export interface FilterQueryOp extends core.IEnum {
    Contains: FilterQueryOpSymbol;
    EndsWith: FilterQueryOpSymbol;
    Equals: FilterQueryOpSymbol;
    GreaterThan: FilterQueryOpSymbol;
    GreaterThanOrEqual: FilterQueryOpSymbol;
    IsTypeOf: FilterQueryOpSymbol;
    LessThan: FilterQueryOpSymbol;
    LessThanOrEqual: FilterQueryOpSymbol;
    NotEquals: FilterQueryOpSymbol;
    StartsWith: FilterQueryOpSymbol;
    Any: FilterQueryOpSymbol;
    All: FilterQueryOpSymbol;
}
export var FilterQueryOp: FilterQueryOp;
