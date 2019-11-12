(function () {
    "use strict";

    var pages = {
        login: "/pages/login/login.html",
        home: "/pages/home/home.html",
        settings: "/pages/settings/settings.html",
        expenses: "/pages/expenses/expenses.html",
        expenseDetail: "/pages/expenseDetail/expenseDetail.html",
        employeeExpenses: "/pages/employeeExpenses/employeeExpenses.html"
    };

    var pictureType = {
        unknown: 0,
        small: 1,
        big: 2
    };

    var expenseStatus = {
        unknown: 0,
        pending: 1,
        approved: 2,
        denied: 4
    };
    expenseStatus.all = expenseStatus.pending + expenseStatus.approved + expenseStatus.denied;

    var expenseType = {
        unknown: 0,
        travel: 1,
        food: 2,
        accommodation: 4,
        other: 8,
    };
    expenseType.all = expenseType.travel + expenseType.food + expenseType.accommodation + expenseType.other;

    var expensesGroupType = {
        single: 0,
        total: 1
    };

    var errorType = {
        expiredToken: "token expired"
    };

    var images = {
        expenseTile: "ms-appx:///images/assets/Logo150x150 White Background.png",
        expenseTileWide: "ms-appx:///images/Assets/Wide Logo 310x150 White Background.png",
    };

    var tileType = {
        expense: 0
    };

    var notificationType = {
        windowsStoreNotification: 0,
        windowsPhoneNotification: 1
    };


    WinJS.Namespace.define("MyCompany.Expenses.Enums", {
        errorType: errorType,
        pages: pages,
        pictureType: pictureType,
        expenseStatus: expenseStatus,
        expenseType: expenseType,
        expensesGroupType: expensesGroupType,
        images: images,
        tileType: tileType,
        notificationType: notificationType
    });
})();
