const path = require("path");
const fableUtils = require("fable-utils");

function resolve(filePath) {
  return path.resolve(__dirname, filePath)
}

var outFile = resolve("test/bin/test.js");

module.exports = {
  entry: resolve("test/test.fsproj"),
  outDir: path.dirname(outFile),
  babel: fableUtils.resolveBabelOptions({
    plugins: ["transform-es2015-modules-commonjs"]
  }),
  fable: { define: ["DEBUG"] }
};