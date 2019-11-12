//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

//==============================================================================
// EME Support
//==============================================================================

// KeyMessage is in xml format:
//<PlayReadyKeyMessage type="LicenseAcquisition" >
//  <LicenseAcquisition version="1.0" >
//    <Challenge encoding="base64encoded">
//      License challenge
//    </Challenge>
//    <HttpHeaders>
//      <HttpHeader>
//        <name>Content-Type</name>
//        <value>"content type data"</value>
//      </HttpHeader>
//      <HttpHeader>
//        <name>SOAPAction</name>
//        <value>soap action</value>
//      </HttpHeader>
//      ....
//    </HttpHeaders>
//  </LicenseAcquisition>
//</PlayReadyKeyMessage>
function downloadPlayReadyKey(url, keyMessage, callback)
{
    console.log("Parsing key message XML");
    var keyMessageXML = new DOMParser().parseFromString(keyMessage, "application/xml");

    var challenge;
    if (keyMessageXML.getElementsByTagName("Challenge")[0]) {
        challenge = atob(keyMessageXML.getElementsByTagName("Challenge")[0].childNodes[0].nodeValue);
    } else {
        throw "Can not find <Challenge> in key message";
    }

    var headerNames = keyMessageXML.getElementsByTagName("name");
    var headerValues = keyMessageXML.getElementsByTagName("value");
    if (headerNames.length !== headerValues.length) {
        throw "Mismatched header <name>/<value> pair in key message";
    }

    var xhr = new XMLHttpRequest();
    xhr.open("POST", url);
    xhr.responseType = "arraybuffer";
    xhr.onreadystatechange = function () {

    var READYSTATE_COMPLETE = 4;
    var HTTP_STATUS_OK = 200;
    
      
        if (xhr.readyState === READYSTATE_COMPLETE) {
            if (xhr.status === HTTP_STATUS_OK) {
                console.log("Got License Acquisition response");
                callback(new Uint8Array(xhr.response));
            } else {
                throw "XHR failed (" + url + "). Status: " + xhr.status + " (" + xhr.statusText + ")";
            }
        }
    }
    
    for (var i = 0; i < headerNames.length; i++) {
        xhr.setRequestHeader(headerNames[i].childNodes[0].nodeValue, headerValues[i].childNodes[0].nodeValue);
    }

    console.log("Loading PlayReady key from: " + url);
    xhr.send(challenge);
}

/**
* desc@ formatCDMData
*   generate playready CDMData
*   CDMData is in xml format:
*   <PlayReadyCDMData type="LicenseAcquisition">
*     <LicenseAcquisition version="1.0" Proactive="true">
*       <KeyIDs>
*         <KeyID>B64 encoded KID</KeyID>
*         ...
*       </KeyIDs>
*       <CustomData>B64 encoded custom data</CustomData>
*     </LicenseAcquisition>
* </PlayReadyCDMData>        
*/
function formatCDMData(proactiveFlag, kids) 
{   
    var txtProactiveFlag = xmldecode(proactiveFlag);
    var CDMDataStr = "<PlayReadyCDMData type=\"LicenseAcquisition\">" +
                     "<LicenseAcquisition version=\"1.0\" " +
                     "Proactive=\"" + txtProactiveFlag + "\">" +
                     "<KeyIDs>";

    logMsg("format CDM data with KIDS=" + kids);

    for (var i = 0; i < kids.length; i++)
    {
        var txtKid = xmldecode(kids[i]);
        CDMDataStr += "<KeyID>" + txtKid + "</KeyID>";
    }

    CDMDataStr += "</KeyIDs>" +
                  "</LicenseAcquisition></PlayReadyCDMData>";

    logMsg("LA CDMData: " + CDMDataStr);

    var rgbCDMData = stringToByteArray( CDMDataStr, true );
    var int8ArrayCDMdata = new Uint8Array( rgbCDMData );

    return int8ArrayCDMdata;
}

//==============================================================================
// Reactive PlayReadyManager  
//==============================================================================
function PlayReadyManager(vid, url)
{
    this.vid = vid;
    this.url = url;
    this.cdmSession = null;
}

PlayReadyManager.prototype = {
    NEEDKEY_EVENT: "msneedkey",
    KEYMESSAGE_EVENT: "mskeymessage",
    KEYADDED_EVENT: "mskeyadded",
    KEYERROR_EVENT: "mskeyerror",
    KEY_SYSTEM: "com.microsoft.playready",

    setupSession: function (session, isLDL, isRootLicense, uplink) {

        var that = this;

        this.cdmSession = session;

        session.addEventListener(this.KEYMESSAGE_EVENT, function (e) {
            logMsg("Processing key message");
            var url = that.url;

            if( url == undefined || url == null ) {
                url = e.destinationURL;
            }

            if (isRootLicense) {

                // This sample app uses persistent root license
                url += "&UseRootLicense=1";

            } else {

                url += "&CompressedDigitalVideoOPL=300";

                if (!g_persistentLicense) {
                    url += "&UseSimpleNonPersistentLicense=1";
                }

                if (g_chainedLicense ) {
                    url += "&UseChainLicense=1" + "&UplinkKey=" + uplink;
                }

                if (isLDL) {
                    url += "&RealTimeExpiration=1" + "&FirstPlayExpiration=60";
                }
                else {
                    if (g_secureStopEnabled) {
                        url += "&SecureStop=1";
                    }
                }

                if (g_HDCPType1) {
                    url += "&DigitalVideoOutputProtection={ABB2C6F1-E663-4625-A945-972D17B231E7};AAAAAQ==";
                }

                if (!isNaN(g_uncompDigitalVideo)) {
                    url += "&UncompressedDigitalVideoOPL=" + g_uncompDigitalVideo;
                }

            }

            logMsg("LA URL: " + url);

            downloadPlayReadyKey(url, String.fromCharCode.apply(null, new Uint16Array(e.message.buffer)), function (data) {
                console.log("session update");
                session.update(data);
            });
        });

        session.addEventListener(this.KEYADDED_EVENT, function () {
            logMsg("Key successfully added with session ID=" + session.sessionId );
            
            if( that.completeCallback != undefined ) {
                that.completeCallback();
            }

            that.cdmSession.close();
        });

        session.addEventListener(this.KEYERROR_EVENT, function () {
            that.cdmSession.close();
            throw "Unexpected 'keyerror' event from key session. Code: " + session.error.code + ", systemCode: " + session.error.systemCode;
        });
    }

}

