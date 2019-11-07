// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

// SyncController is the class encapsulating core synchronization logic. It first
// talks to storage layer to enumerate changes, upload them to server and download
// changes from server. Server blob is also updated during the upload/download
// process accordingly.
function SyncController(opt) {
    var _opt = opt;
    // Helper function to avoid calling a null callback
    function _ProcessSyncDoneCallback() {
        if (_opt.syncDoneCallback != null) {
            _opt.syncDoneCallback();
        }
    }
    // Output sync error. SyncFormatter will output details.
    function _OnError(status) {
        TraceMsg("[Sync Error]:", "Error occurs during sync. Please check logs below.");
    }
    // Send a download change request
    function _DownloadChanges() {
        var req = new SyncFormatter();
        req.serverBlob = _opt.storage.getServerBlob();
        req.sendRequest(
            _opt.downloadServiceUri + encodeURIComponent(_opt.user.id),
            // Handle download change response
            function (res) {
                // Save change and server blob
                for (var i in res.results) {
                    _opt.storage.setEntity(res.results[i]);
                }
                _opt.storage.setServerBlob(res.serverBlob);
                _opt.storage.commit();
                // If there is more change, initiate another downdload request
                if (res.moreChangesAvailable) {
                    _DownloadChanges();
                }
                else {
                    _ProcessSyncDoneCallback();
                }
            },
            _OnError,
            "Download Change");
    }
    // Helper function to set dirty bit in a list of changes given an entity
    function _SetDirtyBit(changes, entity) {
        for (var j in changes) {
            if (entity.__metadata.uri) {
                if (changes[j].__metadata.uri == entity.__metadata.uri) {
                    changes[j].__metadata.isDirty = true;
                }
            }
            else if (entity.__metadata.tempId) {
                if (changes[j].__metadata.tempId == entity.__metadata.tempId) {
                    changes[j].__metadata.isDirty = true;
                }
            }
        }
    }
    // Send an upload change request if there is local change
    function _UploadChanges() {
        var req = new SyncFormatter();
        req.serverBlob = _opt.storage.getServerBlob();
        req.results = _opt.storage.enumerateChanges();
        if (req.results.length > 0) {
            req.sendRequest(
                _opt.uploadServiceUri + encodeURIComponent(_opt.user.id),
                // Handle upload change response
                function (res) {
                    // First assume all changes are applied successfully
                    for (var i in req.results) {
                        req.results[i].__metadata.isDirty = false;
                    }
                    // Look for errors/conflicts/new ids
                    for (var i in res.results) {
                        // Mark entity with errors
                        if (res.results[i].__syncError) {
                            _SetDirtyBit(req.results, res.results[i]);
                        }
                        // Save conflict resolution and mark unsolved conflict
                        if (res.results[i].__syncConflict) {
                            if (res.results[i].__syncConflict.conflictResolution) {
                                _opt.storage.setEntity(res.results[i]);
                            }
                            else {
                                _SetDirtyBit(req.results, res.results[i]);
                            }
                        }
                        // Save id of newly added entity
                        if (res.results[i].__metadata.tempId) {
                            _opt.storage.setEntity(res.results[i]);
                        }
                    }
                    // Save server blob and flush
                    _opt.storage.setServerBlob(res.serverBlob);
                    _opt.storage.commit();
                    _DownloadChanges();
                },
                _OnError, "Upload Change");
        }
        else {
            _DownloadChanges();
        }
    }
    // Initiate a sync
    this.sync = function () {
        _UploadChanges()
    };
}