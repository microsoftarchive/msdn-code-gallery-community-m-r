// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

// Perform user login from offline cache or online
function DoLogin(name, opt, callback) {
    // Check offline cache for user identity first
    var user = opt.storage.getCachedUser();
    if (user != null) {
        if (user.name == name) {
            TraceMsg("[Login]:", "Login from local cache");
            TraceObj("[Login]:", "User Info", user);
            callback(user);
            return;
        }
    }
    // Send a GET request to server to get user's identity
    var serviceUri = opt.loginUri + encodeURIComponent(name);
    TraceMsg("[Login]:", "Login from " + serviceUri);
    $.ajax({
        success: function (data) {
            // Store user's identity to offline cache and then callback
            var user = new Object();
            user.name = name;
            user.id = data;
            opt.storage.setCachedUser(user);
            TraceObj("[Login]:", "User Info", user);
            callback(user);
        },
        error: function (req, status) {
            TraceMsg("[Login]:", "Login error (" + status + ")");
            callback(null);
        },
        url: serviceUri
    });
}