(function ($) {

    $.fn.timePicker = function () {
        var self = $(this);
        var initialValue = self.val();

        if (initialValue)
            initialValue = moment.utc(initialValue);
        var now = moment.utc();

        var TIMETYPEAM = 0;
        var TIMETYPEPM = 1;
        var SELECT_CLASS = 'custom-select';

        self.hide();

        var hoursDropDown = createhoursDropDown();
        var minutesDropDown = createMinutesDown();
        var timeTypesDropDown = createTimeTypesDropDown();
        
        function updateSelectedTime() {
            var hour, timeType, minutes;

            if (hoursDropDown) {
                hour = parseInt(hoursDropDown.val());
            }
            else {
                hour = now.hours() + 1;
            }

            if (timeTypesDropDown) {
                timeType = parseInt(timeTypesDropDown.val());
            } else {
                if(initialValue){
                    timeType = initialValue.hours() >= 12 ? TIMETYPEPM : TIMETYPEAM;
                } else {
                    timeType = TIMETYPEAM;
                }
            }

            if (minutesDropDown) {
                minutes = parseInt(minutesDropDown.val());
            } else {
                minutes = 0;
            }

            if (timeType == TIMETYPEPM)
                hour = hour + 12;

            var selectedTime = moment.utc([now.year(), now.month(), now.date(), hour, minutes, 0, 0]); // February 14th, 3:25:0.0 PM
            console.log(selectedTime.toDate().toUTCString());
            self.val(selectedTime.format());
        }

        function createhoursDropDown() {
            var hoursDropDownId = self.attr('id') + '_hours';
            var hoursDropDownHtml = '<select id="' + hoursDropDownId + '">';

            var hoursLength = 12;

            for (var hoursIndex = 0; hoursIndex < hoursLength; hoursIndex++) {
                hoursDropDownHtml += '<option value="' + hoursIndex + '">' + fillWithZeros(hoursIndex,2) + '</option>';
            }

            hoursDropDownHtml += '</select>';

            hoursDropDownHtml = $(hoursDropDownHtml);
            self.parent().append(hoursDropDownHtml);
            hoursDropDownHtml.wrapAll("<div class='" + SELECT_CLASS + "' />");

            hoursDropDownHtml.change(function () {
                updateSelectedTime();
            });

            var initialHour;
            if (initialValue) {
                initialHour = initialValue.hours();
                if (initialHour > 12)
                    initialHour = initialHour - 12;
            } else {
                initialHour = now.hours() + 1;
            }

            hoursDropDownHtml.val(parseInt(initialHour));
            hoursDropDownHtml.change();
            return hoursDropDownHtml;
        }


        function createMinutesDown() {
            var minutesDropDownHtmlId = self.attr('id') + '_minutes';;

            var minutesLength = 60;

            var minutesDropDownHtml = '<select id="' + minutesDropDownHtmlId + '">';

            for (var minuteIndex = 0; minuteIndex < minutesLength; minuteIndex++) {
                minutesDropDownHtml += '<option value="' + minuteIndex + '">' + fillWithZeros(minuteIndex,2) + '</option>';
            }

            minutesDropDownHtml += '</select>';
            minutesDropDownHtml = $(minutesDropDownHtml);
            self.parent().append(minutesDropDownHtml);
            minutesDropDownHtml.wrapAll("<div class='" + SELECT_CLASS + "' />");
            
            minutesDropDownHtml.change(function () {
                updateSelectedTime();
            });
            
            var initialMinutes;
            if (initialValue) {
                initialMinutes = initialValue.minutes();
            } else {
                initialMinutes = 0;
            }

            minutesDropDownHtml.val(parseInt(initialMinutes));
            minutesDropDownHtml.change();

            return minutesDropDownHtml;
        }


        function createTimeTypesDropDown() {
            var timeTypesDropDownId = self.attr('id') + '_timeTypes';
            var timeTypesDropDownHtml = '<select id="' + timeTypesDropDownId + '">';

            var timeTypes = ["AM", "PM"];
            var timeTypesLength = timeTypes.length;

            for (var timeTypesIndex = 0; timeTypesIndex < timeTypesLength; timeTypesIndex++) {
                timeTypesDropDownHtml += '<option value="' + timeTypesIndex + '">' + timeTypes[timeTypesIndex] + '</option>';
            }

            timeTypesDropDownHtml += '</select>';

            timeTypesDropDownHtml = $(timeTypesDropDownHtml);
            self.parent().append(timeTypesDropDownHtml);
            timeTypesDropDownHtml.wrapAll("<div class='" + SELECT_CLASS + "' />");

            timeTypesDropDownHtml.change(function () {
                updateSelectedTime();
            });
            
            var initialTimeType;
            if (initialValue) {
                initialTimeType = initialValue.hours() >= 12 ? TIMETYPEPM : TIMETYPEAM;
            } else {
                initialTimeType = 0;
            }

            timeTypesDropDownHtml.val(initialTimeType);
            timeTypesDropDownHtml.change();
            return timeTypesDropDownHtml;
        }
        
        function fillWithZeros(number, width) {
            width -= number.toString().length;
            if (width > 0) {
                return new Array(width + (/\./.test(number) ? 2 : 1)).join('0') + number;
            }
            return number + "";
        }

        return this;
    };



})(jQuery);
