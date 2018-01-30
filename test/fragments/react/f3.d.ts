import { HTMLAttributes, ClassAttributes } from "react";

type DetailedHTMLProps<E extends HTMLAttributes<T>, T> = ClassAttributes<T> & E;
