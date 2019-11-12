/*!
* <copyright file="BlockBlobUpload.js" company="Microsoft Corporation">
*  Copyright 2011 Microsoft Corporation
* </copyright>
* Licensed under the MICROSOFT LIMITED PUBLIC LICENSE version 1.1 (the "License"); 
* You may not use this file except in compliance with the License. 
*/

/*global Node, FormData, $, document, setTimeout, request, window*/
var uploader;
var jqxhr;
var maxRetries = 3;
var retryAfterSeconds = 5;
var operationType = {
    "METADATA_SEND": 0,
    "CANCELLED": 1,
    "RESUME_UPLOAD": 2,
    "METADATA_FAILED": 3,
    "FILE_NOT_SELECTED": 4,
    "UNSUPPORTED_BROWSER": 5,
    "ZERO_BYTE_FILE": 6
};

var ChunkedUploader = {
    constructor: function (controlElements) {
        this.file = controlElements.fileControl.files[0];
        this.fileControl = controlElements.fileControl;
        this.statusLabel = controlElements.statusLabel;
        this.progressElement = controlElements.progressElement;
        this.uploadButton = controlElements.uploadButton;
        this.cancelButton = controlElements.cancelButton;
        this.totalBlocks = controlElements.totalBlocks;
    },
    isElementNode: function (node) {
        return !!(node.nodeType && node.nodeType === Node.ELEMENT_NODE);
    },
    clearChildren: function (node) {
        if (this.isElementNode(node)) {
            while (node.firstChild) {
                node.removeChild(node.firstChild);
            }
        }
    },
    displayStatusMessage: function (message) {
        this.clearChildren(this.statusLabel);
        if (message) {
            this.statusLabel.appendChild(document.createTextNode(message));
        }
    },
    initializeUpload: function () {
        this.displayStatusMessage('');
        this.uploadButton.setAttribute('disabled', 'disabled');
        this.fileControl.setAttribute('disabled', 'disabled');
        this.cancelButton.removeAttribute('disabled');
    },
    resetControls: function () {
        this.progressElement.setAttribute('hidden', 'hidden');
        this.cancelButton.setAttribute('disabled', 'disabled');
        this.fileControl.removeAttribute('disabled');
        this.uploadButton.removeAttribute('disabled');
        this.fileControl.value = '';
    },
    displayLabel: function (operation) {
        switch (operation) {
            case operationType.METADATA_SEND:
                this.displayStatusMessage('Sending file metadata to server. Please wait..');
                break;
            case operationType.CANCELLED:
                this.displayStatusMessage('File upload has been cancelled.');
                break;
            case operationType.RESUME_UPLOAD:
                this.displayStatusMessage('Error encountered during upload. Resuming upload..');
                break;
            case operationType.METADATA_FAILED:
                this.displayStatusMessage('Failed to send file meta data. Retry after some time.');
                break;
            case operationType.FILE_NOT_SELECTED:
                this.displayStatusMessage('Please select a file to upload.');
                break;
            case operationType.UNSUPPORTED_BROWSER:
                this.displayStatusMessage("Your browser does not support this functionality.");
                break;
            case operationType.ZERO_BYTE_FILE:
                this.displayStatusMessage("File should not be empty.");
                break;
        }
    },
    uploadError: function (message) {
        this.displayStatusMessage('The file could not be uploaded' + (message ? 'because ' + message : '') + '. Operation aborted.');
        if (jqxhr !== null) {
            jqxhr.abort();
        }
    },
    renderProgress: function (blocksCompleted) {
        var percentCompleted = Math.floor((blocksCompleted - 1) * 100 / this.totalBlocks);
        this.progressElement.removeAttribute('hidden');
        this.progressElement.setAttribute('value', percentCompleted.toString());
        this.displayStatusMessage("Completed: " + percentCompleted + '%');
    }
};

var cancelUpload = function () {
    if (jqxhr !== null) {
        jqxhr.abort();
    }
};

