# Role-Based and ACL-Based Authorization in a Windows Azure AD Application
## Requires
- Visual Studio 2012
## License
- Apache License, Version 2.0
## Technologies
- windows azure active directory
## Topics
- Authorization
- Role Based Access Control
- ACL-Based Access Control
- Windows Azure AD Graph
## Updated
- 01/10/2015
## Description

<h1><span style="color:#ff0000">NOTE: This sample is outdated. The technology, methods, and/or user interface instructions used in this sample are still supported, but have been succeeded by newer features. &nbsp;The scenario addressed by this sample is accomplished
 using the latest technology in&nbsp;<a href="https://github.com/AzureADSamples/WebApp-RoleClaims-DotNet" target="_blank">WebApp-RoleClaims-DotNet</a>.</span></h1>
<h1><span style="font-size:large">Introduction</span></h1>
<div><span style="font-size:small">
<p>This sample application demonstrates how to use Windows Azure Active Directory (AD) for authorization. TaskTracker is a Windows Azure Active Directory ASP.NET application that uses Role-Based Access Control (RBAC) and ACLs to ensure that only authorized
 users can access secured resources. You can use one or both of these techniques in your Windows Azure AD apps, or adapt your own methods to permit selective access to resources. &nbsp;&nbsp;</p>
The app&nbsp;creates four application-specific roles and grants access to&nbsp;users and groups that have that role. When a user logs in, it immediately&nbsp;checks the user's assigned application&nbsp;roles&nbsp;customizes the UI based on permissions granted
 to the roles.&nbsp;App users with the &quot;Admin&quot; app&nbsp;role can assign roles to users and to&nbsp;groups to which the user belongs.&nbsp;<br>
</span></div>
<div><span style="font-size:small">Access to the &quot;Users&quot; page in the app is controlled by an ACL, which is represented by entries in an ACL.xml file. Only users and groups that are specified in an ACL entry can see the content of the Users page; others see
 an &quot;access denied&quot; message.</span></div>
<div><span style="font-size:small"><br>
The&nbsp;sample application uses <a href="http://msdn.microsoft.com/en-us/library/ee748475.aspx">
Windows Identity Foundation</a> (WIF) to authenticate users from Windows Azure AD.&nbsp;It extends
</span><span style="font-size:small">WIF by adding role and group claims to the <a href="http://msdn.microsoft.com/en-us/library/system.security.claims.claimsprincipal(v=vs.110).aspx">
ClaimsPrincipal</a>&nbsp;</span><span style="font-size:small">object that represents the user. For details, see the ReadMe in the ZIP file and
<strong>How the Sample App Works</strong>, below.</span></div>
<div><span style="font-size:small"><br>
The&nbsp;ZIP file that you download includes a ReadMe file with conceptual details, a code walkthrough, and detailed instructions for working with Windows Azure AD, an AD directory, and organizational users and groups. It explains in detail how to customize
 your <em>web.config</em> file to&nbsp;point to and use resources in your Windows Azure AD tenant.</span></div>
<div>&nbsp;</div>
<h1>Download Tip</h1>
<p><span style="font-size:small">When you download the code, rename the ZIP file to TaskTracker.zip or, when you extract from the ZIP file, change the destination directory name to TaskTracker. Otherwise, you might&nbsp;get errors&nbsp;because the length of
 the path&#43;filename exceeds the maximum value.</span></p>
