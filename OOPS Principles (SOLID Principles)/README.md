# OOPS Principles (SOLID Principles)
## Requires
- Visual Studio 2010
## License
- MS-LPL
## Technologies
- Silverlight
- ASP.NET
- Windows Forms
- WPF
- ASP.NET MVC
- ASP.NET Web Forms
- MVVM
- Design Patterns
## Topics
- ioc
- Dependency Injection
- Inversion of Control
- OOPS Principles
- SOLID Principles
- Single Responsibility Principle
- Open Closed Principle
- Liskov Substitution Principle
- Interface Segregation Principle
- Dependency Inversion Principle
- Dependency Injection Pattern
- Inversion of Control principle
- OOPS
- SOLID
## Updated
- 04/30/2013
## Description

<div style="line-height:150%"><span style="color:green; line-height:150%; font-family:calibri; font-size:14pt">Targeted Audience:</span></div>
<div style="line-height:150%; text-indent:-0.25in; margin-left:0.25in"><span style="line-height:150%; font-family:calibri; font-size:11pt">1.<span style="line-height:normal; font-family:&quot;times new roman&quot;; font-size:7pt">&nbsp;&nbsp;&nbsp;</span></span><span style="line-height:150%; font-family:calibri; font-size:11pt">.NET
 Architects</span></div>
<div style="line-height:150%; text-indent:-0.25in; margin-left:0.25in"><span style="line-height:150%; font-family:calibri; font-size:11pt">2.<span style="line-height:normal; font-family:&quot;times new roman&quot;; font-size:7pt">&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style="line-height:150%; font-family:calibri; font-size:11pt">.NET
 Application Designers</span></div>
<div style="line-height:150%; text-indent:-0.25in; margin-left:0.25in"><span style="line-height:150%; font-family:calibri; font-size:11pt">3.<span style="line-height:normal; font-family:&quot;times new roman&quot;; font-size:7pt">&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style="line-height:150%; font-family:calibri; font-size:11pt">.NET
 Application Developers</span></div>
<div style="line-height:150%"><span style="color:green; line-height:150%; font-family:calibri; font-size:14pt">Prerequisites:</span></div>
<div style="line-height:150%; text-indent:-0.25in; margin-left:0.25in"><span style="line-height:150%; font-family:calibri; font-size:11pt">1.<span style="line-height:normal; font-family:&quot;times new roman&quot;; font-size:7pt">&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style="line-height:150%; font-family:calibri; font-size:11pt">OOPS</span></div>
<div style="line-height:150%; text-indent:-0.25in; margin-left:0.25in"><span style="line-height:150%; font-family:calibri; font-size:11pt">2.<span style="line-height:normal; font-family:&quot;times new roman&quot;; font-size:7pt">&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style="line-height:150%; font-family:calibri; font-size:11pt">Any
 OOPS Programming language</span></div>
<div style="line-height:150%; text-indent:-0.25in; margin-left:0.25in"><span style="color:green; line-height:150%; font-family:calibri; font-size:14pt">Introduction:</span><span style="color:#000000; line-height:150%; font-family:calibri; font-size:small"><span style="font-family:calibri">&nbsp;</span></span>&nbsp;<span style="color:maroon; font-family:calibri; font-size:11pt">&nbsp;</span></div>
<div><span style="font-family:calibri; font-size:11pt">As we all know that using Object Oriented Programming System (OOPS) concepts and using classes, objects will not show that we are writing efficient code in our applications. Without knowing OOPS principles
 we will be using OOPS and facing problems in&nbsp;maintaing and enhancing the code in&nbsp;our applications.&nbsp;So w</span><span style="font-family:calibri; font-size:11pt">e should know how to implement OOPS and use them in right manner, that is where 5
 Object Oriented Principles&nbsp;(also called as&nbsp;SOLID Principles) comes into picture.&nbsp;</span><span style="font-family:calibri; font-size:11pt">Let us go through each principle and understand how to implement classes and objects in our real time applications.</span></div>
