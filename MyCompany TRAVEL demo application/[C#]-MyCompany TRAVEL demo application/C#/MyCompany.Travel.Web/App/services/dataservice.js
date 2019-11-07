define(['services/logger', 'durandal/system', 'services/model', 'config'], function (logger, system, model, config) {
    var apiBaseUrl = 'api/';
    var serverDateFormat = "YYYY-MM-DDT00:00:00.000";

    var getLoggedEmployeeInfo = function (pictureType) {
        return system.defer(function (deferred) {
            var options = {
                type: 'GET',
                url: apiBaseUrl + 'Employees/' + pictureType,
                cache: false
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

    var getUserTravelRequests = function (parameters, cache) {
        return system.defer(function (deferred) {
            var options = {
                type: 'GET',
                cache: cache,
                url: apiBaseUrl + 'travelRequests/user?' +
                    'filter=' + parameters.filter +
                    '&status=' + parameters.travelRequestStatus +
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
                var travels = [];
                var length = data.length;
                for (var i = 0; i < length; i++) {
                    var travel = new model.TravelRequest(data[i]);
                    travels.push(travel);
                }

                deferred.resolve(travels);
                log('Retrieved user travel requests from remote data source', travels);
            }
        }).promise();
    };

    var getNotFinishedUserTravelRequests = function (parameters, cache) {
        return system.defer(function (deferred) {
            var options = {
                type: 'GET',
                cache: cache,
                url: apiBaseUrl + 'travelRequests/unfinished/user?' +
                    'filter=' + parameters.filter +
                    '&status=' + parameters.travelRequestStatus
            };
            $.ajax(options)
                .then(succeeded)
                .fail(function (jqXHR, textStatus) {
                    failed(jqXHR, textStatus);
                    deferred.reject();
                });

            function succeeded(data) {
                var travels = [];
                var length = data.length;
                for (var i = 0; i < length; i++) {
                    var travel = new model.TravelRequest(data[i]);
                    travels.push(travel);
                }

                deferred.resolve(travels);
                log('Retrieved user unfinished travel requests from remote data source', travels);
            }
        }).promise();
    };

    var getTeamTravelRequests = function (parameters, cache) {
        return system.defer(function (deferred) {
            var options = {
                type: 'GET',
                cache: cache,
                url: apiBaseUrl + 'travelrequests/team?' +
                    'filter=' + parameters.filter +
                    '&status=' + parameters.travelRequestStatus +
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
                var travels = [];
                var length = data.length;
                for (var i = 0; i < length; i++) {
                    var travel = new model.TravelRequest(data[i]);
                    travels.push(travel);
                }

                deferred.resolve(travels);
                log('Retrieved team travel requests from remote data source', travels);
            }
        }).promise();
    };

    var getNotFinishedTeamTravelRequests = function (parameters, cache) {
        return system.defer(function (deferred) {
            var options = {
                type: 'GET',
                cache: cache,
                url: apiBaseUrl + 'travelrequests/unfinished/team?' +
                    'filter=' + parameters.filter +
                    '&status=' + parameters.travelRequestStatus +
                    '&pictureType=' + parameters.pictureType
            };
            $.ajax(options)
                .then(succeeded)
                .fail(function (jqXHR, textStatus) {
                    failed(jqXHR, textStatus);
                    deferred.reject();
                });

            function succeeded(data) {
                var travels = [];
                var length = data.length;
                for (var i = 0; i < length; i++) {
                    var travel = new model.TravelRequest(data[i]);
                    travels.push(travel);
                }

                deferred.resolve(travels);
                log('Retrieved team unfinished travel requests from remote data source', travels);
            }
        }).promise();
    };

    var getAllTravelRequests = function (parameters, cache) {
        return system.defer(function (deferred) {
            var options = {
                type: 'GET',
                cache: cache,
                url: apiBaseUrl + 'travelrequests/all?' +
                    'filter=' + parameters.filter +
                    '&status=' + parameters.travelRequestStatus +
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
                var travels = [];
                var length = data.length;
                for (var i = 0; i < length; i++) {
                    var travel = new model.TravelRequest(data[i]);
                    travels.push(travel);
                }

                deferred.resolve(travels);
                log('Retrieved all travel requests from remote data source', travels);
            }
        }).promise();
    };

    var getTeamTravelDistribution = function (maxPicturesPerCity) {
        return system.defer(function (deferred) {
            var options = {
                type: 'GET',
                url: apiBaseUrl + 'travelrequests/team/distribution/' + maxPicturesPerCity
            };
            $.ajax(options)
                .then(succeeded)
                .fail(function (jqXHR, textStatus) {
                    failed(jqXHR, textStatus);
                    deferred.reject();
                });

            function succeeded(data) {
                var travelsDistribution = [];
                var length = data.length;
                for (var i = 0; i < length; i++) {
                    var cityTravels = new model.TravelDistribution(data[i]);
                    travelsDistribution.push(cityTravels);
                }

                deferred.resolve(travelsDistribution);
                log('Retrieved team travel distribution from remote data source', travelsDistribution);
            }
        }).promise();
    };
    var getUserTravelRequestsCount = function (parameters, cache) {
        return system.defer(function (deferred) {
            var options = {
                type: 'GET',
                cache: cache,
                url: apiBaseUrl + 'travelRequests/user/count?' +
                    'filter=' + parameters.filter + '&status=' + parameters.travelRequestStatus
            };
            $.ajax(options)
                .then(succeeded)
                .fail(function (jqXHR, textStatus) {
                    failed(jqXHR, textStatus);
                    deferred.reject();
            });

            function succeeded(data) {
                deferred.resolve(data);
                log('Retrieved user travel requests count from remote data source', data);
            }
        }).promise();
    };

    var getTeamTravelRequestsCount = function (parameters, cache) {
        return system.defer(function (deferred) {
            var options = {
                type: 'GET',
                cache: cache,
                url: apiBaseUrl + 'travelRequests/team/count?' +
                    'filter=' + parameters.filter + '&status=' + parameters.travelRequestStatus
            };
            $.ajax(options)
                .then(succeeded)
                .fail(function (jqXHR, textStatus) {
                    failed(jqXHR, textStatus);
                    deferred.reject();
                });

            function succeeded(data) {
                deferred.resolve(data);
                log('Retrieved team travel requests count from remote data source', data);
            }
        }).promise();
    };

    var getAllTravelRequestsCount = function (parameters, cache) {
        return system.defer(function (deferred) {
            var options = {
                type: 'GET',
                cache: cache,
                url: apiBaseUrl + 'travelRequests/all/count?' +
                    'filter=' + parameters.filter + '&status=' + parameters.travelRequestStatus
            };
            $.ajax(options)
                .then(succeeded)
                .fail(function (jqXHR, textStatus) {
                    failed(jqXHR, textStatus);
                    deferred.reject();
                });

            function succeeded(data) {
                deferred.resolve(data);
                log('Retrieved all travel requests count from remote data source', data);
            }
        }).promise();
    };

    var addTravelRequest = function (travelRequest) {
        return system.defer(function (deferred) {
            var options = {
                type: 'POST',
                data: ko.toJSON(travelRequest),
                url: apiBaseUrl + 'travelrequests'
            };
            $.ajax(options)
                .then(succeeded)
                .fail(function (jqXHR, textStatus) {
                    failed(jqXHR, textStatus);
                    deferred.reject();
                });

            function succeeded(data) {
                var t = travelRequest();
                t.travelRequestId(data);
                deferred.resolve(t);
                log('Added travel', t);
            }
        }).promise();
    };

    var getTravelRequest = function (travelRequestId, pictureType, cache) {
        return system.defer(function (deferred) {
            var options = {
                type: 'GET',
                cache: cache,
                url: apiBaseUrl + 'travelRequests/' + travelRequestId + '/' + pictureType
            };
            $.ajax(options)
                .then(succeeded)
                .fail(function (jqXHR, textStatus) {
                    failed(jqXHR, textStatus);
                    deferred.reject();
                });

            function succeeded(data) {
                var travel = null;
                if (data)
                {
                    travel = new model.TravelRequest(data);
                }

                deferred.resolve(travel);
                log('Retrieved travel requests from remote data source', travel);
            }
        }).promise();
    };    

    var deleteTravelRequest = function (travelRequestId) {
        return system.defer(function (deferred) {
            var options = {
                type: 'DELETE',
                url: apiBaseUrl + 'travelRequests/' + travelRequestId
            };
            $.ajax(options)
                .then(succeeded)
                .fail(function (jqXHR, textStatus) {
                    failed(jqXHR, textStatus);
                    deferred.reject();
                });

            function succeeded() {
                deferred.resolve();
                log('Deleted travel request with id ' + travelRequestId, null);
            }
        }).promise();
    };

    var updateTravelRequest = function (travelRequest) {
        return system.defer(function (deferred) {
            var options = {
                type: 'PUT',
                data: ko.toJSON(travelRequest),
                url: apiBaseUrl + 'travelRequests'
            };
            $.ajax(options)
                .then(succeeded)
                .fail(function (jqXHR, textStatus) {
                    failed(jqXHR, textStatus);
                    deferred.reject();
                });

            function succeeded() {
                deferred.resolve(travelRequest);
                log('Updated travelRequest with id ' + travelRequest().travelRequestId, travelRequest);
            }
        }).promise();
    };

    var updateTravelRequestStatus = function (travelRequestId, travelRequestStatus, comments) {
        return system.defer(function (deferred) {
            var options = {
                type: 'GET',
                url: apiBaseUrl + 'travelrequests/update?' +
                                    'travelRequestId=' + travelRequestId +
                                    '&status=' + travelRequestStatus + 
                                    '&comments=' + (comments ? comments : ''),
            };
            $.ajax(options)
                .then(succeeded)
                .fail(function (jqXHR, textStatus) {
                    failed(jqXHR, textStatus);
                    deferred.reject();
                });

            function succeeded() {
                deferred.resolve();
                log('Updated status of travelRequest with id ' + travelRequestId + ' to ' + travelRequestStatus);
            }
        }).promise();
    };

    function uploadFile(file, travelRequestId, fileFriendlyName) {
        return system.defer(function (deferred) {
            var data = new FormData();
            data.append('image-file', file);
            if (travelRequestId)
                data.append('travelRequestId', travelRequestId);
            if (fileFriendlyName)
                data.append('fileFriendlyName', fileFriendlyName);

            var options = {
                type: 'POST',
                data: data,
                processData: false,
                contentType: false,
                url: apiBaseUrl + 'travelattachments/files'
            };
            $.ajax(options)
                .then(function (travelAttachmentId) {
                    deferred.resolve(travelAttachmentId);
                    log('Upload file');
                })
                .fail(function (jqXHR, textStatus) {
                    failed(jqXHR, textStatus);
                    deferred.reject();
                }
            );
        }).promise();
    }

    var deleteTravelRequestAttachment = function (travelAttachmentId) {
        return system.defer(function (deferred) {
            var options = {
                type: 'DELETE',
                url: apiBaseUrl + 'travelAttachments/' + travelAttachmentId
            };
            $.ajax(options)
                .then(succeeded)
                .fail(function (jqXHR, textStatus) {
                    failed(jqXHR, textStatus);
                    deferred.reject();
                });

            function succeeded() {
                deferred.resolve();
                log('Deleted travel attachment with id ' + travelAttachmentId, null);
            }
        }).promise();
    };

    var dataservice = {
        getLoggedEmployeeInfo: getLoggedEmployeeInfo,
        getUserTravelRequestsCount: getUserTravelRequestsCount,
        getUserTravelRequests: getUserTravelRequests,
        addTravelRequest: addTravelRequest,
        getTravelRequest: getTravelRequest,
        deleteTravelRequest: deleteTravelRequest,
        updateTravelRequest: updateTravelRequest,
        getTeamTravelRequests: getTeamTravelRequests,
        getTeamTravelRequestsCount: getTeamTravelRequestsCount,
        updateTravelRequestStatus: updateTravelRequestStatus,
        getTeamTravelDistribution: getTeamTravelDistribution,
        getAllTravelRequests: getAllTravelRequests,
        getAllTravelRequestsCount: getAllTravelRequestsCount,
        uploadFile: uploadFile,
        deleteTravelRequestAttachment: deleteTravelRequestAttachment,
        getNotFinishedUserTravelRequests: getNotFinishedUserTravelRequests,
        getNotFinishedTeamTravelRequests: getNotFinishedTeamTravelRequests
    };

    return dataservice;

    //#region Internal methods

    function log(msg, data) {
        logger.log(msg, data, system.getModuleId(dataservice));
    }

    function failed(jqXHR, textStatus) {
        var msg = 'Error getting data. ' + textStatus;
        logger.log(msg, jqXHR, system.getModuleId(dataservice));
    }

    //#endregion
});