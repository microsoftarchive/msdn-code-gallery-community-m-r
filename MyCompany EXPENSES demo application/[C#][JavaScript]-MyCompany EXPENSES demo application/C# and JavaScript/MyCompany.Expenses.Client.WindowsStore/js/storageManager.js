(function () {
    "use strict";

    var localSettings = Windows.Storage.ApplicationData.current.localSettings;
    var localFolder = Windows.Storage.ApplicationData.current.localFolder;

    var saveSetting = function (key, data) {
        var item = new Windows.Storage.ApplicationDataCompositeValue();
        for (var property in data) {
            item[property] = data[property];
        }
        localSettings.values[key] = item;
    };

    var getSetting = function (key) {
        return localSettings.values[key];
    };

    var removeSetting = function (key) {
        localSettings.values.remove(key);
    };
    
    var clearSettings = function () {
        Windows.Storage.ApplicationData.current.clearAsync(Windows.Storage.ApplicationDataLocality.local);
    };

    var saveSettingIntoContainer = function (container, key, data) {
        if (!localSettings.containers[container])
            localSettings.createContainer(container, Windows.Storage.ApplicationDataCreateDisposition.Always);

        var item = new Windows.Storage.ApplicationDataCompositeValue();
        for (var property in data) {
            item[property] = data[property];
        }

        localSettings.containers[container].values[key] = item;
    };

    var getSettingFromContainer = function (container) {
        return localSettings.containers[container] ? localSettings.containers[container].values : null;
    };

    var saveFile = function (name, folder, buffer) {
        return new WinJS.Promise(function (complete, raiseError, progress) {
            localFolder.createFolderAsync(folder, Windows.Storage.CreationCollisionOption.openIfExists)
            .done(function (folder) {
                folder.createFileAsync(name, Windows.Storage.CreationCollisionOption.replaceExisting)
                   .then(function (file) {
                       return Windows.Storage.FileIO.writeBufferAsync(file, buffer).done(complete());
                   });
            });
        });
    };

    WinJS.Namespace.define("MyCompany.Expenses.Storage", {
        saveSetting: saveSetting,
        getSetting: getSetting,
        removeSetting: removeSetting,
        saveFile: saveFile,
        saveSettingIntoContainer: saveSettingIntoContainer,
        getSettingFromContainer: getSettingFromContainer,
        clearSettings: clearSettings
    });
})();