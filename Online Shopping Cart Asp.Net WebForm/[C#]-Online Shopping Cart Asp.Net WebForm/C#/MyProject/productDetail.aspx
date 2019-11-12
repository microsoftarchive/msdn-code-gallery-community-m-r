<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"
    CodeBehind="productDetail.aspx.cs" Inherits="MyProject.productDetail"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style2
        {
            width: 187px;
        }
        #dl
        {
            margin-left:300px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
   <table id="dl">
   <tr><td>
    <asp:DataList ID="DataList1" runat="server" DataKeyField="id" 
        DataSourceID="SqlDataSource1" onitemdatabound="use" BackColor="White" 
           BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
           GridLines="Horizontal">
        <AlternatingItemStyle BackColor="#F7F7F7" />
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
        <ItemTemplate>
            PRODUCT NAME:
            
            <asp:Label ID="productNameLabel" runat="server" Text='<%# Eval("productName") %>' />
            <br />
            BRAND:
            <asp:Label ID="brandLabel" runat="server" Text='<%# Eval("brand") %>' />
            <br />
            <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("image") %>' Height="250px" Width="200px" />
            <br />
            CATAGORY:
            <asp:Label ID="catagoryLabel" runat="server" Text='<%# Eval("catagory") %>' />
            <br />
            PRICE:
            <asp:Label ID="priceLabel" runat="server" Text='<%# Eval("price") %>' />

            <br />
            <asp:ImageButton ID="ImageButton1" CausesValidation="false" runat="server" ImageUrl="~/img/buy_now.png" PostBackUrl='<%#"https://www.paypal.com/cgi-bin/webscr?cmd=_xclick&business=wd.pra_1317033108_biz@gmail.com&image_url=http://www.gravatar.com/avatar/6329074fd4edc72597a688f2c65ff44c.png&item_name="+Eval("brand")+" "+Eval("productName")+"&item_number=132&amount="+Eval("price")%>' />
            
        </ItemTemplate>
        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
    </asp:DataList>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:shopingConnectionString1 %>"
        SelectCommand="SELECT [id], [productName], [brand], [image], [catagory], [price] FROM [product] WHERE ([productName] = @productName)">
        <SelectParameters>
            <asp:QueryStringParameter Name="productName" QueryStringField="pName" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    </td><td class="style2">
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</br></br>
    <%--<asp:Button ID="Button1" runat="server" Text="Add to Cart" OnClick="Button1_Click" />--%>
    QUANTITY<br /><asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox>
           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
               ControlToValidate="TextBox1" ErrorMessage="must fill some value"></asp:RequiredFieldValidator>
           <asp:RangeValidator ID="RangeValidator1" runat="server" 
               ControlToValidate="TextBox1" ErrorMessage="must fill value 1-1000" 
               MaximumValue="1000" MinimumValue="1" Display="Dynamic" Type="Integer"></asp:RangeValidator>
           <br />
    <asp:ImageButton ID="ImageButton2" runat="server"   
        ImageUrl="~/img/str/buttons/AddToCart.gif" onclick="ImageButton2_Click" />
    &nbsp;&nbsp;&nbsp;&nbsp;
   
    </td></tr></table>
   
   
   
    
    <br />
</asp:Content>
