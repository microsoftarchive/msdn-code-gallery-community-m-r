# Mobile Development Helper Code for Dynamics CRM
## Requires
- Visual Studio 2013
## License
- MS-LPL
## Technologies
- Web Services
- OData
- Dynamics CRM
- SOAP
## Topics
- OData
- Microsoft Dynamics CRM Modern app SOAP endpoint
- Dynamics CRM web services
## Updated
- 05/20/2015
## Description

<h1>
<div class="endscriptcode"></div>
<div class="endscriptcode">Introduction</div>
</h1>
<p>The Microsoft.Crm.Sdk.Mobile source code sample is a partial re-implementation of the Microsoft Dynamics CRM SDK classes written as a Portable Class Library to facilitate development of store apps for Windows 8.1 desktop, tablets, and phones. The code makes
 use of the SOAP and OData protocols to issue web service method calls. An organization web service proxy and most of the message response/request classes in the CRM SDK have been implemented. When writing apps that use this code, you don&rsquo;t have to link
 to the CRM SDK assemblies to access the organization web service.</p>
<p>This code can also be used when you develop apps using <a href="http://xamarin.com/">
Xamarin</a>. However, you must comment out the <strong>EnableProxyTypes</strong> method in Microsoft.Xrm.Sdk.Samples.cs when developing iOS or Android apps using Xamarin because that method contains code that is specific to Windows Store. An alternative is
 to comment out the relevant code as shown here.</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">public</span>&nbsp;async&nbsp;Task&nbsp;EnableProxyTypes()&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;List&lt;TypeInfo&gt;&nbsp;typeList&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;List&lt;TypeInfo&gt;();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">////&nbsp;Obtain&nbsp;folder&nbsp;of&nbsp;executing&nbsp;application.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//var&nbsp;folder&nbsp;=&nbsp;Package.Current.InstalledLocation;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//foreach&nbsp;(var&nbsp;file&nbsp;in&nbsp;await&nbsp;folder.GetFilesAsync())</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;&nbsp;&nbsp;//&nbsp;Not&nbsp;only&nbsp;.dll&nbsp;but&nbsp;.exe&nbsp;also&nbsp;contains&nbsp;types.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;&nbsp;&nbsp;if&nbsp;(file.FileType&nbsp;==&nbsp;&quot;.dll&quot;&nbsp;||&nbsp;file.FileType&nbsp;==&nbsp;&quot;.exe&quot;)</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;&nbsp;&nbsp;{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;assemblyName&nbsp;=&nbsp;new&nbsp;AssemblyName(file.DisplayName);</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;assembly&nbsp;=&nbsp;Assembly.Load(assemblyName);</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;foreach&nbsp;(TypeInfo&nbsp;type&nbsp;in&nbsp;assembly.DefinedTypes)</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;//&nbsp;Store&nbsp;only&nbsp;CRM&nbsp;entities.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;if&nbsp;(type.BaseType&nbsp;==&nbsp;typeof(Entity))</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;typeList.Add(type);</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;&nbsp;&nbsp;}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;typeList.Add(<span class="cs__keyword">typeof</span>(ActivityParty).GetTypeInfo());&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;typeList.Add(<span class="cs__keyword">typeof</span>(SavedQuery).GetTypeInfo());&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;typeList.Add(<span class="cs__keyword">typeof</span>(Privilege).GetTypeInfo());&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;typeList.Add(<span class="cs__keyword">typeof</span>(SystemUser).GetTypeInfo());&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;typeList.Add(<span class="cs__keyword">typeof</span>(Annotation).GetTypeInfo());&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;typeList.Add(<span class="cs__keyword">typeof</span>(TimeZoneDefinition).GetTypeInfo());&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;types&nbsp;=&nbsp;typeList.ToArray();&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>&nbsp;</p>
<h1><span>Building the Sample</span></h1>
<p>Your development computer must have an internet connection to build the library as a
<a href="http://nuget.org/">NuGet</a> package named Json.NET will be automatically downloaded during the build.</p>
<p>To build the Microsoft.Crm.Sdk.Mobile.dll assembly, follow these steps.</p>
<ol>
<li>In Visual Studio 2013, with update 2 or later installed, load the Microsoft.Crm.Sdk.Mobile.sln file.
</li><li>Press F6. </li></ol>
<p>&nbsp;</p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p>The Microsoft Dynamics CRM 2013 SDK (and earlier releases) support client and server side code development for Dynamics CRM. However, the SDK assemblies cannot be used to develop mobile and store applications as the Windows run-time (WinRT) is not supported.
 To provide support for mobile apps, including cross platform iOS and Android apps, the Dynamics CRM server now supports client access to the organization web service using industry standard protocols such as OAuth (for authentication), plus SOAP and OData
 for sending web service requests. The problem is that while these protocols are supported, they are difficult to use. The solution is to layer a framework on top of the protocols to make app programming easier. For developing mobile and store apps, you can
 use the Microsoft Azure Active Directory Library (ADAL) for authentication, and this sample helper code library for web service requests and early-bound type support.<em>
