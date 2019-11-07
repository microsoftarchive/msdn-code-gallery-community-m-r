'use strict';

describe("analytics data service test", function () {

    beforeEach(function () {

    });

    it("AnalyticsService_GetTopDrivers_Should_Retrieve_Top_Drivers", function () {

        var asyncCallComplete, result, _this = this;

        // asyncCallComplete is set to true when the ajax call is complete
        asyncCallComplete = false;

        // result stores the result of the successful ajax call
        result = null;

        // SECTION 1 - call asynchronous function
        runs(function() {
            return $.ajax('http://localhost:5000/analytics/topdrivers', {
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

        // SECTION 2 - wait for the asynchronous call to complete
        waitsFor(function() {
            return asyncCallComplete !== false;
        });

        // SECTION 3 - perform tests
        return runs(function() {
            expect(result).not.toBeNull();
            expect(result.length === 5).toBeTruthy();
            expect(result[0].Name).toBeDefined();
            expect(result[0].TotalRides).toBeDefined();
            expect(result[0].RatingAvg).toBeDefined();
            expect(result[0].Picture).not.toBeNull();


            expect(result[0].Carrier).toBeNull();
            expect(result[0].Vehicle).toBeNull();
            expect(result[0].Rides).toBeNull();
        });

    });

    it("AnalyticsService_GetTopVehicles_Should_Retrieve_Top_Vehicles", function () {

        var asyncCallComplete, result, _this = this;

        // asyncCallComplete is set to true when the ajax call is complete
        asyncCallComplete = false;

        // result stores the result of the successful ajax call
        result = null;

        // SECTION 1 - call asynchronous function
        runs(function() {
            return $.ajax('http://localhost:5000/analytics/topvehicles', {
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

        // SECTION 2 - wait for the asynchronous call to complete
        waitsFor(function() {
            return asyncCallComplete !== false;
        });

        // SECTION 3 - perform tests
        return runs(function() {
            expect(result).not.toBeNull();
            expect(result.length === 5).toBeTruthy();
            expect(result[0].LicensePlate).toBeDefined();
            expect(result[0].Model).toBeDefined();
            expect(result[0].TotalRides).toBeDefined();
            expect(result[0].RatingAvg).toBeDefined();
            expect(result[0].Picture).not.toBeNull();
        });

    });

    it("AnalyticsService_GetSummary_Should_Retrieve_Summary", function () {

        var asyncCallComplete, result, _this = this;

        // asyncCallComplete is set to true when the ajax call is complete
        asyncCallComplete = false;

        // result stores the result of the successful ajax call
        result = null;

        // SECTION 1 - call asynchronous function
        runs(function() {
            return $.ajax('http://localhost:5000/analytics/summary', {
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

        // SECTION 2 - wait for the asynchronous call to complete
        waitsFor(function() {
            return asyncCallComplete !== false;
        });

        // SECTION 3 - perform tests
        return runs(function() {
            expect(result).not.toBeNull();
            expect(result.TotalVehicles).toBeDefined();
            expect(result.TotalDrivers).toBeDefined();
            expect(result.TotalPassengers).toBeDefined();
            expect(result.Rating).toBeDefined();
        });

    });

    it("AnalyticsService_GetRidesInfo_Should_Retrieve_Rides_Info", function () {

        var asyncCallComplete, result, _this = this;

        // asyncCallComplete is set to true when the ajax call is complete
        asyncCallComplete = false;

        // result stores the result of the successful ajax call
        result = null;

        // SECTION 1 - call asynchronous function
        runs(function() {
            return $.ajax('http://localhost:5000/analytics/rides', {
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

        // SECTION 2 - wait for the asynchronous call to complete
        waitsFor(function() {
            return asyncCallComplete !== false;
        });

        // SECTION 3 - perform tests
        return runs(function() {
            expect(result).not.toBeNull();
            expect(result.LastDaysRides).toBeDefined();
            expect(result.LastDaysPassengers).toBeDefined();
            expect(result.RidesEvolution).not.toBeNull();
            expect(result.RidesEvolution.Days.length === 30).toBeTruthy();
            expect(result.RidesEvolution.Values.length === 30).toBeTruthy();
        });

    });

});
