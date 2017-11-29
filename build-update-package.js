// remove scripts from package.json
const fs = require('fs');
var json = require('./package.json');
json.scripts = undefined;
json.engines = undefined;
fs.writeFileSync("./package.json", JSON.stringify(json, undefined, 2));