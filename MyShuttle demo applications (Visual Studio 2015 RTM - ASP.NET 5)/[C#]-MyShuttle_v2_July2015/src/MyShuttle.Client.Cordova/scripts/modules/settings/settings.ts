module MyShuttle {
    export module Settings {
        export class Module {
            private _module: ng.IModule;
            name: string;

            constructor() {
                this.name = 'ms.Settings';
                this._module = angular.module(this.name, ['ngRoute']);
            }

            public register() {
                ApplicationConfiguration.registerModule(this.name);
                return this._module;
            }
        }

        export var angularModule = new Settings.Module().register();
    }
}