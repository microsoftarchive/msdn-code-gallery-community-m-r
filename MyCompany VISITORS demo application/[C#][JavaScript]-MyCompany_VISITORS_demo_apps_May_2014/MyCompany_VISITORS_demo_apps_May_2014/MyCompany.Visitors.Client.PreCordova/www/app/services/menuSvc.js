(function () {
    'use strict';

    angular.module('VisitorsApp').factory('menuSvc', [
        menuSvc
    ]);

    function menuSvc() {

        function getAllPages() {

            var pages = [
                    {
                        display: 'Today visits',
                        url: '#/',
                        controller: 'TodayVisitsCtrl'
                    },
                    {
                        display: 'All visits',
                        url: '#/visits',
                        controller: 'VisitsCtrl'
                    },
                    {
                        display: 'Visit detail',
                        url: '#/detail',
                        controller: 'VisitDetailCtrl'
                    }
            ];

            return pages;

        };

        function getMenuOptions() {

            var pages = [
                    {
                        display: 'Today visits',
                        url: '#/',
                        controller: 'TodayVisitsCtrl'
                    },
                    {
                        display: 'All visits',
                        url: '#/visits',
                        controller: 'VisitsCtrl'
                    }
            ];

            return pages;
        }

        var service = {
            getMenuOptions: getMenuOptions,
            getAllPages: getAllPages
        };

        return service;
    }

}());