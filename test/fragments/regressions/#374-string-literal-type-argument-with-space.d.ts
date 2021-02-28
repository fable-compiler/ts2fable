// `☕` is recognized as `Char.IsLetterOrDigit`
// Fixed in Fable 3
// -> //todo: uncomment and adjust this test once upgrade to fable 3 is merged 

interface A {
    on(event: "Hello Space"): void;
    on(event: "Hello	Tab"): void;
    on(event: "Hello Multiple Words"): void;
    on(event: "Hello   Multiple Spaces"): void;
    // on(event: "Hello☕Invalid"): void;
    // on(event: "Hello ☕ Invalid With Spaces"): void;
    on(event: "(╯°□°）╯︵ ┻━┻"): void;
    on(event: "post-require"): void;
}

declare type S = 
    | "Foo" 
    | "Hello World" 
    // | "Hello☕"