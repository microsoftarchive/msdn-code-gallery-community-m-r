# Processing Service Bus Queue Using WebJobs
## Requires
- Visual Studio 2015
## License
- MIT
## Technologies
- Microsoft Azure
- WebJobs
- Azure SQL Database
## Topics
- integration
- WebJobs
## Updated
- 04/19/2016
## Description

<h1>Introduction</h1>
<p><em>A WebJob is a simple way to set up a background job, which can process continuously or on a schedule. WebJobs differ from a cloud service as it gives you get less fine-grained control over your processing environment, making it a more true PaaS service.
 This sample is part of my blogpost which you can find <a href="http://blog.eldert.net/?p=1355" target="_blank">
here</a>.</em></p>
<h1><span style="font-size:20px; font-weight:bold">Description</span></h1>
<p>We will need a Web App to host our WebJob, so lets create one in the Azure Portal. You can create a new Web App by going to App Services, and selecting New.<br>
<br>
</p>
<p><img id="151078" src="151078-image_thumb5.png" alt="" style="margin-right:auto; margin-left:auto; display:block"></p>
<p>To simplify our deployment later on, we will download the publish profile for our Web App once it has been created.<br>
<br>
</p>
<p><img id="151079" width="529" height="411" src="151079-image_thumb-22.png" alt="" style="margin-right:auto; margin-left:auto; display:block"><br>
Next we will create a new project for our WebJob, so be sure to install the <a href="https://azure.microsoft.com/en-us/documentation/articles/websites-dotnet-webjobs-sdk/" target="_blank">
Azure WebJob SDK</a> if you don&rsquo;t have it yet.<br>
<br>
</p>
<p><img id="151080" width="941" height="653" src="151080-image-21.png" alt="" style="margin-right:auto; margin-left:auto; display:block"><br>
Once the project has been created, start by going to the App.Config, and setting the connection strings for the dashboard and storage. This should be in the format DefaultEndpointsProtocol=https;AccountName=NAME;AccountKey=KEY. Both the name and the key can
 be found in the settings of your storage account.<br>
<br>
</p>
<p><img id="151081" width="529" height="277" src="151081-image_thumb7_thumb.png" alt="" style="margin-right:auto; margin-left:auto; display:block"><br>
We will also need to set the connection string for our Service Bus Queue, for which we will need a Shared Access Key with Manage permissions, as&nbsp;required by the WebJob&rsquo;s job host.<br>
<br>
</p>
<p><img id="151082" width="529" height="310" src="151082-image_thumb-23.png" alt="" style="margin-right:auto; margin-left:auto; display:block"><br>
And finally, we will also need to add the connection string to our Azure SQL database, which we will use from our Entity Framework library to communicate with the database.<strong>&nbsp;</strong><em>&nbsp;</em><br>
<br>
</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>

<div class="preview">
<pre class="js">&lt;connectionStrings&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;name=<span class="js__string">&quot;AzureWebJobsDashboard&quot;</span>&nbsp;connectionString=<span class="js__string">&quot;DefaultEndpointsProtocol=https;AccountName=eldertiot;AccountKey=xxxxxxxxxxxxxxxxxxxxxxxxxxxxx&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;name=<span class="js__string">&quot;AzureWebJobsStorage&quot;</span>&nbsp;connectionString=<span class="js__string">&quot;DefaultEndpointsProtocol=https;AccountName=eldertiot;AccountKey=xxxxxxxxxxxxxxxxxxxxxxxxxxxxx&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;name=<span class="js__string">&quot;AzureWebJobsServiceBus&quot;</span>&nbsp;connectionString=<span class="js__string">&quot;Endpoint=sb://eldertiot.servicebus.windows.net/;SharedAccessKeyName=administrationconsole;SharedAccessKey=xxxxxxxxxxxxxxxxxxxxxxxxxxxxx&quot;</span>&nbsp;/&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;add&nbsp;name=<span class="js__string">&quot;IoTDatabaseContext&quot;</span>&nbsp;connectionString=<span class="js__string">&quot;Server=tcp:eldertiot.database.windows.net,1433;Database=eldertiot;User&nbsp;ID=Eldert@eldertiot;Password=xxxxxxxxxxxxxxx;Encrypt=True;TrustServerCertificate=False;Connection&nbsp;Timeout=30;&quot;</span>&nbsp;providerName=<span class="js__string">&quot;System.Data.SqlClient&quot;</span>/&gt;&nbsp;
&lt;/connectionStrings&gt;&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>We also have to add the AzureWebJobsDashboard connection string to the Application settings of the Web App in the Azure portal, so the logs can be stored in your storage.<br>
<br>
<strong>&nbsp;</strong><em>&nbsp;</em></p>
<p>&nbsp;</p>
<p><img id="151083" src="151083-image_thumb9_thumb.png" alt="" style="margin-right:auto; margin-left:auto; display:block"></p>
<p><br>
By default a trigger is added to the WebJob for storage queues, however as we want to work with a Service Bus Queue, we will need to add the Microsoft.Azure.WebJob.ServiceBus NuGet package to our project.<br>
<br>
<strong>&nbsp;</strong><em>&nbsp;</em></p>
<p><img id="151084" width="1024" height="350" src="151084-image-24.png" alt="" style="margin-right:auto; margin-left:auto; display:block"><br>
Now that we have all configuration in place, we&rsquo;ll go and implement the code in our WebJob. Open up the Functions class which was created with inside your WebJob project. We will change the trigger type to ServiceBusTrigger so we can get triggers from
 our Service Bus Queue. As we are using a Service Bus trigger, we will also need to change the type of the message to be a BrokeredMessage instead of a string. When we have received the message, we will save its contents to the database, using
