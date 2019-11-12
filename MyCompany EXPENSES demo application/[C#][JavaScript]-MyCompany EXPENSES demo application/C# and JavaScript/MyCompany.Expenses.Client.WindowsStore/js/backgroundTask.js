(function () {
    var run = function () {
        var key = null,
            settings = Windows.Storage.ApplicationData.current.localSettings;

        // Record information in LocalSettings to communicate with the app.
        key = backgroundTaskInstance.task.taskId.toString();
        settings.values[key] = "Succeeded";

        close();
    };

    run();

})();

