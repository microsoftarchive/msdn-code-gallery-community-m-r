'use strict';

angular.module('myShuttleDrivers').controller('DriverFormController',
    ['$scope', '$state', '$stateParams', 'driversDataService', '$timeout',
        function ($scope, $state, $stateParams, dataService, $timeout) {
            var master = null,
                isEditMode = false;

            $scope.reset = function () {
                init();
            };

            $scope.save = function () {
                if ($scope.driverForm.$valid && $scope.driver.Picture) {
                    var promise = null;
                    if (isEditMode) {
                        promise = dataService.update($scope.driver);
                    }
                    else {
                        promise = dataService.add($scope.driver);
                    }

                    promise.then(function () {
                        var alert = {type: 'success', msg: 'The driver was saved successfully.'};
                        $scope.alerts.push(alert);

                        $timeout(function () {
                            $scope.closeAlert($scope.alerts.indexOf(alert));
                            $state.go('drivers');
                        }, 1000);
                    });
                }
                else {
                    $scope.showValidationMessages = true;
                }
            };

            $scope.isUnchanged = function (driver) {
                return angular.equals(driver, master);
            };

            $scope.closeAlert = function (index) {
                $scope.alerts.splice(index, 1);
            };

            var getDriver = function (driverId) {
                dataService.getDriver(driverId).then(function (driver) {
                    $scope.driver = driver;
                    master = angular.copy(driver);
                });
            };

            var init = function () {
                master = { TotalRides: 0};
                $scope.showValidationMessages = false;
                $scope.alerts = [];

                if ($stateParams.id) {
                    isEditMode = true;
                    getDriver($stateParams.id);
                }
                else {
                    $scope.driver = angular.copy(master);
                }
            };

            init();
        }
    ]);
