<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="MyCompany.Vacation.MailOfficeAppBasicWeb.App.Home" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        label{
            display: inline-block;
            width: 120px;
            color: #a5a5a5;
            font-family: "Segoe UI Light","Segoe WP Light";
        }

        span {
            font-family: "Segoe UI Light","Segoe WP Light";
        }

        
        input {
            cursor: pointer;
            font-family: "Segoe UI Light","Segoe WP Light";
            color: white;
            padding: 5px 10px;
        }

        .message{
            cursor: pointer;
            font-family: "Segoe UI Light","Segoe WP Light";
            color: white;
            padding: 5px 10px;
        }

        .title {
            font-weight: bold;
            margin-bottom: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="FullNameLbl" CssClass="title" runat="server"></asp:Label>
        </div>
        <br />
        <div>
            <label>State: </label>
            <asp:Label ID="StateLbl" runat="server"></asp:Label>
        </div>
        <div>
            <label>From: </label>
            <asp:Label ID="FromLbl" runat="server"></asp:Label>
        </div>
        <div>
            <label>To: </label>
            <asp:Label ID="ToLbl" runat="server"></asp:Label>
        </div>
        <div>
            <label>Days: </label>
            <asp:Label ID="NumDaysLbl" runat="server"></asp:Label>
        </div>
        <br />
        <div>
            <label>Comments </label>
        </div>
        <div>
            <asp:Label ID="CommentsLbl" runat="server"></asp:Label>
        </div>
        <br />
        <div>
            <asp:Button ID="ApproveBtn" runat="server" Text="Approve" OnClick="Approve_Click" 
                 BackColor="#2A8DD4"
                BorderStyle="None"
                 />
        </div>
        <asp:Label ID="MessageLbl" CssClass="message" runat="server"  BackColor="#a5a5a5" Visible="false"/>
    </form>
</body>
</html>
