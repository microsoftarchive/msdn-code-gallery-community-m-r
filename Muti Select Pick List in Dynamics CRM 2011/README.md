# Muti Select Pick List in Dynamics CRM 2011
## License
- MS-LPL
## Technologies
- Microsoft Dynamics CRM 2011
## Topics
- Multi Select Pick List
## Updated
- 11/27/2012
## Description

<h1>Introduction</h1>
<p style="text-align:justify">Our company is in the process of implementing Microsoft Dynamics CRM 2011. We have many &ldquo;customer preference fields&rdquo; that require multi-select functionality. My service provider&rsquo;s solutions were not to the requirement
 most of the time, so I thought I would try to fix this up. I had this solution in mind and contacted Microsoft, for which they provided link to Jim&rsquo;s blog. That is a great idea from Jim. But our requirement was little<br>
different, mainly because some of my lists of values are long. Thanks to Karen<br>
Carvalho (our Snr Marketing Manager), Christine Suell (my supervisor) and<br>
Andrew Bourke (Project Manager) &nbsp;for all the<br>
support and encouragement. &nbsp;Also thanks<br>
to Google and bloggers from whom I have found out syntax for many of the<br>
statements.</p>
<p><br>
<strong>The solution is simple though the code looks lengthy (I wanted error checks and customisation as far<br>
as possible). Anyone can implement this without modifying code.</strong></p>
<p>Advantages:</p>
<p>&nbsp;</p>
<ul>
<li>&nbsp; Picklist can be of any length&nbsp;No need to modify the code </li><li>&nbsp; Even non-technical persons can implement </li><li>&nbsp; Values are searchable in your reports </li><li>&nbsp; There is no special characters in the text field, so you can view the list without formatting
</li><li>&nbsp; There is no additional formatting or fields in the form except the two you create
</li><li>&nbsp; Does not occupy space on your form, requires only a single column </li><li>&nbsp; There is only one function you need to implement at the onload event, so in case you lose this on an update&nbsp; you could easily bring it back.
</li><li>&nbsp; Fully free (of course LIKES and donations welcome!) </li></ul>
<p><br>
Disadvantages:</p>
<p>&nbsp;</p>
<ul>
<li>&nbsp; Limitation on support (the method may be supported but the script is not supported)
</li><li>&nbsp; You need to create one additional field </li><li>&nbsp; The values are stored in text field </li></ul>
<p>&nbsp;</p>
<h1><span>Building the Sample</span></h1>
<p><em>This sample works in Microsoft Dynamic CRM 2011. You dont need any additional software or coding experience. Just upload the script, add fields, configure and publish. for more info follow the step-by-step instructions.</em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><strong>Solution:</strong></p>
<p>Those who need step-by-step explanation and screen shot, please scroll down.</p>
<p>&nbsp;Steps:</p>
<p>1)&nbsp;&nbsp;Create an option set and a multiline text field . You can choose your own names</p>
<p>2)&nbsp;&nbsp;Hide the text field (script takes care even if you forget, but suggested )</p>
<p>3)&nbsp;&nbsp;Add the script to Web Resource library.</p>
<p>4)&nbsp; Register a function on the onload event of the form and name the function as MultiPickList3</p>
<p>5)&nbsp;&nbsp;Pass the string parameters as show below:</p>
<p>Format: <strong>&lt;optionset fieldname&gt; ,&nbsp; &lt;option value text field&gt;</strong> , [&lt;option menu header&gt; ], [&lt;select color&gt;]&nbsp;&nbsp; ,&nbsp;&nbsp; [&lt;deselect color&gt; ]</p>
<p>Eg:&nbsp;&ldquo;new_herospl&rdquo;,&rdquo;new_herostext&rdquo;, &rdquo;&rdquo;, &ldquo;green&rdquo;, &ldquo;white&rdquo;</p>
<p>Only first 2 are compulsory. The 3<sup>rd</sup> parameter is a tricky one. It is assumed that the first option in your list of<br>
values (Picklist option field) is the heading, so suggest leaving it blank. However if you don&rsquo;t have a header item you have to pass some junk string such as &ldquo;~~no heading~~&rdquo; or &ldquo;xxxxxxx&rdquo;.</p>
<p>Your solution is ready to use. I have tested this in Microsoft Dynamics CRM 2011 and should work in CRM 4 with a<br>
change of code in one or two places. However you have to test it for your installation and your comments are always welcome. I have not made use of some java functions because of compatibility issues.</p>
<p>&nbsp;</p>
<p><strong>&nbsp;<img id="67795" src="http://i1.code.msdn.s-msft.com/muti-select-pick-list-in-bf68d254/image/file/67795/1/crm_mspl1.jpg" alt="" width="960" height="600" style="float:left"></strong></p>
<p><br>
&nbsp; <br>
</p>
<p><strong>Code without explanation:</strong></p>
<p>&nbsp;</p>
<p><strong>function MultiPickList3</strong>(param1, param2, param3,param4,param5)</p>
<p>{</p>
<p>//************************************************************************************************</p>
<p>//******&nbsp; Solution:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Multi Select Pick List function:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
<p>//******&nbsp; Platform:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Microsoft Dynamics CRM 2011</p>
<p>//******&nbsp; Last updated date:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;25th Apirl 2012</p>
<p>//****** &nbsp;Created by:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Aroop Vachali,
<a href="mailto:aroopv@gmail.com">aroopv@gmail.com</a>, Company: Central Equity, Melbourne, Australia<br>
//</p>
<p>//************************************************************************************************</p>
<p>//****<br>
//Courtsey: Karen Carvalho (Marketing Manager), Christine Suell (Supervisor), Andrew Burke(Project Manager)<br>
//Google and all bloggers for many of the syntaxes</p>
<p>//************************************************************************************************</p>
<p>try</p>
<p>{</p>
<p>var fn = arguments.callee.toString().match(/function\s&#43;([^\s\(]&#43;)/);</p>
<p>if (param1==null ||<br>
param2==null)</p>
<p>{</p>
<p>alert(&quot;Error: Parameter missing. \n Format: &lt;optionset fieldsname&gt; ,&nbsp; &lt;option value text field&gt; , &nbsp;[&lt;option header&gt;], [&lt;select color&gt;]&nbsp;&nbsp; ,&nbsp; &nbsp;[&lt;deselect color&gt;] ,&nbsp; \n [&quot;&#43;&quot;Function=&quot;&#43;fn[1]&#43;&quot;]&quot;&nbsp;
 );</p>
<p>return true;</p>
<p>}</p>
<p>var<br>
tparamtype=Xrm.Page.data.entity.attributes.get(param1).getAttributeType();</p>
<p>if<br>
(tparamtype!=&quot;optionset&quot;)</p>
<p>&nbsp; { alert (param1&#43;&quot;(first parameter) should be of type optionset \n&quot;&#43;&quot;[function=&quot;&#43;fn[1]&#43;&quot;]&quot;);</p>
<p>&nbsp;&nbsp;&nbsp; return true;</p>
<p>&nbsp; }</p>
<p>var tparamtype=Xrm.Page.data.entity.attributes.get(param2).getAttributeType();</p>
<p>if<br>
(tparamtype!=&quot;memo&quot;)</p>
<p>&nbsp; { alert (param2&#43;&quot;(second parameter) should be of type memo (text with mutiline) \n[function=&quot;&#43;fn[1]&#43;&quot;]&quot;);</p>
<p>&nbsp;&nbsp;&nbsp; return true;</p>
<p>&nbsp; }</p>
<p><br>
var plOptions=param1;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
<p>var plText=param2;&nbsp;&nbsp;&nbsp;</p>
<p>var plMenuItem=&quot;View Selected List&quot;;</p>
<p>var SelectedList_orig = crmForm.all[plText];</p>
<p>&nbsp;var FullList=crmForm.all[plOptions];</p>
<p>var SelCtr=-1;</p>
<p>var new_selColor=&quot;orange&quot;;</p>
<p>var&nbsp; new_deSelColor=&quot;white&quot;;</p>
<p>&nbsp;</p>
<p>if (param4!=null)</p>
<p>&nbsp;&nbsp;&nbsp; new_selColor=param4;</p>
<p>if (param5!=null)</p>
<p>&nbsp;&nbsp;&nbsp; new_deSelColor=param5;</p>
<p>var SelectedList =SelectedList_orig.value.split(&quot;\r\n&quot;);</p>
<p>&nbsp;</p>
<p>crmForm.all[plText].style.display=&quot;none&quot;;</p>
<p>if(FullList!=null &amp;&amp; SelectedList!=null)</p>
<p>&nbsp;&nbsp;&nbsp; {</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp; initColor();</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp; if (param3!=null &amp;&amp; param3!=&quot;&quot;)</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; plMenuItem=param3;</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p>&nbsp;&nbsp; else</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; plMenuItem=FullList.options[0].text;</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; changeColor(&quot;grey&quot;,0);</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp; for (var ctr=0; ctr&lt;SelectedList.length;ctr&#43;&#43;)</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>
selCtr=SelectedIndex(SelectedList[ctr]);</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; if (selCtr &gt;-1)</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>
changeColor(new_selColor,selCtr);</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p><strong>function SelectedIndex</strong>(selectedText)</p>
<p>{</p>
<p>var FullListText;</p>
<p>for (var ctr1=0; ctr1&lt;FullList.options.length;ctr1&#43;&#43;)</p>
<p>&nbsp;&nbsp;&nbsp; {</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; FullListText=FullList.options[ctr1].text;</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; if ( FullListText==selectedText)</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; return ctr1;</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p>return -2;</p>
<p>}</p>
<p>crmForm.all[plOptions].attachEvent('onchange', OnChangePL);</p>
<p><strong>function OnChangePL</strong>()</p>
<p>{</p>
<p>var SelCtr=-1;</p>
<p>var<br>
sel=crmForm.all[plOptions].SelectedText;</p>
<p>if (sel==plMenuItem)</p>
<p>return;</p>
<p>SelCtr=SelectedIndex(sel);</p>
<p>var SelColor=&quot;grey&quot;;</p>
<p>SelColor=crmForm.all[plOptions][SelCtr].style.backgroundColor;</p>
<p>if (SelColor==new_selColor)</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; changeColor(new_deSelColor,SelCtr);</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; saveChanges(sel,selCtr,&quot;del&quot;);</p>
<p>&nbsp;&nbsp;&nbsp; }</p>
<p>else</p>
<p>&nbsp;&nbsp;&nbsp; {</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; changeColor(new_selColor, SelCtr);&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; saveChanges(sel,selCtr,&quot;add&quot;);</p>
<p>&nbsp;&nbsp;&nbsp; }</p>
<p>}</p>
<p><strong>function saveChanges</strong>(p_selText,p_SelCtr,p_mode)</p>
<p>{</p>
<p>switch(p_mode)</p>
<p>{</p>
<p>&nbsp;&nbsp;&nbsp; case &quot;add&quot;:</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; SelectedList.push(p_selText);</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; break;</p>
<p>&nbsp;&nbsp;&nbsp; case &quot;del&quot;:</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; for (var ctr2=0;ctr2&lt;SelectedList.length;ctr2&#43;&#43;)</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; if (SelectedList[ctr2]==p_selText)</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>
SelectedList.splice(ctr2,1);</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; break;</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; break;</p>
<p>}</p>
<p><br>
Xrm.Page.getAttribute(plText).setValue(SelectedList.join(&quot;\r\n&quot;));&nbsp;</p>
<p>}</p>
<p><strong>function initColor</strong>()</p>
<p>{</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; for (var ctr3=0; ctr3&lt;FullList.options.length;ctr3&#43;&#43;)</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; changeColor(new_deSelColor, ctr3);&nbsp; &nbsp;}</p>
<p>}</p>
<p><strong>function changeColor</strong>(newColor, newCtr)</p>
<p>{</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;<br>
crmForm.all[plOptions][newCtr].style.backgroundColor=newColor;</p>
<p>}</p>
<p>}</p>
<p>catch (e)</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {&nbsp;<br>
alert (e.description);}</p>
<p>}</p>
<p>//*************************************END*******************************************************</p>
<p><strong>Step-by-step explanation:</strong></p>
<p><br>
<br>
</p>
<p><br>
<br>
</p>
<p><strong>Step-by-step explanation:</strong></p>
<p><br>
1&nbsp;Create an option set and a multiline text field . You can choose your own names<br>
Navigation: Contacts-&gt;Customize Contacts-&gt;Forms-&gt;formType Main-&gt;New field</p>
<p>&nbsp;<img id="67800" src="http://i1.code.msdn.s-msft.com/muti-select-pick-list-in-bf68d254/image/file/67800/1/crm_mspl2.png" alt="" width="1920" height="1200" style="float:left"></p>
<p>Add a field of type optionset, name<br>
the field, add header text as the first item and add list of values as shown<br>
below, save the changes</p>
<p><img id="67803" src="http://i1.code.msdn.s-msft.com/muti-select-pick-list-in-bf68d254/image/file/67803/1/crm_mspl2.jpg" alt="" width="1920" height="1200"></p>
<p><br>
<br>
Repeat the same procedure to add another field, name<br>
it,&nbsp; select Mutiple lines of text as type,<br>
see below screen shot</p>
<p><img id="67804" src="http://i1.code.msdn.s-msft.com/muti-select-pick-list-in-bf68d254/image/file/67804/1/crm_mspl3.5.jpg" alt="" width="1920" height="1200"><br>
<br>
</p>
<p>2&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>
On the display<br>
tab of the field properties uncheck all of the options, this is to hide the field<br>
as well as label</p>
<p><br>
<img id="67806" src="http://i1.code.msdn.s-msft.com/muti-select-pick-list-in-bf68d254/image/file/67806/1/crm_mspl4.jpg.png" alt="" width="1920" height="1200"></p>
<p>&nbsp;3&nbsp;&nbsp;Add the script to Web Resource library&nbsp;&nbsp;&nbsp; <br>
Navigate to:&nbsp;&nbsp;&nbsp; Settings-&gt;Customization-&gt;Cystomize the system-&gt;Components-&gt;Web Resource-&gt;New</p>
<p><img id="67807" src="http://i1.code.msdn.s-msft.com/muti-select-pick-list-in-bf68d254/image/file/67807/1/crm_mspl5.jpg.png" alt="" width="1920" height="1200"><br>
<br>
</p>
<p>&nbsp;Type a name for the javascript file,<br>
type a name for the Display Name, under type select Script (Jscipt), click on<br>
the Text Editor, paste our script, &nbsp;save.</p>
<p><br>
<img id="67808" src="http://i1.code.msdn.s-msft.com/muti-select-pick-list-in-bf68d254/image/file/67808/1/crm_mspl5.jpg" alt="" width="950" height="600" style="float:left"></p>
<p>&nbsp;4&nbsp;&nbsp;&nbsp;&nbsp;Register a function on the onload event of the form and name the function as MultiPickList3</p>
<p>Navigation: Contacts-&gt;Customize Contacts-&gt;Forms-&gt;formType= Main-&gt;Form Properties</p>
<p>Under Event Handler select Form on Control, onLoad for Event-&gt;Add</p>
<p><img id="67809" src="http://i1.code.msdn.s-msft.com/muti-select-pick-list-in-bf68d254/image/file/67809/1/crm_mspl6.jpg" alt="" width="1920" height="1200"></p>
<p>Under Library select the javascript u registered earliar, under Function type MultiPickList3, under parameters type your field names as given on top of the page press OK save and Publish</p>
<p><img id="67810" src="http://i1.code.msdn.s-msft.com/muti-select-pick-list-in-bf68d254/image/file/67810/1/crm_mspl7.jpg" alt="" width="1098" height="1215" style="float:left"></p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>

<div class="preview">
<pre class="js"><span class="js__operator">function</span>&nbsp;MultiPickList3(param1,&nbsp;param2,&nbsp;param3,param4,param5)&nbsp;
<span class="js__brace">{</span>&nbsp;
<span class="js__sl_comment">//************************************************************************************************</span>&nbsp;
<span class="js__sl_comment">//******&nbsp;&nbsp;Solution:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Multi&nbsp;Select&nbsp;Pick&nbsp;List&nbsp;function:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="js__sl_comment">//******&nbsp;&nbsp;Platform:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Microsoft&nbsp;Dynamics&nbsp;CRM&nbsp;2011&nbsp;</span>&nbsp;
<span class="js__sl_comment">//******&nbsp;&nbsp;Last&nbsp;updated&nbsp;date:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;25th&nbsp;Apirl&nbsp;2012</span>&nbsp;
<span class="js__sl_comment">//******&nbsp;&nbsp;Created&nbsp;by:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Aroop&nbsp;Vachali,&nbsp;aroopv@gmail.com,&nbsp;Company:&nbsp;Central&nbsp;Equity,&nbsp;Melbourne,&nbsp;Australia</span>&nbsp;
<span class="js__sl_comment">//&nbsp;detailed&nbsp;step-by-step&nbsp;instructions&nbsp;in&nbsp;MultiSelectPickListCRM2011Readme.docx&nbsp;file</span>&nbsp;
<span class="js__sl_comment">//************************************************************************************************</span>&nbsp;
<span class="js__sl_comment">//****&nbsp;Courtsey:&nbsp;Karen&nbsp;Carvalho&nbsp;(Marketing&nbsp;Manager),&nbsp;Christine&nbsp;Suell&nbsp;(Supervisor),&nbsp;Google&nbsp;and&nbsp;all&nbsp;bloggers&nbsp;(for&nbsp;many&nbsp;</span>&nbsp;
<span class="js__sl_comment">//&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;of&nbsp;the&nbsp;syntaxes)</span>&nbsp;
<span class="js__sl_comment">//************************************************************************************************</span>&nbsp;
&nbsp;
<span class="js__statement">try</span>&nbsp;
<span class="js__brace">{</span>&nbsp;
<span class="js__statement">var</span>&nbsp;fn&nbsp;=&nbsp;<span class="js__property">arguments</span>.callee.toString().match(<span class="js__reg_exp">/function\s&#43;([^\s\(]&#43;)/</span>);&nbsp;&nbsp;
&nbsp;
<span class="js__statement">if</span>&nbsp;(param1==null&nbsp;||&nbsp;param2==null)&nbsp;
<span class="js__brace">{</span>&nbsp;
alert(<span class="js__string">&quot;Error:&nbsp;Parameter&nbsp;missing.&nbsp;\n&nbsp;Format:&nbsp;&lt;optionset&nbsp;fieldname&gt;&nbsp;,&nbsp;&nbsp;&lt;option&nbsp;value&nbsp;text&nbsp;field&gt;&nbsp;,&nbsp;&nbsp;[&lt;option&nbsp;header&gt;],&nbsp;[&lt;select&nbsp;color&gt;]&nbsp;&nbsp;&nbsp;,&nbsp;&nbsp;&nbsp;[&lt;deselect&nbsp;color&gt;]&nbsp;,&nbsp;&nbsp;\n&nbsp;[&quot;</span>&#43;<span class="js__string">&quot;Function=&quot;</span>&#43;fn[<span class="js__num">1</span>]&#43;<span class="js__string">&quot;]&quot;</span>&nbsp;&nbsp;);&nbsp;
<span class="js__statement">return</span>&nbsp;true;&nbsp;
<span class="js__brace">}</span>&nbsp;
&nbsp;
<span class="js__statement">var</span>&nbsp;tparamtype=Xrm.Page.data.entity.attributes.get(param1).getAttributeType();&nbsp;
<span class="js__statement">if</span>&nbsp;(tparamtype!=<span class="js__string">&quot;optionset&quot;</span>)&nbsp;
&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;alert&nbsp;(param1&#43;<span class="js__string">&quot;(first&nbsp;parameter)&nbsp;should&nbsp;be&nbsp;of&nbsp;type&nbsp;optionset&nbsp;\n&quot;</span>&#43;<span class="js__string">&quot;[function=&quot;</span>&#43;fn[<span class="js__num">1</span>]&#43;<span class="js__string">&quot;]&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;true;&nbsp;
&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
<span class="js__statement">var</span>&nbsp;tparamtype=Xrm.Page.data.entity.attributes.get(param2).getAttributeType();&nbsp;
<span class="js__statement">if</span>&nbsp;(tparamtype!=<span class="js__string">&quot;memo&quot;</span>)&nbsp;
&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;alert&nbsp;(param2&#43;<span class="js__string">&quot;(second&nbsp;parameter)&nbsp;should&nbsp;be&nbsp;of&nbsp;type&nbsp;memo&nbsp;(text&nbsp;with&nbsp;mutiline)&nbsp;\n[function=&quot;</span>&#43;fn[<span class="js__num">1</span>]&#43;<span class="js__string">&quot;]&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;true;&nbsp;
&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
<span class="js__statement">var</span>&nbsp;plOptions=param1;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<span class="js__statement">var</span>&nbsp;plText=param2;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<span class="js__statement">var</span>&nbsp;plMenuItem=<span class="js__string">&quot;View&nbsp;Selected&nbsp;List&quot;</span>;&nbsp;
<span class="js__statement">var</span>&nbsp;SelectedList_orig&nbsp;=&nbsp;crmForm.all[plText];&nbsp;
<span class="js__statement">var</span>&nbsp;FullList=crmForm.all[plOptions];&nbsp;
&nbsp;
<span class="js__statement">var</span>&nbsp;SelCtr=-<span class="js__num">1</span>;&nbsp;
<span class="js__statement">var</span>&nbsp;new_selColor=<span class="js__string">&quot;orange&quot;</span>;&nbsp;
<span class="js__statement">var</span>&nbsp;new_deSelColor=<span class="js__string">&quot;white&quot;</span>;&nbsp;
<span class="js__statement">if</span>&nbsp;(param4!=null)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;new_selColor=param4;&nbsp;
<span class="js__statement">if</span>&nbsp;(param5!=null)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;new_deSelColor=param5;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;
<span class="js__statement">var</span>&nbsp;SelectedList&nbsp;=SelectedList_orig.value.split(<span class="js__string">&quot;\r\n&quot;</span>);&nbsp;
crmForm.all[plText].style.display=<span class="js__string">&quot;none&quot;</span>;&nbsp;
&nbsp;
<span class="js__statement">if</span>(FullList!=null&nbsp;&amp;&amp;&nbsp;SelectedList!=null)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;initColor();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(param3!=null&nbsp;&amp;&amp;&nbsp;param3!=<span class="js__string">&quot;&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;plMenuItem=param3;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;<span class="js__statement">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;plMenuItem=FullList.options[<span class="js__num">0</span>].text;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;changeColor(<span class="js__string">&quot;grey&quot;</span>,<span class="js__num">0</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">for</span>&nbsp;(<span class="js__statement">var</span>&nbsp;ctr=<span class="js__num">0</span>;&nbsp;ctr&lt;SelectedList.length;ctr&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;selCtr=SelectedIndex(SelectedList[ctr]);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(selCtr&nbsp;&gt;-<span class="js__num">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;changeColor(new_selColor,selCtr);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
<span class="js__operator">function</span>&nbsp;SelectedIndex(selectedText)&nbsp;
<span class="js__brace">{</span>&nbsp;
<span class="js__statement">var</span>&nbsp;FullListText;&nbsp;
<span class="js__statement">for</span>&nbsp;(<span class="js__statement">var</span>&nbsp;ctr1=<span class="js__num">0</span>;&nbsp;ctr1&lt;FullList.options.length;ctr1&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FullListText=FullList.options[ctr1].text;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(&nbsp;FullListText==selectedText)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;ctr1;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
<span class="js__statement">return</span>&nbsp;-<span class="js__num">2</span>;&nbsp;
<span class="js__brace">}</span>&nbsp;
&nbsp;
crmForm.all[plOptions].attachEvent(<span class="js__string">'onchange'</span>,&nbsp;OnChangePL);&nbsp;
&nbsp;
<span class="js__operator">function</span>&nbsp;OnChangePL()&nbsp;&nbsp;
<span class="js__brace">{</span>&nbsp;
<span class="js__statement">var</span>&nbsp;SelCtr=-<span class="js__num">1</span>;&nbsp;
<span class="js__statement">var</span>&nbsp;sel=crmForm.all[plOptions].SelectedText;&nbsp;
&nbsp;
<span class="js__statement">if</span>&nbsp;(sel==plMenuItem)&nbsp;
<span class="js__statement">return</span>;&nbsp;
&nbsp;
SelCtr=SelectedIndex(sel);&nbsp;
<span class="js__statement">var</span>&nbsp;SelColor=<span class="js__string">&quot;grey&quot;</span>;&nbsp;&nbsp;
SelColor=crmForm.all[plOptions][SelCtr].style.backgroundColor;&nbsp;
&nbsp;
<span class="js__statement">if</span>&nbsp;(SelColor==new_selColor)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;changeColor(new_deSelColor,SelCtr);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;saveChanges(sel,selCtr,<span class="js__string">&quot;del&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
<span class="js__statement">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;changeColor(new_selColor,&nbsp;SelCtr);&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;saveChanges(sel,selCtr,<span class="js__string">&quot;add&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
<span class="js__brace">}</span>&nbsp;
&nbsp;
<span class="js__operator">function</span>&nbsp;saveChanges(p_selText,p_SelCtr,p_mode)&nbsp;
<span class="js__brace">{</span>&nbsp;
<span class="js__statement">switch</span>(p_mode)&nbsp;
<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">case</span>&nbsp;<span class="js__string">&quot;add&quot;</span>:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SelectedList.push(p_selText);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">break</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">case</span>&nbsp;<span class="js__string">&quot;del&quot;</span>:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">for</span>&nbsp;(<span class="js__statement">var</span>&nbsp;ctr2=<span class="js__num">0</span>;ctr2&lt;SelectedList.length;ctr2&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(SelectedList[ctr2]==p_selText)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SelectedList.splice(ctr2,<span class="js__num">1</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">break</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">break</span>;&nbsp;
<span class="js__brace">}</span>&nbsp;
Xrm.Page.getAttribute(plText).setValue(SelectedList.join(<span class="js__string">&quot;\r\n&quot;</span>));&nbsp;&nbsp;&nbsp;
<span class="js__brace">}</span>&nbsp;
&nbsp;
<span class="js__operator">function</span>&nbsp;initColor()&nbsp;
<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">for</span>&nbsp;(<span class="js__statement">var</span>&nbsp;ctr3=<span class="js__num">0</span>;&nbsp;ctr3&lt;FullList.options.length;ctr3&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;changeColor(new_deSelColor,&nbsp;ctr3);&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
<span class="js__brace">}</span>&nbsp;
&nbsp;
<span class="js__operator">function</span>&nbsp;changeColor(newColor,&nbsp;newCtr)&nbsp;
<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;crmForm.all[plOptions][newCtr].style.backgroundColor=newColor;&nbsp;
<span class="js__brace">}</span>&nbsp;
&nbsp;
<span class="js__brace">}</span>&nbsp;
<span class="js__statement">catch</span>&nbsp;(e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;&nbsp;alert&nbsp;(e.description);<span class="js__brace">}</span>&nbsp;
<span class="js__brace">}</span>&nbsp;
<span class="js__sl_comment">//*************************************END*******************************************************</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em>source code file name #1 - summary for this source code file.</em> </li><li><em><em>source code file name #2 - summary for this source code file.</em></em>
</li></ul>
<h1>More Information</h1>
<p><em>For more information on X, see ...?</em></p>
