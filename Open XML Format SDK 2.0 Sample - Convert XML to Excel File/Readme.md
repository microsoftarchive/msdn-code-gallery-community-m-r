# Open XML Format SDK 2.0 Sample - Convert XML to Excel File
## Requires
- Visual Studio 2010
## License
- Apache License, Version 2.0
## Technologies
- Windows Forms
- XML
- Microsoft Office Excel 2007
- Microsoft Office Excel 2010
- Open XML SDK 2.0
- Excel 2010
## Topics
- Excel
- Excel Automation
- Office Automation
- Excel 2007
- Open XML
- Excel 2010
- Microsoft Office
- Office 2010 101 code samples
- Convert XML to Excel
- Open XML SDK
- Open Office
- Office 2007 Excel
## Updated
- 10/13/2011
## Description

<h1>Introduction</h1>
<p><em>This sample is to convert an XML file to an Excel file at office 2007/2010 format. It has five overloaded methods to allow you easily choose to convert between files and memorystreams. The method used in test project will use the root node name of XML
 as the Excel file name, the child node name of root node as the sheet name, the grand child node name as the column name, and the elements of the grand child as the actual list in the spread sheet. For example, the following XML
</em></p>
<p>&lt;PARTS&gt;<br>
&nbsp;&nbsp; &lt;PART&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;ITEM&gt;Motherboard&lt;/ITEM&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;MANUFACTURER&gt;ASUS&lt;/MANUFACTURER&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;MODEL&gt;P3B-F&lt;/MODEL&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;COST&gt; 123.00&lt;/COST&gt;<br>
&nbsp;&nbsp; &lt;/PART&gt;<br>
&nbsp;&nbsp; &lt;PART&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;ITEM&gt;Video Card&lt;/ITEM&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;MANUFACTURER&gt;ATI&lt;/MANUFACTURER&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;MODEL&gt;All-in-Wonder Pro&lt;/MODEL&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;COST&gt; 160.00&lt;/COST&gt;<br>
&nbsp;&nbsp; &lt;/PART&gt;<br>
&nbsp;&nbsp; &lt;PART&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;ITEM&gt;Sound Card&lt;/ITEM&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;MANUFACTURER&gt;Creative Labs&lt;/MANUFACTURER&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;MODEL&gt;Sound Blaster Live&lt;/MODEL&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;COST&gt; 80.00&lt;/COST&gt;<br>
&nbsp;&nbsp; &lt;/PART&gt;<br>
&nbsp;&nbsp; &lt;PART&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;ITEM&gt;Monitor&lt;/ITEM&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;MANUFACTURER&gt;LG Electronics&lt;/MANUFACTURER&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;MODEL&gt; 995E&lt;/MODEL&gt;<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;COST&gt; 290.00&lt;/COST&gt;<br>
&nbsp;&nbsp; &lt;/PART&gt;<br>
&lt;/PARTS&gt;</p>
<p><em>will be converted to an Excel file which name is &quot;PARTS.xlsx&quot;, it contians a worksheet which name is &quot;PART&quot;, on that worksheet, ITEM, MANUFACTURER, MODEL, COST of each PART will be listed. The sample only converts one worksheet, but should be easily
 expanded to allow multi-worksheet convertion.<br>
