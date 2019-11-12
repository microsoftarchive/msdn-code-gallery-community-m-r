'use strict';

describe("carriers data service test", function () {

    beforeEach(function () {

    });

    it("CarriersService_Test_Call_Add_Should_Add_New_Carrier", function () {
        var asyncCallComplete = false, result = null, _this = this;

        // Add new carrier
        var carrier = {Name:'Name',Description:'Description'};

        runs(function() {
            return $.ajax('http://localhost:5000/carriers/post', {
                type: 'POST',
                data: JSON.stringify(carrier),
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

    it("CarriersService_Test_Call_Get_Should_Get_One_Carrier", function () {
        var asyncCallComplete = false, result = null, _this = this;

        runs(function() {
            return $.ajax('http://localhost:5000/carriers', {
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
            expect(result.Name).toBeDefined();
            expect(result.Description).toBeDefined();
            expect(result.Picture).toBeDefined();
            expect(result.CarrierId).toBeDefined();
            expect(result.CompanyID).toBeDefined();
            expect(result.Address).toBeDefined();
            expect(result.ZipCode).toBeDefined();
            expect(result.City).toBeDefined();
            expect(result.State).toBeDefined();
            expect(result.Country).toBeDefined();
            expect(result.Phone).toBeDefined();
            expect(result.Email).toBeDefined();

            expect(result.Vehicles).toBeNull();
            expect(result.Drivers).toBeNull();


        });

    });

    it("CarriersService_Test_Call_Update_Methods_Should_Update", function () {

        var asyncCallComplete = false, result = null, _this = this;

        var carrier = {CarrierId:2, Name:'NewName',Description:'Description'};

        runs(function() {
            return $.ajax('http://localhost:5000/carriers/put', {
                type: 'PUT',
                data: JSON.stringify(carrier),
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

});
