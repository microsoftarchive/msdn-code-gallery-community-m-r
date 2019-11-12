'use strict';

angular.module('myShuttleCore').directive('ngKeyEnter', [function () {
    return function (scope, element, attrs) {
        var ele = element;
        var selector = attrs.ngSelector;
        if (selector)
            ele = angular.element(selector);

        ele.bind('keydown keypress', function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.ngKeyEnter);
                });

                event.preventDefault();
            }
        });
    };
}]);
