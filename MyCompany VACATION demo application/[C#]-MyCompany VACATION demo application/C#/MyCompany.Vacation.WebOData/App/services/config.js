vacationApp.factory('config', ['context', function (context) {
    var pageSize = 10,
        dateFormat = 'DD/MM/YYYY',
        timeFormat = 'HH:mm',
        weekStartsOnMonday = false;

    return {
        pageSize: pageSize,
        dateFormat: dateFormat,
        timeFormat: timeFormat,
        weekStartsOnMonday: weekStartsOnMonday
    };
}]);
