/// <binding BeforeBuild='beforeBuild' Clean='clean' />
var gulp = require('gulp');
var del = require('del');
var $ = require('gulp-load-plugins')({ lazy: true });

gulp.task('default', ['beforeBuild']);

gulp.task('beforeBuild', ['bower', 'less', 'templatecache']);

gulp.task('test', ['typescript-tests'], function () {
    // When executing this task from gulp, this files overrides the
    // files configuration in tests/karma.conf.js
    var files = [
            'tests/phantomBindPolyfill.js',
            'bower_components/jquery/dist/jquery.js',
            'bower_components/angular/angular.js',
            'bower_components/angular-mocks/angular-mocks.js',
            'bower_components/angular-route/angular-route.js',
            'bower_components/bootstrap/dist/js/bootstrap.js',
            'bower_components/angular-bootstrap/ui-bootstrap-tpls.js',
            'bower_components/moment/moment.js',
            'bower_components/angular-moment/angular-moment.js',
            'bower_components/signalr/jquery.signalR.js',

            'tests/out/scripts/config.js',
            'tests/out/scripts/index.js',
            'tests/out/scripts/**/*.js',

            'tests/out/**/*.spec.js'
    ];
    return gulp.src(files)
        .pipe($.karma({
            configFile: './tests/karma.conf.js',
            action: 'run'
        }))
        .on('error', function (err) {
            throw err;
        });
});

gulp.task('clean', ['clean-bower', 'clean-less', 'clean-templatecache']);

gulp.task('typescript-tests', ['clean-typescript-tests'], function () {
    var options = {
        target: 'ES5',
    };
    return gulp.src(['./tests/**/*.ts'])
        .pipe($.tsc(options))
        .pipe(gulp.dest('./tests/out'));
});

gulp.task('clean-typescript-tests', function (done) {
    clean('./tests/out', done);
});

gulp.task('bower', function () {
    log('Copying bower dependencies to normalized folders');

    var mainBowerFiles = require('main-bower-files');
    return gulp.src(mainBowerFiles(), { base: './bower_components' })
        .pipe($.bowerNormalize({ bowerJson: './bower.json' }))
        .pipe(gulp.dest('./www/vendors/'));
});

gulp.task('clean-bower', function (done) {
    clean('./www/vendors/**', done);
});

gulp.task('less', ['clean-less'], function () {
    log('Compiling Less --> CSS');

    return gulp.src('./www/css/main.less')
      .pipe($.less())
      .pipe(gulp.dest('./www/css'));
});

gulp.task('clean-less', function (done) {
    clean('./www/css/main.css', done);
});

gulp.task('templatecache', ['clean-templatecache'], function () {
    log('Creating AngularJS $templateCache');

    var options = {
        module: 'myShuttleDriverApp',
        standAlone: false,
        root: 'app/'
    };
    return gulp
        .src('./scripts/**/*.html')
        .pipe($.angularTemplatecache('templates.js', options))
        .pipe(gulp.dest('./www/scripts'));
});

gulp.task('clean-templatecache', function (done) {
    clean('./www/scripts/templates.js', done);
});

////////////

function clean(path, done) {
    log('Cleaning: ' + $.util.colors.blue(path));
    del(path, done);
}

function log(msg) {
    if (typeof (msg) === 'object') {
        for (var item in msg) {
            if (msg.hasOwnProperty(item)) {
                $.util.log($.util.colors.blue(msg[item]));
            }
        }
    } else {
        $.util.log($.util.colors.blue(msg));
    }
}