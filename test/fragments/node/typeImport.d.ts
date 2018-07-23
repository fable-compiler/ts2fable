declare module "querystring" {
}
declare module "url" {
    import { ParsedUrlQuery } from 'querystring';
}
declare module "http" {
    import { URL } from "url";
}