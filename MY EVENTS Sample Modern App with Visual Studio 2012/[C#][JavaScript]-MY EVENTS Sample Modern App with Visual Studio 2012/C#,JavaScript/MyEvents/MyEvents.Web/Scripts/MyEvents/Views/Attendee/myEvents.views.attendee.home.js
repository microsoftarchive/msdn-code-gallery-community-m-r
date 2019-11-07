var myEvents = myEvents || {};
myEvents.views = myEvents.views || {};
myEvents.views.attendee = myEvents.views.attendee || {};
myEvents.views.attendee.home = (function () {
   
    $(document).ready(function () {
        $('.multi-line-ellipsis').dotdotdot({
            watch: 'window'
        });
    });

    return {};
}());
