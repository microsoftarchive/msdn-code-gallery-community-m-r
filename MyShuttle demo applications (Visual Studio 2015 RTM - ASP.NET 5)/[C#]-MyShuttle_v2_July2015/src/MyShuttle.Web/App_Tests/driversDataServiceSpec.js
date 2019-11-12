'use strict';

describe("drivers data service test", function () {

    beforeEach(function () {

    });

    it("DriversService_Test_Call_Add_Should_Add_New_Driver", function () {
        var asyncCallComplete = false, result = null, _this = this;

        var driver = {CarrierId:1,Name:'Name'};

        runs(function() {
            return $.ajax('http://localhost:5000/drivers/post', {
                type: 'POST',
                data: JSON.stringify(driver),
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

    it("DriversService_Test_Call_Get_Should_Get_One_Driver", function () {
        var asyncCallComplete = false, result = null, _this = this;

        runs(function() {
            return $.ajax('http://localhost:5000/drivers/1', {
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

            expect(result.DriverId).toBeDefined();
            expect(result.Name).toBeDefined();
            expect(result.Phone).toBeDefined();
            expect(result.Picture).toBeDefined();
            expect(result.CarrierId).toBeDefined();
            expect(result.VehicleId).toBeDefined();
            expect(result.RatingAvg).toBeDefined();
            expect(result.TotalRides).toBeDefined();

            expect(result.Carrier).toBeNull();
            expect(result.Rides).toBeNull();

        });

    });

    it("DriversService_Test_Call_Update_Methods_Should_Update", function () {

        var asyncCallComplete = false, result = null, _this = this;

        var driver = {DriverId:5, CarrierId:2,Name:'Name'};

        runs(function() {
            return $.ajax('http://localhost:5000/drivers/put', {
                type: 'PUT',
                data: JSON.stringify(driver),
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

        waitsFor(function() {
            return asyncCallComplete !== false;
        });
    });

    it("DriversService_Test_Call_Get_List_Should_Return_Drivers", function () {

        var asyncCallComplete = false, result = null, _this = this;

        runs(function() {
            return $.ajax('http://localhost:5000/drivers?filter=&pageSize=1&pageCount=0', {
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

            expect(result[0].DriverId).toBeDefined();
            expect(result[0].Name).toBeDefined();
            expect(result[0].Phone).toBeDefined();
            expect(result[0].Picture).toBeDefined();
            expect(result[0].CarrierId).toBeDefined();
            expect(result[0].VehicleId).toBeDefined();
            expect(result[0].RatingAvg).toBeDefined();
            expect(result[0].TotalRides).toBeDefined();

            expect(result[0].Carrier).toBeNull();
            expect(result[0].Vehicle).toBeNull();
            expect(result[0].Rides).toBeNull();
        });

    });

    it("DriversService_Test_Call_Get_Count_Should_Return_Value", function () {
        var asyncCallComplete = false, result = null, _this = this;

        runs(function() {
            return $.ajax('http://localhost:5000/drivers/count?filter=', {
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

    it("DriversService_Test_Call_Delete_Should_Delete", function () {
        var asyncCallComplete = false, result = null, _this = this;

        runs(function() {
            return $.ajax('http://localhost:5000/drivers/delete/2', {
                type: 'DELETE',
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

    });
});
