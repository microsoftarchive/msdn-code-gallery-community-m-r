var myEvents = myEvents || {};
myEvents.controls = myEvents.controls || {};
myEvents.controls.schedule = myEvents.controls.schedule || {};

myEvents.controls.schedule.ScheduleControl = function (options, sessionPainter, availableSessionPainter, onDrawCalendarCompleted) {
    var self = this;
    var model;

    var defaultSettings = {
        timePrecision: 5,
        sizeFactor: 20.75
    };

    //TODO: ask for settings.
    var settings = defaultSettings;

    var controlData = {
        scheduleContainer: null,
        schedule: null,
        eventStart: null,
        rooms: null,
        itemHeight: null,
        minuteHeight: null,
        itemMargin: null
    };

    $(document).ready(function () {
        if (options.getAction) {
            $.getJSON(options.getAction, { eventDefinitionId: options.eventDefinitionId }, onGetDataCompleted);
        } else {
            model = options.model;

            model.Times = model.Times.$values || model.Times;
            model.EventDefinition.Sessions = model.EventDefinition.Sessions.$values || model.EventDefinition.Sessions;

            onGetDataCompleted(model);
        }

        function onGetDataCompleted(data) {
            model = data;
            controlData.eventStart = moment.utc(model.Times[0]);
            controlData.scheduleContainer = $('#schedule-container');

            createSchedule();
            drawSessions();

            if (onDrawCalendarCompleted)
                onDrawCalendarCompleted();
        }
    });

    function createSchedule() {
        controlData.scheduleContainer.empty();
        controlData.scheduleContainer.addClass("total-rooms-" + model.EventDefinition.Rooms);
        controlData.itemHeight = getScheduleItemHeight();
        controlData.minuteHeight = controlData.itemHeight / 60;
        controlData.itemMargin = 6 * (controlData.minuteHeight);
        controlData.rooms = [];

        createHeaders();

        var schedule = $('<div id="schedule" class="box"></div>');
        controlData.scheduleContainer.append(schedule);
        controlData.schedule = schedule;

        createTimesColumn();

        if (availableSessionPainter)
            createAvailableSessionsContainer();

        for (var i = 0; i < model.EventDefinition.Rooms; i++) {
            createRoom(i + 1);
        }
        controlData.schedule.append('<div class="clear"></div>');
    };

    function createHeaders() {
        var headerContainer = $('<div id="header-container"></div>');
        controlData.scheduleContainer.append(headerContainer);

        var timeHeader = $('<div class="times-column header"/>');
        headerContainer.append(timeHeader);

        for (var i = 0; i < model.EventDefinition.Rooms; i++) {
            var roomHeader = $('<div class="room-column header center"> room ' + (i + 1) + '</div>');
            headerContainer.append(roomHeader);
        }
    }

    function createAvailableSessionsContainer() {
        var availableSessionsContainer = $('#available-sessions');
        if (availableSessionsContainer.droppable)
            availableSessionsContainer.droppable({
                drop: function (e, ui) {
                    var sessionId = ui.draggable.data('sessionId');
                    var session = null;
                    var currentSession;
                    for (var sessionIndex = 0; sessionIndex < model.EventDefinition.Sessions.length; sessionIndex++) {
                        currentSession = model.EventDefinition.Sessions[sessionIndex];
                        if (currentSession.SessionId == sessionId) {
                            session = currentSession;
                            break;
                        }
                    }

                    ui.draggable.remove();
                    session.RoomNumber = 0;
                    drawSessionInAvailableSessions(availableSessionsContainer, session);

                    var data = {
                        sessionId: session.SessionId,
                        startTime: moment.utc(session.StartTime).format('YYYY-MM-DDTHH:mm'),
                        duration: session.Duration,
                        roomNumber: session.RoomNumber
                    };
                    updateSession(data);
                }
            });
    }

    function getScheduleItemHeight() {
        return 90;
    }

    function createTimesColumn() {
        var timeColumn = $('<div class="times-column"/>');
        controlData.schedule.append(timeColumn);

        var numberOfTimes = model.Times.length;
        for (var i = 0; i < numberOfTimes; i++) {
            var timeRow = '<div class="time-container">' +
                '<span>' +
                moment.utc(model.Times[i]).format('HH:mm') +
                '</span>' +
                '</div>';
            timeRow = $(timeRow);

            timeColumn.append(timeRow);
            timeRow.css('height', controlData.itemHeight);
            timeRow.css('margin-top', controlData.itemMargin);
        }
    }

    function createRoom(roomNumber) {
        var roomContainer = $('<div class="room-column"></div>');
        roomContainer.data('roomNumber', roomNumber);
        controlData.schedule.append(roomContainer);

        if (roomContainer.droppable)
            roomContainer.droppable({
                drop: function (e, ui) {
                    moveSession(ui.helper, ui.draggable, roomNumber);
                },
                accept: function (originalSessionContainer) {
                    var sessionContainer = $('.dragging-helper');
                    var intersectors = findIntersectors(sessionContainer, '.session-container');
                    var currentSessionId = originalSessionContainer.data('sessionId');
                    var isValid = true;
                    $(intersectors).each(function (index, otherSession) {
                        var sessionId = $(otherSession).data('sessionId');
                        if (sessionId && sessionId != currentSessionId) {
                            isValid = false;
                        }
                    });

                    if (isValid) {
                        var minTop = $('.time-gap').first().offset().top;
                        if (sessionContainer.offset().top < minTop)
                            isValid = false;
                    }

                    if (isValid) {
                        sessionContainer.css('cursor', 'move');
                        return isValid;
                    } else {

                        var isInAvailablseSession = findIntersectors(sessionContainer, $('#available-sessions')).length > 0;
                        if (isInAvailablseSession) {
                            sessionContainer.css('cursor', 'move');
                            return isValid;
                        } else {
                            sessionContainer.css('cursor', 'no-drop');

                        }
                    }
                }
            });

        roomContainer.attr('id', 'room' + roomNumber);
        controlData.rooms.push(roomContainer);
        createGaps(roomContainer, roomNumber);
    }

    function getStarPosition(sessionContainer, gap) {
        var startPosition = sessionContainer.offset().top - gap.offset().top;
        startPosition = roundStartPosition(startPosition);
        return startPosition;
    }

    function moveSession(sessionContainer, originalSessionContainer, roomNumber) {
        var gaps = findIntersectors(sessionContainer, $('.time-gap'));
        var firstGap = $(gaps[0]);

        var duration = getDurationByElement(sessionContainer);
        duration = roundDuration(duration);

        var numberOfIntersectionGaps = gaps.length;

        var startPosition = getStarPosition(sessionContainer, firstGap);

        var sessionHeight = getSessionHeigthByGapsAndDuration(numberOfIntersectionGaps, duration, startPosition);
        sessionContainer.height(sessionHeight);

        var sessionId = originalSessionContainer.data('sessionId');
        if (!originalSessionContainer.hasClass('available-session')) {
            originalSessionContainer.resizable('destroy');
        }

        var sessionContainerClone = originalSessionContainer.clone();
        originalSessionContainer.remove();

        if (sessionContainerClone.hasClass('available-session')) {
            sessionContainerClone.removeClass('available-session');
            sessionContainerClone.height(60 * controlData.minuteHeight);
        }

        sessionContainerClone.css('display', 'inline');
        sessionContainerClone.css('top', startPosition);
        sessionContainerClone.css('left', 0);
        sessionContainerClone.data('sessionId', sessionId);

        firstGap.append(sessionContainerClone);

        ifHasCollisionsCorrectHeight(sessionContainerClone);

        duration = getDurationByElement(sessionContainerClone);
        duration = roundDuration(duration);

        sessionPainter.setSessionCardBehavior(sessionContainerClone, controlData, settings, onResizeSession, onStopResizeSession);
        var startTime = getTimeByElement(sessionContainerClone);
        var data = {
            sessionId: sessionId,
            startTime: startTime.format('YYYY-MM-DDTHH:mm'),
            duration: duration,
            roomNumber: roomNumber
        };

        changeSessionUI(sessionContainerClone, startTime, duration);
        updateSession(data);
    }

    function changeSessionUI(sessionContainer, startTime, duration) {
        var timeInfoSpans = sessionContainer.find('.session-time').children();

        if (sessionContainer.height() < ((controlData.itemHeight / 2) + 10)) {
            sessionContainer.find('.session-speaker').hide();
            $(timeInfoSpans[1]).hide();
        } else {
            sessionContainer.find('.session-speaker').show();
            $(timeInfoSpans[1]).show();
        }

        $(timeInfoSpans[0]).html(startTime.format('HH:mm'));
        $(timeInfoSpans[1]).html(duration + '\'');

        sessionContainer.find('.multi-line-ellipsis').dotdotdot({
            watch: 'window'
        });
    }

    function createGaps(roomContainer) {
        var timeLength = model.Times.length;
        for (var timeIndex = 0; timeIndex < timeLength; timeIndex++) {
            var gap = $('<div class="time-gap"></div>');
            roomContainer.append(gap);
            gap.css('height', controlData.itemHeight);
            gap.css('margin-top', controlData.itemMargin);
            gap.data('time', model.Times[timeIndex].toString());
        }
    }

    function drawSessions() {
        var availableSessionsContainer = $('#available-sessions');
        availableSessionsContainer.empty();
        var sessionsLength = model.EventDefinition.Sessions.length;

        for (var sessionIndex = 0; sessionIndex < sessionsLength; sessionIndex++) {
            var session = model.EventDefinition.Sessions[sessionIndex];
            if (session.IsAssigned) {
                drawSessionInRoom(session);
            } else {
                drawSessionInAvailableSessions(availableSessionsContainer, session);
            }
        }
    }

    function getSessionHeigthByGapsAndDuration(numberOfGapsOccuped, duration) {
        var marginHeight = ((numberOfGapsOccuped - 1) * controlData.itemMargin);
        var height = (duration * controlData.minuteHeight) + marginHeight;
        return height;
    }

    function drawSessionInRoom(session) {
        var room = controlData.rooms[session.RoomNumber - 1];
        var sessionCard = sessionPainter.getSessionCard(session);
        var sessionStart = moment.utc(session.StartTime);

        var startMinute = sessionStart.diff(controlData.eventStart, 'minutes');
        var endMinute = startMinute + session.Duration;

        var gapsOccuped = Math.ceil(endMinute / 60) - Math.floor(startMinute / 60);
        var gapsBefore = Math.ceil(startMinute / 60);

        if (!(startMinute % 60 == 0))
            gapsBefore = gapsBefore - 1;

        var gapContainer = room.find('.time-gap')[gapsBefore];
        $(gapContainer).append(sessionCard);

        gapStartTime = moment($(gapContainer).data('time'));
        var startPosition = (Math.abs(sessionStart.minutes() - gapStartTime.minutes())) * controlData.minuteHeight;

        startPosition = roundStartPosition(startPosition);

        var height = getSessionHeigthByGapsAndDuration(gapsOccuped, session.Duration, startPosition);

        sessionCard.css('height', height);
        sessionCard.css('top', startPosition);

        sessionCard.data('sessionId', session.SessionId);
        sessionPainter.setSessionCardBehavior(sessionCard, controlData, settings, onResizeSession, onStopResizeSession);
        changeSessionUI(sessionCard, sessionStart, session.Duration);
    }

    function onResizeSession(e, ui) {
        var duration = getDurationByElement(ui.element);
        duration = roundDuration(duration);
        var numberOfIntersectionGaps = findIntersectors(ui.element, $('.time-gap')).length;

        var sessionHeight = getSessionHeigthByGapsAndDuration(numberOfIntersectionGaps, duration);

        (ui.element).height(sessionHeight);

        ifHasCollisionsCorrectHeight(ui.element);

        var startTime = getTimeByElement(ui.element);

        changeSessionUI(ui.element, startTime, duration);
    }

    function onStopResizeSession(e, ui) {
        ifHasCollisionsCorrectHeight(ui.element);

        var sessionId = ui.element.data('sessionId');
        var roomNumber = ui.element.parent().parent().data('roomNumber');
        var startTime = getTimeByElement(ui.element);
        var duration = getDurationByElement(ui.element);
        duration = roundDuration(duration);

        var data = {
            sessionId: sessionId,
            startTime: startTime.format('YYYY-MM-DDTHH:mm'),
            duration: duration,
            roomNumber: roomNumber
        };

        changeSessionUI(ui.element, startTime, duration);
        updateSession(data);
    }

    function roundDuration(duration) {
        var groupsOfPrecision = Math.round(duration / settings.timePrecision);
        return groupsOfPrecision * settings.timePrecision;
    }

    function roundStartPosition(top) {
        var topInMinutes = top / controlData.minuteHeight;
        var groupsOfPrecision = Math.round(topInMinutes / settings.timePrecision);
        return (groupsOfPrecision * settings.timePrecision) * controlData.minuteHeight;
    }

    function updateSession(data) {
        $.post(options.updateAction + '?ts=' + new Date().getTime(), data);
    }

    function ifHasCollisionsCorrectHeight(currentSession) {
        var currentSessionId = currentSession.data('sessionId');
        //find sessions that intersects, and are not me.
        var intersectors = findIntersectors(currentSession, $('.session-container:not(.available-session)'));
        var gaps, firstGap, lastGap, currentSessionTop, currentSessionBottom;

        $(intersectors).each(function (index, otherSession) {
            var sessionId = $(otherSession).data('sessionId');

            if (sessionId && sessionId != currentSessionId) {
                currentSessionTop = currentSession.offset().top;
                currentSessionBottom = currentSessionTop + currentSession.height();

                var otherSessionTop = otherSession.offset().top;
                var otherSessionBottom = otherSessionTop + otherSession.height();

                var isCovering = (currentSessionTop < otherSessionTop) && (otherSessionBottom < currentSessionBottom);

                if (isCovering) {
                    currentSession.height(currentSession.height() - (currentSessionBottom - otherSessionTop));

                    currentSessionTop = currentSession.offset().top;
                    currentSessionBottom = currentSessionTop + currentSession.height();
                }

                var otherSessionIsDown = (otherSessionTop < currentSessionBottom) && (currentSessionBottom < otherSessionBottom);

                if (otherSessionIsDown) {
                    currentSession.height(currentSession.height() - (currentSessionBottom - otherSessionTop));
                }
            }
        });

        gaps = findIntersectors(currentSession, $('.time-gap'));
        lastGap = gaps[gaps.length - 1];
        var lastGapBottom = lastGap.offset().top + controlData.itemHeight;
        currentSessionBottom = currentSessionTop + currentSession.height();

        var isInMargin = lastGapBottom < currentSessionBottom;

        if (isInMargin) {
            currentSession.height(currentSession.height() - (currentSessionBottom - lastGapBottom));

            currentSessionBottom = currentSessionTop + currentSession.height();
        }

        var isInTheBebiningOfAMargin = (currentSessionBottom - lastGap.offset().top < 2);
        if (isInTheBebiningOfAMargin) {
            currentSession.height(currentSession.height() - controlData.itemMargin);
        }

        var duration = getDurationByElement(currentSession);
        duration = roundDuration(duration);
        var height = getSessionHeigthByGapsAndDuration(gaps.length, duration);
        currentSession.height(height);
    }

    function drawSessionInAvailableSessions(availableSessionsContainer, session) {
        if (availableSessionPainter) {
            var sessionCard = availableSessionPainter.getSessionCard(session);
            sessionCard.addClass('available-session');
            sessionCard.css('height', controlData.itemHeight);
            sessionCard.data('sessionId', session.SessionId);

            availableSessionPainter.setSessionCardBehavior(sessionCard, controlData);
            availableSessionsContainer.append(sessionCard);

            availableSessionsContainer.find('.multi-line-ellipsis').dotdotdot({
                watch: 'window'
            });
        }
    }

    function getTimeByElement(element) {
        var intersectionGaps = findIntersectors(element, $('.time-gap'));
        var firstgap = intersectionGaps[0];
        var gapStartTime = moment.utc(firstgap.data('time'));

        var startPosition = element.position().top;
        startPosition = roundStartPosition(startPosition);

        var differenceInMinutes = startPosition / controlData.minuteHeight;
        var startTime = gapStartTime.add('minutes', differenceInMinutes);
        return startTime;
    }

    function getDurationByElement(element) {
        var intersectionGaps = findIntersectors(element, $('.time-gap'));
        var duration = Math.round((element.height() - (intersectionGaps.length - 1) * controlData.itemMargin) / controlData.minuteHeight);
        duration = parseInt(duration);
        return duration;
    }
};

//TODO: move to utils
function findIntersectors(element, elements) {
    var intersectors = [];

    var tAxis = element.offset();
    var t_x = [tAxis.left, tAxis.left + element.outerWidth()];
    var t_y = [tAxis.top, tAxis.top + element.outerHeight()];

    $(elements).each(function () {
        var $this = $(this);
        var thisPos = $this.offset();
        var i_x = [thisPos.left, thisPos.left + $this.outerWidth()];
        var i_y = [thisPos.top, thisPos.top + $this.outerHeight()];

        if (t_x[0] < i_x[1] && t_x[1] > i_x[0] &&
             t_y[0] < i_y[1] && t_y[1] > i_y[0]) {
            intersectors.push($this);
        }

    });
    return intersectors;
}



