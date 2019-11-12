# Progress of a Task in C#
## Requires
- Visual Studio 2012
## License
- Apache License, Version 2.0
## Technologies
- C# Language
## Topics
- backgroundworker
- Task
## Updated
- 07/14/2014
## Description

<div style="text-align:justify">By default <a href="http://msdn.microsoft.com/en-us/library/system.threading.tasks.task.aspx" target="_blank">
Task</a> doesn&rsquo;t report its progress as a <a href="http://msdn.microsoft.com/en-us/library/system.componentmodel.backgroundworker.aspx" target="_blank">
BackgroundWorker</a> does. But that doesn&rsquo;t mean we can&rsquo;t get a progress of a Task. There is a new interface which was introduced with .NET framework 4.5 which is
<a href="http://msdn.microsoft.com/en-us/library/hh138298.aspx" target="_blank">IProgress&lt;T&gt;</a>. This interface exposes a Report(T) method, which the async task calls to report progress.</div>
<div style="text-align:justify">&nbsp;</div>
<div style="text-align:justify">Let&rsquo;s go by an example. I have the following async method.</div>
<div id="codeSnippetWrapper" style="overflow:auto; font-size:8pt; border:1px solid silver; font-family:'Courier New',courier,monospace; width:97.5%; padding:4px; direction:ltr; margin:20px 0px 10px; line-height:12pt; max-height:1000px; background-color:#f4f4f4">
<div id="codeSnippet" style="border-style:none; overflow:visible; font-size:8pt; width:100%; color:black; padding:0px; direction:ltr; line-height:12pt">
<pre style="border-style:none; overflow:visible; font-size:8pt; font-family:'Courier New',courier,monospace; width:100%; padding:0px; direction:ltr; margin:0em; line-height:12pt; background-color:white">async Task MyMethodAsync(<span style="color:blue">int</span> sleepTime, IProgress&lt;MyTaskProgressReport&gt; progress)</pre>
<pre style="border-style:none; overflow:visible; font-size:8pt; font-family:'Courier New',courier,monospace; width:100%; padding:0px; direction:ltr; margin:0em; line-height:12pt">{</pre>
<pre style="border-style:none; overflow:visible; font-size:8pt; font-family:'Courier New',courier,monospace; width:100%; padding:0px; direction:ltr; margin:0em; line-height:12pt; background-color:white">    <span style="color:blue">int</span> totalAmount = 10000;<span style="font-size:8pt; line-height:12pt; background-color:#f4f4f4">&nbsp;</span></pre>
<br>
<pre style="border-style:none; overflow:visible; font-size:8pt; font-family:'Courier New',courier,monospace; width:100%; padding:0px; direction:ltr; margin:0em; line-height:12pt; background-color:white">    <span style="color:blue">for</span> (<span style="color:blue">int</span> i = 0; i &lt;= totalAmount;)</pre>
<pre style="border-style:none; overflow:visible; font-size:8pt; font-family:'Courier New',courier,monospace; width:100%; padding:0px; direction:ltr; margin:0em; line-height:12pt">    {</pre>
<pre style="border-style:none; overflow:visible; font-size:8pt; font-family:'Courier New',courier,monospace; width:100%; padding:0px; direction:ltr; margin:0em; line-height:12pt; background-color:white">        await Task.Delay(sleepTime);</pre>
<pre style="border-style:none; overflow:visible; font-size:8pt; font-family:'Courier New',courier,monospace; width:100%; padding:0px; direction:ltr; margin:0em; line-height:12pt">        progress.Report(<span style="color:blue">new</span> MyTaskProgressReport { CurrentProgressAmount = i, TotalProgressAmount = totalAmount, CurrentProgressMessage = <span style="color:blue">string</span>.Format(<span style="color:#006080">&quot;On {0} Message&quot;</span>, i) });</pre>
<pre style="border-style:none; overflow:visible; font-size:8pt; font-family:'Courier New',courier,monospace; width:100%; padding:0px; direction:ltr; margin:0em; line-height:12pt; background-color:white">        i = i &#43; sleepTime;</pre>
<pre style="border-style:none; overflow:visible; font-size:8pt; font-family:'Courier New',courier,monospace; width:100%; padding:0px; direction:ltr; margin:0em; line-height:12pt">    }</pre>
<pre style="border-style:none; overflow:visible; font-size:8pt; font-family:'Courier New',courier,monospace; width:100%; padding:0px; direction:ltr; margin:0em; line-height:12pt; background-color:white">}</pre>
</div>
</div>
<div style="text-align:justify">It takes a progress parameter which is a IProgress of type &ldquo;MyTaskProgressReport&rdquo; and here is my &ldquo;MyTaskProgressReport&rdquo; class.</div>
<div id="codeSnippetWrapper" style="overflow:auto; font-size:8pt; border:1px solid silver; font-family:'Courier New',courier,monospace; width:97.5%; padding:4px; direction:ltr; margin:20px 0px 10px; line-height:12pt; max-height:1000px; background-color:#f4f4f4">
<span style="font-size:8pt; line-height:12pt; color:blue">public</span><span style="font-size:8pt; line-height:12pt; background-color:white">
</span><span style="font-size:8pt; line-height:12pt; color:blue">class</span><span style="font-size:8pt; line-height:12pt; background-color:white"> MyTaskProgressReport</span><br>
<div id="codeSnippet" style="border-style:none; overflow:visible; font-size:8pt; width:100%; color:black; padding:0px; direction:ltr; line-height:12pt">
<pre style="border-style:none; overflow:visible; font-size:8pt; font-family:'Courier New',courier,monospace; width:100%; padding:0px; direction:ltr; margin:0em; line-height:12pt">{</pre>
<pre style="border-style:none; overflow:visible; font-size:8pt; font-family:'Courier New',courier,monospace; width:100%; padding:0px; direction:ltr; margin:0em; line-height:12pt; background-color:white">   <span style="color:green">//current progress</span></pre>
<pre style="border-style:none; overflow:visible; font-size:8pt; font-family:'Courier New',courier,monospace; width:100%; padding:0px; direction:ltr; margin:0em; line-height:12pt">   <span style="color:blue">public</span> <span style="color:blue">int</span> CurrentProgressAmount { get; set; }</pre>
<pre style="border-style:none; overflow:visible; font-size:8pt; font-family:'Courier New',courier,monospace; width:100%; padding:0px; direction:ltr; margin:0em; line-height:12pt; background-color:white">   <span style="color:green">//total progress</span></pre>
<pre style="border-style:none; overflow:visible; font-size:8pt; font-family:'Courier New',courier,monospace; width:100%; padding:0px; direction:ltr; margin:0em; line-height:12pt">   <span style="color:blue">public</span> <span style="color:blue">int</span> TotalProgressAmount { get; set; }</pre>
<pre style="border-style:none; overflow:visible; font-size:8pt; font-family:'Courier New',courier,monospace; width:100%; padding:0px; direction:ltr; margin:0em; line-height:12pt; background-color:white">   <span style="color:green">//some message to pass to the UI of current progress</span></pre>
<pre style="border-style:none; overflow:visible; font-size:8pt; font-family:'Courier New',courier,monospace; width:100%; padding:0px; direction:ltr; margin:0em; line-height:12pt">   <span style="color:blue">public</span> <span style="color:blue">string</span> CurrentProgressMessage { get; set; }</pre>
<pre style="border-style:none; overflow:visible; font-size:8pt; font-family:'Courier New',courier,monospace; width:100%; padding:0px; direction:ltr; margin:0em; line-height:12pt; background-color:white">}</pre>
</div>
</div>
<p><span style="text-align:justify">To simulate a time taking task, inside my async method, I have a For loop and inside it I have a Task Delay. In every iteration, progress is reported to the caller.</span><span style="text-align:justify">&nbsp;Now let&rsquo;
 see how UI captures this.</span><br>
