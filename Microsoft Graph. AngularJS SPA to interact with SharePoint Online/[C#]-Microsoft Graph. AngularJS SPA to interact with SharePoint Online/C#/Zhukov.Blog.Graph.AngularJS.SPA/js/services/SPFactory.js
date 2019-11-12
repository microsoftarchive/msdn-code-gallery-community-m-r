angular
    .module('MicrosoftGraphSPA')
    .constant('graphBetaUrl', 'https://graph.microsoft.com/beta')
    .factory('spFactory',
    [
        '$http', 'graphBetaUrl',
        function($http, graphBetaUrl) {

            var spFactory = {};

            // Получение списка сайтов
            spFactory.getSites = function(siteId) {
                var url = graphBetaUrl + '/sharepoint/sites';
                if (siteId) {
                    url = url + '/' + siteId + '/sites';
                }
                return $http({
                    method: 'GET',
                    url: url
                });
            };

            // Получение информации о сайте
            spFactory.getSite = function(siteId) {
                return $http({
                    method: 'GET',
                    url: graphBetaUrl + '/sharepoint/sites/' + siteId
                });
            };

            // Получение списков сайта
            spFactory.getLists = function(siteId) {
                return $http({
                    method: 'GET',
                    url: graphBetaUrl + '/sharepoint/sites/' + siteId + '/lists'
                });
            };

            spFactory.getList = function(siteId, listId) {
                return $http({
                    method: 'GET',
                    url: graphBetaUrl + '/sharepoint/sites/' + siteId + '/lists/' + listId
                });
            };

            spFactory.getItems = function(siteId, listId) {
                return $http({
                    method: 'GET',
                    url: graphBetaUrl + '/sharepoint/sites/' + siteId + '/lists/' + listId + '/items?expand=columnSet'
                });
            };

            spFactory.getItem = function(siteId, listId, itemId) {
                return $http({
                    method: 'GET',
                    url: graphBetaUrl + '/sharepoint/sites/' + siteId + '/lists/' + listId + '/items/' + itemId + '?expand=columnSet'
                });
            };

            return spFactory;
        }
    ]);