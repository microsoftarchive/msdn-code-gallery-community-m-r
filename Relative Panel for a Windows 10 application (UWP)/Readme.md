# Relative Panel for a Windows 10 application (UWP)
## Requires
- Visual Studio 2015
## License
- MIT
## Technologies
- XAML
- Windows UWP
## Topics
- UWP
## Updated
- 12/28/2015
## Description

<h1>Introduction</h1>
<p><em>In this sample, we will discover a new tool available in XAML for the Universal Windows Platform which is the Relative Panel. Almost every application will have to deal with a Realtive Panel in order to adapt its UI and components.</em></p>
<h1><span>Building the Sample</span></h1>
<p><em>This sample needs to run on Visual Studio 2015 which runs on Windows 10.</em></p>
<p><em>Make sure to activate the Developer mode on your machine in order to test the sample.</em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p>This sample will shows us how to deal with many XAML controls inside a relative pannel. The Relative Panel will help us adapt the UI of our application depending on the maximise or minimize with the user inputs.</p>
<p>We will have some rectangles with different colors in the UI of our application. These rectangles will be inside a Relative Panel. Once you will run the project and try to minimize the application, you will notice the behaviour of our XAML controls and how
 do they adapt to the changes occured on the UI depending on the size of the applicaiton running.</p>
<p>The Relative Pannel has a child proprety which means that it can contain more than a proprety inside it which are the rectangles in our case.</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XAML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xaml</span>
<pre class="hidden">&lt;RelativePanel MinHeight=&quot;300&quot; Grid.Row=&quot;1&quot; &gt;

            &lt;Rectangle Name=&quot;RedRectangle&quot; 
                           Fill=&quot;Red&quot; 
                           Width=&quot;100&quot; 
                           Height=&quot;100&quot; 
                           RelativePanel.AlignRightWithPanel=&quot;True&quot; /&gt;

            &lt;Rectangle Name=&quot;BlueRectangle&quot; 
                           Fill=&quot;Blue&quot; 
                           Width=&quot;50&quot; 
                           Height=&quot;50&quot; 
                           RelativePanel.LeftOf=&quot;RedRectangle&quot; /&gt;

            &lt;Rectangle Name=&quot;GreenRectangle&quot; 
                           Fill=&quot;Green&quot; 
                           Width=&quot;50&quot; 
                           Height=&quot;50&quot; 
                           RelativePanel.AlignVerticalCenterWith=&quot;RedRectangle&quot; 
                           RelativePanel.AlignHorizontalCenterWithPanel=&quot;True&quot; /&gt;

            &lt;Rectangle Name=&quot;YellowRectangle&quot; 
                           Fill=&quot;Yellow&quot;
                           MinWidth=&quot;50&quot; 
                           MinHeight=&quot;50&quot;
                           RelativePanel.AlignBottomWithPanel=&quot;True&quot;
                           RelativePanel.AlignTopWith=&quot;PurpleRectangle&quot;/&gt;


            &lt;Rectangle Name=&quot;PurpleRectangle&quot; 
                           Fill=&quot;Purple&quot; 
                           MinWidth=&quot;50&quot; MinHeight=&quot;50&quot; 
                           RelativePanel.Below=&quot;RedRectangle&quot; 
                           RelativePanel.AlignRightWith=&quot;RedRectangle&quot;
                           RelativePanel.AlignLeftWith=&quot;GreenRectangle&quot;
                            /&gt;

        &lt;/RelativePanel&gt;</pre>
