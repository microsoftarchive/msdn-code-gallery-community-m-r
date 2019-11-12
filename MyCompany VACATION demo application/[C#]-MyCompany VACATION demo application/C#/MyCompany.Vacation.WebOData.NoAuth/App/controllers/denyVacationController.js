vacationApp.controller('DenyVacationController', ['$scope', 'dialog', 'model', 'dataService', 
    function ($scope, dialog, model, dataService) {
        var vacationRequestId,
            from,
            to;

        $scope.cancel = function () {
            dialog.close({ denyVacationRequest: false });
        };

        function initialize() {
            $scope.startDay = moment(model.from).format('MMM-D');
            $scope.endDay = moment(model.to).format('MMM-D');
            $scope.workDays = model.numDays;
            vacationRequestId = model.vacationRequestId;
            from = model.from;
            to = model.to;
            $scope.reason = '';
            $scope.title = 'Deny vacation request';
        }

        $scope.deny = function () {
            model.scope.$parent.showLoading();
            dataService.rejectVacationRequest({
                vacationRequestId: vacationRequestId,
                from: moment.utc(from.utc()).toDate(),
                to: moment.utc(to.utc()).toDate(),
                reason: $scope.reason
            }, $scope).then(function (vacationRequestId) {
                model.scope.$parent.hideLoading();
                dialog.close({
                    denyVacationRequest: true,
                    vacationRequestId: vacationRequestId
                });
            });
        }

        initialize();
    }]);