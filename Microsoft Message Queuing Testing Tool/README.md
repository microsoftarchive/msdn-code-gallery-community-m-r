# Microsoft Message Queuing Testing Tool
## Requires
- Visual Studio 2013
## License
- MIT
## Technologies
- C#
- BizTalk Server
- MSMQ
- Microsoft Message Queuing
## Topics
- Tool
- BizTalk Server
- MSMQ
- Testing
- Microsoft Message Queuing
## Updated
- 02/27/2017
## Description

<h1>Introduction</h1>
<p>&ldquo;Microsoft Message Queuing Testing Tool&rdquo; is a simple tool that you can use to test sending files to Microsoft Queue.</p>
<p>Microsoft Message Queuing or MSMQ is a message queue implementation developed by Microsoft and deployed in its Windows Server operating systems and enables applications running at different times to communicate across heterogeneous networks and systems that
 may be temporarily offline. It provides guaranteed message delivery, efficient routing, security, and priority-based messaging.</p>
<p>Message Queuing&nbsp;can be used to implement solutions to both asynchronous and synchronous scenarios requiring high performance. The following list shows several places where Message Queuing can be used.</p>
<ul>
<li>Mission-critical financial services: for example, electronic commerce. </li><li>Embedded and hand-held applications: for example, underlying communications to and from embedded devices that route baggage through airports by means of an automatic baggage system.
</li><li>Outside sales: for example, sales automation applications for traveling sales representatives.
</li><li>Workflow: Message Queuing makes it easy to create a workflow that updates each system. A typical design pattern is to implement an agent to interact with each system. Using a workflow agent architecture also minimizes the impact of changes in one system
 on the other systems. With Message Queuing, the loose coupling between systems makes upgrading individual systems simpler.
</li></ul>
<p>Source:&nbsp;<a href="https://msdn.microsoft.com/en-us/library/ms711472%28v=vs.85%29.aspx?f=255&MSPPError=-2147217396">Message Queuing (MSMQ)</a></p>
<p>With this tool, you can easily send messages to a queue in order to evaluate whether other applications are reading correctly the messages.&nbsp;</p>
<h1><img id="145147" src="https://i1.code.msdn.s-msft.com/message-queuing-testing-20782d44/image/file/145147/1/microsoft-message-queuing-testing-tool-v2.png" alt="" width="623" height="417"></h1>
<ul>
</ul>
<h1>Description</h1>
<p>This tool&nbsp;allows you to set:</p>
<ul>
<li>The queue where you want to send the files/messages </li><li>A Label associated with the&nbsp;file/message </li><li>Import an existing file or manually add a message </li><li>Set code page identifier of the document </li><li>The Queue transaction type </li></ul>
<p>And it will provide:</p>
<ul>
<li>a success of delivery information (with transaction id) </li><li>or error detail information </li></ul>
<h1>Release Notes</h1>
<p>Version 2.0 brings:</p>
<ul>
<li>Improvements in the &ldquo;Message Label&rdquo; auto fill </li><li>Improvements in the &ldquo;Import Message File&rdquo; functionality with better control of the encoder associated with the file
<ul>
<li>Add the ability to choose the code page identifier&nbsp; </li></ul>
</li></ul>
<h1>About Me</h1>
<p><strong>Sandro Pereira</strong><br>
<a href="http://www.devscope.net/">DevScope</a>&nbsp;| MVP &amp; MCTS BizTalk Server 2010<br>
<a href="http://sandroaspbiztalkblog.wordpress.com/">http://sandroaspbiztalkblog.wordpress.com/</a>&nbsp;|&nbsp;<a href="http://twitter.com/sandro_asp">@sandro_asp</a></p>
<p><a href="http://www.devscope.net/"><img id="129835" src="https://gallery.technet.microsoft.com/site/view/file/129835/1/devscope-monochrome-black.png" alt="" width="166" height="51"></a></p>
