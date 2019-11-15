# MyCompany EXPENSES demo application
## Requires
- Visual Studio 2013
## License
- MS-LPL
## Technologies
- jQuery Mobile
- windows azure active directory
- Windows Phone 8
- CORS
- .NET Framework 4.5.1
- OWIN self-hosting
- Entity Framework 6
- Windows 8.1 HTML/WinJS
- ASP.NET Web API 2
## Topics
- Devices and Services development
## Updated
- 11/19/2013
## Description

<h1>Introduction</h1>
<p><em>At the Visual Studio 2013 Launch Event (Nov. 13th 2013), Microsoft used a set of demo applications to explain the new features in .NET 4.5.1 and Visual Studio 2013.<em>&nbsp;Here we are publishing those demo applications.</em></em></p>
<p><em><strong><a href="http://aka.ms/mycompanyapps">MyCompany</a></strong> is a set of sample applications comprised of typical enterprise/business modules: Travel, Staff, Vacation, Visitors and Expenses.Each of these business applications are autonomous and
 could have been developed by different teams. They use different technologies and solutions to fulfill the different requirements from different user profiles and desired scenarios.&nbsp;All web and services solutions can be deployed to Windows Azure, and
 all of them are using Windows Azure Active Directory to support Single-Sign-On between all the applications in the suite.The companies can also federate the directory service with their on-premises directories (corporate Active Directory) to support SSO using
 the on-premises corporate AD credentials.A few of the web apps can also be integrated as apps for SharePoint within Office 365.<br>
In summary, different technologies are used depending on the business application scenarios: Web Apps, Windows Store, Windows Phone, WPF desktop, etc.</em></p>
<p><em><img id="101438" src="http://i1.code.msdn.s-msft.com/mycompany-expenses-demo-ed172993/image/file/101438/1/expenses_logo.png" alt="" width="316" height="107"><br>
</em></p>
<p><em>This concrete business application is called&nbsp;<strong>EXPENSES&nbsp;</strong>and it is described below.</em></p>
<h1><span>Building the Sample</span></h1>
<p><strong>Requirements</strong></p>
<div>-Visual Studio 2013</div>
<div>-Windows Azure SDK 2.2</div>
<div>-SQL Server 2012 Express LocalDB (included in VS 2013)</div>
<div>-Windows Azure SQL DB (for Cloud deployment)</div>
<div>-Windows Azure Web Sites (for Cloud deployment)</div>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<div>&bull; Expenses is composed by several client applications to facilitate expenses management to the employees.</div>
<div>&bull; The web mobile application is an application for employees, managers and HHRR. Each role can make different actions. Employees can create and edit their travel requests. Managers can accept or deny the team member requests. HHRR can add attachments
 to the requests and accept them.</div>
<div>&bull; The Windows 8 HTML5/WinJS native application targets managers, so they can review all the team&rsquo;s expenses, approve an deny them and view summaries.</div>
<div>&bull; The Windows Phone 8 native application targets employees and managers. The employees can report their expenses and managers can review/approve team expenses.</div>
<div></div>
<div></div>
<div></div>
<div><strong><span style="font-size:small">EXPENSES Windows Store app (HTML/WinJS)</span></strong></div>
<div><img id="101440" src="http://i1.code.msdn.s-msft.com/mycompany-expenses-demo-ed172993/image/file/101440/1/expenses_windows_store_app.png" alt="" width="702" height="434"></div>
<div></div>
<div>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p><span style="font-size:small"><strong>EXPENSES Windows Phone native app (C#/XAML)</strong></span></p>
<p><strong><img id="101444" src="http://i1.code.msdn.s-msft.com/mycompany-expenses-demo-ed172993/image/file/101444/1/expenses_phone_native_app.png" alt="" width="256" height="457"><br>
</strong></p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p><span style="font-size:small"><strong>EXPENSES Web Mobile app (ASP.NET MVC)</strong></span></p>
<div><strong><span><img id="101443" src="http://i1.code.msdn.s-msft.com/mycompany-expenses-demo-ed172993/image/file/101443/1/expenses_web_mobile_app.png" alt="" width="310" height="444"></span></strong></div>
<div></div>
<div></div>
<h1><strong><span>Simplified Architecture Diagram </span></strong></h1>
</div>
<div></div>
<div></div>
<div></div>
<div><img id="101339" src="101339-expenses%20simplified%20architecture.png" alt="" width="406" height="559"></div>
<h1><strong>Goals</strong></h1>
<div>&bull; Demo support to highlight <strong>Web API 2&nbsp;</strong>features like
<strong>attribute routes</strong>, <strong>CORS</strong>, <strong>OWIN self-hosting</strong>. Also, HTML/WinJS native store apps, and Web Mobile apps for any mobile client.&nbsp;</div>
<p>&nbsp;</p>
<h1><span>Covered Technologies</span></h1>
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
<p><strong>CORS</strong></p>
</td>
</tr>
<tr>
<td width="216" valign="top">
<p><strong>OWIN-Katana</strong></p>
</td>
</tr>
<tr>
<td width="216" valign="top">
<p><strong>OWIN self-hosting</strong></p>
</td>
</tr>
<tr>
<td width="216" valign="top">
<p><strong>Entity Framework 6</strong></p>
</td>
</tr>
<tr>
<td width="216" valign="top">
<p><strong>Windows Azure Service Bus</strong></p>
</td>
</tr>
</tbody>
</table>
<p><span style="font-size:medium">Web Application technologies</span></p>
<table class="GridTable1Light1" border="1" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td width="216" valign="top">
<p><strong>JQuery Mobile</strong></p>
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
<p><strong>Windows 8.1 Store HTML/JS</strong></p>
</td>
</tr>
<tr>
<td width="216" valign="top">
<p><strong>Windows 8.1 Push notifications</strong></p>
</td>
</tr>
<tr>
<td width="216" valign="top">
<p><strong>Windows Phone C#/XAML</strong></p>
</td>
</tr>
<tr>
<td width="216" valign="top">
<p><strong>Windows Phone Push notifications</strong></p>
</td>
</tr>
</tbody>
</table>
<p>&nbsp;</p>
<h1>What this demo application is NOT about</h1>
<p>These applications are not production systems and are not intended as a guidance for mission-critical applications, as it mostly covers CRUD and Data-Driven scenarios, only. If you need guidance for complex scenarios, we recommend to check guidance material
 from the Microsoft Patterns &amp; Practices group like the &lsquo;CQRS Journey guidance&rsquo;, which shows a reference application with a related functional domain &amp; scope (Events/Conferences platform) but from a different point of view based on design-patterns
 and best architectural practices for complex scenarios, covering approaches like CQRS (Command &amp; Query Responsibility Segregation) &amp; DDD (Domain Driven Design).</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p><span><br>
</span></p>
