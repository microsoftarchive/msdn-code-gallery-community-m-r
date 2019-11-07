var myEvents = myEvents || {};
myEvents.controls = myEvents.controls || {};
myEvents.controls.schedule = myEvents.controls.schedule || {};
myEvents.controls.schedule.painters = myEvents.controls.schedule.painters || {};

myEvents.controls.schedule.painters.EditableSessionPainter = function () {
    var self = this;

    self.getSessionCard = function (session, controlData, options) {
        var sessionDiv = $(
            '<div class="session-container">' +
                '<div class="wrapper">' +
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
                '</div>' +
            '</div>');

        sessionDiv.attr('id', session.SessionId);

        return sessionDiv;
    };

    self.setSessionCardBehavior = function (sessionCard, controlData, options, onResize, onStopResize) {
        var width = sessionCard.parent().width();
        sessionCard.resizable({
            maxWidth: width,
            minWidth: width,
            minHeight: 30 * controlData.itemHeight / 60, //30minutes
            handles: 's',
            grid: controlData.timePrecision * controlData.itemHeight / 60, //5minutes
            resize: onResize,
            stop: onStopResize
        });

        sessionCard.draggable({
            revert: "invalid",
            zIndex: 9999,
            containment: 'body',
            helper: 'clone',
            appendTo: 'body',
            refreshPositions: true,
            scroll: true,
            start: function (e, ui) {
                $this = $(this);
                ui.helper.addClass('dragging-helper');
                ui.helper.width($this.width());
                $this.addClass('dragging-item');
                $this.hide();
            },
            stop: function (e, ui) {
                $(this).show()
            }
        });
    };
};
