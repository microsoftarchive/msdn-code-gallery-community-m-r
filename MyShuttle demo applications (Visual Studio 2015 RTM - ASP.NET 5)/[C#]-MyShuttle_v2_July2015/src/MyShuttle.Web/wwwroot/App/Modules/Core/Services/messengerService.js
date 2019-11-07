'use strict';

angular.module('myShuttleCore').service('messenger', ['$rootScope', function ($rootScope) {
    // private notification messages
    var messageTypes = {
        startLoading: '_START_LOADING_',
        endLoading: '_END_LOADING_',
        carrierProfileUpdated: '_CARRIER_UPDATED_'
    };

    var send = function (message, data) {
        $rootScope.$broadcast(message, data);
    };

    return {
        messageTypes: messageTypes,
        send: send
    };
}]);

