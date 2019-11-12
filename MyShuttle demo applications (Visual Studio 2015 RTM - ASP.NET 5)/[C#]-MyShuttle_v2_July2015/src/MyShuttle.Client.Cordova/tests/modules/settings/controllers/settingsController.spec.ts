/// <reference path="../../../../scripts/typings/jasmine/jasmine.d.ts" />
/// <reference path="../../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../../scripts/typings/angularjs/angular-mocks.d.ts" />
/// <reference path="../../../../scripts/config.ts" />
/// <reference path="../../../../scripts/index.ts" />
/// <reference path="../../../../scripts/modules/core/core.ts" />
/// <reference path="../../../../scripts/modules/core/config/corerun.ts" />
/// <reference path="../../../../scripts/modules/core/services/settingsservice.ts" />
/// <reference path="../../../../scripts/modules/core/services/storageservice.ts" />
/// <reference path="../../../../scripts/modules/core/services/messengerservice.ts" />
/// <reference path="../../../../scripts/modules/core/services/navigationservice.ts" />
/// <reference path="../../../../scripts/modules/settings/controllers/settingscontroller.ts" />

describe('SettingsController Tests',(): void=> {

    var $scope, controllerFactory;
    var settingsServiceMock, storageServiceMock, messengerServiceMock, navigationServiceMock;

    // Controller factory function
    function createController() {
        return controllerFactory('SettingsController', {
            $scope: $scope,
            settingsService: settingsServiceMock,
            storageService: storageServiceMock,
            messengerService: messengerServiceMock,
            navigationService: navigationServiceMock
        });
    }

    // Module initialization
    beforeEach(function () {
        module(ApplicationConfiguration.applicationName);
    });

    // Mock creation
    beforeEach(function () {
        settingsServiceMock = jasmine.createSpyObj('settingsService', ['getMobileServiceUrl']);
        settingsServiceMock.getMobileServiceUrl.and.callFake(function () {
            return 'fake connection';
        });
        storageServiceMock = jasmine.createSpyObj('storageService', ['getById']);
        messengerServiceMock = jasmine.createSpyObj('messengerService', ['send', 'messageTypes']);
        messengerServiceMock.messageTypes.and.callFake(function(){
            return {
                startLoading: '_START_LOADING_',
                endLoading: '_END_LOADING_',
                showNavigateBackBtn: '_SHOW_NAVIGATE_BACK_BTN_',
                hideNavigateBackBtn: '_HIDE_NAVIGATE_BACK_BTN_',
                getSignature: '_GET_SIGNATURE_',
                settingsChanged: '_SETTINGS_CHANGED_'
            }
        });
        navigationServiceMock = jasmine.createSpyObj('navigationService', ['getById']);
    });

    // Dependency injection and controller creation
    beforeEach(inject(function (_$controller_, _$rootScope_) {
        controllerFactory = _$controller_;
        $scope = _$rootScope_.$new();
    }));

    it('can be initialized',(): void=> {
        createController();
        $scope.$digest();

        expect(settingsServiceMock.getMobileServiceUrl).toHaveBeenCalled();
        expect(messengerServiceMock.send).toHaveBeenCalled();
    });

});