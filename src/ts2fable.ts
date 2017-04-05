#!/usr/bin/env node

import * as fs from 'fs'
import 'byots' // This exposes a ts global usable from any imported file after this
import { templates } from './templates'
import { keywords } from './keywords'
import { reserved } from './reserved'
import { Module, MemberOptions, InterfaceOptions } from './types'

let typeCache = {}

let genReg = /<.+?>$/
let mappedTypes = {
  Date: 'DateTime',
  Object: 'obj',
  object: 'obj',
  Array: 'ResizeArray',
  RegExp: 'Regex',
  String: 'string',
  Number: 'float',
  Never: '`a',
}

function escape( x: string ) {
  // HACK: ignore strings with a comment (* ... *), tuples ( * )
  // and union types arrays U2<string,float>[] 
  if ( x !== undefined && ( x.indexOf( '(*' ) >= 0 || x.indexOf( ' * ' ) >= 0 || /^U\d+<.*>$/.test( x ) ) ) {
    return x
  }
  let genParams = genReg.exec( x )
  let name = x.replace( genReg, '' )
  name = ( keywords.indexOf( name ) >= 0 || reserved.indexOf( name ) >= 0 || /[^\w.']/.test( name ) )
    ? '``' + name + '``'
    : name
  return name + ( genParams ? genParams[0] : '' )
}

function stringToUnionCase( str ) {
  function upperFirstLetter( str ) {
    return typeof str === 'string' && str.length > 1
      ? str[0].toUpperCase() + str.substr( 1 )
      : str
  }
  if ( str.length == 0 ) {
    return `[<CompiledName("")>] EmptyString`
  } else if ( /^[A-Z]/.test( str ) ) {
    return `[<CompiledName("${str}")>] ${escape( str )}`
  } else {
    return escape( upperFirstLetter( str ) )
  }
}

function append( template, txt ) {
  return typeof txt === 'string' && txt.length > 0 ? template + txt + '\n\n' : template
}

function joinPath( path, name ) {
  return typeof path === 'string' && path.length > 0 ? path + '.' + name : name
}

function isDuplicate( member, other ) {
  function arrayEquals( ar1 = [], ar2 = [], f ) {
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
    if ( m.name === member.name && arrayEquals(
      m.parameters, member.parameters,
      ( x, y ) => x.type === y.type ) ) {
      return true
    }
  }
  return false
}

function printParameters( parameters, sep: string = ', ', def: string = '' ) {
  function printParameter( x ) {
    if ( x.rest ) {
      let execed = /^ResizeArray<(.*?)>$/.exec( escape( x.type ) )
      let type = ( execed == null ? 'obj' : escape( execed[1] ) ) + '[]'
      return '[<ParamArray>] ' + escape( x.name ) + ': ' + type
    }
    else {
      return ( x.optional ? '?' : '' ) + escape( x.name ) + ': ' + escape( x.type )
    }
  }
  return Array.isArray( parameters ) && parameters.length > 0
    ? parameters.map( printParameter ).join( sep )
    : def
}

function printMethod( prefix: string ): ( x: string ) => string {
  return function ( y: Partial<MemberOptions> ): string {
    let method = prefix + ( y.emit ? '[<Emit("' + y.emit + '")>] ' : '' ) + templates.method
    if ( y !== undefined && y.name !== undefined ) {
      method = method.replace( '[NAME]', escape( y.name ) )
    }
    if ( y !== undefined && y.type !== undefined ) {
      method = method.replace( '[TYPE]', escape( y.type ) )
    }
    if ( y !== undefined && y.parameters !== undefined ) {
      method = method.replace( '[PARAMETERS]', printParameters( y.parameters, ' * ', 'unit' ) )
    }

    return method
  }
}

function printProperty( prefix: string ) {
  return function ( x: Partial<MemberOptions> ): string {
    let param = Array.isArray( x.parameters ) && x.parameters.length === 1
      ? printParameters( x.parameters ) + ' -> ' : ''
    let property = prefix + ( x.emit ? '[<Emit("' + x.emit + '")>] ' : '' ) + templates.property
    if ( x && x.name ) {
      property = property.replace( '[NAME]', escape( x.name ) )
    }
    if ( x && x.type ) {
      property = property.replace( '[TYPE]', param + escape( x.type ) )
    }
    if ( x && x.optional ) {
      property = property.replace( '[OPTION]', x.optional ? ' option' : '' )
    }

    return property
  }
}

function printParents( prefix: string, node, template ) {
  function printParentMembers( prefix, lines: any[], parent, child ) {
    if ( child.kind === 'class' ) {
      lines.push( `${prefix}interface ${parent.name} with` )
      parent.properties.forEach( x => lines.push( printClassProperty( prefix + '    ' )( x ) ) )
      parent.methods.forEach( x => lines.push( printClassMethod( prefix + '    ' )( x ) ) )
    }
    else {
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
      }
      else {
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
  })

  return template + ( lines.length ? lines.join( '\n' ) + '\n' : '' )
}

function printArray( arr: Partial<MemberOptions>[], mapper: ( s: string ) => string ) {
  return arr && arr.length > 1
    ? arr.map( mapper ).filter( x => x.length > 0 ).join( '\n' )
    : ''
}

function printMembers( prefix: string, ent: Partial<InterfaceOptions> ) {
  if ( ent !== undefined && ent.properties !== undefined && ent.methods !== undefined && prefix !== undefined )
    return [
      printArray( ent.properties, printProperty( prefix ) ),
      printArray( ent.methods, printMethod( prefix ) )
    ].filter( x => x.length > 0 ).join( '\n' )
}

function printClassMethod( prefix ) {
  return function ( x ) {
    if ( x.name !== undefined ) {
      return prefix + ( x.emit ? '[<Emit("' + x.emit + '")>] ' : '' ) + templates.classMethod
      .replace( '[STATIC]', x.static ? 'static ' : '' )
      .replace( '[MEMBER_KEYWORD]', 'member' )
      .replace( '[INSTANCE]', x.static ? '' : '__.' )
      .replace( '[NAME]', escape( x.name ) )
      .replace( '[TYPE]', escape( x.type ) )
      .replace( '[PARAMETERS]', printParameters( x.parameters ) )
    }
    return ''
  }
}

function printClassProperty( prefix ) {
  return function ( x: Partial<MemberOptions> ) {
    if ( x.name !== undefined ) {
      return prefix + ( x.emit ? '[<Emit("' + x.emit + '")>] ' : '' ) + templates.classProperty
        .replace( '[STATIC]', x.static ? 'static ' : '' )
        .replace( '[INSTANCE]', x.static ? '' : '__.' )
        .replace( '[NAME]', escape( x.name ) )
        .replace( /\[TYPE\]/g, x && x.type ? escape( x.type ) : '' )
        .replace( /\[OPTION\]/g, x.optional ? ' option' : '' )
    }
    return ''
  }
}

function printClassMembers( prefix, ent ) {
  return [
    printArray( ent.properties, printClassProperty( prefix ) ),
    printArray( ent.methods, printClassMethod( prefix ) )
  ].filter( x => x.length > 0 ).join( '\n' )
}

function printImport( path, name ) {
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

function printInterface( prefix ) {
  function printDecorator( ifc ) {
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
    let hasParents = tmp != template
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
        }).join( '\n' )
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
        return template += ( members && members.length === 0 && !hasParents
          ? prefix + '    interface end'
          : members )

    }
  }
}

