# Reading and Writing Values from INI Files with C#
## Requires
- Visual Studio 2013
## License
- MIT
## Technologies
- PInvoke
- P/Invoke
- Platform Invoke
- P-Invoke
- Platform Invocation Services
## Topics
- INI Files
- Read and Write INI files
- Access INI files
## Updated
- 09/03/2015
## Description

<h1>Introduction</h1>
<p>INI files is often used by legacy software. But sometimes also by not so legacy software. So sometimes it is needed to access INI files from .NET/C#, but there is no class or method in .NET/C# that can read or write INI files. But there are some functions
 in kernel32.dll that can be used by Platform Invoke (PInvoke). These functions (GetPrivateProfileString, GetPrivateProfileSection and WritePrivateProfileString) are provided for compatibility, nevertheless they can be used.</p>
<h1><span>Building the Sample</span></h1>
<p>There are no special requirements for building the sample.</p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p>You need to add a using that allows using PInvoke.</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">using</span>&nbsp;<a class="libraryLink" href="https://msdn.microsoft.com/en-US/library/System.Runtime.InteropServices.aspx" target="_blank" title="Auto generated link to System.Runtime.InteropServices">System.Runtime.InteropServices</a>;&nbsp;
&nbsp;
&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">Then you can declare the methods that you want to access.</div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp">[DllImport(<span class="cs__string">&quot;kernel32&quot;</span>,&nbsp;CharSet&nbsp;=&nbsp;CharSet.Unicode)]&nbsp;
<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">extern</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;GetPrivateProfileString(<span class="cs__keyword">string</span>&nbsp;section,&nbsp;<span class="cs__keyword">string</span>&nbsp;key,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;defaultValue,&nbsp;StringBuilder&nbsp;<span class="cs__keyword">value</span>,&nbsp;<span class="cs__keyword">int</span>&nbsp;size,&nbsp;<span class="cs__keyword">string</span>&nbsp;filePath);&nbsp;
&nbsp;&nbsp;
[DllImport(<span class="cs__string">&quot;kernel32.dll&quot;</span>,&nbsp;CharSet&nbsp;=&nbsp;CharSet.Unicode)]&nbsp;
<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">extern</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;GetPrivateProfileString(<span class="cs__keyword">string</span>&nbsp;section,&nbsp;<span class="cs__keyword">string</span>&nbsp;key,&nbsp;<span class="cs__keyword">string</span>&nbsp;defaultValue,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[In,&nbsp;Out]&nbsp;<span class="cs__keyword">char</span>[]&nbsp;<span class="cs__keyword">value</span>,&nbsp;<span class="cs__keyword">int</span>&nbsp;size,&nbsp;<span class="cs__keyword">string</span>&nbsp;filePath);&nbsp;
&nbsp;&nbsp;
[DllImport(<span class="cs__string">&quot;kernel32.dll&quot;</span>,&nbsp;CharSet&nbsp;=&nbsp;CharSet.Auto)]&nbsp;
<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">extern</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;GetPrivateProfileSection(<span class="cs__keyword">string</span>&nbsp;section,&nbsp;IntPtr&nbsp;keyValue,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;size,&nbsp;<span class="cs__keyword">string</span>&nbsp;filePath);&nbsp;
&nbsp;&nbsp;
[DllImport(<span class="cs__string">&quot;kernel32&quot;</span>,&nbsp;CharSet&nbsp;=&nbsp;CharSet.Unicode,&nbsp;SetLastError&nbsp;=&nbsp;<span class="cs__keyword">true</span>)]&nbsp;
[<span class="cs__keyword">return</span>:&nbsp;MarshalAs(UnmanagedType.Bool)]&nbsp;
<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">extern</span>&nbsp;<span class="cs__keyword">bool</span>&nbsp;WritePrivateProfileString(<span class="cs__keyword">string</span>&nbsp;section,&nbsp;<span class="cs__keyword">string</span>&nbsp;key,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;<span class="cs__keyword">value</span>,&nbsp;<span class="cs__keyword">string</span>&nbsp;filePath);&nbsp;
&nbsp;
&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">Now we can use these methods to access an INI file. Therefor we have defined a size into that the results should fit. You can extend or reduce the size as needed.</div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="fsharp"><span class="fs__keyword">public</span>&nbsp;<span class="fs__keyword">static</span>&nbsp;int&nbsp;capacity&nbsp;=&nbsp;<span class="fs__number">512</span>;</pre>
</div>
</div>
</div>
<div class="endscriptcode">You can read values with the following code.</div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;ReadValue(<span class="cs__keyword">string</span>&nbsp;section,&nbsp;<span class="cs__keyword">string</span>&nbsp;key,&nbsp;<span class="cs__keyword">string</span>&nbsp;filePath,&nbsp;<span class="cs__keyword">string</span>&nbsp;defaultValue&nbsp;=&nbsp;<span class="cs__string">&quot;&quot;</span>)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;<span class="cs__keyword">value</span>&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;StringBuilder(capacity);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;GetPrivateProfileString(section,&nbsp;key,&nbsp;defaultValue,&nbsp;<span class="cs__keyword">value</span>,&nbsp;<span class="cs__keyword">value</span>.Capacity,&nbsp;filePath);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">value</span>.ToString();&nbsp;
}&nbsp;
&nbsp;&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">string</span>[]&nbsp;ReadSections(<span class="cs__keyword">string</span>&nbsp;filePath)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;first&nbsp;line&nbsp;will&nbsp;not&nbsp;recognize&nbsp;if&nbsp;ini&nbsp;file&nbsp;is&nbsp;saved&nbsp;in&nbsp;UTF-8&nbsp;with&nbsp;BOM</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">while</span>&nbsp;(<span class="cs__keyword">true</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">char</span>[]&nbsp;chars&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;<span class="cs__keyword">char</span>[capacity];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;size&nbsp;=&nbsp;GetPrivateProfileString(<span class="cs__keyword">null</span>,&nbsp;<span class="cs__keyword">null</span>,&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;chars,&nbsp;capacity,&nbsp;filePath);&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(size&nbsp;==&nbsp;<span class="cs__number">0</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">null</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(size&nbsp;&lt;&nbsp;capacity&nbsp;-&nbsp;<span class="cs__number">2</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;result&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;String(chars,&nbsp;<span class="cs__number">0</span>,&nbsp;size);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>[]&nbsp;sections&nbsp;=&nbsp;result.Split(<span class="cs__keyword">new</span>&nbsp;<span class="cs__keyword">char</span>[]&nbsp;{&nbsp;<span class="cs__string">'\0'</span>&nbsp;},&nbsp;StringSplitOptions.RemoveEmptyEntries);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;sections;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;capacity&nbsp;=&nbsp;capacity&nbsp;*&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
&nbsp;&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">string</span>[]&nbsp;ReadKeys(<span class="cs__keyword">string</span>&nbsp;section,&nbsp;<span class="cs__keyword">string</span>&nbsp;filePath)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;first&nbsp;line&nbsp;will&nbsp;not&nbsp;recognize&nbsp;if&nbsp;ini&nbsp;file&nbsp;is&nbsp;saved&nbsp;in&nbsp;UTF-8&nbsp;with&nbsp;BOM</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">while</span>&nbsp;(<span class="cs__keyword">true</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">char</span>[]&nbsp;chars&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;<span class="cs__keyword">char</span>[capacity];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;size&nbsp;=&nbsp;GetPrivateProfileString(section,&nbsp;<span class="cs__keyword">null</span>,&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;chars,&nbsp;capacity,&nbsp;filePath);&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(size&nbsp;==&nbsp;<span class="cs__number">0</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">null</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(size&nbsp;&lt;&nbsp;capacity&nbsp;-&nbsp;<span class="cs__number">2</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;result&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;String(chars,&nbsp;<span class="cs__number">0</span>,&nbsp;size);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>[]&nbsp;keys&nbsp;=&nbsp;result.Split(<span class="cs__keyword">new</span>&nbsp;<span class="cs__keyword">char</span>[]&nbsp;{&nbsp;<span class="cs__string">'\0'</span>&nbsp;},&nbsp;StringSplitOptions.RemoveEmptyEntries);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;keys;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;capacity&nbsp;=&nbsp;capacity&nbsp;*&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
&nbsp;&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">string</span>[]&nbsp;ReadKeyValuePairs(<span class="cs__keyword">string</span>&nbsp;section,&nbsp;<span class="cs__keyword">string</span>&nbsp;filePath)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">while</span>&nbsp;(<span class="cs__keyword">true</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IntPtr&nbsp;returnedString&nbsp;=&nbsp;Marshal.AllocCoTaskMem(capacity&nbsp;*&nbsp;<span class="cs__keyword">sizeof</span>(<span class="cs__keyword">char</span>));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;size&nbsp;=&nbsp;GetPrivateProfileSection(section,&nbsp;returnedString,&nbsp;capacity,&nbsp;filePath);&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(size&nbsp;==&nbsp;<span class="cs__number">0</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Marshal.FreeCoTaskMem(returnedString);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">null</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(size&nbsp;&lt;&nbsp;capacity&nbsp;-&nbsp;<span class="cs__number">2</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;result&nbsp;=&nbsp;Marshal.PtrToStringAuto(returnedString,&nbsp;size&nbsp;-&nbsp;<span class="cs__number">1</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Marshal.FreeCoTaskMem(returnedString);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>[]&nbsp;keyValuePairs&nbsp;=&nbsp;result.Split(<span class="cs__string">'\0'</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;keyValuePairs;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Marshal.FreeCoTaskMem(returnedString);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;capacity&nbsp;=&nbsp;capacity&nbsp;*&nbsp;<span class="cs__number">2</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}</pre>
</div>
</div>
</div>
<div class="endscriptcode">You can write values with the following code.</div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">bool</span>&nbsp;WriteValue(<span class="cs__keyword">string</span>&nbsp;section,&nbsp;<span class="cs__keyword">string</span>&nbsp;key,&nbsp;<span class="cs__keyword">string</span>&nbsp;<span class="cs__keyword">value</span>,&nbsp;<span class="cs__keyword">string</span>&nbsp;filePath)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">bool</span>&nbsp;result&nbsp;=&nbsp;WritePrivateProfileString(section,&nbsp;key,&nbsp;<span class="cs__keyword">value</span>,&nbsp;filePath);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;result;&nbsp;
}</pre>
</div>
</div>
</div>
<div class="endscriptcode">Sections and keys will be created, if they not exist.</div>
<p>&nbsp;</p>
<p>Or you can delete values with the following code.</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">bool</span>&nbsp;DeleteSection(<span class="cs__keyword">string</span>&nbsp;section,&nbsp;<span class="cs__keyword">string</span>&nbsp;filepath)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">bool</span>&nbsp;result&nbsp;=&nbsp;WritePrivateProfileString(section,&nbsp;<span class="cs__keyword">null</span>,&nbsp;<span class="cs__keyword">null</span>,&nbsp;filepath);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;result;&nbsp;
}&nbsp;
&nbsp;&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">bool</span>&nbsp;DeleteKey(<span class="cs__keyword">string</span>&nbsp;section,&nbsp;<span class="cs__keyword">string</span>&nbsp;key,&nbsp;<span class="cs__keyword">string</span>&nbsp;filepath)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">bool</span>&nbsp;result&nbsp;=&nbsp;WritePrivateProfileString(section,&nbsp;key,&nbsp;<span class="cs__keyword">null</span>,&nbsp;filepath);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;result;&nbsp;
}</pre>
</div>
</div>
</div>
<p>&nbsp;</p>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em>IniFileHelper.cs - provides methods to access INI files<br>
</em></li><li><em><em>MainWindow.xaml - test GUI to demonstrate the functions of IniFileHelper</em></em>
</li><li><em>MainWindow.xaml.cs - example calls of IniFileHelper</em> </li></ul>
<h1>More Information</h1>
<p><strong>For further code samples visit <a href="http://chrigas.blogspot.com/">
http://chrigas.blogspot.com/</a></strong></p>
