(function () {
    "use strict";

    var getFakeFacebookUser = function() {
        return {
            Name: "Nicole Herskowitz",
            Bio: "Sr. Director at Microsoft",
            FacebookId: "100004258750017"
        };
    };

    var getFriends = function () {
        return new WinJS.Promise(function (complete, raiseError, progress) {
            var offline = MyEvents.Config.getOfflineMode();
            if (offline) {
                complete(
                    [getFakeFacebookUser()]
                );
                return;
            }

            if (MyEvents.Context.facebookFriends)
                complete(MyEvents.Context.facebookFriends);

            var url = "https://graph.facebook.com/me/friends?fields=name,bio&access_token={accessToken}";
            url = url.replace("{accessToken}", MyEvents.Context.facebookToken);
            MyEvents.Services.doGetAsync(url).then(function (facebookData) {
                if (!facebookData || !facebookData.data)
                    complete();

                var friends = [];
                var friendsLength = facebookData.data.length;
                var friend;

                for (var friendIndex = 0; friendIndex < friendsLength; friendIndex++) {
                    friend = facebookData.data[friendIndex];
                    friends.push({
                        Name: friend.name,
                        Bio: friend.bio || '',
                        FacebookId: friend.id
                    });
                }

                MyEvents.Context.facebookFriends = friends;
                complete(friends);
            },
            function (error) {
                console.log(error);
                raiseError(error);
            });
        });
    };

    var getFriendsBySession = function (session) {
        return new WinJS.Promise(function (complete, raiseError, progress) {
            var offline = MyEvents.Config.getOfflineMode();
            if (offline) {
                complete(
                   [getFakeFacebookUser()]
                );
                return;
            }
            
            getFriends().then(function (facebookData) {
                if (!facebookData)
                    complete();

                var friends = [];
                var friendsLength = facebookData.length;
                var friend;

                for (var friendIndex = 0; friendIndex < friendsLength; friendIndex++) {
                    friend = facebookData[friendIndex];
                    if (isFriendRegisteredToSession(session, friend.FacebookId)) {
                        friends.push(friend);
                    }
                }
                complete(friends);
            },
            function (error) {
                console.log(error);
                raiseError(error);
            });
        });
    };

    var isFriendRegisteredToSession = function(session, facebookUserId) {
        var registeredUsersLength = session.SessionRegisteredUsers.length;

        for (var registeredUserIndex = 0; registeredUserIndex < registeredUsersLength; registeredUserIndex++) {
            if (session.SessionRegisteredUsers[registeredUserIndex].FacebookId == facebookUserId)
                return true;
        }

        return false;
    };

    var getFriendsInSession = function(session, friends) {
        var friend;
        var friendsLength = friends.length;
        var friendsInSession = [];
        
        for (var friendIndex = 0; friendIndex < friendsLength; friendIndex++) {
            friend = friends[friendIndex];
            if (isFriendRegisteredToSession(session, friend.FacebookId)) {
                friendsInSession.push(friend);
            }
        }

        return friendsInSession;
    };

    WinJS.Namespace.define("MyEvents.Services.FacebookService", {
        getFriendsInSession: getFriendsInSession,
        getFriends: getFriends,
        getFriendsBySession: getFriendsBySession
    });
})();