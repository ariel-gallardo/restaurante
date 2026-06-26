
import { createConfig } from '@nx/angular-rspack';
import { container } from '@rspack/core';

const { ModuleFederationPlugin } = container;

export default createConfig({
  options: {
    root: __dirname,

    "outputPath": {
      "base": "../../dist/apps/restaurante-v2"
    },
    "index": "./src/index.html",
    "browser": "./src/main.ts",
    "tsConfig": "./tsconfig.app.json",
    "inlineStyleLanguage": "scss",
    "assets": [
      {
        "glob": "**/*",
        "input": "./public"
      }
    ],
    "styles": [
      "./src/styles.scss"
    ],
    "devServer": {
      "historyApiFallback": true,
      "proxy": [
        {
          "context": [
            "/views",
            "/components/views",
            "/assets",
            "/bootstrap",
            "/bundle.js"
          ],
          "target": "http://localhost:4201",
          "secure": false,
          "changeOrigin": true
        }
      ]
    }

  },
  "rspackConfigOverrides": {
    "plugins": [
      new ModuleFederationPlugin({
        "name": "restaurante_v2",
        "remotes": {
          "v1": "v1@http://localhost:4201/remoteEntry.js"
        },
        "shared": {}
      })
    ]
  }
}, {
  "production": {
    options: {

      "budgets": [
        {
          "type": "initial",
          "maximumWarning": "500kb",
          "maximumError": "1mb"
        },
        {
          "type": "anyComponentStyle",
          "maximumWarning": "4kb",
          "maximumError": "8kb"
        }
      ],
      "outputHashing": "all"

    }
  },

  "development": {
    options: {

      "optimization": false,
      "vendorChunk": true,
      "extractLicenses": false,
      "sourceMap": true,
      "namedChunks": true

    }
  }
});