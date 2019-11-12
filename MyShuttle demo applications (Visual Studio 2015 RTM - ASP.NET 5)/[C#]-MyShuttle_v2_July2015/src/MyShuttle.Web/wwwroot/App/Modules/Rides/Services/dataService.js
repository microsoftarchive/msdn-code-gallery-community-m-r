'use strict';

angular.module('myShuttleRides').service('ridesDataService', ['$http', '$q',
    function ($http, $q) {
        var service = this;

        service.getRides = function (page, pageSize, vehicleId, driverId) {
            vehicleId = vehicleId || '';
            driverId = driverId || '';

            var params = {
                pageSize: pageSize,
                pageCount: page,
                vehicleId: vehicleId,
                driverId: driverId
            };

            var promises = [];
            promises.push($http.get('rides/search', {params: params}));
            promises.push($http.get('rides/count', {params: {vehicleId: vehicleId, driverId: driverId}}));

            return $q.all(promises).then(function (results) {
                var count = results[1].data;

                return {
                    data: results[0].data,
                    count: count
                };
            });
        };

        service.getRide = function (rideId) {
            return $http.get('rides/get/' + rideId).then(function (response) {
                return response.data;
            });
        };
    }
]);
