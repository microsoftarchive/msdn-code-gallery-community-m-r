(function () {
    "use strict";

    var webUrl;
    var apiUrl;
    var offLineMode;

    function save () {
        var settings = {
            baseWebUrl: webUrl.value,
            baseAPIUrl: apiUrl.value,
            offLineMode: offLineMode.checked,
        };

        MyEvents.Storage.saveSetting('settings', settings);
        MyEvents.Config.forceRefreshSettings();
        WinJS.Navigation.navigate(MyEvents.Enums.pages.login);
    }

    WinJS.UI.Pages.define(MyEvents.Enums.pages.settings, {
        ready: function (element, options) {
            webUrl = document.getElementById("webUrl");
            apiUrl = document.getElementById("apiUrl");
            offLineMode = document.getElementById("offlineCheck");

            webUrl.value = MyEvents.Config.getWebUrl();
            apiUrl.value = MyEvents.Config.getApiUrl();
            offLineMode.checked = MyEvents.Config.getOfflineMode();

            var openSessionButton = element.querySelector(".button-save");
            openSessionButton.addEventListener("click", save, false);
        }
    });
})();