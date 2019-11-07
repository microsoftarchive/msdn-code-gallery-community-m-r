(function () {
    "use strict";

    var appView = Windows.UI.ViewManagement.ApplicationView;
    var appViewState = Windows.UI.ViewManagement.ApplicationViewState;
    var nav = WinJS.Navigation;
    var ui = WinJS.UI;
    var utils = WinJS.Utilities;

    var onDataRequestHandler = null;

    function addComment(sessionId) {
        var textControl = document.querySelector(".text-comment");
        var text = textControl.innerText;

        var comment = {
            Text: text,
            RegisteredUserId: MyEvents.Context.currentUserId,
            AddedDateTime: new Date(),
            SessionId: sessionId
        };

        MyEvents.Services.SessionService.addComment(comment).then(function (commentId) {
            var commentsInSessionList = document.querySelector(".commentsInSessionList").winControl;
            comment.RegisteredUser = { Name: MyEvents.Context.userName };
            commentsInSessionList.itemDataSource.insertAtStart(commentId, comment);

            textControl.innerText = "";
            updateCommentCounter();
        });
    }

    function setSessionDuration(session) {
        var startTime = moment(session.StartTime);
        var endTime = moment(session.EndTime);
        session.Duration = endTime.diff(startTime, "minutes");
    }

    function updateCommentCounter() {
        var commentsInSessionList = document.querySelector(".commentsInSessionList").winControl;
        document.querySelector(".title-comments-count").innerText = commentsInSessionList.itemDataSource.getCount()._value;
    }

    function configureRatingControl(session) {
        var ratingControl = document.querySelector(".rating").winControl;

        if (session.IsFavorite) {
            ratingControl.removeEventListener("change");
            ratingControl.addEventListener("change", function (rating) {
                var score = rating.detail.tentativeRating;
                var sessionId = session.SessionId;
                MyEvents.Application.showLoading();
                MyEvents.Services.RegisteredUser.rateSession(MyEvents.Context.currentUserId, sessionId, score).then(MyEvents.Application.hideLoading);
            });

            if (session.UserScore) {
                ratingControl.userRating = session.UserScore;
            }
            ratingControl.disabled = false;
        }
        else {
            ratingControl.disabled = true;
        }
    }

    function bindFriendsList(session) {
        MyEvents.Services.FacebookService.getFriendsBySession(session).then(function (friends) {
            var friendInSessionList = document.querySelector(".friendInSessionList");
            friendInSessionList.winControl.itemDataSource = new WinJS.Binding.List(friends).dataSource;
            document.querySelector(".title-friends-count").innerText = friends.length;

            if (friends.length == 0) {
                document.querySelector(".no-friends").style.display = "block";
                friendInSessionList.style.display = "none";
            }
        });
    }

    function initAppBarCommands(session) {
        var appbar = document.getElementById('appbar');

        if (appbar) {
            var appbarwin = appbar.winControl;

            pin.winControl.hidden = true;
            favorite.winControl.hidden = true;

            // For demo porpouses always notes button is shown.
            notes.winControl.hidden = false;
            notes.onclick = function () {
                nav.navigate(MyEvents.Enums.pages.eventNotes, { eventId: session.EventDefinitionId, sessionId: session.SessionId });
                appbarwin.hide();
            };

            if (session && session.SessionId && !session.IsFavorite) {
                favorite.winControl.hidden = false;
                favorite.onclick = function () {
                    MyEvents.Application.showLoading();
                    MyEvents.Services.SessionService.addToFavorite(MyEvents.Context.currentUserId, session.SessionId).then(function () {
                        session.IsFavorite = true;
                        configureRatingControl(session);
                        favorite.winControl.hidden = true;
                        document.getElementsByClassName("details-favorite")[0].style.display = "block";
                        appbarwin.hide();
                        MyEvents.Application.hideLoading();
                    });
                };
            }

            if (session) {
                pin.winControl.hidden = false;

                if (Windows.ApplicationModel.DesignMode.designModeEnabled)
                    return;
                pin.onclick = function () {
                    var data = { type: MyEvents.Enums.tileType.session, sessionId: session.SessionId };
                    var tileLogo = new Windows.Foundation.Uri(MyEvents.Enums.images.sessionTile);
                    var tileWideLogo = new Windows.Foundation.Uri(MyEvents.Enums.images.sessionTileWide);

                    var secondaryTile = new Windows.UI.StartScreen.SecondaryTile(
                        "Tile.SessionId." + session.SessionId,
                        session.Title,
                        session.Title,
                        JSON.stringify(data),
                        Windows.UI.StartScreen.TileOptions.showNameOnLogo | Windows.UI.StartScreen.TileOptions.showNameOnWideLogo,
                        tileLogo,
                        tileWideLogo
                    );

                    secondaryTile.requestCreateAsync();
                };
            }
        }
    }

    function initAppBar() {
        var appbar = document.getElementById('appbar');

        if (appbar) {
            var appbarwin = appbar.winControl;
            appbarwin.disabled = false;
        }
    }

    function setCommentsDataSource(comments) {
        var commentsList = new WinJS.Binding.List(comments);
        var commentsInSessionList = document.querySelector(".commentsInSessionList").winControl;
        commentsInSessionList.itemDataSource = commentsList.dataSource;
        updateCommentCounter();
    }

    function bindSession(session) {
        initAppBarCommands(session);

        var dataTransferManager = Windows.ApplicationModel.DataTransfer.DataTransferManager.getForCurrentView();
        onDataRequestHandler = function (e) {
            MyEvents.Application.dataRequested(e,
                {
                    title: session.Title,
                    description: session.Description,
                    url: "Event/SessionDetail?sessionId=" + session.SessionId
                });
        };
        dataTransferManager.addEventListener("datarequested", onDataRequestHandler);

        setSessionDuration(session);
        bindFriendsList(session);
        configureRatingControl(session);

        WinJS.Binding.processAll(document.querySelector(".user-data"), {
            user: {
                facebookId: MyEvents.Context.facebookId,
                name: MyEvents.Context.userName
            }
        });
        WinJS.Binding.processAll(document.querySelector(".pagetitle"), session);
        WinJS.Binding.processAll(document.querySelector(".details"), session);

        $('.multi-line-ellipsis').dotdotdot();
    }

    function onGetSessionCompleted(session) {
        bindSession(session);
        MyEvents.Application.showLoading();
        
        MyEvents.Services.SessionService.getComments(session.SessionId).then(function (data) {
            setCommentsDataSource(data);
            MyEvents.Application.hideLoading();
        });

        var promises = [];
        promises.push(MyEvents.Services.EventService.getEventMapImage(session.EventDefinitionId));
        promises.push(MyEvents.Services.EventService.getRoomPoints(session.SessionId));

        WinJS.Promise.join(promises).then(function (results) {
            session.MapImage = results[0];
            session.RoomPoints = results[1];

            MyEvents.Controls.RoomMap.drawSessionMapImage(session);
        });
    }

    ui.Pages.define(MyEvents.Enums.pages.sessionDetail, {
        ready: function (element, options) {
            var self = this;
            
            initAppBar();

            if (Windows.ApplicationModel.DesignMode.designModeEnabled) {
                if (!(options && options.session)) {
                    options = {
                        session: MyEvents.Data.Fake.getSession()
                    };
                }
            }
            
            if (options && options.session) {
                self.sessionId = options.session.SessionId;
                onGetSessionCompleted(options.session);
            }

            element.querySelector(".button-comment").addEventListener("click", function () {
                addComment(self.sessionId);
            });
            element.querySelector(".text-comment").attachEvent("onpropertychange", function (e) {
                var commentText = element.querySelector(".text-comment").innerText;
                var button = element.querySelector(".button-comment");
                if (commentText.trim().length > 0) {
                    button.disabled = false;
                }
                else {
                    button.disabled = true;
                }
            });
        },
        unload: function (e) {
            var dataTransferManager = Windows.ApplicationModel.DataTransfer.DataTransferManager.getForCurrentView();
            dataTransferManager.removeEventListener("datarequested", onDataRequestHandler);
        },
        updateLayout: function (element, viewState) {
            $('.multi-line-ellipsis').dotdotdot();
        },
    });
})();
