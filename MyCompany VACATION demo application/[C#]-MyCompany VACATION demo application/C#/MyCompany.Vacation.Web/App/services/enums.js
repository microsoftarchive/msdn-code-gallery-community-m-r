define(function () {
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

    var enums = {
        pictureType: pictureType,
        options: options,
        vacationRequestStatus: vacationRequestStatus
    };

    return enums;
});