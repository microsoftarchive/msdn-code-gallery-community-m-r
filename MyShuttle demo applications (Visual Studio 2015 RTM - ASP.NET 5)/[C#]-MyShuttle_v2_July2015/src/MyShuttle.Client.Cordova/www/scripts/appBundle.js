var ApplicationConfiguration = (function () {
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
/// <reference path="../plugins/cordova-plugin-appinsights/www/appinsights.d.ts" />
/// <reference path="../scripts/typings/cordova/plugins/device.d.ts" />
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
// Platform specific overrides will be placed in the merges folder versions of this file 
var MyShuttle;
(function (MyShuttle) {
    var Core;
    (function (Core) {
        var Module = (function () {
            function Module() {
                this.name = 'ms.Core';
                this._module = angular.module(this.name, ['ngRoute']);
            }
            Module.prototype.register = function () {
                ApplicationConfiguration.registerModule(this.name);
                return this._module;
            };
            return Module;
        })();
        Core.Module = Module;
        Core.angularModule = new Core.Module().register();
    })(Core = MyShuttle.Core || (MyShuttle.Core = {}));
})(MyShuttle || (MyShuttle = {}));
/// <reference path="../core.ts" />
var MyShuttle;
(function (MyShuttle) {
    var Core;
    (function (Core) {
        Core.angularModule.config(function ($provide) {
            $provide.decorator("$exceptionHandler", function ($delegate) {
                return function (exception, cause) {
                    // Track all exceptions in AppInsights
                    appInsights.trackException(exception);
                    $delegate(exception, cause);
                };
            });
        });
    })(Core = MyShuttle.Core || (MyShuttle.Core = {}));
})(MyShuttle || (MyShuttle = {}));
var MyShuttle;
(function (MyShuttle) {
    var Core;
    (function (Core) {
        Core.angularModule.config(function ($httpProvider, $compileProvider) {
            $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|file|ghttps?|ms-appx|x-wmapp0):/);
            var customInterceptor = function ($q, $injector) {
                var serviceName = 'httpInterceptorService';
                return {
                    request: function (config) {
                        var service = $injector.get(serviceName);
                        service.startRequest();
                        return config || $q.when(config);
                    },
                    response: function (response) {
                        var service = $injector.get(serviceName);
                        service.requestSuccess(response);
                        return response || $q.when(response);
                    },
                    responseError: function (rejection) {
                        var service = $injector.get(serviceName);
                        service.requestError(rejection);
                        return $q.reject(rejection);
                    }
                };
            };
            $httpProvider.interceptors.push(customInterceptor);
        });
    })(Core = MyShuttle.Core || (MyShuttle.Core = {}));
})(MyShuttle || (MyShuttle = {}));
/// <reference path="../core.ts" />
var MyShuttle;
(function (MyShuttle) {
    var Core;
    (function (Core) {
        Core.angularModule.config(function ($routeProvider) {
            $routeProvider.otherwise({ redirectTo: '/home' });
        });
    })(Core = MyShuttle.Core || (MyShuttle.Core = {}));
})(MyShuttle || (MyShuttle = {}));
/// <reference path="../core.ts" />
var MyShuttle;
(function (MyShuttle) {
    var Core;
    (function (Core) {
        Core.angularModule.run(function (navigationService) {
            document.addEventListener("backbutton", onBackKeyDown.bind(this), false);
            function onBackKeyDown() {
                navigationService.navigateBack();
            }
            ;
        });
    })(Core = MyShuttle.Core || (MyShuttle.Core = {}));
})(MyShuttle || (MyShuttle = {}));
/// <reference path="../core.ts" />
var MyShuttle;
(function (MyShuttle) {
    var Core;
    (function (Core) {
        Core.angularModule.controller('HeaderController', function ($scope, $rootScope, messengerService, navigationService) {
            var init = function () {
            };
            var jumpToMainPage, cleanUpShowBackBtn = $rootScope.$on(messengerService.messageTypes.showNavigateBackBtn, function (event, params) {
                $scope.backBtnVisible = true;
                if (params)
                    jumpToMainPage = params.jumpToMainPage;
            }), cleanUpHideBackBtn = $rootScope.$on(messengerService.messageTypes.hideNavigateBackBtn, function (event, params) {
                $scope.backBtnVisible = false;
            });
            $scope.navigateBack = function () {
                $scope.backBtnVisible = false;
                if (jumpToMainPage) {
                    navigationService.navigateTo('home');
                }
                else {
                    navigationService.navigateBack();
                }
            };
            var cleanUpDestroy = $scope.$on('$destroy', function () {
                cleanUpShowBackBtn();
                cleanUpHideBackBtn();
            });
            init();
        });
    })(Core = MyShuttle.Core || (MyShuttle.Core = {}));
})(MyShuttle || (MyShuttle = {}));
/// <reference path="../core.ts" />
var MyShuttle;
(function (MyShuttle) {
    var Core;
    (function (Core) {
        Core.angularModule.directive('msLoader', function (messengerService) {
            return {
                restrict: 'AE',
                link: function (scope, element) {
                    // hide the element initially
                    element.addClass('hidden');
                    var cleanUpStartLoading = scope.$on(messengerService.messageTypes.startLoading, function (event) {
                        element.removeClass('hidden');
                    });
                    var cleanUpEndLoading = scope.$on(messengerService.messageTypes.endLoading, function (event) {
                        element.addClass('hidden');
                    });
                    var cleanUpDestroy = scope.$on('$destroy', function () {
                        cleanUpStartLoading();
                        cleanUpEndLoading();
                        cleanUpDestroy();
                    });
                }
            };
        });
    })(Core = MyShuttle.Core || (MyShuttle.Core = {}));
})(MyShuttle || (MyShuttle = {}));
/// <reference path="../core.ts" />
var MyShuttle;
(function (MyShuttle) {
    var Core;
    (function (Core) {
        var HttpInterceptorService = (function () {
            function HttpInterceptorService($http, messengerService, $q) {
                this.$http = $http;
                this.messengerService = messengerService;
                this.$q = $q;
                this.requestCompleted = function (forceEndLoading) {
                    // don't send notification until all requests are complete
                    if (forceEndLoading || (this.$http.pendingRequests.length < 1)) {
                        this.messengerService.send(this.messengerService.messageTypes.endLoading);
                    }
                };
                this.startRequest = function () {
                    this.messengerService.send(this.messengerService.messageTypes.startLoading);
                };
                this.requestSuccess = function (response) {
                    this.requestCompleted(false);
                    return response;
                };
                this.requestError = function (response) {
                    this.requestCompleted(false);
                    // TODO: Show error message
                    console.log('An error ocurred: ' + response);
                    return this.$q.reject(response);
                };
                return this;
            }
            return HttpInterceptorService;
        })();
        Core.HttpInterceptorService = HttpInterceptorService;
        Core.angularModule.service('httpInterceptorService', HttpInterceptorService);
    })(Core = MyShuttle.Core || (MyShuttle.Core = {}));
})(MyShuttle || (MyShuttle = {}));
/// <reference path="../core.ts" />
var MyShuttle;
(function (MyShuttle) {
    var Core;
    (function (Core) {
        var MessengerService = (function () {
            function MessengerService($rootScope) {
                this.$rootScope = $rootScope;
                this.messageTypes = {
                    startLoading: '_START_LOADING_',
                    endLoading: '_END_LOADING_',
                    showNavigateBackBtn: '_SHOW_NAVIGATE_BACK_BTN_',
                    hideNavigateBackBtn: '_HIDE_NAVIGATE_BACK_BTN_',
                    getSignature: '_GET_SIGNATURE_',
                    settingsChanged: '_SETTINGS_CHANGED_'
                };
                return this;
            }
            MessengerService.prototype.send = function (message, data) {
                this.$rootScope.$broadcast(message, data);
            };
            return MessengerService;
        })();
        Core.MessengerService = MessengerService;
        Core.angularModule.service('messengerService', MessengerService);
    })(Core = MyShuttle.Core || (MyShuttle.Core = {}));
})(MyShuttle || (MyShuttle = {}));
/// <reference path="../core.ts" />
var MyShuttle;
(function (MyShuttle) {
    var Core;
    (function (Core) {
        var NavigationService = (function () {
            function NavigationService($rootScope, $timeout, $location, $anchorScroll) {
                this.$rootScope = $rootScope;
                this.$timeout = $timeout;
                this.$location = $location;
                this.$anchorScroll = $anchorScroll;
                this.urlHistory = [];
                return this;
            }
            NavigationService.prototype.start = function () {
                var location = this.$location;
                var history = this.urlHistory;
                this.$rootScope.$on('$routeChangeSuccess', function () {
                    if (location.absUrl().split('#')[1] !== history[history.length - 1]) {
                        history.push(location.absUrl().split('#')[1]);
                    }
                });
            };
            NavigationService.prototype.navigateTo = function (url, data) {
                this.transferData = data;
                var $location = this.$location;
                var $anchorScroll = this.$anchorScroll;
                this.$timeout(function () {
                    $location.path(url);
                    $anchorScroll();
                }, 10);
            };
            NavigationService.prototype.getTransferedData = function () {
                return this.transferData;
            };
            NavigationService.prototype.navigateBack = function () {
                var location = this.$location;
                var history = this.urlHistory;
                this.$timeout(function () {
                    if (location.path() === '/' || location.path() === '/home') {
                        navigator.app.exitApp();
                        return;
                    }
                    ;
                    history.pop();
                    location.path(history[history.length - 1]);
                }, 10);
            };
            return NavigationService;
        })();
        Core.NavigationService = NavigationService;
        Core.angularModule.factory('navigationService', function ($rootScope, $timeout, $location, $anchorScroll) {
            var instance = new MyShuttle.Core.NavigationService($rootScope, $timeout, $location, $anchorScroll);
            instance.start();
            return instance;
        });
    })(Core = MyShuttle.Core || (MyShuttle.Core = {}));
})(MyShuttle || (MyShuttle = {}));
/// <reference path="../core.ts" />
var MyShuttle;
(function (MyShuttle) {
    var Core;
    (function (Core) {
        var Vehicle = (function () {
            function Vehicle() {
            }
            return Vehicle;
        })();
        Core.Vehicle = Vehicle;
        var Coordinate = (function () {
            function Coordinate(latitude, longitude) {
                this.latitude = latitude;
                this.longitude = longitude;
            }
            return Coordinate;
        })();
        Core.Coordinate = Coordinate;
        var SettingsService = (function () {
            function SettingsService(storageService) {
                this.storageService = storageService;
                this.defaultEmployeeEmail = 'amanda@microsoft.com';
                this.vehicle = new Vehicle();
                this.vehicle.VehicleId = 5;
                this.vehicle.DriverId = 5;
                this.vehicle.CarrierId = 1;
                this.vehicle.Rate = 3;
                var coord = new Coordinate(47.641944, -122.127222);
                this.startRideLocation = coord;
                this.endRideLocation = coord;
                this.rideDistance = 0.1;
                this.rideAddress = 'Microsoft Redmond Campus, Redmond, Washington';
                this.bingMapsKey = 'YOUR_BING_MAPS_TOKEN_KEY';
                this.mobileServiceKey = 'YOUR_MOBILE_SERVICE_KEY';
                this.gcmSenderId = 'SENDER_ID';
                this.realTimeNotificationsServerUrl = 'http://YOUR_SITE.azurewebsites.net/web/';
                return this;
            }
            SettingsService.prototype.getMobileServiceUrl = function () {
                return this.storageService.getValue('serviceUrl', 'https://YOUR_SITE.azure-mobile.net/');
            };
            return SettingsService;
        })();
        Core.SettingsService = SettingsService;
        Core.angularModule.factory('settingsService', function (storageService) {
            return new MyShuttle.Core.SettingsService(storageService);
        });
    })(Core = MyShuttle.Core || (MyShuttle.Core = {}));
})(MyShuttle || (MyShuttle = {}));
/// <reference path="../core.ts" />
var MyShuttle;
(function (MyShuttle) {
    var Core;
    (function (Core) {
        var StorageService = (function () {
            function StorageService($window) {
                this.$window = $window;
                return this;
            }
            StorageService.prototype.getValue = function (key, defaultValue) {
                return this.$window.localStorage.getItem(key) || defaultValue;
            };
            StorageService.prototype.setValue = function (key, value) {
                this.$window.localStorage.setItem(key, value);
            };
            return StorageService;
        })();
        Core.StorageService = StorageService;
        Core.angularModule.factory('storageService', function ($window) {
            return new MyShuttle.Core.StorageService($window);
        });
    })(Core = MyShuttle.Core || (MyShuttle.Core = {}));
})(MyShuttle || (MyShuttle = {}));
var MyShuttle;
(function (MyShuttle) {
    var Rides;
    (function (Rides) {
        var Module = (function () {
            function Module() {
                this.name = 'ms.Rides';
                this._module = angular.module(this.name, ['ngRoute']);
            }
            Module.prototype.register = function () {
                ApplicationConfiguration.registerModule(this.name);
                return this._module;
            };
            return Module;
        })();
        Rides.Module = Module;
        Rides.angularModule = new Rides.Module().register();
    })(Rides = MyShuttle.Rides || (MyShuttle.Rides = {}));
})(MyShuttle || (MyShuttle = {}));
/// <reference path="../rides.ts" />
var MyShuttle;
(function (MyShuttle) {
    var Rides;
    (function (Rides) {
        'use strict';
        Rides.angularModule.config(function ($routeProvider) {
            $routeProvider.when('/home', {
                controller: 'HomeController',
                templateUrl: 'app/modules/rides/views/home.html'
            });
            $routeProvider.when('/service', {
                controller: 'ServiceController',
                templateUrl: 'app/modules/rides/views/service.html',
                resolve: {
                    'params': function (navigationService) {
                        return navigationService.getTransferedData();
                    }
                }
            });
            $routeProvider.when('/ride', {
                controller: 'RideController',
                templateUrl: 'app/modules/rides/views/ride.html',
                resolve: {
                    'params': function (navigationService) {
                        return navigationService.getTransferedData();
                    }
                }
            });
            $routeProvider.when('/signature', {
                controller: 'SignatureController',
                templateUrl: 'app/modules/rides/views/signature.html',
                resolve: {
                    'params': function (navigationService) {
                        return navigationService.getTransferedData();
                    }
                }
            });
        });
    })(Rides = MyShuttle.Rides || (MyShuttle.Rides = {}));
})(MyShuttle || (MyShuttle = {}));
/// <reference path="../rides.ts" />
var MyShuttle;
(function (MyShuttle) {
    var Rides;
    (function (Rides) {
        Rides.angularModule.controller('HomeController', function ($scope, pushNotificationsService, settingsService, dataService, messengerService, modalService, navigationService) {
            var timeRequest;
            var showRequest = function (data) {
                var employeeId = data.employeeId;
                var position = new MyShuttle.Core.Coordinate(data.latitude, data.longitude);
                messengerService.send(messengerService.messageTypes.startLoading);
                dataService.getEmployee(employeeId).done(function (results) {
                    messengerService.send(messengerService.messageTypes.endLoading);
                    var employee = results[0];
                    modalService.showRideRequest(employee, position).then(function (result) {
                        if (result) {
                            pushNotificationsService.notifyApprovedRequest(employeeId);
                            var params = { employee: employee, position: position, timeRequest: timeRequest };
                            navigationService.navigateTo('service', params);
                        }
                        else {
                            pushNotificationsService.notifyRejectedRequest(employeeId);
                        }
                    });
                });
            };
            var init = function () {
                pushNotificationsService.initPushNotifications(function (data) {
                    timeRequest = moment();
                    showRequest(data);
                });
            };
            init();
        });
    })(Rides = MyShuttle.Rides || (MyShuttle.Rides = {}));
})(MyShuttle || (MyShuttle = {}));
/// <reference path="../rides.ts" />
var MyShuttle;
(function (MyShuttle) {
    var Rides;
    (function (Rides) {
        Rides.angularModule.controller('RideController', function ($scope, settingsService, navigationService, params) {
            var employee;
            var rideDuration;
            var init = function () {
                $scope.distance = settingsService.rideDistance;
                $scope.duration = 0;
                $scope.cost = 0;
                employee = params;
            };
            $scope.startRide = function () {
                $scope.startRideTime = moment().milliseconds(0);
                $scope.startRideLocation = {
                    latitude: settingsService.startRideLocation.latitude,
                    longitude: settingsService.startRideLocation.longitude
                };
                var eventData = {
                    date: $scope.startRideTime.toDate(),
                    latitude: $scope.startRideLocation.latitude,
                    longitude: $scope.startRideLocation.longitude
                };
                appInsights.trackEvent('startRide', eventData);
            };
            $scope.endRide = function () {
                $scope.endRideTime = moment().milliseconds(0);
                $scope.endRideLocation = {
                    latitude: settingsService.endRideLocation.latitude,
                    longitude: settingsService.endRideLocation.longitude
                };
                var eventData = {
                    date: $scope.endRideTime.toDate(),
                    latitude: $scope.startRideLocation.latitude,
                    longitude: $scope.startRideLocation.longitude
                };
                appInsights.trackEvent('endRide', eventData);
            };
            $scope.endOfRoute = function (distance) {
                if (distance)
                    $scope.distance = distance;
                $scope.cost = Math.round($scope.distance * settingsService.vehicle.Rate * 10) / 10;
                $scope.duration = $scope.endRideTime.diff($scope.startRideTime);
                rideDuration = $scope.endRideTime.diff($scope.startRideTime, 'seconds');
            };
            $scope.sign = function () {
                var params = {
                    distance: $scope.distance,
                    duration: rideDuration,
                    cost: $scope.cost,
                    startRideTime: $scope.startRideTime,
                    endRideTime: $scope.endRideTime,
                    startRideLocation: $scope.startRideLocation,
                    endRideLocation: $scope.endRideLocation,
                    employee: employee
                };
                navigationService.navigateTo('signature', params);
            };
            init();
        });
    })(Rides = MyShuttle.Rides || (MyShuttle.Rides = {}));
})(MyShuttle || (MyShuttle = {}));
/// <reference path="../rides.ts" />
var MyShuttle;
(function (MyShuttle) {
    var Rides;
    (function (Rides) {
        Rides.angularModule.controller('ServiceController', function ($scope, pushNotificationsService, navigationService, params) {
            $scope.notifyEmployee = function () {
                pushNotificationsService.notifyVehicleArrived($scope.employee.id).then(function () {
                    navigationService.navigateTo('ride', $scope.employee);
                });
            };
            var init = function () {
                if (!params) {
                    navigationService.navigateTo('home');
                }
                $scope.employee = params.employee;
                $scope.position = params.position;
                $scope.timeRequest = params.timeRequest;
            };
            init();
        });
    })(Rides = MyShuttle.Rides || (MyShuttle.Rides = {}));
})(MyShuttle || (MyShuttle = {}));
/// <reference path="../rides.ts" />
var MyShuttle;
(function (MyShuttle) {
    var Rides;
    (function (Rides) {
        Rides.angularModule.controller('SignatureController', function ($scope, dataService, navigationService, messengerService, settingsService, params) {
            var ride;
            $scope.sendRide = function () {
                if (!$scope.signatureForm.$valid)
                    return;
                messengerService.send(messengerService.messageTypes.getSignature, function (signature) {
                    ride.signature = signature;
                    ride.employeeEmail = $scope.email;
                    messengerService.send(messengerService.messageTypes.startLoading);
                    dataService.addRide(ride).done(function () {
                        messengerService.send(messengerService.messageTypes.endLoading);
                        navigationService.navigateTo('home');
                    }, function () {
                        messengerService.send(messengerService.messageTypes.endLoading);
                        $scope.email = '';
                        $scope.$digest();
                    });
                });
            };
            var init = function () {
                ride = params || {};
                messengerService.send(messengerService.messageTypes.showNavigateBackBtn, { jumpToMainPage: true });
                $scope.email = (ride.employee && ride.employee.email) || settingsService.defaultEmployeeEmail;
                $scope.$on('$locationChangeStart', function (event) {
                    messengerService.send(messengerService.messageTypes.hideNavigateBackBtn);
                });
            };
            init();
        });
    })(Rides = MyShuttle.Rides || (MyShuttle.Rides = {}));
})(MyShuttle || (MyShuttle = {}));
/// <reference path="../rides.ts" />
var MyShuttle;
(function (MyShuttle) {
    var Rides;
    (function (Rides) {
        Rides.angularModule.directive('msBingMapsRoutes', ['settingsService', function (settingsService) {
                var link = function (scope, element, attrs) {
                    var MM = Microsoft.Maps;
                    var mapOptions = {
                        zoom: 16,
                        showScalebar: false,
                        enableSearchLogo: false,
                        showMapTypeSelector: false,
                        showDashboard: false,
                        credentials: settingsService.bingMapsKey,
                        center: MM.Location,
                        bounds: MM.LocationRect
                    };
                    var showMapInSimpleMode = !scope.startLocation || (scope.startLocation.latitude == scope.endLocation.latitude && scope.startLocation.longitude == scope.endLocation.longitude);
                    if (showMapInSimpleMode) {
                        mapOptions.center = new MM.Location(scope.endLocation.latitude, scope.endLocation.longitude);
                    }
                    else {
                        mapOptions.bounds = MM.LocationRect.fromCorners(scope.startLocation, scope.endLocation);
                    }
                    ;
                    var map = new MM.Map(element[0].firstChild, mapOptions);
                    if (!showMapInSimpleMode) {
                        MM.loadModule('Microsoft.Maps.Directions', { callback: directionsModuleLoaded });
                    }
                    var endPushpinOptions = {
                        icon: 'images/pin_icon_full.png',
                        width: 50,
                        height: 50,
                        anchor: { x: 25, y: 25 }
                    };
                    var endPinLocation = new MM.Pushpin(scope.endLocation, endPushpinOptions);
                    if (showMapInSimpleMode) {
                        if (scope.trackPositionChanges) {
                            $.connection.hub = $.hubConnection(settingsService.realTimeNotificationsServerUrl + 'signalr/js', { useDefaultPath: false });
                            var hub = $.connection.hub;
                            var proxy = hub.createHubProxy('myShuttleHub');
                            proxy.on('updateEmployeePosition', function (employeeId, latitude, longitude) {
                                console.log('updateEmployeePosition received');
                                map.entities.clear();
                                var pin = new MM.Pushpin(new Microsoft.Maps.Location(latitude, longitude), endPushpinOptions);
                                map.entities.push(pin);
                            });
                            $.connection.hub.start()
                                .done(function () {
                                if ($.connection.hub.state === $.signalR.connectionState.connected) {
                                    console.log('Hub connected');
                                }
                            });
                        }
                        map.entities.push(endPinLocation);
                        if (attrs.endRouteCallback) {
                            scope.endRouteCallback({ distanceArg: 0 });
                        }
                    }
                    function directionsModuleLoaded() {
                        var directionsManager = new MM.Directions.DirectionsManager(map);
                        var directionsManagerRequestOptions = {
                            routeMode: MM.Directions.RouteMode.driving,
                            routeDraggable: false,
                            distanceUnit: 1
                        };
                        var directionsManagerRenderOptions = {
                            displayRouteSelector: false,
                            drivingPolylineOptions: { strokeColor: new MM.Color(255, 255, 127, 102), strokeThickness: 3 },
                            autoUpdateMapView: true
                        };
                        if (showMapInSimpleMode) {
                            directionsManagerRenderOptions.autoUpdateMapView = false;
                        }
                        directionsManager.setRequestOptions(directionsManagerRequestOptions);
                        directionsManager.setRenderOptions(directionsManagerRenderOptions);
                        var directionsUpdatedEventObj = MM.Events.addHandler(directionsManager, 'directionsUpdated', onDdirectionsUpdated);
                        var startPushpinOptions = {
                            htmlContent: '<div class="start-pushpin"></div>',
                            width: 8,
                            height: 8,
                            anchor: { x: 5, y: 5 }
                        };
                        var startPinLocation = new MM.Pushpin(scope.startLocation, startPushpinOptions);
                        var startWaypointLocation = new MM.Directions.Waypoint({ location: scope.startLocation, pushpin: startPinLocation });
                        directionsManager.addWaypoint(startWaypointLocation);
                        var endWaypointLocation = new MM.Directions.Waypoint({ location: scope.endLocation, pushpin: endPinLocation });
                        directionsManager.addWaypoint(endWaypointLocation);
                        directionsManager.calculateDirections();
                    }
                    ;
                    function onDdirectionsUpdated(e) {
                        var distance = Math.round(e.routeSummary[0].distance * 10) / 10;
                        if (attrs.endRouteCallback) {
                            scope.$apply(function () {
                                scope.endRouteCallback({ distanceArg: distance });
                            });
                        }
                    }
                    ;
                };
                return {
                    restrict: 'E',
                    scope: {
                        startLocation: '=',
                        endLocation: '=',
                        trackPositionChanges: '=',
                        endRouteCallback: '&'
                    },
                    template: '<div id="mapDiv"></div>',
                    link: link
                };
            }]);
    })(Rides = MyShuttle.Rides || (MyShuttle.Rides = {}));
})(MyShuttle || (MyShuttle = {}));
var MyShuttle;
(function (MyShuttle) {
    var Rides;
    (function (Rides) {
        Rides.angularModule.directive('msSignaturePad', function ($window, messengerService) {
            var link = function (scope, element, attrs) {
                var padOptions = {
                    drawOnly: true,
                    defaultAction: 'drawIt',
                    validateFields: false,
                    lineWidth: 0,
                    output: null,
                    sigNav: null,
                    name: null,
                    typed: null,
                    typeIt: null,
                    drawIt: null,
                    typeItDesc: null,
                    drawItDesc: null
                };
                //var form = element.find('form');
                var $form = $('form.canvas-wrapper');
                var control = $form.signaturePad(padOptions);
                var canvas = element.find('.pad')[0];
                function resizeCanvas() {
                    var ratio = $window.devicePixelRatio || 1;
                    canvas.width = $form.outerWidth();
                    canvas.height = $form.outerHeight();
                }
                $window.onresize = resizeCanvas;
                resizeCanvas();
                scope.$on(messengerService.messageTypes.getSignature, function (event, callback) {
                    var signature = control.getSignatureImage();
                    if (signature && signature.indexOf('data:image/png;base64,') === 0) {
                        signature = signature.split('base64,')[1];
                    }
                    callback(signature);
                });
            };
            return {
                restrict: 'E',
                template: '<form method="POST" class="canvas-wrapper"><canvas class="pad"></canvas></form>',
                link: link
            };
        });
    })(Rides = MyShuttle.Rides || (MyShuttle.Rides = {}));
})(MyShuttle || (MyShuttle = {}));
/// <reference path="../rides.ts" />
var MyShuttle;
(function (MyShuttle) {
    var Rides;
    (function (Rides) {
        Rides.angularModule.filter('imageBase64', function () {
            return function (bytes) {
                if (bytes) {
                    if (bytes.slice(0, 11) !== 'data:image/') {
                        bytes = 'data:image/png;base64,' + bytes;
                    }
                    return bytes;
                }
                else {
                    return '';
                }
            };
        });
    })(Rides = MyShuttle.Rides || (MyShuttle.Rides = {}));
})(MyShuttle || (MyShuttle = {}));
/// <reference path="../rides.ts" />
var MyShuttle;
(function (MyShuttle) {
    var Rides;
    (function (Rides) {
        var Employee = (function () {
            function Employee() {
            }
            return Employee;
        })();
        Rides.Employee = Employee;
        var Ride = (function () {
            function Ride() {
            }
            return Ride;
        })();
        Rides.Ride = Ride;
        var DataService = (function () {
            function DataService($rootScope, settingsService, messengerService) {
                var _this = this;
                this.$rootScope = $rootScope;
                this.settingsService = settingsService;
                this.messengerService = messengerService;
                this.createClient();
                this.$rootScope.$on(this.messengerService.messageTypes.settingsChanged, function () {
                    _this.createClient();
                });
                return this;
            }
            DataService.prototype.createClient = function () {
                this.client = new WindowsAzure.MobileServiceClient(this.settingsService.getMobileServiceUrl(), this.settingsService.mobileServiceKey);
                this.rideTable = this.client.getTable('ride');
                this.employeeTable = this.client.getTable('employee');
            };
            DataService.prototype.getEmployee = function (id) {
                return this.employeeTable.where({ Id: id }).read();
            };
            DataService.prototype.addRide = function (data) {
                var ride = new Ride();
                ride.StartDateTime = data.startRideTime.toDate();
                ride.EndDateTime = data.endRideTime.toDate();
                ride.StartLatitude = data.startRideLocation.latitude;
                ride.StartLongitude = data.startRideLocation.longitude;
                ride.EndLatitude = data.endRideLocation.latitude;
                ride.EndLongitude = data.endRideLocation.longitude;
                ride.StartAddress = this.settingsService.rideAddress; // TODO: Calculate
                ride.EndAddress = this.settingsService.rideAddress;
                ride.Distance = data.distance;
                ride.Duration = data.duration;
                ride.Cost = data.cost;
                ride.Signature = data.signature;
                ride.VehicleId = this.settingsService.vehicle.VehicleId;
                ride.CarrierId = this.settingsService.vehicle.CarrierId;
                ride.DriverId = this.settingsService.vehicle.DriverId;
                ride.Employee = new Employee();
                ride.Employee.Email = data.employeeEmail;
                return this.rideTable.insert(ride);
            };
            return DataService;
        })();
        Rides.DataService = DataService;
        Rides.angularModule.factory('dataService', function ($rootScope, settingsService, messengerService) {
            return new MyShuttle.Rides.DataService($rootScope, settingsService, messengerService);
        });
    })(Rides = MyShuttle.Rides || (MyShuttle.Rides = {}));
})(MyShuttle || (MyShuttle = {}));
/// <reference path="../rides.ts" />
var MyShuttle;
(function (MyShuttle) {
    var Rides;
    (function (Rides) {
        var ModalService = (function () {
            function ModalService($modal) {
                this.$modal = $modal;
                this.modalOptions = {
                    backdrop: 'static',
                    keyboard: false,
                    modalFade: true
                };
                return this;
            }
            ModalService.prototype.showRideRequest = function (employee, position) {
                this.modalOptions.templateUrl = 'app/modules/rides/views/rideRequest.html';
                this.modalOptions.controller = function ($scope) {
                    $scope.employee = employee;
                    $scope.position = position;
                };
                return this.$modal.open(this.modalOptions).result;
            };
            return ModalService;
        })();
        Rides.ModalService = ModalService;
        Rides.angularModule.factory('modalService', function ($modal) {
            return new MyShuttle.Rides.ModalService($modal);
        });
    })(Rides = MyShuttle.Rides || (MyShuttle.Rides = {}));
})(MyShuttle || (MyShuttle = {}));
var MyShuttle;
(function (MyShuttle) {
    var Settings;
    (function (Settings) {
        var Module = (function () {
            function Module() {
                this.name = 'ms.Settings';
                this._module = angular.module(this.name, ['ngRoute']);
            }
            Module.prototype.register = function () {
                ApplicationConfiguration.registerModule(this.name);
                return this._module;
            };
            return Module;
        })();
        Settings.Module = Module;
        Settings.angularModule = new Settings.Module().register();
    })(Settings = MyShuttle.Settings || (MyShuttle.Settings = {}));
})(MyShuttle || (MyShuttle = {}));
/// <reference path="../settings.ts" />
var MyShuttle;
(function (MyShuttle) {
    var Settings;
    (function (Settings) {
        Settings.angularModule.config(function ($routeProvider) {
            $routeProvider.when('/settings', {
                controller: 'SettingsController',
                templateUrl: 'app/modules/settings/views/settings.html'
            });
        });
    })(Settings = MyShuttle.Settings || (MyShuttle.Settings = {}));
})(MyShuttle || (MyShuttle = {}));
/// <reference path="../../core/core.ts" />
/// <reference path="../settings.ts" />
var MyShuttle;
(function (MyShuttle) {
    var Settings;
    (function (Settings) {
        Settings.angularModule.controller('SettingsController', function ($scope, settingsService, storageService, messengerService, navigationService) {
            $scope.saveSettings = function () {
                storageService.setValue('serviceUrl', $scope.serviceUrl);
                messengerService.send(messengerService.messageTypes.settingsChanged);
                navigationService.navigateBack();
            };
            var init = function () {
                messengerService.send(messengerService.messageTypes.showNavigateBackBtn, { jumpToMainPage: false });
                $scope.serviceUrl = settingsService.getMobileServiceUrl();
                $scope.$on('$locationChangeStart', function (event) {
                    messengerService.send(messengerService.messageTypes.hideNavigateBackBtn);
                });
            };
            init();
        });
    })(Settings = MyShuttle.Settings || (MyShuttle.Settings = {}));
})(MyShuttle || (MyShuttle = {}));
//# sourceMappingURL=appBundle.js.map