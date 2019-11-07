/// <reference path="../core.ts" />

module MyShuttle.Core {
    export class MessengerService {
        public messageTypes: {
            startLoading: string;
            endLoading: string;
            showNavigateBackBtn: string;
            hideNavigateBackBtn: string;
            getSignature: string;
            settingsChanged: string;
        }

        constructor(private $rootScope: ng.IRootScopeService) {
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

        public send(message:string, data?:any) {
            this.$rootScope.$broadcast(message, data);
        }
    }

    angularModule.service('messengerService', MessengerService);
}

