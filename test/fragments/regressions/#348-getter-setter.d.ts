export declare class I {
    /** get,set */
    length1: number;
    /** get */
    readonly length2: number;
    /** get */
    get length3(): number;
    /** set */
    set length4(value: number);
    /** get,set */
    get length5(): number;
    set length5(value: number);
}

export declare class SKey {
    /** remove */
    private _name;
    constructor(name: string);
    /** get */
    get name(): string;
    /** get */
    get sName(): string;
    /** 
     * get
     * 
     * Returns the name in the format, {schemaName}.{name}.
     */
    get fullName(): string;
    /**
     * Checks whether this SKey matches the one provided.
     * @param rhs The SKey to compare to this.
     */
    matches(rhs: SKey): boolean;
    matchesFullName(name: string): boolean;
}