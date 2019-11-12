# Multithreading Dictionary<>
## Requires
- Visual Studio 2010
## License
- Apache License, Version 2.0
## Technologies
- C#
- .NET
- Parallel Programming
- .NET Framework 4.0
- C# Language
- Async
- Task Parallel Library
- .NET Framework 4.5
- .NET Framwork
- C# 3.0
- .NET 4.5
## Topics
- Multithreading
- threading
- Background thread
- Threads
## Updated
- 06/23/2013
## Description

<h1>Introduction</h1>
<p><em><span style="font:16px/24px Georgia,&quot;Bitstream Charter&quot;,serif; color:#333333; text-transform:none; text-indent:0px; letter-spacing:normal; word-spacing:0px; float:none; display:inline!important; white-space:normal; background-color:#ffffff">The sample
 project highlights differences between Dictionary&lt;&gt;, ConcurrentDictionary&lt;&gt; and using Dictionary&lt;&gt; in combination with a lock.&nbsp; Issues arise with these data structures in a multithreaded scenario when care is not taken when adding and
 updating entries.&nbsp; Namely, unpredictable results.&nbsp; This was original posted from
<a href="http://spikesoftware.azurewebsites.net/?p=41">Spike Software</a>.</span></em></p>
<h1><span>Building the Sample</span></h1>
<p><em>There are not any special requirements for building this example project.</em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p>The sample project creates&nbsp;three dictionaries: two Dictionary&lt;string, <em>
model</em>&gt; and one ConcurrentDictionary&lt;string, <em>model</em>&gt;.&nbsp; The different between the two basic dictionaries is that a lock() is used when updating or adding to the dictionary.</p>
<h2>Repository Setup</h2>
<p>As part of this exploration, I have defined a simple class to collect the number of times the class has been updated.&nbsp; For simplicity I made these static dictionaries as shown below:</p>
<div id="codeSnippetWrapper" style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:20px 0px 10px; width:97.5%; font-family:'Courier New',courier,monospace; direction:ltr; max-height:200px; font-size:8pt; overflow:auto; border:silver 1px solid; padding:4px">
<div id="codeSnippet" style="text-align:left; line-height:12pt; background-color:#f4f4f4; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px"><span style="color:#0000ff">static</span> Dictionary&lt;<span style="color:#0000ff">string</span>, DataCountModel&gt; Dictionary { get; set; }</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px"><span style="color:#0000ff">static</span> Dictionary&lt;<span style="color:#0000ff">string</span>, DataCountModel&gt; LockedDictionary { get; set; }</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px"><span style="color:#0000ff">static</span> ConcurrentDictionary&lt;<span style="color:#0000ff">string</span>, DataCountModel&gt; ConcurrentDictionary { get; set; }</pre>
</div>
</div>
<p>With these three, I wanted to explore how these different dictionaries behave in a multithreaded solution.</p>
<p>Isolated in separate methods with equivalent functionality, I have an update to the model to increment its count each time the method is called.&nbsp; The method will either update or add depending on if the key to the dictionary has previously been seen.&nbsp;
 Also, I added a delay of 200 milliseconds to make the detection of threading issues easier.</p>
