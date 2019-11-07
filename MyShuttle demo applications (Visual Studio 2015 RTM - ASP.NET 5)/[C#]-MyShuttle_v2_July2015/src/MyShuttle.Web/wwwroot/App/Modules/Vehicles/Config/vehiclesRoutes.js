'use strict';

angular.module('myShuttleVehicles').config(['$stateProvider',
	function($stateProvider) {
		$stateProvider.state('vehicles', {
                url: '/vehicles?page&q',
                templateUrl: 'App/Modules/Vehicles/Views/vehicles.html',
                data: {title: 'Vehicles Management - MyShuttle'}
            });

        $stateProvider.state('newVehicle', {
            url: '/vehicles/new',
            templateUrl: 'App/Modules/Vehicles/Views/form.html',
            data: {title: 'New Vehicle - MyShuttle'}
        });

        $stateProvider.state('editVehicle', {
            url: '/vehicles/edit/:id',
            templateUrl: 'App/Modules/Vehicles/Views/form.html',
            data: {title: 'Edit Vehicle - MyShuttle'}
        });

        $stateProvider.state('editDevice', {
            url: '/vehicles/editdevice/:deviceId',
            templateUrl: 'App/Modules/Vehicles/Views/form.html',
            data: {title: 'Edit Vehicle - MyShuttle'}
        });
	}
]);