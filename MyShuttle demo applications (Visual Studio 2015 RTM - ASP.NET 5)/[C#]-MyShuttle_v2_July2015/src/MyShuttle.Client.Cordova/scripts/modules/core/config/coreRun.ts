/// <reference path="../core.ts" />

module MyShuttle.Core {
    
    angularModule.run(function (navigationService) {
        document.addEventListener("backbutton", onBackKeyDown.bind(this), false);

        function onBackKeyDown() {
            navigationService.navigateBack();
        };
    });
}