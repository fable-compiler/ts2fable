import { Component, ComponentState, ComponentClass } from "react";

type ClassType<P, T extends Component<P, ComponentState>, C extends ComponentClass<P>> =
C &
(new (props?: P, context?: any) => T) &
(new (props?: P, context?: any) => { props: P });