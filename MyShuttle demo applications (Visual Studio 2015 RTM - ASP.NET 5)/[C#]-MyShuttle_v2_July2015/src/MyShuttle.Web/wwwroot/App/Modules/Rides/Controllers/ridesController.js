'use strict';

angular.module('myShuttleRides').controller('RidesController', ['$scope', '$state', '$q', 'ridesDataService', 'dataService', '$location',
    function($scope, $state, $q, dataService, coreDataService, $location) {
        var pageSize = 10,
            defaultPage = 1;

        $scope.pageSize = pageSize;

        $scope.goTo = function (page) {
            var params = {page: page };
            if ($scope.vehicleIdFilter) params.vehicleId = $scope.vehicleIdFilter;
            if ($scope.driverIdFilter) params.driverId = $scope.driverIdFilter;
            $state.go('rides', params);
        };

        var find = function () {
            $scope.count = null;
            dataService.getRides($scope.page - 1, pageSize, $scope.vehicleIdFilter, $scope.driverIdFilter).then(function (result) {
                var rides = result.data;
                rides.forEach(function(ride){
                    ride.collapsed = true;
                });
                $scope.rides = rides;
                $scope.count = result.count;
            });
        };

        $scope.filterChange = function() {
            if ($scope.driverIdFilter || $scope.vehicleIdFilter) {
                var params = 'page=' + defaultPage;

                if ($scope.driverIdFilter)
                    params = params + '&driverId=' + $scope.driverIdFilter;

                if ($scope.vehicleIdFilter)
                    params = params + '&vehicleId=' + $scope.vehicleIdFilter;

                $location.search(params);
            }
            else {
                $location.search('');
            }
        };

        var loadMasters = function() {
            var promises = [];
            promises.push(coreDataService.getAllDrivers());
            promises.push(coreDataService.getAllVehicles());

            return $q.all(promises).then(function(results) {
                $scope.drivers = results[0];
                var vehicles = results[1];
                vehicles.forEach(function(vehicle) {
                    vehicle.Name = vehicle.Make + ' ' + vehicle.Model;
                });
                $scope.vehicles = vehicles;
            });
        };

        var init = function() {
            loadMasters();

            $scope.page = parseInt($state.params.page) || defaultPage;
            $scope.vehicleIdFilter = parseInt($state.params.vehicleId) || null;
            $scope.driverIdFilter = parseInt($state.params.driverId) || null;

            find();
        };

        init();

        $scope.collapsed = true;
    }
]);
