(function () {
    'use strict';

    angular.module('VisitorsApp').controller('TodayVisitsCtrl', [
        '$scope',
        'dataSvc',
        todayVisitsCtrl
    ]);

    function todayVisitsCtrl($scope, dataSvc) {
        var init = function () {

            $scope.showLoading();

            dataSvc.getTodayVisits().then(
                function (result) {
                    $scope.todayVisits = result;
                    $scope.hideLoading();
                },
                function (error) {
                    $scope.hideLoading();
                }
            );
        };

        init();
    };

}());