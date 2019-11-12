# My First Integration Services Solution
## Requires
- Visual Studio 2010
## License
- Apache License, Version 2.0
## Technologies
- SSIS
- SQL Server Integration Services
- SSIS 2012
- SQL Server Integration Services 2012
## Topics
- Getting Started
## Updated
- 07/01/2012
## Description

<h1>Introduction</h1>
<div>This sample serves as an introduction to the SQL Server Integration Services development environment. By following along with the steps in the video you'll create a simple data flow that transfers data from a CSV file into a table in SQL Server.</div>
<div></div>
<div>The solution attached to this sample is a copy of what you'll have at the end of the process of following along with this lesson.</div>
<h1><object width="350" height="300" data="data:application/x-silverlight-2," type="application/x-silverlight-2"> <param name="source" value="/Content/Common/videoplayer.xap" /> <param name="initParams" value="deferredLoad=false,duration=0,m=http://code.msdn.microsoft.com/site/view/file/60422/1/My%20First%20SSIS%20Package.wmv,autostart=false,autohide=true,showembed=true"
 /> <param name="background" value="#00FFFFFF" /> <param name="minRuntimeVersion" value="3.0.40624.0" /> <param name="enableHtmlAccess" value="true" /> <param name="src" value="/site/view/file/60422/1/My%20First%20SSIS%20Package.wmv" /> <param name="id" value="60422"
 /> <param name="name" value="My First SSIS Package.wmv" /><span><a href="http://go.microsoft.com/fwlink/?LinkID=149156" style="text-decoration:none"><img src="-?linkid=108181" alt="Get Microsoft Silverlight" style="border-style:none"></a></span>
 </object> <br>
<a id="x_/site/view/file/60422/1/My%20First%20SSIS%20Package.wmv" href="http://code.msdn.microsoft.com/site/view/file/60422/1/My%20First%20SSIS%20Package.wmv">Download video</a></h1>
<h1>Prerequisites</h1>
<div>Before attempting to follow along with the video you'll need to perform two points of configuration:</div>
<ul>
<li>Copy a data file to c:\SSIS . The file is named Product data.csv, and is contained within the sample archive.
</li><li>Create a table in a SQL Server database into which you have permission to insert data.
</li></ul>
<div>The table can be created by executing the following SQL in the context of the database:</div>
<div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>SQL</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">mysql</span>

