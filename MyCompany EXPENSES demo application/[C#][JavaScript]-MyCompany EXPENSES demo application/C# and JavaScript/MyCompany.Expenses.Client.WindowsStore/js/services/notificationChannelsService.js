(function () {
    "use strict";
    var channel = null;

    var createChannel = function () {
        var pushNotifications = Windows.Networking.PushNotifications;
        var channelOperation = pushNotifications.PushNotificationChannelManager.createPushNotificationChannelForApplicationAsync();

        return channelOperation.then(function (newChannel) {
            channel = newChannel;
            MyCompany.Expenses.Services.NotificationsService.subscribe(newChannel.uri);
        },
        function (error) {
            // Could not create a channel. Retrieve the error through error.number.
        });
    }, 
        deleteChannel = function () {
            if (channel)
                return MyCompany.Expenses.Services.NotificationsService.unsubscribe(channel.uri);

            return true;
        };

    WinJS.Namespace.define("MyCompany.Expenses.Services.NotificationChannelsService", {
        createChannel: createChannel,
        deleteChannel: deleteChannel
    });
})();


