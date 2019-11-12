/// <reference path="../rides.ts" />

module MyShuttle.Rides {
    interface IServiceControllerScope extends ng.IScope {
        employee: any;
        position: MyShuttle.Core.Coordinate;
        timeRequest: moment.Moment;
        notifyEmployee(): void;
    }

    angularModule.controller('ServiceController', function (
        $scope: IServiceControllerScope,
        pushNotificationsService: any,
        navigationService: MyShuttle.Core.NavigationService,
        params) {

        $scope.notifyEmployee = function () {
            pushNotificationsService.notifyVehicleArrived($scope.employee.id).then(function () {
                navigationService.navigateTo('ride', $scope.employee);
            });
        }

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
}
