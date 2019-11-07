'use strict';

angular.module('myShuttleCore').service('httpInterceptorService', ['$http', 'messenger', '$q', '$state', 'modalService',
    function ($http, messenger, $q, $state, modalService) {
        var service = this;

        var requestCompleted = function (forceEndLoading) {
            // don't send notification until all requests are complete
            if (forceEndLoading || ($http.pendingRequests.length < 1)) {
                messenger.send(messenger.messageTypes.endLoading);
            }
        };

        var showErrorMessage = function (response) {
            var message = '',
                title = '';

            switch (response.status) {
                case 400:
                    title = 'Bad Request';
                    message = 'There was an error in the request.';
                    break;
                case 401:
                    title = 'Unauthorized';
                    message = 'This action requires an authenticated user.';
                    break;
                case 403:
                    title = 'Forbidden';
                    message = 'This action requires permissions that you do not own.';
                    break;
                case 404:
                    title = 'Not Found';
                    message = 'Unable to find the requested resource.';
                    break;
                case 500:
                    title = 'Internal Server Error';
                    message = 'There was an error in the server. Please try again later.';
                    break;
                default:
                    title = 'Unknown error';
                    message = 'An unknown error occurred. Please try again later.';
                    break;
            }

            if (response && response.data)
                console.log('Http error ' + response.data.message);

            return modalService.show({
                headerText: title,
                bodyText: message
            });
        };

        service.startRequest = function (url) {
            if(url === 'vehicles/events') return;
            messenger.send(messenger.messageTypes.startLoading);
        };

        service.requestSuccess = function (response) {
            requestCompleted();
            return response;
        };

        service.requestError = function (response) {
            requestCompleted();

            showErrorMessage(response).finally(function (result) {
                //if (response.status === 401) $state.go('signin');
            });

            return $q.reject(response);
        };
    }
]);
