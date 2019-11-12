vacationApp.factory('dataService', ['$q', 'logger', 'model', 'enums', 'context', 'config',
    function ($q, logger, model, enums, context, config) {
        var apiBaseUrl = 'api/';
        var odataBaseUrl = 'odata/';

        var getLoggedEmployeeInfo = function (pictureType, scope) {
            var deferred = $q.defer();
            var options = {
                type: 'GET',
                url: apiBaseUrl + 'CurrentEmployees'
            };

            $.ajax(options)
              .then(succeeded)
              .fail(function (jqXHR, textStatus) {
                  logger.log(textStatus);
                  deferred.reject();
              });

            function succeeded(data) {
                var employee = new model.Employee(data[0]);
                scope.$apply(function () {
                    deferred.resolve(employee);
                });
                console.log('Retrieved employee from remote data source')
            }
            return deferred.promise;
        };

        var getCalendar = function (scope) {
            var deferred = $q.defer();
            var options = {
                url: odataBaseUrl + "CalendarsOData"
            };

            var expand = '$expand=CalendarHolidays';

            options.url = options.url + '?' + expand;

            OData.read(options.url, succeeded, function (err) {
                logger.log(err);
            });

            function succeeded(data) {
                var calendar = new model.Calendar(data.results[0]);

                scope.$apply(function () {
                    deferred.resolve(calendar);
                });
                log('Retrieved calendar from remote data source', calendar);
            }
            return deferred.promise;
        };

        var getUserVacations = function (parameters, scope) {
            var deferred = $q.defer();

            var options = {
                type: 'GET',
                url: apiBaseUrl + 'VacationRequests/' + parameters.year + '/user'
            };

            $.ajax(options)
                .then(succeeded)
                .fail(function (jqXHR, textStatus) {
                    logger.log(textStatus);
                    deferred.reject();
                });

            function succeeded(data) {
                var teamVacations = [];
                var length = data.length;
                for (var i = 0; i < length; i++) {
                    var teamVacation = new model.TeamVacation(data[i]);
                    teamVacations.push(teamVacation);
                }

                scope.$apply(function () {
                    deferred.resolve(teamVacations);
                });
                log('Retrieved team vacations from remote data source', teamVacations);
            }
            return deferred.promise;
        };

        var getTeamVacations = function (parameters, scope) {
            var deferred = $q.defer();
            var options = {
                url: odataBaseUrl + "VacationRequestsOData"
            };

            //expand
            var expand = '$expand=Employee/EmployeePictures';

            //filter
            var filterClauses = [];

            if (parameters.filter) {
                filterClauses.push("(" +
                    "substringof('" + parameters.filter + "', Employee/FirstName) eq true" +
                    " or substringof('" + parameters.filter + "', Employee/LastName) eq true" +
                    ")")
            }

            if (parameters.status) {
                filterClauses.push("Status eq '" + parameters.status + "'");
            }

            if (parameters.month) {
                filterClauses.push("month(From) eq " + parameters.month);
            }

            if (parameters.year) {
                filterClauses.push("year(From) eq " + parameters.year);
            }

            //paginatio
            var pagination;
            var top = parameters.pageSize,
                skip = parameters.pageSize * parameters.pageCount;

            pagination = '&$skip=' + skip + '&$top=' + top + '&$inlinecount=allpages';

            options.url = options.url + "?" + expand + pagination;

            if (filterClauses.length > 0) {
                options.url = options.url + "&" + "$filter=" + filterClauses.join(" and ");
            }

            OData.read(options.url, succeeded, function (err) {
                logger.log(err);
            });

            function succeeded(data) {
                var result = {
                    items: [],
                    count: data['__count']
                };

                var length = data.results.length;
                for (var i = 0; i < length; i++) {
                    var teamVacation = new model.TeamVacation(data.results[i]);
                    result.items.push(teamVacation);
                }

                scope.$apply(function () {
                    deferred.resolve(result);
                });
                log('Retrieved team vacations from remote data source', result);
            }
            return deferred.promise;
        };

        var getAllTeamVacations = function (parameters, scope) {
            var deferred = $q.defer();

            var options = {
                type: 'GET',
                url: apiBaseUrl + 'vacationrequests/team/employee?' +
                    'month=' + parameters.month +
                    '&year=' + parameters.year +
                    '&status=' + parameters.status +
                    '&pictureType=' + parameters.pictureType
            };
            $.ajax(options)
                .then(succeeded)
                .fail(function (jqXHR, textStatus) {
                    logger.log(textStatus);
                    deferred.reject();
                });

            function succeeded(data) {
                var employeeVacationRequests = [];
                var length = data.length;
                for (var i = 0; i < length; i++) {
                    var employee = new model.Employee(data[i]);
                    employeeVacationRequests.push(employee);
                }

                log('Retrieved all team vacations from remote data source', employeeVacationRequests);
                scope.$apply(function () {
                    deferred.resolve(employeeVacationRequests);
                });
            }

            return deferred.promise;
        };

        var acceptVacationRequest = function (parameters, scope) {
            var deferred = $q.defer();
            var options = {
                url: odataBaseUrl + "VacationRequestsOData(" + parameters.vacationRequestId + ")/AcceptVacation"
            };

            var request = {
                request: {},
                requestUri: options.url,
                method: 'POST',
                data: {
                    Reason: parameters.reason
                }
            };

            OData.request(request, function (data) {
                scope.$apply(function () {
                    deferred.resolve();
                });
                console.log(data);
            }, function (error) {
                console.log(error);
            });
            return deferred.promise;
        };

        var rejectVacationRequest = function (parameters, scope) {
            var deferred = $q.defer();
            var options = {
                url: odataBaseUrl + "VacationRequestsOData(" + parameters.vacationRequestId + ")/RejectVacation"
            };

            var request = {
                request: {},
                requestUri: options.url,
                method: 'POST',
                data: {
                    Reason: parameters.reason
                }
            };

            OData.request(request, function (data) {
                scope.$apply(function () {
                    deferred.resolve();
                });
                console.log(data);
            }, function (error) {
                console.log(error);
            });
            return deferred.promise;

        };

        var deleteVacationRequest = function (vacationRequestId, scope) {
            var deferred = $q.defer();

            var options = {
                requestUri: odataBaseUrl + 'VacationRequestsOData(' + vacationRequestId + ')',
                method: 'DELETE'
            };

            OData.request(options, succeeded, function (err) {
                logger.log(err);
            });

            function succeeded() {
                scope.$apply(function () {
                    deferred.resolve();
                });
                log('Updated vacation request');
            }
            return deferred.promise;
        };

        var addVacationRequest = function (vacationRequest, scope) {
            var deferred = $q.defer();
            var options = {
                requestUri: odataBaseUrl + 'VacationRequestsOData',
                method: 'POST',
                data: {
                    From: vacationRequest.from,
                    To: vacationRequest.to,
                    Comments: vacationRequest.comments
                }
            };

            OData.request(options, succeeded, function (err) {
                logger.log(err);
            });

            function succeeded(data) {
                scope.$apply(function () {
                    deferred.resolve(data);
                });
                log('Updated vacation request');
            }
            return deferred.promise;
        };

        var dataservice = {
            getLoggedEmployeeInfo: getLoggedEmployeeInfo,
            getCalendar: getCalendar,
            getUserVacations: getUserVacations,
            getTeamVacations: getTeamVacations,
            acceptVacationRequest: acceptVacationRequest,
            rejectVacationRequest: rejectVacationRequest,
            deleteVacationRequest: deleteVacationRequest,
            getAllTeamVacations: getAllTeamVacations,
            addVacationRequest: addVacationRequest
        };

        return dataservice;

        function log(msg, data) {
            logger.log(msg, data, 'dataService');
        }
    }]);