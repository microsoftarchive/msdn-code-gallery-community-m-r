(function () {
    'use strict';

    angular.module('VisitorsApp').controller('VisitsCtrl', [
        '$scope',
        'dataSvc',
        visitsCtrl
    ]);

    function visitsCtrl($scope, dataSvc) {

        var init = function () {
            $scope.showLoading();

            dataSvc.getAllVisits().then(
                function (result) {
                    $scope.visits = result;
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