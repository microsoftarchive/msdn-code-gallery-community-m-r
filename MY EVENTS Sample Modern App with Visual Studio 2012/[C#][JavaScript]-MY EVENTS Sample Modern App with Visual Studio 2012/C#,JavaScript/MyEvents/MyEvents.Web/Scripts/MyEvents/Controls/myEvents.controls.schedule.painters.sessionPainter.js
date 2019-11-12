var myEvents = myEvents || {};
myEvents.controls = myEvents.controls || {};
myEvents.controls.schedule = myEvents.controls.schedule || {};
myEvents.controls.schedule.painters = myEvents.controls.schedule.painters || {};

myEvents.controls.schedule.painters.SessionPainter = function () {
    var self = this;

    self.getSessionCard = function (session, controlData, options) {
        var sessionDiv = $('<div class="session-container">' +
            '<a class="link block full-height" href="/Event/SessionDetail?sessionId=' + session.SessionId + '">' +
                '<div class="left session-title multi-line-ellipsis">' +
                    '<span>' + session.Title + '</span>' +
                '</div>' +
                '<div class="right session-time">' +
                    '<span>' + moment(session.StartTime).format("HH:mm") + '</span>' +
                    '<span>' + session.Duration + '\'</span>' +
                '</div>' +
                '<div class="clear session-speaker">' +
                    '<span>' + session.Speaker + '</span>' +
                '</div>' +
            '</a>' +
            '</div>');
        return sessionDiv;
    };

    self.setSessionCardBehavior = function (sessionCard, controlData, options, onResize, onStopResize) {
    
    };
};
