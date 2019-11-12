'use strict';

angular.module('myShuttleDashboard').controller('DashboardController', ['$scope', 'dashboardDataService',
    function ($scope, dataService) {
        $scope.chartOptions = {
            bezierCurve: true,
            responsive: true,
            maintainAspectRatio: false,
            pointHitDetectionRadius: 2,
            tooltipTemplate: '<%= value %>'
        };

        var init = function () {
            dataService.getTopVehicles().then(function (data) {
                $scope.vehicles = data;
            });

            dataService.getTopDrivers().then(function (data) {
                $scope.drivers = data;
            });

            dataService.getSummaryInfo().then(function (data) {
                $scope.summary = data;
            });

            dataService.getRidesInfo().then(function (data) {
                data.evolution = {
                    labels: data.RidesEvolution.Days,
                    datasets: [
                        {
                            data: data.RidesEvolution.Values,
                            fillColor: '#46D1B6',
                            strokeColor: '#46D1B6',
                            pointColor: 'rgba(30,130,111,.75)',
                            pointStrokeColor: '#FFF',
                            pointHighlightFill: '#FFF',
                            pointHighlightStroke: 'rgba(30,130,111,.75)'
                        }
                    ]
                };

                $scope.rides = data;
            });
        };

        init();
    }
]);