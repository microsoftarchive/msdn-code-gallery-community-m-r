# Reveal parameter values from a Command object
## Requires
- Visual Studio 2010
## License
- Apache License, Version 2.0
## Technologies
- SQL
- ADO.NET
- LINQ
- Visual Basic .NET
- VB.Net
- .NET Framework 4.0
- Library
- OLE DB
- WinForms
- DataTable
## Topics
- Data Binding
- SQL
- Data Access
- custom controls
- Data
- How to
## Updated
- 07/02/2014
## Description

<h1>Introduction</h1>
<p><span style="font-size:small">Shows how to reveal values of parameters from a command object, which implements IDbCommand interface such as OleDb and SqlClient classes. For developers using parameterized SQL commands already and understand language extension
 methods you can place the project PeekerForCommands into your solution and use it immediately.</span></p>
<p><span style="font-size:small"><img id="74966" src="74966-figure1.jpg" alt="" width="593" height="634"></span></p>
<h1><span>Building the Sample</span></h1>
<p><span style="font-size:small">As delivered you should be able to unzip the solution, compile, and run Demo and Demo2 projects within the solution. All other projects are support Class library projects which are used by Demo and Demo2. If you have any issues
 please let me know. The solution was created with VS2010, Client Framework 4 with Option Strict On, Option Infer On for all projects.</span></p>
<p><span style="font-size:20px; font-weight:bold">Description</span></p>
<p><span style="font-size:small">To read and write data to backend databases at some point an SQL statement is required. Many times these SQL statements are written by using string concatenation which when done perfectly will read or send data to a backend
 database. Many times these SQL statements are not perfect and will need to be redone. Whether perfect or not concatenating SQL statements within a string make maintaining these statements difficult and are hard to read. Several methods are shown in my MSDN
 article <a href="http://code.msdn.microsoft.com/Creating-SQL-statements-b3017132#content">
Creating SQL statements that are easy to create and maintain</a> that make creating SQL statements easier to write and maintain. This article shows how to use xml literals and embedded expressions to construct SQL statements. Although this makes maintenance
 easier, we are still open to what is called SQL injection, which may be a consideration for outward facing applications. There are links below, which explain SQL injection along with several ideas to prevent this.</span></p>
<p><span style="font-size:small">I and many other developers will tell you to use parameterized SQL statements, which reduce your risk of
<a href="http://en.wikipedia.org/wiki/SQL_injection">SQL injection</a> (<a href="http://www.codeproject.com/Articles/9378/SQL-Injection-Attacks-and-Some-Tips-on-How-to-Prev">Code Project article</a>) along with properly formatting parameters in your SQL statements
 invoked in a Command object. Each Command (i.e. OleDbCommand, SqlClientCommand) object has a ParameterCollection.</span></p>
<p><span style="font-size:small">When a SQL statement is created via using parameters you cannot easily view the values in the SQL statement unless you write a function that will replace parameter placeholders with values set for each parameter. This may deter
 developers from using parameterized SQL statements along with the extra typing one has to do, seem like to much work? It all depends on how important protecting your data is along with writing tight SQL statements.</span></p>
<p><span style="font-size:small">In the attached VS2010 solution I introduce a language extension method for
<a href="http://msdn.microsoft.com/en-us/library/system.data.idbcommand.aspx">IDbCommand interface</a> that will return values rather than placeholders so that when an SQL statement does not work correctly you can copy the SQL statement produced by this language
 extension and a) examine the statement b) paste the statement into an SQL editor and debug it.</span></p>
<p><span style="font-size:small">In this solution there are several projects, one for reading data from a backend database known as DataAccess project which is used by a windows form project Demo2. Demo2 project shows how to do simple select statements using
 parameterized SQL along with a SQL statement which does not work. Normally as mentioned above when one of these SQL statements fail there is little you can do but in this example the language extension is invoked and writes the SQL statement with values to
 a text file and displayed to the developer. The SQL is only displayed if running from the IDE which is easy to change by removal of one if statement.</span></p>
<p><span style="font-size:small">The project Demo uses no backend database. There are three simulations done for parameterized SQL statements, which utilize a C Sharp project to syntax highlight SQL via my language extension.</span></p>
<p><span style="font-size:small">The following SQL statement is what a statement appears like when using a parameterized SQL statement queried via CommandText property of a command object. No values are shown so it makes it hard to determine what might be an
 issue when an SQL statement fails</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>SQL</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">mysql</span>
