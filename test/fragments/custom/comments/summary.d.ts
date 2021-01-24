/**
 * Single line main summary
 */
export interface A {}
/**
 * Multi line
 * main summary
 */
export interface B {}

/**
 * @summary jsDoc single line summary tag
 */
export interface C {}
/**
 * @summary 
 * jsDoc multi line
 * summary tag
 */
export interface D {}

/**
 * @description jsDoc single line description tag
 */
export interface E {}
/**
 * @description 
 * jsDoc multi line
 * description tag
 */
export interface F {}

// -> merge summaries & description
/**
 * Main summary
 * 
 * @summary jsDoc summary tag
 * @description jsDoc description tag
 */
export interface G {}

// -> summary tag instead of no tags because of another tag
/**
 * Single line main summary
 * 
 * @remarks jsDoc remarks tag
 */
export interface H {}

/**
 * Multi line 
 * main summary
 * 
 * @remarks jsDoc remarks tag
 */
export interface J {}

// -> merge summary & description and put in front
/**
 * Main summary
 * 
 * @remarks jsDoc remarks tag
 * @description jsDoc description tag
 */
export interface K {}

// -> put summary in front
/**
 * @remarks jsDoc remarks tag
 * @summary jsDoc description tag
 */
export interface L {}