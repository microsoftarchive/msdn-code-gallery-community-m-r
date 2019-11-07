# Multiple Message Queuing Listener MSMQ
## Requires
- Visual Studio 2010
## License
- Apache License, Version 2.0
## Technologies
- Console
- MSMQ
- Message Queuing
## Topics
- MSMQ
- Threads
- Message Queuing
## Updated
- 07/28/2011
## Description

<h1><strong>Introduction</strong></h1>
<p><span style="font-family:arial,helvetica,sans-serif; font-size:xx-small"><em>This project is used as a Windows Services and intends to listen multiple Message Queues (MSMQ) at the same time and handle the message content as the developer needs.</em></span></p>
<h1><strong>Building the Sample</strong></h1>
<p><span style="font-family:arial,helvetica,sans-serif; font-size:xx-small"><em>To build and run this sample you need to install Message Queuing (MSMQ) of your Windows. The following procedures show how to install Message Queuing 4.0 and Message Queuing 3.0.</em></span></p>
<p><span style="font-family:arial,helvetica,sans-serif; font-size:xx-small"><strong>To install Message Queuing 4.0 on Windows Server 2008 or Windows Server 2008 R2</strong></span></p>
<ol>
<li><span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">In <strong>
Server Manager</strong>, click <strong>Features</strong>. </span></li><li><span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">In the</span><br>
<span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">right-hand pane under
<strong>Features Summary</strong>,</span><br>
<span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">click <strong>
Add Features</strong>. </span></li><li><span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">In the</span><br>
<span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">resulting window, expand Message Queuing.
</span></li><li><span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">Expand
<strong>Message Queuing Services</strong>. </span></li><li><span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">Click
<strong>Directory Services Integration</strong> (for</span><br>
<span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">computers joined to a Domain), then click
<strong>HTTP<br>
Support</strong>. </span></li><li><span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">Click
<strong>Next</strong>, then click Install. </span></li></ol>
<p><span style="font-family:arial,helvetica,sans-serif; font-size:xx-small"><strong>To install Message Queuing 4.0 on Windows 7 or Windows Vista</strong></span></p>
<ol>
<li><span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">Open
<strong>Control Panel</strong>. </span></li><li><span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">Click
<strong>Programs</strong> and then, under <strong>Programs and Features</strong>, click
<strong>Turn Windows Features on and off</strong>. </span></li><li><span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">Expand</span><br>
<span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">Microsoft Message Queue (MSMQ) Server, expand Microsoft Message Queue (MSMQ)</span><br>
<span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">Server Core, and then select the check boxes for the following Message Queuing</span><br>
<span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">features to install:
</span>
<ol>
<li><span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">MSMQ Active Directory Domain Services Integration (for computers joined to a Domain).
</span></li><li><span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">MSMQ HTTP Support.
</span></li></ol>
</li><li><span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">Click
<strong>OK</strong>. </span></li><li><span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">If you are</span><br>
<span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">prompted to restart the computer, click
<strong>OK</strong></span><br>
<span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">to complete the installation.
</span></li></ol>
<p><span style="font-family:arial,helvetica,sans-serif; font-size:xx-small"><strong>To install Message Queuing 3.0 on Windows XP and Windows Server 2003</strong></span><br>
<span style="font-family:arial,helvetica,sans-serif; font-size:xx-small"><strong>Note:</strong> If you are running Windows Server 2003, select Application Server to access Message Queuing</span></p>
<ol>
<li><span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">Open Control</span><br>
<span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">Panel. </span>
</li><li><span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">Click
<strong>Add Remove Programs</strong> and then click <strong>Add Windows Components</strong>.
</span></li><li><span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">Select Message</span><br>
<span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">Queuing and click
<strong>Details</strong>. </span></li><li><span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">Ensure that the</span><br>
<span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">option MSMQ HTTP Support is selected on the details page.
</span></li><li><span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">Click
<strong>OK</strong> to exit the details page, and then</span><br>
<span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">click <strong>
Next</strong>. Complete the</span><br>
<span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">installation.
</span></li><li><span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">If you are</span><br>
<span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">prompted to restart the computer, click
<strong>OK</strong></span><br>
<span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">to complete the installation.
</span></li></ol>
<p><span style="font-family:arial,helvetica,sans-serif">&nbsp;</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><span style="font-family:arial,helvetica,sans-serif; font-size:xx-small">This project is used as a Windows Services and intends to listen multiple Message Queues (MSMQ) at the same time and handle the message content as the developer needs.</span></p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">            const string @namespace = &quot;MultipleQueueListener.Queues&quot;;

            var assembly = Assembly.LoadFrom(string.Format(@&quot;{0}\MultipleQueueListener.Queues.dll&quot;, Environment.CurrentDirectory));

            var type = typeof(IMessageQueueHandler);
            var assemblyClasses = assembly.GetTypes().ToList().Where(p =&gt; p.IsClass &amp;&amp; p.Namespace == @namespace &amp;&amp; type.IsAssignableFrom(p));

            foreach (var queue in assemblyClasses.Select(@class =&gt; assembly.CreateInstance(string.Format(&quot;{0}.{1}&quot;, @class.Namespace, @class.Name)) as IMessageQueueHandler))
            {
                if (queue != null)
                {
                    Console.WriteLine(string.Format(&quot;Start reading {0} queue...&quot;, queue.ToString()));
                    var thread = new Thread(new ThreadStart(queue.StartRead));
                    thread.Start();
                }
                else               
                    throw new ArgumentNullException();
            }</pre>
<div class="preview">
<pre class="csharp">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">const</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;@<span class="cs__keyword">namespace</span>&nbsp;=&nbsp;<span class="cs__string">&quot;MultipleQueueListener.Queues&quot;</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;assembly&nbsp;=&nbsp;Assembly.LoadFrom(<span class="cs__keyword">string</span>.Format(@<span class="cs__string">&quot;{0}\MultipleQueueListener.Queues.dll&quot;</span>,&nbsp;Environment.CurrentDirectory));&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;type&nbsp;=&nbsp;<span class="cs__keyword">typeof</span>(IMessageQueueHandler);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;assemblyClasses&nbsp;=&nbsp;assembly.GetTypes().ToList().Where(p&nbsp;=&gt;&nbsp;p.IsClass&nbsp;&amp;&amp;&nbsp;p.Namespace&nbsp;==&nbsp;@<span class="cs__keyword">namespace</span>&nbsp;&amp;&amp;&nbsp;type.IsAssignableFrom(p));&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">foreach</span>&nbsp;(var&nbsp;queue&nbsp;<span class="cs__keyword">in</span>&nbsp;assemblyClasses.Select(@<span class="cs__keyword">class</span>&nbsp;=&gt;&nbsp;assembly.CreateInstance(<span class="cs__keyword">string</span>.Format(<span class="cs__string">&quot;{0}.{1}&quot;</span>,&nbsp;@<span class="cs__keyword">class</span>.Namespace,&nbsp;@<span class="cs__keyword">class</span>.Name))&nbsp;<span class="cs__keyword">as</span>&nbsp;IMessageQueueHandler))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(queue&nbsp;!=&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__keyword">string</span>.Format(<span class="cs__string">&quot;Start&nbsp;reading&nbsp;{0}&nbsp;queue...&quot;</span>,&nbsp;queue.ToString()));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;thread&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Thread(<span class="cs__keyword">new</span>&nbsp;ThreadStart(queue.StartRead));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;thread.Start();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">throw</span>&nbsp;<span class="cs__keyword">new</span>&nbsp;ArgumentNullException();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em><em>MultipleQueueListener.csproj</em></em> </li><li>MultipleQueueListener.Interfaces.csproj<em></em> </li><li>MultipleQueueListener.Queues.csproj </li></ul>
