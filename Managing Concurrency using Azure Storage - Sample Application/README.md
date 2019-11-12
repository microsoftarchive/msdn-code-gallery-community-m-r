# Managing Concurrency using Azure Storage - Sample Application
## Requires
- Visual Studio 2013
## License
- Apache License, Version 2.0
## Technologies
- Windows Azure Storage
- Windows Azure Storage Blobs
- Windows Azure Storage Tables
- Microsoft Azure Storage
## Topics
- C#
- Azure
- Microsoft Azure
- Azure Table Storage
- Code Sample
- Table Storage
- Windows Azure Application
- Azure Blob Storage
- Storage
- Windows Azure Web Sites
- data and storage
- Microsoft Azure Storage
- Windows Azure Storage Client Library
## Updated
- 08/20/2014
## Description

<h1>Introduction</h1>
<p><em>Modern Internet based applications usually have multiple users viewing and updating data simulataneously. This requires application developers to think carefully about how to provide a predictable experience to their end users, particularly for scenarios
 where multiple users can update the same data. The Azure Storage Service includes support for optimistic, pessimistic and last-writer-wins concurrency strategies. This sample application developed to accompany the Azure blog post demonstrates these capabilities.
</em></p>
<h1><span>Building the Sample</span></h1>
<p><em>1. Unzip the zip file </em></p>
<p><em>2. Open the solution in Visual Studio</em></p>
<p><em>3. Modify the app.config file to include your storage account and access key</em><em><br>
</em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><em>&nbsp;</em><em>The sample demonstrates optimistic and pessimistic locking strategies with blob storage. It also simulates a third party modifying the backend data to force concurrency execeptions.
</em></p>
<p><em>The sample also demonstrates how optimistic concurrency is supported using Table storage - as well as how to change the default behavior from optimistic concurrency to last-writer wins.
</em></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__com">//&nbsp;Try&nbsp;to&nbsp;update&nbsp;the&nbsp;blob&nbsp;using&nbsp;the&nbsp;ETag&nbsp;provided&nbsp;</span>&nbsp;
<span class="cs__keyword">try</span>&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Trying&nbsp;to&nbsp;update&nbsp;blob&nbsp;using&nbsp;orignal&nbsp;etag&nbsp;to&nbsp;generate&nbsp;if-match&nbsp;access&nbsp;condition&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;blockBlob.UploadText(helloText,&nbsp;accessCondition:AccessCondition.GenerateIfMatchCondition(orignalETag));&nbsp;
}&nbsp;
<span class="cs__keyword">catch</span>&nbsp;(StorageException&nbsp;ex)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(ex.RequestInformation.HttpStatusCode&nbsp;==&nbsp;(<span class="cs__keyword">int</span>)HttpStatusCode.PreconditionFailed)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Precondition&nbsp;failure&nbsp;as&nbsp;expected.&nbsp;Blob's&nbsp;orignal&nbsp;etag&nbsp;no&nbsp;longer&nbsp;matches&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">throw</span>;&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em>blobsample.cs - blob storage sample code</em> </li><li><em><em>tablesample.cs - table storage sample code</em></em> </li></ul>
<h1>More Information</h1>
<p><em>For more information on Azure Storage see:</em></p>
<ul>
<li><em>Managing Concurrency using Azure Storage</em> </li><li><em>Microsoft Azure Storage Home Page<br>
</em></li><li><em>Introduction to Azure Storage <br>
</em></li><li><em>Storage Getting Started for Blob, Table and Queues <br>
</em></li><li><em>Storage Architecture &ndash; Windows Azure Storage : A Highly Available Cloud Storage Service with Strong Consistency<br>
<br>
</em></li></ul>
<p>&nbsp;</p>
<p><em><br>
</em></p>
<div id="_mcePaste" class="mcePaste" style="left:-10000px; top:0px; width:1px; height:1px; overflow:hidden">
</div>
