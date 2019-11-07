'use strict';

angular.module('myShuttleCore').directive('listPager', [function () {
    var defaultPageSize = 8,
        defaultPage = 1;

    return {
        restrict: 'AE',
        scope: {
            count: '=',
            page: '=',
            pageSize: '=?',
            goTo: '&goTo'
        },
        link: function (scope, element) {
            scope.goBack = function () {
                scope.goTo({numPage: scope.page - 1});
            };
            scope.goForward = function () {
                scope.goTo({numPage: scope.page + 1});
            };
            scope.goToPage = function(page) {
                scope.goTo({numPage: page});
            };

            var calculatePager = function (count) {
                var number = Math.ceil(count / scope.pageSize);

                var pages = [];
                for (var i = defaultPage; i <= number; i++) {
                    var isCurrentPage = scope.page === i;
                    var page = { number: i, current: isCurrentPage };
                    pages.push(page);
                }

                scope.isFirstPage = scope.page === defaultPage;
                scope.isLastPage = scope.page > number || scope.page === number;
                scope.pages = pages;
            };

            scope.pageSize = scope.pageSize || defaultPageSize;

            scope.$watch('count', function (value) {
                calculatePager(value);
            });
        },
        templateUrl: 'App/Modules/Core/Views/pagerTemplate.html'
    };
}]);
