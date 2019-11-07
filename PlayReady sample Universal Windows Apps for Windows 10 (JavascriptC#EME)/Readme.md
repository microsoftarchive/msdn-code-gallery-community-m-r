# PlayReady sample Universal Windows Apps for Windows 10 (Javascript/C#/EME)
## Requires
- Visual Studio 2015
## License
- MS-LPL
## Technologies
- Javascript
- PlayReady
- HTML5/JavaScript
- XAML/C#
- Universal Windows App Development
- Windows Univeral App
## Topics
- Media
- PlayReady
- Universal Windows Applicaitons
## Updated
- 07/29/2015
## Description

<h1>Introduction</h1>
<p><span style="font-size:medium">You can use PlayReady Content Protection to help you deliver audio and video content that is more secure and better protected from unauthorized redistribution.</span></p>
<p><span style="font-size:medium">Windows 10&nbsp;supports Enhanced Content Protection (ECP) with compatible Hardware, to unlock premium content, based on PlayReady 3.0.</span></p>
<h1>Prerequisites</h1>
<ul>
<li>
<p><span style="font-size:medium">Windows 10 RTM</span></p>
</li><li>
<p><span style="font-size:medium">Visual Studio 2015 RTM</span></p>
</li><li>
<p><span style="font-size:medium">Microsoft Universal Smooth Streaming Client SDK</span></p>
<ul>
<li>
<p><span style="font-size:medium">Add <a href="https://visualstudiogallery.msdn.microsoft.com/1e7d4700-7fa8-49b6-8a7b-8d8666685459?SRC=Home">
Microsoft Universal Smooth Streaming Client SDK</a> to the visual studio project&rsquo;s reference in order to play smooth streaming (PIFF) content. &nbsp;</span></p>
</li></ul>
</li></ul>
<p>&nbsp;</p>
<div><strong><span style="font-size:2em">Sample Details</span></strong></div>
<p><span style="font-size:medium">The Solution contains <span style="text-decoration:underline">
<em><strong>3 projects</strong></em></span>:</span></p>
<ol>
<li><strong><em><span style="font-size:medium">Encrypted Media Extensions (EME) JS Universal Windows Application</span> (code can be repurposed to build a website or a universal app, and is based on teh W3C Encrypted Media Extensions spec)</em></strong>
</li><li><strong><em><span style="font-size:medium">PlayReady JS&nbsp;Universal Windows Application</span>
</em></strong></li><li><strong><em><span style="font-size:medium">PlayReady XAML/C#&nbsp;Universal Windows Application</span></em></strong>
</li></ol>
<p>&nbsp;</p>
<p><span style="font-size:medium">Each sample project illustrates how to utilize PlayReady to play protected content. Below is a list of features that the samples enable:</span></p>
<ol>
<li><span style="font-size:medium">Playback for Audio only, Video only, Audio Video encrypted and clear content. With content encoded in several industry standard formats. (note:&nbsp;EME sample app only supports DASH content).&nbsp;</span>
<ul>
<li><span style="font-size:medium">You can also input your own content</span> </li></ul>
</li><li><span style="font-size:medium">Proactive License Acquisition</span> </li><li><span style="font-size:medium">Secure Stop Request</span> </li><li><span style="font-size:medium">Opting out of Hardware DRM support </span></li><li><span style="font-size:medium">Persistent License Request</span> </li><li><span style="font-size:medium">Setting new PlayReady 3.0 specific&nbsp;policies (such as MaxResDecode)</span>
</li><li><span style="font-size:medium">Setting OPL value and PlayEnabler&nbsp;</span>
</li></ol>
<h1>For Additional Info on the above features</h1>
<div>
<p><span style="font-size:medium">The Microsoft PlayReady Windows 10 documentation can be found&nbsp;<a href="https://msdn.microsoft.com/en-us/library/windows/apps/mt282145.aspx" target="_parent">here</a>.</span></p>
</div>
<p>&nbsp;</p>
<h1>Building the Sample</h1>
<ol>
<li>
<p><span style="font-size:medium">Extract the solution from the Zip file</span></p>
</li><li>
<p><span style="font-size:medium">Make sure you have installed the Microsoft Smooth Streaming Client SDK and Visual Studio 2015 RTM, and add a reference to it in the projects.</span></p>
</li><li>
<p><span style="font-size:medium">Open the Solution File (.SLN) from that directory created in step #1.</span></p>
</li><li>
<p><span style="font-size:medium">In Visual Studio, go into the Build menu, and select Build Solution.
</span></p>
</li><li>
<p><span style="font-size:medium">Ensure the build succeeds with zero errors. </span>
</p>
</li><li>
<p><span style="font-size:medium">Hit F5, or go to the&nbsp;&quot;Debug&quot;&nbsp;Menu and select the &quot;Start Debugging&quot; Menu item to run. Make sure you have selected the correct architecture (x86, x64).&nbsp;</span><span style="font-size:medium">This will run the default
 StartUp project.&nbsp;</span></p>
</li><li>
<p><span style="font-size:medium">Change the StartUp project settings for the Project menu to
<strong><em>run the other 2</em></strong> and repeat step#6 to debug another project.</span></p>
</li></ol>
<p><span style="font-size:small">&nbsp;&nbsp;</span><span style="font-size:medium">&nbsp;</span></p>
<div></div>
<h1><span><span>Known issues</span></span></h1>
<div>
<ul>
<li><span style="font-size:medium">Audio Video separation content may fail when played on a device with Hardware&nbsp;DRM&nbsp;support. This will be fixed in an upcoming Windows Update in late 2015.
</span></li><li><span style="font-size:medium">In the&nbsp;<span>EME sample app on the phone,&nbsp;</span>If you choose Dash.all.js as the DASH media source the playback stops shortly after it starts&nbsp;</span>
</li></ul>
</div>