<br>
</em></p>
<p>&nbsp;</p>
<h1>What is included in the library</h1>
<p>This library contains the following key components:</p>
<p>&nbsp;</p>
<p>&nbsp; <strong>Web Service Proxy</strong></p>
<p>This library provides most methods that the <a href="http://msdn.microsoft.com/en-us/library/microsoft.xrm.sdk.client.organizationserviceproxy.aspx">
OrganizationServiceProxy</a> class in the CRM SDK does - Create, Update, Assign, Execute, and so on. The class supports early-bound development by providing an
<strong>EnableProxyTypes</strong> method. SOAP implementations of these methods use the same name as are used in the CRM SDK while the OData-based methods have names that begin with &ldquo;Rest&rdquo; for example
<strong>RestCreate</strong>.</p>
<p>&nbsp;</p>
<p>&nbsp; <strong>SDK classes and enumerations</strong></p>
<p>This library supports most common CRM SDK classes and enumerations. With the addition of early-bound type support, you can use Visual Studio IntelliSense when writing your apps.</p>
<p>&nbsp;</p>
<p>&nbsp; <strong>Organization web service messages</strong></p>
<p>This library includes request/response classes for over 200 messages. It covers both data operations, for example
<strong>Create</strong>, and metadata operations, for example <strong>RetrieveMetadataChanges</strong>.</p>
<p>&nbsp;</p>
<h1>What isn't included in the library</h1>
<p>Several functional areas that the library doesn&rsquo;t cover are as follows:</p>
<p><strong><span style="text-decoration:underline">&nbsp;</span></strong></p>
<p><strong>Authentication</strong></p>
<p>Since this sample code library targets multiple platforms, and each platform has its unique way to authenticate the user, the library doesn&rsquo;t provide any authentication mechanism. You must obtain an OAuth access token and pass it to an
<strong>OrganizationDataWebServiceProxy</strong> object to access the organization web service. It&rsquo;s recommended that you use the
<a href="http://msdn.microsoft.com/en-us/library/jj573266.aspx">Microsoft Azure Active Directory Authentication Library</a> (ADAL) for identity authentication. This library is referenced in the CRM SDK documentation and sample code. There is an open source
 implementation of ADAL available for <a href="https://github.com/AzureAD/azure-activedirectory-library-for-objc">
