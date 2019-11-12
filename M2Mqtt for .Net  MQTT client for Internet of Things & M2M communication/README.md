# M2Mqtt for .Net : MQTT client for Internet of Things & M2M communication
## Requires
- Visual Studio 2012
## License
- Apache License, Version 2.0
## Technologies
- C#
- .NET Framework
- Windows Embedded Compact 7
- C# Language
- .NET Micro Framework
- .Net Compact Framework
- Windows Embedded Compact 2013
## Topics
- Messaging
- Cloud
- Devices and sensors
- Connected devices
- MQTT
- Internet of Things
- IoT
- M2M
- Machine to Machine
- Device to Cloud
## Updated
- 03/28/2015
## Description

<h1>Introduction</h1>
<p><em>M2Mqtt is a MQTT client available for all .Net platform (.Net Framework, .Net Compact Framework and .Net Micro Framework) for Internet of Things and M2M communication.</em></p>
<p><em>MQTT, short for Message Queue Telemetry Transport, is a light weight messaging protocol that enables embedded devices with limited resources to perform asynchronous communication on a constrained network.</em></p>
<p><em>MQTT protocol is based on publish/subscribe pattern so that a client can subscribe to one or more topics and receive messages that other clients publish on these topics.</em></p>
<p><em>This sample is a library contains an MQTT client that you can use to connect to any MQTT broker. It is developed in C# language and works on all the following .Net platforms :</em></p>
<ul>
<li><em>.Net Framework (up to 4.5)</em> </li><li><em>.Net Compact Framework 3.5 &amp; 3.9 (for Windows Embedded Compact 7 / 2013)</em>
</li><li><em>.Net Micro Framework 4.2 &amp; 4.3</em> </li><li><em>Mono (for Linux O.S.)</em> </li></ul>
<p>It can be used on Windows O.S, Windows Embedded Compact 7 / 2013 and Linux O.S. (thanks to Mono Project).</p>
<p>Official web site on CodePlex :&nbsp;<a href="https://m2mqtt.codeplex.com/">https://m2mqtt.codeplex.com/</a></p>
<h1><span>Building the Sample</span></h1>
<p><em>The library is available for the following solution and project files :</em></p>
<ul>
<li><em>M2MqttVS2012.sln : solution for Visual Studio 2012 that contains projects file for .Net Framework, .Net Compact Framework 3.9, .Net Micro Framework 4.2 and .Net Micro Framework 4.3;</em>
</li><li><em>M2MqttVS2010.sln : solution for Visual Studio 2010&nbsp;<em>that contains projects file for .Net Framework and&nbsp;<em>.Net Micro Framework 4.2;</em></em></em>
</li><li><em><em><em>M2MqttVS2008.sln :&nbsp;<em>solution for Visual Studio 2008&nbsp;<em>that contains project file for .Net Compact Framework 3.5;</em></em></em></em></em>
</li><li><em><em><em><em><em>M2MqttMono.sln :&nbsp;<em><em><em><em>solution for MonoDevelop for building project under Linux O.S. with Mono Project;</em></em></em></em></em></em></em></em></em>
</li></ul>
<p>In this sample there is only the Visual Studio 2012 solution but on the CodePlex web site&nbsp;<a href="https://m2mqtt.codeplex.com/">https://m2mqtt.codeplex.com/</a>&nbsp;all other versions are available.</p>
<p>To build sample based on .Net Micro Framework (4.2 and 4.3) you need to download .Net Micro Framework SDK from the official CodePlex web site :&nbsp;<a href="https://netmf.codeplex.com/">https://netmf.codeplex.com/</a></p>
<p>To build sample based on .Net Compact Framework 3.9 you need to download Application Builder for Windows Embedded Compact 2013 from here :&nbsp;<a href="http://www.microsoft.com/en-us/download/details.aspx?id=38819">http://www.microsoft.com/en-us/download/details.aspx?id=38819</a></p>
<h1><span style="font-size:20px; font-weight:bold">Description</span></h1>
<p><em>The M2Mqtt library provides a main class MqttClient that represents the MQTT client to connect to a broker. You can connect to the broker providing its IP address or host name and optionally some parameters related to MQTT protocol.</em></p>
<p><em>After connecting to the broker you can use Publish() method to publish a message to a topic and Subscribe() method to subscribe to a topic and receive message published on it. The
<em>MqttClient class is events based so that you receive an event when a message is published to a topic you subscribed to. You can receive event when a message publishing is complete, you have subscribed or unsubscribed to a topic.</em></em></p>
<p><em>Following an example of client subscriber to a topic :</em></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp">...&nbsp;
&nbsp;
<span class="cs__com">//&nbsp;create&nbsp;client&nbsp;instance</span>&nbsp;
MqttClient&nbsp;client&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;MqttClient(IPAddress.Parse(MQTT_BROKER_ADDRESS));&nbsp;
&nbsp;
<span class="cs__com">//&nbsp;register&nbsp;to&nbsp;message&nbsp;received</span>&nbsp;
client.MqttMsgPublishReceived&nbsp;&#43;=&nbsp;client_MqttMsgPublishReceived;&nbsp;
&nbsp;
<span class="cs__keyword">string</span>&nbsp;clientId&nbsp;=&nbsp;Guid.NewGuid().ToString();&nbsp;
client.Connect(clientId);&nbsp;
&nbsp;
<span class="cs__com">//&nbsp;subscribe&nbsp;to&nbsp;the&nbsp;topic&nbsp;&quot;/home/temperature&quot;&nbsp;with&nbsp;QoS&nbsp;2</span>&nbsp;
client.Subscribe(<span class="cs__keyword">new</span>&nbsp;<span class="cs__keyword">string</span>[]&nbsp;{&nbsp;<span class="cs__string">&quot;/home/temperature&quot;</span>&nbsp;},&nbsp;<span class="cs__keyword">new</span>&nbsp;<span class="cs__keyword">byte</span>[]&nbsp;{&nbsp;MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE&nbsp;});&nbsp;
&nbsp;
...&nbsp;
&nbsp;
<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;client_MqttMsgPublishReceived(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;MqttMsgPublishEventArgs&nbsp;e)&nbsp;
{&nbsp;
<span class="cs__com">//&nbsp;handle&nbsp;message&nbsp;received</span>&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<p><em>Following an example of client publisher to a topic :</em><em>&nbsp;</em></p>
<div class="scriptcode"><em>
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp">...&nbsp;
&nbsp;
<span class="cs__com">//&nbsp;create&nbsp;client&nbsp;instance</span>&nbsp;
MqttClient&nbsp;client&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;MqttClient(IPAddress.Parse(MQTT_BROKER_ADDRESS));&nbsp;
&nbsp;
<span class="cs__keyword">string</span>&nbsp;clientId&nbsp;=&nbsp;Guid.NewGuid().ToString();&nbsp;
client.Connect(clientId);&nbsp;
&nbsp;
<span class="cs__keyword">string</span>&nbsp;strValue&nbsp;=&nbsp;Convert.ToString(<span class="cs__keyword">value</span>);&nbsp;
&nbsp;
<span class="cs__com">//&nbsp;publish&nbsp;a&nbsp;message&nbsp;on&nbsp;&quot;/home/temperature&quot;&nbsp;topic&nbsp;with&nbsp;QoS&nbsp;2</span>&nbsp;
client.Publish(<span class="cs__string">&quot;/home/temperature&quot;</span>,&nbsp;Encoding.UTF8.GetBytes(strValue),&nbsp;MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE);&nbsp;
&nbsp;
...</pre>
</div>
</div>
</em></div>
<h1><em>M2Mqtt 3.0.0.0 Update</em></h1>
<p><em>With this update, the <strong>&quot;inflight queue&quot;</strong> was added for the publish, subscribe and unsubscribe messages. Now, all these
<strong>operations are executed asynchronously</strong>.</em></p>
<p><em>On Subscribe() method there is a breaking changes; the method doesn't return granted QoS levels but message id assigned to subscribe message sent. The granted QoS levels are returned in the event args of the related event at the end of asynchronous operation
 as before.</em></p>
<h1><em>M2Mqtt 3.1.0.0 Update</em></h1>
<p><em>With this update, QoS level 2 handle was improved and some other features to MqttClient were added.</em></p>
<p><em>It was also uniformed all classes with future GnatMQ broker (<a href="https://mqttbroker.codeplex.com/" target="_blank">https://mqttbroker.codeplex.com/</a>) based on M2Mqtt core and under development.</em></p>
<p><em>For more information, please visit official CodePlex project web site :&nbsp;<a href="https://m2mqtt.codeplex.com/" target="_blank">https://m2mqtt.codeplex.com/</a></em></p>
<h1><em>M2Mqtt 3.2.0.0 Update</em></h1>
<p><em>Bug fix :&nbsp;<span>&nbsp;excluded SUBSCRIBE, UNSUBSCRIBE, CONNECT and DISCONNECT messages handling;</span></em></p>
<h1><em>M2Mqtt 3.3.0.0 Update</em></h1>
<p><em>Added more overloads for Connect() method;</em></p>
<p><em>Exposed client disconnection (from broker) event;</em></p>
<h1><em>M2Mqtt 3.4.0.0 Update</em></h1>
<p><em><span>Added tracing features in DEBUG mode with Trace class and TraceLevel enumeration;</span></em></p>
<h1><em><span>M2Mqtt 3.5.0.0 Update</span></em></h1>
<p><em>Exposed callbacks for SSL/TLS server certificate validation and client certificate selection;</em></p>
<p><em>Moved tracing under symbol TRACE;</em></p>
<p><em>Bug fixing from issues on CodePlex web site;</em></p>
<p><em>Change license from L-GPL to Apache 2.0;</em></p>
<h1><em>M2Mqtt 3.6.0.0 Update</em></h1>
<p><em>Exported all network and security related operations in IMqttNetworkChannel implementation;</em></p>
<p><em>Added support for WinRT platforms (Windows 8.1 and Windows Phone 8.1);</em></p>
<h1><em>M2Mqtt 4.0.0.0 Update</em></h1>
<p><em>Support for old 3.1.0 and new 3.1.1 OASIS specification;</em><br>
<em>&nbsp;</em></p>
<p><em>Added session management (however without persistence) for supporting for 3.1.1 OASIS specification;</em></p>
<p><em>Keep Alive ping request disabled if keep alive timeout is set to zero;</em></p>
<p><em>&nbsp;</em><em>Inflight queue size managed;</em></p>
<p><em></em></p>
<p><em>Improvement on receiving thread with some modification on underlying network channel;</em><br>
<em></em></p>
<p><em>Bug fixing from issues on CodePlex <em>web site;</em></em></p>
<h1><em>M2Mqtt 4.1.0.0 Update</em></h1>
<p><em><span>Exposed IsPublished flag in the published event to know if message is published or not due to timeout;</span></em></p>
<p><em><span>Internal changes for event management;</span></em></p>
<p><em><span>Fixed bug on negative timeout;</span></em></p>
<p><em><span>Improved stability on not reliable network;</span></em></p>
<p><em><span>Fixed bug on session management;</span></em></p>
<p><em><span>Added trace on queueing operations;</span><br>
</em></p>
<p><em>See the new code sample with Visual Studio 2013 solution <a href="http://code.msdn.microsoft.com/M2Mqtt-for-WinRT-MQTT-71c7d571">
here</a>.</em></p>
