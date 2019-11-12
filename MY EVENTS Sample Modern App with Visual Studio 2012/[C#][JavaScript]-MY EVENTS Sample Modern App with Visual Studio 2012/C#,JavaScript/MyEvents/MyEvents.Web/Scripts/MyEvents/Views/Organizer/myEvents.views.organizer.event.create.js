var myEvents = myEvents || {};
myEvents.views = myEvents.views || {};
myEvents.views.organizer = myEvents.views.organizer || {};
myEvents.views.organizer.event = myEvents.views.organizer.event || {};
myEvents.views.organizer.event.create = (function () {

    $(document).ready(function () {
        $('#RoomNumber').prettyDropDown();
        $('#Date').datePicker();
        $('#StartTime').timePicker();
        $('#EndTime').timePicker();
        $('#TimeZoneOffset').val((moment().zone() * -1) / 60);
        
        $('#localizationMap').bingMap(
            $('#Address'),
            $('#City'),
            $('#ZipCode'),
            $('#Latitude'),
            $('#Longitude')
        );

        $('#logoFileUploader').change(function () {
            $(this).upload(myEvents.url.getUrl('UploadEventLogo', 'Event', 'Organizer'), function (res) {
                $('#HasLogo').val('true');
                $("#logoImage").attr("src", myEvents.url.getUrl('CurrentLogo', 'Event', 'Organizer') + "#" + new Date());
            }, 'html');
        });

        $('#buttons input[type="submit"]').click(function () {
            var formDiv = $('#event-form');
            var formHtml = '<form action="' + myEvents.url.getUrl('Create', 'Event', 'Organizer') + '" method="post" />';
            var form = formDiv.wrapAll(formHtml).parent('form');

            $.validator.unobtrusive.parse(form);

            var isValid = form.valid();
            if (isValid) {
                form.submit();
            }
            formDiv.unwrap();
        });
    });

    return {};
}());
