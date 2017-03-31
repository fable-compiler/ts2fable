#!/usr/bin/env node

import * as fs from 'fs'
import 'byots' // This exposes a ts global usable from any imported file after this
import { templates } from './templates'
import { keywords } from './keywords'
import { reserved } from './reserved'

let typeCache = {}

let genReg = /<.+?>$/
let mappedTypes = {
  Date: 'DateTime',
  Object: 'obj',
  Array: 'ResizeArray',
  RegExp: 'Regex',
  String: 'string',
  Number: 'float'
}

function escape ( x: string ) {
  // HACK: ignore strings with a comment (* ... *), tuples ( * )
  // and union types arrays U2<string,float>[]
  if (typeof x !== 'string') { return x }
  if ( x && x.indexOf( '(*' ) >= 0 || x.indexOf( ' * ' ) >= 0 || /^U\d+<.*>$/.test( x ) ) {
    return x
  }
  let genParams = genReg.exec( x )
  let name = x.replace( genReg, '' )
  name = ( keywords.indexOf( name ) >= 0 || reserved.indexOf( name ) >= 0 || /[^\w.']/.test( name ) )
    ? '``' + name + '``'
    : name
  return name + ( genParams ? genParams[0] : '' )
}

function stringToUnionCase ( str: string ) {
  function upperFirstLetter ( str: string ) {
    return typeof str === 'string' && str.length > 1
      ? str[0].toUpperCase() + str.substr( 1 )
      : str
  }
  if ( str.length === 0 ) {
    return `[<CompiledName("")>] EmptyString`
  }
  if ( /^[A-Z]/.test( str ) ) {
    return `[<CompiledName("${str}")>] ${escape( str )}`
  } else {
    return escape( upperFirstLetter( str ) )
  }
}

function append ( template: string, txt: string ) {
  return typeof txt === 'string' && txt.length > 0 ? template + txt + '\n\n' : template
}

function joinPath ( path: string, name: string ) {
  return typeof path === 'string' && path.length > 0 ? path + '.' + name : name
}

function isDuplicate ( member, other ) {
  function arrayEquals ( ar1: any[] = [], ar2: any[] = [], f: (ar1: any, ar2: any) => boolean = () => false) {
    if ( ar1.length !== ar2.length ) {
      return false
    }
    for ( let i = 0; i < ar1.length; i++ ) {
      if ( !f( ar1[i], ar2[i] ) ) {
        return false
      }
    }
    return true
  }

  for ( let m of other ) {
    if ( m.name === member.name && arrayEquals( m.parameters, member.parameters, ( x, y ) => x.type === y.type ) ) {
      return true
    }
  }
  return false
}

function printParameters ( parameters, sep = ', ', def = '' ) {
  function printParameter ( x ) {
    if ( x.rest ) {
      let execed = /^ResizeArray<(.*?)>$/.exec( escape( x.type ) )
      let type = ( execed == null ? 'obj' : escape( execed[1] ) ) + '[]'
      return '[<ParamArray>] ' + escape( x.name ) + ': ' + type
    } else {
      return ( x.optional ? '?' : '' ) + escape( x.name ) + ': ' + escape( x.type )
    }
  }
  return Array.isArray( parameters ) && parameters.length > 0
    ? parameters.map( printParameter ).join( sep )
    : def
}

function printMethod ( prefix ) {
  return function ( x ) {
    return prefix + ( x.emit ? '[<Emit("' + x.emit + '")>] ' : '' ) + templates.method
      .replace( '[NAME]', escape( x.name ) )
      .replace( '[TYPE]', escape( x.type ) )
      .replace( '[PARAMETERS]', printParameters( x.parameters, ' * ', 'unit' ) )
  }
}

function printProperty ( prefix ) {
  return function ( x ) {
    let param = Array.isArray( x.parameters ) && x.parameters.length === 1
      ? printParameters( x.parameters ) + ' -> ' : ''
    return prefix + ( x.emit ? '[<Emit("' + x.emit + '")>] ' : '' ) + templates.property
      .replace( '[NAME]', escape( x.name ) )
      .replace( '[TYPE]', param + escape( x.type ) )
      .replace( '[OPTION]', x.optional ? ' option' : '' )
  }
}

function printParents ( prefix, node, template ) {
  function printParentMembers ( prefix, lines, parent, child ) {
    if ( child.kind === 'class' ) {
      lines.push( `${prefix}interface ${parent.name} with` )
      parent.properties.forEach( x => lines.push( printClassProperty( prefix + '    ' )( x ) ) )
      parent.methods.forEach( x => lines.push( printClassMethod( prefix + '    ' )( x ) ) )
    } else {
      parent.properties.forEach( x => lines.push( printProperty( prefix )( x ) ) )
      parent.methods.forEach( x => lines.push( printMethod( prefix )( x ) ) )
    }
    // Clean methods and properties from the child
    child.properties = child.properties.filter( x => !isDuplicate( x, parent.properties ) )
    child.methods = child.methods.filter( x => !isDuplicate( x, parent.methods ) )
  }

  let lines: string[] = []
  node.parents.forEach( function ( parentName ) {
    let nameNoArgs = parentName.replace( genReg, '' )
    let parent = typeCache[nameNoArgs.indexOf( '.' ) > 0 ? nameNoArgs : joinPath( node.path, nameNoArgs )]
    if ( node.kind === 'class' ) {
      if ( parent && parent.kind === 'class' ) {
        lines.push( prefix + 'inherit ' + parentName + '()' ) // TODO: Check base class constructor arguments?
      } else {
        if ( parent != null && ( parent.properties.length || parent.methods.length ) ) {
          printParentMembers( prefix, lines, parent, node )
        }
        else if ( parentName !== 'obj' ) {
          lines.push( prefix + 'interface ' + parentName )
        }
      }
    }
    else if ( node.kind === 'interface' ) {
      if ( parent && parent.kind === 'class' ) {
        // Interfaces cannot extend classes, just copy the members
        printParentMembers( prefix, lines, parent, node )
      }
      else if ( parentName !== 'obj' ) {
        lines.push( prefix + 'inherit ' + parentName )
      }
    }
  } )

  return template + ( lines.length ? lines.join( '\n' ) + '\n' : '' )
}

function printArray ( arr, mapper ) {
  return arr && arr.length > 0
    ? arr.map( mapper ).filter( x => x.length > 0 ).join( '\n' )
    : ''
}

function printMembers ( prefix, ent ) {
  return [
    printArray( ent.properties, printProperty( prefix ) ),
    printArray( ent.methods, printMethod( prefix ) )
  ].filter( x => x.length > 0 ).join( '\n' )
}

function printClassMethod ( prefix ) {
  return function ( x ) {
    return prefix + ( x.emit ? '[<Emit("' + x.emit + '")>] ' : '' ) + templates.classMethod
      .replace( '[STATIC]', x.static ? 'static ' : '' )
      .replace( '[MEMBER_KEYWORD]', 'member' )
      .replace( '[INSTANCE]', x.static ? '' : '__.' )
      .replace( '[NAME]', escape( x.name ) )
      .replace( '[TYPE]', escape( x.type ) )
      .replace( '[PARAMETERS]', printParameters( x.parameters ) )
  }
}

function printClassProperty ( prefix ) {
  return function ( x ) {
    return prefix + ( x.emit ? '[<Emit("' + x.emit + '")>] ' : '' ) + templates.classProperty
      .replace( '[STATIC]', x.static ? 'static ' : '' )
      .replace( '[INSTANCE]', x.static ? '' : '__.' )
      .replace( '[NAME]', escape( x.name.text ) )
      .replace( /\[TYPE\]/g, escape( x.type ) )
      .replace( /\[OPTION\]/g, x.optional ? ' option' : '' )
  }
}

function printClassMembers ( prefix, ent ) {
  return [
    printArray( ent.properties, printClassProperty( prefix ) ),
    printArray( ent.methods, printClassMethod( prefix ) )
  ].filter( x => x.length > 0 ).join( '\n' )
}

function printImport ( path, name ) {
  if ( !name ) {
    return '[<Erase>]'
  }
  else {
    let fullPath = joinPath( path, name.replace( genReg, '' ) )
    let period = fullPath.indexOf( '.' )
    let importPath = period >= 0
      ? fullPath.substr( period + 1 ) + '","' + fullPath.substr( 0, period )
      : '*","' + fullPath
    return `[<Import("${importPath}")>] `
  }
}

function printInterface ( prefix ) {
  function printDecorator ( ifc ) {
    switch ( ifc.kind ) {
      case 'class':
        return printImport( ifc.path, ifc.name )
      case 'stringEnum':
        return '[<StringEnum>] '
      default:
        return ''
    }
  }
  return function ( ifc, i ) {
    let isEnum = ifc.kind === 'enum' || ifc.kind === 'stringEnum'
    let isAlias = ifc.kind === 'alias'
    let template =
      isEnum ? templates.enum :
        isAlias ? templates.alias :
          templates.interface
    template =
      prefix + template
        .replace( '[TYPE_KEYWORD]', i === 0 ? 'type' : 'and' )
        .replace( '[NAME]', escape( ifc.name ) )
        .replace( '[DECORATOR]', printDecorator( ifc ) )
        .replace( '[CONSTRUCTOR]', ifc.kind === 'class'
          ? '(' + printParameters( ifc.constructorParameters ) + ')' : '' )

    let tmp = printParents( prefix + '    ', ifc, template )
    let hasParents = tmp !== template
    template = tmp

    switch ( ifc.kind ) {
      case 'alias':
        return template += prefix + '    ' + ifc.parents[0]
      case 'enum':
        return template + ifc.properties.map( function ( currentValue ) {
          let cv = templates.enumCase
            .replace( '[NAME]', currentValue.name )
            .replace( '[ID]', currentValue.value )
          return prefix + cv
        } ).join( '\n' )
      case 'stringEnum':
        return template + prefix + prefix + '| ' + ifc.properties.map( x =>
          stringToUnionCase( x.name ) ).join( ' | ' )
      case 'class':
        let classMembers = printClassMembers( prefix + '    ', ifc )
        return template += ( classMembers.length === 0 && !hasParents
          ? prefix + '    class end'
          : classMembers )
      // case "interface":
      default:
        let members = printMembers( prefix + '    ', ifc )
        return template += ( members.length === 0 && !hasParents
          ? prefix + '    interface end'
          : members )

    }
  }
}

function printGlobals ( prefix, ent ) {
  let members = printClassMembers( prefix + '    ' + ( ent.name ? '' : '[<Global>] ' ), ent )
  if ( members.length > 0 ) {
    return prefix + templates.moduleProxy.replace(
      '[IMPORT]', printImport( ent.path, ent.name ) ) + members
  }
  return ''
}

function printModule ( prefix ) {
  return function ( mod ) {
    let template = prefix + templates.module
      .replace( '[NAME]', escape( mod.name ) )

    template = append( template, mod.interfaces.map(
      printInterface( prefix + '    ' ) ).join( '\n\n' ) )

    template = append( template, printGlobals( prefix + '    ', mod ) )

    template += mod.modules.map( printModule( prefix + '    ' ) ).join( '\n\n' )

    return template
  }
}

function printFile ( file ) {
  let template = templates.file
  template = append( template, file.interfaces.map( printInterface( '' ) ).join( '\n\n' ) )
  template = append( template, printGlobals( '', file ) )
  return template + file.modules.map( printModule( '' ) ).join( '\n\n' )
}

function hasFlag ( flags, flag ) {
  return flags != null && ( flags & flag ) === flag
}

function getName ( node: any ) {
  if ( node.expression && node.expression.kind === ts.SyntaxKind.PropertyAccessExpression ) {
    return node.expression.expression.text + '.' + node.expression.name.text
  }
  else {
    // TODO: Throw exception if there's no name?
    return node.name ? node.name.text : ( node.expression ? node.expression.text : null )
  }
}

function printTypeArguments ( typeArgs ) {
  typeArgs = typeArgs || []
  return typeArgs.length === 0 ? '' : '<' + typeArgs.map( getType ).join( ', ' ) + '>'
}

function findTypeParameters ( node, acc: string[] = [] ) {
  if ( !node ) {
    return acc
  }
  if ( Array.isArray( node.typeParameters ) ) {
    node.typeParameters.forEach( x => acc.push( x.name.text ) )
  }
  return findTypeParameters( node.parent, acc )
}

function getType ( type: ts.TypeNode ): string {
  if ( !type ) {
    return 'obj'
  }
  let typeParameters = findTypeParameters( type )
  switch ( type.kind ) {
    case ts.SyntaxKind.StringKeyword:
      return 'string'
    case ts.SyntaxKind.NumberKeyword:
      return 'float'
    case ts.SyntaxKind.BooleanKeyword:
      return 'bool'
    case ts.SyntaxKind.VoidKeyword:
      return 'unit'
    case ts.SyntaxKind.SymbolKeyword:
      return 'Symbol'
    case ts.SyntaxKind.ArrayType:
      return 'ResizeArray<' + getType(( <ts.ArrayTypeNode> type ).elementType ) + '>'
    case ts.SyntaxKind.FunctionType:
      let cbParams = ( <ts.FunctionTypeNode> type ).parameters.map( function ( x ) {
        return x.dotDotDotToken ? 'obj' : getType( x.type || ( <any> x ).type )
      } ).join( ', ' )
      const typeToGet = ( <ts.FunctionTypeNode> type ).type || ( <any> type )
      return 'Func<' + ( cbParams || 'unit' ) + ', ' + getType( typeToGet ) + '>'
    case ts.SyntaxKind.UnionType:
      if ( ( <ts.UnionTypeNode> type ).types && ( <ts.UnionTypeNode> type ).types[0].kind === ts.SyntaxKind.StringLiteral ) {
        return '(* TODO StringEnum ' + ( <ts.UnionTypeNode> type ).types.map( x => x.getText ).join( ' | ' ) + ' *) string'
      }
      if ( ( <ts.UnionTypeNode> type ).types.length <= 4 ) {
        return 'U' + ( <ts.UnionTypeNode> type ).types.length + printTypeArguments(( <ts.UnionTypeNode> type ).types )
      } else {
        return 'obj'
      }
    case ts.SyntaxKind.TupleType:
      return ( <ts.TupleTypeNode> type ).elementTypes.map( getType ).join( ' * ' )
    case ts.SyntaxKind.ParenthesizedType:
      return getType(( <ts.ParenthesizedTypeNode> type ).type )
    // case ts.SyntaxKind.TypeQuery:
    //     return type.exprName.text + "Constructor";

    default:
      let anyType: any = type
      let name = ( <any> type ).typeName ? anyType.typeName.text : ( anyType.expression ? anyType.expression.text : null )
      if ( anyType.expression && anyType.expression.kind === ts.SyntaxKind.PropertyAccessExpression ) {
        name = anyType.expression.expression.text + '.' + anyType.expression.name.text
      }
      if ( anyType.typeName && anyType.typeName.left && anyType.typeName.right ) {
        name = anyType.typeName.left.text + '.' + anyType.typeName.right.text
      }

      if ( !name ) {
        return 'obj'
      }
      if ( name in mappedTypes ) {
        name = mappedTypes[name]
      }

      // console.error( `UNSUPPORTED: ${ts.SyntaxKind[type.kind]}` )
      let result = name + printTypeArguments( anyType.typeArguments )
      return ( typeParameters.indexOf( result ) > -1 ? "'" : '' ) + result
  }
}

function getParents ( node ) {
  let parents: string[] = []
  if ( Array.isArray( node.heritageClauses ) ) {
    for ( let i = 0; i < node.heritageClauses.length; i++ ) {
      let types = node.heritageClauses[i].types
      for ( let j = 0; j < types.length; j++ ) {
        parents.push( getType( types[j] || ( <any> types )[j] ) )
      }
    }
  }
  return parents
}

// TODO: get comments
// TODO: Find out what to replace ts.NodeFlags.Static with
function getProperty ( node: ts.TypeNode, opts: PropertyOptions = {} ) {
  return {
    name: opts.name || getName( node ),
    type: getType( node ),
    optional: node.transformFlags != null,
    static: node.kind ? hasFlag( node.flags, ts.SyntaxKind.StaticKeyword ) : false
  }
}

function getStringEnum ( node ) {
  return {
    kind: 'stringEnum',
    name: getName( node ),
    properties: node.type.types.map( function ( n ) {
      return { name: n.text }
    } ),
    parents: [],
    methods: []
  }
}

function getEnum ( node ) {
  return {
    kind: 'enum',
    name: getName( node ),
    properties: node.members.map( function ( n, i ) {
      return {
        name: getName( n ),
        value: n.initializer ? n.initializer.text : i
      }
    } ),
    parents: [],
    methods: []
  }
}

// TODO: Check if it's const
// I replaced declaration list with locals.
function getVariables ( node: ts.VariableStatement ) {
  let variables: any[] = []
  let anonymousTypes: any[] = []
  let name
  let type
  let declarationList = Array.isArray( node.declarationList ) ? node.declarationList : [node.declarationList]
  declarationList.forEach( dList => {
    let declarations = dList.declarations
    declarations.forEach( dec => {
      name = dec.name
      if ( <any> dec.kind === ts.SyntaxKind.TypeLiteral ) {
        type = visitInterface( dec, { name: name + 'Type', anonymous: true } )
        anonymousTypes.push( type )
        type = type.name
      } else {
        type = getType( dec.type || ( <any> dec ).type )
      }
      variables.push( {
        name: name,
        type: type,
        static: true,
        parameters: []
      } )
    } )
  } )
  return {
    variables: variables,
    anonymousTypes: anonymousTypes
  }
}

function getParameter ( param: ts.ParameterDeclaration ) {
  if ( param.type ) {
    return {
      name: param.name.toString(),
      type: getType( param.type ),
      optional: param.questionToken != null,
      rest: param.dotDotDotToken != null
    }
  }
}

// TODO: get comments
function getMethod ( node: ts.FunctionTypeNode, opts: PropertyOptions = {} ) {

  let meth: any = {
    name: opts.name || getName( node ),
    type: getType(( <ts.FunctionTypeNode> node ) ),
    optional: node.flags['Optional'] != null,
    parameters: node.parameters.map( getParameter ),
    static: opts.static || ( node.id && hasFlag( node.modifiers, ts.ModifierFlags.Static ) || ( node.modifiers && hasFlag( node.modifiers, ts.ModifierFlags.Static ) ) )
  }
  let firstParam: any = node.parameters[0]
  let secondParam: any = node.parameters[1]

  if ( secondParam && secondParam.type && secondParam.type.kind === ts.SyntaxKind.StringLiteral ) {
    // The only case I've seen following this pattern is
    // createElementNS(namespaceURI: "http://www.w3.org/2000/svg", qualifiedName: "a"): SVGAElement
    meth.parameters = meth !== undefined ? meth.parameters.slice( 2 ) : null
    meth.emit = `$0.${meth.name}('${firstParam.type.getText()}', '${secondParam.type.getText()}'${meth.parameters.length ? ',$1...' : ''})`
    meth.name += '_' + secondParam.type.getText()
  }
  else if ( firstParam && firstParam.type && firstParam.type.kind === ts.SyntaxKind.StringLiteral ) {
    meth.parameters = meth.parameters.slice( 1 )
    meth.emit = `$0.${meth.name}('${firstParam.type.getText()}'${meth.parameters.length ? ',$1...' : ''})`
    meth.name += '_' + firstParam.type.getText()
  }
  return meth
}

function getInterface ( node, opts ) {
  function printTypeParameters ( typeParams ) {
    typeParams = typeParams || []
    return typeParams.length === 0 ? '' : '<' + typeParams.map( function ( x ) {
      return "'" + x.name.text
    } ).join( ', ' ) + '>'
  }
  opts = opts || {}
  let ifc = {
    name: opts.name || ( getName( node ) + printTypeParameters( node.typeParameters ) ),
    kind: opts.kind || 'interface',
    parents: opts.kind === 'alias' ? [getType( node.type )] : getParents( node ),
    properties: [],
    methods: [],
    path: opts.path
  }
  if ( !opts.anonymous ) {
    typeCache[joinPath( ifc.path, ifc.name.replace( genReg, '' ) )] = ifc
  }
  return ifc
}

function mergeNamesakes ( xs, getName, mergeTwo ) {
  let grouped = {}
  xs.forEach( function ( x ) {
    let name = getName( x )
    if ( !Array.isArray( grouped[name] ) ) {
      grouped[name] = []
    }
    grouped[name].push( x )
  } )

  return Object.keys( grouped ).map( function ( k ) { return grouped[k].reduce( mergeTwo ) } )
}

function mergeInterfaces ( a, b ) {
  return {
    name: a.name,
    kind: a.kind,
    parents: a.parents,
    path: a.path,
    properties: a.properties.concat( b.properties ),
    methods: a.methods.concat( b.methods )
  }
}

function mergeNamesakeInterfaces ( intfs ) {
  return mergeNamesakes( intfs, function ( i ) { return i.name }, mergeInterfaces )
}

function visitInterface ( node, opts ) {
  let ifc: any = getInterface( node, opts );
  ( node.members || [] ).forEach( function ( node ) {
    let member
    let name
    switch ( node.kind ) {
      case ts.SyntaxKind.PropertySignature:
      case ts.SyntaxKind.PropertyDeclaration:
        if ( node.name.kind === ts.SyntaxKind.ComputedPropertyName ) {
          name = getName( node.name )
          member = getProperty( node, { name: '[' + name + ']' } )
          member.emit = '$0[' + name + ']{{=$1}}'
        }
        else {
          member = getProperty( node )
        }
        ifc.properties.push( member )
        break
      // TODO: If interface only contains one `Invoke` method
      // make it an alias of Func
      case ts.SyntaxKind.CallSignature:
        member = getMethod( node, { name: 'Invoke' } )
        member.emit = '$0($1...)'
        ifc.methods.push( member )
        break
      case ts.SyntaxKind.MethodSignature:
      case ts.SyntaxKind.MethodDeclaration:
        if ( node.name.kind === ts.SyntaxKind.ComputedPropertyName ) {
          name = getName( node.name )
          member = getMethod( node, { name: '[' + name + ']' } )
          member.emit = '$0[' + name + ']($1...)'
        }
        else {
          member = getMethod( node )
        }
        // Sometimes TypeScript definitions contain duplicated methods
        if ( !isDuplicate( member, ifc.methods ) ) {
          ifc.methods.push( member )
        }
        break
      case ts.SyntaxKind.ConstructSignature:
        member = getMethod( node, { name: 'Create' } )
        member.emit = 'new $0($1...)'
        ifc.methods.push( member )
        break
      case ts.SyntaxKind.IndexSignature:
        member = getMethod( node, { name: 'Item' } )
        member.emit = '$0[$1]{{=$2}}'
        ifc.properties.push( member )
        break
      case ts.SyntaxKind.Constructor:
        ifc.constructorParameters = node.parameters.map( getParameter )
        break
    }
  } )
  return ifc
}

function mergeModules ( a, b ) {
  return {
    name: a.name,
    path: a.path,
    interfaces: mergeNamesakeInterfaces( a.interfaces.concat( b.interfaces ) ),
    properties: a.properties.concat( b.properties ),
    methods: a.methods.concat( b.methods ),
    modules: mergeNamesakeModules( a.modules.concat( b.modules ) )
  }
}

function mergeNamesakeModules ( modules ) {
  return mergeNamesakes( modules, function ( m ) { return m.name }, mergeModules )
}

function visitModuleNode ( mod, modPath ) {
  return function ( node ) {
    switch ( node.kind ) {
      case ts.SyntaxKind.InterfaceDeclaration:
        mod.interfaces.push( visitInterface( node, { kind: 'interface', path: modPath } ) )
        break
      case ts.SyntaxKind.ClassDeclaration:
        mod.interfaces.push( visitInterface( node, { kind: 'class', path: modPath } ) )
        break
      case ts.SyntaxKind.TypeAliasDeclaration:
        if ( node.type.types && node.type.types[0].kind === ts.SyntaxKind.StringLiteral ) {
          mod.interfaces.push( getStringEnum( node ) )
        }
        else {
          mod.interfaces.push( visitInterface( node, { kind: 'alias', path: modPath } ) )
        }
        break
      case ts.SyntaxKind.VariableStatement:
        let varsAndTypes = getVariables( node )
        varsAndTypes.variables.forEach( x => mod.properties.push( x ) )
        varsAndTypes.anonymousTypes.forEach( x => mod.interfaces.push( x ) )
        break
      case ts.SyntaxKind.FunctionDeclaration:
        mod.methods.push( getMethod( node, { static: true } ) )
        break
      case ts.SyntaxKind.ModuleDeclaration:
        let m = visitModule( node, { path: modPath } )
        let isEmpty = Object.keys( m ).every( function ( k ) { return !Array.isArray( m[k] ) || m[k].length === 0 } )
        if ( !isEmpty ) {
          mod.modules.push( m )
        }
        break
      case ts.SyntaxKind.EnumDeclaration:
        mod.interfaces.push( getEnum( node ) )
        break
    }
  }
}

function visitModule ( node, opts ) {
  opts = opts || {}
  let mod: any = {
    name: getName( node ),
    path: opts.path,
    interfaces: [],
    properties: [],
    methods: [],
    modules: []
  }
  let modPath = joinPath( mod.path, mod.name )

  switch ( node.body.kind ) {
    case ts.SyntaxKind.ModuleDeclaration:
      mod.modules.push( visitModule(( <ts.ModuleDeclaration> node ).body, { path: modPath } ) )
      break

    case ts.SyntaxKind.ModuleBlock:
      node.body.statements.forEach( visitModuleNode( mod, modPath ) )
      break
  }

  return mod
}

function visitFile ( node ) {
  let file = {
    interfaces: [],
    properties: [],
    methods: [],
    modules: []
  }

  ts.forEachChild( node, visitModuleNode( file, null ) )

  return {
    properties: file.properties,
    interfaces: file.interfaces,
    methods: file.methods,
    modules: mergeNamesakeModules( file.modules )
  }
}

try {
  let filePath = process.argv[2]
  if ( filePath == null ) {
    throw 'Please provide the path to a TypeScript definition file'
  }
  // fileName = (fileName = path.basename(filePath).replace(".d.ts",""), fileName[0].toUpperCase() + fileName.substr(1));
  // `readonly` keyword is causing problems, remove it
  let code = fs.readFileSync( filePath ).toString().replace( /readonly/g, '' )
  let sourceFile = ts.createSourceFile( filePath, code, ts.ScriptTarget.ES2015, /*setParentNodes*/ true )
  let fileInfo = visitFile( sourceFile )
  let ffi = printFile( fileInfo )
  console.log( ffi )
  process.exit( 0 )
}
catch ( err ) {
  console.log( 'ERROR: ' + err )
  process.exit( 1 )
}

interface PropertyOptions {
  name?: string
  type?: string
  optional?: boolean
  static?: boolean
  parameters?: any[]
  emit?: any
}
