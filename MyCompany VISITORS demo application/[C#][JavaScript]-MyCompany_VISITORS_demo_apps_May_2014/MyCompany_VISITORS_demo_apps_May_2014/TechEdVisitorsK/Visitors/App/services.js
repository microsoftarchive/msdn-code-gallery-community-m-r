(function servicesRegistration() {
    'use strict';

    var app = angular.module('app');

    app.factory('dataService', [
        '$http',
        '$q',
        function ($http, $q) {

            var apiBaseUrl = '/noauth/api/';
            var defaultPicture = 'Content/Images/no-photo.jpg';

            var Employee = function (entity) {
                var employee = this;

                var pictureContent = entity && entity.EmployeePictures && entity.EmployeePictures.length > 0 ? entity.EmployeePictures[0].Content : null;

                employee.employeeId = entity && entity.EmployeeId;
                employee.firstName = entity && entity.FirstName;
                employee.lastName = entity && entity.LastName;
                employee.picture = pictureContent ? 'data:image/jpeg;base64,' + pictureContent : defaultPicture;
                employee.position = entity && entity.JobTitle;
                employee.email = entity && entity.Email;
                employee.fullName = employee.firstName + ' ' + employee.lastName;
            };

            var Visitor = function (entity) {
                var visitor = this;

                var pictureContent = entity && entity.VisitorPictures && entity.VisitorPictures.length > 0 && entity.VisitorPictures[0].Content;

                visitor.visitorId = entity && entity.VisitorId;
                visitor.firstName = entity && entity.FirstName;
                visitor.lastName = entity && entity.LastName;
                visitor.picture = pictureContent ? 'data:image/jpeg;base64,' + pictureContent : defaultPicture;
                visitor.company = entity && entity.Company;
                visitor.personalId = entity && entity.PersonalId;
                visitor.email = entity && entity.Email;
                visitor.position = entity && entity.Position;
                visitor.createdDateTime = entity && entity.CreatedDateTime;
                visitor.lastModifiedDateTime = entity && entity.LastModifiedDateTime;
                visitor.fullName = visitor.firstName + ' ' + visitor.lastName;

                return visitor;
            };

            var getLoggedEmployeeInfo = function (pictureType) {
                var deferred = $q.defer();

                $http.get(apiBaseUrl + 'Employees/current/' + pictureType)
                    .success(function (data, status, headers, config) {
                        var model = new Employee(data);
                        deferred.resolve(model);
                    })
                    .error(function (data, status, headers, config) {
                        deferred.reject();
                    });

                return deferred.promise;
            };

            function getVisitors(filter, pictureType, pageIndex) {

                var deferred = $q.defer();

                var config = {
                    params: {
                        filter: filter ? filter : '',
                        pictureType: pictureType ? pictureType : 1,
                        pageSize: 100,
                        pageCount: pageIndex < 1 ? 0 : pageIndex,
                    }
                };

                $http.get(apiBaseUrl + 'visitors', config)
                    .success(function (data, status, headers, config) {
                        var visitors = [];
                        var length = data.length;
                        for (var i = 0; i < length; i++) {
                            var visitor = new Visitor(data[i]);
                            visitors.push(visitor);
                        }
                        deferred.resolve(visitors);
                    })
                    .error(function (data, status, headers, config) {
                        deferred.reject();
                    });

                return deferred.promise;
            }

            return {
                getLoggedEmployeeInfo: getLoggedEmployeeInfo,
                getVisitors: getVisitors
            }
        }
    ]);

}())