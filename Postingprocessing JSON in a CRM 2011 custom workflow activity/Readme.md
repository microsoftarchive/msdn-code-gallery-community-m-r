# Posting/processing JSON in a CRM 2011 custom workflow activity
## Requires
- Visual Studio 2012
## License
- Apache License, Version 2.0
## Technologies
- C#
- JSON
- Dynamics CRM
- Microsoft Dynamics CRM 2011
## Topics
- JSON
- REST
- Web Services
- Microsoft Dynamics CRM SDK
## Updated
- 06/24/2013
## Description

<h1>Introduction</h1>
<p>With the proliferation of RESTful APIs, JSON is frequently used as a message format for interoperability. Though perphaps not as familiar to C# developers as XML, JSON is easy to serialize/deserialize in C#, and it is easily human-readable. This sample shows
 how to POST JSON messages to a REST endpoint and process the response in a Dynamics CRM custom workflow activity.</p>
<p>This sample assumes there is a REST API to which you will post a JSON message containing two input parameters and will respond with two output parameters that are then surfaced to the calling workflow in Dynamics CRM. It is based on a blog post I wrote called&nbsp;<a href="http://alexanderdevelopment.net/post/2013/04/21/Sending-SMS-Messages-and-Making-Robocalls-from-Dynamics-CRM.aspx">Sending
 SMS Messages and Making Robocalls from Dynamics CRM</a>. In that post I showed how to integrate Dynamics CRM with Tropo using JSON, and I wanted to show a more general approach.</p>
<h1><span>Building the Sample</span></h1>
<p><span>Visual Studio 2012/.Net 4.5 is required as is the Microsoft Dynamics CRM SDK. No other non-System assemblies are required.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p>This sample shows how to do the following:</p>
<ol>
<li>Take input parameters from a calling workflow </li><li>Instantiate a custom object to represent the request to the REST endpoint </li><li>Serialize the object to JSON </li><li>Post the JSON to the REST endpoint </li><li>Deserialie the response to a custom object </li><li>Return output parameters to the calling workflow </li></ol>
<h1><span>Source Code Files</span></h1>
<p>The sample consists of two classes:</p>
<ol>
<li>DoSomething.cs (contains the custom workflow activity logic) </li><li>JsonRequestResponse.cs (contains the classes for the objects from which/to which JSON will be serialized/deserialized)
</li></ol>
