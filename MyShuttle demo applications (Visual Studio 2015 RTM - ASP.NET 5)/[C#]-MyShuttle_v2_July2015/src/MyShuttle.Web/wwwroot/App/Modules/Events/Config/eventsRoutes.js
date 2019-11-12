'use strict';

angular.module('myShuttleEvents').config(['$stateProvider',
	function($stateProvider) {
	    $stateProvider.state('events', {
			url: '/events',
			templateUrl: 'App/Modules/Events/Views/events.html',
            data: {title: 'Events - MyShuttle'}
		});
	}
]);