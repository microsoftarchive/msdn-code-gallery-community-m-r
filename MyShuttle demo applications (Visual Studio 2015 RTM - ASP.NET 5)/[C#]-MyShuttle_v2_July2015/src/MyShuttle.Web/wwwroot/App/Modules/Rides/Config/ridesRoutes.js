'use strict';

angular.module('myShuttleRides').config(['$stateProvider',
	function($stateProvider) {
		$stateProvider.state('rides', {
			url: '/rides?page&vehicleId&driverId',
			templateUrl: 'App/Modules/Rides/Views/rides.html',
            data: {title: 'Rides - MyShuttle'}
		});
	}
]);