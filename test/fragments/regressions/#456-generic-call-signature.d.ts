export interface Node {}

export interface NodeVisitor {
  <T extends Node>(nodes: T)
  <T extends Node>(nodes: T, test: (node: T) => boolean)
}