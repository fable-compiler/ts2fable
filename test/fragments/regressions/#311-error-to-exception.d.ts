
export module ErrorExceptionTest {
    // should be System.Exception
    var instanceErrorProperty: Error;

    // should be replaced with an alias to System.Exception, "i" will not be mapped.
    interface InheritFromError extends Error {
        i : number
    }

    var instanceInheritFromErrorProperty: InheritFromError;
}

