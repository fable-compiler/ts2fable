
declare namespace React {
    class Component<P, S> {
        setState<K extends keyof S>(
            state: ((prevState: Readonly<S>, props: P) => (Pick<S, K> | S)) | (Pick<S, K> | S),
            callback?: () => any
        ): void;
    }
}

interface StatelessComponent<P = {}> {
    defaultProps?: Partial<P>;
}