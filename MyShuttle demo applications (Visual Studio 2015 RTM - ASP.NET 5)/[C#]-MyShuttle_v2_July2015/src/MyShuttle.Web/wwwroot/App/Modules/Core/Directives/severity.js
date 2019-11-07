'use strict';

angular.module('myShuttleCore').directive('severity', ['$compile', function ($compile) {

    return {
        restrict: 'AE',
        scope: {
            model: '=',
        },
        templateUrl: 'App/Modules/Core/Views/severityTemplate.html'
    };
}]);