var sendFile = function (blockLength) {
    var start = 0,
        end = Math.min(blockLength, uploader.file.size),
        incrimentalIdentifier = 1,
        retryCount = 0,
        sendNextChunk, fileChunk;
    uploader.displayStatusMessage();
    sendNextChunk = function () {
        fileChunk = new FormData();
        uploader.renderProgress(incrimentalIdentifier);
        if (uploader.file.slice) {
            fileChunk.append('Slice', uploader.file.slice(start, end));
        }
        else if (uploader.file.webkitSlice) {
            fileChunk.append('Slice', uploader.file.webkitSlice(start, end));
        }
        else if (uploader.file.mozSlice) {
            fileChunk.append('Slice', uploader.file.mozSlice(start, end));
        }
        else {
            uploader.displayLabel(operationType.UNSUPPORTED_BROWSER);
            return;
        }
        jqxhr = $.ajax({
            async: true,
            url: ('/Home/UploadBlock/' + incrimentalIdentifier),
            data: fileChunk,
            cache: false,
            contentType: false,
            processData: false,
            type: 'POST',
            error: function (request, error) {
                if (error !== 'abort' && retryCount < maxRetries) {
                    ++retryCount;
                    setTimeout(sendNextChunk, retryAfterSeconds * 1000);
                }

                if (error === 'abort') {
                    uploader.displayLabel(operationType.CANCELLED);
                    uploader.resetControls();
                    uploader = null;
                }
                else {
                    if (retryCount === maxRetries) {
                        uploader.uploadError(request.responseText);
                        uploader.resetControls();
                        uploader = null;
                    }
                    else {
                        uploader.displayLabel(operationType.RESUME_UPLOAD);
                    }
                }

                return;
            },
            success: function (notice) {
                if (notice.error || notice.isLastBlock) {
                    uploader.renderProgress(uploader.totalBlocks + 1);
                    uploader.displayStatusMessage(notice.message);
                    uploader.resetControls();
                    uploader = null;
                    return;
                }

                ++incrimentalIdentifier;
                start = (incrimentalIdentifier - 1) * blockLength;
                end = Math.min(incrimentalIdentifier * blockLength, uploader.file.size);
                retryCount = 0;
                sendNextChunk();
            }
        });
    };

    sendNextChunk();
};

function startUpload(fileElementId, blockLength, uploadProgressElement, statusLabel, uploadButton, cancelButton) {
    Object.freeze(operationType);
    uploader = Object.create(ChunkedUploader);
    if (!window.FileList) {
        uploader.statusLabel = document.getElementById(statusLabel);
        uploader.displayLabel(operationType.UNSUPPORTED_BROWSER);
        return;
    }

    uploader.constructor({
        "fileControl": document.getElementById(fileElementId),
        "statusLabel": document.getElementById(statusLabel),
        "progressElement": document.getElementById(uploadProgressElement),
        "uploadButton": document.getElementById(uploadButton),
        "cancelButton": document.getElementById(cancelButton),
        "totalBlocks": 0
    });
    uploader.initializeUpload();
    if (typeof uploader.file === "undefined" || uploader.file.size <= 0) {
        uploader.displayLabel(typeof uploader.file === "undefined" ? operationType.FILE_NOT_SELECTED : operationType.ZERO_BYTE_FILE);
        uploader.resetControls();
        return;
    }

    uploader.totalBlocks = Math.ceil(uploader.file.size / blockLength);
    uploader.progressElement.setAttribute('value', '0');
    uploader.displayLabel(operationType.METADATA_SEND);
    $.ajax({
        type: "POST",
        async: true,
        url: '/Home/PrepareMetadata',
        data: {
            'blocksCount': uploader.totalBlocks,
            'fileName': uploader.file.name,
            'fileSize': uploader.file.size
        },
        dataType: "json",
        error: function () {
            uploader.displayLabel(operationType.METADATA_FAILED);
            uploader.resetControls();
        },
        success: function (operationState) {
            if (operationState === true) {
                sendFile(blockLength);
            }
        }
    });
}