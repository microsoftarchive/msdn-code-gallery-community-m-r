# PHP Sample App For Windows Azure Active Directory Graph
## Requires
- 
## License
- MS-LPL
## Technologies
- JSON
- REST
- Microsoft Azure
- OData
- PHP
- Graph API
- windows azure active directory
## Topics
- REST
- windows azure active directory
- Windows Azure Rest API
## Updated
- 04/04/2014
## Description

<h1>Introduction</h1>
<p>&nbsp;</p>
<p><span style="font-size:small">Note:&nbsp;this app is now being updated at <a href="http://GitHub.com/AzureADSamples/WebApp-GraphAPI-PHP">
http://GitHub.com/AzureADSamples/WebApp-GraphAPI-PHP</a>.</span></p>
<p><span style="font-size:small">Updates: App was updated on Dec 16, 2013, to use the latest api version: api-version=2013-11-08</span></p>
<p><span style="font-size:small">This PHP sample application demonstrates how to Read and Write tenant data to Windows Azure Active Directory using the AAD Graph API, which is a new RESTful interface that allows programmatic access to tenant data such as users,
 contacts, groups, roles etc.&nbsp; The Graph API is now GA. Graph service enables developers to incorporate new capabilities with existing applications and to build new applications that leverage directory data access to Windows Azure AD.</span></p>
<p><span style="font-size:small">You can visit the <a href="http://blogs.msdn.com/b/aadgraphteam/" target="_blank">
AAD Graph team blog</a> on MSDN to learn more about the Windows Azure Active Directory Graph Service.&nbsp;If you are actively using AAD Graph Service or have tried it, we would love to hear feedback from you. The team blog is a great place to ask any Graph
 service related questions and provide us feedback.</span></p>
<p><span style="font-size:small">The PHP sample produces HTTP requests&nbsp;and reads the JSON responses back&nbsp;from the Graph service based on the user action.&nbsp;You can read about the exact format of the&nbsp;HTTP requests and JSON responses from the
 Graph service <a href="http://blogs.msdn.com/b/aadgraphteam/archive/2013/01/15/understanding-azure-active-directory-graph-operations.aspx">
here</a>. The GraphServiceAccessHelper.php file contains the code for producing all the requests and reading responses from the Graph service.</span></p>
<p><span style="font-size:small">The Graph API is compatible with OData V3 and enables applications to construct more complex queries. The sample application authenticates to Graph API by presenting a token that is issued by Windows Azure Access Control Service
 using OAuth 2.0. The sample application is authorized to read user information from a demonstration Azure AD company. To execute Write Operations for your company's data (Create, Delete, Update), then you must use your own Azure AD company (sign up for a Windows
 Azure Company here ) and configure authorization for the app to have both read and write permissions (a PowerShell script is provided with the sample app that will help you configure this authorization).&nbsp;</span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">You can use <a href="http://www.microsoft.com/web/webmatrix/">
WebMatrix</a> to work with this PHP sample. WebMatrix is a free and light weight tool that will let you</span><br>
<span style="font-size:small">create web sites using various technologies like PHP, Asp.Net, Node.js etc. In order to run this sample in Web matrix, choose&nbsp; &quot;Open Site&quot; -&gt; &quot;Folder as Site&quot; and select the folder that contains all the downloaded files.
</span></p>
<p><span style="font-size:small">The application is configured and authorized to Read data from a demo company hosted in Windows Azure AD. If you wish to access your own Windows Azure AD tenant or wish to try the Write operations (Create, Delete, Update), then
 you must use your own tenant. You can manage your existing Azure AD tenant or create a new tenant using the Windows Azure Management portal. You can find more information on the topic for managing your directory from Azure Management Portal