<br>
</p>
<div style="text-align:justify">On the UI thread, we have to define an event handler Action&lt;T&gt;, which will be called when IProgress&lt;T&gt;.Report is invoked.</div>
<div id="codeSnippetWrapper" style="overflow:auto; font-size:8pt; border:1px solid silver; font-family:'Courier New',courier,monospace; width:97.5%; padding:4px; direction:ltr; margin:20px 0px 10px; line-height:12pt; max-height:1000px; background-color:#f4f4f4">
<span style="font-size:8pt; line-height:12pt; color:blue">private</span><span style="font-size:8pt; line-height:12pt; background-color:white">
</span><span style="font-size:8pt; line-height:12pt; color:blue">void</span><span style="font-size:8pt; line-height:12pt; background-color:white"> ReportProgress(MyTaskProgressReport progress)</span><br>
<div id="codeSnippet" style="border-style:none; overflow:visible; font-size:8pt; width:100%; color:black; padding:0px; direction:ltr; line-height:12pt">
<pre style="border-style:none; overflow:visible; font-size:8pt; font-family:'Courier New',courier,monospace; width:100%; padding:0px; direction:ltr; margin:0em; line-height:12pt">{</pre>
<pre style="border-style:none; overflow:visible; font-size:8pt; font-family:'Courier New',courier,monospace; width:100%; padding:0px; direction:ltr; margin:0em; line-height:12pt; background-color:white">    label1.Text = progress.CurrentProgressMessage;</pre>
<pre style="border-style:none; overflow:visible; font-size:8pt; font-family:'Courier New',courier,monospace; width:100%; padding:0px; direction:ltr; margin:0em; line-height:12pt">    textBox1.Text = <span style="color:blue">string</span>.Format(<span style="color:#006080">&quot;{0} out of {1}&quot;</span>, progress.CurrentProgressAmount, progress.TotalProgressAmount);</pre>
<pre style="border-style:none; overflow:visible; font-size:8pt; font-family:'Courier New',courier,monospace; width:100%; padding:0px; direction:ltr; margin:0em; line-height:12pt; background-color:white">}</pre>
</div>
</div>
<p>Now I am going to call my async method. I have created a Progress&lt;T&gt; instance and invoked the async method, which is triggered by a button click.</p>
<div id="codeSnippetWrapper" style="overflow:auto; font-size:8pt; border:1px solid silver; font-family:'Courier New',courier,monospace; width:97.5%; padding:4px; direction:ltr; margin:20px 0px 10px; line-height:12pt; max-height:1000px; background-color:#f4f4f4">
<span style="font-size:8pt; line-height:12pt; color:blue">private</span><span style="font-size:8pt; line-height:12pt; background-color:white"> async
</span><span style="font-size:8pt; line-height:12pt; color:blue">void</span><span style="font-size:8pt; line-height:12pt; background-color:white"> button1_Click(</span><span style="font-size:8pt; line-height:12pt; color:blue">object</span><span style="font-size:8pt; line-height:12pt; background-color:white">
 sender, EventArgs e)</span><br>
