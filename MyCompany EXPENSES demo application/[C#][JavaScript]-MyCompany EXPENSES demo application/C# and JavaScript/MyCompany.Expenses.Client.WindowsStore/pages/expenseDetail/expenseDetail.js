(function () {
    "use strict";

    var nav = WinJS.Navigation;
    var ui = WinJS.UI;
    var expense;
    var audio;

    function bindData(data) {
        var userData = {
            image: (MyCompany.Expenses.Context.currentUser.EmployeePictures && MyCompany.Expenses.Context.currentUser.EmployeePictures.length > 0) ? MyCompany.Expenses.Context.currentUser.EmployeePictures[0].Content : null,
            name: MyCompany.Expenses.Context.currentUser.FirstName + " " + MyCompany.Expenses.Context.currentUser.LastName
        };

        data.user = userData;

        initAppBarCommands(data);
        WinJS.Binding.processAll(document.querySelector(".expensesdetailpage"), data);
        $(".multi-line-ellipsis").dotdotdot();
    };



    function setEmployeePicture(expense) {
        if (expense.Employee.EmployeePictures && expense.Employee.EmployeePictures.length > 0) {
            expense.Employee.Picture = expense.Employee.EmployeePictures[0];
        }
        else {
            expense.Employee.Picture = null;
        }
    }

    function EnableDisableAppBarButtons()
    {
        var btn = document.getElementById("refuse");
        var ctx = { disabled: expense.Status == MyCompany.Expenses.Enums.expenseStatus.denied };
        WinJS.Binding.processAll(btn, ctx);

        btn = document.getElementById("approve");
        ctx = { disabled: expense.Status == MyCompany.Expenses.Enums.expenseStatus.approved };
        WinJS.Binding.processAll(btn, ctx);

    }

    function initAppBarCommands(expense) {
        EnableDisableAppBarButtons();
        refuse.onclick = function () {
            if (expense.Status != MyCompany.Expenses.Enums.expenseStatus.denied) {
                MyCompany.Expenses.Application.showLoading();
                MyCompany.Expenses.Services.ExpensesService.updateStatus(expense.ExpenseId, MyCompany.Expenses.Enums.expenseStatus.denied).then(function (result) {
                    expense.Status = MyCompany.Expenses.Enums.expenseStatus.denied;
                    bindData(expense);
                    EnableDisableAppBarButtons();
                    MyCompany.Expenses.Application.hideLoading();
                });
            }
        };

        approve.onclick = function () {
            if (expense.Status != MyCompany.Expenses.Enums.expenseStatus.approved) {
                MyCompany.Expenses.Application.showLoading();
                MyCompany.Expenses.Services.ExpensesService.updateStatus(expense.ExpenseId, MyCompany.Expenses.Enums.expenseStatus.approved).then(function (result) {
                    expense.Status = MyCompany.Expenses.Enums.expenseStatus.approved;
                    bindData(expense);
                    EnableDisableAppBarButtons();
                    MyCompany.Expenses.Application.hideLoading();
                });
            }
        };

        pin.onclick = function () {
            var data = { type: MyCompany.Expenses.Enums.tileType.expense, expenseId: expense.ExpenseId };
            var tileLogo = new Windows.Foundation.Uri(MyCompany.Expenses.Enums.images.expenseTile);
            var tileWideLogo = new Windows.Foundation.Uri(MyCompany.Expenses.Enums.images.expenseTileWide);
            var tileId = "Tile.ExpenseId." + expense.ExpenseId;
            
            var secondaryTile = new Windows.UI.StartScreen.SecondaryTile ( 
                tileId,
                expense.Name,
                expense.Name,
                JSON.stringify(data),
                Windows.UI.StartScreen.TileOptions.showNameOnLogo | Windows.UI.StartScreen.TileOptions.showNameOnWideLogo,
                tileLogo,
                tileWideLogo
            );
            secondaryTile.requestCreateAsync().then(function (result) {
                if (result) {
                    var imageName = "employee" + expense.Employee.EmployeeId + ".png";
                    MyCompany.Expenses.Services.ExpensesService.getExpense(expense.ExpenseId, MyCompany.Expenses.Enums.pictureType.small).then(function (result) {
                        var expenseWithSmallPhoto = result;
                        if (expenseWithSmallPhoto.Employee.EmployeePictures.length > 0) {
                            var photo = Windows.Security.Cryptography.CryptographicBuffer.decodeFromBase64String(expenseWithSmallPhoto.Employee.EmployeePictures[0].Content);
                            MyCompany.Expenses.Storage.saveFile(imageName, "secondaryTileImages", photo).then(function () {
                                MyCompany.Expenses.Tiles.updateSecondaryTile(expense, imageName, tileId);

                            })
                        }
                    });
                }
            });
        };
    }

    var formatExpenseStatusText = function (status) {
        var statusText = "";
        if (status == MyCompany.Expenses.Enums.expenseStatus.approved)
            statusText = "approved";
        if (status == MyCompany.Expenses.Enums.expenseStatus.denied)
            statusText = "denied";
        if (status == MyCompany.Expenses.Enums.expenseStatus.pending)
            statusText = "pending";

        return statusText;
    };

    var formatExpenseType = function (status) {
        var typeText = "";
        if (status == MyCompany.Expenses.Enums.expenseType.accommodation)
            typeText = "accomodation";
        if (status == MyCompany.Expenses.Enums.expenseType.food)
            typeText = "food";
        if (status == MyCompany.Expenses.Enums.expenseType.travel)
            typeText = "travel";
        if (status == MyCompany.Expenses.Enums.expenseType.other)
            typeText = "other";

        return typeText;
    };

    function speakExpense(expense) {
        var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
        audio = new Audio();
        var expenseStatus = formatExpenseStatusText(expense.Status);
        var expenseType = formatExpenseType(expense.ExpenseType);
        var text = "Expense detail for expense with name " + expense.Name + ", of type " + expenseType +
            ". The expense has an amount of " + expense.Amount + ". The state is " + expenseStatus;
        synth.synthesizeTextToStreamAsync(text).then(function (markersStream){
            var blob = MSApp.createBlobFromRandomAccessStream(markersStream.contentType, markersStream);
            audio.src = URL.createObjectURL(blob, { oneTimeOnly: true });
            audio.play();
        });
    };

    function stopAudio(){
        if ( audio )
            audio.pause();
    };
        

    ui.Pages.define(MyCompany.Expenses.Enums.pages.expenseDetail, {
        updateLayout: function (element, viewState, data) {
            $(".multi-line-ellipsis").dotdotdot();
        },
        loadData: function (expenseId) {
            var self = this;
            if (Windows.ApplicationModel.DesignMode.designModeEnabled) {
                var result = MyCompany.Expenses.Data.Fake.getExpense();
                setEmployeePicture(result);
                expense = result;
                bindData(expense);
                return;
            }

            MyCompany.Expenses.Application.showLoading();
            MyCompany.Expenses.Services.ExpensesService.getExpense(expenseId, MyCompany.Expenses.Enums.pictureType.big).then(function (result) {
                setEmployeePicture(result);
                expense = result;
                bindData(expense);
                MyCompany.Expenses.Application.hideLoading();
                speakExpense(expense);
            });

            var backButton = document.getElementById("expenseDetailBackButton");
            backButton.addEventListener("click", stopAudio, false);
        },
        ready: function (element, options) {
            if (Windows.ApplicationModel.DesignMode.designModeEnabled) {
                this.loadData(-1);
            }
            else {                
                this.loadData(options.expense.ExpenseId);
            }
          
        }
    });
})();