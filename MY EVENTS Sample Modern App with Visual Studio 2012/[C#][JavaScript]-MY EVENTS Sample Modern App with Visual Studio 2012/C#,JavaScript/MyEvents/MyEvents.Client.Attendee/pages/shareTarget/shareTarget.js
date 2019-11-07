(function () {
    "use strict";
    WinJS.strictProcessing();

    var app = WinJS.Application;
    var share;
    var thumbnailBlob;
    var selectedEventId;
    var selectedSessionId;
    var nomberOfElements = 5;

    function createOption(ddl, text, value, selected) {
        var opt = document.createElement('option');
        opt.value = value;
        opt.text = text;
        opt.selected = selected;
        ddl.options.add(opt);
    }

    function setEventsDataSource(data) {
        if (data) {
            var eventList = document.querySelector(".shareEventList");
            if (data.length > 0) {
                eventList.size = nomberOfElements;
                if (data.length < nomberOfElements) {
                    eventList.size = data.length;
                }

                selectedEventId = data[0].EventDefinitionId;
                MyEvents.Services.SessionService.getSessions(selectedEventId)
                    .then(setSessionsDataSource);

                for (var i = 0; i < data.length; i++) {
                    var selected = false;
                    if (i === 0) {
                        selected = true;
                    }
                    createOption(eventList, data[i].Name, data[i].EventDefinitionId, selected);
                }
            }
        }
    };

    function setSessionsDataSource(data) {
        if (data) {
            var sessionList = document.querySelector(".shareSessionList");
            sessionList.innerHTML = "";
            if (data.length > 0) {
                sessionList.size = nomberOfElements;
                if (data.length < nomberOfElements) {
                    sessionList.size = data.length;
                }
                selectedSessionId = data[0].SessionId;
                for (var i = 0; i < data.length; i++) {
                    var selected = false;
                    if (i === 0) {
                        selected = true;
                    }
                    
                    if (data[i].RoomNumber > 0) {
                        createOption(sessionList, data[i].Title, data[i].SessionId, selected);
                    }
                }
            }
        }
    };

    function handleEventChange(evt) {
        selectedEventId = evt.target.value;
        MyEvents.Services.SessionService.getSessions(selectedEventId).then(setSessionsDataSource);
    }

    function handleSessionChange(evt) {
        selectedSessionId = evt.target.value;
    }

    function onShareSubmit() {
        document.querySelector(".progressindicators").style.visibility = "visible";
        document.querySelector(".commentbox").disabled = true;
        document.querySelector(".submitbutton").disabled = true;

        if (selectedEventId) {
            var container = selectedEventId;
            var formatter = new Windows.Globalization.DateTimeFormatting.DateTimeFormatter("longdate");
            var timestamp = formatter.format(new Date());
            var imageName = "note_" + container + "_" + makeid() + ".jpg";

            var note = {
                comment: document.querySelector(".commentbox").value,
                imageName: imageName,
                date: timestamp,
                picture: "ms-appdata:///local/" + container + "/" + imageName,
                sessionId: selectedSessionId
            };

            MyEvents.Storage.saveSettingIntoContainer(container, imageName, note);
            MyEvents.Storage.saveFile(container + "\\" + imageName, thumbnailBlob);
        }

        share.reportCompleted();
    }

    function makeid() {
        var text = "";
        var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        for (var i = 0; i < 5; i++)
            text += possible.charAt(Math.floor(Math.random() * possible.length));

        return text;
    }

    // This function responds to all application activations.
    app.onactivated = function (args) {
        if (args.detail.kind === Windows.ApplicationModel.Activation.ActivationKind.shareTarget) {
            MyEvents.Services.EventService.getCurrentEvents()
                .then(setEventsDataSource);

            document.querySelector(".submitbutton").onclick = onShareSubmit;
            share = args.detail.shareOperation;

            document.querySelector(".shareEventList").onchange = handleEventChange;
            document.querySelector(".shareSessionList").onchange = handleSessionChange;

            if (share.data.properties.thumbnail) {
                share.data.properties.thumbnail.openReadAsync().done(function (thumbnailStream) {
                    thumbnailBlob = MSApp.createBlobFromRandomAccessStream(thumbnailStream.contentType, thumbnailStream);
                });
            }
        }
    };

    app.start();
})();
