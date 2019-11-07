# RadioButton Sample in Xamarin.Forms (C# - Xaml)
## Requires
- Visual Studio 2012
## License
- MIT
## Technologies
- Xamarin.Forms
## Topics
- radio button in Xamarin.Forms
## Updated
- 03/13/2018
## Description

<p><span style="font-family:verdana,sans-serif; font-size:small"><strong>Introduction:</strong></span></p>
<p><span style="font-family:verdana,sans-serif; font-size:small">In Xamarin.Forms, there is no default RadioButton control available and we need to create our own custom RadioButton control. This article can explain you about to create RadioButton&nbsp;control
 and it's properties.&nbsp;&nbsp;</span></p>
<p><span style="color:#ff6600"><strong><span style="font-family:verdana,sans-serif; font-size:small">You can also read this article from my original blog from
<a title="RadioButton" href="http://bsubramanyamraju.blogspot.in/2018/03/how-to-create-radiobutton-in_13.html" target="_blank">
here</a>.</span></strong></span></p>
<p><span style="font-size:small"><a href="https://2.bp.blogspot.com/-6d1S4kJ_RwU/WqfIKFX3sII/AAAAAAAADiM/ws4_PQWUAFcXd1fPLMEdLPfpcVaLJUZTgCLcBGAs/s1600/RadioButton.png"><img src=":-radiobutton.png" border="0" alt="" width="317" height="320"></a></span></p>
<div></div>
<p><span style="font-size:small"><strong>Requirements:</strong></span></p>
<ul>
<li><span style="font-family:verdana,sans-serif; font-size:small">This article source code was prepared by using Visual Studio Community for Mac (7.3.2). And it is better to install latest visual studio updates from&nbsp;<a href="https://www.visualstudio.com/downloads/">here</a>.</span>
</li><li><span style="font-family:verdana,sans-serif; font-size:small">This sample project is Xamarin.Forms project and tested in Android emulator and iOS simulator.</span>
</li></ul>
<div><span style="font-family:verdana,sans-serif; font-size:small"><strong>Description:</strong>
</span></div>
<div><span style="font-family:verdana,sans-serif; font-size:small">This article can explain you below topics:
</span></div>
<div><span style="font-family:verdana,sans-serif; font-size:small">1. How to create Xamarin.Forms PCL project with Visual studio for Mac?
</span></div>
<div><span style="font-family:verdana,sans-serif; font-size:small">2. How to create RadioButton&nbsp;in Xamarin.Forms app?
</span></div>
<div><span style="font-family:verdana,sans-serif; font-size:small">3. How to use custom RadioButton&nbsp;in Xamarin.Forms?
</span></div>
<div><span style="font-family:verdana,sans-serif; font-size:small"><br>
</span></div>
<div><span style="font-size:small"><strong><span style="font-family:verdana,sans-serif">1.&nbsp;</span><span style="font-family:verdana,sans-serif">How to create Xamarin.Forms PCL project with Visual studio for Mac?</span></strong>
</span></div>
<div><span style="font-family:verdana,sans-serif; font-size:small">Before to create RadioButton control, first we need to create the new Xamarin.Forms project.&nbsp;
</span></div>
<div>&nbsp;
<ul>
<li><span style="font-family:verdana,sans-serif; font-size:small">Launch Visual Studio for Mac.</span>
</li><li><span style="font-family:verdana,sans-serif; font-size:small">On the File menu, select New Solution.</span>
</li><li><span style="font-family:verdana,sans-serif; font-size:small">The New Project dialog appears. The left pane of the dialog lets you select the type of templates to display. In the left pane, expand&nbsp;<strong>Multiplatform&nbsp;</strong>&gt;&nbsp;<strong>App&nbsp;</strong>&gt;&nbsp;<strong>Xamarin.Forms</strong>&nbsp;&gt;&nbsp;<strong>Forms
 App&nbsp;</strong>and click on&nbsp;<strong>Next</strong>. <a href="https://1.bp.blogspot.com/-G5KjrAxLlBY/WqNvoiY_mKI/AAAAAAAADdI/doBPbrs-LCoemZyvQDe6JYqag3ihJZGjQCLcBGAs/s1600/1.NewProject.png">
<img src=":-1.newproject.png" border="0" alt="" width="640" height="464"></a>
</span></li><li><span style="font-size:small"><span style="font-family:verdana,sans-serif">Enter your App Name (Ex:&nbsp;RadioButtonSample). Select&nbsp;<strong>Target Platforms&nbsp;</strong>to Android &amp; iOS and click on&nbsp;<strong>Next</strong><span style="font-family:verdana,sans-serif">&nbsp;button.</span></span>
<span style="font-family:verdana,sans-serif"><span style="font-family:verdana,sans-serif"><a href="https://3.bp.blogspot.com/--jOoJxEhLVY/WqfIzOJLAEI/AAAAAAAADiU/cxpixu17WvMpJMO_0bj8ctnjS1gatTUaQCLcBGAs/s1600/Screen%2BShot%2B2018-03-13%2Bat%2B6.18.03%2BPM.png"><img src=":-screen%2bshot%2b2018-03-13%2bat%2b6.18.03%2bpm.png" border="0" alt="" width="640" height="462"></a></span></span>
</span><br>
&nbsp; <br>
</li><li><span style="font-size:small"><span style="font-family:verdana,sans-serif">You can choose your project location like below and&nbsp;<strong>Create</strong>&nbsp;new project.</span>
<span style="font-family:verdana,sans-serif"><a href="https://3.bp.blogspot.com/-hVLiwf1bWZc/WqfI0OQP5TI/AAAAAAAADiY/G9JTMhjhv24vVSkboRBP-EknlXGmqvXQgCEwYBhgL/s1600/Screen%2BShot%2B2018-03-13%2Bat%2B6.18.29%2BPM.png"><img src=":-screen%2bshot%2b2018-03-13%2bat%2b6.18.29%2bpm.png" border="0" alt="" width="640" height="464"></a></span>
</span><br>
&nbsp; <br>
&nbsp; </li></ul>
<span style="font-size:small"><span style="font-family:verdana,sans-serif">After that your new Xamarin.Forms will be load with default MVVM pattern which will have three folders name like&nbsp;</span><strong>Models</strong><span style="font-family:verdana,sans-serif">,&nbsp;</span><strong>Views</strong><span style="font-family:verdana,sans-serif">,&nbsp;</span><strong>ViewModels.&nbsp;</strong><span style="font-family:verdana,sans-serif">So
 remove all files inside that folders and later we will add our own files in this article.</span>
<span style="font-family:verdana,sans-serif"><strong><br>
</strong></span><span style="font-family:verdana,sans-serif"><strong>2.&nbsp;</strong></span><span style="font-family:verdana,sans-serif"><strong>How to create&nbsp;RadioButton&nbsp;in Xamarin.Forms app?</strong></span>
<span style="font-family:verdana,sans-serif">&nbsp;</span> </span></div>
<div><span style="font-size:small"><span style="font-family:verdana,sans-serif">If you search for RadioButton&nbsp;in Xamarin.Forms in internet you will find more resources to create RadioButton&nbsp;and you may find mostly two answers&nbsp;like below</span>
<span style="font-family:verdana,sans-serif">1. We can create our own RadioButton&nbsp;by inheriting&nbsp;<strong>ContentView.</strong></span>
<span style="font-family:verdana,sans-serif">2. We can use Nuget package library like&nbsp;<a href="https://github.com/XLabs/Xamarin-Forms-Labs">XLabs.form</a>. But this is having more bugs and i</span><span style="font-family:verdana,sans-serif">s no longer
 maintained, and even says so on the github repo.</span> <span style="font-family:verdana,sans-serif">
<br>
</span><span style="font-family:verdana,sans-serif">In this article, we are choosing first option to create RadioButton&nbsp;by&nbsp;</span><span style="font-family:verdana,sans-serif">inheriting&nbsp;</span><strong>ContentView.&nbsp;</strong><span style="font-family:verdana,sans-serif">And
 please follow below few steps for the same</span> <strong><br>
