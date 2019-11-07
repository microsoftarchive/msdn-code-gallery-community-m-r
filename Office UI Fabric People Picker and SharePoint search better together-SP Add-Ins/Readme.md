# Office UI Fabric People Picker and SharePoint search better together-SP Add-Ins
## Requires
- Visual Studio 2015
## License
- MIT
## Technologies
- Sharepoint Online
- apps for SharePoint
- SharePoint 2013
- SharePoint Add-ins
## Topics
- REST API
- People picker
- SharePoint Add-in
- Office UI Fabric
## Updated
- 09/11/2017
## Description

<h1>Introduction</h1>
<p>In this article I'll be focused on&nbsp;<a href="https://dev.office.com/fabric" target="_blank">Office UI Fabric</a>, the official Office and Office 365 front-end framework, in particular I'll talk about the&nbsp;<a href="https://dev.office.com/fabric#/components/peoplepicker" target="_blank">People
 Picker</a>, an important component to have a user friendly and comfortable functionality for the end user.</p>
<p>On the Office UI Fabric website, there is a&nbsp;<a href="https://dev.office.com/fabric#/components/peoplepicker" target="_blank">sample</a>&nbsp;of this helpful control, however the demo is not so complete because we have static info about the people, I
 mean there is not a use case where is possible to retrieve the people across a search and display them in the people picker field dynamically.</p>
<h1><span>Building the Sample</span></h1>
<p><img id="170809" src="170809-officeuifabricpeoplepickeradd-in.gif" alt="" width="1909" height="493"></p>
<p><em><span>So I decided to develop a solution to fill the gap, my intention is, to split this topic in two parts, I'll give you a first solution with&nbsp;</span><a href="https://msdn.microsoft.com/en-us/library/office/fp179930.aspx?f=255&MSPPError=-2147217396" target="_blank">SharePoint
 Add-In</a><span>&nbsp;and another one with&nbsp;</span><a href="https://dev.office.com/sharepoint/docs/spfx/sharepoint-framework-overview" target="_blank">SharePoint Framework</a><span>&nbsp;in another article.</span></em></p>
<p><em><span><span>Let's go forward with the solution, I created a simple&nbsp;</span><a href="https://msdn.microsoft.com/en-us/library/office/fp142379.aspx?f=255&MSPPError=-2147217396" target="_blank">SharePoint Hosted App</a><span>&nbsp;to achieve my goal.</span></span></em></p>
<p><em><span><span><span>In order to take advantage the SharePoint Search, is&nbsp;necessary specify in the App manifest the right permission:</span></span></span></em></p>
<p><em><span><span><span><img id="170810" src="170810-officeuifabricpeoplepickeradd-inappmanifest.png" alt="" width="708" height="246"><br>
</span></span></span></em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><em>Regarding the logic, every time that user will write&nbsp;something in the text field, the App will perform a&nbsp;<a href="https://msdn.microsoft.com/en-us/library/office/jj163876.aspx" target="_blank">REST API call on the SharePoint Search</a>&nbsp;to
 grab a result filtered, and the syntax of the url will be something like that:</em></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">/_api/search/query?querytext='*&quot; &#43; &quot;Characters to search&quot; &#43; &quot;*'&amp;rowlimit=10&amp;sourceid='b09a7990-05ea-4af9-81ef-edfab16c4e31'</pre>
<div class="preview">
<pre class="js"><span class="js__reg_exp">/_api/</span>search/query?querytext=<span class="js__string">'*&quot;&nbsp;&#43;&nbsp;&quot;Characters&nbsp;to&nbsp;search&quot;&nbsp;&#43;&nbsp;&quot;*'</span>&amp;rowlimit=<span class="js__num">10</span>&amp;sourceid=<span class="js__string">'b09a7990-05ea-4af9-81ef-edfab16c4e31'</span></pre>
</div>
</div>
</div>
<p>As you can see I specified a row limit&nbsp;to not overlook the performance and most important,&nbsp;I have narrowed the field of action&nbsp;<a href="https://social.technet.microsoft.com/wiki/contents/articles/25074.sharepoint-online-working-with-people-search-and-user-profiles.aspx" target="_blank">to
 work only with people and user profiles</a>&nbsp;(sourceid='b09a7990-05ea-4af9-81ef-edfab16c4e31').</p>
<p>Naturally the App is ready to go, then you can deploy it directly on your SharePoint Online or On-Premise.</p>
<h1><span>Source Code Files</span></h1>
<p>The solution it's also available on github:</p>
<p><a href="https://github.com/giuleon/OfficeUIFabricPeoplePickerAddIn" target="_blank">https://github.com/giuleon/OfficeUIFabricPeoplePickerAddIn</a></p>
<p>&nbsp;</p>
<h1>More Information</h1>
<p class="projectTitle"><a href="https://code.msdn.microsoft.com/People-Picker-and-440a76b6" target="_blank">People Picker and SharePoint search better together - SharePoint Framework</a></p>
<p><em><em><em><em><a href="http://www.delucagiuliano.com/office-ui-fabric-people-picker-and-sharepoint-search-better-together-part-1" target="_blank">Read this article on my website</a></em></em></em></em></p>
<p><em><em><em><em><a href="https://youtu.be/_SAOlHZBTR4" target="_blank">https://youtu.be/_SAOlHZBTR4</a><br>
</em></em></em></em></p>
