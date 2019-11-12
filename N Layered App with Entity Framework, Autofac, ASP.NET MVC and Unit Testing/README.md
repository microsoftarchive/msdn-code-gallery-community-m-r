# N Layered App with Entity Framework, Autofac, ASP.NET MVC and Unit Testing
## Requires
- Visual Studio 2012
## License
- MIT
## Technologies
- C#
- SQL Server
- autofac
- ASP.NET MVC 5
- Entity Framework 6
## Topics
- Architecture and Design
- ASP.NET MVC
- Unit Testing
- Web Architecture
- Dependancy Injection
## Updated
- 11/29/2014
## Description

<h1>Introduction</h1>
<p><em><em>This is source code of following tutorial</em></em></p>
<h2><a title="Permanent Link to N-Layered App with Entity Framework, Autofac, ASP.NET MVC and Unit Testing" rel="bookmark" href="http://techbrij.com/service-layer-entity-framework-asp-net-mvc-unit-testing">N-Layered App with Entity Framework, Autofac, ASP.NET
 MVC and Unit Testing</a></h2>
<h1><span>Building the Sample</span></h1>
<p><em>1. .NET Framework 4.5<br>
2. ASP.NET MVC 5.1<br>
3. Entity Framework 6.1.1<br>
4. Sql Server<br>
5. Autofac 3.4<br>
6. Moq<br>
7. Visual Studio 2012</em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><span style="font-size:small; font-weight:bold">Implementing a decoupled, unit-testable, N tier architecture with
<a href="http://techbrij.com/tag/entity-framework" target="_blank">Entity Framework</a>, IoC Container and Dependency Injection in
<a href="http://techbrij.com/category/dev/asp-net-mvc" target="_blank">ASP.NET MVC</a>. (WITHOUT Repository/Unit of Work pattern on top of Entity Framework to avoid redundancy). A simple and clean implementation.<br>
</span></p>
<p><span style="font-size:small"><strong>The repository &amp; UnitOfWork are already implemented in entity framework so no need to implement it on top of EF. This is updated sample of the following code after removing repository layer:</strong></span></p>
<p><span style="font-size:small"><strong><a href="https://code.msdn.microsoft.com/Generic-Repository-Entity-b6b980f0/" target="_blank">Generic Repository, Entity Framework, Unit Testing, Autofac and ASP.NET MVC</a></strong></span></p>
<p>&nbsp;</p>
<h1><span>Source Code Files</span></h1>
<ul>
</ul>
<p><strong>SampleArch.Model : It has model and context related files<br>
SampleArch.Service: It has service related files<br>
<strong>SampleArch : It is ASP.NET MVC project<br>
<strong>SampleArch.Test : It is test project<br>
</strong></strong></strong></p>
<p>&nbsp;</p>
<h1>Layers</h1>
<p>1. Database (to manage data)<br>
2. Data Access Layer (contains EF LINQ queries, models and datacontext)<br>
3. Service Layer (Business and domain Logic)<br>
4. MVC Web Layer (UI part which talks to service layer only)<br>
5. Test Layer (for unit testing&hellip;etc)</p>
<ul>
</ul>
<h1>More Information</h1>
<p><em><em>For more information, see following tutorial:</em></em></p>
<h2><a title="Permanent Link to N-Layered App with Entity Framework, Autofac, ASP.NET MVC and Unit Testing" rel="bookmark" href="http://techbrij.com/service-layer-entity-framework-asp-net-mvc-unit-testing">N-Layered App with Entity Framework, Autofac, ASP.NET
 MVC and Unit Testing</a></h2>
<p><em><em><br>
</em></em></p>
