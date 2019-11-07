'use strict';

angular.module('myShuttleCore').service('dataService', ['$http', '$q',
    function ($http, $q) {
        var service = this;

        service.getAllDrivers = function() {

            return $http.get('drivers/filter').then(function(response){
                return response.data;
            });
        };


        service.getAllVehicles = function() {

            return $http.get('vehicles/filter').then(function(response){
                return response.data;
            });
        };

    }
]);
