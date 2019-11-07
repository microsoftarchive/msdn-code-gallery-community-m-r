# Parameter validation using data annotations for REST services
## Requires
- Visual Studio 2010
## License
- Apache License, Version 2.0
## Technologies
- WCF
## Topics
- WCF
- WCF Extensibility
## Updated
- 06/07/2011
## Description

<p><em>This sample was provided as part of a blog series on WCF extensibility. The entry for this sample can be found at
<a href="http://blogs.msdn.com/b/carlosfigueira/archive/2011/06/07/wcf-extensibility-ierrorhandler.aspx">
http://blogs.msdn.com/b/carlosfigueira/archive/2011/06/07/wcf-extensibility-ierrorhandler.aspx</a>.</em></p>
<p>I&rsquo;ve seen quite a few of services where the first thing that each operation does is to validate that the input parameters have valid properties. A zip code has to follow certain regular expression, the balance a bank account should not be negative
 (at least in most banks), someone&rsquo;s age should not be negative, etc. There is a great library for annotating class properties in the
<a href="http://msdn.microsoft.com/en-us/library/system.componentmodel.dataannotations.aspx">
System.ComponentModel.DataAnnotations</a> namespace, and it&rsquo;s fairly simple to implement a simple validation using a parameter inspector for all the operations in the contract. In this example I have a simple HTML form application, and I&rsquo;ll use
 an error handler to return the validation information in format which is easily accessible by the browser (i.e., in JSON).</p>
<p>And before starting with the code, the usual disclaimer: this is a sample for illustrating the topic of this post, this is not production-ready code. I tested it for a few contracts and it worked, but I cannot guarantee that it will work for all scenarios
 (please let me know if you find a bug or something missing). A more complete implementation would also include validation for simple types (which this one doesn&rsquo;t), and it would definitely *not* use an in-memory dictionary as the &ldquo;database&rdquo;
 for storing the data. Also, the validation doesn&rsquo;t go deep in the objects by default, so it currently it only validates the members of the parameters (not the members of the members).</p>
<p>I&rsquo;ll start with the service. It&rsquo;s a simple contact manager, where we can add, delete and query contacts</p>
<div class="wlWriterEditableSmartContent" id="scid:9ce6104f-a9aa-4a17-a79f-3a39532ebf7c:88e7b9a6-e59b-4ed1-ac47-96573590a3bd" style="margin:0px; display:inline; float:none; padding:0px">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginEditHolderLink">Edit Script</div>
<span class="hidden">csharp</span>
<pre class="hidden">[ServiceContract]
public class ContactManager
{
    static Dictionary&lt;string, Contact&gt; contacts = new Dictionary&lt;string, Contact&gt;();
    static int nextId = 0;

