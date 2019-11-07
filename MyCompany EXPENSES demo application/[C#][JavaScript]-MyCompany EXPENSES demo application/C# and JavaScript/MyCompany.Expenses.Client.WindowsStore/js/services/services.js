(function () {
    "use strict";

    var dialog;
    var showingDialog = false;
    var cache = {};

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
        return new WinJS.Promise(function (complete, raiseError, progress) {

            if (cache[url]) {
                complete(JSON.parse(JSON.stringify(cache[url])));
                return;
            }

            WinJS.xhr({ url: url, headers: { "Authorization": "Bearer " + MyCompany.Expenses.Context.authToken } }).done(
                function (data) {
                    try {
                        data = JSON.parse(data.responseText);
                    }
                    catch (err) {
                        data = null;
                    }
                    cache[url] = JSON.parse(JSON.stringify(data));
                    complete(data);
                },
                function (error) {
                    console.log(error);
                    MyCompany.Expenses.Application.hideLoading();
                    if (error.status == 401) {
                        MyCompany.Expenses.Storage.removeSetting('auth_token');
                        WinJS.Navigation.navigate(MyCompany.Expenses.Enums.pages.login);

                    }
                    else {
                        dialog = new Windows.UI.Popups.MessageDialog("An unexpected error ocurred, please, try again.");
                        if (!showingDialog) {
                            showingDialog = true;
                            dialog.showAsync().then(function () { showingDialog = false; });
                        }
                    }
                }
            );
        });
    }

    function doPostAsync(url, data) {
        return new WinJS.Promise(function (complete, raiseError, progress) {
            WinJS.xhr(
            {
                url: url,
                type: "post",
                headers: {
                    "Content-type": "application/json",
                    "Authorization": "Bearer " + MyCompany.Expenses.Context.authToken
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

    function doPutAsync(url, data) {
        return new WinJS.Promise(function (complete, raiseError, progress) {
            WinJS.xhr(
            {
                url: url,
                type: "put",
                headers: {
                    "Content-type": "application/json",
                    "Authorization": "Bearer " + MyCompany.Expenses.Context.authToken
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

    function doDeleteAsync(url, data) {
        return new WinJS.Promise(function (complete, raiseError, progress) {
            WinJS.xhr(
            {
                url: url,
                type: "delete",
                headers: {
                    "Content-type": "application/json",
                    "Authorization": "Bearer " + MyCompany.Expenses.Context.authToken
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
    
    function cleanCache() {
        for (var key in cache) {
            if(cache.hasOwnProperty(key))
                delete cache[key];
        }
    }

    WinJS.Namespace.define("MyCompany.Expenses.Services", {
        getParamsFromQuery: getParamsFromQuery,
        doGetAsync: doGetAsync,
        doPostAsync: doPostAsync,
        doPutAsync: doPutAsync,
        doDeleteAsync: doDeleteAsync,
        cleanCache: cleanCache
    });
})();