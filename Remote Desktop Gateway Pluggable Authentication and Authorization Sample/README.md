# Remote Desktop Gateway Pluggable Authentication and Authorization Sample
## Requires
- Visual Studio 2012
## License
- MS-LPL
## Technologies
- C++
- Visual Studio 2012
## Topics
- Authentication
- customization
- Code Sample
- remote desktop
## Updated
- 03/27/2014
## Description

<h1></h1>
<h1><span style="font-size:medium">RD Gateway Authentication and Authorization Models</span></h1>
<div id="offlineDescription">
<div id="longDesc">
<div id="mainSection">
<p style="text-align:left">In Windows Server 2012 R2, RD Gateway supports two authentication and authorization models:
<em>native</em> and <em>custom</em>. The default configuration of RD Gateway uses the native model for both.</p>
<dl></dl>
<p style="text-align:left">In the <em>native authentication model</em>, a user connecting through RD Gateway is authenticated by password or smart card using Integrated Windows Authentication. This can be configured with Connection Authorization Policies (CAPs)
 on the RD Gateway Server. Similarly, in the <em>native authorization model</em>, a user connecting through RD Gateway is authorized to access a specified resource (computer) as controlled by Connection Authorization Policies (CAPs) and Resource Authorization
 Policies (RAPs) on the RD Gateway Server. The policies can leverage security groups defined in RD Gateway as well as in Active Directory.</p>
<dl></dl>
<p style="text-align:left">The custom models, otherwise known as the <em>pluggable authentication model</em> and
<em>pluggable authorization model</em>, are designed for organizations that</p>
<dl></dl>
<p style="text-align:left">&nbsp;&nbsp;&nbsp;&nbsp;a)&nbsp;want to use custom authentication or authorization methods and/or</p>
<p style="text-align:left">&nbsp;&nbsp;&nbsp;&nbsp;b)&nbsp;want finer-grained control than provided by the inbox CAP/RAP system.</p>
<dl></dl>
<p style="text-align:left">In these models, all aspects of the authentication/authorization process are controlled by a plugin DLL, which may be further integrated into an organization&rsquo;s access control systems. It is possible to choose to use custom authentication
 only, custom authorization only, or both custom authentication and custom authorization.</p>
<dl></dl>
<p style="text-align:left">In order for the plug-in to perform authentication/authorization of a connection, it often needs custom data to be supplied by an external authentication/authorization mechanism. In an end-to-end system this is often supplied by a
 web portal, as shown in the flow below:</p>
<p style="text-align:left">&nbsp;</p>
<p style="text-align:left"><img width="807" src="111278-flow.png" alt="" height="405" style="width:707px; height:375px"></p>
<p style="text-align:left">&nbsp;</p>
<p style="text-align:left">In clients before Windows Server 2012 R2 and Windows 8.1, custom authentication and authorization data was provided via IE browser cookie only. In Windows 8.1 and Windows 7 SP1 with the RDP 8.1 Update, this data may be provided as
 part of the .RDP file used to launch a connection, providing cross-browser compatibility as well as scenarios outside of the browser.</p>
