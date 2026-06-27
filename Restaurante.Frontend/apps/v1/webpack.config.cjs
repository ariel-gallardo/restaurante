const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const CopyWebpackPlugin = require('copy-webpack-plugin');
const Dotenv = require('dotenv-webpack');
const {DefinePlugin, ProvidePlugin} = require('webpack');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const glob = require('glob');
const fs = require('fs');
const ModuleFederationPlugin = require('webpack/lib/container/ModuleFederationPlugin');


module.exports = {
  entry: [
    './src/index.js',
    ...glob.sync('./src/**/*.js').filter(f => !f.endsWith('index.js')),
    ...glob.sync('./src/**/*.ts')
  ],
  devtool: 'source-map',
  mode: 'development',
  output: {
    path: path.resolve(__dirname, 'dist'),
    filename: 'bundle.js',
  },
  module: {
    rules: [
      {
        test: /\.js$/,
        enforce: 'pre',
        use: ['source-map-loader'],
        exclude: /node_modules/,
      },
      {
        test: /\.ts$/,
        use: {
          loader: 'ts-loader',
          options: {
            transpileOnly: true,
            compilerOptions: {
              sourceMap: true,
            }
          }
       },
        exclude: [/node_modules/],
      },
      {
        test: /\.js$/,
        exclude: /node_modules/,
        use: {
          loader: 'babel-loader',
          options: {
            plugins: ['lodash'],
            presets: ['@babel/preset-env'],
            sourceType: 'module',
          },
        },
        resolve:{
          fullySpecified: false
        }
      },
      {
        test: /\.css$/,
        use: [MiniCssExtractPlugin.loader, 'css-loader'],
      },
      {
        test: /\.html5$/,
        use: ['html-loader']
      }
    ],
  },
  devServer: {
    static: {
      directory: path.join(__dirname, 'dist'),
    },
    headers: {
      'Access-Control-Allow-Origin': 'http://localhost:4200',
      'Access-Control-Allow-Methods': 'GET, POST, PUT, DELETE, PATCH, OPTIONS',
      'Access-Control-Allow-Headers': 'X-Requested-With, content-type, Authorization, GroupId'
    },
    port: 4201,
    server:{
      type: 'http',
      /*options:{
        key: fs.readFileSync(path.resolve(__dirname, 'certs', 'key.pem')),
        cert: fs.readFileSync(path.resolve(__dirname, 'certs', 'cert.pem'))
      }*/
    },
    open: false,
    historyApiFallback: {
      rewrites: [
        { from: /\/assets\/.*\.(css)/, to: '/assets/styles/empty.css' }
      ]
    },
    hot: true,
  },
  plugins: [
    new ProvidePlugin({
      $: 'jquery',
      jQuery: 'jquery',
      signalR: '@microsoft/signalr'
    }),
    new DefinePlugin({
      'window': 'window'
    }),
    new MiniCssExtractPlugin({
      filename: 'assets/styles/bundle.css',
    }),
    new Dotenv(),
    new HtmlWebpackPlugin({
      template: './src/index.html',
    }),
    new CopyWebpackPlugin({
      patterns: [
        {
          from: path.resolve(__dirname, 'public'),
          to: path.resolve(__dirname, 'dist'),
          globOptions: {
            glob: '**/*.{css,jpg,jpeg,png,svg,gif,mp3,wav,ogg}',
          }
        },
        {
          from: path.resolve(__dirname, 'src/components/views'),
          to: path.resolve(__dirname, 'dist/components/views'),
          globOptions: {
            glob: '**/*.html5',
          }
        },
        {
          from: path.resolve(__dirname, 'src/views'),
          to: path.resolve(__dirname, 'dist/views'),
          globOptions: {
            glob: '**/*.html5',
          }
        },
        {
          from: path.resolve(__dirname, 'node_modules/bootstrap/dist'),
          to: path.resolve(__dirname, 'dist/bootstrap')
        },
      ],
    }),
    new ModuleFederationPlugin({
      name: "v1",
      filename: "remoteEntry.js",
      exposes: {
        "./main": "./src/index.js",
      },
      shared: {},
    })
  ],
  resolve:{
    extensions: ['.js', '.ts', '.css'],
    alias:{
      '@components': path.resolve(__dirname, 'src/components/'),
      '@componentsView': path.resolve(__dirname, 'src/components/views'),
      '@controllers': path.resolve(__dirname, 'src/controllers/'),
      '@models': path.resolve(__dirname, 'src/models/'),
      '@modules': path.resolve(__dirname, 'src/modules/'),
      '@services': path.resolve(__dirname, 'src/services/'),
      '@views': path.resolve(__dirname, 'src/views/'),
      '@routes': path.resolve(__dirname, 'src/routes/'),
      '@interceptors': path.resolve(__dirname, 'src/interceptors/'),
      '@filters': path.resolve(__dirname, 'src/filters/'),
      '@queries': path.resolve(__dirname, 'src/queries/'),
      '@store': path.resolve(__dirname, 'src/store/'),
      '@org/shared-shell': path.resolve(__dirname, '../../libs/shared/src/lib/shell/src/index.ts')
    }
  }
};
