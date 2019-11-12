<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true"  CodeBehind="Register.aspx.cs" Inherits="MyProject.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="StyleSheet1.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    #Content2
    {   background-color:#B5C7DE;
      
        
            Border-Color:#E7E7FF;
            border-width:1px;
            
             
        }
        #pra
        {margin-left:1px;}
    
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <table width="800px" cellpadding="3" ForeColor="#4A3C8C" >
    <tr>
        <td>
            First Name
        </td>
        <td>
            <asp:TextBox ID="TextBox1" runat="server" Height="22px" Width="189px"></asp:TextBox>
            <asp:RequiredFieldValidator Display="Dynamic"  ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="TextBox1" ForeColor="#FF3300" ErrorMessage="It is a mandatory field Can't be left blank"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr align="left">
        <td>
            Last Name
        </td>
        <td>
            <asp:TextBox ID="TextBox2" runat="server" Height="22px" Width="189px"></asp:TextBox>
            <asp:RequiredFieldValidator Display="Dynamic"  ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="TextBox2" ForeColor="#FF3300" ErrorMessage="It is a mandatory field Can't be left blank"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            Email Id</td>
            
            <td><asp:TextBox ID="TextBox3" runat="server" Height="22px" Width="189px" 
                         ></asp:TextBox> 
             <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator3" runat="server" 
                ForeColor="#FF3300" ControlToValidate="TextBox3" ErrorMessage="It is a mandatory field Can't be left blank"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator Display="Dynamic" ID="RegularExpressionValidator1" runat="server" 
                 ForeColor="#FF3300" ControlToValidate="TextBox3" ErrorMessage="Not in the correct format i.e abc@domain" 
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </td>
          
          
        
    </tr>
    <tr>
        <td>
            User Id
        </td>
        <td>
             
    
            <asp:TextBox ID="TextBox7"   runat="server" EnableViewState="true" Height="22px" 
                Width="189px"   ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4"  Display="Dynamic" runat="server" 
                ControlToValidate="TextBox7" 
                ErrorMessage="It is a mandatory field Can't be left blank" ForeColor="#FF3300"></asp:RequiredFieldValidator>
           
                <asp:CustomValidator ID="CustomValidator1" runat="server" 
                    ControlToValidate="TextBox7" 
                ErrorMessage="user id not available try another" onservervalidate="val" ForeColor="#FF3300"></asp:CustomValidator>
           
            

        </td>
        </tr>
    <tr>
        <td>
            Password
        </td>
        <td>
            <asp:TextBox ID="TextBox4" runat="server" EnableViewState="true" TextMode="Password" Height="22px" Width="189px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="Dynamic" runat="server" 
                ForeColor="#FF3300" ControlToValidate="TextBox4" ErrorMessage="It is a mandatory field Can't be left blank"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            Full Address
        </td>
        <td>
            <asp:TextBox ID="TextBox5" runat="server" Height="64px" TextMode="MultiLine" Width="189px"></asp:TextBox>
            <asp:RequiredFieldValidator  ID="RequiredFieldValidator6" runat="server" 
                ForeColor="#FF3300" ControlToValidate="TextBox5" Display="Dynamic" ErrorMessage="It is a mandatory field Can't be left blank"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td>
            Mobile No.
        </td>
        <td>
            <asp:TextBox ID="TextBox6" runat="server" Width="189px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                ForeColor="#FF3300" ControlToValidate="TextBox4" Display="Dynamic" ErrorMessage="It is a mandatory field Can't be left blank"></asp:RequiredFieldValidator>
            <asp:RangeValidator ID="RangeValidator1" runat="server" 
               ForeColor="#FF3300" ControlToValidate="TextBox6" Display="Dynamic" ErrorMessage="Not in the correct format" 
                MaximumValue="9999999999" MinimumValue="1000000000" Type="Double"></asp:RangeValidator>
        </td>
    </tr>
</table>

    <asp:ImageButton ID="ImageButton1" runat="server" 
        ImageUrl="~/img/str/buttons/submit_blue.png" onclick="ImageButton1_Click" />
<br /><br />

    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:TextBox ID="user" runat="server" AutoPostBack="true" ontextchanged="CheckUsername" ></asp:TextBox>
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>--%>

</asp:Content>
