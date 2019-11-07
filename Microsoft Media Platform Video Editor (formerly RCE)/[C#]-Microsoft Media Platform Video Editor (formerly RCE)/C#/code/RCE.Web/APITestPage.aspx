<%@ Import Namespace="System.Threading"%>
<%@ Page Language="C#" AutoEventWireup="true" UICulture="auto" Culture="auto" %>

<%@ Register Assembly="System.Web.Silverlight" Namespace="System.Web.UI.SilverlightControls"
    TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=7" />
    <title>Rough Cut Editor</title>
</head>
<body style="height:100%;margin:0;">
    <form id="form1" runat="server" style="height:100%;">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <button onclick="javascript:SetCurrentPosition(200);">Set Position To 200</button>
        <button onclick="javascript:GetMediaBin();">Get Media Bin</button>
        <button onclick="javascript:TogglePlayTimeline();">Toggle Play Timeline</button>
        <button onclick="javascript:SaveProject();">Save Project</button>
        <button onclick="javascript:StopTimeline();">Stop Timeline</button>

        <div id="silverlightControlHost">
        <object id="silverlightControl" data="data:application/x-silverlight-2," type="application/x-silverlight-2" width="100%" height="100%">
			<param name="source" value="ClientBin/RCE.xap"/>
			<param name="onerror" value="onSilverlightError" />
			<param name="background" value="black" />
			<param name="Culture" value="<%=Thread.CurrentThread.CurrentUICulture %>"  />
            <param name="UICulture" value="<%=Thread.CurrentThread.CurrentUICulture %>" />
            <param name="initParams" value="UserName=<%=Environment.UserName %>,QueryString=<%=Request.QueryString.ToString() %>, CDN=http://rcecdn/, MetadataFields=Title;Duration;Frame Rate, MaxNumberOfItems=1000, CommentTypes=Global;Playhead;Shot;Ink, UndoLevel=20"/>
			<param name="minRuntimeVersion" value="2.0.31005.0" />
			<param name="autoUpgrade" value="true" />
			<a href="http://go.microsoft.com/fwlink/?LinkID=124807" style="text-decoration: none;">
     			<img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight" style="border-style: none"/>
			</a>
		</object>
		<iframe style='visibility:hidden;height:0;width:0;border:0px'></iframe>
    </div>
    </form>
    <script type="text/javascript">
        function SetCurrentPosition(seconds) {
            var control = document.getElementById("silverlightControl");
            control.Content.Timeline.SetCurrentPosition(seconds);
        }

        function TogglePlayTimeline() {
            var control = document.getElementById("silverlightControl");
            control.Content.Player.TogglePlayTimeline();
        }

        function StopTimeline() {
            var control = document.getElementById("silverlightControl");
            control.Content.Player.StopTimeline();
        }

        function SetProjectId(projectId) {
            var control = document.getElementById("silverlightControl");
            control.Content.Shell.SetProjectId(projectId);
        }

        function GetMediaBin() {
            var control = document.getElementById("silverlightControl");
            var result = control.Content.MediaBin.GetMediaBin();

            alert(result.length.toString());
            return result;
        }

        function SaveProject() {
            var control = document.getElementById("silverlightControl");
            control.Content.Settings.SaveProject();
        }
        
    </script>
</body>
</html>