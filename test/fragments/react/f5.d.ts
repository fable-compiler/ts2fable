

export = React;

declare namespace React {
    
    type a = JSX.ElementAttributesProperty
}

declare global {
    namespace JSX {
        // tslint:disable-next-line:no-empty-interface
        interface ElementAttributesProperty { props: {}; }
    }
}

