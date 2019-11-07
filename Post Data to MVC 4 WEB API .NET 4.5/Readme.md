# Post Data to MVC 4 WEB API .NET 4.5
## Requires
- Visual Studio 2012
## License
- Apache License, Version 2.0
## Technologies
- ASP.NET MVC 4
- ASP.NET Web API
## Topics
- ASP.NET Web API
- Web API
- ASP.NET Web API browser clients
## Updated
- 07/04/2012
## Description

<h1>Introduction</h1>
<p><span style="font-size:x-small"><span style="font-family:Verdana">In the previous Article&nbsp;<a href="http://www.c-sharpcorner.com/UploadFile/amit12345/mvc-4-web-api-net-4-5/"></a><a href="http://code.msdn.microsoft.com/MVC-4-WEB-API-NET-45-7fa62da6">http://code.msdn.microsoft.com/MVC-4-WEB-API-NET-45-7fa62da6</a>&nbsp;we
 saw how to get data from a Web API in various formats.</span></span></p>
<p><span style="font-family:Verdana">In this article I am going to explain how we post &amp; put data to Web API.</span></p>
<p><span style="font-family:Verdana">When we post data it will simple insert in the Database and when we use PUT method that time data will update only. We can use DELETE method for delete record from Database.</span></p>
<p><strong style="font-family:Verdana">Code:&nbsp;</strong><span style="font-family:Verdana">Let's see the code and understand what we need to implement here.</span></p>
<p><span style="font-family:Verdana"><strong>Step 1:&nbsp;</strong>First all we will see what are POST and PUT methods in the Customer Controller.</span></p>
<p><span style="font-family:Verdana">In the following code the Post Method will take a CustomerModel.</span><span style="font-family:Verdana">&nbsp;</span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">  // POST /api/customer
public void Post(CustomerModel customer)
{
            context.AddToCustomers(new Customer { Id = customer.Id, Name = customer.Name, Salary = customer.Salary });
            context.SaveChanges(System.Data.Objects.SaveOptions.AcceptAllChangesAfterSave);
}</pre>
<div class="preview">
<pre class="csharp">&nbsp;&nbsp;<span class="cs__com">//&nbsp;POST&nbsp;/api/customer</span>&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Post(CustomerModel&nbsp;customer)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;context.AddToCustomers(<span class="cs__keyword">new</span>&nbsp;Customer&nbsp;{&nbsp;Id&nbsp;=&nbsp;customer.Id,&nbsp;Name&nbsp;=&nbsp;customer.Name,&nbsp;Salary&nbsp;=&nbsp;customer.Salary&nbsp;});&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;context.SaveChanges(System.Data.Objects.SaveOptions.AcceptAllChangesAfterSave);&nbsp;
}</pre>
</div>
</div>
</div>
<div class="endscriptcode">In the following code the Put Method will take CustomerModel and customerID.</div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">// PUT /api/customer/5
public void Put(int id, CustomerModel customer)
{
            var cust = context.Customers.First(c =&gt; c.Id == id);
            cust.Name = customer.Name;
            cust.Salary = customer.Salary;
            context.SaveChanges();
}</pre>
<div class="preview">
<pre class="csharp"><span class="cs__com">//&nbsp;PUT&nbsp;/api/customer/5</span>&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Put(<span class="cs__keyword">int</span>&nbsp;id,&nbsp;CustomerModel&nbsp;customer)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;var&nbsp;cust&nbsp;=&nbsp;context.Customers.First(c&nbsp;=&gt;&nbsp;c.Id&nbsp;==&nbsp;id);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;cust.Name&nbsp;=&nbsp;customer.Name;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;cust.Salary&nbsp;=&nbsp;customer.Salary;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;context.SaveChanges();&nbsp;
}</pre>
</div>
</div>
</div>
<div class="endscriptcode"><span style="font-family:Verdana">Now in Step 2 will see how we can post data to a Web API using JSON.</span></div>
<p><strong style="font-family:Verdana">Step 2:&nbsp;</strong><span style="font-family:Verdana">See the highlighted code inside the JQuery block. We have created an instance of a CustomerModel Object inside a JavaScript block:</span></p>
<p><strong><span style="font-family:Verdana">POST</span></strong></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">&lt;script type=&quot;text/javascript&quot;&gt;
    $(document).ready(function(){
        var CustomerCreate = {
            Name: &quot;Jackson&quot;,
            Salary: 22222
        };
        createCustomer(CustomerCreate, function (newCustomer) {
            alert(&quot;New Customer created with an Id of &quot; &#43; newCustomer.Id);
        });
        $(&quot;#AddCustomer&quot;).click(function () {
            createCustomer(CustomerCreate, null);
        });
        function createCustomer(CustomerCreate, callback) {
            $.ajax({
                url: &quot;/api/Customer&quot;,
                data: JSON.stringify(CustomerCreate),
                type: &quot;POST&quot;,
                contentType: &quot;application/json;charset=utf-8&quot;,
                statusCode: {
                    201: function (newCustomer) {
                        callback(newCustomer);
                    }
                }
            });
 
        }});
