(function () {
    "use strict";

    var rateSession = function(userId, sessionId, score) {
        var url = MyEvents.Config.getApiUrl() + "registeredusers/PostRegisteredUserScore?registeredUserId={registeredUserId}&sessionId={sessionId}&score={score}";
        url = url.replace("{registeredUserId}", userId);
        url = url.replace("{sessionId}", sessionId);
        url = url.replace("{score}", score);

        return MyEvents.Services.doPostAsync(url);
    };

    var registerUserToEvent = function(userId, eventId) {
        var url = MyEvents.Config.getApiUrl() + "registeredusers/PostRegisteredUserToEvent?registeredUserId={registeredUserId}&eventDefinitionId={eventDefinitionId}";
        url = url.replace("{registeredUserId}", userId);
        url = url.replace("{eventDefinitionId}", eventId);

        return MyEvents.Services.doPostAsync(url);
    };

    WinJS.Namespace.define("MyEvents.Services.RegisteredUser", {
        rateSession: rateSession,
        registerUserToEvent: registerUserToEvent
    });
})();