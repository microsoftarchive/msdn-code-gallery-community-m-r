(function () {
    "use strict";

    var pages = {
        login: "/pages/login/login.html",
        eventList: "/pages/eventList/eventList.html",
        eventDetail: "/pages/eventDetail/eventDetail.html",
        sessionDetail: "/pages/sessionDetail/sessionDetail.html",
        eventNotes: "/pages/eventNotes/eventNotes.html",
        settings: "/pages/settings/settings.html",
        shareTarget: "/pages/shareTarget/shareTarget.html"
    };

    var tileType = {
        event: 0,
        session: 1
    };
    
    var eventGroupType = {
        comingSoon: 0,
        iAssist: 1
    };

    var errorType = {
        expiredToken: "token expired"
    };

    var images = {
        eventTile: "ms-appx:///Assets/eventTile.png",
        eventTileWide: "ms-appx:///Assets/eventTileWide.png",
        sessionTile: "ms-appx:///Assets/sessionTile.png",
        sessionTileWide: "ms-appx:///Assets/sessionTileWide.png"
    };

    WinJS.Namespace.define("MyEvents.Enums", {
        tileType: tileType,
        eventGroupType: eventGroupType,
        errorType: errorType,
        pages: pages,
        images: images
    });
})();
