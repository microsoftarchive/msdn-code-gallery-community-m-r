# Middleware And Staticfiles In ASP.NET Core 1.0 - Part One
## Requires
- Visual Studio 2015
## License
- MIT
## Technologies
- C#
- ASP.NET
- ASP.NET Core 1.0
- ASP.NET Core
- ASP.NET Core 1.0.1
## Topics
- C#
- ASP.NET
- .NET Core
- ASP.NET Core 1.0
- ASP.NET Core
## Updated
- 12/30/2016
## Description

<h1>Introduction</h1>
<p><span style="font-size:16px">We are all familiar with asp.net HttpHandler and HttpModules but unfortunately both are gone in Asp.Net Core 1.0. Don't worry!! They are replaced with an efficient and easy-to-implement approach called &quot;Middleware.&quot; We can say
 reusable classes are middleware or middleware components.<br>
</span><br>
<span style="font-size:16px">In Asp.net HttpHandler and HttpModules are configured through webconfig but in Middleware they are configured via code rather than web.config. We can add the middleware components code in Startup.cs file in ASP.NET Core 1.0.<br>
</span></p>
<h1>Built-in Middleware</h1>
<div></div>
<p><span style="font-size:16px">The following are the Built-in Middlewares in ASP.Net Core 1.0.<br>
</span><br>
<a href="http://social.technet.microsoft.com/wiki/cfs-file.ashx/__key/communityserver-wikis-components-files/00-00-00-00-05/1805.image001.jpg"><img src=":-1805.image001.jpg" alt="" style="border-width:0px; border-style:solid"></a><br>
<em>Pic Source By : Microsoft ASP.NET Core Documents</em><br>
<br>
</p>
<h1>Single Request Delegate</h1>
<ul>
<li>
<h5>app.Run</h5>
</li><li>
<h5>app.Use</h5>
</li></ul>
<h1>app.Run</h1>
<p><br>
<span style="font-size:16px">App.Run is a single request delegate that handles all requests. If you want to call the next delegate request then you can use
<em><strong>&quot;</strong>next<strong>&quot;</strong></em> keyword in lambda expression. The Run method short circuits the pipeline or terminates the pipeline ( It will not call a next request delegate). Run method should only be called at the end of your pipeline or
 it will call at last.</span><br>
<br>
<span style="font-size:16px"><strong><em>Example app.Run<br>
</em></strong></span></p>
<div class="reCodeBlock" style="border:1px solid #7f9db9; overflow-y:auto">
<div style="background-color:#ffffff"><span style="margin-left:0px!important"><code style="color:#000000">app.Run(async (context) =&gt;&nbsp;
</code></span></div>
<div style="background-color:#f8f8f8"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:36px!important"><code style="color:#000000">{&nbsp;
</code></span></span></div>
<div style="background-color:#ffffff"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:48px!important"><code style="color:#000000">await context.Response.WriteAsync(</code><code style="color:blue">&quot;
 Welcome to Dotnet Core !!&quot;</code><code style="color:#000000">);&nbsp; </code></span></span></div>
<div style="background-color:#f8f8f8"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:57px!important">&nbsp;</span></span></div>
<div style="background-color:#ffffff"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:36px!important"><code style="color:#000000">});</code></span></span></div>
</div>
<p>&nbsp;</p>
<h1>app.Use</h1>
<p><br>
<span style="font-size:16px">The following code clearly mentions that app.Run method should only be called at the end of your pipeline or it will be called last. App use will take care of the next delegate request with the help of &nbsp;<em><strong>&quot;</strong>next<strong>&quot;</strong></em>.&nbsp;<br>
</span><br>
<strong><em><span style="font-size:16px">Example app.Use<br>
</span></em></strong></p>
<div class="reCodeBlock" style="border:1px solid #7f9db9; overflow-y:auto">
<div style="background-color:#ffffff"><span style="margin-left:0px!important"><code style="color:#000000">app.Use(async (context, next) =&gt;&nbsp;
</code></span></div>
<div style="background-color:#f8f8f8"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:36px!important"><code style="color:#000000">{&nbsp;
</code></span></span></div>
<div style="background-color:#ffffff"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:48px!important"><code style="color:#000000">await context.Response.WriteAsync(</code><code style="color:blue">&quot;
 Hello World !!&quot;</code><code style="color:#000000">);&nbsp; </code></span></span></div>
