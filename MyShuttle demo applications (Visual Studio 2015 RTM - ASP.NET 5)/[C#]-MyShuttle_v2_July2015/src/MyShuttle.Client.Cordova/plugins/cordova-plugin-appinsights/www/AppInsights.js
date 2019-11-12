
/*
Copyright (c) Microsoft Open Technologies, Inc.  All Rights Reserved.
Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.
*/

/*global module*/

var appInsightsConfig = {
    // To override this stub there is a three ways
    // * pass --variable INSTRUMENTATION_KEY=<your_key> to `cordova plugin add` command
    //      - THIS DOESN'T WORK YET DUE TO ISSUE WITH CORDOVA-LIB
    //
    // * add <preference name="instrumentation_key" value="<your_key>"> to config.xml at the root of project
    // * update this file manually
    instrumentationKey: "PASTE_YOUR_KEY_HERE_!!!",
    // Need to specify this explicitly, because default value doesn't provide URL scheme
    endpointUrl: "https://dc.services.visualstudio.com/v2/track"
};

module.exports = appInsightsConfig;
