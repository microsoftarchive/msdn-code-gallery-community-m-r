# Multi Select ComboBox in WPF
## Requires
- Visual Studio 2010
## License
- Apache License, Version 2.0
## Technologies
- C#
- WPF
## Topics
- Data Binding
- custom controls
- MVVM
- User Control
## Updated
- 07/23/2013
## Description

<h1>Introduction</h1>
<p><em><em>WPF don't have Multi select combobox. Generally people go for third party tools to use a multi select combobox. The attached sample will help you to build your own custom Multiselect comboBox</em></em></p>
<h1>Description</h1>
<p><em><span>Recently in our project we wanted to allow the user to select multiple values in a list. But the list should be populated inside a grid row. So we didn&rsquo;t want to use a listbox and also we were not interested in third party tools. Instead
 of that, we wanted to use a&nbsp; multiselect combobox. When I browsed through various blogs, forums, etc., I got a few good codes, but none of them worked with MVVM pattern. In those articles, most of the datasource bindings were done at code behind. So I
 have changes to those existing code to support MVVM. So i have written an article with step by step procedure</span>&nbsp;to build the control. You can find the article in the below link. One should know basics of WPF and dependency properties before beginning
 this sample.</em></p>
<p><em><a title="http://www.codeproject.com/Articles/563862/Multi-Select-ComboBox-in-WPF" href="http://www.codeproject.com/Articles/563862/Multi-Select-ComboBox-in-WPF" target="_blank">http://www.codeproject.com/Articles/563862/Multi-Select-ComboBox-in-WPF</a></em></p>
<p>or</p>
<p><em><a href="http://www.c-sharpcorner.com/UploadFile/1a81c5/multi-select-combobox-in-wpf/" target="_blank">http://www.c-sharpcorner.com/UploadFile/1a81c5/multi-select-combobox-in-wpf/</a><br>
</em></p>
<p><em>In the view where you are using this control , you just need to specify six properties for this multiselect combobox.</em></p>
<ul class="property">
<li><code>Items</code> </li><li><code>SelectedItems</code> </li><li><code>Width</code> </li><li><code>Height</code> </li><li><code>Text</code> </li><li><code>DefaultText</code> </li></ul>
<p><em><br>
</em></p>
<p>Browse through the code and go through the steps to understand more.</p>
<p>&nbsp;</p>
<p>Extension:</p>
<p>Added ToolTip.</p>
<p>Added Drodown arrow, background for popup and fixed uncheck all option</p>
<p><em><br>
</em></p>
<ul>
</ul>
