<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="login1.aspx.cs" Inherits="MyProject.login1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    User Name<br />
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
    ControlToValidate="TextBox1" ErrorMessage="fill usename "></asp:RequiredFieldValidator>
<br />

Password<br />
    <asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
    ControlToValidate="TextBox2" ErrorMessage="fill password"></asp:RequiredFieldValidator>
<br />


<asp:ImageButton ID="ImageButton3" runat="server" AlternateText="sign in" 
    onclick="ImageButton3_Click" ImageUrl="~/img/str/buttons/sign in.bmp" />


<br />
<br />
<asp:Label ID="Label1" runat="server"></asp:Label>


    <br />


    <a href="reg/Password recover.aspx" style={color:red;} >Forgot passward</a>

    
</asp:Content>
