export declare namespace ns {
  const node: {
    (arg: string): boolean
    (arg: string): boolean
  }

  /**
   * Source: https://github.com/microsoft/TypeScript/blob/e2868216f637e875a74c675845625eb15dcfe9a2/lib/typescript.d.ts#L7232-L7236
   */
  const createImportTypeNode: {
      (argument: TypeNode, assertions?: ImportTypeAssertionContainer | undefined, qualifier?: EntityName | undefined, typeArguments?: readonly TypeNode[] | undefined, isTypeOf?: boolean | undefined): ImportTypeNode;
      (argument: TypeNode, assertions?: ImportTypeAssertionContainer | undefined, qualifier?: EntityName | undefined, typeArguments?: readonly TypeNode[] | undefined, isTypeOf?: boolean | undefined): ImportTypeNode;
      (argument: TypeNode, qualifier?: EntityName | undefined, typeArguments?: readonly TypeNode[] | undefined, isTypeOf?: boolean | undefined): ImportTypeNode;
  };
}