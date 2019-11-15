# MyShuttle demo applications (Visual Studio 2015 RTM - ASP.NET 5)
## Requires
- Visual Studio 2015
## License
- MIT
## Technologies
- Microsoft Azure
- Windows Store app
- universal windows app
- Apache Cordova
- Xamarin.Android
- Xamarin.iOS
- ASP.NET 5
- Visual Studio 2015
- .NET 2015
- .NET Core 5
- .NET Framework 4.6
- Xamarin
- Windows 10
- Universal Windows Platform
## Topics
- New technologies in Visual Studio 2015
## Updated
- 07/29/2015
## Description

<h1><img id="137532" src="https://i1.code.msdn.s-msft.com/windowsapps/myshuttle-demo-applications-1a4b68fe/image/file/137532/1/visual%20studio.png" alt="" width="224" height="63" style="font-size:2em"></h1>
<h1><img id="130606" src="https://i1.code.msdn.s-msft.com/myshuttle-demo-applications-1a4b68fe/image/file/130606/1/myshuttlebiz_logo.png" alt="" width="354" height="82">&nbsp;</h1>
<p><span style="font-size:small; background-color:#ffff00">[**NEW!! - Updated on&nbsp;July 29th 2015 -
<strong>Compatible with Visual Studio&nbsp;2015&nbsp;RTM</strong> **]</span></p>
<h1>Introduction</h1>
<p>The <strong>MYSHUTTLE.biz</strong> sample <strong>applications </strong>are implemented with the new
<strong>Visual Studio 2015</strong>&nbsp;and <strong>.NET 2015</strong>. These demo apps&nbsp;were part of the Visual Studio 2015 launch event&nbsp;and have been updated to new features and versions during 2015, like Windows 10 UWP app, migration from Azure
 Mobile Services to Azure Mobile App, Azure AD Authentication from Cordova app, ASP.NET5 running on .NET Core 5 on Linux and Mac, plus other minor updates.</p>
<h1>Setting&nbsp;up and demos' deep dive blog posts&nbsp;</h1>
<p><em>These sample applications are not&nbsp;extremely complex but&nbsp;take into account that&nbsp;this is not&nbsp;just about a simple example or snippet. These apps&nbsp;are built by using&nbsp;most of&nbsp;the new technologies coming in
<strong>Visual Studio 2015 </strong>and <strong>.NET 2015</strong>, so there are many different projects for each app (more than 16 VS projects) whithin several scenarios, covering web, Services and mobile apps for any device (iOS, Android, Windows Phone and
 Windows Store).</em></p>
<h2>Add Nuget v2 source to Visual Studio 2015 RTM</h2>
<p>Add <a href="https://www.nuget.org/api/v2/">https://www.nuget.org/api/v2/</a>&nbsp;as an aditional Nuget source in Visual Studio as it is needed for MvvmCross nuget packages dependencies.</p>
<p>&nbsp;</p>
<h2>Issues with Cordova app and dependencies</h2>
<p><br>
The Cordova app depends on node packages, js frameworks installed using bower and gulp to complete some javascript tasks.</p>
<p>If the dependencies are not installed, the app will show a blank screen after showing the splash screen.</p>
<p>Manual steps to restore the dependencies.<br>
To restore the dependencies manually you have to configure your environment from a console CMD, this is needed only the first time:<br>
<strong>&bull;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; npm install bower &ndash;g</strong><br>
<strong>&bull;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; npm install gulp &ndash;g</strong><br>
<strong>&bull;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; npm install karma-cli &ndash;g</strong></p>
<p>After having this tools installed you&nbsp;need to&nbsp;install the dependencies with these two command lines: (you must be&nbsp;positioned within&nbsp;the project directory src\MyShuttle.Client.Cordova)
<br>
<strong>&bull;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; npm install</strong><br>
<strong>&bull;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; gulp beforeBuild</strong></p>
<p>After completing these steps, check that these directories exits:<br>
&bull;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; src\MyShuttle.Client.Cordova\bower_components<br>
&bull;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; rc\MyShuttle.Client.Cordova\node_modules<br>
&bull;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; src\MyShuttle.Client.Cordova\www\vendors</p>
<p>When using Visual Studio 2015, all those previous steps should be needed as after opening the solution with Visual Studio, it should detect&nbsp;the dependencies and automatically starts to install them.</p>
<p><br>
After installing the dependencies, build the solution.&nbsp; Visual Studio runs the gulp task automatically
<br>
After completing these steps, check that these directories exits:<br>
&bull;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; src\MyShuttle.Client.Cordova\bower_components<br>
&bull;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; rc\MyShuttle.Client.Cordova\node_modules<br>
&bull;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; src\MyShuttle.Client.Cordova\www\vendors</p>
<p>Even though Visual Studio should get the dependencies, our experience is that sometimes doesn&acute;t restore the packages&hellip;.the output window shows error restoring the dependencies. We're researching if this issue is in the Tooling or in the sample
 application.</p>
