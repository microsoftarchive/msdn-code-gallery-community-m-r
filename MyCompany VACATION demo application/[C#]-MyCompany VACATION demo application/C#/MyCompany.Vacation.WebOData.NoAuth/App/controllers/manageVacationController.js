vacationApp.controller('ManageVacationController', ['$scope', 'dialog', 'model', 'enums', 'dataService', 'dialogService',
    function ($scope, dialog, model, enums, dataService, dialogService) {
        var vacationRequestId,
            from,
            to;

        $scope.close = function () {
            dialog.close();
        };

        function initialize() {
            $scope.title = 'Vacation request';
            $scope.startDay = moment(model.from).format('MMM-D');
            $scope.endDay = moment(model.to).format('MMM-D');
            $scope.workDays = model.numDays;
            $scope.isApproved = model.status == enums.vacationRequestStatus.approved;
            $scope.isDenied = model.status == enums.vacationRequestStatus.denied;

            vacationRequestId = model.vacationRequestId;
            from = model.from;
            to = model.to;
        }

        $scope.acceptVacation = function () {
            $scope.$parent.showLoading();

            var parameters = {
                vacationRequestId: vacationRequestId
            };

            dataService.acceptVacationRequest(parameters, $scope).then(function () {
                $scope.$parent.hideLoading();
                dialog.close({ vacationAccepted: true });
            });
        }

        $scope.rejectVacation = function () {
            var vacationRequest = {
                vacationRequestId: vacationRequestId,
                from: moment(from),
                to: moment(to),
                startDay: $scope.startDay,
                endDay: $scope.endDay,
                workDays: $scope.workDays,
                scope: model.scope
            };

            dialogService.open('App/views/denyVacationRequest.html', 'DenyVacationController', vacationRequest)
                         .then(function (result) {
                             dialog.close({
                                 vacationRejected: (result && result.denyVacationRequest) || false
                             });
                         });
        }

        initialize();
    }]);