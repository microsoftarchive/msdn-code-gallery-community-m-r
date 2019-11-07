(function () {
    "use strict";

    var getTeamExpensesByMember = function (pictureType) {
        var url = Windows.Foundation.Uri(MyCompany.Expenses.Config.getApiUrl(), "api/expenses/teammembers/pictureType").rawUri;
        url = url.replace("pictureType", pictureType);
        return MyCompany.Expenses.Services.doGetAsync(url);
    };

    var getTeamExpenses = function (expenseStatus, pictureType, number) {
        var url = Windows.Foundation.Uri(MyCompany.Expenses.Config.getApiUrl(), "api/expenses/team?expenseStatus={expenseStatus}&pictureType={pictureType}&pageSize={number}&pageCount=0").rawUri;
        url = url.replace("{expenseStatus}", expenseStatus);
        url = url.replace("{pictureType}", pictureType);
        url = url.replace("{number}", number);
        return MyCompany.Expenses.Services.doGetAsync(url);
    };

    var getTeamCount = function (expenseStatus) {
        var url = Windows.Foundation.Uri(MyCompany.Expenses.Config.getApiUrl(), "api/expenses/team/expenseStatus/count").rawUri;
        url = url.replace("expenseStatus", expenseStatus);
        return MyCompany.Expenses.Services.doGetAsync(url);
    };

    var getTeamExpensesByMemberCount = function () {
        var url = Windows.Foundation.Uri(MyCompany.Expenses.Config.getApiUrl(), "api/expenses/teammembers/count").rawUri;
        return MyCompany.Expenses.Services.doGetAsync(url);
    };

    var getExpense = function (expenseId, pictureType) {
        var url = Windows.Foundation.Uri(MyCompany.Expenses.Config.getApiUrl(), "api/expenses/expenseId/pictureType").rawUri;
        url = url.replace("expenseId", expenseId);
        url = url.replace("pictureType", pictureType);
        return MyCompany.Expenses.Services.doGetAsync(url);
    };

    var updateStatus = function (expenseId, status) {
        var url = Windows.Foundation.Uri(MyCompany.Expenses.Config.getApiUrl(), "api/expenses?expenseId={expenseId}&status={status}").rawUri;
        url = url.replace("{expenseId}", expenseId);
        url = url.replace("{status}", status);
        MyCompany.Expenses.Services.cleanCache();
        return MyCompany.Expenses.Services.doPutAsync(url);
    };

    var getTeamExpensesByMonth = function (expenseType) {
        var url = Windows.Foundation.Uri(MyCompany.Expenses.Config.getApiUrl(), "api/expenses/team/month/expenseType").rawUri;
        url = url.replace("expenseType", expenseType);
        return MyCompany.Expenses.Services.doGetAsync(url);
    };

    var getTeamMemberExpensesByMonth = function (employeeId, expenseType) {
        var url = Windows.Foundation.Uri(MyCompany.Expenses.Config.getApiUrl(), "api/expenses/teammember/employeeId/month/expenseType").rawUri;
        url = url.replace("employeeId", employeeId);
        url = url.replace("expenseType", expenseType);
        return MyCompany.Expenses.Services.doGetAsync(url);
    };

    var getTeamMemberSummaryExpenses = function (employeeId, year, month) {
        var url = Windows.Foundation.Uri(MyCompany.Expenses.Config.getApiUrl(), "api/expenses/teammember/employeeId/summary/year/month").rawUri;
        url = url.replace("employeeId", employeeId);
        url = url.replace("year", year);
        url = url.replace("month", month ? month : "");
        return MyCompany.Expenses.Services.doGetAsync(url);
    };

    WinJS.Namespace.define("MyCompany.Expenses.Services.ExpensesService", {
        getTeamExpensesByMember: getTeamExpensesByMember,
        getTeamExpenses: getTeamExpenses,
        getTeamCount: getTeamCount,
        getTeamExpensesByMemberCount: getTeamExpensesByMemberCount,
        getExpense: getExpense,
        updateStatus: updateStatus,
        getTeamExpensesByMonth: getTeamExpensesByMonth,
        getTeamMemberExpensesByMonth: getTeamMemberExpensesByMonth,
        getTeamMemberSummaryExpenses: getTeamMemberSummaryExpenses
    });
})();