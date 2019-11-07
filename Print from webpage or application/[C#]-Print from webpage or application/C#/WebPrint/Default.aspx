<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebPrint.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>Print</h1>

    <b>Printer settings</b><br /><br />
    Name<br />
    <asp:TextBox ID="txtPrinterName" runat="server" Text="HP LaserJet 4100" />
    <br />
    Tray<br />
    <asp:TextBox ID="txtTray" runat="server" Text=" Tray 2" />
    <br />
    Text to print<br />
    <asp:TextBox TextMode="MultiLine" ID="txtToPrint" runat="server" Height="143px" 
            Width="409px" />
            <br /><br />
    <asp:Button ID="btnPrint" runat="server" Text="Print" Width="87px" 
            onclick="btnPrint_Click" />
    &nbsp;
        <asp:Label ID="lblPrintingStatus" runat="server" Text="Label" Visible="false" />
    </div>
    </form>
</body>
</html>
