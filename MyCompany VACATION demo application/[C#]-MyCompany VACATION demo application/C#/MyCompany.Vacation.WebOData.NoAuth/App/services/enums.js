vacationApp.factory('enums', ['context',
    function (context) {
        var pictureType = {
            small: 1,
            big: 2
        };

        var options = {
            ok: 'ok',
            cancel: 'cancel',
            yes: 'yes',
            no: 'no',
            close: 'close'
        };

        var vacationRequestStatus = {
            unknown: 0,
            pending: 1,
            approved: 2,
            denied: 3
        };

        var getEnumValue = function (enumType, value) {
            if (!value)
                throw "value can not be null";

            if (typeof value === "number")
                return value;

            if (enumType[value.toLowerCase()])
                return enumType[value.toLowerCase()];

            throw "value not found";
        };


        var enums = {
            pictureType: pictureType,
            options: options,
            vacationRequestStatus: vacationRequestStatus,
            getEnumValue: getEnumValue
        };

        return enums;
    }]);