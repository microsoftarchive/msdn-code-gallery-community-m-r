var myEvents = myEvents || {};
myEvents.views = myEvents.views || {};
myEvents.views.organizer = myEvents.views.organizer || {};
myEvents.views.organizer.session = myEvents.views.organizer.session || {};
myEvents.views.organizer.session.manageMaterials = (function () {

    $(document).ready(function () {

        $('#materialFileUploader').change(function () {
            var sessionId = $('#sessionId').val();
            $(this).upload(myEvents.url.getUrl('UploadMaterial', 'Session', 'Organizer'),
                { sessionId: sessionId },
                function (data) {
                    if (!data)
                        return;
                    $("#materialList").append(data);
                    myEvents.controls.confirm.createConfirms();
                }, 'html');
        });
    });

    return {};
}());
