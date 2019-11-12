//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

//==============================================================================
// Logging/Diagnostics
//==============================================================================

// Global error handler.
window.onerror = function (message, url, line) {
      var errormsg = "ERROR: " + message + " (" + url + ", line " + line + ")";
      console.log(errormsg);
      alert(errormsg);
      return true;
};

//==============================================================================
// Helpers
//==============================================================================

function downloadArrayBuffer(url, context, callback)
{
    var xhr = new XMLHttpRequest();
    xhr.open("GET", url);
    xhr.responseType = "arraybuffer";
    xhr.msCaching = "disabled";
    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4) {
            if (xhr.status === 200) {
                callback(xhr.response, context);
            } else {
                console.log("XHR failed (" + url + "). Status: " + xhr.status + " (" + xhr.statusText + ")");
                callback(null, context);
            }
        }
    }
    xhr.send();
    return xhr;
}

function prettyPrintHex(number)
{
    if (number < 0) {
        number += 0x100000000;
    }
    return "0x" + number.toString(16);
}

function prettyPrintMediaError(error)
{
    switch (error) {
        case MediaError.MEDIA_ERR_ABORTED:
            error = "MEDIA_ERR_ABORTED";
            break;
        case MediaError.MEDIA_ERR_NETWORK:
            error = "MEDIA_ERR_NETWORK";
            break;
        case MediaError.MEDIA_ERR_DECODE:
            error = "MEDIA_ERR_DECODE";
            break;
        case MediaError.MEDIA_ERR_SRC_NOT_SUPPORTED:
            error = "MEDIA_ERR_SRC_NOT_SUPPORTED";
            break;
        default:
          error += "";
    }
    return error;
}

// Returns the version and system of Internet Explorer or a version of -1(indicating the use of another browser).
function getInternetExplorerVersion()
{
    var version = -1;
    console.log("App Name = " + navigator.appName + ", User Agent = " + navigator.userAgent);
    if (navigator.appName == 'Netscape') {
        var ua = navigator.userAgent;
        //  Get IE version
        var re = new RegExp("Trident/([0-9]{1,}[\.0-9]{0,})");
        if (re.exec(ua) != null) {
            var version = parseFloat( RegExp.$1 );
        }
        //  Get system version
        var re = new RegExp("Windows NT ([0-9]{1,}[\.0-9]{0,})");
        if (re.exec(ua) != null) {
            var system = parseFloat( RegExp.$1 );
        }
    }
    return [version, system];
}

function writeLog(msg)
{
    alert(msg);
}

function writeError(msg, allowDismiss)
{
    showUpgradeNotice(msg, allowDismiss);
}

function showUpgradeNotice(text, allowDismiss)
{
    var dialog = document.getElementById("upgrade");
    var dialogText = document.getElementById("upgrade_text");
    var dismissButton = document.getElementById("dismiss_button");
    
    if (dialog) {
        if(text && dialogText) {
            dialogText.innerHTML = text;
        }
        dismissButton.style.display = (allowDismiss && dismissButton) ? "block" : "none";
        dialog.style.display = "block";
    }
}

function hideUpgradeNotice()
{
    var dialog = document.getElementById("upgrade");
    if(dialog) {
        dialog.style.display = "none";
    }
}


//==============================================================================
// Feature Detect
//==============================================================================

function detectMSESupport()
{
    var ver = getInternetExplorerVersion();
    console.log(ver[0], ver[1]);
    
    var mseSupported = false;
    
    // Check for MediaSource support
    if(window.MediaSource) {
        mseSupported = true;        
    } else if(ver[0]==7&&ver[1]==6.1) {
        mseSupported = false;
        writeError("This demo requires media features supported by Internet Explorer 11 on Windows 8.1.", true);
        
    //  Else, check for WebKitMediaSource AND appendBuffer support (for up to date MSE)
    } else if(window.WebKitMediaSource) {
        mseSupported = true;
        if (!window.WebKitSourceBuffer.prototype.appendBuffer) {
            mseSupported = false;
            writeError("Your browser does not support the most recent version of Media Source Extensions and is unable to play these videos. Try upgrading to Internet Explorer 11 on Windows 8.1 for the best experience.", true);
        }	
        
    //  Else, no MSE
    } else {
        mseSupported = false;
        writeError("Your browser does not support Media Source Extensions and is unable to play these videos. Try upgrading to Internet Explorer 11 on Windows 8.1 for the best experience.", true);
    }
    
    return mseSupported;
}

var decode_map = {
    '&lt;': '<',
    '&gt;': '>'
};

function xmldecode( str )
{
    return str.replace( /(&lt;|&gt;)/g,
        function ( itm )
        {
            return decode_map[itm];
        });
}

function stringToByteArray(strIn, WideChar)
{
    var rgbBytes = new Array();
    var strTmp = xmldecode(strIn);
    var cbBytes = strTmp.length;
    var i = 0;

    try {
        for (i = 0; i < cbBytes; i++) {
            var chr = strTmp.charCodeAt(i);
            rgbBytes.push(chr);
            if (WideChar != 'undefined' && WideChar == true) {
                rgbBytes.push('\0'.charCodeAt(0));
            }

        }
    }
    catch (e) {
        logException("function(stringToByteArrayW): exception ", e);
        return;
    }

    return rgbBytes;
}

