# MVC + Entity Framework on a 3 Tier Architecture
## Requires
- Visual Studio 2012
## License
- Apache License, Version 2.0
## Technologies
- Entity Framework
- MVC
- MVC Scaffolding
## Topics
- 3 Tier Architecture
## Updated
- 06/06/2014
## Description

<h1>Introduction</h1>
<p><em>This sample is an idea for develop web applications with MVC and Entity Framework on a 3 Tier Architecture. I have considered the classic functionality for two joined entities, in this case Product and Category.</em></p>
<p><em>The following screenshots show the functionality:</em></p>
<p><em><img id="116377" src="116377-1.jpg" alt="" width="1366" height="728"><br>
</em></p>
<h1></h1>
<p><img id="116378" src="116378-2.jpg" alt="" width="1366" height="728"></p>
<h1></h1>
<p><img id="116379" src="116379-3.jpg" alt="" width="1366" height="728"></p>
<p>&nbsp;</p>
<h1><span>Building the Sample</span></h1>
<p><em>For run this solution, you need SQL Server with local databases support, the
<strong>web.config</strong> file indicates a connectionstring using a local database (this database should be created automatically)</em></p>
<p><em></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>

<div class="preview">
<pre class="xml"><span class="xml__tag_start">&lt;connectionStrings</span><span class="xml__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;add</span>&nbsp;<span class="xml__attr_name">name</span>=<span class="xml__attr_value">&quot;DefaultConnection&quot;</span>&nbsp;<span class="xml__attr_name">connectionString</span>=<span class="xml__attr_value">&quot;Data&nbsp;Source=(LocalDb)\v11.0;Initial&nbsp;Catalog=aspnet-MVCEF3TCS.Web-20140604134240;Integrated&nbsp;Security=SSPI;AttachDBFilename=|DataDirectory|\aspnet-MVCEF3TCS.Web-20140604134240.mdf&quot;</span>&nbsp;<span class="xml__attr_name">providerName</span>=<span class="xml__attr_value">&quot;System.Data.SqlClient&quot;</span>&nbsp;<span class="xml__tag_start">/&gt;</span>&nbsp;
&nbsp;&nbsp;<span class="xml__tag_end">&lt;/connectionStrings&gt;</span></pre>
</div>
</div>
</div>
<div class="endscriptcode"><em>If you dont have idea about local databases, you can use a specific database only modifying the
<strong>web.config</strong>, something like:</em></div>
</em>
<p></p>
<div class="endscriptcode"></div>
<div class="endscriptcode"><em>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xml</span>

<div class="preview">
<pre class="xml"><span class="xml__tag_start">&lt;connectionStrings</span><span class="xml__tag_start">&gt;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__tag_start">&lt;add</span>&nbsp;<span class="xml__attr_name">name</span>=<span class="xml__attr_value">&quot;DefaultConnection&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__attr_name">connectionString</span>=<span class="xml__attr_value">&quot;Data&nbsp;Source=(Your&nbsp;server);Initial&nbsp;Catalog=(Your&nbsp;database);User&nbsp;ID=(Your&nbsp;user);Password=(Your&nbsp;password)&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xml__attr_name">providerName</span>=<span class="xml__attr_value">&quot;System.Data.SqlClient&quot;</span><span class="xml__tag_start">/&gt;</span>&nbsp;&nbsp;
&nbsp;&nbsp;<span class="xml__tag_end">&lt;/connectionStrings&gt;</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;* The parameter &quot;name&quot; of connectionstring must be the same as the DatabaseContext constructor (DatabaseContext.cs file from Data Access Layer)</div>
</em></div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><em><span>C#</span></em></div>
<div class="pluginLinkHolder"><em><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></em></div>
<em><span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">namespace</span>&nbsp;MVCEF3TCS.Data&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">using</span>&nbsp;<a class="libraryLink" href="http://msdn.microsoft.com/en-US/library/System.Data.Entity.aspx" target="_blank" title="Auto generated link to System.Data.Entity">System.Data.Entity</a>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">using</span>&nbsp;MVCEF3TCS.Entities;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;DatabaseContext&nbsp;:&nbsp;DbContext&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;DatabaseContext()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;<span class="cs__keyword">base</span>(<span class="cs__string">&quot;<strong><span style="color:#0000ff">DefaultConnection</span></strong>&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;DbSet&lt;Product&gt;&nbsp;ProductList&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;DbSet&lt;Category&gt;&nbsp;CategoryList&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}</pre>
</div>
</em></div>
</div>
<div class="endscriptcode"><em>&nbsp;</em><strong style="font-size:medium">Note:&nbsp;</strong></div>
<p><span style="font-size:x-small">If you have error when trying to create database, maybe you need to add this lines in DatabaseContext.cs file:</span><span style="font-size:x-small">&nbsp;</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">protected</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;OnModelCreating(DbModelBuilder&nbsp;modelBuilder)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Database.SetInitializer&lt;DatabaseContext&gt;(<span class="cs__keyword">new</span>&nbsp;DropCreateDatabaseIfModelChanges&lt;DatabaseContext&gt;());&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<div class="endscriptcode">If you will use scaffold tool for add more entities, you should<span style="color:#ff0000"> erase or comment</span> this lines,
<span style="color:#ff0000">because&nbsp;could cause an error</span>.</div>
<p>&nbsp;</p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><strong><em>Solution Structure</em></strong></p>
<p><em><img id="116407" src="116407-structure.jpg" alt="" width="316" height="244"></em></p>
<p><em>01 Presentation Layer</em></p>
<p><em>02 Croscutting Layer</em></p>
<p><em>03 Business Layer (Logic)</em></p>
<p><em>04 Data Access Layer</em></p>
<p>&nbsp;</p>
<p><strong>Projects List</strong></p>
<p>[01 Presentation]</p>
<p>&gt; Web [ASP.NET MVC 4 Web Application Project]</p>
<p>[02 Common, 03 Business, 04 Data]</p>
<p>&gt; Entities, Business, Data [Class Library Project]</p>
<h1><span>Source Code Files</span></h1>
<ul>
<li><span style="color:#0000ff"><em>[04] DatabaseContext.cs - Data Access with Entity Framework.</em></span>
</li><li><span style="color:#808000"><em><em>[03] ProductManager.cs - Product Data Handler, data access layer consumer.</em></em></span>
</li><li><span style="color:#808000"><em>[03] CategoryManager.cs - Category Data Handler, data access layer consumer.</em></span>
</li><li><span style="color:#008080"><em><em>[02] Product.cs - Entity definition for Product (Model)</em></em></span>
</li><li><span style="color:#008080"><em>[02] Category.cs -&nbsp;<em><em>Entity definition for Category (Model)</em></em></em></span>
</li><li><span style="color:#993366"><em><em>[01] Controllers/ProductController.cs - Controller for Product Entity, this controller use the Product Data Handler from Business Layer.</em></em></span>
</li></ul>
