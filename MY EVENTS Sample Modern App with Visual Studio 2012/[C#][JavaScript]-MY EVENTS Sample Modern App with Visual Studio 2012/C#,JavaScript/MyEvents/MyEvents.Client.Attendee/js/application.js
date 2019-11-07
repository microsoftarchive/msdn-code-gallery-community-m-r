(function () {
    "use strict";

    var loadingCounter = 0;
    var isAppBarDisabled;
    
    function hideLoading() {
        if (Windows.ApplicationModel.DesignMode.designModeEnabled)
            return;

        loadingCounter--;
        if (loadingCounter <= 0) {
            var appbarCtrl = document.querySelector("#appbar").winControl;
            appbarCtrl.disabled = isAppBarDisabled;
            
            setTimeout(function () {
                WinJS.Utilities.removeClass(document.querySelector('#loading .overlay'), 'transparent');
                WinJS.Utilities.removeClass(document.querySelector('#loading'), 'show');
            }, 150);
        }
    }

    function showLoading(number) {
        if (Windows.ApplicationModel.DesignMode.designModeEnabled)
            return;

        if (loadingCounter === 0) {
            var appBar = document.querySelector("#appbar").winControl;
            isAppBarDisabled = appBar.disabled;
        }

        if (number) {
            loadingCounter = loadingCounter + number;
        }
        else {
            loadingCounter++;
        }

        if (loadingCounter > 0) {
            WinJS.Utilities.addClass(document.querySelector('#loading'), 'show');
            WinJS.Utilities.addClass(document.querySelector('#loading .overlay'), 'transparent');
            var appbarCtrl = document.querySelector("#appbar").winControl;
            appbarCtrl.disabled = true;
        }
    }

    String.prototype.trim = function () { return this.replace(/^\s\s*/, '').replace(/\s\s*$/, ''); };

    function dataRequested(e, data) {
        var request = e.request;
        request.data.properties.title = "My Events: " + data.title;
        request.data.properties.description = data.description;
        var uri = MyEvents.Config.getWebUrl() + data.url;
        request.data.setUri(new Windows.Foundation.Uri(uri));
    }

    WinJS.Namespace.define("MyEvents.Application", {
        hideLoading: hideLoading,
        showLoading: showLoading,
        dataRequested: dataRequested
    });
})();