</strong></span></div>
<div><span style="font-family:verdana,sans-serif; font-size:small"><strong>Step 1: Create ContentView</strong>
</span></div>
<div><span style="font-size:small"><span style="font-family:verdana,sans-serif">We are following default MVVM design pattern. And here we will place custom controls in Controls folder. So right click on your project name RadioButton Sample =&gt; Add =&gt; New
 Folder name is &quot;Controls&quot;. After that right click on your newly created the folder =&gt; Add =&gt; New File =&gt; Forms =&gt; Forms ContentView Xaml and name it RadioButton.</span>
<span style="font-family:verdana,sans-serif"><a href="https://4.bp.blogspot.com/-JILNwl3nYrk/WqfKLQYGL1I/AAAAAAAADi0/qzhWJsZs71EOmDlXuAtf7PsOD6rDs8BVwCLcBGAs/s1600/Screen%2BShot%2B2018-03-13%2Bat%2B6.24.45%2BPM.png"><img src=":-screen%2bshot%2b2018-03-13%2bat%2b6.24.45%2bpm.png" border="0" alt="" width="640" height="486"></a></span><strong>Step
 2: Add UI to&nbsp;</strong><span style="font-family:verdana,sans-serif">RadioButton</span>
<span style="font-family:verdana,sans-serif">&nbsp;</span> </span></div>
<div><span style="font-family:verdana,sans-serif; font-size:small">In general, RadioButton&nbsp;control required major four elements.
</span>
<ul>
<li><span style="font-size:small"><strong>Title</strong><span style="font-family:verdana,sans-serif">: Here we will take Label control to display title of RadioButton.</span></span>
</li><li><span style="font-family:verdana,sans-serif; font-size:small"><strong>Border</strong>: Here we will take one Image control that will hold&nbsp;BorderImageSource.</span>
</li><li><span style="font-size:small"><span style="font-family:verdana,sans-serif"><strong>Checkmark:&nbsp;</strong></span><span style="font-family:verdana,sans-serif">Here we will take one Image control that will hold&nbsp;CheckMarkSource. And this check mark
 visibility should be based on user tap interaction which should deal by the some other property like IsChecked. I mean if&nbsp;IsChecked is True, check mark should be visible or else not visible.</span></span>
</li><li><span style="font-family:verdana,sans-serif; font-size:small"><strong>Background:&nbsp;</strong>Here we will take one Image control that will hold&nbsp;BackGroundImageSource</span>
</li></ul>
<span style="font-size:small"><span style="font-family:verdana,sans-serif">So in your content view we need three Image controls and one label.</span>
<span style="font-family:verdana,sans-serif">Now open RadioButton.xaml and add below code.</span>
<span style="font-family:verdana,sans-serif">&nbsp;
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XAML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xaml</span>
<pre class="hidden">&lt;?xml version=&quot;1.0&quot; encoding=&quot;UTF-8&quot;?&gt;  
&lt;ContentView xmlns=&quot;http://xamarin.com/schemas/2014/forms&quot; xmlns:x=&quot;http://schemas.microsoft.com/winfx/2009/xaml&quot; x:Class=&quot;RadioButtonSample.Controls.RadioButton&quot;&gt;  
    &lt;ContentView.Content&gt;  
        &lt;StackLayout Orientation=&quot;Horizontal&quot;  
                     x:Name=&quot;mainContainer&quot;  
                     HorizontalOptions=&quot;FillAndExpand&quot;  
                     VerticalOptions=&quot;FillAndExpand&quot;  
                     Padding=&quot;0&quot;  
                     Spacing=&quot;5&quot;&gt;  
            &lt;AbsoluteLayout HorizontalOptions=&quot;Center&quot;  
                            VerticalOptions=&quot;Center&quot;  
                            WidthRequest=&quot;20&quot;  
                            HeightRequest=&quot;20&quot;  
                            x:Name=&quot;imageContainer&quot;&gt;  
                &lt;Image Source=&quot;{Binding CheckedBackgroundImageSource}&quot;  
                       x:Name=&quot;checkedBackground&quot;  
                       Aspect=&quot;AspectFit&quot;  
                       AbsoluteLayout.LayoutBounds=&quot;0.5, 0.5, 1, 1&quot;  
                       AbsoluteLayout.LayoutFlags=&quot;All&quot;  
                       Opacity=&quot;0&quot;  
                       InputTransparent=&quot;True&quot;/&gt;  
                &lt;Image Source=&quot;{Binding BorderImageSource}&quot;  
                       x:Name=&quot;borderImage&quot;  
                       Aspect=&quot;AspectFit&quot;  
                       AbsoluteLayout.LayoutBounds=&quot;0.5, 0.5, 1, 1&quot;  
                       AbsoluteLayout.LayoutFlags=&quot;All&quot;  
                       InputTransparent=&quot;True&quot;/&gt;  
                &lt;Image Source=&quot;{Binding CheckmarkImageSource}&quot;  
                       x:Name=&quot;checkedImage&quot;  
                       Aspect=&quot;AspectFit&quot;  
                       AbsoluteLayout.LayoutBounds=&quot;0.5, 0.5, 1, 1&quot;  
                       AbsoluteLayout.LayoutFlags=&quot;All&quot;  
                       Opacity=&quot;0&quot;  
                       InputTransparent=&quot;True&quot;/&gt;  
            &lt;/AbsoluteLayout&gt;  
            &lt;Label x:Name=&quot;controlLabel&quot;  
                   HorizontalOptions=&quot;FillAndExpand&quot;  
                   VerticalOptions=&quot;FillAndExpand&quot;  
                   HorizontalTextAlignment=&quot;Start&quot;  
                   VerticalTextAlignment=&quot;Center&quot;  
                   Text=&quot;{Binding Title}&quot;  
                   Style=&quot;{Binding LabelStyle}&quot;  
                   InputTransparent=&quot;True&quot;/&gt;  
        &lt;/StackLayout&gt;  
    &lt;/ContentView.Content&gt;  
