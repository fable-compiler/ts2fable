import { ComponentLifecycle, ComponentClass, StatelessComponent } from "react";
declare namespace React {
    interface Component<P = {}, S = {}> extends ComponentLifecycle<P, S> { }
    type ComponentType<P = {}> = ComponentClass<P> | StatelessComponent<P>;

}
