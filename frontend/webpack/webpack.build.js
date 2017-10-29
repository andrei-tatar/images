var loaders = require('./loaders');
var HtmlWebpackPlugin = require('html-webpack-plugin');
var webpack = require('webpack');
var path = require('path');

module.exports = {
    entry: ['./src/app.ts'],
    output: {
        filename: 'build.js',
        path: path.resolve(__dirname, '..', 'dist'),
    },
    devtool: 'source-map',
    resolve: {
        extensions: ['.ts', '.js', '.json']
    },
    resolveLoader: {
        modules: ['node_modules']
    },
    plugins: [
        new webpack.optimize.UglifyJsPlugin(
            {
                warning: false,
                mangle: true,
                comments: false
            }
        ),
        new HtmlWebpackPlugin({
            template: './src/index.html',
            inject: 'body',
            hash: true
        }),
    ],
    module: {
        loaders: loaders
    },
};