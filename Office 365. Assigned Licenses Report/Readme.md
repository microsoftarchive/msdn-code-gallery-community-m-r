# Office 365. Assigned Licenses Report
## Requires
- Visual Studio 2017
## License
- MIT
## Technologies
- C#
- Console Application
- Office 365
- Graph API
- Microsoft Graph
## Topics
- Microsoft Graph API
## Updated
- 07/20/2018
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">Solution generates report in Excel format (.xlsx). Data for report is retrieved from Office 365 tenant via Microsoft Graph .NET Provider.</span></p>
<h1>Building the Sample</h1>
<p><span style="font-size:small">To be able to interact with Microsoft Graph we have to register the application. You can use this guide to achieve this goal:
<a href="https://developer.microsoft.com/en-us/graph/docs/concepts/auth_register_app_v2">
Register your app with the Azure AD v2.0 endpoint</a>.</span></p>
<p><span style="font-size:x-small">After app registration you have to modify app.config file and type in the following information:</span></p>
<ul>
<li><span style="font-size:small"><strong>ClientId</strong> - application identifier</span>
</li><li><span style="font-size:small"><strong>ClientSecret</strong> - key generated in Azure Portal</span>
</li><li><span style="font-size:small"><strong>DomainName</strong> - Office 365 tenant domain like this:
<strong>{tenant}</strong>.microsoftonline.com</span> </li></ul>
<p><span style="font-size:small">All these parameters must be in the appSettings section:</span></p>
<p><span style="font-size:small"><img id="205104" src="205104-office-365-assigned-licenses-report-3%5b1%5d.png" alt="" width="867" height="381"><br>
</span></p>
<h1>Description</h1>
<p><span style="font-size:small">The app uses the following nuget packages:</span></p>
<ul>
<li><span style="font-size:small">Microsoft.Graph</span> </li><li><span style="font-size:small">Microsoft.IdentityModel.Clients.ActiveDirectory</span>
</li><li><span style="font-size:small">ClosedXML</span> </li></ul>
<p><span style="font-size:small">First two packages are used to interact with Microsoft Graph. The last one is used for generating the report in Excel format.</span></p>
<p><span style="font-size:small"><em>GraphProvider.cs</em> file contains class which is used to perform Microsoft Graph requests:</span></p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">public class GraphProvider : IAuthenticationProvider
{
    public async Task AuthenticateRequestAsync(HttpRequestMessage request)
    {

        var token = await GetToken();
        request.Headers.Add(&quot;Authorization&quot;, &quot;Bearer &quot; &#43; token);
    }

    public static async Task&lt;string&gt; GetToken(string resource = @&quot;https://graph.microsoft.com/&quot;)
    {
        var clientId = ConfigurationManager.AppSettings[&quot;ida:ClientId&quot;];
        var clientKey = ConfigurationManager.AppSettings[&quot;ida:ClientSecret&quot;];
        var azureDomain = ConfigurationManager.AppSettings[&quot;ida:Domain&quot;];
        var authory = $@&quot;https://login.microsoftonline.com/{azureDomain}&quot;;
        var creds = new ClientCredential(clientId, clientKey);
        var authContext = new AuthenticationContext(authory);
        var authResult = await authContext.AcquireTokenAsync(resource, creds);
        return authResult.AccessToken;
    }
}</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;GraphProvider&nbsp;:&nbsp;IAuthenticationProvider&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;async&nbsp;Task&nbsp;AuthenticateRequestAsync(HttpRequestMessage&nbsp;request)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;token&nbsp;=&nbsp;await&nbsp;GetToken();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;request.Headers.Add(<span class="cs__string">&quot;Authorization&quot;</span>,&nbsp;<span class="cs__string">&quot;Bearer&nbsp;&quot;</span>&nbsp;&#43;&nbsp;token);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;async&nbsp;Task&lt;<span class="cs__keyword">string</span>&gt;&nbsp;GetToken(<span class="cs__keyword">string</span>&nbsp;resource&nbsp;=&nbsp;@<span class="cs__string">&quot;https://graph.microsoft.com/&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;clientId&nbsp;=&nbsp;ConfigurationManager.AppSettings[<span class="cs__string">&quot;ida:ClientId&quot;</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;clientKey&nbsp;=&nbsp;ConfigurationManager.AppSettings[<span class="cs__string">&quot;ida:ClientSecret&quot;</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;azureDomain&nbsp;=&nbsp;ConfigurationManager.AppSettings[<span class="cs__string">&quot;ida:Domain&quot;</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;authory&nbsp;=&nbsp;$@<span class="cs__string">&quot;https://login.microsoftonline.com/{azureDomain}&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;creds&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;ClientCredential(clientId,&nbsp;clientKey);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;authContext&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;AuthenticationContext(authory);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;authResult&nbsp;=&nbsp;await&nbsp;authContext.AcquireTokenAsync(resource,&nbsp;creds);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;authResult.AccessToken;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode"><span style="font-size:small">All logic represented into program.cs file. The application does the following:</span></div>
<div class="endscriptcode"><span style="font-size:small">1. Gets user information from Office 365 page by page (by default page size equals 100 and you can change this value to fit your needs)</span></div>
<div class="endscriptcode"><span style="font-size:small">2. Generates Excel report based on user information and SKUs.</span></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"><span style="font-size:small">List of SKUs you can find into my blog post.</span></div>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<h1><span style="font-size:small">More Information</span></h1>
<p><span style="font-size:small"><em><em>More information about this solution in my blog:&nbsp;<a href="http://blog.vitalyzhukov.ru/en/office-365-assigned-licenses-report">Office 365. Assigned Licenses Report</a></em></em></span></p>
