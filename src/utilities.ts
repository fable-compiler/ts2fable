import { reserved } from './reserved';
import { keywords } from './keywords';


export const genReg = /<.+?>$/

export function escape( x: string ) {
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

export function stringToUnionCase( str ) {
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

export function append( template, txt ) {
  return typeof txt === 'string' && txt.length > 0 ? template + txt + '\n\n' : template
}

export function joinPath( path, name ) {
  return typeof path === 'string' && path.length > 0 ? path + '.' + name : name
}

export function isDuplicate( member, other ) {
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