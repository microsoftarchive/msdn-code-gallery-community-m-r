vacationApp.controller('AddVacationRequestController', ['$scope', 'dialog', 'model', 'dataService',
    function ($scope, dialog, model, dataService) {
        $scope.title = 'Request new vacation days';
        $scope.startDay = model.startDay.date.format('MMM-D');
        $scope.endDay = model.endDay.date.format('MMM-D');
        $scope.workDays = model.workDays;
        $scope.comment = '';

        $scope.close = function () {
            dialog.close();
        };

        $scope.accept = function () {
            model.scope.$parent.showLoading();
            dataService.addVacationRequest({
                from: moment.utc(model.startDay.date.utc()).toDate(),
                to: moment.utc(model.endDay.date.utc()).toDate(),
                comments: $scope.comment
            }, $scope).then(function (vacationRequest) {
                model.scope.$parent.hideLoading();
                dialog.close({
                    updateVacations: true,
                    vacationRequestId: vacationRequest.VacationRequestId
                });
            });
        }
    }]);