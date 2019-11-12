<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="Sample.ResetADPassword.Web.ResetPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:TextBox runat="server" ID="txtUsername"></asp:TextBox>
    <br/>
    <asp:Button runat="server" ID="btnResetPassword" Text="Reset password" 
            onclick="ResetUserPassword" />
    </div>
    </form>
</body>
</html>