<a href="http://msdn.microsoft.com/library/windowsazure/dn151790.aspx#BKMK_Working">
here</a>. Once you have the tenant, you need to configure the application for for accessing Graph API. The
<a href="http://msdn.microsoft.com/library/windowsazure/dn151791.aspx#BKMK_Configuring">
following topic</a> shows how to enable authentication for accessing Graph API for your application. If you followed the steps in the topic, you will have the Client ID and Key values. Copy these values along with your Tenant Domain Name into the Web.Config
 of the sample application. Client ID maps to AppPrincipalId, Key value to Password in Web.Config. After you have set up these values in web.config, the application should run against your tenant.</span></p>
<p>&nbsp;</p>
<p><span style="font-size:20px; font-weight:bold"><span>Description</span></span></p>
<p><span style="font-size:small">This PHP sample application accesses the Windows Azure Active Directory Graph API, and query a demo tenant's user information. It also demonstrates a way to synchronize changes from AAD Graph, getting only updated changes from
 the last request.</span></p>
<div><span style="font-size:small"><strong>What you can do with the sample application</strong></span></div>
<ul>
<li><span style="font-size:small">Read user and group object information </span></li><li><span style="font-size:small">Create, Edit and Delete Users and Security&nbsp;Groups
</span></li><li><span style="font-size:small">See the members of a group and add new users.&nbsp;You can look at the Members of a group by clicking the DisplayName of the Group in the Group Administration page.</span>
</li><li><span style="font-size:small">Synchronize changes from AAD Graph and query for updates.
</span></li></ul>
<div><span style="font-size:x-small"><strong>&nbsp;</strong></span></div>
<h1>More Information and Resources</h1>
<ul>
<li><span style="font-size:small">Microsoft Windows Azure Active Directory Graph API full description:<a href="http://msdn.microsoft.com/en-us/library/hh974476.aspx" target="_blank">http://msdn.microsoft.com/en-us/library/hh974476.aspx</a>
</span></li></ul>
<ul>
<li><span style="font-size:small">Windows Azure Active Directory Graph Team blog:
<a href="http://blogs.msdn.com/b/aadgraphteam/">http://blogs.msdn.com/b/aadgraphteam/</a>
</span></li><li><span style="font-size:small">Release Notes <a href="http://msdn.microsoft.com/en-us/library/jj149807" target="_blank">
http://msdn.microsoft.com/en-us/library/jj149807</a> </span></li><li><span style="font-size:small">Graph Explorer:&nbsp; a simple User interface that allows you to query and review results from the Demo company, or your own company:
<a href="https://GraphExplorer.CloudApp.net " target="_blank">https://GraphExplorer.CloudApp.net</a>
</span></li><li><span style="font-size:small">Graph API Differential Queries &ndash;get directory changes.
</span>
<ul>
<li><span style="font-size:small">Documentation: <a href="http://msdn.microsoft.com/en-us/library/jj836245.aspx" target="_blank">
http://msdn.microsoft.com/en-us/library/jj836245.aspx</a> </span></li><li><span style="font-size:small">Sample Application: <a href="http://code.msdn.microsoft.com/windowsazure/Sample-App-for-Windows-97eaec90" target="_blank">
http://code.msdn.microsoft.com/windowsazure/Sample-App-for-Windows-97eaec90</a> </span>
</li></ul>
</li></ul>
<ul>
<li>
<div><span style="font-size:small">Graph API Metadata endpoint: <a href="https://Directory.Windows.net/$metadata" target="_blank">
https://Graph.Windows.net/Contoso.com/$metadata</a></span></div>
</li><li>
<div><span style="font-size:small">View Presentations from TechEd June 2012 on the Graph API and Windows Azure Active Directory overview.</span></div>
<ul>
<li>
<div><span style="font-size:small">Windows Azure AD Graph API Drill down <a href="http://channel9.msdn.com/events/teched/northamerica/2012/sia322" target="_blank">
http://channel9.msdn.com/events/teched/northamerica/2012/sia322</a></span></div>
</li><li>
<div><span style="font-size:small">A Lap around Windows Azure Active Directory <a href="http://channel9.msdn.com/events/teched/northamerica/2012/sia209" target="_blank">
http://channel9.msdn.com/events/teched/northamerica/2012/sia209</a></span></div>
</li></ul>
</li></ul>
