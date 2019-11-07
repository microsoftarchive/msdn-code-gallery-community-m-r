module.exports = function () {
    var config = {
        allcss: [
            './wwwroot/App/Modules/**/Css/*.css',
            './wwwroot/Content/login.css',
            '!./wwwroot/App/Modules/Core/Css/colors.css'
        ],
        alljs: [
            './gulpfile.js',
            './wwwroot/App/**/*.js',
            '!./wwwroot/App/lib/**/*.js',
            '!./wwwroot/App/Themes/**/*.js'
        ],
        less: [
            './**/Css/*.less',
            './**/Content/*.less'
        ]
    };

    return config;
};