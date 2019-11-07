<%@ Page Title="" Language="C#" MasterPageFile="~/ResumeParser.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ResumeParser.Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="Server" AssociatedUpdatePanelID="pnlUpdatePanel" DisplayAfter="1">
    <ProgressTemplate>
      <div class="divWaiting">            
	    <asp:Label ID="lblWait" runat="server" 
	    Text=" Please wait... " />
	    <asp:Image ID="imgWait" runat="server" 
	    ImageAlign="Middle" ImageUrl="~/loader.gif" />
      </div>
    </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="pnlUpdatePanel" runat="server" UpdateMode="Always">
    <ContentTemplate>
    
    <br />
    <br />
    <br />
    <div class="ts-product-container"></div>

 <div class="ts-panel panel panel-info">
    <div class="panel-heading">
        <h3>Resume Parser Using Aspose.Words and Aspose.Pdf for .NET APIs</h3>
    </div>
 <div class="panel-body"> 
      
        <asp:AjaxFileUpload ID="AjaxFileUpload1"
        CssClass="alert alert-warning"
    AllowedFileTypes="doc,docx,pdf,txt"
    MaximumNumberOfFiles=10
    runat="server"
    OnUploadComplete="File_Upload"/>
      <asp:Button ID="btnParse" runat="server" Text="Parse" CssClass="btn btn-info" OnClick="btnParse_Click" />        
 </div>
 </div>
 <div class="ts-panel panel panel-heading">
 <div class="panel-title">
 <h5>Summary</h5>
 </div>   
<div class="panel-primary" > 
<asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" Width="1900px"  
CssClass="table table-bordered table-hover table-striped" Font-Size="Smaller" OnRowDataBound="GridView1_RowDataBound">
    <Columns>
        <asp:BoundField DataField="Name" HeaderText="Name" />
        <asp:BoundField DataField="Email" HeaderText="Email" />
        <asp:BoundField DataField="Phone" HeaderText="Phone #"  />
        <asp:BoundField DataField="Skills" HeaderText="Skills"  />
        <asp:BoundField DataField="Summary" HeaderText="Summary"  />
        <asp:BoundField DataField="Experience" HeaderText="Experience"  />
        <asp:BoundField DataField="Education" HeaderText="Education"  />
        <asp:TemplateField>
        <ItemTemplate>
        <asp:Button ID="imgbtn" Text="Edit" runat="server" CssClass="edit-button btn btn-primary" OnClick="imgbtn_Click" />
       
           <asp:Button ID="btnInsertText" style="visibility:hidden" runat="server" />
<asp:ModalPopupExtender id="ModalPopupExtender4" runat="server" 
	cancelcontrolid="Button6"  
	targetcontrolid="btnInsertText" popupcontrolid="pnlDetails" BackgroundCssClass="modalBackground" >
</asp:ModalPopupExtender>
        </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
</div>
</div>

<asp:panel id="pnlDetails" style="display: none; vertical-align: top; text-align:left" runat="server">
<div class="ts-panel panel panel-info" style="overflow:auto">
    <div class="panel-heading">
        <h4>Details</h4>
    </div>
 <div class="panel-body"> 

                    <asp:Table runat="server" ID="table1" CssClass="table table-striped table-bordered table-condensed">
                       <asp:TableRow>
                       <asp:TableCell> 
                       <asp:panel id="Panel1" style="vertical-align: top; text-align:left"  runat="server">
                        <asp:Label ID="Label3" runat="server" Text="Name"  /> </br>
                        <asp:TextBox ID="txtDName" Text = "" runat="server" Width="100%" ></asp:TextBox>
                        </asp:panel>
                       </asp:TableCell>
                       <asp:TableCell> 
                       <asp:panel id="Panel2" style="vertical-align: top; text-align:left" runat="server">
                        <asp:Label ID="Label4" runat="server" Text="Phone Number" /></br>
                        <asp:TextBox ID="txtDPhone" Text = "" runat="server" Width="100%" ></asp:TextBox>
                        </asp:panel>
                       </asp:TableCell>
                       </asp:TableRow>	

                       <asp:TableRow>
                       <asp:TableCell> 
                        <asp:panel id="Panel3"  runat="server">
                    <asp:Label ID="Label5" runat="server" Text="Email" /></br>
                    <asp:TextBox ID="txtDEmail" Text = "" runat="server" Width="100%" ></asp:TextBox>
                    </asp:panel>
                       </asp:TableCell>
                       <asp:TableCell> 
                      <asp:panel id="Panel10"  runat="server">
                    <asp:Label ID="Label9" runat="server" Text="Summary" /></br>
                    <asp:TextBox ID="txtDSummary" Text = "" runat="server" TextMode="MultiLine" Width="100%" ></asp:TextBox>
                    </asp:panel>
                       </asp:TableCell>
                       </asp:TableRow>	


                       <asp:TableRow>
                       <asp:TableCell> 
                       <asp:panel id="Panel4"  runat="server">
                    <asp:Label ID="Label6" runat="server" Text="Skills" /></br>
                    <asp:TextBox ID="txtDSkills" Text = "" runat="server" TextMode="MultiLine" Width="100%" ></asp:TextBox>
                    </asp:panel>
                       </asp:TableCell>
                       <asp:TableCell> 
                      <asp:panel id="Panel5"  runat="server">
                    <asp:Label ID="Label1" runat="server" Text="Experience" /></br>
                    <asp:TextBox ID="txtDExperience" Text = "" runat="server" TextMode="MultiLine" Width="100%" ></asp:TextBox>
                    </asp:panel>
                       </asp:TableCell>
                       </asp:TableRow>	

                       <asp:TableRow>
                       <asp:TableCell> 
                       <asp:panel id="Panel6"  runat="server">
                    <asp:Label ID="Label2" runat="server" Text="Education" /></br>
                    <asp:TextBox ID="txtDEducation" Text = "" runat="server" TextMode="MultiLine" Width="100%" ></asp:TextBox>
                    </asp:panel>
                       </asp:TableCell>
                       <asp:TableCell> 
                   <asp:panel id="Panel7"  runat="server">
                    <asp:Label ID="Label7" runat="server" Text="Interests" /></br>
                    <asp:TextBox ID="txtDInterests" Text = "" runat="server" TextMode="MultiLine" Width="100%" ></asp:TextBox>
                    </asp:panel>
                       </asp:TableCell>
                       </asp:TableRow>
                    
                      <asp:TableRow>
                       <asp:TableCell> 
                       <asp:panel id="Panel8"  runat="server">
                    <asp:Label ID="Label8" runat="server" Text="Languages" /></br>
                    <asp:TextBox ID="txtDLanguages" Text = "" runat="server" TextMode="MultiLine" Width="100%" ></asp:TextBox>
                    </asp:panel>
                       </asp:TableCell>
                       <asp:TableCell> 
                   <asp:panel id="Panel9" runat="server">
                    <asp:Button ID="Button5" runat="server"  Text="Update" OnClick="Button5_Click"/>
                    <asp:Button id="Button6" runat="server" Text="Cancel" />
                    </asp:panel>
                       </asp:TableCell>
                       </asp:TableRow>
                    </asp:Table>
                    </div>
</asp:panel>        
 </ContentTemplate></asp:UpdatePanel>   
</form>
</asp:Content>
