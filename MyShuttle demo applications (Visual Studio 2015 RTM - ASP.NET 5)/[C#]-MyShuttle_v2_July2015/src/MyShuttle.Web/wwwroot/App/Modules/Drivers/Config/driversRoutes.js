'use strict';

angular.module('myShuttleDrivers').config(['$stateProvider',
	function($stateProvider) {
        $stateProvider.state('drivers', {
            url: '/drivers?page&q',
            templateUrl: 'App/Modules/Drivers/Views/drivers.html',
            data: {title: 'Drivers Management - MyShuttle'}
        });

        $stateProvider.state('newDriver', {
            url: '/drivers/new',
            templateUrl: 'App/Modules/Drivers/Views/form.html',
            data: {title: 'New Driver - MyShuttle'}
        });

        $stateProvider.state('editDriver', {
            url: '/drivers/edit/:id',
            templateUrl: 'App/Modules/Drivers/Views/form.html',
            data: {title: 'Edit Driver - MyShuttle'}
        });
	}
]);