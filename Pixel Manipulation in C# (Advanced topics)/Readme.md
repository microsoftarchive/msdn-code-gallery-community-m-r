# Pixel Manipulation in C# (Advanced topics)
## Requires
- Visual Studio 2010
## License
- Apache License, Version 2.0
## Technologies
- GDI+
## Topics
- Images
- Image manipulation
## Updated
- 09/18/2012
## Description

<h1>Introduction</h1>
<p><em>I have been noticing that for some reason people are developing several projects with pictures in Visual Studio Projects, mainly Windows forms projects.</em></p>
<p><em>Some days ago I helped a guy with an issue of zoom in and out an image within a picture box. For those that don&rsquo;t know this visual studio toolbox artifact, it represents a Windows picture box control for displaying an image.
</em></p>
<p><em>After helping this guy I started to Google for some related problems and found out many other people asking the same questions. In order to help these people and other that may work with Images in Visual Studio projects I developed a small Demo, to show
 you how simple operations can be done. You can find this previous demo in the following link:
<a href="http://code.msdn.microsoft.com/Image-Manipulation-C-09020bc1">http://code.msdn.microsoft.com/Image-Manipulation-C-09020bc1</a></em></p>
<p><span id="more-399">&nbsp;</span></p>
<p><em>When I started to build that demo my interest in picture manipulation increased and I continued searching and working on algorithms to manipulate images using C#. After those simple operations I mentioned before I found out that working with images manipulation
 its Pixels could bring me more flexibility and with it more functionalities to my picture manipulation demos.
