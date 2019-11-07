'use strict';

angular.module('myShuttleCore').directive('listInputSearch', ['$timeout', '$state', '$location',
    function ($timeout, $state, $location) {
        var defaultPage = 1;

        return {
            restrict: 'AE',
            scope: {
                search: '=model',
                page: '='
            },
            link: function (scope, element) {
                var searchTimeout;
                scope.$watch('search', function (value) {
                    if (searchTimeout) $timeout.cancel(searchTimeout);

                    searchTimeout = $timeout(function () {
                        if (value === undefined) return;

                        if (value) {
                            if ($state.params.q !== value)
                                $location.search('q=' + value + '&page=' + defaultPage);
                        }
                        else {
                            if ($state.params.q && $state.params.q != value) { // jshint ignore:line
                                $location.search('');
                            }
                            else {
                                if(scope.page !== defaultPage){
                                    $location.search('page=' + scope.page);
                                }
                            }
                        }
                    }, 300);
                });
            },
            templateUrl: 'App/Modules/Core/Views/listInputSearchTemplate.html'
        };
    }]);