<h3>Update Dictionary&lt;string, DataCountModel&gt;</h3>
<div id="codeSnippetWrapper" style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:20px 0px 10px; width:97.5%; font-family:'Courier New',courier,monospace; direction:ltr; max-height:200px; font-size:8pt; overflow:auto; border:silver 1px solid; padding:4px">
<div id="codeSnippet" style="text-align:left; line-height:12pt; background-color:#f4f4f4; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px"><span style="color:#0000ff">public</span> <span style="color:#0000ff">void</span> UpdateModel(<span style="color:#0000ff">string</span> key)</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">{</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">    Thread.CurrentThread.Join(200);</pre>
&nbsp;
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">    <span style="color:#0000ff">if</span>(Dictionary.ContainsKey(key))</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">        Dictionary[key].Count&#43;&#43;;</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">    <span style="color:#0000ff">else</span></pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">        Dictionary.Add(key, <span style="color:#0000ff">new</span> DataCountModel { Count = 1 });</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">}</pre>
</div>
</div>
<h3>Update ConcurrentDictionary&lt;string, DataCountModel&gt;</h3>
<div id="codeSnippetWrapper" style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:20px 0px 10px; width:97.5%; font-family:'Courier New',courier,monospace; direction:ltr; max-height:200px; font-size:8pt; overflow:auto; border:silver 1px solid; padding:4px">
<div id="codeSnippet" style="text-align:left; line-height:12pt; background-color:#f4f4f4; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px"><span style="color:#0000ff">public</span> <span style="color:#0000ff">void</span> UpdateConcurrentDictionary(<span style="color:#0000ff">string</span> key)</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">{</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">    Thread.CurrentThread.Join(200);</pre>
&nbsp;
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">    ConcurrentDictionary.AddOrUpdate(key,</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">                                     s =&gt; <span style="color:#0000ff">new</span> DataCountModel {Count = 1},</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">                                     (s, model) =&gt;</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">                                         {</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">                                             model.Count&#43;&#43;;</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">                                             <span style="color:#0000ff">return</span> model;</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">                                         });</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">}</pre>
</div>
</div>
<h3>Update Dictionary&lt;string, DataCountModel&gt; with lock</h3>
<div id="codeSnippetWrapper" style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:20px 0px 10px; width:97.5%; font-family:'Courier New',courier,monospace; direction:ltr; max-height:200px; font-size:8pt; overflow:auto; border:silver 1px solid; padding:4px">
<div id="codeSnippet" style="text-align:left; line-height:12pt; background-color:#f4f4f4; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px"><span style="color:#0000ff">public</span> <span style="color:#0000ff">void</span> UpdateLockedModel(<span style="color:#0000ff">string</span> key)</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">{</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">    Thread.CurrentThread.Join(200);</pre>
&nbsp;
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">    <span style="color:#0000ff">lock</span> (_lock)</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">    {</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">        <span style="color:#0000ff">if</span> (LockedDictionary.ContainsKey(key))</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">            LockedDictionary[key].Count&#43;&#43;;</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">        <span style="color:#0000ff">else</span></pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">            LockedDictionary.Add(key, <span style="color:#0000ff">new</span> DataCountModel { Count = 1 });</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">    }</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">}</pre>
</div>
</div>
<h2>Test Setup</h2>
<p>I created test that will create 100 models and perform 30 updates to the models.&nbsp; Each update is launched as a separate thread so my test consists of 3000 threads and I expect every model to have a final count of 30.</p>
<div id="codeSnippetWrapper" style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:20px 0px 10px; width:97.5%; font-family:'Courier New',courier,monospace; direction:ltr; max-height:200px; font-size:8pt; overflow:auto; border:silver 1px solid; padding:4px">
<div id="codeSnippet" style="text-align:left; line-height:12pt; background-color:#f4f4f4; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">List&lt;Task&lt;TimeSpan&gt;&gt; dictionaryTasks = <span style="color:#0000ff">new</span> List&lt;Task&lt;TimeSpan&gt;&gt;();</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">List&lt;Task&lt;TimeSpan&gt;&gt; lockedTasks = <span style="color:#0000ff">new</span> List&lt;Task&lt;TimeSpan&gt;&gt;();</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">List&lt;Task&lt;TimeSpan&gt;&gt; concurrencyTasks = <span style="color:#0000ff">new</span> List&lt;Task&lt;TimeSpan&gt;&gt;();</pre>
&nbsp;
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px"><span style="color:#0000ff">for</span> (<span style="color:#0000ff">int</span> model = 0; model &lt; 100; model&#43;&#43;)</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">{</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">    <span style="color:#0000ff">string</span> key = <span style="color:#0000ff">string</span>.Format(<span style="color:#006080">&quot;Model:{0}&quot;</span>, model);</pre>
&nbsp;
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">    <span style="color:#0000ff">for</span> (<span style="color:#0000ff">int</span> thread = 0; thread &lt; 30; thread&#43;&#43;)</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">    {</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">        Task&lt;TimeSpan&gt; task = UpdateDictionaryModel(key);</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">        task.Start();</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">        dictionaryTasks.Add(task);</pre>
&nbsp;
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">        task = UpdateLockedDictionaryModel(key);</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">        task.Start();</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">        lockedTasks.Add(task);</pre>
&nbsp;
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">        task = UpdateConcurrentDictionaryModel(key);</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">        task.Start();</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">        concurrencyTasks.Add(task);</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">    }</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">}</pre>
</div>
</div>
<p>Below is an example of the UpdateDictionary step that launches a thread to perform a single update to the model.&nbsp;
<em>The other update methods are not shown but source code is available.</em></p>
<div id="codeSnippetWrapper" style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:20px 0px 10px; width:97.5%; font-family:'Courier New',courier,monospace; direction:ltr; max-height:200px; font-size:8pt; overflow:auto; border:silver 1px solid; padding:4px">
<div id="codeSnippet" style="text-align:left; line-height:12pt; background-color:#f4f4f4; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px"><span style="color:#0000ff">public</span> Task&lt;TimeSpan&gt; UpdateDictionaryModel(<span style="color:#0000ff">string</span> key)</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">{</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">    <span style="color:#0000ff">return</span> <span style="color:#0000ff">new</span> Task&lt;TimeSpan&gt;(() =&gt;</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">    {</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">        ListRepository repository = <span style="color:#0000ff">new</span> ListRepository();</pre>
&nbsp;
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">        Random r = <span style="color:#0000ff">new</span> Random();</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">        Thread.CurrentThread.Join(r.Next(200));</pre>
&nbsp;
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">        var startTime = DateTime.Now;</pre>
&nbsp;
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">        repository.UpdateModel(key);</pre>
&nbsp;
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">        <span style="color:#0000ff">return</span> DateTime.Now.Subtract(startTime);</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">    });</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">}</pre>
</div>
</div>
<p>&nbsp; After the threads are launched, I've introduced a wait loop until all tasks have finished.&nbsp;
<em>Note: I was surprised by the behaviour of WaitAll(), as indicated below, so if there is a clever C#er out there who has a more elegant mechanism than the wait loop please post the solution as a comment.</em></p>
<div id="codeSnippetWrapper" style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:20px 0px 10px; width:97.5%; font-family:'Courier New',courier,monospace; direction:ltr; max-height:200px; font-size:8pt; overflow:auto; border:silver 1px solid; padding:4px">
<div id="codeSnippet" style="text-align:left; line-height:12pt; background-color:#f4f4f4; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px"><span style="color:#008000">// waitall will throw an aggregate exception for each failed task</span></pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px"><span style="color:#008000">// Task&lt;TimeSpan&gt;.WaitAll(tasks.ToArray());</span></pre>
&nbsp;
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px"><span style="color:#0000ff">while</span> (dictionaryTasks.Any(t =&gt; t.Status == TaskStatus.Running)</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">       || lockedTasks.Any(t =&gt; t.Status == TaskStatus.Running)</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">       || concurrencyTasks.Any(t =&gt; t.Status == TaskStatus.Running))</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">{</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">    Thread.CurrentThread.Join(200);</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">}</pre>
</div>
</div>
<p>&nbsp; For simplicity, I posted some basic validations to the debug window.</p>
<div id="codeSnippetWrapper" style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:20px 0px 10px; width:97.5%; font-family:'Courier New',courier,monospace; direction:ltr; max-height:200px; font-size:8pt; overflow:auto; border:silver 1px solid; padding:4px">
<div id="codeSnippet" style="text-align:left; line-height:12pt; background-color:#f4f4f4; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">var dictionaryModels = repository.GetDictionaryModels();</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">var lockedModels = repository.GetLockedDictionaryModels();</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">var concurrentModels = repository.GetConcurrentDictionaryModels();</pre>
&nbsp;
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">Debug.WriteLine(<span style="color:#006080">&quot;100 keys were created.&quot;</span>);</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">Debug.WriteLine(<span style="color:#0000ff">string</span>.Format(<span style="color:#006080">&quot;\tDictionary Model Count:\t\t\t\t{0}&quot;</span>, dictionaryModels.Count));</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">Debug.WriteLine(<span style="color:#0000ff">string</span>.Format(<span style="color:#006080">&quot;\tLocked Dictionary Model Count:\t\t{0}&quot;</span>, lockedModels.Count));</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">Debug.WriteLine(<span style="color:#0000ff">string</span>.Format(<span style="color:#006080">&quot;\tConcurrent Dictionary Model Count:\t{0}&quot;</span>, concurrentModels.Count));</pre>
&nbsp;
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">Debug.WriteLine(<span style="color:#006080">&quot;Each key was updated 30 times.&quot;</span>);</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">Debug.WriteLine(<span style="color:#0000ff">string</span>.Format(<span style="color:#006080">&quot;\tAre all Dictionary Models set to 30:\t\t\t\t{0}&quot;</span>, dictionaryModels.All(m =&gt; m.Count == 30)));</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">Debug.WriteLine(<span style="color:#0000ff">string</span>.Format(<span style="color:#006080">&quot;\tAre all Locked Dictionary Models set to 30:\t\t\t{0}&quot;</span>, lockedModels.All(m =&gt; m.Count == 30)));</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">Debug.WriteLine(<span style="color:#0000ff">string</span>.Format(<span style="color:#006080">&quot;\tAre all Concurrent Dictionary Models set to 30:\t\t{0}&quot;</span>, concurrentModels.All(m =&gt; m.Count == 30)));</pre>
&nbsp;
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">Debug.WriteLine(<span style="color:#006080">&quot;Roughly speaking, what is the average elapsed time for an update to each model.&quot;</span>);</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">Debug.WriteLine(<span style="color:#0000ff">string</span>.Format(<span style="color:#006080">&quot;\tAverage update for Dictionary Models:\t\t\t\t{0}&quot;</span>, dictionaryTasks.Where(t =&gt; t.IsCompleted &amp;&amp; !t.IsFaulted).Average(t =&gt; t.Result.TotalMilliseconds)));</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">Debug.WriteLine(<span style="color:#0000ff">string</span>.Format(<span style="color:#006080">&quot;\tAverage update for Locked Dictionary Models:\t\t{0}&quot;</span>, lockedTasks.Average(t =&gt; t.Result.TotalMilliseconds)));</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">Debug.WriteLine(<span style="color:#0000ff">string</span>.Format(<span style="color:#006080">&quot;\tAverage update for Concurrent Dictionary Models:\t{0}&quot;</span>, concurrencyTasks.Average(t =&gt; t.Result.TotalMilliseconds)));</pre>
&nbsp;
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">Debug.WriteLine(<span style="color:#006080">&quot;How many tasks failed?&quot;</span>);</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">Debug.WriteLine(<span style="color:#0000ff">string</span>.Format(<span style="color:#006080">&quot;\tNumber of Dictionary Model Task Failures:\t\t\t\t{0}&quot;</span>, dictionaryTasks.Count(t =&gt; t.IsFaulted)));</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">Debug.WriteLine(<span style="color:#0000ff">string</span>.Format(<span style="color:#006080">&quot;\tNumber of Locked Dictionary Model Task Failures:\t\t{0}&quot;</span>, lockedTasks.Count(t =&gt; t.IsFaulted)));</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">Debug.WriteLine(<span style="color:#0000ff">string</span>.Format(<span style="color:#006080">&quot;\tNumber of Concurrent Dictionary Model Task Failures:\t{0}&quot;</span>, concurrencyTasks.Count(t =&gt; t.IsFaulted)));</pre>
</div>
</div>
<h1>Test Results</h1>
<p>First off, I wanted to make sure only 100 models were created.&nbsp; Both the Locked Dictionary and the Concurrent Dictionary pass this step.&nbsp; The two additional models that were created relate to two times were a threads checked the dictionary for
 the existance of a key and when not finding one attempted an insert.&nbsp; This was because another thread had inserted the key between lookup and the insert.&nbsp; The lock and the ConcurrentDictionary&lt;&gt; prevent this from happening.</p>
<blockquote>100 keys were created. Dictionary Model Count:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<span style="color:#ff0000">102</span> Locked Dictionary Model Count:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 100 Concurrent Dictionary Model Count:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 100</blockquote>
<p>To me the most important aspect is verifying the results are accurate, only the heavy-handed lock passes this test.&nbsp; This indicates the ConcurrentDictionary&lt;&gt; is thread safe for interacting with the dictionary object but not with the entities.</p>
<blockquote>Each key was updated 30 times. Are all Dictionary Models set to 30:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<span style="color:#ff0000">False</span> Are all Locked Dictionary Models set to 30:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; True Are all Concurrent Dictionary Models set to 30:&nbsp;
<span style="color:#ff0000">False</span></blockquote>
<p>Though not a thorough measurement, I wanted to get some indication of performance.&nbsp; The performance of the different dictionaries did not vary significantly (~1 millisecond).</p>
<blockquote>Roughly speaking, what is the average elapsed time for an update to each model. Average update for Dictionary Models:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 211.375291861241 Average update for Locked Dictionary Models:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 210.578125 Average update for Concurrent Dictionary Models:&nbsp; 210.536458333333</blockquote>
<p>And lastly, I wanted some measurement of if the tasks completed normally without throwing an exception.&nbsp; As expected, 2 of the dictionary&lt;&gt; related tasks failed because of duplicate keys.</p>
<blockquote>How many tasks failed? Number of Dictionary Model Task Failures:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<span style="color:#ff0000">2</span> Number of Locked Dictionary Model Task Failures:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 0 Number of Concurrent Dictionary Model Task Failures:&nbsp; 0</blockquote>
<h1>Summary</h1>
<p>No surprises really in this post when you think about it.&nbsp; Microsoft even documented this when you thoroughly read the remarks in MSDN:</p>
<blockquote>All these operations are atomic and are thread-safe with regards to all other operations on the ConcurrentDictionary&lt;TKey, TValue&gt; class. The only exceptions are the methods that accept a delegate, that is, AddOrUpdate and GetOrAdd. For modifications
 and write operations to the dictionary, ConcurrentDictionary&lt;TKey, TValue&gt; uses fine-grained locking to ensure thread safety. (Read operations on the dictionary are performed in a lock-free manner.) However, delegates for these methods are called outside
 the locks to avoid the problems that can arise from executing unknown code under a lock.
<span style="color:#0000ff">Therefore, the code executed by these delegates is not subject to the atomicity of the operation.</span></blockquote>
<p>With the last line of the remarks above in mind, the following change to the ConcurrentDictionary&lt;&gt;.AddOrUpdate will make it accurrate for my test scenario.&nbsp; Basically, a lock is placed when updating the model.</p>
<div id="codeSnippetWrapper" style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:20px 0px 10px; width:97.5%; font-family:'Courier New',courier,monospace; direction:ltr; max-height:200px; font-size:8pt; overflow:auto; border:silver 1px solid; padding:4px">
<div id="codeSnippet" style="text-align:left; line-height:12pt; background-color:#f4f4f4; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">ConcurrentDictionary.AddOrUpdate(key,</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">                                 s =&gt; <span style="color:#0000ff">new</span> DataCountModel {Count = 1},</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">                                 (s, model) =&gt;</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">                                                 {</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">                                                     <span style="color:#0000ff">lock</span>(_lock)</pre>
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">                                                        model.Count&#43;&#43;;</pre>
&nbsp;
<pre style="text-align:left; line-height:12pt; background-color:#f4f4f4; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">                                                     <span style="color:#0000ff">return</span> model;</pre>
<pre style="text-align:left; line-height:12pt; background-color:white; margin:0em; width:100%; font-family:'Courier New',courier,monospace; direction:ltr; color:black; font-size:8pt; overflow:visible; border-style:none; padding:0px">                                                 });</pre>
</div>
</div>
<p>&nbsp;</p>
