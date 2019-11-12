// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509
(function () {
    "use strict";

    WinJS.Binding.optimizeBindingReferences = true;

    var app = WinJS.Application;
    var activation = Windows.ApplicationModel.Activation;
    var nav = WinJS.Navigation;

    app.onactivated = function (args) {
        if (args.detail.kind === activation.ActivationKind.launch) {
            if (args.detail.previousExecutionState !== activation.ApplicationExecutionState.terminated) {
                // TODO: This application has been newly launched. Initialize
                // your application here.
                var pp = 0;
            } else {
                // TODO: This application has been reactivated from suspension.
                // Restore application state here.
            }

            if (app.sessionState.history) {
                nav.history = app.sessionState.history;
            }

            args.setPromise(WinJS.UI.processAll().then(function () {
                if (args.detail.arguments) {
                    var data = JSON.parse(args.detail.arguments);
                    return nav.navigate(MyCompany.Expenses.Enums.pages.login, data);
                }

                if (nav.location) {
                    nav.history.current.initialPlaceholder = true;
                    return nav.navigate(nav.location, nav.state);
                } else {

                    return nav.navigate(MyCompany.Expenses.Enums.pages.login);
                }
            }));

            WinJS.Application.onerror = function (event) {
                var errors = event.detail.error;

                if (typeof errors === 'string') {
                    errors = [errors];
                }
            };
        }
    };

    app.oncheckpoint = function (args) {
        // TODO: This application is about to be suspended. Save any state
        // that needs to persist across suspensions here. You might use the
        // WinJS.Application.sessionState object, which is automatically
        // saved and restored across suspension. If you need to complete an
        // asynchronous operation before your application is suspended, call
        // args.setPromise().
    };


    app.onsettings = function (e) {
        e.detail.applicationcommands =
            {
                "Settings": {
                    title: "Configuration",
                    href: "/pages/settings/settings.html"
                }
            };
        WinJS.UI.SettingsFlyout.populateSettings(e);

        var appSettings = Windows.UI.ApplicationSettings;
        var vector = e.detail.e.request.applicationCommands;
        var signOutCommand = new appSettings.SettingsCommand("Signout", "Sign out", onSignOut);
        vector.append(signOutCommand);
    };

    function onSignOut(eventArgs) {
        MyCompany.Expenses.Services.NotificationChannelsService.deleteChannel();
        MyCompany.Expenses.Storage.removeSetting('auth_token');
        nav.navigate(MyCompany.Expenses.Enums.pages.login);
    }

    if (Windows.ApplicationModel.DesignMode.designModeEnabled) {
        var scripts = [
            , '//Microsoft.WinJS.2.0/js/base.js',
            , '//Microsoft.WinJS.2.0/js/ui.js',
            , '/js/lib/moment.js'
            , '/js/lib/jquery-1.8.3.min.js'
            , '/js/lib/jquery.dotdotdot-1.5.9.js'
            , '/js/lib/linq.min.js'
            , '/js/navigator.js'
            , '/js/context.js'
            , '/js/config.js'
            , '/js/application.js'
            , '/js/enums.js'
            , '/js/formatters.js'
            , '/js/storageManager.js'
            , '/js/tiles.js'
            , '/js/services/services.js'
            , '/js/services/authenticationService.js'
            , '/js/services/expensesService.js'
            , '/js/fakeData.js'
        ];

        for (var i = 0; i < scripts.length; i++) {
            var script = document.createElement('script');
            script.src = scripts[i];
            document.head.appendChild(script);
        }
    }

    var registerBackgroundTasks = function () {
        var _isRegistered = false,
        _bgTaskName = "toastNotificationTask",
        _appModel = Windows.ApplicationModel,
        _background = _appModel.Background,
        _registeredTasks = _background.BackgroundTaskRegistration.allTasks.first(),
        task;

        while (_registeredTasks.hasCurrent) {
            task = _registeredTasks.current.value;
            if (task.name === _bgTaskName) {
                _isRegistered = true;
                break;
            }
            _registeredTasks.moveNext();
        }

        //if (!_isRegistered) {
        //    var taskBuilder = new _background.BackgroundTaskBuilder();
        //    var taskTrigger = new _background.PushNotificationTrigger();

        //    taskBuilder.name = _bgTaskName;
        //    taskBuilder.taskEntryPoint = "js\\backgroundTask.js";
        //    taskBuilder.setTrigger(taskTrigger);
        //    taskBuilder.register();
        //}
    }

    registerBackgroundTasks();
    app.start();
})();
