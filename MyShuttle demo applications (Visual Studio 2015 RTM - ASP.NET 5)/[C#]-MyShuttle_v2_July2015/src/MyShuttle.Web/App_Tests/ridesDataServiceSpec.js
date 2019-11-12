'use strict';

describe("rides data service test", function () {

    beforeEach(function () {

    });

    it("RidesService_Test_Call_GetCount_Should_Return_Value", function () {

          var asyncCallComplete = false, result = null, _this = this;

          runs(function() {
              return $.ajax('http://localhost:5000/rides/count?&driverId=&vehicleId=', {
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
              expect(result > 0).toBeTruthy();
          });

    });

    it("RidesService_Test_Call_GetList_Should_Return_Values", function () {

        var asyncCallComplete = false, result = null, _this = this;

        runs(function() {

            return $.ajax('http://localhost:5000/rides?&driverId=&vehicleId=&pageSize=1&pageCount=0', {
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

            expect(result[0].RideId).toBeDefined();
            expect(result[0].StartDateTime).toBeDefined();
            expect(result[0].EndDateTime).toBeDefined();
            expect(result[0].StartLatitude).toBeDefined();
            expect(result[0].StartLongitude).toBeDefined();
            expect(result[0].EndLatitude).toBeDefined();
            expect(result[0].EndLongitude).toBeDefined();
            expect(result[0].StartAddress).toBeDefined();
            expect(result[0].EndAddress).toBeDefined();
            expect(result[0].Distance).toBeDefined();
            expect(result[0].Duration).toBeDefined();
            expect(result[0].Cost).toBeDefined();
            expect(result[0].Signature).toBeNull();
            expect(result[0].Rating).toBeDefined();
            expect(result[0].Comments).toBeDefined();
            expect(result[0].VehicleId).toBeDefined();
            expect(result[0].CarrierId).toBeDefined();
            expect(result[0].DriverId).toBeDefined();
            expect(result[0].EmployeeId).toBeDefined();

            expect(result[0].Vehicle).not.toBeNull();
            expect(result[0].Driver).not.toBeNull();
            expect(result[0].Employee).toBeNull();
        });

    });

    it("RidesService_Test_Call_Get_Should_Return_One_Ride", function () {

        var asyncCallComplete = false, result = null, _this = this;
        runs(function() {

            var date = new Date();
            return $.ajax('http://localhost:5000/rides/1', {
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

            expect(result.RideId).toBeDefined();
            expect(result.StartDateTime).toBeDefined();
            expect(result.EndDateTime).toBeDefined();
            expect(result.StartLatitude).toBeDefined();
            expect(result.StartLongitude).toBeDefined();
            expect(result.EndLatitude).toBeDefined();
            expect(result.EndLongitude).toBeDefined();
            expect(result.StartAddress).toBeDefined();
            expect(result.EndAddress).toBeDefined();
            expect(result.Distance).toBeDefined();
            expect(result.Duration).toBeDefined();
            expect(result.Cost).toBeDefined();
            expect(result.Signature).toBeNull();
            expect(result.Rating).toBeDefined();
            expect(result.Comments).toBeDefined();
            expect(result.VehicleId).toBeDefined();
            expect(result.CarrierId).toBeDefined();
            expect(result.DriverId).toBeDefined();
            expect(result.EmployeeId).toBeDefined();

            expect(result.Vehicle).not.toBeNull();
            expect(result.Vehicle.Picture).toBeDefined();
            expect(result.Driver).not.toBeNull();
            expect(result.Employee).not.toBeNull();
            expect(result.Employee.Picture).toBeDefined();
        });
    });

    it("RidesService_Test_Call_GetMyRides_Should_Return_One_Value", function () {

        var asyncCallComplete = false, result = null, _this = this;

        runs(function() {

            var date = new Date();
            return $.ajax('http://localhost:5000/rides/myrides?count=1', {
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

            expect(result[0].RideId).toBeDefined();
            expect(result[0].StartDateTime).toBeDefined();
            expect(result[0].EndDateTime).toBeDefined();
            expect(result[0].StartLatitude).toBeDefined();
            expect(result[0].StartLongitude).toBeDefined();
            expect(result[0].EndLatitude).toBeDefined();
            expect(result[0].EndLongitude).toBeDefined();
            expect(result[0].StartAddress).toBeDefined();
            expect(result[0].EndAddress).toBeDefined();
            expect(result[0].Distance).toBeDefined();
            expect(result[0].Duration).toBeDefined();
            expect(result[0].Cost).toBeDefined();
            expect(result[0].Signature).toBeNull();
            expect(result[0].Rating).toBeDefined();
            expect(result[0].Comments).toBeDefined();
            expect(result[0].VehicleId).toBeDefined();
            expect(result[0].CarrierId).toBeDefined();
            expect(result[0].DriverId).toBeDefined();
            expect(result[0].EmployeeId).toBeDefined();

            expect(result[0].Vehicle).toBeNull();
            expect(result[0].Driver).not.toBeNull();
            expect(result[0].Employee).toBeNull();
        });

    });

    it("RidesService_Test_Call_GetMyCompanyRides_Should_Return_One_Value", function () {

        var asyncCallComplete = false, result = null, _this = this;

        runs(function () {

            var date = new Date();
            return $.ajax('http://localhost:5000/rides/mycompanyrides?count=1', {
                type: 'GET',
                contentType: "application/json",
                dataType: "json",
                success: function (data) {
                    asyncCallComplete = true;
                    result = data;
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    asyncCallComplete = true;
                }
            });
        });

        waitsFor(function () {
            return asyncCallComplete !== false;
        });

        return runs(function () {
            expect(result).not.toBeNull();

            expect(result[0].RideId).toBeDefined();
            expect(result[0].StartDateTime).toBeDefined();
            expect(result[0].EndDateTime).toBeDefined();
            expect(result[0].StartLatitude).toBeDefined();
            expect(result[0].StartLongitude).toBeDefined();
            expect(result[0].EndLatitude).toBeDefined();
            expect(result[0].EndLongitude).toBeDefined();
            expect(result[0].StartAddress).toBeDefined();
            expect(result[0].EndAddress).toBeDefined();
            expect(result[0].Distance).toBeDefined();
            expect(result[0].Duration).toBeDefined();
            expect(result[0].Cost).toBeDefined();
            expect(result[0].Signature).toBeNull();
            expect(result[0].Rating).toBeDefined();
            expect(result[0].Comments).toBeDefined();
            expect(result[0].VehicleId).toBeDefined();
            expect(result[0].CarrierId).toBeDefined();
            expect(result[0].DriverId).toBeDefined();
            expect(result[0].EmployeeId).toBeDefined();

            expect(result[0].Vehicle).toBeNull();
            expect(result[0].Driver).not.toBeNull();
            expect(result[0].Employee).not.toBeNull();
            expect(result[0].Employee.Picture).toBeDefined();
        });

    });

    it("RidesService_Test_Call_Update_Should_Be_Updated", function () {


        var asyncCallComplete, result, _this = this, ride = null;
        asyncCallComplete = false;
        result = null;

        runs(function() {

            var date = new Date();
            return $.ajax('http://localhost:5000/rides/1', {
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
            var ride = result;
            ride.Rate = 10;
            asyncCallComplete = false;
            result = null;

            runs(function() {
                return $.ajax('http://localhost:5000/rides/put', {
                    type: 'PUT',
                    data: JSON.stringify(ride),
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



});
