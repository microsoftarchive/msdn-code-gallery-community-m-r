'use strict';

angular.module('myShuttleEvents').run(['navigationService',
    function(navigationService) {
        navigationService.addMenuOption('events', 'Events');
    }
]);
