(function () {
    "use strict";

    var nav = WinJS.Navigation;
    var ui = WinJS.UI;
    var expense;

    function bindData(employee, monthExpenses, currentYearExpenses, lastYearExpenses, expensesByMonth, expenseType, updateLayout) {
        var data = { employee: employee, monthExpenses: monthExpenses, currentYearExpenses: currentYearExpenses, lastYearExpenses: lastYearExpenses, expenseType: expenseType, expensesByMonth: expensesByMonth }
        updateLayout(document, Windows.UI.ViewManagement.ApplicationView.value, data);
    }

    function setEmployeePicture(employee) {
        if (employee.EmployeePictures && employee.EmployeePictures.length > 0) {
            employee.Picture = employee.EmployeePictures[0];
        }
        else {
            employee.Picture = null;
        }
    }

    function bindUserData() {
        var userData = {
            image: (MyCompany.Expenses.Context.currentUser.EmployeePictures && MyCompany.Expenses.Context.currentUser.EmployeePictures.length > 0) ? MyCompany.Expenses.Context.currentUser.EmployeePictures[0].Content : null,
            name: MyCompany.Expenses.Context.currentUser.FirstName + " " + MyCompany.Expenses.Context.currentUser.LastName
        };

        WinJS.Binding.processAll(document.querySelector(".user-data"), {
            user: userData
        });
    }

    function bindEmployee(data) {
        if (data && data.employee) {
            WinJS.Binding.processAll(document.querySelector(".employee.name"), data);
            WinJS.Binding.processAll(document.querySelector("#employee-photo"), data);
        }
    }

    function bindTimeExpenses(data) {
        if (data && data.monthExpenses) {
            WinJS.Binding.processAll(document.querySelector(".employee-expenses-amount"), data);
            WinJS.Binding.processAll(document.querySelector("#timeExpendings"), data);
        }
    }

    function summarizeTimeExpenses(timeExpensesList, month, year) {
        var timeExpenses = { month: month, year: year, food: 0, accommodation: 0, travel: 0, others: 0, total: 0 };

        if (timeExpensesList.length > 0) {
            timeExpenses.total = Enumerable.From(timeExpensesList)
                .Select(function (x) { return x.Amount })
                .Sum();


            if (timeExpenses.total > 0) {
                timeExpensesList.forEach(function (element, index, array) {
                    switch (element.ExpenseType) {
                        case MyCompany.Expenses.Enums.expenseType.food:
                            timeExpenses.food = 100 * element.Amount / timeExpenses.total;
                            break;
                        case MyCompany.Expenses.Enums.expenseType.accommodation:
                            timeExpenses.accommodation = 100 * element.Amount / timeExpenses.total;
                            break;
                        case MyCompany.Expenses.Enums.expenseType.travel:
                            timeExpenses.travel = 100 * element.Amount / timeExpenses.total;
                            break;
                        case MyCompany.Expenses.Enums.expenseType.other:
                            timeExpenses.others = 100 * element.Amount / timeExpenses.total;
                            break;

                    }
                });
            }
        }
        return timeExpenses;
    }

    function initMonthList(element, employeeId) {
        var foodButton = element.querySelector("#foodExpensesButton");
        foodButton.onclick = function () { loadTeamMemberExpensesByMonth(employeeId, MyCompany.Expenses.Enums.expenseType.food) };

        var travelButton = element.querySelector("#travelExpensesButton");
        travelButton.onclick = function () { loadTeamMemberExpensesByMonth(employeeId, MyCompany.Expenses.Enums.expenseType.travel) };

        var accommodationButton = element.querySelector("#accommodationExpensesButton");
        accommodationButton.onclick = function () { loadTeamMemberExpensesByMonth(employeeId, MyCompany.Expenses.Enums.expenseType.accommodation) };

        var otherButton = element.querySelector("#otherExpensesButton");
        otherButton.onclick = function () { loadTeamMemberExpensesByMonth(employeeId, MyCompany.Expenses.Enums.expenseType.other) };

        var allButton = element.querySelector("#allExpensesButton");
        allButton.onclick = function () { loadTeamMemberExpensesByMonth(employeeId, MyCompany.Expenses.Enums.expenseType.all) };

    }

    function prepareAndBindData(results, updateLayout, currentMonth, currentYear, lastYear) {
        var employee = results[0];
        setEmployeePicture(employee);
        var monthExpenses = summarizeTimeExpenses(results[1], currentMonth, currentYear);
        var currentYearExpenses = summarizeTimeExpenses(results[2], null, currentYear);
        var lastYearExpenses = summarizeTimeExpenses(results[3], null, lastYear);
        var expensesByMonth = results[4];

        bindData(employee, monthExpenses, currentYearExpenses, lastYearExpenses, expensesByMonth, MyCompany.Expenses.Enums.expenseType.all, updateLayout);
    }

    ui.Pages.define(MyCompany.Expenses.Enums.pages.employeeExpenses, {
        updateLayout: function (element, viewState, data) {
            bindEmployee(data);
            bindTimeExpenses(data);
            bindMonthExpenses(element, viewState, data);
            bindUserData();
        },
        loadData: function (employeeId) {
            var self = this;
            var allStatus = MyCompany.Expenses.Enums.expenseStatus.all;
            var promises = [];
            var currentYear = moment().format("YYYY");
            var currentMonth = moment().format("M");
            var lastYear = currentYear - 1;

            if (Windows.ApplicationModel.DesignMode.designModeEnabled) {
                var results = new Array();
                results[0] = MyCompany.Expenses.Data.Fake.getEmployee();
                results[1] = MyCompany.Expenses.Data.Fake.getTeamMemberSummaryExpenses();
                results[2] = MyCompany.Expenses.Data.Fake.getTeamMemberSummaryExpenses();
                results[3] = MyCompany.Expenses.Data.Fake.getTeamMemberSummaryExpenses();
                results[4] = MyCompany.Expenses.Data.Fake.getTeamExpensesByMonth();

                prepareAndBindData(results, self.updateLayout, currentMonth, currentYear, lastYear);
                return;
            }

            promises.push(MyCompany.Expenses.Services.EmployeesService.get(employeeId, MyCompany.Expenses.Enums.pictureType.big));
            promises.push(MyCompany.Expenses.Services.ExpensesService.getTeamMemberSummaryExpenses(employeeId, currentYear, currentMonth));
            promises.push(MyCompany.Expenses.Services.ExpensesService.getTeamMemberSummaryExpenses(employeeId, currentYear));
            promises.push(MyCompany.Expenses.Services.ExpensesService.getTeamMemberSummaryExpenses(employeeId, lastYear));
            promises.push(MyCompany.Expenses.Services.ExpensesService.getTeamMemberExpensesByMonth(employeeId, MyCompany.Expenses.Enums.expenseType.all));

            MyCompany.Expenses.Application.showLoading();
            WinJS.Promise.join(promises).then(function (results) {
                prepareAndBindData(results, self.updateLayout, currentMonth, currentYear, lastYear);
                MyCompany.Expenses.Application.hideLoading();
            });
        },
        ready: function (element, options) {
            if (Windows.ApplicationModel.DesignMode.designModeEnabled) {
                options = { employeeId : -1 };
            }

            this.loadData(options.employeeId);

            initMonthList(element, options.employeeId);
        }
    });
})();