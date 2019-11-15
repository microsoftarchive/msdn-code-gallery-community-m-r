# Model View Presenter clean implementation
## Requires
- Visual Studio 2010
## License
- Apache License, Version 2.0
## Technologies
- C#
- ASP.NET
- Inversion of Control / Dependency Injection
- Dependency Injection
- Inversion of Control
- Observer Pattern
## Topics
- Architecture and Design
- Patterns and Practices
- Model View Presenter (MVP)
## Updated
- 02/05/2013
## Description

<h1>Introduction</h1>
<p><em>This is a simple MVP in an ASP.NET application. It separates visual display logic from data procurement logic. in MVP Model-View-Presenter, View has the task of displaying data. Presenter is concerned about getting the relevant data for view. Model is
 a repository in MVP. </em></p>
<h1><span>Building the Sample</span></h1>
<p><em>Visual Studio 2010 required to open solution file available with the code sample.&nbsp;You can alternatively add projects in sample code to an earlier version of Visual Studio solution to&nbsp;make it run in case you don't have VS2010. It is built using
 3.5 .NET framework. Framework 2.0 will also work if you change&nbsp;the sample&nbsp;code accordingly.&nbsp;</em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p>Goal of using MVP pattern is to separate the responsibilities of application so that application code is :-<br>
&bull; Code is testable using&nbsp;test frameworks&nbsp;<br>
&bull; Application is more maintainable <br>
&bull; Application is more extensible</p>
<p>Using MVP you can seperate View from Presenter. The responsibility of View is to display data while Presenter has to prepare data for the View.&nbsp;View will raise requests to serve data and Presenter honors those&nbsp;request.</p>
<p>Presenter and View&nbsp;know each other only through the contract which is implemented&nbsp;by View. View requests are served based on the contract it has implemented. A Presenter can serve multiple Views.&nbsp;&nbsp;&nbsp;</p>
<p>Presenter's talks to the model (Database) through Business API&nbsp;to pull&nbsp;required data as per the requested contract.&nbsp;The&nbsp;Presenter functionality can be tested by&nbsp;mocking up views that raises request and is served by Presenter.</p>
<p>&nbsp;</p>
<p><strong>View</strong>: A view is any form or window that represents UI&nbsp;of the application.</p>
<p><strong>Presenter</strong>: The presenter is an entity that presents the data to the view that is to be shown to the user.</p>
<p><strong>Model</strong>: The model is the actual data that the Presenter will request and gets displayed in the View. The Model is responsible for obtaining the data.</p>
<p><img id="59984" src="http://i1.code.msdn.s-msft.com/model-view-presenter-clean-d33ee8d1/image/file/59984/1/mvp.jpg" alt=""></p>
<p>&nbsp;</p>
<p>The attached sample code contains four projects:-</p>
<p>1. Service</p>
<p>2. Presenter</p>
<p>3. EModel</p>
<p>4. Views</p>
<p>&nbsp;</p>
<p>The Service project provides a dummy implementation of back end. It represents how the backend will provide data to the front end. In MVP it is paying role of Model. Service provides an API GetPersons for a given criteria. This returns a collection of Person
 type.</p>
<p>&nbsp;</p>
<p>The EModel project represents Enterprise model classes. These are light weight objects to be used between the app server and the web server communication. The service serves EModel class and not the business classes which is not shown in the sample.</p>
<p>The business or domain object will have lot more attributes along with behavior while EModel class Person is just a container object and a DTO on service tier would populate it To and From business object.</p>
<p>&nbsp;</p>
<p>Presenter listens to View. It provides initial data required by the view. When view raises a request it is handled by presenter which talks to the service and provides the requested data.</p>
<p><img id="60079" src="http://i1.code.msdn.s-msft.com/model-view-presenter-clean-d33ee8d1/image/file/60079/1/classdiagram1.bmp" alt=""></p>
<p>I hope you find it as easy and understandable implementation.</p>
