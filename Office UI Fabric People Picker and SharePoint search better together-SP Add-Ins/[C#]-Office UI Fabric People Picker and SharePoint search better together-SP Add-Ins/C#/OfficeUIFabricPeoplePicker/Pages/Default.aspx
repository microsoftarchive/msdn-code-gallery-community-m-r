<%-- The following 4 lines are ASP.NET directives needed when using SharePoint components --%>

<%@ Page Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" MasterPageFile="~masterurl/default.master" Language="C#" %>

<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%-- The markup and script in the following Content element will be placed in the <head> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script type="text/javascript" src="../Scripts/jquery-1.9.1.min.js"></script>
    <SharePoint:ScriptLink Name="sp.js" runat="server" OnDemand="true" LoadAfterUI="true" Localizable="false" />
    <meta name="WebPartPageExpansion" content="full" />

    <!-- Add your CSS styles to the following file -->
    <link rel="stylesheet" href="https://static2.sharepointonline.com/files/fabric/office-ui-fabric-js/1.4.0/css/fabric.min.css">
    <link rel="stylesheet" href="https://static2.sharepointonline.com/files/fabric/office-ui-fabric-js/1.4.0/css/fabric.components.min.css">
    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />

    <!-- Add your JavaScript to the following file -->
    <script src="https://static2.sharepointonline.com/files/fabric/office-ui-fabric-js/1.2.0/js/fabric.min.js"></script>

    <script src="https://publiccdn.sharepointonline.com/giuleon.sharepoint.com/1288003475c3d1387170eaaeef6bf911ba865d894569959b6bc8db4bdc9a8a121fa1ba69/OfficeUIFabricPeoplePickerAddIn/PeoplePicker.js"></script>
    <script src="https://publiccdn.sharepointonline.com/giuleon.sharepoint.com/1288003475c3d1387170eaaeef6bf911ba865d894569959b6bc8db4bdc9a8a121fa1ba69/OfficeUIFabricPeoplePickerAddIn/App.js"></script>
    <!-- Internal References -->
    <!--
    <script src="../Scripts/PeoplePicker.js"></script>
    <script type="text/javascript" src="../Scripts/App.js"></script>
    -->
</asp:Content>

<%-- The markup in the following Content element will be placed in the TitleArea of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Office UI Fabric People Picker Add-In Demo
</asp:Content>

<%-- The markup and script in the following Content element will be placed in the <body> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <label class="ms-Label">Single people picker</label>
    <div class="ms-PeoplePicker">
        <div class="ms-PeoplePicker-searchBox">
            <div class="ms-TextField  ms-TextField--textFieldUnderlined ">
                <input id="_peoplePicker" class="ms-TextField-field" type="text" value="" placeholder="Select or enter an option">
            </div>
        </div>
    </div>
    <label class="ms-Label">Multiple people picker</label>
    <div class="ms-PeoplePicker">
        <div class="ms-PeoplePicker-searchBox">
            <div class="ms-TextField  ms-TextField--textFieldUnderlined ">
                <input id="_peoplePickerMulti" class="ms-TextField-field" type="text" value="" placeholder="Select or enter an option">
            </div>
        </div>
    </div>
</asp:Content>
