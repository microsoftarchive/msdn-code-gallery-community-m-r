(function () {
    "use strict";

    var get = function (employeeId, pictureType) {
        var url = Windows.Foundation.Uri(MyCompany.Expenses.Config.getApiUrl(), "api/employees/employeeId/pictureType").rawUri;
        url = url.replace("employeeId", employeeId);
        url = url.replace("pictureType", pictureType);
        return MyCompany.Expenses.Services.doGetAsync(url);
    };

    WinJS.Namespace.define("MyCompany.Expenses.Services.EmployeesService", {
        get: get
    });
})();