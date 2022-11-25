export interface StringLiteral {
  readonly kind: string
}
export const createStringLiteral: {
    (text: string, isSingleQuote?: boolean | undefined): StringLiteral;
    (text: string, isSingleQuote?: boolean | undefined, hasExtendedUnicodeEscape?: boolean | undefined): StringLiteral;
};