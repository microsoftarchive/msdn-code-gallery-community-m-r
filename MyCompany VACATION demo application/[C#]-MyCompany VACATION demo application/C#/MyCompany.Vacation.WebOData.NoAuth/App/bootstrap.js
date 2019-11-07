var vacationApp = angular.module('vacationApp', ['ui.bootstrap']);

vacationApp.run(['notifications', function (notifications) {
    notifications.startConnection();
}]);

vacationApp.config(['$routeProvider',
    function ($routeProvider) {
        var setRoutes = function () {
            $routeProvider.
            when('/', { controller: 'MyCalendarController', templateUrl: 'App/views/myCalendar.html' }).
            when('/teamVacation', { controller: 'TeamVacationController', templateUrl: 'App/views/teamVacation.html' }).
            when('/overlaps', { controller: 'OverlapsController', templateUrl: 'App/views/overlaps.html' }).
            otherwise({ redirectTo: '/' });
        };

        var configAjax = function () {
            $.ajaxSetup({
                dataType: 'json',
                contentType: 'application/json'
            });
        }

        var configCulture = function () {
        };

        configAjax();
        configCulture();
        setRoutes();
    }]);

