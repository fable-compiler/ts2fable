
export declare class DoubleKeyMap<TKey1, TKey2, TValue> {
    private readonly _map;
    set({ key1, key2 }: {
        key1: TKey1;
        key2: TKey2;
    }, value: TValue): void;
    get({ key1, key2 }: {
        key1: TKey1;
        key2: TKey2;
    }): TValue | undefined;
    delete({ key1, key2 }: {
        key1: TKey1;
        key2: TKey2;
    }): void;
}