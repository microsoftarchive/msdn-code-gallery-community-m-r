/// <reference path="../rides.ts" />

module MyShuttle.Rides {
    export class Employee {
        EmployeeId: number;
        Name: string;
        Email: string;
        CustomerId: number;
        Picture: any;
    }

    export class Ride {
        RideId: number;
        StartDateTime: Date;
        EndDateTime: Date;
        StartLatitude: number;
        StartLongitude: number;
        EndLatitude: number;
        EndLongitude: number;
        StartAddress: string;
        EndAddress: string;
        Distance: number;
        Duration: number;
        Cost: number;
        Signature: any;
        Rating: number;
        Comments: string;
        VehicleId: number;
        CarrierId: number;
        DriverId: number;
        EmployeeId: number;
        Employee: Employee;
    }

    export class DataService {
        private client: Microsoft.WindowsAzure.MobileServiceClient;
        private rideTable: Microsoft.WindowsAzure.MobileServiceTable;
        private employeeTable: Microsoft.WindowsAzure.MobileServiceTable;

        constructor(private $rootScope: ng.IRootScopeService,
            private settingsService: MyShuttle.Core.SettingsService,
            private messengerService: MyShuttle.Core.MessengerService) {

            this.createClient();

            this.$rootScope.$on(this.messengerService.messageTypes.settingsChanged, () => {
                this.createClient();
            });

            return this;
        }

        private createClient() {
            this.client = new WindowsAzure.MobileServiceClient(
                this.settingsService.getMobileServiceUrl(),
                this.settingsService.mobileServiceKey);

            this.rideTable = this.client.getTable('ride');
            this.employeeTable = this.client.getTable('employee');
        }

        public getEmployee(id: string) {
            return this.employeeTable.where({ Id: id }).read();
        }

        public addRide(data) {
            var ride = new Ride();
            ride.StartDateTime = data.startRideTime.toDate();
            ride.EndDateTime = data.endRideTime.toDate();
            ride.StartLatitude = data.startRideLocation.latitude;
            ride.StartLongitude = data.startRideLocation.longitude;
            ride.EndLatitude = data.endRideLocation.latitude;
            ride.EndLongitude = data.endRideLocation.longitude;
            ride.StartAddress = this.settingsService.rideAddress; // TODO: Calculate
            ride.EndAddress = this.settingsService.rideAddress;
            ride.Distance = data.distance;
            ride.Duration = data.duration;
            ride.Cost = data.cost;
            ride.Signature = data.signature;
            ride.VehicleId = this.settingsService.vehicle.VehicleId;
            ride.CarrierId = this.settingsService.vehicle.CarrierId;
            ride.DriverId = this.settingsService.vehicle.DriverId;
            ride.Employee = new Employee();
            ride.Employee.Email = data.employeeEmail;

            return this.rideTable.insert(ride);
        }
    }

    angularModule.factory('dataService', ($rootScope, settingsService, messengerService) => {
        return new MyShuttle.Rides.DataService($rootScope, settingsService, messengerService);
    });
}