&lt;/ContentView&gt;  </pre>
<div class="preview">
<pre class="xaml"><span class="xaml__tag_start">&lt;?xml</span>&nbsp;<span class="xaml__attr_name">version</span>=<span class="xaml__attr_value">&quot;1.0&quot;</span>&nbsp;<span class="xaml__attr_name">encoding</span>=<span class="xaml__attr_value">&quot;UTF-8&quot;</span><span class="xaml__tag_start">?&gt;</span>&nbsp;&nbsp;&nbsp;
<span class="xaml__tag_start">&lt;ContentView</span>&nbsp;<span class="xaml__attr_name">xmlns</span>=<span class="xaml__attr_value">&quot;http://xamarin.com/schemas/2014/forms&quot;</span>&nbsp;<span class="xaml__keyword">xmlns</span>:<span class="xaml__attr_name">x</span>=<span class="xaml__attr_value">&quot;http://schemas.microsoft.com/winfx/2009/xaml&quot;</span>&nbsp;x:<span class="xaml__attr_name">Class</span>=<span class="xaml__attr_value">&quot;RadioButtonSample.Controls.RadioButton&quot;</span><span class="xaml__tag_start">&gt;&nbsp;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;ContentView</span>.Content<span class="xaml__tag_start">&gt;&nbsp;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;StackLayout</span>&nbsp;<span class="xaml__attr_name">Orientation</span>=<span class="xaml__attr_value">&quot;Horizontal&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;x:<span class="xaml__attr_name">Name</span>=<span class="xaml__attr_value">&quot;mainContainer&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">HorizontalOptions</span>=<span class="xaml__attr_value">&quot;FillAndExpand&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">VerticalOptions</span>=<span class="xaml__attr_value">&quot;FillAndExpand&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">Padding</span>=<span class="xaml__attr_value">&quot;0&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">Spacing</span>=<span class="xaml__attr_value">&quot;5&quot;</span><span class="xaml__tag_start">&gt;&nbsp;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;AbsoluteLayout</span>&nbsp;<span class="xaml__attr_name">HorizontalOptions</span>=<span class="xaml__attr_value">&quot;Center&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">VerticalOptions</span>=<span class="xaml__attr_value">&quot;Center&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">WidthRequest</span>=<span class="xaml__attr_value">&quot;20&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">HeightRequest</span>=<span class="xaml__attr_value">&quot;20&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;x:<span class="xaml__attr_name">Name</span>=<span class="xaml__attr_value">&quot;imageContainer&quot;</span><span class="xaml__tag_start">&gt;&nbsp;</span>&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;Image</span>&nbsp;<span class="xaml__attr_name">Source</span>=<span class="xaml__attr_value">&quot;{Binding&nbsp;CheckedBackgroundImageSource}&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;x:<span class="xaml__attr_name">Name</span>=<span class="xaml__attr_value">&quot;checkedBackground&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">Aspect</span>=<span class="xaml__attr_value">&quot;AspectFit&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;AbsoluteLayout.<span class="xaml__attr_name">LayoutBounds</span>=<span class="xaml__attr_value">&quot;0.5,&nbsp;0.5,&nbsp;1,&nbsp;1&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;AbsoluteLayout.<span class="xaml__attr_name">LayoutFlags</span>=<span class="xaml__attr_value">&quot;All&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">Opacity</span>=<span class="xaml__attr_value">&quot;0&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">InputTransparent</span>=<span class="xaml__attr_value">&quot;True&quot;</span><span class="xaml__tag_start">/&gt;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;Image</span>&nbsp;<span class="xaml__attr_name">Source</span>=<span class="xaml__attr_value">&quot;{Binding&nbsp;BorderImageSource}&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;x:<span class="xaml__attr_name">Name</span>=<span class="xaml__attr_value">&quot;borderImage&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">Aspect</span>=<span class="xaml__attr_value">&quot;AspectFit&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;AbsoluteLayout.<span class="xaml__attr_name">LayoutBounds</span>=<span class="xaml__attr_value">&quot;0.5,&nbsp;0.5,&nbsp;1,&nbsp;1&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;AbsoluteLayout.<span class="xaml__attr_name">LayoutFlags</span>=<span class="xaml__attr_value">&quot;All&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">InputTransparent</span>=<span class="xaml__attr_value">&quot;True&quot;</span><span class="xaml__tag_start">/&gt;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;Image</span>&nbsp;<span class="xaml__attr_name">Source</span>=<span class="xaml__attr_value">&quot;{Binding&nbsp;CheckmarkImageSource}&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;x:<span class="xaml__attr_name">Name</span>=<span class="xaml__attr_value">&quot;checkedImage&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">Aspect</span>=<span class="xaml__attr_value">&quot;AspectFit&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;AbsoluteLayout.<span class="xaml__attr_name">LayoutBounds</span>=<span class="xaml__attr_value">&quot;0.5,&nbsp;0.5,&nbsp;1,&nbsp;1&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;AbsoluteLayout.<span class="xaml__attr_name">LayoutFlags</span>=<span class="xaml__attr_value">&quot;All&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">Opacity</span>=<span class="xaml__attr_value">&quot;0&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">InputTransparent</span>=<span class="xaml__attr_value">&quot;True&quot;</span><span class="xaml__tag_start">/&gt;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_end">&lt;/AbsoluteLayout&gt;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;Label</span>&nbsp;x:<span class="xaml__attr_name">Name</span>=<span class="xaml__attr_value">&quot;controlLabel&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">HorizontalOptions</span>=<span class="xaml__attr_value">&quot;FillAndExpand&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">VerticalOptions</span>=<span class="xaml__attr_value">&quot;FillAndExpand&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">HorizontalTextAlignment</span>=<span class="xaml__attr_value">&quot;Start&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">VerticalTextAlignment</span>=<span class="xaml__attr_value">&quot;Center&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">Text</span>=<span class="xaml__attr_value">&quot;{Binding&nbsp;Title}&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">Style</span>=<span class="xaml__attr_value">&quot;{Binding&nbsp;LabelStyle}&quot;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__attr_name">InputTransparent</span>=<span class="xaml__attr_value">&quot;True&quot;</span><span class="xaml__tag_start">/&gt;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_end">&lt;/StackLayout&gt;</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/ContentView.Content&gt;&nbsp;&nbsp;&nbsp;
<span class="xaml__tag_end">&lt;/ContentView&gt;</span>&nbsp;&nbsp;</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<strong style="font-family:Verdana,Arial,Helvetica,sans-serif">Step 3: Add properties to&nbsp;</strong><span style="font-family:verdana,sans-serif">RadioButton</span><span style="font-family:Verdana,Arial,Helvetica,sans-serif">
</span><span style="font-family:verdana,sans-serif">We are going to add below&nbsp;</span><span style="font-family:verdana,sans-serif">Bindable&nbsp;</span><span style="font-family:verdana,sans-serif">properties for our RadioButton&nbsp;control.</span></div>
</span></span>
<ul>
<li><span style="font-size:small"><strong>TitleProperty:</strong><span style="font-family:verdana,sans-serif">&nbsp;To bind tile of check box.</span></span>
</li><li><span style="color:#222222; font-size:small"><span style="font-family:verdana,sans-serif"><strong>LabelStyleProperty:&nbsp;</strong>To to set style to Title label.</span></span>
</li><li><span style="font-family:verdana,sans-serif; font-size:small"><span style="color:#222222"><strong>IsCheckedProperty:&nbsp;</strong>To maintain RadioButton&nbsp;states for check or uncheck.</span></span>
</li><li><span style="color:#222222; font-size:small"><span style="font-family:verdana,sans-serif"><strong>BorderImageSourceProperty:&nbsp;</strong>To set Border image for RadioButton.</span></span>
</li><li><span style="font-size:small"><span style="color:#222222"><span style="font-family:verdana,sans-serif"><strong>CheckedBackgroundImageSourceProperty</strong>:</span></span><span style="color:#222222; font-family:verdana,sans-serif">To set Background image
 for RadioButton.</span></span> </li><li><span style="font-size:small"><span style="color:#222222"><strong><span style="font-family:verdana,sans-serif">CheckMarkImageSourceProperty:&nbsp;</span></strong></span><span style="color:#222222; font-family:verdana,sans-serif">To set CheckMark image for
 RadioButton.</span></span> </li><li><span style="font-size:small"><span style="color:#222222"><span style="font-family:verdana,sans-serif"><strong>CheckedChangedCommandProperty:&nbsp;</strong></span></span><strong>&nbsp;</strong><span style="color:#222222; font-family:verdana,sans-serif">To
 make interaction with RadioButton&nbsp;when user tap on it's main container.</span></span>
</li></ul>
<span style="font-size:small"><span style="font-family:verdana,sans-serif">Also we are applying animation while check box taking up check mark and hiding it.</span>
<span style="font-family:verdana,sans-serif"><br>
</span><span style="font-family:verdana,sans-serif">Now open</span><span style="font-family:verdana,sans-serif">&nbsp;RadioButton.Xaml.cs and add below code.
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">using System;  
using System.Collections.Generic;  
using Xamarin.Forms;  
using Xamarin.Forms.Xaml;  
  
namespace RadioButtonSample.Controls  
{  
    /// &lt;summary&gt;  
 /// Custom RadioButton control  
 /// &lt;/summary&gt;  
    [XamlCompilation(XamlCompilationOptions.Compile)]  
  
    public partial class RadioButton : ContentView  
    {  
        public RadioButton()  
        {  
            InitializeComponent();  
            controlLabel.BindingContext = this;  
            checkedBackground.BindingContext = this;  
            checkedImage.BindingContext = this;  
            borderImage.BindingContext = this;  
            mainContainer.GestureRecognizers.Add(new TapGestureRecognizer()  
            {  
                Command = new Command(tapped)  
            });  
        }  
  
