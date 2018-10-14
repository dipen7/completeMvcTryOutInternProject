var path = require('path');
module.exports = {
    entry: './scripts/index.js',
    output: {
        filename: './scripts/dist/bundle.js'
    },
    devtool: 'source-map',
    module: {
        loaders: [
            {
                test: /\.js$/,
                exclude: /node_modules/,
                loader: 'babel-loader',
                query: {
                    presets: ['es2015', 'stage-0']
                },
                include: [path.resolve(__dirname, '/node_modules/vue-datatable/src')]
                //node_modules/vue - datatable / src
                
            },
            //{
            //    test: /\.svg$/,
            //    loader: 'vue-svg-loader', // `vue-svg` for webpack 1.x
            //    options: {
            //        svgo: {
            //            plugins: [
            //                { removeDoctype: true },
            //                { removeComments: true }
            //            ]
            //        }
            //    }
            //}
            {
                test: /\.svg$/,
                use: [
                    {
                        loader: "babel-loader"
                    },
                    {
                        loader: "react-svg-loader",
                        options: {
                            jsx: true // true outputs JSX tags
                        }
                    }
                ]
            },
            //,svg loader
            {
                test: /\.vue$/,
                
                loader: 'vue-loader',
                options: {
                    loaders: {
                        js: 'babel-loader!es2015'
                    }
                },
                include: [path.resolve(__dirname, '/node_modules/vue-datatable/src')]
            }
        ]
            //test: /\.vue$/,
            //loader: 'vue-loader',
            //options: {
            //    loaders: {
            //        js: 'babel-loader!eslint-loader'
            //    }
            //}
        
    }
};