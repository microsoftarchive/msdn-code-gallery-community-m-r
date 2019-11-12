'use strict';

angular.module('myShuttleDashboard').config(['$stateProvider',
	function($stateProvider) {
		$stateProvider.state('dashboard', {
			url: '/dashboard',
			templateUrl: 'App/Modules/Dashboard/Views/dashboard.html',
            data: {title: 'Carrier Dashboard - MyShuttle'}
		});
	}
]);