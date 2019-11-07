# MyCompany VACATION demo application
## Requires
- Visual Studio 2013
## License
- MS-LPL
## Technologies
- OData
- windows azure active directory
- ASP.NET MVC 5
- ASP.NET SignalR
- AngularJS
- SPA Web application
- ASP.NET Web API 2
## Topics
- Web Development
## Updated
- 11/19/2013
## Description

<h1>Introduction</h1>
<p><em>At the Visual Studio 2013 Launch Event (Nov. 13th 2013), Microsoft used a set of demo applications to explain the new features in .NET 4.5.1 and Visual Studio 2013.<em>&nbsp;Here we are publishing those demo applications.</em></em></p>
<p><em><strong><a href="http://aka.ms/mycompanyapps">MyCompany</a>&nbsp;</strong>is a set of sample applications comprised of typical enterprise/business modules: Travel, Staff, Vacation, Visitors and Expenses.Each of these business applications are autonomous
 and could have been developed by different teams. They use different technologies and solutions to fulfill the different requirements from different user profiles and desired scenarios.&nbsp;All web and services solutions can be deployed to Windows Azure,
 and all of them are using Windows Azure Active Directory to support Single-Sign-On between all the applications in the suite.The companies can also federate the directory service with their on-premises directories (corporate Active Directory) to support SSO
 using the on-premises corporate AD credentials.A few of the web apps can also be integrated as apps for SharePoint within Office 365.<br>
In summary, different technologies are used depending on the business application scenarios: Web Apps, Windows Store, Windows Phone, WPF desktop, etc.</em></p>
<p><em><img id="101457" src="101457-vacation_logo.png" alt="" width="322" height="107"><br>
</em></p>
<p><em>This concrete business application is called&nbsp;<strong>VACATION&nbsp;</strong>and it is described below.</em></p>
<h1>Building the Sample</h1>
<p><strong><em>Requirements</em></strong></p>
<p><em>Visual Studio 2013</em></p>
<p><em>Windows Azure SDK 2.2</em></p>
<p><em>SQL Server 2012 Express LocalDB (included in VS 2013)</em></p>
<p><em>Windows Azure SQL DB (for Cloud deployment)</em></p>
<p><em>Windows Azure Web Sites (for Cloud deployment)</em></p>
<p>&nbsp;</p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div>&bull; <strong>Vacation </strong>is a web application that allows employees to request and manage vacation periods. The web application targets both regular employees and managers. Each role can perform different actions. Employees can create and edit
 their vacation requests. Managers can manage the team vacation requests and can view overlaps.</div>
<div>&bull; Email notifications are sent in some circumstances and there are also web notifications using SignalR.</div>
<div></div>
<div></div>
<div>
<p><em><span style="font-size:medium"><strong>Vacation Web App (SPA - Single Page Application):</strong></span></em></p>
<p><img id="101458" src="101458-vacation_web_app.png" alt="" width="851" height="481"></p>
<h1><strong>Simplified Architecture Diagram</strong><em><span style="font-size:medium"><strong><br>
</strong></span></em></h1>
</div>
<div></div>
<div><img id="101417" src="101417-vacation%20simplified%20architecture.png" alt="" width="410" height="559"></div>
<h1>Goals</h1>
<div>&bull; To highlight and compare web JavaScript development frameworks like <strong>
Angular.js</strong> vs. <strong>Durandal &amp; Knockout</strong>.</div>
<div>&bull; To highlight full <strong>OData Web API 2 services</strong> (data-driven oriented and very powerful in client queries and client data operations)</div>
<div>&bull; To highlight <strong>Apps for Office</strong> development (App for Outlook)</div>
<p>&nbsp;</p>
<h1>Covered Technologies</h1>
<p>&nbsp;</p>
<p><span style="font-size:medium">Services technologies</span></p>
<table class="GridTable1Light1" border="1" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td width="264" valign="top">
<p><strong>ASP.NET Web API 2</strong></p>
</td>
</tr>
<tr>
<td width="264" valign="top">
<p><strong>Attribute Routes</strong></p>
</td>
</tr>
<tr>
<td width="264" valign="top">
<p><strong>OWIN-Katana</strong></p>
</td>
</tr>
<tr>
<td width="264" valign="top">
<p><strong>Web API OData</strong></p>
</td>
</tr>
<tr>
<td width="264" valign="top">
<p><strong>SignalR 2.0</strong></p>
</td>
</tr>
<tr>
<td width="264" valign="top">
<p><strong>Entity Framework 6</strong></p>
</td>
</tr>
<tr>
<td width="264" valign="top">
<p><strong>Email notification</strong></p>
</td>
</tr>
<tr>
<td width="264" valign="top">
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
<td width="264" valign="top">
<p><strong>ASP.NET MVC 5</strong></p>
</td>
</tr>
<tr>
<td width="264" valign="top">
<p><strong>SPA Web application</strong></p>
</td>
</tr>
<tr>
<td width="264" valign="top">
<p><strong>Durandal</strong></p>
<p><strong>(App for SharePoint Version)</strong></p>
</td>
</tr>
<tr>
<td width="264" valign="top">
<p><strong>Knockout.js</strong></p>
<p><strong>(App for SharePoint Version)</strong></p>
</td>
</tr>
<tr>
<td width="264" valign="top">
<p><strong>Angular.js </strong></p>
<p><strong>(Standalone Version)</strong></p>
</td>
</tr>
<tr>
<td width="264" valign="top">
<p><strong>App for SharePoint</strong></p>
</td>
</tr>
<tr>
<td width="264" valign="top">
<p><strong>App for Office 365</strong></p>
</td>
</tr>
</tbody>
</table>
<p>&nbsp;</p>
<p><span style="font-size:medium">Security technologies</span></p>
<table class="GridTable1Light1" border="1" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td width="264" valign="top">
<p><strong>Windows Azure Active Directory</strong></p>
</td>
</tr>
<tr>
<td width="264" valign="top">
<p><strong>ASP.NET Identity</strong></p>
</td>
</tr>
<tr>
<td width="264" valign="top">
<p><strong>SharePoint integrated security</strong></p>
</td>
</tr>
</tbody>
</table>
<p>&nbsp;</p>
<p><span style="font-size:medium">Office 365 technologies</span></p>
<table class="GridTable1Light1" border="1" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td width="264" valign="top">
<p><strong>App for SharePoint &ndash; ASP.NET MVC</strong></p>
</td>
</tr>
<tr>
<td width="264" valign="top">
<p><strong>App for Office 365 &ndash; Mail app</strong></p>
</td>
</tr>
</tbody>
</table>
<p>&nbsp;</p>
<h1>What this demo application is NOT about</h1>
<p>These applications are not production systems and are not intended as a guidance for mission-critical applications, as it mostly covers CRUD and Data-Driven scenarios, only. If you need guidance for complex scenarios, we recommend to check guidance material
 from the Microsoft Patterns &amp; Practices group like the &lsquo;CQRS Journey guidance&rsquo;, which shows a reference application with a related functional domain &amp; scope (Events/Conferences platform) but from a different point of view based on design-patterns
 and best architectural practices for complex scenarios, covering approaches like CQRS (Command &amp; Query Responsibility Segregation) &amp; DDD (Domain Driven Design).</p>