<div style="background-color:#f8f8f8"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:48px!important"><code style="color:#000000">await next.Invoke();</code><code style="color:#008200">//mandatory
 for invoking next delegates request&nbsp; </code></span></span></div>
<div style="background-color:#ffffff"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:36px!important"><code style="color:#000000">});&nbsp;
</code></span></span></div>
<div style="background-color:#f8f8f8"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:36px!important"><code style="color:#000000">app.Run(async (context) =&gt;&nbsp;
</code></span></span></div>
<div style="background-color:#ffffff"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:36px!important"><code style="color:#000000">{&nbsp;
</code></span></span></div>
<div style="background-color:#f8f8f8"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:48px!important"><code style="color:#000000">await context.Response.WriteAsync(</code><code style="color:blue">&quot;
 Welcome to Dotnet Core !!&quot;</code><code style="color:#000000">);&nbsp; </code></span></span></div>
<div style="background-color:#ffffff"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:57px!important">&nbsp;</span></span></div>
<div style="background-color:#f8f8f8"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:36px!important"><code style="color:#000000">});</code></span></span></div>
</div>
<p>&nbsp;</p>
<h1>StaticFiles</h1>
<p><br>
<span style="font-size:16px">The new folder in ASP.NET Core is wwwroot and it stores all the StaticFiles in our project. The StaticFiles means that the HTML files, CSS files, image files, and JavaScript files which are sent to the users&rsquo; browsers should
 be stored inside the wwwroot folder.<br>
</span></p>
<h1>Startup Page</h1>
<p><br>
Right Click &quot;wwwroot&quot; and click Add -&gt; New Item -&gt; Click &quot;Client-side&quot; sub category and select HTML Page.<br>
<br>
<a href="http://social.technet.microsoft.com/wiki/cfs-file.ashx/__key/communityserver-wikis-components-files/00-00-00-00-05/5314.image002.jpg"><img src=":-5314.image002.jpg" alt="" style="border-width:0px; border-style:solid"></a><br>
<em><span style="font-size:10px">Pic Source By :&nbsp;<a title="RajeeshMenoth Blog" href="https://rajeeshmenoth.wordpress.com/" target="_blank">RajeeshMenoth Blog</a><br>
</span></em></p>
<h1>HTML Index Page Code</h1>
<p><br>
<a href="http://social.technet.microsoft.com/wiki/cfs-file.ashx/__key/communityserver-wikis-components-files/00-00-00-00-05/4380.image003.jpg"><img src=":-4380.image003.jpg" alt="" style="border-width:0px; border-style:solid"></a><br>
<span style="font-size:10px"><em>Pic Source By :&nbsp;<a title="RajeeshMenoth Blog" href="https://rajeeshmenoth.wordpress.com/" target="_blank">RajeeshMenoth Blog</a><br>
</em></span></p>
<h1>Output</h1>
<p><span style="font-size:16px">When we run our application then you will get the following: &nbsp;Why are our StaticFiles not running ? Because StaticFiles are placed inside the &quot;wwwroot&quot; and when we want to call those files in ASP.NET Core 1.0 then you must
 install the StaticFiles package manager for ASP.NET Core through NuGet.<br>