<dl></dl>
<p>Additionally, the plug-in may be used to provide custom accounting.</p>
<dl></dl>
<h1><a id="A1"></a><span style="font-size:medium">Configure Development Environment</span></h1>
<p><strong>Note:</strong> Before implementing a new RD Gateway PAA plugin, we recommend reviewing the
<a href="http://msdn.microsoft.com/en-us/library/windows/desktop/ee672231(v=vs.85).aspx">
Remote Desktop Gateway Interfaces</a> documentation on MSDN.</p>
<dl></dl>
<p>To compile the sample code, you&rsquo;ll need to install the required software:</p>
<ol>
<li>Install <a href="http://msdn.microsoft.com/en-us/library/vstudio/dd831853.aspx">
Microsoft Visual Studio 2012</a>. </li><li>If it has not been already installed, install <a href="http://www.microsoft.com/en-us/download/details.aspx?id=8279">
Microsoft Windows SDK for Windows 7 and .NET Framework 4</a> or later. </li></ol>
<h1><span style="font-size:medium">Build the Sample Code</span></h1>
<p>In this process, you&rsquo;ll generate the following sample RD Gateway plug-ins:</p>
<dl></dl>
<p>&nbsp;&nbsp;&nbsp;Authentication: <em>RDGAuthenticationPlugin.dll</em></p>
<p>&nbsp;&nbsp;&nbsp;Authorization: <em>RDGAuthorizationPlugin.dll</em></p>
<ol>
<li>Start Visual Studio&nbsp;2012 and select <strong>File</strong> &gt; <strong>Open</strong> &gt;
<strong>Project/Solution</strong>. </li><li>Go to the directory in which you extracted the sample. Go to the directory named for the sample, and double-click the Visual Studio&nbsp;2012 Solution (.sln) file.
</li><li>Make sure the &quot;x64 release&quot; solution configuration is selected. </li><li>Press F7 or use <strong>Build</strong> &gt; <strong>Build Solution</strong> to build the sample.
</li></ol>
<h1><span style="font-size:medium">Deployment Steps for the Custom Authentication and Authorization Plugins</span></h1>
<p><strong>Note:</strong> In order to evaluate the sample plugins, a RD Gateway Server machine must be setup, with the Remote Desktop Gateway role installed and configured with an appropriate SSL certificate. It is recommended to test connectivity through the
 RD Gateway server using native authentication/authorization before installing the sample plugins.</p>
<dl></dl>
<p>Follow the below steps on the RD Gateway Server to deploy the pluggable authentication and authorization DLLs generated in the previous section.</p>
<dl></dl>
<ol>
<li>Create the following folder: <em>%SystemDrive%\RDGPlugins</em> </li><li>Copy the files listed below to the newly created RDGPlugins directory. These files can be found in the same .zip file as this sample.
<p>&nbsp;&nbsp;&nbsp;&nbsp;RDGPluginPolicy.xml</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;SetAuthenticationPlugin.vbs</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;SetAuthorizationPlugin.vbs</p>
<dl></dl>
<table>
<tbody>
<tr>
<th>
<h1><span style="font-size:medium">File Name</span></h1>
</th>
<td>
<h1><span style="font-size:medium">Description</span></h1>
</td>
</tr>
<tr>
<th>
<p><span style="font-size:small">RDGPluginPolicy.xml</span></p>
</th>
<td><dt>The RD Gateway PAA sample plugin needs an authentication and authorization policy to validate against user information. This XML file contains a sample set of authentication and authorization criteria that the plugin will evaluate at connection time.
 (In an actual deployment, the plugin would approve/deny users based on the organization&rsquo;s desired authentication and authorization backend - for example, the native model uses Active Directory). Before clients will be able to connect, you must edit this
 file to configure the plugin using the parameters below. This file must be placed in
