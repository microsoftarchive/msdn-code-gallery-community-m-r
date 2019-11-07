'use strict';

angular.module('myShuttleProfile').controller('ProfileController', ['$scope', 'profileDataService', '$modal', '$timeout', 'messenger',
    function ($scope, profileDataService, $modal, $timeout, messenger) {
        var master = null;

        var init = function () {
            master = null;
            $scope.showValidationMessages = false;
            $scope.alerts = [];

            getAsync();
        };

        $scope.update = function () {
            if ($scope.profileForm.$valid) {
                profileDataService.putAsync($scope.profile).then(function () {
                    var alert = {type: 'success', msg: 'The profile was updated successfully.'};
                    $scope.alerts.push(alert);

                    $timeout(function () {
                        $scope.closeAlert($scope.alerts.indexOf(alert));
                    }, 2000);

                    messenger.send(messenger.messageTypes.carrierProfileUpdated);
                });
            } else {
                $scope.showValidationMessages = true;
            }
        };

        $scope.closeAlert = function (index) {
            $scope.alerts.splice(index, 1);
        };

        $scope.isUnchanged = function (profile) {
            return angular.equals(profile, master);
        };

        var getAsync = function () {
            profileDataService.getAsync().then(function (serviceProfile) {
                $scope.profile = angular.copy(serviceProfile);
                master = angular.copy(serviceProfile);
            });
        };

        $scope.reset = function () {
            init();
        };

        init();
    }
]);