(function () {
    "use strict";

    var defaultApiUrl = "http://localhost:2565/api/";
    var defaultWebUrl = "http://localhost:2407/";
    var defaultOffLineMode = false;
    var facebookClientId = "512549695437523";
    var bingMapsKey = "AuK2YpsRk6wUyEX2IG6GG1nl-c-zMrvY6EKNfACven5m-9-zHNCVUxxIp3rihD4R";
    var maximumNumberOfItems = 12;
    var apiUrl;
    var webUrl;
    var offlineMode;
    var imagesPath = "/images/content/";
    
    var getApiUrl = function () {
        if (!apiUrl) {
            var settings = MyEvents.Storage.getSetting('settings');
            if (settings) {
                apiUrl = settings.baseAPIUrl;
            }
            else {
                apiUrl = defaultApiUrl;
            }
        }
    
        return apiUrl;
    };

    var getWebUrl = function () {
        if (!webUrl) {
            var settings = MyEvents.Storage.getSetting('settings');
            if (settings) {
                webUrl = settings.baseWebUrl;
            }
            else {
                webUrl = defaultWebUrl;
            }
        }

        return webUrl;
    };

    var getOfflineMode = function () {
        if (!offlineMode) {
            var settings = MyEvents.Storage.getSetting('settings');
            if (settings) {
                offlineMode = settings.offLineMode;
            }
            else {
                offlineMode = defaultOffLineMode;
            }
        }

        return offlineMode;
    };
    
    var forceRefreshSettings = function() {
        apiUrl = undefined;
        webUrl = undefined;
        offlineMode = undefined;
    };

    WinJS.Namespace.define("MyEvents.Config", {
        getApiUrl: getApiUrl,
        getWebUrl: getWebUrl,
        getOfflineMode: getOfflineMode,
        forceRefreshSettings: forceRefreshSettings,
        facebookClientId: facebookClientId,
        bingMapsKey: bingMapsKey,
        maximumNumberOfItems: maximumNumberOfItems,
        imagesPath: imagesPath
    });
})();
