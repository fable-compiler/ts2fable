type Validator<T> = { bivarianceHack(object: T, key: string, componentName: string, ...rest: any[]): Error | null }["bivarianceHack"];