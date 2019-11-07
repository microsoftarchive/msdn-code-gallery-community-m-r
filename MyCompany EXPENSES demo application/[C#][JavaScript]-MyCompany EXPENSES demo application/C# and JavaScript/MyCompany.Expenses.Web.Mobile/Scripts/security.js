Security = function () {
    var isNoAuth = false;

    var getSecurityHeaders = function getSecurityHeaders() {
        var accessToken = sessionStorage["accessToken"];

        if (accessToken) {
            return "Bearer " + accessToken;
        }

        return null;
    };

    var clearAccessToken = function clearAccessToken() {
        sessionStorage.removeItem("accessToken");
    };

    var setAccessToken = function setAccessToken(accessToken) {
        sessionStorage["accessToken"] = accessToken;
    };

    var setIsNoAuth = function (v) {
        isNoAuth = v;
    }

    var getIsNoAuth = function () {
        return isNoAuth;
    }

    return {
        getSecurityHeaders: getSecurityHeaders,
        clearAccessToken: clearAccessToken,
        setAccessToken: setAccessToken,
        setIsNoAuth: setIsNoAuth,
        getIsNoAuth: getIsNoAuth
    };
}();
