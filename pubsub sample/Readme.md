# pub/sub sample
## Requires
- Visual Studio 2008
## License
- MIT
## Technologies
- redis
## Topics
- redis pub/sub
## Updated
- 11/25/2014
## Description

<h1>Introduction</h1>
<p><em>redis pub/sub test sample</em></p>
<h1><span>Building the Sample</span></h1>
<p><em>vs2008</em></p>
<p><em>dont download this</em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><a href="http://redis.io/commands/subscribe">SUBSCRIBE</a>,&nbsp;<a href="http://redis.io/commands/unsubscribe">UNSUBSCRIBE</a>&nbsp;and&nbsp;<a href="http://redis.io/commands/publish">PUBLISH</a>&nbsp;implement the&nbsp;<a href="http://en.wikipedia.org/wiki/Publish/subscribe">Publish/Subscribe
 messaging paradigm</a>&nbsp;where (citing Wikipedia) senders (publishers) are not programmed to send their messages to specific receivers (subscribers). Rather, published messages are characterized into channels, without knowledge of what (if any) subscribers
 there may be. Subscribers express interest in one or more channels, and only receive messages that are of interest, without knowledge of what (if any) publishers there are. This decoupling of publishers and subscribers can allow for greater scalability and
 a more dynamic network topology.</p>
<p>For instance in order to subscribe to channels&nbsp;<code>foo</code>&nbsp;and&nbsp;<code>bar</code>&nbsp;the client issues a&nbsp;<a href="http://redis.io/commands/subscribe">SUBSCRIBE</a>&nbsp;providing the names of the channels:</p>
<pre><code>SUBSCRIBE foo bar
</code></pre>
<p>Messages sent by other clients to these channels will be pushed by Redis to all the subscribed clients.</p>
<p>A client subscribed to one or more channels should not issue commands, although it can subscribe and unsubscribe to and from other channels. The reply of the&nbsp;<a href="http://redis.io/commands/subscribe">SUBSCRIBE</a>&nbsp;and&nbsp;<a href="http://redis.io/commands/unsubscribe">UNSUBSCRIBE</a>&nbsp;operations
 are sent in the form of messages, so that the client can just read a coherent stream of messages where the first element indicates the type of message.</p>
<h2>Format of pushed messages</h2>
<p>A message is a&nbsp;<a href="http://redis.io/topics/protocol#array-reply">Array reply</a>&nbsp;with three elements.</p>
<p>The first element is the kind of message:</p>
<ul>
<li>
<p><code>subscribe</code>: means that we successfully subscribed to the channel given as the second element in the reply. The third argument represents the number of channels we are currently subscribed to.</p>
</li><li>
<p><code>unsubscribe</code>: means that we successfully unsubscribed from the channel given as second element in the reply. The third argument represents the number of channels we are currently subscribed to. When the last argument is zero, we are no longer
 subscribed to any channel, and the client can issue any kind of Redis command as we are outside the Pub/Sub state.</p>
</li><li>
<p><code>message</code>: it is a message received as result of a&nbsp;<a href="http://redis.io/commands/publish">PUBLISH</a>&nbsp;command issued by another client. The second element is the name of the originating channel, and the third argument is the actual
 message payload.</p>
