# Muenchian Grouping and Sorting in BizTalk Maps without losing Map functionalitie
## Requires
- Visual Studio 2010
## License
- Apache License, Version 2.0
## Technologies
- BizTalk Server
- BizTalk
## Topics
- BizTalk Server
- BizTalk
- BizTalk Mapper
- BizTalk Mapper Patterns
- Grouping Pattern
- Sorting Pattern
- Muenchian Method
## Updated
- 02/27/2017
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">Searching for all the preceding siblings with the 'preceding-siblings' axis takes a long time if you're near the end of the records, this involves looking at every single contact each time which this method very inefficient.</span></p>
<p><span style="font-size:small">The Muenchian Method is a method developed by Steve Muench for performing grouping and sorting functionalities in a more efficient way using keys.&nbsp; Keys work by assigning a key value to a node and giving you easy access
 to that node through the key value.&nbsp; If there are lots of nodes that have the same key value, then all those nodes are retrieved when you use that key value.&nbsp; Effectively this means that if you want to group a set of nodes according to a particular
 property of the node, then you can use keys to group them together.</span></p>
<p style="font-family:Tahoma"><span style="font-size:small">There is an astonishing post by Chris Romp about
<a href="http://blogs.msdn.com/chrisromp/archive/2008/07/31/muenchian-grouping-and-sorting-in-biztalk-maps.aspx">
Muenchian Grouping and Sorting in BizTalk Maps</a>, but it has one limitation, by creating and configures Custom XSL Path we lose all mapping features.<br>
</span></p>
<p style="text-align:center"><span style="font-size:small"><img src="http://sandroaspbiztalkblog.files.wordpress.com/2009/10/custom-xsl-path.jpg?w=274" alt=""></span></p>
<p><span style="font-size:small"><strong>So how can we use Muenchian Grouping without losing Map features?</strong></span></p>
<h1><span>Building </span>my first approach</h1>
<p style="font-family:Tahoma"><span style="font-size:small"><strong>My First approach</strong>: Was try to put an Inline XSLT functoid and put all the XSL inside<br>
</span></p>
<p style="font-family:Tahoma; text-align:center"><span style="font-size:small"><img src="http://sandroaspbiztalkblog.files.wordpress.com/2009/10/meunchian-grouping1.jpg?w=300" alt=""><br>
</span></p>
<div style="margin-left:40px; font-family:Courier New"><span style="font-size:small"><em>&lt;xsl:key name=&rdquo;groups&rdquo; match=&rdquo;Order&rdquo; use=&rdquo;OrderId&rdquo;/&gt;</em><br>
</span><span style="font-size:small"><em>&lt;!&ndash; This will loop through our key (&ldquo;OrderId&rdquo;) &ndash;&gt;</em><br>
</span><span style="font-size:small"><em>&lt;xsl:for-each select=&rdquo;Order[generate-id(.)=generate-id(key('groups',OrderId))]&ldquo;&gt;</em><br>
</span><span style="font-size:small"><em>&lt;!&ndash; And let&rsquo;s do some sorting for good measure&hellip; &ndash;&gt;</em><br>
</span><span style="font-size:small"><em>&lt;xsl:sort select=&rdquo;OrderId&rdquo; order=&rdquo;ascending&rdquo;/&gt;</em><br>
</span><span style="font-size:small"><em>&lt;Order&gt;</em><br>
</span><span style="font-size:small"><em>&lt;OrderId&gt;&lt;xsl:value-of select=&rdquo;OrderId/text()&rdquo; /&gt;&lt;/OrderId&gt;</em><br>
</span><span style="font-size:small"><em>&lt;Items&gt;</em><br>
</span><span style="font-size:small"><em>&lt;!&ndash; Another loop&hellip; &ndash;&gt;</em><br>
</span><span style="font-size:small"><em>&lt;xsl:for-each select=&rdquo;key(&lsquo;groups&rsquo;,OrderId)&rdquo;&gt;</em><br>
</span><span style="font-size:small"><em>&lt;ItemId&gt;&lt;xsl:value-of select=&rdquo;ItemId&rdquo; /&gt;&lt;/ItemId&gt;</em><br>
</span><span style="font-size:small"><em>&lt;/xsl:for-each&gt;</em><br>
</span><span style="font-size:small"><em>&lt;/Items&gt;</em><br>
</span><span style="font-size:small"><em>&lt;/Order&gt;</em><br>
</span><span style="font-size:small"><em>&lt;/xsl:for-each&gt;</em></span></div>
<p style="font-family:Courier New">&nbsp;</p>
<p style="font-family:Tahoma"><span style="font-size:small">The problem with that approach is that gives an error:
</span></p>
<ul style="font-family:Tahoma">
<li><span style="font-size:small"><em>XSLT compile error at (9,8). See InnerException for details. &lsquo;xsl:key&rsquo; cannot be a child of the &lsquo;ns0:OutputOrder&rsquo; element</em></span>
</li></ul>
<p style="font-family:Tahoma"><span style="font-size:small">So, to avoid this error we need to separate &ldquo;<em><span style="font-family:Courier New">&lt;xsl:key name=&rdquo;groups&rdquo; match=&rdquo;Order&rdquo; use=&rdquo;OrderId&rdquo;/&gt;</span>&rdquo;</em>
 expression from the rest of the XSL (see second approach) </span></p>
