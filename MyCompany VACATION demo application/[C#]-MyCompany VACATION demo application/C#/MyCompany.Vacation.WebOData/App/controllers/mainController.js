vacationApp.controller('MainController', ['$scope',
    function ($scope) {
        var processes = 0;
        $scope.isBusy = false;
        $scope.showLoading = function () {
            processes++;
            updateStatus();
        };

        $scope.hideLoading = function () {
            processes--;
            updateStatus();
        };

        var updateStatus = function () {
            $scope.isBusy = processes > 0;
        }        
    }]);