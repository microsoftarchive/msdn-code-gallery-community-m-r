'use strict';

angular.module('myShuttleVehicles').run(['navigationService',
    function(navigationService) {
        navigationService.addMenuOption('vehicles', 'Vehicles');
    }
]);
