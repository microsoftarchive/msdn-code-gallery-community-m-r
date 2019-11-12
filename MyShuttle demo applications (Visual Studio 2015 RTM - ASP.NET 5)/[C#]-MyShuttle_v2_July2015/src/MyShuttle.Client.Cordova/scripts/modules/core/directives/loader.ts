/// <reference path="../core.ts" />

module MyShuttle.Core {
    angularModule.directive('msLoader', function (messengerService: MessengerService) {
        return {
            restrict: 'AE',
            link: function (scope, element) {
                // hide the element initially
                element.addClass('hidden');

                var cleanUpStartLoading = scope.$on(messengerService.messageTypes.startLoading, function (event) {
                    element.removeClass('hidden');
                });

                var cleanUpEndLoading = scope.$on(messengerService.messageTypes.endLoading, function (event) {
                    element.addClass('hidden');
                });

                var cleanUpDestroy = scope.$on('$destroy', function () {
                    cleanUpStartLoading();
                    cleanUpEndLoading();
                    cleanUpDestroy();
                });
            }
        };
    });
}