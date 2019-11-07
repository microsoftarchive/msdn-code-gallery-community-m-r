vacationApp.controller('NavigationController', ['$scope', '$location', 'menuService', '$rootScope',
    function ($scope, $location, menuService, $rootScope) {
        $scope.pages = [];
        
        $rootScope.$on('userInfoUpdated', function () {
            menuService.getMenuOptions($scope)
                .then(function (data) {
                    $scope.pages.replaceValues(data);
                });
        });

        $scope.isActive = function (page) {
            var isActive = $location.path() == page.path;
            return isActive;
        }
    }]);