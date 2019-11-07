'use strict';
/*global $ */

angular.module('myShuttleEvents').controller('EventsController', ['$scope', '$state', 'settingsService',

    function($scope, $state, settingsService) {

        $scope.events = [];
        $scope.count = 0;
        $scope.currentPage = 20;

        $.connection.hub.logging = false;

        var init = function() {
            startConnection();
        };

        function startConnection() {

            var connection = $.hubConnection('/web');
            var myShuttleHubProxy = connection.createHubProxy(settingsService.signalRHubName);
            myShuttleHubProxy.on('updateEvents', function(message) {
                $scope.$apply(function() {
                    $scope.events.splice(0, 0, message);
                    $scope.count = $scope.count + 1;

                    if ($scope.events.length > $scope.currentPage){
                        $scope.events.splice($scope.currentPage - 1, $scope.events.length - $scope.currentPage);
                    }
                });

            });

            connection.start();
        }


        init();

        $scope.collapsed = true;
    }
]);
