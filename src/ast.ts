// import { ILinterOptions } from 'tslint/lib'
import 'byots'

function printAllChildren ( node: ts.Node, depth = 0 ) {
  console.log( new Array( depth + 1 ).join( '----' ),
    ts.SyntaxKind[node.kind], node.pos, node.end )
  depth++
  node.getChildren().forEach( c => printAllChildren( c, depth ) )
}

let sourceCode = ` var foo = 123; `.trim()
let sourceFile = ts.createSourceFile( './node_modules/@types/react/index.d.ts', sourceCode, ts.ScriptTarget.ES5, true )
printAllChildren( sourceFile )