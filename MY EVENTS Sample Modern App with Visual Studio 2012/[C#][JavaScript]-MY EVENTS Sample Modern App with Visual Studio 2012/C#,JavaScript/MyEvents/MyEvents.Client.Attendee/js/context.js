(function () {
    "use strict";
    var currentUserId = undefined;
    var userName = undefined;
    var serviceToken = undefined;
    var facebookId = undefined;
    var expirationDate = undefined;
    var facebookFriends = undefined;
    var facebookToken = undefined;

    WinJS.Namespace.define("MyEvents.Context", {
        currentUserId: currentUserId,
        userName: userName,
        serviceToken: serviceToken,
        facebookId: facebookId,
        facebookToken: facebookToken,
        facebookFriends: facebookFriends,
        expirationDate: expirationDate
    });
})();