function printGlobals( prefix, ent ) {
  let members = printClassMembers( prefix + '    ' + ( ent.name ? '' : '[<Global>] ' ), ent )
  if ( members.length > 0 ) {
    return prefix + templates.moduleProxy.replace(
      '[IMPORT]', printImport( ent.path, ent.name ) ) + members
  }
  return ''
}

function printModule( prefix ) {
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

function printFile( file ) {
  let template = templates.file
  template = append( template, file.interfaces.map( printInterface( '' ) ).join( '\n\n' ) )
  template = append( template, printGlobals( '', file ) )
  return template + file.modules.map( printModule( '' ) ).join( '\n\n' )
}

function hasFlag( flags, flag ) {
  return flags != null && ( flags & flag ) === flag
}

function getName( node: ts.Node ) {
  if ( ( <ts.PropertyAccessExpression>node ).expression && ( <any>node ).expression.kind == ts.SyntaxKind.PropertyAccessExpression ) {
    return ( <any>node ).expression.expression.text + '.' + ( <any>node ).expression.name.text
    //return ( <any> node ).expression.getText() + '.' + ( <any> node ).expression.name.text
  }
  else {
    // TODO: Throw exception if there's no name?
    return ( <any>node ).name ? ( <any>node ).name.text : ( ( <any>node ).expression ? ( <any>node ).expression.text : null )
  }
}

function printTypeArguments( typeArgs: any[] = [] ) {
  return typeArgs.length === 0 ? '' : '<' + typeArgs.map( getType ).join( ', ' ) + '>'
}

function findTypeParameters( node, acc: any[] = [] ) {

  if ( !node ) {
    return acc
  }
  if ( Array.isArray( node.typeParameters ) ) {
    node.typeParameters.forEach( x => acc.push( x.name.text ) )
  }
  return findTypeParameters( node.parent, acc )
}

function getType( type ) {
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
      return 'ResizeArray<' + getType( type.elementType ) + '>'
    case ts.SyntaxKind.FunctionType:
      let cbParams = type.parameters.map( function ( x ) {
        return x.dotDotDotToken ? 'obj' : getType( x.type )
      }).join( ', ' )
      return 'Func<' + ( cbParams || 'unit' ) + ', ' + getType( type.type ) + '>'
    case ts.SyntaxKind.UnionType:
      if ( type.types && type.types[0].kind === ts.SyntaxKind.StringLiteral ) {
        return '(* TODO StringEnum ' + type.types.map( x => x.text ).join( ' | ' ) + ' *) string'
      } else if ( type.types.length <= 4 ) {
        return 'U' + type.types.length + printTypeArguments( type.types )
      } else {
        return 'obj'
      }
    case ts.SyntaxKind.TupleType:
      return type.elementTypes.map( getType ).join( ' * ' )
    case ts.SyntaxKind.ParenthesizedType:
      return getType( type.type )
    // case ts.SyntaxKind.TypeQuery:
    //     return type.exprName.text + "Constructor";
    default:
      let name = type.typeName ? type.typeName.text : ( type.expression ? type.expression.text : null )
      if ( type.expression && type.expression.kind === ts.SyntaxKind.PropertyAccessExpression ) {
        name = type.expression.expression.text + '.' + type.expression.name.text
      }
      if ( type.typeName && type.typeName.left && type.typeName.right ) {
        name = type.typeName.left.text + '.' + type.typeName.right.text
      }

      if ( !name ) {
        return 'obj'
      }
      if ( name in mappedTypes ) {
        name = mappedTypes[name]
      }

      let result = name + printTypeArguments( type.typeArguments )
      return ( typeParameters.indexOf( result ) > -1 ? "'" : '' ) + result
  }
}

