import { ComponentLifecycle, ComponentClass, StatelessComponent } from "react";
interface Component<P = {}, S = {}> extends ComponentLifecycle<P, S> { }