type A<T = string> = {}
type B<T = string, K = number> = {}
type C<T = {}> = {}
type D<T = string | number> = {}
type E<T = string & number> = {}
type F<T = A<string>> = {}