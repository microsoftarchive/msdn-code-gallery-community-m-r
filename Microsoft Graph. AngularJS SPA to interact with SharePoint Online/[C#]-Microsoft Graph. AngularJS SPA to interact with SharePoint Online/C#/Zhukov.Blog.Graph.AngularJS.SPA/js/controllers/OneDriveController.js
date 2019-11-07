(function () {
    angular
        .module('MicrosoftGraphSPA')
        .controller('OneDriveController', [
            '$scope', '$rootScope', '$http', '$routeParams', 'oneDriveFactory',
            function ($scope, $rootScope, $http, $routeParams, oneDriveFactory) {
                $scope.itemId = $routeParams.itemId;
                $scope.currentItem = null;
                $scope.currentItemName = null;
                $scope.currentItemFolder = false;
                $scope.items = null;
                $scope.uploadFile = null;

                $scope.loadItems = function () {
                    $scope.clearResponse();
                    
                    if ($scope.itemId) {
                        // Если выбран файл/папка
                        oneDriveFactory.getItem($scope.itemId).then(
                          function (response) {
                              $scope.currentItem = response.data;
                              //Имя
                              $scope.currentItemName = $scope.currentItem.name;
                              $scope.currentItemFolder = $scope.currentItem.folder != null;

                              // Если выбрана папка, то загружаем её содержимое
                              if ($scope.currentItemFolder) {
                                  oneDriveFactory.getChildren($scope.itemId).then(
                                      function (response) {
                                          $scope.items = response.data.value;
                                      },
                                      $rootScope.responseError);
                              }
                          },
                          $scope.responseError);
                    }
                    else {
                        oneDriveFactory.getRoot().then(
                          function (response) {
                              $scope.currentItem = { folder: { name: 'OneDrive' } };
                              $scope.currentItemName = 'OneDrive';
                              $scope.currentItemFolder = true;
                              $scope.items = response.data.value;
                          },
                          $rootScope.responseError);
                    }
                    if(!$scope.$$phase)
                    {
                        $scope.$apply();
                    }
                }

                $scope.saveItem = function () {
                    $scope.clearResponse();

                    oneDriveFactory.saveItem($scope.itemId, $scope.currentItemName)
                        .then($rootScope.responseSuccess, $rootScope.responseError);
                }

                $scope.addFile = function () {
                    $scope.clearResponse();

                    oneDriveFactory.addFile($scope.itemId, $scope.uploadFile.name)
                        .then(function (response) {
                            $scope.uploadContent(response.data.id);
                        }, $rootScope.responseError);
                }

                $scope.addFolder = function () {
                    $scope.clearResponse();

                    oneDriveFactory.addFolder($scope.itemId, $scope.newFolderName)
                        .then($rootScope.responseSuccess, $rootScope.responseError);
                }

                $scope.removeItem = function () {
                    $scope.clearResponse();

                    oneDriveFactory.removeItem($scope.itemId)
                    .then(function (e) {
                        $scope.responseSuccess(e);
                        $scope.itemId = $scope.currentItem.parentReference.id;
                        $scope.loadItems();
                        if (!$scope.$$phase) {
                            $scope.$apply();
                        }
                        $('#itemDeleteModal').modal('hide');
                    }, $rootScope.responseError);
                }

                $scope.uploadContent = function (itemId) {
                    oneDriveFactory.uploadContent(itemId, $scope.uploadFile)
                        .then($rootScope.responseSuccess, $rootScope.responseError);
                }

                $scope.loadItems();
            }
        ]);
})();