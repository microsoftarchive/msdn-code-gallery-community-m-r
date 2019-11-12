'use strict';

angular.module('myShuttleDashboard').run(['navigationService',
    function(navigationService) {
        navigationService.addMenuOption('dashboard', 'Dashboard');
    }
]);
