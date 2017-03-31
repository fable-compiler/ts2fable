#!/usr/bin/env node
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var fs = require("fs");
require("byots"); // This exposes a ts global usable from any imported file after this
var templates_1 = require("./templates");
var keywords_1 = require("./keywords");
var reserved_1 = require("./reserved");
var typeCache = {};
var genReg = /<.+?>$/;
var mappedTypes = {
    Date: 'DateTime',
    Object: 'obj',
    Array: 'ResizeArray',
    RegExp: 'Regex',
    String: 'string',
    Number: 'float'
};
function escape(x) {
    // HACK: ignore strings with a comment (* ... *), tuples ( * )
    // and union types arrays U2<string,float>[]
    if (typeof x === 'undefined') {
        return '';
    }
    if (x && x.indexOf('(*') >= 0 || x.indexOf(' * ') >= 0 || /^U\d+<.*>$/.test(x)) {
        return x;
    }
    var genParams = genReg.exec(x);
    var name = x.replace(genReg, '');
    name = (keywords_1.keywords.indexOf(name) >= 0 || reserved_1.reserved.indexOf(name) >= 0 || /[^\w.']/.test(name))
        ? '``' + name + '``'
        : name;
    return name + (genParams ? genParams[0] : '');
}
function stringToUnionCase(str) {
    function upperFirstLetter(str) {
        return typeof str === 'string' && str.length > 1
            ? str[0].toUpperCase() + str.substr(1)
            : str;
    }
    if (str.length === 0) {
        return "[<CompiledName(\"\")>] EmptyString";
    }
    if (/^[A-Z]/.test(str)) {
        return "[<CompiledName(\"" + str + "\")>] " + escape(str);
    }
    else {
        return escape(upperFirstLetter(str));
    }
}
function append(template, txt) {
    return typeof txt === 'string' && txt.length > 0 ? template + txt + '\n\n' : template;
}
function joinPath(path, name) {
    return typeof path === 'string' && path.length > 0 ? path + '.' + name : name;
}
function isDuplicate(member, other) {
    function arrayEquals(ar1, ar2, f) {
        if (ar1 === void 0) { ar1 = []; }
        if (ar2 === void 0) { ar2 = []; }
        if (f === void 0) { f = function () { return false; }; }
        if (ar1.length !== ar2.length) {
            return false;
        }
        for (var i = 0; i < ar1.length; i++) {
            if (!f(ar1[i], ar2[i])) {
                return false;
            }
        }
        return true;
    }
    for (var _i = 0, other_1 = other; _i < other_1.length; _i++) {
        var m = other_1[_i];
        if (m.name === member.name && arrayEquals(m.parameters, member.parameters, function (x, y) { return x.type === y.type; })) {
            return true;
        }
    }
    return false;
}
function printParameters(parameters, sep, def) {
    if (sep === void 0) { sep = ', '; }
    if (def === void 0) { def = ''; }
    function printParameter(x) {
        if (x.rest) {
            var execed = /^ResizeArray<(.*?)>$/.exec(escape(x.type));
            var type = (execed == null ? 'obj' : escape(execed[1])) + '[]';
            return '[<ParamArray>] ' + escape(x.name) + ': ' + type;
        }
        else {
            return (x.optional ? '?' : '') + escape(x.name) + ': ' + escape(x.type);
        }
    }
    return Array.isArray(parameters) && parameters.length > 0
        ? parameters.map(printParameter).join(sep)
        : def;
}
function printMethod(prefix) {
    return function (x) {
        return prefix + (x.emit ? '[<Emit("' + x.emit + '")>] ' : '') + templates_1.templates.method
            .replace('[NAME]', escape(x.name))
            .replace('[TYPE]', escape(x.type))
            .replace('[PARAMETERS]', printParameters(x.parameters, ' * ', 'unit'));
    };
}
function printProperty(prefix) {
    return function (x) {
        var param = Array.isArray(x.parameters) && x.parameters.length === 1
            ? printParameters(x.parameters) + ' -> ' : '';
        return prefix + (x.emit ? '[<Emit("' + x.emit + '")>] ' : '') + templates_1.templates.property
            .replace('[NAME]', escape(x.name))
            .replace('[TYPE]', param + escape(x.type))
            .replace('[OPTION]', x.optional ? ' option' : '');
    };
}
function printParents(prefix, node, template) {
    function printParentMembers(prefix, lines, parent, child) {
        if (child.kind === 'class') {
            lines.push(prefix + "interface " + parent.name + " with");
            parent.properties.forEach(function (x) { return lines.push(printClassProperty(prefix + '    ')(x)); });
            parent.methods.forEach(function (x) { return lines.push(printClassMethod(prefix + '    ')(x)); });
        }
        else {
            parent.properties.forEach(function (x) { return lines.push(printProperty(prefix)(x)); });
            parent.methods.forEach(function (x) { return lines.push(printMethod(prefix)(x)); });
        }
        // Clean methods and properties from the child
        child.properties = child.properties.filter(function (x) { return !isDuplicate(x, parent.properties); });
        child.methods = child.methods.filter(function (x) { return !isDuplicate(x, parent.methods); });
    }
    var lines = [];
    node.parents.forEach(function (parentName) {
        var nameNoArgs = parentName.replace(genReg, '');
        var parent = typeCache[nameNoArgs.indexOf('.') > 0 ? nameNoArgs : joinPath(node.path, nameNoArgs)];
        if (node.kind === 'class') {
            if (parent && parent.kind === 'class') {
                lines.push(prefix + 'inherit ' + parentName + '()'); // TODO: Check base class constructor arguments?
            }
            else {
                if (parent != null && (parent.properties.length || parent.methods.length)) {
                    printParentMembers(prefix, lines, parent, node);
                }
                else if (parentName !== 'obj') {
                    lines.push(prefix + 'interface ' + parentName);
                }
            }
        }
        else if (node.kind === 'interface') {
            if (parent && parent.kind === 'class') {
                // Interfaces cannot extend classes, just copy the members
                printParentMembers(prefix, lines, parent, node);
            }
            else if (parentName !== 'obj') {
                lines.push(prefix + 'inherit ' + parentName);
            }
        }
    });
    return template + (lines.length ? lines.join('\n') + '\n' : '');
}
function printArray(arr, mapper) {
    return arr && arr.length > 0
        ? arr.map(mapper).filter(function (x) { return x.length > 0; }).join('\n')
        : '';
}
function printMembers(prefix, ent) {
    return [
        printArray(ent.properties, printProperty(prefix)),
        printArray(ent.methods, printMethod(prefix))
    ].filter(function (x) { return x.length > 0; }).join('\n');
}
function printClassMethod(prefix) {
    return function (x) {
        return prefix + (x.emit ? '[<Emit("' + x.emit + '")>] ' : '') + templates_1.templates.classMethod
            .replace('[STATIC]', x.static ? 'static ' : '')
            .replace('[MEMBER_KEYWORD]', 'member')
            .replace('[INSTANCE]', x.static ? '' : '__.')
            .replace('[NAME]', escape(x.name))
            .replace('[TYPE]', escape(x.type))
            .replace('[PARAMETERS]', printParameters(x.parameters));
    };
}
function printClassProperty(prefix) {
    return function (x) {
        return prefix + (x.emit ? '[<Emit("' + x.emit + '")>] ' : '') + templates_1.templates.classProperty
            .replace('[STATIC]', x.static ? 'static ' : '')
            .replace('[INSTANCE]', x.static ? '' : '__.')
            .replace('[NAME]', escape(x.name.text))
            .replace(/\[TYPE\]/g, escape(x.type))
            .replace(/\[OPTION\]/g, x.optional ? ' option' : '');
    };
}
function printClassMembers(prefix, ent) {
    return [
        printArray(ent.properties, printClassProperty(prefix)),
        printArray(ent.methods, printClassMethod(prefix))
    ].filter(function (x) { return x.length > 0; }).join('\n');
}
function printImport(path, name) {
    if (!name) {
        return '[<Erase>]';
    }
    else {
        var fullPath = joinPath(path, name.replace(genReg, ''));
        var period = fullPath.indexOf('.');
        var importPath = period >= 0
            ? fullPath.substr(period + 1) + '","' + fullPath.substr(0, period)
            : '*","' + fullPath;
        return "[<Import(\"" + importPath + "\")>] ";
    }
}
function printInterface(prefix) {
    function printDecorator(ifc) {
        switch (ifc.kind) {
            case 'class':
                return printImport(ifc.path, ifc.name);
            case 'stringEnum':
                return '[<StringEnum>] ';
            default:
                return '';
        }
    }
    return function (ifc, i) {
        var isEnum = ifc.kind === 'enum' || ifc.kind === 'stringEnum';
        var isAlias = ifc.kind === 'alias';
        var template = isEnum ? templates_1.templates.enum :
            isAlias ? templates_1.templates.alias :
                templates_1.templates.interface;
        template =
            prefix + template
                .replace('[TYPE_KEYWORD]', i === 0 ? 'type' : 'and')
                .replace('[NAME]', escape(ifc.name))
                .replace('[DECORATOR]', printDecorator(ifc))
                .replace('[CONSTRUCTOR]', ifc.kind === 'class'
                ? '(' + printParameters(ifc.constructorParameters) + ')' : '');
        var tmp = printParents(prefix + '    ', ifc, template);
        var hasParents = tmp !== template;
        template = tmp;
        switch (ifc.kind) {
            case 'alias':
                return template += prefix + '    ' + ifc.parents[0];
            case 'enum':
                return template + ifc.properties.map(function (currentValue) {
                    var cv = templates_1.templates.enumCase
                        .replace('[NAME]', currentValue.name)
                        .replace('[ID]', currentValue.value);
                    return prefix + cv;
                }).join('\n');
            case 'stringEnum':
                return template + prefix + prefix + '| ' + ifc.properties.map(function (x) {
                    return stringToUnionCase(x.name);
                }).join(' | ');
            case 'class':
                var classMembers = printClassMembers(prefix + '    ', ifc);
                return template += (classMembers.length === 0 && !hasParents
                    ? prefix + '    class end'
                    : classMembers);
            // case "interface":
            default:
                var members = printMembers(prefix + '    ', ifc);
                return template += (members.length === 0 && !hasParents
                    ? prefix + '    interface end'
                    : members);
        }
    };
}
function printGlobals(prefix, ent) {
    var members = printClassMembers(prefix + '    ' + (ent.name ? '' : '[<Global>] '), ent);
    if (members.length > 0) {
        return prefix + templates_1.templates.moduleProxy.replace('[IMPORT]', printImport(ent.path, ent.name)) + members;
    }
    return '';
}
function printModule(prefix) {
    return function (mod) {
        var template = prefix + templates_1.templates.module
            .replace('[NAME]', escape(mod.name));
        template = append(template, mod.interfaces.map(printInterface(prefix + '    ')).join('\n\n'));
        template = append(template, printGlobals(prefix + '    ', mod));
        template += mod.modules.map(printModule(prefix + '    ')).join('\n\n');
        return template;
    };
}
function printFile(file) {
    var template = templates_1.templates.file;
    template = append(template, file.interfaces.map(printInterface('')).join('\n\n'));
    template = append(template, printGlobals('', file));
    return template + file.modules.map(printModule('')).join('\n\n');
}
function hasFlag(flags, flag) {
    return flags != null && (flags & flag) === flag;
}
function getName(node) {
    if (node.expression && node.expression.kind === ts.SyntaxKind.PropertyAccessExpression) {
        return node.expression.expression.text + '.' + node.expression.name.text;
    }
    else {
        // TODO: Throw exception if there's no name?
        return node.name ? node.name.text : (node.expression ? node.expression.text : null);
    }
}
function printTypeArguments(typeArgs) {
    typeArgs = typeArgs || [];
    return typeArgs.length === 0 ? '' : '<' + typeArgs.map(getType).join(', ') + '>';
}
function findTypeParameters(node, acc) {
    if (acc === void 0) { acc = []; }
    if (!node) {
        return acc;
    }
    if (Array.isArray(node.typeParameters)) {
        node.typeParameters.forEach(function (x) { return acc.push(x.name.text); });
    }
    return findTypeParameters(node.parent, acc);
}
function getType(type) {
    if (!type) {
        return 'obj';
    }
    var typeParameters = findTypeParameters(type);
    switch (type.kind) {
        case ts.SyntaxKind.StringKeyword:
            return 'string';
        case ts.SyntaxKind.NumberKeyword:
            return 'float';
        case ts.SyntaxKind.BooleanKeyword:
            return 'bool';
        case ts.SyntaxKind.VoidKeyword:
            return 'unit';
        case ts.SyntaxKind.SymbolKeyword:
            return 'Symbol';
        case ts.SyntaxKind.ArrayType:
            return 'ResizeArray<' + getType(type.elementType) + '>';
        case ts.SyntaxKind.FunctionType:
            var cbParams = type.parameters.map(function (x) {
                return x.dotDotDotToken ? 'obj' : getType(x.type || x.type);
            }).join(', ');
            var typeToGet = type.type || type;
            return 'Func<' + (cbParams || 'unit') + ', ' + getType(typeToGet) + '>';
        case ts.SyntaxKind.UnionType:
            if (type.types && type.types[0].kind === ts.SyntaxKind.StringLiteral) {
                return '(* TODO StringEnum ' + type.types.map(function (x) { return x.getText; }).join(' | ') + ' *) string';
            }
            if (type.types.length <= 4) {
                return 'U' + type.types.length + printTypeArguments(type.types);
            }
            else {
                return 'obj';
            }
        case ts.SyntaxKind.TupleType:
            return type.elementTypes.map(getType).join(' * ');
        case ts.SyntaxKind.ParenthesizedType:
            return getType(type.type);
        // case ts.SyntaxKind.TypeQuery:
        //     return type.exprName.text + "Constructor";
        default:
            var anyType = type;
            var name_1 = type.typeName ? anyType.typeName.text : (anyType.expression ? anyType.expression.text : null);
            if (anyType.expression && anyType.expression.kind === ts.SyntaxKind.PropertyAccessExpression) {
                name_1 = anyType.expression.expression.text + '.' + anyType.expression.name.text;
            }
            if (anyType.typeName && anyType.typeName.left && anyType.typeName.right) {
                name_1 = anyType.typeName.left.text + '.' + anyType.typeName.right.text;
            }
            if (!name_1) {
                return 'obj';
            }
            if (name_1 in mappedTypes) {
                name_1 = mappedTypes[name_1];
            }
            // console.error( `UNSUPPORTED: ${ts.SyntaxKind[type.kind]}` )
            var result = name_1 + printTypeArguments(anyType.typeArguments);
            return (typeParameters.indexOf(result) > -1 ? "'" : '') + result;
    }
}
function getParents(node) {
    var parents = [];
    if (Array.isArray(node.heritageClauses)) {
        for (var i = 0; i < node.heritageClauses.length; i++) {
            var types = node.heritageClauses[i].types;
            for (var j = 0; j < types.length; j++) {
                parents.push(getType(types[j] || types[j]));
            }
        }
    }
    return parents;
}
// TODO: get comments
// TODO: Find out what to replace ts.NodeFlags.Static with
function getProperty(node, opts) {
    if (opts === void 0) { opts = {}; }
    return {
        name: opts.name || getName(node),
        type: getType(node),
        optional: node.transformFlags != null,
        static: node.kind ? hasFlag(node.flags, ts.SyntaxKind.StaticKeyword) : false
    };
}
function getStringEnum(node) {
    return {
        kind: 'stringEnum',
        name: getName(node),
        properties: node.type.types.map(function (n) {
            return { name: n.text };
        }),
        parents: [],
        methods: []
    };
}
function getEnum(node) {
    return {
        kind: 'enum',
        name: getName(node),
        properties: node.members.map(function (n, i) {
            return {
                name: getName(n),
                value: n.initializer ? n.initializer.text : i
            };
        }),
        parents: [],
        methods: []
    };
}
// TODO: Check if it's const
// I replaced declaration list with locals.
function getVariables(node) {
    var variables = [];
    var anonymousTypes = [];
    var name;
    var type;
    var declarationList = Array.isArray(node.declarationList) ? node.declarationList : [node.declarationList];
    declarationList.forEach(function (dList) {
        var declarations = dList.declarations;
        declarations.forEach(function (dec) {
            name = dec.name;
            if (dec.kind === ts.SyntaxKind.TypeLiteral) {
                type = visitInterface(dec, { name: name + 'Type', anonymous: true });
                anonymousTypes.push(type);
                type = type.name;
            }
            else {
                type = getType(dec.type || dec.type);
            }
            variables.push({
                name: name,
                type: type,
                static: true,
                parameters: []
            });
        });
    });
    return {
        variables: variables,
        anonymousTypes: anonymousTypes
    };
}
function getParameter(param) {
    if (param.type) {
        return {
            name: param.name.toString(),
            type: getType(param.type),
            optional: param.questionToken != null,
            rest: param.dotDotDotToken != null
        };
    }
}
// TODO: get comments
function getMethod(node, opts) {
    if (opts === void 0) { opts = {}; }
    var meth = {
        name: opts.name || getName(node),
        type: getType(node),
        optional: node.flags['Optional'] != null,
        parameters: node.parameters.map(getParameter),
        static: opts.static || (node.id && hasFlag(node.modifiers, ts.ModifierFlags.Static) || (node.modifiers && hasFlag(node.modifiers, ts.ModifierFlags.Static)))
    };
    var firstParam = node.parameters[0];
    var secondParam = node.parameters[1];
    if (secondParam && secondParam.type && secondParam.type.kind === ts.SyntaxKind.StringLiteral) {
        // The only case I've seen following this pattern is
        // createElementNS(namespaceURI: "http://www.w3.org/2000/svg", qualifiedName: "a"): SVGAElement
        meth.parameters = meth !== undefined ? meth.parameters.slice(2) : null;
        meth.emit = "$0." + meth.name + "('" + firstParam.type.getText() + "', '" + secondParam.type.getText() + "'" + (meth.parameters.length ? ',$1...' : '') + ")";
        meth.name += '_' + secondParam.type.getText();
    }
    else if (firstParam && firstParam.type && firstParam.type.kind === ts.SyntaxKind.StringLiteral) {
        meth.parameters = meth.parameters.slice(1);
        meth.emit = "$0." + meth.name + "('" + firstParam.type.getText() + "'" + (meth.parameters.length ? ',$1...' : '') + ")";
        meth.name += '_' + firstParam.type.getText();
    }
    return meth;
}
function getInterface(node, opts) {
    function printTypeParameters(typeParams) {
        typeParams = typeParams || [];
        return typeParams.length === 0 ? '' : '<' + typeParams.map(function (x) {
            return "'" + x.name.text;
        }).join(', ') + '>';
    }
    opts = opts || {};
    var ifc = {
        name: opts.name || (getName(node) + printTypeParameters(node.typeParameters)),
        kind: opts.kind || 'interface',
        parents: opts.kind === 'alias' ? [getType(node.type)] : getParents(node),
        properties: [],
        methods: [],
        path: opts.path
    };
    if (!opts.anonymous) {
        typeCache[joinPath(ifc.path, ifc.name.replace(genReg, ''))] = ifc;
    }
    return ifc;
}
function mergeNamesakes(xs, getName, mergeTwo) {
    var grouped = {};
    xs.forEach(function (x) {
        var name = getName(x);
        if (!Array.isArray(grouped[name])) {
            grouped[name] = [];
        }
        grouped[name].push(x);
    });
    return Object.keys(grouped).map(function (k) { return grouped[k].reduce(mergeTwo); });
}
function mergeInterfaces(a, b) {
    return {
        name: a.name,
        kind: a.kind,
        parents: a.parents,
        path: a.path,
        properties: a.properties.concat(b.properties),
        methods: a.methods.concat(b.methods)
    };
}
function mergeNamesakeInterfaces(intfs) {
    return mergeNamesakes(intfs, function (i) { return i.name; }, mergeInterfaces);
}
function visitInterface(node, opts) {
    var ifc = getInterface(node, opts);
    (node.members || []).forEach(function (node) {
        var member;
        var name;
        switch (node.kind) {
            case ts.SyntaxKind.PropertySignature:
            case ts.SyntaxKind.PropertyDeclaration:
                if (node.name.kind === ts.SyntaxKind.ComputedPropertyName) {
                    name = getName(node.name);
                    member = getProperty(node, { name: '[' + name + ']' });
                    member.emit = '$0[' + name + ']{{=$1}}';
                }
                else {
                    member = getProperty(node);
                }
                ifc.properties.push(member);
                break;
            // TODO: If interface only contains one `Invoke` method
            // make it an alias of Func
            case ts.SyntaxKind.CallSignature:
                member = getMethod(node, { name: 'Invoke' });
                member.emit = '$0($1...)';
                ifc.methods.push(member);
                break;
            case ts.SyntaxKind.MethodSignature:
            case ts.SyntaxKind.MethodDeclaration:
                if (node.name.kind === ts.SyntaxKind.ComputedPropertyName) {
                    name = getName(node.name);
                    member = getMethod(node, { name: '[' + name + ']' });
                    member.emit = '$0[' + name + ']($1...)';
                }
                else {
                    member = getMethod(node);
                }
                // Sometimes TypeScript definitions contain duplicated methods
                if (!isDuplicate(member, ifc.methods)) {
                    ifc.methods.push(member);
                }
                break;
            case ts.SyntaxKind.ConstructSignature:
                member = getMethod(node, { name: 'Create' });
                member.emit = 'new $0($1...)';
                ifc.methods.push(member);
                break;
            case ts.SyntaxKind.IndexSignature:
                member = getMethod(node, { name: 'Item' });
                member.emit = '$0[$1]{{=$2}}';
                ifc.properties.push(member);
                break;
            case ts.SyntaxKind.Constructor:
                ifc.constructorParameters = node.parameters.map(getParameter);
                break;
        }
    });
    return ifc;
}
function mergeModules(a, b) {
    return {
        name: a.name,
        path: a.path,
        interfaces: mergeNamesakeInterfaces(a.interfaces.concat(b.interfaces)),
        properties: a.properties.concat(b.properties),
        methods: a.methods.concat(b.methods),
        modules: mergeNamesakeModules(a.modules.concat(b.modules))
    };
}
function mergeNamesakeModules(modules) {
    return mergeNamesakes(modules, function (m) { return m.name; }, mergeModules);
}
function visitModuleNode(mod, modPath) {
    return function (node) {
        switch (node.kind) {
            case ts.SyntaxKind.InterfaceDeclaration:
                mod.interfaces.push(visitInterface(node, { kind: 'interface', path: modPath }));
                break;
            case ts.SyntaxKind.ClassDeclaration:
                mod.interfaces.push(visitInterface(node, { kind: 'class', path: modPath }));
                break;
            case ts.SyntaxKind.TypeAliasDeclaration:
                if (node.type.types && node.type.types[0].kind === ts.SyntaxKind.StringLiteral) {
                    mod.interfaces.push(getStringEnum(node));
                }
                else {
                    mod.interfaces.push(visitInterface(node, { kind: 'alias', path: modPath }));
                }
                break;
            case ts.SyntaxKind.VariableStatement:
                var varsAndTypes = getVariables(node);
                varsAndTypes.variables.forEach(function (x) { return mod.properties.push(x); });
                varsAndTypes.anonymousTypes.forEach(function (x) { return mod.interfaces.push(x); });
                break;
            case ts.SyntaxKind.FunctionDeclaration:
                mod.methods.push(getMethod(node, { static: true }));
                break;
            case ts.SyntaxKind.ModuleDeclaration:
                var m_1 = visitModule(node, { path: modPath });
                var isEmpty = Object.keys(m_1).every(function (k) { return !Array.isArray(m_1[k]) || m_1[k].length === 0; });
                if (!isEmpty) {
                    mod.modules.push(m_1);
                }
                break;
            case ts.SyntaxKind.EnumDeclaration:
                mod.interfaces.push(getEnum(node));
                break;
        }
    };
}
function visitModule(node, opts) {
    opts = opts || {};
    var mod = {
        name: getName(node),
        path: opts.path,
        interfaces: [],
        properties: [],
        methods: [],
        modules: []
    };
    var modPath = joinPath(mod.path, mod.name);
    switch (node.body.kind) {
        case ts.SyntaxKind.ModuleDeclaration:
            mod.modules.push(visitModule(node.body, { path: modPath }));
            break;
        case ts.SyntaxKind.ModuleBlock:
            node.body.statements.forEach(visitModuleNode(mod, modPath));
            break;
    }
    return mod;
}
function visitFile(node) {
    var file = {
        interfaces: [],
        properties: [],
        methods: [],
        modules: []
    };
    ts.forEachChild(node, visitModuleNode(file, null));
    return {
        properties: file.properties,
        interfaces: file.interfaces,
        methods: file.methods,
        modules: mergeNamesakeModules(file.modules)
    };
}
try {
    var filePath = process.argv[2];
    if (filePath == null) {
        throw 'Please provide the path to a TypeScript definition file';
    }
    // fileName = (fileName = path.basename(filePath).replace(".d.ts",""), fileName[0].toUpperCase() + fileName.substr(1));
    // `readonly` keyword is causing problems, remove it
    var code = fs.readFileSync(filePath).toString().replace(/readonly/g, '');
    var sourceFile = ts.createSourceFile(filePath, code, ts.ScriptTarget.ES2015, /*setParentNodes*/ true);
    var fileInfo = visitFile(sourceFile);
    var ffi = printFile(fileInfo);
    console.log(ffi);
    process.exit(0);
}
catch (err) {
    console.log('ERROR: ' + err);
    process.exit(1);
}
//# sourceMappingURL=ts2fable.js.map