<h1>Download and Unblock the .ZIP file first!</h1>
<p>Once you download the .ZIP file with the code, and BEFORE unzipping, go to the&nbsp;<strong>.ZIP file properties</strong>&nbsp;from&nbsp;<strong>Windows Explorer</strong>&nbsp;and check if it is blocked. If it blocked because Windows detected it was a download
 from the Internet, unblock it before unzipping the files, like in the following image:</p>
<p><img id="130628" src="https://i1.code.msdn.s-msft.com/myshuttle-demo-applications-1a4b68fe/image/file/130628/1/unblock_zip.png" alt="" width="250" height="300"></p>
<p>If you miss this step, you might get warnings and errors when loading the projects in Visual Studio and once you unpackage the code and projects files, if you didn't unblock the .ZIP file, unblocking each of the internal files is a much harder task to do.</p>
<h2><em>Further information<br>
</em></h2>
<p><em>Check the following blog posts in order to&nbsp;get more info&nbsp;about each scenario (NOTE: The blog posts are still not updated to the Visual Studio 2015 version):</em></p>
<p><a href="http://blogs.msdn.com/b/cesardelatorre/archive/2014/11/30/myshuttle-biz-demo-apps-from-connect-visual-studio-and-azure-event.aspx"><em>&nbsp;</em><em><strong>1. Blog Post: Global Intro of the MyShuttle.biz demo apps from Connect(); Visual Studio
 and Azure event</strong></em></a></p>
<p><a href="http://blogs.msdn.com/b/cesardelatorre/archive/2014/11/21/myshuttle-biz-asp-net-5-demos.aspx"><em><strong>&nbsp;</strong></em><em><strong>2. Blog Post: MyShuttle.biz ASP.NET 5&nbsp;Web Apps and Services</strong></em></a></p>
<p><a href="http://blogs.msdn.com/b/cesardelatorre/archive/2014/11/30/myshuttle-biz-and-cross-platform-mobile-development-of-native-apps-for-ios-android-and-windows-with-net-and-xamarin.aspx"><em><strong>&nbsp;</strong></em><em><strong>3. Blog Post: MyShuttle.biz
 and Cross-platform Mobile Development of native apps for iOS, Android and Windows with .NET and Xamarin</strong></em></a></p>
<p><a href="http://blogs.msdn.com/b/cesardelatorre/archive/2014/11/30/myshuttle-biz-and-multi-device-development-of-hybrid-apps-for-ios-android-and-windows-with-visual-studio-tools-for-apache-cordova.aspx"><em><strong>&nbsp;</strong></em><em><strong>4.&nbsp;Blog
 Post:&nbsp;MyShuttle.biz and multi device development of hybrid apps for iOS, Android and Windows with Visual Studio Tools for Apache Cordova</strong></em></a></p>
<p><a href="http://blogs.msdn.com/b/cesardelatorre/archive/2014/11/30/myshuttle-biz-and-multi-device-development-of-hybrid-apps-for-ios-android-and-windows-with-visual-studio-tools-for-apache-cordova.aspx"><em><strong>&nbsp;</strong></em></a><em><strong><a href="http://blogs.msdn.com/b/cesardelatorre/archive/2014/12/10/myshuttle-biz-azure-backend-services-and-lob-integration-to-o365-and-salesforce.aspx">5.
 Blog post:&nbsp;MyShuttle.biz backend integration with LOB services (Invoicing, Azure WebJob, Azure queues, O365 API and SalesForce API)&nbsp;</a></strong></em></p>
<p><span style="font-size:20px; font-weight:bold">The technologies</span></p>
<h2>Web applications</h2>
<blockquote>
<p>- <strong>ASP.NET 5 MVC</strong> site&nbsp;</p>
<p>- <strong>SPA</strong> (Single Page Application) using <strong>Angular.js</strong>,
<strong>jQuery</strong>, <strong>bootstrap</strong> and consuming our <strong>ASP.NET 5 Web API
</strong>Services&nbsp;</p>
<p>- Support for <strong>Bower</strong>, <strong>NPM</strong> and <strong>Grunt </strong>
in VS 2015 Tooling</p>
</blockquote>
<h2>Services, Azure, middleware and LOB integration</h2>
<blockquote>
<p>- <strong>ASP.NET 5 Web API</strong> services (Web API in codename &ldquo;<strong>Project K</strong>&rdquo;)</p>
<p>- <strong>ASP.NET 5 SignalR</strong></p>
<p>- Azure <strong>Web Sites</strong></p>
<p>- Azure <strong>Mobile Service</strong> (Web API)</p>
<p>- Azure <strong>Web Job</strong></p>
<p>- Azure<strong> Push Notifications Hub</strong></p>
<p>- <strong>Entity Framework 7</strong></p>
<p><strong>- Connected Services: O365 API </strong>and <strong>SalesForce API</strong></p>
</blockquote>
<h2>Cross-platform Mobile apps</h2>
<blockquote>
<p>- <strong>Universal Windows apps</strong> in <strong>C#</strong> (<strong>Windows Phone</strong> app and
<strong>Windows Store</strong> app)</p>
<p>- <strong>Xamarin</strong> C# apps (<strong>iOS</strong> app and <strong>Android</strong> app)</p>
<p>- <strong>Cordova</strong> HTML/JS <strong>Hybrid</strong> app (for <strong>Android</strong>,
<strong>iOS</strong> and <strong>Windows Phone</strong>)<em>&nbsp;&nbsp;</em></p>
</blockquote>
<p>&nbsp;</p>
<h1><span>The Business Domain</span></h1>
<ul>
</ul>
<p><em>MyShuttle is a B2B highly scalable multitenant SaaS solution that targets corporate scenarios in which carrier companies offer transport services to enterprise customers.</em></p>
<p>So this multi-tenant SaaS system would allow any number of carrier companies who must be syndicated with the system, to provide their services (cabs/shuttles) directly to any number of customers enterprises/companies who would also be registered in the MyShuttle.biz
 system. The final outcome? any employee in those customer companies would be able to request a cab/shuttle at any time in any place/city without worrying about how to pay. Everything would take place underneath between their company and the carrier company
 for that ride. That&rsquo;s kind of the idea for the business domain.</p>
