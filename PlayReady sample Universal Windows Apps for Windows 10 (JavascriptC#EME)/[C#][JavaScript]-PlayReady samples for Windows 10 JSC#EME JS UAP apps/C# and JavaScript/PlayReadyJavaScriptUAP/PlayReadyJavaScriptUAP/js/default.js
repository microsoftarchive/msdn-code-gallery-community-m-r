//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

//Global Variables
var g_fQueryProperty = true;

var g_TotalNumOfVideo = 6;
var g_ActualNumOfVideo = 1;
var g_PreviousNumOfVideo = 0;
var g_contentList = [];
var g_explicitGuidList = [];
var g_playReadyPlay = [];
var myJSONObject;
var g_timeBuffer = 20000; // 20 seconds

var g_enabelSecureStopButton = false;

//set this to true to use ManualEnable via BeginServiceRequest
var g_fUseManualEnabling = false;

var g_PlayEnablerGUIDList = {
    "None": "None",
    "UnknownOutput": "{786627d8-c2a6-44be-8f88-08ae255b01a7}",
    "UnknownOutputConstrained": "{b621d91f-edcc-4035-8d4b-dc71760d43e9}",
};

var g_VideoOPLTypes = ["UncompressedDigitalVideoOPL", "CompressedDigitalVideoOPL", "AnalogVideoOPL"];

var g_AudioOPLTypes = ["UncompressedDigitalAudioOPL", "CompressedDigitalAudioOPL"];

var g_OPLValues = ["None", "0", "100", "150", "200", "201", "250", "270", "300"];

var g_byteOneExplicitGuidList = ["{225CD36F-F132-49EF-BA8C-C91EA28E4369}",
                                 "{2098DE8D-7DDD-4BAB-96C6-32EBB6FABEA3}",
                                 "{C3FD11C6-F8B7-4D20-B008-1DB17D61F2DA}",
                                 "{6D5CFA59-C250-4426-930E-FAC72C8FCFA6}"];

var g_licenseSecurityLevel = ["150", "2000", "3000"];

// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509
(function () {
	"use strict";

	var app = WinJS.Application;
	var activation = Windows.ApplicationModel.Activation;

	app.onactivated = function (args) {
		if (args.detail.kind === activation.ActivationKind.launch) {
			if (args.detail.previousExecutionState !== activation.ApplicationExecutionState.terminated) {
			    // This application has been newly launched. Initialize your application here.

			    var addEvent = function (elem, type, eventHandle) {
			        if (elem == null || typeof (elem) == 'undefined') return;
			        if (elem.addEventListener) {
			            elem.addEventListener(type, eventHandle, false);
			        } else if (elem.attachEvent) {
			            elem.attachEvent("on" + type, eventHandle);
			        } else {
			            elem["on" + type] = eventHandle;
			        }
			    };

			    addEvent(window, "resize", resize);

			    readMediaListAsync();

			    AddEventHandler();

			    setSoftwareOverride_And_cbOptOutHWDRM();
			    initilize();

			} else {
				// TODO: This application was suspended and then terminated.
				// To create a smooth user experience, restore application state here so that it looks like the app never stopped running.
			}
			args.setPromise(WinJS.UI.processAll());
		}
	};

	app.oncheckpoint = function (args) {
		// TODO: This application is about to be suspended. Save any state that needs to persist across suspensions here.
		// You might use the WinJS.Application.sessionState object, which is automatically saved and restored across suspension.
		// If you need to complete an asynchronous operation before your application is suspended, call args.setPromise().
	};

	app.start();
})();

function setSelectedValue(selectObj, valueToSet) {
    for (var i = 0; i < selectObj.options.length; i++) {
        if (selectObj.options[i].text == valueToSet) {
            selectObj.options[i].selected = true;
            return;
        }
    }
}

function AddEventHandler() {

    // Register click handlers for each of the buttons
    var btnPlay = document.getElementById("btnPlay");
    btnPlay.addEventListener("click", btnPlay_click, false);

    var btnProactiveLicenseAcquisition = document.getElementById("btnProactiveLicenseAcquisition");
    btnProactiveLicenseAcquisition.addEventListener("click", btnProactiveLA_click, false);

    var btnSecureStop = document.getElementById("btnSecureStop");
    btnSecureStop.addEventListener("click", btnSecureStop_click, false);

    var video0 = document.getElementById("video0");
    video0.addEventListener("ended", myVideo_onEnded, false);

    var btnDeleteHDS = document.getElementById("btnDeleteHDS");
    btnDeleteHDS.addEventListener("click", btnDeleteHDS_click, false);

    var cbOptOutHWDRM = document.getElementById("cbOptOutHWDRM");
    cbOptOutHWDRM.addEventListener("click", cbOptOutHWDRMOnClick, false);

    var selectMediaFile0 = document.getElementById('selectMediaFile0');
    selectMediaFile0.addEventListener("change", selectMediaFileChange0, false);

    var selectMediaFile1 = document.getElementById('selectMediaFile1');
    selectMediaFile1.addEventListener("change", selectMediaFileChange1, false);

    var selectMediaFile2 = document.getElementById('selectMediaFile2');
    selectMediaFile2.addEventListener("change", selectMediaFileChange2, false);

    var selectMediaFile3 = document.getElementById('selectMediaFile3');
    selectMediaFile3.addEventListener("change", selectMediaFileChange3, false);

    var selectMediaFile4 = document.getElementById('selectMediaFile4');
    selectMediaFile4.addEventListener("change", selectMediaFileChange4, false);

    var selectMediaFile5 = document.getElementById('selectMediaFile5');
    selectMediaFile5.addEventListener("change", selectMediaFileChange5, false);

    var selectExtraVideoCount = document.getElementById('selectExtraVideoCount');
    selectExtraVideoCount.addEventListener("change", selectExtraVideoCountChange, false);
    cbUserInput.addEventListener('change', userInputChange, false);

    var bOptOutHWDRM = document.getElementById('bOptOutHWDRM');
    bOptOutHWDRM.addEventListener("click", bOptOutHWDRM_click, false);

    var bPersistentLicense = document.getElementById('bPersistentLicense');
    bPersistentLicense.addEventListener("click", bPersistentLicense_click, false);

    var bBoundToDomain = document.getElementById('bBoundToDomain');
    bBoundToDomain.addEventListener("click", bBoundToDomain_click, false);

    var bUserInput = document.getElementById('bUserInput');
    bUserInput.addEventListener("click", bUserInput_click, false);

    var bBesteffortCGMS_A = document.getElementById('bBesteffortCGMS_A');
    bBesteffortCGMS_A.addEventListener("click", bBesteffortCGMS_A_click, false);

    var bCGMS_A = document.getElementById('bCGMS_A');
    bCGMS_A.addEventListener("click", bCGMS_A_click, false);

    var bDigitalToken = document.getElementById('bDigitalToken');
    bDigitalToken.addEventListener("click", bDigitalToken_click, false);

    var bAGCColorStrip = document.getElementById('bAGCColorStrip');
    bAGCColorStrip.addEventListener("click", bAGCColorStrip_click, false);

    var bMAX_VGA_RESOLUTION = document.getElementById('bMAX_VGA_RESOLUTION');
    bMAX_VGA_RESOLUTION.addEventListener("click", bMAX_VGA_RESOLUTION_click, false);

    var bMAX_COMPONENT_RESOLUTION = document.getElementById('bMAX_COMPONENT_RESOLUTION');
    bMAX_COMPONENT_RESOLUTION.addEventListener("click", bMAX_COMPONENT_RESOLUTION_click, false);

    var bSCMS = document.getElementById('bSCMS');
    bSCMS.addEventListener("click", bSCMS_click, false);

    var bHDCPType1 = document.getElementById('bHDCPType1');
    bHDCPType1.addEventListener("click", bHDCPType1_click, false);
}

function userInputChange() {
    if (cbUserInput.checked) {
        document.getElementById("talaURL").hidden = false;
        document.getElementById("txtlaURL").hidden = false;
    }
    else {
        document.getElementById("talaURL").hidden = true;
        document.getElementById("txtlaURL").hidden = true;
    }
}

function resize() {

    var presentWidth = window.innerWidth;
    var presentHeight = window.innerHeight;
}

function uninitilize() {
    logMsg('Begin uninitilize');
    var videoFrame = document.getElementById('videoFrame');
    var videoFrames = document.getElementById('videoFrames');

    for (var i = 0; i < g_ActualNumOfVideo; i++) {
        g_playReadyPlay[i].uninitilize();
        g_playReadyPlay[i] = null;

        var videotag = 'video' + i;
        var videoElement = document.getElementById(videotag);
        if (i == 0) {
            videoFrame.removeChild(videoElement);
            videoFrame.appendChild(videoElement);
        }
        else {
            videoFrames.removeChild(videoElement);
            videoFrames.appendChild(videoElement);
        }
    }
    logMsg('End initilize');
}

function initilize() {
    logMsg('Begin initilize');

    //document.getElementById("talaURL").value = "";
    document.getElementById("taInputMediaFile").value = "";
    //reset mediaFile dropdown menu
    var MediaFileOptions = document.getElementById('selectMediaFile0').options;
    if (MediaFileOptions.length > 0) {
        MediaFileOptions[0].selected = true;
        for (var i = 1; i < MediaFileOptions.length; i++) {
            MediaFileOptions[i].selected = false;
        }
    }

    //reset extra video dropdown menu
    var VideoOptions = document.getElementById('selectExtraVideoCount').options;
    if (VideoOptions.length > 0) {
        VideoOptions[0].selected = true;
        for (var i = 1; i < VideoOptions.length; i++) {
            VideoOptions[i].selected = false;
        }
    }

    for (var i = 0; i < g_TotalNumOfVideo; i++) {
        g_playReadyPlay[i] = new PlayReadyPlay(i);
        if (i != 0) {
            g_playReadyPlay[i].HideVideos();
        }
    }

    selectExtraVideoCountChange();
    logMsg('End initilize');

}

function deleteHDS(complete) {
    Windows.Storage.ApplicationData.current.localFolder.getFolderAsync("playready").then(function (folder) {
        return folder.deleteAsync();
    }).done(function () {
        logMsg('License store has been deleted');
        if (complete != null) {
            complete();
        }
    }, function (e) {
        if (e.number == -2147024894) // ignore file not found error (0x80070002)
        {
            if (complete != null) {
                complete();
            }
        }
        else {
            logException('exception(deleteHDS):', e);
            var msg = "Delete license store failed.  Please close the app and relaunch it, then try delete license store again!"
            logMsg('\n ' + msg);

            // popup a message dialog to show the error
            var md = new Windows.UI.Popups.MessageDialog(msg);
            md.showAsync();
        }
    });
}

//this function is not working when playing any content
function btnDeleteHDS_click() {
    uninitilize();
    clearLog();
    deleteHDS();
    initilize();
}

function clearLog() {
    var taLog = document.getElementById('myText');
    taLog.style.color = "black";
    taLog.value = '';
}


function sleep(milliseconds) {
    var dt = new Date();
    while ((new Date()) - dt <= milliseconds) { /* Do nothing */ }
}

function getContentIndexForVideo(videoIndex) {
    logMsg('videoIndex=' + videoIndex);
    var videotag = 'video' + videoIndex.toString();
    var selectMediaFile = 'selectMediaFile' + videoIndex.toString();
    var idx = document.getElementById(selectMediaFile).value;
    return idx;
}

function setUserInputContentAndUrl() {
    document.getElementById("talaURL").hidden = false;
    document.getElementById("txtlaURL").hidden = false;

    var content = document.getElementById("taInputMediaFile").value;
    var url = document.getElementById("talaURL").value;
    if (content == null || content == "") {
        logException("setUserInputContentAndUrl", "taInputMediaFile is empty");
    }

    g_contentList[0] = {
        contentIndex: 0,
        uniqueId: "userInput",
        content: content,
        laurl: url,
        kid: "null",
        uplinkKey: "null",
        contentKey: "null",
        domainUrl: "null",
        serviceId: "null",
        domainId: "null",
        customData: "null",
        firstPlayExpiration: "null",
        actualLaurl: url,
        secureStopCertFile: "null"
    };

}

function enableSecureStopButton(cert) {
    btnSecureStop.disabled = true;

    if (cert == 'null') {
        btnSecureStop.title = "This button is available for secure stop content only.";
        g_enabelSecureStopButton = false;
    }
    else {
        g_enabelSecureStopButton = true;
        btnSecureStop.title = "This button will be enabled when the playback ends.";
    }
}

function btnPlay_click() {
    clearLog();

    // Disable cbOptOutHWDRM button
    var cbOptOutHWDRM = document.getElementById("cbOptOutHWDRM");
    cbOptOutHWDRM.disabled = true;
    var bOptOutHWDRM = document.getElementById("bOptOutHWDRM");
    bOptOutHWDRM.style.color = "Gray";

    try {

        logMsg('g_ActualNumOfVideo =' + g_ActualNumOfVideo);

        // Change the button's border back to normal
        var btnPlay = document.getElementById('btnPlay');
        btnPlay.style.border = "";

        if (cbUserInput.checked) {
            enableSecureStopButton(g_contentList[0].secureStopCertFile);

            setUserInputContentAndUrl();
            g_playReadyPlay[0].initialize(g_contentList[0]);
            g_playReadyPlay[0].setSource();
        }
        else {
            for (var videoIndex = 0; videoIndex < g_ActualNumOfVideo; videoIndex++) {
                var contentidx = getContentIndexForVideo(videoIndex);

                // The secure stop button applies to the primay video.
                if (videoIndex == 0) {
                    enableSecureStopButton(g_contentList[contentidx].secureStopCertFile);
                }

                if (g_contentList[contentidx].kid != 'null') {
                    g_playReadyPlay[videoIndex].initialize(g_contentList[contentidx]);

                    logMsg('Use kid set in data configuration file to pre-acquire license(s), then play');
                    if (g_contentList[contentidx].firstPlayExpiration != 'null') {
                        //pre-acquire a short duration license which will expire in firstPlayExpiration seconds
                        g_playReadyPlay[videoIndex].proactivePlay(true);
                    }
                    else {
                        g_playReadyPlay[videoIndex].proactivePlay();
                    }
                }
                else {
                    //reactive acquire license
                    g_playReadyPlay[videoIndex].initialize(g_contentList[contentidx]);
                    g_playReadyPlay[videoIndex].setLicenseRights();
                    g_playReadyPlay[videoIndex].setSource();
                }

                if (g_contentList[contentidx].firstPlayExpiration != 'null') {

                    var expiration = parseInt(g_contentList[contentidx].firstPlayExpiration, 10);
                    var timeout = expiration * 1000 - g_timeBuffer;
                    if (timeout < 0) {
                        timeout = 0;
                    }
                    var thatVideoIndex = videoIndex;
                    var thatContentidx = contentidx;
                    setTimeout(function () {
                        //when firstPlayExpiration has non-null value, a SDL has been acquired before, 
                        //acquire a long duration license g_timeBuffer seconds before previous license expired.
                        g_playReadyPlay[thatVideoIndex].setContentInfo(g_contentList[thatContentidx]);
                        if (g_playReadyPlay[thatVideoIndex].proactiveServiceRequest != undefined &&
                            g_playReadyPlay[thatVideoIndex].proactiveServiceRequest != null) {
                            g_playReadyPlay[thatVideoIndex].proactiveServiceRequest.proactiveLicenseAcquistion(null, false);
                        }
                        else {
                            logException('exception(btnPlay_click:proactiveServiceRequest):', "proactiveServiceRequest is null");
                        }
                    }, timeout);
                }
            }
        }
    }
    catch (e) {
        logException('exception(btnPlay_click):', e);
    }
}

function btnProactiveLA_click() {
    clearLog();
    for (var videoIndex = 0; videoIndex < g_ActualNumOfVideo; videoIndex++) {
        var videotag = "video" + videoIndex;
        var contentidx = getContentIndexForVideo(videoIndex);
        g_playReadyPlay[videoIndex].initialize(g_contentList[contentidx]);

        if (g_contentList[contentidx].kid != 'null') {
            var proactiveServiceRequest = new ProactiveServiceRequest(videoIndex, g_contentList[contentidx]);
            proactiveServiceRequest.proactiveIndivChain(function () { return proactiveServiceRequest.proactiveLicenseAcquistion() });
        }
    }
}

function btnSecureStop_click() {
    clearLog();

    btnSecureStop.disabled = true;

    // Secure stop applies to the main video only (i.e. video index 0)
    var proactiveSR = new ProactiveServiceRequest(0, g_contentList[getContentIndexForVideo(0)]);
    proactiveSR.proactiveIndivChain(function () { proactiveSR.proactiveSecureStop() });
}

function myVideo_onEnded() {

    if (g_enabelSecureStopButton) {
        btnSecureStop.disabled = false;
    }

    logMsg('My Video onEnded()');
}

function populateOPLList(tagId) {
    var oplValueDropdown = document.getElementById(tagId);
    for (var idx = 0; idx < g_OPLValues.length; idx++) {
        oplValueDropdown.options[idx] = new Option(g_OPLValues[idx]);
    }
}

function populateVideos(totalCount, myJSONObject) {
    var dropdown = [];
    for (var count = 0; count < totalCount; count++) {
        var selectMediaFile = 'selectMediaFile' + count.toString();
        dropdown[count] = document.getElementById(selectMediaFile);
        dropdown[count].options[0] = new Option('Select a media file...', 0);

        var contentIndex = 0;
        var groupsLenth = myJSONObject.Groups.length;
        g_contentList.push([]); // skip g_contentList[0];
        for (idx1 = 0; idx1 < groupsLenth; idx1++) {
            var itemsLength = myJSONObject.Groups[idx1].Items.length;

            for (idx2 = 0; idx2 < itemsLength; idx2++) {
                var item = myJSONObject.Groups[idx1].Items[idx2];
                var uniqueId = item.UniqueId;
                contentIndex++;
                dropdown[count].options[contentIndex] = new Option("#" + contentIndex + " " + item.UniqueId, contentIndex);
                g_contentList.push({
                    contentIndex: contentIndex,
                    uniqueId: item.UniqueId,
                    content: item.Content,
                    laurl: item.Laurl,
                    kid: item.Kid,
                    uplinkKey: item.UplinkKey,
                    contentKey: item.ContentKey,
                    domainUrl: item.DomainUrl,
                    serviceId: item.ServiceId,
                    domainId: item.DomainId,
                    customData: item.CustomData,
                    firstPlayExpiration: item.FirstPlayExpiration,
                    actualLaurl: item.Laurl,
                    secureStopCertFile: item.SecureStopCertFile,
                    bitRateMin: item.BitRateMin,
                    bitRateMax: item.BitRateMax,
                    resolutionMin: item.ResolutionMin,
                    resolutionMax: item.ResolutionMax,
                    keyRotation: item.KeyRotation,
                    avSep: item.AVSep,
                    deliveryMethod: item.DeliveryMethod,
                    audioCodec: item.AudioCodec,
                    videoCodec: item.VideoCodec,
                    isAudioEncrypted: item.IsAudioEncrypted,
                    slicesPerFrame: item.SlicesPerFrame
                });
            }
        }
    }

    //populate extra video count for an option selection by user
    var extraVideoCountDropdown = document.getElementById('selectExtraVideoCount');
    for (var videoIndex = 0; videoIndex < g_TotalNumOfVideo; videoIndex++) {
        extraVideoCountDropdown.options[videoIndex] = new Option(videoIndex);
    }

    populateOPLList('selectVideoOPLValue');
    populateOPLList('selectVideoOPLValue2');
    populateOPLList('selectVideoOPLValue3');

    populateOPLList('selectAudioOPLValue');
    populateOPLList('selectAudioOPLValue2');

    //populate Security Level options
    var licSLDropdown = document.getElementById('SetLicenseSecurityLevel');
    for (var licSLIdx = 0; licSLIdx < g_licenseSecurityLevel.length; licSLIdx++) {
        licSLDropdown.options[licSLIdx] = new Option(g_licenseSecurityLevel[licSLIdx]);
    }

    // populate PlayEnablerGUIDs options
    var PlayEnablerDropdown = document.getElementById('selectPlayEnablerGUIDList');
    var i = 0;
    for (var index in g_PlayEnablerGUIDList) {
        PlayEnablerDropdown.options[i++] = new Option(index);
    }
}

function populateExplicitGuids(myJSONObject) {
    //var dropdown = document.getElementById('selectExplicitGUID');
    //dropdown.options[0] = new Option('Select ExplicitGuid...', 0);
    var guidIndex = 0;
    var explicitGUIDsLenth = myJSONObject.ExplicitGUIDs.length;
    g_explicitGuidList.push([]); //skip [0]
    for (idx1 = 0; idx1 < explicitGUIDsLenth; idx1++) {
        var itemsLength = myJSONObject.ExplicitGUIDs[idx1].Items.length;
        var guidType = myJSONObject.ExplicitGUIDs[idx1].GuidType;
        for (idx2 = 0; idx2 < itemsLength; idx2++) {
            var item = myJSONObject.ExplicitGUIDs[idx1].Items[idx2];
            var description = item.Description;
            guidIndex++;
            //dropdown.options[guidIndex] = new Option("#" + guidIndex + " " + item.Description, guidIndex);
            g_explicitGuidList.push({
                guidIndex: guidIndex,
                description: item.Description,
                guidType: guidType,
                guid: item.Guid,
                defaultValue: item.DefaultValue,
                detail: item.Detail,
                comment: item.Comment
            });
        }
    }
}

function selectMediaFileChange0() {

    cbUserInput.checked = false;
    document.getElementById("talaURL").hidden = true;
    document.getElementById("txtlaURL").hidden = true;

    var idx = document.getElementById('selectMediaFile0').value;
    if (idx != 0) {
        document.getElementById("taInputMediaFile").value = g_contentList[idx].content;

        if (g_contentList[idx].kid == 'null') {
            btnProactiveLicenseAcquisition.disabled = true;
        }
        else {
            btnProactiveLicenseAcquisition.disabled = false;
        }

        var details = getItemDetails(idx);
        document.getElementById("taInputMediaFile").title = details;
        document.getElementById("selectMediaFile0").title = details;
    }

    // Highlight the play button to remind the user to click it 
    var btnPlay = document.getElementById('btnPlay');
    btnPlay.style.border = "thick solid #00FFFF";
}

function handleSelectMediaFileChange(videoIndex) {
    var itemList = document.getElementById("selectMediaFile" + videoIndex);
    var idx = itemList.value;
    if (idx != 0) {
        itemList.title = getItemDetails(idx);
    }

    // Highlight the play button to remind the user to click it 
    var btnPlay = document.getElementById('btnPlay');
    btnPlay.style.border = "thick solid #00FFFF";
}

function selectMediaFileChange1() {
    handleSelectMediaFileChange(1);
}

function selectMediaFileChange2() {
    handleSelectMediaFileChange(2);
}

function selectMediaFileChange3() {
    handleSelectMediaFileChange(3);
}

function selectMediaFileChange4() {
    handleSelectMediaFileChange(4);
}

function selectMediaFileChange5() {
    handleSelectMediaFileChange(5);
}

function getItemDetails(idx) {
    //
    // Format the title (tooltip) base on the properties in 
    // the selected item in g_contentList.
    // Skip the property if it is 'null'.
    //
    var details = "Details:\n\n";
    var content = g_contentList[idx];
    for (var prop in content) {
        if (content.hasOwnProperty(prop)) {
            if (content[prop] != 'null') {
                details += prop + ": " + content[prop] + "\n";
            }
        }
    }

    return details;
}

function selectExtraVideoCountChange() {

    g_PreviousNumOfVideo = g_ActualNumOfVideo;
    for (var videoIndex = 1; videoIndex < g_PreviousNumOfVideo; videoIndex++) {
        g_playReadyPlay[videoIndex].HideVideos();
    }

    var extraVideoCount = document.getElementById('selectExtraVideoCount').value;
    if (extraVideoCount != 'undefined' && extraVideoCount != null && extraVideoCount != "") {
        g_ActualNumOfVideo = parseInt(extraVideoCount) + 1;
        for (var videoIndex = 1; videoIndex < g_ActualNumOfVideo; videoIndex++) {
            g_playReadyPlay[videoIndex].ShowVideos();
        }
    }

}

function readMediaListAsync() {
    try {

        var contentInforFile = 'ContentInfo.json';
        var package = Windows.ApplicationModel.Package.current;
        var installedLoc = package.installedLocation;

        logMsg('then reading file from \"' + installedLoc.path + '\\' + contentInforFile + '\"');

        var getJsonStringAsync = function () {
            return installedLoc.getFileAsync(contentInforFile).then(
                function (dataFile) {
                    return Windows.Storage.FileIO.readTextAsync(dataFile);
                });
        };

        getJsonStringAsync().then(function (text) {
            myJSONObject = JSON.parse(text.toString());
            populateVideos(g_TotalNumOfVideo, myJSONObject);
            populateExplicitGuids(myJSONObject);
        });
    }
    catch (e) {
        logException('exception(readJSONFile):', e);
        return;
    }
}

function cbOptOutHWDRMOnClick() {
    var localSettings = Windows.Storage.ApplicationData.current.localSettings;
    var ContainerPlayReady;

    if (cbOptOutHWDRM.checked) {
        if (localSettings.containers.hasKey("PlayReady")) {
            logMsg('ContainsKey PlayReady exists ');
            ContainerPlayReady = localSettings.containers.lookup("PlayReady");
        }
        else {
            logMsg('create a new Container Key PlayReady ');
            ContainerPlayReady = localSettings.createContainer("PlayReady", Windows.Storage.ApplicationDataCreateDisposition.always);
        }
        logMsg('set SoftwareOverride = 1 ');
        ContainerPlayReady.values["SoftwareOverride"] = 1;
    }
    else {
        logMsg('set SoftwareOverride = 0 ');
        if (localSettings.containers.hasKey("PlayReady")) {
            ContainerPlayReady = localSettings.containers.lookup("PlayReady");
            ContainerPlayReady.values["SoftwareOverride"] = 0;
        }
    }

    //re-initilize when switching between SWDRM and HWDRM
    clearLog();
    uninitilize();
    initilize();

}

// if localSettings.Containers["PlayReady"].Values["SoftwareOverride"] = 1
// set  cbOptOutHWDRM.checked = true
// otherwise leave cbOptOutHWDRM alone
// this function need to be called right after app launched
function setSoftwareOverride_And_cbOptOutHWDRM() {
    var localSettings = Windows.Storage.ApplicationData.current.localSettings;
    if (localSettings.containers.hasKey("PlayReady")) {
        //"PlayReady" key exists, because "cbOptOutHWDRM" has been checked before, continue to set remembered previous setting
        var SoftwareOverride = localSettings.containers.lookup("PlayReady").values["SoftwareOverride"];

        if (SoftwareOverride != 'undefined' && SoftwareOverride == 1) {
            logMsg('***** SoftwareOverride = 1 is already set, so continue mark cbOptOutHWDRM as checked ******');
            cbOptOutHWDRM.checked = true;
        }
    }

}

function bOptOutHWDRM_click() {
    setCheckBox("cbOptOutHWDRM");
}

function bPersistentLicense_click() {
    setCheckBox("cbPersistentLicense");
}

function bBoundToDomain_click() {
    setCheckBox("cbBoundToDomain");
}

function bUserInput_click() {
    setCheckBox("cbUserInput");
}

function bBesteffortCGMS_A_click() {
    setCheckBox("cbBesteffortCGMS_A");
}

function bCGMS_A_click() {
    setCheckBox("cbCGMS_A");
}

function bDigitalToken_click() {
    setCheckBox("cbDigitalToken");
}

function bAGCColorStrip_click() {
    setCheckBox("cbAGCColorStrip");
}

function bMAX_VGA_RESOLUTION_click() {
    setCheckBox("cbMAX_VGA_RESOLUTION");
}

function bMAX_COMPONENT_RESOLUTION_click() {
    setCheckBox("cbMAX_COMPONENT_RESOLUTION");
}

function bSCMS_click() {
    setCheckBox("cbSCMS");
}

function bHDCPType1_click() {
    setCheckBox("cbHDCPType1");
}

function setCheckBox(checkBoxName) {
    var chkBox = document.getElementById(checkBoxName);
    chkBox.checked = !chkBox.checked;
}