&lt;/script&gt;</pre>
<div class="preview">
<pre class="js">&lt;script&nbsp;type=<span class="js__string">&quot;text/javascript&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$(document).ready(<span class="js__operator">function</span>()<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;CustomerCreate&nbsp;=&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Name:&nbsp;<span class="js__string">&quot;Jackson&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Salary:&nbsp;<span class="js__num">22222</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;createCustomer(CustomerCreate,&nbsp;<span class="js__operator">function</span>&nbsp;(newCustomer)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;alert(<span class="js__string">&quot;New&nbsp;Customer&nbsp;created&nbsp;with&nbsp;an&nbsp;Id&nbsp;of&nbsp;&quot;</span>&nbsp;&#43;&nbsp;newCustomer.Id);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$(<span class="js__string">&quot;#AddCustomer&quot;</span>).click(<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;createCustomer(CustomerCreate,&nbsp;null);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">function</span>&nbsp;createCustomer(CustomerCreate,&nbsp;callback)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$.ajax(<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url:&nbsp;<span class="js__string">&quot;/api/Customer&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;data:&nbsp;JSON.stringify(CustomerCreate),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;type:&nbsp;<span class="js__string">&quot;POST&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;contentType:&nbsp;<span class="js__string">&quot;application/json;charset=utf-8&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;statusCode:&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__num">201</span>:&nbsp;<span class="js__operator">function</span>&nbsp;(newCustomer)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;callback(newCustomer);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span>);&nbsp;
&lt;/script&gt;</pre>
</div>
</div>
</div>
<div class="endscriptcode"><strong>PUT</strong></div>
<div class="endscriptcode"><strong>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">&lt;script type=&quot;text/javascript&quot;&gt;
    $(document).ready(function(){
        var CustomerCreate = {
Id:1,
            Name: &quot;Jackson&quot;,
            Salary: 22222
        };
        createCustomer(CustomerCreate, function (newCustomer) {
            alert(&quot;New Customer created with an Id of &quot; &#43; newCustomer.Id);
        });
        $(&quot;#AddCustomer&quot;).click(function () {
            createCustomer(CustomerCreate, null);
        });
        function createCustomer(CustomerCreate, callback) {
            $.ajax({
                url: &quot;/api/Customer&quot;,
                data: JSON.stringify(CustomerCreate),
                type: &quot;PUT&quot;,
                contentType: &quot;application/json;charset=utf-8&quot;,
                statusCode: {
                    201: function (newCustomer) {
                        callback(newCustomer);
                    }
                }
            });
 
        }});
&lt;/script&gt;</pre>
<div class="preview">
<pre class="js">&lt;script&nbsp;type=<span class="js__string">&quot;text/javascript&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$(document).ready(<span class="js__operator">function</span>()<span class="js__brace">{</span><span class="js__statement">var</span>&nbsp;CustomerCreate&nbsp;=&nbsp;<span class="js__brace">{</span>&nbsp;
Id:<span class="js__num">1</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Name:&nbsp;<span class="js__string">&quot;Jackson&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Salary:&nbsp;<span class="js__num">22222</span><span class="js__brace">}</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;createCustomer(CustomerCreate,&nbsp;<span class="js__operator">function</span>&nbsp;(newCustomer)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;alert(<span class="js__string">&quot;New&nbsp;Customer&nbsp;created&nbsp;with&nbsp;an&nbsp;Id&nbsp;of&nbsp;&quot;</span>&nbsp;&#43;&nbsp;newCustomer.Id);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$(<span class="js__string">&quot;#AddCustomer&quot;</span>).click(<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;createCustomer(CustomerCreate,&nbsp;null);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">function</span>&nbsp;createCustomer(CustomerCreate,&nbsp;callback)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$.ajax(<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url:&nbsp;<span class="js__string">&quot;/api/Customer&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;data:&nbsp;JSON.stringify(CustomerCreate),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;type:&nbsp;<span class="js__string">&quot;PUT&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;contentType:&nbsp;<span class="js__string">&quot;application/json;charset=utf-8&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;statusCode:&nbsp;<span class="js__brace">{</span><span class="js__num">201</span>:&nbsp;<span class="js__operator">function</span>&nbsp;(newCustomer)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;callback(newCustomer);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span><span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span>);&nbsp;
&lt;/script&gt;</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;DELETE</div>
<div class="endscriptcode">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">js</span>
<pre class="hidden">&lt;script type=&quot;text/javascript&quot;&gt;
    $(document).ready(function(){
        var CustomerCreate = {
          ID:1
        };
        createCustomer(CustomerCreate, function (newCustomer) {
            alert(&quot;New Customer created with an Id of &quot; &#43; newCustomer.Id);
        });
        $(&quot;#AddCustomer&quot;).click(function () {
            createCustomer(CustomerCreate, null);
        });
        function createCustomer(CustomerCreate, callback) {
            $.ajax({
                url: &quot;/api/Customer&quot;,
                data: JSON.stringify(CustomerCreate),
                type: &quot;DELETE&quot;,
                contentType: &quot;application/json;charset=utf-8&quot;,
                statusCode: {
                    201: function (newCustomer) {
                        callback(newCustomer);
                    }
                }
            });
 
        }});
&lt;/script&gt;</pre>
<div class="preview">
<pre class="js">&lt;script&nbsp;type=<span class="js__string">&quot;text/javascript&quot;</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$(document).ready(<span class="js__operator">function</span>()<span class="js__brace">{</span><span class="js__statement">var</span>&nbsp;CustomerCreate&nbsp;=&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ID:<span class="js__num">1</span><span class="js__brace">}</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;createCustomer(CustomerCreate,&nbsp;<span class="js__operator">function</span>&nbsp;(newCustomer)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;alert(<span class="js__string">&quot;New&nbsp;Customer&nbsp;created&nbsp;with&nbsp;an&nbsp;Id&nbsp;of&nbsp;&quot;</span>&nbsp;&#43;&nbsp;newCustomer.Id);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$(<span class="js__string">&quot;#AddCustomer&quot;</span>).click(<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;createCustomer(CustomerCreate,&nbsp;null);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__operator">function</span>&nbsp;createCustomer(CustomerCreate,&nbsp;callback)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$.ajax(<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url:&nbsp;<span class="js__string">&quot;/api/Customer&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;data:&nbsp;JSON.stringify(CustomerCreate),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;type:&nbsp;<span class="js__string">&quot;DELETE&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;contentType:&nbsp;<span class="js__string">&quot;application/json;charset=utf-8&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;statusCode:&nbsp;<span class="js__brace">{</span><span class="js__num">201</span>:&nbsp;<span class="js__operator">function</span>&nbsp;(newCustomer)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;callback(newCustomer);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span><span class="js__brace">}</span>);&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span><span class="js__brace">}</span>);&nbsp;
&lt;/script&gt;</pre>
</div>
</div>
</div>
</div>
</strong>
<div class="endscriptcode">
<div class="endscriptcode"><span style="font-family:Verdana">In the HTML body we have added one HTML anchor tag and above inside the JavaScript block we have registered a click event for the anchor tag. Now when the user clicks on the anchor link, data will
 be posted to the server.</span></div>
</div>
</div>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>HTML</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">html</span>
<pre class="hidden">&lt;body&gt;
    &lt;header&gt;
        &lt;div class=&quot;content-wrapper&quot;&gt;
            &lt;div class=&quot;float-left&quot;&gt;
                &lt;p class=&quot;site-title&quot;&gt;&lt;a href=&quot;/&quot;&gt;ASP.NET Web API&lt;/a&gt;&lt;/p&gt;
            &lt;/div&gt;
        &lt;/div&gt;
    &lt;/header&gt;
    &lt;div id=&quot;body&quot;&gt;
        &lt;a id=&quot;AddCustomer&quot; href=&quot;#&quot;&gt;Create New Customer&lt;/a&gt;
    &lt;/div&gt;
&lt;/body&gt;</pre>
<div class="preview">
<pre class="html"><span class="html__tag_start">&lt;body</span><span class="html__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;header</span><span class="html__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;div</span>&nbsp;<span class="html__attr_name">class</span>=<span class="html__attr_value">&quot;content-wrapper&quot;</span><span class="html__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;div</span>&nbsp;<span class="html__attr_name">class</span>=<span class="html__attr_value">&quot;float-left&quot;</span><span class="html__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;p</span>&nbsp;<span class="html__attr_name">class</span>=<span class="html__attr_value">&quot;site-title&quot;</span><span class="html__tag_start">&gt;</span><span class="html__tag_start">&lt;a</span>&nbsp;<span class="html__attr_name">href</span>=<span class="html__attr_value">&quot;/&quot;</span><span class="html__tag_start">&gt;</span>ASP.NET&nbsp;Web&nbsp;API<span class="html__tag_end">&lt;/a&gt;</span><span class="html__tag_end">&lt;/p&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_end">&lt;/div&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_end">&lt;/div&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_end">&lt;/header&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;div</span>&nbsp;<span class="html__attr_name">id</span>=<span class="html__attr_value">&quot;body&quot;</span><span class="html__tag_start">&gt;&nbsp;
</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_start">&lt;a</span>&nbsp;<span class="html__attr_name">id</span>=<span class="html__attr_value">&quot;AddCustomer&quot;</span>&nbsp;<span class="html__attr_name">href</span>=<span class="html__attr_value">&quot;#&quot;</span><span class="html__tag_start">&gt;</span>Create&nbsp;New&nbsp;Customer<span class="html__tag_end">&lt;/a&gt;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="html__tag_end">&lt;/div&gt;</span>&nbsp;
<span class="html__tag_end">&lt;/body&gt;</span></pre>
</div>
</div>
</div>
<div class="endscriptcode"><span style="font-family:Verdana">In the following image we can see how the data will be sent via the Post Method:</span></div>
<p><span style="font-family:Verdana"><span><img src="-postwepapi2.png" alt="PostwepApi2.png"></span></span></p>
<div class="PaddingLeft5"><span style="font-size:x-small"><span style="font-size:x-small">
<p><span style="font-family:Verdana"><strong>&nbsp;</strong>Happy Coding.</span></p>
</span></span></div>
