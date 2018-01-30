import { ComponentLifecycle, ReactNode, ReactInstance } from "react";

   // Base component for plain JS classes
    // tslint:disable-next-line:no-empty-interface
declare class Component<P, S> {
    constructor(props: P, context?: any);
    state: Readonly<S>;
    defaultProps?: Partial<P>;
}