function getParents( node ) {
  let parents: any[] = []
  if ( Array.isArray( node.heritageClauses ) ) {
    for ( let i = 0; i < node.heritageClauses.length; i++ ) {
      let types = node.heritageClauses[i].types
      for ( let j = 0; j < types.length; j++ ) {
        parents.push( getType( types[j] ) )
      }
    }
  }
  return parents
}

// TODO: get comments
function getProperty( node: ts.Node, opts: Partial<MemberOptions> = {}): Partial<MemberOptions> {
  return {
    name: opts.name || getName( node ),
    type: getType(( <ts.PropertyDeclaration>node ).type ),
    optional: ( <ts.PropertyDeclaration>node ).questionToken !== undefined,
    static: ( <ts.PropertyDeclaration>node ).name ? hasFlag(( <ts.PropertyDeclaration>node ).name.modifierFlagsCache, ts.ModifierFlags.Static ) : false
  }
}

function getStringEnum( node ) {
  return {
    kind: 'stringEnum',
    name: getName( node ),
    properties: node.type.types.map( function ( n ) {
      return { name: n.text }
    }),
    parents: [],
    methods: []
  }
}

function getEnum( node ) {
  return {
    kind: 'enum',
    name: getName( node ),
    properties: node.members.map( function ( n, i ) {
      return {
        name: getName( n ),
        value: n.initializer ? n.initializer.text : i
      }
    }),
    parents: [],
    methods: []
  }
}

