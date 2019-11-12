# Picture gallery in thumbnails view and playing slide show in vb.net
## Requires
- Visual Studio 2010
## License
- Apache License, Version 2.0
## Technologies
- VB.Net
## Topics
- Image Gallery
- Image
- User Control
## Updated
- 09/27/2012
## Description

<h1>Introduction</h1>
<p><span class="content">This article demonstrates how we can create a simple image gallery in vb.net. In this article we create a usercontrol for doing it, our user control will create the thumbnails view of the images from the specified directory or folder.You
 can also learn that how to play slide show of the images.<br>
</span></p>
<h1><span>Building the Sample</span></h1>
<p>Before run and test the sample you need to follow the next steps.</p>
<p>1. Open your Visual Studio 2010.</p>
<p>2. Open sample project from File--&gt;Open Project option.</p>
<p>3. Run the application.</p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><span class="content">I can divide the project in to two module.</span></p>
<p><span class="content">1. Creating usecontrol for displaying images in thumbnails view.</span></p>
<p><span class="content">2. Design the user inteterface.<br>
</span></p>
<p><span class="content">3. Add a 'Playing slide show' functionality.</span></p>
<p><br>
<strong><span class="content">Creating user control</span></strong><span class="content">&nbsp;</span></p>
<p><span class="content">For creating your Image Gallery usercontrol, follow these steps:<br>
1. Add a usercontrol in your project and give the name AuthorCodeImageGalleryVB.</span></p>
<p>Code</p>
<p>See the code for creating a picturebox as thumbnail of the image.</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>

