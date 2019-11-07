# MyCompany TRAVEL demo application
## Requires
- Visual Studio 2013
## License
- MS-LPL
## Technologies
- WPF
- knockout.js
- windows azure active directory
- ASP.NET MVC 5
- Durandal
- .NET Framework 4.5.1
- SignalR 2.0
- Entity Framework 6
- SPA Web application
- ASP.NET Web API 2
## Topics
- Desktop and Web apps development
## Updated
- 11/19/2013
## Description

<h1>Introduction</h1>
<p><em>At the Visual Studio 2013 Launch Event (Nov. 13th 2013), Microsoft used a set of demo applications to explain the new features in .NET 4.5.1 and Visual Studio 2013. Here we are publishing those demo applications.<br>
<strong><a href="http://aka.ms/mycompanyapps"></a></strong></em></p>
<p><em><strong><a href="http://aka.ms/mycompanyapps">MyCompany </a></strong>is a set of sample applications comprised of typical enterprise/business modules: Travel, Staff, Vacation, Visitors and Expenses.Each of these business applications are autonomous and
 could have been developed by different teams. They use different technologies and solutions to fulfill the different requirements from different user profiles and desired scenarios. All web and services solutions can be deployed to Windows Azure, and all of
 them are using Windows Azure Active Directory to support Single-Sign-On between all the applications in the suite.The companies can also federate the directory service with their on-premises directories (corporate Active Directory) to support SSO using the
 on-premises corporate AD credentials.A few of the web apps can also be integrated as apps for SharePoint within Office 365.In summary, different technologies are used depending on the business application scenarios: Web Apps, Windows Store, Windows Phone,
 WPF desktop, etc.<br>
<br>
<img id="101445" src="101445-travel_logo.png" alt="" width="271" height="108"><br>
This concrete business application is called <strong>TRAVEL </strong>and it is described below.</em></p>
<h1><span>Building the Sample</span></h1>
<p>Requirements</p>
<div>-Visual Studio 2013</div>
<div>-Windows Azure SDK 2.2</div>
<div>-SQL Server 2012 Express LocalDB (included in VS 2013)</div>
<div>-Windows Azure SQL DB (for Cloud deployment)</div>
<div>-Windows Azure Web Sites (for Cloud deployment)</div>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div>
<div>&bull; Travel is made up by several applications which allow employees to request and manage business travels.</div>
<div>&bull; The web application targets any employee, including managers and HHRR. Each role can make different actions. Regular employees can create and edit their own travel requests. Managers can accept or deny team member requests. HHRR can add attachments
 to the requests and accept them.</div>
<div>&bull; The desktop application (WPF) targets HHRR people, so they can edit and process travel requests.</div>
<div>&bull; In both applications email notifications are sent in some circumstances and there are also web notifications using SignalR.</div>
<div></div>
<div></div>
<div></div>
<div></div>
<div><span style="font-size:small"><strong>Travel WPF Desktop App:</strong></span></div>
<div></div>
<div><img id="101451" src="101451-travel_wpf_app.png" alt="" width="675" height="455"></div>
<div></div>
<div></div>
<div></div>
<div></div>
<div></div>
<div><span style="font-size:small"><strong>Travel Web App (SPA - Single Page Application):</strong></span></div>
<div><strong><img id="101448" src="101448-travel_web_app_heatmap_3.png" alt="" width="713" height="468"><br>
</strong></div>
<p>&nbsp;</p>
<div>
<h1><strong>Simplified Architecture Diagram</strong></h1>
</div>
</div>
<p><img id="101340" src="101340-travel%20simplified%20architecture.png" alt="" width="413" height="559"></p>
<p>&nbsp;</p>
<h1><strong>Goals</strong></h1>
<div>&bull; To highlight the importance of&nbsp;<strong>desktop clients</strong>&nbsp;(<strong>WPF</strong>) for many scenarios.</div>
<div>&bull;&nbsp;It shows new technologies in&nbsp;<strong>WPF</strong>, like&nbsp;<strong>SignalR client lib for .NET</strong>&nbsp;and&nbsp;<strong>Toast notifications</strong></div>
<div>&bull; Highlights Bing Maps use in web applications&nbsp;</div>
<p>&nbsp;</p>
<h1>Covered Technologies</h1>
<p><span style="font-size:medium">Services technologies</span></p>
<table class="GridTable1Light1" border="1" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td width="216" valign="top">
<p><strong>ASP.NET Web API 2</strong></p>
</td>
</tr>
<tr>
<td width="216" valign="top">
<p><strong>Attribute Routes</strong></p>
</td>
</tr>
<tr>
<td width="216" valign="top">
<p><strong>OWIN-Katana</strong></p>
</td>
</tr>
<tr>
<td width="216" valign="top">
<p><strong>SignalR 2.0</strong></p>
</td>
</tr>
<tr>
<td width="216" valign="top">
<p><strong>Entity Framework 6</strong></p>
</td>
</tr>
<tr>
<td width="216" valign="top">
<p><strong>Email notification</strong></p>
</td>
</tr>
<tr>
<td width="216" valign="top">
<p><strong>Windows Azure Service Bus</strong></p>
</td>
</tr>
</tbody>
</table>
<p>&nbsp;</p>
<p><span style="font-size:medium">Web Application technologies</span></p>
<table class="GridTable1Light1" border="1" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td width="216" valign="top">
<p><strong>ASP.NET MVC 5</strong></p>
</td>
</tr>
<tr>
<td width="216" valign="top">
<p><strong>SPA Web application</strong></p>
</td>
</tr>
<tr>
<td width="216" valign="top">
<p><strong>Durandal</strong></p>
</td>
</tr>
<tr>
<td width="216" valign="top">
<p><strong>Knockout.js</strong></p>
</td>
</tr>
<tr>
<td width="216" valign="top">
<p><strong>Web Mobile View</strong></p>
</td>
</tr>
</tbody>
</table>
<p>&nbsp;</p>
<p><span style="font-size:medium">Security technologies</span></p>
<table class="GridTable1Light1" border="1" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td width="216" valign="top">
<p><strong>Windows Azure Active Directory</strong></p>
</td>
</tr>
</tbody>
</table>
<p>&nbsp;</p>
<p><span style="font-size:medium">Windows Apps technologies</span></p>
<table class="GridTable1Light1" border="1" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td width="216" valign="top">
<p><strong>.NET WPF</strong></p>
</td>
</tr>
</tbody>
</table>
<p>&nbsp;</p>
<h1>What this demo applications is NOT about</h1>
<p>These applications are not production systems and are not intended as a guidance for mission-critical applications, as it mostly covers CRUD and Data-Driven scenarios, only. If you need guidance for complex scenarios, we recommend to check guidance material
 from the Microsoft Patterns &amp; Practices group like the &lsquo;CQRS Journey guidance&rsquo;, which shows a reference application with a related functional domain &amp; scope (Events/Conferences platform) but from a different point of view based on design-patterns
 and best architectural practices for complex scenarios, covering approaches like CQRS (Command &amp; Query Responsibility Segregation) &amp; DDD (Domain Driven Design).</p>
