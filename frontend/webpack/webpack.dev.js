var loaders = require("./loaders");
var BrowserSyncPlugin = require('browser-sync-webpack-plugin');
var HtmlWebpackPlugin = require('html-webpack-plugin');
var webpack = require('webpack');
var path = require('path');
var historyApiFallback = require('connect-history-api-fallback')

module.exports = {
    entry: ['./src/app.ts'],
    output: {
        filename: 'build.js',
        path: path.resolve(__dirname, '..', 'dist'),
    },
    resolve: {
        extensions: ['.ts', '.js', '.json']
    },
    resolveLoader: {
        modules: ["node_modules"]
    },
    devtool: "inline-eval-cheap-source-map",
    plugins: [
        new HtmlWebpackPlugin({
            template: './src/index.html',
            inject: 'body',
            hash: true
        }),
        new BrowserSyncPlugin({
            host: 'localhost',
            port: 6002,
            server: {
                baseDir: './dist'
            },
            ui: false,
            online: false,
            notify: false,
            middleware: [ historyApiFallback()],
        }),
    ],
    module: {
        loaders: loaders
    }
};