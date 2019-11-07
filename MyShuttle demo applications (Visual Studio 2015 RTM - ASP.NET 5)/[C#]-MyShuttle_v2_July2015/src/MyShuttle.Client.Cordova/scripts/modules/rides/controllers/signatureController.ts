/// <reference path="../rides.ts" />

module MyShuttle.Rides {
    interface ISignatureControllerScope extends ng.IScope {
        email: string;
        signature: string;
        signatureForm: ng.IFormController;
        sendRide(): void;
    }

    angularModule.controller('SignatureController', function (
        $scope: ISignatureControllerScope,
        dataService: MyShuttle.Rides.DataService,
        navigationService: MyShuttle.Core.NavigationService,
        messengerService: MyShuttle.Core.MessengerService,
        settingsService: MyShuttle.Core.SettingsService,
        params) {

        var ride;

        $scope.sendRide = function() {
            if (!$scope.signatureForm.$valid) return;

            messengerService.send(messengerService.messageTypes.getSignature, function(signature) {
                ride.signature = signature;
                ride.employeeEmail = $scope.email;

                messengerService.send(messengerService.messageTypes.startLoading);
                dataService.addRide(ride).done(
                    function() {
                        messengerService.send(messengerService.messageTypes.endLoading);
                        navigationService.navigateTo('home');
                    }, function() {
                        messengerService.send(messengerService.messageTypes.endLoading);
                        $scope.email = '';
                        $scope.$digest();
                    });
            });
        };

        var init = function() {
            ride = params || {};
            messengerService.send(messengerService.messageTypes.showNavigateBackBtn, { jumpToMainPage: true });
            $scope.email = (ride.employee && ride.employee.email) || settingsService.defaultEmployeeEmail;

            $scope.$on('$locationChangeStart', function(event) {
                messengerService.send(messengerService.messageTypes.hideNavigateBackBtn);
            });
        };

        init();
    });
}
