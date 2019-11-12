# Producer Consumer inter-thread communication using condition variables
## Requires
- Visual Studio 2012
## License
- Apache License, Version 2.0
## Technologies
- C++
## Topics
- threading
- Parallel Programming
## Updated
- 03/26/2013
## Description

<h1>Introduction</h1>
<div style="font-size:small">
<p>In this example we show inter-thread communication between producer and consumer threads using
<span style="color:#808000">condition_variable</span>.&nbsp; In the end two threads will ping-pong&nbsp;passing control to&nbsp;each other.</p>
<p>Code can be modified in such a way that producer will read and queue chunks of data from some source while consumer thread will process data from the queue. For example, we could be reading some data from a network stream&nbsp;in chunks adding them to a
 queue while another thread will be reading and removing data one chunk at a time from the queue as long as the queue is not empty, otherwise it will block until new data is queued. In that scenario, ping-ponging will not be required as consumer should be processing
 data chunks independently of producer queuing it in.</p>
<p>The Standard C&#43;&#43; 11 Library provides two implementations of a condition variable:
<span style="color:#808000">condition_variable </span>and <span style="color:#808000">
condition_variable_any</span>, - both declared in &lt;condition_variable&gt; header file. condition_variable_any is a &nbsp;more general implementation of two and could incur some overhead compared to its brethren, it should be avoided unless additional flexibility
 is required. condition_variable must be used in conjunction with a <span style="color:#808000">
mutex </span>in order to provide synchronization while condition_variable_any does not have such restriction (it still requires some &lsquo;mutex-like&rsquo; synchronization primitive).</p>
<p>First, let&rsquo;s create a producer thread which is responsible for reading data from some data source, a network stream for example, and queuing it for another thread to process it.</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C&#43;&#43;</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">cplusplus</span>

<div class="preview">
<pre class="cplusplus"><span class="cpp__keyword">thread</span>&nbsp;producer([&amp;m_mutex,&nbsp;&amp;m_queue,&nbsp;&amp;m_alarm,&nbsp;&amp;m_isNotified,&nbsp;&amp;m_haveData]()&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//.&nbsp;.&nbsp;.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;lock_guard&lt;mutex&gt;&nbsp;lock(m_mutex);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//m_queue.push(i);&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_isNotified&nbsp;=&nbsp;<span class="cpp__keyword">true</span>;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_alarm.notify_one();&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//.&nbsp;.&nbsp;.</span>&nbsp;
});&nbsp;
</pre>
</div>
</div>
</div>
<p>We can now create a consumer thread which first has to lock the mutex. Unlike in producer, we have to use unique_lock and not lock_guard because the waiting consumer thread must unlock the mutex when it receives notification and then lock it again. lock_guard
 does not allow that.</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C&#43;&#43;</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">cplusplus</span>

<div class="preview">
<pre class="js">thread&nbsp;consumer([&amp;m_mutex,&nbsp;&amp;m_queue,&nbsp;&amp;m_alarm,&nbsp;&amp;m_isNotified,&nbsp;&amp;m_haveData]()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">while</span>&nbsp;(m_haveData)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;unique_lock&lt;mutex&gt;&nbsp;lock(m_mutex);&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">while</span>&nbsp;(!m_isNotified)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_alarm.wait(lock);&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//.&nbsp;.&nbsp;.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
<span class="js__brace">}</span>);&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">Note that in the consumer thread we are guarding against
<a title="spurious wakeup" href="http://en.wikipedia.org/wiki/Spurious_wakeup" target="_blank">
spurious wakeup</a> in the following loop, please read about it at the provided link:</div>
<div class="endscriptcode"></div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C&#43;&#43;</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">cplusplus</span>

<div class="preview">
<pre class="js"><span class="js__statement">while</span>&nbsp;(!m_isNotified)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;m_alarm.wait(lock);&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<span class="js__brace">}</span>&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</div>
<p>&nbsp;</p>
<p>Here's full source code listing:</p>
</div>
<div style="font-size:small">
<div class="endscriptcode" style="font-size:small"></div>
<div class="endscriptcode" style="font-size:small">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C&#43;&#43;</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">cplusplus</span>

