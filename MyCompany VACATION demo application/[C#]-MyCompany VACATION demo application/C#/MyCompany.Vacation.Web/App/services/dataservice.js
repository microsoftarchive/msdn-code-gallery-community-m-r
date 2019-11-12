define(['services/logger', 'durandal/system', 'services/model', 'services/enums'], function (logger, system, model, enums) {
    var apiBaseUrl = 'api/';

    var getLoggedEmployeeInfo = function (pictureType) {
        return system.defer(function (deferred) {
            var options = {
                type: 'GET',
                cache:false,
                url: apiBaseUrl + 'Employees/current/' + pictureType
            };
            $.ajax(options)
                .then(succeeded)
                .fail(function (jqXHR, textStatus) {
                    failed(jqXHR, textStatus);
                    deferred.reject();
                });

            function succeeded(data) {
                var employee = new model.Employee(data);
                log('Retrieved current user from remote data source', data);
                deferred.resolve(employee);
            }
        }).promise();
    };
    
    var getCalendar = function () {
        return system.defer(function (deferred) {
            var options = {
                type: 'GET',
                url: apiBaseUrl + 'calendar'
            };
            $.ajax(options)
                .then(succeeded)
                .fail(function (jqXHR, textStatus) {
                    failed(jqXHR, textStatus);
                    deferred.reject();
                });

            function succeeded(data) {
                var calendar = new model.Calendar(data);

                deferred.resolve(calendar);
                log('Retrieved calendar from remote data source', calendar);
            }
        }).promise();
    };
    
    var getUserVacations = function (parameters) {
        return system.defer(function (deferred) {
            var options = {
                type: 'GET',
                url: apiBaseUrl + 'VacationRequests/' + parameters.year + '/user'
            };
            $.ajax(options)
                .then(succeeded)
                .fail(function (jqXHR, textStatus) {
                    failed(jqXHR, textStatus);
                    deferred.reject();
                });

            function succeeded(data) {
                var teamVacations = [];
                var length = data.length;
                for (var i = 0; i < length; i++) {
                    var teamVacation = new model.TeamVacation(data[i]);
                    teamVacations.push(teamVacation);
                }

                deferred.resolve(teamVacations);
                log('Retrieved team vacations from remote data source', teamVacations);
            }
        }).promise();
    };
    
    var getTeamVacations = function (parameters) {
        return system.defer(function (deferred) {
            var options = {
                type: 'GET',
                url: apiBaseUrl + 'VacationRequests/team?' +
                    'filter=' + parameters.filter +
                    '&month=' + parameters.month +
                    '&year=' + parameters.year +
                    '&status=' + parameters.status +
                    '&pictureType=' + parameters.pictureType +
                    '&pageSize=' + parameters.pageSize +
                    '&pageCount=' + parameters.pageCount
            };
            $.ajax(options)
                .then(succeeded)
                .fail(function (jqXHR, textStatus) {
                    failed(jqXHR, textStatus);
                    deferred.reject();
                });

            function succeeded(data) {
                var teamVacations = [];
                var length = data.length;
                for (var i = 0; i < length; i++) {
                    var teamVacation = new model.TeamVacation(data[i]);
                    teamVacations.push(teamVacation);
                }

                deferred.resolve(teamVacations);
                log('Retrieved team vacations from remote data source', teamVacations);
            }
        }).promise();
    };

    var getAllTeamVacations = function (parameters) {
        return system.defer(function (deferred) {
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
                    failed(jqXHR, textStatus);
                    deferred.reject();
                });

            function succeeded(data) {
                var employeeVacationRequests = [];
                var length = data.length;
                for (var i = 0; i < length; i++) {
                    var employee = new model.Employee(data[i]);
                    employeeVacationRequests.push(employee);
                }

                deferred.resolve(employeeVacationRequests);
                log('Retrieved all team vacations from remote data source', employeeVacationRequests);
            }
        }).promise();
    };
    
    var getTeamVacationCount = function (parameters) {
        return system.defer(function (deferred) {
            var options = {
                type: 'GET',
                url: apiBaseUrl + 'VacationRequests/team/count?' +
                    'filter=' + parameters.filter +
                    '&month=' + parameters.month +
                    '&year=' + parameters.year +
                    '&status=' + parameters.status
            };
            $.ajax(options)
                .then(succeeded)
                .fail(function (jqXHR, textStatus) {
                    failed(jqXHR, textStatus);
                    deferred.reject();
                });

            function succeeded(data) {
                deferred.resolve(data);
                log('Retrieved team vacations count from remote data source', data);
            }
        }).promise();
    };

    var acceptVacationRequest = function(parameters) {
        parameters.status = enums.vacationRequestStatus.approved;
        return updateVacationRequestStatus(parameters);
    };

    var rejectVacationRequest = function(parameters) {
        parameters.status = enums.vacationRequestStatus.denied;
        return updateVacationRequestStatus(parameters);
    };
    
    var deleteVacationRequest = function (vacationRequestId) {
        return system.defer(function (deferred) {
            var options = {
                type: 'DELETE',
                url: apiBaseUrl + 'vacationrequests?' +
                    'vacationRequestId=' + vacationRequestId
            };
            $.ajax(options)
                .then(succeeded)
                .fail(function (jqXHR, textStatus) {
                    failed(jqXHR, textStatus);
                    deferred.reject();
                });

            function succeeded() {
                deferred.resolve();
                log('Updated vacation request');
            }
        }).promise();
    };

    var addVacationRequest = function (vacationRequest) {
        return system.defer(function (deferred) {
            var options = {
                type: 'POST',
                data: JSON.stringify(vacationRequest),
                url: apiBaseUrl + 'vacationrequests?'
            };
            $.ajax(options)
                .then(succeeded)
                .fail(function (jqXHR, textStatus) {
                    failed(jqXHR, textStatus);
                    deferred.reject();
                });

            function succeeded(data) {
                deferred.resolve(data);
                log('Updated vacation request');
            }
        }).promise();
    };

    var updateVacationRequestStatus = function(parameters) {
        return system.defer(function (deferred) {
            var options = {
                type: 'PUT',
                url: apiBaseUrl + 'vacationrequests/' + parameters.vacationRequestId +
                '/status/'  + parameters.status +
                '?reason=' + (parameters.reason || '')
            };

            $.ajax(options)
                .then(succeeded)
                .fail(function (jqXHR, textStatus) {
                    failed(jqXHR, textStatus);
                    deferred.reject();
                });

            function succeeded() {
                deferred.resolve();
                log('Updated vacation request');
            }
        }).promise();
    };

    var dataservice = {
        getLoggedEmployeeInfo: getLoggedEmployeeInfo,
        getCalendar: getCalendar,
        getUserVacations: getUserVacations,
        getTeamVacations: getTeamVacations,
        getTeamVacationCount: getTeamVacationCount,
        acceptVacationRequest: acceptVacationRequest,
        rejectVacationRequest: rejectVacationRequest,
        deleteVacationRequest: deleteVacationRequest,
        getAllTeamVacations: getAllTeamVacations,
        addVacationRequest: addVacationRequest
    };

    return dataservice;

    function log(msg, data) {
        logger.log(msg, data, system.getModuleId(dataservice));
    }

    function failed(jqXHR, textStatus) {
        var msg = 'Error getting data. ' + textStatus;
        logger.log(msg, jqXHR, system.getModuleId(dataservice));
    }
});