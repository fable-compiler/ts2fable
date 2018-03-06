var path = require("path");
var webpack = require("webpack");
var common = require("./webpack.config.common");
var fs = require('fs');

console.log("Bundling for test...");

module.exports = {
  devtool: "inline-source-map",
  entry: common.config.testEntry,
  target: "node",
  externals: common.config.nodeExternals,
  output: {
    filename: 'test.js',
    path: common.config.buildDir,
    devtoolModuleFilenameTemplate: info =>
      path.resolve(info.absoluteResourcePath).replace(/\\/g, '/'),
  },
  module: {
    rules: common.getModuleRules()
  },
  resolve: {
    modules: [common.config.nodeModulesDir]
  },
};
