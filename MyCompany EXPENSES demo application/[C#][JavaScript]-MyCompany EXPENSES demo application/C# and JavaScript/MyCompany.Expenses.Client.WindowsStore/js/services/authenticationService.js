(function () {
    "use strict";

    function getFullAuthUrl() {
        var baseAuthUrl = Windows.Foundation.Uri(MyCompany.Expenses.Config.getAuthUrl());
        var baseServerUrl = Windows.Foundation.Uri(MyCompany.Expenses.Config.getApiUrl());
        var testMode = MyCompany.Expenses.Config.isInTestMode();

        // testMode is intended for demos only and don't use validation
        // testMode applies always on local server
        if (testMode) {
            baseServerUrl = baseServerUrl.combineUri("/noauth/");
        }
        
        return baseAuthUrl;
    }


    function launchWebAuth() {
        return new WinJS.Promise(function (complete, raiseError, progress) {
            Microsoft.Preview.WindowsAzure.ActiveDirectory.Authentication.AuthenticationContext.tokenCache = null; 
            var authenticationContext = Microsoft.Preview.WindowsAzure.ActiveDirectory.Authentication.AuthenticationContext(MyCompany.Expenses.Config.getAuthUrl());
            authenticationContext.acquireTokenAsync(
                    MyCompany.Expenses.Config.getApiUrl(),
                    MyCompany.Expenses.Config.getClientId(),
                    MyCompany.Expenses.Config.getReplyUri(), "", "").done(function (result, a) {
                                    switch (result.status) {
                                        case Microsoft.Preview.WindowsAzure.ActiveDirectory.Authentication.AuthenticationStatus.succeeded:
                                            var token = { accessToken: result.accessToken };
                                            logOn(token).done(complete);
                                            break;
                                        default:
                                            MyCompany.Expenses.Storage.removeSetting('auth_token');
                                            logOn().done(complete);
                                            break;
                                    }
                                }, function (error) {
                                    MyCompany.Expenses.Storage.removeSetting('auth_token');
                                    console.log("WebAuthenticationBroker returned an error: " + error);
                                    raiseError(error);
                                });

        });
    }

    function logOn(tokenInfo) {
        return new WinJS.Promise(function (complete, raiseError, progress) {
            if (tokenInfo && tokenInfo.accessToken) {
                MyCompany.Expenses.Storage.saveSetting('auth_token', tokenInfo);
                MyCompany.Expenses.Context.authToken = tokenInfo.accessToken;
                
                var channel = MyCompany.Expenses.Services.NotificationChannelsService.createChannel();
                
                var url = Windows.Foundation.Uri(MyCompany.Expenses.Config.getApiUrl(), "api/employees/" + MyCompany.Expenses.Enums.pictureType.small).rawUri;
                MyCompany.Expenses.Services.doGetAsync(url).then(function (data) {
                    if (data) {
                        complete(data);
                    }
                    else {
                        if (MyCompany.Expenses.Config.isInTestMode()) {
                            var dialog = Windows.UI.Popups.MessageDialog("Cannot get user data from service, please, try again.");
                            MyCompany.Expenses.Application.hideLoading();
                            dialog.showAsync();
                        }
                        else {
                            MyCompany.Expenses.Storage.removeSetting('auth_token');
                            WinJS.Navigation.navigate(MyCompany.Expenses.Enums.pages.login);
                        }
                    }
                }, function (error) {
                    MyCompany.Expenses.Storage.removeSetting('auth_token');
                    WinJS.Navigation.navigate(MyCompany.Expenses.Enums.pages.login);
                });
            }
            else {
                complete();
            }
        });
    }

    var authenticate = function () {
        // testMode is intended for demos only and don't use validation
        // testMode is intended for demos only
        var testMode = MyCompany.Expenses.Config.isInTestMode();
        if (testMode) {
            var dummyToken = { accessToken: "TestMode" };
            return logOn(dummyToken);
        }

        var tokenInfo = MyCompany.Expenses.Storage.getSetting('auth_token');
        if (tokenInfo) {
            return logOn(tokenInfo);
        }
        else {
            return launchWebAuth();
        }
    };

    WinJS.Namespace.define("MyCompany.Expenses.Services.Authentication", {
        authenticate: authenticate,
    });
})();