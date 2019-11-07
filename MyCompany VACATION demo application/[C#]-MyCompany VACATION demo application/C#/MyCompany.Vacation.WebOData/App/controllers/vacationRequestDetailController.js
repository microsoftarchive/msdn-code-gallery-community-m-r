vacationApp.controller('VacationRequestDetailController', ['$scope', 'dialog', 'model', 'enums', 'dataService', 'dialogService',
    function ($scope, dialog, model, enums, dataService, dialogService) {
        $scope.title = 'Vacation request';
        $scope.vacationRequestId = model.vacationRequestId;
        $scope.startDay = model.startDay.date.format('MMM-D');
        $scope.endDay = model.endDay.date.format('MMM-D');
        $scope.workDays = model.workDays;
        $scope.isDeletable = model.startDay.date.isAfter(moment().subtract('days', 1));

        $scope.close = function () {
            dialog.close();
        };

        $scope.deleteRequest = function () {
            var message = 'Are you sure you want to delete the request?';
            dialogService.messageBox(message, [enums.options.yes, enums.options.no])
            .then(function (result) {
                if (result == enums.options.yes) {
                    model.scope.showLoading();
                    dataService.deleteVacationRequest(model.vacationRequestId, $scope)
                        .then(function () {
                            model.scope.hideLoading();
                            dialog.close({
                                updateVacations: true,
                                vacationRequestId: model.vacationRequestId
                            });
                        });
                }
                else {
                    dialog.close();
                }
            })
        }
    }]);