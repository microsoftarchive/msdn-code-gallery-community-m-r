# QR Code Generator Add-In for SharePoint
## Requires
- Visual Studio 2015
## License
- MIT
## Technologies
- Javascript
- Sharepoint Online
- QR CODE
- SharePoint Server 2013
- SharePoint Foundation 2013
- apps for SharePoint
- SharePoint Add-ins
## Topics
- SharePoint
- QR CODE
- apps for SharePoint
- SharePoint 2013
## Updated
- 02/25/2016
## Description

<h1>Introduction</h1>
<p><span>Using this you can generate QR Code for any text value and&nbsp;URL, this add-in can be add anywhere in you SharePoint Dashboard. Below you can see step by step instructions to&nbsp;develop&nbsp;this SharePoint Add-In.</span></p>
<p><strong>Solution compatibility</strong></p>
<p>This sample is tested with SharePoint Online</p>
<p>This sample also compatible with SharePoint 2013 and SharePoint 2016</p>
<p><br>
<strong>To Modify and deploy this solution</strong></p>
<p>Open visual studio 2015</p>
<p>On the file menu select Open -&gt; Project (Ctrl &#43; Shift &#43; o)</p>
<p>In the Open Project window navigate the directory and select solution file (.sln)</p>
<p>Open solution explorer windows and select project solution and click (F4) to open project propertiesChange the site URL property on the property window&nbsp;</p>
<p>Edit the code if required and click play button or (F5) in visual studio&nbsp;</p>
<p>&nbsp;</p>
<p><strong>To add new resource file (.js or .css or Images) into project</strong></p>
<p>Select a folder from solution explorer based on your file type (Images or Scripts or Content for CSS)</p>
<p>Right click and select &ldquo;Open Folder in File Explorer&rdquo; option</p>
<p>Now paste your files into the folderAgain in the solution explorer window at the top, click &ldquo;Show All Files&rdquo; icon</p>
<p>Now you can find the file without active icon, right click and select &ldquo;Include in Project&rdquo; Option</p>
<p>&nbsp;</p>
<p>Add new &ldquo;Client Web Part (Host Web)&rdquo; and select &ldquo;Create a new app web page for the client web part content&rdquo;, Edit newly created aspx page which is located in the Pages folder. Add below JS code and HTML. In the JS function I am passing
 Textbox value and Div name as parameter, JavaScript will generate QR code based on the content in Text box value.</p>
<div></div>
<div></div>
<p>&nbsp;</p>
<p><img id="147781" src="147781-2016-01-26_21-30-19.png" alt="" width="672" height="447"></p>
<h1><em style="font-size:10px">&nbsp; &nbsp;</em></h1>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>HTML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">html</span>
<pre class="hidden">&lt;%@ Page Language=&quot;C#&quot; Inherits=&quot;Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c&quot; %&gt;

&lt;%@ Register TagPrefix=&quot;SharePoint&quot; Namespace=&quot;Microsoft.SharePoint.WebControls&quot; Assembly=&quot;Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c&quot; %&gt;
&lt;%@ Register TagPrefix=&quot;Utilities&quot; Namespace=&quot;Microsoft.SharePoint.Utilities&quot; Assembly=&quot;Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c&quot; %&gt;
&lt;%@ Register TagPrefix=&quot;WebPartPages&quot; Namespace=&quot;Microsoft.SharePoint.WebPartPages&quot; Assembly=&quot;Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c&quot; %&gt;

&lt;WebPartPages:AllowFraming ID=&quot;AllowFraming&quot; runat=&quot;server&quot; /&gt;

&lt;html&gt;
&lt;head&gt;
    &lt;title&gt;&lt;/title&gt;

    &lt;script type=&quot;text/javascript&quot; src=&quot;../Scripts/jquery-1.9.1.min.js&quot;&gt;&lt;/script&gt;
    &lt;script type=&quot;text/javascript&quot; src=&quot;/_layouts/15/MicrosoftAjax.js&quot;&gt;&lt;/script&gt;
    &lt;script type=&quot;text/javascript&quot; src=&quot;/_layouts/15/sp.runtime.js&quot;&gt;&lt;/script&gt;
    &lt;script type=&quot;text/javascript&quot; src=&quot;/_layouts/15/sp.js&quot;&gt;&lt;/script&gt;
    &lt;script type=&quot;text/javascript&quot; src=&quot;../Scripts/initstrings.js&quot;&gt;&lt;/script&gt;
    &lt;script type=&quot;text/javascript&quot; src=&quot;../Scripts/qr.js&quot;&gt;&lt;/script&gt;
    &lt;script type=&quot;text/javascript&quot;&gt;
        // Set the style of the client web part page to be consistent with the host web.
        (function () {
            'use strict';

            var hostUrl = '';
            if (document.URL.indexOf('?') != -1) {
                var params = document.URL.split('?')[1].split('&amp;');
                for (var i = 0; i &lt; params.length; i&#43;&#43;) {
                    var p = decodeURIComponent(params[i]);
                    if (/^SPHostUrl=/i.test(p)) {
                        hostUrl = p.split('=')[1];
                        document.write('&lt;link rel=&quot;stylesheet&quot; href=&quot;' &#43; hostUrl &#43; '/_layouts/15/defaultcss.ashx&quot; /&gt;');
                        break;
                    }
                }
            }
            if (hostUrl == '') {
                document.write('&lt;link rel=&quot;stylesheet&quot; href=&quot;/_layouts/15/1033/styles/themable/corev15.css&quot; /&gt;');
            }
        })();

       
        function generatefn() {
            if ($('#QR_URL').val() != '')
                onLoadQrCode($('#QR_URL').val(), 'DivImage');
        }
    &lt;/script&gt;
&lt;/head&gt;
&lt;body&gt;
    &lt;input type=&quot;text&quot; id=&quot;QR_URL&quot; value=&quot;&quot; style=&quot;width:250px&quot; /&gt;&lt;input onclick=&quot;generatefn()&quot; type=&quot;button&quot; id=&quot;btnGenerate&quot; value=&quot;Generate&quot; /&gt;
    &lt;div id=&quot;DivImage&quot;&gt;&lt;/div&gt;
&lt;/body&gt;
&lt;/html&gt;
</pre>
<div class="preview">
<pre class="html"><span class="html__tag_start">&lt;%@&nbsp;Page</span>&nbsp;<span class="html__attr_name">Language</span>=<span class="html__attr_value">&quot;C#&quot;</span>&nbsp;<span class="html__attr_name">Inherits</span>=<span class="html__attr_value">&quot;Microsoft.SharePoint.WebPartPages.WebPartPage,&nbsp;Microsoft.SharePoint,&nbsp;Version=15.0.0.0,&nbsp;Culture=neutral,&nbsp;PublicKeyToken=71e9bce111e9429c&quot;</span>&nbsp;<span class="html__tag_start">%&gt;</span>&nbsp;
&nbsp;
<span class="html__tag_start">&lt;%@&nbsp;Register</span>&nbsp;<span class="html__attr_name">TagPrefix</span>=<span class="html__attr_value">&quot;SharePoint&quot;</span>&nbsp;<span class="html__attr_name">Namespace</span>=<span class="html__attr_value">&quot;Microsoft.SharePoint.WebControls&quot;</span>&nbsp;<span class="html__attr_name">Assembly</span>=<span class="html__attr_value">&quot;Microsoft.SharePoint,&nbsp;Version=15.0.0.0,&nbsp;Culture=neutral,&nbsp;PublicKeyToken=71e9bce111e9429c&quot;</span>&nbsp;<span class="html__tag_start">%&gt;</span>&nbsp;
<span class="html__tag_start">&lt;%@&nbsp;Register</span>&nbsp;<span class="html__attr_name">TagPrefix</span>=<span class="html__attr_value">&quot;Utilities&quot;</span>&nbsp;<span class="html__attr_name">Namespace</span>=<span class="html__attr_value">&quot;Microsoft.SharePoint.Utilities&quot;</span>&nbsp;<span class="html__attr_name">Assembly</span>=<span class="html__attr_value">&quot;Microsoft.SharePoint,&nbsp;Version=15.0.0.0,&nbsp;Culture=neutral,&nbsp;PublicKeyToken=71e9bce111e9429c&quot;</span>&nbsp;<span class="html__tag_start">%&gt;</span>&nbsp;
<span class="html__tag_start">&lt;%@&nbsp;Register</span>&nbsp;<span class="html__attr_name">TagPrefix</span>=<span class="html__attr_value">&quot;WebPartPages&quot;</span>&nbsp;<span class="html__attr_name">Namespace</span>=<span class="html__attr_value">&quot;Microsoft.SharePoint.WebPartPages&quot;</span>&nbsp;<span class="html__attr_name">Assembly</span>=<span class="html__attr_value">&quot;Microsoft.SharePoint,&nbsp;Version=15.0.0.0,&nbsp;Culture=neutral,&nbsp;PublicKeyToken=71e9bce111e9429c&quot;</span>&nbsp;<span class="html__tag_start">%&gt;</span>&nbsp;
&nbsp;
<span class="html__tag_start">&lt;WebPartPages</span>:AllowFraming&nbsp;<span class="html__attr_name">ID</span>=<span class="html__attr_value">&quot;AllowFraming&quot;</span>&nbsp;<span class="html__attr_name">runat</span>=<span class="html__attr_value">&quot;server&quot;</span>&nbsp;<span class="html__tag_start">/&gt;</span>&nbsp;
&nbsp;
<span class="html__tag_start">&lt;html</span><span class="html__tag_start">&gt;&nbsp;
</span><span class="html__tag_start">&lt;head</span><span class="html__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;title</span><span class="html__tag_start">&gt;</span><span class="html__tag_end">&lt;/title&gt;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;script</span>&nbsp;<span class="html__attr_name">type</span>=<span class="html__attr_value">&quot;text/javascript&quot;</span>&nbsp;<span class="html__attr_name">src</span>=<span class="html__attr_value">&quot;../Scripts/jquery-1.9.1.min.js&quot;</span><span class="html__tag_start">&gt;</span><span class="html__tag_end">&lt;/script&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;script</span>&nbsp;<span class="html__attr_name">type</span>=<span class="html__attr_value">&quot;text/javascript&quot;</span>&nbsp;<span class="html__attr_name">src</span>=<span class="html__attr_value">&quot;/_layouts/15/MicrosoftAjax.js&quot;</span><span class="html__tag_start">&gt;</span><span class="html__tag_end">&lt;/script&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;script</span>&nbsp;<span class="html__attr_name">type</span>=<span class="html__attr_value">&quot;text/javascript&quot;</span>&nbsp;<span class="html__attr_name">src</span>=<span class="html__attr_value">&quot;/_layouts/15/sp.runtime.js&quot;</span><span class="html__tag_start">&gt;</span><span class="html__tag_end">&lt;/script&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;script</span>&nbsp;<span class="html__attr_name">type</span>=<span class="html__attr_value">&quot;text/javascript&quot;</span>&nbsp;<span class="html__attr_name">src</span>=<span class="html__attr_value">&quot;/_layouts/15/sp.js&quot;</span><span class="html__tag_start">&gt;</span><span class="html__tag_end">&lt;/script&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;script</span>&nbsp;<span class="html__attr_name">type</span>=<span class="html__attr_value">&quot;text/javascript&quot;</span>&nbsp;<span class="html__attr_name">src</span>=<span class="html__attr_value">&quot;../Scripts/initstrings.js&quot;</span><span class="html__tag_start">&gt;</span><span class="html__tag_end">&lt;/script&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;script</span>&nbsp;<span class="html__attr_name">type</span>=<span class="html__attr_value">&quot;text/javascript&quot;</span>&nbsp;<span class="html__attr_name">src</span>=<span class="html__attr_value">&quot;../Scripts/qr.js&quot;</span><span class="html__tag_start">&gt;</span><span class="html__tag_end">&lt;/script&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;script</span>&nbsp;<span class="html__attr_name">type</span>=<span class="html__attr_value">&quot;text/javascript&quot;</span><span class="html__tag_start">&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__sl_comment">//&nbsp;Set&nbsp;the&nbsp;style&nbsp;of&nbsp;the&nbsp;client&nbsp;web&nbsp;part&nbsp;page&nbsp;to&nbsp;be&nbsp;consistent&nbsp;with&nbsp;the&nbsp;host&nbsp;web.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__string">'use&nbsp;strict'</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;hostUrl&nbsp;=&nbsp;<span class="js__string">''</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(document.URL.indexOf(<span class="js__string">'?'</span>)&nbsp;!=&nbsp;-<span class="js__num">1</span>)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;params&nbsp;=&nbsp;document.URL.split(<span class="js__string">'?'</span>)[<span class="js__num">1</span>].split(<span class="js__string">'&amp;'</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">for</span>&nbsp;(<span class="js__statement">var</span>&nbsp;i&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;i&nbsp;&lt;&nbsp;params.length;&nbsp;i&#43;&#43;)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;p&nbsp;=&nbsp;<span class="js__function">decodeURIComponent</span>(params[i]);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(<span class="js__reg_exp">/^SPHostUrl=/i</span>.test(p))&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;hostUrl&nbsp;=&nbsp;p.split(<span class="js__string">'='</span>)[<span class="js__num">1</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;document.write(<span class="js__string">'&lt;link&nbsp;rel=&quot;stylesheet&quot;&nbsp;href=&quot;'</span>&nbsp;&#43;&nbsp;hostUrl&nbsp;&#43;&nbsp;<span class="js__string">'/_layouts/15/defaultcss.ashx&quot;&nbsp;/&gt;'</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">break</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(hostUrl&nbsp;==&nbsp;<span class="js__string">''</span>)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;document.write(<span class="js__string">'&lt;link&nbsp;rel=&quot;stylesheet&quot;&nbsp;href=&quot;/_layouts/15/1033/styles/themable/corev15.css&quot;&nbsp;/&gt;'</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>)();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">function</span>&nbsp;generatefn()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;($(<span class="js__string">'#QR_URL'</span>).val()&nbsp;!=&nbsp;<span class="js__string">''</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;onLoadQrCode($(<span class="js__string">'#QR_URL'</span>).val(),&nbsp;<span class="js__string">'DivImage'</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_end">&lt;/script&gt;</span>&nbsp;
<span class="html__tag_end">&lt;/head&gt;</span>&nbsp;
<span class="html__tag_start">&lt;body</span><span class="html__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;input</span>&nbsp;<span class="html__attr_name">type</span>=<span class="html__attr_value">&quot;text&quot;</span>&nbsp;<span class="html__attr_name">id</span>=<span class="html__attr_value">&quot;QR_URL&quot;</span>&nbsp;<span class="html__attr_name">value</span>=<span class="html__attr_value">&quot;&quot;</span>&nbsp;<span class="html__attr_name">style</span>=<span class="html__attr_value">&quot;width:250px&quot;</span>&nbsp;<span class="html__tag_start">/&gt;</span><span class="html__tag_start">&lt;input</span>&nbsp;<span class="html__attr_name">onclick</span>=<span class="html__attr_value">&quot;generatefn()&quot;</span>&nbsp;<span class="html__attr_name">type</span>=<span class="html__attr_value">&quot;button&quot;</span>&nbsp;<span class="html__attr_name">id</span>=<span class="html__attr_value">&quot;btnGenerate&quot;</span>&nbsp;<span class="html__attr_name">value</span>=<span class="html__attr_value">&quot;Generate&quot;</span>&nbsp;<span class="html__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;div</span>&nbsp;<span class="html__attr_name">id</span>=<span class="html__attr_value">&quot;DivImage&quot;</span><span class="html__tag_start">&gt;</span><span class="html__tag_end">&lt;/div&gt;</span>&nbsp;
<span class="html__tag_end">&lt;/body&gt;</span>&nbsp;
<span class="html__tag_end">&lt;/html&gt;</span>&nbsp;
</pre>
</div>
</div>
</div>
<p>*****************************************************************************************************</p>
<p>Hope you find this sample helpful, check out my other samples too.</p>
<p>Give five stars if you wish to appreciate my work.</p>
<p>Facebook page URL:&nbsp;<a title="https://www.facebook.com/sptechnet2016/" href="https://www.facebook.com/sptechnet2016/" target="_blank">https://www.facebook.com/sptechnet2016/</a></p>
<p>Blog URL:&nbsp;<a title="https://sptechnet.wordpress.com/" href="https://sptechnet.wordpress.com/" target="_blank">https://sptechnet.wordpress.com/</a></p>
<p>*****************************************************************************************************</p>
