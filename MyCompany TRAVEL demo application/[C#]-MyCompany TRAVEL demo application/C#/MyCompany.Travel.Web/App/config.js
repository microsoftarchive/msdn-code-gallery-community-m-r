define(['services/context'], function (context) {
    var startModule = 'user/travels',
        detailPage = 'user/travelDetail/',
        pageSize = 10,
        dateFormat = 'MM/DD/YYYY',
        bingCredentialsKey = "AizcJgRZiGY_JR8rr6z0PMO4nFjAxMiwu7t0rLbGM97Uuy73FZybR2RwCRLjXYzT",
        isNoAuth = location.href.toLowerCase().indexOf('noauth') != -1;

    if (isNoAuth) {
        var hasHash = location.href.toLowerCase().indexOf('noauth#/') != -1;
        if (!hasHash) {
            location.href = location.href.toLowerCase().replace('noauth', 'noauth#/');
        }
    }

    var configureRoutes = function (platform)
    {
        var routes = [];

        if (context.platform == 'desktop') {
            routes = [
                {
                    url: 'user/travels',
                    moduleId: 'viewmodels/travels',
                    name: 'My Travels',
                    visible: true,
                    iconClass: 'travels-icon'
                },
                {
                    url: 'user/travelDetail/:travelRequestId',
                    moduleId: 'viewmodels/travelDetail',
                    name: 'Travel Detail',
                    visible: false
                },
                {
                    url: 'user/travelForm/:travelRequestId',
                    moduleId: 'viewmodels/travelForm',
                    name: 'Edit Travel',
                    visible: false
                },
                {
                    url: 'user/travelForm',
                    moduleId: 'viewmodels/travelForm',
                    name: 'Add Travel',
                    visible: false
                }
            ];

            if (context.currentUser.isManager()) {
                routes.push({
                    url: 'manager/travels/team',
                    moduleId: 'viewmodels/teamTravels',
                    name: 'Team Travels',
                    visible: true,
                    iconClass: 'manager-travels-icon'
                },
                {
                    url: 'manager/heatMap',
                    moduleId: 'viewmodels/heatMap',
                    name: 'Heat Map',
                    visible: true,
                    iconClass: 'manager-heatmap-icon'
                },
                {
                    url: 'manager/travelRequest/:travelRequestId',
                    moduleId: 'viewmodels/travelDetail',
                    name: 'Travel Request',
                    visible: false,
                    settings: { mode: 'manager' }
                });

            }

            if (context.currentUser.isRRHH()) {
                routes.push({
                    url: 'rrhh/travels/employees',
                    moduleId: 'viewmodels/employeesTravels',
                    name: 'All Travels',
                    visible: true,
                    iconClass: 'rrhh-travels-icon'
                },
                {
                    url: 'rrhh/travelRequest/:travelRequestId',
                    moduleId: 'viewmodels/travelDetail',
                    name: 'Travel Request',
                    visible: false,
                    settings: { mode: 'rrhh' }
                },
                {
                    url: 'rrhh/travelForm/:travelRequestId',
                    moduleId: 'viewmodels/travelForm',
                    name: 'Edit Travel',
                    visible: false,
                    settings: { mode: 'rrhh' }
                });
            }
        }

        if (context.platform == 'mobile')
        {
            routes = [
                {
                    url: 'user/travels',
                    moduleId: 'viewmodels/travels',
                    name: 'My Travels',
                    visible: true,
                    iconClass: 'travels-icon'
                },
                {
                    url: 'user/travelDetail/:travelRequestId',
                    moduleId: 'viewmodels/travelDetail',
                    name: 'Travel Detail',
                    visible: false
                }]

            if (context.currentUser.isManager()) {
                routes.push({
                    url: 'manager/travels/team',
                    moduleId: 'viewmodels/teamTravels',
                    name: 'Team Travels',
                    visible: true,
                    iconClass: 'manager-travels-icon'
                },
                {
                    url: 'manager/travelRequest/:travelRequestId',
                    moduleId: 'viewmodels/travelDetail',
                    name: 'Travel Request',
                    visible: false,
                    settings: { mode: 'manager' }
                });

            }
        }

        if (isNoAuth) {
            for (var i = 0; i < routes.length; i++) {
                routes[i].hash = 'noauth#/' + routes[i].url;
            }
        }

        return routes;
    }

    return {
        configureRoutes: configureRoutes,
        startModule: startModule,
        detailPage: detailPage,
        pageSize: pageSize,
        dateFormat: dateFormat,
        bingCredentialsKey: bingCredentialsKey,
        isNoAuth: isNoAuth
    };
});