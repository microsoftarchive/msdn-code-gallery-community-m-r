(function () {
    "use strict";

    var appView = Windows.UI.ViewManagement.ApplicationView;
    var appViewState = Windows.UI.ViewManagement.ApplicationViewState;
    var nav = WinJS.Navigation;
    var ui = WinJS.UI;
    var scheduleModel;
    var alreadyRegisteredMessage = "already registered to this event";

    var onDataRequestHandler = null;

    function navigateToSessionDetail(selectedSession) {
        nav.navigate(MyEvents.Enums.pages.sessionDetail, { session: selectedSession });
    }

    function setEventDuration(event) {
        var startTime = moment(event.StartTime);
        var endTime = moment(event.EndTime);
        event.Duration = endTime.diff(startTime, "minutes");
    }

    function register(event) {
        MyEvents.Application.showLoading();
        MyEvents.Services.RegisteredUser.registerUserToEvent(MyEvents.Context.currentUserId, event.EventDefinitionId).then(function () {
            var button = document.querySelector(".register");
            button.disabled = true;
            button.innerText = alreadyRegisteredMessage;
            event.Registered = true;
            MyEvents.Application.hideLoading();
        });
    }

    function configureRegisterButton(event) {
        var button = document.querySelector(".register");
        if (event.Registered) {
            button.disabled = true;
            button.innerText = alreadyRegisteredMessage;
        }
        else {
            button.disabled = false;
            button.innerText = "register with Facebook";
            button.addEventListener("click", function () {
                register(event);
            });
        }
    }

    function initAppBarCommands(event) {
        var appbar = document.getElementById('appbar');

        if (appbar) {
            var appbarwin = appbar.winControl;
            pin.winControl.hidden = false;
            favorite.winControl.hidden = true;

            // For demo porpouses always notes button is shown.
            notes.winControl.hidden = false;
            notes.onclick = function () {
                appbarwin.hide();
                nav.navigate(MyEvents.Enums.pages.eventNotes, { eventId: event.EventDefinitionId });
            };

            if (Windows.ApplicationModel.DesignMode.designModeEnabled)
                return;

            pin.onclick = function () {
                var data = { type: MyEvents.Enums.tileType.event, eventId: event.EventDefinitionId };
                var tileLogo = new Windows.Foundation.Uri(MyEvents.Enums.images.eventTile);
                var tileWideLogo = new Windows.Foundation.Uri(MyEvents.Enums.images.eventTileWide);

                var secondaryTile = new Windows.UI.StartScreen.SecondaryTile(
                    "Tile.EventId." + event.EventDefinitionId,
                    event.Name,
                    event.Name,
                    JSON.stringify(data),
                    Windows.UI.StartScreen.TileOptions.showNameOnLogo | Windows.UI.StartScreen.TileOptions.showNameOnWideLogo,
                    tileLogo,
                    tileWideLogo
                );
                secondaryTile.requestCreateAsync();
            };
        }
    }

    function initAppBar() {
        var appBar = document.getElementById("appbar");
        if (appBar) {
            var appbarwin = appBar.winControl;
            appbarwin.disabled = false;
        }
    }

    function onGetEventComplete(event) {
        bindEvent(event);
        MyEvents.Services.EventService.getScheduleInformation(event.EventDefinitionId).then(function (data) {
            scheduleModel = data;
            MyEvents.Services.FacebookService.getFriends().then(function (facebookUsers) {
                var sessionsLength = scheduleModel.EventDefinition.Sessions.length;

                for (var sessionIndex = 0; sessionIndex < sessionsLength; sessionIndex++) {
                    var session = scheduleModel.EventDefinition.Sessions[sessionIndex];
                    session.Friends = MyEvents.Services.FacebookService.getFriendsInSession(session, facebookUsers)
                }

                var scheduleControl = new myEvents.controls.schedule.ScheduleControl(
                    {
                        eventDefinitionId: event.EventDefinitionId,
                        model: scheduleModel
                    },
                    new myEvents.controls.schedule.painters.SessionPainter(navigateToSessionDetail),
                    null,
                    MyEvents.Application.hideLoading
                );
            });
        });
    }

    function bindEvent(event) {
        initAppBarCommands(event);
        if (!Windows.ApplicationModel.DesignMode.designModeEnabled) {
            $('.localization-map').showLocationInBingMap(event.Latitude, event.Longitude);
        }

        var dataTransferManager = Windows.ApplicationModel.DataTransfer.DataTransferManager.getForCurrentView();
        onDataRequestHandler = function (e) {
            MyEvents.Application.dataRequested(e,
                {
                    title: event.Name,
                    description: event.Description,
                    url: "Event/Detail/" + event.EventDefinitionId
                });
        };
        dataTransferManager.addEventListener("datarequested", onDataRequestHandler);

        setEventDuration(event);
        configureRegisterButton(event);

        WinJS.Binding.processAll(document.querySelector(".eventdetailpage"), event);
        WinJS.Binding.processAll(document.querySelector(".user-data"), {
            user: {
                facebookId: MyEvents.Context.facebookId,
                name: MyEvents.Context.userName
            }
        });

        $('.multi-line-ellipsis').dotdotdot();
    };

    ui.Pages.define(MyEvents.Enums.pages.eventDetail, {
        ready: function (element, options) {
            initAppBar();

            if (Windows.ApplicationModel.DesignMode.designModeEnabled) {
                if (!(options && options.event)) {
                    options = {
                        event: MyEvents.Data.Fake.getEvent()
                    };
                }
            }

            if (options && options.event) {
                MyEvents.Application.showLoading();
                onGetEventComplete(options.event);
            }
        },
        unload: function (e) {
            var dataTransferManager = Windows.ApplicationModel.DataTransfer.DataTransferManager.getForCurrentView();
            dataTransferManager.removeEventListener("datarequested", onDataRequestHandler);
        }
    });
})();