<div class="preview">
<pre class="js">&nbsp;Private&nbsp;Sub&nbsp;DrawPictureBox(ByVal&nbsp;_filename&nbsp;As&nbsp;<span class="js__object">String</span>,&nbsp;ByVal&nbsp;_displayname&nbsp;As&nbsp;<span class="js__object">String</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Dim&nbsp;Pic1&nbsp;As&nbsp;New&nbsp;PictureBox&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Pic1.Location&nbsp;=&nbsp;New&nbsp;<a class="libraryLink" href="http://msdn.microsoft.com/en-US/library/System.Drawing.Point.aspx" target="_blank" title="Auto generated link to System.Drawing.Point">System.Drawing.Point</a>(XLocation,&nbsp;YLocation)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XLocation&nbsp;=&nbsp;XLocation&nbsp;&#43;&nbsp;PicWidth&nbsp;&#43;&nbsp;<span class="js__num">20</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;If&nbsp;XLocation&nbsp;&#43;&nbsp;PicWidth&nbsp;&gt;=&nbsp;CtrlWidth&nbsp;Then&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XLocation&nbsp;=&nbsp;<span class="js__num">25</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;YLocation&nbsp;=&nbsp;YLocation&nbsp;&#43;&nbsp;PicHeight&nbsp;&#43;&nbsp;<span class="js__num">20</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;End&nbsp;If&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Pic1.Name&nbsp;=&nbsp;<span class="js__string">&quot;PictureBox&quot;</span>&nbsp;&amp;&nbsp;i&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;i&nbsp;&#43;=&nbsp;<span class="js__num">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Pic1.Size&nbsp;=&nbsp;New&nbsp;<a class="libraryLink" href="http://msdn.microsoft.com/en-US/library/System.Drawing.Size.aspx" target="_blank" title="Auto generated link to System.Drawing.Size">System.Drawing.Size</a>(PicWidth,&nbsp;PicHeight)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Pic1.TabIndex&nbsp;=&nbsp;<span class="js__num">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Pic1.TabStop&nbsp;=&nbsp;False&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Pic1.BorderStyle&nbsp;=&nbsp;BorderStyle.Fixed3D&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Me.ToolTip1.SetToolTip(Pic1,&nbsp;_displayname)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;AddHandler&nbsp;Pic1.MouseEnter,&nbsp;AddressOf&nbsp;Pic1_MouseEnter&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;AddHandler&nbsp;Pic1.MouseLeave,&nbsp;AddressOf&nbsp;Pic1_MouseLeave&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Me.Controls.Add(Pic1)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Pic1.Image&nbsp;=&nbsp;Image.FromFile(_filename)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Pic1.SizeMode&nbsp;=&nbsp;System.Windows.Forms.PictureBoxSizeMode.StretchImage&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;End&nbsp;Sub</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;You can find the definitions of the <span class="content">
Pic1_MouseEnter and </span><span class="content">Pic1_MouseLeave in the source code.</span> In the source code you can also find the code for resizing usercontrol and resizing the thumnbails also.</div>
<p>2. Build your project. After building project you can find the usercontrol in the your project's toolbox.</p>
<p><br>
<strong><span class="content">Design the user inteterface.</span></strong></p>
<p><span class="content">1. Add </span><span class="content">a new windows form in your project named Form1.</span><strong><span class="content">&nbsp;</span></strong><span class="content">&nbsp;</span></p>
<p><span class="content">2. Add PictureBox control and give the name PictureBox1.</span></p>
<p><span class="content">3. Add a textbox control and button.<br>
</span></p>
<p><span class="content">4. Add another Tooltip control ( by double click on ToolTip in toolbox or you can add by drag and drop)</span></p>
<p><span class="content">see the design of the Form1:</span></p>
<p><span class="content"><img id="67286" src="67286-form1.jpg" alt=""></span></p>
<p><br>
<strong><span class="content">&nbsp;Add a 'Playing slide show' functionality.</span></strong></p>
<p><span class="content">Add abutton 'Play Slide Show' named </span>btnSlideShow.</p>
<p><strong><span class="content"></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>

<div class="preview">
<pre class="js">&nbsp;&nbsp;&nbsp;&nbsp;Private&nbsp;Sub&nbsp;btnSlideShow_Click(ByVal&nbsp;sender&nbsp;As&nbsp;System.<span class="js__object">Object</span>,&nbsp;ByVal&nbsp;e&nbsp;As&nbsp;<a class="libraryLink" href="http://msdn.microsoft.com/en-US/library/System.EventArgs.aspx" target="_blank" title="Auto generated link to System.EventArgs">System.EventArgs</a>)&nbsp;Handles&nbsp;btnSlideShow.Click&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;If&nbsp;TextBox1.Text&nbsp;&lt;&gt;&nbsp;<span class="js__string">&quot;&quot;</span>&nbsp;Then&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Dim&nbsp;di&nbsp;As&nbsp;New&nbsp;IO.DirectoryInfo(TextBox1.Text)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ImageDir&nbsp;=&nbsp;di.GetFiles(<span class="js__string">&quot;*.jpg&quot;</span>).Concat(di.GetFiles(<span class="js__string">&quot;*.bmp&quot;</span>)).Concat(di.GetFiles(<span class="js__string">&quot;*.png&quot;</span>)).Concat(di.GetFiles(<span class="js__string">&quot;*.gif&quot;</span>)).ToArray&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Dim&nbsp;dra&nbsp;As&nbsp;IO.FileInfo&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;frm&nbsp;=&nbsp;New&nbsp;Form&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;frm.Name&nbsp;=&nbsp;<span class="js__string">&quot;frm&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FullSizePic&nbsp;=&nbsp;New&nbsp;PictureBox&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FullSizePic.Dock&nbsp;=&nbsp;DockStyle.Fill&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FullSizePic.BackColor&nbsp;=&nbsp;Color.Black&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FullSizePic.SizeMode&nbsp;=&nbsp;System.Windows.Forms.PictureBoxSizeMode.Zoom&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;frm.Controls.Add(FullSizePic)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;AddHandler&nbsp;frm.KeyDown,&nbsp;AddressOf&nbsp;frm_keydown&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;frm.Show()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Timer1.Enabled&nbsp;=&nbsp;True&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;EnterFullScreen(frm)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;GetnextImage()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;End&nbsp;If&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;End&nbsp;Sub&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Private&nbsp;Sub&nbsp;GetnextImage()&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;If&nbsp;Not&nbsp;ImageDir&nbsp;Is&nbsp;Nothing&nbsp;Then&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;If&nbsp;ImageIndex&nbsp;&lt;&nbsp;ImageDir.Length&nbsp;-&nbsp;<span class="js__num">1</span>&nbsp;Then&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ImageIndex&nbsp;&#43;=&nbsp;<span class="js__num">1</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FullSizePic.ImageLocation&nbsp;=&nbsp;ImageDir(ImageIndex).FullName&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ElseIf&nbsp;ImageIndex&nbsp;=&nbsp;ImageDir.Length&nbsp;-&nbsp;<span class="js__num">1</span>&nbsp;Then&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ImageIndex&nbsp;=&nbsp;<span class="js__num">0</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FullSizePic.ImageLocation&nbsp;=&nbsp;ImageDir(ImageIndex).FullName&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;End&nbsp;If&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;End&nbsp;If&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;End&nbsp;Sub</pre>
</div>
</div>
</div>
</span></strong><span class="content">
<div class="endscriptcode">Add Timer control that specify the interval for the slide show</div>
</span><strong><span class="content">
<div class="endscriptcode"></div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>

<div class="preview">
<pre class="js">&nbsp;Private&nbsp;Sub&nbsp;Timer1_Tick(ByVal&nbsp;sender&nbsp;As&nbsp;System.<span class="js__object">Object</span>,&nbsp;ByVal&nbsp;e&nbsp;As&nbsp;<a class="libraryLink" href="http://msdn.microsoft.com/en-US/library/System.EventArgs.aspx" target="_blank" title="Auto generated link to System.EventArgs">System.EventArgs</a>)&nbsp;Handles&nbsp;Timer1.Tick&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;GetnextImage()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;End&nbsp;Sub</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;Run the program:</div>
<div class="endscriptcode"></div>
<img id="67287" src="67287-thumbnails%20view.jpg" alt=""><br>
</span></strong>
<p></p>
<h1><span>Source Code Files</span></h1>
<p><strong>AuthorCodeImageGalleryVB.vb</strong> is the usercontrol for displaying images of the folder in thumbnails view.</p>
<p><strong>Form1.vb</strong> contains the <em>AuthorCodeImageGalleryVB </em>user control, slide show button and a textbox control for entering the image folder path.</p>
<p>&nbsp;</p>
