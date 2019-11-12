'use strict';

angular.module('myShuttleCore').directive('msLoader', ['messenger', function (messenger) {
    return {
        restrict: 'AE',
        link: function (scope, element) {
            // hide the element initially
            element.addClass('hidden');

            var cleanUpStartLoading = scope.$on(messenger.messageTypes.startLoading, function (event) {
                element.removeClass('hidden');
            });

            var cleanUpEndLoading = scope.$on(messenger.messageTypes.endLoading, function (event) {
                element.addClass('hidden');
            });

            var cleanUpDestroy = scope.$on('$destroy', function () {
                cleanUpStartLoading();
                cleanUpEndLoading();
                cleanUpDestroy();
            });
        }
    };
}]);
