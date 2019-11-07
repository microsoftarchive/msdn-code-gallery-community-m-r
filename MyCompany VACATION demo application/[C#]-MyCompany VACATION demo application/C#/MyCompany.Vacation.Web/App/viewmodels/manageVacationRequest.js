define(['services/dataservice', 'services/model', 'services/enums', 'services/dateUtils', 'durandal/app'], function (dataservice, model, enums, dateUtils, app) {
    var startDay = ko.observable(),
        endDay = ko.observable(),
        from = ko.observable(),
        to = ko.observable(),
        workDays = ko.observable(0),
        isApproved = ko.observable(),
        isDenied = ko.observable(),
        reason = ko.observable(),
        vacationRequestId;

    var vm = {
        title: 'Vacation request',
        activate: activate,
        refresh: refresh,
        startDay: startDay,
        endDay: endDay,
        workDays: workDays,
        isApproved: isApproved,
        isDenied: isDenied,
        reason: reason,
        acceptVacation: acceptVacation,
        rejectVacation: rejectVacation,
        close: close
    };

    return vm;

    function activate(routeData) {
        vacationRequestId = routeData.vacationRequestId();
        startDay(moment(routeData.from()).format('MMM-D'));
        endDay(moment(routeData.to()).format('MMM-D'));
        workDays(routeData.numDays());
        from(routeData.from());
        to(routeData.to());
        isApproved(routeData.status() == enums.vacationRequestStatus.approved);
        isDenied(routeData.status() == enums.vacationRequestStatus.denied);
    }

    function refresh() {
    }

    function acceptVacation() {
        var parameters = {
            vacationRequestId: vacationRequestId
        };

        dataservice.acceptVacationRequest(parameters).then(
            vm.modal.close({
                vacationAccepted: true
            }));
    }

    function rejectVacation() {
        var parameters = {
            vacationRequestId: vacationRequestId,
            reason: reason()
        };

        var vacationRequest = {
            vacationRequestId: vacationRequestId,
            startDay: startDay(),
            endDay: endDay(),
            from: moment(from()),
            to: moment(to()),
            workDays: workDays(),
        };

        app.showModal('viewmodels/denyVacationRequest', vacationRequest)
             .then(function (result) {
                 if (result && result.denyVacationVacation) {
                     dataservice.rejectVacationRequest(parameters).then(
                     vm.modal.close({
                         vacationRejected: true
                     }));
                 } else {
                     vm.modal.close({
                         vacationRejected: false
                     });
                 }
             });
    }

    function close() {
        vm.modal.close();
    }
});