</em></p>
<p><em>This way and as complementary software I bring you this &ldquo;Pixel manipulation in C#&rdquo; with some advanced topics on working with images such as applying filters (Black and white, Sepia and
<strong>my very own filters</strong>.). With these two open source projects I thing that you can start building your own image manipulation software for windows phone or windows, as you prefer.</em></p>
<h1>Description</h1>
<p>The Function included in this release are the following:</p>
<p>&middot; Changing a Pixel color with mouse click</p>
<p><a href="http://rpmachado.files.wordpress.com/2012/08/clip_image001.png"><img title="clip_image001" src="-clip_image001_thumb.png?w=244&h=81" border="0" alt="clip_image001" width="244" height="81" style="margin:0; padding-left:0; padding-right:0; display:inline; padding-top:0; border:0"></a></p>
<p>&middot; Changing a Pixel color with Position manipulation</p>
<p><a href="http://rpmachado.files.wordpress.com/2012/08/clip_image002.png"><img title="clip_image002" src="-clip_image002_thumb.png?w=244&h=85" border="0" alt="clip_image002" width="244" height="85" style="margin:0; padding-left:0; padding-right:0; display:inline; padding-top:0; border:0"></a></p>
<p>&middot; Getting a Pixel Color with mouse click</p>
<p><a href="http://rpmachado.files.wordpress.com/2012/08/clip_image003.png"><img title="clip_image003" src="-clip_image003_thumb.png?w=79&h=91" border="0" alt="clip_image003" width="79" height="91" style="margin:0; padding-left:0; padding-right:0; display:inline; padding-top:0; border:0"></a></p>
<p>&middot; Getting several Pixels color with mouse move</p>
<p><a href="http://rpmachado.files.wordpress.com/2012/08/clip_image005.png"><img title="clip_image005" src="-clip_image005_thumb.png?w=155&h=80" border="0" alt="clip_image005" width="155" height="80" style="margin:0; padding-left:0; padding-right:0; display:inline; padding-top:0; border:0"></a><a href="http://rpmachado.files.wordpress.com/2012/08/clip_image007.png"><img title="clip_image007" src="-clip_image007_thumb.png?w=157&h=79" border="0" alt="clip_image007" width="157" height="79" style="margin:0; padding-left:0; padding-right:0; display:inline; padding-top:0; border:0"></a></p>
<p>&middot; Applying Filters</p>
<p>o <strong>Rainbow Filter</strong></p>
<p><a href="http://rpmachado.files.wordpress.com/2012/08/clip_image009.png"><img title="clip_image009" src="-clip_image009_thumb.png?w=176&h=138" border="0" alt="clip_image009" width="176" height="138" style="margin:0; padding-left:0; padding-right:0; display:inline; padding-top:0; border:0"></a></p>
<p>&nbsp;</p>
<p>Rainbow Filter Code</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">public static Bitmap RainbowFilter(Bitmap bmp)
        {

            Bitmap temp = new Bitmap(bmp.Width, bmp.Height);
            int raz = bmp.Height / 4;
            for (int i = 0; i &lt; bmp.Width; i&#43;&#43;)
            {
                for (int x = 0; x &lt; bmp.Height; x&#43;&#43;)
                {

                    if (i &lt; (raz))
                    {
                        temp.SetPixel(i, x, Color.FromArgb(bmp.GetPixel(i, x).R / 5, bmp.GetPixel(i, x).G, bmp.GetPixel(i, x).B));
                    }
                    else if (i &lt; (raz * 2))
                    {
                        temp.SetPixel(i, x, Color.FromArgb(bmp.GetPixel(i, x).R, bmp.GetPixel(i, x).G / 5, bmp.GetPixel(i, x).B));
                    }
                    else if (i &lt; (raz * 3))
                    {
                        temp.SetPixel(i, x, Color.FromArgb(bmp.GetPixel(i, x).R, bmp.GetPixel(i, x).G, bmp.GetPixel(i, x).B / 5));
                    }
                    else if (i &lt; (raz * 4))
                    {
                        temp.SetPixel(i, x, Color.FromArgb(bmp.GetPixel(i, x).R / 5, bmp.GetPixel(i, x).G, bmp.GetPixel(i, x).B / 5));
                    }
                    else
                    {
                        temp.SetPixel(i, x, Color.FromArgb(bmp.GetPixel(i, x).R / 5, bmp.GetPixel(i, x).G / 5, bmp.GetPixel(i, x).B / 5));
                    }
                }

            }
            return temp;
        }</pre>
<div class="preview">
<pre class="js">public&nbsp;static&nbsp;Bitmap&nbsp;RainbowFilter(Bitmap&nbsp;bmp)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Bitmap&nbsp;temp&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;Bitmap(bmp.Width,&nbsp;bmp.Height);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;int&nbsp;raz&nbsp;=&nbsp;bmp.Height&nbsp;/&nbsp;<span class="js__num">4</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">for</span>&nbsp;(int&nbsp;i&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;i&nbsp;&lt;&nbsp;bmp.Width;&nbsp;i&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">for</span>&nbsp;(int&nbsp;x&nbsp;=&nbsp;<span class="js__num">0</span>;&nbsp;x&nbsp;&lt;&nbsp;bmp.Height;&nbsp;x&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(i&nbsp;&lt;&nbsp;(raz))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;temp.SetPixel(i,&nbsp;x,&nbsp;Color.FromArgb(bmp.GetPixel(i,&nbsp;x).R&nbsp;/&nbsp;<span class="js__num">5</span>,&nbsp;bmp.GetPixel(i,&nbsp;x).G,&nbsp;bmp.GetPixel(i,&nbsp;x).B));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">else</span>&nbsp;<span class="js__statement">if</span>&nbsp;(i&nbsp;&lt;&nbsp;(raz&nbsp;*&nbsp;<span class="js__num">2</span>))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;temp.SetPixel(i,&nbsp;x,&nbsp;Color.FromArgb(bmp.GetPixel(i,&nbsp;x).R,&nbsp;bmp.GetPixel(i,&nbsp;x).G&nbsp;/&nbsp;<span class="js__num">5</span>,&nbsp;bmp.GetPixel(i,&nbsp;x).B));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">else</span>&nbsp;<span class="js__statement">if</span>&nbsp;(i&nbsp;&lt;&nbsp;(raz&nbsp;*&nbsp;<span class="js__num">3</span>))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;temp.SetPixel(i,&nbsp;x,&nbsp;Color.FromArgb(bmp.GetPixel(i,&nbsp;x).R,&nbsp;bmp.GetPixel(i,&nbsp;x).G,&nbsp;bmp.GetPixel(i,&nbsp;x).B&nbsp;/&nbsp;<span class="js__num">5</span>));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">else</span>&nbsp;<span class="js__statement">if</span>&nbsp;(i&nbsp;&lt;&nbsp;(raz&nbsp;*&nbsp;<span class="js__num">4</span>))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;temp.SetPixel(i,&nbsp;x,&nbsp;Color.FromArgb(bmp.GetPixel(i,&nbsp;x).R&nbsp;/&nbsp;<span class="js__num">5</span>,&nbsp;bmp.GetPixel(i,&nbsp;x).G,&nbsp;bmp.GetPixel(i,&nbsp;x).B&nbsp;/&nbsp;<span class="js__num">5</span>));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;temp.SetPixel(i,&nbsp;x,&nbsp;Color.FromArgb(bmp.GetPixel(i,&nbsp;x).R&nbsp;/&nbsp;<span class="js__num">5</span>,&nbsp;bmp.GetPixel(i,&nbsp;x).G&nbsp;/&nbsp;<span class="js__num">5</span>,&nbsp;bmp.GetPixel(i,&nbsp;x).B&nbsp;/&nbsp;<span class="js__num">5</span>));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;temp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p>&nbsp;</p>
<p>o <strong>Magic Mosaic</strong></p>
<p><a href="http://rpmachado.files.wordpress.com/2012/08/clip_image011.png"><img title="clip_image011" src="-clip_image011_thumb.png?w=177&h=138" border="0" alt="clip_image011" width="177" height="138" style="margin:0; padding-left:0; padding-right:0; display:inline; padding-top:0; border:0"></a></p>
<p>o <strong>Night Filter</strong></p>
<p><strong><a href="http://rpmachado.files.wordpress.com/2012/08/clip_image013.png"><img title="clip_image013" src="-clip_image013_thumb.png?w=173&h=135" border="0" alt="clip_image013" width="173" height="135" style="margin:0; padding-left:0; padding-right:0; display:inline; padding-top:0; border:0"></a></strong></p>
<p>o <strong>Hell Filter</strong></p>
<p><a href="http://rpmachado.files.wordpress.com/2012/08/clip_image015.jpg"><img title="clip_image015" src="-clip_image015_thumb.jpg?w=173&h=135" border="0" alt="clip_image015" width="173" height="135" style="margin:0; padding-left:0; padding-right:0; display:inline; padding-top:0; border:0"></a></p>
<p>o <strong>Miami Filter</strong></p>
<p><a href="http://rpmachado.files.wordpress.com/2012/08/clip_image017.png"><img title="clip_image017" src="-clip_image017_thumb.png?w=173&h=136" border="0" alt="clip_image017" width="173" height="136" style="margin:0; padding-left:0; padding-right:0; display:inline; padding-top:0; border:0"></a></p>
<p>o <strong>Zen Filter</strong></p>
<p><a href="http://rpmachado.files.wordpress.com/2012/08/clip_image019.jpg"><img title="clip_image019" src="-clip_image019_thumb.jpg?w=173&h=136" border="0" alt="clip_image019" width="173" height="136" style="margin:0; padding-left:0; padding-right:0; display:inline; padding-top:0; border:0"></a></p>
<p>o <strong>Black and White</strong></p>
<p><a href="http://rpmachado.files.wordpress.com/2012/08/clip_image021.jpg"><img title="clip_image021" src="-clip_image021_thumb.jpg?w=173&h=135" border="0" alt="clip_image021" width="173" height="135" style="margin:0; padding-left:0; padding-right:0; display:inline; padding-top:0; border:0"></a></p>
<p>o <strong>Swap</strong></p>
<p><a href="http://rpmachado.files.wordpress.com/2012/08/clip_image023.png"><img title="clip_image023" src="-clip_image023_thumb.png?w=173&h=135" border="0" alt="clip_image023" width="173" height="135" style="margin:0; padding-left:0; padding-right:0; display:inline; padding-top:0; border:0"></a></p>
<p>o <strong>Crazy Filter</strong></p>
<p><a href="http://rpmachado.files.wordpress.com/2012/08/clip_image025.png"><img title="clip_image025" src="-clip_image025_thumb.png?w=173&h=134" border="0" alt="clip_image025" width="173" height="134" style="margin:0; padding-left:0; padding-right:0; display:inline; padding-top:0; border:0"></a></p>
<p>o <strong>Mega Filter Green</strong></p>
<p><a href="http://rpmachado.files.wordpress.com/2012/08/clip_image027.png"><img title="clip_image027" src="-clip_image027_thumb.png?w=173&h=135" border="0" alt="clip_image027" width="173" height="135" style="margin:0; padding-left:0; padding-right:0; display:inline; padding-top:0; border:0"></a></p>
<p>o <strong>Mega Filter Orange</strong></p>
<p><strong><a href="http://rpmachado.files.wordpress.com/2012/08/clip_image029.png"><img title="clip_image029" src="-clip_image029_thumb.png?w=173&h=134" border="0" alt="clip_image029" width="173" height="134" style="margin:0; padding-left:0; padding-right:0; display:inline; padding-top:0; border:0"></a></strong></p>
<p>o <strong>Mega Filter Custom</strong></p>
<p>Apply the Mega filter with the color you prefer by selecting it in the button that says &ldquo;Color&rdquo;</p>
<p>&middot; Drawing on a Picture Box</p>
<p><a href="http://rpmachado.files.wordpress.com/2012/08/clip_image031.png"><img title="clip_image031" src="-clip_image031_thumb.png?w=244&h=132" border="0" alt="clip_image031" width="244" height="132" style="margin:0; padding-left:0; padding-right:0; display:inline; padding-top:0; border:0"></a></p>
<p>&middot; Circumvent an image in a Picture Box and watch your circumvent in another Picture Box</p>
<p><a href="http://rpmachado.files.wordpress.com/2012/08/clip_image033.jpg"><img title="clip_image033" src="-clip_image033_thumb.jpg?w=173&h=244" border="0" alt="clip_image033" width="173" height="244" style="margin:0; padding-left:0; padding-right:0; display:inline; padding-top:0; border:0"></a></p>
<p><a href="http://rpmachado.files.wordpress.com/2012/08/clip_image035.png"><img title="clip_image035" src="-clip_image035_thumb.png?w=244&h=132" border="0" alt="clip_image035" width="244" height="132" style="padding-left:0; padding-right:0; display:inline; padding-top:0; border:0"></a></p>
<h1>Building the Project</h1>
<p>Download the project, open in Visual Studio 2010, build it and enjoy.</p>
<h1>About Me</h1>
<p>Rui Pedro Machado | 2012</p>
<p>rpmachado.wordpress.com</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
