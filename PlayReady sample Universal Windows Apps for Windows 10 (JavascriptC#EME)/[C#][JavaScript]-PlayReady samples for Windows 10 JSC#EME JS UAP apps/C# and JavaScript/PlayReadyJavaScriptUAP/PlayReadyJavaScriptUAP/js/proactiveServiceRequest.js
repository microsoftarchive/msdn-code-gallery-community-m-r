//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

var g_domainInfo = {
    domainURL: 'http:\/\/playready.directtaps.net/pr/svc/rightsmanager.asmx',
    serviceId: '{DEB47F00-8A3B-416D-9B1E-5D55FD023044}',
    accountID: '{3A87FB03-C53E-46F9-8CF8-9967CB6A1B14}'
};

function ProactiveServiceRequest(VideoIndex, ContentInfo) {
    this.videoIndex = VideoIndex;
    this.contentInfo = ContentInfo;
    this.licenseSession;
}

ProactiveServiceRequest.prototype = {
    //
    // For proactive tasks, we need to indiv beforehand. Otherwise, 0x8004B822 error code will be returned.
    // To check if we already indived, we call PlayReadyStatics.playReadySecurityVersion. 
    // If we are not indived, we will indiv first, then do the task
    // If we are indived, then we will just do the task
    //
    proactiveIndivChain: function (complete) {

        logMsg('Begin proactiveIndivChain');
        var prServiceRequest = new PRServiceRequest(this.videoIndex);

        try {
            //playReadySecurityVersion will throw an error if we are not indived.
            if (Windows.Media.Protection.PlayReady.PlayReadyStatics.playReadySecurityVersion != undefined) {

                logMsg('SecurityVersion = ' + Windows.Media.Protection.PlayReady.PlayReadyStatics.playReadySecurityVersion);

                if (complete != null) {
                    complete();
                }
            }
        } catch (e) {
            //We have not indived, so we will indiv here. Upon success, onServiceRequestSuccess will call complete()
            logMsg('Begin Indiv');
            var indivServiceRequest = new Windows.Media.Protection.PlayReady.PlayReadyIndividualizationServiceRequest();
            prServiceRequest.HandleServiceRequest(indivServiceRequest, complete);
        }
    },

    proactiveLicenseAcquistion: function (complete, f_bFirstPlayExpiration, uplinkKey )
    {
        try {
            logMsg('Begin proactiveLicenseAcquistion');

            var selectMediaFile;

            selectMediaFile = 'selectMediaFile' + this.videoIndex;
        
            var idx = document.getElementById(selectMediaFile).value;    
            logMsg('idx=' + idx);


            var laServiceRequest;
            var kids = [];

            if (uplinkKey != undefined &&
                uplinkKey != null)
            {
                //kid = this.contentInfo.uplinkKey;
                var kids = uplinkKey.split(",");
            }
            else
            {
                kid = this.contentInfo.kid;
                var tempkids = kid.split(",");
                if (f_bFirstPlayExpiration != undefined &&
                    f_bFirstPlayExpiration != null &&
                   f_bFirstPlayExpiration == true) {
                    var count = 100;

                    var i = 0;
                    for (; i < count / 2; i++) {
                        kids[i] = _generateUUID();
                    }

                    tempkids.forEach(function (kid) {
                        kids[i++] = kid;
                    });

                    for (; i < count; i++) {
                        kids[i] = _generateUUID();
                    }
                }
                else {
                    kids = tempkids;
                }
            }

            var contentHeader = new Windows.Media.Protection.PlayReady.PlayReadyContentHeader(
                   0,
                   kids,
                   null,
                   Windows.Media.Protection.PlayReady.PlayReadyEncryptionAlgorithm.aes128Ctr,
                   Windows.Foundation.Uri(this.contentInfo.actualLaurl),
                   Windows.Foundation.Uri(this.contentInfo.actualLaurl),
                   'customAttribute',
                   this.contentInfo.serviceId);

            if (!cbPersistentLicense.checked)
            {
                if (this.licenseSession == null)
                {
                    laServiceRequest = this.createLicenseSession().createLAServiceRequest();
                }
                else
                {
                    laServiceRequest = this.licenseSession.createLAServiceRequest();
                }
            }
            else
            {
                laServiceRequest = new Windows.Media.Protection.PlayReady.PlayReadyLicenseAcquisitionServiceRequest();
            }
        
            laServiceRequest.contentHeader = contentHeader;

            if (uplinkKey != undefined &&
                uplinkKey != null)
            {
                //acquire a root license
                g_playReadyPlay[this.videoIndex].setUseRootLicense();
                g_playReadyPlay[this.videoIndex].setInMemeryOrPersistentLicense();
                //check to see if adding playEnabler
                g_playReadyPlay[this.videoIndex].setupPlayEnabler();
            }
            else
            {
                if ((uplinkKey == undefined || uplinkKey == null) &&
                    this.contentInfo.uplinkKey != "null") {
                    //acquire leaf for a chained license
                    g_playReadyPlay[this.videoIndex].setUplinkKey();

                }
                //rights and restrictions setting other than playenabler applies to leaf or simple license ( not root license)
                g_playReadyPlay[this.videoIndex].setLicenseRights(f_bFirstPlayExpiration);
            }

           

            var prServiceRequest = new PRServiceRequest(this.videoIndex);

            prServiceRequest.HandleServiceRequest(laServiceRequest, complete);
        }
        catch(e)
        {
            logException('exception(proactiveLicenseAcquistion):', e);
        }
    },

    createLicenseSession: function (){
        logMsg( '--- Creating license session' );

        var propSet = new Windows.Foundation.Collections.PropertySet();
        propSet["Windows.Media.Protection.MediaProtectionSystemId"] = "{F4637010-03C3-42CD-B932-B48ADF3A6A54}";

        var cpsystems;
        cpsystems = new Windows.Foundation.Collections.PropertySet();
        cpsystems["{F4637010-03C3-42CD-B932-B48ADF3A6A54}"] = "Windows.Media.Protection.PlayReady.PlayReadyWinRTTrustedInput"; //Playready TrustedInput Class Name
        propSet["Windows.Media.Protection.MediaProtectionSystemIdMapping"] = cpsystems;

        var localSettings = Windows.Storage.ApplicationData.current.localSettings;
        var UseSoftwareProtectionLayer;

        if (localSettings.containers.hasKey("PlayReady")) {
            UseSoftwareProtectionLayer = localSettings.containers.lookup("PlayReady").values["SoftwareOverride"];

            if (UseSoftwareProtectionLayer != 'undefined' && UseSoftwareProtectionLayer == 1) {
                logMsg(' ');
                logMsg('***** Use Software Protection Layer for CreateLicenseSession ******');
                propSet["Windows.Media.Protection.UseSoftwareProtectionLayer"] = true;
            }
        }

        var pmpServer = new Windows.Media.Protection.MediaProtectionPMPServer( propSet );

        var propSet2 = new Windows.Foundation.Collections.PropertySet();
        propSet2["Windows.Media.Protection.MediaProtectionPMPServer"] = pmpServer;
        this.licenseSession = new Windows.Media.Protection.PlayReady.PlayReadyLicenseSession(propSet2);
        return this.licenseSession;
    },

    configMediaProtectionManager: function (mediaProtectionManager) {

        if (this.licenseSession === undefined)
        {
            createLicenseSession();
        }
        this.licenseSession.configureMediaProtectionManager(mediaProtectionManager);
    },

    onComponentLoadFailed: function (e) {
        logMsg(e.information.items.size + " failed components!");
        logMsg("Components:");

        //  List the failing components
        for (var i = 0; i < e.information.items.size; i++) {
            logMsg(e.information.items[i].name + "\nReasons=0x" + e.information.items[i].reasons + '\n'
                                                + "Renewal Id=" + e.information.items[i].renewalId);
        }

        e.completion.complete(false);
        logMsg("Resumed source (false)");
    },

    configDomainServiceRequest: function (domainServiceRequest) {

        logMsg('Begin configDomainServiceRequest');

        domainServiceRequest.uri = Windows.Foundation.Uri(this.contentInfo.domainUrl);
        domainServiceRequest.domainServiceId = this.contentInfo.ServiceId;
        domainServiceRequest.domainAccountId = this.contentInfo.domainId;
    },


    proactiveSecureStop: function () {

        logMsg('Begin proactiveSecureStop');

        try {
            //
            // The secure stop cert is same as metering cert. This sample
            // shares the metering cert for secure stop.
            //

            // Frist read the cert
            var meteringCertFile = this.contentInfo.secureStopCertFile;
            var package = Windows.ApplicationModel.Package.current;
            var installedLoc = package.installedLocation;

            logMsg('Read metering cert file from \"' + installedLoc.path + meteringCertFile + '\"');

            var getMewteringCertAsync = function() {
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

                // Start the secure stop service request
                var prServiceRequest = new PRServiceRequest(this.videoIndex);
                var secureStopSR = new Windows.Media.Protection.PlayReady.PlayReadySecureStopServiceRequest(certBytes);
                prServiceRequest.HandleServiceRequest(secureStopSR, function () {
                    logMsg('End proactiveSecureStop');
                });
            });
        }
        catch (e) {
            logException('exception(proactiveSecureStop):', e);
            return;
        }
    },
}

