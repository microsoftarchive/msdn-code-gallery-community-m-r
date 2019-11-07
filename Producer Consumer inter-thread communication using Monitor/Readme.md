# Producer Consumer inter-thread communication using Monitor
## Requires
- Visual Studio 2012
## License
- Apache License, Version 2.0
## Technologies
- C#
## Topics
- threading
- Parallel Programming
## Updated
- 03/26/2013
## Description

<h1>Introduction</h1>
<div style="font-size:small"><br>
<br>
<p>In this example we show inter-thread communication between producer and consumer threads using Monitor Wait and Pulse functions.&nbsp; In the end two threads will ping-pong&nbsp;passing control to&nbsp;each other.</p>
<p>Code can be modified in such a way that producer will read and queue chunks of data from some source while consumer thread will process data from the queue. For example, we could be reading some data from a network in chunks and we want to process data as
 soon as new chunk arrives. In that scenario, ping-ponging will not be required as consumer should be processing data chunks independently of producer queuing it.</p>
<p>First we have to acquire lock on a producer thread because we can only make notifications to other threads from a synchronized block of code. Next we have to call Monitor.Wait() to release the lock and to change thread state to waiting. Now we can add a
 data chunk we have received from our data source to the queue and pulse to another thread that it can wake up and start processing the data. We do that by calling Monitor.Pulse() which changes state of the waiting thread to ready. Once producer thread releases
 the lock, consumer thread acquires the lock and can process data. Then we pulse from the consumer back to producer.</p>
<p>In production we would want consumer to process data independently from the producer thread where the latter will be accumulating data in the queue while the consumer does something with the data.</p>
</div>
<div style="font-size:small">
<div class="endscriptcode" style="font-size:small"></div>
<div class="endscriptcode" style="font-size:small">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">/*
// Desc: Demonstrates inter-thread communication using Monitor.
//       Producer and consumer threads will ping-pong between each other.
//       Code can be modified in such a way that producer will read and queue 
//		 chunks of data from some source, network stream for example, while
//		 consumer thread will process data from the queue. In that scenario,
//		 ping-ponging will not be required as consumer should be disposing of data
//		 independently of producer queuing it.
*/

using System;
using System.Collections.Generic;
using System.Threading;

namespace Bisque
{
    public sealed class ProducerConsumer
    {
        const int MagicNumber = 30;                                 // Indicates how many times to bounce between ping and pong threads
        private Object m_lock = new Object();                       // Lock to protect counter increment
        private Queue&lt;int&gt; m_queue = new Queue&lt;int&gt;();

        // Ctor
        public ProducerConsumer()
        {
        }

        // Ping
        public void Producer()
        {
            int counter = 0;

            lock (m_lock)                                           // Allows only one thread at a time inside m_lock
            {
                while (counter &lt; MagicNumber)
                {
                    Thread.Sleep(500);                              // Get data chunks from some source
                    Monitor.Wait(m_lock);                           // Wait if the thread is busy. 'wait' will hold this loop until something else pulses it to release the wait.
                    Console.WriteLine(&quot;producer {0}&quot;, counter);
                    m_queue.Enqueue(counter);
                    Monitor.Pulse(m_lock);                          // Releases consumer thread

                    counter&#43;&#43;;
                }
            }
        }

