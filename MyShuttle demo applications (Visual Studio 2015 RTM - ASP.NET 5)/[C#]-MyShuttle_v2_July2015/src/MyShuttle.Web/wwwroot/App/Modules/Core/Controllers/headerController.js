'use strict';

angular.module('myShuttleCore').controller('HeaderController', ['$scope', 'navigationService', 'profileDataService', 'messenger', 'settingsService',
    function($scope, navigationService, profileDataService, messenger, settingsService) {

        $scope.isActiveRoute = function (path) {
            return navigationService.isActiveRoute(path);
        };

        $scope.vehicleDashboardUrl = settingsService.vehicleDashboardUrl;

        var getCarrierProfile = function () {
            profileDataService.getAsync().then(function (serviceProfile) {
                $scope.profile = angular.copy(serviceProfile);
            });
        };

         var init = function(){
            $scope.menu = navigationService.getMenu();

            getCarrierProfile();
        };

        var carrierProfileUpdated = $scope.$on(messenger.messageTypes.carrierProfileUpdated, function (event) {
            getCarrierProfile();
        });

        init();
    }
]);
