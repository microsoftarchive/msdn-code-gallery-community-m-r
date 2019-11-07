# RDLC example
## Requires
- Visual Studio 2008
## License
- Apache License, Version 2.0
## Technologies
- Reporting
- RDLC
## Topics
- Reporting
- RDLC
## Updated
- 06/15/2012
## Description

<p>This is a simple tutorial which demonstrates how to create a dynamic Local Report.</p>
<p>&nbsp;</p>
<p><img src="59925-15-06-2012%2022.50.49.jpg" alt="" width="322" height="458"></p>
<p>&nbsp;</p>
<p>Follow these simple steps:</p>
<ul>
<li>Create a new Project (named RDLC example)<br>
Add a Button to Form1 &#43; change the text to Show Report, Name = btnReport<br>
Add a dataset to your Project &#43; accept the default Name (Project--&gt;Add New Item)<br>
Right click the Dataset designer &#43; select Add--&gt;Datatable<br>
Right click the Datatable designer &#43; add 2 Columns (ID &#43; Name)<br>
Rebuild &#43; save your Project<br>
Select the ID column &#43; change it's DataType Property to Int32<br>
Add a Report to your Project &#43; accept the default Name (Project--&gt;Add New Item--&gt;Reporting)<br>
Set your Report's Copy to Output Directory Property to Copy Always<br>
Add a TextBox to your Report designer &#43; set these Properties:<br>
&nbsp;&nbsp;&nbsp; Value = &quot;Dynamic Report&quot;<br>
&nbsp;&nbsp; &nbsp;Color = Color.SteelBlue<br>
&nbsp;&nbsp;&nbsp; Font = Tahoma 20pt Bold<br>
Add a Table to your Report designer<br>
From DataSet1 Data Sources view, drag the ID Field to the 1st Column, 2nd Row &#43; the Name Field to the 2nd Column, 2nd Row<br>
Set the 2 Textboxes in the 1st Column's TextAlign Property to Left<br>
Set the ID TextBox's BackGroundColor Property to SteelBlue &#43; it's Color Property to White, FontWeight = Bold &#43; do the same for the Name TextBox<br>
Select the 3rd Column in your Report &#43; right click &#43; click Delete Column<br>
Resize your Table Width to 6cm<br>
Add a new Form to your Project &#43; name it frmViewer<br>
Add a ReportViewer to frmViewer &#43; set it's Dock Property to Fill<br>
Finally, type in the code in Form1 </li></ul>
