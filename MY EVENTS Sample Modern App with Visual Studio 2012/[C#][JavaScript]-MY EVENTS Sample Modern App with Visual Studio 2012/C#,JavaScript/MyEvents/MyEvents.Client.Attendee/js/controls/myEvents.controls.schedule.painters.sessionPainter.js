var myEvents = myEvents || {};
myEvents.controls = myEvents.controls || {};
myEvents.controls.schedule = myEvents.controls.schedule || {};
myEvents.controls.schedule.painters = myEvents.controls.schedule.painters || {};

myEvents.controls.schedule.painters.SessionPainter = function (sessionDetailOnClick) {
    var self = this;

    self.getSessionCard = function (session, controlData, options) {
        var sessionDiv = $('<div class="session-container">' +
            '<a class="link block full-height" href="#">' +
                '<div class="left session-title multi-line-ellipsis">' +
                    '<span>' + session.Title + '</span>' +
                '</div>' +
                '<div class="right session-time">' +
                    '<span>' + moment(session.StartTime).format("HH:mm") + '</span>' +
                    '<span>' + session.Duration + '\'</span>' +
                '</div>' +
                '<div class="left session-friends" style="clear: both;">' +
                    '<div class="session-friends-wrapper">' +
                    '</div>' +
                '</div>' +
                '<div class="clear session-speaker">' +
                    '<div class="session-speaker-wrapper">' +
                        '<span>' + session.Speaker + '</span>' +
                    '</div>' +
                '</div>' +
            '</a>' +
            '</div>');
        
        if (session.IsFavorite) {
            sessionDiv.append('<div class="favorite" />');
        }

        if (session.Friends && session.Friends.length > 0)
        {
            var friendsDiv = sessionDiv.find('.session-friends-wrapper');
            var friendsLength = session.Friends.length;
            for (var friendIndex = 0; friendIndex < friendsLength; friendIndex++) {
                var image = $('<img src="' +
                    'https://graph.facebook.com/' +
                    session.Friends[friendIndex].FacebookId +
                    '/picture"/>'
                    );
                friendsDiv.append(image);
            }
        } else {
            sessionDiv.find('.session-title').css('max-height', '80%');
        }

        sessionDiv.find('.link').bind("click",
            function () {
                sessionDetailOnClick(session);
            });

        return sessionDiv;
    };

    self.setSessionCardBehavior = function (sessionCard, controlData, options, onResize, onStopResize) {
    
    };
};
