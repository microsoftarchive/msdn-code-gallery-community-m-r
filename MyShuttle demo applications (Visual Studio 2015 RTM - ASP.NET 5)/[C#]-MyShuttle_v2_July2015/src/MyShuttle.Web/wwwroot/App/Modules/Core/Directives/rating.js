'use strict';

angular.module('myShuttleCore').directive('msRating', ['$compile', function ($compile) {
    var getStateOnClassName = function (stateNumber, big) {
        var sufix = big ? '-big' : '';
        return 'rating-star-' + stateNumber + sufix;
    };

    return {
        restrict: 'AE',
        scope: {
            model: '=',
            big: '='
        },
        controller: function ($scope, $element) {
            var empty = $scope.big ? 'rating-empty-star-big' : 'rating-empty-star';
            $scope.states = [
                {stateOn: getStateOnClassName(1, $scope.big), stateOff: empty},
                {stateOn: getStateOnClassName(2, $scope.big), stateOff: empty},
                {stateOn: getStateOnClassName(3, $scope.big), stateOff: empty},
                {stateOn: getStateOnClassName(4, $scope.big), stateOff: empty},
                {stateOn: getStateOnClassName(5, $scope.big), stateOff: empty}
            ];
        },
        link: function (scope, element) {
            scope.$watch('model', function (data) {
                scope.current = Math.round(data);
                if (data) {
                    scope.model = parseFloat(parseFloat(data).toFixed(1));
                }
            }, true);
        },
        templateUrl: 'App/Modules/Core/Views/ratingTemplate.html'
    };
}]);