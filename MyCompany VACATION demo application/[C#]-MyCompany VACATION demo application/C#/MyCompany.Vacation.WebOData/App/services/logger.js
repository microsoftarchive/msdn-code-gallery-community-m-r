vacationApp.factory('logger',
    function () {
        var logger = {
            log: log
        };

        return logger;

        function log(message, data, source) {
            source = source ? '[' + source + '] ' : '';
            if (data) {
                console.log(message + '\n' + source + '\n' + data);
            } else {
                console.log(message + '\n' + source);
            }
        }
    });