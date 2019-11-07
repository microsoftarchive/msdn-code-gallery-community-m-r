/// <reference path="../rides.ts" />

module MyShuttle.Rides {
    declare var Microsoft;

    angularModule.directive('msBingMapsRoutes', ['settingsService', function (settingsService) {
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
            } else {
                mapOptions.bounds = MM.LocationRect.fromCorners(scope.startLocation, scope.endLocation);
            };

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
            };

            function onDdirectionsUpdated(e) {
                var distance = Math.round(e.routeSummary[0].distance * 10) / 10;
                if (attrs.endRouteCallback) {
                    scope.$apply(function () {
                        scope.endRouteCallback({ distanceArg: distance });
                    });
                }
            };
        }

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

    // TODO: Bing Maps offers the possibility to reverse-geocode location data. More information: http://msdn.microsoft.com/en-us/library/ff701710.aspx
}
