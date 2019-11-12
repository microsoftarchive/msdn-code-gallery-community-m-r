# Quick start for Microsoft Dynamics 365
## Requires
- Visual Studio 2015
## License
- MS-LPL
## Technologies
- Microsoft Dynamics CRM
- Dynamics 365 Customer Engagement
## Topics
- library assembly
- Microsoft Dynamics CRM SDK
- Dynamics CRM web services
## Updated
- 11/24/2017
## Description

<div class="content">
<div>
<div class="topic">
<h1 class="title">Quick start to Microsoft Dynamics 365</h1>
<div id="mainSection">
<div id="mainBody">
<p>Applies To: Dynamics 365 (online), Dynamics 365 (on-premises), Dynamics CRM 2016, Dynamics CRM Online</p>
<div class="introduction">
<p>This topic shows you how to compile and run a program that creates an account record, retrieves the record, updates the record, and then prompts to see if you would like the record deleted.</p>
</div>
<div class="section">
<h2 class="heading">Prerequisites</h2>
<div class="section">
<ul class="unordered">
<li>
<p>You must be able to sign in to Microsoft Dynamics 365 or Microsoft Dynamics 365 (online).</p>
</li><li>
<p>Microsoft Visual Studio 2012, or 2013</p>
</li><li>
<p>Microsoft .NET Framework 4.5.2</p>
</li></ul>
</div>
</div>
<div class="section">
<h2 class="heading">Compile and run the Quick Start sample</h2>
<ol class="steps">
<li class="step">
<div class="section">
<p>Extract the downloaded sample.</p>
</div>
</li><li class="step">
<div class="section">
<p>Open the QuickStart.csproj file in Visual Studio.</p>
</div>
</li><li class="step">
<div class="section">
<p>Press <strong>F5</strong> to compile and run the program.</p>
</div>
</li><li class="step">
<div class="section">
<p>Enter the following information at the prompts:</p>
<table>
<tbody>
<tr>
<th>
<p>Prompt</p>
</th>
<th>
<p>Description</p>
</th>
</tr>
<tr>
<td>
<p>Enter a CRM server name and port [crm.dynamics.com]</p>
</td>
<td>
<p>Type the name of your Microsoft Dynamics 365 server. The default is Microsoft Dynamics 365 (online) (crm.dynamics.com) in North America.</p>
<p>Example: myservername</p>
<p>Example: myprefix.myservername:5500</p>
<p>Example: crm5.dynamics.com</p>
<p>Do not include the name of your organization or Internet protocol (http or https). You will be prompted for that later.</p>
</td>
</tr>
<tr>
<td>
<p>Is this server configured for Secure Sockets Layer (https) (y/n) [n]</p>
</td>
<td>
<p>Type <strong>y</strong> if the URL you use to access Dynamics 365 begins with https://, otherwise type
<strong>n</strong>. Microsoft Dynamics 365 (online) users do not see this prompt.</p>
</td>
</tr>
<tr>
<td>
<p>Is this organization provisioned in Microsoft Online Services (y/n) [n]</p>
</td>
<td>
<p>Type <strong>y</strong> if this is a Microsoft Online Services provisioned organization. Otherwise, type
<strong>n</strong>.</p>
<p>Only Microsoft Dynamics 365 (online) users see this prompt.</p>
</td>
</tr>
<tr>
<td>
<p>Enter domain\username</p>
</td>
<td>
<p>For Microsoft Dynamics 365, type your network domain and user name separated by a backslash (\). For Microsoft Dynamics 365 (online), enter your Microsoft account.</p>
<p>On-premises/IFD Example: mydomain\tadams</p>
<p>Online Example: terryadams@somedomain.onmicrosoft.com</p>
<p>If you just press &lt;Enter&gt; for the user name, the program will look for and use saved credentials. If there are no saved credentials, the program will fail.</p>
</td>
</tr>
<tr>
<td>
<p>Enter password</p>
</td>
<td>
<p>Type your password. The characters will show as &ldquo;*&rdquo; in the window. Your password is securely saved in the Microsoft Credential Manager for later reuse.</p>
</td>
</tr>
<tr>
<td>
<p>Specify an organization number (1-n) [1]</p>
</td>
<td>
<p>From the list of organizations shown that you belong to, type the corresponding number. The default is 1, indicating the first organization in the list.</p>
</td>
</tr>
<tr>
<td>
<p>Do you want these entity records deleted? (y/n) [y]</p>
</td>
<td>
<p>Type <strong>y</strong> for yes and <strong>n</strong> for no. After the program runs, demonstrating create, retrieve, and update of an account record, you can delete the record that was created, or you can leave it so that you can view it in the Web application
 or Microsoft Dynamics 365 for Outlook.</p>
