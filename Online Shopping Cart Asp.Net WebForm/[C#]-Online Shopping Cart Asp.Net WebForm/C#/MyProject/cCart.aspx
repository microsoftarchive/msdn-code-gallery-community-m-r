<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="cCart.aspx.cs" Inherits="MyProject.cCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
.GridView1{ margin-left:100px;}
#price{ margin-left:100px;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width="100%"><tr><td>
    <asp:GridView ID="GridView1" CssClass="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="id" DataSourceID="SqlDataSource1" 
        EmptyDataText="No Item in the Cart" 
        onrowdatabound="GridView1_RowDataBound" 
        onrowdeleted="GridView1_RowDeleted" BackColor="White" 
        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" 
        >
        <AlternatingRowStyle BackColor="#F7F7F7" />
        <Columns>
            <asp:CommandField ShowDeleteButton="True" />
            <asp:BoundField DataField="pName" HeaderText="PRODUCT" SortExpression="pName" />
            <asp:BoundField DataField="brand" HeaderText="BRAND" SortExpression="brand" />
            <asp:TemplateField HeaderText="img" SortExpression="img">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("img") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Image ID="Image1" runat="server" Width="250px" Height="300px" ImageUrl='<%# Bind("img")  %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="quantity" HeaderText="QUANTITY" 
                SortExpression="quantity" />
            <asp:BoundField DataField="price" HeaderText="COST" SortExpression="price" />
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
    </asp:GridView>
    </td>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:shopingConnectionString1 %>" 
        DeleteCommand="DELETE FROM [completeCart] WHERE [id] = @id" 
        InsertCommand="INSERT INTO [completeCart] ([uName], [pName], [brand], [img], [quantity], [price]) VALUES (@uName, @pName, @brand, @img, @quantity, @price)" 
        SelectCommand="SELECT * FROM [completeCart] WHERE ([uName] = @uName)" 
        UpdateCommand="UPDATE [completeCart] SET [uName] = @uName, [pName] = @pName, [brand] = @brand, [img] = @img, [quantity] = @quantity, [price] = @price WHERE [id] = @id">
        <DeleteParameters>
            <asp:Parameter Name="id" Type="Int64" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="uName" Type="String" />
            <asp:Parameter Name="pName" Type="String" />
            <asp:Parameter Name="brand" Type="String" />
            <asp:Parameter Name="img" Type="String" />
            <asp:Parameter Name="quantity" Type="Int32" />
            <asp:Parameter Name="price" Type="Int64" />
        </InsertParameters>
        <SelectParameters>
            <asp:CookieParameter CookieName="uname" Name="uName" Type="String" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="uName" Type="String" />
            <asp:Parameter Name="pName" Type="String" />
            <asp:Parameter Name="brand" Type="String" />
            <asp:Parameter Name="img" Type="String" />
            <asp:Parameter Name="quantity" Type="Int32" />
            <asp:Parameter Name="price" Type="Int64" />
            <asp:Parameter Name="id" Type="Int64" />
        </UpdateParameters>
    </asp:SqlDataSource>
    
    <td> 
    <div id="price">Total Cost<asp:TextBox ID="TextBox2" runat="server" 
            BorderColor="#CC3300" Enabled="False" 
        Font-Bold="True" ForeColor="Red"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp;
    <br /> &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;<%--<asp:ImageButton ID="ImageButton3" runat="server" AlternateText="check out" 
        onclick="ImageButton3_Click1" ToolTip="click for check out" ImageUrl="~/img/str/buttons/checkout.jpg"  />--%>
    <br />

    <asp:HyperLink ID="HyperLink1" runat="server"   >HyperLink</asp:HyperLink>
    </div>
   </td> </tr>
    </table>
</asp:Content>
