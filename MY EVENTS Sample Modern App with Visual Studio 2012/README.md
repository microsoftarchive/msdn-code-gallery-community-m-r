# MY EVENTS Sample Modern App with Visual Studio 2012
## Requires
- Visual Studio 2012
## License
- MS-LPL
## Technologies
- WPF
- XAML
- HTML5
- WinJS
- ASP.NET MVC 4
- Windows 8
- ASP.NET Web API
- Visual Studio 2012
- Entity Framework 5
## Topics
- Continuous services
- Connected devices
## Updated
- 09/21/2012
## Description

<p>&nbsp;</p>
<h1><span>Building the Sample</span></h1>
<p><em>Software required: </em></p>
<p><em>- Visual Studio 2012</em></p>
<p><em>- Windows 8</em></p>
<p><em>- Bing Maps SDK for Metro style apps: <a href="http://visualstudiogallery.msdn.microsoft.com/bb764f67-6b2c-4e14-b2d3-17477ae1eaca">
http://visualstudiogallery.msdn.microsoft.com/bb764f67-6b2c-4e14-b2d3-17477ae1eaca</a></em></p>
<p><em>IMPORTANT: First of all, after downloading the .ZIP file and before unpackaging it, go the the .ZIP file properties and press the 'Unblock' button. Then, unpackage it. If you don't do it, you might get Visual Studio hung&nbsp;when trying to open the
 solution.&nbsp;</em><em>You&nbsp;can also&nbsp;get rid of that long path created by the long .ZIP name.<br>
Other than that, you might get errors because of having a too long path.</em></p>
<p><em>Check that you have the required software dependencies, open the solution, select the Web project as default,&nbsp;and run it. The database will be generated for you using LocalDB and Entity Framework Initializers.
</em></p>
<p><em>For more information about how to how to set it up in your dev machine and even in a&nbsp;full IIS environment, check this blog post from Cesar de la Torre:
<a href="http://blogs.msdn.com/b/cesardelatorre/archive/2012/09/17/setting-the-sample-my-events-modern-application-up-amp-running.aspx">
http://blogs.msdn.com/b/cesardelatorre/archive/2012/09/17/setting-the-sample-my-events-modern-application-up-amp-running.aspx</a></em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><em><em><span style="color:black; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:7.5pt">The main goal of this sample modern application is to provide a sample app where you can see most of the new technologies related to Visual Studio 2012, in a practical
 way. </span></em><em><span style="color:black; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:7.5pt"><br>
