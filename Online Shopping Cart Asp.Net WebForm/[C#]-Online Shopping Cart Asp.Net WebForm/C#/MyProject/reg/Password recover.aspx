<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Password recover.aspx.cs" Inherits="MyProject.reg.Password_recover" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table><tr><td>Enter your email Id:</td><td><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
    <td><asp:ImageButton ID="ImageButton3" runat="server" 
        AlternateText="Click to get password" onclick="ImageButton3_Click" 
        ImageUrl="~/img/str/buttons/btn_send.png" />
      </td> </tr>
 </table>
    <br />

    <asp:Label ID="Label1" runat="server"></asp:Label>
</asp:Content>
