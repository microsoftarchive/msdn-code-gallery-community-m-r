'use strict';
angular.module('myShuttleProfile').service('profileDataService', ['$http',
    function ($http) {
        var service = this;

        service.getAsync = function () {
            var config = {
                headers:{
                    'If-Modified-Since':0
                }
            };
            return $http.get('carriers/get/',config).then(function (response) {
                return response.data;
            });
        };

        service.putAsync = function (carrier) {
            return $http.put('carriers/Put', carrier).then(function (response) {
                return response.data;
            });
        };
    }
]);