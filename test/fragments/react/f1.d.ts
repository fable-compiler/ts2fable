import { ClassAttributes, ReactNode, DOMElement, HTMLAttributes, DOMAttributes, DetailedReactHTMLElement } from "react";

type DOMFactory<P extends DOMAttributes<T>, T extends Element> =
(props?: ClassAttributes<T> & P | null, ...children: ReactNode[]) => DOMElement<P, T>;

interface DetailedHTMLFactory<P extends HTMLAttributes<T>, T extends HTMLElement> extends DOMFactory<P, T> {
    (props?: ClassAttributes<T> & P | null, ...children: ReactNode[]): DetailedReactHTMLElement<P, T>;
}