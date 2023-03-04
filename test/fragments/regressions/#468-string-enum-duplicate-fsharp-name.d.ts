/**
 * Keep 1st case
 */
export type A =
  | "utf8"
  | "utf-8"

/**
 * Keep 2nd case
 */
export type B =
  | "utf-8"
  | "utf8"

export type C =
  | "utf8"
  | "Utf8"
  | "UTF8"
  | "utf-8"
  | "UTF-8"


export type BufferEncoding = "ascii" | "utf8" | "utf-8" | "utf16le" | "ucs2" | "ucs-2" | "base64" | "latin1" | "binary" | "hex";
