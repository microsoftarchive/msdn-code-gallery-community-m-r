# MSCRM - How to call Rest Service from Plugins
## Requires
- Visual Studio 2012
## License
- Apache License, Version 2.0
## Technologies
- REST
- Dynamics CRM
- Microsoft Dynamics CRM 2011
- Microsoft CRM SDK
- C# Microsoft Dynamics CRM 2011
- Microsoft Dynamics CRM 2013
## Topics
- Plug-in
## Updated
- 04/09/2014
## Description

<p style="background-color:#ffffff; border:0px; margin:0px 0px 24px; padding:0px; vertical-align:baseline; color:#333333; font-family:Georgia,'Bitstream Charter',serif; font-size:16px; font-style:normal; font-variant:normal; font-weight:normal; letter-spacing:normal; line-height:24px; orphans:2; text-align:start; text-indent:0px; text-transform:none; white-space:normal; widows:2; word-spacing:0px">
In many case we need to integrate data from external source. In my solutions I needed to load and view information from external source in our CRM.&nbsp; First of all we created the Rest service to expones the right call at our external source.</p>
<p style="background-color:#ffffff; border:0px; margin:0px 0px 24px; padding:0px; vertical-align:baseline; color:#333333; font-family:Georgia,'Bitstream Charter',serif; font-size:16px; font-style:normal; font-variant:normal; font-weight:normal; letter-spacing:normal; line-height:24px; orphans:2; text-align:start; text-indent:0px; text-transform:none; white-space:normal; widows:2; word-spacing:0px">
<strong style="background-color:transparent; border:0px; margin:0px; padding:0px; vertical-align:baseline; font-weight:bold">Step 1: Create the Service Rest</strong></p>
<p style="background-color:#ffffff; border:0px; margin:0px 0px 24px; padding:0px; vertical-align:baseline; color:#333333; font-family:Georgia,'Bitstream Charter',serif; font-size:16px; font-style:normal; font-variant:normal; font-weight:normal; letter-spacing:normal; line-height:24px; orphans:2; text-align:start; text-indent:0px; text-transform:none; white-space:normal; widows:2; word-spacing:0px">
<strong style="background-color:transparent; border:0px; margin:0px; padding:0px; vertical-align:baseline; font-weight:bold"><strong style="background-color:transparent; border:0px; margin:0px; padding:0px; vertical-align:baseline; font-weight:bold">Step 2:
</strong>Deploy the Service Rest</strong></p>
<p style="background-color:#ffffff; border:0px; margin:0px 0px 24px; padding:0px; vertical-align:baseline; color:#333333; font-family:Georgia,'Bitstream Charter',serif; font-size:16px; font-style:normal; font-variant:normal; font-weight:normal; letter-spacing:normal; line-height:24px; orphans:2; text-align:start; text-indent:0px; text-transform:none; white-space:normal; widows:2; word-spacing:0px">
<strong style="background-color:transparent; border:0px; margin:0px; padding:0px; vertical-align:baseline; font-weight:bold"><strong style="background-color:transparent; border:0px; margin:0px; padding:0px; vertical-align:baseline; font-weight:bold">Step 3:
</strong>Write plugin to consume the Service Rest</strong></p>
<p style="background-color:#ffffff; border:0px; margin:0px 0px 24px; padding:0px; vertical-align:baseline; color:#333333; font-family:Georgia,'Bitstream Charter',serif; font-size:16px; font-style:normal; font-variant:normal; font-weight:normal; letter-spacing:normal; line-height:24px; orphans:2; text-align:start; text-indent:0px; text-transform:none; white-space:normal; widows:2; word-spacing:0px">
<span style="text-decoration:underline"><strong style="background-color:transparent; border:0px; margin:0px; padding:0px; vertical-align:baseline; font-weight:bold">On internet you can find a lot of tutorial and manul that tey discuss about the right step to
 create a rest service. This sample will explain only the right </strong><strong style="background-color:transparent; border:0px; margin:0px; padding:0px; vertical-align:baseline; font-weight:bold">way to youse inside the plugin</strong></span><strong style="background-color:transparent; border:0px; margin:0px; padding:0px; vertical-align:baseline; font-weight:bold"><br>
