function bindMonthExpenses(element, isAppInSnappedMode, data) {
    if (data && data.expensesByMonth) {
        var now = moment().add('months', 1);

        if (data.expenseType == MyCompany.Expenses.Enums.expenseType.all) {
            maxMonthExpenseAllExpenseTypes = data.expensesByMonth.length == 0 ? 0 : Enumerable.From(data.expensesByMonth).Select(function (x) { return x.Amount }).Max();
        }

        var monthTemplate = document.querySelector("#expendingsMonthTemplate");
        var monthsContainers = document.querySelectorAll(".months .month");
        for (var i = 0; i < monthsContainers.length; i++) {
            WinJS.Utilities.empty(monthsContainers[i]);
        }

        for (var monthDiff = 0; monthDiff < 12; monthDiff++) {
            var month = now.subtract('months', 1);
            var monthNumber = month.format("M");
            var amount = Enumerable.From(data.expensesByMonth)
                            .Where(function (x) { return moment(x.Date).format("M") == monthNumber })
                            .Select(function (x) { return x.Amount })
                            .FirstOrDefault();

            var monthExpense = { year: month.format("YYYY"), monthName: month.format("MMMM").toUpperCase(), monthNumber: monthNumber, amount: amount ? amount : 0, maxMonthExpense: maxMonthExpenseAllExpenseTypes };


            var monthElement = document.querySelector("#month" + monthDiff);
            monthTemplate.winControl.render(monthExpense, monthElement);
        }

        WinJS.Binding.processAll(document.querySelector("#button-bar"), {
            expenseType: data.expenseType
        });

    }
}


function loadMonthExpenses(expenseType) {
    MyCompany.Expenses.Services.ExpensesService.getTeamExpensesByMonth(expenseType).then(function (data) {
        bindMonthExpenses(document, Windows.UI.ViewManagement.ApplicationView.value, { expensesByMonth: data, expenseType: expenseType });
    });
}

function loadTeamMemberExpensesByMonth(employeeId, expenseType) {
    MyCompany.Expenses.Services.ExpensesService.getTeamMemberExpensesByMonth(employeeId, expenseType).then(function (data) {
        bindMonthExpenses(document, Windows.UI.ViewManagement.ApplicationView.value, { expensesByMonth: data, expenseType: expenseType });
    });
}