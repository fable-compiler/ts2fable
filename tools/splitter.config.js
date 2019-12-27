const path = require("path");
// const fableUtils = require("fable-utils");
var common = require("./webpack.config.common");

function resolve(filePath) {
  return path.resolve(__dirname, filePath)
}


module.exports = {
  entry: common.config.cliEntry,
  outDir: common.config.distDir,
  babel: {
    plugins: ["@babel/plugin-transform-modules-commonjs"],
  },
}