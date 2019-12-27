var fs = require("fs");
var path = require("path");
var fableUtils = require("fable-utils");
// var DynamicCdnWebpackPlugin = require('dynamic-cdn-webpack-plugin');

var errorMsg = "{0} missing in package.json";

var nodeModulesDir = resolve("../node_modules");

var nodeExternals = {};
fs.readdirSync(nodeModulesDir)
  .filter(function(x) {
    return ['.bin'].indexOf(x) === -1;
  })
  .forEach(function(mod) {
    nodeExternals[mod] = 'commonjs ' + mod;
  });

var config = {
  nodeExternals: nodeExternals,
  testEntry: resolve("../test/test.fsproj"),
  cliEntry: resolve("../src/ts2fable.fsproj"),
  publicDir: resolve("../public"),
  buildDir: resolve("../build"),
  distDir: resolve("../dist"),
  jsonPath: resolve('../package.json'),
  nodeModulesDir: nodeModulesDir,
}



function resolve(filePath) {
  return path.join(__dirname, filePath)
}

function forceGet(obj, path, errorMsg) {
  function forceGetInner(obj, head, tail) {
    if (head in obj) {
      var res = obj[head];
      return tail.length > 0 ? forceGetInner(res, tail[0], tail.slice(1)) : res;
    }
    throw new Error(errorMsg.replace("{0}", path));
  }
  var parts = path.split('.');
  return forceGetInner(obj, parts[0], parts.slice(1));
}

function getModuleRules(isProduction) {
  var babelOptions = fableUtils.resolveBabelOptions({
    presets: [
      ["@babel/preset-env", { "targets": { "browsers": "> 1%" }, "modules": false }]
    ],
  });

  return [
    {
      test: /\.fs(x|proj)?$/,
      use: {
        loader: "fable-loader",
        options: {
          babel: babelOptions,
          define: isProduction ? [] : ["DEBUG"]
        }
      }
    },
    {
      test: /\.js$/,
      exclude: /node_modules/,
      use: {
        loader: 'babel-loader',
        options: babelOptions
      },
    }
  ];
}

function getPlugins(isProduction) {
  return [];
}

module.exports = {
  resolve: resolve,
  config: config,
  getModuleRules: getModuleRules,
  getPlugins: getPlugins
}
