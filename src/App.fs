module FableApp

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Fable.Import.Node
open Fable.Import.ts

let makeFactorialFunction() =
    let functionName = ts.createIdentifier "factorial"
    let paramName = ts.createIdentifier "n"
    
    let parameter = 
        ts.createParameter(
            None, // decorators
            None, // modifiers
            None, // dotDotDotToken
            paramName |> BindingName.Case1 |> U2.Case2);

    let condition =
        ts.createBinary(
            paramName,
            SyntaxKind.LessThanEqualsToken,
            ts.createLiteral(1))

    let ifBody =
        ts.createBlock(
            [| ts.createReturn(ts.createLiteral(1)) |],
            true) // multiline
    let decrementedArg = ts.createBinary(paramName, SyntaxKind.MinusToken, ts.createLiteral(1))

    let recurse =
        ts.createBinary(
            paramName,
            SyntaxKind.AsteriskToken,
            ts.createCall(functionName, None, [| decrementedArg |])); // typeArgs
    let statements: Statement array = 
        [|
            ts.createIf(condition, ifBody)
            ts.createReturn(recurse)
        |]

    ts.createFunctionDeclaration(
        None, // decorators
        Some [| ts.createToken(SyntaxKind.ExportKeyword) |], // modifiers
        None, // asteriskToken
        functionName |> U2.Case2 |> Some,
        None, // typeParameters
        [| parameter |],
        ts.createKeywordTypeNode(SyntaxKind.NumberKeyword) :> TypeNode |> Some, // returnType
        ts.createBlock(statements, true) |> Some // multiline
    )

let resultFile = 
    ts.createSourceFile("someFileName.ts", "", ScriptTarget.Latest, false, ScriptKind.TS); // setParentNodes

let printerOptions = createEmpty<PrinterOptions>
printerOptions.newLine <- Some NewLineKind.LineFeed

let printer = ts.createPrinter(printerOptions);

let result = printer.printNode(EmitHint.Unspecified, makeFactorialFunction(), resultFile);

printfn "%s" result