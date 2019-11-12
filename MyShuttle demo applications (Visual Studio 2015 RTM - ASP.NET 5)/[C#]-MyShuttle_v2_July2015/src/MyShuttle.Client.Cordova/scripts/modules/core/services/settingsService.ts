/// <reference path="../core.ts" />

module MyShuttle.Core {
    export class Vehicle {
        VehicleId: number;
        CarrierId: number;
        DriverId: number;
        Rate: number;
    }

    export class Coordinate {
        latitude: number;
        longitude: number;

        constructor(latitude: number, longitude: number) {
            this.latitude = latitude;
            this.longitude = longitude;
        }
    }

    export class SettingsService {
        public defaultEmployeeEmail: string;
        public vehicle: Vehicle;
        public startRideLocation: Coordinate;
        public endRideLocation: Coordinate;
        public rideDistance: number;
        public rideAddress: string;
        public bingMapsKey: string;
        public mobileServiceKey: string;
        public gcmSenderId: string;
        public realTimeNotificationsServerUrl: string;

        constructor(private storageService: MyShuttle.Core.StorageService) {
            this.defaultEmployeeEmail = 'amanda@microsoft.com';

            this.vehicle = new Vehicle();
            this.vehicle.VehicleId = 5;
            this.vehicle.DriverId = 5;
            this.vehicle.CarrierId = 1;
            this.vehicle.Rate = 3;

            var coord = new Coordinate(47.641944, -122.127222);
            this.startRideLocation = coord;
            this.endRideLocation = coord;
            this.rideDistance = 0.1;
            this.rideAddress = 'Microsoft Redmond Campus, Redmond, Washington';

            this.bingMapsKey = 'YOUR_BING_MAPS_TOKEN_KEY';
            this.mobileServiceKey = 'YOUR_MOBILE_SERVICE_KEY';
            this.gcmSenderId = 'SENDER_ID';
            this.realTimeNotificationsServerUrl = 'http://YOUR_SITE.azurewebsites.net/web/';

            return this;
        }

        public getMobileServiceUrl():string {
            return this.storageService.getValue('serviceUrl', 'https://YOUR_SITE.azure-mobile.net/');
        }
    }

    angularModule.factory('settingsService', (storageService) => {
        return new MyShuttle.Core.SettingsService(storageService);
    });
}