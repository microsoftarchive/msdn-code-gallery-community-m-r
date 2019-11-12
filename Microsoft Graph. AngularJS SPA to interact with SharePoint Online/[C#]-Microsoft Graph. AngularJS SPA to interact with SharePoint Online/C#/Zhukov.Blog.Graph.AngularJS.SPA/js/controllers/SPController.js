(function () {
    angular
        .module('MicrosoftGraphSPA')
        .controller('SPController', [
            '$scope', '$rootScope', '$http', '$routeParams', 'spFactory',
            function ($scope, $rootScope, $http, $routeParams, spFactory) {
                // Параметры из роутинга
                $scope.siteId = $routeParams.siteId;
                $scope.listId = $routeParams.listId;
                $scope.itemId = $routeParams.itemId;

                $scope.sites = null;
                $scope.lists = null;
                $scope.items = null;
                $scope.item = null;
                $scope.site = null;
                $scope.list = null;

                // Загрузка данных
                $scope.init = function () {

                    // Очищаем результат предыдущего запроса
                    $scope.clearResponse();

                    // Получаем дочерние сайты или корневые
                    spFactory.getSites($scope.siteId).then(
                        function(response) {
                            $scope.sites = response.data.value;
                        },
                        $rootScope.responseError);

                    // Если выбран сайн
                    if ($scope.siteId) {

                        // Получение информации о сайте
                        spFactory.getSite($scope.siteId).then(
                            function (response) {
                                $scope.site = response.data;
                            },
                            $rootScope.responseError);

                        // Получение списков для сайта
                        spFactory.getLists($scope.siteId).then(
                            function(response) {
                                $scope.lists = response.data.value;
                            },
                            $rootScope.responseError);

                        // Если выбран список
                        if ($scope.listId) {

                            // Получение информации о списке
                            spFactory.getList($scope.siteId, $scope.listId).then(
                                function (response) {
                                    $scope.list = response.data;
                                },
                                $rootScope.responseError);

                            //Получение элементов
                            spFactory.getItems($scope.siteId, $scope.listId).then(
                                function(response) {
                                    $scope.items = response.data.value;
                                },
                                $rootScope.responseError);
                        }

                        // Если выбран элемент
                        if ($scope.itemId) {

                            // Получение информации ою элементе
                            spFactory.getItem($scope.siteId, $scope.listId, $scope.itemId).then(
                                function(response) {
                                    $scope.item = response.data;
                                },
                                $rootScope.responseError);
                        }
                    }
                }

                $scope.init();
            }
        ]);
})();