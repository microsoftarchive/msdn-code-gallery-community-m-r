var myEvents = myEvents || {};
myEvents.views = myEvents.views || {};
myEvents.views.attendee = myEvents.views.attendee || {};
myEvents.views.attendee.event = myEvents.views.attendee.event || {};
myEvents.views.attendee.event.schedule = (function () {
   
    $(document).ready(function () {
        var share = new myEvents.controls.Share();
        share.onLinkedin(
            $("#share-linkedin"),
            location.href,
            $(".event-title")[0].innerText,
            'schedule information'
        );
        share.onTwitter(
            $("#share-twitter"),
            $(".event-title")[0].innerText + ': ' + location.href
        );
        share.onFacebook(
            $("#share-facebook"),
            location.href
        );
    });

    return {};
}());
