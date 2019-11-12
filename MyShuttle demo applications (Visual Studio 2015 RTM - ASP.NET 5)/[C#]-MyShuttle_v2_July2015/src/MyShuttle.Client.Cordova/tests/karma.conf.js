module.exports = function (config) {
    config.set({
        basePath: './',
        frameworks: ['jasmine'],
        files: [
            './phantomBindPolyfill.js',
            '../bower_components/jquery/dist/jquery.js',
            '../bower_components/angular/angular.js',
            '../bower_components/angular-mocks/angular-mocks.js',
            '../bower_components/angular-route/angular-route.js',
            '../bower_components/bootstrap/dist/js/bootstrap.js',
            '../bower_components/angular-bootstrap/ui-bootstrap-tpls.js',
            '../bower_components/moment/moment.js',
            '../bower_components/angular-moment/angular-moment.js',
            '../bower_components/signalr/jquery.signalR.js',

            './out/scripts/config.js',
            './out/scripts/index.js',
            './out/scripts/**/*.js',

            './out/tests/**/*.spec.js'
        ],
        browsers: ['PhantomJS'],
        singleRun: true,
        plugins: [
            'karma-jasmine',
            'karma-phantomjs-launcher'
        ]
    });
};