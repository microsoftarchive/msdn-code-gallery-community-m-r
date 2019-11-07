(function () {
    "use strict";

    var getNextEvents = function (number) {
        var url = MyEvents.Config.getApiUrl() + "eventdefinitions?pageSize={pageSize}&pageIndex=0";
        url = url.replace("{pageSize}", number);
        return MyEvents.Services.doGetAsync(url);
    };

    var getEventsIAssist = function (userId) {
        var url = MyEvents.Config.getApiUrl() + "registeredusers?registeredUserId={registeredUserId}";
        url = url.replace("{registeredUserId}", userId);
        return MyEvents.Services.doGetAsync(url);
    };

    var getCurrentEvents = function () {
        var url = MyEvents.Config.getApiUrl() + "eventdefinitions/GetCurrentEventDefinitions";
        return MyEvents.Services.doGetAsync(url);
    };

    var getEvent = function (eventId) {
        var url = MyEvents.Config.getApiUrl() + "eventdefinitions/{eventDefinitionId}";
        url = url.replace("{eventDefinitionId}", eventId);
        return MyEvents.Services.doGetAsync(url);
    };

    var getEventMapImage = function (eventId) {
        var url = MyEvents.Config.getApiUrl() + "roompoints?eventDefinitionId={eventDefinitionId}";
        url = url.replace("{eventDefinitionId}", eventId);
        return MyEvents.Services.doGetAsync(url);
    };

    var getRoomPoints = function (sessionId) {
        var url = MyEvents.Config.getApiUrl() + "roompoints?sessionId={sessionId}";
        url = url.replace("{sessionId}", sessionId);
        return MyEvents.Services.doGetAsync(url);
    };

    var getScheduleInformation = function (eventId) {
        var url = MyEvents.Config.getApiUrl() + "eventdefinitions/GetScheduleInformation?eventDefinitionId={eventDefinitionId}";
        url = url.replace("{eventDefinitionId}", eventId);
        return MyEvents.Services.doGetAsync(url);
    };

    WinJS.Namespace.define("MyEvents.Services.EventService", {
        getNextEvents: getNextEvents,
        getEventsIAssist: getEventsIAssist,
        getEvent: getEvent,
        getEventMapImage: getEventMapImage,
        getRoomPoints: getRoomPoints,
        getScheduleInformation: getScheduleInformation,
        getCurrentEvents: getCurrentEvents
    });
})();