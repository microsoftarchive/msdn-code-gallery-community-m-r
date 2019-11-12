//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkId=232509
(function ()
{
	"use strict";

	var app = WinJS.Application;
	var activation = Windows.ApplicationModel.Activation;

	app.onactivated = function (args) {
		if (args.detail.kind === activation.ActivationKind.launch) {
		    if (args.detail.previousExecutionState !== activation.ApplicationExecutionState.terminated) {
		        Init();
		        PopulateVideoLibrary();
			} else {
				// TODO: This application has been reactivated from suspension.
				// Restore application state here.
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

var g_mseSupported;
var g_upgrade_button;
var g_dismiss_button;
var g_library;
var g_videoFrame;
var g_video;
var g_msMediaKeys;
var g_selectedItem;
var g_player;
var g_chainedLicense;
var g_persistentLicense;
var g_secureStopEnabled;
var g_enabelSecureStopButton = false;
var g_HDCPType1;
var g_uncompDigitalVideo;
var g_ignoreLDLTimer;
var g_adaptiveStreaming = Windows.Media.Streaming.Adaptive;
var g_mediaSource = null;
var g_url;

var g_OPLValues = ["None", "0", "100", "150", "200", "201", "250", "270", "300"];

var g_videoLibrary = [
        {
            title: "Big Buck Bunny",
            property: "Clear",
            attribution: "© Copyright 2008, Blender Foundation / www.bigbuckbunny.org",
            baseUrl: "http://amssamples.streaming.mediaservices.windows.net/683f7e47-bd83-4427-b0a3-26a6c4547782/BigBuckBunny.ism/manifest(format=mpd-time-csf)",
            fileType: "manifest(format=mpd-time-csf)",
            LAURL: "null",
            proactiveLA: "null",
            LDL: "null",
            kid1: "null",
            kid2: "null",
            kidRoot: "null", 
            secureStopURL: "null"
        },
        {
            title: "Tears of Steel",
            property: "Clear",
            attribution: "© Copyright Blender Foundation | mango.blender.org",
            baseUrl: "http://amssamples.streaming.mediaservices.windows.net/bc57e088-27ec-44e0-ac20-a85ccbcd50da/TearsOfSteel.ism/manifest(format=mpd-time-csf)",
            fileType: "manifest(format=mpd-time-csf)",
            LAURL: "null",
            proactiveLA: "null",
            LDL: "null",
            kid1: "null",
            kid2: "null",
            kidRoot: "null", 
            secureStopURL: "null"
        },
        {
            title: "DASH DualKey Sample (AV sep)",
            property: "Protected",
            attribution: "Proactive License Acquisition",
            baseUrl: "http://playready.directtaps.net/media/dash/dualkey/dash.mpd",
            fileType: "manifest(format=mpd-time-csf)",
            LAURL: "http://playready.directtaps.net/svc/pr30priv/rightsmanager.asmx?PlayRight=1",
            proactiveLA: "true",
            LDL: "false",
            kid1: "SFNBRGhGRkdPS1VTdmlkZQ==",
            kid2: "SFNBRGhGRkdPS1VTc291bg==",
            kidRoot: "u9G4VugLLUSV/En1i2JnYw==",  // only when chained license is selected
            secureStopURL: "http://playready.directtaps.net/svc/pr30stage/rightsmanager.asmx"
        },
        {
            title: "DASH DualKey Sample (AV sep, LDL)",
            property: "Protected",
            attribution: "Proactive License Acquisition",
            baseUrl: "http://playready.directtaps.net/media/dash/dualkey/dash.mpd",
            fileType: "manifest(format=mpd-time-csf)",
            LAURL: "http://playready.directtaps.net/svc/pr30priv/rightsmanager.asmx?PlayRight=1",
            proactiveLA: "true",  // Available only in the proactive LA
            LDL: "true",
            kid1: "SFNBRGhGRkdPS1VTdmlkZQ==",
            kid2: "SFNBRGhGRkdPS1VTc291bg==",
            kidRoot: "u9G4VugLLUSV/En1i2JnYw==", // only when chained license is selected
            secureStopURL: "http://playready.directtaps.net/svc/pr30stage/rightsmanager.asmx"
        },
        {
            title: "sintel trailer (1080p)",
            property: "Protected",
            attribution: "Reactive License Acquisition",
            baseUrl: "http://wams.edgesuite.net/media/SintelTrailer_Smooth_from_WAME_CENC/NoSubSampleAdjustment/sintel_trailer-1080p.ism/manifest(format=mpd-time-csf)",
            fileType: "manifest(format=mpd-time-csf)",
            LAURL: "http://playready.directtaps.net/svc/pr30priv/rightsmanager.asmx?PlayRight=1",
            proactiveLA: "false",
            LDL: "false",
            kid1: "null",
            kid2: "null",
            kidRoot: "Voad717JSkCgQTx5POiRSw==",
            secureStopURL: "http://playready.directtaps.net/svc/pr30stage/rightsmanager.asmx"
        },
        {
            title: "Tears of Steel (1080p)",
            property: "Protected",
            attribution: "Reactive License Acquisition",
            baseUrl: "http://wams.edgesuite.net/media/Tears_of_Steel_Smooth_1080p_Protected2/tears_of_steel_1080p.ism/manifest(format=mpd-time-csf)",
            fileType: "manifest(format=mpd-time-csf)",
            LAURL: "http://playready.directtaps.net/svc/pr30priv/rightsmanager.asmx?PlayRight=1",
            proactiveLA: "false",
            LDL: "false",
            kid1: "null",
            kid2: "null",
            kidRoot: "Yh3qcXy0iUyktEeiS09V4g==",
            secureStopURL: "http://playready.directtaps.net/svc/pr30stage/rightsmanager.asmx"
        }
    ];

function getItemDetails(idx) {
    //
    // Format the title (tooltip) base on the properties in 
    // the selected item in g_videoLibrary.
    // Skip the property if it is 'null'.
    //
    var details = "Details:\n\n";
    var content = g_videoLibrary[idx];
    for (var prop in content) {
        if (content.hasOwnProperty(prop)) {
            if (content[prop] != 'null') {
                details += prop + ": " + content[prop] + "\n";
            }
        }
    }

    return details;
}

function PopulateVideoLibrary()
{
    // Populate the library of video files.
    var defaultVideo = false;
    var selectedVideo = -1;

    for (var i = 0; i < g_videoLibrary.length; i++) {
        var div = document.createElement("div");
        div.id = "video" + i;
        div.className = "libraryelement";
        div.title = getItemDetails(i);
        div.onclick = (function (index) {
            return function (event) {
                if (selectedVideo >= 0) {
                    document.getElementById("video" + selectedVideo).style.opacity = "";
                }
                document.getElementById("video" + index).style.opacity = 1;
                defaultVideo = false;
                selectedVideo = index;

                onVideoSelected(index);
            };
        })(i);

        // Draw library elements, with closed lock on protected files
        if (g_videoLibrary[i].property != "Protected") {
            div.innerHTML = "<p class='title'>" + g_videoLibrary[i].title + "</p><p><span class='property'>" + g_videoLibrary[i].property + "</span></p><p class = 'attribution'>" + g_videoLibrary[i].attribution + "</p>";
        } else {
            div.innerHTML = "<p class='title'>" + g_videoLibrary[i].title + "</p><p><span class='property'>" + g_videoLibrary[i].property + "</span><span><img width='25' style='vertical-align: -40%;' src='data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAABGdBTUEAAK/INwWK6QAAABl0RVh0U29mdHdhcmUAQWRvYmUgSW1hZ2VSZWFkeXHJZTwAAARxSURBVHja5FdNbBtFFH4zu+u/2IljJ3FwjDhESCk5WW0iwFIkS40EETnQRjInKiFO0BOHquLCpRfgUC6IE0g5uErEpaKcwikHIlFCbySpmx8hJVGaxHb9v/buzjA7++e4xY28kXpgrPGsZ2ff++b73nuzRpRSeJUNwytuYrebmUzmRdPBWCz2aTqdvp5IJCY0TcMHBwdP1tfXf9nb2/uBMVrsfGB5efk/faBuEnQCaDQa783Ozv44Pj4e39zchNPTU0AIQTQahcnJSX3MLy0t3dzf3186LwDxvFQVCoXM2NhYtlgsCicnJ/BuKgV9gb6Goij0+Pg48PDhn1AsFqILCwv3VlZWoltbW9+7lsBqsiyPM6c/pVIpYW5uDgih939fW7tTelZ65PV6YHAw/BZj4NbOzu7Hi4uLaH5+/ju2/o98Pr9+IUHINP6KaR6YmZnRr+9ms9kPj46e/kWB60crlcrfudzjG+Vy+bbfH4DV1VVxamrqjussIISAqqpepvP1ZDIJsdho7sGDX281GjK0xw5jBLa3t1nPfSMI4trh4SH09/fPSpI04poBRv+l6enpAAMCGxsbWY/Ho4qiALVqBShzrIPc2d2BVqsFzCEtFPKLOqB6vY7j8XjSVQyUSiXdcIQ17qhSKefC4QHnYUzgn93HcPg0b881m80nQ0NDfH0oFIq4AsB2rxvCPp+Pp5vX61UZtaZzgLdfV6GRGIG7P+9Dudbk84wpVU9JnuMIYdcS6I4ZtWc0FxCBy3ENwn4Cr0WD8MVHKQiH/CCKIgiCoAO9mFKsO8cY89F2zp5IRo4h7HMAjTIQn3/wJvgkDKwychAXAiA9PQEiM2btHiMKl0dlGPSpz61NDAfh5rUkeBkIHbTVJhKh3mPgxvyVa+krb1zVpCD0hzwQGxA/kZSRq0AHPUgMSAKmmINDbFB9ykgCKV8Ph6NVzQ/B/hYkJHhfLnGq7vV0Fuz+9m0tMRoOyPUalJ4VoF6r8tIjiB7WRUMa1inReNRrqgKq0uK9LregWKlDpS7TzJf3cW+lmNmn5si8gNKUgdX+zkABsDdBzcVOe9nrRtcYoMwpN2pZ4b40c94ug3bAmkjbuoHPxWFk7Y4agWjTYYAzJAAuAWXX+sf2SA3WXvbG1R1ABwPUvm6nGjnfqJ13Cud52TvHcazvCjuGLaqpyQoQ/pu2ObbrBl9DXB7HnHpisqEPKqfc0dmgHZnBiGyJLDC4dwaMHAcnwrlRpzLqJCNznhAnWwzQ1ByoWwkcKYwM0M4Qx/fMGbCyEhu0WyoQ4hKApTU16OSUmsxQk3IDhOHYcI74PUeqXmOggz4LiJ1mz9UMKyNQGxtuJTBT0Ypma7TZsIuPUwmpHQPkAv4ZobZixCk10tKmuN0JfQE1gFyVYnpmc2BUPSPkzd0S4kiDkLNzq3jpNnqWwBP9jCDxHSSF4v4BPCz1RSJIkMLszSeABSGAMY9IHRTDQetEUxuqqpTYaVioVmsn5ebRkUSCj7oS/L//d/yvAAMA4g5v3g3i8TsAAAAASUVORK5CYII='></span></p><p class = 'attribution'>" + g_videoLibrary[i].attribution + "</p>";
        }

        src = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAABGdBTUEAAK/INwWK6QAAABl0RVh0U29mdHdhcmUAQWRvYmUgSW1hZ2VSZWFkeXHJZTwAAAS+SURBVHjazFfdT1tlGH/OV1tOu/LRtYW2iKbJkLGAJsBiTBZDMRqFMOYmGrMLb7zAOG+MZokab0zEv2FxXhATLiSGO5NdcuFIZjC6hE1mCYRgS/gqo56ej/f1ec9He4B+UFh0J3nTw/se3uf3/J7f83vP4Sil8H9e3FMHYHx8vLTIceGenp5P+vv7R8PhcLuiKJvz8/N35ubmvjUMY1EQBPZM1QChM1748HIPEIyzu5cHxRBg6PrnxXWx0j/G4/HRsbGx79LpdAsGhEKhAKIoyu3t7e8PDw+/NzMz8yUCmDwtA2UBJBKJNwYHB3+cnp4W+vr6SWro1TlREB4SQlr9fnnol7t3fZFI5JvFxUWxra3t6ycKALNq7OvruzU7OyucP39hRVULVx4+eHBPVVVYWV0FuUGOj4y8OR3w+19eWlr6Kp/P/yTL8v2TAuAPTwwMDHyGmbVJktd49OjP0Uwmc8+9vrOzvXb79vcjvb29f6dSKTGbzU7oug6VBrJWH4Du7u63cVPAjO8QQhcymSxkshlzjRiGKSaO57fT6eVbyWQSfD7fJU3ToNqoqwShUKiD53lYXk7/7vF4zLlVpL6zsxOw/uC3n8vldv/ATmBija6vr1cMEG/21scABucQBFBKdEIwY3tsbW2yLigOvDQGIBAI8Ewf5UbibACuv9ZTfxc4mbt7nN0f/ptdkiSV3TgZD8HH71zCDPX6GHA2ZQZVa9igjlhpMtYEN65dBJ9XOpkPMOWi6x1QcBkxlbXAZKwRblzpxeDCyY1oZ2eH/dx3A8B+B9YdLkBL0WhUdQN5rq0RPhrD4B7hdE6Yy+V+QO+fcmfd0NBQ1Aa7sMd/w+64iSW4aQaPtcDE5Rcw+BOwYgw8ji73F3bDF0V7bj0Lzz8bNe+x+uANRsZQ6ZMLCwu7Ha1NMPHWS+DjGWD9dACmpqaYDwTR41eCwaA51xL0w9XUiyDb6clevktu9L2e3SZixNMhd8W7JmWf5AVqiAJHeAYROJGgY+mGLhWQJaVQULCK2uO9/T1mGjMVAeCpt4YKX3PPvdJ/Dh1PAgKW4Kmx/27CTz4I0Rxs6RsN/2zkP9VECc8RCWNa+qSoH+YfhqYioyooqgabu/uwly9Qd/eJtSiKhZugI9oMQA/Jn1pTrB01VQFaUJg5sInyG9nTh98/agK4eKEDjEMHSrE72GaU2LfEYt6kgFprCMjxi1LwOgDEwo3wTGszVE2JBaS0lDlHi+xw4HZPAjbC4wNg2Zd7Z6RQyoRl7n6CYt0da6AYlIEwg1riMLVxLACs9olIc/mSOvGdzKmdnRmYcw6LIliTFAxslYM/xmEkCZAaqHaK0dIvJWXmLXCWDDiLMVpirCaAZLwFIqHgEcGUeam2hUZcTJWyxBeXklht+o9dgmqfC9YadYmPM5ko6oUjZs3N3HHeZIEaNlBSXxtWLIFdd2qDMGvNCwfKY2ZrlsDqFouZ43QBBaj6xUThgA84HkCpbjWfo3rOLdqSN9QEQJ3MKpbAvdFR9zNpd3WCA5I6rNRkgKHnK1eHivLPOvGc4yR/TA6Gw55AqFmUpDOCKPp4W4F4DhAMpui69lhT1W08iTZ2lc11D8f/+nR/nP7X178CDACSU6IjYtZAngAAAABJRU5ErkJggg=="

        g_library.appendChild(div);
    }
}

function Init()
{
    g_upgrade_button = document.getElementById("upgrade_button");
    g_dismiss_button = document.getElementById("dismiss_button");

    // UI elements/functions.
    g_library = document.getElementById("library");
    g_videoFrame = document.getElementById("videoFrame");
    g_video = document.getElementById("video");

    g_msMediaKeys = null;

    g_video.addEventListener("ended", video_onended, false);
    g_upgrade_button.addEventListener("click", function upgrade() { window.location = "http://msdn.microsoft.com/en-us/ie/"; }, false);
    g_dismiss_button.addEventListener("click", function dismiss() { hideUpgradeNotice(); }, false);
    g_mseSupported = detectMSESupport();

    var btnDeleteHDS = document.getElementById("btnDeleteHDS");
    btnDeleteHDS.addEventListener("click", btnDeleteHDS_click, false);

    var btnSecureStop = document.getElementById("btnSecureStop");
    btnSecureStop.addEventListener("click", btnSecureStop_click, false);

    var btnClearLog = document.getElementById("btnClearLog");
    btnClearLog.addEventListener("click", btClearLog_click, false);

    var cbOptOutHWDRM = document.getElementById("cbOptOutHWDRM");
    cbOptOutHWDRM.addEventListener("click", cbOptOutHWDRM_click, false);
    
    setSoftwareOverride_And_cbOptOutHWDRM();

    var bChainLicense = document.getElementById("bChainLicense");
    bChainLicense.addEventListener("click", bChainLicense_click, false);

    var bPersistLicense = document.getElementById("bPersistLicense");
    bPersistLicense.addEventListener("click", bPersistLicense_click, false);

    var bSecureStop = document.getElementById("bSecureStop");
    bSecureStop.addEventListener("click", bSecureStop_click, false);

    var bOptOutHWDRM = document.getElementById("bOptOutHWDRM");
    bOptOutHWDRM.addEventListener("click", bOptOutHWDRM_click, false);

    var bHDCPType1 = document.getElementById("bHDCPType1");
    bHDCPType1.addEventListener("click", bHDCPType1_click, false);
    
    var rbDashSourceInBox = document.getElementById("rbDashSourceInBox");
    rbDashSourceInBox.addEventListener("click", rbDashSourceInBox_click);

    var bDashSourceInBox = document.getElementById("bDashSourceInBox");
    bDashSourceInBox.addEventListener("click", bDashSourceInBox_click);

    var rbDashSourceDashJS = document.getElementById("rbDashSourceDashJS");
    rbDashSourceDashJS.addEventListener("click", rbDashSourceDashJS_click);

    var bDashSourceDashJS = document.getElementById("bDashSourceDashJS");
    bDashSourceDashJS.addEventListener("click", bDashSourceDashJS_click);

    for (var idx = 0; idx < g_OPLValues.length; idx++) {
        sUncompDigitalVideo.options[idx] = new Option(g_OPLValues[idx]);
    }

    if (Windows.Foundation.Metadata.ApiInformation.isTypePresent("Windows.Phone.UI.Input.HardwareButtons")) {

        // Adjust the videoFrame' position and width when
        // runs on the phone.
        g_library.style.float = "none";
        g_videoFrame.style.float = "none";
        g_videoFrame.style.marginLeft = 0;

        g_library.style.width = "100%";
        g_library.style.height = "300px";
        g_videoFrame.style.width = "100%";
        document.getElementById("taLog").style.width = "100%";
    }

    // Global resize
    window.onresize = (function () {
        function resize() {
            console.log("resize");
            g_library.style.height = g_videoFrame.offsetHeight + "px";
        }
        resize();
        return resize;
    })();

    g_video.addEventListener("msneedkey", function (e) {

        logMsg("Received needkey message");

        if (!g_video.msKeys) {
            console.log("Call video element to set media keys");
            try {
                g_video.msSetMediaKeys(g_msMediaKeys);

                if (g_selectedItem.proactiveLA == "false") {
                    var reactiveLA = new ReactivePlayReadyManager(g_selectedItem, g_selectedItem.LDL == "true", false);
                    reactiveLA.ReactiveLicenseAcquire(g_msMediaKeys, e.initData, function () {
                    });
                }

            } catch (e) {
                throw "Fail to set media keys. Verify the components are installed and functional. Original error: " + e.message;
            }
        } else {
            console.log("NeedKey in progress");
        }

    }, false);
}

function mapAdaptiveMediaSourceCreationStatus(status) {
    var statusStrings = ["Success",
                         "ManifestDownloadFailure",
                         "ManifestParseFailure",
                         "UnsupportedManifestContentType",
                         "UnsupportedManifestVersion",
                         "UnsupportedManifestProfile",
                         "UnknownFailure"];

    if (status < 0 || status > statusStrings.length) {
        return "Unknown AdaptiveMediaSourceCreationStatus";
    }

    return statusStrings[status];
}

function onMediaSourceCreated(result) {
    if (result.status === g_adaptiveStreaming.AdaptiveMediaSourceCreationStatus.success) {

        logMsg("AdaptiveMediaSource.createFromUriAsync completed with status: " + result.status + " - " + mapAdaptiveMediaSourceCreationStatus(result.status));
        g_mediaSource = result.mediaSource;

        try {
            if (g_mediaSource != null) {

                g_video.src = URL.createObjectURL(g_mediaSource, { oneTimeOnly: true });
                logMsg("Set media element src to the AdaptiveMediaSource for url: " + g_url);
            }
        } catch (e) {
            WinJS.log && WinJS.log("EXCEPTION: " + e.toString(), "sample", "error");
        }

    } else {
        var errorString = "";
        var httpResponseMessage = result.httpResponseMessage;

        if (httpResponseMessage != null) {
            errorString = " (HTTP response: " + httpResponseMessage.statusCode + " - " + httpResponseMessage.reasonPhrase;

            if (httpResponseMessage.isSuccessStatusCode &&
                result.status == g_adaptiveStreaming.AdaptiveMediaSourceCreationStatus.unsupportedManifestContentType &&
                httpResponseMessage.content != null) {
                errorString += "; Content-Type: " + httpResponseMessage.content.headers.contentType;
            }

            errorString += ")";

        }
        logMsg("Failed to create adaptive media source: " + mapAdaptiveMediaSourceCreationStatus(result.status) + errorString);
    }
}

function startVideo(url)
{
    logMsg("Start video");

    resetVideo();

    var rbDashSourceInBox = document.getElementById("rbDashSourceInBox");
    if (rbDashSourceInBox.checked) {
        logMsg("Use inbox DASH media source");

        try {
            g_url = new Windows.Foundation.Uri(url);

            logMsg("Creating AdaptiveMediaSource for url: " + url);

            g_adaptiveStreaming.AdaptiveMediaSource.createFromUriAsync(g_url).done(
                function completed(result) {
                    onMediaSourceCreated(result);
                });

        } catch (e) {
            logException("function(startVideo): exception ", e);
        }
    } else {
        logMsg("Use dash.all.js");

        if (g_player == undefined || g_player == null) {
            g_player = new MediaPlayer(new Dash.di.DashContext());
        }

        g_player.startup();
        g_player.attachView(g_video);
        g_player.setAutoPlay(true);
        g_player.attachSource(url);
    }
}

function resetVideo() {

    if (g_player != undefined && g_player != null) {

        g_player.attachView(null);
        g_player.attachSource(null);
    }

    g_video.removeAttribute("src");
    g_mediaSource = null;
}

function processSelectedVideo()
{
    if (g_selectedItem.proactiveLA == "true") {

        if (g_selectedItem.LDL == "true") {

            console.log("Acquiring limited duration licenses.");
            var proactiveLDL = new ProactivePlayReadyManager(g_selectedItem, true, false);
            proactiveLDL.ProactiveLicenseAcquire(g_msMediaKeys, function () {
                startVideo(g_selectedItem.baseUrl);
                g_video.controls = true;
                g_ignoreLDLTimer = false;

                // Wait 30 seconds before acquiring full licenses
                setTimeout(function () {
                    if (!g_ignoreLDLTimer) {
                        // The user havsn't switched to other video since this timer started
                        // continue the full license acquisition.
                        console.log("Acquiring full licenses.");
                        var proactiveFull1 = new ProactivePlayReadyManager(g_selectedItem, false, false);
                        proactiveFull1.ProactiveLicenseAcquire(g_msMediaKeys, null);
                    }
                }, 30000);
            });

        } else {

            console.log("Acquiring simple/leaf licenses.");
            var proactiveLDL = new ProactivePlayReadyManager(g_selectedItem, false, false);
            proactiveLDL.ProactiveLicenseAcquire(g_msMediaKeys, function () {
                startVideo(g_selectedItem.baseUrl);
                g_video.controls = true;
            });
        }


    } else {

        // Start video to trigger reactive license acquisition
        console.log("Reactive license acquisition.");
        startVideo(g_selectedItem.baseUrl);
        g_video.controls = true;
    }
}

function onVideoSelected(index)
{
    // Set below flag to true in case the previous video is LDL
    // to stop the full license acquisition.
    g_ignoreLDLTimer = true;

    // Disable cbOptOutHWDRM button
    var cbOptOutHWDRM = document.getElementById("cbOptOutHWDRM");
    cbOptOutHWDRM.disabled = true;
    var bOptOutHWDRM = document.getElementById("bOptOutHWDRM");
    bOptOutHWDRM.style.color = "Gray";

    resetVideo();

    // Create a global media keys to create all the session.
    // This is required for non-persistent licenses.
    g_msMediaKeys = new MSMediaKeys("com.microsoft.playready");

    g_chainedLicense = cbChainLicense.checked;
    g_persistentLicense = cbPersistLicense.checked;
    g_secureStopEnabled = cbSecureStop.checked;
    g_HDCPType1 = cbHDCPType1.checked;

    g_uncompDigitalVideo = parseInt(sUncompDigitalVideo.value, 10);

    if (g_persistentLicense && g_secureStopEnabled) {
        logMsg("Error: secure stop is for non-persistent license only!");
        return;
    }

    g_selectedItem = g_videoLibrary[index];

    btnSecureStop.disabled = true;
    if (g_secureStopEnabled && g_selectedItem.property == 'Protected') {
        g_enabelSecureStopButton = true;
        btnSecureStop.title = "This button will be enabled when the playback ends.";
    }
    else {
        g_enabelSecureStopButton = false;
        btnSecureStop.title = "This button is available for secure stop content only.";
    }

    if (g_selectedItem.property == "Protected") {

        if (g_chainedLicense) {
            console.log("Acquiring root licenses.");
            var proactiveLDL = new ProactivePlayReadyManager(g_selectedItem, g_selectedItem.LDL == "true", true);
            proactiveLDL.ProactiveLicenseAcquire(g_msMediaKeys, function () {
                processSelectedVideo();
            });
        } else {
            processSelectedVideo();
        }

    } else {

        // Play clear video
        startVideo(g_selectedItem.baseUrl);
        g_video.controls = true;
    }
}

function video_onended()
{
    logMsg("video ended");
    if( g_enabelSecureStopButton ) {
        btnSecureStop.disabled = false;
    }
}

function btnSecureStop_click()
{
    ProcessSecureStop(g_msMediaKeys, g_selectedItem.secureStopURL);
}

function btClearLog_click()
{
    clearMsg();
}

//this function is not working when playing any content
function btnDeleteHDS_click()
{
    deleteHDS();
}

function deleteHDS(complete) {
    // Delete the PlayReady folder from local folder then create an empty PlayReady folder
    Windows.Storage.ApplicationData.current.localFolder.getFolderAsync("playready").then(function (folder) {
        return folder.deleteAsync().then(function (folder) {
            Windows.Storage.ApplicationData.current.localFolder.createFolderAsync("playready");
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
                    var msg = 'Delete license store failed. Please close the app and relaunch it, then try delete license store again!';
                    logMsg(msg);
                    var md = new Windows.UI.Popups.MessageDialog(msg);
                    md.showAsync();
                }
            });
    }).done(function () {
        // complete, do nothing
    }, function (e) {
        // error, do nothing
    });
}

function cbOptOutHWDRM_click() {
    var localSettings = Windows.Storage.ApplicationData.current.localSettings;
    var ContainerPlayReady;

    if (cbOptOutHWDRM.checked)
    {
        if (localSettings.containers.hasKey("PlayReady"))
        {
            logMsg('ContainsKey PlayReady exists ');
            ContainerPlayReady = localSettings.containers.lookup("PlayReady");
        }
        else
        {
            logMsg('create a new Container Key PlayReady ');
            ContainerPlayReady = localSettings.createContainer("PlayReady", Windows.Storage.ApplicationDataCreateDisposition.always);
        }
        logMsg('set SoftwareOverride = 1 ');
        ContainerPlayReady.values["SoftwareOverride"] = 1;
    }
    else
    {
        logMsg('set SoftwareOverride = 0 ');
        if (localSettings.containers.hasKey("PlayReady"))
        {
            ContainerPlayReady = localSettings.containers.lookup("PlayReady");
            ContainerPlayReady.values["SoftwareOverride"] = 0;
        }
    }
    
    //re-initilize when switching between SWDRM and HWDRM
    clearMsg();

    var videoFrame = document.getElementById("videoFrame");
    var video = document.getElementById("video");
    
    video.pause();
    video.msSetMediaProtectionManager(null);
    video.removeAttribute("src");
    video.load();

    videoFrame.removeChild(video);
    videoFrame.appendChild(video);
}

// if localSettings.Containers["PlayReady"].Values["SoftwareOverride"] = 1
// set  cbOptOutHWDRM.checked = true
// otherwise leave cbOptOutHWDRM alone
// this function need to be called right after app launched
function setSoftwareOverride_And_cbOptOutHWDRM() {
    var localSettings = Windows.Storage.ApplicationData.current.localSettings;
    if (localSettings.containers.hasKey("PlayReady")) {
        // "PlayReady" key exists, because "cbOptOutHWDRM" has been checked before, continue to set remembered previous setting
        var SoftwareOverride = localSettings.containers.lookup("PlayReady").values["SoftwareOverride"];

        if (SoftwareOverride != 'undefined' && SoftwareOverride == 1) {
            logMsg('SoftwareOverride = 1 is already set, so continue mark cbOptOutHWDRM as checked');
            cbOptOutHWDRM.checked = true;
        }
    }
}

function bChainLicense_click() {
    var chkBox = document.getElementById("cbChainLicense");
    chkBox.checked = !chkBox.checked;
}

function bPersistLicense_click() {
    var chkBox = document.getElementById("cbPersistLicense");
    chkBox.checked = !chkBox.checked;
}

function bSecureStop_click() {
    var chkBox = document.getElementById("cbSecureStop");
    chkBox.checked = !chkBox.checked;
}

function bOptOutHWDRM_click() {
    var chkBox = document.getElementById("cbOptOutHWDRM");
    chkBox.checked = !chkBox.checked;
}

function bHDCPType1_click() {
    var chkBox = document.getElementById("cbHDCPType1");
    chkBox.checked = !chkBox.checked;
}

function rbDashSourceInBox_click() {
    setMediaSource("rbDashSourceInBox");
}

function rbDashSourceDashJS_click() {
    setMediaSource("rbDashSourceDashJS");
}

function setMediaSource(radioBtnName) {
    var inBox = document.getElementById("rbDashSourceInBox");
    var dashJS = document.getElementById("rbDashSourceDashJS");
    if (radioBtnName == "rbDashSourceInBox") {
        dashJS.checked = !inBox.checked;
    } else {
        inBox.checked = !dashJS.checked;
    }
}

function bDashSourceInBox_click() {
    document.getElementById("rbDashSourceInBox").checked = true;
    document.getElementById("rbDashSourceDashJS").checked = false;
}

function bDashSourceDashJS_click() {
    document.getElementById("rbDashSourceInBox").checked = false;
    document.getElementById("rbDashSourceDashJS").checked = true;
}
