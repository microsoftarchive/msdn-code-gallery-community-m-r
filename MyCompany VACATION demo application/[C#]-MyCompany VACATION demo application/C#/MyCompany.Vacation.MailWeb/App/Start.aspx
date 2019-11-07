<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Start.aspx.cs" Inherits="MyCompany.Vacation.MailWeb.App.Start" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Scripts/jquery-1.8.2.js"></script>
    <script src="../Scripts/Office/MicrosoftAjax.js"></script>
    <script src="../Scripts/Office/1.0/office.js"></script>
    <script src="../Scripts/MyCompany.Vacation.MailWeb.js"></script>
    <script src="../Scripts/authFunctions.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <script>var session = '<%= Session["code"] %>';</script>
        <p>loading...</p>
    </div>
    </form>
</body>
</html>
