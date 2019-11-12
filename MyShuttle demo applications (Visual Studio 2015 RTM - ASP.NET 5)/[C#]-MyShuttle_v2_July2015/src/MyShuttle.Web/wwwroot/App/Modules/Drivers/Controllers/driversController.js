'use strict';

angular.module('myShuttleDrivers').controller('DriversController', [
    '$scope', '$state', 'driversDataService', 'modalService', '$location', '$timeout',
    function ($scope, $state, driversDataService, modalService, $location, $timeout) {
        var pageSize = 8, // TODO: Settings?
            defaultPage = 1;

        $scope.goTo = function (page) {
            var params = {page: page };
            if ($scope.search) params.q = $scope.search;
            $state.go('drivers', params);
        };

        $scope.remove = function (driver) {
            var message = 'Are you sure you want to delete the driver ' + driver.Name + '?';
            modalService.confirm('Confirm Driver Deletion', message).then(function (result) {
                if (result) {
                    driversDataService.remove(driver.DriverId).then(function () {
                        find();
                    });
                }
            });
        };

        var find = function () {
            $scope.count = null;
            driversDataService.getDrivers($scope.search || '', $scope.page - 1, pageSize).then(function (result) {
                $scope.drivers = result.data;
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
