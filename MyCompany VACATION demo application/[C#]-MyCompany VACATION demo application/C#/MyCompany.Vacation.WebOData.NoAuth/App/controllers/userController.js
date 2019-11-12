vacationApp.controller('UserController', ['$scope', 'context', 'dataService', 'enums',
    function ($scope, context, dataService, enums) {
        $scope.user = null;

        $scope.$parent.showLoading();
        dataService.getLoggedEmployeeInfo(enums.pictureType.small, $scope)
        .then(function (data) {
            $scope.user = data;
            $scope.$parent.hideLoading();
            context.setCurrentUser(data);
        });
    }]);