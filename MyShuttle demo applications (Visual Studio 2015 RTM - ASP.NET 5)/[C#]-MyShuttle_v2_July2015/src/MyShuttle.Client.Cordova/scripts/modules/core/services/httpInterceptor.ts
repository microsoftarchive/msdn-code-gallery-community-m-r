/// <reference path="../core.ts" />

module MyShuttle.Core {
    export class HttpInterceptorService {
        private requestCompleted = function (forceEndLoading) {
            // don't send notification until all requests are complete
            if (forceEndLoading || (this.$http.pendingRequests.length < 1)) {
                this.messengerService.send(this.messengerService.messageTypes.endLoading);
            }
        }

        constructor(private $http: ng.IHttpService, private messengerService: MessengerService, private $q) {
            return this;
        }

        public startRequest = function () {
            this.messengerService.send(this.messengerService.messageTypes.startLoading);
        }

        public requestSuccess = function (response) {
            this.requestCompleted(false);
            return response;
        }

        public requestError = function (response) {
            this.requestCompleted(false);

            // TODO: Show error message
            console.log('An error ocurred: ' + response);

            return this.$q.reject(response);
        }
    }

    angularModule.service('httpInterceptorService', HttpInterceptorService);
}