<div><span style="font-family:calibri; font-size:11pt">
<div style="line-height:150%; text-indent:-0.25in; margin-left:0.25in"><span style="color:green; line-height:150%; font-family:calibri; font-size:14pt">Single Responsibility Principle:</span></div>
<div><span style="color:maroon; font-family:calibri; font-size:11pt">One Class should be responsible for one task.</span></div>
</span>
<div>&nbsp;</div>
</div>
<div><span style="font-family:calibri; font-size:11pt">E.g. </span></div>
<div><span style="font-family:calibri; font-size:11pt">Want to Insert data into database and want to log the details.</span></div>
<div><span style="font-family:calibri; font-size:11pt">If we create a class to represent DataAccess, it should not be used to save to the database (task 1), as well as&nbsp;log the details&nbsp;(task 2).</span></div>
<div><span style="font-family:calibri; font-size:11pt">&nbsp;</span></div>
<div><span style="font-family:calibri; font-size:11pt">Problem:</span></div>
<div><span style="font-family:calibri; font-size:11pt">If we want to change database type or we want to change the logger type/location,&nbsp;If both tasks present in single class,&nbsp;one task&nbsp;changes affects to&nbsp;another.
</span></div>
<div><span style="font-family:calibri; font-size:11pt">&nbsp;</span></div>
<div><span style="font-family:calibri; font-size:11pt">Solution:</span></div>
<div><span style="font-family:calibri; font-size:11pt">Create one class for saving into database and another class for logging the details.</span></div>
<div><span style="font-family:calibri; font-size:11pt">&nbsp;</span>&nbsp;</div>
<div><span style="font-family:calibri; font-size:11pt">Exmple:</span></div>
<div><span style="font-family:calibri; font-size:11pt">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__com">//1.&nbsp;Single&nbsp;Responsibility&nbsp;Principle</span>&nbsp;
&nbsp;
<span class="cs__com">//&nbsp;Data&nbsp;access&nbsp;class&nbsp;is&nbsp;only&nbsp;responsible&nbsp;for&nbsp;data&nbsp;base&nbsp;related&nbsp;operations</span>&nbsp;
<span class="cs__keyword">class</span>&nbsp;DataAccess&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;InsertData()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Data&nbsp;inserted&nbsp;into&nbsp;database&nbsp;successfully&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
<span class="cs__com">//&nbsp;Logger&nbsp;class&nbsp;is&nbsp;only&nbsp;responsible&nbsp;for&nbsp;logging&nbsp;related&nbsp;operations</span>&nbsp;
<span class="cs__keyword">class</span>&nbsp;Logger&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;WriteLog()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(&nbsp;<span class="cs__string">&quot;Logged&nbsp;Time:&quot;</span>&nbsp;&nbsp;&#43;&nbsp;DateTime.Now.ToLongTimeString()&nbsp;&#43;&nbsp;<span class="cs__string">&quot;&nbsp;Log&nbsp;&nbsp;Data&nbsp;insertion&nbsp;completed&nbsp;successfully&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}</pre>
</div>
</div>
</div>
</span>
<div class="endscriptcode"><span style="color:green; line-height:150%; font-family:calibri; font-size:14pt">Open Closed Principle:</span></div>
</div>
<div>&nbsp;</div>
<div><span style="color:maroon; font-family:calibri; font-size:11pt">Classes, modules, functions, etc. should be open for extension, but closed for modification.
</span></div>
<div><span style="font-family:calibri; font-size:11pt">&nbsp;</span></div>
<div><span style="font-family:calibri; font-size:11pt">Create a Base class with Required functionality, and ensure we will not modify that class. (Closed for modification)</span></div>
<div><span style="font-family:calibri; font-size:11pt">Create a Derived class by inheriting the Base class for extension (Open for modification)</span></div>
<div><span style="font-family:calibri; font-size:11pt">&nbsp;</span></div>
<div><span style="font-family:calibri; font-size:11pt">Example:</span></div>
<div><span style="font-family:calibri; font-size:11pt">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__com">//2.&nbsp;Open&nbsp;Close&nbsp;Principle</span>&nbsp;
<span class="cs__com">//&nbsp;Here&nbsp;DataProvder&nbsp;is&nbsp;open&nbsp;for&nbsp;extension&nbsp;(extends&nbsp;to&nbsp;Sql,&nbsp;Oracle,&nbsp;Oledb&nbsp;Providers&nbsp;and&nbsp;so&nbsp;on..)&nbsp;and&nbsp;closed&nbsp;for&nbsp;manipulation</span>&nbsp;
<span class="cs__keyword">abstract</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;DataProvider&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">abstract</span>&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;OpenConnection();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">abstract</span>&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;CloseConnection();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">abstract</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;ExecuteCommand();&nbsp;
}&nbsp;
<span class="cs__keyword">class</span>&nbsp;SqlDataProvider&nbsp;:&nbsp;DataProvider&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;OpenConnection()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;\nSql&nbsp;Connection&nbsp;opened&nbsp;successfully&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;CloseConnection()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Sql&nbsp;Connection&nbsp;closed&nbsp;successfully&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;ExecuteCommand()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Sql&nbsp;Command&nbsp;Executed&nbsp;successfully&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
<span class="cs__keyword">class</span>&nbsp;OracleDataProvider&nbsp;:&nbsp;DataProvider&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;OpenConnection()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Oracle&nbsp;Connection&nbsp;opened&nbsp;successfully&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;CloseConnection()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Oracle&nbsp;Connection&nbsp;closed&nbsp;successfully&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;ExecuteCommand()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Oracle&nbsp;Command&nbsp;Executed&nbsp;successfully&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
&nbsp;
<span class="cs__keyword">class</span>&nbsp;OledbDataProvider&nbsp;:&nbsp;DataProvider&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;OpenConnection()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;OLEDB&nbsp;Connection&nbsp;opened&nbsp;successfully&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;CloseConnection()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;OLEDB&nbsp;Connection&nbsp;closed&nbsp;successfully&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;ExecuteCommand()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;OEDB&nbsp;Command&nbsp;Executed&nbsp;successfully&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
<span class="cs__keyword">class</span>&nbsp;OpenClosePrincipleDemo&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;OSPDemo()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;\n\nOpen&nbsp;Close&nbsp;Principle&nbsp;Demo&nbsp;&quot;</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DataProvider&nbsp;DataProviderObject&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;SqlDataProvider();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DataProviderObject.OpenConnection();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DataProviderObject.ExecuteCommand();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DataProviderObject.CloseConnection();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DataProviderObject&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;OracleDataProvider();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DataProviderObject.OpenConnection();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DataProviderObject.ExecuteCommand();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DataProviderObject.CloseConnection();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</span>
<div class="endscriptcode"><span style="color:green; line-height:150%; font-family:calibri; font-size:14pt">Liskov Substitution Principle:</span></div>
</div>
<div>&nbsp;</div>
<div><span style="color:#000000; font-family:calibri; font-size:11pt">This principle is just an extension of the Open Close Principle.&nbsp;</span></div>
<div><span style="color:#000000; font-family:calibri; font-size:11pt">It means that we must make sure that new derived classes are extending the base classes without changing their behavior.</span></div>
<div><span style="color:maroon; font-family:calibri; font-size:11pt">&nbsp;</span></div>
<div><span style="color:maroon; font-family:calibri; font-size:11pt">Functions that use pointers or references to base classes must be able to use objects of derived classes without knowing it.
</span></div>
<div><span style="font-family:calibri; font-size:11pt">&nbsp;</span></div>
<div><span style="font-family:calibri; font-size:11pt">&nbsp;Or</span>&nbsp;</div>
<div><span style="font-family:calibri; font-size:11pt">&nbsp;</span>&nbsp;</div>
<div><span style="color:#800000; font-family:calibri; font-size:11pt">If any module is using a Base class then the reference to that Base class can be replaced with a Derived class without affecting the functionality of the module.</span></div>
<div><span style="font-family:calibri; font-size:11pt">&nbsp;</span></div>
<div><span style="font-family:calibri; font-size:11pt">Or</span></div>
<div><span style="color:#800000; font-family:calibri; font-size:11pt">&nbsp;</span></div>
<div><span style="color:#800000; font-family:calibri; font-size:11pt">While implementing derived classes, need to ensure that the&nbsp;derived classes just extend the functionality of base classes without replacing the functionality of base classes.</span></div>
<div><span style="font-family:calibri; font-size:11pt">&nbsp;</span></div>
<div><span style="font-family:calibri; font-size:11pt">E.g. </span></div>
<div><span style="font-family:calibri; font-size:11pt">If we are calling a method defined at a base class upon an abstracted class, the function must be implemented properly on the subtype class.</span></div>
<div><span style="font-family:calibri; font-size:11pt">&nbsp;</span></div>
<div><span style="font-family:calibri; font-size:11pt">Example: </span></div>
<div><span style="font-family:calibri; font-size:11pt">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__com">//&nbsp;If&nbsp;any&nbsp;module&nbsp;is&nbsp;using&nbsp;a&nbsp;Base&nbsp;class&nbsp;then&nbsp;the&nbsp;reference&nbsp;to&nbsp;that&nbsp;Base&nbsp;class&nbsp;can&nbsp;be&nbsp;replaced&nbsp;with&nbsp;a&nbsp;Derived&nbsp;class&nbsp;without&nbsp;affecting&nbsp;the&nbsp;functionality&nbsp;of&nbsp;the&nbsp;module.</span>&nbsp;
&nbsp;
<span class="cs__com">//&nbsp;Or</span>&nbsp;
&nbsp;
<span class="cs__com">//&nbsp;While&nbsp;implementing&nbsp;derived&nbsp;classes,&nbsp;one&nbsp;needs&nbsp;to&nbsp;ensure&nbsp;that,&nbsp;derived&nbsp;classes&nbsp;just&nbsp;extend&nbsp;the&nbsp;functionality&nbsp;of&nbsp;base&nbsp;classes&nbsp;without&nbsp;replacing&nbsp;the&nbsp;functionality&nbsp;of&nbsp;base&nbsp;classes.&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
<span class="cs__keyword">class</span>&nbsp;Rectangle&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">protected</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;mWidth&nbsp;=&nbsp;<span class="cs__number">0</span>&nbsp;;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">protected</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;mHeight&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">virtual</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;SetWidth(<span class="cs__keyword">int</span>&nbsp;width)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;mWidth&nbsp;=&nbsp;width;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">virtual</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;SetHeight(<span class="cs__keyword">int</span>&nbsp;height)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;mHeight&nbsp;=&nbsp;height;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">virtual</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;GetArea()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;mWidth&nbsp;*&nbsp;mHeight;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
&nbsp;
<span class="cs__com">//&nbsp;While&nbsp;implementing&nbsp;derived&nbsp;class&nbsp;if&nbsp;one&nbsp;replaces&nbsp;the&nbsp;functionality&nbsp;of&nbsp;base&nbsp;class&nbsp;then,&nbsp;</span>&nbsp;
<span class="cs__com">//&nbsp;it&nbsp;might&nbsp;results&nbsp;into&nbsp;undesired&nbsp;side&nbsp;effects&nbsp;when&nbsp;such&nbsp;derived&nbsp;classes&nbsp;are&nbsp;used&nbsp;in&nbsp;existing&nbsp;program&nbsp;modules.</span>&nbsp;
<span class="cs__keyword">class</span>&nbsp;Square&nbsp;:&nbsp;Rectangle&nbsp;
{&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;This&nbsp;class&nbsp;modifies&nbsp;the&nbsp;base&nbsp;class&nbsp;functionality&nbsp;instead&nbsp;of&nbsp;extending&nbsp;the&nbsp;base&nbsp;class&nbsp;functionality</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Now&nbsp;below&nbsp;methods&nbsp;implementation&nbsp;will&nbsp;impact&nbsp;base&nbsp;class&nbsp;functionality.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;SetWidth(<span class="cs__keyword">int</span>&nbsp;width)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;mWidth&nbsp;=&nbsp;width;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;mHeight&nbsp;=&nbsp;width;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;SetHeight(<span class="cs__keyword">int</span>&nbsp;height)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;mWidth&nbsp;=&nbsp;height;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;mHeight&nbsp;=&nbsp;height;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
&nbsp;
<span class="cs__keyword">class</span>&nbsp;LiskovSubstitutionPrincipleDemo&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;Rectangle&nbsp;CreateInstance()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;As&nbsp;per&nbsp;Liskov&nbsp;Substitution&nbsp;Principle&nbsp;&quot;Derived&nbsp;types&nbsp;must&nbsp;be&nbsp;completely&nbsp;substitutable&nbsp;for&nbsp;their&nbsp;base&nbsp;types&quot;.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">bool</span>&nbsp;SomeCondition&nbsp;=&nbsp;<span class="cs__keyword">false</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(SomeCondition&nbsp;==&nbsp;<span class="cs__keyword">true</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">new</span>&nbsp;Rectangle();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">new</span>&nbsp;Square();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;LSPDemo()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;\n\nLiskov&nbsp;Substitution&nbsp;Principle&nbsp;Demo&nbsp;&quot;</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Rectangle&nbsp;RectangleObject&nbsp;=&nbsp;CreateInstance();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;User&nbsp;assumes&nbsp;that&nbsp;RectangleObject&nbsp;is&nbsp;a&nbsp;rectangle&nbsp;and&nbsp;(s)he&nbsp;is&nbsp;able&nbsp;to&nbsp;set&nbsp;the&nbsp;width&nbsp;and&nbsp;height&nbsp;as&nbsp;for&nbsp;the&nbsp;base&nbsp;class</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;RectangleObject.SetWidth(<span class="cs__number">5</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;RectangleObject.SetHeight(<span class="cs__number">10</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Now&nbsp;this&nbsp;results&nbsp;into&nbsp;the&nbsp;area&nbsp;100&nbsp;(10&nbsp;*&nbsp;10&nbsp;)&nbsp;instead&nbsp;of&nbsp;50&nbsp;(10&nbsp;*&nbsp;5).</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Liskov&nbsp;Substitution&nbsp;Principle&nbsp;has&nbsp;been&nbsp;violated&nbsp;and&nbsp;returned&nbsp;wrong&nbsp;result&nbsp;:&nbsp;&quot;</span>&nbsp;&#43;&nbsp;RectangleObject.GetArea());&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;So&nbsp;once&nbsp;again&nbsp;I&nbsp;repaet&nbsp;that&nbsp;sub&nbsp;classes&nbsp;should&nbsp;extend&nbsp;the&nbsp;functionality,&nbsp;sub&nbsp;classes&nbsp;functionality&nbsp;should&nbsp;not&nbsp;impact&nbsp;base&nbsp;class&nbsp;functionality.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}</pre>
</div>
</div>
</div>
</span>
<div class="endscriptcode"><span style="color:green; line-height:150%; font-family:calibri; font-size:14pt">Interface Segregation Principle:</span></div>
</div>
<div>&nbsp;</div>
<div><span style="color:maroon; font-family:calibri; font-size:11pt">Clients should not be forced to depend upon interfaces that they do not use.
</span></div>
<div><span style="font-family:calibri; font-size:11pt">&nbsp;</span></div>
<div><span style="font-family:calibri; font-size:11pt">E.g.</span></div>
<div><span style="font-family:calibri; font-size:11pt">When a client depends upon a class that contains interfaces that the client does not use, but that other clients do use, then that client will be affected by the changes that those other clients force upon
 the class</span></div>
<div><span style="font-family:calibri; font-size:11pt">&nbsp;</span></div>
<div><span style="font-family:calibri; font-size:11pt">Base Interface:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 IDataProvider</span></div>
<div><span style="font-family:calibri; font-size:11pt">Derived Interfaces:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ISqlDataProvider, IOracleDataProvider</span></div>
<div><span style="font-family:calibri; font-size:11pt">Derived Classes:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; SqlDataClient, OracleDataClient</span></div>
<div><span style="font-family:calibri; font-size:11pt">&nbsp;</span></div>
<div><span style="font-family:calibri; font-size:11pt">Each Derived class should implement functions from their respective interfaces.</span></div>
<div><span style="font-family:calibri; font-size:11pt">No derived interface force other derived classes to implement the functionalities which they won't use.</span></div>
<div><span style="font-family:calibri; font-size:11pt">&nbsp;</span></div>
<div><span style="font-family:calibri; font-size:11pt">Example:</span></div>
<div><span style="font-family:calibri; font-size:11pt">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__com">//&nbsp;Only&nbsp;common&nbsp;generic&nbsp;methods&nbsp;exists&nbsp;for&nbsp;all&nbsp;derived&nbsp;classes.</span>&nbsp;
<span class="cs__keyword">interface</span>&nbsp;IDataProvider&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;OpenConnection();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;CloseConnection();&nbsp;
}&nbsp;
<span class="cs__com">//&nbsp;Implement&nbsp;methods&nbsp;specific&nbsp;to&nbsp;the&nbsp;respective&nbsp;derived&nbsp;classes</span>&nbsp;
<span class="cs__keyword">interface</span>&nbsp;ISqlDataProvider&nbsp;:&nbsp;IDataProvider&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;ExecuteSqlCommand();&nbsp;
}&nbsp;
<span class="cs__com">//&nbsp;Implement&nbsp;methods&nbsp;specific&nbsp;to&nbsp;the&nbsp;respective&nbsp;derived&nbsp;classes</span>&nbsp;
<span class="cs__keyword">interface</span>&nbsp;IOracleDataProvider&nbsp;:&nbsp;IDataProvider&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;ExecuteOracleCommand();&nbsp;
}&nbsp;
<span class="cs__com">//&nbsp;Client&nbsp;1</span>&nbsp;
<span class="cs__com">//&nbsp;Should&nbsp;not&nbsp;force&nbsp;SqlDataProvider&nbsp;client&nbsp;to&nbsp;implement&nbsp;ExecuteOracleCommand,&nbsp;as&nbsp;it&nbsp;wont&nbsp;required&nbsp;that&nbsp;method&nbsp;to&nbsp;be&nbsp;implemented.</span>&nbsp;
<span class="cs__com">//&nbsp;So&nbsp;that&nbsp;we&nbsp;will&nbsp;derive&nbsp;ISqlDataProvider&nbsp;not&nbsp;IOracleDataProvider</span>&nbsp;
<span class="cs__keyword">class</span>&nbsp;SqlDataClient&nbsp;:&nbsp;ISqlDataProvider&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;OpenConnection()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;\nSql&nbsp;Connection&nbsp;opened&nbsp;successfully&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;CloseConnection()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Sql&nbsp;Connection&nbsp;closed&nbsp;successfully&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Implemeting&nbsp;ISqlDataProvider,&nbsp;we&nbsp;are&nbsp;not&nbsp;forcing&nbsp;the&nbsp;client&nbsp;to&nbsp;implement&nbsp;IOracleDataProvider</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;ExecuteSqlCommand()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Sql&nbsp;Server&nbsp;specific&nbsp;Command&nbsp;Executed&nbsp;successfully&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
<span class="cs__com">//&nbsp;Client&nbsp;2</span>&nbsp;
<span class="cs__com">//&nbsp;Should&nbsp;not&nbsp;force&nbsp;OracleDataProvider&nbsp;client&nbsp;to&nbsp;implement&nbsp;ExecuteSqlCommand,&nbsp;as&nbsp;it&nbsp;wont&nbsp;required&nbsp;that&nbsp;method&nbsp;to&nbsp;be&nbsp;implemented.</span>&nbsp;
<span class="cs__com">//&nbsp;So&nbsp;that&nbsp;we&nbsp;will&nbsp;derive&nbsp;IOracleDataProvider&nbsp;not&nbsp;ISqlDataProvider</span>&nbsp;
<span class="cs__keyword">class</span>&nbsp;OracleDataClient&nbsp;:&nbsp;IOracleDataProvider&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;OpenConnection()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;\nOracle&nbsp;Connection&nbsp;opened&nbsp;successfully&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;CloseConnection()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Oracle&nbsp;Connection&nbsp;closed&nbsp;successfully&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Implemeting&nbsp;IOracleDataProvider,&nbsp;we&nbsp;are&nbsp;not&nbsp;forcing&nbsp;the&nbsp;client&nbsp;to&nbsp;implement&nbsp;ISqlDataProvider</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;ExecuteOracleCommand()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Oracle&nbsp;specific&nbsp;Command&nbsp;Executed&nbsp;successfully&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__number">1</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
<span class="cs__keyword">class</span>&nbsp;InterfaceSegregationPrincipleDemo&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;ISPDemo()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;\n\nInterface&nbsp;Inversion&nbsp;Principle&nbsp;Demo&nbsp;&quot;</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Each&nbsp;client&nbsp;will&nbsp;implement&nbsp;their&nbsp;respective&nbsp;methods&nbsp;no&nbsp;base&nbsp;class&nbsp;forces&nbsp;the&nbsp;other&nbsp;client&nbsp;to&nbsp;implement&nbsp;the&nbsp;methods&nbsp;which&nbsp;dont&nbsp;required.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;From&nbsp;the&nbsp;above&nbsp;implementation,&nbsp;we&nbsp;are&nbsp;not&nbsp;forcing&nbsp;Sql&nbsp;client&nbsp;to&nbsp;implemnt&nbsp;orcale&nbsp;logic&nbsp;or&nbsp;Oracle&nbsp;client&nbsp;to&nbsp;implement&nbsp;sql&nbsp;logic.</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ISqlDataProvider&nbsp;SqlDataProviderObject&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;SqlDataClient();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SqlDataProviderObject.OpenConnection();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SqlDataProviderObject.ExecuteSqlCommand();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;SqlDataProviderObject.CloseConnection();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IOracleDataProvider&nbsp;OracleDataProviderObject&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;OracleDataClient();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;OracleDataProviderObject.OpenConnection();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;OracleDataProviderObject.ExecuteOracleCommand();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;OracleDataProviderObject.CloseConnection();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</span>
<div class="endscriptcode"><span style="color:green; line-height:150%; font-family:calibri; font-size:14pt">Dependency Inversion Principle:</span></div>
</div>
<div>&nbsp;</div>
<div><span style="color:maroon; font-family:calibri; font-size:11pt">A: High level modules should not depend upon low level modules. Both should depend upon abstractions.</span></div>
<div><span style="color:maroon; font-family:calibri; font-size:11pt">B: Abstractions should not depend upon details. Details should depend upon abstractions.</span></div>
<div><span style="font-family:calibri; font-size:11pt">&nbsp;</span></div>
<div><span style="font-family:calibri; font-size:11pt">By passing dependencies to classes as abstractions, you remove the need to program dependency specific.&nbsp;</span></div>
<div></div>
<div><span style="color:#808000; font-family:calibri; font-size:11pt">Also referred as<strong> IoC</strong> Inversion of Control principle and implements as
<strong>DI</strong> Dependency Injection Pattern in programming world.</span></div>
<div><span style="font-family:calibri; font-size:11pt">&nbsp;</span></div>
<div><span style="font-family:calibri; font-size:11pt">E.g:</span></div>
<div><span style="font-family:calibri; font-size:11pt">An Employee class that needs to be able to save to XML and a database.
</span></div>
<div><span style="font-family:calibri; font-size:11pt">&nbsp;</span></div>
<div><span style="color:#008000; font-family:calibri; font-size:11pt">Problem 1:</span></div>
<div><span style="font-family:calibri; font-size:11pt">If we placed ToXML() and ToDB() functions in the class, we'd be violating the single responsibility principle.
</span></div>
<div><span style="font-family:calibri; font-size:11pt">&nbsp;</span></div>
<div><span style="color:#008000; font-family:calibri; font-size:11pt">Problem 2:</span></div>
<div><span style="font-family:calibri; font-size:11pt">If we created a function that took a value that represented whether to print to XML or to DB, we'd be hard-coding a set of devices and thus be violating the open closed principle.
</span></div>
<div><span style="color:#008000; font-family:calibri; font-size:11pt">&nbsp;</span></div>
<div><span style="color:#008000; font-family:calibri; font-size:11pt">Solution:</span></div>
<div><span style="font-family:calibri; font-size:11pt">&nbsp;</span></div>
<div><span style="font-family:calibri; font-size:11pt">1. Create an abstract class named 'DataWriter' that can be inherited from 'XMLDataWriter' and 'DbDataWriter'.</span></div>
<div><span style="font-family:calibri; font-size:11pt">2. Then Create a class named 'EmployeeWriter' that would expose an Output 'DataWriter saveMethod' that accepts a dependency as an argument.
</span></div>
<div><span style="font-family:calibri; font-size:11pt">&nbsp;</span></div>
<div><span style="font-family:calibri; font-size:11pt">See how the Output method is dependent upon the abstractions just as the output types are?
</span></div>
<div><span style="font-family:calibri; font-size:11pt">&nbsp;</span></div>
<div><span style="font-family:calibri; font-size:11pt">The dependencies have been inverted.
</span></div>
<div><span style="font-family:calibri; font-size:11pt">&nbsp;</span></div>
<div><span style="font-family:calibri; font-size:11pt">Now we can create new types of ways for Employee data to be written, perhaps via HTTP/HTTPS by creating abstractions, and without modifying any of our previous code!</span></div>
<div></div>
<div><span style="color:#008080; font-size:small">Read more about Inversion of Control (IoC) with Dependency Injection (DI) Pattern here
<a href="http://code.msdn.microsoft.com/Dependency-Injection-with-5702acaf">http://code.msdn.microsoft.com/Dependency-Injection-with-5702acaf</a></span></div>
<div><span style="font-family:calibri; font-size:11pt">
<div class="endscriptcode"></div>
<div class="endscriptcode">Example:</div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>

