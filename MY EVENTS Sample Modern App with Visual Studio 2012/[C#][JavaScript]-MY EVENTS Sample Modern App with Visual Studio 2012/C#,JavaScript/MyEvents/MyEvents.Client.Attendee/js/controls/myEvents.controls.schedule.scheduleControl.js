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
        itemSize: null,
        minuteSize: null,
        itemMargin: null
    };

    $(document).ready(function () {
        model = options.model;
        
        model.Times = model.Times.$values || model.Times;
        model.EventDefinition.Sessions = model.EventDefinition.Sessions.$values || model.EventDefinition.Sessions;

        controlData.eventStart = moment.utc(model.Times[0]);
        controlData.scheduleContainer = $('.schedule');
        createSchedule();
        drawSessions();

        if (onDrawCalendarCompleted)
            onDrawCalendarCompleted();
    });

    function setElementMargin(element, margin) {
        if (controlData.orientation == 0) //vertical
        {
            element.css('margin-top', margin);

        } else { //horizontal
            element.css('margin-left', margin);
        }
    }

    function setElementSize(element, size) {
        if (controlData.orientation == 0) //vertical
        {
            element.css('height', size);

        } else { //horizontal
            element.css('width', size);
        }
    }

    function getElementSize(element) {
        if (controlData.orientation == 0) //vertical
        {
            return element.height();

        } else { //horizontal
            return element.width();
        }
    }

    function getElementPointA(element) {
        if (controlData.orientation == 0) //vertical
        {
            return element.offset().top;

        } else { //horizontal
            return element.offset().left;
        }
    }

    function getElementPointB(element) {
        if (controlData.orientation == 0) //vertical
        {
            return element.offset().top + element.height();
        } else { //horizontal
            return element.offset().left + element.width;
        }
    }

    function setElementRelativePosition(element, position) {
        if (controlData.orientation == 0) //vertical
        {
            element.css('top', position);

        } else { //horizontal
            element.css('left', position);
        }
    }

    function getElementRelativePosition(element) {
        if (controlData.orientation == 0) //vertical
        {
            return element.position().top;

        } else { //horizontal
            return element.position().left;
        }
    }

    function createSchedule() {
        controlData.scheduleContainer.empty();
        controlData.scheduleContainer.addClass("total-rooms-" + model.EventDefinition.Rooms);
        controlData.itemSize = getScheduleitemSize();
        controlData.minuteSize = controlData.itemSize / 60;
        controlData.itemMargin = 6 * (controlData.minuteSize);
        controlData.orientation = 1; //0 - vertical //1 - horizontal
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

        //TODO: quitar 30?
        controlData.scheduleContainer.width(model.Times.length * controlData.itemSize + model.Times.length * controlData.itemMargin + 120);
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

    function getScheduleitemSize() {
        return 200;
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
            setElementSize(timeRow, controlData.itemSize);
            setElementMargin(timeRow, controlData.itemMargin);
        }
    }

    function createRoom(roomNumber) {
        var roomContainer = $('<div class="room-column"></div>');
        roomContainer.data('roomNumber', roomNumber);
        controlData.schedule.append(roomContainer);

        if (roomContainer.droppable)
            roomContainer.droppable({
                drop: function (e, ui) {
                    moveSession(ui.draggable, roomNumber);
                },
                accept: function (sessionContainer) {
                    var intersectors = findIntersectors(sessionContainer, '.session-container');
                    var currentSessionId = sessionContainer.data('sessionId');
                    var isValid = true;
                    $(intersectors).each(function (index, otherSession) {
                        var sessionId = $(otherSession).data('sessionId');
                        if (sessionId && sessionId != currentSessionId) {
                            isValid = false;
                        }
                    });

                    if (isValid) {
                        var minPosition = getElementPointA($('.time-gap').first());
                        if (getElementPointA(sessionContainer) < minPosition)
                            isValid = false;
                    }

                    if (isValid)
                        return isValid;
                }
            });

        roomContainer.attr('id', 'room' + roomNumber);
        controlData.rooms.push(roomContainer);
        createGaps(roomContainer, roomNumber);
    }

    function getStarPosition(sessionContainer, gap) {
        var startPosition = getElementPointA(sessionContainer) - getElementPointA(gap);
        startPosition = roundStartPosition(startPosition);
        return startPosition;
    }

    function moveSession(sessionContainer, roomNumber) {
        var gaps = findIntersectors(sessionContainer, $('.time-gap'));
        var firstGap = $(gaps[0]);

        var duration = getDurationByElement(sessionContainer);
        duration = roundDuration(duration);

        var numberOfIntersectionGaps = gaps.length;

        var startPosition = getStarPosition(sessionContainer, firstGap);

        var sessionSize = getSessionSizeByGapsAndDuration(numberOfIntersectionGaps, duration, startPosition);
        setElementSize(sessionContainer, sessionSize);

        var sessionId = sessionContainer.data('sessionId');
        sessionContainer.resizable('destroy');
        var sessionContainerClone = sessionContainer.clone();
        sessionContainer.remove();

        if (sessionContainerClone.hasClass('available-session')) {
            sessionContainerClone.removeClass('available-session');
            setElementSize(sessionContainer, 60 * controlData.minuteSize);
        }


        sessionContainerClone.css('left', 0);
        sessionContainerClone.css('top', 0);

        setElementRelativePosition(startPosition);
        sessionContainerClone.data('sessionId', sessionId);

        firstGap.append(sessionContainerClone);

        ifHasCollisionsCorrectSize(sessionContainerClone);

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
        if (getElementSize(sessionContainer) < ((controlData.itemSize / 2) + 10)) {
            sessionContainer.find('.session-speaker').hide();
        } else {
            sessionContainer.find('.session-speaker').show();
        }

        var timeInfoSpans = sessionContainer.find('.session-time').children();
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
            setElementSize(gap, controlData.itemSize);
            setElementMargin(gap, controlData.itemMargin);
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

    function getSessionSizeByGapsAndDuration(numberOfGapsOccuped, duration) {
        var marginSize = ((numberOfGapsOccuped - 1) * controlData.itemMargin);
        var size = (duration * controlData.minuteSize) + marginSize;
        return size;
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

        gapStartTime = moment.utc($(gapContainer).data('time'));
        var startPosition = (Math.abs(sessionStart.minutes() - gapStartTime.minutes())) * controlData.minuteSize;
        startPosition = roundStartPosition(startPosition);

        var size = getSessionSizeByGapsAndDuration(gapsOccuped, session.Duration, startPosition);


        setElementSize(sessionCard, size);
        setElementRelativePosition(sessionCard, startPosition);

        sessionCard.data('sessionId', session.SessionId);
        sessionPainter.setSessionCardBehavior(sessionCard, controlData, settings, onResizeSession, onStopResizeSession);
        changeSessionUI(sessionCard, sessionStart, session.Duration);
    }

    function onResizeSession(e, ui) {
        var duration = getDurationByElement(ui.element);
        duration = roundDuration(duration);
        var numberOfIntersectionGaps = findIntersectors(ui.element, $('.time-gap')).length;

        var sessionSize = getSessionSizeByGapsAndDuration(numberOfIntersectionGaps, duration);

        setElementSize(ui.element, sessionSize);

        ifHasCollisionsCorrectSize(ui.element);

        var startTime = getTimeByElement(ui.element);

        changeSessionUI(ui.element, startTime, duration);
    }

    function onStopResizeSession(e, ui) {
        ifHasCollisionsCorrectSize(ui.element);

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

    function roundStartPosition(position) {
        var positionInMinutes = position / controlData.minuteSize;
        var groupsOfPrecision = Math.round(positionInMinutes / settings.timePrecision);
        return (groupsOfPrecision * settings.timePrecision) * controlData.minuteSize;
    }

    function updateSession(data) {
        $.post(options.updateAction + '?ts=' + new Date().getTime(), data);
    }

    function ifHasCollisionsCorrectSize(currentSession) {
        if (controlData.orientation == 1) //vertical
            return;

        var currentSessionId = currentSession.data('sessionId');
        //find sessions that intersects, and are not me.
        var intersectors = findIntersectors(currentSession, $('.session-container:not(.available-session)'));
        var gaps, firstGap, lastGap, currentSessionTop, currentSessionBottom;

        $(intersectors).each(function (index, otherSession) {
            var sessionId = $(otherSession).data('sessionId');

            if (sessionId && sessionId != currentSessionId) {
                currentSessionTop = getElementPointA(currentSession);
                currentSessionBottom = getElementPointB(currentSession);

                var otherSessionTop = getElementPointA(otherSession);
                var otherSessionBottom = getElementPointB(otherSessionBottom);

                var isCovering = (currentSessionTop < otherSessionTop) && (otherSessionBottom < currentSessionBottom);

                if (isCovering) {
                    setElementSize(currentSession, getElementSize(currentSession) - (currentSessionBottom - otherSessionTop));

                    currentSessionTop = getElementPointA(currentSession);
                    currentSessionBottom = getElementPointB(currentSession);
                }

                var otherSessionIsDown = (otherSessionTop < currentSessionBottom) && (currentSessionBottom < otherSessionBottom);

                if (otherSessionIsDown) {
                    setElementSize(currentSession, getElementSize(currentSession) - (currentSessionBottom - otherSessionTop));
                }
            }
        });

        gaps = findIntersectors(currentSession, $('.time-gap'));
        lastGap = gaps[gaps.length - 1];
        var lastGapBottom = getElementPointB(lastGap);
        currentSessionBottom = getElementPointB(currentSession);

        var isInMargin = lastGapBottom < currentSessionBottom;

        if (isInMargin) {
            setElementSize(currentSession, getElementSize(currentSession) - (currentSessionBottom - lastGapBottom));
            currentSessionBottom = currentSessionTop + getElementSize(currentSession);
        }

        var isInTheBebiningOfAMargin = (currentSessionBottom - getElementPointA(lastGap) < 2);
        if (isInTheBebiningOfAMargin) {
            setElementSize(currentSession, getElementSize(currentSession) - controlData.itemMargin);
        }

        var duration = getDurationByElement(currentSession);
        duration = roundDuration(duration);
        var size = getSessionSizeByGapsAndDuration(gaps.length, duration);
        setElementSize(currentSession, size);
    }

    function drawSessionInAvailableSessions(availableSessionsContainer, session) {
        if (availableSessionPainter) {
            var sessionCard = availableSessionPainter.getSessionCard(session);
            sessionCard.addClass('available-session');
            setElementSize(sessionCard, controlData.itemSize);
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

        var startPosition = getElementRelativePosition();
        startPosition = roundStartPosition(startPosition);

        var differenceInMinutes = startPosition / controlData.minuteSize;
        var startTime = gapStartTime.add('minutes', differenceInMinutes);
        return startTime;
    }

    function getDurationByElement(element) {
        var intersectionGaps = findIntersectors(element, $('.time-gap'));
        var duration = Math.round((getElementSize(element) - (intersectionGaps.length - 1) * controlData.itemMargin) / controlData.minuteSize);
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