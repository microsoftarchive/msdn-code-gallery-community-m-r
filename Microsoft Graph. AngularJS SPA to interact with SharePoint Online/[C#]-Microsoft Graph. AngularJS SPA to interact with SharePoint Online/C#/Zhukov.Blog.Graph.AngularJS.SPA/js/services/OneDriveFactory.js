angular
    .module('MicrosoftGraphSPA')
    .constant('graphUrl', 'https://graph.microsoft.com/v1.0')
    .factory('oneDriveFactory',
    ['$http', 'graphUrl',
        function ($http, graphUrl) {

            var oneDriveFactory = {};

            // Получение содержимого из корня OneDrive
            oneDriveFactory.getRoot = function () {
                return $http({
                    method: 'GET',
                    url: graphUrl + '/me/drive/root/children'
                });
            };

            oneDriveFactory.getChildren = function (itemId) {
                return $http({
                    method: 'GET',
                    url: graphUrl + '/me/drive/items/' + itemId + '/children'
                });
            };

            oneDriveFactory.getItem = function (itemId) {
                return $http({
                    method: 'GET',
                    url: graphUrl + '/me/drive/items/' + itemId
                });
            };

            oneDriveFactory.saveItem = function (itemId, name) {
                return $http({
                    method: 'PATCH',
                    url: graphUrl + '/me/drive/items/' + itemId,
                    data: {
                        name: name
                    }
                });
            };

            oneDriveFactory.addFile = function (parentId, name) {
                var url = parentId
                ? graphUrl + '/me/drive/items/' + parentId + '/children'
                : graphUrl + '/me/drive/root/children';
                return $http({
                    method: 'POST',
                    url: url,
                    data: {
                        name: name,
                        file: {}
                    }
                });
            }

            oneDriveFactory.addFolder = function (parentId, name) {
                var url = parentId
                ? graphUrl + '/me/drive/items/' + parentId + '/children'
                : graphUrl + '/me/drive/root/children';
                return $http({
                    method: 'POST',
                    url: url,
                    data: {
                        name: name,
                        folder: {}
                    }
                });
            }

            oneDriveFactory.removeItem = function (itemId) {
                return $http({
                    method: 'DELETE',
                    url: graphUrl + '/me/drive/items/' + itemId
                });
            }

            oneDriveFactory.uploadContent = function (itemId, file) {
                var url = graphUrl + '/me/drive/items/' + itemId + '/content';
                var fd = new FormData();
                fd.append('file', file);
                return $http.put(url, fd);
            };

            oneDriveFactory.searchItems = function (query) {
                return $http( {
                    method: 'GET',
                    url: graphUrl + '/me/drive/root/search(q=\'{' + escape(query) + '}\')'
                });
            };

            return oneDriveFactory;
        }
    ]);