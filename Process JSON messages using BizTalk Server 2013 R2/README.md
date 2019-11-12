# Process JSON messages using BizTalk Server 2013 R2
## Requires
- Visual Studio 2013
## License
- Apache License, Version 2.0
## Technologies
- JSON
- BizTalk Server
## Topics
- JSON messages in BizTalk Server
## Updated
- 06/30/2014
## Description

<h1>Introduction</h1>
<p>This tutorial demonstrates how to process JSON messages using BizTalk Server. The tutorial uses custom pipeline components, now available with BizTalk Server 2013 R2. These pipeline components convert the JSON message to XML (while receiving the message
 into BizTalk Server orchestration, and converts the message from XML to JSON while sending the message out.</p>
<h1>What does this sample do?</h1>
<ol class="ordered">
<li>Receives a JSON purchase order and in the receive pipeline, uses a JSON decoder component to transform the JSON message to XML message.<br>
<br>
</li><li>Transforms the XML purchase order into an XML invoice using a map.<br>
<br>
</li><li>In the send pipeline, uses a JSON encoder to transform the XML invoice into a JSON invoice and sends it out.
</li></ol>
<h1>Build the sample</h1>
<ol class="ordered">
<li>
<p>In the Solution Explorer, right-click the BizTalk project name, and then click
<strong>Properties</strong>.</p>
</li><li>
<p>On the Property page, click the Signing tab, select the Sign the assembly check box, and from the drop-down choose the option to create a new strong name key file. Follow the prompts to create the file.</p>
</li><li>
<p>Save changes to the project. In Solution Explorer, right-click the solution name, and then click
<strong>Build Solution</strong>.</p>
</li><li>
<p>After project builds successfully, in the Solution Explorer, right-click the solution name, and then click
<strong>Deploy Solution</strong>.</p>
</li></ol>
<p><span style="font-size:20px; font-weight:bold">Configure the application</span></p>
<p>To configure the application, follow the instructions at&nbsp;<a href="http://msdn.microsoft.com/en-us/library/dn789174(v=bts.80).aspx" target="_blank">http://msdn.microsoft.com/en-us/library/dn789174(v=bts.80).aspx</a>.</p>
<p>&nbsp;</p>
<h1><span style="font-size:medium"><span style="line-height:15px">Test the application</span><em style="font-size:10px">&nbsp;</em></span></h1>
<ol>
<em>&nbsp;</em>
<li><em>From the TestMessage folder (at the location where you downloaded the sample), copy and paste JsonPurchaseOrder.json into the In folder.
</em></li><li><em>Observe the .json file created in the&nbsp;<strong>Out</strong>&nbsp;folder. This file contains the Json invoice constructed from the input file JsonPurchaseOrder.json. The format of the name of this file is &lt;<em>MessageID</em>&gt;.json, where&nbsp;<em>&lt;MessageID&gt;</em>&nbsp;is
 the GUID generated to uniquely identify the message.</em> </li></ol>
<p><em><br>
</em></p>
<ul>
</ul>
<h1>More Information</h1>
<p><em>For more information on the tutorial, which is based on this sample, see <a href="http://msdn.microsoft.com/en-us/library/dn789173(v=bts.80).aspx" target="_blank">
http://msdn.microsoft.com/en-us/library/dn789173(v=bts.80).aspx</a>.&nbsp;</em></p>