<em>%SystemDrive%\RDGPlugins</em>. All of the parameters are required:
<dl></dl>
</dt><dt><strong>CookieValue</strong> contains the cookie data which the sample authentication plugin expects to receive from the client through RD Gateway. In this sample, the cookie value and the
<strong>CookieValue</strong> parameter should be set to the following:
<dl></dl>
</dt><dt>Domain\UserName
<dl></dl>
</dt><dt>&nbsp;&nbsp;&nbsp;Where Domain is the domain of the user you wish to allow and UserName is the user name of the user you wish to allow.
</dt><dt>&nbsp;&nbsp;&nbsp;In an actual deployment, the domain/username information might be obtained via a call to a backend system.
<dl></dl>
</dt><dt><strong>Timeout</strong> will be used to define a timeout, specified in minutes, after which a
<strong>TimeoutAction</strong> will be performed. You may also specify 0 to indicate no timeout (and thus no
<strong>TimeoutAction</strong>) is to be performed.
<dl></dl>
</dt><dt><strong>TimeoutAction </strong>specifies what action is to be performed after a
<strong>Timeout</strong>. The following actions are available:
<dl></dl>
</dt><dt>&nbsp;&nbsp;&nbsp;0 : Reauthenticate User after &quot;timeout&quot; period </dt><dt>&nbsp;&nbsp;&nbsp;1 : Disconnect User after &quot;timeout&quot; period </dt><dt>&nbsp;&nbsp;&nbsp;2 : Cancel Authentication
<dl></dl>
</dt><dt><strong>UserName </strong>is used to define the expected username to allow, in the format
<dl></dl>
</dt><dt>Domain\UserName
<dl></dl>
</dt><dt>&nbsp;&nbsp;&nbsp;Where Domain is the domain of the user you wish to allow and UserName is the user name of the user you wish to allow.
<dl></dl>
</dt><dt><strong>ClientMachine </strong>defines the FQDN of the client PC that will be allowed to connect. Connections will only be allowed from this machine.
<dl></dl>
</dt><dt><strong>SmartcardAllowed </strong>defines if Smart Card authentication is permitted. Valid values are the following:
<dl></dl>
</dt><dt>&nbsp;&nbsp;&nbsp;false&nbsp;&nbsp;Smart Card authentication is denied. </dt><dt>&nbsp;&nbsp;&nbsp;true&nbsp;&nbsp;Smart Card authentication is accepted.
<dl></dl>
</dt><dt><strong>PasswordAllowed </strong>defines if Password authentication is permitted. Valid values are the following:
<dl></dl>
</dt><dt>&nbsp;&nbsp;&nbsp;false&nbsp;&nbsp;Password authentication is denied. </dt><dt>&nbsp;&nbsp;&nbsp;false&nbsp;&nbsp;Password authentication is accepted.
<dl></dl>
</dt><dt><strong>ResourceMachine </strong>defines the FQDN of the Resource machine that the client is connecting to. Connections will only be allowed to this machine.
<dl></dl>
</dt><dt><strong>PortNumber </strong>defines the TCP/UDP port of the Resource machine that the client is connecting to using RDP. Connections will only be allowed to this port.
<dl></dl>
</dt></td>
</tr>
<tr>
<th>
<p><span style="font-size:small">SetAuthenticationPlugin.vbs</span></p>
</th>
<td><dt>During the deployment process, the authentication DLL must be registered with RD Gateway. This is done by passing the plugin name to RDG&rsquo;s
<em>SetAuthenticationPluginAndRecycleRPCApplicationPools</em> WMI method. This script file will make this WMI call and register the authentication plugin with RD Gateway.
<dl></dl>
</dt></td>
</tr>
<tr>
<th>
<p><span style="font-size:small">SetAuthorizationPlugin.vbs</span></p>
</th>
<td><dt>During the deployment process, the authorization DLL must be registered with RD Gateway. This is done by passing the plugin name to RDG&rsquo;s
<em>SetAuthorizationPlugin</em> WMI method. This script file will make this WMI call and register the authorization plugin with RD Gateway.
<dl></dl>
</dt></td>
</tr>
</tbody>
</table>
<dl></dl>
</li><li>Copy <em>RDGAuthenticationPlugin.dll</em> and <em>RDGAuthorizationPlugin.dll</em> you compiled to
<em>%SystemDrive%\RDGPlugins</em>. </li><li>As the authentication and authorization DLLs are COM components, COM registration is required using regsvr32.exe. Open an elevated command prompt and navigate to the DLL path then run the following commands to register the new authentication and authorization
 DLLs:
