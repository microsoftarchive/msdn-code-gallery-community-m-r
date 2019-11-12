/* global __dirname: false */
/* global webpack.ProvidePlugin: false */

const webpack = require('webpack');
const path = require('path');
const merge = require('webpack-merge');

module.exports = () => {

    const sharedConfig = () => ({
        mode: 'development',
        resolve: {
            modules: ['node_modules', path.resolve(__dirname,'ClientApp')],
            extensions: ['.js', '.jsx', '.json', '.css', '.sass']
        }
    });

    const clientBundleConfig = merge(sharedConfig(),
        {
            devtool: 'source-map',
            target: 'web',
            entry: {
                'main': './ClientApp/client.jsx'
            },
            output: {
                path: path.resolve(__dirname, 'wwwroot/dist/'),
                publicPath: '/dist/',
                filename: '[name].bundle.js',
                chunkFilename: '[name].bundle.js' 
            },
            module: {
                rules: [
                    {
                        test: /.jsx?$/,
                        include: [path.resolve(__dirname, 'ClientApp')],
                        loader: 'babel-loader',
                        options: {
                            presets: [
                                '@babel/env', 
                                '@babel/flow',
                                '@babel/react' 
                            ],
                            plugins: [
                                '@babel/plugin-proposal-object-rest-spread', 
                                '@babel/plugin-syntax-dynamic-import', 
                                '@babel/plugin-proposal-class-properties', 
                                ['@babel/plugin-proposal-decorators', { decoratorsBeforeExport: false }] 
                            ]
                        }
                    },
                    {
                        test: /\.css$/,
                        use: [
                            { loader: 'style-loader' },
                            {
                                loader: 'css-loader',
                                options: {
                                    sourceMap: true
                                }
                            }
                        ]
                    },
                    {
                        test: /\.scss$/,
                        use: [
                            { loader: 'style-loader' }, 
                            {
                                loader: 'css-loader',
                                options: {
                                    sourceMap: true
                                }
                            }, 
                            {
                                loader: 'sass-loader',
                                options: {
                                    sourceMap: true
                                }
                            } 
                        ]
                    },
                    {
                        test: /\.png$/,
                        loader: 'url-loader',
                        options: {
                            outputPath: 'images/',
                            mimetype: 'image/png',
                            limit: 100000
                        }
                    },
                    {
                        test: /\.svg(\?v=\d+\.\d+\.\d+)?$/,
                        loader: 'url-loader',
                        options: {
                            name: '[name].[ext]',
                            outputPath: '../fonts/',
                            mimetype: 'image/svg+xml',
                            limit: 10000
                        }
                    },
                    {
                        test: /\.ttf(\?v=\d+\.\d+\.\d+)?$/,
                        loader: 'url-loader',
                        options: {
                            name: '[name].[ext]',
                            outputPath: '../fonts/',
                            mimetype: 'application/octet-stream',
                            limit: 10000
                        }
                    },
                    {
                        test: /\.(woff|woff2)(\?v=\d+\.\d+\.\d+)?$/,
                        loader: 'url-loader',
                        options: {
                            name: '[name].[ext]',
                            outputPath: '../fonts/',
                            mimetype: 'application/font-woff',
                            limit: 10000
                        }
                    },
                    {
                        test: /\.(eot)$/,
                        loader: 'file-loader',
                        options: {
                            name: '[name].[ext]',
                            outputPath: '../fonts/',
                            limit: 10000
                        }
                    },
                    {
                        test: /\.(jpg)$/,
                        loader: 'file-loader',
                        options: {
                            outputPath: 'images/'
                        }
                    }
                ]
            },
            plugins: [
                new webpack.ProvidePlugin({
                    $: 'jquery',
                    jQuery: 'jquery',
                    'window.jQuery': 'jquery'
                })
            ]
        });

    const serverBundleConfig = merge(sharedConfig(),
        {
            target: 'node',
            entry: { 'server': './ClientApp/server.jsx' },
            output: {
                libraryTarget: 'commonjs',
                path: path.resolve(__dirname, 'ClientApp/dist/'),
                publicPath: '/ClientApp/dist/',
                filename: '[name].bundle.js',
                chunkFilename: '[name].bundle.js'
            },
            module: {
                rules: [
                    {
                        test: /.jsx?$/,
                        include: [path.resolve(__dirname, 'ClientApp')],
                        loader: 'babel-loader',
                        options: {
                            presets: [
                                '@babel/env', 
                                '@babel/flow',
                                '@babel/react' 
                            ],
                            plugins: [
                                '@babel/plugin-proposal-object-rest-spread', 
                                '@babel/plugin-syntax-dynamic-import', 
                                '@babel/plugin-proposal-class-properties', 
                                ['@babel/plugin-proposal-decorators', { decoratorsBeforeExport: false }] 
                            ]
                        }
                    },
                    {
                        test: /\.css$/,
                        use: [
                            {
                                loader: 'css-loader',
                                options: {
                                    sourceMap: true
                                }
                            }
                        ]
                    },
                    {
                        test: /\.scss$/,
                        use: [
                            { loader: 'isomorphic-style-loader' },
                            {
                                loader: 'css-loader',
                                options: {
                                    sourceMap: true
                                }
                            }, 
                            {
                                loader: 'sass-loader',
                                options: {
                                    sourceMap: true
                                }
                            }  
                        ]
                    },
                    {
                        test: /\.png$/,
                        loader: 'url-loader',
                        options: {
                            outputPath: 'images/',
                            mimetype: 'image/png',
                            limit: 100000
                        }
                    },
                    {
                        test: /\.svg(\?v=\d+\.\d+\.\d+)?$/,
                        loader: 'url-loader',
                        options: {
                            name: '[name].[ext]',
                            outputPath: '../fonts/',
                            mimetype: 'image/svg+xml',
                            limit: 10000
                        }
                    },
                    {
                        test: /\.ttf(\?v=\d+\.\d+\.\d+)?$/,
                        loader: 'url-loader',
                        options: {
                            name: '[name].[ext]',
                            outputPath: '../fonts/',
                            mimetype: 'application/octet-stream',
                            limit: 10000
                        }
                    },
                    {
                        test: /\.(woff|woff2)(\?v=\d+\.\d+\.\d+)?$/,
                        loader: 'url-loader',
                        options: {
                            name: '[name].[ext]',
                            outputPath: '../fonts/',
                            mimetype: 'application/font-woff',
                            limit: 10000
                        }
                    },
                    {
                        test: /\.(eot)$/,
                        loader: 'file-loader',
                        options: {
                            name: '[name].[ext]',
                            outputPath: '../fonts/',
                            limit: 10000
                        }
                    },
                    {
                        test: /\.(jpg)$/,
                        loader: 'file-loader',
                        options: {
                            outputPath: 'images/'
                        }
                    }
                ]
            },
            plugins: [

            ]
        });

    return [clientBundleConfig, serverBundleConfig];
};
