# Quick Launch and Top Navigation link programmatically – SharePoint Add In
## Requires
- Visual Studio 2015
## License
- MIT
## Technologies
- Javascript
- Sharepoint Online
- SharePoint Server 2013
- JSOM
- SharePoint 2016
- SharePoint Add-ins
## Topics
- Javascript
- apps for SharePoint
- SharePoint 2013
- SharePoint 2016
## Updated
- 04/26/2016
## Description

<h1>Introduction</h1>
<p>We can add or remove Quick Launch and Top Navigation link programmatically using SharePoint JavaScript Object Model (JSOM), in this below sample I have added both links for this I have used web.get_navigation().get_topNavigationBar() and web.get_navigation().get_quickLaunch().
 You can find the full source code download link in this page,</p>
<p><em><img id="152369" src="152369-get_topnavigationbar.png" alt="" width="686" height="463">&nbsp; &nbsp;</em></p>
<p><strong>Solution compatibility</strong></p>
<p>This sample is tested with SharePoint Online</p>
<p>This sample also compatible with SharePoint 2013 and SharePoint 2016</p>
<p>&nbsp;</p>
<p><strong>To Modify and deploy this solution</strong></p>
<p>Open visual studio 2015</p>
<p>On the file menu select Open -&gt; Project (Ctrl &#43; Shift &#43; o)</p>
<p>In the Open Project window navigate the directory and select solution file (.sln)</p>
<p>Open solution explorer windows and select project solution and click (F4) to open project properties</p>
<p>Change the site URL property on the property window</p>
<div>
<p>Edit the code if required and click play button or (F5) in visual studio&nbsp;</p>
</div>
<p><strong>Code Flow</strong><br>
I have added four buttons for add and remove both&nbsp;Quick Launch and Top Navigation links, buttons call respective&nbsp;function for do operation. in the page load I&nbsp;got the web object and utilized in button events</p>
<p>After Add or Remove links we have refresh the page for see the changes, in below i have shared both HTML and JavaScript. you can also download complete project.</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span><span>HTML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span><span class="hidden">html</span>


<div class="preview">
<pre class="js"><span class="js__string">'use&nbsp;strict'</span>;&nbsp;
&nbsp;
<span class="js__statement">var</span>&nbsp;context;&nbsp;
<span class="js__statement">var</span>&nbsp;web;&nbsp;
context&nbsp;=&nbsp;SP.ClientContext.get_current();&nbsp;
<span class="js__statement">var</span>&nbsp;hostweburl;&nbsp;
<span class="js__statement">var</span>&nbsp;appweburl;&nbsp;
<span class="js__statement">var</span>&nbsp;appContextSite;&nbsp;
&nbsp;
<span class="js__sl_comment">//&nbsp;This&nbsp;code&nbsp;runs&nbsp;when&nbsp;the&nbsp;DOM&nbsp;is&nbsp;ready&nbsp;and&nbsp;creates&nbsp;a&nbsp;context&nbsp;object&nbsp;which&nbsp;is&nbsp;needed&nbsp;to&nbsp;use&nbsp;the&nbsp;SharePoint&nbsp;object&nbsp;model</span>&nbsp;
$(document).ready(<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;getUrl();&nbsp;
<span class="js__brace">}</span>);&nbsp;
&nbsp;
<span class="js__sl_comment">//&nbsp;This&nbsp;function&nbsp;get&nbsp;the&nbsp;URL&nbsp;informations</span>&nbsp;
<span class="js__operator">function</span>&nbsp;getUrl()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;hostweburl&nbsp;=&nbsp;getQueryStringParameter(<span class="js__string">&quot;SPHostUrl&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;appweburl&nbsp;=&nbsp;getQueryStringParameter(<span class="js__string">&quot;SPAppWebUrl&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;hostweburl&nbsp;=&nbsp;<span class="js__function">decodeURIComponent</span>(hostweburl);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;appweburl&nbsp;=&nbsp;<span class="js__function">decodeURIComponent</span>(appweburl).toString().replace(<span class="js__string">&quot;#&quot;</span>,&nbsp;<span class="js__string">&quot;&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;scriptbase&nbsp;=&nbsp;hostweburl&nbsp;&#43;&nbsp;<span class="js__string">&quot;/_layouts/15/&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$.getScript(scriptbase&nbsp;&#43;&nbsp;<span class="js__string">&quot;SP.RequestExecutor.js&quot;</span>,&nbsp;execOperation);&nbsp;
<span class="js__brace">}</span>&nbsp;
&nbsp;
<span class="js__sl_comment">//&nbsp;This&nbsp;function&nbsp;get&nbsp;list&nbsp;data&nbsp;from&nbsp;SharePoint</span>&nbsp;
<span class="js__operator">function</span>&nbsp;execOperation()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;factory&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;SP.ProxyWebRequestExecutorFactory(appweburl);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;context.set_webRequestExecutorFactory(factory);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;appContextSite&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;SP.AppContextSite(context,&nbsp;hostweburl);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;web&nbsp;=&nbsp;appContextSite.get_web();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;context.load(web);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;context.executeQueryAsync(onGetWebSuccess,&nbsp;onFail);&nbsp;
<span class="js__brace">}</span>&nbsp;
&nbsp;
<span class="js__sl_comment">//&nbsp;This&nbsp;function&nbsp;is&nbsp;executed&nbsp;if&nbsp;the&nbsp;above&nbsp;call&nbsp;is&nbsp;successful</span>&nbsp;
<span class="js__operator">function</span>&nbsp;onGetWebSuccess()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;console.log(<span class="js__string">'Hello&nbsp;'</span>&nbsp;&#43;&nbsp;web.get_title());&nbsp;
<span class="js__brace">}</span>&nbsp;
&nbsp;
<span class="js__sl_comment">//&nbsp;This&nbsp;function&nbsp;is&nbsp;executed&nbsp;if&nbsp;the&nbsp;above&nbsp;call&nbsp;fails</span>&nbsp;
<span class="js__operator">function</span>&nbsp;onFail(sender,&nbsp;args)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;console.log(<span class="js__string">'Failed.&nbsp;Error:'</span>&nbsp;&#43;&nbsp;args.get_message());&nbsp;
<span class="js__brace">}</span>&nbsp;
&nbsp;
<span class="js__sl_comment">//for&nbsp;adding&nbsp;new&nbsp;link&nbsp;to&nbsp;Quick&nbsp;Launch</span>&nbsp;
<span class="js__operator">function</span>&nbsp;AddQuickLaunchLink()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;ql&nbsp;=&nbsp;web.get_navigation().get_quickLaunch();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;nnci&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;SP.NavigationNodeCreationInformation();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;nnci.set_title(<span class="js__string">'My&nbsp;Custom&nbsp;Link'</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;nnci.set_url(<span class="js__string">'/_layouts/settings.aspx'</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;nnci.set_asLastNode(true);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ql.add(nnci);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;context.load(ql);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;context.executeQueryAsync(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$(<span class="js__string">'#lblmessage'</span>).append(<span class="js__string">&quot;QuickLaunch&nbsp;link&nbsp;added&nbsp;successfully...&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;onFail);&nbsp;
<span class="js__brace">}</span>&nbsp;
&nbsp;
<span class="js__sl_comment">//adding&nbsp;new&nbsp;link&nbsp;to&nbsp;Top&nbsp;Navigation</span>&nbsp;
<span class="js__operator">function</span>&nbsp;AddTopNavicationLink()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;TopNav&nbsp;=&nbsp;web.get_navigation().get_topNavigationBar();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;nnci&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;SP.NavigationNodeCreationInformation();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;nnci.set_title(<span class="js__string">'My&nbsp;Custom&nbsp;Link'</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;nnci.set_url(<span class="js__string">'/_layouts/settings.aspx'</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;nnci.set_asLastNode(true);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;TopNav.add(nnci);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;context.load(TopNav);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;context.executeQueryAsync(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$(<span class="js__string">'#lblmessage'</span>).append(<span class="js__string">&quot;Top&nbsp;Navigation&nbsp;link&nbsp;added&nbsp;successfully...&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;console.log(<span class="js__string">&quot;TopNav&nbsp;Added&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;onFail);&nbsp;
<span class="js__brace">}</span>&nbsp;
&nbsp;
<span class="js__sl_comment">//Removing&nbsp;new&nbsp;link&nbsp;to&nbsp;Quick&nbsp;Launch</span>&nbsp;
<span class="js__operator">function</span>&nbsp;RemoreQuickLaunchLink()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;ql&nbsp;=&nbsp;web.get_navigation().get_quickLaunch();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;context.load(ql);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;context.executeQueryAsync(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;e&nbsp;=&nbsp;ql.getEnumerator();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;notFound&nbsp;=&nbsp;true;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">while</span>&nbsp;(notFound&nbsp;&amp;&amp;&nbsp;e.moveNext())&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;node&nbsp;=&nbsp;e.get_current();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(node.get_title()&nbsp;===&nbsp;<span class="js__string">&quot;My&nbsp;Custom&nbsp;Link&quot;</span>)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;node.deleteObject();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;notFound&nbsp;=&nbsp;false;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;context.executeQueryAsync(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$(<span class="js__string">'#lblmessage'</span>).append(<span class="js__string">&quot;QuickLaunch&nbsp;link&nbsp;removed&nbsp;successfully...&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;console.log(<span class="js__string">&quot;QuickLaunch&nbsp;link&nbsp;removed&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;onFail);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;onFail);&nbsp;
<span class="js__brace">}</span>&nbsp;
&nbsp;
<span class="js__sl_comment">//removing&nbsp;new&nbsp;link&nbsp;to&nbsp;Top&nbsp;Navigation</span>&nbsp;
<span class="js__operator">function</span>&nbsp;RemoveTopNavicationLink()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;tn&nbsp;=&nbsp;web.get_navigation().get_topNavigationBar();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;context.load(tn);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;context.executeQueryAsync(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;e&nbsp;=&nbsp;tn.getEnumerator();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;notFound&nbsp;=&nbsp;true;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">while</span>&nbsp;(notFound&nbsp;&amp;&amp;&nbsp;e.moveNext())&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;node&nbsp;=&nbsp;e.get_current();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(node.get_title()&nbsp;===&nbsp;<span class="js__string">&quot;My&nbsp;Custom&nbsp;Link&quot;</span>)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;node.deleteObject();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;notFound&nbsp;=&nbsp;false;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;context.executeQueryAsync(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$(<span class="js__string">'#lblmessage'</span>).append(<span class="js__string">&quot;Top&nbsp;Navigation&nbsp;link&nbsp;removed&nbsp;successfully...&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;console.log(<span class="js__string">&quot;TopNav&nbsp;link&nbsp;removed&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;onFail);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;onFail);&nbsp;
<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;
&nbsp;
<span class="js__sl_comment">//This&nbsp;function&nbsp;split&nbsp;the&nbsp;url&nbsp;and&nbsp;trim&nbsp;the&nbsp;App&nbsp;and&nbsp;Host&nbsp;web&nbsp;URLs</span>&nbsp;
<span class="js__operator">function</span>&nbsp;getQueryStringParameter(paramToRetrieve)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;params&nbsp;=&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;document.URL.split(<span class="js__string">&quot;?&quot;</span>)[<span class="js__num">1</span>].split(<span class="js__string">&quot;&amp;&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">for</span>&nbsp;(<span class="js__statement">var</span>&nbsp;i&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;i&nbsp;&lt;&nbsp;params.length;&nbsp;i&nbsp;=&nbsp;i&nbsp;&#43;&nbsp;<span class="js__num">1</span>)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;singleParam&nbsp;=&nbsp;params[i].split(<span class="js__string">&quot;=&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(singleParam[<span class="js__num">0</span>]&nbsp;==&nbsp;paramToRetrieve)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;singleParam[<span class="js__num">1</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
<span class="js__brace">}</span></pre>
</div>
</div>
</div>
<h1>More Information</h1>
<p>*****************************************************************************************************</p>
<p>Hope you find this sample helpful, check out my other samples too.</p>
<p>Give five stars if you wish to appreciate my work.</p>
<p>Facebook page URL:&nbsp;<a title="https://www.facebook.com/sptechnet2016/" href="https://www.facebook.com/sptechnet2016/" target="_blank">https://www.facebook.com/sptechnet2016/</a></p>
<p>Blog URL:&nbsp;<a title="https://sptechnet.wordpress.com/" href="https://sptechnet.wordpress.com/" target="_blank">https://sptechnet.wordpress.com/</a></p>
<p>*****************************************************************************************************</p>
