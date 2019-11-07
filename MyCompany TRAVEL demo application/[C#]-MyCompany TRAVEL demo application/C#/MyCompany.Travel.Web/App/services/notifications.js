define(['services/logger', 'config', 'services/navigation'], function (logger, config, navigation) {
    var notificationHub = $.connection.travelsNotificationHub,
    notificationOptions = {
        "debug": false,
        "positionClass": "toast-top-full-width",
        "fadeIn": 300,
        "fadeOut": 1000,
        "timeOut": 0,
        "extendedTimeOut": 1000
    };

    function notifyApproved(travelRequest) {
        toastr.options = notificationOptions;
        var message = 'A new travel request has been approved.';
        var url = '#/rrhh/travelRequest/' + travelRequest.TravelRequestId;
        toastr.options.onclick = function () { navigation.navigateTo(url); return true; }

        var $toast = toastr["info"](message);
    };

    function notifyNew(travelRequest) {
        toastr.options = notificationOptions;
        var message = travelRequest.Employee.FirstName + ' ' + travelRequest.Employee.LastName + ' has sent a new travel request.';
        var url = '#/manager/travelRequest/' + travelRequest.TravelRequestId;
        toastr.options.onclick = function () { navigation.navigateTo(url); return true; }

        var $toast = toastr["info"](message);
    };

    function startConnection() {
        if (notificationHub) {
            notificationHub.client.notifyApproved = notifyApproved;
            notificationHub.client.notifyNew = notifyNew;

            $.connection.hub.qs = { isNoAuth: config.isNoAuth };
            $.connection.hub.start().done(function (myHubConnection) {
                if ($.connection.hub.state == 1) {
                    logger.log("connected");
                }
            }).fail(function (err) {
                logger.log(err);
            });
        }
        else {
            logger.log('notification hub unavailable');
        }
    }

    var notificationService = {
        startConnection: startConnection,
    };

    return notificationService;
});