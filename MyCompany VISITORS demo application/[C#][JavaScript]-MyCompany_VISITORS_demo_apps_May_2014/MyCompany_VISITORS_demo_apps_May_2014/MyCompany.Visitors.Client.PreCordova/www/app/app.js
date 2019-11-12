(function appInit() {
    'use strict';

    var visitorsApp = angular.module('VisitorsApp', [
        // Angular modules
        'ngRoute',

        // Third party modules
        'snap']);

    visitorsApp.config(['$routeProvider', 'snapRemoteProvider', function ($routeProvider, snapRemoteProvider) {

        $routeProvider
            .when('/visits', { controller: 'VisitsCtrl', templateUrl: 'app/views/visits.html', pageTitle: 'All visits' })
            .when('/visit/:visitId', { controller: 'VisitDetailCtrl', templateUrl: 'app/views/visitDetail.html', pageTitle: 'Visit detail' })
            .otherwise({ controller: 'TodayVisitsCtrl', templateUrl: 'app/views/todayVisits.html', pageTitle: 'Today visits' });

        snapRemoteProvider.globalOptions.disable = 'right';
    }]);
})();