
define(['services/context'], function (context) {
    var routes,
        configureRoutes = function () {
            routes = [{
                url: 'overlaps',
                moduleId: 'viewmodels/overlaps',
                name: 'Overlaps',
                visible: context.currentUser.isManager(),
                iconClass: 'team-vacation-overlaps-icon'
            }, {
                url: 'teamVacation',
                moduleId: 'viewmodels/teamVacation',
                name: 'Team vacation',
                visible: context.currentUser.isManager(),
                iconClass: 'team-vacation-list-icon'
            },
            {
                url: 'myCalendar',
                moduleId: 'viewmodels/myCalendar',
                name: 'My calendar',
                visible: true,
                iconClass: 'mycalendar-icon'
            }];

            var isNoAuth = location.href.toLowerCase().indexOf('noauth') != -1;
            if (isNoAuth) {
                for (var i = 0; i < routes.length; i++) {
                    routes[i].hash = 'noauth#/' + routes[i].url;
                }
            }

            var search = window.location.search;
            if(search)
            {
                var hasHash = location.href.toLowerCase().indexOf('#') != -1;
                if (!hasHash) {
                    location.href = location.href + '#/';
                }

                for (var i = 0; i < routes.length; i++) {
                    routes[i].hash = search + '#/' + routes[i].url;
                }
            }
        },
        getRoutes = function () {
            return routes;
        };

    var startModule = 'myCalendar',
        pageSize = 10,
        dateFormat = 'DD/MM/YYYY',
        timeFormat = 'HH:mm',
        weekStartsOnMonday = false;

    return {
        getRoutes: getRoutes,
        configureRoutes: configureRoutes,
        startModule: startModule,
        pageSize: pageSize,
        dateFormat: dateFormat,
        timeFormat: timeFormat,
        weekStartsOnMonday: weekStartsOnMonday
    };
});