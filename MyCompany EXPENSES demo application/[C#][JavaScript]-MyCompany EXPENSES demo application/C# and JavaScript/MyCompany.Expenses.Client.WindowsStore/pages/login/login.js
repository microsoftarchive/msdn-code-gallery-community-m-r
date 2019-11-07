(function () {
    "use strict";

    var nav = WinJS.Navigation;
    var ui = WinJS.UI;

    ui.Pages.define(MyCompany.Expenses.Enums.pages.login, {
        ready: function (element, options) {

            var onAuthenticated = function (authenticationResponse) {
                MyCompany.Expenses.Context.currentUser = authenticationResponse;
                if (authenticationResponse) {  
                        MyCompany.Expenses.Context.serviceToken = authenticationResponse.Token;
                        MyCompany.Expenses.Tiles.refreshMainTile().then(function () {
                            if(options && options.expenseId){
                                MyCompany.Expenses.Application.navigateToUrl({
                                    url: MyCompany.Expenses.Enums.pages.expenseDetail,
                                    backUrl: MyCompany.Expenses.Enums.pages.home,
                                    data: { expense: { ExpenseId: options.expenseId } }
                                });
                                return;
                            }

                            MyCompany.Expenses.Application.navigateToUrl({
                                url: MyCompany.Expenses.Enums.pages.home
                            });
                            return;
                        });
                }
                else {
                    MyCompany.Expenses.Application.hideLoading();
                    var dialog = Windows.UI.Popups.MessageDialog("You don't have permissions to access this application.");
                    dialog.showAsync();
                }
            };

            MyCompany.Expenses.Application.showLoading();
            MyCompany.Expenses.Services.Authentication.authenticate().then(onAuthenticated);
        }
    });
})();