# MVC Sample App for Windows Azure Active Directory Graph
## Requires
- Visual Studio 2012
## License
- MS-LPL
## Technologies
- JSON
- REST
- Microsoft Azure
- WCF Data Services
- OData
- ASP.NET MVC 4
- Graph API
- windows azure active directory
## Topics
- REST
- OData
- Graph API
- windows azure active directory
- Active Directory Authentication Library
- ADAL
## Updated
- 05/13/2014
## Description

<h1>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;ActionResult&nbsp;Index(<span class="cs__keyword">string</span>&nbsp;displayName,&nbsp;<span class="cs__keyword">string</span>&nbsp;nextLinkUri)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;QueryOperationResponse&lt;User&gt;&nbsp;response;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;users&nbsp;=&nbsp;DirectoryService.users;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;If&nbsp;a&nbsp;filter&nbsp;query&nbsp;for&nbsp;displayName&nbsp;&nbsp;is&nbsp;submitted,&nbsp;we&nbsp;throw&nbsp;away&nbsp;previous&nbsp;results&nbsp;we&nbsp;were&nbsp;paging.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(displayName&nbsp;!=&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ViewBag.CurrentFilter&nbsp;=&nbsp;displayName;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Linq&nbsp;query&nbsp;for&nbsp;filter&nbsp;for&nbsp;DisplayName&nbsp;property.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;users&nbsp;=&nbsp;(DataServiceQuery&lt;User&gt;)(users.Where(user&nbsp;=&gt;&nbsp;user.displayName.StartsWith(displayName)));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;response&nbsp;=&nbsp;users.Execute()&nbsp;<span class="cs__keyword">as</span>&nbsp;QueryOperationResponse&lt;User&gt;;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Handle&nbsp;the&nbsp;case&nbsp;for&nbsp;first&nbsp;request&nbsp;vs&nbsp;paged&nbsp;request.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(nextLinkUri&nbsp;==&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;response&nbsp;=&nbsp;users.Execute()&nbsp;<span class="cs__keyword">as</span>&nbsp;QueryOperationResponse&lt;User&gt;;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;response&nbsp;=&nbsp;DirectoryService.Execute&lt;User&gt;(<span class="cs__keyword">new</span>&nbsp;Uri(nextLinkUri))&nbsp;<span class="cs__keyword">as</span>&nbsp;QueryOperationResponse&lt;User&gt;;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;List&lt;User&gt;&nbsp;userList&nbsp;=&nbsp;response.ToList();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Handle&nbsp;the&nbsp;SkipToken&nbsp;if&nbsp;present&nbsp;in&nbsp;the&nbsp;response.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(response.GetContinuation()&nbsp;!=&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ViewBag.ContinuationToken&nbsp;=&nbsp;response.GetContinuation().NextLinkUri;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;View(userList);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<div class="endscriptcode"><strong style="font-size:medium"><span style="background-color:#ffffff; color:#ff0000">NOTE: this sample is outdated. The technology, methods and interface instructions have been replaced with newer features. To see an updated app
 that builds a similar app, please see</span></strong></div>
</h1>
<p><span style="font-size:large"><strong><span style="background-color:#ffffff; color:#ff0000"><span style="font-size:small"><a href="http://github.com/AzureADSamples/WebApp-GraphAPI-DotNet">http://github.com/AzureADSamples/WebApp-GraphAPI-DotNet</a></span><br>
</span></strong></span></p>
<p>&nbsp;</p>
<p><span style="font-size:x-small">This C# sample application demonstrates how to Read and Write tenant data&nbsp;to Windows Azure Active Directory using the AAD Graph API, which is a new RESTful interface that allows programmatic access to tenant data such
 as users, contacts, groups, roles etc.&nbsp; The Graph API is&nbsp;now GA.&nbsp;Graph service enables developers to incorporate new capabilities with existing applications and to build new applications that leverage directory data access to Windows Azure AD.</span></p>
<p><span style="font-size:x-small">The <a href="http://blogs.msdn.com/b/aadgraphteam/archive/2013/01/24/walk-through-for-building-a-net-application-for-accessing-windows-azure-active-directory-graph-service.aspx">
walk through for building a .Net application</a> on the AAD Graph team blog provides a deep dive into the steps&nbsp;involved in&nbsp;building any .Net application for accessing Graph service. The blog post covers most of the Graph Service related code present
 in this sample.&nbsp;If you are actively using AAD Graph Service or have tried it, we would love to hear feedback from you. The
<a href="http://blogs.msdn.com/b/aadgraphteam/">team blog</a> is a great place to ask any Graph service related questions and provide us feedback.</span></p>
<p><span style="font-size:x-small">The Graph API is compatible with OData V3 and enables applications to construct more complex queries. The sample application authenticates to Graph API by presenting a token that is issued by Windows Azure Access Control Service
 using OAuth 2.0.</span></p>
<p><span style="font-size:x-small"><span style="font-size:x-small">The application is configured and authorized to Read data from a demo company hosted in Windows Azure AD. If you wish to access your own Windows Azure AD tenant or wish to try the Write operations
 (Create, Delete, Update), then you must use your own tenant. You can manage your existing Azure AD tenant or create a new tenant using the Windows Azure Management portal. You can find more information on the topic&nbsp;for managing your directory from Azure
 Management Portal <a href="http://msdn.microsoft.com/library/windowsazure/dn151790.aspx#BKMK_Working">
here</a>. Once you have the tenant, you need to configure the application&nbsp;for for accessing Graph API. The
<a href="http://msdn.microsoft.com/library/windowsazure/dn151791.aspx#BKMK_Configuring">
following topic</a> shows how to enable authentication for accessing Graph API for your application. If you followed the steps in the topic, you will have the Client ID and Key values. Copy these values&nbsp;along with your Tenant Domain Name into the Web.Config
 of the sample application. Client ID maps to AppPrincipalId, Key value to Password in Web.Config. After you have set up these values in web.config, the application should run against your tenant.</span></span></p>
<p><span style="font-size:2em">Building the Sample</span></p>
<p><span style="font-size:x-small">This sample application can be built using Visual Studio 2012. &nbsp;</span><strong style="font-size:x-small">Visual Studio 2012 Professional or Visual Studio 2012 Ultimate</strong><span style="font-size:x-small">: You can
 download a free trial here: </span><a href="http://www.microsoft.com/visualstudio/eng/downloads" target="_blank" style="font-size:x-small">Visual Studio Free Trial</a></p>
<p><a href="http://visualstudiogallery.msdn.microsoft.com/27077b70-9dad-4c64-adcf-c7cf6bc9970c" target="_blank"><span style="font-size:x-small">Install Nuget Package Manager</span></a></p>
<p><a href="https://www.nuget.org/packages/Microsoft.Data.Services.Client/5.6.0" target="_blank"><span style="font-size:x-small">Install&nbsp;</span><span style="font-size:x-small">WCF Data Services Client version 5.6.0</span></a></p>
<p><span style="font-size:x-small"><a href="https://www.nuget.org/packages/Microsoft.IdentityModel.Clients.ActiveDirectory/" target="_blank">Install Active Directory Authentication Library (ADAL) version 1.0.2</a></span></p>
<p><span style="font-size:x-small">You will also need to install ASP.NET MVC 4, which is available from&nbsp;</span><a href="http://www.microsoft.com/en-us/download/details.aspx?id=30683" style="font-size:x-small">http://www.microsoft.com/en-us/download/details.aspx?id=30683</a><span style="font-size:x-small">&nbsp;</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><span style="font-size:x-small"><span style="color:black; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;">This C# sample application is an ASP.NET MVC 4 app, which will access the Windows Azure Active Directory Graph API, and query a demo tenant's user information</span><span style="color:black; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;">.
 The full list of capabilities for the Graph API can be found on MSDN (link below).</span></span></p>
<p><span style="font-size:20px; font-weight:bold">&nbsp;</span><span style="font-size:x-small"><strong>What you can do with the sample application?</strong></span></p>
<ul>
<li><span style="font-size:x-small">UPDATED (Dec 2013): calls Graph api-version=2013-11-08, includes ADAL for JWT token handling, uses WCF Data Services Client 5.6.<br>
</span></li><li><span style="font-size:x-small">UPDATED (April 2013): Created a small helper library that contains the Service Reference to the Graph service and some helper methods.</span>
</li><li><span style="font-size:x-small">Edit Group Membership and reset users' passwords.</span>
</li><li><span style="font-size:x-small">Read user and group object information</span>
</li><li><span style="font-size:x-small">Search for users and groups by display name (using &quot;starts with&quot; search)</span>
</li><li><span style="font-size:x-small">Create, Edit and Delete Users - the minimum properties required&nbsp;to create a user are: UserPrincipalName, DisplayName and AccountEnabled=true</span>
</li><li><span style="font-size:x-small">Create, Edit and Delete Security Groups - the minimum properites required to create a group are: Description and DisplayName.</span>
</li><li><span style="font-size:x-small">View and Update Users' thumbnail photo properties</span>
</li></ul>
<h1>More Information and Resources</h1>
<ul>
<li><span style="font-size:x-small">Windows Azure Walkthrough - follow this walk-through document that explains how to configure your application credentials for this application:
<a href="https://msdn.microsoft.com/en-us/library/dn151121.aspx">https://msdn.microsoft.com/en-us/library/dn151121.aspx</a></span>
</li><li><span style="font-size:x-small">Microsoft Windows Azure Active Directory Graph API and Windows Azure Active Directory:
<a href="http://msdn.microsoft.com/en-us/library/windowsazure/jj673460.aspx">http://msdn.microsoft.com/en-us/library/windowsazure/jj673460.aspx</a></span>
</li></ul>
<ul>
<li><span style="font-size:x-small">Release Notes <a href="http://msdn.microsoft.com/en-us/library/dn168859.aspx">
http://msdn.microsoft.com/en-us/library/dn168859.aspx</a> </span></li><li><span style="font-size:x-small">Graph Explorer:&nbsp; a simple User interface that allows you to query and review results from the Demo company, or your own company:
<a href="https://GraphExplorer.CloudApp.net " target="_blank">https://GraphExplorer.cloudApp.net</a></span>
</li><li><span style="font-size:x-small">Graph API Differential Queries &ndash;get directory
</span><span style="font-size:x-small">changes. </span>
<ul>
<li><span style="font-size:x-small">Documentation: <a href="http://msdn.microsoft.com/en-us/library/jj836245.aspx" target="_blank">
http://msdn.microsoft.com/en-us/library/jj836245.aspx</a></span> </li><li><span style="font-size:x-small">Sample Application: <a href="http://code.msdn.microsoft.com/windowsazure/Sample-App-for-Windows-97eaec90" target="_blank">
http://code.msdn.microsoft.com/windowsazure/Sample-App-for-Windows-97eaec90</a></span>
</li></ul>
</li></ul>
<ul>
<li>
<div><span style="font-size:x-small">Graph API Metadata endpoint: <a href="https://graph.windows.net/Contoso.com/$metadata?api-version=1.2" target="_blank">
https://Graph.Windows.net/Contoso.com/$metadata?api-version=2013-11-08</a></span></div>
</li><li>
<div><span style="font-size:x-small">View Presentations from TechEd June 2012 on the Graph API and Windows Azure Active Directory overview.</span></div>
<ul>
<li>
<div><span style="font-size:x-small">Windows Azure AD Graph API Drill down <a href="http://channel9.msdn.com/events/teched/northamerica/2012/sia322" target="_blank">
http://channel9.msdn.com/events/teched/northamerica/2012/sia322</a></span></div>
</li><li>
<div><span style="font-size:x-small">A Lap around Windows Azure Active Directory <a href="http://channel9.msdn.com/events/teched/northamerica/2012/sia209" target="_blank">
http://channel9.msdn.com/events/teched/northamerica/2012/sia209</a></span></div>
</li></ul>
</li></ul>
