const path = require("path");
const webpack = require("webpack");
const HtmlWebpackPlugin = require('html-webpack-plugin');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");


function resolve(filePath) {
    return path.join(__dirname, filePath)
}

var babelOptions = {
    presets: [
        // ["@babel/preset-env", {
        //     "targets": {
        //         "browsers": ["last 2 versions"]
        //     },
        //     "modules": false
        // }],
        "@babel/preset-react",
    ],
    plugins: [
        "@babel/plugin-proposal-class-properties"
    ]
};

var commonPlugins = [
    new HtmlWebpackPlugin({
        filename: resolve('./output/index.html'),
        template: resolve('index.html')
    })
];

module.exports = (env, argv) => {
    var isProduction = argv.mode == "production"
    console.log("Bundling for " + (isProduction ? "production" : "development") + "...");

    return {
        devtool: isProduction ? false : "source-map",
        mode: isProduction ? "production" : "development",
        entry: isProduction ? // We don't use the same entry for dev and production, to make HMR over style quicker for dev env
            {
                app: [
                    "@babel/polyfill",
                    resolve("./temp/App.js"),
                    resolve('./sass/main.scss')
                ]
            } : {
                app: [
                    "@babel/polyfill",
                    resolve("./temp/App.js")
                ],
                style: [
                    resolve('./sass/main.scss')
                ]
            },
        output: {
            path: resolve('./output'),
            filename: isProduction ? '[name].[hash].js' : '[name].js'
        },
        optimization: {
            splitChunks: {
                cacheGroups: {
                    commons: {
                        test: /[\\/]node_modules[\\/]/,
                        name: 'vendor',
                        chunks: 'all'
                    },
                    fable: {
                        test: /[\\/]fable-core[\\/]/,
                        name: 'fable',
                        chunks: 'all'
                    }
                }
            }
        },
        plugins: isProduction ?
            commonPlugins.concat([
                new MiniCssExtractPlugin({
                    filename: 'style.[hash].css'
                }),
                // ensure that we get a production build of any dependencies
                // this is primarily for React, where this removes 179KB from the bundle
                new webpack.DefinePlugin({
                    'process.env.NODE_ENV': '"production"'
                })
            ])
            : commonPlugins.concat([
                new webpack.HotModuleReplacementPlugin(),
                new webpack.NamedModulesPlugin()
            ]),
        resolve: {
            modules: [
                "node_modules/",
                resolve("./../node_modules/")
            ]
        },
        devServer: {
            contentBase: resolve('./output/'),
            publicPath: "/",
            port: 8080,
            hot: true,
            inline: true
        },
        module: {
            rules: [
                {
                    test: /\.js$/,
                    enforce: "pre",
                    use: ["source-map-loader"],
                },
                {
                    test: /\.js$/,
                    exclude: /node_modules/,
                    use: {
                        loader: 'babel-loader',
                        options: babelOptions
                    },
                },
                {
                    test: /\.(sass|scss|css)$/,
                    use: [
                        isProduction ? MiniCssExtractPlugin.loader : 'style-loader',
                        'css-loader',
                        'sass-loader',
                    ],
                },
                {
                    test: /\.(png|jpg|jpeg|gif|svg|woff|woff2|ttf|eot)(\?.*$|$)/,
                    use: ["file-loader"]
                }
            ]
        }
    }
};
