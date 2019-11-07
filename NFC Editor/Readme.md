# NFC Editor
## Requires
- Visual Studio 2012
## License
- MS-LPL
## Technologies
- C#
- WPF
- Windows RT
- nfc
- proximity
- NDEF
## Topics
- Near Field Proximity (NFP)
- nfc
- proximity
## Updated
- 04/09/2013
## Description

<h1>Introduction</h1>
<p>This example shows howto use a proximity device for a <strong>Windows 8 Desktop Application</strong>. It implements a very simple editor for reading and writing URLs from respectively to a NFC tag. Please note that the NFC tag must be a formatted in
<em><a href="http://www.developer.nokia.com/Community/Wiki/Understanding_NFC_Data_Exchange_Format_(NDEF)_messages" target="_blank">NDEF format</a></em>. Writing or reading other types of information is also possible but not scope of this example. Please refer
 to the MSDN documentation for a list of <a href="http://msdn.microsoft.com/en-us/library/windows/apps/hh701129.aspx" target="_blank">
supported information types</a>&nbsp;and a <a href="http://msdn.microsoft.com/en-us/library/windows/apps/xaml/Hh465221(v=win.10).aspx" target="_blank">
getting started guide</a> for using NFCs.<br>
<br>
As a prerequisite you must have installed a NFC device that is recognized as Proximity device by Windows 8. I used the Sony RC-S380
<em><a title="Sony NFC Device" href="http://www.sony.net/Products/felica/business/information/120405.html" target="_blank">Cardreader/writer</a></em> and Sony's device driver that can be found in the free
<em><a title="Sony's NFC SDK for Windows" href="http://www.sony.net/Products/felica/business/products/ICS-D004_002_003.html" target="_blank">SDK for Windows</a></em>. Others devices may also work fine, but they have to be recognized as Proximity devices. You
 can check in the Windows device manager:</p>
<h1><span><img id="78894" src="78894-proximity%20device.png" alt="" width="242" height="59"></span></h1>
<p><span>The API for Proximity devices resides in Windows RT. For a Windows 8 Desktop Application you have to tell Visual Studio the targe platform version. Unfortunately there is no support in the graphical user interface for that, but you can simply edit
 the project file definition. </span></p>
