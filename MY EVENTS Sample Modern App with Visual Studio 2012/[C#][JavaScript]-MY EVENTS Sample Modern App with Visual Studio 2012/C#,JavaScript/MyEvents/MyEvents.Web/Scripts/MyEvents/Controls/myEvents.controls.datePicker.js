(function ($) {

    $.fn.datePicker = function () {
        var self = $(this);
        var SELECT_CLASS = 'custom-select';
        var currentValue = self.val();
        var initialValue = null;
        var now = moment.utc();
        now = moment.utc(new Date(now.year(), now.month(), now.date()));

        if (currentValue) {
            currentValue = moment.utc(currentValue);
            initialValue = moment(new Date(currentValue.year(), currentValue.month(), currentValue.date()));
        } else {
            initialValue = moment.utc();
        }

        var monthsDropDownId = self.attr('id') + '_months';
        var daysDropDownId = self.attr('id') + '_days';;
        var monthsDropDown = '<select id="' + monthsDropDownId + '">';
        var months = new Array();

        months[0] = "January";
        months[1] = "February";
        months[2] = "March";
        months[3] = "April";
        months[4] = "May";
        months[5] = "June";
        months[6] = "July";
        months[7] = "August";
        months[8] = "September";
        months[9] = "October";
        months[10] = "November";
        months[11] = "December";

        var monthsLength = months.length;

        for (var monthIndex = 0; monthIndex < monthsLength; monthIndex++) {
            monthsDropDown += '<option value="' + monthIndex + '">' + months[monthIndex] + '</option>';
        }

        monthsDropDown += '</select>';

        monthsDropDown = $(monthsDropDown);
        self.hide();
        self.parent().append(monthsDropDown);
        var classes = self.attr('class');
        monthsDropDown.wrapAll("<div class='" + SELECT_CLASS + ' ' + classes + "' />");

        monthsDropDown.change(function () {
            var selectedMonth = parseInt(monthsDropDown.val());
            createDaysDropDown(selectedMonth);
        });

        monthsDropDown.val(initialValue.month());
        monthsDropDown.change();

        function createDaysDropDown(month) {
            var dayOneOfMonth = moment.utc(new Date(initialValue.year(), month, 1));
            var daysInMonth = dayOneOfMonth.daysInMonth();

            var oldDropDown = $('#' + daysDropDownId);
            var oldSelectedDay = parseInt(oldDropDown.val());
            oldDropDown.parent().remove();

            var selectedDay = null;

            if (!oldSelectedDay) {
                selectedDay = initialValue.date();
            }
            else {
                selectedDay = oldSelectedDay;
            }

            var daysDropDown = '<select id="' + daysDropDownId + '">';

            for (var dayIndex = 0; dayIndex < daysInMonth; dayIndex++) {
                var day = (dayIndex + 1);
                daysDropDown += '<option value="' + day + '">' + day + '</option>';
            }

            daysDropDown += '</select>';
            daysDropDown = $(daysDropDown);
            self.parent().append(daysDropDown);
            daysDropDown.wrapAll("<div class='" + SELECT_CLASS + "' />");

            daysDropDown.change(function () {
                var timeZone = (moment().zone() * -1) / 60;
                var actualSelectedDay = parseInt(daysDropDown.val());
                var oldDate = moment.utc(new Date(initialValue.year(), month, actualSelectedDay));
                var newDate = moment.utc(new Date(initialValue.year() + 1, month, actualSelectedDay));
                var selectedDate = oldDate < now ? newDate : oldDate;

                if (timeZone > 0) {
                    selectedDate.add('hours', timeZone);
                }
                else {
                    selectedDate.subtract('hours', timeZone);
                }

                self.val(selectedDate.format());
            });

            daysDropDown.val(selectedDay);
            daysDropDown.change();
        }

        return this;
    };



})(jQuery);
