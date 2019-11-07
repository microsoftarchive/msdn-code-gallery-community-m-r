'use strict';

angular.module('myShuttleVehicles').service('vehiclesDataService', ['$http', '$q',
    function ($http, $q) {
        var service = this;

        service.getVehicles = function (filter, page, pageSize) {
            var params = {
                filter: filter,
                pageSize: pageSize,
                pageCount: page
            };

            var promises = [];
            promises.push($http.get('vehicles/search', {params: params}));
            promises.push($http.get('vehicles/count', {params: {filter: filter}}));

            return $q.all(promises).then(function(results){
                var count = results[1].data;

                return {
                    data: results[0].data,
                    count: count
                };
            });
        };

        service.remove = function(vehicleId) {
            return $http.delete('vehicles/Delete/' + vehicleId);
        };

        service.getVehicle = function (vehicleId) {
            return $http.get('vehicles/get/' + vehicleId).then(function (response) {
                return response.data;
            });
        };

        service.getVehicleByDeviceId = function (deviceId) {
            return $http.get('vehicles/device?deviceId=' + deviceId).then(function (response) {
                return response.data;
            });
        };



        service.add = function (vehicle) {
            return $http.post('vehicles/Post', vehicle).then(function (response) {
                return response.data;
            });
        };

        service.update = function (vehicle) {
            vehicle.Driver = null;

            return $http.put('vehicles/Put', vehicle).then(function (response) {
                return response.data;
            });
        };
    }
]);
