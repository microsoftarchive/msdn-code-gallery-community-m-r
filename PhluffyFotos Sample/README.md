# PhluffyFotos Sample
## Requires
- Visual Studio 2012
## License
- Apache License, Version 2.0
## Technologies
- Microsoft Azure
- Windows Azure Storage
- ASP.NET MVC 4
## Topics
- Microsoft Azure
- ASP.NET MVC
- Windows Azure Storage
## Updated
- 08/08/2012
## Description

<h1>PhluffyFotos Sample</h1>
<p>PhluffyFotos is a Picture Gallery Service where users can upload their pictures from the web or mobile device. Users can upload, tag, and share photos in this sample.</p>
<p>The sample utilizes several technologies including <a href="http://www.asp.net/mvc/mvc4">
ASP.NET MVC 4</a>, <a href="http://www.windowsazure.com/en-us/develop/net/how-to-guides/sql-database/">
Windows Azure SQL Databases</a> and <a href="http://www.windowsazure.com/en-us/home/features/storage/">
Windows Azure Storage</a>, including <a href="https://www.windowsazure.com/en-us/develop/net/how-to-guides/table-services/">
Tables</a>, <a href="https://www.windowsazure.com/en-us/develop/net/how-to-guides/blob-storage/">
Blobs</a>, and <a href="https://www.windowsazure.com/en-us/develop/net/how-to-guides/queue-service/">
Queues</a>. In this sample, you will see how to pull together both a web application and Windows Azure Worker role to build cloud applications.</p>
<h3>Prerequisites</h3>
<ul>
<li><a href="http://www.microsoft.com/visualstudio/en-us/products">Visual Studio 2012</a>
</li><li><a href="http://www.windowsazure.com/en-us/develop/net/">Windows Azure SDK for .NET 1.7</a>
</li><li><a href="http://www.asp.net/mvc/mvc4">ASP.NET MVC 4</a> </li></ul>
<h3>Running the Sample Locally</h3>
<ol>
<li>Open Visual Studio 2012 as administrator. </li><li>Compile the solution. The NuGet packages dependencies will be automatically downloaded and installed.
</li><li>Run the <strong>PhluffyFotos</strong> cloud project (right-click the project and select Debug | Start new Instance).
</li><li>Run the <strong>PhluffyFotos.Web</strong> project (right-click the project and select Debug | Start new Instance).
</li></ol>
<h3>Running the Sample in Windows Azure</h3>
<ol>
<li>
<p>To run this sample on the cloud you need a Windows Azure Subscription. If you don't have a Windows Azure account, you can sign up for a free trial
<a href="http://bit.ly/windowsazuretrial">here</a>.</p>
</li><li>
<p>You also need a Windows Azure Storage account, for leveraging Queues, Table and Blob Storage. Once you have your Windows Azure subscription, follow the steps in
<a href="https://www.windowsazure.com/en-us/develop/net/how-to-guides/blob-storage/">
this article</a> to create a storage account. Make note of the account name and Primary Acess Key.</p>
</li><li>
<p>To update the solution with the values obtained previously, follow these steps:</p>
<ol>
<li>Use the Windows Azure portal to create a Web site with a database </li><li>Use the Windows Azure portal to create a Cloud Service </li><li>Use the Windows Azure portal to create a Windows Azure Storage account </li><li>Start Visual Studio 2012 and open the solution included with this sample's download.
</li><li>Open the <strong>ServiceConfiguration.Cloud.cscfg</strong> file located in the
<strong>PhluffyFotos</strong> Cloud project. </li><li>
<p>Modify the <strong>DataConnectionsString</strong> and the Diagnostic Connection string (Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString) for the worker role to point to the storage account you've just created.</p>
<blockquote>
<p><strong>Note:</strong> The connection string used for accessing the windows azure storage account is built with the following format:
<code>DefaultEndpointsProtocol=https;AccountName={YOUR-ACCONT};AccountKey={YOUR-ACCONT-KEY}</code> In it you'll need to replace
<code>{YOUR-ACCONT}</code> with the storage account name and <code>{YOUR-ACCONT-KEY}</code> with the Primary Access Key copied.</p>
</blockquote>
</li><li>Open the <strong>Web.config</strong> file located in the <strong>PhluffyFotos.Web</strong> project and update the
<strong>DataConnectionString</strong> with the same value used in the step above.
</li><li>Open the <strong>Web.config</strong> file located in the <strong>PhluffyFotos.Web</strong> project and update the
<strong>DefaultConnection</strong> connction string to reflect the connection string to the Windows Azure SQL Database you created in step 1.
</li><li>Save the changes made to the file. </li></ol>
</li><li>
<p>Publish the <strong>PhlufffyPhotos</strong> Cloud Service project to the Windows Azure Cloud Service you created in Step 2.</p>
</li><li>Publish the <strong>PhluffyPhotos.Web</strong> project to the Windows Azure Web Site you created in Step 1.
</li></ol>
<h3>Detailed Instructions</h3>
<p>For a more detailed walk-through on how to perform each of the individual tasks required to get this sample working in Windows Azure, see the sample's
<a href="https://github.com/WindowsAzure-Samples/PhluffyFotos/blob/master/GettingStarted.md">
Getting Started document</a> in the repository containing the code for the sample.</p>
