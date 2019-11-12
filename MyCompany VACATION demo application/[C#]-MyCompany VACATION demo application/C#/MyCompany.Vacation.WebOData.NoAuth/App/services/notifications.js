vacationApp.factory('notifications', ['logger',
    function (logger) {
        var notificationHub = $.connection.vacationNotificationHub,
        notificationOptions = {
            "debug": false,
            "positionClass": "toast-top-full-width",
            "fadeIn": 300,
            "fadeOut": 1000,
            "timeOut": 0,
            "extendedTimeOut": 1000
        };

        function navigateToUrl(url) {
            window.location.href = url;
        }

        function notifyNew(vacationRequest) {
            toastr.options = notificationOptions;
            var message = 'New vacations have been requested by ' +
                vacationRequest.Employee.FirstName + ' ' +
                vacationRequest.Employee.LastName;
            var url = '#/teamVacation';

            toastr.options.onclick = function (e) {
                navigateToUrl(url);
                toastr.clear($toast);
                return true;
            };

            var $toast = toastr["info"](message);
        };

        function startConnection() {
            if (notificationHub) {
                notificationHub.client.notifyNew = notifyNew;
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
    }]);