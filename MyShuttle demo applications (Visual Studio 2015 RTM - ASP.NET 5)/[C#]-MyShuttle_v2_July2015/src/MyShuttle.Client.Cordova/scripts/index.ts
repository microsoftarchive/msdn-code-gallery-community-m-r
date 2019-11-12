/// <reference path="../plugins/cordova-plugin-appinsights/www/appinsights.d.ts" />
/// <reference path="../scripts/typings/cordova/plugins/device.d.ts" />

// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkID=397705
// To debug code on page load in Ripple or on Android devices/emulators: launch your app, set breakpoints, 
// and then run "window.location.reload()" in the JavaScript Console.

declare var Microsoft: any
(function () {
    "use strict";
        
    //Start by defining the main module and adding the module dependencies
    angular.module(ApplicationConfiguration.applicationName, ApplicationConfiguration.applicationModuleVendorDependencies);

    document.addEventListener('deviceready', onDeviceReady, false);

    function onDeviceReady() {

        if (device.platform == "windows") {
            Microsoft.Maps.loadModule('Microsoft.Maps.Map');
        }
        
        // Configure appInsights key
        appInsights.config.instrumentationKey = 'fee7300d-b698-43e9-8150-3ece9fbb6605';

        // Track app is started
        appInsights.trackPageView();
        var eventData = { Timestamp: new Date() };
        appInsights.trackEvent('deviceready', eventData);

        // Handle the Cordova pause and resume events
        document.addEventListener('pause', onPause, false);
        document.addEventListener('resume', onResume, false);

        // Then init the app
        angular.bootstrap(document, [ApplicationConfiguration.applicationName]);
    }

    function onPause() {
        // TODO: This application has been suspended. Save application state here.
    }

    function onResume() {
        // TODO: This application has been reactivated. Restore application state here.
    }

})();
