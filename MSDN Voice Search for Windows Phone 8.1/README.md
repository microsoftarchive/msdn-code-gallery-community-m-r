# MSDN Voice Search for Windows Phone 8.1
## Requires
- Visual Studio 2013
## License
- MS-LPL
## Technologies
- Silverlight
- Windows Phone
- Windows Phone 8
- Windows Phone Development
- Windows Phone 8.1
## Topics
- Phone application development
- Windows Phone
- Speech recognition
- Speech Synthesis
- Speech
- Windows Phone 8
- Bing Speech
## Updated
- 04/13/2014
## Description

<h1>Introduction</h1>
<p><strong>MSDN Voice Search</strong> is a Store application for Windows Phone 8.1 and Windows Phone 8.0 written to demonstrate the new capabilities of Cortana-integrated Voice Commands on Windows Phone 8.1 as well as to provide examples of good practices when
 dealing with user input, including by &quot;continuing the conversation&quot; using the in-application Speech Recognition and Speech Synthesis APIs available on Windows Phone.</p>
<p>The application itself is a fully-functional utility designed to enable voice- and text-based natural language searches of MSDN, but it's primary purpose is to facilitate developer interest and education in the Speech and Natural Language integration features
 available on Windows Phone.</p>
<h1><span>Building the Sample</span></h1>
<p>The <strong>MSDN Voice Search</strong> project was built using the Windows Phone 8.1 SDK on Visual Studio 2013. Prior versions of Visual Studio are not supported.</p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><strong>MSDN Voice Search </strong>demonstrates end-to-end use of Cortana-enabled Voice Commands, Speech Recognition, and Speech Synthesis as combined to deliver a complete and natural user experience for both spoken and text input.</p>
<p>The included <em>Voice Command Definition</em>&nbsp;files (VCDs) are installed at application launch, which will provide integration into the system-level commands for queries like &quot;MSDN, find Windows Phone Voice Commands&quot; or &quot;MSDN, go to the Windows Phone
 Dev Center.&quot;</p>
<p>The code in MainPage.xaml.cs demonstrates low- to mid-complexity management of Voice Command parsing, Speech Recognition use, and Speech Synthesis use.</p>
<p><img id="112762" src="http://i1.code.msdn.s-msft.com/msdn-voice-search-for-95c16d92/image/file/112762/1/ss_small.png" alt="" width="180" height="300">&nbsp;<img id="112763" src="http://i1.code.msdn.s-msft.com/msdn-voice-search-for-95c16d92/image/file/112763/1/ss_small2.png" alt="" width="180" height="300">&nbsp;<img id="112765" src="http://i1.code.msdn.s-msft.com/msdn-voice-search-for-95c16d92/image/file/112765/1/ss_small3.png" alt="" width="180" height="300"></p>
<h1><span>Source Code Files</span></h1>
<ul>
<li><strong>MainPage.xaml</strong> - The UI definitions associated with the application
</li><li><strong>MainPage.xaml.cs</strong> - The bulk of the programmatic logic is located in this file
</li><li><strong>AppResources.resx</strong> - Resource strings are located here, ready for future localization
</li><li><strong>VoiceCommandDefinition_8.1.xml</strong> - The Voice Command Definition (VCD) for Windows Phone 8.1
</li><li><strong>VoiceCommandDefinition_8.0.xml</strong> - The fallback Voice Command Definition (VCD) for Windows Phone 8.0
</li></ul>
<h1>More Information</h1>
<p>To learn more about the Voice Command, Speech Recogniton, and Speech Synthesis capabilities available to Store applications on Windows Phone, see the Bing Dev Center page:
<a href="http://www.bing.com/dev/en-us/speech">http://www.bing.com/dev/en-us/speech</a>.</p>
