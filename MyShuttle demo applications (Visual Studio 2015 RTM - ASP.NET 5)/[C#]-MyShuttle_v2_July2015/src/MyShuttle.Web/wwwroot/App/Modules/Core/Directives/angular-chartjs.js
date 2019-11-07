/* jshint undef: false */
/* global Chart */

(function () {
    'use strict';

    var chartjs = angular.module('chartjs', []),
        chartTypes = {
            line: 'Line',
            bar: 'Bar',
            radar: 'Radar',
            polar: 'PolarArea',
            pie: 'Pie',
            doughnut: 'Doughnut'
        },
        makeChartDirective = function (chartType) {
            var upper = chartType.charAt(0).toUpperCase() + chartType.slice(1);
            chartjs.directive('cjs' + upper, ['ChartFactory', function (ChartFactory) {
                return new ChartFactory(chartType);
            }]);
        };

    for (var c in chartTypes) {
        makeChartDirective(c);
    }

    chartjs.factory('ChartFactory', function () {
        return function (chartType) {

            chartType = chartTypes[chartType];
            var extractSpecOpts = function (opts, attrs) {
                var i = opts.length,
                    extracted = {},
                    cv;

                while (i--) {
                    cv = attrs[opts[i]];
                    if (typeof(cv) !== 'undefined') {
                        extracted[opts[i]] = cv;
                    }
                }
                return extracted;
            };

            if (typeof(chartType) === 'undefined') {
                return;
            }

            return {
                restrict: 'EAC',
                template: '<canvas></canvas>',
                replace: true,
                scope: {
                    dataset: '=',
                    options: '='
                },
                link: function postLink(scope, element, attrs) {
                    var ctx = element[0].getContext('2d'),
                        chart = new Chart(ctx),
                        chartOpts = {};

                    angular.extend(
                        chartOpts,
                        Chart.defaults.global,
                        Chart.defaults[chartType]
                    );

                    angular.extend(
                        chartOpts,
                        scope.options,
                        extractSpecOpts(
                            chartOpts,
                            attrs
                        )
                    );

                    if (scope.dataset) {
                        chart = chart[chartType](scope.dataset, chartOpts);
                    }

                    scope.$watch('dataset', function (data) {
                        if (chart.clear){
                            chart.clear();
                        }

                        if (data) {
                            chart = chart[chartType](data, chartOpts);
                        }
                    }, true);
                }
            };
        };
    });
})();
