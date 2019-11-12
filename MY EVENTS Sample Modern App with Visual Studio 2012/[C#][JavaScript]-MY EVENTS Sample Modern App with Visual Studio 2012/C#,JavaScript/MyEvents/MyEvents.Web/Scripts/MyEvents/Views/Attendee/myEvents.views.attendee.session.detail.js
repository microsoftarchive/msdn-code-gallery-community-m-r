var myEvents = myEvents || {};
myEvents.views = myEvents.views || {};
myEvents.views.attendee = myEvents.views.attendee || {};
myEvents.views.attendee.session = myEvents.views.attendee.session || {};
myEvents.views.attendee.session.detail = (function () {
   
    $(document).ready(function () {
        var share = new myEvents.controls.Share();
        share.onLinkedin(
            $("#share-linkedin"),
            location.href,
            $("#Title").val(),
            $("#Description").val()
        );
        share.onTwitter(
            $("#share-twitter"),
            $("#Title").val() + ': ' + location.href,
            $("#TwitterAccount").val()
        );
        share.onFacebook(
            $("#share-facebook"),
            location.href
        );
    });

    return {};
}());
