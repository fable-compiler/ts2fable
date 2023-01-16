/**
 * `myTest1` & `myTest2` & `myTest4` should get reduced to just one `member` each
 * 
 * all others should be transformed to unit parameter with value in emit & name
 */
export declare namespace ns {
  function myTest1(v: any): v is number
  function myTest1(v: any): v is number | string

  function myTest2(v: any, condition: false): v is number
  function myTest2(v: any, condition: boolean): v is number | string

  function myTest3(v: "hello"): false
  function myTest3(v: "world"): true

  function myTest4(v: any): false
  function myTest4(v: any): true

  function myTest5(v: "hello"): boolean
  function myTest5(v: "world"): boolean

  function myTest6(v: "hello", s: "world"): boolean
  function myTest6(v: "world", s: "hello"): boolean

  function myTest7(v: "hello"): "world"
  function myTest7(v: "world"): "hello"

  function myTest8(v: 3.14)
  function myTest8(v: 2.72)
  
  function myTest9(v: 42)
  function myTest9(v: 123)

  function myTest10(v: false)
  function myTest10(v: true)

  function myTest11(a: string, b: 42, c: 123)
  function myTest11(a: string, b: "foo", c: 3.14)
}