<div class="preview">
<pre class="cplusplus"><span class="cpp__com">//-----------------------------------------------------------------------------</span>&nbsp;
<span class="cpp__com">//&nbsp;File:&nbsp;Program.h</span>&nbsp;
<span class="cpp__com">//</span>&nbsp;
<span class="cpp__com">//&nbsp;Desc:&nbsp;Demonstrates&nbsp;inter-thread&nbsp;communication&nbsp;using&nbsp;condition&nbsp;variables.</span>&nbsp;
<span class="cpp__com">//&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Producer&nbsp;and&nbsp;consumer&nbsp;threads&nbsp;will&nbsp;ping-pong&nbsp;between&nbsp;each&nbsp;other.</span>&nbsp;
<span class="cpp__com">//&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Code&nbsp;can&nbsp;be&nbsp;modified&nbsp;in&nbsp;such&nbsp;a&nbsp;way&nbsp;that&nbsp;producer&nbsp;will&nbsp;read&nbsp;and&nbsp;queue&nbsp;</span>&nbsp;
<span class="cpp__com">//&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;chunks&nbsp;of&nbsp;data&nbsp;from&nbsp;some&nbsp;source,&nbsp;network&nbsp;stream&nbsp;for&nbsp;example,&nbsp;while</span>&nbsp;
<span class="cpp__com">//&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;consumer&nbsp;thread&nbsp;will&nbsp;process&nbsp;data&nbsp;from&nbsp;the&nbsp;queue.&nbsp;In&nbsp;that&nbsp;scenario,</span>&nbsp;
<span class="cpp__com">//&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ping-ponging&nbsp;will&nbsp;not&nbsp;be&nbsp;required&nbsp;as&nbsp;consumer&nbsp;should&nbsp;be&nbsp;disposing&nbsp;of&nbsp;data</span>&nbsp;
<span class="cpp__com">//&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;independently&nbsp;of&nbsp;producer&nbsp;queuing&nbsp;it.</span>&nbsp;
<span class="cpp__com">//</span>&nbsp;
<span class="cpp__com">//-----------------------------------------------------------------------------</span><span class="cpp__preproc">&nbsp;
&nbsp;
#include&nbsp;&lt;chrono&gt;</span><span class="cpp__preproc">&nbsp;
#include&nbsp;&lt;condition_variable&gt;</span><span class="cpp__preproc">&nbsp;
#include&nbsp;&lt;iostream&gt;</span><span class="cpp__preproc">&nbsp;
#include&nbsp;&lt;mutex&gt;</span><span class="cpp__preproc">&nbsp;
#include&nbsp;&lt;queue&gt;</span><span class="cpp__preproc">&nbsp;
#include&nbsp;&lt;thread&gt;</span>&nbsp;
&nbsp;
<span class="cpp__keyword">using</span>&nbsp;std::cout;&nbsp;
<span class="cpp__keyword">using</span>&nbsp;std::endl;&nbsp;
<span class="cpp__keyword">using</span>&nbsp;std::condition_variable;&nbsp;
<span class="cpp__keyword">using</span>&nbsp;std::lock_guard;&nbsp;
<span class="cpp__keyword">using</span>&nbsp;std::unique_lock;&nbsp;
<span class="cpp__keyword">using</span>&nbsp;std::mutex;&nbsp;
<span class="cpp__keyword">using</span>&nbsp;std::queue;&nbsp;
<span class="cpp__keyword">using</span>&nbsp;std::<span class="cpp__keyword">thread</span>;&nbsp;
<span class="cpp__keyword">using</span>&nbsp;std::chrono::milliseconds;&nbsp;
<span class="cpp__keyword">using</span>&nbsp;std::this_thread::sleep_for;&nbsp;
&nbsp;
<span class="cpp__keyword">static</span>&nbsp;<span class="cpp__keyword">const</span>&nbsp;<span class="cpp__datatype">int</span>&nbsp;MagicNumber&nbsp;=&nbsp;<span class="cpp__number">30</span>;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;Magic&nbsp;number&nbsp;used&nbsp;for&nbsp;the&nbsp;sample,&nbsp;remove&nbsp;it&nbsp;for&nbsp;production&nbsp;code</span>&nbsp;
&nbsp;
<span class="cpp__datatype">int</span>&nbsp;main()&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;condition_variable&nbsp;&nbsp;&nbsp;&nbsp;m_alarm;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;Notifies&nbsp;threads&nbsp;that&nbsp;more&nbsp;work&nbsp;is&nbsp;available</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;mutex&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_mutex;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;Synchronizes&nbsp;access&nbsp;to&nbsp;shared&nbsp;variables</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;queue&lt;<span class="cpp__datatype">int</span>&gt;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_queue;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;Accumulates&nbsp;data&nbsp;chunks</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__datatype">bool</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_isNotified&nbsp;=&nbsp;<span class="cpp__keyword">false</span>;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;This&nbsp;is&nbsp;a&nbsp;guard&nbsp;to&nbsp;prevent&nbsp;accidental&nbsp;spurious&nbsp;wakeups</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__datatype">bool</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_haveData&nbsp;&nbsp;&nbsp;=&nbsp;<span class="cpp__keyword">true</span>;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;Only&nbsp;used&nbsp;for&nbsp;this&nbsp;sample&nbsp;to&nbsp;end&nbsp;consumer&nbsp;thread,&nbsp;not&nbsp;required&nbsp;in&nbsp;production&nbsp;code</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">thread</span>&nbsp;producer([&amp;m_mutex,&nbsp;&amp;m_queue,&nbsp;&amp;m_alarm,&nbsp;&amp;m_isNotified,&nbsp;&amp;m_haveData]()&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">for</span>&nbsp;(<span class="cpp__datatype">int</span>&nbsp;i&nbsp;=&nbsp;<span class="cpp__number">0</span>;&nbsp;i&nbsp;&lt;&nbsp;MagicNumber;&nbsp;&#43;&#43;i)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sleep_for(milliseconds(<span class="cpp__number">500</span>));&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;Executing&nbsp;some&nbsp;long&nbsp;operation</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;lock_guard&lt;mutex&gt;&nbsp;lock(m_mutex);&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;Enter&nbsp;critical&nbsp;section</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;cout&nbsp;&lt;&lt;&nbsp;<span class="cpp__string">&quot;producer&nbsp;&quot;</span>&nbsp;&lt;&lt;&nbsp;i&nbsp;&lt;&lt;&nbsp;endl;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_queue.push(i);&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;Add&nbsp;data&nbsp;chunk&nbsp;to&nbsp;the&nbsp;queue</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_isNotified&nbsp;=&nbsp;<span class="cpp__keyword">true</span>;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;Consumer&nbsp;can&nbsp;be&nbsp;woken&nbsp;up&nbsp;and&nbsp;it&nbsp;is&nbsp;not&nbsp;a&nbsp;fluke&nbsp;(see&nbsp;spurious&nbsp;wakeups)</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_alarm.notify_one();&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;Notify&nbsp;consumer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;lock_guard&lt;mutex&gt;&nbsp;lock(m_mutex);&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;Work&nbsp;is&nbsp;done,&nbsp;app&nbsp;can&nbsp;exit</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_isNotified&nbsp;=&nbsp;<span class="cpp__keyword">true</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_haveData&nbsp;=&nbsp;<span class="cpp__keyword">false</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_alarm.notify_one();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;});&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">thread</span>&nbsp;consumer([&amp;m_mutex,&nbsp;&amp;m_queue,&nbsp;&amp;m_alarm,&nbsp;&amp;m_isNotified,&nbsp;&amp;m_haveData]()&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">while</span>&nbsp;(m_haveData)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;In&nbsp;production,&nbsp;this&nbsp;check&nbsp;will&nbsp;be&nbsp;done&nbsp;on&nbsp;whether&nbsp;there&nbsp;is&nbsp;more&nbsp;data&nbsp;in&nbsp;the&nbsp;queue</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;unique_lock&lt;mutex&gt;&nbsp;lock(m_mutex);&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;Must&nbsp;aquire&nbsp;unique_lock&nbsp;with&nbsp;condition&nbsp;variables</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">while</span>&nbsp;(!m_isNotified)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;Prevents&nbsp;from&nbsp;spurious&nbsp;wakeup</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_alarm.wait(lock);&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;Wait&nbsp;for&nbsp;a&nbsp;signal&nbsp;from&nbsp;producer&nbsp;thread</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">while</span>&nbsp;(!m_queue.empty())&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;Process&nbsp;data&nbsp;and&nbsp;remove&nbsp;it&nbsp;from&nbsp;the&nbsp;queue</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;cout&nbsp;&lt;&lt;&nbsp;<span class="cpp__string">&quot;consumer&nbsp;&quot;</span>&nbsp;&lt;&lt;&nbsp;m_queue.front()&nbsp;&lt;&lt;&nbsp;endl;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_queue.pop();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_isNotified&nbsp;=&nbsp;<span class="cpp__keyword">false</span>;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;Protect&nbsp;from&nbsp;spurious&nbsp;wakeup</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;});&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__com">//&nbsp;Join&nbsp;threads&nbsp;and&nbsp;finish&nbsp;app</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;producer.join();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;consumer.join();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cpp__keyword">return</span>&nbsp;<span class="cpp__number">0</span>;&nbsp;
}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</div>
<div class="endscriptcode" style="font-size:small">
<h2><span style="font-size:small">Summary</span></h2>
<dl><dt>This is a very simple example of inter-thread communication using condition_variable.</dt></dl>
</div>
<div class="endscriptcode">&nbsp;</div>
</div>
<h2>Source Code Files</h2>
<dl><dt>program.ccp - the only file in the project &nbsp;</dt></dl>
<div>&nbsp;</div>
<dl></dl>
