module.exports = function () {
    var src = "./Src/";
    var config = {
        js: src + ["**/*.js"],
        css:src+ ["**/*.css"],
        html: src + ["**/*.html"],
        dist:'./dist',
        temp:'./temp',
        bowerJson:'./bower.json',
        index: src + "index.html",
        bower: {
            json: require('./bower.json'),
            directory: 'bower_components/',
            ignorePath: '../..'
        },
        root:src,
    };
    config.getWiredepDefaultOptions = function () {
        var options = {
            bowerJson: config.bower.json,
            directory: config.bower.directory,
            ignorePath: config.bower.ignorePath
        };
        return options;
    };
    return config;
}