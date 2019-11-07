'use strict';

angular.module('myShuttleDrivers').run(['navigationService',
    function(navigationService) {
        navigationService.addMenuOption('drivers', 'Drivers');
    }
]);