<pre class="hidden">INSERT INTO CustomerMaster (AccountNumber,UDNumeric1,CreatedDate,Comments) VALUES (@AccountNumber,@UDNumeric1, @CreatedDate, @Comments)</pre>
<div class="preview">
<pre class="mysql"><span class="sql__keyword">INSERT</span>&nbsp;<span class="sql__keyword">INTO</span>&nbsp;<span class="sql__id">CustomerMaster</span>&nbsp;(<span class="sql__id">AccountNumber</span>,<span class="sql__id">UDNumeric1</span>,<span class="sql__id">CreatedDate</span>,<span class="sql__id">Comments</span>)&nbsp;<span class="sql__keyword">VALUES</span>&nbsp;(<span class="sql__keyword">@</span><span class="sql__variable">AccountNumber</span>,<span class="sql__keyword">@</span><span class="sql__variable">UDNumeric1</span>,&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">CreatedDate</span>,&nbsp;<span class="sql__keyword">@</span><span class="sql__variable">Comments</span>)</pre>
</div>
</div>
</div>
<div class="scriptcode">The following language extension makes it possible to see values</div>
<div class="scriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">vb</span>
<pre class="hidden">&lt;System.Diagnostics.DebuggerStepThrough()&gt; _
&lt;Runtime.CompilerServices.Extension()&gt; _
Public Function ActualCommandTextByNames(
    ByVal sender As IDbCommand) As String
    Dim sb As New System.Text.StringBuilder(sender.CommandText)
    Dim EmptyParameterNames =
        (
            From T In sender.Parameters.Cast(Of IDataParameter)()
            Where String.IsNullOrWhiteSpace(T.ParameterName)
        ).FirstOrDefault

    If EmptyParameterNames IsNot Nothing Then
        Return sender.CommandText
    End If

    For Each p As IDataParameter In sender.Parameters

        Select Case p.DbType
            Case _
                DbType.AnsiString,
                DbType.AnsiStringFixedLength,
                DbType.Date, DbType.DateTime,
                DbType.DateTime2, DbType.Guid,
                DbType.String,
                DbType.StringFixedLength,
                DbType.Time,
                DbType.Xml
                If p.ParameterName(0) = &quot;@&quot; Then
                    If p.Value Is Nothing Then
                        Throw New Exception(&quot;no value given for parameter '&quot; &amp;
                                            p.ParameterName &amp; &quot;'&quot;)
                    End If
                    sb = sb.Replace(p.ParameterName, String.Format(&quot;'{0}'&quot;,
                                            p.Value.ToString.Replace(&quot;'&quot;, &quot;''&quot;)))
                Else
                    sb = sb.Replace(String.Concat(&quot;@&quot;, p.ParameterName),
                                    String.Format(&quot;'{0}'&quot;, p.Value.ToString.Replace(&quot;'&quot;, &quot;''&quot;)))
                End If
            Case Else
                sb = sb.Replace(p.ParameterName, p.Value.ToString)
        End Select
    Next
    Return sb.ToString
End Function</pre>
<div class="preview">
<pre class="vb">&lt;System.Diagnostics.DebuggerStepThrough()&gt;&nbsp;_&nbsp;
&lt;Runtime.CompilerServices.Extension()&gt;&nbsp;_&nbsp;
<span class="visualBasic__keyword">Public</span>&nbsp;<span class="visualBasic__keyword">Function</span>&nbsp;ActualCommandTextByNames(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">ByVal</span>&nbsp;sender&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;IDbCommand)&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">String</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;sb&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;System.Text.StringBuilder(sender.CommandText)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Dim</span>&nbsp;EmptyParameterNames&nbsp;=&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;From&nbsp;T&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;sender.Parameters.Cast(<span class="visualBasic__keyword">Of</span>&nbsp;IDataParameter)()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Where&nbsp;<span class="visualBasic__keyword">String</span>.IsNullOrWhiteSpace(T.ParameterName)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;).FirstOrDefault&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;EmptyParameterNames&nbsp;<span class="visualBasic__keyword">IsNot</span>&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Return</span>&nbsp;sender.CommandText&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">For</span>&nbsp;<span class="visualBasic__keyword">Each</span>&nbsp;p&nbsp;<span class="visualBasic__keyword">As</span>&nbsp;IDataParameter&nbsp;<span class="visualBasic__keyword">In</span>&nbsp;sender.Parameters&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Select</span>&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;p.DbType&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;_&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DbType.AnsiString,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DbType.AnsiStringFixedLength,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DbType.<span class="visualBasic__keyword">Date</span>,&nbsp;DbType.DateTime,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DbType.DateTime2,&nbsp;DbType.Guid,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DbType.<span class="visualBasic__keyword">String</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DbType.StringFixedLength,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DbType.Time,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DbType.Xml&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;p.ParameterName(<span class="visualBasic__number">0</span>)&nbsp;=&nbsp;<span class="visualBasic__string">&quot;@&quot;</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;p.Value&nbsp;<span class="visualBasic__keyword">Is</span>&nbsp;<span class="visualBasic__keyword">Nothing</span>&nbsp;<span class="visualBasic__keyword">Then</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Throw</span>&nbsp;<span class="visualBasic__keyword">New</span>&nbsp;Exception(<span class="visualBasic__string">&quot;no&nbsp;value&nbsp;given&nbsp;for&nbsp;parameter&nbsp;'&quot;</span>&nbsp;&amp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;p.ParameterName&nbsp;&amp;&nbsp;<span class="visualBasic__string">&quot;'&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sb&nbsp;=&nbsp;sb.Replace(p.ParameterName,&nbsp;<span class="visualBasic__keyword">String</span>.Format(<span class="visualBasic__string">&quot;'{0}'&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;p.Value.ToString.Replace(<span class="visualBasic__string">&quot;'&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;''&quot;</span>)))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sb&nbsp;=&nbsp;sb.Replace(<span class="visualBasic__keyword">String</span>.Concat(<span class="visualBasic__string">&quot;@&quot;</span>,&nbsp;p.ParameterName),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">String</span>.Format(<span class="visualBasic__string">&quot;'{0}'&quot;</span>,&nbsp;p.Value.ToString.Replace(<span class="visualBasic__string">&quot;'&quot;</span>,&nbsp;<span class="visualBasic__string">&quot;''&quot;</span>)))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">If</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Case</span>&nbsp;<span class="visualBasic__keyword">Else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sb&nbsp;=&nbsp;sb.Replace(p.ParameterName,&nbsp;p.Value.ToString)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Select</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Next</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="visualBasic__keyword">Return</span>&nbsp;sb.ToString&nbsp;
<span class="visualBasic__keyword">End</span>&nbsp;<span class="visualBasic__keyword">Function</span></pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
Now we see</div>
<div class="scriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>SQL</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">mysql</span>
<pre class="hidden">INSERT INTO CustomerMaster (AccountNumber,UDNumeric1,CreatedDate,Comments) VALUES (100,12, '1/21/2013 9:25:36 AM', '100')</pre>
<div class="preview">
<pre class="js">INSERT&nbsp;INTO&nbsp;CustomerMaster&nbsp;(AccountNumber,UDNumeric1,CreatedDate,Comments)&nbsp;VALUES&nbsp;(<span class="js__num">100</span>,<span class="js__num">12</span>,&nbsp;<span class="js__string">'1/21/2013&nbsp;9:25:36&nbsp;AM'</span>,&nbsp;<span class="js__string">'100'</span>)</pre>
</div>
</div>
</div>
<div class="endscriptcode">Each demo in Demo and Demo2 projects are extremely simple but don't let that fool you. What the language extension really shines is with complex SQL statements.</div>
<div class="endscriptcode"></div>
<div class="endscriptcode">The following extension (see usage in Demo2 project) might be considered a tad over the top, that is for you to decided. In short this is a language extension that was design to use for development of a project SQL, not for production.
 In short if there are no rows returned from a select statement that you expected rows this invokes the extension above.</div>
<div class="endscriptcode"></div>
<div class="endscriptcode">
<pre style="font-family:Consolas; background:white; color:black; font-size:13px"><span style="color:blue">Public</span>&nbsp;<span style="color:blue">Module</span>&nbsp;<span style="color:#2b91af">DataTableExtensions</span>
&nbsp;&nbsp;&nbsp;&nbsp;&lt;System.Diagnostics.<span style="color:#2b91af">DebuggerStepThrough</span>()&gt;&nbsp;_
&nbsp;&nbsp;&nbsp;&nbsp;&lt;Runtime.CompilerServices.<span style="color:#2b91af">Extension</span>()&gt;&nbsp;_
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue">Public</span>&nbsp;<span style="color:blue">Sub</span>&nbsp;DiagnoseBadSql(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue">ByVal</span>&nbsp;sender&nbsp;<span style="color:blue">As</span>&nbsp;<span style="color:#2b91af">DataTable</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue">ByVal</span>&nbsp;cmd&nbsp;<span style="color:blue">As</span>&nbsp;<span style="color:#2b91af">IDbCommand</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue">ByVal</span>&nbsp;FileName&nbsp;<span style="color:blue">As</span>&nbsp;<span style="color:blue">String</span>,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue">ByVal</span>&nbsp;DisplayFile&nbsp;<span style="color:blue">As</span>&nbsp;<span style="color:blue">Boolean</span>)
 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue">If</span>&nbsp;sender.Rows.Count&nbsp;=&nbsp;0&nbsp;<span style="color:blue">Then</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;cmd.ActualCommandTextToFile(FileName,&nbsp;<span style="color:blue">True</span>)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue">If</span>&nbsp;DisplayFile&nbsp;<span style="color:blue">Then</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af">Process</span>.Start(FileName)
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue">End</span>&nbsp;<span style="color:blue">If</span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue">End</span>&nbsp;<span style="color:blue">If</span>
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue">End</span>&nbsp;<span style="color:blue">Sub</span>
<span style="color:blue">End</span>&nbsp;<span style="color:blue">Module</span>
</pre>
</div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
</div>
<div class="scriptcode"></div>
<h1>More Information</h1>
<ul>
<li><span style="font-size:small">Syntax highlighter project is not mine, author credited in about.txt</span>
</li><li><span style="font-size:small">PeekerForCommand project has all that is needed to reveal parameter values.</span>
</li><li><span style="font-size:small">07/02/2014 added a C# solution which is very striaght forward and to the point.</span>
</li></ul>