<a href="https://code.msdn.microsoft.com/Entity-Framework-Code-e9000ebc" target="_blank">
this library</a>.<strong></strong><em></em></p>
<p>&nbsp;</p>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="js">using&nbsp;System;&nbsp;
using&nbsp;<a class="libraryLink" href="https://msdn.microsoft.com/en-US/library/System.IO.aspx" target="_blank" title="Auto generated link to System.IO">System.IO</a>;&nbsp;
&nbsp;&nbsp;
using&nbsp;Eldert.IoT.Data.DataTypes;&nbsp;
&nbsp;&nbsp;
using&nbsp;Microsoft.Azure.WebJobs;&nbsp;
using&nbsp;<a class="libraryLink" href="https://msdn.microsoft.com/en-US/library/Microsoft.ServiceBus.Messaging.aspx" target="_blank" title="Auto generated link to Microsoft.ServiceBus.Messaging">Microsoft.ServiceBus.Messaging</a>;&nbsp;
&nbsp;&nbsp;
namespace&nbsp;Eldert.IoT.Azure.ServiceBusQueueProcessor&nbsp;
<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;class&nbsp;Functions&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;private&nbsp;static&nbsp;readonly&nbsp;IoTDatabaseContext&nbsp;database&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;IoTDatabaseContext();&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">///&nbsp;This&nbsp;function&nbsp;will&nbsp;get&nbsp;triggered/executed&nbsp;when&nbsp;a&nbsp;new&nbsp;message&nbsp;is&nbsp;written&nbsp;on&nbsp;an&nbsp;Azure&nbsp;Service&nbsp;Bus&nbsp;Queue.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;static&nbsp;<span class="js__operator">void</span>&nbsp;ProcessQueueMessage([ServiceBusTrigger(<span class="js__string">&quot;queueerrorsandwarnings&quot;</span>)]&nbsp;BrokeredMessage&nbsp;message,&nbsp;TextWriter&nbsp;log)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">try</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;log.WriteLine($<span class="js__string">&quot;Processing&nbsp;message:&nbsp;{message.Properties[&quot;</span>exceptionmessage<span class="js__string">&quot;]}&nbsp;Ship:&nbsp;{message.Properties[&quot;</span>ship<span class="js__string">&quot;]}&quot;</span>);&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Add&nbsp;the&nbsp;message&nbsp;we&nbsp;received&nbsp;from&nbsp;our&nbsp;queue&nbsp;to&nbsp;the&nbsp;database</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;database.ErrorAndWarningsEntries.Add(<span class="js__operator">new</span>&nbsp;ErrorAndWarning()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CreatedDateTime&nbsp;=&nbsp;DateTime.Parse(message.Properties[<span class="js__string">&quot;time&quot;</span>].ToString()),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ShipName&nbsp;=&nbsp;message.Properties[<span class="js__string">&quot;ship&quot;</span>].ToString(),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Message&nbsp;=&nbsp;message.Properties[<span class="js__string">&quot;exceptionmessage&quot;</span>].ToString()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Save&nbsp;changes&nbsp;in&nbsp;the&nbsp;database</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;database.SaveChanges();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">catch</span>&nbsp;(Exception&nbsp;exception)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;log.WriteLine($<span class="js__string">&quot;Exception&nbsp;in&nbsp;ProcessQueueMessage:&nbsp;{exception}&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
<span class="js__brace">}</span>&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
Next we will update the Program class, as we will need to register our Service Bus extension in the configuration of our job host.<strong></strong><em></em></div>
<div class="endscriptcode"></div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="js">using&nbsp;Microsoft.Azure.WebJobs;&nbsp;
&nbsp;&nbsp;
namespace&nbsp;Eldert.IoT.Azure.ServiceBusQueueProcessor&nbsp;
<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;To&nbsp;learn&nbsp;more&nbsp;about&nbsp;Microsoft&nbsp;Azure&nbsp;WebJobs&nbsp;SDK,&nbsp;please&nbsp;see&nbsp;http://go.microsoft.com/fwlink/?LinkID=320976</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;class&nbsp;Program&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Please&nbsp;set&nbsp;the&nbsp;following&nbsp;connection&nbsp;strings&nbsp;in&nbsp;app.config&nbsp;for&nbsp;this&nbsp;WebJob&nbsp;to&nbsp;run:</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;AzureWebJobsDashboard&nbsp;and&nbsp;AzureWebJobsStorage</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;static&nbsp;<span class="js__operator">void</span>&nbsp;Main()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Create&nbsp;job&nbsp;host&nbsp;configuration</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;config&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;JobHostConfiguration();&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Tell&nbsp;configuration&nbsp;we&nbsp;want&nbsp;to&nbsp;use&nbsp;Azure&nbsp;Service&nbsp;Bus</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;config.UseServiceBus();&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Add&nbsp;the&nbsp;configuration&nbsp;to&nbsp;the&nbsp;job&nbsp;host</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;host&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;JobHost(config);&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;The&nbsp;following&nbsp;code&nbsp;ensures&nbsp;that&nbsp;the&nbsp;WebJob&nbsp;will&nbsp;be&nbsp;running&nbsp;continuously</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;host.RunAndBlock();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
<span class="js__brace">}</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1 class="endscriptcode"><br>
More Information</h1>
</div>
<p><em>If you would like more information on how to publish and debug the application, you can find this&nbsp;in
<a href="http://blog.eldert.net/?p=1355" target="_blank">my blogpost</a>.&nbsp;<strong></strong><em></em></em></p>
