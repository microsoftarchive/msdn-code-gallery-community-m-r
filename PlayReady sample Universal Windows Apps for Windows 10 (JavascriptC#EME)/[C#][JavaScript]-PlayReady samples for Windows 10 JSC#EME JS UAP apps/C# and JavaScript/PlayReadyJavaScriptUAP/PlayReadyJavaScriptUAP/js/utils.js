//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

function MakeUnsigned(number) {
    return number >>> 0;
}

function intToHexString(number) {
    return '0x' + MakeUnsigned(number).toString(16);
}

var decode_map = {
    '&lt;': '<',
    '&gt;': '>'
};

function xmldecode(str) {
    return str.replace(/(&lt;|&gt;)/g,
        function (itm) {
            return decode_map[itm];
        });
}

function stringToByteArrayW(strIn) {
    var rgbBytes = new Array();
    var strTmp = xmldecode(strIn);
    var cbBytes = strTmp.length;
    var i = 0;

    for (i = 0; i < cbBytes; i++) {
        var chr = strTmp.charCodeAt(i);
        rgbBytes.push(chr);
        rgbBytes.push('\0'.charCodeAt(0));
    }
    rgbBytes.push('\0'.charCodeAt(0));
    rgbBytes.push('\0'.charCodeAt(0));

    return rgbBytes;
}

function stringToByteArray(strIn) {
    var rgbBytes = new Array();
    var cbBytes = strIn.length;
    var i = 0;

    for (i = 0; i < cbBytes; i++) {
        rgbBytes.push(strIn.charCodeAt(i));
    }

    return rgbBytes;
}


function getIntBytes(x, length) {
    var bytes = [];
    var i = length;
    do {
        i--;
        bytes[i] = x & (255);
        x = x >> 8;
    } while (i)
    return bytes;
}

function encode(data) {
    var str = "";
    for (var i = 0; i < data.length; i++)
        str += String.fromCharCode(data[i]);

    return btoa(str);
}

function _generateUUID() {
    var d = new Date().getTime();
    var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = (d + Math.random() * 16) % 16 | 0;
        d = Math.floor(d / 16);
        return (c == 'x' ? r : (r & 0x7 | 0x8)).toString(16);
    });
    return uuid;
}


// GUID string with four dashes is always MSB first,
// but base-64 GUID's vary by target-system endian-ness.
// Little-endian systems are far more common.  Set le==true
// when target system is little-endian (e.g., x86 machine).
//
function guid_to_base64(g, le) {
    var hexlist = '0123456789abcdef';
    var b64list = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/';

    var s = g.replace(/[^0-9a-f]/ig, '').toLowerCase();
    if (s.length != 32) return '';

    if (le) s = s.slice(6, 8) + s.slice(4, 6) + s.slice(2, 4) + s.slice(0, 2) +
          s.slice(10, 12) + s.slice(8, 10) +
          s.slice(14, 16) + s.slice(12, 14) +
          s.slice(16);
    s += '0';

    var a, p, q;
    var r = '';
    var i = 0;
    while (i < 33) {
        a = (hexlist.indexOf(s.charAt(i++)) << 8) |
             (hexlist.indexOf(s.charAt(i++)) << 4) |
             (hexlist.indexOf(s.charAt(i++)));

        p = a >> 6;
        q = a & 63;

        r += b64list.charAt(p) + b64list.charAt(q);
    }
    r += '==';

    return r;
} 

function convertNumberTo4ByteArrayInNetworkOrder(number)
{
    var byteArray = [];
    for( var i = 3; i >= 0; i-- ) {
        byteArray[i] = number & 0xff;
        number = number >> 8;
    };
    return byteArray;
}