</span><br>
<a href="http://social.technet.microsoft.com/wiki/cfs-file.ashx/__key/communityserver-wikis-components-files/00-00-00-00-05/6874.image004.jpg"><img src=":-6874.image004.jpg" alt="" style="border-width:0px; border-style:solid"></a><br>
<em><span style="font-size:10px">Pic Source By :&nbsp;<a title="RajeeshMenoth Blog" href="https://rajeeshmenoth.wordpress.com/" target="_blank">RajeeshMenoth Blog</a><br>
</span></em></p>
<h1>StaticFiles Configuration ASP.NET Core 1.0</h1>
<p><br>
<span style="font-size:16px">Go to NuGet Package Manager and Type StaticFiles in &quot;Browse&quot; Category. Then it will display many staticfiles details but we need to choose and Install
<em>&quot;Microsoft.AspNetCore.StaticFiles&quot;</em>.&nbsp;<br>
</span><br>
<a href="http://social.technet.microsoft.com/wiki/cfs-file.ashx/__key/communityserver-wikis-components-files/00-00-00-00-05/6758.image005.jpg"><img src=":-6758.image005.jpg" alt="" style="border-width:0px; border-style:solid"></a><br>
<em style="font-size:12.1px"><span style="font-size:10px">Pic Source By :&nbsp;<a title="RajeeshMenoth Blog" href="https://rajeeshmenoth.wordpress.com/" target="_blank">RajeeshMenoth Blog</a><br>
</span></em></p>
<h1>project.Json</h1>
<p><br>
<span style="font-size:16px">Now Staticfiles version is updated in project.Json file.<br>
</span></p>
<div class="reCodeBlock" style="border:1px solid #7f9db9; overflow-y:auto">
<div style="background-color:#ffffff"><span style="margin-left:0px!important"><code style="color:#000000">{&nbsp;&nbsp;&nbsp;
</code></span></div>
<div style="background-color:#f8f8f8"><span><code>&nbsp;&nbsp;</code><span style="margin-left:6px!important"><code style="color:blue">&quot;dependencies&quot;</code><code style="color:#000000">: {&nbsp;&nbsp;&nbsp;
</code></span></span></div>
<div style="background-color:#ffffff"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:12px!important"><code style="color:blue">&quot;Microsoft.NETCore.App&quot;</code><code style="color:#000000">: {&nbsp;&nbsp;&nbsp;
</code></span></span></div>
<div style="background-color:#f8f8f8"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:18px!important"><code style="color:blue">&quot;version&quot;</code><code style="color:#000000">:
</code><code style="color:blue">&quot;1.0.1&quot;</code><code style="color:#000000">,&nbsp;&nbsp;&nbsp;
</code></span></span></div>
<div style="background-color:#ffffff"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:18px!important"><code style="color:blue">&quot;type&quot;</code><code style="color:#000000">:
</code><code style="color:blue">&quot;platform&quot;</code>&nbsp;&nbsp;</span></span></div>
<div style="background-color:#f8f8f8"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:12px!important"><code style="color:#000000">},&nbsp;&nbsp;&nbsp;
</code></span></span></div>
<div style="background-color:#ffffff"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:12px!important"><code style="color:blue">&quot;Microsoft.AspNetCore.Diagnostics&quot;</code><code style="color:#000000">:
</code><code style="color:blue">&quot;1.0.0&quot;</code><code style="color:#000000">,&nbsp;&nbsp;&nbsp;
</code></span></span></div>
<div style="background-color:#f8f8f8"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:12px!important"><code style="color:blue">&quot;Microsoft.AspNetCore.Server.IISIntegration&quot;</code><code style="color:#000000">:
</code><code style="color:blue">&quot;1.0.0&quot;</code><code style="color:#000000">,&nbsp;&nbsp;&nbsp;
</code></span></span></div>
<div style="background-color:#ffffff"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:12px!important"><code style="color:blue">&quot;Microsoft.AspNetCore.Server.Kestrel&quot;</code><code style="color:#000000">:
</code><code style="color:blue">&quot;1.0.1&quot;</code><code style="color:#000000">,&nbsp;&nbsp;&nbsp;
</code></span></span></div>
<div style="background-color:#f8f8f8"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:12px!important"><code style="color:blue">&quot;Microsoft.Extensions.Logging.Console&quot;</code><code style="color:#000000">:
</code><code style="color:blue">&quot;1.0.0&quot;</code><code style="color:#000000">,&nbsp;&nbsp;&nbsp;
</code></span></span></div>
<div style="background-color:#ffffff"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:12px!important"><code style="color:blue">&quot;Microsoft.AspNetCore.Mvc&quot;</code><code style="color:#000000">:
</code><code style="color:blue">&quot;1.0.1&quot;</code><code style="color:#000000">,&nbsp;
</code></span></span></div>
<div style="background-color:#f8f8f8"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:12px!important"><code style="color:blue">&quot;Microsoft.AspNetCore.StaticFiles&quot;</code><code style="color:#000000">:
</code><code style="color:blue">&quot;1.1.0&quot;</code>&nbsp; <code style="color:#008200">
// Staticfiles dependency&nbsp;&nbsp; </code></span></span></div>
<div style="background-color:#ffffff"><span><code>&nbsp;&nbsp;</code><span style="margin-left:6px!important"><code style="color:#000000">},&nbsp;&nbsp;&nbsp;
</code></span></span></div>
<div style="background-color:#f8f8f8"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:18px!important">&nbsp;</span></span></div>
<div style="background-color:#ffffff"><span><code>&nbsp;&nbsp;</code><span style="margin-left:6px!important"><code style="color:blue">&quot;tools&quot;</code><code style="color:#000000">: {&nbsp;&nbsp;&nbsp;
</code></span></span></div>
<div style="background-color:#f8f8f8"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:12px!important"><code style="color:blue">&quot;Microsoft.AspNetCore.Server.IISIntegration.Tools&quot;</code><code style="color:#000000">:
</code><code style="color:blue">&quot;1.0.0-preview2-final&quot;</code>&nbsp;&nbsp;</span></span></div>
<div style="background-color:#ffffff"><span><code>&nbsp;&nbsp;</code><span style="margin-left:6px!important"><code style="color:#000000">},&nbsp;&nbsp;&nbsp;
</code></span></span></div>
<div style="background-color:#f8f8f8"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:18px!important">&nbsp;</span></span></div>
<div style="background-color:#ffffff"><span><code>&nbsp;&nbsp;</code><span style="margin-left:6px!important"><code style="color:blue">&quot;frameworks&quot;</code><code style="color:#000000">: {&nbsp;&nbsp;&nbsp;
</code></span></span></div>
<div style="background-color:#f8f8f8"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:12px!important"><code style="color:blue">&quot;netcoreapp1.0&quot;</code><code style="color:#000000">: {&nbsp;&nbsp;&nbsp;
</code></span></span></div>
<div style="background-color:#ffffff"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:18px!important"><code style="color:blue">&quot;imports&quot;</code><code style="color:#000000">: [&nbsp;&nbsp;&nbsp;
</code></span></span></div>
<div style="background-color:#f8f8f8"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:24px!important"><code style="color:blue">&quot;dotnet5.6&quot;</code><code style="color:#000000">,&nbsp;&nbsp;&nbsp;
</code></span></span></div>
<div style="background-color:#ffffff"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:24px!important"><code style="color:blue">&quot;portable-net45&#43;win8&quot;</code>&nbsp;&nbsp;</span></span></div>
<div style="background-color:#f8f8f8"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:18px!important"><code style="color:#000000">]&nbsp;&nbsp;&nbsp;
</code></span></span></div>
<div style="background-color:#ffffff"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:12px!important"><code style="color:#000000">}&nbsp;&nbsp;&nbsp;
</code></span></span></div>
<div style="background-color:#f8f8f8"><span><code>&nbsp;&nbsp;</code><span style="margin-left:6px!important"><code style="color:#000000">},&nbsp;&nbsp;&nbsp;
</code></span></span></div>
<div style="background-color:#ffffff"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:18px!important">&nbsp;</span></span></div>
<div style="background-color:#f8f8f8"><span><code>&nbsp;&nbsp;</code><span style="margin-left:6px!important"><code style="color:blue">&quot;buildOptions&quot;</code><code style="color:#000000">: {&nbsp;&nbsp;&nbsp;
</code></span></span></div>
<div style="background-color:#ffffff"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:12px!important"><code style="color:blue">&quot;emitEntryPoint&quot;</code><code style="color:#000000">:
</code><code style="color:#006699; font-weight:bold">true</code><code style="color:#000000">,&nbsp;&nbsp;&nbsp;
</code></span></span></div>
<div style="background-color:#f8f8f8"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:12px!important"><code style="color:blue">&quot;preserveCompilationContext&quot;</code><code style="color:#000000">:
</code><code style="color:#006699; font-weight:bold">true</code>&nbsp;&nbsp;</span></span></div>
<div style="background-color:#ffffff"><span><code>&nbsp;&nbsp;</code><span style="margin-left:6px!important"><code style="color:#000000">},&nbsp;&nbsp;&nbsp;
</code></span></span></div>
<div style="background-color:#f8f8f8"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:18px!important">&nbsp;</span></span></div>
<div style="background-color:#ffffff"><span><code>&nbsp;&nbsp;</code><span style="margin-left:6px!important"><code style="color:blue">&quot;runtimeOptions&quot;</code><code style="color:#000000">: {&nbsp;&nbsp;&nbsp;
</code></span></span></div>
<div style="background-color:#f8f8f8"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:12px!important"><code style="color:blue">&quot;configProperties&quot;</code><code style="color:#000000">: {&nbsp;&nbsp;&nbsp;
</code></span></span></div>
<div style="background-color:#ffffff"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:18px!important"><code style="color:blue">&quot;System.GC.Server&quot;</code><code style="color:#000000">:
</code><code style="color:#006699; font-weight:bold">true</code>&nbsp;&nbsp;</span></span></div>
<div style="background-color:#f8f8f8"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:12px!important"><code style="color:#000000">}&nbsp;&nbsp;&nbsp;
</code></span></span></div>
<div style="background-color:#ffffff"><span><code>&nbsp;&nbsp;</code><span style="margin-left:6px!important"><code style="color:#000000">},&nbsp;&nbsp;&nbsp;
</code></span></span></div>
<div style="background-color:#f8f8f8"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:18px!important">&nbsp;</span></span></div>
<div style="background-color:#ffffff"><span><code>&nbsp;&nbsp;</code><span style="margin-left:6px!important"><code style="color:blue">&quot;publishOptions&quot;</code><code style="color:#000000">: {&nbsp;&nbsp;&nbsp;
</code></span></span></div>
<div style="background-color:#f8f8f8"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:12px!important"><code style="color:blue">&quot;include&quot;</code><code style="color:#000000">: [&nbsp;&nbsp;&nbsp;
</code></span></span></div>
<div style="background-color:#ffffff"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:18px!important"><code style="color:blue">&quot;wwwroot&quot;</code><code style="color:#000000">,&nbsp;&nbsp;&nbsp;
</code></span></span></div>
<div style="background-color:#f8f8f8"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:18px!important"><code style="color:blue">&quot;web.config&quot;</code>&nbsp;&nbsp;</span></span></div>
<div style="background-color:#ffffff"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:12px!important"><code style="color:#000000">]&nbsp;&nbsp;&nbsp;
</code></span></span></div>
<div style="background-color:#f8f8f8"><span><code>&nbsp;&nbsp;</code><span style="margin-left:6px!important"><code style="color:#000000">},&nbsp;&nbsp;&nbsp;
</code></span></span></div>
<div style="background-color:#ffffff"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:18px!important">&nbsp;</span></span></div>
<div style="background-color:#f8f8f8"><span><code>&nbsp;&nbsp;</code><span style="margin-left:6px!important"><code style="color:blue">&quot;scripts&quot;</code><code style="color:#000000">: {&nbsp;&nbsp;&nbsp;
</code></span></span></div>
<div style="background-color:#ffffff"><span><code>&nbsp;&nbsp;&nbsp;&nbsp;</code><span style="margin-left:12px!important"><code style="color:blue">&quot;postpublish&quot;</code><code style="color:#000000">: [
</code><code style="color:blue">&quot;dotnet publish-iis --publish-folder %publish:OutputPath% --framework %publish:FullTargetFramework%&quot;</code>
<code style="color:#000000">]&nbsp;&nbsp;&nbsp; </code></span></span></div>
<div style="background-color:#f8f8f8"><span><code>&nbsp;&nbsp;</code><span style="margin-left:6px!important"><code style="color:#000000">}&nbsp;&nbsp;&nbsp;
</code></span></span></div>
<div style="background-color:#ffffff"><span style="margin-left:0px!important"><code style="color:#000000">}</code></span></div>
</div>
<p>&nbsp;</p>
<h1>Extension Methods</h1>
<p><br>
<span style="font-size:16px">We can add this following method in &quot;Startup.Cs&quot; and every extension method runs as a sequence.</span></p>
<ul>
<li>
<h5><span style="font-size:16px">UseStaticFiles()</span></h5>
</li><li>
<h5><span style="font-size:16px">UseDefaultFiles()</span></h5>
</li><li>
<h5><span style="font-size:16px">UseFileServer()</span></h5>
</li></ul>
<h1>UseStaticFiles()</h1>
<p><br>
<span style="font-size:16px">We can call the UseStaticFiles extension method from Startup.Cs and it makes the files in web wwwroot or web root as servable.</span><br>
<strong></p>
<h5><span style="font-size:16px">Code</span></h5>
</strong>
<p></p>
<div class="reCodeBlock" style="border:1px solid #7f9db9; overflow-y:auto">
<div style="background-color:#ffffff"><span style="margin-left:0px!important"><code style="color:#000000">app.UseDefaultFiles();
</code><code style="color:#008200">// Call first before app.UseStaticFiles()&nbsp;
</code></span></div>
<div style="background-color:#f8f8f8"><span style="margin-left:0px!important"><code style="color:#000000">app.UseStaticFiles();
</code><code style="color:#008200">// For the wwwroot folder</code></span></div>
</div>
<h5><span style="font-size:16px">Output</span></h5>
<p><a href="http://social.technet.microsoft.com/wiki/cfs-file.ashx/__key/communityserver-wikis-components-files/00-00-00-00-05/7127.image006.jpg"><img src=":-7127.image006.jpg" alt="" style="border-width:0px; border-style:solid"></a><br>
<br>
<br>
<em style="font-size:12.1px"><span style="font-size:10px">Pic Source By :&nbsp;<a title="RajeeshMenoth Blog" href="https://rajeeshmenoth.wordpress.com/" target="_blank">RajeeshMenoth Blog</a><br>
</span></em></p>
<h1>UseDefaultFiles()</h1>
<p><br>
<span style="font-size:16px">UseDefaultFiles must be called before UseStaticFiles to serve the default file in client-side browser. If you mention UseStaticFiles() method after UseDefaultFiles() then it will run UseStaticFiles() method as default and automatically
 terminate other files coming after UseStaticFiles() method.<br>
