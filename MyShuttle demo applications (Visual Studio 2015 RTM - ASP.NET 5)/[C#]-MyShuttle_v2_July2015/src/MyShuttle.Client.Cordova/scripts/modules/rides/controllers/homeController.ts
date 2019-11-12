/// <reference path="../rides.ts" />

module MyShuttle.Rides {
    interface IHomeControllerScope extends ng.IScope {
        counter: number;
    }

    angularModule.controller('HomeController', function (
        $scope: IHomeControllerScope,
        pushNotificationsService: any,
        settingsService: MyShuttle.Core.SettingsService,
        dataService: MyShuttle.Rides.DataService,
        messengerService: MyShuttle.Core.MessengerService,
        modalService: MyShuttle.Rides.ModalService,
        navigationService:  MyShuttle.Core.NavigationService) {

        var timeRequest;

        var showRequest = function (data: any) {
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
        }

        var init = function () {
            pushNotificationsService.initPushNotifications(function (data: any) {
                timeRequest = moment();
                showRequest(data);
            });
        }

        init();
    });
}
