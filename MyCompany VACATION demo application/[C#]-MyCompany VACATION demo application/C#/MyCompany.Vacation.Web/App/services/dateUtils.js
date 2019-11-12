define(['config'], function (config) {

    var that = {
        getMonths: getMonths,
        getWeekDays: getWeekDays,
        getMonthCalendarDays: getMonthCalendarDays,
        getMonthDays: getMonthDays
    };

    return that;

    function getMonths(currentMonth) {
        var momentMonths = moment().lang()._monthsShort,
            months = [];

        currentMonth = currentMonth || moment().month();

        $.each(momentMonths, function (index, month) {
            months.push({
                id: index,
                name: month,
                isSelected: ko.observable(currentMonth == index)
            });
        });
        return months;
    }

    function getWeekDays() {
        var momentWeekDays = moment().lang()._weekdays;
        if (!config.weekStartsOnMonday)
            return momentWeekDays;

        var sunday = momentWeekDays[0];
        momentWeekDays.splice(0, 1);
        momentWeekDays.push(sunday);

        return momentWeekDays;
    }

    function getMonthCalendarDays(month) {
        var firstDay = moment.utc().month(month).startOf('month').startOf('week');
        var endDay = moment.utc().month(month).endOf('month').endOf('week');
        var days = [];
        var currentDate = firstDay;
        while (currentDate.isBefore(endDay)) {
            days.push(moment(currentDate));
            currentDate.add('days', 1);
        }
        return days;
    }
    
    function getMonthDays(month) {
        var firstDay = moment.utc().month(month).startOf('month');
        var endDay = moment.utc().month(month).endOf('month');
        var days = [];
        var currentDate = firstDay;
        while (currentDate.isBefore(endDay)) {
            days.push(moment(currentDate));
            currentDate.add('days', 1);
        }
        return days;
    }
});