iOS</a> and <a href="https://github.com/AzureAD/azure-activedirectory-library-for-android">
Android</a>. There is a Windows 8.1 version available for the desktop, tablets, and phones.</p>
<p>For more information on ADAL see <a href="http://www.cloudidentity.com/blog/2014/06/16/adal-for-windows-phone-8-1-deep-dive">
http://www.cloudidentity.com/blog/2014/06/16/adal-for-windows-phone-8-1-deep-dive</a>.</p>
<p>A sample code file named CRMHelpers.cs is provided to demonstrate how to authenticate a universal app. The file is not included in the build of the library.</p>
<p>&nbsp;</p>
<p><strong>DiscoveryService, DeploymentService, OrganizationServiceContext</strong></p>
<p>This library doesn&rsquo;t provide support for the discovery or deployment web services. It also doesn&rsquo;t support the
<a href="http://msdn.microsoft.com/en-us/library/microsoft.xrm.sdk.client.organizationservicecontext.aspx">
OrganizationServiceContext</a> class.</p>
<p>&nbsp;</p>
<h1>How to write apps that use this library</h1>
<p class="Text">You can build Microsoft.Crm.Sdk.Mobile.dll using the supplied Visual Studio 2013 solution and add a reference for it in your app&rsquo;s project or you can add the library&rsquo;s C# sample code files to your project.</p>
<p class="Text">In your app, add code to authenticate the user and obtain a security access token. The method used varies depending on platform as mentioned previously. Next, instantiate the
<strong>OrganizationDataWebServiceProxy</strong> class.</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="js">OrganizationDataWebServiceProxy&nbsp;_proxy&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;OrganizationDataWebServiceProxy();</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>Set the access token and web server URL in the <strong>OrganizationDataWebServiceProxy
</strong>object.</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp">_proxy.ServiceUrl&nbsp;=&nbsp;<span class="cs__string">&quot;&lt;your&nbsp;CRM&nbsp;server&nbsp;URL&nbsp;goes&nbsp;here&gt;&quot;</span>;&nbsp;
_proxy.AccessToken&nbsp;=&nbsp;<span class="cs__string">&quot;&lt;access&nbsp;token&gt;&quot;</span>;</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>Call <strong>EnableProxyTypes</strong> to use early-bound types and optionally assign a service time-out interval.</p>
<pre class="Text"><div class="scriptcode"><div class="pluginEditHolder" pluginCommand="mceScriptCode"><div class="title"><span>C#</span></div><div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div><span class="hidden">csharp</span>
<div class="preview">
<pre class="js">await&nbsp;_proxy.EnableProxyTypes();&nbsp;
&nbsp;
_proxy.Timeout&nbsp;=&nbsp;<span class="js__num">600</span>;</pre>
</div>
</div>
</div>
</pre>
<p class="Text">Send a message request to the web service by using an async/await pattern.</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="js">WhoAmIResponse&nbsp;whoAmIResponse&nbsp;=&nbsp;(WhoAmIResponse)await&nbsp;_proxy.Execute(<span class="js__operator">new</span>&nbsp;WhoAmIRequest());&nbsp;
&nbsp;
<span class="js__sl_comment">//&nbsp;Retrieve&nbsp;User&nbsp;Info</span>&nbsp;
&nbsp;
SystemUser&nbsp;user&nbsp;=&nbsp;(SystemUser)await&nbsp;_proxy.Retrieve(SystemUser.EntityLogicalName,&nbsp;whoAmIResponse.UserId,&nbsp;<span class="js__operator">new</span>&nbsp;ColumnSet(true));</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>&nbsp;</p>
<h1 class="Text">How to update existing application code</h1>
<p class="Text">If you want to use existing application code that was written for the CRM SDK with this library, make the following code changes.</p>
<p class="Text">1. Add the <strong>async</strong> keyword to methods.</p>
<p class="Text">2. Add <strong>await</strong> before the call to a proxy method.</p>
<p class="Text">3. When accessing the property of a web service response, do so as shown here.</p>
<p class="Text">&nbsp;</p>
<p class="Text"><em>Original CRM SDK code</em></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="js">_proxy.RetrieveMultiple(<span class="js__operator">new</span>&nbsp;QueryExpression(&ldquo;account&rdquo;,&nbsp;<span class="js__operator">new</span>&nbsp;Columns(true)).Entities)</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p class="Text"><em>Modified code</em></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="js">(await&nbsp;_proxy.RetrieveMultiple(<span class="js__operator">new</span>&nbsp;QueryExpression(&ldquo;account&rdquo;,&nbsp;<span class="js__operator">new</span>&nbsp;Columns(true))).Entities)</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>&nbsp;</p>
<h1>Important notes</h1>
<ul>
<li>The sample files are not intended to be used in a production environment. You should deploy this sample to a test environment and examine it for interaction or interference with other parts of the system.
</li><li>Before you deploy this sample to a production environment, make sure that you consider the existing customizations you may have implemented in Microsoft Dynamics CRM 2013.
</li><li>This library was written by Kenichiro Nakamura at Microsoft. </li></ul>
<p>Source code for the CrmSvcMobileUtil program is provided in a separate <a href="https://code.msdn.microsoft.com/CRM-Service-Utility-for-4ca0c93b">
sample</a>. Use that program to generate any custom or customized entity types in your organization for inclusion in your application.</p>
<p>&nbsp;</p>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em>Microsoft.Xrm.Sdk.Utility.Samples.cs - This file implementes a utility class that provides methods used by the other code files. There is no equivalent for this functionality in the CRM 2013 SDK.<br>
</em></li></ul>
<h1>More Information</h1>
<p><em>For more information on Dynamics CRM authentication and app development, see
<a title="Write mobile and modern apps" href="http://msdn.microsoft.com/en-us/library/dn481568.aspx" target="_blank">
Write mobile and modern apps</a>.<br>
</em></p>
<h1>Change History</h1>
<p>1.0: Initial version</p>
<p>1.1: Added support for the new CRM Online 2015 Update 1 messages and supporting classes, except for ExecuteTransaction and ExecuteMultiple, plus bug fixes. See the Readme document for more details.</p>
