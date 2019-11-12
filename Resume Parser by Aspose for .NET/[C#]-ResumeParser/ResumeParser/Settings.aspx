<%@ Page Title="" Language="C#" MasterPageFile="~/ResumeParser.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="ResumeParser.Settings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    
    <br />
    <br />
    <br />
    <div class="ts-product-container"></div>

 <div class="ts-panel panel panel-info">
    <div class="panel-heading">
        <h3>Settings</h3>
    </div>
 <div class="panel-body"> 
        
    <asp:Panel ID="pnlSettings" runat="server">
       <asp:Table runat="server" ID="table1" CssClass="table table-striped table-bordered table-condensed">
       <asp:TableRow>       
       <asp:TableCell>
       <asp:Label ID="Label8" runat="server" Text="Summary" /><br></br>
        <asp:TextBox runat="server" ID="txtSummary" Width="100%" TextMode="MultiLine"></asp:TextBox>
        </asp:TableCell><asp:TableCell> 
       <asp:Label ID="Label1" runat="server" Text="Specialties" /><br></br>
       <asp:TextBox runat="server" ID="txtSpecialties" Width="100%" TextMode="MultiLine"></asp:TextBox>
       </asp:TableCell></asp:TableRow><asp:TableRow>       
       <asp:TableCell>
       <asp:Label ID="Label2" runat="server" Text="Skills" /><br></br> 
       <asp:TextBox runat="server" ID="txtSkills" Width="100%" TextMode="MultiLine">
       </asp:TextBox>
       </asp:TableCell><asp:TableCell> 
       <asp:Label ID="Label3" runat="server" Text="Experience" /><br></br>
       <asp:TextBox runat="server" ID="txtExperience" Width="100%" TextMode="MultiLine">
       </asp:TextBox>
       </asp:TableCell></asp:TableRow><asp:TableRow>
       <asp:TableCell>
       <asp:Label ID="Label4" runat="server" Text="Education" /><br></br> 
       <asp:TextBox runat="server" ID="txtEducation" Width="100%" TextMode="MultiLine">
       </asp:TextBox>
       </asp:TableCell><asp:TableCell> 
       <asp:Label ID="Label5" runat="server" Text="Interests" /><br></br>
       <asp:TextBox runat="server" ID="txtInterest" Width="100%" TextMode="MultiLine">
       </asp:TextBox>
       </asp:TableCell></asp:TableRow><asp:TableRow>
       <asp:TableCell> 
       <asp:Label ID="Label6" runat="server" Text="Languages" /><br></br>
       <asp:TextBox runat="server" ID="txtLanguage" Width="100%" TextMode="MultiLine">
       </asp:TextBox>
       </asp:TableCell><asp:TableCell> 
       <asp:Button runat="server" ID="btnUpdate" Text="Update" OnClick="btnUpdate_Click">
       </asp:Button>
       </asp:TableCell></asp:TableRow></asp:Table></asp:Panel></div></div></div></form></asp:Content>