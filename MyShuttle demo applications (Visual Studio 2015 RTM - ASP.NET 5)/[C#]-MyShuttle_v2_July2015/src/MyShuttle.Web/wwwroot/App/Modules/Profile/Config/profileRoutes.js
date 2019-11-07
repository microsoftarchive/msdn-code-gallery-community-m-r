'use strict';

angular.module('myShuttleProfile').config(['$stateProvider',
	function($stateProvider) {
        $stateProvider.state('profile', {
			url: '/profile',
			templateUrl: 'App/Modules/Profile/Views/profile.html',
            data: {title: 'Edit Profile - MyShuttle'}
		});
	}
]);