<p>&nbsp;&nbsp;&nbsp;&nbsp;<em>regsvr32.exe RDGAuthenticationPlugin.dll</em></p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;<em>regsvr32.exe RDGAuthorizationPlugin.dll</em></p>
<dl></dl>
</li><li>In order for RD Gateway to load custom plugins, additional registration via WMI is required. Run the below commands in an elevated command prompt from the
<em>%SystemDrive%\RDGPlugins</em> directory to register the plugins via WMI.
<p>&nbsp;&nbsp;&nbsp;&nbsp;<em>cscript SetAuthenticationPlugin.vbs RdgTestAuthenticationPlugin</em></p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;<em>cscript SetAuthorizationPlugin.vbs RdgTestAuthorizationPlugin</em></p>
<dl></dl>
<p><strong>Note:</strong> Plugin names are hardcoded in the source code. When you are using these plugins, you have to use the same names until or unless changes made in the sample source code.</p>
<dl></dl>
</li><li>Stop and start the RD Gateway service.
<p>&nbsp;&nbsp;&nbsp;&nbsp;net stop tsgateway</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;net start tsgateway</p>
<dl></dl>
</li></ol>
<p>The RD Gateway server is now fully configured with the sample pluggable authentication and authorization plugins.</p>
<dl></dl>
<dl></dl>
<dl></dl>
<p>In order for clients to authenticate, the following properties need to be specified (as well as the RD Gateway information) in the .RDP file clients are using to connect:</p>
<dl></dl>
<p>&nbsp;&nbsp;&nbsp;&nbsp;gatewaycredentialssource:i:5</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;gatewayaccesstoken:s:<em>PlainTextToken</em></p>
<dl></dl>
<p>For this sample, the following properties would be used:</p>
<dl></dl>
<p>&nbsp;&nbsp;&nbsp;&nbsp;gatewaycredentialssource:i:5</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;gatewayaccesstoken:s:<em>domain\username</em></p>
<dl></dl>
<p><strong>Note:</strong> The <em>gatewayaccesstoken</em> property is available only in Windows 8.1&#43; and Windows 7 with the RDP 8.1 Update installed. In order to use custom authentication and authorization models in prior versions of Windows clients, please
 see the following blog article:&nbsp;<a href="http://blogs.msdn.com/b/rds/archive/2010/01/06/customizing-rd-gateway-authentication-and-authorization-schemes.aspx">Customizing RD Gateway authentication and authorization schemes</a></p>
