<%@ Page Title="Home Page" Trace=false Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="WebApplication2._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <header runat="server">
        <script type='text/javascript' src='http://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js'></script>
    </header>
    <h2>
        Welcome to Online Tradin System!
    </h2>
    <p>
        Select a store from which you want to buy!</p>
    <p>
        &nbsp;<asp:Panel ID="Panel1" runat="server">
            <asp:BulletedList ID="BulletedList1" runat="server" AppendDataBoundItems="True" CausesValidation="True">
            </asp:BulletedList>
        </asp:Panel>
    </p>
</asp:Content>
