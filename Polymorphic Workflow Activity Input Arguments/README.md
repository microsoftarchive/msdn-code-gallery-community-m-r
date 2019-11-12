# Polymorphic Workflow Activity Input Arguments
## Requires
- Visual Studio 2012
## License
- Apache License, Version 2.0
## Technologies
- C#
- Dynamics CRM
- Microsoft Dynamics CRM 2011
- Microsoft Dynamics CRM 2013
## Topics
- Workflows
- Custom Workflow Activities
## Updated
- 08/02/2014
## Description

<h1>Introduction</h1>
<p>I often find myself creating &lsquo;utility&rsquo; custom workflow activities that can be used on many different types of entity. One of the challenges with writing this kind of workflow activity is that InArguments can only accept a single type of entity
 (unlike activity regarding object fields).The following code works well for accepting a reference to an account, but if you want to accept account, contact or lead you&rsquo;d need to create 3 input arguments. If you wanted to make the parameter accept a custom
 entity type that you don&rsquo;t know about when writing the workflow activity then you&rsquo;re stuck!</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="js">[Output(<span class="js__string">&quot;Document&nbsp;Location&quot;</span>)]&nbsp;
[ReferenceTarget(<span class="js__string">&quot;account&quot;</span>)]&nbsp;
public&nbsp;InArgument&lt;EntityReference&gt;&nbsp;EntityReference&nbsp;<span class="js__brace">{</span>&nbsp;get;&nbsp;set;&nbsp;<span class="js__brace">}</span>&nbsp;</pre>
</div>
</div>
</div>
<p>&nbsp;</p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p>There are a number of workarounds to this that I&rsquo;ve tried over the years such as starting a child work flow and using the workflow activity context or creating an activity and using it&rsquo;s regarding object field &ndash; but I&rsquo;d like to share
 with you the best approach I&rsquo;ve found.</p>
<p>Dynamics CRM workflows and dialogs have a neat feature of being about to add Hyperlinks to records into emails/dialog responses etc. which is driven by a special attribute called &lsquo;Record Url(Dynamic)&rsquo;</p>
<p>This field can be used also to provide all the information we need to pass an Entity Reference.</p>
<p>The sample I&rsquo;ve provide is a simple Workflow Activity that accepts the Record Url and returns the Guid of the record as a string and the Entity Logical Name &ndash; this isn&rsquo;t much use on its own, but you&rsquo;ll be able to use the DynamicUrlParser.cs
 class in your own Workflow Activities.</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="js">[Input(<span class="js__string">&quot;Record&nbsp;Dynamic&nbsp;Url&quot;</span>)]&nbsp;
[RequiredArgument]&nbsp;
public&nbsp;InArgument&lt;string&gt;&nbsp;RecordUrl&nbsp;<span class="js__brace">{</span>&nbsp;get;&nbsp;set;&nbsp;<span class="js__brace">}</span>&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;The DynamicUrlParser class can then be used as follows:</div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="js"><span class="js__statement">var</span>&nbsp;entityReference&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;DynamicUrlParser(RecordUrl.Get&lt;string&gt;(executionContext));&nbsp;
&nbsp;
RecordGuid.Set(executionContext,&nbsp;entityReference.Id.ToString());&nbsp;
EntityLogicalName.Set(executionContext,&nbsp;entityReference.GetEntityLogicalName(service));&nbsp;</pre>
</div>
</div>
</div>
</div>
<p>&nbsp;</p>
<p>For more information see my blog - <a href="http://www.develop1.net/public">www.develop1.net/public</a></p>