// TODO: Check if it's const
function getVariables( node ) {
  let variables: any[] = []
  let anonymousTypes: any[] = []
  let name
  let type
  let declarationList = Array.isArray( node.declarationList )
    ? node.declarationList : [node.declarationList]
  for ( let i = 0; i < declarationList.length; i++ ) {
    let declarations = declarationList[i].declarations
    for ( let j = 0; j < declarations.length; j++ ) {
      name = declarations[j].name.text
      if ( declarations[j].type.kind === ts.SyntaxKind.TypeLiteral ) {
        type = visitInterface( declarations[j].type, { name: name + 'Type', anonymous: true })
        anonymousTypes.push( type )
        type = type.name
      }
      else {
        type = getType( declarations[j].type )
      }
      variables.push( {
        name: name,
        type: type,
        static: true,
        parameters: []
      })
    }
  }
  return {
    variables: variables,
    anonymousTypes: anonymousTypes
  }
}

function getParameter( param ) {
  return {
    name: param.name.text,
    type: getType( param.type ),
    optional: param.questionToken != null,
    rest: param.dotDotDotToken != null
  }
}

// TODO: get comments
function getMethod( node: ts.Node, opts: Partial<MemberOptions> = [] ) {
  let tNode = node as ts.MethodDeclaration
  opts = opts || {}
  let meth: Partial<MemberOptions> = {
    name: opts.name || getName( tNode ),
    type: getType( tNode.type ),
    optional: tNode.questionToken != null,
    static: opts.static
    || ( tNode.name && hasFlag( tNode.name.modifierFlagsCache, ts.ModifierFlags.Static ) )
    || ( tNode.modifiers && hasFlag( tNode.modifiers.transformFlags, ts.ModifierFlags.Static ) ),
    parameters: tNode.parameters.map( getParameter )
  }
  let firstParam = tNode.parameters[0]
  let secondParam = tNode.parameters[1]
  if ( secondParam && secondParam.type && secondParam.type.kind === ts.SyntaxKind.StringLiteral ) {
    // The only case I've seen following this pattern is
    // createElementNS(namespaceURI: "http://www.w3.org/2000/svg", qualifiedName: "a"): SVGAElement
    if ( meth && meth.parameters ) {
      meth.parameters = meth.parameters.slice( 2 )
    }
    if ( firstParam && firstParam.type && meth.parameters ) {
      meth.emit = `$0.${meth.name}('${firstParam.type.getText()}', '${secondParam.type.getText()}'${meth.parameters.length ? ',$1...' : ''})`
      meth.name += '_' + secondParam.type.getText()

    }
  }
  else if ( firstParam && firstParam.type && firstParam.type.kind === ts.SyntaxKind.StringLiteral ) {
    if ( meth && meth.parameters ) {
      meth.parameters = meth.parameters.slice( 1 )
      meth.emit = `$0.${meth.name}('${firstParam.type.getText()}'${meth.parameters.length ? ',$1...' : ''})`
      meth.name += '_' + firstParam.type.getText()
    }
  }
  return meth
}

function getInterface( node: ts.Node, opts: Partial<MemberOptions> ): Partial<InterfaceOptions> {
  function printTypeParameters( typeParams: any[] ) {
    typeParams = typeParams || []
    return typeParams.length === 0 ? '' : '<' + typeParams.map( function ( x ) {
      return "'" + x.name.text
    }).join( ', ' ) + '>'
  }
  opts = opts || {}
  let ifc: Partial<InterfaceOptions> = {
    name: opts.name || ( getName(( <any>node ) ) + printTypeParameters(( <any>node ).typeParameters ) ),
    kind: opts.kind || 'interface',
    parents: opts.kind === 'alias' ? [getType(( <any>node ).type )] : getParents( node ),
    properties: [{}],
    methods: [{}],
    path: opts.path
  }
  if ( !opts.anonymous ) {
    if ( ifc && typeof ifc.name === 'string' ) {
      typeCache[joinPath( ifc.path, ifc.name.replace( genReg, '' ) )] = ifc
    }
  }
  return ifc
}

