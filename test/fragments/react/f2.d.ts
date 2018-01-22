import { ComponentLifecycle } from "react";

interface Component<P = {}, S = {}> extends ComponentLifecycle<P, S> { }
