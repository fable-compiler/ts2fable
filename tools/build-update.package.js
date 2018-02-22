// remove scripts from package.json
const fs = require('fs');
var common = require("./webpack.config.common");
var json = require(common.config.jsonPath);
json.scripts = undefined;
json.engines = undefined;
fs.writeFileSync(common.config.jsonPath, JSON.stringify(json, undefined, 2));