        public void Consumer()
        {
            lock (m_lock)                                           // Allows only one thread at a time inside m_lock
            {
                Monitor.Pulse(m_lock);

                while (Monitor.Wait(m_lock, 1000))                  // Wait in the loop while producer is busy. Exit when producer times-out. 1000 = 1 second; app will hang without this time-out value
                {
                    int data = m_queue.Dequeue();
                    Console.WriteLine(&quot;consumer {0}&quot;, data);
                    Monitor.Pulse(m_lock);                          // Release consumer
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ProducerConsumer app = new ProducerConsumer();

            // Create 2 threads
            Thread t_producer = new Thread(new ThreadStart(app.Producer));
            Thread t_consumer = new Thread(new ThreadStart(app.Consumer));

            // Start threads
            t_producer.Start();
            t_consumer.Start();

            // Waith for the threads to complete
            t_producer.Join();
            t_consumer.Join();

            Console.WriteLine(&quot;\nPress any key to complete the program.\n&quot;);
            Console.ReadKey(false);
        }
    }
}

/*
 * Will print:

producer
consumer
producer
consumer
producer
consumer

Press any key to complete the program.
*/</pre>
<div class="preview">
<pre class="csharp"><span class="cs__mlcom">/*&nbsp;
//&nbsp;Desc:&nbsp;Demonstrates&nbsp;inter-thread&nbsp;communication&nbsp;using&nbsp;Monitor.&nbsp;
//&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Producer&nbsp;and&nbsp;consumer&nbsp;threads&nbsp;will&nbsp;ping-pong&nbsp;between&nbsp;each&nbsp;other.&nbsp;
//&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Code&nbsp;can&nbsp;be&nbsp;modified&nbsp;in&nbsp;such&nbsp;a&nbsp;way&nbsp;that&nbsp;producer&nbsp;will&nbsp;read&nbsp;and&nbsp;queue&nbsp;&nbsp;
//&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;chunks&nbsp;of&nbsp;data&nbsp;from&nbsp;some&nbsp;source,&nbsp;network&nbsp;stream&nbsp;for&nbsp;example,&nbsp;while&nbsp;
//&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;consumer&nbsp;thread&nbsp;will&nbsp;process&nbsp;data&nbsp;from&nbsp;the&nbsp;queue.&nbsp;In&nbsp;that&nbsp;scenario,&nbsp;
//&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ping-ponging&nbsp;will&nbsp;not&nbsp;be&nbsp;required&nbsp;as&nbsp;consumer&nbsp;should&nbsp;be&nbsp;disposing&nbsp;of&nbsp;data&nbsp;
//&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;independently&nbsp;of&nbsp;producer&nbsp;queuing&nbsp;it.&nbsp;
*/</span>&nbsp;
&nbsp;
<span class="cs__keyword">using</span>&nbsp;System;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Collections.Generic;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Threading;&nbsp;
&nbsp;
<span class="cs__keyword">namespace</span>&nbsp;Bisque&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">sealed</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;ProducerConsumer&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">const</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;MagicNumber&nbsp;=&nbsp;<span class="cs__number">30</span>;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Indicates&nbsp;how&nbsp;many&nbsp;times&nbsp;to&nbsp;bounce&nbsp;between&nbsp;ping&nbsp;and&nbsp;pong&nbsp;threads</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;Object&nbsp;m_lock&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Object();&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Lock&nbsp;to&nbsp;protect&nbsp;counter&nbsp;increment</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;Queue&lt;<span class="cs__keyword">int</span>&gt;&nbsp;m_queue&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Queue&lt;<span class="cs__keyword">int</span>&gt;();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Ctor</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;ProducerConsumer()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Ping</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Producer()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;counter&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">lock</span>&nbsp;(m_lock)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Allows&nbsp;only&nbsp;one&nbsp;thread&nbsp;at&nbsp;a&nbsp;time&nbsp;inside&nbsp;m_lock</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">while</span>&nbsp;(counter&nbsp;&lt;&nbsp;MagicNumber)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Thread.Sleep(<span class="cs__number">500</span>);&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Get&nbsp;data&nbsp;chunks&nbsp;from&nbsp;some&nbsp;source</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Monitor.Wait(m_lock);&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Wait&nbsp;if&nbsp;the&nbsp;thread&nbsp;is&nbsp;busy.&nbsp;'wait'&nbsp;will&nbsp;hold&nbsp;this&nbsp;loop&nbsp;until&nbsp;something&nbsp;else&nbsp;pulses&nbsp;it&nbsp;to&nbsp;release&nbsp;the&nbsp;wait.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;producer&nbsp;{0}&quot;</span>,&nbsp;counter);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;m_queue.Enqueue(counter);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Monitor.Pulse(m_lock);&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Releases&nbsp;consumer&nbsp;thread</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;counter&#43;&#43;;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Consumer()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">lock</span>&nbsp;(m_lock)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Allows&nbsp;only&nbsp;one&nbsp;thread&nbsp;at&nbsp;a&nbsp;time&nbsp;inside&nbsp;m_lock</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Monitor.Pulse(m_lock);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">while</span>&nbsp;(Monitor.Wait(m_lock,&nbsp;<span class="cs__number">1000</span>))&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Wait&nbsp;in&nbsp;the&nbsp;loop&nbsp;while&nbsp;producer&nbsp;is&nbsp;busy.&nbsp;Exit&nbsp;when&nbsp;producer&nbsp;times-out.&nbsp;1000&nbsp;=&nbsp;1&nbsp;second;&nbsp;app&nbsp;will&nbsp;hang&nbsp;without&nbsp;this&nbsp;time-out&nbsp;value</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;data&nbsp;=&nbsp;m_queue.Dequeue();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;consumer&nbsp;{0}&quot;</span>,&nbsp;data);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Monitor.Pulse(m_lock);&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Release&nbsp;consumer</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">class</span>&nbsp;Program&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Main(<span class="cs__keyword">string</span>[]&nbsp;args)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ProducerConsumer&nbsp;app&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;ProducerConsumer();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Create&nbsp;2&nbsp;threads</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Thread&nbsp;t_producer&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Thread(<span class="cs__keyword">new</span>&nbsp;ThreadStart(app.Producer));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Thread&nbsp;t_consumer&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Thread(<span class="cs__keyword">new</span>&nbsp;ThreadStart(app.Consumer));&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Start&nbsp;threads</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;t_producer.Start();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;t_consumer.Start();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Waith&nbsp;for&nbsp;the&nbsp;threads&nbsp;to&nbsp;complete</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;t_producer.Join();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;t_consumer.Join();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;\nPress&nbsp;any&nbsp;key&nbsp;to&nbsp;complete&nbsp;the&nbsp;program.\n&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.ReadKey(<span class="cs__keyword">false</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
&nbsp;
<span class="cs__mlcom">/*&nbsp;
&nbsp;*&nbsp;Will&nbsp;print:&nbsp;
&nbsp;
producer&nbsp;
consumer&nbsp;
producer&nbsp;
consumer&nbsp;
producer&nbsp;
consumer&nbsp;
&nbsp;
Press&nbsp;any&nbsp;key&nbsp;to&nbsp;complete&nbsp;the&nbsp;program.&nbsp;
*/</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</div>
<div class="endscriptcode" style="font-size:small">
<h2><span style="font-size:small">Summary</span></h2>
<dl><dt>This is a very simple example of inter-thread communication using Monitor object.</dt></dl>
</div>
<div class="endscriptcode">&nbsp;</div>
</div>
<h2>Source Code Files</h2>
<dl><dt>program.cs - the only file in the project &nbsp;</dt></dl>
<div>&nbsp;</div>
<dl></dl>