<p>&nbsp;</p>
<h1>Requirements</h1>
<div><span style="font-size:small">The following resources are required to run the TaskTracker</span><br>
<span style="font-size:small">sample application.</span></div>
<ul>
<li><span style="font-size:small">An Internet connection<br>
</span></li><li><span style="font-size:small">Visual Studio 2012<br>
</span></li><li><span style="font-size:small">&nbsp;</span><span style="font-size:small"><a href="http://go.microsoft.com/fwlink/?LinkID=245849">Identity and Access Tool</a>&nbsp;</span><span style="font-size:small">for Visual Studio 2012<br>
</span></li><li><span style="font-size:small">&nbsp;</span><span style="font-size:small">A Windows Azure subscription. If you
</span><span style="font-size:small">don't have one, can get a <a href="http://www.windowsazure.com/en-us/pricing/free-trial/">
90-day free trial</a>. <br>
</span></li><li><span style="font-size:small">&nbsp;</span><span style="font-size:small">A Windows Azure AD tenant.
</span><span style="font-size:small">If you don't have a tenant, it's&nbsp;easy to create one. You can find&nbsp;</span><span style="font-size:small">instructions in the &quot;How to get a Windows Azure AD tenant&quot;
</span><span style="font-size:small">section of <a href="http://go.microsoft.com/fwlink/?LinkID=288845">
What is a Windows Azure AD tenant?</a> &nbsp;To see these instructions used in the context
</span><span style="font-size:small">of application development, see &quot;Working with Your Windows Azure AD Directory</span><br>
<span style="font-size:small">Tenant&quot; in <a href="http://msdn.microsoft.com/en-us/library/dn151790.aspx">
Adding Sign-On to You Web Application Using Windows Azure AD</a>. <br>
</span></li><li><span style="font-size:small">A user with an organizational account, such as User@ContosoEnterprises.com or User@Contoso.onmicrosoft.com. You cannot use a Microsoft account, such as User@Live.com or
<a href="mailto:User@Outlook.com">User@Outlook.com</a>. <br>
<br>
For this sample, it's best to have two users; one with the Global Administrator role&nbsp;and one with the User role. For more information, see
<strong>How to Create a User with an Organizational Account</strong>, below.<br>
</span></li><li><span style="font-size:small">Add the sample app to your Windows Azure AD tenant. For&nbsp;instructions, see&nbsp;the ReadMe in the ZIP file.<br>
</span></li><li><span style="font-size:small">Edit the <em>web.config</em> file in the app to associate it with your Windows Azure AD tenant. When you do, the app has access to the resources in your tenant. For&nbsp;instructions, see&nbsp;the ReadMe in the ZIP file.
</span></li></ul>
<h1><br>
Recommendations</h1>
<ul>
<li><span style="font-size:small"><a href="http://msdn.microsoft.com/en-us/library/windowsazure/dn151791.aspx">Using the Graph API to Query Windows Azure AD Walkthrough</a>. This sample builds</span><br>
<span style="font-size:small">directly upon the results of this walkthrough.&nbsp;&nbsp;</span>
</li></ul>
<h1><span style="font-size:small">&nbsp;<br>
</span>Directory Roles, Organizational Roles,&nbsp;and Application Roles</h1>
<div><span style="font-size:small">It's important to distinguish the Windows Azure AD&nbsp;organizational roles, the Windows Azure AD&nbsp;built-in directory roles, and&nbsp;the application roles that the sample app creates.
<br>
</span></div>
<div><span style="font-size:small">In Windows Azure AD, you can assign users to one of two organizational&nbsp;roles, Global Administrator and User. Users with these roles have different privileges in Windows Azure AD. For more information, see
<a href="http://technet.microsoft.com/en-us/library/dn468213.aspx">Assigning Admin Roles</a>.</span></div>
<div><span style="font-size:small"><br>
Windows Azure AD also has built-in directory roles, such as Company Administrator, HelpDesk Administrator, and User. When you query the Windows Azure AD Graph, you use these built-in&nbsp;roles. For a list of roles in your directory, use the
<a href="http://technet.microsoft.com/en-us/library/dn194100.aspx">Get-MsolRole</a> cmdlet. (The User role, which is included in every directory, is not returned because it is implicit.)
</span></div>
<div><span style="font-size:small"><br>
Users with the Global Administrator organizational&nbsp;role have the Company Administrator built-in role. Users with the User organizational&nbsp;role have the User built-in role.</span></div>
<div><span style="font-size:small"><br>
In addition, the sample app creates four application roles: Admin, Writer, Approver, and Observer. By default, users with the Global Administrators organizational role&nbsp;have the Admin application&nbsp;role. They can do anything in the app, except see the
 Users page, which is controlled by the ACL. Users with the User organizational role have the Observer application role. They can't do anything in the app. To change a user's application role, users with the Admin app role can&nbsp;use the RoleMapping page
 in the app. </span></div>
