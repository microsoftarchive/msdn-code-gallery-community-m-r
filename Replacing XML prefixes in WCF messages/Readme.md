# Replacing XML prefixes in WCF messages
## Requires
- Visual Studio 2010
## License
- Apache License, Version 2.0
## Technologies
- WCF
## Topics
- WCF Extensibility
## Updated
- 01/04/2012
## Description

<p><em>This sample was originally introduced in the blog post at <a href="http://blogs.msdn.com/b/carlosfigueira/archive/2010/06/13/changing-prefixes-in-xml-responses.aspx">
http://blogs.msdn.com/b/carlosfigueira/archive/2010/06/13/changing-prefixes-in-xml-responses.aspx</a>.</em></p>
<h1>Introduction</h1>
<p>When responding to requests using any of the text/XML bindings (BasicHttpBinding / WSHttpBinding / CustomBinding with a TextMessageEncodingBindingElement), WCF has some specific prefixes which it uses corresponding to a certain namespaces. The SOAP namespace
 (&ldquo;http://www.w3.org/2003/05/soap-envelope&rdquo; for SOAP 1.2, &ldquo;http://schemas.xmlsoap.org/soap/envelope/&rdquo; for SOAP 1.1), for example, is always mapped to the prefix &ldquo;s&rdquo;. This is usually not a problem, but there are some Web Service
 stack implementations which require that a certain prefix to be used (there have been a few questions in the WCF forums about this issue.</p>
<p>First a small introduction on why this should not be a problem. The following two documents are essentially equivalent, from a XML Infoset standpoint, for &ldquo;namespace-aware applications&rdquo;, according to
<a title="http://www.w3.org/TR/xml-infoset/#infoitem.element" href="http://www.w3.org/TR/xml-infoset/#infoitem.element">
http://www.w3.org/TR/xml-infoset/#infoitem.element</a>:</p>
<pre>&lt;a:root xmlns:a=&quot;http://my.namespace&quot;&gt;
  &lt;a:item&gt;value&lt;/a:item&gt;
&lt;/a:root&gt;</pre>
<pre>&lt;b:root xmlns:b=&quot;http://my.namespace&quot;&gt;
  &lt;b:item&gt;value&lt;/b:item&gt;
&lt;/b:root&gt;</pre>
<p>Here, the only difference between those two documents is the prefix used to represent the namespace &ldquo;http://my.namespace&rdquo;. So, I imagine that the WCF developers decided to follow that &ldquo;rule&rdquo; and didn&rsquo;t add any simple way of
 changing the prefixes used in its SOAP responses &ndash; after all, it <em>shouldn&rsquo;t matter</em>. The problem is that sometimes it does :-). Thankfully, the WCF extensibility model is rich enough that we can overcome this limitation by a number of different
 ways. Here I&rsquo;ll show one which uses the message encoding extensibility to trap the message right before it&rsquo;s encoded into bytes (to be sent over the wire), modify it changing its prefixes when necessary, and then proceed with the encoding.</p>
<h1><span>The prefix changer</span></h1>
<p>The class PrefixReplacer does most of the work, by using the XmlElement class to change, whenever necessary, the elements / attributes of the XML document which have the prefix which needs to be changed.</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">public class PrefixReplacer
{
    const string XmlnsNamespace = &quot;http://www.w3.org/2000/xmlns/&quot;;
    Dictionary&lt;string, string&gt; namespaceToNewPrefixMapping = new Dictionary&lt;string, string&gt;();

    public void AddNamespace(string namespaceUri, string newPrefix)
    {
        this.namespaceToNewPrefixMapping.Add(namespaceUri, newPrefix);
    }

    public void ChangePrefixes(XmlDocument doc)
    {
        XmlElement element = doc.DocumentElement;
        XmlElement newElement = ChangePrefixes(doc, element);
        doc.LoadXml(newElement.OuterXml);
    }

    private XmlElement ChangePrefixes(XmlDocument doc, XmlElement element)
    {
        string newPrefix;
        if (this.namespaceToNewPrefixMapping.TryGetValue(element.NamespaceURI, out newPrefix))
        {
            XmlElement newElement = doc.CreateElement(newPrefix, element.LocalName, element.NamespaceURI);
            List&lt;XmlNode&gt; children = new List&lt;XmlNode&gt;(element.ChildNodes.Cast&lt;XmlNode&gt;());
            List&lt;XmlAttribute&gt; attributes = new List&lt;XmlAttribute&gt;(element.Attributes.Cast&lt;XmlAttribute&gt;());
            foreach (XmlNode child in children)
            {
                newElement.AppendChild(child);
            }

            foreach (XmlAttribute attr in attributes)
            {
                newElement.Attributes.Append(attr);
            }

            element = newElement;
        }

        List&lt;XmlAttribute&gt; newAttributes = new List&lt;XmlAttribute&gt;();
        bool modified = false;
        for (int i = 0; i &lt; element.Attributes.Count; i&#43;&#43;)
        {
            XmlAttribute attr = element.Attributes[i];
            if (this.namespaceToNewPrefixMapping.TryGetValue(attr.NamespaceURI, out newPrefix))
            {
                XmlAttribute newAttr = doc.CreateAttribute(newPrefix, attr.LocalName, attr.NamespaceURI);
                newAttr.Value = attr.Value;
                newAttributes.Add(newAttr);
                modified = true;
            }
            else if (attr.NamespaceURI == XmlnsNamespace &amp;&amp; this.namespaceToNewPrefixMapping.TryGetValue(attr.Value, out newPrefix))
            {
                XmlAttribute newAttr;
                if (newPrefix != &quot;&quot;)
                {
                    newAttr = doc.CreateAttribute(&quot;xmlns&quot;, newPrefix, XmlnsNamespace);
                }
                else
                {
                    newAttr = doc.CreateAttribute(&quot;xmlns&quot;);
                }

                newAttr.Value = attr.Value;
                newAttributes.Add(newAttr);
                modified = true;
            }
            else
            {
                newAttributes.Add(attr);
            }
        }

        if (modified)
        {
            element.Attributes.RemoveAll();
            foreach (var attr in newAttributes)
            {
                element.Attributes.Append(attr);
            }
        }

        List&lt;KeyValuePair&lt;XmlNode, XmlNode&gt;&gt; toReplace = new List&lt;KeyValuePair&lt;XmlNode, XmlNode&gt;&gt;();
        foreach (XmlNode child in element.ChildNodes)
        {
            XmlElement childElement = child as XmlElement;
            if (childElement != null)
            {
                XmlElement newChildElement = ChangePrefixes(doc, childElement);
                if (newChildElement != childElement)
                {
                    toReplace.Add(new KeyValuePair&lt;XmlNode, XmlNode&gt;(childElement, newChildElement));
                }
            }
        }

        if (toReplace.Count &gt; 0)
        {
            for (int i = 0; i &lt; toReplace.Count; i&#43;&#43;)
            {
                element.InsertAfter(toReplace[i].Value, toReplace[i].Key);
                element.RemoveChild(toReplace[i].Key);
            }
        }

        return element;
    }
}</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;PrefixReplacer&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">const</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;XmlnsNamespace&nbsp;=&nbsp;<span class="cs__string">&quot;http://www.w3.org/2000/xmlns/&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Dictionary&lt;<span class="cs__keyword">string</span>,&nbsp;<span class="cs__keyword">string</span>&gt;&nbsp;namespaceToNewPrefixMapping&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Dictionary&lt;<span class="cs__keyword">string</span>,&nbsp;<span class="cs__keyword">string</span>&gt;();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;AddNamespace(<span class="cs__keyword">string</span>&nbsp;namespaceUri,&nbsp;<span class="cs__keyword">string</span>&nbsp;newPrefix)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.namespaceToNewPrefixMapping.Add(namespaceUri,&nbsp;newPrefix);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;ChangePrefixes(XmlDocument&nbsp;doc)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlElement&nbsp;element&nbsp;=&nbsp;doc.DocumentElement;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlElement&nbsp;newElement&nbsp;=&nbsp;ChangePrefixes(doc,&nbsp;element);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc.LoadXml(newElement.OuterXml);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;XmlElement&nbsp;ChangePrefixes(XmlDocument&nbsp;doc,&nbsp;XmlElement&nbsp;element)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;newPrefix;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(<span class="cs__keyword">this</span>.namespaceToNewPrefixMapping.TryGetValue(element.NamespaceURI,&nbsp;<span class="cs__keyword">out</span>&nbsp;newPrefix))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlElement&nbsp;newElement&nbsp;=&nbsp;doc.CreateElement(newPrefix,&nbsp;element.LocalName,&nbsp;element.NamespaceURI);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;List&lt;XmlNode&gt;&nbsp;children&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;List&lt;XmlNode&gt;(element.ChildNodes.Cast&lt;XmlNode&gt;());&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;List&lt;XmlAttribute&gt;&nbsp;attributes&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;List&lt;XmlAttribute&gt;(element.Attributes.Cast&lt;XmlAttribute&gt;());&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">foreach</span>&nbsp;(XmlNode&nbsp;child&nbsp;<span class="cs__keyword">in</span>&nbsp;children)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;newElement.AppendChild(child);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">foreach</span>&nbsp;(XmlAttribute&nbsp;attr&nbsp;<span class="cs__keyword">in</span>&nbsp;attributes)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;newElement.Attributes.Append(attr);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;element&nbsp;=&nbsp;newElement;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;List&lt;XmlAttribute&gt;&nbsp;newAttributes&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;List&lt;XmlAttribute&gt;();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">bool</span>&nbsp;modified&nbsp;=&nbsp;<span class="cs__keyword">false</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(<span class="cs__keyword">int</span>&nbsp;i&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;i&nbsp;&lt;&nbsp;element.Attributes.Count;&nbsp;i&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlAttribute&nbsp;attr&nbsp;=&nbsp;element.Attributes[i];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(<span class="cs__keyword">this</span>.namespaceToNewPrefixMapping.TryGetValue(attr.NamespaceURI,&nbsp;<span class="cs__keyword">out</span>&nbsp;newPrefix))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlAttribute&nbsp;newAttr&nbsp;=&nbsp;doc.CreateAttribute(newPrefix,&nbsp;attr.LocalName,&nbsp;attr.NamespaceURI);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;newAttr.Value&nbsp;=&nbsp;attr.Value;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;newAttributes.Add(newAttr);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;modified&nbsp;=&nbsp;<span class="cs__keyword">true</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(attr.NamespaceURI&nbsp;==&nbsp;XmlnsNamespace&nbsp;&amp;&amp;&nbsp;<span class="cs__keyword">this</span>.namespaceToNewPrefixMapping.TryGetValue(attr.Value,&nbsp;<span class="cs__keyword">out</span>&nbsp;newPrefix))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlAttribute&nbsp;newAttr;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(newPrefix&nbsp;!=&nbsp;<span class="cs__string">&quot;&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;newAttr&nbsp;=&nbsp;doc.CreateAttribute(<span class="cs__string">&quot;xmlns&quot;</span>,&nbsp;newPrefix,&nbsp;XmlnsNamespace);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;newAttr&nbsp;=&nbsp;doc.CreateAttribute(<span class="cs__string">&quot;xmlns&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;newAttr.Value&nbsp;=&nbsp;attr.Value;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;newAttributes.Add(newAttr);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;modified&nbsp;=&nbsp;<span class="cs__keyword">true</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;newAttributes.Add(attr);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(modified)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;element.Attributes.RemoveAll();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">foreach</span>&nbsp;(var&nbsp;attr&nbsp;<span class="cs__keyword">in</span>&nbsp;newAttributes)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;element.Attributes.Append(attr);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;List&lt;KeyValuePair&lt;XmlNode,&nbsp;XmlNode&gt;&gt;&nbsp;toReplace&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;List&lt;KeyValuePair&lt;XmlNode,&nbsp;XmlNode&gt;&gt;();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">foreach</span>&nbsp;(XmlNode&nbsp;child&nbsp;<span class="cs__keyword">in</span>&nbsp;element.ChildNodes)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlElement&nbsp;childElement&nbsp;=&nbsp;child&nbsp;<span class="cs__keyword">as</span>&nbsp;XmlElement;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(childElement&nbsp;!=&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlElement&nbsp;newChildElement&nbsp;=&nbsp;ChangePrefixes(doc,&nbsp;childElement);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(newChildElement&nbsp;!=&nbsp;childElement)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;toReplace.Add(<span class="cs__keyword">new</span>&nbsp;KeyValuePair&lt;XmlNode,&nbsp;XmlNode&gt;(childElement,&nbsp;newChildElement));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(toReplace.Count&nbsp;&gt;&nbsp;<span class="cs__number">0</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(<span class="cs__keyword">int</span>&nbsp;i&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;i&nbsp;&lt;&nbsp;toReplace.Count;&nbsp;i&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;element.InsertAfter(toReplace[i].Value,&nbsp;toReplace[i].Key);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;element.RemoveChild(toReplace[i].Key);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;element;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<p><span style="font-size:20px; font-weight:bold">The prefix replacer message encoding binding element</span></p>
<p>The encoding element follows a pattern similar to the custom message encoder found in the WCF samples at
<a title="http://msdn.microsoft.com/en-us/library/ms751486.aspx" href="http://msdn.microsoft.com/en-us/library/ms751486.aspx">
http://msdn.microsoft.com/en-us/library/ms751486.aspx</a>. Essentially, it simply delegates all calls to an inner encoder, except when writing messages to the wire; in this case, it will instead first change the message (to replace the prefixes, then call the
 inner encoder to convert it to bytes.<em><br>
</em></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">public class ReplacePrefixMessageEncodingBindingElement : MessageEncodingBindingElement
{
    MessageEncodingBindingElement inner;
    Dictionary&lt;string, string&gt; namespaceToPrefixMapping = new Dictionary&lt;string, string&gt;();
    public ReplacePrefixMessageEncodingBindingElement(MessageEncodingBindingElement inner)
    {
        this.inner = inner;
    }

    private ReplacePrefixMessageEncodingBindingElement(ReplacePrefixMessageEncodingBindingElement other)
    {
        this.inner = other.inner;
        this.namespaceToPrefixMapping = new Dictionary&lt;string, string&gt;(other.namespaceToPrefixMapping);
    }

    public void AddNamespaceMapping(string namespaceUri, string newPrefix)
    {
        this.namespaceToPrefixMapping.Add(namespaceUri, newPrefix);
    }

    public override MessageEncoderFactory CreateMessageEncoderFactory()
    {
        return new ReplacePrefixMessageEncoderFactory(this.inner.CreateMessageEncoderFactory(), this.namespaceToPrefixMapping);
    }

    public override MessageVersion MessageVersion
    {
        get { return this.inner.MessageVersion; }
        set { this.inner.MessageVersion = value; }
    }

    public override BindingElement Clone()
    {
        return new ReplacePrefixMessageEncodingBindingElement(this);
    }

    public override IChannelListener&lt;TChannel&gt; BuildChannelListener&lt;TChannel&gt;(BindingContext context)
    {
        context.BindingParameters.Add(this);
        return context.BuildInnerChannelListener&lt;TChannel&gt;();
    }

    public override bool CanBuildChannelListener&lt;TChannel&gt;(BindingContext context)
    {
        return context.CanBuildInnerChannelListener&lt;TChannel&gt;();
    }

    public static CustomBinding ReplaceEncodingBindingElement(Binding originalBinding, Dictionary&lt;string, string&gt; namespaceToPrefixMapping)
    {
        CustomBinding custom = originalBinding as CustomBinding;
        if (custom == null)
        {
            custom = new CustomBinding(originalBinding);
        }

        for (int i = 0; i &lt; custom.Elements.Count; i&#43;&#43;)
        {
            if (custom.Elements[i] is MessageEncodingBindingElement)
            {
                ReplacePrefixMessageEncodingBindingElement element = new ReplacePrefixMessageEncodingBindingElement((MessageEncodingBindingElement)custom.Elements[i]);
                foreach (var mapping in namespaceToPrefixMapping)
                {
                    element.AddNamespaceMapping(mapping.Key, mapping.Value);
                }

                custom.Elements[i] = element;
                break;
            }
        }

        return custom;
    }

    class ReplacePrefixMessageEncoderFactory : MessageEncoderFactory
    {
        private MessageEncoderFactory messageEncoderFactory;
        private Dictionary&lt;string, string&gt; namespaceToNewPrefixMapping;

        public ReplacePrefixMessageEncoderFactory(MessageEncoderFactory messageEncoderFactory, Dictionary&lt;string, string&gt; namespaceToNewPrefixMapping)
        {
            this.messageEncoderFactory = messageEncoderFactory;
            this.namespaceToNewPrefixMapping = namespaceToNewPrefixMapping;
        }

        public override MessageEncoder Encoder
        {
            get { return new ReplacePrefixMessageEncoder(this.messageEncoderFactory.Encoder, this.namespaceToNewPrefixMapping); }
        }

        public override MessageVersion MessageVersion
        {
            get { return this.messageEncoderFactory.MessageVersion; }
        }

        public override MessageEncoder CreateSessionEncoder()
        {
            return new ReplacePrefixMessageEncoder(this.messageEncoderFactory.CreateSessionEncoder(), this.namespaceToNewPrefixMapping);
        }
    }

    class ReplacePrefixMessageEncoder : MessageEncoder
    {
        private MessageEncoder messageEncoder;
        private Dictionary&lt;string, string&gt; namespaceToNewPrefixMapping;

        public ReplacePrefixMessageEncoder(MessageEncoder messageEncoder, Dictionary&lt;string, string&gt; namespaceToNewPrefixMapping)
        {
            this.messageEncoder = messageEncoder;
            this.namespaceToNewPrefixMapping = namespaceToNewPrefixMapping;
        }

        public override string ContentType
        {
            get { return this.messageEncoder.ContentType; }
        }

        public override string MediaType
        {
            get { return this.messageEncoder.MediaType; }
        }

        public override MessageVersion MessageVersion
        {
            get { return this.messageEncoder.MessageVersion; }
        }

        public override bool IsContentTypeSupported(string contentType)
        {
            return this.messageEncoder.IsContentTypeSupported(contentType);
        }

        public override Message ReadMessage(ArraySegment&lt;byte&gt; buffer, BufferManager bufferManager, string contentType)
        {
            return this.messageEncoder.ReadMessage(buffer, bufferManager, contentType);
        }

        public override Message ReadMessage(Stream stream, int maxSizeOfHeaders, string contentType)
        {
            throw new NotSupportedException(&quot;Streamed not supported&quot;);
        }

        public override ArraySegment&lt;byte&gt; WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset)
        {
            MemoryStream ms = new MemoryStream();
            XmlDictionaryWriter w = XmlDictionaryWriter.CreateBinaryWriter(ms);
            message.WriteMessage(w);
            w.Flush();
            ms.Position = 0;
            XmlDictionaryReader r = XmlDictionaryReader.CreateBinaryReader(ms, XmlDictionaryReaderQuotas.Max);
            XmlDocument doc = new XmlDocument();
            doc.Load(r);
            PrefixReplacer replacer = new PrefixReplacer();
            foreach (var mapping in this.namespaceToNewPrefixMapping)
            {
                replacer.AddNamespace(mapping.Key, mapping.Value);
            }

            replacer.ChangePrefixes(doc);
            ms = new MemoryStream();
            w = XmlDictionaryWriter.CreateBinaryWriter(ms);
            doc.WriteTo(w);
            w.Flush();
            ms.Position = 0;
            r = XmlDictionaryReader.CreateBinaryReader(ms, XmlDictionaryReaderQuotas.Max);
            Message newMessage = Message.CreateMessage(r, maxMessageSize, message.Version);
            newMessage.Properties.CopyProperties(message.Properties);
            return this.messageEncoder.WriteMessage(newMessage, maxMessageSize, bufferManager, messageOffset);
        }

        public override void WriteMessage(Message message, Stream stream)
        {
            throw new NotSupportedException(&quot;Streamed not supported&quot;);
        }
    }
}</pre>
<div class="preview">
<pre class="csharp"><span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;ReplacePrefixMessageEncodingBindingElement&nbsp;:&nbsp;MessageEncodingBindingElement&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;MessageEncodingBindingElement&nbsp;inner;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Dictionary&lt;<span class="cs__keyword">string</span>,&nbsp;<span class="cs__keyword">string</span>&gt;&nbsp;namespaceToPrefixMapping&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Dictionary&lt;<span class="cs__keyword">string</span>,&nbsp;<span class="cs__keyword">string</span>&gt;();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;ReplacePrefixMessageEncodingBindingElement(MessageEncodingBindingElement&nbsp;inner)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.inner&nbsp;=&nbsp;inner;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;ReplacePrefixMessageEncodingBindingElement(ReplacePrefixMessageEncodingBindingElement&nbsp;other)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.inner&nbsp;=&nbsp;other.inner;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.namespaceToPrefixMapping&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Dictionary&lt;<span class="cs__keyword">string</span>,&nbsp;<span class="cs__keyword">string</span>&gt;(other.namespaceToPrefixMapping);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;AddNamespaceMapping(<span class="cs__keyword">string</span>&nbsp;namespaceUri,&nbsp;<span class="cs__keyword">string</span>&nbsp;newPrefix)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.namespaceToPrefixMapping.Add(namespaceUri,&nbsp;newPrefix);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;MessageEncoderFactory&nbsp;CreateMessageEncoderFactory()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">new</span>&nbsp;ReplacePrefixMessageEncoderFactory(<span class="cs__keyword">this</span>.inner.CreateMessageEncoderFactory(),&nbsp;<span class="cs__keyword">this</span>.namespaceToPrefixMapping);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;MessageVersion&nbsp;MessageVersion&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">get</span>&nbsp;{&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">this</span>.inner.MessageVersion;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">set</span>&nbsp;{&nbsp;<span class="cs__keyword">this</span>.inner.MessageVersion&nbsp;=&nbsp;<span class="cs__keyword">value</span>;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;BindingElement&nbsp;Clone()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">new</span>&nbsp;ReplacePrefixMessageEncodingBindingElement(<span class="cs__keyword">this</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;IChannelListener&lt;TChannel&gt;&nbsp;BuildChannelListener&lt;TChannel&gt;(BindingContext&nbsp;context)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;context.BindingParameters.Add(<span class="cs__keyword">this</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;context.BuildInnerChannelListener&lt;TChannel&gt;();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;<span class="cs__keyword">bool</span>&nbsp;CanBuildChannelListener&lt;TChannel&gt;(BindingContext&nbsp;context)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;context.CanBuildInnerChannelListener&lt;TChannel&gt;();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">static</span>&nbsp;CustomBinding&nbsp;ReplaceEncodingBindingElement(Binding&nbsp;originalBinding,&nbsp;Dictionary&lt;<span class="cs__keyword">string</span>,&nbsp;<span class="cs__keyword">string</span>&gt;&nbsp;namespaceToPrefixMapping)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;CustomBinding&nbsp;custom&nbsp;=&nbsp;originalBinding&nbsp;<span class="cs__keyword">as</span>&nbsp;CustomBinding;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(custom&nbsp;==&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;custom&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;CustomBinding(originalBinding);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">for</span>&nbsp;(<span class="cs__keyword">int</span>&nbsp;i&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;i&nbsp;&lt;&nbsp;custom.Elements.Count;&nbsp;i&#43;&#43;)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(custom.Elements[i]&nbsp;<span class="cs__keyword">is</span>&nbsp;MessageEncodingBindingElement)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ReplacePrefixMessageEncodingBindingElement&nbsp;element&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;ReplacePrefixMessageEncodingBindingElement((MessageEncodingBindingElement)custom.Elements[i]);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">foreach</span>&nbsp;(var&nbsp;mapping&nbsp;<span class="cs__keyword">in</span>&nbsp;namespaceToPrefixMapping)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;element.AddNamespaceMapping(mapping.Key,&nbsp;mapping.Value);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;custom.Elements[i]&nbsp;=&nbsp;element;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">break</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;custom;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">class</span>&nbsp;ReplacePrefixMessageEncoderFactory&nbsp;:&nbsp;MessageEncoderFactory&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;MessageEncoderFactory&nbsp;messageEncoderFactory;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;Dictionary&lt;<span class="cs__keyword">string</span>,&nbsp;<span class="cs__keyword">string</span>&gt;&nbsp;namespaceToNewPrefixMapping;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;ReplacePrefixMessageEncoderFactory(MessageEncoderFactory&nbsp;messageEncoderFactory,&nbsp;Dictionary&lt;<span class="cs__keyword">string</span>,&nbsp;<span class="cs__keyword">string</span>&gt;&nbsp;namespaceToNewPrefixMapping)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.messageEncoderFactory&nbsp;=&nbsp;messageEncoderFactory;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.namespaceToNewPrefixMapping&nbsp;=&nbsp;namespaceToNewPrefixMapping;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;MessageEncoder&nbsp;Encoder&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">get</span>&nbsp;{&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">new</span>&nbsp;ReplacePrefixMessageEncoder(<span class="cs__keyword">this</span>.messageEncoderFactory.Encoder,&nbsp;<span class="cs__keyword">this</span>.namespaceToNewPrefixMapping);&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;MessageVersion&nbsp;MessageVersion&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">get</span>&nbsp;{&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">this</span>.messageEncoderFactory.MessageVersion;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;MessageEncoder&nbsp;CreateSessionEncoder()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">new</span>&nbsp;ReplacePrefixMessageEncoder(<span class="cs__keyword">this</span>.messageEncoderFactory.CreateSessionEncoder(),&nbsp;<span class="cs__keyword">this</span>.namespaceToNewPrefixMapping);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">class</span>&nbsp;ReplacePrefixMessageEncoder&nbsp;:&nbsp;MessageEncoder&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;MessageEncoder&nbsp;messageEncoder;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;Dictionary&lt;<span class="cs__keyword">string</span>,&nbsp;<span class="cs__keyword">string</span>&gt;&nbsp;namespaceToNewPrefixMapping;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;ReplacePrefixMessageEncoder(MessageEncoder&nbsp;messageEncoder,&nbsp;Dictionary&lt;<span class="cs__keyword">string</span>,&nbsp;<span class="cs__keyword">string</span>&gt;&nbsp;namespaceToNewPrefixMapping)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.messageEncoder&nbsp;=&nbsp;messageEncoder;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">this</span>.namespaceToNewPrefixMapping&nbsp;=&nbsp;namespaceToNewPrefixMapping;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;ContentType&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">get</span>&nbsp;{&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">this</span>.messageEncoder.ContentType;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;MediaType&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">get</span>&nbsp;{&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">this</span>.messageEncoder.MediaType;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;MessageVersion&nbsp;MessageVersion&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">get</span>&nbsp;{&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">this</span>.messageEncoder.MessageVersion;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;<span class="cs__keyword">bool</span>&nbsp;IsContentTypeSupported(<span class="cs__keyword">string</span>&nbsp;contentType)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">this</span>.messageEncoder.IsContentTypeSupported(contentType);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;Message&nbsp;ReadMessage(ArraySegment&lt;<span class="cs__keyword">byte</span>&gt;&nbsp;buffer,&nbsp;BufferManager&nbsp;bufferManager,&nbsp;<span class="cs__keyword">string</span>&nbsp;contentType)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">this</span>.messageEncoder.ReadMessage(buffer,&nbsp;bufferManager,&nbsp;contentType);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;Message&nbsp;ReadMessage(Stream&nbsp;stream,&nbsp;<span class="cs__keyword">int</span>&nbsp;maxSizeOfHeaders,&nbsp;<span class="cs__keyword">string</span>&nbsp;contentType)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">throw</span>&nbsp;<span class="cs__keyword">new</span>&nbsp;NotSupportedException(<span class="cs__string">&quot;Streamed&nbsp;not&nbsp;supported&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;ArraySegment&lt;<span class="cs__keyword">byte</span>&gt;&nbsp;WriteMessage(Message&nbsp;message,&nbsp;<span class="cs__keyword">int</span>&nbsp;maxMessageSize,&nbsp;BufferManager&nbsp;bufferManager,&nbsp;<span class="cs__keyword">int</span>&nbsp;messageOffset)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MemoryStream&nbsp;ms&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;MemoryStream();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlDictionaryWriter&nbsp;w&nbsp;=&nbsp;XmlDictionaryWriter.CreateBinaryWriter(ms);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;message.WriteMessage(w);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;w.Flush();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ms.Position&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlDictionaryReader&nbsp;r&nbsp;=&nbsp;XmlDictionaryReader.CreateBinaryReader(ms,&nbsp;XmlDictionaryReaderQuotas.Max);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlDocument&nbsp;doc&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;XmlDocument();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc.Load(r);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PrefixReplacer&nbsp;replacer&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;PrefixReplacer();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">foreach</span>&nbsp;(var&nbsp;mapping&nbsp;<span class="cs__keyword">in</span>&nbsp;<span class="cs__keyword">this</span>.namespaceToNewPrefixMapping)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;replacer.AddNamespace(mapping.Key,&nbsp;mapping.Value);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;replacer.ChangePrefixes(doc);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ms&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;MemoryStream();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;w&nbsp;=&nbsp;XmlDictionaryWriter.CreateBinaryWriter(ms);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc.WriteTo(w);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;w.Flush();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ms.Position&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;r&nbsp;=&nbsp;XmlDictionaryReader.CreateBinaryReader(ms,&nbsp;XmlDictionaryReaderQuotas.Max);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Message&nbsp;newMessage&nbsp;=&nbsp;Message.CreateMessage(r,&nbsp;maxMessageSize,&nbsp;message.Version);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;newMessage.Properties.CopyProperties(message.Properties);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">this</span>.messageEncoder.WriteMessage(newMessage,&nbsp;maxMessageSize,&nbsp;bufferManager,&nbsp;messageOffset);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">override</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;WriteMessage(Message&nbsp;message,&nbsp;Stream&nbsp;stream)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">throw</span>&nbsp;<span class="cs__keyword">new</span>&nbsp;NotSupportedException(<span class="cs__string">&quot;Streamed&nbsp;not&nbsp;supported&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}</pre>
</div>
</div>
</div>
<div class="endscriptcode"></div>
<h1><span>An example</span></h1>
<p><span>This example shows one way to use this new encoding. Notice that to really see the prefixes changed, you&rsquo;d need to use a network sniffer, such as
<a href="http://www.fiddlertool.com/">Fiddler</a> (which is, by the way, one of the most useful tools I use at work).</span></p>
<p><span></span></p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span>
<pre class="hidden">[ServiceContract(Name = &quot;ITest&quot;, Namespace = &quot;http://service.contract.namespace&quot;)]
public interface ICalculator
{
    [OperationContract]
    int Add(int x, int y);

    [OperationContract]
    int Subtract(int x, int y);

    [OperationContract]
    int Multiply(int x, int y);

    [OperationContract]
    int Divide(int x, int y);
}


public class CalculatorService : ICalculator
{
    public int Add(int x, int y)
    {
        return x &#43; y;
    }

    public int Subtract(int x, int y)
    {
        return x - y;
    }

    public int Multiply(int x, int y)
    {
        return x * y;
    }

    public int Divide(int x, int y)
    {
        return x / y;
    }
}


static void Main(string[] args)
{
    string baseAddress = &quot;http://&quot; &#43; Environment.MachineName &#43; &quot;:8000/Service&quot;;
    ServiceHost host = new ServiceHost(typeof(CalculatorService), new Uri(baseAddress));
    Dictionary&lt;string, string&gt; namespaceToPrefixMapping = new Dictionary&lt;string, string&gt;
    {
        { &quot;http://www.w3.org/2003/05/soap-envelope&quot;, &quot;SOAP12-ENV&quot; },
        { &quot;http://www.w3.org/2005/08/addressing&quot;, &quot;SOAP12-ADDR&quot; },
    };
    Binding binding = ReplacePrefixMessageEncodingBindingElement.ReplaceEncodingBindingElement(
        new WSHttpBinding(SecurityMode.None),
        namespaceToPrefixMapping);
    host.AddServiceEndpoint(typeof(ICalculator), binding, &quot;&quot;);
    host.Open();

    Binding clientBinding = new WSHttpBinding(SecurityMode.None);
    ChannelFactory&lt;ICalculator&gt; factory = new ChannelFactory&lt;ICalculator&gt;(clientBinding, new EndpointAddress(baseAddress));
    ICalculator proxy = factory.CreateChannel();

    Console.WriteLine(proxy.Add(234, 456));

    ((IClientChannel)proxy).Close();
    factory.Close();
    host.Close();
}</pre>
<div class="preview">
<pre class="csharp">[ServiceContract(Name&nbsp;=&nbsp;<span class="cs__string">&quot;ITest&quot;</span>,&nbsp;Namespace&nbsp;=&nbsp;<span class="cs__string">&quot;http://service.contract.namespace&quot;</span>)]&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">interface</span>&nbsp;ICalculator&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[OperationContract]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;Add(<span class="cs__keyword">int</span>&nbsp;x,&nbsp;<span class="cs__keyword">int</span>&nbsp;y);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[OperationContract]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;Subtract(<span class="cs__keyword">int</span>&nbsp;x,&nbsp;<span class="cs__keyword">int</span>&nbsp;y);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[OperationContract]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;Multiply(<span class="cs__keyword">int</span>&nbsp;x,&nbsp;<span class="cs__keyword">int</span>&nbsp;y);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;[OperationContract]&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;Divide(<span class="cs__keyword">int</span>&nbsp;x,&nbsp;<span class="cs__keyword">int</span>&nbsp;y);&nbsp;
}&nbsp;
&nbsp;
&nbsp;
<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;CalculatorService&nbsp;:&nbsp;ICalculator&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;Add(<span class="cs__keyword">int</span>&nbsp;x,&nbsp;<span class="cs__keyword">int</span>&nbsp;y)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;x&nbsp;&#43;&nbsp;y;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;Subtract(<span class="cs__keyword">int</span>&nbsp;x,&nbsp;<span class="cs__keyword">int</span>&nbsp;y)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;x&nbsp;-&nbsp;y;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;Multiply(<span class="cs__keyword">int</span>&nbsp;x,&nbsp;<span class="cs__keyword">int</span>&nbsp;y)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;x&nbsp;*&nbsp;y;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">int</span>&nbsp;Divide(<span class="cs__keyword">int</span>&nbsp;x,&nbsp;<span class="cs__keyword">int</span>&nbsp;y)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;x&nbsp;/&nbsp;y;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
}&nbsp;
&nbsp;
&nbsp;
<span class="cs__keyword">static</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;Main(<span class="cs__keyword">string</span>[]&nbsp;args)&nbsp;
{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;baseAddress&nbsp;=&nbsp;<span class="cs__string">&quot;http://&quot;</span>&nbsp;&#43;&nbsp;Environment.MachineName&nbsp;&#43;&nbsp;<span class="cs__string">&quot;:8000/Service&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ServiceHost&nbsp;host&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;ServiceHost(<span class="cs__keyword">typeof</span>(CalculatorService),&nbsp;<span class="cs__keyword">new</span>&nbsp;Uri(baseAddress));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Dictionary&lt;<span class="cs__keyword">string</span>,&nbsp;<span class="cs__keyword">string</span>&gt;&nbsp;namespaceToPrefixMapping&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;Dictionary&lt;<span class="cs__keyword">string</span>,&nbsp;<span class="cs__keyword">string</span>&gt;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;<span class="cs__string">&quot;http://www.w3.org/2003/05/soap-envelope&quot;</span>,&nbsp;<span class="cs__string">&quot;SOAP12-ENV&quot;</span>&nbsp;},&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;<span class="cs__string">&quot;http://www.w3.org/2005/08/addressing&quot;</span>,&nbsp;<span class="cs__string">&quot;SOAP12-ADDR&quot;</span>&nbsp;},&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;};&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Binding&nbsp;binding&nbsp;=&nbsp;ReplacePrefixMessageEncodingBindingElement.ReplaceEncodingBindingElement(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">new</span>&nbsp;WSHttpBinding(SecurityMode.None),&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;namespaceToPrefixMapping);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;host.AddServiceEndpoint(<span class="cs__keyword">typeof</span>(ICalculator),&nbsp;binding,&nbsp;<span class="cs__string">&quot;&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;host.Open();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Binding&nbsp;clientBinding&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;WSHttpBinding(SecurityMode.None);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ChannelFactory&lt;ICalculator&gt;&nbsp;factory&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;ChannelFactory&lt;ICalculator&gt;(clientBinding,&nbsp;<span class="cs__keyword">new</span>&nbsp;EndpointAddress(baseAddress));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;ICalculator&nbsp;proxy&nbsp;=&nbsp;factory.CreateChannel();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;Console.WriteLine(proxy.Add(<span class="cs__number">234</span>,&nbsp;<span class="cs__number">456</span>));&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;((IClientChannel)proxy).Close();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;factory.Close();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;host.Close();&nbsp;
}</pre>
</div>
</div>
</div>
<div class="endscriptcode">&nbsp;</div>
<h1>Notes about message security</h1>
<p>Notice that in the example above I set the message security to None in the WSHttpBinding constructor. If you try using a binding which uses message security (such as WSHttpBinding with its default constructor), this prefix changer will not work. The problem
 is that the default security causes the original message to be signed, and a signature header to be added to the message itself with a (signed) hash of its contents. By changing the prefix of the XML we&rsquo;d be invalidating the message signature. There
 are some ways to circumvent this problem, such as implementing the changer as a custom channel which sits on top of the security channel, so that it would change the XML prefixes before the message is signed. Building a custom channel requires a lot more code
 than a custom encoder, but if you really need it you can definitely do it as shown by the Custom Message Interceptor sample from
<a title="http://msdn.microsoft.com/en-us/library/ms751495.aspx" href="http://msdn.microsoft.com/en-us/library/ms751495.aspx">
http://msdn.microsoft.com/en-us/library/ms751495.aspx</a>.</p>
<p><em><br>
</em></p>
