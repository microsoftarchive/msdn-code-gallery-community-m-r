function redirect(listItem) {
    var clientId = '[ClientId]';
    var resource = 'https://remotevacationdev.azurewebsites.net/'
    var thisUrl = window.location.href;

    thisUrl = thisUrl.substring(0, thisUrl.substr(8).indexOf('/') + 8) + '/app/close.aspx';
    var authUrl = 'https://login.windows.net/[tenantname].onmicrosoft.com/oauth2/authorize?response_type=code&resource=' + resource + '&client_id=' + clientId + '&redirect_uri=' + thisUrl;
    var authWindow = window.open(authUrl, 'SharePoint App Authorization', 'height=800,width=800');
    var pollTimer = window.setInterval(
        function () {
            if (authWindow.closed) {
                window.clearInterval(pollTimer);
                window.location.replace('home.aspx?subject=' + listItem);
            }
        }, 200);
}

function getQueryStringParameter(paramToRetrieve) {
    var params =
        document.URL.split("?")[1].split("&");
    var strParams = "";
    for (var i = 0; i < params.length; i = i + 1) {
        var singleParam = params[i].split("=");
        if (singleParam[0] == paramToRetrieve)
            return singleParam[1];
    }
}