vacationApp.controller('MsgBoxController', ['$scope', 'dialog', 'model',
    function ($scope, dialog, model) {
        $scope.message = model.message;
        $scope.options = model.options;

        $scope.close = function () {
            dialog.close();
        };

        $scope.selectOption = function (option) {
            dialog.close(option);
        }
    }]);