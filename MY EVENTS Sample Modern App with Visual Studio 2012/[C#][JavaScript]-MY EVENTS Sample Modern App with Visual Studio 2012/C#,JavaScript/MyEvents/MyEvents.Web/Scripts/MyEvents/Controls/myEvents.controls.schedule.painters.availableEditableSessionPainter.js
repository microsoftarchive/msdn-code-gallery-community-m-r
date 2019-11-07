var myEvents = myEvents || {};
myEvents.controls = myEvents.controls || {};
myEvents.controls.schedule = myEvents.controls.schedule || {};
myEvents.controls.schedule.painters = myEvents.controls.schedule.painters || {};



myEvents.controls.schedule.painters.AvailableEditableSessionPainter = function () {
    var self = this;

    self.getSessionCard = function (session) {
        var sessionDiv = $(
            '<div class="session-container">' +
                '<div class="wrapper">' +
                    '<div class="left session-title multi-line-ellipsis" title="' + session.Title + '">' +
                        '<span>' + session.Title + '</span>' +
                    '</div>' +
                    '<div class="right session-time">' +
                        '<span></span>' +
                        '<span></span>' +
                    '</div>' +
                    '<div class="clear session-speaker">' +
                        '<span class="ellipsis">' + session.Speaker + '</span>' +
                    '</div>' +
                '</div>' +
            '</div>');
        
        sessionDiv.attr('id', session.SessionId);

        return sessionDiv;
    };

    self.setSessionCardBehavior = function (sessionCard, controlData, options, onResize, onStopResize) {
        var width = sessionCard.parent().width();
        //TODO: DRY
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