<%@ Import Namespace="System.Threading" %>

<%@ Page Language="C#" AutoEventWireup="true" UICulture="auto" Culture="auto" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Microsoft Media Platform Video Editor</title>
    <style type="text/css">
        html
        {
            overflow-y: auto;
        }
    </style>
    <script type="text/javascript">
        var RCEBridge = {
            OpenSearchPanel: function () {
                window.open('popup.html', 'Search', 'witdh=600,height=400');
            }
        };

    </script>
</head>
<body style="height: 100%; margin: 0;">
    <form id="form1" runat="server" style="height: 100%;">
    <div id="silverlightControlHost">
        <object id="silverlightControl" data="data:application/x-silverlight-2," type="application/x-silverlight-2"
            width="100%" height="100%">
            <param name="source" value="ClientBin/RCE.xap" />
            <param name="onerror" value="onSilverlightError" />
            <param name="background" value="black" />
            <param name="Culture" value="<%=Thread.CurrentThread.CurrentUICulture %>" />
            <param name="UICulture" value="<%=Thread.CurrentThread.CurrentUICulture %>" />
            <param name="minRuntimeVersion" value="3.0.40624.0" />
            <param name="autoUpgrade" value="true" />
            <param name="splashscreensource" value="SplashScreen/SplashScreen.xaml" />
            <param name="onSourceDownloadProgressChanged" value="onSourceDownloadProgressChanged" />
            <param name="EnableGPUAcceleration" value="false" />
            <!-- <param name="EnableCacheVisualization" value="true" /> -->
            <param name="initParams" value="settings=/Settings.xml" />
            <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=3.0.40624.0" style="text-decoration: none">
                <img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight"
                    style="border-style: none" />
            </a>
        </object>
        <iframe id="_sl_historyFrame" style="visibility: hidden; height: 0px; width: 0px;border: 0px"></iframe>
    </div>
    </form>
    <script type="text/javascript" src="SplashScreen/splashscreen.js"></script>    
</body>
</html>
