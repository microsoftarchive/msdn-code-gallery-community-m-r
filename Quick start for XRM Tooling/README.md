# Quick start for XRM Tooling
## Requires
- Visual Studio 2013
## License
- MS-LPL
## Technologies
- Microsoft Dynamics CRM
- Dynamics 365 Customer Engagement
## Topics
- library assembly
- Microsoft Dynamics CRM SDK
## Updated
- 12/12/2017
## Description

<div class="content">
<div>
<div class="topic">
<h1 class="title">Quick start for XRM Tooling</h1>
<div id="mainSection">
<div id="mainBody">
<p>Applies To: Dynamics 365 (online), Dynamics 365 (on-premises), Dynamics CRM 2016, Dynamics CRM Online</p>
<div class="introduction">
<p>The QuickStart sample is a Microsoft .NET Framework managed code sample that shows how to connect to a Microsoft Dynamics 365 instance by using the XRM Tooling APIs, and perform basic create, update, retrieve, and delete operations on an entity. For more
 information about XRM Tooling, see <a href="https://docs.microsoft.com/en-us/dynamics365/customer-engagement/developer/build-windows-client-applications-xrm-tools">
Build Windows client applications using the XRM tools</a>.</p>
<p>Click to see the following files:</p>
<ul>
<li><a href="https://code.msdn.microsoft.com/XRM-Tooling-Sample-24a5c55c/sourcecode?fileId=183598&pathId=621084766">CrmLogin.xaml.cs</a>
</li><li><a href="https://code.msdn.microsoft.com/XRM-Tooling-Sample-24a5c55c/sourcecode?fileId=183598&pathId=788624547">AssemblyInfo.cs</a><strong>&nbsp;</strong><em>&nbsp;</em>
</li><li><a href="https://code.msdn.microsoft.com/XRM-Tooling-Sample-24a5c55c/sourcecode?fileId=183598&pathId=1434552886">Resources.Designer.cs</a><strong>&nbsp;</strong><em>&nbsp;</em>
</li><li><a href="https://code.msdn.microsoft.com/XRM-Tooling-Sample-24a5c55c/sourcecode?fileId=183598&pathId=1523264864">Settings.Designer.cs</a>
</li><li><a href="https://code.msdn.microsoft.com/XRM-Tooling-Sample-24a5c55c/sourcecode?fileId=183598&pathId=1001513950">App.xaml.cs</a><strong></strong><em></em>
</li><li><a href="https://code.msdn.microsoft.com/XRM-Tooling-Sample-24a5c55c/sourcecode?fileId=183598&pathId=1270463228">MainWindow.xaml.cs</a><strong></strong><em></em>
</li></ul>
<p>More information: <a class="selected" href="https://docs.microsoft.com/en-us/dynamics365/customer-engagement/developer/xrm-tooling/sample-quick-start-xrm-tooling-api" tabindex="0">
Sample: Quick start for XRM Tooling API</a> and <a href="https://docs.microsoft.com/en-us/dynamics365/customer-engagement/developer/xrm-tooling/use-xrm-tooling-classes-generated-code-generation-tool" tabindex="0">
Use XRM tooling with classes generated using the code-generation tool</a> <strong>
&nbsp;</strong><em>&nbsp;</em></p>
</div>
<div class="section">
<div class="section"></div>
</div>
<div class="section">
<h2 class="heading">Demonstrates</h2>
<div class="section">
<ul class="unordered">
<li>
<p>The sample code is built using the <strong>WPF Application for Dynamics 365</strong> SDK template that provides a common login control with built-in support for authentication and credential caching and reuse. For more information about the common login
 control and how to use the SDK template in Microsoft Visual Studio, see <a href="https://docs.microsoft.com/en-us/dynamics365/customer-engagement/developer/xrm-tooling/use-xrm-tooling-common-login-control-client-applications">
Use the XRM tooling common login control in your client applications</a>.</p>
</li><li>
<p>No helper code is used to establish a connection to Dynamics 365.</p>
</li><li>
<p>After connecting to Dynamics 365, the sample performs basic create, update, retrieve, and delete operations on an account entity.</p>
</li><li>
<p>Stores user credentials in a configuration file (Default_QuickStartXRMToolingWPFClient.exe.config) in the c:\Users\<em>&lt;username&gt;</em>\AppData\Roaming\Microsoft\QuickStartXRMToolingWPFClient folder when the sample is run for the first time, and thereafter
 prompts the user to either use the stored or specify new credentials at runtime to sign in to Dynamics 365.</p>
</li><li>
<p>Generates the following log files, if any issue occurs, to aid troubleshooting:</p>
<ul class="unordered">
<li>
<p>Login_ErrorLog.log: To report sign-in errors. This file is available at C:\Users\<em>&lt;username&gt;</em>\AppData\Roaming\Microsoft\QuickStartXRMToolingWPFClient.</p>
</li><li>
<p>QuickStartXRMToolingWPFClient.log: To report operational errors. This file is available at the same location as the executable, that is in the debug folder of your Microsoft Visual Studio project.</p>
</li></ul>
</li></ul>
</div>
<div class="LW_CollapsibleArea_Container">
<div class="LW_CollapsibleArea_TitleDiv"><span class="LW_CollapsibleArea_Title">To run the sample</span></div>
<div class="section">
<ol class="ordered">
<li>
<p>Open the QuickStartXRMToolingWPFClient.sln file in Visual Studio.</p>
</li><li>
<p>Press <strong>F5</strong> to compile and run the program.</p>
</li></ol>
</div>
</div>
</div>
<div class="section">
<div class="codeSnippetContainer" id="code-snippet-1">
<div class="code">
<div class="endscriptcode"></div>
<div class="endscriptcode">More information:&nbsp;<a href="https://docs.microsoft.com/en-us/dynamics365/customer-engagement/developer/tutorials-resources-sdk">Tutorials and resources for learning about development for Microsoft Dynamics 365</a>.</div>
</div>
</div>
</div>
<p>&nbsp;</p>
</div>
</div>
</div>
</div>
</div>
