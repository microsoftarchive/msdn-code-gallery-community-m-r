# MyCompany demo applications [Microsoft Visual Studio 2013 - .NET 4.5.1]
## License
- MS-LPL
## Technologies
- C#
- ASP.NET
- .NET
- LINQ
- Microsoft Azure
- XAML
- SharePoint
- jQuery
- .NET Framework
- Javascript
- Visual Studio LightSwitch
- Service Bus
- Windows Phone
- C# Language
- Bootstrap
- HTML5
- ASP.NET Web API
- knockout.js
- SharePoint Server 2013
- apps for SharePoint
- Visual Studio 2013
- SignalR
- windows azure active directory
- Windows RT
- Windows Phone 8
- Windows Store app
- .NET Development
- ASP.NET MVC 5
- ASP.NET SignalR
- Durandal
- AngularJS
- .NET Framework 4.5.1
- OWIN self-hosting
- Web API 2
- SignalR 2.0
- Entity Framework 6
- SPA Web application
- ASP.NET Scaffolding
- Office 365 Cloud Business Application
- LightSwitch 2013 HTML5 Client
- Windows 8.1 XAML/C#
- Windows 8.1 HTML/WinJS
## Topics
- New technologies in Visual Studio 2013
## Updated
- 05/16/2014
## Description

<h1>Introduction</h1>
<p><em>At the Visual Studio 2013 Launch Event (Nov. 13th 2013), Microsoft used a set of demo applications to explain the new features in .NET 4.5.1 and Visual Studio 2013.</em></p>
<p>&quot;MyCompany demo applications&quot;&nbsp;is a simplified enterprise application suite, single tenant, SaaS solution deployed on Windows Azure and O365, and composed by several autonomous custom business applications.</p>
<p>Short URL for this page:&nbsp;<a href="http://aka.ms/mycompanyapps">http://aka.ms/mycompanyapps</a>&nbsp;</p>
<p><span style="font-size:medium"><strong>Apps Download landing pages:</strong></span></p>
<p><span style="font-size:small">1.&nbsp;<a href="http://aka.ms/MyCompanyVisitors"><strong>Visitors</strong>&nbsp;application</a></span></p>
<p><span style="font-size:small">2.&nbsp;<a href="http://aka.ms/MyCompanyExpenses"><strong>Expenses</strong>&nbsp;application</a></span></p>
<p><span style="font-size:small">3.&nbsp;<a href="http://aka.ms/MyCompanyStaff"><strong>Staff</strong>&nbsp;application</a></span></p>
<p><span style="font-size:small">4.&nbsp;<a href="http://aka.ms/MyCompanyTravel"><strong>Travel</strong>&nbsp;application</a></span></p>
<p><span style="font-size:small">5.&nbsp;<a href="http://aka.ms/MyCompanyVacation"><strong>Vacation</strong>&nbsp;application</a></span></p>
<p><span style="font-size:small">6. <a href="http://aka.ms/MyCompanyFaceRecognition">
<strong>Face recognition</strong> app</a></span></p>
<p>&nbsp;</p>
<p><img alt="" id="101464" src="101464-global%20apps%20picture%20-%20small.png" width="786" height="521"></p>
<p>&nbsp;</p>
<p>Each of these business applications follow these approaches and facts:</p>
<p>Each business application may have&nbsp;several client-apps&nbsp;(Web, Windows 8.1, Windows Phone 8,&nbsp;and&nbsp;Desktop) depending on the scenarios targeting different type of users.</p>
<p>Each business application is&nbsp;autonomous&nbsp;and&nbsp;loosely-coupled&nbsp;from the other applications with clear established&nbsp;boundaries&nbsp;surrounding each&nbsp;application context, from a development lifecycle point of view and from a production
 execution point of view.</p>
<p>Each business application owns&nbsp;its own entity-model,&nbsp;database&nbsp;and&nbsp;code/libraries. This is trying to simulate real development lifecycles of large enterprise applications composed by different autonomous sub-systems. But, at the same time,
 each application is a small application that can be treated autonomously and easier than when working with monolithic and large applications.</p>
<p>Even when these applications are autonomous, they are conveniently correlated in the following points:</p>
<p>- Single sign-on&nbsp;between all the suite applications, so a user does not have to have different credentials for each application (Based on corporate credentials from a&nbsp;Windows Azure Active Directory&nbsp;tenant and/or&nbsp;Office 365)</p>
<p>- Asynchronously communication between all applications, propagating just the minimum required common data/entities, like employee data, etc. This is based on asynchronous events, an event-bus and a publish-subscription approach (internally based on&nbsp;Windows
 Azure Service Bus).</p>
