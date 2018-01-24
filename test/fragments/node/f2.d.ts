import { ExecOptions } from "child_process";

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace exec {
        export function __promisify__(command: string): Promise<{ stdout: string, stderr: string }>;
        export function __promisify__(command: string, options: { encoding: "buffer" | null } & ExecOptions): Promise<{ stdout: Buffer, stderr: Buffer }>;
        export function __promisify__(command: string, options: { encoding: BufferEncoding } & ExecOptions): Promise<{ stdout: string, stderr: string }>;
        export function __promisify__(command: string, options: ExecOptions): Promise<{ stdout: string, stderr: string }>;
        export function __promisify__(command: string, options?: ({ encoding?: string | null } & ExecOptions) | null): Promise<{ stdout: string | Buffer, stderr: string | Buffer }>;
    }