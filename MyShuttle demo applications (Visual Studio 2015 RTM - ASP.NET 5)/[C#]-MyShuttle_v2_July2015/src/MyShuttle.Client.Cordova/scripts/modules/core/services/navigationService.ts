/// <reference path="../core.ts" />

module MyShuttle.Core {
    declare var navigator;

    export class NavigationService {
        private transferData: any;
        private urlHistory: string[];
        
        constructor(
            private $rootScope: ng.IRootScopeService,
            private $timeout,
            private $location: ng.ILocationService,
            private $anchorScroll) {
            this.urlHistory = [];
            return this;
        }

        public start() {
            var location = this.$location;
            var history = this.urlHistory;
            this.$rootScope.$on('$routeChangeSuccess', function () {
                if (location.absUrl().split('#')[1] !== history[history.length - 1]) {
                    history.push(location.absUrl().split('#')[1]);
                }
            });
        }

        public navigateTo(url: string, data?: any): void {
            this.transferData = data;
            var $location = this.$location;
            var $anchorScroll = this.$anchorScroll;

            this.$timeout(function () {
                $location.path(url);
                $anchorScroll();
            }, 10);
        }

        public getTransferedData(): any {
            return this.transferData;
        }

        public navigateBack(): void {
            var location = this.$location;
            var history = this.urlHistory;
            this.$timeout(function () {
                if (location.path() === '/' || location.path() === '/home') {
                    navigator.app.exitApp();
                    return;
                };

                history.pop();
                location.path(history[history.length - 1]);
            }, 10);
        }
    }

    angularModule.factory('navigationService', ($rootScope, $timeout, $location, $anchorScroll) => {
        var instance = new MyShuttle.Core.NavigationService($rootScope, $timeout, $location, $anchorScroll);
        instance.start();
        return instance;
    });
}