<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AdminDelete.aspx.cs" Inherits="MyProject.AdminDelete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
#x{ margin-left:170px;}
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="x">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="id" DataSourceID="SqlDataSource1" BackColor="White" 
        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
        GridLines="Horizontal">
        <AlternatingRowStyle BackColor="#F7F7F7" />
        <Columns>
            <asp:CommandField ShowDeleteButton="True" />
            <asp:BoundField DataField="productName" HeaderText="PRODUCTNAME" 
                SortExpression="productName" />
            <asp:BoundField DataField="brand" HeaderText="BRAND" SortExpression="brand" />
            <asp:TemplateField HeaderText="IMAGE" SortExpression="image">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("image") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate >
                
                    <asp:Image ID="Image1" runat="server"  ImageUrl='<%# Bind("image") %>'  Height="250px" Width="200px"/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="catagory" HeaderText="CATEGORY" 
                SortExpression="catagory" />
        </Columns>
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
        <SortedAscendingCellStyle BackColor="#F4F4FD" />
        <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
        <SortedDescendingCellStyle BackColor="#D8D8F0" />
        <SortedDescendingHeaderStyle BackColor="#3E3277" />
    </asp:GridView></div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:shopingConnectionString1 %>" 
        ProviderName="<%$ ConnectionStrings:shopingConnectionString1.ProviderName %>" 
        
        SelectCommand="SELECT [productName], [brand], [image], [catagory], [id] FROM [product]" 
        DeleteCommand="DELETE FROM [product] WHERE [id] = @id" 
        InsertCommand="INSERT INTO [product] ([productName], [brand], [image], [catagory]) VALUES (@productName, @brand, @image, @catagory)" 
        UpdateCommand="UPDATE [product] SET [productName] = @productName, [brand] = @brand, [image] = @image, [catagory] = @catagory WHERE [id] = @id">
        <DeleteParameters>
            <asp:Parameter Name="id" Type="Int64" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="productName" Type="String" />
            <asp:Parameter Name="brand" Type="String" />
            <asp:Parameter Name="image" Type="String" />
            <asp:Parameter Name="catagory" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="productName" Type="String" />
            <asp:Parameter Name="brand" Type="String" />
            <asp:Parameter Name="image" Type="String" />
            <asp:Parameter Name="catagory" Type="String" />
            <asp:Parameter Name="id" Type="Int64" />
        </UpdateParameters>
    </asp:SqlDataSource>

</asp:Content>
