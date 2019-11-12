/// <reference path="../rides.ts" />

module MyShuttle.Rides {
    export class ModalService {
        private modalOptions: any;

        constructor(private $modal) {
            this.modalOptions = {
                backdrop: 'static',
                keyboard: false,
                modalFade: true
            };

            return this;
        }

        public showRideRequest(employee, position: MyShuttle.Core.Coordinate) {
            this.modalOptions.templateUrl = 'app/modules/rides/views/rideRequest.html';
            this.modalOptions.controller = function ($scope) {
                $scope.employee = employee;
                $scope.position = position;
            };

            return this.$modal.open(this.modalOptions).result;
        }
    }

    angularModule.factory('modalService', ($modal) => {
        return new MyShuttle.Rides.ModalService($modal);
    });
}