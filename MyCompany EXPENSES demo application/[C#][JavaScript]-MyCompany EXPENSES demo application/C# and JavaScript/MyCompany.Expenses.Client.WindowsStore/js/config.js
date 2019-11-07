(function () {
    "use strict";

    var defaultApiUrl = "http://localhost:31329/"; 
    var defaultAuthUrl = "https://login.windows.net/[tenantname].onmicrosoft.com";
    var apiUrl;
    var authUrl;
    var testMode = true;
    var numberOfImagesOnTile = 5;
    
    var getApiUrl = function () {
        if (!apiUrl) {
            var settings = MyCompany.Expenses.Storage.getSetting('settings');
            apiUrl = defaultApiUrl;

            if (settings) {
                if (settings.baseAPIUrl) {
                    apiUrl = settings.baseAPIUrl;
                }
            }
        }
        var testMode = MyCompany.Expenses.Config.isInTestMode();
        if (testMode) {
            return Windows.Foundation.Uri(apiUrl).combineUri("noauth/").rawUri;
        }
        return apiUrl.trim();
    };

    var getReplyUri = function () {
        return "http://localhost:31329/";
    };

    var getClientId = function () {
        return '[clientId]';
    };

    var getRawApiUrl = function () {
        if (!apiUrl) {
            var settings = MyCompany.Expenses.Storage.getSetting('settings');
            apiUrl = defaultApiUrl;

            if (settings) {
                if (settings.baseAPIUrl) {
                    apiUrl = settings.baseAPIUrl;
                }
            }
        }
        return apiUrl.trim();
    };

    var getAuthUrl = function () {
        if (!authUrl) {
            authUrl = defaultAuthUrl;
        }

        return authUrl;
    };

    var isInTestMode = function () {
        if (!testMode) {
            var settings = MyCompany.Expenses.Storage.getSetting('settings');

            testMode = false;
            if (settings) {
                if (settings.testMode) {
                    testMode = settings.testMode;
                }
            }
        }
        return testMode;
    };
    
    var forceRefreshSettings = function() {
        apiUrl = undefined;
        authUrl = undefined;
        testMode = undefined;

    };

    var getNumberOfImagesOnTile = function () {
        return numberOfImagesOnTile;
    };

    WinJS.Namespace.define("MyCompany.Expenses.Config", {
        getApiUrl: getApiUrl,
        getAuthUrl: getAuthUrl,
        getReplyUri: getReplyUri,
        getClientId: getClientId,
        getRawApiUrl: getRawApiUrl,
        forceRefreshSettings: forceRefreshSettings,
        isInTestMode: isInTestMode,
        getNumberOfImagesOnTile: getNumberOfImagesOnTile
    });
})();