<p><em><span style="font-size:20px; font-weight:bold">Description</span></em></p>
<p><em><strong>&quot;MyCompany&quot; </strong>is a modern business demo application suite that leverages the Cloud OS pillars. These apps are business oriented and are using different technologies and clients in each business application. Each application is not very
 complex and pretty data-driven, so it is easy to explore. <br>
The Services are based on Web API 2 and SignalR for services. The several web apps available are following a SPA approach (Single page applications) based on ASP.NET MVC and JavaScript frameworks (Knockout, Angular, Bootstrap, etc.) and they can be deployed
 on-premises or into Windows Azure. <br>
Regarding the client side and native apps, is has several Windows Store 8.1 clients (XAML/C# and HTML5/JS), several Windows Phone clients (XAML/C#), a WPF desktop client, and even related iOS and Android apps develop with C#/Xamarin (There last apps are not
 available in this download, but will be available soon in different URLs, probably in Xamarin's web site).</em></p>
<p><img alt="" id="101465" src="101465-global%20screenshot.png" width="692" height="300"></p>
<p><em><em><img alt=""></em></em></p>
<h1>Covered Technologies</h1>
<p><span style="font-size:medium">Services technologies</span></p>
<table class="GridTable1Light1" border="1" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td width="217" valign="top">
<p><strong>&nbsp;</strong></p>
</td>
<td width="142" valign="top">
<p><strong>Staff</strong></p>
</td>
<td width="121" valign="top">
<p><strong>Expenses</strong></p>
</td>
<td width="160" valign="top">
<p><strong>Visitors</strong></p>
</td>
<td width="160" valign="top">
<p><strong>Travel</strong></p>
</td>
<td width="160" valign="top">
<p><strong>Vacation</strong></p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>ASP.NET Web API 2</strong></p>
</td>
<td width="142" valign="top">
<p>&nbsp;</p>
</td>
<td width="121" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>Attribute Routes</strong></p>
</td>
<td width="142" valign="top">
<p>&nbsp;</p>
</td>
<td width="121" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>CORS</strong></p>
</td>
<td width="142" valign="top">
<p>&nbsp;</p>
</td>
<td width="121" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>OWIN-Katana</strong></p>
</td>
<td width="142" valign="top">
<p>X</p>
</td>
<td width="121" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>OWIN self-hosting</strong></p>
</td>
<td width="142" valign="top">
<p>&nbsp;</p>
</td>
<td width="121" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>Web API OData</strong></p>
</td>
<td width="142" valign="top">
<p>&nbsp;</p>
</td>
<td width="121" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>LightSwitch OData Services</strong></p>
</td>
<td width="142" valign="top">
<p>X</p>
</td>
<td width="121" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>SignalR 2.0</strong></p>
</td>
<td width="142" valign="top">
<p>X</p>
</td>
<td width="121" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>Entity Framework 6</strong></p>
</td>
<td width="142" valign="top">
<p>&nbsp;</p>
</td>
<td width="121" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>Email notification</strong></p>
</td>
<td width="142" valign="top">
<p>&nbsp;</p>
</td>
<td width="121" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>Windows Azure Service Bus</strong></p>
</td>
<td width="142" valign="top">
<p>X</p>
</td>
<td width="121" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
</tr>
</tbody>
</table>
<p>&nbsp;</p>
<p><span style="font-size:medium">Web Application technologies</span></p>
<table class="GridTable1Light1" border="1" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td width="217" valign="top">
<p><strong>&nbsp;</strong></p>
</td>
<td width="142" valign="top">
<p><strong>Staff</strong></p>
</td>
<td width="121" valign="top">
<p><strong>Expenses</strong></p>
</td>
<td width="160" valign="top">
<p><strong>Visitors</strong></p>
</td>
<td width="160" valign="top">
<p><strong>Travel</strong></p>
</td>
<td width="160" valign="top">
<p><strong>Vacation</strong></p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>ASP.NET MVC 5</strong></p>
</td>
<td width="142" valign="top">
<p>&nbsp;</p>
</td>
<td width="121" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>SPA Web application</strong></p>
</td>
<td width="142" valign="top">
<p>&nbsp;</p>
</td>
<td width="121" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>Durandal</strong></p>
</td>
<td width="142" valign="top">
<p>&nbsp;</p>
</td>
<td width="121" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>Knockout.js</strong></p>
</td>
<td width="142" valign="top">
<p>&nbsp;</p>
</td>
<td width="121" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>Angular.js</strong></p>
</td>
<td width="142" valign="top">
<p>&nbsp;</p>
</td>
<td width="121" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>ASP.NET Scaffolding</strong></p>
</td>
<td width="142" valign="top">
<p>&nbsp;</p>
</td>
<td width="121" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>Web Mobile View</strong></p>
</td>
<td width="142" valign="top">
<p>&nbsp;</p>
</td>
<td width="121" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>JQuery Mobile</strong></p>
</td>
<td width="142" valign="top">
<p>&nbsp;</p>
</td>
<td width="121" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>Bootstrap</strong></p>
</td>
<td width="142" valign="top">
<p>&nbsp;</p>
</td>
<td width="121" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>LightSwitch Web Application</strong></p>
</td>
<td width="142" valign="top">
<p>X</p>
</td>
<td width="121" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>Office 365 Cloud Business Application</strong></p>
</td>
<td width="142" valign="top">
<p>X</p>
</td>
<td width="121" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>App for SharePoint</strong></p>
</td>
<td width="142" valign="top">
<p>&nbsp;</p>
</td>
<td width="121" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>App for Office 365</strong></p>
</td>
<td width="142" valign="top">
<p>&nbsp;</p>
</td>
<td width="121" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
</tr>
</tbody>
</table>
<p><span style="font-size:medium">Security technologies</span></p>
<table class="GridTable1Light1" border="1" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td width="217" valign="top">
<p><strong>&nbsp;</strong></p>
</td>
<td width="142" valign="top">
<p><strong>Staff</strong></p>
</td>
<td width="121" valign="top">
<p><strong>Expenses</strong></p>
</td>
<td width="160" valign="top">
<p><strong>Visitors</strong></p>
</td>
<td width="160" valign="top">
<p><strong>Travel</strong></p>
</td>
<td width="160" valign="top">
<p><strong>Vacation</strong></p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>Windows Azure Active Directory</strong></p>
</td>
<td width="142" valign="top">
<p>&nbsp;</p>
</td>
<td width="121" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>ASP.NET Identity</strong></p>
</td>
<td width="142" valign="top">
<p>X</p>
</td>
<td width="121" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>SharePoint integrated security</strong></p>
</td>
<td width="142" valign="top">
<p>X</p>
</td>
<td width="121" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
</tr>
</tbody>
</table>
<p><span style="font-size:medium">Windows Apps technologies</span></p>
<table class="GridTable1Light1" border="1" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td width="217" valign="top">
<p><strong>&nbsp;</strong></p>
</td>
<td width="142" valign="top">
<p><strong>Staff</strong></p>
</td>
<td width="121" valign="top">
<p><strong>Expenses</strong></p>
</td>
<td width="160" valign="top">
<p><strong>Visitors</strong></p>
</td>
<td width="160" valign="top">
<p><strong>Travel</strong></p>
</td>
<td width="160" valign="top">
<p><strong>Vacation</strong></p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>Windows 8.1 Store C#/XAML</strong></p>
</td>
<td width="142" valign="top">
<p>&nbsp;</p>
</td>
<td width="121" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>Windows 8.1 Store HTML/JS</strong></p>
</td>
<td width="142" valign="top">
<p>&nbsp;</p>
</td>
<td width="121" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>Windows 8.1 Push notifications</strong></p>
</td>
<td width="142" valign="top">
<p>&nbsp;</p>
</td>
<td width="121" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>NFC communication</strong></p>
</td>
<td width="142" valign="top">
<p>&nbsp;</p>
</td>
<td width="121" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>Windows Phone C#/XAML</strong></p>
</td>
<td width="142" valign="top">
<p>&nbsp;</p>
</td>
<td width="121" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>Windows Phone Push notifications</strong></p>
</td>
<td width="142" valign="top">
<p>&nbsp;</p>
</td>
<td width="121" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>.NET WPF</strong></p>
</td>
<td width="142" valign="top">
<p>&nbsp;</p>
</td>
<td width="121" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
</tr>
</tbody>
</table>
<p>&nbsp;</p>
<p><span style="font-size:medium">Office 365 technologies</span></p>
<table class="GridTable1Light1" border="1" cellspacing="0" cellpadding="0">
<tbody>
<tr>
<td width="217" valign="top">
<p><strong>&nbsp;</strong></p>
</td>
<td width="142" valign="top">
<p><strong>Staff</strong></p>
</td>
<td width="121" valign="top">
<p><strong>Expenses</strong></p>
</td>
<td width="160" valign="top">
<p><strong>Visitors</strong></p>
</td>
<td width="160" valign="top">
<p><strong>Travel</strong></p>
</td>
<td width="160" valign="top">
<p><strong>Vacation</strong></p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>Office 365 Cloud Business Application (aka. LightSwitch)</strong></p>
</td>
<td width="142" valign="top">
<p>X</p>
</td>
<td width="121" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>App for SharePoint &ndash; ASP.NET MVC</strong></p>
</td>
<td width="142" valign="top">
<p>&nbsp;</p>
</td>
<td width="121" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>App for SharePoint &ndash; HTML/JS</strong></p>
</td>
<td width="142" valign="top">
<p>X</p>
</td>
<td width="121" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>App for SharePoint &ndash; BCS</strong></p>
</td>
<td width="142" valign="top">
<p>X</p>
</td>
<td width="121" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
</tr>
<tr>
<td width="217" valign="top">
<p><strong>App for Office 365 &ndash; Mail app</strong></p>
</td>
<td width="142" valign="top">
<p>&nbsp;</p>
</td>
<td width="121" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>&nbsp;</p>
</td>
<td width="160" valign="top">
<p>X</p>
</td>
</tr>
</tbody>
</table>
<p>&nbsp;</p>
<h1><span>Detailed setup and description</span></h1>
<p><em>Download the detailed description and setup documents:</em></p>
<p><strong><em>http://mycompany.blob.core.windows.net/msft-mycompany-demos/MyCompany%20demo%20apps%20-%20Description%20and%20Setup.zip&nbsp;</em></strong></p>
<p>The following are the basic requirements</p>
<p><strong><em>MyCompany business applications requirements:</em></strong></p>
<p><em>&bull;&nbsp;&nbsp;&nbsp; Windows 8.1 RTM (Recommended: Professional SKU or higher)&nbsp;
<br>
&bull;&nbsp;&nbsp;&nbsp; Visual Studio 2013 RTM (Recommended: Ultimate or Premium SKU)<br>
&bull;&nbsp;&nbsp;&nbsp; Windows Azure SDK 2.2 or higher <em>(When deploying to Windows Azure)</em><br>
&bull;&nbsp;&nbsp;&nbsp; Windows Azure subscription (When deploying to Windows Azure)<br>
&bull;&nbsp;&nbsp;&nbsp; Office 365 online account and developer site (Only the SharePoint/Office apps)</em></p>
<p><strong><em>FaceRecognition app requirements:</em></strong></p>
<p><em>&bull;&nbsp;&nbsp;&nbsp; Windows 8.1 RTM (Recommended: Professional SKU or higher)&nbsp;
<br>
&bull;&nbsp;&nbsp;&nbsp; Visual Studio 2013 RTM (Recommended: Ultimate or Premium SKU)<br>
&bull;&nbsp;&nbsp;&nbsp; SQL Server C<em>ompact 4.0 SP1</em><br>
</em></p>
<h1>What these demo applications are NOT about</h1>
<p>These applications are not production systems and are not intended as a guidance for mission-critical applications, as it mostly covers CRUD and Data-Driven scenarios, only. If you need guidance for complex scenarios, we recommend to check guidance material
 from the Microsoft Patterns &amp; Practices group like the &lsquo;CQRS Journey guidance&rsquo;, which shows a reference application with a related functional domain &amp; scope (Events/Conferences platform) but from a different point of view based on design-patterns
 and best architectural practices for complex scenarios, covering approaches like CQRS (Command &amp; Query Responsibility Segregation) &amp; DDD (Domain Driven Design).</p>
<h1>Extended setup info, DemoScripts and related sessions</h1>
<p><a href="http://blogs.msdn.com/b/cesardelatorre/archive/2013/12/12/mycompany-demo-apps-extended-support-info-and.aspx">http://blogs.msdn.com/b/cesardelatorre/archive/2013/12/12/mycompany-demo-apps-extended-support-info-and.aspx&nbsp;</a></p>
<h1>More Information</h1>
<p><em>For more information check Cesar de la Torre Blog, at MSDN blogs: <a href="http://blogs.msdn.com/b/cesardelatorre/archive/2013/12/12/mycompany-demo-apps-extended-support-info-and.aspx">
http://blogs.msdn.com/b/cesardelatorre/archive/2013/12/12/mycompany-demo-apps-extended-support-info-and.aspx</a> &nbsp;&nbsp;</em></p>
<p>&nbsp;</p>
<p><strong><em>[UPDATE - January 2014]</em></strong></p>
<p>Added&nbsp;<strong>two more client apps</strong>&nbsp;to 'MyCompany VISITORS Demo App', the&nbsp;<strong>iPad</strong>&nbsp;and&nbsp;<strong>Android</strong>&nbsp;client applications developed in<strong>C#</strong>&nbsp;with&nbsp;<strong><a href="http://xamarin.com/ios">Xamarin.iOS</a></strong>&nbsp;and&nbsp;<strong><a href="http://xamarin.com/android">Xamarin.Android</a></strong>&nbsp;thanks
 to a partnership we made between Microsoft and Xamarin].</p>
<p>Get it from:&nbsp;<a href="https://github.com/xamarin/MyCompany/">https://github.com/xamarin/MyCompany/</a></p>
<p><em><br>
</em></p>