//==============================================================================
// ProactivePlayReadyManager
//==============================================================================
var ProactivePlayReadyManager = function (selectedItem, isLDL, isRootLicense)
{
    this.selectedItem = selectedItem;
    this.url = selectedItem.LAURL;
    this.kid1 = selectedItem.kid1;
    this.kid2 = selectedItem.kid2;
    this.isLDL = isLDL;
    this.isRootLicense = isRootLicense;
}

ProactivePlayReadyManager.prototype = new PlayReadyManager();

ProactivePlayReadyManager.prototype.ProactiveLicenseAcquire = function (mediaKeys, callback)
{
    logMsg("Begin proactive LA, LDL=" + this.isLDL
        + " Root=" + this.isRootLicense
        + (this.isRootLicense ? " RootKID=" + this.selectedItem.kidRoot : " KIDs=" + this.selectedItem.kid1 + "," + this.selectedItem.kid2)
        + " ChainLicense=" + g_chainedLicense
        + " Persistent=" + g_persistentLicense
        + " SecureStop=" + g_secureStopEnabled);

    this.completeCallback = callback;

    var kids;
    var cdmData;

    if (this.isRootLicense) {
        // Acquire root license
        kids = [this.selectedItem.kidRoot];
    }
    else {
        // Acquire simple or leaf license
        if (this.isLDL) {
            // Specify the actual KID we're interested in plus several random KIDs
            // converted to Base64
            kids = [
                this.kid1,
                this.kid2,
                "7U3T5cQCRwOXIOHdYYJK/w==",
                "jiTWJ4hvRqyhY/Qb/0FGuw==",
                "iu2oUFL4QNmpg5dK1hvTVA==",
                "ar04sH/+R6u1V7qR22Hk9w==",
                "CFNC+aLpQiaKEcpDaHZFbw==",
                "usETpUJ7Tpi4yIJAyxi7EA==",
                "kuAxXl7PSri2L+6skJr5oA==",
                "nQtOT3PETYWPKrHiIfDvrg==",
                "wxLrW4CSQFO7LgrrnRb7AA=="
            ];
        } else {
            kids = [this.kid1, this.kid2];
        }

        // Delete kid2 from the array if not specified
        if (kids[1] == "null" || kids[1] == "") {
            kids.splice(1, 1);
        }

        // Delete kid1 from the array if not specified
        if (kids[0] == "null" || kids[0] == "null") {
            kids.splice(0, 1);
        }
    }

    var cdmData = formatCDMData('true', kids);
    var emptyArrayofInitData = new Uint8Array();
    
    //initData is optional for createSession if cdmData contains KID
    //For proactive, cdmData must contain the Proactive flag
    var session = mediaKeys.createSession("video/mp4", emptyArrayofInitData, cdmData);
    
    if (!session) {
        throw "Could not create key session";
    }

    this.setupSession(session, this.isLDL, this.isRootLicense, this.selectedItem.kidRoot);
};

//==============================================================================
// ReactivePlayReadyManager
//==============================================================================
var ReactivePlayReadyManager = function (selectedItem, isLDL, isRootLicense)
{
    this.selectedItem = selectedItem;
    this.url = selectedItem.LAURL;
    this.kid1 = selectedItem.kid1;
    this.kid2 = selectedItem.kid2;
    this.isLDL = isLDL;
    this.isRootLicense = isRootLicense;
}

ReactivePlayReadyManager.prototype = new PlayReadyManager();

ReactivePlayReadyManager.prototype.ReactiveLicenseAcquire = function (mediaKeys, intiData, callback)
{
    logMsg("Begin reactive LA, LDL=" + this.isLDL 
        + " Root=" + this.isRootLicense 
        + (this.isRootLicense ? " RootKID=" + this.selectedItem.kidRoot : " KIDs=" + this.selectedItem.kid1 + "," + this.selectedItem.kid2 )
        + " ChainLicense=" + g_chainedLicense
        + " Persistent=" + g_persistentLicense
        + " SecureStop=" + g_secureStopEnabled );

    this.completeCallback = callback;

    var session = mediaKeys.createSession("video/mp4", intiData, null);

    if (!session) {
        throw "Could not create key session";
    }

    this.setupSession(session, this.isLDL, this.isRootLicense, this.selectedItem.kidRoot);
};

