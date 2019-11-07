define(['services/logger', 'services/navigation'], function (logger, navigation) {
    var notificationHub = $.connection.vacationNotificationHub,
    notificationOptions = {
        "debug": false,
        "positionClass": "toast-top-full-width",
        "fadeIn": 300,
        "fadeOut": 1000,
        "timeOut": 0,
        "extendedTimeOut": 1000
    };

    function notifyNew(vacationRequest) {
        toastr.options = notificationOptions;
        var message = 'New vacations have been requested by ' +
            vacationRequest.Employee.FirstName + ' ' +
            vacationRequest.Employee.LastName;

        toastr.options.onclick = function (e) {
            navigation.navigateTo('#/teamVacation');
            toastr.clear($toast);
            return true;
        };

        var $toast = toastr["info"](message);
    };

    function startConnection() {
        if (notificationHub) {
            $.connection.hub.start().done(function () {
                logger.log("connected");
            }).fail(function (err) {
                logger.log(err);
            });

            notificationHub.client.notifyNew = notifyNew;
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