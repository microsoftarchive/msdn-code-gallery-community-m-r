(function () {
    "use strict";

    var nav = WinJS.Navigation;
    var ui = WinJS.UI;

    function isSnapped() {
        return document.documentElement.offsetWidth <= 500;
    };

    function bindData(data, updateLayout) {
        var expensesList = new WinJS.Binding.List(data);

        var groupedExpenses = expensesList.createGrouped(
            function (dataItem) {
                return dataItem.type.toString();
            },
            function (dataItem) {
                var title = "";
                if (dataItem.type === MyCompany.Expenses.Enums.expensesGroupType.single) {
                    title = "Team expenses";
                }
                else if (dataItem.type === MyCompany.Expenses.Enums.expensesGroupType.total) {
                    title = "Total expenses per employee";
                }
                return { title: title };
            },
            function (group1, group2) {
                return group1 - group2;
            }
        );

        updateLayout(document, isSnapped(), { groupedExpenses: groupedExpenses });
    };

    function getGridLayoutGroupInfo() {
        return {
            cellWidth: 402,
            cellHeight: 160,
            enableCellSpanning: true
        };
    };

    function initAppBar() {
    }

    function setEmployeePicture(expense) {
        if (expense.Employee.EmployeePictures && expense.Employee.EmployeePictures.length > 0) {
            expense.Employee.Picture = expense.Employee.EmployeePictures[0];
        }
        else {
            expense.Employee.Picture = null;
        }
    }

    function prepareAndBindData(result, updateLayout)
    {
        for (var i = 0; i < result.length; i++) {
            result[i].type = MyCompany.Expenses.Enums.expensesGroupType.single;
            setEmployeePicture(result[i]);
        }
        bindData(result, updateLayout);
    }

    ui.Pages.define(MyCompany.Expenses.Enums.pages.expenses, {
        updateLayout: function (element, viewState, data) {

            var isAppInSnappedMode = isSnapped();

            var expensesListControl = element.querySelector("#expensesList").winControl;

            if (isAppInSnappedMode) {
                var snappedTemplate = document.querySelector("#expenseTemplateSnapped");
                if (data && data.groupedExpenses) {
                    ui.setOptions(expensesListControl, {
                        layout: new ui.ListLayout(),
                        itemTemplate: snappedTemplate,
                        itemDataSource: data.groupedExpenses.dataSource,
                        groupDataSource: data.groupedExpenses.groups.dataSource
                    });
                }
                else {
                    ui.setOptions(expensesListControl, {
                        layout: new ui.ListLayout(),
                        itemTemplate: snappedTemplate
                    });
                }
            }
            else {
                var template = document.querySelector("#expenseTemplate");
                if (data && data.groupedExpenses) {
                    ui.setOptions(expensesListControl, {
                        layout: new ui.GridLayout({ groupInfo: getGridLayoutGroupInfo }),
                        itemTemplate: template,
                        itemDataSource: data.groupedExpenses.dataSource,
                        groupDataSource: data.groupedExpenses.groups.dataSource
                    });
                }
                else {
                    ui.setOptions(expensesListControl, {
                        layout: new ui.GridLayout({ groupInfo: getGridLayoutGroupInfo }),
                        itemTemplate: template,
                    });
                }
            }
        },
        itemInvoked: function (args) {
            var control = args.currentTarget.winControl;
            var index = args.detail.itemIndex;
            control.itemDataSource.itemFromIndex(index).then(function (item) {
                if (item.data.type == MyCompany.Expenses.Enums.expensesGroupType.single)
                    nav.navigate(MyCompany.Expenses.Enums.pages.expenseDetail, { expense: item.data });
            });
        },
        loadData: function (expensesCount) {
            var self = this;
            var allStatus = MyCompany.Expenses.Enums.expenseStatus.all;

            if (Windows.ApplicationModel.DesignMode.designModeEnabled) {
                var result = MyCompany.Expenses.Data.Fake.getAllTeamExpenses();

                prepareAndBindData(result, self.updateLayout);
                return;
            }

            MyCompany.Expenses.Application.showLoading();
            MyCompany.Expenses.Services.ExpensesService.getTeamExpenses(allStatus, MyCompany.Expenses.Enums.pictureType.small, expensesCount).then(function (result) {
                prepareAndBindData(result, self.updateLayout);
                MyCompany.Expenses.Application.hideLoading();
            })
        },
        ready: function (element, options) {
            var MAXNUMBEROFEXPENSES = 30;
            initAppBar();
            var expensesListControl = element.querySelector("#expensesList").winControl;
            expensesListControl.oniteminvoked = this.itemInvoked.bind(this);

            var expensesCount;
            if (Windows.ApplicationModel.DesignMode.designModeEnabled) {
                expensesCount = 6;
            }
            else {
                expensesCount = options.totalTeamExpensesCount
            }

            this.loadData(Math.min(expensesCount, MAXNUMBEROFEXPENSES));
            var userData = {
                image: (MyCompany.Expenses.Context.currentUser.EmployeePictures && MyCompany.Expenses.Context.currentUser.EmployeePictures.length > 0) ? MyCompany.Expenses.Context.currentUser.EmployeePictures[0].Content : null,
                name: MyCompany.Expenses.Context.currentUser.FirstName + " " + MyCompany.Expenses.Context.currentUser.LastName
            };

            WinJS.Binding.processAll(document.querySelector(".user-data"), {
                user: userData
            });
        }
    });
})();