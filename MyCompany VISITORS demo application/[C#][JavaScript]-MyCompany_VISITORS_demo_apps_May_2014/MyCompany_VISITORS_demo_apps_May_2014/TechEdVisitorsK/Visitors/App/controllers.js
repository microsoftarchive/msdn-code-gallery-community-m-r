(function controllersRegistration() {
    'use strict';

    var app = angular.module('app');

    app.controller('MainController', [
        '$scope',
        function ($scope) {

            var processes = 0;

            var updateStatus = function () {
                if ($scope.showSplash) {
                    if (processes === 0) {
                        $scope.showSplash = false;
                    }

                } else {
                    $scope.isBusy = processes > 0;
                }
            };

            $scope.isBusy = false;
            $scope.showSplash = true;

            $scope.showLoading = function () {
                processes++;
                updateStatus();
            };

            $scope.hideLoading = function () {
                processes--;
                updateStatus();
            };

        }
    ]);

    app.controller('UserController', [
        '$scope',
        'dataService',
        function ($scope, dataService) {

            $scope.employee = {};

            function loadUserInfo() {

                $scope.showLoading();

                dataService.getLoggedEmployeeInfo(1).then(
                    function success(employee) {
                        $scope.employee = employee;
                        $scope.hideLoading();
                    },
                    function error() {
                        $scope.hideLoading();
                    });

            }

            loadUserInfo();
        }
    ]);

    app.controller('VisitorsController', [
        '$scope',
        'dataService',
        function ($scope, dataService) {

            $scope.filter = '';

            $scope.totalVisitors = 0;

            $scope.visitors = [];

            $scope.navigateToPage = function () {

                $scope.showLoading();

                dataService.getVisitors($scope.filter, 1, 0).then(
                    function success(result) {
                        $scope.visitors = result;
                        $scope.totalVisitors = result.length;
                        $scope.hideLoading();
                    },
                    function error() {
                        $scope.hideLoading();
                    }
                );
            };

            $scope.search = function () {
                $scope.navigateToPage();
            };

            $scope.refresh = function () {
                $scope.navigateToPage();
            };

            $scope.navigateToPage();
        }
    ]);

}())