function mergeNamesakes( xs, getName, mergeTwo ) {
  let grouped = {}
  xs.forEach( function ( x ) {
    let name = getName( x )
    if ( !Array.isArray( grouped[name] ) ) { grouped[name] = [] }
    grouped[name].push( x )
  })

  return Object.keys( grouped ).map( function ( k ) { return grouped[k].reduce( mergeTwo ) })
}

function mergeInterfaces( a, b ) {
  return {
    name: a.name,
    kind: a.kind,
    parents: a.parents,
    path: a.path,
    properties: a.properties.concat( b.properties ),
    methods: a.methods.concat( b.methods )
  }
}

function mergeNamesakeInterfaces( intfs ) {
  return mergeNamesakes( intfs, function ( i ) { return i.name }, mergeInterfaces )
}

function visitInterface( node: ts.Node, opts: Partial<InterfaceOptions> ): Partial<InterfaceOptions> {
  let ifc = getInterface( node, opts )
  ifc.properties = ifc.properties || [{}] as Partial<MemberOptions>[]
  ifc.methods = ifc.methods || [{}] as Partial<MemberOptions>[]

  let visitMembers = node => {
    let member
    let name

    switch ( node.kind ) {
      case ts.SyntaxKind.PropertySignature:
      case ts.SyntaxKind.PropertyDeclaration:
        if ( node && node.name && node.name.kind === ts.SyntaxKind.ComputedPropertyName ) {
          name = getName( node.name )
          member = getProperty(( <ts.Node>node ), { name: '[' + name + ']' })
          member.emit = '$0[' + name + ']{{=$1}}'
        }
        else {
          member = getProperty( node )
        }
        if ( ifc && ifc.properties ) { ifc.properties.push( member ) }
        break
      // TODO: If interface only contains one `Invoke` method
      // make it an alias of Func
      case ts.SyntaxKind.CallSignature:
        member = getMethod( node, { name: 'Invoke' })
        member.emit = '$0($1...)'
        if ( ifc && ifc.methods ) { ifc.methods.push( member ) }
        break
      case ts.SyntaxKind.MethodSignature:
      case ts.SyntaxKind.MethodDeclaration:

        if ( node && node.name && node.name.kind === ts.SyntaxKind.ComputedPropertyName ) {
          name = getName( node.name )
          member = getMethod( node, { name: '[' + name + ']' })
          member.emit = '$0[' + name + ']($1...)'
        }
        else {
          member = getMethod( node )
        }
        // Sometimes TypeScript definitions contain duplicated methods
        if ( !isDuplicate( member, ifc.methods ) ) {
          if ( ifc && ifc.methods ) {
            ifc.methods.push( member )
          }
        }
        break
      case ts.SyntaxKind.ConstructSignature:
        member = getMethod( node, { name: 'Create' })
        member.emit = 'new $0($1...)'
        if ( ifc && ifc.methods ) {
          ifc.methods.push( member )
        }
        break
      case ts.SyntaxKind.IndexSignature:
        member = getMethod( node, { name: 'Item' })
        member.emit = '$0[$1]{{=$2}}'
        if ( ifc && ifc.properties ) {
          ifc.properties.push( member )
        }
        break
      case ts.SyntaxKind.Constructor:
        if ( ifc && ifc.constructorParameters ) {
          ifc.constructorParameters = ( <ts.ConstructSignatureDeclaration>node ).parameters.map( getParameter )
        }
        break
    }
  }
  if ( node.kind === ts.SyntaxKind.InterfaceDeclaration ) {
    ( <ts.InterfaceDeclaration>node ).members.forEach( n => visitMembers( n ) )
  }

  if ( ( <ts.TypeAliasDeclaration>node ).kind === ts.SyntaxKind.TypeAliasDeclaration ) {
    let tNode = ( node as ts.TypeAliasDeclaration )
    tNode.getChildren().forEach(( n ) => visitMembers( n ) )
  }
  return ifc
}

