(function appInit() {
    'use strict';

    var visitorsApp = angular.module('VisitorsApp', [
        // Angular modules
        'ngRoute',

        // Third party modules
        'snap']);

    visitorsApp.config(['$routeProvider', '$httpProvider', 'snapRemoteProvider', function ($routeProvider, $httpProvider, snapRemoteProvider) {

        $routeProvider
            .when('/visits', { controller: 'VisitsCtrl', templateUrl: 'app/views/visits.html', pageTitle: 'All visits' })
            .when('/visit/:visitId', { controller: 'VisitDetailCtrl', templateUrl: 'app/views/visitDetail.html', pageTitle: 'Visit detail' })
            .otherwise({ controller: 'TodayVisitsCtrl', templateUrl: 'app/views/todayVisits.html', pageTitle: 'Today visits' });

        $httpProvider.defaults.headers.post['Content-Type'] = 'application/json';

        snapRemoteProvider.globalOptions.disable = 'right';
    }]);

    // Delay Angular bootstrapping until cordova is initialized 
    document.addEventListener('deviceready', initializeApp, false);

    function initializeApp() {
        angular.element(document).ready(function () {
            angular.bootstrap(document, ['VisitorsApp']);
        });
    }

})();