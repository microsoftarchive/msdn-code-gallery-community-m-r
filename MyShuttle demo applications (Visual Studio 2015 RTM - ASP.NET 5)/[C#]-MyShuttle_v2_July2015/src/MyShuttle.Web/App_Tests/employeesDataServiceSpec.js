'use strict';

describe("employees data service test", function () {

    beforeEach(function () {

    });

    it("EmployeesService_Test_Call_GetMyProfile_Should_Get_Employee", function () {

        var asyncCallComplete = false, result = null, _this = this;

        runs(function() {
            return $.ajax('http://localhost:5000/employees/myprofile', {
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
            expect(result.EmployeeId).toBeDefined();
            expect(result.CustomerId).toBeDefined();
            expect(result.Name).toBeDefined();
            expect(result.Email).toBeDefined();
            expect(result.Picture).not.toBeNull();
            expect(result.Customer).toBeNull();
        });

    });


});
