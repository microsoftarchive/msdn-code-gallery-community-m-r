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

    var saveFile = function (name, blob) {
        localFolder.createFileAsync(name, Windows.Storage.CreationCollisionOption.replaceExisting)
           .then(function (file) {

               // Open the returned file in order to copy the data
               return file.openAsync(Windows.Storage.FileAccessMode.readWrite).
                   then(function (stream) {
                       return Windows.Storage.Streams.RandomAccessStream.copyAsync(blob.msDetachStream(), stream).then(function () {
                           // Copy the stream from the blob to the File stream
                           return stream.flushAsync().then(function () {
                               stream.close();
                           });
                       });
                   });
           }).done(function () {
           });
    };

    var getFiles = function (name, onCompleted) {
        localFolder.getFileAsync(name)
            .then(function (folder) {
                folder.getFilesAsync()
                    .then(function (files) {
                        onCompleted(files);
                    });
            });
    };

    WinJS.Namespace.define("MyEvents.Storage", {
        saveSetting: saveSetting,
        getSetting: getSetting,
        removeSetting: removeSetting,
        saveFile: saveFile,
        getFiles: getFiles,
        saveSettingIntoContainer: saveSettingIntoContainer,
        getSettingFromContainer: getSettingFromContainer,
        clearSettings: clearSettings
    });
})();