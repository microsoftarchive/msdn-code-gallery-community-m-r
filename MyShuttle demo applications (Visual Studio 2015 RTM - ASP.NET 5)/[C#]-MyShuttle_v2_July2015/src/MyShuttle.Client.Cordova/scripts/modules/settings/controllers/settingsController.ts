/// <reference path="../../core/core.ts" />
/// <reference path="../settings.ts" />

module MyShuttle.Settings {
    interface ISettingsControllerScope extends ng.IScope {
        serviceUrl: string;
        saveSettings(): void;
    }

    angularModule.controller('SettingsController', function (
        $scope: ISettingsControllerScope,
        settingsService: MyShuttle.Core.SettingsService,
        storageService: MyShuttle.Core.StorageService,
        messengerService: MyShuttle.Core.MessengerService,
        navigationService: MyShuttle.Core.NavigationService) {

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
        }

        init();
    });
}