<h1><span>Building </span>the right approach and solving the problem</h1>
<p style="font-family:Tahoma"><span style="font-size:small">Add two scripting functoids to the map
</span></p>
<ul style="font-family:Tahoma">
<li><span style="font-size:small">In the first, configure to an &ldquo;Inline XSLT Call Template&rdquo; and put key expression</span>
<ul>
<li><span style="font-size:small"><em>&lt;xsl:key name=&rdquo;groups&rdquo; match=&rdquo;Order&rdquo; use=&rdquo;OrderId&rdquo;/&gt;</em></span>
</li></ul>
</li><li><span style="font-size:small">In the second, configure to an &ldquo;Inline XSLT&rdquo; and the rest of the XSL</span>
<ul style="font-family:Courier New">
<li><span style="font-size:small"><em>&lt;!&ndash; This will loop through our key (&ldquo;OrderId&rdquo;) &ndash;&gt;<br>
&lt;xsl:for-each select=&rdquo;Order[generate-id(.)=generate-id(key('groups',OrderId))]&ldquo;&gt;<br>
&lt;!&ndash; And let&rsquo;s do some sorting for good measure&hellip; &ndash;&gt;<br>
&lt;xsl:sort select=&rdquo;OrderId&rdquo; order=&rdquo;ascending&rdquo;/&gt;<br>
&lt;Order&gt;<br>
&lt;OrderId&gt;&lt;xsl:value-of select=&rdquo;OrderId/text()&rdquo; /&gt;&lt;/OrderId&gt;<br>
&lt;Items&gt;<br>
&lt;!&ndash; Another loop&hellip; &ndash;&gt;<br>
&lt;xsl:for-each select=&rdquo;key(&lsquo;groups&rsquo;,OrderId)&rdquo;&gt;<br>
&lt;ItemId&gt;&lt;xsl:value-of select=&rdquo;ItemId&rdquo; /&gt;&lt;/ItemId&gt;<br>
&lt;/xsl:for-each&gt;<br>
&lt;/Items&gt;<br>
&lt;/Order&gt;<br>
&lt;/xsl:for-each&gt;</em></span> </li></ul>
</li></ul>
<p style="font-family:Tahoma; text-align:center"><span style="font-size:small"><img src="http://sandroaspbiztalkblog.files.wordpress.com/2009/10/meunchian-grouping.jpg?w=300" alt=""><br>
</span></p>
<p><span style="font-size:small">See Sample 1, map &ldquo;MapOrder.btm&rdquo;</span><span style="font-size:20px; font-weight:bold">&nbsp;</span></p>
<p><span style="font-size:20px; font-weight:bold">How can we improved (a little more) this solution</span></p>
<p style="font-family:Tahoma"><span style="font-size:small">When leading with large files, speed processing is vital. Classical Muenchian grouping use generate-id(). Muenchian grouping using generate-id() is slowest that using count() function, and shows worst
 scalability. Probably the reason is poor generate-id() function implementation. In other words, count() function performs is much better.
</span></p>
<p style="font-family:Tahoma"><span style="font-size:small">So to improve Meunchian a little more we have to use count() function instead of generate-id():
</span></p>
<ul style="font-family:Tahoma">
<li><span style="font-size:small"><em>&lt;xsl:for-each select=&rdquo;Order[count(. | key('groups',OrderId)[1]) = 1]&rdquo;&gt;</em></span>
</li><li><span style="font-size:small">See Sample 1, map &ldquo;MapOrder2.btm&rdquo;</span>
</li></ul>
<p style="font-family:Tahoma"><span style="font-size:small">Here some performance stats that I found (see original
<a href="http://www.tkachenko.com/blog/archives/000401.html">post</a>):</span></p>
<p><a href="http://sandroaspbiztalkblog.files.wordpress.com/2009/10/muenchian-performance-table1.jpg"><img src="https://code.msdn.microsoft.com/site/view/file/46268/1/muenchian-performance-table1.jpg" alt="" width="594" height="177"><br>
</a></p>
<p style="font-family:Tahoma"><span style="font-size:small">The graph view works better:</span></p>
<p><span style="font-size:small"><img class="aligncenter" src="http://sandroaspbiztalkblog.files.wordpress.com/2009/10/muenchian-performance.jpg?w=300" alt="" style="display:block; margin-left:auto; margin-right:auto"></span></p>
<h1><span>Samples files</span></h1>
<p><span id="result_box" lang="en"><span class="hps">In the</span> <span class="hps">
project directory</span><span>, there is</span> <span class="hps">a</span> <span class="hps">
folder</span> <span class="hps x_x_x_x_x_x_atn">&quot;</span><span>Samples&quot;</span><span>,</span>
<span class="hps">containing the</span> <span class="hps">test</span> <span class="hps">
files</span></span></p>
<ul>
<li>InputOrder.xml<em> - map input file.<br>
</em></li><li>InputOrder.xml_output.xml<em><em> - map output file.</em></em> </li><li>MapOrderUsingCount.btm </li><li>MapOrderUsingGenerateId.btm </li></ul>
<h1>About Me</h1>
<p><strong>Sandro Pereira</strong><br>
<a href="http://www.devscope.net/">DevScope</a>&nbsp;| MVP &amp; MCTS BizTalk Server 2010<br>
<a href="http://sandroaspbiztalkblog.wordpress.com/">http://sandroaspbiztalkblog.wordpress.com/</a>&nbsp;|&nbsp;<a href="http://twitter.com/sandro_asp">@sandro_asp</a></p>
<p><a href="http://www.devscope.net/"><img id="129835" src="https://gallery.technet.microsoft.com/site/view/file/129835/1/devscope-monochrome-black.png" alt="" width="166" height="51"></a></p>
