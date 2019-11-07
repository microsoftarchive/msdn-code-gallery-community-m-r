'use strict';

var gulp = require('gulp');
var del = require('del');
var $ = require('gulp-load-plugins')({ lazy: true });
var karma = require('karma').server;
var config = require('./gulp.config')();

////////////

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

function clean(path, done) {
    log('Cleaning: ' + $.util.colors.blue(path));
    del(path, done);
}

////////////

// Default task(s).
gulp.task('default', ['jshint', 'csslint']);

// Lint task(s).
gulp.task('lint', ['jshint', 'csslint']);

gulp.task('jshint', function () {

    return gulp.src(config.alljs)
        .pipe($.jshint())
        .pipe($.jshint.reporter('jshint-stylish'))
        .pipe($.jshint.reporter('fail'));
});

gulp.task('csslint', function () {

    return gulp.src(config.allcss)
        .pipe($.csslint('.csslintrc'))
        .pipe($.csslint.reporter());
});

gulp.task('less', function () {

    var options = {
        compress: true,
        yuicompress: true,
        optimization: 2
    };

    return gulp.src(config.less)
          .pipe($.less(options))
          .pipe(gulp.dest(''));
});

gulp.task('test', function (done) {
    karma.start({
        configFile: __dirname + '/karma.conf.js',
        singleRun: true
    }, done);
});