</strong></p>
<p style="background-color:#ffffff; border:0px; margin:0px 0px 24px; padding:0px; vertical-align:baseline; color:#333333; font-family:Georgia,'Bitstream Charter',serif; font-size:16px; font-style:normal; font-variant:normal; font-weight:normal; letter-spacing:normal; line-height:24px; orphans:2; text-align:start; text-indent:0px; text-transform:none; white-space:normal; widows:2; word-spacing:0px">
<em><strong style="background-color:transparent; border:0px; margin:0px; padding:0px; vertical-align:baseline; font-weight:bold">In our Plugin we call this
</strong><strong style="background-color:transparent; border:0px; margin:0px; padding:0px; vertical-align:baseline; font-weight:bold">rest service:</strong></em><strong style="background-color:transparent; border:0px; margin:0px; padding:0px; vertical-align:baseline; font-weight:bold"><br>
</strong></p>
<ol style="background-color:#ffffff; border:0px; margin:0px 0px 24px 1.5em; padding:0px; vertical-align:baseline; list-style:decimal; color:#333333; font-family:Georgia,'Bitstream Charter',serif; font-size:16px; font-style:normal; font-variant:normal; font-weight:normal; letter-spacing:normal; line-height:24px; orphans:2; text-align:start; text-indent:0px; text-transform:none; white-space:normal; widows:2; word-spacing:0px">
<li style="background-color:transparent; border:0px; margin:0px; padding:0px; vertical-align:baseline">
Create new Dynamics 2011 CRM Plugin Library projects </li><li style="background-color:transparent; border:0px; margin:0px; padding:0px; vertical-align:baseline">
Add the above generated .CS class into the plugin solution. In&nbsp;Visual Studio, select
<strong style="background-color:transparent; border:0px; margin:0px; padding:0px; vertical-align:baseline; font-weight:bold">
CRM Explorer</strong>, open <strong>entities node </strong>and select an entity (in example I use the Account). click right-click on entity and select Create plugin
</li><li style="background-color:transparent; border:0px; margin:0px; padding:0px; vertical-align:baseline">
Set all information (In the example I choose <strong>Update</strong>) </li><li style="background-color:transparent; border:0px; margin:0px; padding:0px; vertical-align:baseline">
Open PostAccountUpdate.cs </li><li style="background-color:transparent; border:0px; margin:0px; padding:0px; vertical-align:baseline">
And call the Rest Service. </li></ol>
<p style="background-color:#ffffff; border:0px none; margin:0px 0px 24px; padding:0px 0px 0px 60px; vertical-align:baseline; color:#333333; font-family:Georgia,'Bitstream Charter',serif; font-size:16px; font-style:normal; font-variant:normal; font-weight:normal; letter-spacing:normal; line-height:24px; orphans:2; text-align:start; text-indent:0px; text-transform:none; white-space:normal; widows:2; word-spacing:0px">
To use it we need<code> </code><span style="background-color:#ffff00; color:#ff0000"><code>Newtonsoft.Json library.<br>
</code></span></p>
<p style="background-color:#ffffff; border:0px none; margin:0px 0px 24px; padding:0px 0px 0px 60px; vertical-align:baseline; color:#333333; font-family:Georgia,'Bitstream Charter',serif; font-size:16px; font-style:normal; font-variant:normal; font-weight:normal; letter-spacing:normal; line-height:24px; orphans:2; text-align:start; text-indent:0px; text-transform:none; white-space:normal; widows:2; word-spacing:0px">
<span style="background-color:#ffff00; color:#ff0000">You can install the library from Nuget Packages:</span></p>
<p style="background-color:#ffffff; border:0px none; margin:0px 0px 24px; padding:0px 0px 0px 60px; vertical-align:baseline; color:#333333; font-family:Georgia,'Bitstream Charter',serif; font-size:16px; font-style:normal; font-variant:normal; font-weight:normal; letter-spacing:normal; line-height:24px; orphans:2; text-align:start; text-indent:0px; text-transform:none; white-space:normal; widows:2; word-spacing:0px">
<span style="background-color:#ffff00; color:#ff0000">Hera the instruction : http://www.nuget.org/packages/Newtonsoft.Json</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">var uri = new Uri(&quot;http://localhost:9090/GetHelloWorld&quot;);

                var request = WebRequest.Create(uri);
                request.Method = WebRequestMethods.Http.Get;
                request.ContentType = &quot;application/json&quot;;
                try
                {
                    string helloWorldString = &quot;&quot;;
                    using (var response = request.GetResponse())
                    {
                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            string tmp = reader.ReadToEnd();
                            helloWorldString = JsonConvert.DeserializeObject&lt;string&gt;(tmp);
                            Debug.Print(&quot;I read this string {0}: &quot;, helloWorldString);
                        }
                    }

                    var entity = (Entity) context.InputParameters[&quot;Target&quot;];
                    if (entity.Contains(&quot;description&quot;))
                        entity.Attributes[&quot;description&quot;] = helloWorldString;
                    else
                        entity.Attributes.Add(&quot;description&quot;, helloWorldString);

                    //Update Sales Order Entity
                    if (context.Depth == 1)
                        service.Update(entity);

                }
                catch (Exception ex)
                {
                    throw;
                }</pre>
