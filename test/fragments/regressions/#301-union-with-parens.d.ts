/** everything must be `string option` */
export interface I {
    fReturn1(): string | null
    fReturn2(): (string) | null
    fReturn3(): string | (null)
    fReturn4(): (string) | (null)

    fParam1(v: string | null): void
    fParam2(v: (string) | null): void
    fParam3(v: string | (null)): void
    fParam4(v: (string) | (null)): void
}