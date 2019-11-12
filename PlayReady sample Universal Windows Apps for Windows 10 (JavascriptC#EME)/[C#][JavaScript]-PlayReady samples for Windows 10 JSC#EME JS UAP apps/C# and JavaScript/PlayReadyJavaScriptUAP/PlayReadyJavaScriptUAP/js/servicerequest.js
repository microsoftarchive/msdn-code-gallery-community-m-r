//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

function PRServiceRequest(VideoIndex) {
    this.videoIndex = VideoIndex;
    this.mpmCompletionNotifier = null;
    this.currentServiceRequest;
}

PRServiceRequest.prototype = {

    prepareSR: function (serviceRequest) {
        switch (this.getSRType(serviceRequest)){
            case 'PlayReadyLicenseAcquirerServiceRequest':
                this.configLASR(serviceRequest);
                break;

            case 'PlayReadyDomainJoinServiceRequest':
            case 'PlayReadyDomainLeaveServiceRequest':
                this.configDomainSR(serviceRequest);
                break;

            case 'PlayReadySecureStopServiceRequest':
                this.configSecureStopSR(serviceRequest);
                break;

            default:
                // There are other possible ServiceRequest that can be returned by PlayReady
                // Revocation,
                // You can config those service request here also
                break;
        }
    },

    //Override here is optional
    configLASR: function(objLASR) {
        try
        {
            if (g_fQueryProperty) {
                if (objLASR.uri != null) {
                    logMsg('current LA serviceRequest URL = ' + objLASR.uri.absoluteUri, 3);
                }

                if (objLASR.challengeCustomData != null) {
                    logMsg('current LA serviceRequest challengeCustomData = ' + objLASR.challengeCustomData, 3);
                }
            }

            if (this.videoIndex === undefined)
            {
                this.videoIndex = 0;
            }

            var contentIndex = this.getContentIndexForVideo();

            var txtlaURL = g_contentList[contentIndex].actualLaurl;
            
            if (txtlaURL != undefined &&
                txtlaURL != null &&
                txtlaURL != 'null' &&
                txtlaURL != '')
            {
                logMsg('override lauri = ' + txtlaURL, 3);
                objLASR.uri = Windows.Foundation.Uri(txtlaURL);
            }
            else
            {
                if (g_fQueryProperty)
                {
                    logMsg('use default url from header: ' + objLASR.uri, 3);
                } 
            }
            
            if (g_contentList[contentIndex].customData != 'null')
            {
                var txtlaChlgCustData = g_contentList[contentIndex].customData;
                logMsg('setting custom data - objLASR.challengeCustomData = ' + txtlaChlgCustData, 3);
                objLASR.challengeCustomData = txtlaChlgCustData;
            }
            
            if (g_fQueryProperty) {
                var kids = objLASR.contentHeader.keyIdStrings;
                if (kids == null || kids == 'undefined') {
                    kids = objLASR.contentHeader.keyIdString;
                    logMsg('Enabler KeyId: ' + kids, 6);
                }
                else {
                    for (var i = 0; i < kids.length; i++) {
                        logMsg('Enabler KeyId[' + i + ']:' + kids[i], 6);
                    }
                }
            }
        }
        catch (e) {
            logException('exception(configLASR):' , e);
            return;
        }
    },

    configDomainSR: function (objDomainSR) {
        try {

            if (objDomainSR.uri != null)
            {
                logMsg('current domain join serviceRequest URL = ' + objDomainSR.uri.absoluteUri, 3);
            }

            var contentIndex = this.getContentIndexForVideo();
            var txtDomainURL = g_contentList[contentIndex].domainUrl;
           

            logMsg('override domainUri = ' + txtDomainURL, 3);
            objDomainSR.uri = Windows.Foundation.Uri(txtDomainURL);
            objDomainSR.domainServiceId = g_contentList[contentIndex].serviceId;
            objDomainSR.domainAccountId = g_contentList[contentIndex].domainId;

        }
        catch (e) {
            logException('exception(configDomainSR):', e);
            return;
        }
    },

    configSecureStopSR: function (objSecureStopDR) {
        try {

            if (objSecureStopDR.uri != null) {
                logMsg('current secure stop serviceRequest URL = ' + objSecureStopDR.uri.absoluteUri, 3);
            }

            if (this.videoIndex === undefined) {
                this.videoIndex = 0;
            }

            var contentIndex = this.getContentIndexForVideo();
            var txtSecureStopURL = g_contentList[contentIndex].actualLaurl;

            logMsg('override securesStopUri = ' + txtSecureStopURL, 3);
            objSecureStopDR.uri = Windows.Foundation.Uri(txtSecureStopURL);

        }
        catch (e) {
            logException('exception(configSecureStopSR):', e);
            return;
        }
    },

    HandleServiceRequest: function (serviceRequest, complete) {
        var currentServiceRequestType = this.getSRType(serviceRequest);
        logMsg('SRtype: ' + currentServiceRequestType, 6);

        this.currentServiceRequest = serviceRequest;

        this.prepareSR(serviceRequest);

        logMsg('Querying Data set on service request\n', 3);
        logMsg('Protection System ID:' + serviceRequest.protectionSystem, 6);
        logMsg('Service request type:' + currentServiceRequestType, 6);
        if (g_fQueryProperty){
             if (serviceRequest.uri != null) {
                logMsg('Default Uri:' + serviceRequest.uri.absoluteUri, 6);
            }
            else {
                logMsg('Default Uri: (NULL)\n', 6);
            }
        }
       
        logMsg('Starting PlayReadyServiceRequest processing ', 3);

        if (g_fUseManualEnabling) {
            performManualEnabling(serviceRequest);

            var nextServiceRequest = serviceRequest.nextServiceRequest();

            if (nextServiceRequest != null) {
                this.HandleServiceRequest(nextServiceRequest, complete);
            }
            else {
                this.onServiceRequestSuccess(complete);
            }
        }
        else {
            var that = this;
            var onServiceRequestSuccessWrapper = function () {
                that.onServiceRequestSuccess(complete);
            }

            var  onServiceRequestError = function(e){
                logMsg(currentServiceRequestType + ' operation errored out!');
                if (e.number != -2147174251) // MSPR_E_CONTENT_ENABLING_ACTION_REQUIRED (0x8004b895)
                {
                    logException('exception(onServiceRequestError)', e);
                }
                else
                {
                    logMsg('onServiceRequestError MSPR_E_CONTENT_ENABLING_ACTION_REQUIRED (0x8004b895)');
                    var nextServiceRequest = that.currentServiceRequest.nextServiceRequest();
                    if (nextServiceRequest != null) {
                        logMsg('NextServiceRequest returned a new SR');
                        that.HandleServiceRequest(nextServiceRequest, complete);
                    }
                }
            }


            logMsg('using promises model ( serviceRequest.then(..) )', 3);
            serviceRequest.beginServiceRequest().then(onServiceRequestSuccessWrapper, onServiceRequestError);
        }
    },

    onServiceRequestSuccess: function(complete) {
        logMsg(this.getSRType(this.currentServiceRequest) + ' operation completed successfully');
        this.performSRCompletion();

        if (complete != null) {
            complete();
        }
    },

    performSRCompletion: function () {
        if (this.getSRType(this.currentServiceRequest) == 'IndividualizationServiceRequest') {
            logMsg('playready security version is ' + Windows.Media.Protection.PlayReady.PlayReadyStatics.playReadySecurityVersion);
        }

        // All ServiceRequests are done.  Fire completion
        logMsg('Call PostSRCompleteFunction');
        this.PostSRCompleteFunction(0);
    },


    PostSRCompleteFunction: function(hrResult) {
        if (this.mpmCompletionNotifier != undefined && this.mpmCompletionNotifier != null) {
            if (hrResult >= 0) {
                logMsg("this.mpmCompletionNotifier.complete( true )");
                this.mpmCompletionNotifier.complete(true);
            }
            else {
                logMsg("this.mpmCompletionNotifier.complete( false )");
                this.mpmCompletionNotifier.complete(false);
            }

            this.mpmCompletionNotifier = null;
        }
    },


    performManualEnabling: function(serviceRequest) {
        var messageBytes;
        var soapMessage;
        var headers;
        var currentSRString;

        logMsg('Starting manual serviceRequest processing', 4);

        currentSRString = this.getSRType(serviceRequest);
        soapMessage = serviceRequest.generateManualEnablingChallenge();

        headers = soapMessage.messageHeaders;
        messageBytes = soapMessage.getMessageBody();

        var strResponse;
        var rgbResponse;
        var lauri;
        var xmlRequest = new XMLHttpRequest();

        logMsg('Initializing HTTP Request object', 4);

        var contentIndex = this.getContentIndexForVideo();
        lauri = Windows.Foundation.Uri(g_contentList[contentIndex].actualLaurl);
        
        if (lauri == null) {
            lauri = soapMessage.uri;
        }

        logMsg('Opening a POST request to ' + lauri.absoluteUri, 8);

        xmlRequest.open("POST", lauri.absoluteUri, false);

        Object.keys(headers).forEach(
            function (headerName) {
                var headerValue = headers[headerName];
                logMsg('Header: ' + headerName + ': ' + headerValue, 8);
                xmlRequest.setRequestHeader(headerName, headerValue);
            });

        xmlRequest.send(messageBytes);

        logMsg('Response recieved ', 4);
        logMsg('Result Code: ' + xmlRequest.status, 8);
        logMsg('Result String: ' + xmlRequest.statusText, 8);

        strResponse = xmlRequest.responseText;
        rgbResponse = stringToByteArray(strResponse);

        var result = serviceRequest.processManualEnablingResponse(rgbResponse);
        if (result >= 0) {
            logMsg('processManualEnablingResponse retured a success code... ', 4);
        }
        else {
            logMsg('processManualEnablingResponse retured a failure code... ', 4);
        }
    },

    getSRType: function (sr) {
        if (undefined == sr) {
            return '( Service Request Object Undefined )';
        }
        else if (null == sr) {
            return '( Service Request Object NULL )';
        }
        else if (Windows.Media.Protection.PlayReady.PlayReadyStatics.licenseAcquirerServiceRequestType == sr.type) {
            return 'PlayReadyLicenseAcquirerServiceRequest';
        }
        else if (Windows.Media.Protection.PlayReady.PlayReadyStatics.individualizationServiceRequestType == sr.type) {
            return 'IndividualizationServiceRequest';
        }
        else if (Windows.Media.Protection.PlayReady.PlayReadyStatics.domainJoinServiceRequestType == sr.type) {
            return 'PlayReadyDomainJoinServiceRequest';
        }
        else if (Windows.Media.Protection.PlayReady.PlayReadyStatics.domainLeaveServiceRequestType == sr.type) {
            return 'PlayReadyDomainLeaveServiceRequest';
        }
        else if (Windows.Media.Protection.PlayReady.PlayReadyStatics.meteringReportServiceRequestType == sr.type) {
            return 'PlayReadyMeteringServiceRequest';
        }
        else if (Windows.Media.Protection.PlayReady.PlayReadyStatics.secureStopServiceRequestType == sr.type) {
            return 'PlayReadySecureStopServiceRequest';
        }
        else {
            return '( UNKNOWN TYPE! )' + sr.type;
        }
    },

    getContentIndexForVideo: function()
    {
        var selectMediaFile = 'selectMediaFile' + this.videoIndex;
        var contentIndex = document.getElementById(selectMediaFile).value;
        return contentIndex;
    }
}
