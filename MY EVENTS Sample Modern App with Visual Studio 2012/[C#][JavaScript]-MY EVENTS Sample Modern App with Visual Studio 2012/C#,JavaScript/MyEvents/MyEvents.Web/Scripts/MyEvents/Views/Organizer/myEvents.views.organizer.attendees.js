var myEvents = myEvents || {};
myEvents.views = myEvents.views || {};
myEvents.views.organizer = myEvents.views.organizer || {};
myEvents.views.organizer.attendees = (function () {
   
    $(document).ready(function () {
        $('.multi-line-ellipsis').dotdotdot({
            watch: 'window'
        });
    });

    return {};
}());
