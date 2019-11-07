// App ClientId from https://manage.windowsazure.com
var clientId = '5ed6a2c7-d62e-456b-b3cc-4ec8cde29dcd';

// OneDrive App
var oneDriveApp = angular.module('MicrosoftGraphSPA', [
    'ngRoute',
    'AdalAngular',
	'angular-loading-bar'
]);

oneDriveApp
    // Input file model binding
    .directive('fileModel', ['$parse', function ($parse) {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                var model = $parse(attrs.fileModel);
                var modelSetter = model.assign;

                element.bind('change', function () {
                    scope.$apply(function () {
                        modelSetter(scope, element[0].files[0]);
                    });
                });
            }
        };
    }]);

oneDriveApp
    .config(config)
    // Format file size
    .filter('bytes', function() {
        return function(bytes, precision) {
            if (isNaN(parseFloat(bytes)) || !isFinite(bytes)) return '-';
            if (typeof precision === 'undefined') precision = 1;
            var units = ['bytes', 'kB', 'MB', 'GB', 'TB', 'PB'],
                number = Math.floor(Math.log(bytes) / Math.log(1024));
            return (bytes / Math.pow(1024, Math.floor(number))).toFixed(precision) + ' ' + units[number];
        }
    })
    .run(function($rootScope, $location, adalAuthenticationService) {
        $rootScope.isAuthenticated = false;
        $rootScope.searchQuery = null;

        $rootScope.responseResult = null;
        $rootScope.responseSuccess = function(e) {
            console.log(e);
            $rootScope.responseResult = {
                error: false
            };
        }
        $rootScope.responseError = function(e) {
            console.log(e);
            $rootScope.responseResult = {
                error: true
            };
            if (e.data) {
                if (e.data.error.message) {
                    $rootScope.responseResult.errorText = e.data.error.message;
                } else {
                    $rootScope.responseResult.errorText = e.statusText;
                }
            } else {
                console.log(e);
            }
            if (!$rootScope.$$phase) {
                $rootScope.$apply();
            }
        }

        $rootScope.clearResponse = function() {
            $rootScope.responseResult = null;
        }

        $rootScope.search = function() {
            if ($rootScope.searchQuery) {
                $location
                    .path('/onedrive/search')
                    .search('q', $rootScope.searchQuery);
            }
        }

        $rootScope.connect = function() {
            adalAuthenticationService.login();

            if (!$rootScope.$$phase) {
                $rootScope.$apply();
            }
        };

        $rootScope.disconnect = function() {
            adalAuthenticationService.logOut();

            if (!$rootScope.$$phase) {
                $rootScope.$apply();
            }
        };

        if (adalAuthenticationService.userInfo.isAuthenticated) {
            $rootScope.isAuthenticated = true;
        } else {
            $rootScope.isAuthenticated = false;
        }
    });

function config($routeProvider, $httpProvider, adalAuthenticationServiceProvider, cfpLoadingBarProvider) {
$routeProvider
    // Routing
    .when('/onedrive', { //Root
        templateUrl: 'views/onedrive.html',
        controller: 'OneDriveController',
        controllerAs: 'controller'
    })
    .when('/onedrive/search', { //Search
        templateUrl: 'views/search.html',
        controller: 'SearchController',
        controllerAs: 'controller'
    })
    .when('/onedrive/:itemId', { //File/Folder
        templateUrl: 'views/onedrive.html',
        controller: 'OneDriveController',
        controllerAs: 'controller'
    })
    .when('/sp', { //SharePoint
    templateUrl: 'views/sp.html',
    controller: 'SPController',
    controllerAs: 'controller'
    })
    .when('/sp/:siteId', { //SharePoint/Site
    templateUrl: 'views/sp/site.html',
    controller: 'SPController',
    controllerAs: 'controller'
    })
    .when('/sp/:siteId/:listId', { //SharePoint/Site/List
    templateUrl: 'views/sp/list.html',
    controller: 'SPController',
    controllerAs: 'controller'
    })
    .when('/sp/:siteId/:listId/:itemId', { //SharePoint/Site/List/Item
    templateUrl: 'views/sp/item.html',
    controller: 'SPController',
    controllerAs: 'controller'
    })
    .otherwise({ //Root by default
        redirectTo: '/onedrive'
    });

    // ADAL
    adalAuthenticationServiceProvider.init(
    {
        clientId: clientId,
        endpoints: {
            'https://graph.microsoft.com': 'https://graph.microsoft.com'
        }
    }, $httpProvider);

    // Hide loading spinner
    cfpLoadingBarProvider.includeSpinner = false;
};