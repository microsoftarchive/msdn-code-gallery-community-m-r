define(['services/dataservice', 'services/model', 'services/enums', 'services/dateUtils', 'viewmodels/base'], function (dataservice, model, enums, dateUtils, base) {
    var startDay = ko.observable(),
        endDay = ko.observable(),
        workDays = ko.observable(0),
        comment = ko.observable(),
        startDate = 0,
        endDate = 0;

    var vm = {
        title: 'Request new vacation days',
        activate: activate,
        refresh: refresh,
        startDay: startDay,
        endDay: endDay,
        workDays: workDays,
        comment: comment,
        accept: accept,
        cancel: cancel
    };

    return vm;

    function activate(routeData) {
        startDay(routeData.startDay.date.format('MMM-D'));
        endDay(routeData.endDay.date.format('MMM-D'));
        workDays(routeData.workDays);
        comment('');
        startDate = routeData.startDay.date;
        endDate = routeData.endDay.date;

        $('button').focus();
    }
    
    function refresh() {
    }

    function accept() {
        console.log('add accept');
        base.showLoading();
        dataservice.addVacationRequest({
            from: moment.utc(startDate.utc()).toDate(),
            to: moment.utc(endDate.utc()).toDate(),
            comments: comment()
        }).then(function(vacationRequestId) {
            base.hideLoading();
            vm.modal.close({
                updateVacations: true,
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