<p><span>In our example we will first create a new Windows 8 Project for a WPF application. In order to access the proximity API we need to access the Windows Runtime as described in the
<em><a href="http://msdn.microsoft.com/en-us/library/hh708954(v=vs.110).aspx" target="_blank">MSDN documentation</a></em>: When the project has been created unload the project and open the project file. Now you add a line specifying Windows 8 as the
<em><strong>TargetPlatformVersion</strong></em>:</span></p>
<h1><span><img id="78895" src="78895-targetplatformversion.png" alt="" width="600" height="180"></span></h1>
<p><span>After adding the target platform version you can reload the project. Now a the
<strong>Core</strong> tab will appear and you can&nbsp;add a reference to&nbsp;<strong>Windows</strong>:</span></p>
<p><span><img id="78897" src="78897-windows.png" alt="" width="214" height="100">&nbsp;</span></p>
<p><span>Finally&nbsp;add a reference to the Windows Runtime in the folder containing the reference assemblies:</span></p>
<h1><span><img id="78896" src="78896-referenceassembly.png" alt="" width="600" height="100"></span></h1>
<p><span>&nbsp;</span><span>&nbsp;</span></p>
<h1><span>Building the Sample</span></h1>
<p><span><span>The example project has already been prepared as described in the previous section. In order to build the NFC Editor simple run the build command.</span></span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p>The sample makes use of the proximity API. All logic is encapsulated in the <strong>
<em>MainWindowViewModel.cs </em></strong>file. In the constructor the the events for arring and departing NFC labels are wired:</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">_proximityDevice = ProximityDevice.GetDefault();
if (_proximityDevice != null)
{
   _proximityDevice.DeviceArrived &#43;= _proximityDevice_DeviceArrived;
   _proximityDevice.DeviceDeparted &#43;= _proximityDevice_DeviceDeparted;
   _MessageType = _proximityDevice.SubscribeForMessage(&quot;WindowsUri&quot;, MessageReceivedHandler);
}</pre>
<div class="preview">
<pre class="csharp">_proximityDevice&nbsp;=&nbsp;ProximityDevice.GetDefault();&nbsp;
<span class="cs__keyword">if</span>&nbsp;(_proximityDevice&nbsp;!=&nbsp;<span class="cs__keyword">null</span>)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;_proximityDevice.DeviceArrived&nbsp;&#43;=&nbsp;_proximityDevice_DeviceArrived;&nbsp;
&nbsp;&nbsp;&nbsp;_proximityDevice.DeviceDeparted&nbsp;&#43;=&nbsp;_proximityDevice_DeviceDeparted;&nbsp;
&nbsp;&nbsp;&nbsp;_MessageType&nbsp;=&nbsp;_proximityDevice.SubscribeForMessage(<span class="cs__string">&quot;WindowsUri&quot;</span>,&nbsp;MessageReceivedHandler);&nbsp;
}</pre>
</div>
</div>
</div>
<p><span>The proximity device subscribes for messages of the type <em><strong>WindowsUri</strong></em>: As soon as an NFC tag bearing a URI is detected, the method MessageReceivedHandler will be called. Since the NFC tag contains the URI encoded as 16bit Unicode,
 this has to be taken into account when reading the information from the NFC tag:</span>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">using (var reader = DataReader.FromBuffer(message.Data))
{
    reader.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf16LE;
    string receivedString = reader.ReadString(reader.UnconsumedBufferLength / 2 - 1);
    Debug.WriteLine(&quot;Received message from NFC: &quot; &#43; receivedString);
    Url = receivedString;
}</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">using</span>&nbsp;(var&nbsp;reader&nbsp;=&nbsp;DataReader.FromBuffer(message.Data))&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;reader.UnicodeEncoding&nbsp;=&nbsp;Windows.Storage.Streams.UnicodeEncoding.Utf16LE;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;receivedString&nbsp;=&nbsp;reader.ReadString(reader.UnconsumedBufferLength&nbsp;/&nbsp;<span class="cs__number">2</span>&nbsp;-&nbsp;<span class="cs__number">1</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.WriteLine(<span class="cs__string">&quot;Received&nbsp;message&nbsp;from&nbsp;NFC:&nbsp;&quot;</span>&nbsp;&#43;&nbsp;receivedString);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Url&nbsp;=&nbsp;receivedString;&nbsp;
}</pre>
</div>
</div>
</div>
<div class="endscriptcode">When a NFC tag has been detected you can update the URI and writing it to the NFC by calling the method PublishBinaryMessage:</div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">using (var writer = new DataWriter{ UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf16LE } )
{
    Debug.WriteLine(&quot;Writing message to NFC: &quot; &#43; Url);
    writer.WriteString(Url);
    long id = _proximityDevice.PublishBinaryMessage(&quot;WindowsUri:WriteTag&quot;, writer.DetachBuffer());
   _proximityDevice.StopPublishingMessage(id);
}</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">using</span>&nbsp;(var&nbsp;writer&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;DataWriter{&nbsp;UnicodeEncoding&nbsp;=&nbsp;Windows.Storage.Streams.UnicodeEncoding.Utf16LE&nbsp;}&nbsp;)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Debug.WriteLine(<span class="cs__string">&quot;Writing&nbsp;message&nbsp;to&nbsp;NFC:&nbsp;&quot;</span>&nbsp;&#43;&nbsp;Url);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;writer.WriteString(Url);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">long</span>&nbsp;id&nbsp;=&nbsp;_proximityDevice.PublishBinaryMessage(<span class="cs__string">&quot;WindowsUri:WriteTag&quot;</span>,&nbsp;writer.DetachBuffer());&nbsp;
&nbsp;&nbsp;&nbsp;_proximityDevice.StopPublishingMessage(id);&nbsp;
}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>&nbsp;</p>
<p>&nbsp;</p>
<h1><em>&nbsp;</em></h1>
