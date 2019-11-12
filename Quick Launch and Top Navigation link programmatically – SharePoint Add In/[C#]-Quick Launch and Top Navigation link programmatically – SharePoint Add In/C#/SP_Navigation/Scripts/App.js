'use strict';

var context;
var web;
context = SP.ClientContext.get_current();
var hostweburl;
var appweburl;
var appContextSite;

// This code runs when the DOM is ready and creates a context object which is needed to use the SharePoint object model
$(document).ready(function () {
    getUrl();
});

// This function get the URL informations
function getUrl() {
    hostweburl = getQueryStringParameter("SPHostUrl");
    appweburl = getQueryStringParameter("SPAppWebUrl");
    hostweburl = decodeURIComponent(hostweburl);
    appweburl = decodeURIComponent(appweburl).toString().replace("#", "");
    var scriptbase = hostweburl + "/_layouts/15/";
    $.getScript(scriptbase + "SP.RequestExecutor.js", execOperation);
}

// This function get list data from SharePoint
function execOperation() {
    var factory = new SP.ProxyWebRequestExecutorFactory(appweburl);
    context.set_webRequestExecutorFactory(factory);
    appContextSite = new SP.AppContextSite(context, hostweburl);
    web = appContextSite.get_web();
    context.load(web);
    context.executeQueryAsync(onGetWebSuccess, onFail);
}

// This function is executed if the above call is successful
function onGetWebSuccess() {
    console.log('Hello ' + web.get_title());
}

// This function is executed if the above call fails
function onFail(sender, args) {
    console.log('Failed. Error:' + args.get_message());
}

//for adding new link to Quick Launch
function AddQuickLaunchLink() {
    var ql = web.get_navigation().get_quickLaunch();
    var nnci = new SP.NavigationNodeCreationInformation();
    nnci.set_title('My Custom Link');
    nnci.set_url('/_layouts/settings.aspx');
    nnci.set_asLastNode(true);
    ql.add(nnci);
    context.load(ql);
    context.executeQueryAsync(
               function () {
                   $('#lblmessage').append("QuickLaunch link added successfully...");
               }, onFail);
}

//adding new link to Top Navigation
function AddTopNavicationLink() {
    var TopNav = web.get_navigation().get_topNavigationBar();
    var nnci = new SP.NavigationNodeCreationInformation();
    nnci.set_title('My Custom Link');
    nnci.set_url('/_layouts/settings.aspx');
    nnci.set_asLastNode(true);
    TopNav.add(nnci);
    context.load(TopNav);
    context.executeQueryAsync(
        function () {
            $('#lblmessage').append("Top Navigation link added successfully...");
            console.log("TopNav Added");
        }, onFail);
}

//Removing new link to Quick Launch
function RemoreQuickLaunchLink() {
    var ql = web.get_navigation().get_quickLaunch();
    context.load(ql);
    context.executeQueryAsync(
        function () {
            var e = ql.getEnumerator();
            var notFound = true;
            while (notFound && e.moveNext()) {
                var node = e.get_current();
                if (node.get_title() === "My Custom Link") {
                    node.deleteObject();
                    notFound = false;
                }
            }
            context.executeQueryAsync(
                function () {
                    $('#lblmessage').append("QuickLaunch link removed successfully...");
                    console.log("QuickLaunch link removed");
                },
                onFail);

        },
        onFail);
}

//removing new link to Top Navigation
function RemoveTopNavicationLink() {
    var tn = web.get_navigation().get_topNavigationBar();
    context.load(tn);
    context.executeQueryAsync(
        function () {
            var e = tn.getEnumerator();
            var notFound = true;
            while (notFound && e.moveNext()) {
                var node = e.get_current();
                if (node.get_title() === "My Custom Link") {
                    node.deleteObject();
                    notFound = false;
                }
            }
            context.executeQueryAsync(
                function () {
                    $('#lblmessage').append("Top Navigation link removed successfully...");
                    console.log("TopNav link removed");
                },
                onFail);

        },
        onFail);
}



//This function split the url and trim the App and Host web URLs
function getQueryStringParameter(paramToRetrieve) {
    var params =
    document.URL.split("?")[1].split("&");
    for (var i = 0; i < params.length; i = i + 1) {
        var singleParam = params[i].split("=");
        if (singleParam[0] == paramToRetrieve)
            return singleParam[1];
    }
}