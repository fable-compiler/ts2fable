
// ``
export interface O1<T extends {}> {}
// ``
export interface O2<T extends { new (): T }> {}
// ``
export interface O3<T, U extends { new (...args: any[]): T }> {}
// ``
export interface O4<T extends { value: true}> {}

// ``
export interface O5<T extends "foo"> {}
