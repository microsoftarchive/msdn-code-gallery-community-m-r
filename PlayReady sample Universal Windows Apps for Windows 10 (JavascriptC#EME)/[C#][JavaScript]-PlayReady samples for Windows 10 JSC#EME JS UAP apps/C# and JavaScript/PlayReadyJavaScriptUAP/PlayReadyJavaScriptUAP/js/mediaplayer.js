//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

function MediaPlayer(videoIdx) {
    this.videoIndex = videoIdx;
    this.videoPlayer;
    this.mediaProtectionManager;
    this.serviceRequest;
}

MediaPlayer.prototype = {

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


    //Depends on your application, you might not need to set all of the setting here for MediaProtectionManager
    //However, all these settings are benign and you can keep it as-is.
    setupMediaProtectionManager: function () {
        try{
            var mediaProtectionManager;
            var cpsystems;

            //logMsg('Creating new Windows.Media.Protection.MediaProtectionManager', 3);
            mediaProtectionManager = new Windows.Media.Protection.MediaProtectionManager();

            //The system ID used by the Inbox ASF and CFF media source to instantiate PlayReady ITA
            //This is optional if ASF and CFF media source is not used
            //logMsg('--- Setting up MediaProtectionSystemId');
            mediaProtectionManager.properties["Windows.Media.Protection.MediaProtectionSystemId"] = '{F4637010-03C3-42CD-B932-B48ADF3A6A54}'

            // Setup for underlying ASF media source how to instantiate PlayReady ITA.
            // This attribute is used by both the ASF and CFF source
            cpsystems = new Windows.Foundation.Collections.PropertySet();
            cpsystems["{F4637010-03C3-42CD-B932-B48ADF3A6A54}"] = "Windows.Media.Protection.PlayReady.PlayReadyWinRTTrustedInput"; //Playready TrustedInput Class Name
            mediaProtectionManager.properties["Windows.Media.Protection.MediaProtectionSystemIdMapping"] = cpsystems;

            // Setup the container GUID for CFF
            mediaProtectionManager.properties["Windows.Media.Protection.MediaProtectionContainerGuid"] = "{9A04F079-9840-4286-AB92-E65BE0885F95}";

            var localSettings = Windows.Storage.ApplicationData.current.localSettings;
            var UseSoftwareProtectionLayer;

            if (localSettings.containers.hasKey("PlayReady")) {
                UseSoftwareProtectionLayer = localSettings.containers.lookup("PlayReady").values["SoftwareOverride"];

                if (UseSoftwareProtectionLayer != 'undefined' && UseSoftwareProtectionLayer == 1) {
                    logMsg(' ');
                    logMsg('***** Use Software Protection Layer ******');
                    mediaProtectionManager.properties["Windows.Media.Protection.UseSoftwareProtectionLayer"] = true;
                }
            }

            //logMsg('Setting up MediaProtectionManager componentloadfailed event handler', 3);
            mediaProtectionManager.addEventListener("componentloadfailed", this.onComponentLoadFailed, false);
        }
        catch (e) {
            logException('Exception(setupMediaProtectionManager): ', e);
        }

        var that = this;
        var onServiceRequested = function (request) {

            try {

                logMsg('calling onServiceRequested');
                // Event handler for reactive service requests
                // When you try to play a protected content, the protected media path (PMP) might return a service request to the application.
                // For PlayReady, there are various services requests. The main one is LicenseAcquisitionServiceRequest (LASR). 
                // LASR will be used to acquire license reactively. You wil also have the opportunity to configurate the service requests here (See HandleServiceRequest function).
                var serviceRequest = request.request;
               
                var prServiceRequest = new PRServiceRequest(that.videoIndex);

                logMsg('Service request required before playback can happen, service request type: ' + prServiceRequest.getSRType(serviceRequest));

                prServiceRequest.HandleServiceRequest(serviceRequest, null);

                //The application needs to notify the PMP that the app has finished handling the ServiceRequest event via request.completion.complete(bool)
                //completion.complete has to be called to unblock the media pipeline
                prServiceRequest.mpmCompletionNotifier = request.completion;

                
            }
            catch(e){
                logException('Exception(onServiceRequested): ', e);
            }
        }

        this.serviceRequest = onServiceRequested;

        //Setting up event handler for PlayReady related events
        mediaProtectionManager.addEventListener("servicerequested", onServiceRequested, false);

        this.mediaProtectionManager = mediaProtectionManager;
    },



    setupPlugins: function () {
        //logMsg('Setting up MediaExtensionManager', 3);

        // You don't need to register the SmoothByteStreamHandler if you are not playing PIFF content using  Microsoft Smooth Streaming Client SDK
        var plugins = new Windows.Media.MediaExtensionManager();
        plugins.registerByteStreamHandler("Microsoft.Media.AdaptiveStreaming.SmoothByteStreamHandler", ".ism", "text/xml");
        plugins.registerByteStreamHandler("Microsoft.Media.AdaptiveStreaming.SmoothByteStreamHandler", ".ism", "application/vnd.ms-ss");

        //You don't need to registery PlayReadyByteStreamHandler if you are not playing pyv/pya content
        //logMsg('---Setup ByteStreamHandlers for pyv/pya');
        plugins.registerByteStreamHandler("Windows.Media.Protection.PlayReady.PlayReadyByteStreamHandler", ".pyv", null);
        plugins.registerByteStreamHandler("Windows.Media.Protection.PlayReady.PlayReadyByteStreamHandler", ".pya", null);
    },

    setupVideoPlayer: function () {

        try{
            this.setupPlugins();

            var videotag = 'video' + this.videoIndex;
            this.videoPlayer = document.getElementById(videotag);

            this.setupMediaProtectionManager();

            var that = this;

            var onCanPlay = function ()
            {
                //logMsg('Attempting to start playback');
                that.videoPlayer.addEventListener('canplay', onCanPlay, false);
                that.videoPlayer.play();
            }

            var onPlayError = function() {
                if (that.videoPlayer.src == null) {
                    logMsg('Media Source not set');
                }
                else {
                    var errMsg = 'The media element error event was thrown: ' + that.videoPlayer.error.code + ',  extended error code: ' + intToHexString(that.videoPlayer.error.msExtendedCode);
                    logException('(onPlayError)', errMsg);
                }
            }

            this.videoPlayer.addEventListener('canplay', onCanPlay, false);
            this.videoPlayer.addEventListener('error', onPlayError, false);

            //logMsg('Setting MediaProtectionManager to video player', 3);
            this.videoPlayer.msSetMediaProtectionManager(this.mediaProtectionManager);
        }
        catch(e)
        {
            logException('exception(setupVideoPlayer):', e);
        }
    }

}


