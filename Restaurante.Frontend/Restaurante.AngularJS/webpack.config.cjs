const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const CopyWebpackPlugin = require('copy-webpack-plugin');

module.exports = {
  entry: './src/index.js',
  output: {
    path: path.resolve(__dirname, 'dist'),
    filename: 'bundle.js',
  },
  module: {
    rules: [
      {
        test: /\.js$/,
        exclude: /node_modules/,
        use: {
          loader: 'babel-loader',
          options: {
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
        use: ['style-loader', 'css-loader'],
      },
      {
        test: /\.scss$/,
        use: ['style-loader', 'css-loader', 'sass-loader'],
      },
      {
        test: /\.html5$/,
        use: ['html-loader']
      },
    ],
  },
  devServer: {
    static: {
      directory: path.join(__dirname, 'dist'),
    },
    port: 8080,
    open: true,
    historyApiFallback: true,
    hot: true
  },
  plugins: [
    new HtmlWebpackPlugin({
      template: './src/index.html',
    }),
    new CopyWebpackPlugin({
      patterns: [
        {
          from: path.resolve(__dirname, 'public'),
          to: path.resolve(__dirname, 'dist'),
          globOptions: {
            glob: '**/*.{css,jpg,jpeg,png,svg,gif,mp3,wav,ogg}'
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
    })
  ],
  resolve:{
    extensions: ['.js'],
    alias:{
      '@components': path.resolve(__dirname, 'src/components/'),
      '@componentsView': path.resolve(__dirname, 'src/components/views'),
      '@controllers': path.resolve(__dirname, 'src/controllers/'),
      '@models': path.resolve(__dirname, 'src/models/'),
      '@modules': path.resolve(__dirname, 'src/modules/'),
      '@services': path.resolve(__dirname, 'src/services/'),
      '@views': path.resolve(__dirname, 'src/views/'),
      '@routes': path.resolve(__dirname, 'src/routes/')
    }
  }
};
