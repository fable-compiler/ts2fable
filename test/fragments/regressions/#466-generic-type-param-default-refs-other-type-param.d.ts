export interface Node {
  readonly kind: string
  //...
}
export type VisitResult<T extends Node | undefined> = T | readonly Node[]
/**
 * in F#: 3 `Visitor`s: with 2 generic type params, with 1 type param, without type param
 */
export type Visitor<TIn extends Node = Node, TOut extends Node | undefined = TIn | undefined> = (node: TIn) => VisitResult<TOut>;

export interface W<T> {}
export interface A<TA extends Node = Node, TB extends W<TA> = W<TA>> {}
/**
 * Note: This isn't valid in F#: `'TB :> 'TA` is not allowed!:
 * ```fsharp
 * type E<'TA, 'TB when 'TA :> Node and 'TB :> 'TA> = interface end
 * //                                   ^^^^^^^^^^ Invalid constraint: the type used for the constraint is sealed, which means the constraint could only be satisfied by at most one solution
 * //                                   ^^^^^^^^^^ This construct causes code to be less generic than indicated by the type annotations. The type variable 'TB has been constrained to be type ''TA'.
 * //          ^^^ This type parameter has been used in a way that constrains it to always be '#Node'
 * ```
 */
export interface B<TA extends Node = Node, TB extends TA = TA> {}

export interface C<TA extends Node = Node, TB extends W<TA> = W<TA>, TC extends W<TB> = W<TB>, TD extends W<TB> = W<TB>> {}
export interface D<TA extends Node = Node, TB extends TA[] = TA[], TC extends W<TB> | undefined = W<TB>> {}