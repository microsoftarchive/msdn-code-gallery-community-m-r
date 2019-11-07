(function () {
    'use strict';

    angular.module('VisitorsApp').controller('MainCtrl', [
        '$scope',
        '$route',
        'menuSvc',
        mainCtrl
    ]);

    function mainCtrl($scope, $route, menuSvc) {
        var updateStatus = function () {
            $scope.isBusy = processes > 0;
        },
            processes = 0;

        $scope.isBusy = false;

        $scope.showLoading = function () {
            processes++;
            updateStatus();
        };

        $scope.hideLoading = function () {
            processes--;
            updateStatus();
        };

        $scope.$on('$routeChangeSuccess', function () {
            $scope.pageTitle = $route.current.pageTitle;
        });
    };

}());