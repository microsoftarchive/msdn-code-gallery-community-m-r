require.config({
    paths: { "text": "durandal/amd/text" }
});

define(function (requiere) {
    if ((window.location.search.length > 0) && window.location.href.indexOf("#") < 0
        && window.location.href.indexOf("SPHost") < 0) {
        var search = window.location.search.replace('?', '');
        window.location.href = window.location.href.replace(window.location.search, "#/" + search);
    }

    var system = require('durandal/system'),
        app = require('durandal/app'),
        config = require('config'),
        router = require('durandal/plugins/router'),
        viewLocator = require('durandal/viewLocator'),
        logger = require('services/logger'),
        dataservice = require('services/dataservice'),
        context = require('services/context'),
        enums = require('services/enums'),
        notifications = require('services/notifications');

    system.debug(true);

    dataservice.getLoggedEmployeeInfo(enums.pictureType.small).then(function (employee) {
        context.currentUser = employee;
        config.configureRoutes();

        if (config.weekStartsOnMonday)
            moment.lang('en-gb', moment.languages['en-gb']);

        app.title = 'MyCompany Vacation';

        app.start().then(function () {
            // route will use conventions for modules
            // assuming viewmodels/views folder structure
            router.useConvention();
            viewLocator.useConvention();

            notifications.startConnection();

            app.setRoot('viewmodels/shell');

            $.ajaxSetup({
                dataType: 'json',
                contentType: 'application/json'
            });

            router.handleInvalidRoute = function (route, params) {
                logger.log('No route found', route, 'main');
            };

            // handle escape key for modal dialogs
            $(document).keyup(function (e) {
                // esc
                if (e.keyCode == 27) {
                    $(".button-close").last().click();
                }
            });
        });
    });
});