<div class="preview">
<pre class="csharp"><span class="cs__keyword">using</span>&nbsp;System;&nbsp;
<span class="cs__keyword">using</span>&nbsp;<a class="libraryLink" href="http://msdn.microsoft.com/en-US/library/System.Collections.Generic.aspx" target="_blank" title="Auto generated link to System.Collections.Generic">System.Collections.Generic</a>;&nbsp;
<span class="cs__keyword">using</span>&nbsp;<a class="libraryLink" href="http://msdn.microsoft.com/en-US/library/System.Linq.aspx" target="_blank" title="Auto generated link to System.Linq">System.Linq</a>;&nbsp;
<span class="cs__keyword">using</span>&nbsp;<a class="libraryLink" href="http://msdn.microsoft.com/en-US/library/System.Text.aspx" target="_blank" title="Auto generated link to System.Text">System.Text</a>;&nbsp;
&nbsp;
<span class="cs__keyword">namespace</span>&nbsp;SOLIDPrinciplesDemo&nbsp;
{&nbsp;
<span class="cs__com">//DIP&nbsp;Violation</span>&nbsp;
<span class="cs__com">//&nbsp;Low&nbsp;level&nbsp;Class</span>&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;BankAccount&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;AccountNumber&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">decimal</span>&nbsp;Balance&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;AddFunds(<span class="cs__keyword">decimal</span>&nbsp;<span class="cs__keyword">value</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Balance&nbsp;&#43;=&nbsp;<span class="cs__keyword">value</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;RemoveFunds(<span class="cs__keyword">decimal</span>&nbsp;<span class="cs__keyword">value</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Balance&nbsp;-=&nbsp;<span class="cs__keyword">value</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
<span class="cs__com">//&nbsp;High&nbsp;level&nbsp;Class</span>&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;TransferAmount&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;BankAccount&nbsp;Source&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;BankAccount&nbsp;Destination&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">decimal</span>&nbsp;Value&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Transfer()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Source.RemoveFunds(Value);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Destination.AddFunds(Value);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
<span class="cs__mlcom">/*&nbsp;&nbsp;
Problem&nbsp;with&nbsp;above&nbsp;design:&nbsp;
&nbsp;&nbsp;
1.&nbsp;The&nbsp;high&nbsp;level&nbsp;TransferAmount&nbsp;class&nbsp;is&nbsp;directly&nbsp;dependent&nbsp;upon&nbsp;the&nbsp;lower&nbsp;level&nbsp;BankAccount&nbsp;class&nbsp;i.e.&nbsp;Tight&nbsp;coupling.&nbsp;
2.&nbsp;The&nbsp;Source&nbsp;and&nbsp;Destination&nbsp;properties&nbsp;reference&nbsp;the&nbsp;BankAccount&nbsp;type.So&nbsp;impossible&nbsp;to&nbsp;substitute&nbsp;other&nbsp;account&nbsp;types&nbsp;unless&nbsp;they&nbsp;are&nbsp;subclasses&nbsp;of&nbsp;BankAccount.&nbsp;&nbsp;
3.&nbsp;Later&nbsp;we&nbsp;want&nbsp;to&nbsp;add&nbsp;the&nbsp;ability&nbsp;to&nbsp;transfer&nbsp;money&nbsp;from&nbsp;a&nbsp;bank&nbsp;account&nbsp;to&nbsp;pay&nbsp;bills,&nbsp;the&nbsp;BillAccount&nbsp;class&nbsp;would&nbsp;have&nbsp;to&nbsp;inherit&nbsp;from&nbsp;BankAccount.&nbsp;&nbsp;
&nbsp;As&nbsp;bills&nbsp;would&nbsp;not&nbsp;support&nbsp;the&nbsp;removal&nbsp;of&nbsp;funds,&nbsp;&nbsp;
&nbsp;3.A.&nbsp;This&nbsp;is&nbsp;likely&nbsp;to&nbsp;break&nbsp;the&nbsp;rules&nbsp;of&nbsp;the&nbsp;Liskov&nbsp;Substitution&nbsp;Principle&nbsp;(LSP)&nbsp;or&nbsp;&nbsp;
&nbsp;3.B.&nbsp;Require&nbsp;changes&nbsp;to&nbsp;the&nbsp;TransferAmount&nbsp;class&nbsp;that&nbsp;do&nbsp;not&nbsp;comply&nbsp;with&nbsp;the&nbsp;Open/Closed&nbsp;Principle&nbsp;(OCP).&nbsp;
&nbsp;&nbsp;
4.&nbsp;Any&nbsp;extension&nbsp;functionality&nbsp;changes&nbsp;be&nbsp;required&nbsp;to&nbsp;low&nbsp;level&nbsp;modules.&nbsp;&nbsp;
&nbsp;4.A.&nbsp;Change&nbsp;in&nbsp;the&nbsp;BankAccount&nbsp;class&nbsp;may&nbsp;break&nbsp;the&nbsp;TransferAmount.&nbsp;&nbsp;
&nbsp;4.B.&nbsp;In&nbsp;complex&nbsp;scenarios,&nbsp;changes&nbsp;to&nbsp;low&nbsp;level&nbsp;classes&nbsp;can&nbsp;cause&nbsp;problems&nbsp;that&nbsp;cascade&nbsp;upwards&nbsp;through&nbsp;the&nbsp;hierarchy&nbsp;of&nbsp;modules.&nbsp;&nbsp;
5.&nbsp;As&nbsp;the&nbsp;software&nbsp;grows,&nbsp;this&nbsp;structural&nbsp;problem&nbsp;can&nbsp;be&nbsp;compounded&nbsp;and&nbsp;the&nbsp;software&nbsp;can&nbsp;become&nbsp;fragile&nbsp;or&nbsp;rigid.&nbsp;
6.&nbsp;Without&nbsp;the&nbsp;DIP,&nbsp;only&nbsp;the&nbsp;lowest&nbsp;level&nbsp;classes&nbsp;may&nbsp;be&nbsp;easily&nbsp;reusable.&nbsp;
7.&nbsp;Unit&nbsp;testing&nbsp;should&nbsp;be&nbsp;redone&nbsp;when&nbsp;there&nbsp;is&nbsp;a&nbsp;change&nbsp;in&nbsp;high&nbsp;level&nbsp;or&nbsp;low&nbsp;level&nbsp;classes.&nbsp;
8.&nbsp;Time&nbsp;taken&nbsp;process&nbsp;to&nbsp;change&nbsp;the&nbsp;existing&nbsp;functionality&nbsp;and&nbsp;extending&nbsp;the&nbsp;functionality&nbsp;
*/</span>&nbsp;&nbsp;
&nbsp;
<span class="cs__com">//Applying&nbsp;DIP&nbsp;resolves&nbsp;these&nbsp;problems&nbsp;by&nbsp;removing&nbsp;direct&nbsp;dependencies&nbsp;between&nbsp;classes.&nbsp;</span>&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">interface</span>&nbsp;ITransferSource&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">long</span>&nbsp;AccountNumber&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">decimal</span>&nbsp;Balance&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">void</span>&nbsp;RemoveFunds(<span class="cs__keyword">decimal</span>&nbsp;<span class="cs__keyword">value</span>);&nbsp;
}&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">interface</span>&nbsp;ITransferDestination&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">long</span>&nbsp;AccountNumber&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">decimal</span>&nbsp;Balance&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">void</span>&nbsp;AddFunds(<span class="cs__keyword">decimal</span>&nbsp;<span class="cs__keyword">value</span>);&nbsp;
}&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;BOABankAccount&nbsp;:&nbsp;ITransferSource,&nbsp;ITransferDestination&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">long</span>&nbsp;AccountNumber&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">decimal</span>&nbsp;Balance&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;AddFunds(<span class="cs__keyword">decimal</span>&nbsp;<span class="cs__keyword">value</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Balance&nbsp;&#43;=&nbsp;<span class="cs__keyword">value</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;RemoveFunds(<span class="cs__keyword">decimal</span>&nbsp;<span class="cs__keyword">value</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Balance&nbsp;-=&nbsp;<span class="cs__keyword">value</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;TransferAmounts&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">decimal</span>&nbsp;Amount&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Transfer(ITransferSource&nbsp;TransferSource,&nbsp;ITransferDestination&nbsp;TransferDestination)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TransferSource.RemoveFunds(Amount);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TransferDestination.AddFunds(Amount);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<span class="cs__mlcom">/*&nbsp;
Advantage&nbsp;in&nbsp;above&nbsp;example&nbsp;after&nbsp;applying&nbsp;DIP:&nbsp;
&nbsp;
1.&nbsp;Higher&nbsp;level&nbsp;classes&nbsp;refer&nbsp;to&nbsp;their&nbsp;dependencies&nbsp;using&nbsp;abstractions,&nbsp;such&nbsp;as&nbsp;interfaces&nbsp;or&nbsp;abstract&nbsp;classes&nbsp;i.e.&nbsp;loose&nbsp;coupling.&nbsp;&nbsp;
2.&nbsp;Lower&nbsp;level&nbsp;classes&nbsp;implement&nbsp;the&nbsp;interfaces,&nbsp;or&nbsp;inherit&nbsp;from&nbsp;the&nbsp;abstract&nbsp;classes.&nbsp;
3.&nbsp;This&nbsp;allows&nbsp;new&nbsp;dependencies&nbsp;can&nbsp;be&nbsp;substituted&nbsp;without&nbsp;any&nbsp;impact.&nbsp;&nbsp;
4.&nbsp;Lower&nbsp;levels&nbsp;classes&nbsp;will&nbsp;not&nbsp;cascade&nbsp;upwards&nbsp;as&nbsp;long&nbsp;as&nbsp;they&nbsp;do&nbsp;not&nbsp;involve&nbsp;changing&nbsp;the&nbsp;abstraction.&nbsp;
5.&nbsp;Increases&nbsp;the&nbsp;robustness&nbsp;of&nbsp;the&nbsp;software&nbsp;and&nbsp;improves&nbsp;flexibility.&nbsp;&nbsp;
6.&nbsp;Separation&nbsp;of&nbsp;high&nbsp;level&nbsp;classes&nbsp;from&nbsp;their&nbsp;dependencies&nbsp;raises&nbsp;the&nbsp;possibility&nbsp;of&nbsp;reuse&nbsp;of&nbsp;these&nbsp;larger&nbsp;areas&nbsp;of&nbsp;functionality.&nbsp;&nbsp;
7.&nbsp;Minimized&nbsp;risk&nbsp;to&nbsp;affect&nbsp;old&nbsp;funtionallity&nbsp;present&nbsp;in&nbsp;Higher&nbsp;level&nbsp;classes.&nbsp;
8.&nbsp;Testing&nbsp;applies&nbsp;only&nbsp;for&nbsp;&nbsp;newly&nbsp;added&nbsp;low&nbsp;level&nbsp;classes.&nbsp;
9.&nbsp;Though&nbsp;using&nbsp;this&nbsp;principle&nbsp;implies&nbsp;an&nbsp;increased&nbsp;effort&nbsp;and&nbsp;a&nbsp;more&nbsp;complex&nbsp;code,&nbsp;but&nbsp;it&nbsp;is&nbsp;more&nbsp;flexible.&nbsp;&nbsp;
&nbsp;&nbsp;
Note:&nbsp;
In&nbsp;that&nbsp;case&nbsp;the&nbsp;creation&nbsp;of&nbsp;new&nbsp;low&nbsp;level&nbsp;objects&nbsp;inside&nbsp;the&nbsp;high&nbsp;level&nbsp;classes(if&nbsp;necessary)&nbsp;can&nbsp;not&nbsp;be&nbsp;done&nbsp;using&nbsp;the&nbsp;operator&nbsp;new.&nbsp;&nbsp;
Instead,&nbsp;some&nbsp;of&nbsp;the&nbsp;Creational&nbsp;design&nbsp;patterns&nbsp;can&nbsp;be&nbsp;used,&nbsp;such&nbsp;as&nbsp;Factory&nbsp;Method,&nbsp;Abstract&nbsp;Factory,&nbsp;Prototype.&nbsp;
&nbsp;
The&nbsp;Template&nbsp;Design&nbsp;Pattern&nbsp;is&nbsp;an&nbsp;example&nbsp;where&nbsp;the&nbsp;DIP&nbsp;principle&nbsp;is&nbsp;applied.&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;
This&nbsp;principle&nbsp;cannot&nbsp;be&nbsp;applied&nbsp;for&nbsp;every&nbsp;class&nbsp;or&nbsp;every&nbsp;module.&nbsp;&nbsp;
If&nbsp;we&nbsp;have&nbsp;a&nbsp;class&nbsp;functionality&nbsp;that&nbsp;is&nbsp;more&nbsp;likely&nbsp;to&nbsp;remain&nbsp;unchanged&nbsp;in&nbsp;the&nbsp;future&nbsp;there&nbsp;is&nbsp;not&nbsp;need&nbsp;to&nbsp;apply&nbsp;this&nbsp;principle.&nbsp;
*/</span>&nbsp;
<span class="cs__keyword">class</span>&nbsp;DependencyInversionPrincipleDemo&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;DIPDemo()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;\n\nDependency&nbsp;Inversion&nbsp;Principle&nbsp;Demo&nbsp;&quot;</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//Created&nbsp;abstract&nbsp;class/interfaces&nbsp;objects&nbsp;for&nbsp;low&nbsp;level&nbsp;classes.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ITransferSource&nbsp;TransferSource&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;BOABankAccount();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TransferSource.AccountNumber&nbsp;=&nbsp;<span class="cs__number">123456789</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TransferSource.Balance&nbsp;=&nbsp;<span class="cs__number">1000</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Balance&nbsp;in&nbsp;Source&nbsp;Account&nbsp;:&nbsp;&quot;</span>&nbsp;&#43;&nbsp;TransferSource.AccountNumber&nbsp;&#43;&nbsp;<span class="cs__string">&quot;&nbsp;Amount&nbsp;&quot;</span>&nbsp;&#43;&nbsp;TransferSource.Balance);&nbsp;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ITransferDestination&nbsp;TransferDestination&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;BOABankAccount();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TransferDestination.AccountNumber&nbsp;=&nbsp;<span class="cs__number">987654321</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TransferDestination.Balance&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Balance&nbsp;in&nbsp;Destination&nbsp;Account&nbsp;:&nbsp;&quot;</span>&nbsp;&#43;&nbsp;TransferDestination.AccountNumber&nbsp;&#43;&nbsp;<span class="cs__string">&quot;&nbsp;Amount&nbsp;&quot;</span>&nbsp;&#43;&nbsp;TransferDestination.Balance);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TransferAmounts&nbsp;TransferAmountsObject&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;TransferAmounts();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TransferAmountsObject.Amount&nbsp;=&nbsp;<span class="cs__number">100</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;High&nbsp;level&nbsp;class&nbsp;using&nbsp;abstract&nbsp;class/interface&nbsp;objects&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TransferAmountsObject.Transfer(TransferSource,&nbsp;TransferDestination);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Transaction&nbsp;successful&quot;</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Balance&nbsp;in&nbsp;Source&nbsp;Account&nbsp;:&nbsp;&quot;</span>&nbsp;&#43;&nbsp;TransferSource.AccountNumber&nbsp;&#43;&nbsp;<span class="cs__string">&quot;&nbsp;Amount&nbsp;&quot;</span>&nbsp;&#43;&nbsp;TransferSource.Balance);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(<span class="cs__string">&quot;Balance&nbsp;in&nbsp;Destination&nbsp;Account&nbsp;:&nbsp;&quot;</span>&nbsp;&#43;&nbsp;TransferDestination.AccountNumber&nbsp;&#43;&nbsp;<span class="cs__string">&quot;&nbsp;Amount&nbsp;&quot;</span>&nbsp;&#43;&nbsp;TransferDestination.Balance);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</div>
<div class="endscriptcode">I have explained each principle in detail with&nbsp;example in the sample. Download the sample and go through the code with comments.</div>
<div class="endscriptcode"></div>
<div class="endscriptcode"><span style="color:#000000; font-size:small">Thank you for reading my article. Drop all your questions/comments in QA tab give me your feedback with
<span style="color:#3366ff"><img id="67168" src="http://i1.code.msdn.s-msft.com/oops-principles-solid-7a4e69bf/image/file/67168/1/ratings.png" alt="" width="74" height="15">
<span style="color:#000000">star rating (1 Star - Very Poor, 5&nbsp;Star -&nbsp;Very Good).
</span></span></span></div>
<div class="endscriptcode"><span style="color:#3366ff">&nbsp;</span></div>
<div class="endscriptcode"><span style="color:#808000; font-size:small">Visit my all other articles here
<a href="http://code.msdn.microsoft.com/site/search?f%5B0%5D.Type=User&f%5B0%5D.Value=Srigopal%20Chitrapu">
http://code.msdn.microsoft.com/site/search?f%5B0%5D.Type=User&amp;f%5B0%5D.Value=Srigopal%20Chitrapu</a></span></div>
<span style="color:#008000">
<div class="endscriptcode"></div>
<span style="color:#0000ff"><span style="color:#000000">
<div class="endscriptcode">
<div class="endscriptcode"></div>
</div>
</span>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
<div class="endscriptcode">
<div class="endscriptcode"></div>
<div class="endscriptcode"><span style="color:#000000; font-size:small">&nbsp;</span></div>
</div>
</span>
<div class="endscriptcode">
<div class="endscriptcode"></div>
<div class="endscriptcode"></div>
</div>
</span>
<div class="endscriptcode"></div>
</span></div>
