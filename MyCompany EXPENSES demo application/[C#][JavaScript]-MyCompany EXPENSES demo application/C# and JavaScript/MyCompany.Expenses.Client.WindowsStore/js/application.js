(function () {
    "use strict";

    var loadingCounter = 0;
    var isAppBarDisabled;

    function hideLoading() {
        setTimeout(function () {
            WinJS.Utilities.removeClass(document.querySelector('#loading .overlay'), 'transparent');
            WinJS.Utilities.removeClass(document.querySelector('#loading'), 'show');
        }, 150);
    }

    function showLoading(number) {
            WinJS.Utilities.addClass(document.querySelector('#loading'), 'show');
            WinJS.Utilities.addClass(document.querySelector('#loading .overlay'), 'transparent');
    }

    String.prototype.trim = function () { return this.replace(/^\s\s*/, '').replace(/\s\s*$/, ''); };

    function navigateToUrl (navigateOptions) {
        WinJS.Navigation.navigate(navigateOptions.url, navigateOptions.data).done(function () {
            if (navigateOptions.backUrl) {
                WinJS.Navigation.history.backStack.push({ location: navigateOptions.backUrl });
            }
        }); 
    }

    WinJS.Namespace.define("MyCompany.Expenses.Application", {
        hideLoading: hideLoading,
        showLoading: showLoading,
        navigateToUrl: navigateToUrl
    });
})();