<div id="codeSnippet" style="border-style:none; overflow:visible; font-size:8pt; width:100%; color:black; padding:0px; direction:ltr; line-height:12pt">
<pre style="border-style:none; overflow:visible; font-size:8pt; font-family:'Courier New',courier,monospace; width:100%; padding:0px; direction:ltr; margin:0em; line-height:12pt">{</pre>
<pre style="border-style:none; overflow:visible; font-size:8pt; font-family:'Courier New',courier,monospace; width:100%; padding:0px; direction:ltr; margin:0em; line-height:12pt; background-color:white">    var progressIndicator = <span style="color:blue">new</span> Progress&lt;MyTaskProgressReport&gt;(ReportProgress);</pre>
<pre style="border-style:none; overflow:visible; font-size:8pt; font-family:'Courier New',courier,monospace; width:100%; padding:0px; direction:ltr; margin:0em; line-height:12pt">    await MyMethodAsync(1000, progressIndicator);</pre>
<pre style="border-style:none; overflow:visible; font-size:8pt; font-family:'Courier New',courier,monospace; width:100%; padding:0px; direction:ltr; margin:0em; line-height:12pt; background-color:white">}</pre>
</div>
</div>
<p>Here is the output,</p>
<table class="tr-caption-container" cellspacing="0" cellpadding="0" align="center" style="text-align:center; margin-left:auto; margin-right:auto">
<tbody>
<tr>
<td><a href="http://lh5.ggpht.com/-zCBWB6mqjos/UeLQ5TLtFZI/AAAAAAAABrM/Ep5hJh1rUio/s1600-h/image%25255B2%25255D.png" style="margin-left:auto; margin-right:auto"><img title="image" src="-image_thumb.png?imgmax=800" alt="image" width="158" height="158" style="border-width:0px; padding-top:0px; padding-left:0px; display:inline; padding-right:0px; border-style:solid"></a></td>
</tr>
<tr>
<td class="tr-caption">Result</td>
</tr>
</tbody>
</table>
