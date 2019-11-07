angular.module('ms.Rides').factory('pushNotificationsService', function ($http, settingsService) {
    var mobileClient,
        tagsToRegister,
        notificationCallback;

    window.onNotificationGCM = onNotificationGCM;
    window.onNotificationWP8 = onNotificationWP8;
    window.onNotificationAPN = onNotificationAPN;
    window.channelHandler = channelHandler;
    window.errorHandler = errorHandler;

    function initPushNotifications(callback) {
        // Only register notifications if not running in Ripple
        var emulated = window.tinyHippos != undefined;
        if (emulated) return;

        var mobileServiceUrl = settingsService.getMobileServiceUrl();
        var zumoApplicationKey = settingsService.mobileServiceKey;
        tagsToRegister = ['VehicleRequested-' + settingsService.vehicle.DriverId];
        notificationCallback = callback;

        mobileClient = new WindowsAzure.MobileServiceClient(mobileServiceUrl, zumoApplicationKey);

        register();
    }

    function register() {
        var pushNotification = window.plugins.pushNotification;

        if (device.platform === 'android' || device.platform === 'Android') {
            var gcmSenderId = settingsService.gcmSenderId;

            pushNotification.register(
                successHandler,
                errorHandler,
                {
                    'senderID': gcmSenderId,
                    'ecb': 'onNotificationGCM'
                });
        } else if (device.platform === 'Win32NT') {
            pushNotification.register(
                channelHandler,
                errorHandler,
                {
                    'channelName': 'MyPushChannel',
                    'ecb': 'onNotificationWP8',
                    'uccb': 'channelHandler',
                    'errcb': 'errorHandler'
                });
        } else if (device.platform === 'iOS') {
            pushNotification.register(
                tokenHandler,
                errorHandler,
                {
                    'ecb': 'onNotificationAPN'
                });
        }
    }

    function processMessage(message) {
        var data = {
            employeeId: message.employeeId,
            latitude: parseFloat(message.latitude),
            longitude: parseFloat(message.longitude)
        };
        notificationCallback(data);
    }

    function onNotificationGCM(e) {
        switch (e.event) {
            case 'registered':
                if (e.regid.length > 0) {
                    console.log('GCM identifier:  ' + e.regid);
                    if (mobileClient) {
                        var hub = new NotificationHub(mobileClient);

                        var template = "{ \"data\" : {" +
                                "\"title\":\"Request received\"," +
                                "\"message\":\"Touch to see the information\"," +
                                "\"employeeId\":\"$(employeeId)\"," +
                                "\"latitude\":\"$(latitude)\"," +
                                "\"longitude\":\"$(longitude)\"" +
                            "}}";

                        hub.gcm.register(e.regid, tagsToRegister, 'myTemplate', template).done(function () {
                            console.log('Registered with hub.');
                        }).fail(function (error) {
                            console.log('Failed registering with hub: ' + error);
                        });
                    }
                }
                break;
            case 'message':
                if (e.foreground) {
                    runAudioNotification();
                }
                processMessage(e.payload);
                break;
            case 'error':
                console.log('GCM error: ' + e.message);
                break;
            default:
                console.log('An unknown GCM event has occurred.');
                break;
        }
    }

    // Windows Phone channel handler.
    function channelHandler(result) {
        if (result.uri !== '') {
            if (mobileClient) {
                var hub = new NotificationHub(mobileClient);

                var template = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                    "<wp:Notification xmlns:wp=\"WPNotification\">" +
                        "<wp:Toast>" +
                            "<wp:Text1>Request received</wp:Text1>" +
                            "<employeeId>$(employeeId)</employeeId>" +
                            "<latitude>$(latitude)</latitude>" +
                            "<longitude>$(longitude)</longitude>" +
                        "</wp:Toast>" +
                    "</wp:Notification>";
                
                hub.mpns.register(result.uri, tagsToRegister, 'myTemplate', template)
                    .done(function () {
                        console.log('Registered with hub.');
                    }).fail(function (error) {
                        console.log('Failed registering with hub: ' + error);
                    });
            }
        }
        else {
            console.log('Channel URI could not be obtained.');
        }
    }

    // iOs token handler.
    function tokenHandler(result) {
        if (mobileClient) {
            var hub = new NotificationHub(mobileClient);

            var template = "{" +
                                "\"aps\" : {" +
                                    "\"alert\":\"Request received\"" +
                                "}," +
                                "\"employeeId\":\"$(employeeId)\"," +
                                "\"latitude\":\"$(latitude)\"," +
                                "\"longitude\":\"$(longitude)\"" +
                            "}";

            hub.apns.register(result, tagsToRegister, 'myTemplate', template, null)
                .done(function () {
                    console.log('Registered with hub.');
                }).fail(function (error) {
                    console.log('Failed registering with hub: ' + error);
                });
        }
    }

    function onNotificationAPN(e) {
        if (e.alert) {
            runAudioNotification();
            processMessage(e);
        }
    }

    function onNotificationWP8(e) {
        if (e.type == 'toast' && e.jsonContent) {
            runAudioNotification();
            processMessage(e.jsonContent);
        }
    }

    function successHandler(result) {
        console.log('Callback success, result: ' + result);
    }

    function errorHandler(error) {
        console.log('Error: ' + error);
    }

    function runAudioNotification() {
        navigator.notification.vibrate(1000);
        navigator.notification.beep(1);
    }

    function notify(method, employeeId) {
        var mobileServiceUrl = settingsService.getMobileServiceUrl();
        var zumoApplicationKey = settingsService.mobileServiceKey;

        return $http.post(mobileServiceUrl + '/api/' + method, { employeeId: employeeId }, {
            headers: { 'X-ZUMO-APPLICATION': zumoApplicationKey }
        });
    }

    function notifyApprovedRequest(employeeId) {
        return notify('NotifyApprovedRequest', employeeId);
    }

    function notifyRejectedRequest(employeeId) {
        return notify('NotifyRejectedRequest', employeeId);
    }

    function notifyVehicleArrived(employeeId) {
        return notify('NotifyVehicleArrived', employeeId);
    }

    return {
        initPushNotifications: initPushNotifications,
        notifyApprovedRequest: notifyApprovedRequest,
        notifyRejectedRequest: notifyRejectedRequest,
        notifyVehicleArrived: notifyVehicleArrived
    }
});