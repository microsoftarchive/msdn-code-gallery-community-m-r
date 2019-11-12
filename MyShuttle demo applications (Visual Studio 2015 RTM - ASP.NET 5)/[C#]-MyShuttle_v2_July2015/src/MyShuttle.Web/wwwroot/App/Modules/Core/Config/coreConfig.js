'use strict';

angular.module('myShuttleCore').config(['$httpProvider',
    function($httpProvider) {
        var customInterceptor = function ($q, $injector) {
            var serviceName = 'httpInterceptorService';
            return {
                request: function (config) {
                    var service = $injector.get(serviceName);
                    service.startRequest(config.url);
                    return config || $q.when(config);
                },
                response: function (response) {
                    var service = $injector.get(serviceName);
                    service.requestSuccess(response);
                    return response || $q.when(response);
                },
                responseError: function (rejection) {
                    var service = $injector.get(serviceName);
                    service.requestError(rejection);
                    return $q.reject(rejection);
                }
            };
        };

        $httpProvider.interceptors.push(customInterceptor);
    }
]);
