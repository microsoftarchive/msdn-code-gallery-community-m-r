/// <reference path="../core.ts" />

module MyShuttle.Core {

    angularModule.config(function ($provide: ng.auto.IProvideService) {
        $provide.decorator("$exceptionHandler", function ($delegate: ng.IExceptionHandlerService) {
            return function (exception: Error, cause: string) {

                // Track all exceptions in AppInsights
                appInsights.trackException(exception);

                $delegate(exception, cause)
            };
        });
    });

}