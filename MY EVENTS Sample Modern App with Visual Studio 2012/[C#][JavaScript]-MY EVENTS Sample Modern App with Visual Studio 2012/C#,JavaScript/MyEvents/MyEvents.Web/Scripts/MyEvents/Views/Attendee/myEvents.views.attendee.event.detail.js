var myEvents = myEvents || {};
myEvents.views = myEvents.views || {};
myEvents.views.attendee = myEvents.views.attendee || {};
myEvents.views.attendee.event = myEvents.views.attendee.event || {};
myEvents.views.attendee.event.detail = (function () {
   
    $(document).ready(function () {
        $('.map').showLocationInBingMap(
            $("#Latitude").val(),
            $("#Longitude").val()
        );
        
        $('#last-tweets').showTweets(
            $("#TwitterAccount").val(),
            6
        );
        
        var share = new myEvents.controls.Share();
        share.onLinkedin(
            $("#share-linkedin"),
            location.href,
            $("#Name").val(),
            $("#Description").val()
        );
        share.onTwitter(
            $("#share-twitter"),
            $("#Name").val() + ': ' + location.href,
            $("#TwitterAccount").val()
        );
        share.onFacebook(
            $("#share-facebook"),
            location.href
        );
    });

    return {};
}());
