declare namespace React {
    type Ref<T> = string | { bivarianceHack(instance: T | null): any }["bivarianceHack"];
}
