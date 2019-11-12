(function () {
    "use strict";

    var nav = WinJS.Navigation;
    var ui = WinJS.UI;

    ui.Pages.define(MyEvents.Enums.pages.login, {
        ready: function (element, options) {

            var onAuthenticated = function (authenticationResponse) {
                MyEvents.Application.hideLoading();

                if (authenticationResponse && authenticationResponse.RegisteredUserId && authenticationResponse.Token) {
                    MyEvents.Context.currentUserId = authenticationResponse.RegisteredUserId;
                    MyEvents.Context.userName = authenticationResponse.UserName;
                    MyEvents.Context.facebookId = authenticationResponse.FacebookUserId;
                    MyEvents.Context.serviceToken = authenticationResponse.Token;

                    var expirationDate = new Date();
                    expirationDate.setMinutes(expirationDate.getMinutes() + (parseInt(authenticationResponse.ExpirationTime) / 1000 / 60));
                    MyEvents.Context.expirationDate = expirationDate;

                    // for preloading facebook friends
                    MyEvents.Services.FacebookService.getFriends();

                    var navigateToUrl = function (navigateOptions) {
                        nav.navigate(navigateOptions.url, navigateOptions.data).done(function () {
                            if (navigateOptions.backUrl) {
                                WinJS.Navigation.history.backStack.push({ location: MyEvents.Enums.pages.eventList });
                            }
                        });
                    };

                    if (!options) {
                        navigateToUrl({
                            url: MyEvents.Enums.pages.eventList
                        });
                        return;
                    }
                    
                    if (options.type == MyEvents.Enums.tileType.event) {
                        MyEvents.Application.showLoading();
                        MyEvents.Services.EventService.getEvent(options.eventId).then(function (event) {
                            navigateToUrl({
                                url: MyEvents.Enums.pages.eventDetail,
                                backUrl: MyEvents.Enums.pages.eventList,
                                data: {event: event}
                            });
                            MyEvents.Application.hideLoading();
                        });
                    }
                    else if (options.type == MyEvents.Enums.tileType.session) {
                        MyEvents.Application.showLoading();
                        MyEvents.Services.SessionService.getSession(options.sessionId).then(function (session) {
                            navigateToUrl({
                                url: MyEvents.Enums.pages.sessionDetail,
                                backUrl: MyEvents.Enums.pages.eventList,
                                data: { session: session }
                            });
                            MyEvents.Application.hideLoading();
                        });
                    }
                }
                else {
                    var dialog = Windows.UI.Popups.MessageDialog("you don't have permissions to access this application.");
                    dialog.showAsync();
                }
            };

            MyEvents.Application.showLoading();
            MyEvents.Services.Authentication.authenticate().then(onAuthenticated);
        }
    });
})();