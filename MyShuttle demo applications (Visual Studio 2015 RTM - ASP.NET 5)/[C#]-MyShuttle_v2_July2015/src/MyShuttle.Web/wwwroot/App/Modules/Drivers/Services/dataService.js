'use strict';

angular.module('myShuttleDrivers').service('driversDataService', ['$http', '$q',
    function ($http, $q) {
        var service = this;

        service.getDrivers = function (filter, page, pageSize) {
            var params = {
                filter: filter,
                pageSize: pageSize,
                pageCount: page
            };

            var promises = [];
            promises.push($http.get('drivers/search', {params: params}));
            promises.push($http.get('drivers/count', {params: {filter: filter}}));

            return $q.all(promises).then(function (results) {
                var count = results[1].data;

                return {
                    data: results[0].data,
                    count: count
                };
            });
        };

        service.remove = function (driverId) {
            return $http.delete('drivers/Delete/' + driverId);
        };

        service.getDriver = function (driverId) {
            return $http.get('drivers/get/' + driverId).then(function (response) {
                return response.data;
            });
        };

        service.add = function (driver) {
            return $http.post('drivers/Post', driver).then(function (response) {
                return response.data;
            });
        };

        service.update = function (driver) {
            driver.Vehicle = null;

            return $http.put('drivers/Put', driver).then(function (response) {
                return response.data;
            });
        };
    }
]);