<p>In any case, the business domain is just a channel we need in order to explain the different Microsoft development technologies provided under
<strong>Visual Studio 2015</strong> and <strong>.NET 2015</strong>. The theme or business domain could have been any other business domain. We just wanted a familiar and easy to follow domain.</p>
<p>You can see a live intro to the domain and the apps working together (push notifications between the apps, etc.) in
<a href="http://channel9.msdn.com/Events/Visual-Studio/Connect-event-2014/011">ScottGu&rsquo;s kenote with Nicole&rsquo;s demos at Connect(); event</a> (<strong>MINUTE 12:00 of that keynote</strong>).</p>
<p><em><em><br>
</em></em></p>
<ul>
</ul>
<h1>The scenario: Mobile first and Cloud first&nbsp;</h1>
<p>The global scenario is described in the following image:</p>
<p><img id="130607" src="https://i1.code.msdn.s-msft.com/myshuttle-demo-applications-1a4b68fe/image/file/130607/1/mobilefirst_cloudfirst_scenario_small.jpg" alt="" width="733" height="367">In this diagram we have quite a few scenarios, but all are inter-connected
 between them thru Services in the cloud. We have web apps implemented using the brand new
<strong>ASP.NET 5 (&ldquo;Project K&rdquo;)</strong>, either <strong>MVC</strong>,
<strong>Web API Services</strong> and even <strong>SignalR </strong>real-time communication services. Then we also have native mobile apps for the customers (based on C#, either
<strong>.NET</strong> as <strong>Universal Windows apps</strong> or <strong>Xamarin</strong> native apps for
<strong>iOS</strong> and <strong>Android</strong>). We also have a simpler mobile hybrid app implemented with
<strong>Apache Cordova</strong> (HTML/JS) and using the <strong>Visual Studio 2015 tooling for Cordova</strong>, a
<strong>WPF</strong> desktop app and finally we&rsquo;re integrating our processes (invoices) with
<strong>LOB</strong> systems, thru <strong>VS Connected Services</strong>, like using
<strong>O365 API</strong> and using <em>SharePoint libraries as repository of .PDF invoices</em> or using
<strong>Salesforce REST API</strong> to upload customer&rsquo;s data into the SalesForce CRM.</p>
<p>Below you can check a few screenshots of the apps, but if you want to have a better introduction to all the apps and scenarios, make sure you check this blog post. TBD *******</p>
<h2>Public Web Site &ndash; ASP.NET 5 MVC</h2>
<p><img id="130608" src="https://i1.code.msdn.s-msft.com/myshuttle-demo-applications-1a4b68fe/image/file/130608/1/01_aspnet5_mvc.png" alt="" width="500" height="450"></p>
<h2><em>SPA web application consuming ASP.NET 5 Web API Services</em></h2>
<p><img id="130609" src="https://i1.code.msdn.s-msft.com/myshuttle-demo-applications-1a4b68fe/image/file/130609/1/01_aspnet5_spa.png" alt="" width="480" height="450"></p>
<h2>Native Mobile apps for Windows Store, Windows Phone, iOS and Android</h2>
<h3>With C# cross-platform development powered by .NET and Xamarin</h3>
<p><img id="130610" src="https://i1.code.msdn.s-msft.com/myshuttle-demo-applications-1a4b68fe/image/file/130610/1/01_mobile_apps.png" alt="" width="587" height="625"></p>
<p>&nbsp;</p>
<p>For deeper details and explanations of the new technologies coming in Visual Studio 2015, .NET 2015 and the MyShuttle apps themselves, please, check the blog posts mentioned at the begining of this page.</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p><em><br>
</em></p>
<div class="mcePaste" id="_mcePaste" style="left:-10000px; top:0px; width:1px; height:1px; overflow:hidden">
</div>
