/// <reference path="../settings.ts" />

module MyShuttle.Settings {
    angularModule.config(function ($routeProvider: ng.route.IRouteProvider) {
        $routeProvider.when('/settings', {
            controller: 'SettingsController',
            templateUrl: 'app/modules/settings/views/settings.html'
        });
    });
}