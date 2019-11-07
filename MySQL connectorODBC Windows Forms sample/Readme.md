# MySQL connector/ODBC Windows Forms sample
## Requires
- Visual Studio 2008
## License
- Apache License, Version 2.0
## Technologies
- Windows Forms
- ODBC
- MySQL
## Topics
- ODBC
- MySQL
## Updated
- 06/04/2012
## Description

<h1>Introduction</h1>
<p>&nbsp;</p>
<p>MySQL is the world's most popular open source database software, with over 100 million copies of its software downloaded or distributed throughout it's history.</p>
<p>The MySQL&trade; software delivers a very fast, multi-threaded, multi-user, and robust SQL (Structured Query Language) database server. MySQL Server is intended for mission-critical, heavy-load production systems as well as for embedding into mass-deployed
 software. Oracle is a registered trademark of Oracle Corporation and/or its affiliates. MySQL is a trademark of Oracle Corporation and/or its affiliates, and shall not be used by Customer without Oracle's express written authorization. Other names may be trademarks
 of their respective owners.</p>
<p>Connector/ODBC is a standardized database driver for Windows, Linux, Mac OS X, and Unix platforms.</p>
<p>My sample demonstrates how to make a ODBC connection and simple query request to a MySql database.</p>
<p>This program is also a useful tool for testing connection and SQL queries. I used this tool very often in another MySql industial production system project.</p>
<p>&nbsp;&nbsp;</p>
<h1>Building the Sample</h1>
<p>1.Download the Connector/ODBC 5.1 and install them on Your system</p>
<p>Two version are available:</p>
<ul>
<li>Windows (x86, 32-bit), MSI Installer Connector-ODBC 5.1.11 2.7M </li><li>Windows (x86, 64-bit), MSI Installer Connector-ODBC 5.1.11 4.7M </li></ul>
<p>&nbsp;<a href="http://www.mysql.com/downloads/connector/odbc/">http://www.mysql.com/downloads/connector/odbc/</a></p>
<p>&nbsp;</p>
<p>2. make project specific changes in the connection string:</p>
<p>&nbsp;Dim MyConString As String = &quot;DRIVER={MySQL ODBC 3.51 Driver};&quot; &amp; _</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &quot;SERVER=13.177.224.53;&quot; &amp; _</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &quot;DATABASE=result;&quot; &amp; _</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &quot;USER=userid;&quot; &amp; _</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &quot;PASSWORD=yyyy;&quot; &amp; _</p>
<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &quot;OPTION=2;&quot;</p>
<p>&nbsp;</p>
<p>pease notice</p>
<p>&nbsp;</p>
<ul>
<li>this is connection without predefined DSN name </li><li>In Visual Basic use Option=3 or 2. I've tested with 2 </li><li>I used the 3.51 Driver </li></ul>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<h1>Program description</h1>
<p>&nbsp;</p>
<p>MySql database query tool overview.</p>
<p>&nbsp;</p>
<p>1. Datagridview window</p>
<p>Displays queries in a Datagridview.</p>
<p>&nbsp;</p>
<p>2. Query window</p>
<p>This is a full Richtextbox editor</p>
<p>&nbsp;</p>
<p>Right mouse button menue:</p>
<p>&nbsp;</p>
<ul>
<li>Copy </li><li>Paste </li><li>Cut </li><li>Clear window </li><li>Load file </li><li>Save file </li><li>Font </li></ul>
<p>&nbsp;</p>
<p>Edit queries</p>
<p>&nbsp;</p>
<ul>
<li>// is a comment line </li><li>enter at the end of a line a space (important) </li><li>finish a query string with &lt;semicolon&gt; (important) </li></ul>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>Execute a query string</p>
<p>&nbsp;</p>
<ul>
<li>Doubleclick left mouse button in the first line of a query string </li></ul>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>3. Toolstrip (left to right)</p>
<p>&nbsp;</p>
<ul>
<li>Clear DatagridView </li><li>Windows orientation </li><li>exit </li><li>overwrite mode (Datatable not cleared) </li></ul>
<p>&nbsp;</p>
<p>(sorry, the table result_test is empty)</p>
<p>&nbsp;</p>
<p>&nbsp;<img src="57388-src2.jpg" alt="" width="529" height="435"></p>
<h1><strong>Code</strong></h1>
<h1>&nbsp;
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">   ''' &lt;summary&gt;
    ''' use odbcdataadapter 
    ''' &lt;/summary&gt;
    ''' &lt;param name=&quot;strSQL&quot;&gt;query&lt;/param&gt;
    ''' &lt;returns&gt;successfull&lt;/returns&gt;
    ''' &lt;remarks&gt;read database and bring data to form&lt;/remarks&gt;
    Private Function dbAccess(ByVal strSQL As String) As Boolean

        Dim tab As String = &quot;Table0&quot;
        Dim adapter As OdbcDataAdapter
        'Connector/ODBC 3.51 connection string
        Dim MyConString As String = &quot;DRIVER={MySQL ODBC 3.51 Driver};&quot; &amp; _
        &quot;SERVER=14.177.224.53;&quot; &amp; _
        &quot;DATABASE=result;&quot; &amp; _
        &quot;USER=username;&quot; &amp; _
        &quot;PASSWORD=password;&quot; &amp; _
        &quot;OPTION=2;&quot;

        Try
            adapter = New OdbcDataAdapter(strSQL, MyConString)
            adapter.MissingMappingAction = MissingMappingAction.Passthrough
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey
            stopwatch.Start()
        Catch ex As Exception
            MsgBox(&quot;connection: &quot; &amp; ex.Message)
            Return False
        End Try

        'checkbox overwrite mode
        If Me.CheckBox1.Checked Then
            For Each tbl As DataTable In DataSet1.Tables
                tbl.Rows.Clear()
                tbl.Columns.Clear()
                tbl.Clear()
            Next
        End If

        'fill table now
        Try
            adapter.Fill(DataSet1, tab)
        Catch ex As Exception
            MsgBox(&quot;fill: &quot; &amp; ex.Message)
            Return False
        End Try

        'binding now
        Me.DataGridView1.DataSource = DataSet1
        Me.DataGridView1.DataMember = tab

        stopTime()

        Return True
    End Function</pre>
<div class="preview">
<pre class="vb">&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'''&nbsp;&lt;summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'''&nbsp;use&nbsp;odbcdataadapter&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'''&nbsp;&lt;/summary&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'''&nbsp;&lt;param&nbsp;name=&quot;strSQL&quot;&gt;query&lt;/param&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'''&nbsp;&lt;returns&gt;successfull&lt;/returns&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'''&nbsp;&lt;remarks&gt;read&nbsp;database&nbsp;and&nbsp;bring&nbsp;data&nbsp;to&nbsp;form&lt;/remarks&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Private</span>&nbsp;<span class="visualBasic__keyword">Function</span>&nbsp;dbAccess(<span class="visualBasic__keyword">ByVal</span>&nbsp;strSQL&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>)&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">Boolean</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;tab&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;<span class="visualBasic__string">&quot;Table0&quot;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;adapter&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;OdbcDataAdapter&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'Connector/ODBC&nbsp;3.51&nbsp;connection&nbsp;string</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;MyConString&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;=&nbsp;<span class="visualBasic__string">&quot;DRIVER={MySQL&nbsp;ODBC&nbsp;3.51&nbsp;Driver};&quot;</span>&nbsp;&amp;&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__string">&quot;SERVER=14.177.224.53;&quot;</span>&nbsp;&amp;&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__string">&quot;DATABASE=result;&quot;</span>&nbsp;&amp;&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__string">&quot;USER=username;&quot;</span>&nbsp;&amp;&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__string">&quot;PASSWORD=password;&quot;</span>&nbsp;&amp;&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__string">&quot;OPTION=2;&quot;</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Try</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;adapter&nbsp;=&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;OdbcDataAdapter(strSQL,&nbsp;MyConString)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;adapter.MissingMappingAction&nbsp;=&nbsp;MissingMappingAction.Passthrough&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;adapter.MissingSchemaAction&nbsp;=&nbsp;MissingSchemaAction.AddWithKey&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;stopwatch.Start()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Catch</span>&nbsp;ex&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Exception&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MsgBox(<span class="visualBasic__string">&quot;connection:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;ex.Message)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Return</span>&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Try</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'checkbox&nbsp;overwrite&nbsp;mode</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;<span class="visualBasic__keyword">Me</span>.CheckBox1.Checked&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;<span class="visualBasic__keyword">Each</span>&nbsp;tbl&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;DataTable&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;DataSet1.Tables&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tbl.Rows.Clear()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tbl.Columns.Clear()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;tbl.Clear()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'fill&nbsp;table&nbsp;now</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Try</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;adapter.Fill(DataSet1,&nbsp;tab)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Catch</span>&nbsp;ex&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;Exception&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MsgBox(<span class="visualBasic__string">&quot;fill:&nbsp;&quot;</span>&nbsp;&amp;&nbsp;ex.Message)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Return</span>&nbsp;<span class="visualBasic__keyword">False</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Try</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__com">'binding&nbsp;now</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.DataSource&nbsp;=&nbsp;DataSet1&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Me</span>.DataGridView1.DataMember&nbsp;=&nbsp;tab&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;stopTime()&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Return</span>&nbsp;<span class="visualBasic__keyword">True</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Function</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</h1>
<h1>&nbsp;Topic related links</h1>
<p>&nbsp;</p>
<p>MySQL Documentation: MySQL Reference Manuals<strong>&nbsp;</strong></p>
<p><a href="http://dev.mysql.com/doc/index.html">http://dev.mysql.com/doc/index.html</a></p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