    [WebInvoke(Method = &quot;POST&quot;, UriTemplate = &quot;/Contact&quot;, ResponseFormat = WebMessageFormat.Json)]
    public string AddContact(Contact contact)
    {
        string id = (&#43;&#43;nextId).ToString();
        contacts.Add(id, contact);
        string requestUri = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.ToString();
        if (requestUri.EndsWith(&quot;/&quot;))
        {
            requestUri = requestUri.Substring(0, requestUri.Length - 1);
        }

        WebOperationContext.Current.OutgoingResponse.Headers[HttpResponseHeader.Location] = requestUri &#43; &quot;s/&quot; &#43; id;
        return id;
    }

    [WebInvoke(Method = &quot;DELETE&quot;, UriTemplate = &quot;/Contact/{id}&quot;)]
    public void DeleteContact(string id)
    {
        if (contacts.ContainsKey(id))
        {
            contacts.Remove(id);
        }
        else
        {
            throw new WebFaultException(HttpStatusCode.NotFound);
        }
    }

    [WebGet(UriTemplate = &quot;/Contacts/{id}&quot;, ResponseFormat = WebMessageFormat.Json)]
    public Contact GetContact(string id)
    {
        if (contacts.ContainsKey(id))
        {
            return contacts[id];
        }
        else
        {
            throw new WebFaultException(HttpStatusCode.NotFound);
        }
    }
}</pre>
<div class="preview">
<pre class="csharp">[ServiceContract]&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;ContactManager&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">static</span>&nbsp;Dictionary&lt;<span class="cs__keyword">string</span>,&nbsp;Contact&gt;&nbsp;contacts&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Dictionary&lt;<span class="cs__keyword">string</span>,&nbsp;Contact&gt;();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;nextId&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[WebInvoke(Method&nbsp;=&nbsp;<span class="cs__string">&quot;POST&quot;</span>,&nbsp;UriTemplate&nbsp;=&nbsp;<span class="cs__string">&quot;/Contact&quot;</span>,&nbsp;ResponseFormat&nbsp;=&nbsp;WebMessageFormat.Json)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;AddContact(Contact&nbsp;contact)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;id&nbsp;=&nbsp;(&#43;&#43;nextId).ToString();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;contacts.Add(id,&nbsp;contact);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;requestUri&nbsp;=&nbsp;WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.ToString();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(requestUri.EndsWith(<span class="cs__string">&quot;/&quot;</span>))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;requestUri&nbsp;=&nbsp;requestUri.Substring(<span class="cs__number">0</span>,&nbsp;requestUri.Length&nbsp;-&nbsp;<span class="cs__number">1</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;WebOperationContext.Current.OutgoingResponse.Headers[HttpResponseHeader.Location]&nbsp;=&nbsp;requestUri&nbsp;&#43;&nbsp;<span class="cs__string">&quot;s/&quot;</span>&nbsp;&#43;&nbsp;id;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;id;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[WebInvoke(Method&nbsp;=&nbsp;<span class="cs__string">&quot;DELETE&quot;</span>,&nbsp;UriTemplate&nbsp;=&nbsp;<span class="cs__string">&quot;/Contact/{id}&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;DeleteContact(<span class="cs__keyword">string</span>&nbsp;id)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(contacts.ContainsKey(id))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;contacts.Remove(id);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">throw</span>&nbsp;<span class="cs__keyword">new</span>&nbsp;WebFaultException(HttpStatusCode.NotFound);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[WebGet(UriTemplate&nbsp;=&nbsp;<span class="cs__string">&quot;/Contacts/{id}&quot;</span>,&nbsp;ResponseFormat&nbsp;=&nbsp;WebMessageFormat.Json)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;Contact&nbsp;GetContact(<span class="cs__keyword">string</span>&nbsp;id)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(contacts.ContainsKey(id))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;contacts[id];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">throw</span>&nbsp;<span class="cs__keyword">new</span>&nbsp;WebFaultException(HttpStatusCode.NotFound);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</div>
<p>The Contact class is defined with some annotation for its data members. Both name and e-mail properties are required (i.e., they cannot be null), and their values must follow certain guidelines. The age is not required, but it&rsquo;s limited to a certain
 range.</p>
<div class="wlWriterEditableSmartContent" id="scid:9ce6104f-a9aa-4a17-a79f-3a39532ebf7c:ab50eefc-b13c-405e-9e2f-a62680ad6980" style="margin:0px; display:inline; float:none; padding:0px">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginEditHolderLink">Edit Script</div>
<span class="hidden">csharp</span>
<pre class="hidden">[DataContract]
public class Contact
{
    [DataMember]
    [Required(ErrorMessage = &quot;Name is required&quot;)]
    [StringLength(20, MinimumLength = 1, ErrorMessage = &quot;Name must have between 1 and 20 characters&quot;)]
    public string Name { get; set; }

    [DataMember]
    [Range(0, 150, ErrorMessage = &quot;Age must be an integer between 0 and 150&quot;)]
    public int Age { get; set; }

    [DataMember]
    [Required(ErrorMessage = &quot;E-mail is required&quot;)]
    [RegularExpression(@&quot;[^\@]&#43;\@[a-zA-Z0-9]&#43;(\.[a-zA-Z0-9]&#43;)&#43;&quot;, ErrorMessage = &quot;E-mail is invalid&quot;)]
    public string Email { get; set; }
}</pre>
<div class="preview">
<pre class="csharp">[DataContract]&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;Contact&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[DataMember]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[Required(ErrorMessage&nbsp;=&nbsp;<span class="cs__string">&quot;Name&nbsp;is&nbsp;required&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[StringLength(<span class="cs__number">20</span>,&nbsp;MinimumLength&nbsp;=&nbsp;<span class="cs__number">1</span>,&nbsp;ErrorMessage&nbsp;=&nbsp;<span class="cs__string">&quot;Name&nbsp;must&nbsp;have&nbsp;between&nbsp;1&nbsp;and&nbsp;20&nbsp;characters&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;Name&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[DataMember]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[Range(<span class="cs__number">0</span>,&nbsp;<span class="cs__number">150</span>,&nbsp;ErrorMessage&nbsp;=&nbsp;<span class="cs__string">&quot;Age&nbsp;must&nbsp;be&nbsp;an&nbsp;integer&nbsp;between&nbsp;0&nbsp;and&nbsp;150&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;Age&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[DataMember]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[Required(ErrorMessage&nbsp;=&nbsp;<span class="cs__string">&quot;E-mail&nbsp;is&nbsp;required&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[RegularExpression(@<span class="cs__string">&quot;[^\@]&#43;\@[a-zA-Z0-9]&#43;(\.[a-zA-Z0-9]&#43;)&#43;&quot;</span>,&nbsp;ErrorMessage&nbsp;=&nbsp;<span class="cs__string">&quot;E-mail&nbsp;is&nbsp;invalid&quot;</span>)]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;Email&nbsp;{&nbsp;<span class="cs__keyword">get</span>;&nbsp;<span class="cs__keyword">set</span>;&nbsp;}&nbsp;
}&nbsp;
&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</div>
<p>Now to add the validation logic. Since this is a web endpoint, I&rsquo;ll create a subclass of
<a href="http://msdn.microsoft.com/en-us/library/system.servicemodel.description.webhttpbehavior.aspx">
WebHttpBehavior</a> which has some nice overrides I can use for that. The behavior must add two elements: a parameter inspector which will validate the incoming data, and the new error handler which will return the errors in a nice format.</p>
<div class="wlWriterEditableSmartContent" id="scid:9ce6104f-a9aa-4a17-a79f-3a39532ebf7c:04bf8bd2-22a7-495a-afee-c88446c15a36" style="margin:0px; display:inline; float:none; padding:0px">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginEditHolderLink">Edit Script</div>
<span class="hidden">csharp</span>
<pre class="hidden">public class WebHttpWithValidationBehavior : WebHttpBehavior
{
    protected override void AddServerErrorHandlers(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
    {
        int errorHandlerCount = endpointDispatcher.ChannelDispatcher.ErrorHandlers.Count;
        base.AddServerErrorHandlers(endpoint, endpointDispatcher);
        IErrorHandler webHttpErrorHandler = endpointDispatcher.ChannelDispatcher.ErrorHandlers[errorHandlerCount];
        endpointDispatcher.ChannelDispatcher.ErrorHandlers.RemoveAt(errorHandlerCount);
        ValidationAwareErrorHandler newHandler = new ValidationAwareErrorHandler(webHttpErrorHandler);
        endpointDispatcher.ChannelDispatcher.ErrorHandlers.Add(newHandler);
    }

    public override void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
    {
        base.ApplyDispatchBehavior(endpoint, endpointDispatcher);
        foreach (DispatchOperation operation in endpointDispatcher.DispatchRuntime.Operations)
        {
            operation.ParameterInspectors.Add(new ValidatingParameterInspector());
        }
    }
}</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;WebHttpWithValidationBehavior&nbsp;:&nbsp;WebHttpBehavior&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">protected</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;AddServerErrorHandlers(ServiceEndpoint&nbsp;endpoint,&nbsp;EndpointDispatcher&nbsp;endpointDispatcher)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;errorHandlerCount&nbsp;=&nbsp;endpointDispatcher.ChannelDispatcher.ErrorHandlers.Count;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">base</span>.AddServerErrorHandlers(endpoint,&nbsp;endpointDispatcher);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IErrorHandler&nbsp;webHttpErrorHandler&nbsp;=&nbsp;endpointDispatcher.ChannelDispatcher.ErrorHandlers[errorHandlerCount];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;endpointDispatcher.ChannelDispatcher.ErrorHandlers.RemoveAt(errorHandlerCount);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ValidationAwareErrorHandler&nbsp;newHandler&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;ValidationAwareErrorHandler(webHttpErrorHandler);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;endpointDispatcher.ChannelDispatcher.ErrorHandlers.Add(newHandler);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;ApplyDispatchBehavior(ServiceEndpoint&nbsp;endpoint,&nbsp;EndpointDispatcher&nbsp;endpointDispatcher)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">base</span>.ApplyDispatchBehavior(endpoint,&nbsp;endpointDispatcher);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">foreach</span>&nbsp;(DispatchOperation&nbsp;operation&nbsp;<span class="cs__keyword">in</span>&nbsp;endpointDispatcher.DispatchRuntime.Operations)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;operation.ParameterInspectors.Add(<span class="cs__keyword">new</span>&nbsp;ValidatingParameterInspector());&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</div>
<p>The inspector is fairly simple, it simply calls the <a href="http://msdn.microsoft.com/en-us/library/dd382100.aspx">
Validator.ValidateObject</a> on every non-null parameter which is passed to the function. This will scan the data annotation properties and validate the instance against them.</p>
<div class="wlWriterEditableSmartContent" id="scid:9ce6104f-a9aa-4a17-a79f-3a39532ebf7c:e657b239-9cb1-4528-ad67-45339e277d34" style="margin:0px; display:inline; float:none; padding:0px">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginEditHolderLink">Edit Script</div>
<span class="hidden">csharp</span>
<pre class="hidden">public class ValidatingParameterInspector : IParameterInspector
{
    public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
    {
    }

    public object BeforeCall(string operationName, object[] inputs)
    {
        foreach (var input in inputs)
        {
            if (input != null)
            {
                ValidationContext context = new ValidationContext(input, null, null);
                Validator.ValidateObject(input, context, true);
            }
        }

        return null;
    }
}</pre>
<div class="preview">
<pre class="js">public&nbsp;class&nbsp;ValidatingParameterInspector&nbsp;:&nbsp;IParameterInspector&nbsp;
<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;<span class="js__operator">void</span>&nbsp;AfterCall(string&nbsp;operationName,&nbsp;object[]&nbsp;outputs,&nbsp;object&nbsp;returnValue,&nbsp;object&nbsp;correlationState)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;object&nbsp;BeforeCall(string&nbsp;operationName,&nbsp;object[]&nbsp;inputs)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;foreach&nbsp;(<span class="js__statement">var</span>&nbsp;input&nbsp;<span class="js__operator">in</span>&nbsp;inputs)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">if</span>&nbsp;(input&nbsp;!=&nbsp;null)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ValidationContext&nbsp;context&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;ValidationContext(input,&nbsp;null,&nbsp;null);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Validator.ValidateObject(input,&nbsp;context,&nbsp;true);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;null;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
<span class="js__brace">}</span>&nbsp;
&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</div>
<p>The error handler&nbsp; is the piece responsible for sending the validation error in a form that the client understands. In this implementation, it creates a new message object if the exception is a
<a href="http://msdn.microsoft.com/en-us/library/system.componentmodel.dataannotations.validationexception.aspx">
ValidationException</a> (which is thrown when the ValidateObject call fails). If the exception is something else, it will simply delegate to the original error handler added by the WebHttpBehavior. The new message created for validation errors will use the
 Json encoder from the WebMessageEncodingBindingElement (guided by the <a href="http://msdn.microsoft.com/en-us/library/system.servicemodel.channels.webbodyformatmessageproperty.aspx">
WebBodyFormatMessageProperty</a> in the message). And the body of the message is written using the
<a href="http://msdn.microsoft.com/en-us/library/bb924435.aspx">mapping between XML and JSON</a> to create the expected output.</p>
<div class="wlWriterEditableSmartContent" id="scid:9ce6104f-a9aa-4a17-a79f-3a39532ebf7c:1ca9b02a-c82d-4bb7-85a5-e4b917f215c6" style="margin:0px; display:inline; float:none; padding:0px">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginEditHolderLink">Edit Script</div>
<span class="hidden">csharp</span>
<pre class="hidden">public class ValidationAwareErrorHandler : IErrorHandler
{
    IErrorHandler originalErrorHandler;
    public ValidationAwareErrorHandler(IErrorHandler originalErrorHandler)
    {
        this.originalErrorHandler = originalErrorHandler;
    }

    public bool HandleError(Exception error)
    {
        return error is ValidationException || this.originalErrorHandler.HandleError(error);
    }

    public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
    {
        ValidationException validationException = error as ValidationException;
        if (validationException != null)
        {
            fault = Message.CreateMessage(version, null, new ValidationErrorBodyWriter(validationException));
            HttpResponseMessageProperty prop = new HttpResponseMessageProperty();
            prop.StatusCode = HttpStatusCode.BadRequest;
            prop.Headers[HttpResponseHeader.ContentType] = &quot;application/json; charset=utf-8&quot;;
            fault.Properties.Add(HttpResponseMessageProperty.Name, prop);
            fault.Properties.Add(WebBodyFormatMessageProperty.Name, new WebBodyFormatMessageProperty(WebContentFormat.Json));
        }
        else
        {
            this.originalErrorHandler.ProvideFault(error, version, ref fault);
        }
    }

    class ValidationErrorBodyWriter : BodyWriter
    {
        private ValidationException validationException;
        Encoding utf8Encoding = new UTF8Encoding(false);

        public ValidationErrorBodyWriter(ValidationException validationException)
            : base(true)
        {
            this.validationException = validationException;
        }

        protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
        {
            writer.WriteStartElement(&quot;root&quot;);
            writer.WriteAttributeString(&quot;type&quot;, &quot;object&quot;);

            writer.WriteStartElement(&quot;ErrorMessage&quot;);
            writer.WriteAttributeString(&quot;type&quot;, &quot;string&quot;);
            writer.WriteString(this.validationException.ValidationResult.ErrorMessage);
            writer.WriteEndElement();

            writer.WriteStartElement(&quot;MemberNames&quot;);
            writer.WriteAttributeString(&quot;type&quot;, &quot;array&quot;);
            foreach (var member in this.validationException.ValidationResult.MemberNames)
            {
                writer.WriteStartElement(&quot;item&quot;);
                writer.WriteAttributeString(&quot;type&quot;, &quot;string&quot;);
                writer.WriteString(member);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteEndElement();
        }
    }
}</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;ValidationAwareErrorHandler&nbsp;:&nbsp;IErrorHandler&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;IErrorHandler&nbsp;originalErrorHandler;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;ValidationAwareErrorHandler(IErrorHandler&nbsp;originalErrorHandler)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.originalErrorHandler&nbsp;=&nbsp;originalErrorHandler;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">bool</span>&nbsp;HandleError(Exception&nbsp;error)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;error&nbsp;<span class="cs__keyword">is</span>&nbsp;ValidationException&nbsp;||&nbsp;<span class="cs__keyword">this</span>.originalErrorHandler.HandleError(error);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;ProvideFault(Exception&nbsp;error,&nbsp;MessageVersion&nbsp;version,&nbsp;<span class="cs__keyword">ref</span>&nbsp;Message&nbsp;fault)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ValidationException&nbsp;validationException&nbsp;=&nbsp;error&nbsp;<span class="cs__keyword">as</span>&nbsp;ValidationException;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(validationException&nbsp;!=&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;fault&nbsp;=&nbsp;Message.CreateMessage(version,&nbsp;<span class="cs__keyword">null</span>,&nbsp;<span class="cs__keyword">new</span>&nbsp;ValidationErrorBodyWriter(validationException));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;HttpResponseMessageProperty&nbsp;prop&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;HttpResponseMessageProperty();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;prop.StatusCode&nbsp;=&nbsp;HttpStatusCode.BadRequest;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;prop.Headers[HttpResponseHeader.ContentType]&nbsp;=&nbsp;<span class="cs__string">&quot;application/json;&nbsp;charset=utf-8&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;fault.Properties.Add(HttpResponseMessageProperty.Name,&nbsp;prop);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;fault.Properties.Add(WebBodyFormatMessageProperty.Name,&nbsp;<span class="cs__keyword">new</span>&nbsp;WebBodyFormatMessageProperty(WebContentFormat.Json));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.originalErrorHandler.ProvideFault(error,&nbsp;version,&nbsp;<span class="cs__keyword">ref</span>&nbsp;fault);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">class</span>&nbsp;ValidationErrorBodyWriter&nbsp;:&nbsp;BodyWriter&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;ValidationException&nbsp;validationException;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Encoding&nbsp;utf8Encoding&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;UTF8Encoding(<span class="cs__keyword">false</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;ValidationErrorBodyWriter(ValidationException&nbsp;validationException)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;<span class="cs__keyword">base</span>(<span class="cs__keyword">true</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.validationException&nbsp;=&nbsp;validationException;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">protected</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;OnWriteBodyContents(XmlDictionaryWriter&nbsp;writer)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;writer.WriteStartElement(<span class="cs__string">&quot;root&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;writer.WriteAttributeString(<span class="cs__string">&quot;type&quot;</span>,&nbsp;<span class="cs__string">&quot;object&quot;</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;writer.WriteStartElement(<span class="cs__string">&quot;ErrorMessage&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;writer.WriteAttributeString(<span class="cs__string">&quot;type&quot;</span>,&nbsp;<span class="cs__string">&quot;string&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;writer.WriteString(<span class="cs__keyword">this</span>.validationException.ValidationResult.ErrorMessage);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;writer.WriteEndElement();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;writer.WriteStartElement(<span class="cs__string">&quot;MemberNames&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;writer.WriteAttributeString(<span class="cs__string">&quot;type&quot;</span>,&nbsp;<span class="cs__string">&quot;array&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">foreach</span>&nbsp;(var&nbsp;member&nbsp;<span class="cs__keyword">in</span>&nbsp;<span class="cs__keyword">this</span>.validationException.ValidationResult.MemberNames)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;writer.WriteStartElement(<span class="cs__string">&quot;item&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;writer.WriteAttributeString(<span class="cs__string">&quot;type&quot;</span>,&nbsp;<span class="cs__string">&quot;string&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;writer.WriteString(member);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;writer.WriteEndElement();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;writer.WriteEndElement();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;writer.WriteEndElement();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</div>
<p>The service itself is done. In order to hook up the endpoint behavior to the service creation, I&rsquo;m using a custom service host factory (see more about it in the next post), so that I don&rsquo;t need to worry about configuration extensions.</p>
<div class="wlWriterEditableSmartContent" id="scid:9ce6104f-a9aa-4a17-a79f-3a39532ebf7c:23c69a2f-6f43-4e45-aa61-2b3c18c37428" style="margin:0px; display:inline; float:none; padding:0px">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginEditHolderLink">Edit Script</div>
<span class="hidden">csharp</span>
<pre class="hidden">public class ContactManagerFactory : ServiceHostFactory
{
    protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
    {
        ServiceHost host = new ServiceHost(serviceType, baseAddresses);
        ServiceEndpoint endpoint = host.AddServiceEndpoint(serviceType, new WebHttpBinding(), &quot;&quot;);
        endpoint.Behaviors.Add(new WebHttpWithValidationBehavior());
        return host;
    }
}</pre>
<div class="preview">
<pre class="js">public&nbsp;class&nbsp;ContactManagerFactory&nbsp;:&nbsp;ServiceHostFactory&nbsp;
<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;protected&nbsp;override&nbsp;ServiceHost&nbsp;CreateServiceHost(Type&nbsp;serviceType,&nbsp;Uri[]&nbsp;baseAddresses)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ServiceHost&nbsp;host&nbsp;=&nbsp;<span class="js__operator">new</span>&nbsp;ServiceHost(serviceType,&nbsp;baseAddresses);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ServiceEndpoint&nbsp;endpoint&nbsp;=&nbsp;host.AddServiceEndpoint(serviceType,&nbsp;<span class="js__operator">new</span>&nbsp;WebHttpBinding(),&nbsp;<span class="js__string">&quot;&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;endpoint.Behaviors.Add(<span class="js__operator">new</span>&nbsp;WebHttpWithValidationBehavior());&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">return</span>&nbsp;host;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
<span class="js__brace">}</span>&nbsp;
&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</div>
<p>Now that everything is set up, we can test the service. I like to use a unit test framework (when I was writing this sample I wrote the tests first), and qUnit is one of my favorites. Below is a snippet of the tests which I used to verify the service code.</p>
<div class="wlWriterEditableSmartContent" id="scid:9ce6104f-a9aa-4a17-a79f-3a39532ebf7c:67ae60c6-4707-426c-b8be-28c2d1315a67" style="margin:0px; display:inline; float:none; padding:0px">
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>JavaScript</span></div>
<div class="pluginEditHolderLink">Edit Script</div>
<span class="hidden">js</span>
<pre class="hidden">module(&quot;Validation tests&quot;);

asyncTest(&quot;Missing name&quot;, 2, function () {
    sendAndExpectError(undefined, 30, &quot;john@doe.com&quot;, 400, &quot;Name is required&quot;);
});

asyncTest(&quot;Negative age&quot;, 2, function () {
    sendAndExpectError(&quot;John Doe&quot;, -1, &quot;john@doe.com&quot;, 400, &quot;Age must be an integer between 0 and 150&quot;);
});

asyncTest(&quot;Very high age&quot;, 2, function () {
    sendAndExpectError(&quot;John Doe&quot;, 151, &quot;john@doe.com&quot;, 400, &quot;Age must be an integer between 0 and 150&quot;);
});

asyncTest(&quot;Missing e-mail&quot;, 2, function () {
    sendAndExpectError(&quot;John Doe&quot;, 30, undefined, 400, &quot;E-mail is required&quot;);
});

asyncTest(&quot;Invalid e-mail&quot;, 2, function () {
    sendAndExpectError(&quot;John Doe&quot;, 30, &quot;abcdef&quot;, 400, &quot;E-mail is invalid&quot;);
});

function sendAndExpectError(name, age, email, expectedStatusCode, expectedMessage) {
    var data = JSON.stringify({ Name: name, Age: age, Email: email });

    $.ajax({
        type: &quot;POST&quot;,
        url: serviceAddress &#43; &quot;Contact&quot;,
        contentType: &quot;application/json&quot;,
        data: data,
        success: function (data) {
            ok(false, &quot;Validation should have failed the request&quot;);
            ok(false, &quot;Result: &quot; &#43; data);
        },
        error: function (jqXHR) {
            var statusCode = jqXHR.status;
            var response = $.parseJSON(jqXHR.responseText); ;
            equal(statusCode, expectedStatusCode, &quot;Status code is correct&quot;);
            equal(response.ErrorMessage, expectedMessage, &quot;Message is correct&quot;);
        },
        complete: function () {
            start();
        }
    });
}</pre>
<div class="preview">
<pre class="js">module(<span class="js__string">&quot;Validation&nbsp;tests&quot;</span>);&nbsp;
&nbsp;
asyncTest(<span class="js__string">&quot;Missing&nbsp;name&quot;</span>,&nbsp;<span class="js__num">2</span>,&nbsp;<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;sendAndExpectError(<span class="js__property">undefined</span>,&nbsp;<span class="js__num">30</span>,&nbsp;<span class="js__string">&quot;john@doe.com&quot;</span>,&nbsp;<span class="js__num">400</span>,&nbsp;<span class="js__string">&quot;Name&nbsp;is&nbsp;required&quot;</span>);&nbsp;
<span class="js__brace">}</span>);&nbsp;
&nbsp;
asyncTest(<span class="js__string">&quot;Negative&nbsp;age&quot;</span>,&nbsp;<span class="js__num">2</span>,&nbsp;<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;sendAndExpectError(<span class="js__string">&quot;John&nbsp;Doe&quot;</span>,&nbsp;-<span class="js__num">1</span>,&nbsp;<span class="js__string">&quot;john@doe.com&quot;</span>,&nbsp;<span class="js__num">400</span>,&nbsp;<span class="js__string">&quot;Age&nbsp;must&nbsp;be&nbsp;an&nbsp;integer&nbsp;between&nbsp;0&nbsp;and&nbsp;150&quot;</span>);&nbsp;
<span class="js__brace">}</span>);&nbsp;
&nbsp;
asyncTest(<span class="js__string">&quot;Very&nbsp;high&nbsp;age&quot;</span>,&nbsp;<span class="js__num">2</span>,&nbsp;<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;sendAndExpectError(<span class="js__string">&quot;John&nbsp;Doe&quot;</span>,&nbsp;<span class="js__num">151</span>,&nbsp;<span class="js__string">&quot;john@doe.com&quot;</span>,&nbsp;<span class="js__num">400</span>,&nbsp;<span class="js__string">&quot;Age&nbsp;must&nbsp;be&nbsp;an&nbsp;integer&nbsp;between&nbsp;0&nbsp;and&nbsp;150&quot;</span>);&nbsp;
<span class="js__brace">}</span>);&nbsp;
&nbsp;
asyncTest(<span class="js__string">&quot;Missing&nbsp;e-mail&quot;</span>,&nbsp;<span class="js__num">2</span>,&nbsp;<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;sendAndExpectError(<span class="js__string">&quot;John&nbsp;Doe&quot;</span>,&nbsp;<span class="js__num">30</span>,&nbsp;<span class="js__property">undefined</span>,&nbsp;<span class="js__num">400</span>,&nbsp;<span class="js__string">&quot;E-mail&nbsp;is&nbsp;required&quot;</span>);&nbsp;
<span class="js__brace">}</span>);&nbsp;
&nbsp;
asyncTest(<span class="js__string">&quot;Invalid&nbsp;e-mail&quot;</span>,&nbsp;<span class="js__num">2</span>,&nbsp;<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;sendAndExpectError(<span class="js__string">&quot;John&nbsp;Doe&quot;</span>,&nbsp;<span class="js__num">30</span>,&nbsp;<span class="js__string">&quot;abcdef&quot;</span>,&nbsp;<span class="js__num">400</span>,&nbsp;<span class="js__string">&quot;E-mail&nbsp;is&nbsp;invalid&quot;</span>);&nbsp;
<span class="js__brace">}</span>);&nbsp;
&nbsp;
<span class="js__operator">function</span>&nbsp;sendAndExpectError(name,&nbsp;age,&nbsp;email,&nbsp;expectedStatusCode,&nbsp;expectedMessage)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;data&nbsp;=&nbsp;JSON.stringify(<span class="js__brace">{</span>&nbsp;Name:&nbsp;name,&nbsp;Age:&nbsp;age,&nbsp;Email:&nbsp;email&nbsp;<span class="js__brace">}</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;$.ajax(<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;type:&nbsp;<span class="js__string">&quot;POST&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url:&nbsp;serviceAddress&nbsp;&#43;&nbsp;<span class="js__string">&quot;Contact&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;contentType:&nbsp;<span class="js__string">&quot;application/json&quot;</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;data:&nbsp;data,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;success:&nbsp;<span class="js__operator">function</span>&nbsp;(data)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ok(false,&nbsp;<span class="js__string">&quot;Validation&nbsp;should&nbsp;have&nbsp;failed&nbsp;the&nbsp;request&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ok(false,&nbsp;<span class="js__string">&quot;Result:&nbsp;&quot;</span>&nbsp;&#43;&nbsp;data);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;error:&nbsp;<span class="js__operator">function</span>&nbsp;(jqXHR)&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;statusCode&nbsp;=&nbsp;jqXHR.status;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__statement">var</span>&nbsp;response&nbsp;=&nbsp;$.parseJSON(jqXHR.responseText);&nbsp;;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;equal(statusCode,&nbsp;expectedStatusCode,&nbsp;<span class="js__string">&quot;Status&nbsp;code&nbsp;is&nbsp;correct&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;equal(response.ErrorMessage,&nbsp;expectedMessage,&nbsp;<span class="js__string">&quot;Message&nbsp;is&nbsp;correct&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;complete:&nbsp;<span class="js__operator">function</span>&nbsp;()&nbsp;<span class="js__brace">{</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;start();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="js__brace">}</span>);&nbsp;
<span class="js__brace">}</span>&nbsp;
&nbsp;
</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
</div>
<p>And that&rsquo;s it. In the code for this post I added a simple HTML form which uses this service as well.</p>
