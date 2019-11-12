//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reser

function PlayReadyPlay(VideoIndex) {
    this.videoIndex = VideoIndex;
    this.contentInfo;
    this.mediaPlayer;
    this.proactiveServiceRequest;
}


//
PlayReadyPlay.prototype = {

    //call this before call others
    initialize: function (ContentInfo) {
        this.setMediaPlayer();
        this.setContentInfo(ContentInfo);
    },

    uninitilize: function () {
        var videotag = 'video' + this.videoIndex;
        var videoElement = document.getElementById(videotag);
        videoElement.pause();
        videoElement.msSetMediaProtectionManager(null);
        videoElement.removeAttribute("src");
        videoElement.load();
        if (this.mediaPlayer !== undefined) {
            this.mediaPlayer.mediaProtectionManager.removeEventListener("servicerequested", this.mediaPlayer.serviceRequest);
            this.mediaPlayer = null;
        }
    },

    setContentInfo: function (ContentInfo) {
        logMsg('setContentInfo');
        this.contentInfo = ContentInfo;
        this.contentInfo.actualLaurl = ContentInfo.laurl;
    },

    HideVideos: function () {
        var selectMediaFile = 'selectMediaFile' + this.videoIndex;
        document.getElementById(selectMediaFile).value = document.getElementById(selectMediaFile).options[0];
        document.getElementById(selectMediaFile).style.visibility = "hidden";
        var videotag = 'video' + this.videoIndex;
        document.getElementById(videotag).style.visibility = "hidden";
        document.getElementById(videotag).removeAttribute('src');
    },

    ShowVideos: function () {
        var selectMediaFile = 'selectMediaFile' + this.videoIndex;
        document.getElementById(selectMediaFile).style.visibility = "visible";
        var videotag = 'video' + this.videoIndex;
        document.getElementById(videotag).style.visibility = "visible";
    },

    setSource: function () {
        logMsg('(setSource)this.videoIndex = ' + this.videoIndex);

        logMsg('set source for video[' + this.videoIndex + ']');

        if (cbOptOutHWDRM.checked) {
            logMsg(' ');
            logMsg('!!!!! using SWDRM !!!!!');
        }
        var videotag = 'video' + this.videoIndex;

        //
        // In order to play Smooth Streaming content (e.g. PIFF_SuperSpeedway_720.ism at
        // http://playready.directtaps.net/smoothstreaming/SSWSS720H264PR/SuperSpeedway_720.ism/Manifest)
        // you need to add Microsoft Universal Smooth Streaming Client SDK to the project's References.
        // If you haven't installed the SDK, you can download and install the SDK from
        // https://visualstudiogallery.msdn.microsoft.com/1e7d4700-7fa8-49b6-8a7b-8d8666685459?SRC=Home.
        // Also add Visual C++ 2015 Runtime for Universal Windows Platform Apps to the project's References
        // if it hasn't been added.
        //
        document.getElementById(videotag).src = this.contentInfo.content;
    },

    setIndividualOPLvalue: function (oplType, oplValueTagId) {
        var list = document.getElementById(oplValueTagId);
        var oplValue = parseInt(list.value, 10);
        if (!isNaN(oplValue)) {
            this.contentInfo.actualLaurl += "&" + oplType + "=" + oplValue;
        }
    },

    setOPLvalue: function () {
        //check if any OPL value should be set?
        this.setIndividualOPLvalue('UncompressedDigitalVideoOPL', 'selectVideoOPLValue');
        this.setIndividualOPLvalue('CompressedDigitalVideoOPL', 'selectVideoOPLValue2');
        this.setIndividualOPLvalue('AnalogVideoOPL', 'selectVideoOPLValue3');
        this.setIndividualOPLvalue('UncompressedDigitalAudioOPL', 'selectAudioOPLValue');
        this.setIndividualOPLvalue('CompressedDigitalAudioOPL', 'selectAudioOPLValue2');
    },

    setLicenseSurityLevel: function () {
        var licSLValue = document.getElementById('SetLicenseSecurityLevel').value;
        if (licSLValue != undefined) {
            this.contentInfo.actualLaurl += "&" + "SecurityLevel=" + licSLValue;
        }
    },

    setSecureStop: function () {
        if(this.contentInfo.secureStopCertFile != 'null') {
            this.contentInfo.actualLaurl += "&" + "SecureStop=1";
        }
    },

    setupPlayEnabler: function () {
        //check if PlayEnabler should be set for license?
        var PlayEnablerGUID = document.getElementById('selectPlayEnablerGUIDList').value;
        if (PlayEnablerGUID != 'None') {
            var enablerGUIDStr = "&PlayEnablers=" + g_PlayEnablerGUIDList[PlayEnablerGUID];
            this.contentInfo.actualLaurl += enablerGUIDStr;
        }
    },

    setExplicitGuids: function () {
        var guidIdx = 0;
        if (cbSCMS.checked) {
            guidIdx = parseInt(cbSCMS.value, 10);
            this.setExplicitGuid(guidIdx);
        }

        if (cbDigitalToken.checked) {
            guidIdx = parseInt(cbDigitalToken.value, 10);
            this.setExplicitGuid(guidIdx);
        }

        if (cbMAX_VGA_RESOLUTION.checked) {
            guidIdx = parseInt(cbMAX_VGA_RESOLUTION.value, 10);
            this.setExplicitGuid(guidIdx);
        }

        if (cbMAX_COMPONENT_RESOLUTION.checked) {
            guidIdx = parseInt(cbMAX_COMPONENT_RESOLUTION.value, 10);
            this.setExplicitGuid(guidIdx);
        }

        if (cbCGMS_A.checked) {
            guidIdx = parseInt(cbCGMS_A.value, 10);
            this.setExplicitGuid(guidIdx);
        }

        if (cbBesteffortCGMS_A.checked) {
            guidIdx = parseInt(cbBesteffortCGMS_A.value, 10);
            this.setExplicitGuid(guidIdx);
        }

        if (cbAGCColorStrip.checked) {
            guidIdx = parseInt(cbAGCColorStrip.value, 10);
            this.setExplicitGuid(guidIdx);
        }

        if (cbHDCPType1.checked) {
            guidIdx = parseInt(cbHDCPType1.value, 10);
            this.setExplicitGuid(guidIdx);
        }

        var maxResDecoderWidth = parseInt(ipMaxResDecorderWidth.value, 10);
        var maxResDecoderHeight = parseInt(ipMaxResDecorderHeight.value, 10);
        
        if (isNaN(maxResDecoderWidth) != isNaN(maxResDecoderHeight)) {
            var md = new Windows.UI.Popups.MessageDialog("You have to specify both the width and height!  Ignore the incomplete max. resolution decoder.");
            md.showAsync();
        } else if(maxResDecoderWidth > 0 && maxResDecoderHeight > 0) {
            var baWidth = convertNumberTo4ByteArrayInNetworkOrder(maxResDecoderWidth);
            var baHeight = convertNumberTo4ByteArrayInNetworkOrder(maxResDecoderHeight);
            var bcWH = baWidth.concat(baHeight);

            // The 9th element in g_explicitGuidList is DigitV:MaxResDecoder (see contentInfo.json)
            var explicitGuidSetting = "&" + g_explicitGuidList[9].guidType +
                                    "=" + g_explicitGuidList[9].guid;
            var b64EncodedValue = encode(bcWH);

            explicitGuidSetting += ";" + b64EncodedValue;

            this.contentInfo.actualLaurl += explicitGuidSetting;
        }
    },

    setExplicitGuid: function (guidIndex) {
        var explicitGuidSetting = "&" + g_explicitGuidList[guidIndex].guidType +
                                "=" + g_explicitGuidList[guidIndex].guid;

        var valuestr = g_explicitGuidList[guidIndex].defaultValue;
        var valueArr = valuestr.split(";");
        var byteValue = [];
        var byteValues;

        for (var idx = 0; idx < valueArr.length; idx++) {
            var x = parseFloat(valueArr[idx]);
            var i = 1;
            if (g_byteOneExplicitGuidList.indexOf(g_explicitGuidList[guidIndex].guid) == -1) {
                i = 4;
            }
            byteValue[idx] = getIntBytes(x, i);
        }

        if (valueArr.length == 1) {
            byteValues = byteValue[0];
        }
        else if (valueArr.length == 2) {
            byteValues = byteValue[1].concat(byteValue[0]);
        }
        else {
            logException('error(setExplicitGuid)', 'unexpected value');
        }


        var b64EncodedValue = encode(byteValues);

        explicitGuidSetting += ";" + b64EncodedValue;

        this.contentInfo.actualLaurl += explicitGuidSetting;
    },

    setInMemeryOrPersistentLicense: function () {
        logMsg('(setInMemeryOrPersistentLicense)this.videoIndex = ' + this.videoIndex);
        //check if generating a non-persistent license?
        if (!cbPersistentLicense.checked) {
            this.contentInfo.actualLaurl += "&UseSimpleNonPersistentLicense=1";
        }
    },

    setBoundToDomain: function () {
        //check if generating Domain-bound license?
        if (cbBoundToDomain.checked) {
            this.contentInfo.actualLaurl += "&UseDomains=1";
        }
    },

    setContentKey: function () {
        if (this.contentInfo.contentKey != 'null') {
            this.contentInfo.actualLaurl += "&ContentKey=" + this.contentInfo.contentKey;
        }
    },

    setFirstPlayExpiration: function () {
        if (this.contentInfo.firstPlayExpiration != 'null') {
            this.contentInfo.actualLaurl += "&RealTimeExpiration=1" + "&FirstPlayExpiration=" + this.contentInfo.firstPlayExpiration;
        }
    },

    setUplinkKey: function () {
        if (this.contentInfo.uplinkKey != 'null') {
            base64UplinkKid = guid_to_base64(this.contentInfo.uplinkKey, true);
            logMsg("base64UplinkKid=" + base64UplinkKid);
            this.contentInfo.actualLaurl += "&UseChainLicense=1" + "&UplinkKey=" + base64UplinkKid;
        }
    },

    setUseRootLicense: function () {
        this.contentInfo.actualLaurl += "&UseRootLicense=1";
    },

    setLicenseRights: function (f_bFirstPlayExpiration) {
        //
        // This test sample uses our custom DirectTap LicenseServer business logic to acquire 
        // specific licenses with particular property. Your license server might have different logic.
        // To find out what the properties in this page are and what they are used for, consult our
        // compliance rule in https://www.microsoft.com/playready/licensing/compliance/
        //
        var urlPattern = /\bhttp:\/\/playready.directtaps.net\/svc/gi;
        if (urlPattern.test(this.contentInfo.actualLaurl)) {
            //this cofiguration is for DTAP only
            this.setContentKey();
            this.setInMemeryOrPersistentLicense();
            this.setOPLvalue();
            this.setupPlayEnabler();
            this.setExplicitGuids();
            this.setBoundToDomain();
            this.setLicenseSurityLevel();
            this.setSecureStop();
            if (f_bFirstPlayExpiration != undefined &&
               f_bFirstPlayExpiration == true) {
                this.setFirstPlayExpiration();
            }
        }
        else {
            logMsg('not use DTAP server');
        }
    },

    proactivePlay: function (f_bFirstPlayExpiration) {
        logMsg('pre-acquire License(s), then playback');

        var proactiveServiceRequest = new ProactiveServiceRequest(this.videoIndex, this.contentInfo);

        var that = this;

        if (this.contentInfo.uplinkKey != undefined &&
            this.contentInfo.uplinkKey != "null") {
            var acquireLeafLicense = function () {
                that.setContentInfo(that.contentInfo);
                proactiveServiceRequest.proactiveLicenseAcquistion(function () {
                    if (proactiveServiceRequest.licenseSession !== undefined) {
                        proactiveServiceRequest.configMediaProtectionManager(that.mediaPlayer.mediaProtectionManager);
                    }
                    that.setSource();
                }, f_bFirstPlayExpiration);
            };


            proactiveServiceRequest.proactiveIndivChain(function () {
                proactiveServiceRequest.proactiveLicenseAcquistion(function () {
                    that.proactiveServiceRequest = proactiveServiceRequest;
                    acquireLeafLicense();
                }, null, that.contentInfo.uplinkKey);
            });
        }
        else {
            proactiveServiceRequest.proactiveIndivChain(function () {
                proactiveServiceRequest.proactiveLicenseAcquistion(function () {
                    if (proactiveServiceRequest.licenseSession !== undefined) {
                        proactiveServiceRequest.configMediaProtectionManager(that.mediaPlayer.mediaProtectionManager);
                    }
                    that.setSource();
                    that.proactiveServiceRequest = proactiveServiceRequest;
                }, f_bFirstPlayExpiration);
            });
        }

    },

    setMediaPlayer: function () {
        this.mediaPlayer = new MediaPlayer(this.videoIndex);
        this.mediaPlayer.setupVideoPlayer();
    },
}
