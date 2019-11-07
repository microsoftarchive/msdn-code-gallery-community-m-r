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

    var travelRequestStatus = {
        unknown: 0,
        pending: 1,
        approved: 2,
        completed : 4,
        denied: 8
    };
    travelRequestStatus.all = travelRequestStatus.pending + travelRequestStatus.approved + travelRequestStatus.completed + travelRequestStatus.denied;

    var travelType = {
        unknown: 0,
        oneway: 1,
        roundtrip: 2
    }

    var enums = {
        pictureType: pictureType,
        options: options,
        travelRequestStatus: travelRequestStatus,
        travelType: travelType
    };

    return enums;
});