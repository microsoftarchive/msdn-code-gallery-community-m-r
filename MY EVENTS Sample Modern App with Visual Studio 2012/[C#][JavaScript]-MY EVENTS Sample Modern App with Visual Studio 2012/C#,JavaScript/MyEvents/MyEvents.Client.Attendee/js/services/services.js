(function () {
    "use strict";

    function getParamsFromQuery(query) {
        var urlParams = {};
        var match,
            pl = /\+/g,  // Regex for replacing addition symbol with a space
            search = /([^&=]+)=?([^&]*)/g,
            decode = function (s) { return decodeURIComponent(s.replace(pl, " ")); };

        while (match = search.exec(query)) {
            urlParams[decode(match[1])] = decode(match[2]);
        }

        return urlParams;
    };

    function doGetAsync(url) {
        var authenticationErrorPromise = validateToken();
        if (authenticationErrorPromise) {
            return authenticationErrorPromise;
        }

        return new WinJS.Promise(function (complete, raiseError, progress) {
            var token = MyEvents.Context.serviceToken || "";
            WinJS.xhr({ url: url, headers: { "Authorization": "Basic " + token } }).done(
                function (data) {
                    data = JSON.parse(data.responseText);
                    complete(data);
                },
                function (error) {
                    console.log(error);
                    raiseError(error);
                }
            );
        });
    }

    function doPostAsync(url, data) {
        var authenticationErrorPromise = validateToken();
        if (authenticationErrorPromise) {
            return authenticationErrorPromise;
        }

        return new WinJS.Promise(function (complete, raiseError, progress) {
            WinJS.xhr(
            {
                url: url,
                type: "post",
                headers: {
                    "Content-type": "application/json",
                    "Authorization": "Basic " + MyEvents.Context.serviceToken
                },
                data: JSON.stringify(data)
            }).done(
                        function (result) {
                            if (result && result.responseText) {
                                result = JSON.parse(result.responseText);
                                complete(result);
                            }
                            else {
                                complete();
                            }
                        },
                        function (error) {
                            console.log(error);
                            raiseError(error);
                        }
                    );
        });
    }

    function validateToken() {
        var now = new Date();
        if (MyEvents.Context.expirationDate && (MyEvents.Context.expirationDate < now)) {
            return new WinJS.Promise(function (complete, raiseError, progress) {
                raiseError(MyEvents.Enums.errorType.expiredToken);
            });
        }
    }

    WinJS.Namespace.define("MyEvents.Services", {
        getParamsFromQuery: getParamsFromQuery,
        doGetAsync: doGetAsync,
        doPostAsync: doPostAsync
    });
})();