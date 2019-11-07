(function () {
    "use strict";

    WinJS.Binding.optimizeBindingReferences = true;
    
    var app = WinJS.Application;
    var activation = Windows.ApplicationModel.Activation;
    var nav = WinJS.Navigation;
    
    document.addEventListener("DOMContentLoaded", initialize, false);
    function initialize() {
        Microsoft.Maps.loadModule('Microsoft.Maps.Map');        
    }

    app.oncheckpoint = function (args) {
        // TODO: This application is about to be suspended. Save any state
        // that needs to persist across suspensions here. If you need to 
        // complete an asynchronous operation before your application is 
        // suspended, call args.setPromise().
        app.sessionState.history = nav.history;
    };

    app.addEventListener("activated", function (args) {
        if (args.detail.kind === activation.ActivationKind.launch) {
            if (args.detail.previousExecutionState !== activation.ApplicationExecutionState.terminated) {
                // TODO: This application has been newly launched. Initialize
                // your application here.
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
                    return nav.navigate("/pages/login/login.html", data);
                }

                if (nav.location) {
                    nav.history.current.initialPlaceholder = true;                    
                    return nav.navigate(nav.location, nav.state);
                } else {

                    if (Windows.ApplicationModel.DesignMode.designModeEnabled) {
                        return nav.navigate("/pages/eventList/eventList.html");
                    }

                    return nav.navigate(Application.navigator.home);
                }
            }));
        }

        WinJS.Application.onerror = function (event) {
            var errors = event.detail.error;

            if (typeof errors === 'string') {
                errors = [errors];
            }

            var errorsLength = errors.length;

            for (var errorIndex = 0; errorIndex < errorsLength; errorIndex++) {
                if (errors[errorIndex] === MyEvents.Enums.errorType.expiredToken) {
                    onSignOut();
                    return true;
                }
            }
        };
    });

    app.onsettings = function (e) {
        e.detail.applicationcommands =
            {
                "Settings": {
                    title: "Preferences",
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
        MyEvents.Storage.removeSetting('facebook_token');
        nav.navigate("/pages/login/login.html");        
    }

    if (Windows.ApplicationModel.DesignMode.designModeEnabled) {
        var scripts = [
            '//Microsoft.WinJS.1.0/js/ui.js',
            'ms-appx:///Bing.Maps.JavaScript//js/veapicore.js',
            'ms-appx:///Bing.Maps.JavaScript//js/veapimodules.js',
            '/js/lib/moment.js',
            '/js/lib/jquery-1.7.2.min.js',
            '/js/lib/jquery.dotdotdot-1.5.1.js',
            '/js/navigator.js',
            '/js/context.js',
            '/js/config.js',
            '/js/application.js',
            '/js/formaters.js',
            '/js/enums.js',
            '/js/storageManager.js',
            '/js/services/services.js',
            '/js/services/authenticationService.js',
            '/js/services/registeredUserService.js',
            '/js/services/eventService.js',
            '/js/services/sessionService.js',
            '/js/services/facebookService.js',
            '/js/services/facebookService.js',
            '/js/fakeData.js'
        ];

        for (var i = 0; i < scripts.length; i++) {
            var script = document.createElement('script');
            script.src = scripts[i];
            document.head.appendChild(script);
        }
    }

    app.start();
})();
