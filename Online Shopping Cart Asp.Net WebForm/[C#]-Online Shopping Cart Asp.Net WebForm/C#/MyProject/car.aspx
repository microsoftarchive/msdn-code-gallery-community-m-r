<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="car.aspx.cs" Inherits="MyProject.car" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style2
        {
            width: 241px;
            height: 209px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlDataSource1" RepeatDirection="Horizontal"
            RepeatColumns="3" BackColor="White" BorderColor="#E7E7FF" 
    BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" >
            <AlternatingItemStyle BackColor="#F7F7F7" />
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
            <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" Text='<%# Eval("id") %>' Visible='false'></asp:Label>
                <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("image") %>' Height="250px"
                    Width="225px" />
                <br />
                <asp:Label ID="Label1" runat="server" Text='<%# Eval("productName") %>'></asp:Label>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl='~/img/detail.jpg'   PostBackUrl='<%#"productDetail.aspx?pName="+Eval("productName") %>' />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </ItemTemplate>
            <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
  </asp:DataList>
    
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:shopingConnectionString1 %>" 
        DeleteCommand="DELETE FROM [product] WHERE [id] = @id" 
        InsertCommand="INSERT INTO [product] ([productName], [brand], [image], [catagory], [price]) VALUES (@productName, @brand, @image, @catagory, @price)" 
        SelectCommand="SELECT * FROM [product] WHERE ([catagory] = @catagory)" 
        UpdateCommand="UPDATE [product] SET [productName] = @productName, [brand] = @brand, [image] = @image, [catagory] = @catagory, [price] = @price WHERE [id] = @id">
        <DeleteParameters>
            <asp:Parameter Name="id" Type="Int64" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="productName" Type="String" />
            <asp:Parameter Name="brand" Type="String" />
            <asp:Parameter Name="image" Type="String" />
            <asp:Parameter Name="catagory" Type="String" />
            <asp:Parameter Name="price" Type="Int32" />
        </InsertParameters>
        <SelectParameters>
            <asp:Parameter DefaultValue="car" Name="catagory" Type="String" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="productName" Type="String" />
            <asp:Parameter Name="brand" Type="String" />
            <asp:Parameter Name="image" Type="String" />
            <asp:Parameter Name="catagory" Type="String" />
            <asp:Parameter Name="price" Type="Int32" />
            <asp:Parameter Name="id" Type="Int64" />
        </UpdateParameters>
    </asp:SqlDataSource>
 
</asp:Content>
