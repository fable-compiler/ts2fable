export function fIndexerOut(): {
    [key: string]: string;
};
export function fIndexerIn(i: {
    [key: string]: string;
}): void;
export function fIndexerIn2(i: {
    [key: string]: string;
    [index: number]: string;
}): void;
/**
 * Some description
 * 
 * @remarks some remarks
 */
export function fIndexerIn3WithComments(i: {
    [key: string]: string;
}): void;