        public static readonly BindableProperty BorderImageSourceProperty = BindableProperty.Create(nameof(BorderImageSource), typeof(string), typeof(RadioButton), &quot;&quot;, BindingMode.OneWay);  
        public static readonly BindableProperty CheckedBackgroundImageSourceProperty = BindableProperty.Create(nameof(CheckedBackgroundImageSource), typeof(string), typeof(RadioButton), &quot;&quot;, BindingMode.OneWay);  
        public static readonly BindableProperty CheckmarkImageSourceProperty = BindableProperty.Create(nameof(CheckmarkImageSource), typeof(string), typeof(RadioButton), &quot;&quot;, BindingMode.OneWay);  
        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(RadioButton), false, BindingMode.TwoWay, propertyChanged: checkedPropertyChanged);  
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(RadioButton), &quot;&quot;, BindingMode.OneWay);  
        public static readonly BindableProperty CheckedChangedCommandProperty = BindableProperty.Create(nameof(CheckedChangedCommand), typeof(Command), typeof(RadioButton), null, BindingMode.OneWay);  
        public static readonly BindableProperty LabelStyleProperty = BindableProperty.Create(nameof(LabelStyle), typeof(Style), typeof(RadioButton), null, BindingMode.OneWay);  
  
        public string BorderImageSource  
        {  
            get { return (string)GetValue(BorderImageSourceProperty); }  
            set { SetValue(BorderImageSourceProperty, value); }  
        }  
  
        public string CheckedBackgroundImageSource  
        {  
            get { return (string)GetValue(CheckedBackgroundImageSourceProperty); }  
            set { SetValue(CheckedBackgroundImageSourceProperty, value); }  
        }  
  
        public string CheckmarkImageSource  
        {  
            get { return (string)GetValue(CheckmarkImageSourceProperty); }  
            set { SetValue(CheckmarkImageSourceProperty, value); }  
        }  
  
        public bool IsChecked  
        {  
            get { return (bool)GetValue(IsCheckedProperty); }  
            set { SetValue(IsCheckedProperty, value); }  
        }  
  
        public string Title  
        {  
            get { return (string)GetValue(TitleProperty); }  
            set { SetValue(TitleProperty, value); }  
        }  
  
        public Command CheckedChangedCommand  
        {  
            get { return (Command)GetValue(CheckedChangedCommandProperty); }  
            set { SetValue(CheckedChangedCommandProperty, value); }  
        }  
  
        public Style LabelStyle  
        {  
            get { return (Style)GetValue(LabelStyleProperty); }  
            set { SetValue(LabelStyleProperty, value); }  
        }  
  
        public Label ControlLabel  
        {  
            get { return controlLabel; }  
        }  
  
        static void checkedPropertyChanged(BindableObject bindable, object oldValue, object newValue)  
        {  
            ((RadioButton)bindable).ApplyCheckedState();  
        }  
  
        void tapped()  
        {  
            if (!IsChecked)  
                IsChecked = true;  
            setCheckedState(IsChecked);  
            if (CheckedChangedCommand != null &amp;&amp; CheckedChangedCommand.CanExecute(this))  
                CheckedChangedCommand.Execute(this);  
        }  
  
        /// &lt;summary&gt;  
        /// Reflect the checked event change on the UI  
        /// with a small animation  
        /// &lt;/summary&gt;  
        /// &lt;param name=&quot;isChecked&quot;&gt;&lt;/param&gt;  
        void setCheckedState(bool isChecked)  
        {  
            Animation storyboard = new Animation();  
            Animation fadeAnim = null;  
            Animation checkBounceAnim = null;  
            Animation checkFadeAnim = null;  
            double fadeStartVal = 0;  
            double fadeEndVal = 1;  
            double scaleStartVal = 0;  
            double scaleEndVal = 1;  
            Easing checkEasing = Easing.CubicIn;  
  
            if (isChecked)  
            {  
                checkedImage.Scale = 0;  
                fadeStartVal = 0;  
                fadeEndVal = 1;  
                scaleStartVal = 0;  
                scaleEndVal = 1;  
                checkEasing = Easing.CubicIn;  
            }  
            else  
            {  
                fadeStartVal = 1;  
                fadeEndVal = 0;  
                scaleStartVal = 1;  
                scaleEndVal = 0;  
                checkEasing = Easing.CubicOut;  
            }  
            fadeAnim = new Animation(  
                    callback: d =&gt; checkedBackground.Opacity = d,  
                    start: fadeStartVal,  
                    end: fadeEndVal,  
                    easing: Easing.CubicOut  
                    );  
            checkFadeAnim = new Animation(  
                callback: d =&gt; checkedImage.Opacity = d,  
                start: fadeStartVal,  
                end: fadeEndVal,  
                easing: checkEasing  
                );  
            checkBounceAnim = new Animation(  
                callback: d =&gt; checkedImage.Scale = d,  
                start: scaleStartVal,  
                end: scaleEndVal,  
                easing: checkEasing  
                );  
  
            storyboard.Add(0, 0.6, fadeAnim);  
            storyboard.Add(0, 0.6, checkFadeAnim);  
            storyboard.Add(0.4, 1, checkBounceAnim);  
            storyboard.Commit(this, &quot;checkAnimation&quot;, length: 600);  
        }  
  
        public void ApplyCheckedState()  
        {  
            setCheckedState(IsChecked);  
        }  
    }  
}  </pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">using</span>&nbsp;System;&nbsp;&nbsp;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Collections.Generic;&nbsp;&nbsp;&nbsp;
<span class="cs__keyword">using</span>&nbsp;Xamarin.Forms;&nbsp;&nbsp;&nbsp;
<span class="cs__keyword">using</span>&nbsp;Xamarin.Forms.Xaml;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="cs__keyword">namespace</span>&nbsp;RadioButtonSample.Controls&nbsp;&nbsp;&nbsp;
{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;&nbsp;&nbsp;</span>&nbsp;
&nbsp;<span class="cs__com">///&nbsp;Custom&nbsp;RadioButton&nbsp;control&nbsp;&nbsp;</span>&nbsp;
&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[XamlCompilation(XamlCompilationOptions.Compile)]&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;partial&nbsp;<span class="cs__keyword">class</span>&nbsp;RadioButton&nbsp;:&nbsp;ContentView&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;RadioButton()&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;InitializeComponent();&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;controlLabel.BindingContext&nbsp;=&nbsp;<span class="cs__keyword">this</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;checkedBackground.BindingContext&nbsp;=&nbsp;<span class="cs__keyword">this</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;checkedImage.BindingContext&nbsp;=&nbsp;<span class="cs__keyword">this</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;borderImage.BindingContext&nbsp;=&nbsp;<span class="cs__keyword">this</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;mainContainer.GestureRecognizers.Add(<span class="cs__keyword">new</span>&nbsp;TapGestureRecognizer()&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Command&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Command(tapped)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;});&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">readonly</span>&nbsp;BindableProperty&nbsp;BorderImageSourceProperty&nbsp;=&nbsp;BindableProperty.Create(nameof(BorderImageSource),&nbsp;<span class="cs__keyword">typeof</span>(<span class="cs__keyword">string</span>),&nbsp;<span class="cs__keyword">typeof</span>(RadioButton),&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;BindingMode.OneWay);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">readonly</span>&nbsp;BindableProperty&nbsp;CheckedBackgroundImageSourceProperty&nbsp;=&nbsp;BindableProperty.Create(nameof(CheckedBackgroundImageSource),&nbsp;<span class="cs__keyword">typeof</span>(<span class="cs__keyword">string</span>),&nbsp;<span class="cs__keyword">typeof</span>(RadioButton),&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;BindingMode.OneWay);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">readonly</span>&nbsp;BindableProperty&nbsp;CheckmarkImageSourceProperty&nbsp;=&nbsp;BindableProperty.Create(nameof(CheckmarkImageSource),&nbsp;<span class="cs__keyword">typeof</span>(<span class="cs__keyword">string</span>),&nbsp;<span class="cs__keyword">typeof</span>(RadioButton),&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;BindingMode.OneWay);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">readonly</span>&nbsp;BindableProperty&nbsp;IsCheckedProperty&nbsp;=&nbsp;BindableProperty.Create(nameof(IsChecked),&nbsp;<span class="cs__keyword">typeof</span>(<span class="cs__keyword">bool</span>),&nbsp;<span class="cs__keyword">typeof</span>(RadioButton),&nbsp;<span class="cs__keyword">false</span>,&nbsp;BindingMode.TwoWay,&nbsp;propertyChanged:&nbsp;checkedPropertyChanged);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">readonly</span>&nbsp;BindableProperty&nbsp;TitleProperty&nbsp;=&nbsp;BindableProperty.Create(nameof(Title),&nbsp;<span class="cs__keyword">typeof</span>(<span class="cs__keyword">string</span>),&nbsp;<span class="cs__keyword">typeof</span>(RadioButton),&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;BindingMode.OneWay);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">readonly</span>&nbsp;BindableProperty&nbsp;CheckedChangedCommandProperty&nbsp;=&nbsp;BindableProperty.Create(nameof(CheckedChangedCommand),&nbsp;<span class="cs__keyword">typeof</span>(Command),&nbsp;<span class="cs__keyword">typeof</span>(RadioButton),&nbsp;<span class="cs__keyword">null</span>,&nbsp;BindingMode.OneWay);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">readonly</span>&nbsp;BindableProperty&nbsp;LabelStyleProperty&nbsp;=&nbsp;BindableProperty.Create(nameof(LabelStyle),&nbsp;<span class="cs__keyword">typeof</span>(Style),&nbsp;<span class="cs__keyword">typeof</span>(RadioButton),&nbsp;<span class="cs__keyword">null</span>,&nbsp;BindingMode.OneWay);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;BorderImageSource&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">get</span>&nbsp;{&nbsp;<span class="cs__keyword">return</span>&nbsp;(<span class="cs__keyword">string</span>)GetValue(BorderImageSourceProperty);&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">set</span>&nbsp;{&nbsp;SetValue(BorderImageSourceProperty,&nbsp;<span class="cs__keyword">value</span>);&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;CheckedBackgroundImageSource&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">get</span>&nbsp;{&nbsp;<span class="cs__keyword">return</span>&nbsp;(<span class="cs__keyword">string</span>)GetValue(CheckedBackgroundImageSourceProperty);&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">set</span>&nbsp;{&nbsp;SetValue(CheckedBackgroundImageSourceProperty,&nbsp;<span class="cs__keyword">value</span>);&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;CheckmarkImageSource&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">get</span>&nbsp;{&nbsp;<span class="cs__keyword">return</span>&nbsp;(<span class="cs__keyword">string</span>)GetValue(CheckmarkImageSourceProperty);&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">set</span>&nbsp;{&nbsp;SetValue(CheckmarkImageSourceProperty,&nbsp;<span class="cs__keyword">value</span>);&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">bool</span>&nbsp;IsChecked&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">get</span>&nbsp;{&nbsp;<span class="cs__keyword">return</span>&nbsp;(<span class="cs__keyword">bool</span>)GetValue(IsCheckedProperty);&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">set</span>&nbsp;{&nbsp;SetValue(IsCheckedProperty,&nbsp;<span class="cs__keyword">value</span>);&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;Title&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">get</span>&nbsp;{&nbsp;<span class="cs__keyword">return</span>&nbsp;(<span class="cs__keyword">string</span>)GetValue(TitleProperty);&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">set</span>&nbsp;{&nbsp;SetValue(TitleProperty,&nbsp;<span class="cs__keyword">value</span>);&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;Command&nbsp;CheckedChangedCommand&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">get</span>&nbsp;{&nbsp;<span class="cs__keyword">return</span>&nbsp;(Command)GetValue(CheckedChangedCommandProperty);&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">set</span>&nbsp;{&nbsp;SetValue(CheckedChangedCommandProperty,&nbsp;<span class="cs__keyword">value</span>);&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;Style&nbsp;LabelStyle&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">get</span>&nbsp;{&nbsp;<span class="cs__keyword">return</span>&nbsp;(Style)GetValue(LabelStyleProperty);&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">set</span>&nbsp;{&nbsp;SetValue(LabelStyleProperty,&nbsp;<span class="cs__keyword">value</span>);&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;Label&nbsp;ControlLabel&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">get</span>&nbsp;{&nbsp;<span class="cs__keyword">return</span>&nbsp;controlLabel;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;checkedPropertyChanged(BindableObject&nbsp;bindable,&nbsp;<span class="cs__keyword">object</span>&nbsp;oldValue,&nbsp;<span class="cs__keyword">object</span>&nbsp;newValue)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;((RadioButton)bindable).ApplyCheckedState();&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">void</span>&nbsp;tapped()&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(!IsChecked)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IsChecked&nbsp;=&nbsp;<span class="cs__keyword">true</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;setCheckedState(IsChecked);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(CheckedChangedCommand&nbsp;!=&nbsp;<span class="cs__keyword">null</span>&nbsp;&amp;&amp;&nbsp;CheckedChangedCommand.CanExecute(<span class="cs__keyword">this</span>))&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CheckedChangedCommand.Execute(<span class="cs__keyword">this</span>);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;Reflect&nbsp;the&nbsp;checked&nbsp;event&nbsp;change&nbsp;on&nbsp;the&nbsp;UI&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;with&nbsp;a&nbsp;small&nbsp;animation&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;param&nbsp;name=&quot;isChecked&quot;&gt;&lt;/param&gt;&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">void</span>&nbsp;setCheckedState(<span class="cs__keyword">bool</span>&nbsp;isChecked)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Animation&nbsp;storyboard&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Animation();&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Animation&nbsp;fadeAnim&nbsp;=&nbsp;<span class="cs__keyword">null</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Animation&nbsp;checkBounceAnim&nbsp;=&nbsp;<span class="cs__keyword">null</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Animation&nbsp;checkFadeAnim&nbsp;=&nbsp;<span class="cs__keyword">null</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">double</span>&nbsp;fadeStartVal&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">double</span>&nbsp;fadeEndVal&nbsp;=&nbsp;<span class="cs__number">1</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">double</span>&nbsp;scaleStartVal&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">double</span>&nbsp;scaleEndVal&nbsp;=&nbsp;<span class="cs__number">1</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Easing&nbsp;checkEasing&nbsp;=&nbsp;Easing.CubicIn;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(isChecked)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;checkedImage.Scale&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;fadeStartVal&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;fadeEndVal&nbsp;=&nbsp;<span class="cs__number">1</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;scaleStartVal&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;scaleEndVal&nbsp;=&nbsp;<span class="cs__number">1</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;checkEasing&nbsp;=&nbsp;Easing.CubicIn;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;fadeStartVal&nbsp;=&nbsp;<span class="cs__number">1</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;fadeEndVal&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;scaleStartVal&nbsp;=&nbsp;<span class="cs__number">1</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;scaleEndVal&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;checkEasing&nbsp;=&nbsp;Easing.CubicOut;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;fadeAnim&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Animation(&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;callback:&nbsp;d&nbsp;=&gt;&nbsp;checkedBackground.Opacity&nbsp;=&nbsp;d,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;start:&nbsp;fadeStartVal,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;end:&nbsp;fadeEndVal,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;easing:&nbsp;Easing.CubicOut&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;checkFadeAnim&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Animation(&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;callback:&nbsp;d&nbsp;=&gt;&nbsp;checkedImage.Opacity&nbsp;=&nbsp;d,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;start:&nbsp;fadeStartVal,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;end:&nbsp;fadeEndVal,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;easing:&nbsp;checkEasing&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;checkBounceAnim&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Animation(&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;callback:&nbsp;d&nbsp;=&gt;&nbsp;checkedImage.Scale&nbsp;=&nbsp;d,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;start:&nbsp;scaleStartVal,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;end:&nbsp;scaleEndVal,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;easing:&nbsp;checkEasing&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;storyboard.Add(<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0.6</span>,&nbsp;fadeAnim);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;storyboard.Add(<span class="cs__number">0</span>,&nbsp;<span class="cs__number">0.6</span>,&nbsp;checkFadeAnim);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;storyboard.Add(<span class="cs__number">0.4</span>,&nbsp;<span class="cs__number">1</span>,&nbsp;checkBounceAnim);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;storyboard.Commit(<span class="cs__keyword">this</span>,&nbsp;<span class="cs__string">&quot;checkAnimation&quot;</span>,&nbsp;length:&nbsp;<span class="cs__number">600</span>);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;ApplyCheckedState()&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;setCheckedState(IsChecked);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;
}&nbsp;&nbsp;</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;<span style="font-family:verdana,sans-serif">We complete creation of RadioButton&nbsp;and it will also support data binding. So we can dynamically supply all properties of RadioButton.</span><span style="font-family:Verdana,Arial,Helvetica,sans-serif">&nbsp;</span></div>
</span></span><span style="font-size:small"><strong><span style="font-family:verdana,sans-serif">4.&nbsp;</span><span style="font-family:verdana,sans-serif">How to use custom&nbsp;</span></strong><span style="font-family:verdana,sans-serif"><strong>RadioButton</strong></span><strong><span style="font-family:verdana,sans-serif">&nbsp;in
 Xamarin.Forms?</span></strong> </span></div>
<div>
<div><span style="font-size:small"><span style="font-family:verdana,sans-serif"><span style="font-family:verdana,sans-serif">&nbsp;</span></span>
<span style="font-family:verdana,sans-serif">Now we are ready to our own RadioButton&nbsp;control. Before to use it, let's create one ContentPage in Views folder.</span>
<span style="font-family:verdana,sans-serif"><span style="font-family:verdana,sans-serif">&nbsp;</span></span>
<span style="font-family:verdana,sans-serif">To create page, right click on Views folder =&gt; Add =&gt;New File =&gt; Forms =&gt; Forms ContentPage Xaml and name it HomePage like below.</span>
<span style="font-family:verdana,sans-serif"><span style="font-family:verdana,sans-serif">&nbsp;</span></span>
<span style="font-family:verdana,sans-serif"><span style="font-family:verdana,sans-serif"><a href="https://4.bp.blogspot.com/-W7g7bcypS9Y/WqODBnCUfPI/AAAAAAAADd4/1-bCiiBFxq4nARkDMwReTKFmQ1ry2k-iwCLcBGAs/s1600/4.AddContentPage.png"><img src=":-4.addcontentpage.png" border="0" alt="" width="640" height="488"></a></span></span>
</span><span style="font-family:verdana,sans-serif"><span style="font-size:small"><span style="font-family:verdana,sans-serif">To use RadioButton&nbsp;in our xaml page. First we need declare namespace of RadioButton.
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XAML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xaml</span>
<pre class="hidden">xmlns:ctrls=&quot;clr-namespace:RadioButtonSample.Controls&quot;  </pre>
<div class="preview">
<pre class="xaml">xmlns:ctrls=&quot;clr-namespace:RadioButtonSample.Controls&quot;&nbsp;&nbsp;</pre>
</div>
</div>
</div>
<div>And then we can use RadioButton&nbsp;like below
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XAML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xaml</span>
<pre class="hidden">&lt;ctrls:RadioButton x:Name=&quot;cbIndia&quot; Title=&quot;India&quot; IsChecked=&quot;True&quot; BorderImageSource=&quot;radioborder&quot; CheckedBackgroundImageSource=&quot;radiocheckedbg&quot; CheckmarkImageSource=&quot;radiocheckmark&quot; /&gt;  </pre>
<div class="preview">
<pre class="xaml"><span class="xaml__tag_start">&lt;ctrls</span>:RadioButton&nbsp;x:<span class="xaml__attr_name">Name</span>=<span class="xaml__attr_value">&quot;cbIndia&quot;</span>&nbsp;<span class="xaml__attr_name">Title</span>=<span class="xaml__attr_value">&quot;India&quot;</span>&nbsp;<span class="xaml__attr_name">IsChecked</span>=<span class="xaml__attr_value">&quot;True&quot;</span>&nbsp;<span class="xaml__attr_name">BorderImageSource</span>=<span class="xaml__attr_value">&quot;radioborder&quot;</span>&nbsp;<span class="xaml__attr_name">CheckedBackgroundImageSource</span>=<span class="xaml__attr_value">&quot;radiocheckedbg&quot;</span>&nbsp;<span class="xaml__attr_name">CheckmarkImageSource</span>=<span class="xaml__attr_value">&quot;radiocheckmark&quot;</span>&nbsp;<span class="xaml__tag_start">/&gt;</span>&nbsp;&nbsp;</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;Here we set the values for the RadioButton&nbsp;properties are&nbsp;
<strong>Title</strong>: India <strong>IsChecked</strong>: True <strong>BorderImageSource</strong>:&nbsp;radioborder.png
<strong>CheckBackgroundImageSource</strong>: radiocheckedbg.png <strong>CheckmarkImageSource</strong>: radiocheckmark.png &nbsp; We should add all above images to&nbsp;<strong>Android drawable</strong>&nbsp;folder and for&nbsp;<strong>iOS Resources</strong>&nbsp;folder.
 &nbsp; Now open HomePage.xaml file, add below total source code.
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>XAML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">xaml</span>
<pre class="hidden">&lt;?xml version=&quot;1.0&quot; encoding=&quot;UTF-8&quot;?&gt;    
&lt;ContentPage xmlns=&quot;http://xamarin.com/schemas/2014/forms&quot;     
    xmlns:x=&quot;http://schemas.microsoft.com/winfx/2009/xaml&quot;    
    x:Class=&quot;RadioButtonSample.Views.HomePage&quot;    
    xmlns:ctrls=&quot;clr-namespace:RadioButtonSample.Controls&quot; &gt;    
    &lt;StackLayout Margin=&quot;20&quot; x:Name=&quot;stackPanel&quot;&gt;    
        &lt;Label Text=&quot;Select Country&quot; FontSize=&quot;25&quot; FontAttributes=&quot;Bold&quot; /&gt;    
        &lt;ctrls:RadioButton x:Name=&quot;cbIndia&quot; Title=&quot;India&quot; IsChecked=&quot;True&quot; BorderImageSource=&quot;radioborder&quot; CheckedBackgroundImageSource=&quot;radiocheckedbg&quot; CheckmarkImageSource=&quot;radiocheckmark&quot; /&gt;    
    &lt;/StackLayout&gt;    
&lt;/ContentPage&gt;    </pre>
<div class="preview">
<pre class="xaml"><span class="xaml__tag_start">&lt;?xml</span>&nbsp;<span class="xaml__attr_name">version</span>=<span class="xaml__attr_value">&quot;1.0&quot;</span>&nbsp;<span class="xaml__attr_name">encoding</span>=<span class="xaml__attr_value">&quot;UTF-8&quot;</span><span class="xaml__tag_start">?&gt;</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<span class="xaml__tag_start">&lt;ContentPage</span>&nbsp;<span class="xaml__attr_name">xmlns</span>=<span class="xaml__attr_value">&quot;http://xamarin.com/schemas/2014/forms&quot;</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__keyword">xmlns</span>:<span class="xaml__attr_name">x</span>=<span class="xaml__attr_value">&quot;http://schemas.microsoft.com/winfx/2009/xaml&quot;</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;x:<span class="xaml__attr_name">Class</span>=<span class="xaml__attr_value">&quot;RadioButtonSample.Views.HomePage&quot;</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__keyword">xmlns</span>:<span class="xaml__attr_name">ctrls</span>=<span class="xaml__attr_value">&quot;clr-namespace:RadioButtonSample.Controls&quot;</span>&nbsp;<span class="xaml__tag_start">&gt;&nbsp;</span>&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;StackLayout</span>&nbsp;<span class="xaml__attr_name">Margin</span>=<span class="xaml__attr_value">&quot;20&quot;</span>&nbsp;x:<span class="xaml__attr_name">Name</span>=<span class="xaml__attr_value">&quot;stackPanel&quot;</span><span class="xaml__tag_start">&gt;&nbsp;</span>&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;Label</span>&nbsp;<span class="xaml__attr_name">Text</span>=<span class="xaml__attr_value">&quot;Select&nbsp;Country&quot;</span>&nbsp;<span class="xaml__attr_name">FontSize</span>=<span class="xaml__attr_value">&quot;25&quot;</span>&nbsp;<span class="xaml__attr_name">FontAttributes</span>=<span class="xaml__attr_value">&quot;Bold&quot;</span>&nbsp;<span class="xaml__tag_start">/&gt;</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_start">&lt;ctrls</span>:RadioButton&nbsp;x:<span class="xaml__attr_name">Name</span>=<span class="xaml__attr_value">&quot;cbIndia&quot;</span>&nbsp;<span class="xaml__attr_name">Title</span>=<span class="xaml__attr_value">&quot;India&quot;</span>&nbsp;<span class="xaml__attr_name">IsChecked</span>=<span class="xaml__attr_value">&quot;True&quot;</span>&nbsp;<span class="xaml__attr_name">BorderImageSource</span>=<span class="xaml__attr_value">&quot;radioborder&quot;</span>&nbsp;<span class="xaml__attr_name">CheckedBackgroundImageSource</span>=<span class="xaml__attr_value">&quot;radiocheckedbg&quot;</span>&nbsp;<span class="xaml__attr_name">CheckmarkImageSource</span>=<span class="xaml__attr_value">&quot;radiocheckmark&quot;</span>&nbsp;<span class="xaml__tag_start">/&gt;</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="xaml__tag_end">&lt;/StackLayout&gt;</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<span class="xaml__tag_end">&lt;/ContentPage&gt;</span>&nbsp;&nbsp;&nbsp;&nbsp;</pre>
</div>
</div>
</div>
</div>
</div>
For example, if you want to use RadioButton&nbsp;from code behind.
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">void CreateRadioButton()  
       {  
           RadioButton radioButton = new RadioButton();  
           radioButton.IsChecked = false;  
           radioButton.IsVisible = true;  
           radioButton.Title = &quot;Japan&quot;;  
           radioButton.BorderImageSource = &quot;radioborder&quot;;  
           radioButton.CheckedBackgroundImageSource = &quot;radiocheckedbg&quot;;  
           radioButton.CheckmarkImageSource = &quot;radiocheckmark&quot;;  
           stackPanel.Children.Add(radioButton);  
       }  </pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">void</span>&nbsp;CreateRadioButton()&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;RadioButton&nbsp;radioButton&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;RadioButton();&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;radioButton.IsChecked&nbsp;=&nbsp;<span class="cs__keyword">false</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;radioButton.IsVisible&nbsp;=&nbsp;<span class="cs__keyword">true</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;radioButton.Title&nbsp;=&nbsp;<span class="cs__string">&quot;Japan&quot;</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;radioButton.BorderImageSource&nbsp;=&nbsp;<span class="cs__string">&quot;radioborder&quot;</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;radioButton.CheckedBackgroundImageSource&nbsp;=&nbsp;<span class="cs__string">&quot;radiocheckedbg&quot;</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;radioButton.CheckmarkImageSource&nbsp;=&nbsp;<span class="cs__string">&quot;radiocheckmark&quot;</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;stackPanel.Children.Add(radioButton);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;</pre>
</div>
</div>
</div>
<div class="endscriptcode">For example, if you want to bind RadioButton&nbsp;from code behind.
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">void RadioButtonBinding()  
       {  
           Country country = new Country();  
           country.Name = &quot;Singapore&quot;;  
           country.IsSelected = false;  
           country.IsVisible = true;  
  
           RadioButton radioButton = new RadioButton();  
           radioButton.BindingContext = country;  
           radioButton.SetBinding(RadioButton.IsCheckedProperty, &quot;IsSelected&quot;, BindingMode.TwoWay);  
           radioButton.SetBinding(RadioButton.IsVisibleProperty, &quot;IsVisible&quot;);  
           radioButton.SetBinding(RadioButton.TitleProperty, &quot;Name&quot;);  
           radioButton.BorderImageSource = &quot;radioborder&quot;;  
           radioButton.CheckedBackgroundImageSource = &quot;radiocheckedbg&quot;;  
           radioButton.CheckmarkImageSource = &quot;radiocheckmark&quot;;  
           stackPanel.Children.Add(radioButton);  
       }  </pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">void</span>&nbsp;RadioButtonBinding()&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Country&nbsp;country&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Country();&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;country.Name&nbsp;=&nbsp;<span class="cs__string">&quot;Singapore&quot;</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;country.IsSelected&nbsp;=&nbsp;<span class="cs__keyword">false</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;country.IsVisible&nbsp;=&nbsp;<span class="cs__keyword">true</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;RadioButton&nbsp;radioButton&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;RadioButton();&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;radioButton.BindingContext&nbsp;=&nbsp;country;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;radioButton.SetBinding(RadioButton.IsCheckedProperty,&nbsp;<span class="cs__string">&quot;IsSelected&quot;</span>,&nbsp;BindingMode.TwoWay);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;radioButton.SetBinding(RadioButton.IsVisibleProperty,&nbsp;<span class="cs__string">&quot;IsVisible&quot;</span>);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;radioButton.SetBinding(RadioButton.TitleProperty,&nbsp;<span class="cs__string">&quot;Name&quot;</span>);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;radioButton.BorderImageSource&nbsp;=&nbsp;<span class="cs__string">&quot;radioborder&quot;</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;radioButton.CheckedBackgroundImageSource&nbsp;=&nbsp;<span class="cs__string">&quot;radiocheckedbg&quot;</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;radioButton.CheckmarkImageSource&nbsp;=&nbsp;<span class="cs__string">&quot;radiocheckmark&quot;</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;stackPanel.Children.Add(radioButton);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;In above code, we bind the RadioButton&nbsp;with Country class object properties (Name, IsSelected, IsVisibile). So let's create Country Class in Model folder. To create class, right click on Models folder =&gt; Add =&gt;New
 File =&gt; General =&gt; Empty Class and name it Country like below. <a href="https://3.bp.blogspot.com/-NVw_aRkLeHg/WqOGzbHL_TI/AAAAAAAADeE/fbKhuRn9W4kN8i9uUEIjLwHPuxupoqdjgCLcBGAs/s1600/Country.png">
<img src=":-country.png" border="0" alt="" width="640" height="490"></a> Now open Country class and add below properties.
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">using System;  
using System.Collections.Generic;  
using System.ComponentModel;  
using System.Runtime.CompilerServices;  
  
namespace RadioButtonSample  
{  
    public class Country: INotifyPropertyChanged  
    {  
        public string Name { get; set; }  
  
        bool isVisible;  
        public bool IsVisible  
        {  
            get { return isVisible; }  
            set { SetProperty(ref isVisible, value); }  
        }  
  
        bool isSelected;  
        public bool IsSelected  
        {  
            get { return isSelected; }  
            set { SetProperty(ref isSelected, value); }  
        }  
        protected bool SetProperty&lt;T&gt;(ref T backingStore, T value,  
            [CallerMemberName]string propertyName = &quot;&quot;,  
            Action onChanged = null)  
        {  
            if (EqualityComparer&lt;T&gt;.Default.Equals(backingStore, value))  
                return false;  
  
            backingStore = value;  
            onChanged?.Invoke();  
            OnPropertyChanged(propertyName);  
            return true;  
        }  
  
        #region INotifyPropertyChanged  
        public event PropertyChangedEventHandler PropertyChanged;  
        protected void OnPropertyChanged([CallerMemberName] string propertyName = &quot;&quot;)  
        {  
            var changed = PropertyChanged;  
            if (changed == null)  
                return;  
  
            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));  
        }  
        #endregion  
    }  
}  </pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">using</span>&nbsp;System;&nbsp;&nbsp;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Collections.Generic;&nbsp;&nbsp;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.ComponentModel;&nbsp;&nbsp;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Runtime.CompilerServices;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="cs__keyword">namespace</span>&nbsp;RadioButtonSample&nbsp;&nbsp;&nbsp;
{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;Country:&nbsp;INotifyPropertyChanged&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;Name&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">bool</span>&nbsp;isVisible;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">bool</span>&nbsp;IsVisible&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">get</span>&nbsp;{&nbsp;<span class="cs__keyword">return</span>&nbsp;isVisible;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">set</span>&nbsp;{&nbsp;SetProperty(<span class="cs__keyword">ref</span>&nbsp;isVisible,&nbsp;<span class="cs__keyword">value</span>);&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">bool</span>&nbsp;isSelected;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">bool</span>&nbsp;IsSelected&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">get</span>&nbsp;{&nbsp;<span class="cs__keyword">return</span>&nbsp;isSelected;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">set</span>&nbsp;{&nbsp;SetProperty(<span class="cs__keyword">ref</span>&nbsp;isSelected,&nbsp;<span class="cs__keyword">value</span>);&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">protected</span>&nbsp;<span class="cs__keyword">bool</span>&nbsp;SetProperty&lt;T&gt;(<span class="cs__keyword">ref</span>&nbsp;T&nbsp;backingStore,&nbsp;T&nbsp;<span class="cs__keyword">value</span>,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[CallerMemberName]<span class="cs__keyword">string</span>&nbsp;propertyName&nbsp;=&nbsp;<span class="cs__string">&quot;&quot;</span>,&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Action&nbsp;onChanged&nbsp;=&nbsp;<span class="cs__keyword">null</span>)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(EqualityComparer&lt;T&gt;.Default.Equals(backingStore,&nbsp;<span class="cs__keyword">value</span>))&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">false</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;backingStore&nbsp;=&nbsp;<span class="cs__keyword">value</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;onChanged?.Invoke();&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;OnPropertyChanged(propertyName);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">true</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;<span class="cs__preproc">&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;#region&nbsp;INotifyPropertyChanged</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">event</span>&nbsp;PropertyChangedEventHandler&nbsp;PropertyChanged;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">protected</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;OnPropertyChanged([CallerMemberName]&nbsp;<span class="cs__keyword">string</span>&nbsp;propertyName&nbsp;=&nbsp;<span class="cs__string">&quot;&quot;</span>)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;changed&nbsp;=&nbsp;PropertyChanged;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(changed&nbsp;==&nbsp;<span class="cs__keyword">null</span>)&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;changed.Invoke(<span class="cs__keyword">this</span>,&nbsp;<span class="cs__keyword">new</span>&nbsp;PropertyChangedEventArgs(propertyName));&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;<span class="cs__preproc">&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;#endregion</span>&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;
}&nbsp;&nbsp;</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;Let's see overview of total source code of HopePage.xaml.cs file.
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">using RadioButtonSample.Controls;  
using RadioButtonSample.ViewModels;  
using Xamarin.Forms;  
  
namespace RadioButtonSample.Views  
{  
    public partial class HomePage : ContentPage  
    {  
        HomeViewModel _homeViewModel;  
        public HomePage()  
        {  
            InitializeComponent();   
            CreateRadioButton();  
            RadioButtonBinding();  
        }  
  
        /// &lt;summary&gt;  
        /// Creating RadioButton with assigned values (Bg, border, title, selection)  
        /// &lt;/summary&gt;  
        void CreateRadioButton()  
        {  
            RadioButton radioButton = new RadioButton();  
            radioButton.IsChecked = false;  
            radioButton.IsVisible = true;  
            radioButton.Title = &quot;Japan&quot;;  
            radioButton.BorderImageSource = &quot;radioborder&quot;;  
            radioButton.CheckedBackgroundImageSource = &quot;radiocheckedbg&quot;;  
            radioButton.CheckmarkImageSource = &quot;radiocheckmark&quot;;  
            stackPanel.Children.Add(radioButton);  
        }  
  
        /// &lt;summary&gt;  
        /// RadioButton binding with homeViewModel  
        /// &lt;/summary&gt;  
        void RadioButtonBinding()  
        {  
            Country country = new Country();  
            country.Name = &quot;Singapore&quot;;  
            country.IsSelected = false;  
            country.IsVisible = true;  
  
            RadioButton radioButton = new RadioButton();  
            radioButton.BindingContext = country;  
            radioButton.SetBinding(RadioButton.IsCheckedProperty, &quot;IsSelected&quot;, BindingMode.TwoWay);  
            radioButton.SetBinding(RadioButton.IsVisibleProperty, &quot;IsVisible&quot;);  
            radioButton.SetBinding(RadioButton.TitleProperty, &quot;Name&quot;);  
            radioButton.BorderImageSource = &quot;radioborder&quot;;  
            radioButton.CheckedBackgroundImageSource = &quot;radiocheckedbg&quot;;  
            radioButton.CheckmarkImageSource = &quot;radiocheckmark&quot;;  
            stackPanel.Children.Add(radioButton);  
        }  
    }  
}  </pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">using</span>&nbsp;RadioButtonSample.Controls;&nbsp;&nbsp;&nbsp;
<span class="cs__keyword">using</span>&nbsp;RadioButtonSample.ViewModels;&nbsp;&nbsp;&nbsp;
<span class="cs__keyword">using</span>&nbsp;Xamarin.Forms;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
<span class="cs__keyword">namespace</span>&nbsp;RadioButtonSample.Views&nbsp;&nbsp;&nbsp;
{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;partial&nbsp;<span class="cs__keyword">class</span>&nbsp;HomePage&nbsp;:&nbsp;ContentPage&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;HomeViewModel&nbsp;_homeViewModel;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;HomePage()&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;InitializeComponent();&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CreateRadioButton();&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;RadioButtonBinding();&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;Creating&nbsp;RadioButton&nbsp;with&nbsp;assigned&nbsp;values&nbsp;(Bg,&nbsp;border,&nbsp;title,&nbsp;selection)&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">void</span>&nbsp;CreateRadioButton()&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;RadioButton&nbsp;radioButton&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;RadioButton();&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;radioButton.IsChecked&nbsp;=&nbsp;<span class="cs__keyword">false</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;radioButton.IsVisible&nbsp;=&nbsp;<span class="cs__keyword">true</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;radioButton.Title&nbsp;=&nbsp;<span class="cs__string">&quot;Japan&quot;</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;radioButton.BorderImageSource&nbsp;=&nbsp;<span class="cs__string">&quot;radioborder&quot;</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;radioButton.CheckedBackgroundImageSource&nbsp;=&nbsp;<span class="cs__string">&quot;radiocheckedbg&quot;</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;radioButton.CheckmarkImageSource&nbsp;=&nbsp;<span class="cs__string">&quot;radiocheckmark&quot;</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;stackPanel.Children.Add(radioButton);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;RadioButton&nbsp;binding&nbsp;with&nbsp;homeViewModel&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">void</span>&nbsp;RadioButtonBinding()&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Country&nbsp;country&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Country();&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;country.Name&nbsp;=&nbsp;<span class="cs__string">&quot;Singapore&quot;</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;country.IsSelected&nbsp;=&nbsp;<span class="cs__keyword">false</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;country.IsVisible&nbsp;=&nbsp;<span class="cs__keyword">true</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;RadioButton&nbsp;radioButton&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;RadioButton();&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;radioButton.BindingContext&nbsp;=&nbsp;country;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;radioButton.SetBinding(RadioButton.IsCheckedProperty,&nbsp;<span class="cs__string">&quot;IsSelected&quot;</span>,&nbsp;BindingMode.TwoWay);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;radioButton.SetBinding(RadioButton.IsVisibleProperty,&nbsp;<span class="cs__string">&quot;IsVisible&quot;</span>);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;radioButton.SetBinding(RadioButton.TitleProperty,&nbsp;<span class="cs__string">&quot;Name&quot;</span>);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;radioButton.BorderImageSource&nbsp;=&nbsp;<span class="cs__string">&quot;radioborder&quot;</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;radioButton.CheckedBackgroundImageSource&nbsp;=&nbsp;<span class="cs__string">&quot;radiocheckedbg&quot;</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;radioButton.CheckmarkImageSource&nbsp;=&nbsp;<span class="cs__string">&quot;radiocheckmark&quot;</span>;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;stackPanel.Children.Add(radioButton);&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;
}&nbsp;&nbsp;</pre>
</div>
</div>
</div>
<div class="endscriptcode"><strong>FeedBack Note:</strong>&nbsp;Please share your thoughts, what you think about this post, Is this post really helpful for you? Otherwise, it would be very happy, if you have any thoughts for to implement this requirement
 in any other way? I always welcome if you drop comments on this post and it would be impressive.</div>
</div>
</div>
</div>
</span></span><span style="font-family:verdana,sans-serif"><span style="font-size:small">&nbsp;</span>
<div><span style="font-size:small"><span style="font-family:verdana,sans-serif">Follow me always at&nbsp;<a href="https://twitter.com/Subramanyam_B">@Subramanyam_B</a></span>
</span></div>
<div><span style="font-size:small"><span style="font-family:verdana,sans-serif">Have a nice day by<span style="color:#000000">&nbsp;</span><a rel="author" href="http://bsubramanyamraju.blogspot.in/p/about-me.html">Subramanyam Raju</a><span style="color:#000000">&nbsp;:)</span></span></span></div>
</span></span></div>
</div>
