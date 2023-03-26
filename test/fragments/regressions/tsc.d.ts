declare namespace ts {
    enum SyntaxKind {
        Unknown = 0,
        ObjectLiteralExpression = 207,
        JsxAttributes = 289,
    }

    interface CustomTransformers {
        // ...
        afterDeclarations?: (TransformerFactory<Bundle | SourceFile> | CustomTransformerFactory)[];
    }

    interface ObjectLiteralExpression extends ObjectLiteralExpressionBase<ObjectLiteralElementLike>, JSDocContainer {
        readonly kind: SyntaxKind.ObjectLiteralExpression;
    }

    interface JsxAttributes extends ObjectLiteralExpressionBase<JsxAttributeLike> {
        readonly kind: SyntaxKind.JsxAttributes;
        readonly parent: JsxOpeningLikeElement;
    }
}
