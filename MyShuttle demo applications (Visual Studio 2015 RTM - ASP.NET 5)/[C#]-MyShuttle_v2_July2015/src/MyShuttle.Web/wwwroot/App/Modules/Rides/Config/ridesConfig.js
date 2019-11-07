'use strict';

angular.module('myShuttleRides').run(['navigationService',
    function(navigationService) {
        navigationService.addMenuOption('rides', 'Rides');
    }
]);
