<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" EnableViewState="true" Trace="true"  CodeBehind="AdminEntry.aspx.cs" Inherits="MyProject.AdminEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 272px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   
    <table cellspacing=4 cellpadding=2 width=100%>
        <tr>
            <td class="style1">Select product CATEGORY</td>
            <td>
               
                 
                <asp:DropDownList ID="catText" runat="server" AutoPostBack="True" Width="81px" 
                    Height="19px">
                    <asp:ListItem>laptop</asp:ListItem>
                    <asp:ListItem>mobile</asp:ListItem>
                    <asp:ListItem>book</asp:ListItem>
                    <asp:ListItem>car</asp:ListItem>
                    <asp:ListItem Value="miscellaneous">miscellaneous</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style1">Enter product BRAND</td>
            <td>
                <asp:DropDownList ID="brandText" runat="server" DataSourceID="SqlDataSource1" 
                    DataTextField="brand" DataValueField="brand">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style1">Enter product NAME</td>
            <td>
                <asp:TextBox ID="nameText" runat="server" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="nameText" 
                    ErrorMessage="Please fill the name of product"   ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        
        
        
        <tr>
            <td class="style1">Enter product PRICE</td>
            <td>
                <asp:TextBox ID="priceText" runat="server"></asp:TextBox>
                <asp:RangeValidator ID="RangeValidator1" ForeColor="Red" runat="server" MinimumValue="1" ErrorMessage="Must enter a numeric value" ControlToValidate="priceText" MaximumValue="999999" Type="Integer"></asp:RangeValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" runat="server" ErrorMessage="Must enter some price" ControlToValidate="priceText"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style1">Browse product IMAGE </td>
            <td>
                <asp:FileUpload ID="FileUpload1"  runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="FileUpload1" ErrorMessage="show image path"></asp:RequiredFieldValidator>
            </td>
        </tr>


</table>




    <br />
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="SAVE" />

    





    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:shopingConnectionString1 %>" 
        ProviderName="<%$ ConnectionStrings:shopingConnectionString1.ProviderName %>" 
        SelectCommand="SELECT [brand] FROM [brand] WHERE ([category] = @category)">
        <SelectParameters>
            <asp:ControlParameter ControlID="catText" Name="category" 
                PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>

    

</asp:Content>
