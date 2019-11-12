(function () {
    'use strict';

    angular.module('VisitorsApp').controller('NavigationCtrl', [
        '$scope',
        '$route',
        'menuSvc',
        navigationCtrl
    ]);

    function navigationCtrl($scope, $route, menuSvc) {
        $scope.pages = [];
        $scope.currentController = null;
        $scope.showNavigationTabs = true;

        $scope.pages = menuSvc.getMenuOptions();

        $scope.$on('$routeChangeSuccess', function () {
            $scope.currentController = $route.current.controller;
        });

        $scope.isActive = function (page) {
            if (page) {
                return page.controller === $scope.currentController;
            }
        };
    };

}());