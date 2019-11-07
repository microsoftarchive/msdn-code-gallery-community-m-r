define(['services/dataservice', 'services/model', 'services/enums', 'services/dateUtils', 'durandal/app','viewmodels/base'], function (dataservice, model, enums, dateUtils, app, base) {
    var startDay = ko.observable(),
        endDay = ko.observable(),
        workDays = ko.observable(0),
        comment = ko.observable(),
        startDate = 0,
        endDate = 0,
        vacationRequestId,
        isDeletable = ko.observable(false);

    var vm = {
        title: 'Vacation request',
        activate: activate,
        refresh: refresh,
        startDay: startDay,
        endDay: endDay,
        isDeletable: isDeletable,
        workDays: workDays,
        comment: comment,
        close: close,
        deleteRequest: deleteRequest
    };

    return vm;

    function activate(routeData) {
        vacationRequestId = routeData.vacationRequestId;
        startDay(routeData.startDay.date.format('MMM-D'));
        endDay(routeData.endDay.date.format('MMM-D'));
        isDeletable(routeData.startDay.date.isAfter(moment().subtract('days', 1)));
        workDays(routeData.workDays);
        startDate = routeData.startDay.date;
        endDate = routeData.endDay.date;
    }

    function refresh() {

    }

    function close() {
        vm.modal.close();
    }

    function deleteRequest() {
        console.log('detail delete');
        var message = 'Are you sure you want to delete the request?';
        app.showMessage(message, 'confirmation', [enums.options.yes, enums.options.no])
            .then(function (dialogResult) {
                console.log('delte result:' + dialogResult);
                if (dialogResult == enums.options.yes) {
                    base.showLoading();
                    dataservice.deleteVacationRequest(vacationRequestId).then(function () {
                        base.hideLoading();
                        vm.modal.close({
                            updateVacations: true,
                            vacationRequestId: vacationRequestId
                        });
                    });
                } else {
                    vm.modal.close({
                        updateVacations: false
                    });
                }
            });
    }
});