<div class="preview">
<pre class="xaml"><span class="xaml__tag_start">&lt;RelativePanel</span>&nbsp;<span class="xaml__attr_name">MinHeight</span>=<span class="xaml__attr_value">&quot;300&quot;</span>&nbsp;Grid.<span class="xaml__attr_name">Row</span>=<span class="xaml__attr_value">&quot;1&quot;</span>&nbsp;<span class="xaml__tag_start">&gt;&nbsp;
</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;Rectangle</span>&nbsp;<span class="xaml__attr_name">Name</span>=<span class="xaml__attr_value">&quot;RedRectangle&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">Fill</span>=<span class="xaml__attr_value">&quot;Red&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">Width</span>=<span class="xaml__attr_value">&quot;100&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">Height</span>=<span class="xaml__attr_value">&quot;100&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;RelativePanel.<span class="xaml__attr_name">AlignRightWithPanel</span>=<span class="xaml__attr_value">&quot;True&quot;</span>&nbsp;<span class="xaml__tag_start">/&gt;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;Rectangle</span>&nbsp;<span class="xaml__attr_name">Name</span>=<span class="xaml__attr_value">&quot;BlueRectangle&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">Fill</span>=<span class="xaml__attr_value">&quot;Blue&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">Width</span>=<span class="xaml__attr_value">&quot;50&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">Height</span>=<span class="xaml__attr_value">&quot;50&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;RelativePanel.<span class="xaml__attr_name">LeftOf</span>=<span class="xaml__attr_value">&quot;RedRectangle&quot;</span>&nbsp;<span class="xaml__tag_start">/&gt;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;Rectangle</span>&nbsp;<span class="xaml__attr_name">Name</span>=<span class="xaml__attr_value">&quot;GreenRectangle&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">Fill</span>=<span class="xaml__attr_value">&quot;Green&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">Width</span>=<span class="xaml__attr_value">&quot;50&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">Height</span>=<span class="xaml__attr_value">&quot;50&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;RelativePanel.<span class="xaml__attr_name">AlignVerticalCenterWith</span>=<span class="xaml__attr_value">&quot;RedRectangle&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;RelativePanel.<span class="xaml__attr_name">AlignHorizontalCenterWithPanel</span>=<span class="xaml__attr_value">&quot;True&quot;</span>&nbsp;<span class="xaml__tag_start">/&gt;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;Rectangle</span>&nbsp;<span class="xaml__attr_name">Name</span>=<span class="xaml__attr_value">&quot;YellowRectangle&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">Fill</span>=<span class="xaml__attr_value">&quot;Yellow&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">MinWidth</span>=<span class="xaml__attr_value">&quot;50&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">MinHeight</span>=<span class="xaml__attr_value">&quot;50&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;RelativePanel.<span class="xaml__attr_name">AlignBottomWithPanel</span>=<span class="xaml__attr_value">&quot;True&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;RelativePanel.<span class="xaml__attr_name">AlignTopWith</span>=<span class="xaml__attr_value">&quot;PurpleRectangle&quot;</span><span class="xaml__tag_start">/&gt;</span>&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;Rectangle</span>&nbsp;<span class="xaml__attr_name">Name</span>=<span class="xaml__attr_value">&quot;PurpleRectangle&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">Fill</span>=<span class="xaml__attr_value">&quot;Purple&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">MinWidth</span>=<span class="xaml__attr_value">&quot;50&quot;</span>&nbsp;<span class="xaml__attr_name">MinHeight</span>=<span class="xaml__attr_value">&quot;50&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;RelativePanel.<span class="xaml__attr_name">Below</span>=<span class="xaml__attr_value">&quot;RedRectangle&quot;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;RelativePanel.<span class="xaml__attr_name">AlignRightWith</span>=<span class="xaml__attr_value">&quot;RedRectangle&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;RelativePanel.<span class="xaml__attr_name">AlignLeftWith</span>=<span class="xaml__attr_value">&quot;GreenRectangle&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">/&gt;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_end">&lt;/RelativePanel&gt;</span></pre>
</div>
</div>
</div>
<h1><span>Source Code Files</span></h1>
<ul>
<li><em>MainPage.xaml #1 - summary contains the XAML controls</em> </li></ul>
<p>&nbsp;</p>
<ul>
<li><span style="font-size:2em">More Information</span> </li></ul>
<p><em>For more information feel free to ping me on Twitter @khaledjemni</em></p>