<dl></dl>
<p>There is an example .RDP file available in the sample .zip file. In order to use this sample .RDP file, the following entries must be updated:
<em>full address</em>, <em>gatewayhostname</em> and <em>gatewayaccesstoken</em>.</p>
<dl></dl>
<h1><span style="font-size:medium">Uninstallation</span></h1>
<dl></dl>
<p>Follow the below steps on the RD Gateway Server to uninstall the custom authentication and authorization plugin and switch back to native authentication and authorization:</p>
<dl></dl>
<ol>
<li>Set the default authentication and authorization plugins. Run the below commands in an elevated command prompt.
<p>&nbsp;&nbsp;&nbsp;&nbsp;<em>cscript SetAuthenticationPlugin.vbs native</em></p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;<em>cscript SetAuthorizationPlugin.vbs native</em></p>
<dl></dl>
</li><li>Unregister COM DLLs. Run the below commands in an elevated command prompt from the DLL directory.
<p>&nbsp;&nbsp;&nbsp;&nbsp;<em>regsvr32.exe /u RDGAuthenticationPlugin.dll</em></p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;<em>regsvr32.exe /u RDGAuthorizationPlugin.dll</em></p>
<dl></dl>
</li><li>Stop and start the RD Gateway service.
<p>&nbsp;&nbsp;&nbsp;&nbsp;net stop tsgateway</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;net start tsgateway</p>
<dl></dl>
</li></ol>
<p>The RD Gateway Server is now configured with native authentication and authorization (using CAPs and RAPs).</p>
<h1><span style="font-size:medium">Limitations</span></h1>
<dl></dl>
<ol>
<li>You can&rsquo;t configure an RD Gateway server to simultaneously use both native authentication and custom authentication.
</li><li>You can&rsquo;t configure an RD Gateway server to simultaneously use both native authorization and custom authorization.
<dl></dl>
</li></ol>
<h1><span style="font-size:medium">RD Gateway Server event logs for PAA</span></h1>
<dl></dl>
<p>The below events in Windows Event log may be used to verify and troubleshoot a deployed PAA plugin:</p>
<dl></dl>
<p>When RDG is configured to use a custom authentication plugin, the below event will be created in the Windows Event log:</p>
<table>
<tbody>
<tr>
<th>
<p><span style="font-size:small">Event ID: </span></p>
</th>
<td>1002</td>
</tr>
<tr>
<th>
<p><span style="font-size:small">Log Name:</span></p>
</th>
<td>Microsoft-Windows-TerminalServices-Gateway/Admin</td>
</tr>
<tr>
<th><span style="font-size:small">Event Message</span>:</th>
<td>The user authentication plug-in &quot;RdgTestAuthenticationPlugin&quot; has been configured. The configuration will take effect after the RD Gateway service is restarted.</td>
</tr>
</tbody>
</table>
<dl></dl>
<dl></dl>
<p>When RDG is configured to use a custom authorization plugin, the below event will be created in the Windows Event log:</p>
<table>
<tbody>
<tr>
<th>
<p><span style="font-size:small">Event ID: </span></p>
</th>
<td>1004</td>
</tr>
<tr>
<th>
<p><span style="font-size:small">Log Name:</span></p>
</th>
<td>Microsoft-Windows-TerminalServices-Gateway/Admin</td>
</tr>
<tr>
<th>
<p><span style="font-size:small">Event Message:</span></p>
</th>
<td>The user authorization plug-in &quot;RdgTestAuthorizationPlugin&quot; is enabled. No user action is required.</td>
</tr>
</tbody>
</table>
<dl></dl>
<dl></dl>
<p>When a custom authentication plugin is uninstalled, the below event will be created in the Windows Event log:</p>
<table>
<tbody>
<tr>
<th>
<p><span style="font-size:small">Event ID: </span></p>
</th>
<td>1002</td>
</tr>
<tr>
<th>
<p><span style="font-size:small">Log Name:</span></p>
</th>
<td>Microsoft-Windows-TerminalServices-Gateway/Admin</td>
</tr>
<tr>
<th>
<p><span style="font-size:small">Event Message:</span></p>
</th>
<td>The user authentication plug-in &quot;native&quot; has been configured. The configuration will take effect after the RD Gateway service is restarted.</td>
</tr>
</tbody>
</table>
<dl></dl>
<dl></dl>
<p>When a custom authorization plugin is uninstalled, the below event will be created in the Windows Event log:</p>
<table>
<tbody>
<tr>
<th>
<p><span style="font-size:small">Event ID: </span></p>
</th>
<td>1004</td>
</tr>
<tr>
<th>
<p><span style="font-size:small">Log Name:</span></p>
</th>
<td>Microsoft-Windows-TerminalServices-Gateway/Admin</td>
</tr>
<tr>
<th>
<p><span style="font-size:small">Event Message:</span></p>
</th>
<td>The user authorization plug-in &quot;native&quot; is enabled. No user action is required.</td>
</tr>
</tbody>
</table>
<dl></dl>
<dl></dl>
<p>When a client is successfully connected using PAA, the below event will be created in the Windows Event log:</p>
<table>
<tbody>
<tr>
<th>
<p><span style="font-size:small">Event ID:</span></p>
</th>
<td>200</td>
</tr>
<tr>
<th>
<p><span style="font-size:small">Log Name:</span></p>
</th>
<td>Microsoft-Windows-TerminalServices-Gateway/Operational</td>
</tr>
<tr>
<th>
<p><span style="font-size:small">Event Message:</span></p>
</th>
<td>The user <em>&quot;domain\user&quot;</em>, on client computer <em>&quot;IP Address&quot;</em>, met connection authorization policy requirements and was therefore authorized to access the RD Gateway server. The authentication method used was: &quot;Cookie&quot; and connection protocol
 used: &quot;HTTP&quot;.</td>
</tr>
</tbody>
</table>
<dl></dl>
<dl></dl>
<p>When a client attempts to connect using PAA and fails, the below event will be created in the Windows Event log:</p>
<table>
<tbody>
<tr>
<th>
<p><span style="font-size:small">Event ID:</span></p>
</th>
<td>306</td>
</tr>
<tr>
<th>
<p><span style="font-size:small">Log Name:</span></p>
</th>
<td>Microsoft-Windows-TerminalServices-Gateway/Operational</td>
</tr>
<tr>
<th>
<p><span style="font-size:small">Event Message:</span></p>
</th>
<td>The user <em>&quot;domain\user&quot;</em>, on client computer <em>&quot;IP Address&quot;</em>, was not authorized to connect to the RD Gateway server because a tunnel could not be created. The authentication method attempted: &quot;Cookie&quot; and connection protocol &quot;HTTP&quot;. The following
 error occurred: &quot;2147965433&quot;.</td>
</tr>
</tbody>
</table>
<dl></dl>
<dl></dl>
</div>
</div>
</div>
<div class="mcePaste" id="_mcePaste" style="left:-10000px; top:3281px; width:1px; height:1px; overflow:hidden">
</div>
