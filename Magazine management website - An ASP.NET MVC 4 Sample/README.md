# Magazine management website - An ASP.NET MVC 4 Sample
## Requires
- Visual Studio 2012
## License
- MS-LPL
## Technologies
- ADO.NET Entity Framework
- ASP.NET
- Javascript
- Entity Framework
- ASP.NET MVC 4
- autofac
- automapper
- Visual Studio 2012
## Topics
- ADO.NET Entity Framework
- ASP.NET
- ASP.NET and ADO.NET Entity Framework
- ASP.NET MVC
- Javascript
- Entity Framework
- ASP.NET Web API
## Updated
- 10/11/2017
## Description

<h1>Introduction</h1>
<p><em>As we know, ASP.NET MVC 4 came out. And now everyone is very excited to use it. I have read
<a href="http://www.asp.net/whitepapers/mvc4-release-notes">a release notes</a> from ASP.NET MVC 4 website and found out some cool stuff there. At the 4th version, M$ introduced many features, such as Http programming model, routing, model binding, validation,
 Query composite, IoC container, self-host for Web API, Bundle and minification, Mobile, asynchronous programming,... I really love the improving for the Razor view engine (it called automatically removing null value when some values in view model got null,
 and this features actually make us avoid a lot of mistakes in programming time), Web API cannot be believable (hope it will be a standard web soon). Moving from Microsoft's JSON to the Newtonsoft.JSON.4.5.1 third-party library is the big changes as well.</em></p>
<h1><span>Building the Sample</span></h1>
<p><em>I am trying to using Visual Studio 2012 for building up a Magazine website that including a front page and an admin page. It is the actually small example to visualize new technologies that I have learned, so I try to make it as simple as possible.</em></p>
<p><em>I used EF 5.0 Beta in this sample and got the sample from my former team leader for building the
<a href="http://huyrua.wordpress.com/2010/07/13/entity-framework-4-poco-repository-and-specification-pattern/">
EF persistence layer</a>. Thanks, Mr. <a href="http://huyrua.wordpress.com">Huy Nguyen</a> for that great
<a href="http://code.google.com/p/ef4prs/downloads/list">EF framework</a>&nbsp;sample. To adapting with my code,&nbsp;I also have to edit some code in this framework to matching with new EF 5.0 release.</em></p>
<p><em>For the layout, I used some templates for the&nbsp;<a href="http://www.template4all.com/">front-end</a>&nbsp;page and the&nbsp;<a href="http://chocotemplates.com/">admin page</a> from the internet. Thanks for your great templates :)</em></p>
<h1>Upgrade</h1>
<p><span style="color:#000000; background-color:#ffffff">I am working on upgrading this example. The next version of it will use
<strong>Visual Studio 2013</strong> RTM, <strong>ASP.NET MVC 5</strong>, <strong>
Web API 2</strong> and <strong>AngularJS</strong>)</span></p>
<p><span style="background-color:#ffffff; color:#000000">The <strong>source code</strong> can find out at&nbsp;<a href="https://github.com/thangchung/magazine-website-mvc-5">https://github.com/thangchung/magazine-website-mvc-5</a>&nbsp;and&nbsp;https://github.com/thangchung/magazine-website&nbsp;</span></p>
<p><span style="background-color:#ffffff; color:#000000">The <strong>new admin template</strong>&nbsp;using
<strong>Bootstrap 3</strong> can be found at&nbsp;<a href="http://saka-webstack.github.io/templates/new-admin-layout/index.html">http://saka-webstack.github.io/templates/new-admin-layout/index.html</a></span></p>
<p><strong><span style="background-color:#ffffff; color:#000000">Front-end page</span></strong></p>
<p><span style="background-color:#ffff00; color:#ff0000"><img id="100502" src="100502-mvc5_1.png" alt="" width="500" height="400"><br>
</span></p>
<p>&nbsp;</p>
<p><span style="font-size:2em">Demo site</span></p>
<p>The newest source code will come to the MSDN code very soon. At the moment, I just want to introduce the demo pages for it as below</p>
<p><span style="color:#ff0000"><strong>The front-end page:</strong></span>&nbsp;<a href="http://magazinewebsitelite.apphb.com/">http://magazinewebsitelite.apphb.com</a></p>
<h1>Description</h1>
<p><em>We have the front page like this</em></p>
<p><img id="60214" src="60214-magazinewebsite-index%20page-180208.png" alt="" width="1287" height="1511"></p>
<p>&nbsp;And the admin page as below</p>
<p><img id="60215" src="60215-magazinewebsiteadmin--180259.png" alt="" width="1304" height="680"></p>
<p><strong><span style="font-size:medium">How to run the web application:</span></strong></p>
<p>Step 1: Get a source code from the MSDN code</p>
<p>Step 2: Run a database script, the script can be found at C#\1.DatabaseScripts\3.NewsDbScript.sql</p>
<p>Step 3: Host a CIK.News.Web solution to an IIS host</p>
<p>Step 4: Press F5 to building all solutions</p>
<p>Step 5: Changing a connection string to your own database server.</p>
<p>Step 6: Running it</p>
<p><strong><span style="font-size:medium">Structure of solutions:</span></strong></p>
<p><img id="71207" src="71207-sample01.png" alt="" width="239" height="346"></p>
<p><strong><span style="font-size:large">History:</span></strong></p>
<p><span style="color:#ff0000">1.0:</span> finished the draft version</p>
<p><span style="color:#ff0000">1.1:</span> removed some redundant code that commented from the community. Fixed bug, and removed the complex security model and changed it to the simple one. Re-structure the architecture of the application</p>
<p><span style="color:#ff0000">1.2:</span> edited some contents in this site</p>