</td>
</tr>
<tr>
<td>
<p>Press Enter to exit</p>
</td>
<td>
<p>Press Enter to exit the program.</p>
</td>
</tr>
</tbody>
</table>
</div>
</li></ol>
</div>
<div class="section">
<h2 class="heading">Next Steps</h2>
<div class="section">
<p>After running the QuickStart program or most other SDK console-based samples, the server, organization, and user name information you entered when prompted is saved in an XML configuration file for re-use the next time you run a sample. This removes the
 need to enter that information again as you run additional samples. After the first time, the console prompts are as follows.</p>
<table>
<tbody>
<tr>
<th>
<p>Prompt</p>
</th>
<th>
<p>Description</p>
</th>
</tr>
<tr>
<td>
<p>Specify the saved server configuration number (1-x) [x]:</p>
</td>
<td>
<p>Enter zero (0) to create a new server configuration and follow the prompts as shown in the table above. Otherwise, enter the number of a saved configuration as shown in the displayed list.</p>
</td>
</tr>
<tr>
<td>
<p>Enter Password:</p>
</td>
<td>
<p>Type your password. The characters will show as &ldquo;*&rdquo; in the window. If your password has been saved from a previous sample run, you will not see this prompt.</p>
</td>
</tr>
<tr>
<td>
<p>Do you want these entity records deleted? (y/n) [y]</p>
</td>
<td>
<p>Type <strong>y</strong> for yes and <strong>n</strong> for no. After the program runs, demonstrating create, retrieve and update of an account record, you can delete the record that was created, or you can leave it so that you can view it in the Web application
 or Microsoft Dynamics 365 for Outlook.</p>
</td>
</tr>
<tr>
<td>
<p>Press Enter to exit</p>
</td>
<td>
<p>Press Enter to exit the program.</p>
</td>
</tr>
</tbody>
</table>
<p>&nbsp;</p>
<p>If you would like to add or modify the functionality of this QuickStart program, you can modify the code in the CRUDOperations.cs file.</p>
<p>For more information about the helper code files that the QuickStart and many other SDK samples use, or to learn about how to set up a new project with the required assembly references, refer to
<a href="https://docs.microsoft.com/en-us/dynamics365/customer-engagement/developer/org-service/use-sample-helper-code" target="_blank">
Use the sample and helper code</a>. For more information about the source code that handles saving and re-using the server configuration information, see
<a href="https://docs.microsoft.com/en-us/dynamics365/customer-engagement/developer/org-service/helper-code-serverconnection-class">
Helper code: ServerConnection class</a>.</p>
<p>A second sample project, named QuickStart with Simplified Connection, is included in the Microsoft Visual Studio solution. This second sample shows a simplified method to connect to the Microsoft Dynamics 365 Organization web service but is otherwise similar
 to the first QuickStart sample. This sample does not use any helper code to establish a web service connection.</p>
</div>
</div>
<h2>Sample: Quick start for Microsoft Dynamics 365</h2>
<p>This sample authenticates the user with the Microsoft Dynamics 365 web services by using the ServerConnection helper class and methods. After obtaining a reference to the Organization web service, the sample performs basic create, update, retrieve, and delete
 operations on an account entity. The sample also handles common exceptions that can be thrown.</p>
<p>Click to see the <a href="https://code.msdn.microsoft.com/Sample-Quick-start-for-650dbcaa/sourcecode?fileId=179950&pathId=1409068811">
CRUDOperations.cs</a> sample file.</p>
<p>More information:&nbsp;<a href="https://docs.microsoft.com/en-us/dynamics365/customer-engagement/developer/sample-quick-start" target="_blank">Sample: Quick start for Microsoft Dynamics 365</a>&nbsp;and
<a href="https://docs.microsoft.com/en-us/dynamics365/customer-engagement/developer/simple-program-web-services" target="_blank">
Run a simple program using Customer Engagement web services</a>.</p>
<h2>Sample: Simplified connection quick start using Microsoft Dynamics 365</h2>
<p>This sample authenticates the user with the Microsoft Dynamics 365 web services by using the CrmServiceClient and methods. After obtaining a reference to the Organization web service, the sample performs basic create, update, retrieve, and delete operations
 on an account entity. The sample also handles common exceptions. No helper code is used to establish a connection to the Organization web service.</p>
<p>In addition, this sample supports OAuth authentication and advanced connection diagnostics. For more information on using diagnostics, see
<a href="https://docs.microsoft.com/en-us/dynamics365/customer-engagement/developer/xrm-tooling/configure-tracing-xrm-tooling" target="_blank">
Configure tracing for XRM tooling</a>.</p>
<p>Click to see the <a href="https://code.msdn.microsoft.com/Sample-Quick-start-for-650dbcaa/sourcecode?fileId=179950&pathId=509155504">
SimplifiedConnection.cs</a> sample file.</p>
<p>More information:&nbsp;<a href="https://docs.microsoft.com/en-us/dynamics365/customer-engagement/developer/xrm-tooling/sample-simplified-connection-quick-start" target="_blank">Sample: Simplified connection quick start using Microsoft Dynamics 365</a>.</p>
</div>
</div>
</div>
</div>
</div>
