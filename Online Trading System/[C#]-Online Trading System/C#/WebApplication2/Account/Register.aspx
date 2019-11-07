<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebApplication2.Account.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
            border: 5px solid #808080;
            background-color: #f9f9f9;
        }
        .style2
        {
            width: 215px;
            text-align: right;
        }
        .style3
        {
            width: 118px;
        }
        #Reset1
        {
            width: 56px;
        }
        .style4
        {
            width: 635px;
        }
        .style5
        {
            width: 619px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h1>Register </h1>
<br />

    <asp:Panel ID="Panel1" runat="server" Height="281px">
        <table class="style1" dir="ltr">
            <tr>
                <td class="style2">
                    Enter User Name:</td>
                <td class="style3">
                    <asp:TextBox ID="TextBoxUserName" runat="server"></asp:TextBox>
                </td>
                <td class="style5">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="User Name Field Empty!" ForeColor="Red" 
                        ControlToValidate="TextBoxUserName"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Enter E-mail:</td>
                <td class="style3">
                    <asp:TextBox ID="TextBoxEmail" runat="server"></asp:TextBox>
                </td>
                <td class="style5">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ErrorMessage="E mail Field Empty!" ForeColor="Red" 
                        ControlToValidate="TextBoxEmail"></asp:RequiredFieldValidator>
                    <br />
                    <asp:RegularExpressionValidator ID="Email_not_correct" runat="server" 
                        ControlToValidate="TextBoxEmail" ErrorMessage="Email not correct!" 
                        ForeColor="Red" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Enter PAssword:</td>
                <td class="style3">
                    <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password"></asp:TextBox>
                </td>
                <td class="style5">
                    <asp:RequiredFieldValidator ID="Password_validator" runat="server" 
                        ControlToValidate="TextBoxPassword" ErrorMessage="Password Field Empty" 
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Confirm Password:</td>
                <td class="style3">
                    <asp:TextBox ID="TextBoxConfirm" runat="server" TextMode="Password"></asp:TextBox>
                </td>
                <td class="style5">
                    <asp:RequiredFieldValidator ID="confirm_validator" runat="server" 
                        ControlToValidate="TextBoxConfirm" ErrorMessage="Confirm Field Empty!" 
                        ForeColor="Red"></asp:RequiredFieldValidator>
                    <br />
                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ControlToCompare="TextBoxPassword" ControlToValidate="TextBoxConfirm" 
                        ErrorMessage=" Password Compare not Valid!" ForeColor="Red"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
                <td class="style5">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:HyperLink ID="LinkLogIn" runat="server" NavigateUrl="~/Account/Login.aspx">Log In</asp:HyperLink>
                </td>
                <td class="style3">
                    <asp:Button ID="B_Register" runat="server" style="text-align: right" 
                        Text="Register" onclick="B_Register_Click" />
                </td>
                <td class="style5">
                    <input id="Reset1" type="reset" value="reset" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
