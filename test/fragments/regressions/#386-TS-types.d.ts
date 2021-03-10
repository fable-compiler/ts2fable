interface I {
    a(v: PromiseLike<string>): PromiseLike<number>;
    b(v: ArrayLike<string>): ArrayLike<number>;
}