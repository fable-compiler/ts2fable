// remove scripts from package.json
import path from "path"
import fs from "fs"
import url from 'url';
const __dirname = path.dirname(url.fileURLToPath(import.meta.url));

const jsonPath = path.resolve(__dirname, "../package.json");

// https://nodejs.org/api/module.html#module_module_createrequire_filename
import { createRequire } from 'module';
const require = createRequire(import.meta.url);
var json = require(jsonPath);
json.scripts = undefined;
json.devDependencies = undefined;
fs.writeFileSync(jsonPath, JSON.stringify(json, undefined, 2));