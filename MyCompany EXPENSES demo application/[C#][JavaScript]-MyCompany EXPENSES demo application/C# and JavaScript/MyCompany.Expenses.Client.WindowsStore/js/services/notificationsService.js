(function () {
    "use strict";

    var subscribe = function (channelUri) {
        var url = Windows.Foundation.Uri(
            MyCompany.Expenses.Config.getApiUrl(),
            "api/notifications").rawUri;

        var data = {
            ChannelUri: channelUri,
            NotificationType: MyCompany.Expenses.Enums.notificationType.windowsStoreNotification
        }
        return MyCompany.Expenses.Services.doPostAsync(url, data);
    },
        unsubscribe = function (channelUri) {
            var url = Windows.Foundation.Uri(
                MyCompany.Expenses.Config.getApiUrl(),
                "api/notifications").rawUri;

            var data = {
                ChannelUri: channelUri,
                NotificationType: MyCompany.Expenses.Enums.notificationType.windowsStoreNotification
            }
            return MyCompany.Expenses.Services.doDeleteAsync(url, data);
        }; 

    WinJS.Namespace.define("MyCompany.Expenses.Services.NotificationsService", {
        subscribe: subscribe,
        unsubscribe: unsubscribe
    });
})();