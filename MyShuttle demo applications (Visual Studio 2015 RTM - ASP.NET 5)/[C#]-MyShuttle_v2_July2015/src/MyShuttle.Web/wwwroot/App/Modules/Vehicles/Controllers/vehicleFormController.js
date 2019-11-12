'use strict';

angular.module('myShuttleVehicles').controller('VehicleFormController',
    ['$scope', '$state', '$stateParams', 'vehiclesDataService', '$timeout', 'dataService',
        function ($scope, $state, $stateParams, dataService, $timeout, coreDataService) {
            var master = null,
                isEditMode = false;

            $scope.reset = function () {
                init();
            };

            $scope.save = function () {
                if ($scope.vehicleForm.$valid && $scope.vehicle.Picture) {
                    var promise = null;
                    if (isEditMode) {
                        promise = dataService.update($scope.vehicle);
                    }
                    else {
                        promise = dataService.add($scope.vehicle);
                    }

                    promise.then(function () {
                        var alert = {type: 'success', msg: 'The vehicle was saved successfully.'};
                        $scope.alerts.push(alert);

                        $timeout(function () {
                            $scope.closeAlert($scope.alerts.indexOf(alert));
                            $state.go('vehicles');
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

            var getVehicle = function (vehicleId) {
                dataService.getVehicle(vehicleId).then(function (vehicle) {
                    $scope.vehicle = vehicle;
                    master = angular.copy(vehicle);
                });
            };

            var getVehicleByDeviceId = function (deviceId) {
                dataService.getVehicleByDeviceId(deviceId).then(function (vehicle) {
                    $scope.vehicle = vehicle;
                    master = angular.copy(vehicle);
                });
            };

            var init = function () {
                master = {TotalRides: 0};
                $scope.showValidationMessages = false;
                $scope.alerts = [];
                $scope.sizes = [
                    {display: 'Van', value: 1},
                    {display: 'Mini Van', value: 2},
                    {display: 'Luxury', value: 3},
                    {display: 'Compact', value: 4},
                    {display: 'Intermediate', value: 5},
                    {display: 'Premium', value: 6},
                    {display: 'Full Size', value: 7},
                    {display: 'Convertible', value: 8}
                ];

                if ($stateParams.id) {
                    isEditMode = true;
                    getVehicle($stateParams.id);
                }
                else if ($stateParams.deviceId) {
                    isEditMode = true;
                    getVehicleByDeviceId($stateParams.deviceId);
                }
                else {
                    $scope.vehicle = angular.copy(master);
                }

                coreDataService.getAllDrivers().then(function(drivers){
                    $scope.drivers = drivers;
                });
            };

            init();
        }
    ]);
