// remove scripts from package.json
const fs = require('fs');
const path = require("path");
const jsonPath = path.resolve(__dirname, "../package.json");
var json = require(jsonPath);
json.scripts = undefined;
json.engines = undefined;
fs.writeFileSync(jsonPath, JSON.stringify(json, undefined, 2));