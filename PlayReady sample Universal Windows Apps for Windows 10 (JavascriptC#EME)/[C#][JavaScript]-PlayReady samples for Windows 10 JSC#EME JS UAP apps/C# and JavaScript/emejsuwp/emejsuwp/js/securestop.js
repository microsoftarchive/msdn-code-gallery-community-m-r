//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

/**
* desc@ formatSecureStopCDMData
*   generate playready CDMData
*   CDMData is in xml format:
*   <PlayReadyCDMData type="SecureStop">
*     <SecureStop version="1.0">
*       <SessionID>B64 encoded session ID</SessionID>
*       <CustomData>B64 encoded custom data</CustomData>
*       <ServerCert>B64 encoded server cert</ServerCert>
*     </SecureCert>
* </PlayReadyCDMData>        
*/
function formatSecureStopCDMData(encodedSessionId, customData, encodedPublisherCert)
{
    var rgbCustomData = stringToByteArrayW(customData);
    var uint16arrayCustomData = new Uint16Array(rgbCustomData);
    var customDataW = String.fromCharCode.apply(null, uint16arrayCustomData);
    var encodedCustomData = Base64.encode(customDataW);

    var CDMDataStr = "<PlayReadyCDMData type=\"SecureStop\">" +
                     "<SecureStop version=\"1.0\" >" +
                     "<SessionID>" + encodedSessionId + "</SessionID>" +
                     "<CustomData>" + encodedCustomData + "</CustomData>" +
                     "<ServerCert>" + encodedPublisherCert + "</ServerCert>" +
                     "</SecureStop></PlayReadyCDMData>";

    logMsg("Secure Stop CDMData: " + CDMDataStr);

    var rgbCDMData = stringToByteArrayW(CDMDataStr);
    var int8ArrayCDMdata = new Uint8Array(rgbCDMData);

    return int8ArrayCDMdata;
}

function GetValue(xmldata, begintag, endtag)
{
    var beginPos = xmldata.indexOf(begintag);
    var endPos = xmldata.indexOf(endtag);
    if (beginPos != -1 && endPos != -1) {
        return xmldata.substring(beginPos + begintag.length, endPos);
    }
    else {
        return null;
    }
}

// The custom data is server specific that varying from server to server.
// This function bases on the custom data from directtaps server.
function listResponseCustomData(response)
{
    var customDataTagStart = "<CustomData>";
    var customDataEnd = "</CustomData>";

    var recordBeginPos = response.indexOf(customDataTagStart, 0);
    var recordEndPos = response.indexOf(customDataEnd, 0);

    if (recordBeginPos == -1 || recordEndPos == -1) {
        logMsg("Warning: Unable to find the custom data tag in the secure stop response!");
        return;
    }

    var responseCustomData = response.substring(recordBeginPos + customDataTagStart.length, recordEndPos - 1)

    var responseXmlData = responseCustomData.replace(/&lt;/g, "<").replace(/&gt;/g, ">");

    var length = responseXmlData.length;
    var startpos = 0;
    var recStartTag = "<SecureStopRecord";
    var recEndTag = "/SecureStopRecord>";

    while (startpos < length) {
        var recordbeginpos = responseXmlData.indexOf(recStartTag, startpos);
        var recordendpos = responseXmlData.indexOf(recEndTag, startpos);

        if (recordbeginpos == -1 || recordendpos == -1) {
            break;
        }
        else {
            var sr = null;
            var msg = "";
            var recordstring = responseXmlData.substring(recordbeginpos, recordendpos + recEndTag.length);

            startpos = recordendpos + recEndTag.length;

            var sidString = GetValue(recordstring, "<SessionID>", "</SessionID>");
            if (sidString != null) {
                sidString = '{' + sidString + '}';
                msg += "Found secure stop session Id=" + sidString;
            }
            else {
                logMsg("Error: Session ID does not exist!");
                return;
            }

            var startTimeString = GetValue(recordstring, "<StartTime>", "</StartTime>");
            if (startTimeString != null) {
                msg += ", StartTime=" + startTimeString;
            }
            else {
                logMsg("Error: Start Time does not exist!");
                return;
            }

            var updateTimeString = GetValue(recordstring, "<UpdateTime>", "</UpdateTime>");
            if (updateTimeString != null) {
                msg += ", UpdateTime=" + updateTimeString;
            }
            else {
                logMsg('Error: Update Time does not exist!');
                return;
            }

            var stoppedString = GetValue(recordstring, "<Stopped>", "</Stopped>");
            if (stoppedString != null) {
                msg += ", Stopped=" + stoppedString;
            }
            else {
                logMsg("Error: Stopped does not exist");
                return;
            }

            logMsg( msg );
        }
    }
}

//==============================================================================
// PlayReadySecureStopManager
//==============================================================================
function PlayReadySecureStopManager(url) {
    this.url = url;
}

PlayReadySecureStopManager.prototype = {
    NEEDKEY_EVENT: "msneedkey",
    KEYMESSAGE_EVENT: "mskeymessage",
    KEYADDED_EVENT: "mskeyadded",
    KEYERROR_EVENT: "mskeyerror",
    KEY_SYSTEM: "com.microsoft.playready",

    setupSession: function (session) {

        var that = this;

        session.addEventListener(this.KEYMESSAGE_EVENT, function (e) {
            logMsg("Secure Stop: Processing key message");
            var url = that.url;

            if (url == undefined || url == null) {
                url = e.destinationURL;
            }

            downloadPlayReadyKey(url, String.fromCharCode.apply(null, new Uint16Array(e.message.buffer)), function (data) {
                console.log("session update");
                session.update(data);
                listResponseCustomData(String.fromCharCode.apply(null, data));
            });
        });

        session.addEventListener(this.KEYADDED_EVENT, function () {
            logMsg("Secure stop: Key successfully added with session ID=" + session.sessionId);

            if (that.completeCallback != undefined) {
                that.completeCallback();
            }
        });

        session.addEventListener(this.KEYERROR_EVENT, function () {
            if (session.error.systemCode == -2147024637) { // -2147024637 == DRM_E_NOMORE
                logMsg("No secure stop session to process.");
            }
            else {
                throw "SecureStop: Unexpected 'keyerror' event from key session. Code: " + session.error.code + ", systemCode: " + session.error.systemCode;
            }
        });
    }

}

function ProcessSecureStop(mediaKeys, url)
{
    // Frist read the secure stop cert from MeteringCert.txt 
    var meteringCertFile = 'MeteringCert.txt';
    var package = Windows.ApplicationModel.Package.current;
    var installedLoc = package.installedLocation;

    logMsg('Read metering cert file from \"' + installedLoc.path + meteringCertFile + '\"');

    var getMewteringCertAsync = function () {
        return installedLoc.getFileAsync(meteringCertFile).then(
            function (dataFile) {
                return Windows.Storage.FileIO.readBufferAsync(dataFile);
            });
    };

    getMewteringCertAsync().then(function (buffer) {
        // Use a dataReader object to read from the buffer
        var dataReader = Windows.Storage.Streams.DataReader.fromBuffer(buffer);
        var certBytes = new Array(buffer.length);
        dataReader.readBytes(certBytes);
        dataReader.close();

        // Base64 encode the cert contained in buffer
        var certB64Encoded = Base64.encodeBytes(certBytes);

        //
        // Create a CDM data to request all the secure stop sessions 
        // Use "*" to indicate all sessions
        //
        var rgbAsterisk = stringToByteArrayW("*");
        var uint16arrayAsterisk = new Uint16Array(rgbAsterisk);
        var asteriskW = String.fromCharCode.apply(null, uint16arrayAsterisk);
        var sessionId = Base64.encode(asteriskW);

        // Custom data is server specific, provide any addition data required by the server if there is any
        var customData = "This is a custom data.";

        var int8ArrayCDMdata = formatSecureStopCDMData(sessionId, customData, certB64Encoded);

        // Secure stop doesn't use init data, uses empty array.
        var emptyArrayofInitData = new Uint8Array();

        var keySession = mediaKeys.createSession("video/mp4", emptyArrayofInitData, int8ArrayCDMdata);

        var secureStopManager = new PlayReadySecureStopManager(url);

        secureStopManager.setupSession(keySession);
    });
}
