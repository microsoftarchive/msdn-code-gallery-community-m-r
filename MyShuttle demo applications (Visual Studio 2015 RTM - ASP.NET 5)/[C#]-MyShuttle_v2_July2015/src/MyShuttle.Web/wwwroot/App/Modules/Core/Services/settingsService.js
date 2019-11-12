'use strict';

angular.module('myShuttleCore').factory('settingsService', [
    function () {
        var signalRHub =  'http://myshuttleiss.cloudapp.net:8080/signalr/hubs';
        var signalRHubName = 'MyShuttleHub';

        return{
            signalRHub: signalRHub,
            signalRHubName: signalRHubName
        };
    }
]);