</span><span style="font-size:16px"><br>
UseDefaultFiles() will only search for the following files in <em>&quot;wwwroot&quot;</em>. If any of the files are detected first in
<em>&quot;wwwroot&quot;</em> then that file runs as default in client browser.</span></p>
<ul>
<li><span style="font-size:16px">default.html</span> </li><li><span style="font-size:16px">index.html</span> </li><li><span style="font-size:16px">default.htm<br>
</span></li><li><span style="font-size:16px">index.htm</span> </li></ul>
<div><span style="font-size:16px">If you want to run other files as default then check the following code in Startup.Cs</span><br>
<h5><span style="font-size:16px">Code</span></h5>
<div class="reCodeBlock" style="border:1px solid #7f9db9; overflow-y:auto">
<div style="background-color:#ffffff"><span style="margin-left:0px!important"><code style="color:#000000">DefaultFilesOptions DefaultFile =
</code><code style="color:#006699; font-weight:bold">new</code> <code style="color:#000000">
DefaultFilesOptions();&nbsp; </code></span></div>
<div style="background-color:#f8f8f8"><span style="margin-left:0px!important"><code style="color:#000000">DefaultFile.DefaultFileNames.Clear();&nbsp;
</code></span></div>
<div style="background-color:#ffffff"><span style="margin-left:0px!important"><code style="color:#000000">DefaultFile.DefaultFileNames.Add(</code><code style="color:blue">&quot;Welcome.html&quot;</code><code style="color:#000000">);&nbsp;
</code></span></div>
<div style="background-color:#f8f8f8"><span style="margin-left:0px!important"><code style="color:#000000">app.UseDefaultFiles(DefaultFile);&nbsp;
</code></span></div>
<div style="background-color:#ffffff"><span style="margin-left:0px!important"><code style="color:#000000">app.UseStaticFiles();</code></span></div>
</div>
</div>
<h1>UseFileServer()</h1>
<div><br>
<span style="font-size:16px">It combines the functionality of UseStaticFiles() and UseDefaultFiles(). So we can reduce the code and handle Staticfiles and Default Files as a single file. UseFileServer() will take care of the static file as the default start
 page.</span></div>
<h5><span style="font-size:16px">Code</span></h5>
<div>
<div class="reCodeBlock" style="border:1px solid #7f9db9; overflow-y:auto">
<div style="background-color:#ffffff"><span style="margin-left:0px!important"><code style="color:#000000">app.UseFileServer();
</code><code style="color:#008200">// Combines UseStaticFiles() and UseDefaultFiles()</code></span></div>
</div>
</div>
<h1>Reference</h1>
<ul>
<li>
<h5><span style="font-size:16px"><a title="Microsoft Docs" href="https://docs.microsoft.com/en-us/aspnet/core/" target="_blank">Microsoft Docs</a></span></h5>
</li><li>
<h5><span style="font-size:16px">You can read this article in&nbsp;<a title="RajeeshMenoth Blog" href="https://rajeeshmenoth.wordpress.com/2016/12/29/middleware-staticfiles-in-asp-net-core-1-0-part-1/" target="_blank">RajeeshMenoth Blog</a></span></h5>
</li></ul>
<h1>Conclusion</h1>
<p><span style="font-size:16px"><br>
We learned Middleware &amp; Staticfiles in Asp.Net Core 1.0, and I hope you liked this article. Please share your valuable suggestions and feedback.</span></p>
