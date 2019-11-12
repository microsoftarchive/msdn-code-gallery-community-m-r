(function () {
    'use strict';

    angular.module('VisitorsApp').factory('dataSvc', [
        '$http',
        '$q',
        'modelSvc',
        'settings',
        'enums',
        dataSvc
    ]);

    function dataSvc($http, $q, modelSvc, settings, enums) {

        function log(msg) {
            console.log(msg);
        }

        function handleError(deferred, data, message) {
            var errorMessage = data.Message;

            if (errorMessage) {
                log(message + ': ' + errorMessage);
                deferred.reject(errorMessage);
            } else {
                log(message);
                deferred.reject();
            }
        }

        function getTodayVisits() {
            var path = '/visits/company',
                filter = '',
                pictureType = enums.pictureType.small,
                pageSize = settings.maxResults,
                pageCount = 0,
                dateFilter = moment().utc().add('hours', -1).toDate().toJSON(),
                toDate = moment({ hour: 0, minute: 0, seconds: 0, milliseconds: 0 }).utc().add('days', 1).add('seconds', -1).toDate().toJSON();

            var config = {
                params: {
                    filter: filter,
                    pictureType: pictureType,
                    pageSize: pageSize,
                    pageCount: pageCount,
                    dateFilter: dateFilter,
                    toDate: toDate
                }
            },
                deferred = $q.defer();

            $http.get(settings.basePath + path, config)
                 .success(function (data, status, headers, config) {
                     var visits = [],
                         visit = {};

                     for (var i = 0, length = data.length; i < length; i++) {
                         visit = new modelSvc.VisitListItem(data[i]);
                         visits.push(visit);
                     }

                     deferred.resolve(visits);
                 })
                 .error(function (data, status, headers, config) {
                     handleError(deferred, data, 'Error retrieving today visits from remote data source');
                 });

            return deferred.promise;
        }

        function getAllVisits() {
            var path = '/visits/company/fromdate',
                filter = '',
                pictureType = enums.pictureType.small,
                pageSize = settings.maxResults,
                pageCount = 0,
                dateFilter = moment({ hour: 0, minute: 0, seconds: 0, milliseconds: 0 }).add('days', 1).toDate().toJSON();

            var config = {
                params: {
                    filter: filter,
                    pictureType: pictureType,
                    pageSize: pageSize,
                    pageCount: pageCount,
                    dateFilter: dateFilter
                }
            },
                deferred = $q.defer();

            $http.get(settings.basePath + path, config)
                .success(function (data, status, headers, config) {
                    var visitItems = [],
                        visitItem = {};

                    for (var i = 0, length = data.length; i < length; i++) {
                        visitItem = new modelSvc.VisitListItem(data[i]);
                        visitItems.push(visitItem);
                    }

                    deferred.resolve(visitItems);
                })
                .error(function (data, status, headers, config) {
                    handleError(deferred, data, 'Error retrieving visits from remote data source');
                });

            return deferred.promise;
        }

        function getVisit(visitId) {
            var path = '/visits',
                pictureType = enums.pictureType.small,
                deferred = $q.defer();

            $http.get(settings.basePath + path + '/' + visitId + '/' + pictureType)
                .success(function (data, status, header, config) {
                    var visit = new modelSvc.Visit(data);
                    deferred.resolve(visit);
                })
                .error(function (data, status, header, config) {
                    handleError(deferred, data, 'Error retrieving visit ' + visitId + ' from remote data source');
                });

            return deferred.promise;
        }

        function updateVisitorPictures(visitorPictures) {
            var path = '/visitorpictures/addOrUpdatePictures',
                deferred = $q.defer();

            $http.post(settings.basePath + path, visitorPictures)
                 .success(function (data, status, headers, config) {
                     deferred.resolve(data);
                 })
                 .error(function (data, status, headers, config) {
                     handleError(deferred, data, 'Error updating visitor pictures');
                 });

            return deferred.promise;
        }

        var service = {
            getTodayVisits: getTodayVisits,
            getAllVisits: getAllVisits,
            getVisit: getVisit,
            updateVisitorPictures: updateVisitorPictures
        };

        return service;
    }

}());