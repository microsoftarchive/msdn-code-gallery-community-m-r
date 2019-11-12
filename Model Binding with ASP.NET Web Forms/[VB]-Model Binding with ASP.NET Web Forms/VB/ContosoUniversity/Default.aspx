<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="ContosoUniversity._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
                <h2>Model Binding with ASP.NET Web Forms.</h2>
            </hgroup>
            <p>
                This sample project accompanies the 
                <a href="http://go.microsoft.com/fwlink/?LinkId=286117">Model Binding and Web Forms</a> tutorial series.
                The tutorial series describes the steps to create this project.
            </p>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Prerequisites:</h3>
    <ol class="round">
        <li class="one">
            <h5>Install NuGet packages</h5>
            The NuGet packages that are not installed by default in a Web Forms project were not included in this project. 
            You must manually install those packages for the project to work correctly.<br /><br />
            To install the packages, in the Package Manager Console, run:
            <br /><br />
            <code>
                install-package JuiceUI<br />
                install-package DynamicDataTemplatesVB<br />
                install-package ASPNet.ScriptManager.jQuery -Version 1.8.3<br />
                install-package ASPNet.ScriptManager.jQuery.UI.Combined -Version 1.9.2<br />
            </code>
        </li>
        <li class="two">
            <h5>Edit the DateTime_Edit.ascx file</h5>
            The dynamic data template package that was installed in the previous step must be edited to include the popup calender.
             This change is explained in the <a href="http://go.microsoft.com/fwlink/?LinkId=286118">Integrating JQuery UI Datepicker with model binding and web forms</a> topic. 
            Open DateTime_Edit.ascx file, and add the following line above the TextBox element:<br /><br />
            <code>
                &lt;juice:Datepicker ID="t1" TargetControlID="TextBox1" MinDate="1/1/2013" runat="server" /&gt;
            </code>
            <br /><br /> 
            Open DateTime_Edit.ascx.cs file and add the following code to the Page_Load method:<br />
            <code>
                Dim ra = CType(Column.Attributes(GetType(RangeAttribute)), RangeAttribute) <br />
                If Not ra Is Nothing Then <br />
                    t1.MinDate = ra.Minimum.ToString()<br /> 
                    t1.MaxDate = ra.Maximum.ToString() <br />
                End If<br />
            </code>
        </li>
    </ol>
    <h3>One difference to note:</h3>
    This project differs slightly from the tutorial series in that this project contains <strong>Students1</strong> and 
    <strong>Students2</strong> links. The tutorial series only contains a <strong>Students</strong> link. The first five parts of the series show 
    how to use model binding code in the code-behind file. The sixth part of the series shows how to use model binding code in a repository class.
    Both approaches are included in this project.<br /><br />
    Students1, AddStudent1, and Courses1 - use code-behind file<br />
    Students2, AddStudent2, and Courses2 - use repository class
</asp:Content>
