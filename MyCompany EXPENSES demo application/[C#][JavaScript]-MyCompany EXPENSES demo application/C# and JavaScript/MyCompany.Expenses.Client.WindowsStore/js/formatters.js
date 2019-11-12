(function () {
    "use strict";

    var formatDate = WinJS.Binding.converter(function (date) {
        var utcDate = moment.utc(date);
        return utcDate.format("MM/DD/YYYY");
    });

    var getDecimalSeparator = function () {
        var n = 1.1;
        n = n.toLocaleString().substring(1, 2);
        return n;
    };

    var formatDuration =  WinJS.Binding.converter(function (date) {
        var utcDate = moment.utc(date);
        var diff = moment.duration(moment().diff(utcDate));
        var days = diff.days();
        var hours = diff.hours();
        var minutes = diff.minutes();

        var value = "";
        if (days > 0)
            value = days + " days ";
        else if (hours > 0)
            value = hours + " hours ";
        else
            value = minutes + " minutes";

        return value;
    });

    var formatExpenseStatus = WinJS.Binding.converter(function (status) {
        return "status status" + status;
    });

    var formatExpenseStatusText = WinJS.Binding.converter(function (status) {
        var statusText = "";
        if (status == MyCompany.Expenses.Enums.expenseStatus.approved)
            statusText = "approved";
        if (status == MyCompany.Expenses.Enums.expenseStatus.denied)
            statusText = "denied";
        if (status == MyCompany.Expenses.Enums.expenseStatus.pending)
            statusText = "pending";

        return statusText;
    });
   
    var formatExpenseTypeSmall = WinJS.Binding.converter(function (status) {
        var img = "";
        if (status == MyCompany.Expenses.Enums.expenseType.accommodation)
            img = "url('/images/expenseTypeAccommodationSmall.png')";
        if (status == MyCompany.Expenses.Enums.expenseType.food)
            img = "url('/images/expenseTypeFoodSmall.png')";
        if (status == MyCompany.Expenses.Enums.expenseType.travel)
            img = "url('/images/expenseTypeTravelSmall.png')";
        if (status == MyCompany.Expenses.Enums.expenseType.other)
            img = "url('/images/expenseTypeOthersSmall.png')";

        return img;
    });

    var formatExpenseType = WinJS.Binding.converter(function (status) {
        var img = "";
        if (status == MyCompany.Expenses.Enums.expenseType.accommodation)
            img = "url('/images/expenseTypeAccommodation.png')";
        if (status == MyCompany.Expenses.Enums.expenseType.food)
            img = "url('/images/expenseTypeFood.png')";
        if (status == MyCompany.Expenses.Enums.expenseType.travel)
            img = "url('/images/expenseTypeTravel.png')";
        if (status == MyCompany.Expenses.Enums.expenseType.other)
            img = "url('/images/expenseTypeOthers.png')";

        return img;
    });



    var formatAmountDollars = WinJS.Binding.converter(function (amount) {
        var amountString = amount.toLocaleString();
        var decimalSeparatorPosition = amountString.indexOf(getDecimalSeparator());
        if ( decimalSeparatorPosition > 0 )
        amountString = amountString.substring(0, decimalSeparatorPosition);

        return amountString;
    });

    var formatNumber = WinJS.Binding.converter(function (number) {
        return number.toLocaleString();
    });

    var formatAmountCents = WinJS.Binding.converter(function (amount) {
        var decimalPart = Math.round((amount % 1) * 100);
        var decimalSeparator = getDecimalSeparator();
        if (decimalPart < 10)
            return decimalSeparator + "0" + decimalPart;
        else
            return decimalSeparator + decimalPart;

    });

    var formatImage = WinJS.Binding.converter(function (image) {
        return "data:image/jpg;base64," + image;
    });

    var formatBackgroundImage = WinJS.Binding.converter(function (image) {
        return "url(data:image/jpg;base64," + image + ")";
    });

    var groupHeaderClassFormatter = WinJS.Binding.converter(function (groupType) {
        return "groupHeader_" + groupType;
    });

    var amountAsWidthFormatter = WinJS.Binding.converter(function (monthExpense) {
        if (monthExpense.maxMonthExpense == 0)
            return "0%";

        var percent = 16 + (monthExpense.amount * 64 / monthExpense.maxMonthExpense);
        return Math.floor(percent) + "%";
    });

    var formatExpenseTypeButton = WinJS.Binding.converter(function (currentExpenseType) {
        return "expenses" + currentExpenseType;
    });

    var formatMonthBar = WinJS.Binding.converter(function (year) {
        var currentYear = new moment().format("YYYY");
        if (currentYear == year) {
            return "monthBar";
        }
        else {
            return "monthBar pastYear";
        }
    });

    var formatMonthName = WinJS.Binding.converter(function (monthNumber) {
        // month list starts in 0 index
        return moment().month(monthNumber-1).format("MMMM");
    });

    var formatPercent = WinJS.Binding.converter(function (number) {
        return (Math.round(number * 10) / 10) + '%';
    });
    


    WinJS.Namespace.define("MyCompany.Expenses.Formaters", {
        formatDate: formatDate,
        formatDuration: formatDuration,
        formatBackgroundImage: formatBackgroundImage,
        formatImage: formatImage,
        formatExpenseStatus: formatExpenseStatus,
        formatExpenseStatusText: formatExpenseStatusText,
        formatExpenseTypeSmall: formatExpenseTypeSmall,
        formatExpenseType: formatExpenseType,
        formatAmountDollars: formatAmountDollars,
        formatAmountCents: formatAmountCents,
        groupHeaderClassFormatter: groupHeaderClassFormatter,
        amountAsWidthFormatter: amountAsWidthFormatter,
        formatExpenseTypeButton: formatExpenseTypeButton,
        formatMonthBar: formatMonthBar,
        formatMonthName: formatMonthName,
        formatPercent: formatPercent,
        formatNumber: formatNumber
    });
})();