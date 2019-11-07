/// <reference path="../core.ts" />

module MyShuttle.Core {
    interface IHeaderControllerScope extends ng.IScope {
        backBtnVisible: boolean;
        navigateBack(): void;
    }

    angularModule.controller('HeaderController', function (
        $scope: IHeaderControllerScope,
        $rootScope: ng.IRootScopeService,
        messengerService: MyShuttle.Core.MessengerService,
        navigationService: MyShuttle.Core.NavigationService) {

        var init = function () {
        };

        var jumpToMainPage,
            cleanUpShowBackBtn = $rootScope.$on(messengerService.messageTypes.showNavigateBackBtn, function(event, params) {
                $scope.backBtnVisible = true;
                if (params)
                    jumpToMainPage = params.jumpToMainPage;
            }),
            cleanUpHideBackBtn = $rootScope.$on(messengerService.messageTypes.hideNavigateBackBtn, function(event, params) {
                $scope.backBtnVisible = false;
            });

        $scope.navigateBack = function () {
            $scope.backBtnVisible = false;

            if (jumpToMainPage) {
                navigationService.navigateTo('home');
            } else {
                navigationService.navigateBack();
            }
        };

        var cleanUpDestroy = $scope.$on('$destroy', function () {
            cleanUpShowBackBtn();
            cleanUpHideBackBtn();
        });

        init();
    });
}