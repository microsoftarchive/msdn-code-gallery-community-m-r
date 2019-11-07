(function () {
    "use strict";

    var apiUrl;
    var testMode;

    function save () {
        var settings = {
            baseAPIUrl: apiUrl.value.toLowerCase().trim(),
            testMode: testMode.checked
        };

        MyCompany.Expenses.Storage.saveSetting('settings', settings);
        MyCompany.Expenses.Config.forceRefreshSettings();
        MyCompany.Expenses.Storage.removeSetting('auth_token');
        WinJS.Navigation.navigate(MyCompany.Expenses.Enums.pages.login);
    }

    WinJS.UI.Pages.define(MyCompany.Expenses.Enums.pages.settings, {
        ready: function (element, options) {
            apiUrl = document.getElementById("apiUrl");
            apiUrl.value = MyCompany.Expenses.Config.getRawApiUrl();

            testMode = document.getElementById("testMode");
            testMode.checked = MyCompany.Expenses.Config.isInTestMode();

            var saveLink = element.querySelector("#saveLink");
            saveLink.addEventListener("click", save, false);
        }
    });
})();