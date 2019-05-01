var browserSync = require('browser-sync').create();
var historyApiFallback = require('connect-history-api-fallback');// used for angular locationprovider html5 mode enabled
var jsonServer = require('json-server');
var httpProxy = require('http-proxy-middleware');
var $ = require('gulp-load-plugins')({ lazy: true });
var gulp = require('gulp');
var config = require('./gulp.config')();
var fileServer = require("./fileServer")();


gulp.task('serve', ['inject'], function () {
    var proxy = httpProxy('/api', {
        target: 'http://localhost:21217', logLevel: 'debug',
        onProxyReq: (proxyReq, req, res) => {
            if (req.url.indexOf('file') != -1 || (req.url.indexOf('currentUser') != -1 && req.method == "POST")) {
                proxyReq.setHeader('ContentType', 'multipart/form-data;');
            } else {
                proxyReq.setHeader('Content-Type', 'application/json');
            }
            proxyReq.setHeader('X-Panel-Request', 'true');
        },
        onProxyRes: function (proxyRes, req, res) {
            if (proxyRes.statusCode != 200) {
                logRed(proxyRes.statusCode);

            }
            if (proxyRes.statusCode == 200)
                logBlue(proxyRes.statusCode)
        }

    });

    browserSync.init({
        port: 3000,
        open: true,
        files: [config.js, config.html, config.css],
        middleware: [proxy, historyApiFallback()], // used for preventing conflict angular route browser reload historyApiFallback
        ghostMode: {
            clicks: true,
            location: true,
            forms: true,
            scroll: true
        },
        injectChanges: true,
        server: {
            baseDir: config.root,
            routes: {
                "/bower_components": "bower_components",
                "/Src": "Src",
                "Html": 'Html',
                "/Uploads": "localhost:21217/Uploads"
            }
        },
    }, function (err, bs) {
        // var server = jsonServer.create()
        // server.use(jsonServer.bodyParser)
        // server.use((req, res, next) => {
        //     if (req.originalUrl == "/api/file") {
        //         log(req.body.action)
        //         fileServer.handleReq(req.body.action, res)
        //     } else {

        //         next()
        //     }
        // });
        // server.use(jsonServer.defaults())
        // server.use(jsonServer.router('db.json'))

        // // access the browserSync connect instance
        // bs.app.use('/api', server);
    });
    $.watch([config.js]).on('add', function (e) {
        logBlue("new file added")
        logBlue(e);
        gulp.start('inject');
    });

});


gulp.task('wiredep', function () {
    logBlue('Wire up the bower css js and our app js into the html');
    var wiredep = require('wiredep').stream;
    var options = config.getWiredepDefaultOptions();

    return gulp
        .src(config.index)
        .pipe(wiredep(options))
        .pipe(gulp.dest(config.root)).pipe(browserSync.stream({ once: true }));
});

gulp.task('inject', function () {
    logBlue('injecting files');
    return gulp.src(config.index)
        .pipe($.inject(gulp.src([config.js]).pipe($.angularFilesort()).pipe($.debug())))
        .pipe($.inject(gulp.src(config.css)))
        .pipe(gulp.dest(config.root));
});


gulp.task('templates', function () {
    gulp.src([config.html, '!' + config.index])
        .pipe($.htmlmin({ collapseWhitespace: true }))
        .pipe($.angularTemplatecache('templates.js', {
            module: 'app', transformUrl: function (url) {
                return '/Src/' + url
            }
        }))
        .pipe($.replace('/src/Html/HomeImages/', '/ClientApp/build/'))
        .pipe($.replace('/Src/Html/svg/', '/fonts/svg/'))
        .pipe(gulp.dest(config.temp));
});

gulp.task('cssTemp', function () {
    logRed('cssFiles :')
    gulp.src(config.css)
        .pipe($.debug())
        .pipe($.replace('/Src/Html/HomeBootstrap/fonts/', '/fonts/'))
        .pipe(gulp.dest('./build'));
});

gulp.task('bowerToTemp', function () {
    logRed('bowerFiles :')
    var filterJS = $.filter('**/*.js', { restore: true });
    return gulp.src(config.bowerJson)
        .pipe($.mainBowerFiles())
        .pipe($.debug())
        .pipe(filterJS)
        .pipe($.stripDebug())
        
        .pipe($.concat('deps.js'))
        .pipe($.uglify())
        .pipe(filterJS.restore)
        .pipe(gulp.dest(config.temp));
});

gulp.task('minifyJsTemp', function () {
    return gulp.src(config.js)
        .pipe($.stripDebug())
        .pipe($.angularFilesort())
        .pipe($.semi.add({ leading: true }))
        .pipe($.debug())
        .pipe($.concat('scripts.js'))
        .pipe($.replace('/Src/Html/svg/', '/fonts/svg/'))
        .pipe($.babel({
            presets: ['env']
        }))
        .pipe($.uglify().on('error', function (e) {
            console.log(e);
        }))
        .pipe(gulp.dest(config.temp));
})

gulp.task('buildCss', function () {
    gulp.src([config.temp + '/**/*.css'])
        .pipe($.stripCssComments({ preserve: false }))
        .pipe($.cleanCss())
        .pipe($.concat('styles.css'))
        .pipe($.replace('../font-awesome/fonts/', '/fonts/'))
        .pipe($.replace('../malihu-custom-scrollbar-plugin/', '/ClientApp/Build/'))
        .pipe(gulp.dest('./build'));
    gulp.src('./Src/style.css')
        .pipe($.stripCssComments({ preserve: false }))
        .pipe($.cleanCss())
        .pipe($.replace('/Src/Html/HomeBootstrap', ''))
        .pipe(gulp.dest('./build'));
});
gulp.task('buildMyStyle', function () {
    gulp.src(['./Src/style.css','./Src/Html/material.css'])
        .pipe($.stripCssComments({ preserve: false }))
        .pipe($.cleanCss())
        .pipe($.replace('/Src/Html/HomeBootstrap', ''))
        .pipe(gulp.dest('./build'));
})

gulp.task('build', function () {
    return gulp.src([config.temp + '/deps.js', config.temp + '/scripts.js', config.temp + '/templates.js'])
        .pipe($.concat('scripts.js'))

        .pipe(gulp.dest('./build'));
})

gulp.task('default', ['inject', 'wiredep']);

gulp.task('resources', function () {
    gulp.src(config.index)
        .pipe($.resources())
        .pipe($.debug())
        .pipe(gulp.dest(config.temp))
});


function logBlue(msg) {
    if (typeof (msg) === 'object') {
        for (var item in msg) {
            if (msg.hasOwnProperty(item)) {
                $.util.log($.util.colors.blue(item));
                $.util.log($.util.colors.blue(msg[item]));
            }
        }
    } else {
        $.util.log($.util.colors.blue(msg));
    }

}

function logRed(msg) {
    if (typeof (msg) === 'object') {
        for (var item in msg) {
            if (msg.hasOwnProperty(item)) {
                $.util.log($.util.colors.red(item));
                $.util.log($.util.colors.red(msg[item]));
            }
        }
    } else {
        $.util.log($.util.colors.red(msg));
    }

}