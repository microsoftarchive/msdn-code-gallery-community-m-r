<%@ Page Title="About Us" Trace="false" Language="C#" MasterPageFile="~/Site.master"
    AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="WebApplication2.About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <script type='text/javascript' src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script type='text/javascript' src='\Scripts\JScript1.js'></script>
    <h2>
        <asp:Label ID="storenamelabel" runat="server" Text="Store Name"></asp:Label>
    </h2>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Table ID="Table1" runat="server" Width="920px">
        <asp:TableRow BorderColor="Black">
            <asp:TableCell BorderColor="Black" BorderWidth="2px">
                <asp:Panel runat="server" ScrollBars="Auto" ID="Panel2" CssClass="bigpanel">
                </asp:Panel>
            </asp:TableCell>
            <asp:TableHeaderCell BorderColor="Black" Width="200px" BorderWidth="1px">
                <asp:Label ID="ShoppingList" runat="server" Text="Shopping List" CssClass="shop"></asp:Label>
                <asp:UpdatePanel ID=updatepanel1 runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="Panel1" CssClass="panel_right" ScrollBars="Auto" runat="server">
                          
                            <asp:GridView PageSize="15" CssClass="grid1" ID="GridView1" runat="server" OnRowDataBound="mygrid_RowDataBound"
                                CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>
                            <asp:Button ID="Buy_Pruduct" runat="server" CssClass="Buy_all" Text="Buy all Product!"
                                OnClick="buy_all_product" />
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageAlign="Left" Height="35px"
                                Width="32px" ImageUrl="~/Images/market.bmp" />
                        </asp:Panel>
                    </ContentTemplate>
                  
                </asp:UpdatePanel>
            </asp:TableHeaderCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