</span></em></em><br>
<strong>Sample application context and scenarios</strong><br>
This sample application shows most of the new technologies you can use with Visual Studio 2012 in order to build Modern Apps.<br>
The context is about an events/conferences management system called &ldquo;My Events&rdquo;.
<br>
It is composed by:</p>
<p><br>
<strong>Central Web API Services:</strong><br>
This is the core of the system and these services are consumed by all the client apps.</p>
<p><br>
<strong>Global Web-App (ASP.NET MVC4):</strong><br>
For potential attendees so they can browse and register to any published event, but also for events' owners, so anyone can create their own events with all the required data (description, tracks, sessions, speakers, etc.).</p>
<p><img id="66036" src="http://i1.code.msdn.s-msft.com/my-events-modern-sample-9a42abc6/image/file/66036/1/web%20app.png" alt="" width="611" height="337"><br>
<strong>&nbsp;</strong></p>
<p><strong>Organizers WPF App (WPF):</strong><br>
This is a data entry app which is oriented to organizers, especially when they are creating or updating events or even checking global statistics (simplified dashboard).<br>
<strong>&nbsp;<img id="66038" src="http://i1.code.msdn.s-msft.com/my-events-modern-sample-9a42abc6/image/file/66038/1/wpf%20app.png" alt="" width="609" height="403"></strong></p>
<p><strong>Organizers Windows 8 Store App (XAML/C#):</strong><br>
This app is also especially made for events' organizers, but in this case it should be used 'on the go' using a Windows 8 tablet, while running the events. Using this client app the organizers can check comments/feedback coming from attendees, registered people
 to every sessions, etc.</p>
<p>Windows 8 Store App (XAML/C#)</p>
<p><img id="66040" src="http://i1.code.msdn.s-msft.com/my-events-modern-sample-9a42abc6/image/file/66040/1/organizers_xaml_1.png" alt="" width="611" height="316"></p>
<p><img id="66041" src="http://i1.code.msdn.s-msft.com/my-events-modern-sample-9a42abc6/image/file/66041/1/organizers_xaml_2.png" alt="" width="602" height="358"><br>
<strong>&nbsp;</strong></p>
<p><strong>Attendees Windows 8 Store App (HTML5/WinJS):</strong><br>
This app is especially made for attendees and it should be used 'on the go' using a Windows 8 tablet, while attending the events and when jumping from one session to another. This client app is very convenient as the attendess can see the live agenda and schedule
 while checking what friends from FACEBOOK are attending to each session. They can also see the assigned room for each session, manually written notes assigned to some sessions and other information related to the events.</p>
<p><img id="66042" src="http://i1.code.msdn.s-msft.com/my-events-modern-sample-9a42abc6/image/file/66042/1/attendees_html5_1.png" alt="" width="602" height="360"></p>
<p><strong><img id="66043" src="http://i1.code.msdn.s-msft.com/my-events-modern-sample-9a42abc6/image/file/66043/1/attendees_html5_2.png" alt="" width="604" height="383"></strong></p>
<p><strong>Austin/Journal App (manually written notes app) - C&#43;&#43; Windows Store App</strong><br>
Although this other sample app is not really part of 'MY EVENTS', you can also use it to create manually written notes using a Tablet, and then
<strong>share those notes through Windows 8 and assign any note to any specific conference session, so you can see it in the future</strong>.<br>
<span style="text-decoration:underline">You&nbsp;can download this app (and source code, from CODEPLEX)&nbsp;from a different URL, published by the Microsoft Visual C&#43;&#43; team:
<a href="http://blogs.msdn.com/b/vcblog/archive/2012/09/11/10348466.aspx?wa=wsignin1.0">
http://blogs.msdn.com/b/vcblog/archive/2012/09/11/10348466.aspx?wa=wsignin1.0</a></span></p>
<p><img id="66044" src="http://i1.code.msdn.s-msft.com/my-events-modern-sample-9a42abc6/image/file/66044/1/austin_3.png" alt="" width="624" height="350"></p>
<p>&nbsp;</p>
<p><strong>Virtual Events Application (Avatar &amp; KINECT for WINDOWS app)</strong><br>
This is a separated download URL, but related to the same apps' context and demos. This is the download URL for this other app:
<a href="http://code.msdn.microsoft.com/Virtual-Events-Sample-a0228a97">http://code.msdn.microsoft.com/Virtual-Events-Sample-a0228a97</a></p>
<p>Windows 8 Store app (currently showed in a tablet):</p>
<p><img id="66045" src="http://i1.code.msdn.s-msft.com/my-events-modern-sample-9a42abc6/image/file/66045/1/3%20-%20virtual%20events%20-%20client%20with%20event%20description%20speaker%20photo%20avatar%20and%20scenario.png" alt="" width="603" height="362"></p>
<p><strong>&nbsp;</strong></p>
<p><strong>Goals</strong><br>
This global system and the related client apps are highlighting the following points:<br>
- Consumer end-user point of view with the latest UX technologies capabilities, typical of Modern-Apps.<br>
- Enterprise and LOB (Line of Business) point of view, when using a desktop application.<br>
- Cloud friendly. You can deploy very easily all the server assets (Web Api Services and Web app) into the Windows Azure cloud.<br>
- Extensible and open to more complex scenarios. Even though our scenario&rsquo;s main goal is to &lsquo;keep it simple&rsquo;, we wanted to be able to leave it open for future extensions and complexity that is usually required when implementing a real production
 application. <br>
- Multiple technical approaches. We wanted to illustrate how multiple technical approaches can co-exist within the same solution (Web, mainstream-app, CRUD, etc.)
<br>
- Easily deployable. The RI is easily deployable so that you can install it and experiment with it.</p>
<p><em><em><span style="color:black; font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;; font-size:7.5pt"><em><span style="font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;">The selected technologies and frameworks used are the following:</span></em><br>
<br>
<em><span style="font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;">-Core</span></em><br>
<br>
<em><span style="font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;">&nbsp;&nbsp; * ASP.NET Web-API Services</span></em><br>
<br>
<em><span style="font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;">&nbsp;&nbsp; * Entity Framework 5 (Code First)</span></em><br>
<br>
<em><span style="font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;">&nbsp;&nbsp; * SQL Server LocalDB</span></em><br>
<br>
<em><span style="font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;">-Web App</span></em><br>
<br>
<em><span style="font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;">&nbsp; * ASP.NET MVC 4</span></em><br>
<br>
<em><span style="font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;">&nbsp; * JavaScript &amp; </span>
</em><em><span style="font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;">JQuery consuming Web Api Services</span></em><br>
<br>
<em><span style="font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;">-Windows 8 Store Apps</span></em><br>
<br>
<em><span style="font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;">&nbsp;&nbsp; * Windows 8 Store App using HTML5/WinJS</span></em><br>
<br>
<em><span style="font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;">&nbsp;&nbsp; * Windows 8&nbsp;Store Apps using XAML/C#</span></em><br>
<br>
<em><span style="font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;">-Desktop App</span></em><br>
<br>
<em><span style="font-family:&quot;Verdana&quot;,&quot;sans-serif&quot;">&nbsp;&nbsp; * WPF (Windows Presentation Foundation) App</span></em></span></em></em></p>
<p><br>
<strong>What this sample application is&nbsp;NOT about</strong><br>
This is not a production application and it is not intended as a guidance for complex scenarios/applications, as it mostly covers CRUD and Data-Driven scenarios, only. If you need guidance for complex scenarios, we recommend to check guidance material from
 the Microsoft Patterns &amp; Practices group like the &lsquo;CQRS Journey guidance&rsquo;, which shows a reference application with a&nbsp;related functional domain &amp; scope (Events/Conferences platform) but from a different point of view based on design-patterns
 and best architectural practices for complex scenarios, covering approaches like CQRS (Command &amp; Query Responsibility Segregation) &amp; DDD (Domain Driven Design).</p>
<p><strong>Wrap up</strong><br>
Therefore and as previously mentioned, our main objective is to highlight and show most of the new capabilities and technical possibilities offered by Visual Studio 2012 and all its related technologies in order to build
<strong>Modern Apps</strong>.</p>
<p>&nbsp;</p>