</em></p>
<h1><span>Building the Sample</span></h1>
<p><em>The solution contains two projects, one is a class for the XMLToExcel convertion, the other is a test Window application. Before you test it, make sure you have &quot;Visual Studio 2010&quot; and &quot;</em>Open XML Format SDK 2.0<em>&quot; installed on your machine.</em></p>
<p><em>To build and test, set the Window application &quot;XMLToExcel&quot; as startup project, then run it under debug mode.<br>
</em></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><em>The class &quot;XMLToExcel&quot; contains the following five overloaded methods.</em></p>
<p><em>1. </em>XMLToExcel(MemoryStream msXML, ref MemoryStream msExcel)</p>
<p>This is to convert XML MemoryStream to Excel MemoryStream.</p>
<p>2. XMLToExcel(MemoryStream msXML, string strExcelPath)</p>
<p>This is to convert XML MemoryStream to Excel file.</p>
<p>3. XMLToExcel(string strXMLPath, ref MemoryStream msExcel)</p>
<p>This is to convert XML file to Excel MemoryStream.</p>
<p>4. XMLToExcel(string strXMLPath, string strExcelPath)</p>
<p>This is to convert XML file to Excel file.</p>
<p>5. XMLToExcel(string strXMLPath)</p>
<p>This is to convert XML file to Excel file, and use rootnode as file name and first child node name as worksheet name.</p>
<p>&nbsp;</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">//populate the data into the spreadsheet  
using (SpreadsheetDocument spreadsheet =
            SpreadsheetDocument.Open(filePath, true))
{
    WorkbookPart workbook = spreadsheet.WorkbookPart;
    //create a reference to Sheet1  
    WorksheetPart worksheet = workbook.WorksheetParts.Last();
    SheetData data = worksheet.Worksheet.GetFirstChild&lt;SheetData&gt;();
    Sheet sheet = workbook.Workbook.Sheets.GetFirstChild&lt;Sheet&gt;();
    sheet.Name = tblInput.TableName;

    //add column names to the first row  
    Row header = new Row();
    header.RowIndex = (UInt32)1;

    foreach (DataColumn column in tblInput.Columns)
    {
        Cell headerCell = createTextCell(
            tblInput.Columns.IndexOf(column) &#43; 1,
            1,
            column.ColumnName);

        headerCell.StyleIndex = 1;
        header.AppendChild(headerCell);
    }
    data.AppendChild(header);

    //loop through each data row  
    DataRow contentRow;
    for (int i = 0; i &lt; tblInput.Rows.Count; i&#43;&#43;)
    {
        contentRow = tblInput.Rows[i];
        data.AppendChild(createContentRow(contentRow, i &#43; 2));
    }

    spreadsheet.WorkbookPart.Workbook.Save();
}</pre>
<div class="preview">
<pre id="codePreview" class="csharp"><span class="cs__com">//populate&nbsp;the&nbsp;data&nbsp;into&nbsp;the&nbsp;spreadsheet&nbsp;&nbsp;</span>&nbsp;
<span class="cs__keyword">using</span>&nbsp;(SpreadsheetDocument&nbsp;spreadsheet&nbsp;=&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SpreadsheetDocument.Open(filePath,&nbsp;<span class="cs__keyword">true</span>))&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;WorkbookPart&nbsp;workbook&nbsp;=&nbsp;spreadsheet.WorkbookPart;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//create&nbsp;a&nbsp;reference&nbsp;to&nbsp;Sheet1&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;WorksheetPart&nbsp;worksheet&nbsp;=&nbsp;workbook.WorksheetParts.Last();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;SheetData&nbsp;data&nbsp;=&nbsp;worksheet.Worksheet.GetFirstChild&lt;SheetData&gt;();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Sheet&nbsp;sheet&nbsp;=&nbsp;workbook.Workbook.Sheets.GetFirstChild&lt;Sheet&gt;();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;sheet.Name&nbsp;=&nbsp;tblInput.TableName;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//add&nbsp;column&nbsp;names&nbsp;to&nbsp;the&nbsp;first&nbsp;row&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Row&nbsp;header&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Row();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;header.RowIndex&nbsp;=&nbsp;(UInt32)<span class="cs__number">1</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">foreach</span>&nbsp;(DataColumn&nbsp;column&nbsp;<span class="cs__keyword">in</span>&nbsp;tblInput.Columns)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Cell&nbsp;headerCell&nbsp;=&nbsp;createTextCell(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tblInput.Columns.IndexOf(column)&nbsp;&#43;&nbsp;<span class="cs__number">1</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__number">1</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;column.ColumnName);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;headerCell.StyleIndex&nbsp;=&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;header.AppendChild(headerCell);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;data.AppendChild(header);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//loop&nbsp;through&nbsp;each&nbsp;data&nbsp;row&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;DataRow&nbsp;contentRow;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(<span class="cs__keyword">int</span>&nbsp;i&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;i&nbsp;&lt;&nbsp;tblInput.Rows.Count;&nbsp;i&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;contentRow&nbsp;=&nbsp;tblInput.Rows[i];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;data.AppendChild(createContentRow(contentRow,&nbsp;i&nbsp;&#43;&nbsp;<span class="cs__number">2</span>));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;spreadsheet.WorkbookPart.Workbook.Save();&nbsp;
}</pre>
</div>
</div>
</div>
<h1>More Information</h1>
<p><em>For more information on </em>Open XML SDK 2.0 for Microsoft Office or the download,<em> see
</em><a href="http://www.microsoft.com/download/en/details.aspx?id=5124">here</a>.</p>
