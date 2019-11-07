(function () {
    angular
        .module('MicrosoftGraphSPA')
        .controller('SearchController', [
            '$scope', '$rootScope', '$http', '$location', 'oneDriveFactory',
            function ($scope, $rootScope, $http, $location, oneDriveFactory) {
                // Запрос
                $scope.query = $location.search().q;
                // Элементы
                $scope.items = null;
                // Загрузка элементов из Office 365
                $scope.loadItems = function () {
                    oneDriveFactory.searchItems($scope.query)
                        .then(
                            function (response) {
                                // Заполняем items
                                $scope.items = response.data.value;
                            }, $rootScope.responseError);
                }
                // Если указан поисковый запрос, то заполняем items
                if ($scope.query) {
                    $scope.loadItems();
                }
            }
        ]);
})();