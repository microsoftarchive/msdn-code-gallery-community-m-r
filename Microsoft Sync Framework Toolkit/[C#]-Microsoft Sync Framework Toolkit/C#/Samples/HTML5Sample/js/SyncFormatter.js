// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

// SyncFormatter is the class to serialize/deserialize between entity and its
// wire format as defined in the sync protocol
function SyncFormatter() {
    // Flag to indicate whether there is more change
    this.moreChangesAvailable = false;
    // Server blob
    this.serverBlob = "";
    // List of updated entities
    this.results = new Array();
    // Deserialize payload to entity
    this.parse = function (str) {
        try {
            var data = JSON.parse(str);
            this.moreChangesAvailable = data.d.__sync.moreChangesAvailable;
            this.serverBlob = data.d.__sync.serverBlob;
            this.results = data.d.results;
            return true;
        } catch (err) { }
        return false;
    };
    // Serialize entity to payload object
    this.dataObject = function () {
        var data = new Object();
        data.d = new Object();
        data.d.__sync = new Object();
        data.d.__sync.moreChangesAvailable = this.moreChangesAvailable;
        data.d.__sync.serverBlob = this.serverBlob;
        data.d.results = this.results;
        return data;
    };
    // Serialize payload object to its wire format
    this.toString = function () {
        return JSON.stringify(this.dataObject());
    };
    // Send request to sync service
    this.sendRequest = function (serviceUri, successCallback, errorCallback, dir) {
        TraceObj("[" + dir + " Request]:", serviceUri, this.dataObject());
        // Construct HTTP POST request
        var xmlHttp = new XMLHttpRequest();
        xmlHttp.open("POST", serviceUri);
        xmlHttp.setRequestHeader("Accept", "application/json");
        xmlHttp.setRequestHeader("Content-Type", "application/json");
        // Handle success & error response from server and then callback
        xmlHttp.onreadystatechange = function () {
            if (xmlHttp.readyState == 4) {
                if (xmlHttp.status == 200) {
                    var res = new SyncFormatter();
                    if (res.parse(xmlHttp.responseText)) {
                        TraceObj("[" + dir + " Response]:", serviceUri, res.dataObject());
                        successCallback(res);
                        return;
                    }
                }
                TraceMsg("[" + dir + " Response Error]: ", xmlHttp.status + " Response: " + xmlHttp.responseText);
                errorCallback(xmlHttp.responseText);
            }
        };
        xmlHttp.send(this.toString());
    };
}