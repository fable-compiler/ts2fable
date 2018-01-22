import { Validator } from "react";

type ValidationMap<T> = {[K in keyof T]?: Validator<T> };
