# OBD Recorder for .Net Micro Framework with ServiceBus, AMQP (for IoT)
## Requires
- Visual Studio 2012
## License
- Apache License, Version 2.0
## Technologies
- Windows Azure Service Bus
- AMQP
## Topics
- Cloud
## Updated
- 05/20/2014
## Description

<p>Introduction</p>
<p><em>This sample illustrates how to connect a .Net Micro Framework-based device to the Azure cloud using Azure Service Bus as a Device Cloud Gateway for telemetry and command &amp; control.</em></p>
<p><em>The sample uses the AMQP.Net Lite library (<a href="http://amqpnetlite.codeplex.com">http://amqpnetlite.codeplex.com</a>) to implement bi-directional communication to Service Bus. It works with &quot;Reykjavik&quot; (a framework around Service Bus for large-scale
 device/IoT connectivity), but does also work with Service Bus on it's own.<br>
</em></p>
<h1><span>Building the Sample</span></h1>
<p><em>The sample requires Visual Studio 2012 (not 2013) because .Net Micro Framework is not supported on later versions at this time (May 2014).</em></p>
<p><em>You will need an SSL-capable&nbsp;.Net Gadgeteer compatible device with a networking module&nbsp;as well as an OBDII adapter (tested with GHI Electronics Spider&nbsp;board, Ethernet module, and OBDII adapter). If you don't have an OBDII adapter, the
 sample will generate simulated OBD data.</em></p>
<p><em>After downloading the sample you need to add a reference to the AMQP.Net Lite project, i.e. use &quot;Add existing Project&quot;, remove the AMQP reference in OBDRecorder project and re-add a project reference.</em></p>
<p><em>You need to create a Service Bus namespace and update the namespace URLs and keys in OBDRecorder/program.cs (the keys in the sample are no longer valid).<br>
</em></p>
<p><em>The sample does not currently function in the device emulator due to a bug in the emulator networking libraries.<br>
</em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p>At the core, the device uses the AMQP.Net Lite to send messages and receive messages.</p>
<p>Send:</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp">var&nbsp;amqpConnection&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Connection(<span class="cs__keyword">new</span>&nbsp;Address&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(<span class="cs__string">&quot;amqps://owner:&lt;key&gt;=@&lt;namespace&gt;servicebus.windows.net&quot;</span>));&nbsp;&nbsp;
amqpConnection.OnClosed&nbsp;&#43;=&nbsp;<span class="cs__keyword">this</span>.ConnectionClosed;&nbsp;&nbsp;
&nbsp;
var&nbsp;amqpSession&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Session(<span class="cs__keyword">this</span>.AmqpConnection);&nbsp;&nbsp;
amqpSession.OnClosed&nbsp;&#43;=&nbsp;<span class="cs__keyword">this</span>.SessionClosed;&nbsp;&nbsp;
&nbsp;&nbsp;
SenderLink&nbsp;amqpSender&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;SenderLink(amqpSession,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;send-link/in/t0000&quot;</span>,&nbsp;<span class="cs__com">//&nbsp;unique&nbsp;name&nbsp;for&nbsp;all&nbsp;links&nbsp;from&nbsp;this&nbsp;client</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;/in/t0000&quot;</span>);&nbsp;<span class="cs__com">//&nbsp;Service&nbsp;Bus&nbsp;entity&nbsp;name</span>&nbsp;
amqpSender.OnClosed&nbsp;&#43;=&nbsp;<span class="cs__keyword">this</span>.SenderLinkClosed;&nbsp;&nbsp;
&nbsp;&nbsp;
var&nbsp;message&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Message();&nbsp;&nbsp;
message.Properties&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Properties();&nbsp;&nbsp;
message.Properties.Subject&nbsp;=&nbsp;<span class="cs__string">&quot;mymessagetype&quot;</span>;&nbsp;&nbsp;
message.ApplicationProperties&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;ApplicationProperties();&nbsp;&nbsp;
message.ApplicationProperties[<span class="cs__string">&quot;MyProperty&quot;</span>]&nbsp;=&nbsp;<span class="cs__string">&quot;Hello&nbsp;World!&quot;</span>;&nbsp;&nbsp;
&nbsp;&nbsp;
amqpSender.Send(message,&nbsp;MessageOutcomeCallback,&nbsp;<span class="cs__keyword">null</span>);&nbsp;&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;Receive:</div>
<div class="endscriptcode"></div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__com">//&nbsp;Open&nbsp;connection&nbsp;and&nbsp;session&nbsp;as&nbsp;per&nbsp;above&nbsp;(or&nbsp;reuse&nbsp;the&nbsp;same&nbsp;connection/session&nbsp;as&nbsp;for&nbsp;send)</span>&nbsp;
var&nbsp;amqpReceiver&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;ReceiverLink(<span class="cs__keyword">this</span>.AmqpSession,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;receive-linkout/t0000/Subscriptions/s0000&quot;</span>,&nbsp;<span class="cs__com">//&nbsp;unique&nbsp;name&nbsp;for&nbsp;all&nbsp;links&nbsp;from&nbsp;this&nbsp;client</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;out/t0000/Subscriptions/s0000&quot;</span>);&nbsp;<span class="cs__com">//&nbsp;Service&nbsp;Bus&nbsp;entity&nbsp;name</span>&nbsp;
amqpReceiver.OnClosed&nbsp;&#43;=&nbsp;<span class="cs__keyword">this</span>.ReceiverLinkClosed;&nbsp;
amqpReceiver.Start(<span class="cs__number">5</span>,&nbsp;OnMessageCallback);&nbsp;
&nbsp;
...&nbsp;
&nbsp;
<span class="cs__keyword">void</span>&nbsp;OnMessageCallback(ReceiverLink&nbsp;receiver,&nbsp;Message&nbsp;message)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;<span class="cs__keyword">value</span>&nbsp;=&nbsp;message.ApplicationProperties[<span class="cs__string">&quot;MyProperty&quot;</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Do&nbsp;something&nbsp;with&nbsp;the&nbsp;value</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;receiver.Accept(message);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;receiver.SetCredit(<span class="cs__number">5</span>);&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>&nbsp;</p>
<p>&nbsp;</p>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em>OBDRecorder project:</em>
<ul>
<li><em>program.cs - the main logic</em> </li><li><em>obdii.cs - reads data from the OBDII adapter, simulates data if no adapter is connected.</em>
</li><li><em>amqpsender.cs - helper functions for message sending using AMQP.NET Lite</em>
</li><li><em>amqpreceiver.cs - helper functions for receiving messages</em> </li><li><em>amqpsendreceivebase.cs - commun functions use by sender and receiver helper functions.</em>
</li></ul>
</li><li><em>obd II_42/Elm328.Core projects: patches to the .NEt Gadgeteer libraries to make OBD work, at least in a Prius 2007.</em>
<ul>
</ul>
</li><li>Microsoft.ServiceBus.Micro - helper libraries for HTTP-based access to Service Bus (from
<a href="http://ms.aka/iot2">http://ms.aka/iot2</a>, with additions for SAS); only used when the sample is modified. Otherwise, the sample only sends via AMQP.
</li></ul>
<h1>More Information</h1>
<p><em><a href="http://amqpnetlite.codeplex.com">http://amqpnetlite.codeplex.com</a></em></p>
<p><em><br>
</em></p>