function stringToByteArrayW(strIn)
{
    return stringToByteArray(strIn, true);
}

function genTimeStamp()
{
    var currentdate = new Date();
    var timeStamp = currentdate.getHours() + ':' +
                    currentdate.getMinutes() + ':' +
                    currentdate.getSeconds() + ' ';
    return timeStamp;
}

//==============================================================================
// Logs
//==============================================================================

function logMsg(strmsg, numDash)
{

    var strDash = '';

    if (numDash != undefined) {
        for (var i = 0; i < numDash; i++) {
            strDash += '-';
        }
    }

    //add extra space after the dashes
    strDash += ' ';

    document.getElementById('taLog').textContent = genTimeStamp() +
                                                    strDash +
                                                    strmsg + '\n' +
                                                    document.getElementById('taLog').textContent;
}

function clearMsg()
{
    document.getElementById('taLog').textContent = "";
}

//==============================================================================
// B64 decode/encode utility function
//==============================================================================
var Base64 = {
    // private property
    _keyStr: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=",

    // public method for decoding
    decode: function (input) {
        var output = "";
        var chr1, chr2, chr3;
        var enc1, enc2, enc3, enc4;
        var i = 0;

        input = input.replace(/[^A-Za-z0-9\+\/\=]/g, "");

        while (i < input.length) {

            enc1 = this._keyStr.indexOf(input.charAt(i++));
            enc2 = this._keyStr.indexOf(input.charAt(i++));
            enc3 = this._keyStr.indexOf(input.charAt(i++));
            enc4 = this._keyStr.indexOf(input.charAt(i++));

            chr1 = (enc1 << 2) | (enc2 >> 4);
            chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
            chr3 = ((enc3 & 3) << 6) | enc4;

            output = output + String.fromCharCode(chr1);

            if (enc3 != 64) {
                output = output + String.fromCharCode(chr2);
            }
            if (enc4 != 64) {
                output = output + String.fromCharCode(chr3);
            }

        }

        //output = Base64._utf8_decode(output);

        return output;
    },

    // private method for UTF-8 decoding
    _utf8_decode: function (utftext) {
        var string = "";
        var i = 0;
        var c = c1 = c2 = 0;

        while (i < utftext.length) {

            c = utftext.charCodeAt(i);

            if (c < 128) {
                string += String.fromCharCode(c);
                i++;
            }
            else if ((c > 191) && (c < 224)) {
                c2 = utftext.charCodeAt(i + 1);
                string += String.fromCharCode(((c & 31) << 6) | (c2 & 63));
                i += 2;
            }
            else {
                c2 = utftext.charCodeAt(i + 1);
                c3 = utftext.charCodeAt(i + 2);
                string += String.fromCharCode(((c & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63));
                i += 3;
            }

        }

        return string;
    },

    // public method for encoding
    encode: function (input) {
        var output = "";
        var chr1, chr2, chr3, enc1, enc2, enc3, enc4;
        var i = 0;

        input = Base64._utf8_encode(input);

        while (i < input.length) {

            chr1 = input.charCodeAt(i++);
            chr2 = input.charCodeAt(i++);
            chr3 = input.charCodeAt(i++);

            enc1 = chr1 >> 2;
            enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
            enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
            enc4 = chr3 & 63;

            if (isNaN(chr2)) {
                enc3 = enc4 = 64;
            } else if (isNaN(chr3)) {
                enc4 = 64;
            }

            output = output +
            this._keyStr.charAt(enc1) + this._keyStr.charAt(enc2) +
            this._keyStr.charAt(enc3) + this._keyStr.charAt(enc4);

        }

        return output;
    },

    // private method for UTF-8 encoding
    _utf8_encode: function (string) {
        string = string.replace(/\r\n/g, "\n");
        var utftext = "";

        for (var n = 0; n < string.length; n++) {

            var c = string.charCodeAt(n);

            if (c < 128) {
                utftext += String.fromCharCode(c);
            }
            else if ((c > 127) && (c < 2048)) {
                utftext += String.fromCharCode((c >> 6) | 192);
                utftext += String.fromCharCode((c & 63) | 128);
            }
            else {
                utftext += String.fromCharCode((c >> 12) | 224);
                utftext += String.fromCharCode(((c >> 6) & 63) | 128);
                utftext += String.fromCharCode((c & 63) | 128);
            }

        }

        return utftext;
    },

    encodeBytes: function (input) {
        var output = "";
        var byte1, byte2, byte3, enc1, enc2, enc3, enc4;
        var i = 0;

        while (i < input.length) {

            byte1 = input[i++];
            byte2 = input[i++];
            byte3 = input[i++];

            enc1 = byte1 >> 2;
            enc2 = ((byte1 & 3) << 4) | (byte2 >> 4);
            enc3 = ((byte2 & 15) << 2) | (byte3 >> 6);
            enc4 = byte3 & 63;

            if (isNaN(byte2)) {
                enc3 = enc4 = 64;
            } else if (isNaN(byte3)) {
                enc4 = 64;
            }

            output = output +
            this._keyStr.charAt(enc1) + this._keyStr.charAt(enc2) +
            this._keyStr.charAt(enc3) + this._keyStr.charAt(enc4);

        }

        return output;
    }
};