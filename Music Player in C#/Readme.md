# Music Player in C#
## Requires
- Visual Studio 2012
## License
- MS-LPL
## Technologies
- C#
## Topics
- music
- player
## Updated
- 10/17/2013
## Description

<h1>Introduction</h1>
<p><em>Hi This is a simple Music Player app Using C#</em></p>
<p><em><span>&nbsp;Windows 8 Apps&nbsp;have a good control for playing audio &amp; videos in applications</span></em></p>
<p><em><span>I integrated a Media Control in this sample and i used several methods to make it work&nbsp;</span></em></p>
<p><em><span><span>To embed a Music or video Player &nbsp;in your application we use the &quot;MediaElement class&quot;</span></span></em></p>
<p><em><span><span>I also&nbsp;<span>&nbsp;created customized controls to give more functionality like play, pause, volume &#43;, Volume - and Stop .</span></span></span></em></p>
<p><em><span><span><span>You can use also &quot;<span>enable full-screen mode&quot; when you use a video&nbsp;</span></span></span></span></em></p>
<p><em><br>
</em></p>
<p><em>I used&nbsp;<span>&nbsp;the MediaElement class</span><span>&nbsp;to control and manage audio media playback in a Windows Store app using &nbsp;C#</span></em></p>
<p><em><span><span>In this Sample The&nbsp;</span><strong>MediaElement</strong><span>&nbsp;class has these methods:</span></span></em></p>
<table id="memberListMethods" class="members">
<tbody>
<tr>
<td><strong>Pause</strong></td>
<td>
<p>Pauses media at the current position.</p>
</td>
</tr>
<tr>
<td><strong>Play</strong></td>
<td>
<p>Plays media from the current position. &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
<em>&nbsp;</em></p>
</td>
</tr>
</tbody>
</table>
<table id="memberListMethods" class="members">
<tbody>
<tr>
<td><strong>Stop</strong> Stops and resets media to be played from the beginning.<br>
<br>
</td>
</tr>
</tbody>
</table>
<p><em><em><span>The&nbsp;</span><strong>MediaElement</strong><span>&nbsp;class has these properties:</span><br>
</em></em></p>
<p><em><em><strong><span>IsMuted &nbsp;Gets or sets a value indicating whether the audio is muted.</span></strong></em></em></p>
<p><strong><em>Volume &nbsp; &nbsp;<span>Gets or sets the media's volume.</span></em></strong></p>
<p><em><span><span><br>
<br>
</span></span></em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><em>I've used Visual Studio Ultimate 2012.&nbsp;</em></p>
<p><em>i used :</em></p>
<p><em>5 buttons :</em></p>
<p>Button &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;Click<span>&nbsp;handler action</span></p>
<p><span>Play &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<span>Calls the P</span><strong>lay</strong><span>&nbsp;method.</span></span></p>
<p><span>Pause &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;Calls the
<strong>Pause</strong>&nbsp;method.</span></p>
<p>Stop &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;Calls the
<strong>Stop&nbsp;</strong>&nbsp;method.</p>
<p><span>Vol - &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;Volume Down</span></p>
<p><span>Vol &#43; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Volume Up</span></p>
<p><em><br>
</em></p>
<p>and 1 MediaElement (Name property: Media)</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Music_Player
{
    /// &lt;summary&gt;
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// &lt;/summary&gt;
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// &lt;summary&gt;
        /// Invoked when this page is about to be displayed in a Frame.
        /// &lt;/summary&gt;
        /// &lt;param name=&quot;e&quot;&gt;Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.&lt;/param&gt;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Media.Play();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Media.Pause();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Media.Stop();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (Media.IsMuted)
            {
                Media.IsMuted = false;
            }

            if (Media.Volume &gt; 0)
            {
                Media.Volume -= .1;
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (Media.IsMuted)
            {
                Media.IsMuted = false;
            }

            if (Media.Volume &lt; 1)
            {
                Media.Volume &#43;= .1;
            }
        }
    }
}
</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">using</span>&nbsp;System;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Collections.Generic;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.IO;&nbsp;
<span class="cs__keyword">using</span>&nbsp;System.Linq;&nbsp;
<span class="cs__keyword">using</span>&nbsp;Windows.Foundation;&nbsp;
<span class="cs__keyword">using</span>&nbsp;Windows.Foundation.Collections;&nbsp;
<span class="cs__keyword">using</span>&nbsp;Windows.UI.Xaml;&nbsp;
<span class="cs__keyword">using</span>&nbsp;Windows.UI.Xaml.Controls;&nbsp;
<span class="cs__keyword">using</span>&nbsp;Windows.UI.Xaml.Controls.Primitives;&nbsp;
<span class="cs__keyword">using</span>&nbsp;Windows.UI.Xaml.Data;&nbsp;
<span class="cs__keyword">using</span>&nbsp;Windows.UI.Xaml.Input;&nbsp;
<span class="cs__keyword">using</span>&nbsp;Windows.UI.Xaml.Media;&nbsp;
<span class="cs__keyword">using</span>&nbsp;Windows.UI.Xaml.Navigation;&nbsp;
&nbsp;
<span class="cs__com">//&nbsp;The&nbsp;Blank&nbsp;Page&nbsp;item&nbsp;template&nbsp;is&nbsp;documented&nbsp;at&nbsp;http://go.microsoft.com/fwlink/?LinkId=234238</span>&nbsp;
&nbsp;
<span class="cs__keyword">namespace</span>&nbsp;Music_Player&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;An&nbsp;empty&nbsp;page&nbsp;that&nbsp;can&nbsp;be&nbsp;used&nbsp;on&nbsp;its&nbsp;own&nbsp;or&nbsp;navigated&nbsp;to&nbsp;within&nbsp;a&nbsp;Frame.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">sealed</span>&nbsp;partial&nbsp;<span class="cs__keyword">class</span>&nbsp;MainPage&nbsp;:&nbsp;Page&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;MainPage()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.InitializeComponent();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;Invoked&nbsp;when&nbsp;this&nbsp;page&nbsp;is&nbsp;about&nbsp;to&nbsp;be&nbsp;displayed&nbsp;in&nbsp;a&nbsp;Frame.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;&lt;param&nbsp;name=&quot;e&quot;&gt;Event&nbsp;data&nbsp;that&nbsp;describes&nbsp;how&nbsp;this&nbsp;page&nbsp;was&nbsp;reached.&nbsp;&nbsp;The&nbsp;Parameter</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">///&nbsp;property&nbsp;is&nbsp;typically&nbsp;used&nbsp;to&nbsp;configure&nbsp;the&nbsp;page.&lt;/param&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">protected</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;OnNavigatedTo(NavigationEventArgs&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Button_Click_1(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;RoutedEventArgs&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Media.Play();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Button_Click_2(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;RoutedEventArgs&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Media.Pause();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Button_Click_3(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;RoutedEventArgs&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Media.Stop();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Button_Click_4(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;RoutedEventArgs&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(Media.IsMuted)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Media.IsMuted&nbsp;=&nbsp;<span class="cs__keyword">false</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(Media.Volume&nbsp;&gt;&nbsp;<span class="cs__number">0</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Media.Volume&nbsp;-=&nbsp;.<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Button_Click_5(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;RoutedEventArgs&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(Media.IsMuted)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Media.IsMuted&nbsp;=&nbsp;<span class="cs__keyword">false</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(Media.Volume&nbsp;&lt;&nbsp;<span class="cs__number">1</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Media.Volume&nbsp;&#43;=&nbsp;.<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
</pre>
</div>
</div>
</div>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p><em>For more information contact Me : Chiheb-chebbi@outlook.fr</em></p>