function mergeModules( a, b ) {
  return {
    name: a.name,
    path: a.path,
    interfaces: mergeNamesakeInterfaces( a.interfaces.concat( b.interfaces ) ),
    properties: a.properties.concat( b.properties ),
    methods: a.methods.concat( b.methods ),
    modules: mergeNamesakeModules( a.modules.concat( b.modules ) )
  }
}

function mergeNamesakeModules( modules ) {
  return mergeNamesakes( modules, function ( m ) { return m.name }, mergeModules )
}

function visitModuleNode( mod: Partial<Module> = {}, modPath: any = null ) {
  return function ( node: ts.Node ) {
    switch ( node.kind ) {
      case ts.SyntaxKind.InterfaceDeclaration:
        if ( mod && mod.interfaces ) {
          mod.interfaces.push( visitInterface( node, { kind: 'interface', path: modPath }) )
        }
        break
      case ts.SyntaxKind.ClassDeclaration:
        if ( mod && mod.interfaces ) {
          mod.interfaces.push( visitInterface( node, { kind: 'class', path: modPath }) )
        }
        break
      case ts.SyntaxKind.TypeAliasDeclaration:
        const taNode: ts.TypeAliasDeclaration = ( <ts.TypeAliasDeclaration>node )
        if ( mod && mod.interfaces ) {
          if ( taNode && taNode.type.kind === ts.SyntaxKind.StringLiteral ) {
            mod.interfaces.push( getStringEnum( node ) )
          } else {
            mod.interfaces.push( visitInterface( node, { kind: 'alias', path: modPath }) )
          }
        }
        break
      case ts.SyntaxKind.VariableStatement:
        if ( mod && mod.properties ) {
          const props = mod.properties
          let varsAndTypes = getVariables( node )
          varsAndTypes.variables.forEach( x => props.push( x ) )
          varsAndTypes.anonymousTypes.forEach( x => props.push( x ) )
        }
        break
      case ts.SyntaxKind.FunctionDeclaration:
        if ( mod && mod.methods ) {
          mod.methods.push( getMethod( node, { static: true }) )
        }
        break
      case ts.SyntaxKind.ModuleDeclaration:
        let m = visitModule(( <ts.ModuleDeclaration>node ), { path: modPath })
        let isEmpty = Object.keys( m ).every( function ( k ) { return !Array.isArray( m[k] ) || m[k].length === 0 })
        if ( !isEmpty && mod && mod.modules ) {
          mod.modules.push( m )
        } break
      case ts.SyntaxKind.EnumDeclaration:
        if ( mod && mod.interfaces ) {
          mod.interfaces.push( getEnum( node ) )
        }
        break
    }
  }
}

function visitModule( node: ts.ModuleDeclaration | ts.ModuleBody | undefined, opts: Partial<Module> = {}): Partial<Module> {

  let mod: Partial<Module> = {
    name: getName(( <any>node ) ),
    path: opts.path,
    interfaces: [],
    properties: [],
    methods: [],
    modules: []
  }
  let modPath = joinPath( mod.path, mod.name )

  switch ( ( <any>node ).body.kind ) {
    case ts.SyntaxKind.ModuleDeclaration:
      if ( mod !== undefined && mod.modules ) {
        mod.modules.push( visitModule(( <ts.ModuleDeclaration>node ).body, { path: modPath }) )
      } break

    case ts.SyntaxKind.ModuleBlock:
      let body = ( <ts.ModuleBlock>( <ts.ModuleDeclaration>node ).body )
      if ( body !== null && body !== null ) {
        body.statements.forEach( visitModuleNode( mod, modPath ) )
      }
      break
  }

  return mod
}

function visitFile( node: ts.SourceFile ) {
  let file: Partial<Module> = {
    properties: [],
    interfaces: [],
    methods: [],
    modules: []
  }

  ts.forEachChild( node, visitModuleNode( file ) )

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
  let fileInfo: Partial<InterfaceOptions> = visitFile( sourceFile )
  let ffi = printFile( fileInfo )
  console.log( ffi )
  process.exit( 0 )
}
catch ( err ) {
  console.log( 'ERROR: ' + err )
  process.exit( 1 )
}
