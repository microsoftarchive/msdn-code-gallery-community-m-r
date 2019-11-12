define(['services/dataservice', 'services/model', 'services/enums', 'services/dateUtils','viewmodels/base'], function (dataservice, model, enums, dateUtils, base) {
    var vacationRequestId = ko.observable(),
        startDay = ko.observable(),
        endDay = ko.observable(),
        from = ko.observable(),
        to = ko.observable(),
        workDays = ko.observable(0),
        reason = ko.observable(),
        startDate = 0,
        endDate = 0;

    var vm = {
        title: 'Deny vacation request',
        activate: activate,
        refresh: refresh,
        startDay: startDay,
        endDay: endDay,
        workDays: workDays,
        reason: reason,
        deny: deny,
        cancel: cancel
    };

    return vm;

    function activate(routeData) {
        vacationRequestId(routeData.vacationRequestId);
        startDay(routeData.startDay);
        endDay(routeData.endDay);
        from(routeData.from);
        to(routeData.to);
        workDays(routeData.workDays);
        reason('');
    }

    function refresh() {

    }

    function deny() {
        base.showLoading();
        dataservice.rejectVacationRequest({
            vacationRequestId: vacationRequestId(),
            from: moment.utc(from().utc()).toDate(),
            to: moment.utc(to().utc()).toDate(),
            reason: reason()
        }).then(function (vacationRequestId) {
            base.hideLoading();
            vm.modal.close({
                denyVacationVacation: true,
                vacationRequestId: vacationRequestId
            });
        });
    }

    function cancel() {
        vm.modal.close({
            updateVacations: false
        });
    }
});

