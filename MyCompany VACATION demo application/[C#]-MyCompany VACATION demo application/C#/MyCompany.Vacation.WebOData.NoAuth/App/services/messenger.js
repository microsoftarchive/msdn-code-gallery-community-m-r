vacationApp.factory('messenger', ['$rootScope', function ($rootScope) {
    var send = function (message) {
        $rootScope.$broadcast(message);
    };

    return {
        send: send
    };
}]);
