declare namespace ns1 {
    namespace ns2 {
        interface SomeInterface {
            /**
             * Some Value
             * 
             * @deprecated 
             * Value is deprecated
             * 
             * The reason spans multiple lines,  
             * which might lead to incorrect indentation  
             * when last line is shorter than indentation
             * 
             * !
             */
            Alpha: number

            /**
             * Some Value
             * 
             * @deprecated 
             * Value is deprecated
             * 
             * A long last line doesn't need any additional spaces
             */
            Beta: number

            /**
             * Some Value
             * 
             * @deprecated 
             * Value is deprecated. And a single line does never need spaces
             */
            Gamma: number
        }
    }
}