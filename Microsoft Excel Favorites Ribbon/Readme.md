# Microsoft Excel Favorites Ribbon
## Requires
- Visual Studio 2015
## License
- MIT
## Technologies
- C#
- Excel
- VSTO
- Microsoft Office Excel
- VB.Net
- Excel 2010
- Office 2010
- Excel 2013
- Office 2013
- Interop Excel
- Office 2016
- Excel 2016
- Visual Studio 2017
## Topics
- Excel
- VSTO
- Office Ribbon
- copying visible cells
- Excel's camera feature
- Snipping Tool
- Problem Steps Recorder (PSR)
- Windows Calculator
## Updated
- 10/22/2018
## Description

<h1>Microsoft Excel Favorites Ribbon</h1>
<p>This is an Excel Add-In written in Visual Studio Community 2017 C#/VB.NET and <a href="https://github.com/aduguid/MicrosoftExcelFavorites/tree/master/VBA">
VBA</a>.&nbsp;<img class="emoji" src=":-1f195.png" alt=":new:" width="20" height="20">&nbsp;I'm currently working on the Web Add-In in JavaScript. It gives the user a custom ribbon. Key distinctive attributes
 include dedicated buttons for&nbsp;<a rel="nofollow" href="https://support.office.com/en-us/article/Copy-visible-cells-only-6e3a1f01-2884-4332-b262-8b814412847e">copying visible cells</a>,&nbsp;<a rel="nofollow" href="https://answers.microsoft.com/en-us/msoffice/forum/msoffice_excel-mso_winother/how-to-use-camera-function-in-microsoft-excel/44a97349-f694-4bd3-a5ca-e4097f6e9437?auth=1">Excel's
 camera feature</a>,&nbsp;<a rel="nofollow" href="https://support.microsoft.com/en-au/help/4027213/windows-open-snipping-tool-and-take-a-screenshot">Snipping Tool</a>,&nbsp;<a rel="nofollow" href="https://support.microsoft.com/en-au/help/22878/windows-10-record-steps">Problem
 Steps Recorder (PSR)</a>&nbsp;and&nbsp;<a rel="nofollow" href="https://en.wikipedia.org/wiki/Windows_Calculator">Windows Calculator</a>.&nbsp;This project is now on
<a href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md">
GitHub</a>.</p>
<p><span>Please rate this code. :)</span></p>
<h1><img id="216203" src="216203-vsto.excel.ribbon.favorites.gif" alt="" width="725" height="410"></h1>
<h2>Dependencies</h2>
<table>
<thead>
<tr>
<th align="left"><span style="font-size:small">Software</span></th>
<th align="left"><span style="font-size:small">Dependency</span></th>
<th align="left"><span style="font-size:small">Project</span></th>
</tr>
</thead>
<tbody>
<tr>
<td align="left"><a rel="nofollow" href="https://www.visualstudio.com/vs/whatsnew/">Microsoft Visual Studio Community 2017</a></td>
<td align="left">Solution</td>
<td align="left">VSTO</td>
</tr>
<tr>
<td align="left"><a rel="nofollow" href="https://blogs.msdn.microsoft.com/visualstudio/2015/11/23/latest-microsoft-office-developer-tools-for-visual-studio-2015/">Microsoft Office Developer Tools</a></td>
<td align="left">Solution</td>
<td align="left">VSTO</td>
</tr>
<tr>
<td align="left"><a rel="nofollow" href="https://www.microsoft.com/en-au/software-download/office">Microsoft Excel 2010 (or later)</a></td>
<td align="left">Project</td>
<td align="left">VBA, VSTO</td>
</tr>
<tr>
<td align="left"><a rel="nofollow" href="https://msdn.microsoft.com/en-us/vba/vba-language-reference">Visual Basic for Applications</a></td>
<td align="left">Code</td>
<td align="left">VBA</td>
</tr>
<tr>
<td align="left"><a rel="nofollow" href="https://www.rondebruin.nl/win/s2/win001.htm">Extensible Markup Language (XML)</a></td>
<td align="left">Ribbon</td>
<td align="left">VBA, VSTO</td>
</tr>
<tr>
<td align="left"><a rel="nofollow" href="http://discover.techsmith.com/snagit-non-brand-desktop/?gclid=CNzQiOTO09UCFVoFKgod9EIB3g">Snagit</a></td>
<td align="left">Read Me</td>
<td align="left">VBA, VSTO</td>
</tr>
</tbody>
</table>
<h2>Glossary of Terms</h2>
<table border="0">
<thead>
<tr>
<th align="left"><span style="font-size:small">Term</span></th>
<th align="left"><span style="font-size:small">Meaning</span></th>
</tr>
</thead>
<tbody>
<tr>
<td align="left">COM</td>
<td align="left">Component Object Model (COM) is a binary-interface standard for software components introduced by Microsoft in 1993. It is used to enable inter-process communication and dynamic object creation in a large range of programming languages. COM
 is the basis for several other Microsoft technologies and frameworks, including OLE, OLE Automation, ActiveX, COM&#43;, DCOM, the Windows shell, DirectX, UMDF and Windows Runtime.</td>
