'use strict';

angular.module('myShuttleCore').directive('updateTitle', function($rootScope) {
    return {
        link: function(scope, element) {
            var listener = function(event, toState, toParams, fromState, fromParams) {
                var title = 'Carrier Administration Panel';
                if (toState.data && toState.data.title) {
                    title = toState.data.title;
                }

                element.text(title);
            };

            $rootScope.$on('$stateChangeStart', listener);
        }
    };
});
