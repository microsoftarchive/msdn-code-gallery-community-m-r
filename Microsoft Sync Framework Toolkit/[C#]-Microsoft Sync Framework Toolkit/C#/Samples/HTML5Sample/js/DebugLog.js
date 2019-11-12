// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

var debugSelector = "";
// Set jQuery selector for debug area
function SetDebugOutput(selector) {
    debugSelector = selector;
}
// Output a debug messasge
function TraceMsg(title, msg) {
    $(debugSelector)
        .prepend($("<div></div>")
            .append($("<b></b>").text(title))
            .append($("<span></span>").text(msg))
            .append($("<br/>"))
        );
}
// Output a debug message along with a JSON dump of the corresponding object
function TraceObj(title, msg, obj) {
    $(debugSelector)
        .prepend($("<div></div>")
            .append($("<b></b>").text(title))
            .append($("<span></span>").text(msg))
            .append($("<br/>"))
            .append($("<span></span>").text(JSON.stringify(obj)))
            .append($("<br/>"))
        );
}