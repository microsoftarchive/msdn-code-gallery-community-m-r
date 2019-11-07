'use strict';

angular.module('myShuttleVehicles').controller('VehiclesController',
    ['$scope', '$state', '$stateParams', 'vehiclesDataService', '$timeout', '$location', 'modalService',
        function ($scope, $state, $stateParams, vehiclesDataService, $timeout, $location, modalService) {
            var pageSize = 8, // TODO: Settings?
                defaultPage = 1;

            $scope.goTo = function (page) {
                var params = {page: page };
                if ($scope.search) params.q = $scope.search;
                $state.go('vehicles', params);
            };

            $scope.remove = function(vehicle){
                var message = 'Are you sure you want to delete the vehicle ' + vehicle.LicensePlate + '?';
                modalService.confirm('Confirm Vehicle Deletion', message).then(function(result) {
                    if (result) {
                        vehiclesDataService.remove(vehicle.VehicleId).then(function(){
                            find();
                        });
                    }
                });
            };

            var find = function () {
                $scope.count = null;
                vehiclesDataService.getVehicles($scope.search || '', $scope.page - 1, pageSize).then(function (result) {
                    $scope.vehicles = result.data;
                    $scope.count = result.count;
                });
            };

            var init = function () {
                $scope.page = parseInt($state.params.page) || defaultPage;
                if ($state.params.q)
                    $scope.search = $state.params.q;

                find();
            };

            init();
        }
    ]);
