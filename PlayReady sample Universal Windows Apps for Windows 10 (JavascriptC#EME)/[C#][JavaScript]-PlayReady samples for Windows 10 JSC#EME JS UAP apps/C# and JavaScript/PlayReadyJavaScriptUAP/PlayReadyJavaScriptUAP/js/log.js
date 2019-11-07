//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

////////////////////////////////////////////
//
// logging Functionality
//
///////////////////////////////////////////
function genTimeStamp(){
    var currentdate = new Date();
    var timeStamp = currentdate.getHours() + ':' +
                    currentdate.getMinutes() + ':' +
                    currentdate.getSeconds() + ' ';
    return timeStamp;
}

function logMsg(strmsg, numDash) {

    var strDash = '';

    if (numDash != undefined) {
        for (var i = 0; i < numDash; i++) {
            strDash += '-';
        }
    }

    //add extra space after the dashes
    strDash += ' ';
  
    document.getElementById('myText').textContent = genTimeStamp() +
                                                    strDash +
                                                    strmsg + '\n' +
                                                    document.getElementById('myText').textContent;
}

function logMsg_internal(strmsg) {

    document.getElementById('myText').textContent = strmsg + '\n' +
                                                   document.getElementById('myText').textContent;
}

function logException(funcInfo, ex) {
    var taLog = document.getElementById('myText');
    taLog.style.color = "red";

    if (ex) {
        taLog.textContent = genTimeStamp() +
                            funcInfo + ' ' +
                            ex.toString() + '\n' +
                            taLog.textContent;

        if (typeof (ex.number) != 'undefined') {
            var errDetail = intToHexString(ex.number) + ': ' + ex.description;
            taLog.textContent = errDetail + taLog.textContent;
            taLog.textContent = ' stack:' + ex.stack + '\n' + taLog.textContent;
        }
    }
};
