/// <reference path="../rides.ts" />

module MyShuttle.Rides {
    interface IRideControllerScope extends ng.IScope {
        distance: number;
        duration: number;
        cost: number;
        startRideTime: moment.Moment;
        endRideTime: moment.Moment;
        startRideLocation: {
            latitude: number;
            longitude: number;
        };
        endRideLocation: {
            latitude: number;
            longitude: number;
        };
        startRide(): void;
        endRide(): void;
        endOfRoute(distance: number): void;
        sign(): void;
    }

    angularModule.controller('RideController', function (
        $scope: IRideControllerScope,
        settingsService: MyShuttle.Core.SettingsService,
        navigationService: MyShuttle.Core.NavigationService,
        params) {
        var employee;
        var rideDuration;

        var init = function () {
            $scope.distance = settingsService.rideDistance;
            $scope.duration = 0;
            $scope.cost = 0;
            employee = params;
        }

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
}
