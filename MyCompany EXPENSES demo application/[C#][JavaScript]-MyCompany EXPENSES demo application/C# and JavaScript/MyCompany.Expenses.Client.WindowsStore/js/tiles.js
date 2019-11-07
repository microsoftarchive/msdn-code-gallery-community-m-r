(function () {
    "use strict";

    var setExpenseTileText = function(tile, expense)
    {
        var tileTextAttributes = tile.getElementsByTagName("text");
        tileTextAttributes[0].appendChild(tile.createTextNode(expense.Employee.FirstName + " " + expense.Employee.LastName));
        tileTextAttributes[1].appendChild(tile.createTextNode(""));
        tileTextAttributes[2].appendChild(tile.createTextNode(expense.Name));
        tileTextAttributes[3].appendChild(tile.createTextNode(expense.Amount + " $"));
    }

    var setMonthExpenseTileText = function (tile, monthExpenses) {
        if (monthExpenses.length > 0) {
            var lastAvailableMonthExpenses = monthExpenses[0];
            var tileTextAttributes = tile.getElementsByTagName("text");
            tileTextAttributes[0].appendChild(tile.createTextNode("Team expenses"));
            tileTextAttributes[1].appendChild(tile.createTextNode(""));
            tileTextAttributes[2].appendChild(tile.createTextNode(moment(lastAvailableMonthExpenses.Date).format("MMMM")));
            tileTextAttributes[3].appendChild(tile.createTextNode(lastAvailableMonthExpenses.Amount + " $"));
        }
    }
    
    var updateTile = function (imagelist, monthExpenses) {
        var appTile;
        // get wide tile template
        var appTile = Windows.UI.Notifications.TileUpdateManager.getTemplateContent(Windows.UI.Notifications.TileTemplateType.tileWidePeekImageCollection02);

        // set attributes
        setMonthExpenseTileText(appTile, monthExpenses);

        var tileImageAttributes = appTile.getElementsByTagName("image");
        for (var i = 0; i < Math.min(5, imagelist.length) ; i++) {
            tileImageAttributes[i].setAttribute("src", "ms-appdata:///local/expenseImages/" + imagelist[i]);
        }

        // do the same with a square tile template
        var squareTile = Windows.UI.Notifications.TileUpdateManager.getTemplateContent(Windows.UI.Notifications.TileTemplateType.tileSquarePeekImageAndText03);
        var squareTileImageAttributes = squareTile.getElementsByTagName("image");
        squareTileImageAttributes[0].setAttribute("src", MyCompany.Expenses.Enums.images.expenseTile);

        setMonthExpenseTileText(squareTile, monthExpenses);

        // include square tile
        var node = appTile.importNode(squareTile.getElementsByTagName("binding").item(0), true);
        appTile.getElementsByTagName("visual").item(0).appendChild(node);

        // send a notification to update the tile
        var tileNotification = new Windows.UI.Notifications.TileNotification(appTile);
        Windows.UI.Notifications.TileUpdateManager.createTileUpdaterForApplication().update(tileNotification);
    };


    var updateSecondaryTile = function (expense, imageName, tileId) {
        var pinTile;
        // get wide tile template
        var pinTile = Windows.UI.Notifications.TileUpdateManager.getTemplateContent(Windows.UI.Notifications.TileTemplateType.tileWideSmallImageAndText02);

        // set attributes
        setExpenseTileText(pinTile, expense);

        var tileImageAttributes = pinTile.getElementsByTagName("image");
        tileImageAttributes[0].setAttribute("src", "ms-appdata:///local/secondaryTileImages/" + imageName);

        // do the same with a square tile template
        var squareTile = Windows.UI.Notifications.TileUpdateManager.getTemplateContent(Windows.UI.Notifications.TileTemplateType.tileSquarePeekImageAndText03);
        var squareTileImageAttributes = squareTile.getElementsByTagName("image");
        squareTileImageAttributes[0].setAttribute("src", "ms-appdata:///local/secondaryTileImages/" + imageName);

        setExpenseTileText(squareTile, expense);

        // include square tile
        var node = pinTile.importNode(squareTile.getElementsByTagName("binding").item(0), true);
        pinTile.getElementsByTagName("visual").item(0).appendChild(node);

        // send a notification to update the tile
        var tileNotification = new Windows.UI.Notifications.TileNotification(pinTile);
        Windows.UI.Notifications.TileUpdateManager.createTileUpdaterForSecondaryTile(tileId).update(tileNotification);
    };

    var refreshMainTile = function() {
        return new WinJS.Promise(function (complete) {

            var promises = [];
            promises.push(MyCompany.Expenses.Services.ExpensesService.getTeamExpensesByMonth(MyCompany.Expenses.Enums.expenseType.all));
            promises.push(
                MyCompany.Expenses.Services.ExpensesService.getTeamExpenses(MyCompany.Expenses.Enums.expenseType.all,
                                                                        MyCompany.Expenses.Enums.pictureType.small,
                                                                        MyCompany.Expenses.Config.getNumberOfImagesOnTile()));
            WinJS.Promise.join(promises).then(function (results) {
                var expensesData = results[0];
                var imagesData = results[1];
                var storedPhotos = [];
                var firstExpense = null;
                if (imagesData) {
                    if (imagesData.length > 0)
                        firstExpense = imagesData[0];

                    for (var i = 0; i < imagesData.length; i++) {
                        var imageName = "expensePhoto" + imagesData[i].EmployeeId + ".png";
                        // some employees can be repeated. We don't need to store the same photo twice
                        if (storedPhotos.indexOf(imageName) == -1) {
                            var photo = Windows.Security.Cryptography.CryptographicBuffer.decodeFromBase64String(imagesData[i].Employee.EmployeePictures[0].Content);
                            MyCompany.Expenses.Storage.saveFile(imageName, "expenseImages", photo);
                        }
                        storedPhotos.push(imageName);
                    }

                    updateTile(storedPhotos, expensesData);
                }

                complete();
            });
        });
    }

    WinJS.Namespace.define("MyCompany.Expenses.Tiles", {
        refreshMainTile: refreshMainTile,
        updateSecondaryTile: updateSecondaryTile
    });

})();