</tr>
<tr>
<td align="left">VBA</td>
<td align="left">Visual Basic for Applications (VBA) is an implementation of Microsoft's event-driven programming language Visual Basic 6 and uses the Visual Basic Runtime Library. However, VBA code normally can only run within a host application, rather than
 as a standalone program. VBA can, however, control one application from another using OLE Automation. VBA can use, but not create, ActiveX/COM DLLs, and later versions add support for class modules.</td>
</tr>
<tr>
<td align="left">VSTO</td>
<td align="left">Visual Studio Tools for Office (VSTO) is a set of development tools available in the form of a Visual Studio add-in (project templates) and a runtime that allows Microsoft Office 2003 and later versions of Office applications to host the .NET
 Framework Common Language Runtime (CLR) to expose their functionality via .NET.</td>
</tr>
<tr>
<td align="left">XML</td>
<td align="left">Extensible Markup Language (XML) is a markup language that defines a set of rules for encoding documents in a format that is both human-readable and machine-readable.The design goals of XML emphasize simplicity, generality, and usability across
 the Internet. It is a textual data format with strong support via Unicode for different human languages. Although the design of XML focuses on documents, the language is widely used for the representation of arbitrary data structures such as those used in
 web services.</td>
</tr>
</tbody>
</table>
<h2>Functionality</h2>
<p>This Excel ribbon named &ldquo;Favorites&rdquo; is inserted after the &ldquo;Home&rdquo; tab when Excel opens.</p>
<p><a id="user-content-worksheet" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#worksheet"></a></p>
<h3><a id="user-content-worksheet-group" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#worksheet-group"></a>Worksheet (Group)</h3>
<p><a id="user-content-save" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#save"></a></p>
<h4><a id="user-content-save-button" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#save-button"></a>Save (Button)</h4>
<ul>
<li>Save (Ctrl &#43; S) </li></ul>
<p><a id="user-content-save-as" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#save-as"></a></p>
<h4><a id="user-content-save-as-button" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#save-as-button"></a>Save As (Button)</h4>
<ul>
<li>Save As (F12) </li></ul>
<p><a id="user-content-edit" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#edit"></a></p>
<h3><a id="user-content-edit-group" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#edit-group"></a>Edit (Group)</h3>
<p><a id="user-content-undo" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#undo"></a></p>
<h4><a id="user-content-undo-button" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#undo-button"></a>Undo (Button)</h4>
<ul>
<li>Undo (Ctrl &#43; Z) </li></ul>
<p><a id="user-content-copy" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#copy"></a></p>
<h4><a id="user-content-copy-button" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#copy-button"></a>Copy (Button)</h4>
<ul>
<li>Copy (Ctrl &#43; C) </li></ul>
<p><a id="user-content-cut" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#cut"></a></p>
<h4><a id="user-content-cut-button" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#cut-button"></a>Cut (Button)</h4>
<ul>
<li>Cut (Ctrl &#43; X) </li></ul>
<p><a id="user-content-paste" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#paste"></a></p>
<h4><a id="user-content-paste-button" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#paste-button"></a>Paste (Button)</h4>
<ul>
<li>Paste (Ctrl &#43; V) </li></ul>
<p><a id="user-content-spelling" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#spelling"></a></p>
<h4><a id="user-content-spelling-button" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#spelling-button"></a>Spelling (Button)</h4>
<ul>
<li>Spelling (F7) </li></ul>
<p><a id="user-content-print-group" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#print-group"></a></p>
<h3><a id="user-content-print-group" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#print-group"></a>Print (Group)</h3>
<p><a id="user-content-setup" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#setup"></a></p>
<h4><a id="user-content-setup-button" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#setup-button"></a>Setup (Button)</h4>
<ul>
<li>Show the Sheet tab of the page setup dialog box </li></ul>
<p><a id="user-content-preview" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#preview"></a></p>
<h4><a id="user-content-preview-button" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#preview-button"></a>Preview (Button)</h4>
<ul>
<li>Preview (Ctrl &#43; F2) </li></ul>
<p><a id="user-content-print" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#print"></a></p>
<h4><a id="user-content-print-button" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#print-button"></a>Print (Button)</h4>
<ul>
<li>Print (Ctrl &#43; P) </li></ul>
<p><a id="user-content-program" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#program"></a></p>
<h3><a id="user-content-program-group" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#program-group"></a>Program (Group)</h3>
<p><a id="user-content-new" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#new"></a></p>
<h4><a id="user-content-new-button" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#new-button"></a>New (Button)</h4>
<ul>
<li>New file </li></ul>
<p><a id="user-content-open" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#open"></a></p>
<h4><a id="user-content-open-button" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#open-button"></a>Open (Button)</h4>
<ul>
<li>Open (Ctrl &#43; O) </li></ul>
<p><a id="user-content-close" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#close"></a></p>
<h4><a id="user-content-close-button" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#close-button"></a>Close (Button)</h4>
<ul>
<li>Close file </li></ul>
<p><a id="user-content-properties" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#properties"></a></p>
<h4><a id="user-content-properties-button" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#properties-button"></a>Properties (Button)</h4>
<ul>
<li>Open the properties of the file </li></ul>
<p><a id="user-content-options" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#options"></a></p>
<h4><a id="user-content-options-button" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#options-button"></a>Options (Button)</h4>
<ul>
<li>Open the options dialog box </li></ul>
<p><a id="user-content-exit" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#exit"></a></p>
<h4><a id="user-content-exit-button" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#exit-button"></a>Exit (Button)</h4>
<ul>
<li>Exit the application </li></ul>
<p><a id="user-content-evaluate-group" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#evaluate-group"></a></p>
<h3><a id="user-content-evaluate-group" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#evaluate-group"></a>Evaluate (Group)</h3>
<p><a id="user-content-calculator" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#calculator"></a></p>
<h4><a id="user-content-windows-calculator-button" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#windows-calculator-button"></a>Windows Calculator (Button)</h4>
<p><a href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/Images/ReadMe/windows_calculator.png" target="_blank"><img src=":-windows_calculator.png" alt="calculator"></a></p>
<ul>
<li>The Windows Calculator runs in standard mode, which resembles a four-function calculator. More advanced functions are available in scientific mode, including logarithms, numerical base conversions, some logical operators, operator precedence, radian, degree
 and gradians support as well as simple single-variable statistical functions </li></ul>
<p><a id="user-content-calculate-now" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#calculate-now"></a></p>
<h4><a id="user-content-calculate-now-button" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#calculate-now-button"></a>Calculate Now (Button)</h4>
<ul>
<li>Force the Calculation. Even if the Calculation option is set for Manual, you can force a calculation.
</li></ul>
<p><a id="user-content-annotation-group" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#annotation-group"></a></p>
<h3><a id="user-content-annotate-group" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#annotate-group"></a>Annotate (Group)</h3>
<p><a id="user-content-camera" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#camera"></a></p>
<h4><a id="user-content-excel-camerabutton" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#excel-camerabutton"></a>Excel Camera(Button)</h4>
<ul>
<li>The camera tool allows you to take a snapshot of any selected range of data, table, or graph, and paste it as a linked picture. The pasted snapshot can be formatted and resized using picture tools. They can be copied and pasted into Word and PowerPoint
 documents as well. The image is automatically refreshed if the data changes. </li></ul>
<p><a id="user-content-snip" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#snip"></a></p>
<h4><a id="user-content-snipping-tool-button" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#snipping-tool-button"></a>Snipping Tool (Button)</h4>
<p><a href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/Images/ReadMe/snipping_tool.png" target="_blank"><img src=":-snipping_tool.png" alt="snipping_tool"></a></p>
<ul>
<li>Capture all or part of your PC screen, add notes, save the snip, or email it from the Snipping Tool window. You can capture any of the following types of snips:
<ul>
<li>Free-form snip. Draw a free-form shape around an object. </li><li>Rectangular snip. Drag the cursor around an object to form a rectangle. </li><li>Window snip. Select a window, such as a browser window or dialog box, that you want to capture.
</li><li>Full-screen snip. Capture the entire screen. </li></ul>
</li></ul>
<p><a id="user-content-psr" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#psr"></a></p>
<h4><a id="user-content-problem-step-recorder-button" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#problem-step-recorder-button"></a>Problem Step Recorder (Button)</h4>
<p><a href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/Images/ReadMe/problem_steps_recorder.png" target="_blank"><img src=":-problem_steps_recorder.png" alt="psr"></a></p>
<ul>
<li>Steps Recorder (called Problems Steps Recorder in Windows 7), is a program that helps you troubleshoot a problem on your device by recording the exact steps you took when the problem occurred. You can then send this record to a support professional to help
 them diagnose the problem. </li></ul>
<p><a id="user-content-options" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#options"></a></p>
<h3><a id="user-content-options-group" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#options-group"></a>Options (Group)</h3>
<h4><a id="user-content-add-in-settings-button" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#add-in-settings-button"></a>Add-In Settings (Button)</h4>
<p>VSTO&nbsp;<br>
<img src=":-vsto.ribbon.settings.png" alt=""></p>
<ul>
<li>Types of VSTO Settings
<ul>
<li>Application Settings
<ul>
<li>These settings can only be changed in the project and need to be redeployed </li><li>They will appear disabled in the form </li></ul>
</li><li>User Settings
<ul>
<li>These settings can be changed by the end-user </li><li>They will appear enabled in the form </li></ul>
</li></ul>
</li></ul>
<p>VBA&nbsp;<br>
<img src=":-vba.settings.form.png" alt=""></p>
<ul>
<li>VBA Settings
<ul>
<li>To add a new setting
<div class="highlight highlight-source-vbnet">
<pre><span class="pl-smi">ThisWorkbook.CustomDocumentProperties.Add</span> <span class="pl-smi">_</span>
<span class="pl-smi">Name:=</span><span class="pl-s">&quot;App_ReleaseDate&quot;</span> <span class="pl-smi">_</span>
<span class="pl-smi">,</span> <span class="pl-smi">LinkToContent:=</span><span class="pl-k">False</span> <span class="pl-smi">_</span>
<span class="pl-smi">,</span> <span class="pl-smi">Type:=msoPropertyTypeDate</span> <span class="pl-smi">_</span>
<span class="pl-smi">,</span> <span class="pl-smi">Value:=</span><span class="pl-s">&quot;31-Jul-2017 1:05pm&quot;</span></pre>
</div>
</li><li>To update a setting
<div class="highlight highlight-source-vbnet">
<pre><span class="pl-smi">ThisWorkbook.CustomDocumentProperties.Item(</span><span class="pl-s">&quot;App_ReleaseDate&quot;</span><span class="pl-smi">).Value</span> <span class="pl-smi">=</span> <span class="pl-s">&quot;31-Jul-2017 1:05pm&quot;</span></pre>
</div>
</li><li>To delete a setting
<div class="highlight highlight-source-vbnet">
<pre><span class="pl-smi">ThisWorkbook.CustomDocumentProperties.Item(</span><span class="pl-s">&quot;App_ReleaseDate&quot;</span><span class="pl-smi">).Delete</span></pre>
</div>
</li></ul>
</li></ul>
<p><a id="user-content-help" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#help"></a></p>
<h3><a id="user-content-help-group" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#help-group"></a>Help (Group)</h3>
<h1><a id="user-content----1" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#---1"></a><a href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/Images/ReadMe/ribbon.group.help.png" target="_blank"><img src=":-ribbon.group.help.png" alt="help"></a></h1>
<p><a id="user-content-how-to" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#how-to"></a></p>
<h4><a id="user-content-how-to-button" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#how-to-button"></a>How To... (Button)</h4>
<ul>
<li>Opens the how to guide in a browser </li></ul>
<p><a id="user-content-report-issue" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#report-issue"></a></p>
<h4><a id="user-content-report-issue-button" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#report-issue-button"></a>Report Issue (Button)</h4>
<ul>
<li>Opens the new issue page in a browser </li></ul>
<p><a id="user-content-new-version" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#new-version"></a></p>
<h4><a id="user-content-new-version-is-available-button" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#new-version-is-available-button"></a>New Version Is Available (Button)</h4>
<ul>
<li>This button is visible if the version of the Add-In is different from the one in the Read Me page. It will download a new version from the site when pressed.
</li></ul>
<p><a id="user-content-about" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#about"></a></p>
<h3><a id="user-content-about-group" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#about-group"></a>About (Group)</h3>
<h1><a id="user-content----2" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#---2"></a><a href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/Images/ReadMe/ribbon.group.about.png" target="_blank"><img src=":-ribbon.group.about.png" alt="about"></a></h1>
<p><a id="user-content-description" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#description"></a></p>
<h4><a id="user-content-add-in-name-label" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#add-in-name-label"></a>Add-in Name (Label)</h4>
<ul>
<li>The application name with the version </li></ul>
<p><a id="user-content-release-date" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#release-date"></a></p>
<h4><a id="user-content-release-date-label" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#release-date-label"></a>Release Date (Label)</h4>
<ul>
<li>The release date of the application </li></ul>
<p><a id="user-content-copyright" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#copyright"></a></p>
<h4><a id="user-content-copyright-label" class="anchor" href="https://github.com/aduguid/MicrosoftExcelFavorites/blob/master/README.md#copyright-label"></a>Copyright (Label)</h4>
<ul>
<li>The author&rsquo;s name </li></ul>
