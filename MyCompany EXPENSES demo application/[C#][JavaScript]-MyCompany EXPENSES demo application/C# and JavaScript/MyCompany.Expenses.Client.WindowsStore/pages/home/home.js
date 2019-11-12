(function () {
    "use strict";

    var nav = WinJS.Navigation;
    var ui = WinJS.UI;
    var totalTeamExpensesCount;

    var numberOfHighlightedTeamExpenses = 2;
    var numberOfNonHighlightedTeamExpenses = 3;
    var maxMonthExpenseAllExpenseTypes = 0;

    function isSnapped() {
        return document.documentElement.offsetWidth <= 500;
    };

    function bindData(expenses, expensesByMonth, expenseType, updateLayout) {
        var expensesList = new WinJS.Binding.List(expenses);

        var teamExpensesTitle = "Team expenses (" + expenses.totalTeamExpensesCount + ") >";
        var teamExpensesPerPersonTitle = "Total expenses per employee";

        var groupedExpenses = expensesList.createGrouped(
            function (dataItem) {
                return dataItem.type.toString();
            },
            function (dataItem) {
                var title = "";
                if (dataItem.type === MyCompany.Expenses.Enums.expensesGroupType.single) {
                    title = teamExpensesTitle;
                }
                else if (dataItem.type === MyCompany.Expenses.Enums.expensesGroupType.total) {
                    title = teamExpensesPerPersonTitle;
                }
                return { title: title, type: dataItem.type };
            },
            function (group1, group2) {
                return group1 - group2;
            }
        );
        
        updateLayout(document, isSnapped(), { expenseType: expenseType, groupedExpenses: groupedExpenses, totalTeamExpensesCount: expenses.totalTeamExpensesCount, teamExpensesTitle: teamExpensesTitle, teamExpensesPerPersonTitle: teamExpensesPerPersonTitle, expensesByMonth: expensesByMonth });
    };

    function getGridLayoutGroupInfo() {
        return {
            cellWidth: 128,
            cellHeight: 160,
            enableCellSpanning: true
        };
    };

    function assignTemplate(itemPromise) {
        return itemPromise.then(function (currentItem) {
            var templateElement;
            var result = null;
            if (currentItem.groupKey == MyCompany.Expenses.Enums.expensesGroupType.single) {
                if (currentItem.index < numberOfHighlightedTeamExpenses) {
                    templateElement = document.querySelector("#expenseTemplate-highlighted").winControl;
                }
                else {
                    templateElement = document.querySelector("#expenseTemplate").winControl;
                }
            }
            else {
                templateElement = document.querySelector("#totalExpenseTemplate").winControl;
            }

            result = templateElement.render(currentItem.data).then(function (element) {
                return element;
            });

            return result;
        });
    };

    function assignSnappedTemplate(itemPromise) {
        return itemPromise.then(function (currentItem) {
            var templateElement;
            var result = null;
            if (currentItem.groupKey == MyCompany.Expenses.Enums.expensesGroupType.single) {
                templateElement = document.querySelector("#expenseTemplateSnapped").winControl;
            }
            else {
                templateElement = document.querySelector("#totalExpenseTemplateSnapped").winControl;
            }

            result = templateElement.render(currentItem.data).then(function (element) {
                return element;
            });

            return result;
        });
    };

    function setEmployeePicture(expense) {
        if (expense.Employee.EmployeePictures && expense.Employee.EmployeePictures.length > 0) {
            expense.Employee.Picture = expense.Employee.EmployeePictures[0];
        }
        else {
            expense.Employee.Picture = null;
        }
    }

    function groupHeaderClick(headerType) {
        if (headerType == MyCompany.Expenses.Enums.expensesGroupType.single)
            nav.navigate(MyCompany.Expenses.Enums.pages.expenses, { totalTeamExpensesCount: totalTeamExpensesCount });
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

    function bindExpenses(element, isAppInSnappedMode, data) {
        var expensesListControl = element.querySelector("#expensesList").winControl;

        if (isAppInSnappedMode) {
            if (data && data.groupedExpenses) {
                ui.setOptions(expensesListControl, {
                    layout: new ui.ListLayout(),
                    itemTemplate: assignSnappedTemplate,
                    itemDataSource: data.groupedExpenses.dataSource,
                    groupDataSource: data.groupedExpenses.groups.dataSource
                });

                WinJS.Binding.processAll(document.querySelector("#teamExpensesHeader"), {
                    title: data.teamExpensesTitle
                });
                WinJS.Binding.processAll(document.querySelector("#teamExpensesPerPersonTitle"), {
                    title: data.teamExpensesPerPersonTitle
                });
            }
            else {
                ui.setOptions(expensesListControl, {
                    layout: new ui.ListLayout(),
                    itemTemplate: assignSnappedTemplate
                });
            }
        } else {
            if (data && data.groupedExpenses) {
                ui.setOptions(expensesListControl, {
                    layout: new ui.GridLayout({ groupInfo: getGridLayoutGroupInfo }),
                    itemTemplate: assignTemplate,
                    itemDataSource: data.groupedExpenses.dataSource,
                    groupDataSource: data.groupedExpenses.groups.dataSource
                });

                WinJS.Binding.processAll(document.querySelector("#teamExpensesHeader"), {
                    title: data.teamExpensesTitle
                });
                WinJS.Binding.processAll(document.querySelector("#teamExpensesPerPersonHeader"), {
                    title: data.teamExpensesPerPersonTitle
                });
            }
            else {
                ui.setOptions(expensesListControl, {
                    layout: new ui.GridLayout({ groupInfo: getGridLayoutGroupInfo }),
                    itemTemplate: assignTemplate
                });
            }
        }
    }

    function initExpensesList(element, page) {
        var expensesListControl = element.querySelector("#expensesList").winControl;
        expensesListControl.oniteminvoked = page.itemInvoked.bind(page);
        expensesListControl.onloadingstatechanged = WinJS.UI.eventHandler(function (e) {
            if (e.currentTarget.winControl.loadingState == "complete") {
                //Position template on snapped mode
                $(".groupHeader_0").on('click', function () {
                    groupHeaderClick(MyCompany.Expenses.Enums.expensesGroupType.single);
                });

                //ListView takes some delay to show, so we delay evolution to avoid showing it before ListView
                $("#expensesEvolution").delay(2000).fadeIn(500);

                MyCompany.Expenses.Application.hideLoading();
            }
        });
    }

    function initMonthList(element) {       
        var foodButton = element.querySelector("#foodExpensesButton");
        foodButton.onclick = function () { loadMonthExpenses(MyCompany.Expenses.Enums.expenseType.food) };

        var travelButton = element.querySelector("#travelExpensesButton");
        travelButton.onclick = function () { loadMonthExpenses(MyCompany.Expenses.Enums.expenseType.travel) };

        var accommodationButton = element.querySelector("#accommodationExpensesButton");
        accommodationButton.onclick = function () { loadMonthExpenses(MyCompany.Expenses.Enums.expenseType.accommodation) };

        var otherButton = element.querySelector("#otherExpensesButton");
        otherButton.onclick = function () { loadMonthExpenses(MyCompany.Expenses.Enums.expenseType.other) };

        var allButton = element.querySelector("#allExpensesButton");
        allButton.onclick = function () { loadMonthExpenses(MyCompany.Expenses.Enums.expenseType.all) };

    }

    function prepareAndBindData(results, updateLayout) {
        var expensesBigPicture = results[0] ? results[0] : [];
        var expensesSmallPicture = results[1] ? results[1] : [];
        var expensesCount = results[2];
        var teamExpenses = results[3] ? results[3] : [];
        var expensesByMonth = results[4] ? results[4] : [];

        for (var i = 0; i < expensesBigPicture.length; i++) {
            expensesBigPicture[i].type = MyCompany.Expenses.Enums.expensesGroupType.single;
            setEmployeePicture(expensesBigPicture[i]);
        }

        expensesSmallPicture.splice(0, numberOfHighlightedTeamExpenses);
        for (var i = 0; i < expensesSmallPicture.length; i++) {
            expensesSmallPicture[i].type = MyCompany.Expenses.Enums.expensesGroupType.single;
            setEmployeePicture(expensesSmallPicture[i]);
        }

        for (var i = 0; i < teamExpenses.length; i++) {
            teamExpenses[i].type = MyCompany.Expenses.Enums.expensesGroupType.total;
        }

        var expenses = expensesBigPicture.concat(expensesSmallPicture).concat(teamExpenses);
        expenses.totalTeamExpensesCount = expensesCount;
        totalTeamExpensesCount = expensesCount;
        bindData(expenses, expensesByMonth, MyCompany.Expenses.Enums.expenseType.all, updateLayout);
    }

    function showUserData(isAppInSnappedMode) {
        if ( isAppInSnappedMode)
            $(".user-data").hide();
        else
            $(".user-data").show();
    };

    ui.Pages.define(MyCompany.Expenses.Enums.pages.home, {
        updateLayout: function (element, viewState, data) {

            var isAppInSnappedMode = isSnapped();
                                    
            bindUserData(isAppInSnappedMode);

            bindExpenses(element, isAppInSnappedMode, data);

            bindMonthExpenses(element, isAppInSnappedMode, data);
        },
        itemInvoked: function (args) {
            var control = args.currentTarget.winControl;
            var index = args.detail.itemIndex;
            control.itemDataSource.itemFromIndex(index).then(function (item) {
                if (item.data.type == MyCompany.Expenses.Enums.expensesGroupType.single)
                    nav.navigate(MyCompany.Expenses.Enums.pages.expenseDetail, { expense: item.data });
                if (item.data.type == MyCompany.Expenses.Enums.expensesGroupType.total)
                    nav.navigate(MyCompany.Expenses.Enums.pages.employeeExpenses, { employeeId: item.data.EmployeeId });
            });
        },
        loadData: function () {
            var self = this;

            if (Windows.ApplicationModel.DesignMode.designModeEnabled) {
                bindUserData();
                var results = new Array();
                results[0] = MyCompany.Expenses.Data.Fake.getTeamExpensesBigPicture();
                results[1] = MyCompany.Expenses.Data.Fake.getTeamExpensesSmallPicture();
                results[2] = 5;
                results[3] = MyCompany.Expenses.Data.Fake.getTeamExpensesByMember();
                results[4] = MyCompany.Expenses.Data.Fake.getTeamExpensesByMonth();

                prepareAndBindData(results, self.updateLayout);
                return;
            }

            var allStatus = MyCompany.Expenses.Enums.expenseStatus.all;
            var promises = [];
            promises.push(MyCompany.Expenses.Services.ExpensesService.getTeamExpenses(allStatus, MyCompany.Expenses.Enums.pictureType.big, numberOfHighlightedTeamExpenses));
            promises.push(MyCompany.Expenses.Services.ExpensesService.getTeamExpenses(allStatus, MyCompany.Expenses.Enums.pictureType.small, numberOfHighlightedTeamExpenses + numberOfNonHighlightedTeamExpenses));
            promises.push(MyCompany.Expenses.Services.ExpensesService.getTeamCount(allStatus));
            promises.push(MyCompany.Expenses.Services.ExpensesService.getTeamExpensesByMember(MyCompany.Expenses.Enums.pictureType.small));
            promises.push(MyCompany.Expenses.Services.ExpensesService.getTeamExpensesByMonth(MyCompany.Expenses.Enums.expenseType.all));

            MyCompany.Expenses.Application.showLoading();
            WinJS.Promise.join(promises).then(function(results) {
                 prepareAndBindData(results, self.updateLayout);
            });
        },
        initAppBarCommands: function() {
            var self = this;
            refresh.onclick = function () {
                $(".groupHeader_0").off('click');
                MyCompany.Expenses.Services.cleanCache();
                self.loadData();
            };
        },
        ready: function (element, options) {
            this.initAppBarCommands();

            initExpensesList(element, this);

            initMonthList(element);

            this.loadData();
        }
    });
})();