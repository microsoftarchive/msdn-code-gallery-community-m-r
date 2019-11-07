'use strict';

angular.module('myShuttleDashboard').service('dashboardDataService', ['$http',
    function($http) {
        var service = this;

        service.getTopVehicles = function() {
            return $http.get('analytics/topvehicles').then(function(response){
                return response.data;
            });
        };

        service.getTopDrivers = function() {
            return $http.get('analytics/topdrivers').then(function(response){
                return response.data;
            });
        };

        service.getSummaryInfo = function() {
            return $http.get('analytics/summary').then(function(response){
                return response.data;
            });
        };

        service.getRidesInfo = function(){
            return $http.get('analytics/rides').then(function(response){
                return response.data;
            });
        };
    }
]);