</li></ul>
<h2>Database &amp; Scoping</h2>
<p>Pub/Sub has no relation to the key space. It was made to not interfere with it on any level, including database numbers.</p>
<p>Publishing on db 10, will be heard on by a subscriber on db 1.</p>
<p>If you need scoping of some kind, prefix the channels with the name of the environment (test, staging, production, ...).</p>
<h2>Wire protocol example</h2>
<pre><code>SUBSCRIBE first second
*3
$9
subscribe
$5
first
:1
*3
$9
subscribe
$6
second
:2
</code></pre>
<p>At this point, from another client we issue a&nbsp;<a href="http://redis.io/commands/publish">PUBLISH</a>&nbsp;operation against the channel named&nbsp;<code>second</code>:</p>
<pre><code>&gt; PUBLISH second Hello
</code></pre>
<p>This is what the first client receives:</p>
<pre><code>*3
$7
message
$6
second
$5
Hello
</code></pre>
<p>Now the client unsubscribes itself from all the channels using the&nbsp;<a href="http://redis.io/commands/unsubscribe">UNSUBSCRIBE</a>&nbsp;command without additional arguments:</p>
<pre><code>UNSUBSCRIBE
*3
$11
unsubscribe
$6
second
:1
*3
$11
unsubscribe
$5
first
:0
</code></pre>
<h2>Pattern-matching subscriptions</h2>
<p>The Redis Pub/Sub implementation supports pattern matching. Clients may subscribe to glob-style patterns in order to receive all the messages sent to channel names matching a given pattern.</p>
<p>For instance:</p>
<pre><code>PSUBSCRIBE news.*
</code></pre>
<p>Will receive all the messages sent to the channel&nbsp;<code>news.art.figurative</code>,&nbsp;<code>news.music.jazz</code>, etc. All the glob-style patterns are valid, so multiple wildcards are supported.</p>
<pre><code>PUNSUBSCRIBE news.*
</code></pre>
<p>Will then unsubscribe the client from that pattern. No other subscriptions will be affected by this call.</p>
<p>Messages received as a result of pattern matching are sent in a different format:</p>
<ul>
<li>The type of the message is&nbsp;<code>pmessage</code>: it is a message received as result of a&nbsp;<a href="http://redis.io/commands/publish">PUBLISH</a>&nbsp;command issued by another client, matching a pattern-matching subscription. The second element
 is the original pattern matched, the third element is the name of the originating channel, and the last element the actual message payload.
</li></ul>
<p>Similarly to&nbsp;<a href="http://redis.io/commands/subscribe">SUBSCRIBE</a>&nbsp;and&nbsp;<a href="http://redis.io/commands/unsubscribe">UNSUBSCRIBE</a>,&nbsp;<a href="http://redis.io/commands/psubscribe">PSUBSCRIBE</a>&nbsp;and&nbsp;<a href="http://redis.io/commands/punsubscribe">PUNSUBSCRIBE</a>&nbsp;commands
 are acknowledged by the system sending a message of type&nbsp;<code>psubscribe</code>&nbsp;and&nbsp;<code>punsubscribe</code>&nbsp;using the same format as the&nbsp;<code>subscribe</code>&nbsp;and&nbsp;<code>unsubscribe</code>&nbsp;message format.</p>
<h2>Messages matching both a pattern and a channel subscription</h2>
<p>A client may receive a single message multiple times if it's subscribed to multiple patterns matching a published message, or if it is subscribed to both patterns and channels matching the message. Like in the following example:</p>
<pre><code>SUBSCRIBE foo
PSUBSCRIBE f*
</code></pre>
<p>In the above example, if a message is sent to channel&nbsp;<code>foo</code>, the client will receive two messages: one of type&nbsp;<code>message</code>&nbsp;and one of type<code>pmessage</code>.</p>
<h2>The meaning of the subscription count with pattern matching</h2>
<p>In&nbsp;<code>subscribe</code>,&nbsp;<code>unsubscribe</code>,&nbsp;<code>psubscribe</code>&nbsp;and&nbsp;<code>punsubscribe</code>&nbsp;message types, the last argument is the count of subscriptions still active. This number is actually the total number
 of channels and patterns the client is still subscribed to. So the client will exit the Pub/Sub state only when this count drops to zero as a result of unsubscription from all the channels and patterns.</p>
<h2>Programming example</h2>
<p>Pieter Noordhuis provided a great example using EventMachine and Redis to create&nbsp;<a href="https://gist.github.com/348262">a multi user high performance web chat</a>.</p>
<h2>Client library implementation hints</h2>
<p>Because all the messages received contain the original subscription causing the message delivery (the channel in the case of message type, and the original pattern in the case of pmessage type) client libraries may bind the original subscription to callbacks
 (that can be anonymous functions, blocks, function pointers), using an hash table.</p>
<p>When a message is received an&nbsp;<span class="math">O(1)&nbsp;</span>lookup can be done in order to deliver the message to the registered callback.</p>
<h1><span>Source Code Files</span></h1>
<ul>
<li>pubsubtest.sln </li></ul>
<h1>More Information</h1>
<p><em>c992699@netmarble</em></p>