<div><span style="font-size:small">&nbsp;</span><span style="font-size:small">&nbsp;</span></div>
<h1><span>How to Create a User with an Organizational Account&nbsp;</span></h1>
<div><span style="font-size:small">This sample app requires a user with an organizational account. If your Windows Azure AD tenant is based on a Microsoft account, such as User@hotmail.com, you can create a user with an organizational account.<br>
<br>
For this sample, it's best to have two users, one who is a Global Administrator and one who is a User.<br>
</span></div>
<ol>
<li>
<div><span style="font-size:small">Go to the Windows Azure Management Portal (<a href="https://manage.WindowsAzure.com">https://manage.WindowsAzure.com</a>) and log in.
<br>
</span></div>
</li><li>
<div><span style="font-size:small">Click <strong>Active Directory</strong>.<br>
</span></div>
</li><li><span style="font-size:small">If your directory is&nbsp;named &quot;Default Directory&quot;, add a directory, such as &quot;ContosoEngineering&quot;. To add a directory, at the bottom of the
<strong>Active Directory </strong>page, click <strong>Add</strong>.<br>
</span></li><li><span style="font-size:small">Double-click the directory and then click <strong>
Domains</strong>. When you create your users, use the domain name that appears on the page. For example, if your domain is ContosoEngineering@onmicrosoft.com, create usernames in that domain, such as Test@ContosoEngineering@onmicrosoft.com.<br>
</span></li><li><span style="font-size:small">To create a user in the domain, click <strong>Users</strong>. (If you don't see a Users tab, double-click the directory name. The Users tab&nbsp;appears on each directory-specific page.) At the bottom of the page, click
<strong>Add User</strong>.<br>
</span></li><li><span style="font-size:small">Add a user in your new domain, such as Test@ContosoEngineering.onmicrosoft.com, and then click the checkmark at the bottom of the page.<br>
<br>
<img id="101578" src="101578-addorguser.png" alt="" width="385" height="155"></span>
</li><li>
<div><span style="font-size:small">On the User Profile page, assign an&nbsp;organizational role to the user. For this app, it's best to have one user with the&nbsp;Global Administrator role and&nbsp;one user with the User role.</span></div>
</li></ol>
<div style="padding-left:30px"><img id="101579" src="101579-1_role.png" alt="" width="391" height="101"></div>
<div><em>&nbsp;</em></div>
<div>&nbsp;</div>
<h1>Assign Roles to Groups</h1>
<div><span style="font-size:small">The TaskTracker app lets you assign application roles to
</span><span style="font-size:small">Active Directory security groups. Users in the security group have the
</span><span style="font-size:small">privileges that are granted to the application role. You can also add a
</span><span style="font-size:small">security group to the ACL. Members of group have access to the Users page that
</span><span style="font-size:small">the ACL controls.</span></div>
<div><span style="font-size:small"><br>
To test the ACL implementation, use the preview of the Group </span><span style="font-size:small">Management features in the Windows Azure Management Portal to add a security
</span><span style="font-size:small">group to your Windows Azure AD tenant.&nbsp;</span><span style="font-size:small">For instructions, see
<a href="http://go.microsoft.com/fwlink/?LinkID=331384">Add a group</a>.</span></div>
<div>&nbsp;</div>
<h1>How the Sample App Works</h1>
<div><span style="font-size:small">This section provides highlights of how the TaskTracker sample app uses the Windows Identity Foundation and Windows Azure AD graph to implement role-based and ACL-based authorization.</span></div>
<h2><span style="font-size:small">Determine a User&rsquo;s Roles at Login</span></h2>
<div><span style="font-size:small">The TaskTracker sample application uses <a href="http://msdn.microsoft.com/en-us/library/ee748475.aspx">
Windows Identity Foundation</a> (WIF) to authenticate users from Windows Azure AD. We'll extend WIF by adding role and group claims to the
<a href="http://msdn.microsoft.com/en-us/library/system.security.claims.claimsprincipal(v=vs.110).aspx">
ClaimsPrincipal</a> object that represents the user.<br>
</span></div>
<div><span style="font-size:small">As soon as a user logs in, the TaskTracker sample app gets the user's application role. To ensure that this is executed at login, in
<em>web.config</em>, we add the <strong>GraphClaimsAuthenticationManager</strong> class, a subclass of
<a href="http://msdn.microsoft.com/en-us/library/system.security.claims.claimsauthenticationmanager.aspx">
ClaimsAuthenticationManager</a>,&nbsp; to the WIF pipeline. </span></div>
<div class="endscriptcode">&nbsp;</div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>

<div class="preview">
<pre class="xml">//From:&nbsp;web.config&nbsp;
&nbsp;
&nbsp;&nbsp;<span class="xml__tag_start">&lt;system</span>.identityModel<span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;identityConfiguration</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;claimsAuthenticationManager</span>&nbsp;<span class="xml__attr_name">type</span>=<span class="xml__attr_value">&quot;TaskTracker.GraphClaimsAuthenticationManager,TaskTracker&quot;</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
&nbsp;
...</pre>
</div>
</div>
</div>
<p class="endscriptcode"><span style="font-size:small"><strong>Authenticate()</strong>, the&nbsp; main function of the
<strong>GraphClaimsAuthenticationManager</strong> class, begins by querying the Windows Azure AD Graph to determine the user&rsquo;s built-in Windows Azure AD roles. If the user has the &quot;Company Administrator&quot; built-in role, which corresponds to the Global
 Administrator organizational role in the Windows Azure Management Portal, the app assigns the app-specific &ldquo;Admin&rdquo; role to that user.</span></p>
&nbsp;</div>
<div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="js"><span class="js__sl_comment">//From:&nbsp;GraphClaimsAuthenticationManager.cs</span>&nbsp;
&nbsp;
public&nbsp;override&nbsp;ClaimsPrincipal&nbsp;Authenticate(string&nbsp;resourceName,&nbsp;ClaimsPrincipal&nbsp;incomingPrincipal&nbsp;
<span class="js__brace">{</span>&nbsp;
...&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;get&nbsp;the&nbsp;user's&nbsp;built-in&nbsp;roles</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">new</span>&nbsp;WebRetryHelper&lt;object&gt;(()&nbsp;=&gt;&nbsp;graphService.LoadProperty(currentUser,&nbsp;<span class="js__string">&quot;memberOf&quot;</span>));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;List&lt;Role&gt;&nbsp;currentRoles&nbsp;=&nbsp;currentUser.memberOf.OfType&lt;Role&gt;().ToList();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//if&nbsp;the&nbsp;user&nbsp;is&nbsp;a&nbsp;Company&nbsp;Administrator&nbsp;(Global&nbsp;Administrator),&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//assign&nbsp;them&nbsp;the&nbsp;&quot;Admin&quot;&nbsp;role&nbsp;in&nbsp;the&nbsp;app.&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;foreach(Role&nbsp;role&nbsp;<span class="js__operator">in</span>&nbsp;currentRoles)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(role.displayName.Equals(<span class="js__string">&quot;Company&nbsp;Administrator&quot;</span>))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;((ClaimsIdentity)incomingPrincipal.Identity).AddClaim(<span class="js__operator">new</span>&nbsp;Claim(ClaimTypes.Role,&nbsp;<span class="js__string">&quot;Admin&quot;</span>,&nbsp;ClaimValueTypes.<span class="js__object">String</span>,&nbsp;<span class="js__string">&quot;TaskTrackerSampleApplication&quot;</span>));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
...&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</div>
<div class="endscriptcode">
<p><span style="font-size:small">Next, it gets the user's group memberships, stores them in a
</span><span style="font-size:small">&quot;Group&quot; type claim, and adds the claim to the
<a href="http://msdn.microsoft.com/en-us/library/microsoft.identitymodel.claims.claimsidentity.aspx">
ClaimsIdentity</a>&nbsp;</span><span style="font-size:small">class. To get the user's groups, including groups that the user belongs to
</span><span style="font-size:small">because they are members of another group, it uses the
<strong>getCompleteGroupMembership</strong> function (defined in DirectoryDataService_partial.cs),</span><br>
<span style="font-size:small">which calls the <a href="http://msdn.microsoft.com/en-us/library/dn424889.aspx">
<strong>getMemberGroups</strong></a><strong> </strong>REST API function, which returns all groups to which the user
</span><span style="font-size:small">belongs. For more information about the Windows Azure AD Graph REST API, see
<a href="http://msdn.microsoft.com/en-us/library/hh974476.aspx">Windows Azure Active Directory Graph</a>.</span></p>
</div>
</div>
<div>&nbsp;</div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp">From:&nbsp;GraphClaimsAuthenticationManager.cs&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Now,&nbsp;query&nbsp;transitive&nbsp;group&nbsp;membership&nbsp;of&nbsp;the&nbsp;user</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;List&lt;<span class="cs__keyword">string</span>&gt;&nbsp;completeGroupMembership&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;WebRetryHelper&lt;List&lt;String&gt;&gt;(()&nbsp;=&gt;&nbsp;graphService.getCompleteGroupMembership(tenantId,&nbsp;currentUserObjectId,&nbsp;token)).Value;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Store&nbsp;the&nbsp;user's&nbsp;groups&nbsp;as&nbsp;claims&nbsp;of&nbsp;type&nbsp;&quot;Group&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">foreach</span>&nbsp;(<span class="cs__keyword">string</span>&nbsp;groupId&nbsp;<span class="cs__keyword">in</span>&nbsp;completeGroupMembership)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.WriteLine(<span class="cs__string">&quot;adding&nbsp;&quot;</span>&nbsp;&#43;&nbsp;groupId);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;((ClaimsIdentity)incomingPrincipal.Identity).AddClaim(<span class="cs__keyword">new</span>&nbsp;Claim(<span class="cs__string">&quot;Group&quot;</span>,&nbsp;groupId,&nbsp;ClaimValueTypes.String,&nbsp;<span class="cs__string">&quot;WindowsAzureADGraph&quot;</span>));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<div><span style="line-height:107%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;; font-size:11pt">Then it determines the user's application roles by comparing the user&rsquo;s group membership with the role mappings stored in the Roles.xml file and creates a &quot;Role&quot; claim.
</span></div>
<div></div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp">From:&nbsp;GraphClaimsAuthenticationManager.cs&nbsp;
&nbsp;
<span class="cs__com">//Get&nbsp;role&nbsp;assignments</span>&nbsp;
<span class="cs__keyword">foreach</span>(<span class="cs__keyword">string</span>&nbsp;role&nbsp;<span class="cs__keyword">in</span>&nbsp;getRoles(currentUserObjectId,&nbsp;completeGroupMembership))&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Store&nbsp;the&nbsp;user's&nbsp;application&nbsp;roles&nbsp;as&nbsp;claims&nbsp;of&nbsp;type&nbsp;&quot;Role&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;((ClaimsIdentity)incomingPrincipal.Identity).AddClaim(<span class="cs__keyword">new</span>&nbsp;Claim(ClaimTypes.Role,&nbsp;role,&nbsp;ClaimValueTypes.String,&nbsp;<span class="cs__string">&quot;TaskTrackerSampleApplication&quot;</span>));&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;
<h2>Use Application Roles to Enforce Policy</h2>
<span style="font-size:small">To enforce access to resources, the application uses the
<a href="http://msdn.microsoft.com/en-us/library/microsoft.identitymodel.claims.claimsprincipal.isinrole.aspx">
ClaimsPrincipal.IsInRole</a>() </span><span style="font-size:small">method. This method returns True if the user has a role claim that matches the
</span><span style="font-size:small">string parameter passed to the IsInRole() function, and returns False otherwise. &nbsp;
</span><br>
<br>
<span style="font-size:small">For example, in TaskTrackerController.cs, the IsInRole()
</span><span style="font-size:small">method determines whether the user has the Admin or Writer role. If they do,
</span><span style="font-size:small">they are permitted to add a task.</span></div>
</div>
<div></div>
<div></div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="js"><span class="js__sl_comment">//From:&nbsp;TaskTrackerController.cs</span>&nbsp;
&nbsp;
<span class="js__statement">if</span>&nbsp;(User.IsInRole(<span class="js__string">&quot;Admin&quot;</span>)&nbsp;||&nbsp;User.IsInRole(<span class="js__string">&quot;Writer&quot;</span>))&nbsp;
<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//add&nbsp;new&nbsp;task</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(formCollection[<span class="js__string">&quot;newTask&quot;</span>]&nbsp;!=&nbsp;null&nbsp;&amp;&amp;&nbsp;formCollection[<span class="js__string">&quot;newTask&quot;</span>].Length&nbsp;!=&nbsp;<span class="js__num">0</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlHelper.AppendTaskElemToXml(formCollection);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
<span class="js__brace">}</span>&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</div>
<div><span style="line-height:107%; font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;; font-size:11pt">The TaskTracker application also uses the
<a href="http://msdn.microsoft.com/en-us/library/system.web.mvc.authorizeattribute(v=vs.108).aspx">
Authorize attribute</a> to enforce access control. For example, in RoleController.cs, [Authorize(Roles= &quot;Admin&quot;)] grants access to the RoleMappings controller/page only to users with the Admin role.</span></div>
<div></div>
<div></div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__com">//From:&nbsp;RoleController.cs</span>&nbsp;
&nbsp;
[HttpPost]&nbsp;
[Authorize(Roles&nbsp;=&nbsp;<span class="cs__string">&quot;Admin&quot;</span>)]&nbsp;
<span class="cs__keyword">public</span>&nbsp;ActionResult&nbsp;ACLSubmit(FormCollection&nbsp;formCollection)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//remove&nbsp;ACL&nbsp;assignments&nbsp;marked&nbsp;by&nbsp;checkboxes</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;XmlHelper.RemoveACLElemFromXml(formCollection);&nbsp;
...&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;
<h2>Use an Access Control List to Enforce Access&nbsp;</h2>
</div>
</div>
<p><span style="font-size:small">In addition to RBAC, this app also demonstrates how to use an access control list (ACL) to restrict access to resources. In this app, only users and members of groups listed in the ACL can see the contents of the Users page.
 To change the ACL, use the <strong>Users Resource Access Control List</strong> feature of the RoleMappings page. Only users with the Admin application role can edit the ACL. All changes to the ACL are saved in the ACL.xml file in the App_Data directory.</span></p>
<p><span style="font-size:small">The ACL-based permission in the application works by storing the user&rsquo;s group memberships as claims of type &quot;Group&quot; at sign on. Then, when the app needs to determine whether to allow you to see the Users page, it cross-references
 the user&rsquo;s group membership claims with the stored ACL.</span></p>
<p><span style="font-size:small">Because access to the Users page is determined entirely by the ACL, not by roles, even users with the Admin app role cannot view the Users page unless they (or a group to which they belong) appear in the ACL.</span></p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="js"><span class="js__sl_comment">//From:&nbsp;HomeController.cs</span>&nbsp;
&nbsp;
[HttpGet]&nbsp;
public&nbsp;ActionResult&nbsp;Users()&nbsp;
<span class="js__brace">{</span>&nbsp;
&nbsp;
...&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//is&nbsp;user's&nbsp;objectId&nbsp;in&nbsp;the&nbsp;ACL?</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(elem.ObjectId.Equals(userObjectId))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;onACL&nbsp;=&nbsp;true;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;foreach&nbsp;(Claim&nbsp;groupClaim&nbsp;<span class="js__operator">in</span>&nbsp;((ClaimsIdentity)User.Identity).FindAll(<span class="js__string">&quot;Group&quot;</span>))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//is&nbsp;a&nbsp;group&nbsp;the&nbsp;user&nbsp;belongs&nbsp;to&nbsp;in&nbsp;the&nbsp;ACL?</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(elem.ObjectId.Equals(groupClaim.Value))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;onACL&nbsp;=&nbsp;true;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">break</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;
...&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//if&nbsp;user&nbsp;is&nbsp;not&nbsp;in&nbsp;ACL,&nbsp;do&nbsp;not&nbsp;grant&nbsp;permission</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(!onACL)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;RedirectToAction(<span class="js__string">&quot;Error&quot;</span>,&nbsp;<span class="js__string">&quot;Home&quot;</span>,&nbsp;<span class="js__operator">new</span>&nbsp;<span class="js__brace">{</span>&nbsp;errorMessage&nbsp;=&nbsp;<span class="js__string">&quot;Access&nbsp;Denied.&nbsp;To&nbsp;view&nbsp;this&nbsp;resource,&nbsp;have&nbsp;an&nbsp;admin&nbsp;add&nbsp;you&nbsp;or&nbsp;your&nbsp;group&nbsp;to&nbsp;the&nbsp;ACL.&quot;</span>&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
...&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<p>&nbsp;</p>
<h1>Troubleshooting</h1>
<div>
<div><span style="font-size:small"><strong>Microsoft Account </strong>&lt;account&gt;
<strong>is not supported for sign-in to this application</strong>. </span><span style="font-size:small">This error typically indicates that you signed into the sample app with&nbsp;a Microsoft account, such as an account in the Hotmail.com, Live.com, or Outlook.com
 domains. This app requires users to have an organizational account. For instructions, see
</span><span style="font-size:small"><strong>How to Create a User with an Organizational Account</strong> above.</span></div>
<div>&nbsp;</div>
</div>