<div class="preview">
<pre class="mysql"><span class="sql__keyword">CREATE</span>&nbsp;<span class="sql__keyword">TABLE</span>&nbsp;[<span class="sql__id">dbo</span>].[<span class="sql__id">Products</span>](&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">ProductCode</span>]&nbsp;[<span class="sql__keyword">int</span>]&nbsp;<span class="sql__keyword">NOT</span>&nbsp;<span class="sql__value">NULL</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">ShippingWeight</span>]&nbsp;[<span class="sql__keyword">float</span>]&nbsp;<span class="sql__keyword">NOT</span>&nbsp;<span class="sql__value">NULL</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">ShippingLength</span>]&nbsp;[<span class="sql__keyword">float</span>]&nbsp;<span class="sql__keyword">NOT</span>&nbsp;<span class="sql__value">NULL</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">ShippingWidth</span>]&nbsp;[<span class="sql__keyword">float</span>]&nbsp;<span class="sql__keyword">NOT</span>&nbsp;<span class="sql__value">NULL</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">ShippingHeight</span>]&nbsp;[<span class="sql__keyword">float</span>]&nbsp;<span class="sql__keyword">NOT</span>&nbsp;<span class="sql__value">NULL</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">UnitCost</span>]&nbsp;[<span class="sql__keyword">float</span>]&nbsp;<span class="sql__keyword">NOT</span>&nbsp;<span class="sql__value">NULL</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[<span class="sql__id">PerOrder</span>]&nbsp;[<span class="sql__keyword">tinyint</span>]&nbsp;<span class="sql__keyword">NOT</span>&nbsp;<span class="sql__value">NULL</span>&nbsp;
)&nbsp;<span class="sql__keyword">ON</span>&nbsp;[<span class="sql__keyword">PRIMARY</span>]&nbsp;
&nbsp;
<span class="sql__id">GO</span>&nbsp;
</pre>
</div>
</div>
</div>
<h1 class="endscriptcode">Transcript</h1>
<p class="endscriptcode">If you'd prefer to read along rather than listen to the video, a rough transcipt is below:</p>
<p>I&rsquo;ve just created a new Integration Services project in Sql Server Development Tools.</p>
<p>The center pane is the design canvas. This is where I&rsquo;ll build the logic that performs my data integration.</p>
<p>The left pane contains the SSIS Toolbox. The toolbox contains the things I use to build the data integration logic.</p>
<p>In SSIS all the data integration logic one constructs is contained within &ldquo;Packages&rdquo;.</p>
<p>Since I&rsquo;ve just created this project, it has a default package named &ldquo;Package.dtsx&rdquo;, which is what I&rsquo;m looking at, now.</p>
<p>Each package contains a single &ldquo;Control Flow&rdquo; design surface.</p>
<p>You can tell that&rsquo;s what I&rsquo;m looking at because this tab is selected.</p>
<p>Now I&rsquo;ll add something to the design surface: A Data Flow Task.</p>
<p>I added the Data Flow task to the surface by double-clicking it in the toolbox, but I could have dragged and dropped it from the toolbox to the canvas if I wanted to place it somewhere specific.</p>
<p>This Data Flow task doesn&rsquo;t do anything, yet, because I haven&rsquo;t configured it.</p>
<p>I now double-click the Data Flow task to configure it.</p>
<p>Notice two things that just happened:</p>
<p>First, the Data Flow tab is now active.</p>
<p>Second, the toolbox contents have changed.</p>
<p>These are the Data Flow Components from which a Data Flow is constructed.</p>
<p>I&rsquo;ll now construct data flow logic that loads data from a CSV file into a table in SQL Server.</p>
<p>I start by adding the Source Adapter. I&rsquo;ll use the Source Assistant for this. I activate the Source Assistant by double-clicking it in the Toolbox.</p>
<p>I follow the instructions in the Source Assistant.</p>
<p>I pick the file that I want to use.</p>
<p>The rest of the information on this page looks correct. But I see this instruction to specify the Column information for my file, so I switch to the Columns page to review the columns SSIS has detected.</p>
<p>This information looks correct, as well, so I could click OK, now.</p>
<p>However, the Flat File Source adapter reads in the contents of files as strings, by default.</p>
<p>Since I know that I&rsquo;m going to be loading this data into a table in SQL Server that has typed columns, I want the source adapter to convert the strings it reads from the file into numbers.</p>
<p>So I switch to the Advanced page and tell SSIS to suggest data types by scanning the file.</p>
<p>I can review the suggestions here.</p>
<p>These look good, so I click OK.</p>
<p>Now a Flat File Source adapter has been created for me and placed on the canvas.</p>
<p>This represents the entry point of data into this data flow.</p>
<p><br>
Next I&rsquo;ll add a destination adapter which will define where data leaves the data flow. I&rsquo;ll use the Destination Assistant, again by double-clicking it in the toolbox.</p>
<p>I follow the instructions in the Assistant.</p>
<p>I&rsquo;ll be using my local server.</p>
<p>I specify the database.</p>
<p>Now a destination adapter has been added to the canvas.</p>
<p>I&rsquo;ll reposition it by dragging it.</p>
<p>At this point there&rsquo;s an error, which I can read by hovering my mouse over the error icon: The message indicates that a destination table has not been specified.</p>
<p>Before I edit the component to address this error I&rsquo;m going to connect it to the source of the data. Let me explain why:</p>
<p>Right now the destination adapter isn&rsquo;t associated with any source of data. These two components on the canvas don&rsquo;t know about each other. By connecting the source to the destination adapter, I enable the destination adapter to figure things
 out about how to configure itself. For this reason it&rsquo;s a good idea in general to connect components to the upstream sources of data before configuring them.</p>
<p>To connect the destination adapter to the source for its data, I first select the source. Notice the two arrows that appeared. The blue arrow represents the normal data output and the red arrow represents the error output. Ignore the error output, for now,
 since it&rsquo;s the normal data output I&rsquo;ll be using.</p>
<p>I click the output, move over onto the destination adapter, and click again to connect the two.</p>
<p>Now I configure the destination adapter.</p>
<p>I double-click the destination adapter to open its editor.</p>
<p>The instructional message at the bottom of this window tells me I need to specify the name of the table.</p>
<p>I pick the table.</p>
<p>Now the instructional message tells me that I need to provide a column mapping, so I switch to the mapping page.</p>
<p>A destination adapter typically knows about two sets of columns: The columns that enter it from upstream, and the columns that exist in the external data store that it will load data into, in this case the SQL Server table.</p>
<p>The mapping being defined here is between these two sets of columns.</p>
<p>You might recall that these columns names on the left side are the column names that I saw earlier in the Flat File Source adapter editor.</p>
<p><br>
The column names on the right came from the table I selected on the previous page of the destination adapter editor. To establish the mapping between the columns I can drag and drop the columns, or I can specify the mappings in the grid below.</p>
<p>My data flow is now defined, and ready to transfer data.</p>
<p><br>
To run the package inside the debugger, I click this &ldquo;Play&rdquo;<br>
button.</p>
<p>These green check marks indicate that each component has completed successfully. This count represents the number of rows that transferred along this path.</p>
<p>To leave debugging mode and return to package editing mode I click this &ldquo;Stop&rdquo; button.</p>
<p>REVIEW</p>
<p>Data Integration logic is defined on canvases using items from toolboxes.</p>
<p>Control Flow and Data Flow are defined separately, and Data Flow is defined within Data Flow tasks.</p>
<p>Within a Data Flow there are source adapters that bring data into the SSIS data flow as well as Destination adapters that export data out of the Data Flow to external data repositories.</p>
<p>The Data Integration logic can be run inside the development environment.</p>
</div>