<div class="preview">
<pre class="csharp">var&nbsp;uri&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Uri(<span class="cs__string">&quot;http://localhost:9090/GetHelloWorld&quot;</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;request&nbsp;=&nbsp;WebRequest.Create(uri);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;request.Method&nbsp;=&nbsp;WebRequestMethods.Http.Get;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;request.ContentType&nbsp;=&nbsp;<span class="cs__string">&quot;application/json&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">try</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;helloWorldString&nbsp;=&nbsp;<span class="cs__string">&quot;&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">using</span>&nbsp;(var&nbsp;response&nbsp;=&nbsp;request.GetResponse())&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">using</span>&nbsp;(var&nbsp;reader&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;StreamReader(response.GetResponseStream()))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;tmp&nbsp;=&nbsp;reader.ReadToEnd();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;helloWorldString&nbsp;=&nbsp;JsonConvert.DeserializeObject&lt;<span class="cs__keyword">string</span>&gt;(tmp);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debug.Print(<span class="cs__string">&quot;I&nbsp;read&nbsp;this&nbsp;string&nbsp;{0}:&nbsp;&quot;</span>,&nbsp;helloWorldString);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;entity&nbsp;=&nbsp;(Entity)&nbsp;context.InputParameters[<span class="cs__string">&quot;Target&quot;</span>];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(entity.Contains(<span class="cs__string">&quot;description&quot;</span>))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;entity.Attributes[<span class="cs__string">&quot;description&quot;</span>]&nbsp;=&nbsp;helloWorldString;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;entity.Attributes.Add(<span class="cs__string">&quot;description&quot;</span>,&nbsp;helloWorldString);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Update&nbsp;Sales&nbsp;Order&nbsp;Entity</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(context.Depth&nbsp;==&nbsp;<span class="cs__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;service.Update(entity);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">catch</span>&nbsp;(Exception&nbsp;ex)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">throw</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em>PostAccountUpdate - Post update for account.</em> </li></ul>
<h1>More Information</h1>
<p><em><em>For more information please contact me .... <a title="contact me" href="http://nothingnessit.wordpress.com/" target="_blank">
http://nothingnessit.wordpress.com/</a></em></em></p>
