// Init the application configuration module for AngularJS application
interface IApplicationConfiguration {
    applicationName: string;
    applicationModuleVendorDependencies: string[];
    registerModule(moduleName: string): void;
}

var ApplicationConfiguration = ((): IApplicationConfiguration => {
    // Init module configuration options
    var applicationName = 'myShuttleDriverApp';
    var applicationModuleVendorDependencies = ['ngRoute', 'ui.bootstrap', 'angularMoment'];

    // Add a new vertical module
    var registerModule = function (moduleName) {
        angular.module(applicationName).requires.push(moduleName);
    };

    return {
        applicationName: applicationName,
        applicationModuleVendorDependencies: applicationModuleVendorDependencies,
        registerModule: registerModule
    };
})();