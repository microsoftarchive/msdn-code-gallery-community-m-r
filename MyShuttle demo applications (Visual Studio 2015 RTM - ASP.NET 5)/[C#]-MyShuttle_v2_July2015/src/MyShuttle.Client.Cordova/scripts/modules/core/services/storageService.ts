/// <reference path="../core.ts" />

module MyShuttle.Core {
    export class StorageService {

        constructor(private $window: ng.IWindowService) {
            return this;
        }

        public getValue(key: string, defaultValue: string): string {
            return this.$window.localStorage.getItem(key) || defaultValue;
        }

        public setValue(key: string, value: string): void {
            this.$window.localStorage.setItem(key, value);
        }
    }

    angularModule.factory('storageService', ($window) => {
        return new MyShuttle.Core.StorageService($window);
    });
}