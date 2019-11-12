(function () {
    "use strict";

    var getSessions = function (eventDefinitionId) {
        var url = MyEvents.Config.getApiUrl() + "sessions?eventDefinitionId={eventDefinitionId}";
        url = url.replace("{eventDefinitionId}", eventDefinitionId);
        return MyEvents.Services.doGetAsync(url);
    };

    var getSession = function (sessionId) {
        var url = MyEvents.Config.getApiUrl() + "sessions/{sessionId}";
        url = url.replace("{sessionId}", sessionId);
        return MyEvents.Services.doGetAsync(url);
    };
    
    var getComments = function (sessionId) {
        var url = MyEvents.Config.getApiUrl() + "comments/{sessionId}";
        url = url.replace("{sessionId}", sessionId);
        return MyEvents.Services.doGetAsync(url);
    };

    var addComment = function (comment) {
        var url = MyEvents.Config.getApiUrl() + "comments";
        return MyEvents.Services.doPostAsync(url, comment);
    };

    var addToFavorite = function (userId, sessionId) {
        var url = MyEvents.Config.getApiUrl() + "registeredusers/PostRegisteredUserToSession?RegisteredUserId={registeredUserId}&sessionId={sessionId}";
        url = url.replace("{registeredUserId}", userId);
        url = url.replace("{sessionId}", sessionId);
        return MyEvents.Services.doPostAsync(url);
    };

    WinJS.Namespace.define("MyEvents.Services.SessionService", {
        getSession: getSession,
        getSessions: getSessions,
        getComments: getComments,
        addComment: addComment,
        addToFavorite: addToFavorite
    });
})();