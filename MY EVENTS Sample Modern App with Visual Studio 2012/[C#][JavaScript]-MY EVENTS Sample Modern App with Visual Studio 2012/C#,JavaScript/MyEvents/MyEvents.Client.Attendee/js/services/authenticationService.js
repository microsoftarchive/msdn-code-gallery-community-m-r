(function () {
    "use strict";

    function launchFacebookWebAuth(clientId) {
        return new WinJS.Promise(function (complete, raiseError, progress) {
            var callbackUrl = "https://www.facebook.com/connect/login_success.html";
            var facebookUrl = "https://www.facebook.com/dialog/oauth?" +
                "client_id=" + clientId +
                "&redirect_uri=" + encodeURIComponent(callbackUrl) +
                "&scope=email%2Cuser_location%2Cuser_about_me%2Cfriends_about_me" +
                "&display=popup" +
                "&response_type=token";

            Windows.Security.Authentication.Web.WebAuthenticationBroker.authenticateAsync(
                Windows.Security.Authentication.Web.WebAuthenticationOptions.none,
                new Windows.Foundation.Uri(facebookUrl),
                new Windows.Foundation.Uri(callbackUrl)
            ).done(function (result) {
                switch (result.responseStatus) {
                    case Windows.Security.Authentication.Web.WebAuthenticationStatus.success:
                        var params = MyEvents.Services.getParamsFromQuery(result.responseData.split("#")[1]);
                        var now = new Date();
                        var expirationDate = new Date(now.valueOf() + (params.expires_in - 20) * 1000);
                        var tokenInfo = { accessToken: params.access_token, expirationDate: expirationDate };
                        logOn(tokenInfo).done(complete);
                        break;
                    default:
                        logOn().done(complete);
                        break;
                }
            }, function (error) {
                console.log("Error returned by WebAuth broker: " + error, "Web Authentication SDK Sample", "error");
                raiseError(error);
            });
        });
    }

    function logOn(tokenInfo) {
        return new WinJS.Promise(function (complete, raiseError, progress) {
            if (tokenInfo && tokenInfo.accessToken) {
                MyEvents.Storage.saveSetting('facebook_token', tokenInfo);
                MyEvents.Context.facebookToken = tokenInfo.accessToken;

                var url = MyEvents.Config.getApiUrl() + "authentication/logOn?token={token}";
                url = url.replace("{token}", tokenInfo.accessToken);
                MyEvents.Services.doGetAsync(url).then(function (data) {
                    if (data) {
                        complete(data);
                    }
                    else {
                        MyEvents.Storage.removeSetting('facebook_token');
                        WinJS.Navigation.navigate(MyEvents.Enums.pages.login);
                    }
                }, function (error) {
                    raiseError(error);
                });
            }
            else {
                complete();
            }
        });
    }

    function getFakeAuthorization() {
        var url = MyEvents.Config.getApiUrl() + "authentication/GetFakeAuthorization";
        return MyEvents.Services.doGetAsync(url);
    }

    var authenticate = function () {
        var offline = MyEvents.Config.getOfflineMode();
        if (!offline) {
            var tokenInfo = MyEvents.Storage.getSetting('facebook_token');
            if (tokenInfo && tokenInfo.expirationDate > new Date()) {
                return logOn(tokenInfo);
            } else {
                return launchFacebookWebAuth(MyEvents.Config.facebookClientId);
            }
        }
        else {
            return getFakeAuthorization();
        }
    };

    WinJS.Namespace.define("MyEvents.Services.Authentication", {
        authenticate: authenticate,
    });
})();