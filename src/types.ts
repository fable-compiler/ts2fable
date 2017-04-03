
export interface Module {
  name: any
  path: string
  interfaces: any[]
  properties: any[],
  methods: any[],
  modules: any[]
}

export interface MemberOptions {
  name: any
  type: string
  kind: any
  optional: boolean
  static: boolean
  parameters: any[]
  emit: any
  anonymous: boolean
  path: string
}

export interface InterfaceOptions {
  name: any
  kind: string | ts.SyntaxKind
  parents: ts.NodeArray<ts.Node> | any
  properties: Partial<MemberOptions>[]
  methods: Partial<MemberOptions>[]
  constructorParameters: any
  anonymous: boolean
  path: any
}
