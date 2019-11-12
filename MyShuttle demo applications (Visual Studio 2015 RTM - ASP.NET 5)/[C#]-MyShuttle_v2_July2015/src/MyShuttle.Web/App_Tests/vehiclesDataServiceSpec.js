'use strict';

describe("vehicles data service test", function () {

    beforeEach(function () {

    });

    it("VehiclesService_Test_Call_Add__Should_Add_New_Vehicle", function () {
        var asyncCallComplete = false, result = null, _this = this;

        var vehicle = {CarrierId:1, LicensePlate:'Name', DriverId:4};

        runs(function() {
            return $.ajax('http://localhost:5000/vehicles/post', {
                type: 'POST',
                data: JSON.stringify(vehicle),
                contentType:"application/json",
                dataType: 'json',
                success: function(data) {
                    asyncCallComplete = true;
                    result = data;
                },
                error: function(xhr, ajaxOptions, thrownError) {
                    console.log(thrownError);
                    asyncCallComplete = true;
                }
            });
        });

        // Wait for the asynchronous call to complete
        waitsFor(function() {
            return asyncCallComplete !== false;
        });

        // Get the new carrierId
        return runs(function() {
            expect(result).not.toBeNull();
            expect(result > 0).toBeTruthy();
        });
    });

    it("VehiclesService_Test_Call_Get_Should_Get_Vehicle", function () {
        var asyncCallComplete = false, result = null, _this = this;

        runs(function() {
            return $.ajax('http://localhost:5000/vehicles/1', {
                type: 'GET',
                contentType:"application/json",
                dataType: "json",
                success: function(data) {
                    asyncCallComplete = true;
                    result = data;
                },
                error: function(xhr, ajaxOptions, thrownError) {
                    asyncCallComplete = true;
                }
            });
        });

        // Wait for the asynchronous call to complete
        waitsFor(function() {
            return asyncCallComplete !== false;
        });

        // Asserts
        return runs(function() {
            expect(result).not.toBeNull();

            expect(result.VehicleId).toBeDefined();
            expect(result.LicensePlate).toBeDefined();
            expect(result.Model).toBeDefined();
            expect(result.Picture).toBeDefined();
            expect(result.Make).toBeDefined();
            expect(result.Type).toBeDefined();
            expect(result.Seats).toBeDefined();
            expect(result.Latitude).toBeDefined();
            expect(result.Longitude).toBeDefined();
            expect(result.Latitude).toBeDefined();
            expect(result.DistanceFromGivenPosition).toBeDefined();
            expect(result.VehicleStatus).toBeDefined();
            expect(result.CarrierId).toBeDefined();
            expect(result.DriverId).toBeDefined();
            expect(result.Rate).toBeDefined();
            expect(result.RatingAvg).toBeDefined();
            expect(result.TotalRides).toBeDefined();

            expect(result.Carrier).toBeNull();
            expect(result.Driver).not.toBeNull();
            expect(result.Rides).toBeNull();
        });

    });

    it("VehiclesService_Test_Call_Update_Methods_Should_Update", function () {

        var asyncCallComplete = false, result = null, _this = this;

        var vehicle = {Vehicle:2, CarrierId:2, LicensePlate:'NewName', Make: 'NewMake', Model: 'NewModel'};

        runs(function() {
            return $.ajax('http://localhost:5000/vehicles/put', {
                type: 'PUT',
                data: JSON.stringify(vehicle),
                contentType:"application/json",
                dataType: 'json',
                success: function(data) {
                    asyncCallComplete = true;
                    result = data;
                },
                error: function(xhr, ajaxOptions, thrownError) {
                    asyncCallComplete = true;
                }
            });
        });

        // Wait for the asynchronous call to complete
        waitsFor(function() {
            return asyncCallComplete !== false;
        });



    });

    it("VehiclesService_Test_Call_GetByPrice_Should_Return_Values", function () {

        var asyncCallComplete = false, result = null, _this = this;

        runs(function() {

            var date = new Date();
            return $.ajax('http://localhost:5000/vehicles/price?latitude=40.701999&longitude=-74.015794&count=1', {
                type: 'GET',
                contentType:"application/json",
                dataType: "json",
                success: function(data) {
                    asyncCallComplete = true;
                    result = data;
                },
                error: function(xhr, ajaxOptions, thrownError) {
                    asyncCallComplete = true;
                }
            });
        });

        waitsFor(function() {
            return asyncCallComplete !== false;
        });

        return runs(function() {
            expect(result).not.toBeNull();

            expect(result[0].VehicleId).toBeDefined();
            expect(result[0].LicensePlate).toBeDefined();
            expect(result[0].Model).toBeDefined();
            expect(result[0].Picture).toBeDefined();
            expect(result[0].Make).toBeDefined();
            expect(result[0].Type).toBeDefined();
            expect(result[0].Seats).toBeDefined();
            expect(result[0].Latitude).toBeDefined();
            expect(result[0].Longitude).toBeDefined();
            expect(result[0].Latitude).toBeDefined();
            expect(result[0].DistanceFromGivenPosition).toBeDefined();
            expect(result[0].VehicleStatus).toBeDefined();
            expect(result[0].CarrierId).toBeDefined();
            expect(result[0].DriverId).toBeDefined();
            expect(result[0].Rate).toBeDefined();
            expect(result[0].RatingAvg).toBeDefined();
            expect(result[0].TotalRides).toBeDefined();

            expect(result[0].Carrier).toBeNull();
            expect(result[0].Driver).toBeNull();
            expect(result[0].Rides).toBeNull();
        });

    });

    it("VehiclesService_Test_Call_GetByDistance_Should_Return_Values", function () {

        var asyncCallComplete = false, result = null, _this = this;

        runs(function() {

            var date = new Date();
            return $.ajax('http://localhost:5000/vehicles/distance?latitude=40.701999&longitude=-74.015794&count=1', {
                type: 'GET',
                contentType:"application/json",
                dataType: "json",
                success: function(data) {
                    asyncCallComplete = true;
                    result = data;
                },
                error: function(xhr, ajaxOptions, thrownError) {
                    asyncCallComplete = true;
                }
            });
        });

        waitsFor(function() {
            return asyncCallComplete !== false;
        });

        return runs(function() {
            expect(result).not.toBeNull();

            expect(result[0].VehicleId).toBeDefined();
            expect(result[0].LicensePlate).toBeDefined();
            expect(result[0].Model).toBeDefined();
            expect(result[0].Picture).toBeDefined();
            expect(result[0].Make).toBeDefined();
            expect(result[0].Type).toBeDefined();
            expect(result[0].Seats).toBeDefined();
            expect(result[0].Latitude).toBeDefined();
            expect(result[0].Longitude).toBeDefined();
            expect(result[0].Latitude).toBeDefined();
            expect(result[0].DistanceFromGivenPosition).toBeDefined();
            expect(result[0].VehicleStatus).toBeDefined();
            expect(result[0].CarrierId).toBeDefined();
            expect(result[0].DriverId).toBeDefined();
            expect(result[0].Rate).toBeDefined();
            expect(result[0].RatingAvg).toBeDefined();
            expect(result[0].TotalRides).toBeDefined();

            expect(result[0].Carrier).not.toBeNull();
            expect(result[0].Carrier.Picture).toBeDefined();
            expect(result[0].Driver).toBeNull();
            expect(result[0].Rides).toBeNull();
        });

    });

    it("VehiclesService_Test_Call_Get_List_Should_Return_Vehicles", function () {

        var asyncCallComplete = false, result = null, _this = this;

        runs(function() {
            return $.ajax('http://localhost:5000/vehicles?filter=&pageSize=1&pageCount=0', {
                type: 'GET',
                contentType:"application/json",
                dataType: "json",
                success: function(data) {
                    asyncCallComplete = true;
                    result = data;
                },
                error: function(xhr, ajaxOptions, thrownError) {
                    asyncCallComplete = true;
                }
            });
        });

        // Wait for the asynchronous call to complete
        waitsFor(function() {
            return asyncCallComplete !== false;
        });

        // Asserts
        return runs(function() {
            expect(result).not.toBeNull();

            expect(result[0].VehicleId).toBeDefined();
            expect(result[0].LicensePlate).toBeDefined();
            expect(result[0].Model).toBeDefined();
            expect(result[0].Picture).toBeDefined();
            expect(result[0].Make).toBeDefined();
            expect(result[0].Type).toBeDefined();
            expect(result[0].Seats).toBeDefined();
            expect(result[0].Latitude).toBeDefined();
            expect(result[0].Longitude).toBeDefined();
            expect(result[0].Latitude).toBeDefined();
            expect(result[0].DistanceFromGivenPosition).toBeDefined();
            expect(result[0].VehicleStatus).toBeDefined();
            expect(result[0].CarrierId).toBeDefined();
            expect(result[0].DriverId).toBeDefined();
            expect(result[0].Rate).toBeDefined();
            expect(result[0].RatingAvg).toBeDefined();
            expect(result[0].TotalRides).toBeDefined();

            expect(result[0].Carrier).toBeNull();
            expect(result[0].Driver).toBeNull();
            expect(result[0].Rides).toBeNull();
        });

    });

    it("VehiclesService_Test_Call_Get_Count_Should_Return_Count", function () {
        var asyncCallComplete = false, result = null, _this = this;

        runs(function() {
            return $.ajax('http://localhost:5000/vehicles/count?filter=', {
                type: 'GET',
                contentType:"application/json",
                dataType: "json",
                success: function(data) {
                    asyncCallComplete = true;
                    result = data;
                },
                error: function(xhr, ajaxOptions, thrownError) {
                    asyncCallComplete = true;
                }
            });
        });

        // Wait for the asynchronous call to complete
        waitsFor(function() {
            return asyncCallComplete !== false;
        });

        // Asserts
        return runs(function() {
            expect(result).not.toBeNull();
        });
    });

});
