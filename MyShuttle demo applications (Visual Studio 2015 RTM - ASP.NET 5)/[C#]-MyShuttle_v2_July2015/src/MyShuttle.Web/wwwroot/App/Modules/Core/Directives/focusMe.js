'use strict';

angular.module('myShuttleCore').directive('focusMe', ['$timeout', function ($timeout) {
    return {
        restrict: 'A',
        scope: {
            keep: '=?keepOnBlur'
        },
        link: function (scope, element) {
            element = element[0];
            element.focus();

            if (scope.keep) {
                element.onblur = function () {
                    $timeout(function () {
                        element.focus();
                    }, 100);
                };
            }
        }
    };
}]);
