<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication2.Account.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            font-size: x-large;
            font-family: "Adobe Garamond Pro Bold";
        }
        .style2
        {
            width: 100%;
            border: 5px solid #808080;
            background-color: #F9F9F9;
        }
        .style5
        {
            width: 221px;
            text-align: right;
        }
        .style6
        {
            width: 200px;
        }
        #Reset1
        {
            height: 21px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        &nbsp;</p>
    <p>
       
        <span class="style1"></p>
    <asp:Panel ID="Panel1" runat="server" Height="275px">
        <table class="style2" dir="ltr">
            <tr>
                <td class="style5">
                    &nbsp;Enter User Name:</td>
                <td class="style6">
                    <asp:TextBox ID="TextBoxUserName" runat="server" Width="180px"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="Usernamevalidator" runat="server" 
                        ControlToValidate="TextBoxUserName" ErrorMessage="User Name Feild Empty!" 
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style5">
                    Enter Password:</td>
                <td class="style6">
                    <asp:TextBox ID="TextBoxPassword" runat="server" Width="180px" 
                        TextMode="Password"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="passwordvalidatore" runat="server" 
                        ErrorMessage="Password Feild Empty!" ForeColor="Red" 
                        ControlToValidate="TextBoxPassword"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style5">
                    &nbsp;</td>
                <td class="style6">
                    <asp:Button ID="B_Log_in" runat="server" Text="Log In" 
                        onclick="B_Log_in_Click" />
                </td>
                <td>
                    <span class="style1">
                    <input id="Reset1" type="reset" value="reset" />
                    </span></td>
            </tr>
            <tr>
                <td class="style5">
                    <asp:HyperLink ID="LinkRegister" runat="server" 
                        NavigateUrl="~/Account/Register.aspx">Register</asp:HyperLink>
                </td>
                <td class="style6">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
    </span>
    <p>
        &nbsp;</p>
</asp:Content>
