# Manipulate XML in-memory by using the XmlDocument class and other related types
## Requires
- Visual Studio 2013
## License
- MS-LPL
## Technologies
- XML
- DOM
- .NET Framework 4.5
## Topics
- XML
- DOM
- XML data
- XMLDocument
## Updated
- 06/24/2014
## Description

<h1>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="pluginLinkHolder"></div>
</div>
</div>
Introduction</h1>
<p>This sample gives you a bunch of code examples that show you how to manipulate XML in-memory by using the XML Document Object Model (DOM) as implemented by types in the
<strong>System.Xml </strong>namespace of the .NET Framework.</p>
<p>When you run this sample, you'll see a basic Windows Form app that lets you view, add, edit, and reposition books in a list of available books. You do this by interacting with panes of controls and a two TreeView controls. Each book is represented by an
 element, two child elements, and several attributes.&nbsp; As you load the list of books, add new ones or edit existing ones, you can choose to validate the data that you enter against a schema.&nbsp;</p>
<p>The <strong>XMLHelperMethods</strong> file in this solution contains most of implementation code. That code shows you how to use methods and properties in the
<strong>XmlDocument</strong> class, <strong>XmlNode</strong> class and several other related types in the
<strong>System.Xml </strong>namespace. Methods in this file are organized in the following groups (regions):</p>
<ul>
<li>Load and save XML. </li><li>Validate XML against a schema. </li><li>Find XML elements and attributes. </li><li>Add XML elements and attributes. </li><li>Edit XML elements and attributes. </li><li>Remove elements. </li><li>Position elements. </li></ul>
<h1><span style="font-size:20px; font-weight:bold">Description</span></h1>
<p>The following snippets show you&nbsp;the examples featured in the <strong>XMLHelperMethods
</strong>file</p>
<div class="scriptcode">
<div class="pluginEditHolder" pluginCommand="mceScriptCode">
<div class="title"><span>C#</span><span>C&#43;&#43;</span><span>Visual Basic</span></div>
<div class="pluginLinkHolder"><span class="pluginEditHolderLink">Edit</span>|<span class="pluginRemoveHolderLink">Remove</span></div>
<span class="hidden">csharp</span><span class="hidden">cplusplus</span><span class="hidden">vb</span>
<pre class="hidden">     public class XMLHelperMethods
    {
        #region Load and save XML
        //************************************************************************************
        //
        //  Loads XML from a file. If the file is not found, load XML from a string.
        //
        //************************************************************************************
 
        public XmlDocument LoadDocument(bool generateXML)
        {
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            try
            {
                doc.Load(&quot;booksData.xml&quot;);
            }
            catch (System.IO.FileNotFoundException)
            {
                // If specifies that they want to generate XML when the data file is
                // not found, then generate some XML for them to start with.
                if (generateXML)
                {
                    string xml = generateXMLString();
                    doc.LoadXml(xml);
                }
                else
                {
                    return null;
                }               
            }
            return doc;
        }

        //************************************************************************************
        //
        //  Helper method that generates an XML string.
        //
        //************************************************************************************
 
        private string generateXMLString()
        {
            string xml = &quot;&lt;?xml version=\&quot;1.0\&quot;?&gt; \n&quot; &#43;
                &quot;&lt;books xmlns=\&quot;http://www.contoso.com/books\&quot;&gt; \n&quot; &#43;
                &quot;  &lt;book genre=\&quot;novel\&quot; ISBN=\&quot;1-861001-57-8\&quot; publicationdate=\&quot;1823-01-28\&quot;&gt; \n&quot; &#43;
                &quot;    &lt;title&gt;Pride And Prejudice&lt;/title&gt; \n&quot; &#43;
                &quot;    &lt;price&gt;24.95&lt;/price&gt; \n&quot; &#43;
                &quot;  &lt;/book&gt; \n&quot; &#43;
                &quot;  &lt;book genre=\&quot;novel\&quot; ISBN=\&quot;1-861002-30-1\&quot; publicationdate=\&quot;1985-01-01\&quot;&gt; \n&quot; &#43;
                &quot;    &lt;title&gt;The Handmaid's Tale&lt;/title&gt; \n&quot; &#43;
                &quot;    &lt;price&gt;29.95&lt;/price&gt; \n&quot; &#43;
                &quot;  &lt;/book&gt; \n&quot; &#43;
                &quot;  &lt;book genre=\&quot;novel\&quot; ISBN=\&quot;1-861001-45-3\&quot; publicationdate=\&quot;1811-01-01\&quot;&gt; \n&quot; &#43;
                &quot;    &lt;title&gt;Sense and Sensibility&lt;/title&gt; \n&quot; &#43;
                &quot;    &lt;price&gt;19.95&lt;/price&gt; \n&quot; &#43;
                &quot;  &lt;/book&gt; \n&quot; &#43;
                &quot;&lt;/books&gt;&quot;;
            return xml;
        }


        //************************************************************************************
        //
        //  Loads XML from a file. If the file is not found, load XML from a string.
        //
        //************************************************************************************
        public void SaveXML(XmlDocument doc)
        {
            doc.Save(Constants.booksFileName);
        }
        #endregion

        #region Validate XML against a Schema


        //************************************************************************************
        //
        //  Associate the schema with XML. Then, load the XML and validate it against
        //  the schema.
        //
        //************************************************************************************
        public XmlDocument LoadDocumentWithSchemaValidation(bool generateXML, bool generateSchema)
        {
            XmlReader reader;

            XmlReaderSettings settings = new XmlReaderSettings();

            // Helper method to retrieve schema.
            XmlSchema schema = getSchema(generateSchema);

            if (schema == null)
            {
                return null;
            }

            settings.Schemas.Add(schema);

            settings.ValidationEventHandler &#43;= settings_ValidationEventHandler;
            settings.ValidationFlags =
                settings.ValidationFlags | XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationType = ValidationType.Schema;

            try
            {
                reader = XmlReader.Create(&quot;booksData.xml&quot;, settings);
            }
            catch (System.IO.FileNotFoundException)
            {
                if (generateXML)
                {
                    string xml = generateXMLString();
                    byte[] byteArray = Encoding.UTF8.GetBytes(xml);
                    MemoryStream stream = new MemoryStream(byteArray);
                    reader = XmlReader.Create(stream, settings);
                }
                else
                {
                    return null;
                }

            }

            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.Load(reader);
            reader.Close();

            return doc;
        }

        //************************************************************************************
        //
        //  Helper method that generates an XML Schema.
        //
        //************************************************************************************
        private string generateXMLSchema()
        {
            string xmlSchema =
                &quot;&lt;?xml version=\&quot;1.0\&quot; encoding=\&quot;utf-8\&quot;?&gt; &quot; &#43;
                &quot;&lt;xs:schema attributeFormDefault=\&quot;unqualified\&quot; &quot; &#43;
                &quot;elementFormDefault=\&quot;qualified\&quot; targetNamespace=\&quot;http://www.contoso.com/books\&quot; &quot; &#43;
                &quot;xmlns:xs=\&quot;http://www.w3.org/2001/XMLSchema\&quot;&gt; &quot; &#43;
                &quot;&lt;xs:element name=\&quot;books\&quot;&gt; &quot; &#43;
                &quot;&lt;xs:complexType&gt; &quot; &#43;
                &quot;&lt;xs:sequence&gt; &quot; &#43;
                &quot;&lt;xs:element maxOccurs=\&quot;unbounded\&quot; name=\&quot;book\&quot;&gt; &quot; &#43;
                &quot;&lt;xs:complexType&gt; &quot; &#43;
                &quot;&lt;xs:sequence&gt; &quot; &#43;
                &quot;&lt;xs:element name=\&quot;title\&quot; type=\&quot;xs:string\&quot; /&gt; &quot; &#43;
                &quot;&lt;xs:element name=\&quot;price\&quot; type=\&quot;xs:decimal\&quot; /&gt; &quot; &#43;
                &quot;&lt;/xs:sequence&gt; &quot; &#43;
                &quot;&lt;xs:attribute name=\&quot;genre\&quot; type=\&quot;xs:string\&quot; use=\&quot;required\&quot; /&gt; &quot; &#43;
                &quot;&lt;xs:attribute name=\&quot;publicationdate\&quot; type=\&quot;xs:date\&quot; use=\&quot;required\&quot; /&gt; &quot; &#43;
                &quot;&lt;xs:attribute name=\&quot;ISBN\&quot; type=\&quot;xs:string\&quot; use=\&quot;required\&quot; /&gt; &quot; &#43;
                &quot;&lt;/xs:complexType&gt; &quot; &#43;
                &quot;&lt;/xs:element&gt; &quot; &#43;
                &quot;&lt;/xs:sequence&gt; &quot; &#43;
                &quot;&lt;/xs:complexType&gt; &quot; &#43;
                &quot;&lt;/xs:element&gt; &quot; &#43;
                &quot;&lt;/xs:schema&gt; &quot;;
            return xmlSchema;
        }

        //************************************************************************************
        //
        //  Helper method that gets a schema
        //
        //************************************************************************************
        private XmlSchema getSchema(bool generateSchema)
        {
            XmlSchemaSet xs = new XmlSchemaSet();
            XmlSchema schema;
            try
            {
                schema = xs.Add(&quot;http://www.contoso.com/books&quot;, &quot;booksData.xsd&quot;);
            }
            catch (System.IO.FileNotFoundException)
            {
                if (generateSchema)
                {
                    string xmlSchemaString = generateXMLSchema();
                    byte[] byteArray = Encoding.UTF8.GetBytes(xmlSchemaString);
                    MemoryStream stream = new MemoryStream(byteArray);
                    XmlReader reader = XmlReader.Create(stream);
                    schema = xs.Add(&quot;http://www.contoso.com/books&quot;, reader);
                }
                else
                {
                    return null;
                }

            }
            return schema;
        }

        //************************************************************************************
        //
        //  Helper method to validate the XML against the schema.
        //
        //************************************************************************************
        private void validateXML(bool generateSchema, XmlDocument doc)
        {
            if (doc.Schemas.Count == 0)
            {
                // Helper method to retrieve schema.
                XmlSchema schema = getSchema(generateSchema);
                doc.Schemas.Add(schema);
            }

            // Use an event handler to validate the XML node against the schema.
            doc.Validate(settings_ValidationEventHandler);

        }

        //************************************************************************************
        //
        //  Event handler that is raised when XML doesn't validate against the schema.
        //
        //************************************************************************************
        void settings_ValidationEventHandler(object sender,
            System.Xml.Schema.ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                System.Windows.Forms.MessageBox.Show
                    (&quot;The following validation warning occurred: &quot; &#43; e.Message);
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                System.Windows.Forms.MessageBox.Show
                    (&quot;The following critical validation errors occurred: &quot; &#43; e.Message);
                Type objectType = sender.GetType();
            }

        }
        #endregion

        #region Find XML elements and attributes

        //************************************************************************************
        //
        //  Search the XML tree for a specific XMLNode element by using an attribute value.
        //  Description: Must identify the namespace of the nodes and define a prefix. Also include the 
        //  prefix in the XPath string.
        //
        //************************************************************************************
        public XmlNode GetBook(string uniqueAttribute, XmlDocument doc)
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace(&quot;bk&quot;, &quot;http://www.contoso.com/books&quot;);
            string xPathString = &quot;//bk:books/bk:book[@ISBN='&quot; &#43; uniqueAttribute &#43; &quot;']&quot;;
            XmlNode xmlNode = doc.DocumentElement.SelectSingleNode(xPathString, nsmgr);
           return xmlNode;
        }
        //************************************************************************************
        //
        //  Get information about a specific book. Pass in an XMLNode that 
        //  represents the book and populate strings passed in by reference.
        //
        //  **********************************************************************************  
        public void GetBookInformation(ref string title, ref string ISBN, ref string publicationDate,
            ref string price, ref string genre, XmlNode book)
        {
            XmlElement bookElement = (XmlElement)book;

            // Get the attributes of a book.        
            XmlAttribute attr = bookElement.GetAttributeNode(&quot;ISBN&quot;);
            ISBN = attr.InnerXml;

            attr = bookElement.GetAttributeNode(&quot;genre&quot;);
            genre = attr.InnerXml;

            attr = bookElement.GetAttributeNode(&quot;publicationdate&quot;);
            publicationDate = attr.InnerXml;

            // Get the values of child elements of a book.
            title = bookElement[&quot;title&quot;].InnerText;
            price = bookElement[&quot;price&quot;].InnerText;
        }
        //************************************************************************************
        //
        //  Uses filter criteria collection in the UI to retreive specific elements and attributes.
        //
        //************************************************************************************
        public XmlNodeList ApplyFilters(ArrayList conditions, ArrayList operatorSymbols,
            ArrayList values, XmlDocument doc, string matchString)
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace(&quot;bk&quot;, &quot;http://www.contoso.com/books&quot;);

            string xPathQueryString = &quot;//bk:books/bk:book[&quot;;
            string xPathQueryEnding = &quot;]&quot;;
            ArrayList xPathQueryStrings = new ArrayList();
            string booleanOperator;
            if (matchString == &quot;Any&quot;)
            {
                booleanOperator = &quot;or &quot;;
            }
            else
            {
                booleanOperator = &quot;and &quot;;
            }
            int counter = 0;
            string[] operatorArray = (string[])operatorSymbols.ToArray(typeof(string));
            string[] valueArray = (string[])values.ToArray(typeof(string));

            foreach (string condition in conditions)
            {
                string xPathQueryPart = &quot;&quot;;
                string operatorSymbol = operatorArray[counter];
                string value = valueArray[counter];
                if (counter &gt; 0)
                {
                    xPathQueryString = xPathQueryString &#43; booleanOperator;
                }
                counter&#43;&#43;;

                switch (condition)
                {
                    case Constants.Title:
                        switch (operatorSymbol)
                        {
                            case &quot;Contains&quot;:
                                xPathQueryPart = &quot;contains(bk:title,'&quot; &#43; value &#43; &quot;')&quot;;
                                break;
                            case &quot;Excludes&quot;:
                                xPathQueryPart = &quot;not(contains(bk:title,'&quot; &#43; value &#43; &quot;'))&quot;;
                                break;
                            case &quot;=&quot;:
                                xPathQueryPart = &quot;bk:title='&quot; &#43; value &#43; &quot;'&quot;;
                                break;
                        }
                        break;
                    case Constants.ISBN:
                        switch (operatorSymbol)
                        {
                            case &quot;Contains&quot;:
                                xPathQueryPart = &quot;contains(@ISBN, '&quot; &#43; value &#43; &quot;')&quot;;
                                break;
                            case &quot;Excludes&quot;:
                                xPathQueryPart = &quot;not(contains(@ISBN, '&quot; &#43; value &#43; &quot;'))&quot;;
                                break;
                            case &quot;=&quot;:
                                xPathQueryPart = &quot;@ISBN='&quot; &#43; value &#43; &quot;'&quot;;
                                break;
                        }
                        break;
                    case Constants.PubDate:
                        xPathQueryPart = &quot;contains(@publicationdate, '&quot; &#43; value &#43; &quot;')&quot;;
                        break;
                    case Constants.Price:
                        switch (operatorSymbol)
                        {
                            case &quot;=&quot;:
                                xPathQueryPart = &quot;bk:price='&quot; &#43; value &#43; &quot;'&quot;;
                                break;
                            case &quot;&gt;&quot;:
                                xPathQueryPart = &quot;bk:price&gt;'&quot; &#43; value &#43; &quot;'&quot;;
                                break;
                            case &quot;&lt;&quot;:
                                xPathQueryPart = &quot;bk:price&lt;'&quot; &#43; value &#43; &quot;'&quot;;
                                break;
                            case &quot;&gt;=&quot;:
                                xPathQueryPart = &quot;bk:price&gt;='&quot; &#43; value &#43; &quot;'&quot;;
                                break;
                            case &quot;&lt;=&quot;:
                                xPathQueryPart = &quot;bk:price&lt;='&quot; &#43; value &#43; &quot;'&quot;;
                                break;
                            case &quot;&lt;&gt;&quot;:
                                xPathQueryPart = &quot;bk:price!='&quot; &#43; value &#43; &quot;'&quot;;
                                break;
                        }
                        break;
                    case Constants.Genre:
                        xPathQueryPart = &quot;@genre='&quot; &#43; value &#43; &quot;'&quot;;
                        break;

                }

                xPathQueryString = xPathQueryString &#43; xPathQueryPart;
            }

            xPathQueryString = xPathQueryString &#43; xPathQueryEnding;

            XmlNodeList nodeList = doc.DocumentElement.SelectNodes(xPathQueryString, nsmgr);

            return nodeList;
        }

        #endregion

        #region Add XML elements and attributes
        //************************************************************************************
        //
        //  Add an element to the XML document.
        //  This method creates a new book element and saves that element to the 
        //  XMLDocument object. It addes attributes for the new element and introduces
        //  newline characters between elements fora nice readable format.
        //  
        //
        //************************************************************************************

        public XmlElement AddNewBook(string genre, string ISBN, string misc, 
            string title, string price, XmlDocument doc)
        {
            // Create a new book element.
            XmlElement bookElement = doc.CreateElement(&quot;book&quot;, &quot;http://www.contoso.com/books&quot;);

            // Create attributes for book and append them to the book element.
            XmlAttribute attribute = doc.CreateAttribute(&quot;genre&quot;);
            attribute.Value = genre;
            bookElement.Attributes.Append(attribute);

            attribute = doc.CreateAttribute(&quot;ISBN&quot;);
            attribute.Value = ISBN;
            bookElement.Attributes.Append(attribute);

            attribute = doc.CreateAttribute(&quot;publicationdate&quot;);
            attribute.Value = misc;
            bookElement.Attributes.Append(attribute);

            // Create and append a child element for the title of the book.
            XmlElement titleElement = doc.CreateElement(&quot;title&quot;);
            titleElement.InnerText = title;
            bookElement.AppendChild(titleElement);

            // Introduce a newline character so that XML is nicely formatted.
            bookElement.InnerXml = 
                bookElement.InnerXml.Replace(titleElement.OuterXml, 
                &quot;\n    &quot; &#43; titleElement.OuterXml &#43; &quot; \n    &quot;);
            
            // Create and append a child element for the price of the book.
            XmlElement priceElement = doc.CreateElement(&quot;price&quot;);
            priceElement.InnerText= price;
            bookElement.AppendChild(priceElement);

            // Introduce a newline character so that XML is nicely formatted.
            bookElement.InnerXml =
                bookElement.InnerXml.Replace(priceElement.OuterXml, priceElement.OuterXml &#43; &quot;   \n  &quot;);

            return bookElement;
             
        }

        //************************************************************************************
        //
        //  Add an element to the XML document at a specific location
        //  Takes a string that describes where the user wants the new node
        //  to be positioned. The string comes from a series of radio buttons in a UI.
        //  this method also accepts the XMLDocument in context. You have to use the 
        //  this instance because it is the object that was used to generate the 
        //  selectedBook XMLNode.
        //
        //************************************************************************************

        public void InsertBookElement(XmlElement bookElement, string position, XmlNode selectedBook, bool validateNode, bool generateSchema)
        {
            XmlDocument doc = bookElement.OwnerDocument;

            string stringThatContainsNewline = bookElement.OuterXml;

            switch (position)
            {
                case Constants.positionTop:
                    // Add newline characters and spaces to make XML more readable.
                    XmlSignificantWhitespace sigWhiteSpace = doc.CreateSignificantWhitespace(&quot;\n  &quot;);
                    doc.DocumentElement.InsertBefore(sigWhiteSpace, doc.DocumentElement.FirstChild);
                    doc.DocumentElement.InsertAfter(bookElement, doc.DocumentElement.FirstChild);
                    break;

                case Constants.positionBottom:
                    // Add newline characters to make XML more readable.
                    XmlWhitespace whitespace = doc.CreateWhitespace(&quot;  &quot;);
                    XmlNode appendedNode = doc.DocumentElement.AppendChild(bookElement);
                    doc.DocumentElement.InsertBefore(whitespace, appendedNode);
                    sigWhiteSpace = doc.CreateSignificantWhitespace(&quot;\n&quot;);
                    doc.DocumentElement.InsertAfter(sigWhiteSpace, appendedNode);                 
                    break;
                
                case Constants.positionAbove:
                    // Add newline characters to make XML more readable.
                    XmlNode currNode = doc.DocumentElement.InsertBefore(bookElement, selectedBook);
                    sigWhiteSpace = doc.CreateSignificantWhitespace(&quot;\n  &quot;);
                    doc.DocumentElement.InsertAfter(sigWhiteSpace, currNode);
                    break;
                
                case Constants.positionBelow:
                    // Add newline characters to make XML more readable.
                    sigWhiteSpace = doc.CreateSignificantWhitespace(&quot;\n  &quot;);
                    XmlNode whiteSpaceNode = doc.DocumentElement.InsertAfter(sigWhiteSpace, selectedBook);
                    doc.DocumentElement.InsertAfter(bookElement, whiteSpaceNode);                  
                    break;
                
                default:
                    doc.DocumentElement.AppendChild(bookElement);                    
                    break;
            }

            if (validateNode)
            {
                validateXML(generateSchema, doc);
            }
        }

        #endregion

        #region Edit XML elements and attributes

        //************************************************************************************
        //
        //  Edit an XML element
        //
        //************************************************************************************
        public void editBook(string title, string ISBN, string publicationDate,
            string genre, string price, XmlNode book, bool validateNode, bool generateSchema)
        {

            XmlElement bookElement = (XmlElement)book;

            // Get the attributes of a book.        
            bookElement.SetAttribute(&quot;ISBN&quot;, ISBN);
            bookElement.SetAttribute(&quot;genre&quot;, genre);
            bookElement.SetAttribute(&quot;publicationdate&quot;, publicationDate);

            // Get the values of child elements of a book.
            bookElement[&quot;title&quot;].InnerText = title;
            bookElement[&quot;price&quot;].InnerText = price;

            if (validateNode)
            {
                validateXML(generateSchema, bookElement.OwnerDocument);
            }
        }

        #endregion

        #region Remove elements

        //************************************************************************************
        //
        //  Summary: Delete a book node from the XMLDocument.
        //  
        //
        //************************************************************************************
        public void deleteBook(XmlNode book)
        {
            XmlNode prevNode = book.PreviousSibling;
            XmlNode nextNode = book.NextSibling;
            
            book.OwnerDocument.DocumentElement.RemoveChild(book);


            if (prevNode.NodeType == XmlNodeType.Whitespace || 
                prevNode.NodeType == XmlNodeType.SignificantWhitespace)
            {
                prevNode.OwnerDocument.DocumentElement.RemoveChild(prevNode);
            }
        }
        #endregion

        #region Position elements
        
        //************************************************************************************
        //
        //  Summary: Move elements up in the XML.
        //  
        //
        //************************************************************************************
        public void MoveElementUp(XmlNode book)
        {
            XmlNode previousNode = book.PreviousSibling;
            while (previousNode != null &amp;&amp; (previousNode.NodeType != XmlNodeType.Element))
            {
                previousNode = previousNode.PreviousSibling;
            }
            if (previousNode != null)
            {
                XmlNode newLineNode = book.NextSibling;
                book.OwnerDocument.DocumentElement.RemoveChild(book);
                if (newLineNode.NodeType == XmlNodeType.Whitespace | 
                    newLineNode.NodeType == XmlNodeType.SignificantWhitespace)
                {
                    newLineNode.OwnerDocument.DocumentElement.RemoveChild(newLineNode);
                }
                InsertBookElement((XmlElement)book, Constants.positionAbove, 
                    previousNode, false, false);
            }
        }

        //************************************************************************************
        //
        //  Summary: Move elements down in the XML.
        //  
        //
        //************************************************************************************
        public void MoveElementDown(XmlNode book)
        {
            // Walk backwards until we find an element - ignore text nodes
            XmlNode NextNode = book.NextSibling;
            while (NextNode != null &amp;&amp; (NextNode.NodeType != XmlNodeType.Element))
            {
                NextNode = NextNode.NextSibling;
            }
            if (NextNode != null)
            {
                XmlNode newLineNode = book.PreviousSibling;
                book.OwnerDocument.DocumentElement.RemoveChild(book);
                if (newLineNode.NodeType == XmlNodeType.Whitespace |
                    newLineNode.NodeType == XmlNodeType.SignificantWhitespace)
                {
                    newLineNode.OwnerDocument.DocumentElement.RemoveChild(newLineNode);
                }

                InsertBookElement((XmlElement)book, Constants.positionBelow, 
                    NextNode, false, false);
            }

        }
        #endregion


    }</pre>
<pre class="hidden">#pragma region Load and Save XML
//************************************************************************************
//
//  Loads XML from a file. If the file is not found, load XML from a string.
//
//************************************************************************************

XmlDocument ^XMLDOMProcessing::XMLHelperMethods::LoadDocument(bool generateXML)
{
	XmlDocument ^doc = gcnew XmlDocument();
	doc-&gt;PreserveWhitespace = true;
	try
	{
		doc-&gt;Load(&quot;booksData.xml&quot;);
	}
	catch (System::IO::FileNotFoundException ^e1)
	{
		// If specifies that they want to generate XML when the data file is
		// not found, then generate some XML for them to start with.
		if (generateXML)
		{
			String ^xml = generateXMLString();
			doc-&gt;LoadXml(xml);
		}
		else
		{
			return nullptr;
		}
	}
	return doc;
}

//************************************************************************************
//
//  Helper method that generates an XML string.
//
//************************************************************************************


String ^XMLDOMProcessing::XMLHelperMethods::generateXMLString()
{
	String ^xml = &quot;&lt;?xml version=\&quot;1.0\&quot;?&gt; \n&quot; &#43; &quot;&lt;books xmlns=\&quot;http://www.contoso.com/books\&quot;&gt; \n&quot; &#43; &quot;  &lt;book genre=\&quot;novel\&quot; ISBN=\&quot;1-861001-57-8\&quot; publicationdate=\&quot;1823-01-28\&quot;&gt; \n&quot; &#43; &quot;    &lt;title&gt;Pride And Prejudice&lt;/title&gt; \n&quot; &#43; &quot;    &lt;price&gt;24.95&lt;/price&gt; \n&quot; &#43; &quot;  &lt;/book&gt; \n&quot; &#43; &quot;  &lt;book genre=\&quot;novel\&quot; ISBN=\&quot;1-861002-30-1\&quot; publicationdate=\&quot;1985-01-01\&quot;&gt; \n&quot; &#43; &quot;    &lt;title&gt;The Handmaid's Tale&lt;/title&gt; \n&quot; &#43; &quot;    &lt;price&gt;29.95&lt;/price&gt; \n&quot; &#43; &quot;  &lt;/book&gt; \n&quot; &#43; &quot;  &lt;book genre=\&quot;novel\&quot; ISBN=\&quot;1-861001-45-3\&quot; publicationdate=\&quot;1811-01-01\&quot;&gt; \n&quot; &#43; &quot;    &lt;title&gt;Sense and Sensibility&lt;/title&gt; \n&quot; &#43; &quot;    &lt;price&gt;19.95&lt;/price&gt; \n&quot; &#43; &quot;  &lt;/book&gt; \n&quot; &#43; &quot;&lt;/books&gt;&quot;;
	return xml;
}

//************************************************************************************
//
//  Loads XML from a file. If the file is not found, load XML from a string.
//
//************************************************************************************

void XMLDOMProcessing::XMLHelperMethods::SaveXML(XmlDocument ^doc)
{
	doc-&gt;Save(Constants::booksFileName);
}
#pragma endregion

#pragma region Validate XML against a Schema

//************************************************************************************
//
//  Associate the schema with XML. Then, load the XML and validate it against
//  the schema.
//
//************************************************************************************

XmlDocument ^XMLDOMProcessing::XMLHelperMethods::LoadDocumentWithSchemaValidation(bool generateXML, bool generateSchema)
{
	XmlReader ^reader;

	XmlReaderSettings ^settings = gcnew XmlReaderSettings();

	// Helper method to retrieve schema.
	XmlSchema ^schema = getSchema(generateSchema);
	
	if (schema == nullptr)
	{
		return nullptr;
	}

	settings-&gt;Schemas-&gt;Add(schema);
	settings-&gt;ValidationEventHandler &#43;=
		gcnew System::Xml::Schema::ValidationEventHandler
		(this, &amp;XMLDOMProcessing::XMLHelperMethods::OnValidationEventHandler);
	settings-&gt;ValidationFlags = settings-&gt;ValidationFlags | XmlSchemaValidationFlags::ReportValidationWarnings;
	settings-&gt;ValidationType = ValidationType::Schema;

	try
	{
		reader = XmlReader::Create(&quot;booksData.xml&quot;, settings);
	}
	catch (System::IO::FileNotFoundException ^e1)
	{
		if (generateXML)
		{
			String ^xml = generateXMLString();
			array&lt;Byte&gt; ^byteArray = Encoding::UTF8-&gt;GetBytes(xml);
			MemoryStream ^stream = gcnew MemoryStream(byteArray);
			reader = XmlReader::Create(stream, settings);
		}
		else
		{
			return nullptr;
		}

	}

	XmlDocument ^doc = gcnew XmlDocument();
	doc-&gt;PreserveWhitespace = true;
	doc-&gt;Load(reader);
	reader-&gt;Close();

	return doc;
}

//************************************************************************************
//
//  Helper method that generates an XML Schema.
//
//************************************************************************************

String ^XMLDOMProcessing::XMLHelperMethods::generateXMLSchema()
{
	String ^xmlSchema = &quot;&lt;?xml version=\&quot;1.0\&quot; encoding=\&quot;utf-8\&quot;?&gt; &quot; &#43; 
		&quot;&lt;xs:schema attributeFormDefault=\&quot;unqualified\&quot; &quot; &#43; 
		&quot;elementFormDefault=\&quot;qualified\&quot; targetNamespace=\&quot;http://www.contoso.com/books\&quot; &quot; &#43; 
		&quot;xmlns:xs=\&quot;http://www.w3.org/2001/XMLSchema\&quot;&gt; &quot; &#43; &quot;&lt;xs:element name=\&quot;books\&quot;&gt; &quot; &#43; 
		&quot;&lt;xs:complexType&gt; &quot; &#43; &quot;&lt;xs:sequence&gt; &quot; &#43; &quot;&lt;xs:element maxOccurs=\&quot;unbounded\&quot; name=\&quot;book\&quot;&gt; &quot; &#43; 
		&quot;&lt;xs:complexType&gt; &quot; &#43; &quot;&lt;xs:sequence&gt; &quot; &#43; &quot;&lt;xs:element name=\&quot;title\&quot; type=\&quot;xs:string\&quot; /&gt; &quot; &#43; 
		&quot;&lt;xs:element name=\&quot;price\&quot; type=\&quot;xs:decimal\&quot; /&gt; &quot; &#43; &quot;&lt;/xs:sequence&gt; &quot; &#43; 
		&quot;&lt;xs:attribute name=\&quot;genre\&quot; type=\&quot;xs:string\&quot; use=\&quot;required\&quot; /&gt; &quot; &#43; 
		&quot;&lt;xs:attribute name=\&quot;publicationdate\&quot; type=\&quot;xs:date\&quot; use=\&quot;required\&quot; /&gt; &quot; &#43; 
		&quot;&lt;xs:attribute name=\&quot;ISBN\&quot; type=\&quot;xs:string\&quot; use=\&quot;required\&quot; /&gt; &quot; &#43; &quot;&lt;/xs:complexType&gt; &quot; &#43; 
		&quot;&lt;/xs:element&gt; &quot; &#43; &quot;&lt;/xs:sequence&gt; &quot; &#43; &quot;&lt;/xs:complexType&gt; &quot; &#43; &quot;&lt;/xs:element&gt; &quot; &#43; &quot;&lt;/xs:schema&gt; &quot;;
	
	return xmlSchema;
}

//************************************************************************************
//
//  Helper method that gets a schema
//
//************************************************************************************

XmlSchema ^XMLDOMProcessing::XMLHelperMethods::getSchema(bool generateSchema)
{
	XmlSchemaSet ^xs = gcnew XmlSchemaSet();
	XmlSchema ^schema;
	try
	{
		schema = xs-&gt;Add(&quot;http://www.contoso.com/books&quot;, &quot;booksData.xsd&quot;);
	}
	catch (System::IO::FileNotFoundException ^e1)
	{
		if (generateSchema)
		{
			String ^xmlSchemaString = generateXMLSchema();
			array&lt;Byte&gt; ^byteArray = Encoding::UTF8-&gt;GetBytes(xmlSchemaString);
			MemoryStream ^stream = gcnew MemoryStream(byteArray);
			XmlReader ^reader = XmlReader::Create(stream);
			schema = xs-&gt;Add(&quot;http://www.contoso.com/books&quot;, reader);
		}
		else
		{
			return nullptr;
		}

	}
	return schema;
}
//************************************************************************************
//
//  Helper method to validate the XML against the schema.
//
//************************************************************************************
bool XMLDOMProcessing::XMLHelperMethods::validateXML(bool generateSchema, XmlDocument ^doc)
{
	if (doc-&gt;Schemas-&gt;Count == 0)
	{
		// Helper method to retrieve schema.
		XmlSchema ^schema = getSchema(generateSchema);
		doc-&gt;Schemas-&gt;Add(schema);
	}

	ValidationEventHandler^ eventHandler = gcnew System::Xml::Schema::ValidationEventHandler
		(this, &amp;XMLDOMProcessing::XMLHelperMethods::OnValidationEventHandler);

	// Use an event handler to validate the XML node against the schema.
	doc-&gt;Validate(eventHandler);

	if (_IsValid == false)
	{
		_IsValid = true;
		return false;
	}
	else
	{
		return true;
	}

}
//************************************************************************************
//
//  Event handler that is raised when XML doesn't validate against the schema.
//
//************************************************************************************

void XMLDOMProcessing::XMLHelperMethods::OnValidationEventHandler(System::Object ^sender, System::Xml::Schema::ValidationEventArgs ^e)
{
	if (e-&gt;Severity == XmlSeverityType::Warning)
	{
		// do nothing.
	}
	else if (e-&gt;Severity == XmlSeverityType::Error)
	{
		// set some global variables.
		_IsValid = false;
		ValidationError = e-&gt;Message;
	}
}

#pragma endregion

#pragma region Find elements and attributes

//************************************************************************************
//
//  Search the XML tree for a specific XMLNode element by using an attribute value.
//  Description: Must identify the namespace of the nodes and define a prefix. Also include the 
//  prefix in the XPath string.
//
//************************************************************************************

XmlNode ^XMLDOMProcessing::XMLHelperMethods::GetBook(String ^uniqueAttribute, XmlDocument ^doc)
{
	XmlNamespaceManager ^nsmgr = gcnew XmlNamespaceManager(doc-&gt;NameTable);
	nsmgr-&gt;AddNamespace(&quot;bk&quot;, &quot;http://www.contoso.com/books&quot;);
	String ^xPathString = &quot;//bk:books/bk:book[@ISBN='&quot; &#43; uniqueAttribute &#43; &quot;']&quot;;
	XmlNode ^xmlNode = doc-&gt;DocumentElement-&gt;SelectSingleNode(xPathString, nsmgr);
	return xmlNode;
}

//************************************************************************************
//
//  Get information about a specific book. Pass in an XMLNode that 
//  represents the book and populate strings passed in by reference.
//
//  **********************************************************************************  

void XMLDOMProcessing::XMLHelperMethods::GetBookInformation
(String ^%title, String ^%ISBN, String ^%publicationDate, String ^%price, String ^%genre, XmlNode ^book)
{
	XmlElement ^bookElement = safe_cast&lt;XmlElement^&gt;(book);

	// Get the attributes of a book.        
	XmlAttribute ^attr = bookElement-&gt;GetAttributeNode(&quot;ISBN&quot;);
	ISBN = attr-&gt;InnerXml;

	attr = bookElement-&gt;GetAttributeNode(&quot;genre&quot;);
	genre = attr-&gt;InnerXml;

	attr = bookElement-&gt;GetAttributeNode(&quot;publicationdate&quot;);
	publicationDate = attr-&gt;InnerXml;

	// Get the values of child elements of a book.
	title = bookElement[&quot;title&quot;]-&gt;InnerText;
	price = bookElement[&quot;price&quot;]-&gt;InnerText;
}

//************************************************************************************
//
//  Uses filter criteria collection in the UI to retreive specific elements and attributes.
//
//***********^************************************************************************

XmlNodeList ^XMLDOMProcessing::XMLHelperMethods::ApplyFilters(ArrayList ^conditions, ArrayList ^operatorSymbols, ArrayList ^values, XmlDocument ^doc, String ^matchString)
{
	XmlNamespaceManager ^nsmgr = gcnew XmlNamespaceManager(doc-&gt;NameTable);
	nsmgr-&gt;AddNamespace(&quot;bk&quot;, &quot;http://www.contoso.com/books&quot;);

	String ^xPathQueryString = &quot;//bk:books/bk:book[&quot;;
	String ^xPathQueryEnding = &quot;]&quot;;
	ArrayList ^xPathQueryStrings = gcnew ArrayList();
	String ^booleanOperator;
	if (matchString == &quot;Any&quot;)
	{
		booleanOperator = &quot;or &quot;;
	}
	else
	{
		booleanOperator = &quot;and &quot;;
	}
	int counter = 0;
	array&lt;String^&gt; ^operatorArray = safe_cast&lt;array&lt;String^&gt;^&gt;(operatorSymbols-&gt;ToArray(String::typeid));
	array&lt;String^&gt; ^valueArray = safe_cast&lt;array&lt;String^&gt;^&gt;(values-&gt;ToArray(String::typeid));

	for each (String ^condition in conditions)
	{
		String ^xPathQueryPart = &quot;&quot;;
		String ^operatorSymbol = operatorArray[counter];
		String ^value = valueArray[counter];
		if (counter &gt; 0)
		{
			xPathQueryString = xPathQueryString &#43; booleanOperator;
		}
		counter&#43;&#43;;

		if (condition == Constants::Title)
		{
			if (operatorSymbol == &quot;Contains&quot;)
			{
				xPathQueryPart = &quot;contains(bk:title,'&quot; &#43; value &#43; &quot;')&quot;;
			}
			else if (operatorSymbol == &quot;Excludes&quot;)
			{
				xPathQueryPart = &quot;not(contains(bk:title,'&quot; &#43; value &#43; &quot;'))&quot;;
			}
			else if (operatorSymbol == &quot;=&quot;)
			{
				xPathQueryPart = &quot;bk:title='&quot; &#43; value &#43; &quot;'&quot;;
			}
		}
		else if (condition == Constants::ISBN)
		{
			if (operatorSymbol == &quot;Contains&quot;)
			{
				xPathQueryPart = &quot;contains(@ISBN, '&quot; &#43; value &#43; &quot;')&quot;;
			}
			else if (operatorSymbol == &quot;Excludes&quot;)
			{
				xPathQueryPart = &quot;not(contains(@ISBN, '&quot; &#43; value &#43; &quot;'))&quot;;
			}
			else if (operatorSymbol == &quot;=&quot;)
			{
				xPathQueryPart = &quot;@ISBN='&quot; &#43; value &#43; &quot;'&quot;;
			}
		}
		else if (condition == Constants::PubDate)
		{
			xPathQueryPart = &quot;contains(@publicationdate, '&quot; &#43; value &#43; &quot;')&quot;;
		}
		else if (condition == Constants::Price)
		{
			if (operatorSymbol == &quot;=&quot;)
			{
				xPathQueryPart = &quot;bk:price='&quot; &#43; value &#43; &quot;'&quot;;
			}
			else if (operatorSymbol == &quot;&gt;&quot;)
			{
				xPathQueryPart = &quot;bk:price&gt;'&quot; &#43; value &#43; &quot;'&quot;;
			}
			else if (operatorSymbol == &quot;&lt;&quot;)
			{
				xPathQueryPart = &quot;bk:price&lt;'&quot; &#43; value &#43; &quot;'&quot;;
			}
			else if (operatorSymbol == &quot;&gt;=&quot;)
			{
				xPathQueryPart = &quot;bk:price&gt;='&quot; &#43; value &#43; &quot;'&quot;;
			}
			else if (operatorSymbol == &quot;&lt;=&quot;)
			{
				xPathQueryPart = &quot;bk:price&lt;='&quot; &#43; value &#43; &quot;'&quot;;
			}
			else if (operatorSymbol == &quot;&lt;&gt;&quot;)
			{
				xPathQueryPart = &quot;bk:price!='&quot; &#43; value &#43; &quot;'&quot;;
			}
		}
		else if (condition == Constants::Genre)
		{
			xPathQueryPart = &quot;@genre='&quot; &#43; value &#43; &quot;'&quot;;

		}

		xPathQueryString = xPathQueryString &#43; xPathQueryPart;
	}

	xPathQueryString = xPathQueryString &#43; xPathQueryEnding;

	XmlNodeList ^nodeList = doc-&gt;DocumentElement-&gt;SelectNodes(xPathQueryString, nsmgr);

	return nodeList;
}
#pragma endregion

#pragma region Add XML elements and attributes
//************************************************************************************
//
//  Add an element to the XML document.
//  This method creates a new book element and saves that element to the 
//  XMLDocument object. It addes attributes for the new element and introduces
//  newline characters between elements fora nice readable format.
//  
//
//************************************************************************************

XmlElement ^XMLDOMProcessing::XMLHelperMethods::AddNewBook(String ^genre, String ^ISBN, String ^misc, String ^title, String ^price, XmlDocument ^doc)
{
	// Create a new book element.
	XmlElement ^bookElement = doc-&gt;CreateElement(&quot;book&quot;, &quot;http://www.contoso.com/books&quot;);

	// Create attributes for book and append them to the book element.
	XmlAttribute ^attribute = doc-&gt;CreateAttribute(&quot;genre&quot;);
	attribute-&gt;Value = genre;
	bookElement-&gt;Attributes-&gt;Append(attribute);

	attribute = doc-&gt;CreateAttribute(&quot;ISBN&quot;);
	attribute-&gt;Value = ISBN;
	bookElement-&gt;Attributes-&gt;Append(attribute);

	attribute = doc-&gt;CreateAttribute(&quot;publicationdate&quot;);
	attribute-&gt;Value = misc;
	bookElement-&gt;Attributes-&gt;Append(attribute);

	// Create and append a child element for the title of the book.
	XmlElement ^titleElement = doc-&gt;CreateElement(&quot;title&quot;);
	titleElement-&gt;InnerText = title;
	bookElement-&gt;AppendChild(titleElement);

	// Introduce a newline character so that XML is nicely formatted.
	bookElement-&gt;InnerXml = bookElement-&gt;InnerXml-&gt;Replace(titleElement-&gt;OuterXml, &quot;\n    &quot; &#43; titleElement-&gt;OuterXml &#43; &quot; \n    &quot;);

	// Create and append a child element for the price of the book.
	XmlElement ^priceElement = doc-&gt;CreateElement(&quot;price&quot;);
	priceElement-&gt;InnerText = price;
	bookElement-&gt;AppendChild(priceElement);

	// Introduce a newline character so that XML is nicely formatted.
	bookElement-&gt;InnerXml = bookElement-&gt;InnerXml-&gt;Replace(priceElement-&gt;OuterXml, priceElement-&gt;OuterXml &#43; &quot;   \n  &quot;);

	return bookElement;

}

//************************************************************************************
//
//  Add an element to the XML document at a specific location
//  Takes a string that describes where the user wants the new node
//  to be positioned. The string comes from a series of radio buttons in a UI.
//  this method also accepts the XMLDocument in context. You have to use the 
//  this instance because it is the object that was used to generate the 
//  selectedBook XMLNode.
//
//************************************************************************************
bool XMLDOMProcessing::XMLHelperMethods::InsertBookElement(XmlElement ^bookElement, String ^position, XmlNode ^selectedBook, bool validateNode, bool generateSchema)
{
	XmlDocument ^doc = bookElement-&gt;OwnerDocument;

	String ^stringThatContainsNewline = bookElement-&gt;OuterXml;
	XmlSignificantWhitespace ^sigWhiteSpace;
	if (position == Constants::positionTop)
	{
		// Add newline characters and spaces to make XML more readable.
		sigWhiteSpace = doc-&gt;CreateSignificantWhitespace(&quot;\n  &quot;);
		doc-&gt;DocumentElement-&gt;InsertBefore(sigWhiteSpace, doc-&gt;DocumentElement-&gt;FirstChild);
		doc-&gt;DocumentElement-&gt;InsertAfter(bookElement, doc-&gt;DocumentElement-&gt;FirstChild);

	}
	else if (position == Constants::positionBottom)
	{
		// Add newline characters to make XML more readable.
		XmlWhitespace ^whitespace = doc-&gt;CreateWhitespace(&quot;  &quot;);
		XmlNode ^appendedNode = doc-&gt;DocumentElement-&gt;AppendChild(bookElement);
		doc-&gt;DocumentElement-&gt;InsertBefore(whitespace, appendedNode);
		sigWhiteSpace = doc-&gt;CreateSignificantWhitespace(&quot;\n&quot;);
		doc-&gt;DocumentElement-&gt;InsertAfter(sigWhiteSpace, appendedNode);

	}
	else if (position == Constants::positionAbove)
	{
		// Add newline characters to make XML more readable.
		XmlNode ^currNode = doc-&gt;DocumentElement-&gt;InsertBefore(bookElement, selectedBook);
		sigWhiteSpace = doc-&gt;CreateSignificantWhitespace(&quot;\n  &quot;);
		doc-&gt;DocumentElement-&gt;InsertAfter(sigWhiteSpace, currNode);

	}
	else if (position == Constants::positionBelow)
	{
		// Add newline characters to make XML more readable.
		sigWhiteSpace = doc-&gt;CreateSignificantWhitespace(&quot;\n  &quot;);
		XmlNode ^whiteSpaceNode = doc-&gt;DocumentElement-&gt;InsertAfter(sigWhiteSpace, selectedBook);
		doc-&gt;DocumentElement-&gt;InsertAfter(bookElement, whiteSpaceNode);

	}
	else
	{
		doc-&gt;DocumentElement-&gt;AppendChild(bookElement);
	}

	if (validateNode)
	{
		if (validateXML(generateSchema, doc))
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	return true;
}
#pragma endregion

#pragma region Edit XML elements and attributes
//************************************************************************************
//
//  Edit an XML element
//
//************************************************************************************

bool XMLDOMProcessing::XMLHelperMethods::editBook(String ^title, String ^ISBN, String ^publicationDate, String ^genre, String ^price, XmlNode ^book, bool validateNode, bool generateSchema)
{

	XmlElement ^bookElement = safe_cast&lt;XmlElement^&gt;(book);

	// Get the attributes of a book.        
	bookElement-&gt;SetAttribute(&quot;ISBN&quot;, ISBN);
	bookElement-&gt;SetAttribute(&quot;genre&quot;, genre);
	bookElement-&gt;SetAttribute(&quot;publicationdate&quot;, publicationDate);

	// Get the values of child elements of a book.
	bookElement[&quot;title&quot;]-&gt;InnerText = title;
	bookElement[&quot;price&quot;]-&gt;InnerText = price;

	if (validateNode)
	{
		if (validateXML(generateSchema, bookElement-&gt;OwnerDocument))
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	return true;
}

#pragma endregion

#pragma region Remove elements
//************************************************************************************
//
//  Summary: Move elements up in the XML.
//  
//
//************************************************************************************
void XMLDOMProcessing::XMLHelperMethods::deleteBook(XmlNode ^book)
{
	XmlNode ^prevNode = book-&gt;PreviousSibling;
	XmlNode ^nextNode = book-&gt;NextSibling;

	book-&gt;OwnerDocument-&gt;DocumentElement-&gt;RemoveChild(book);


	if (prevNode-&gt;NodeType == XmlNodeType::Whitespace || prevNode-&gt;NodeType == XmlNodeType::SignificantWhitespace)
	{
		prevNode-&gt;OwnerDocument-&gt;DocumentElement-&gt;RemoveChild(prevNode);
	}
}
#pragma endregion

#pragma region Position elements
//************************************************************************************
//
//  Summary: Move elements up in the XML.
//  
//
//************************************************************************************
void XMLDOMProcessing::XMLHelperMethods::MoveElementUp(XmlNode ^book)
{
	XmlNode ^previousNode = book-&gt;PreviousSibling;
	while (previousNode != nullptr &amp;&amp; (previousNode-&gt;NodeType != XmlNodeType::Element))
	{
		previousNode = previousNode-&gt;PreviousSibling;
	}
	if (previousNode != nullptr)
	{
		XmlNode ^newLineNode = book-&gt;NextSibling;
		book-&gt;OwnerDocument-&gt;DocumentElement-&gt;RemoveChild(book);
		if (newLineNode-&gt;NodeType == XmlNodeType::Whitespace | newLineNode-&gt;NodeType == XmlNodeType::SignificantWhitespace)
		{
			newLineNode-&gt;OwnerDocument-&gt;DocumentElement-&gt;RemoveChild(newLineNode);
		}
		InsertBookElement(safe_cast&lt;XmlElement^&gt;(book), Constants::positionAbove, previousNode, false, false);
	}
}

//************************************************************************************
//
//  Summary: Move elements down in the XML.
//  
//
//************************************************************************************
void XMLDOMProcessing::XMLHelperMethods::MoveElementDown(XmlNode ^book)
{
	// Walk backwards until we find an element - ignore text nodes
	XmlNode ^NextNode = book-&gt;NextSibling;
	while (NextNode != nullptr &amp;&amp; (NextNode-&gt;NodeType != XmlNodeType::Element))
	{
		NextNode = NextNode-&gt;NextSibling;
	}
	if (NextNode != nullptr)
	{
		XmlNode ^newLineNode = book-&gt;PreviousSibling;
		book-&gt;OwnerDocument-&gt;DocumentElement-&gt;RemoveChild(book);
		if (newLineNode-&gt;NodeType == XmlNodeType::Whitespace | newLineNode-&gt;NodeType == XmlNodeType::SignificantWhitespace)
		{
			newLineNode-&gt;OwnerDocument-&gt;DocumentElement-&gt;RemoveChild(newLineNode);
		}

		InsertBookElement(safe_cast&lt;XmlElement^&gt;(book), Constants::positionBelow, NextNode, false, false);
	}

}
#pragma endregion</pre>
<pre class="hidden">Public Class XMLHelperMethods

#Region &quot;Load and Save XML&quot;
    '************************************************************************************
    '
    '  Loads XML from a file. If the file is not found, load XML from a string.
    '
    '************************************************************************************
    Public Function LoadDocument(ByVal generateXML As Boolean) As XmlDocument
        Dim doc As XmlDocument = New XmlDocument
        doc.PreserveWhitespace = True
        Try
            doc.Load(&quot;booksData.xml&quot;)
        Catch ex As System.IO.FileNotFoundException
            ' If specifies that they want to generate XML when the data file is
            ' not found, then generate some XML for them to start with.
            If generateXML Then
                Dim xml As String = generateXMLString()
                doc.LoadXml(xml)
            Else
                Return Nothing
            End If

        End Try
        Return doc
    End Function

    '************************************************************************************
    '
    '  Helper method that generates an XML string.
    '
    '************************************************************************************
    Private Function generateXMLString() As String
        Dim xml As String = &quot;&lt;?xml version=&quot;&quot;1.0&quot;&quot;?&gt; &quot; &amp; ControlChars.NewLine &amp; _
                &quot;&lt;books xmlns=&quot;&quot;http://www.contoso.com/books&quot;&quot;&gt; &quot; &amp; ControlChars.NewLine &amp; _
                &quot;  &lt;book genre=&quot;&quot;novel&quot;&quot; ISBN=&quot;&quot;1-861001-57-8&quot;&quot; publicationdate=&quot;&quot;1823-01-28&quot;&quot;&gt; &quot; &amp; ControlChars.NewLine &amp; _
                &quot;    &lt;title&gt;Pride And Prejudice&lt;/title&gt; &quot; &amp; ControlChars.NewLine &amp; _
                &quot;    &lt;price&gt;24.95&lt;/price&gt; &quot; &amp; ControlChars.NewLine &amp; _
                &quot;  &lt;/book&gt; &quot; &amp; ControlChars.NewLine &amp; _
                &quot;  &lt;book genre=&quot;&quot;novel&quot;&quot; ISBN=&quot;&quot;1-861002-30-1&quot;&quot; publicationdate=&quot;&quot;1985-01-01&quot;&quot;&gt; &quot; &amp; ControlChars.NewLine &amp; _
                &quot;    &lt;title&gt;The Handmaid's Tale&lt;/title&gt; &quot; &amp; ControlChars.NewLine &amp; _
                &quot;    &lt;price&gt;29.95&lt;/price&gt; &quot; &amp; ControlChars.NewLine &amp; _
                &quot;  &lt;/book&gt; &quot; &amp; ControlChars.NewLine &amp; _
                &quot;  &lt;book genre=&quot;&quot;novel&quot;&quot; ISBN=&quot;&quot;1-861001-45-3&quot;&quot; publicationdate=&quot;&quot;1811-01-01&quot;&quot;&gt; &quot; &amp; ControlChars.NewLine &amp;
                &quot;    &lt;title&gt;Sense and Sensibility&lt;/title&gt; &quot; &amp; ControlChars.NewLine &amp; _
                &quot;    &lt;price&gt;19.95&lt;/price&gt; &quot; &amp; ControlChars.NewLine &amp; _
                &quot;  &lt;/book&gt; &quot; &amp; ControlChars.NewLine &amp; _
                &quot;&lt;/books&gt;&quot;
        Return xml
    End Function

    '************************************************************************************
    '
    '  Summery: Loads XML from a file. If the file is not found, load XML from a string.
    '
    '************************************************************************************
    Public Sub SaveXML(ByVal doc As XmlDocument)
        doc.Save(Constants.booksFileName)
    End Sub

#End Region

#Region &quot;Validate XML against a Schema&quot;
    '************************************************************************************
    '
    '  Associate the schema with XML. Then, load the XML and validate it against
    '  the schema.
    '
    '************************************************************************************
    Public Function LoadDocumentWithSchemaValidation(ByVal generateXML As Boolean, ByVal generateSchema As Boolean) As XmlDocument
        Dim reader As XmlReader
        Dim settings As XmlReaderSettings = New XmlReaderSettings
        ' Helper method to retrieve schema.
        Dim schema As XmlSchema = getSchema(generateSchema)
        If (schema Is Nothing) Then
            Return Nothing
        End If
        settings.Schemas.Add(schema)
        AddHandler settings.ValidationEventHandler, AddressOf settings_ValidationEventHandler
        settings.ValidationFlags = (settings.ValidationFlags Or XmlSchemaValidationFlags.ReportValidationWarnings)
        settings.ValidationType = ValidationType.Schema
        Try
            reader = XmlReader.Create(&quot;booksData.xml&quot;, settings)
        Catch ex As System.IO.FileNotFoundException
            If generateXML Then
                Dim xml As String = generateXMLString()
                Dim byteArray() As Byte = Encoding.UTF8.GetBytes(xml)
                Dim stream As MemoryStream = New MemoryStream(byteArray)
                reader = XmlReader.Create(stream, settings)
            Else
                Return Nothing
            End If
        End Try
        Dim doc As XmlDocument = New XmlDocument
        doc.PreserveWhitespace = True
        doc.Load(reader)
        reader.Close()
        Return doc
    End Function

    '************************************************************************************
    '
    '  Helper method that generates an XML Schema.
    '
    '************************************************************************************
    Private Function generateXMLSchema() As String

        Dim generatedXmlSchema As String = &quot;&lt;?xml version=&quot;&quot;1.0&quot;&quot; encoding=&quot;&quot;utf-8&quot;&quot;?&gt; &quot; &amp; _
                &quot;&lt;xs:schema attributeFormDefault=&quot;&quot;unqualified&quot;&quot; &quot; &amp; _
                &quot;elementFormDefault=&quot;&quot;qualified&quot;&quot; targetNamespace=&quot;&quot;http://www.contoso.com/books&quot;&quot; &quot; &amp; _
                &quot;xmlns:xs=&quot;&quot;http://www.w3.org/2001/XMLSchema&quot;&quot;&gt; &quot; &amp; _
                &quot;&lt;xs:element name=&quot;&quot;books&quot;&quot;&gt; &quot; &amp; _
                &quot;&lt;xs:complexType&gt; &quot; &amp; _
                &quot;&lt;xs:sequence&gt; &quot; &amp; _
                &quot;&lt;xs:element maxOccurs=&quot;&quot;unbounded&quot;&quot; name=&quot;&quot;book&quot;&quot;&gt; &quot; &amp; _
                &quot;&lt;xs:complexType&gt; &quot; &amp; _
                &quot;&lt;xs:sequence&gt; &quot; &amp; _
                &quot;&lt;xs:element name=&quot;&quot;title&quot;&quot; type=&quot;&quot;xs:string&quot;&quot; /&gt; &quot; &amp; _
                &quot;&lt;xs:element name=&quot;&quot;price&quot;&quot; type=&quot;&quot;xs:decimal&quot;&quot; /&gt; &quot; &amp; _
                &quot;&lt;/xs:sequence&gt; &quot; &amp; _
                &quot;&lt;xs:attribute name=&quot;&quot;genre&quot;&quot; type=&quot;&quot;xs:string&quot;&quot; use=&quot;&quot;required&quot;&quot; /&gt; &quot; &amp; _
                &quot;&lt;xs:attribute name=&quot;&quot;publicationdate&quot;&quot; type=&quot;&quot;xs:date&quot;&quot; use=&quot;&quot;required&quot;&quot; /&gt; &quot; &amp; _
                &quot;&lt;xs:attribute name=&quot;&quot;ISBN&quot;&quot; type=&quot;&quot;xs:string&quot;&quot; use=&quot;&quot;required&quot;&quot; /&gt; &quot; &amp; _
                &quot;&lt;/xs:complexType&gt; &quot; &amp; _
                &quot;&lt;/xs:element&gt; &quot; &amp; _
                &quot;&lt;/xs:sequence&gt; &quot; &amp; _
                &quot;&lt;/xs:complexType&gt; &quot; &amp; _
                &quot;&lt;/xs:element&gt; &quot; &amp; _
                &quot;&lt;/xs:schema&gt; &quot;


        Return generatedXmlSchema

    End Function

    '************************************************************************************
    '
    '  Helper method that gets a schema
    '
    '************************************************************************************
    Private Function getSchema(ByVal generateSchema As Boolean) As XmlSchema
        Dim xs As XmlSchemaSet = New XmlSchemaSet
        Dim schema As XmlSchema
        Try
            schema = xs.Add(&quot;http://www.contoso.com/books&quot;, &quot;booksData.xsd&quot;)
        Catch ex As System.IO.FileNotFoundException
            If generateSchema Then
                Dim xmlSchemaString As String = generateXMLSchema()
                Dim byteArray() As Byte = Encoding.UTF8.GetBytes(xmlSchemaString)
                Dim stream As MemoryStream = New MemoryStream(byteArray)
                Dim reader As XmlReader = XmlReader.Create(stream)
                schema = xs.Add(&quot;http://www.contoso.com/books&quot;, reader)
            Else
                Return Nothing
            End If
        End Try
        Return schema
    End Function

    '************************************************************************************
    '
    '  Helper method to validate the XML against the schema.
    '
    '************************************************************************************
    Private Sub validateXML(ByVal generateSchema As Boolean, ByVal doc As XmlDocument)
        If (doc.Schemas.Count = 0) Then
            ' Helper method to retrieve schema.
            Dim schema As XmlSchema = getSchema(generateSchema)
            doc.Schemas.Add(schema)
        End If
        ' Use an event handler to validate the XML node against the schema.
        doc.Validate(AddressOf settings_ValidationEventHandler)
    End Sub

    '************************************************************************************
    '
    '  Event handler that is raised when XML doesn't validate against the schema.
    '
    '************************************************************************************
    Private Sub settings_ValidationEventHandler(ByVal sender As Object, ByVal e As System.Xml.Schema.ValidationEventArgs)
        If (e.Severity = XmlSeverityType.Warning) Then
            System.Windows.Forms.MessageBox.Show((&quot;The following validation warning occurred: &quot; &amp; e.Message))
        ElseIf (e.Severity = XmlSeverityType.Error) Then
            System.Windows.Forms.MessageBox.Show((&quot;The following critical validation errors occurred: &quot; &amp; e.Message))
            Dim objectType As Type = sender.GetType
        End If
    End Sub

#End Region

#Region &quot;Find Elements and attributes&quot;

    '************************************************************************************
    '
    '  Search the XML tree for a specific XMLNode element by using an attribute value.
    '  Description: Must identify the namespace of the nodes and define a prefix. Also include the 
    '  prefix in the XPath string.
    '
    '************************************************************************************
    Public Function GetBook(ByVal uniqueAttribute As String, ByVal doc As XmlDocument) As XmlNode
        Dim nsmgr As XmlNamespaceManager = New XmlNamespaceManager(doc.NameTable)
        nsmgr.AddNamespace(&quot;bk&quot;, &quot;http://www.contoso.com/books&quot;)
        Dim xPathString As String = (&quot;//bk:books/bk:book[@ISBN='&quot; _
                    &amp; (uniqueAttribute &amp; &quot;']&quot;))
        Dim xmlNode As XmlNode = doc.DocumentElement.SelectSingleNode(xPathString, nsmgr)
        Return xmlNode
    End Function

    '************************************************************************************
    '
    '  Get information about a specific book. Pass in an XMLNode that 
    '  represents the book and populate strings passed in by reference.
    '
    '************************************************************************************
    Public Sub GetBookInformation(ByRef title As String, ByRef ISBN As String, ByRef publicationDate As String, ByRef price As String, ByRef genre As String, ByVal book As XmlNode)
        Dim bookElement As XmlElement = CType(book, XmlElement)
        ' Get the attributes of a book.        
        Dim attr As XmlAttribute = bookElement.GetAttributeNode(&quot;ISBN&quot;)
        ISBN = attr.InnerXml
        attr = bookElement.GetAttributeNode(&quot;genre&quot;)
        genre = attr.InnerXml
        attr = bookElement.GetAttributeNode(&quot;publicationdate&quot;)
        publicationDate = attr.InnerXml
        ' Get the values of child elements of a book.
        title = bookElement(&quot;title&quot;).InnerText
        price = bookElement(&quot;price&quot;).InnerText
    End Sub

    '************************************************************************************
    '
    '  Uses filter criteria collection in the UI to retreive specific elements and attributes.
    '
    '************************************************************************************
    Public Function ApplyFilters(ByVal conditions As ArrayList, ByVal operatorSymbols As ArrayList, ByVal values As ArrayList, ByVal doc As XmlDocument, ByVal matchString As String) As XmlNodeList
        Dim nsmgr As XmlNamespaceManager = New XmlNamespaceManager(doc.NameTable)

        nsmgr.AddNamespace(&quot;bk&quot;, &quot;http://www.contoso.com/books&quot;)
        Dim xPathQueryString As String = &quot;//bk:books/bk:book[&quot;
        Dim xPathQueryEnding As String = &quot;]&quot;
        Dim xPathQueryStrings As ArrayList = New ArrayList
        Dim booleanOperator As String
        If (matchString = &quot;Any&quot;) Then
            booleanOperator = &quot;or &quot;
        Else
            booleanOperator = &quot;and &quot;
        End If
        Dim counter As Integer = 0
        Dim operatorArray() As String = CType(operatorSymbols.ToArray(GetType(System.String)), String())
        Dim valueArray() As String = CType(values.ToArray(GetType(System.String)), String())
        For Each condition As String In conditions
            Dim xPathQueryPart As String = &quot;&quot;
            Dim operatorSymbol As String = operatorArray(counter)
            Dim value As String = valueArray(counter)
            If (counter &gt; 0) Then
                xPathQueryString = (xPathQueryString &amp; booleanOperator)
            End If
            counter = (counter &#43; 1)
            Select Case (condition)
                Case Constants.Title
                    Select Case (operatorSymbol)
                        Case &quot;Contains&quot;
                            xPathQueryPart = (&quot;contains(bk:title,'&quot; _
                                        &amp; (value &amp; &quot;')&quot;))
                        Case &quot;Excludes&quot;
                            xPathQueryPart = (&quot;not(contains(bk:title,'&quot; _
                                        &amp; (value &amp; &quot;'))&quot;))
                        Case &quot;=&quot;
                            xPathQueryPart = (&quot;bk:title='&quot; _
                                        &amp; (value &amp; &quot;'&quot;))
                    End Select
                Case Constants.ISBN
                    Select Case (operatorSymbol)
                        Case &quot;Contains&quot;
                            xPathQueryPart = (&quot;contains(@ISBN, '&quot; _
                                        &amp; (value &amp; &quot;')&quot;))
                        Case &quot;Excludes&quot;
                            xPathQueryPart = (&quot;not(contains(@ISBN, '&quot; _
                                        &amp; (value &amp; &quot;'))&quot;))
                        Case &quot;=&quot;
                            xPathQueryPart = (&quot;@ISBN='&quot; _
                                        &amp; (value &amp; &quot;'&quot;))
                    End Select
                Case Constants.PubDate
                    xPathQueryPart = (&quot;contains(@publicationdate, '&quot; _
                                &amp; (value &amp; &quot;')&quot;))
                Case Constants.Price
                    Select Case (operatorSymbol)
                        Case &quot;=&quot;
                            xPathQueryPart = (&quot;bk:price='&quot; _
                                        &amp; (value &amp; &quot;'&quot;))
                        Case &quot;&gt;&quot;
                            xPathQueryPart = (&quot;bk:price&gt;'&quot; _
                                        &amp; (value &amp; &quot;'&quot;))
                        Case &quot;&lt;&quot;
                            xPathQueryPart = (&quot;bk:price&lt;'&quot; _
                                        &amp; (value &amp; &quot;'&quot;))
                        Case &quot;&gt;=&quot;
                            xPathQueryPart = (&quot;bk:price&gt;='&quot; _
                                        &amp; (value &amp; &quot;'&quot;))
                        Case &quot;&lt;=&quot;
                            xPathQueryPart = (&quot;bk:price&lt;='&quot; _
                                        &amp; (value &amp; &quot;'&quot;))
                        Case &quot;&lt;&gt;&quot;
                            xPathQueryPart = (&quot;bk:price!='&quot; _
                                        &amp; (value &amp; &quot;'&quot;))
                    End Select
                Case Constants.Genre
                    xPathQueryPart = (&quot;@genre='&quot; _
                                &amp; (value &amp; &quot;'&quot;))
            End Select
            xPathQueryString = (xPathQueryString &amp; xPathQueryPart)
        Next
        xPathQueryString = (xPathQueryString &amp; xPathQueryEnding)
        Dim nodeList As XmlNodeList = doc.DocumentElement.SelectNodes(xPathQueryString, nsmgr)
        Return nodeList
    End Function

#End Region

#Region &quot;Add XML elements and attributes&quot;
    '************************************************************************************
    '
    '  Add an element to the XML document.
    '  This method creates a new book element and saves that element to the 
    '  XMLDocument object. It addes attributes for the new element and introduces
    '  newline characters between elements fora nice readable format.
    '  
    '
    '************************************************************************************
    Public Function AddNewBook(ByVal genre As String, ByVal ISBN As String, ByVal misc As String, ByVal title As String, ByVal price As String, ByVal doc As XmlDocument) As XmlElement
        ' Create a new book element.
        Dim bookElement As XmlElement = doc.CreateElement(&quot;book&quot;, &quot;http://www.contoso.com/books&quot;)

        ' Create attributes for book and append them to the book element.
        Dim attribute As XmlAttribute = doc.CreateAttribute(&quot;genre&quot;)
        attribute.Value = genre
        bookElement.Attributes.Append(attribute)

        attribute = doc.CreateAttribute(&quot;ISBN&quot;)
        attribute.Value = ISBN
        bookElement.Attributes.Append(attribute)

        attribute = doc.CreateAttribute(&quot;publicationdate&quot;)
        attribute.Value = misc
        bookElement.Attributes.Append(attribute)

        ' Create and append a child element for the title of the book.
        Dim titleElement As XmlElement = doc.CreateElement(&quot;title&quot;)
        titleElement.InnerText = title
        bookElement.AppendChild(titleElement)

        ' Introduce a newline character so that XML is nicely formatted.
        bookElement.InnerXml = bookElement.InnerXml.Replace(titleElement.OuterXml, _
                               &quot;\n   &quot; &amp; titleElement.OuterXml &amp; &quot; &quot; &amp; ControlChars.NewLine &#43; &quot;    &quot;)
        ' Create and append a child element for the price of the book.
        Dim priceElement As XmlElement = doc.CreateElement(&quot;price&quot;)
        priceElement.InnerText = price
        bookElement.AppendChild(priceElement)

        ' Introduce a newline character so that XML is nicely formatted.
        bookElement.InnerXml = bookElement.InnerXml.Replace(priceElement.OuterXml,
                                                            (priceElement.OuterXml &amp; &quot;   &quot; &amp; ControlChars.NewLine &amp; &quot;  &quot;))
        Return bookElement
    End Function

    '************************************************************************************
    '
    '  Add an element to the XML document at a specific location
    '  Takes a string that describes where the user wants the new node
    '  to be positioned. The string comes from a series of radio buttons in a UI.
    '  this method also accepts the XMLDocument in context. You have to use the 
    '  this instance because it is the object that was used to generate the 
    '  selectedBook XMLNode.
    '
    '************************************************************************************
    Public Sub InsertBookElement(ByVal bookElement As XmlElement, ByVal position As String,
                                 ByVal selectedBook As XmlNode, ByVal validateNode As Boolean,
                                 ByVal generateSchema As Boolean)
        Dim doc As XmlDocument = bookElement.OwnerDocument
        Dim stringThatContainsNewline As String = bookElement.OuterXml

        Dim sigWhiteSpace As XmlSignificantWhitespace

        Select Case (position)

            Case Constants.positionTop
                ' Add newline characters and spaces to make XML more readable.
                sigWhiteSpace = doc.CreateSignificantWhitespace(ControlChars.NewLine &amp; &quot;  &quot;)
                doc.DocumentElement.InsertBefore(sigWhiteSpace, doc.DocumentElement.FirstChild)
                doc.DocumentElement.InsertAfter(bookElement, doc.DocumentElement.FirstChild)

            Case Constants.positionBottom
                ' Add newline characters to make XML more readable.
                Dim whitespace As XmlWhitespace = doc.CreateWhitespace(&quot;  &quot;)
                Dim appendedNode As XmlNode = doc.DocumentElement.AppendChild(bookElement)
                doc.DocumentElement.InsertBefore(whitespace, appendedNode)
                sigWhiteSpace = doc.CreateSignificantWhitespace(ControlChars.NewLine)
                doc.DocumentElement.InsertAfter(sigWhiteSpace, appendedNode)

            Case Constants.positionAbove
                ' Add newline characters to make XML more readable.
                Dim currNode As XmlNode = doc.DocumentElement.InsertBefore(bookElement, selectedBook)
                sigWhiteSpace = doc.CreateSignificantWhitespace(ControlChars.NewLine &amp; &quot;  &quot;)
                doc.DocumentElement.InsertAfter(sigWhiteSpace, currNode)

            Case Constants.positionBelow
                ' Add newline characters to make XML more readable.
                sigWhiteSpace = doc.CreateSignificantWhitespace(ControlChars.NewLine &amp; &quot;  &quot;)
                Dim whiteSpaceNode As XmlNode = doc.DocumentElement.InsertAfter(sigWhiteSpace, selectedBook)
                doc.DocumentElement.InsertAfter(bookElement, whiteSpaceNode)

            Case Else
                doc.DocumentElement.AppendChild(bookElement)

        End Select

        If validateNode Then
            validateXML(generateSchema, doc)
        End If
    End Sub
#End Region

#Region &quot;Edit XML elements and attributes&quot;
    '************************************************************************************
    '
    '  Edit an XML element
    '  
    '
    '************************************************************************************
    Public Sub editBook(ByVal title As String, ByVal ISBN As String,
                        ByVal publicationDate As String, ByVal genre As String,
                        ByVal price As String, ByVal book As XmlNode, ByVal validateNode As Boolean,
                        ByVal generateSchema As Boolean)

        Dim bookElement As XmlElement = CType(book, XmlElement)

        ' Get the attributes of a book.        
        bookElement.SetAttribute(&quot;ISBN&quot;, ISBN)
        bookElement.SetAttribute(&quot;genre&quot;, genre)
        bookElement.SetAttribute(&quot;publicationdate&quot;, publicationDate)

        ' Get the values of child elements of a book.
        bookElement(&quot;title&quot;).InnerText = title
        bookElement(&quot;price&quot;).InnerText = price
        If validateNode Then
            validateXML(generateSchema, bookElement.OwnerDocument)
        End If

    End Sub

#End Region

#Region &quot;Remove elements&quot;
    '************************************************************************************
    '
    '  Summary: Delete a book node from the XMLDocument.
    '  
    '
    '************************************************************************************
    Public Sub deleteBook(ByVal book As XmlNode)

        Dim prevNode As XmlNode = book.PreviousSibling
        Dim nextNode As XmlNode = book.NextSibling
        book.OwnerDocument.DocumentElement.RemoveChild(book)

        If ((prevNode.NodeType = XmlNodeType.Whitespace) _
                    OrElse (prevNode.NodeType = XmlNodeType.SignificantWhitespace)) Then
            prevNode.OwnerDocument.DocumentElement.RemoveChild(prevNode)

        End If
    End Sub

#End Region

#Region &quot;Position elements&quot;

    '************************************************************************************
    '
    '  Summary: Move elements up in the XML.
    '  
    '
    '************************************************************************************
    Public Sub MoveElementUp(ByVal book As XmlNode)
        Dim previousNode As XmlNode = book.PreviousSibling

        While ((Not (previousNode) Is Nothing) _
                    AndAlso (previousNode.NodeType &lt;&gt; XmlNodeType.Element))
            previousNode = previousNode.PreviousSibling

        End While
        If (Not (previousNode) Is Nothing) Then
            Dim newLineNode As XmlNode = book.NextSibling
            book.OwnerDocument.DocumentElement.RemoveChild(book)

            If ((newLineNode.NodeType = XmlNodeType.Whitespace) _
                        Or (newLineNode.NodeType = XmlNodeType.SignificantWhitespace)) Then
                newLineNode.OwnerDocument.DocumentElement.RemoveChild(newLineNode)
            End If

            InsertBookElement(CType(book, XmlElement), Constants.positionAbove,
                              previousNode, False, False)
        End If
    End Sub

    '************************************************************************************
    '
    '  Summary: Move elements down in the XML.
    '  
    '
    '************************************************************************************
    Public Sub MoveElementDown(ByVal book As XmlNode)
        ' Walk backwards until we find an element - ignore text nodes
        Dim NextNode As XmlNode = book.NextSibling

        While ((Not (NextNode) Is Nothing) _
                    AndAlso (NextNode.NodeType &lt;&gt; XmlNodeType.Element))
            NextNode = NextNode.NextSibling

        End While

        If (Not (NextNode) Is Nothing) Then
            Dim newLineNode As XmlNode = book.PreviousSibling
            book.OwnerDocument.DocumentElement.RemoveChild(book)

            If ((newLineNode.NodeType = XmlNodeType.Whitespace) _
                        Or (newLineNode.NodeType = XmlNodeType.SignificantWhitespace)) Then
                newLineNode.OwnerDocument.DocumentElement.RemoveChild(newLineNode)
            End If

            InsertBookElement(CType(book, XmlElement), Constants.positionBelow,
                              NextNode, False, False)
        End If
    End Sub

#End Region

End Class</pre>
<div class="preview">
<pre class="csharp">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">class</span>&nbsp;XMLHelperMethods&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;{<span class="cs__preproc">&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;#region&nbsp;Load&nbsp;and&nbsp;save&nbsp;XML</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;Loads&nbsp;XML&nbsp;from&nbsp;a&nbsp;file.&nbsp;If&nbsp;the&nbsp;file&nbsp;is&nbsp;not&nbsp;found,&nbsp;load&nbsp;XML&nbsp;from&nbsp;a&nbsp;string.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;XmlDocument&nbsp;LoadDocument(<span class="cs__keyword">bool</span>&nbsp;generateXML)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlDocument&nbsp;doc&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;XmlDocument();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc.PreserveWhitespace&nbsp;=&nbsp;<span class="cs__keyword">true</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">try</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc.Load(<span class="cs__string">&quot;booksData.xml&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">catch</span>&nbsp;(System.IO.FileNotFoundException)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;If&nbsp;specifies&nbsp;that&nbsp;they&nbsp;want&nbsp;to&nbsp;generate&nbsp;XML&nbsp;when&nbsp;the&nbsp;data&nbsp;file&nbsp;is</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;not&nbsp;found,&nbsp;then&nbsp;generate&nbsp;some&nbsp;XML&nbsp;for&nbsp;them&nbsp;to&nbsp;start&nbsp;with.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(generateXML)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;xml&nbsp;=&nbsp;generateXMLString();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc.LoadXml(xml);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">null</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;doc;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;Helper&nbsp;method&nbsp;that&nbsp;generates&nbsp;an&nbsp;XML&nbsp;string.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;generateXMLString()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;xml&nbsp;=&nbsp;<span class="cs__string">&quot;&lt;?xml&nbsp;version=\&quot;1.0\&quot;?&gt;&nbsp;\n&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&lt;books&nbsp;xmlns=\&quot;http://www.contoso.com/books\&quot;&gt;&nbsp;\n&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&nbsp;&nbsp;&lt;book&nbsp;genre=\&quot;novel\&quot;&nbsp;ISBN=\&quot;1-861001-57-8\&quot;&nbsp;publicationdate=\&quot;1823-01-28\&quot;&gt;&nbsp;\n&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&nbsp;&nbsp;&nbsp;&nbsp;&lt;title&gt;Pride&nbsp;And&nbsp;Prejudice&lt;/title&gt;&nbsp;\n&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&nbsp;&nbsp;&nbsp;&nbsp;&lt;price&gt;24.95&lt;/price&gt;&nbsp;\n&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&nbsp;&nbsp;&lt;/book&gt;&nbsp;\n&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&nbsp;&nbsp;&lt;book&nbsp;genre=\&quot;novel\&quot;&nbsp;ISBN=\&quot;1-861002-30-1\&quot;&nbsp;publicationdate=\&quot;1985-01-01\&quot;&gt;&nbsp;\n&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&nbsp;&nbsp;&nbsp;&nbsp;&lt;title&gt;The&nbsp;Handmaid's&nbsp;Tale&lt;/title&gt;&nbsp;\n&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&nbsp;&nbsp;&nbsp;&nbsp;&lt;price&gt;29.95&lt;/price&gt;&nbsp;\n&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&nbsp;&nbsp;&lt;/book&gt;&nbsp;\n&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&nbsp;&nbsp;&lt;book&nbsp;genre=\&quot;novel\&quot;&nbsp;ISBN=\&quot;1-861001-45-3\&quot;&nbsp;publicationdate=\&quot;1811-01-01\&quot;&gt;&nbsp;\n&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&nbsp;&nbsp;&nbsp;&nbsp;&lt;title&gt;Sense&nbsp;and&nbsp;Sensibility&lt;/title&gt;&nbsp;\n&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&nbsp;&nbsp;&nbsp;&nbsp;&lt;price&gt;19.95&lt;/price&gt;&nbsp;\n&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&nbsp;&nbsp;&lt;/book&gt;&nbsp;\n&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&lt;/books&gt;&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;xml;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;Loads&nbsp;XML&nbsp;from&nbsp;a&nbsp;file.&nbsp;If&nbsp;the&nbsp;file&nbsp;is&nbsp;not&nbsp;found,&nbsp;load&nbsp;XML&nbsp;from&nbsp;a&nbsp;string.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;SaveXML(XmlDocument&nbsp;doc)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc.Save(Constants.booksFileName);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}<span class="cs__preproc">&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;#endregion</span><span class="cs__preproc">&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;#region&nbsp;Validate&nbsp;XML&nbsp;against&nbsp;a&nbsp;Schema</span>&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;Associate&nbsp;the&nbsp;schema&nbsp;with&nbsp;XML.&nbsp;Then,&nbsp;load&nbsp;the&nbsp;XML&nbsp;and&nbsp;validate&nbsp;it&nbsp;against</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;the&nbsp;schema.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;XmlDocument&nbsp;LoadDocumentWithSchemaValidation(<span class="cs__keyword">bool</span>&nbsp;generateXML,&nbsp;<span class="cs__keyword">bool</span>&nbsp;generateSchema)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlReader&nbsp;reader;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlReaderSettings&nbsp;settings&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;XmlReaderSettings();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Helper&nbsp;method&nbsp;to&nbsp;retrieve&nbsp;schema.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlSchema&nbsp;schema&nbsp;=&nbsp;getSchema(generateSchema);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(schema&nbsp;==&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">null</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;settings.Schemas.Add(schema);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;settings.ValidationEventHandler&nbsp;&#43;=&nbsp;settings_ValidationEventHandler;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;settings.ValidationFlags&nbsp;=&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;settings.ValidationFlags&nbsp;|&nbsp;XmlSchemaValidationFlags.ReportValidationWarnings;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;settings.ValidationType&nbsp;=&nbsp;ValidationType.Schema;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">try</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;reader&nbsp;=&nbsp;XmlReader.Create(<span class="cs__string">&quot;booksData.xml&quot;</span>,&nbsp;settings);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">catch</span>&nbsp;(System.IO.FileNotFoundException)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(generateXML)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;xml&nbsp;=&nbsp;generateXMLString();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">byte</span>[]&nbsp;byteArray&nbsp;=&nbsp;Encoding.UTF8.GetBytes(xml);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MemoryStream&nbsp;stream&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;MemoryStream(byteArray);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;reader&nbsp;=&nbsp;XmlReader.Create(stream,&nbsp;settings);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">null</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlDocument&nbsp;doc&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;XmlDocument();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc.PreserveWhitespace&nbsp;=&nbsp;<span class="cs__keyword">true</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc.Load(reader);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;reader.Close();&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;doc;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;Helper&nbsp;method&nbsp;that&nbsp;generates&nbsp;an&nbsp;XML&nbsp;Schema.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;generateXMLSchema()&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;xmlSchema&nbsp;=&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&lt;?xml&nbsp;version=\&quot;1.0\&quot;&nbsp;encoding=\&quot;utf-8\&quot;?&gt;&nbsp;&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&lt;xs:schema&nbsp;attributeFormDefault=\&quot;unqualified\&quot;&nbsp;&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;elementFormDefault=\&quot;qualified\&quot;&nbsp;targetNamespace=\&quot;http://www.contoso.com/books\&quot;&nbsp;&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;xmlns:xs=\&quot;http://www.w3.org/2001/XMLSchema\&quot;&gt;&nbsp;&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&lt;xs:element&nbsp;name=\&quot;books\&quot;&gt;&nbsp;&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&lt;xs:complexType&gt;&nbsp;&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&lt;xs:sequence&gt;&nbsp;&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&lt;xs:element&nbsp;maxOccurs=\&quot;unbounded\&quot;&nbsp;name=\&quot;book\&quot;&gt;&nbsp;&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&lt;xs:complexType&gt;&nbsp;&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&lt;xs:sequence&gt;&nbsp;&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&lt;xs:element&nbsp;name=\&quot;title\&quot;&nbsp;type=\&quot;xs:string\&quot;&nbsp;/&gt;&nbsp;&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&lt;xs:element&nbsp;name=\&quot;price\&quot;&nbsp;type=\&quot;xs:decimal\&quot;&nbsp;/&gt;&nbsp;&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&lt;/xs:sequence&gt;&nbsp;&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&lt;xs:attribute&nbsp;name=\&quot;genre\&quot;&nbsp;type=\&quot;xs:string\&quot;&nbsp;use=\&quot;required\&quot;&nbsp;/&gt;&nbsp;&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&lt;xs:attribute&nbsp;name=\&quot;publicationdate\&quot;&nbsp;type=\&quot;xs:date\&quot;&nbsp;use=\&quot;required\&quot;&nbsp;/&gt;&nbsp;&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&lt;xs:attribute&nbsp;name=\&quot;ISBN\&quot;&nbsp;type=\&quot;xs:string\&quot;&nbsp;use=\&quot;required\&quot;&nbsp;/&gt;&nbsp;&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&lt;/xs:complexType&gt;&nbsp;&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&lt;/xs:element&gt;&nbsp;&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&lt;/xs:sequence&gt;&nbsp;&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&lt;/xs:complexType&gt;&nbsp;&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&lt;/xs:element&gt;&nbsp;&quot;</span>&nbsp;&#43;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;&lt;/xs:schema&gt;&nbsp;&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;xmlSchema;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;Helper&nbsp;method&nbsp;that&nbsp;gets&nbsp;a&nbsp;schema</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;XmlSchema&nbsp;getSchema(<span class="cs__keyword">bool</span>&nbsp;generateSchema)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlSchemaSet&nbsp;xs&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;XmlSchemaSet();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlSchema&nbsp;schema;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">try</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;schema&nbsp;=&nbsp;xs.Add(<span class="cs__string">&quot;http://www.contoso.com/books&quot;</span>,&nbsp;<span class="cs__string">&quot;booksData.xsd&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">catch</span>&nbsp;(System.IO.FileNotFoundException)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(generateSchema)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;xmlSchemaString&nbsp;=&nbsp;generateXMLSchema();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">byte</span>[]&nbsp;byteArray&nbsp;=&nbsp;Encoding.UTF8.GetBytes(xmlSchemaString);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;MemoryStream&nbsp;stream&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;MemoryStream(byteArray);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlReader&nbsp;reader&nbsp;=&nbsp;XmlReader.Create(stream);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;schema&nbsp;=&nbsp;xs.Add(<span class="cs__string">&quot;http://www.contoso.com/books&quot;</span>,&nbsp;reader);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;<span class="cs__keyword">null</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;schema;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;Helper&nbsp;method&nbsp;to&nbsp;validate&nbsp;the&nbsp;XML&nbsp;against&nbsp;the&nbsp;schema.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">private</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;validateXML(<span class="cs__keyword">bool</span>&nbsp;generateSchema,&nbsp;XmlDocument&nbsp;doc)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(doc.Schemas.Count&nbsp;==&nbsp;<span class="cs__number">0</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Helper&nbsp;method&nbsp;to&nbsp;retrieve&nbsp;schema.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlSchema&nbsp;schema&nbsp;=&nbsp;getSchema(generateSchema);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc.Schemas.Add(schema);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Use&nbsp;an&nbsp;event&nbsp;handler&nbsp;to&nbsp;validate&nbsp;the&nbsp;XML&nbsp;node&nbsp;against&nbsp;the&nbsp;schema.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc.Validate(settings_ValidationEventHandler);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;Event&nbsp;handler&nbsp;that&nbsp;is&nbsp;raised&nbsp;when&nbsp;XML&nbsp;doesn't&nbsp;validate&nbsp;against&nbsp;the&nbsp;schema.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">void</span>&nbsp;settings_ValidationEventHandler(<span class="cs__keyword">object</span>&nbsp;sender,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;System.Xml.Schema.ValidationEventArgs&nbsp;e)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(e.Severity&nbsp;==&nbsp;XmlSeverityType.Warning)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;System.Windows.Forms.MessageBox.Show&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(<span class="cs__string">&quot;The&nbsp;following&nbsp;validation&nbsp;warning&nbsp;occurred:&nbsp;&quot;</span>&nbsp;&#43;&nbsp;e.Message);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;<span class="cs__keyword">if</span>&nbsp;(e.Severity&nbsp;==&nbsp;XmlSeverityType.Error)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;System.Windows.Forms.MessageBox.Show&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(<span class="cs__string">&quot;The&nbsp;following&nbsp;critical&nbsp;validation&nbsp;errors&nbsp;occurred:&nbsp;&quot;</span>&nbsp;&#43;&nbsp;e.Message);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Type&nbsp;objectType&nbsp;=&nbsp;sender.GetType();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}<span class="cs__preproc">&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;#endregion</span><span class="cs__preproc">&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;#region&nbsp;Find&nbsp;XML&nbsp;elements&nbsp;and&nbsp;attributes</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;Search&nbsp;the&nbsp;XML&nbsp;tree&nbsp;for&nbsp;a&nbsp;specific&nbsp;XMLNode&nbsp;element&nbsp;by&nbsp;using&nbsp;an&nbsp;attribute&nbsp;value.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;Description:&nbsp;Must&nbsp;identify&nbsp;the&nbsp;namespace&nbsp;of&nbsp;the&nbsp;nodes&nbsp;and&nbsp;define&nbsp;a&nbsp;prefix.&nbsp;Also&nbsp;include&nbsp;the&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;prefix&nbsp;in&nbsp;the&nbsp;XPath&nbsp;string.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;XmlNode&nbsp;GetBook(<span class="cs__keyword">string</span>&nbsp;uniqueAttribute,&nbsp;XmlDocument&nbsp;doc)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlNamespaceManager&nbsp;nsmgr&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;XmlNamespaceManager(doc.NameTable);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;nsmgr.AddNamespace(<span class="cs__string">&quot;bk&quot;</span>,&nbsp;<span class="cs__string">&quot;http://www.contoso.com/books&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;xPathString&nbsp;=&nbsp;<span class="cs__string">&quot;//bk:books/bk:book[@ISBN='&quot;</span>&nbsp;&#43;&nbsp;uniqueAttribute&nbsp;&#43;&nbsp;<span class="cs__string">&quot;']&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlNode&nbsp;xmlNode&nbsp;=&nbsp;doc.DocumentElement.SelectSingleNode(xPathString,&nbsp;nsmgr);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;xmlNode;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;Get&nbsp;information&nbsp;about&nbsp;a&nbsp;specific&nbsp;book.&nbsp;Pass&nbsp;in&nbsp;an&nbsp;XMLNode&nbsp;that&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;represents&nbsp;the&nbsp;book&nbsp;and&nbsp;populate&nbsp;strings&nbsp;passed&nbsp;in&nbsp;by&nbsp;reference.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;**********************************************************************************&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;GetBookInformation(<span class="cs__keyword">ref</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;title,&nbsp;<span class="cs__keyword">ref</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;ISBN,&nbsp;<span class="cs__keyword">ref</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;publicationDate,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">ref</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;price,&nbsp;<span class="cs__keyword">ref</span>&nbsp;<span class="cs__keyword">string</span>&nbsp;genre,&nbsp;XmlNode&nbsp;book)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlElement&nbsp;bookElement&nbsp;=&nbsp;(XmlElement)book;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Get&nbsp;the&nbsp;attributes&nbsp;of&nbsp;a&nbsp;book.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlAttribute&nbsp;attr&nbsp;=&nbsp;bookElement.GetAttributeNode(<span class="cs__string">&quot;ISBN&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ISBN&nbsp;=&nbsp;attr.InnerXml;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;attr&nbsp;=&nbsp;bookElement.GetAttributeNode(<span class="cs__string">&quot;genre&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;genre&nbsp;=&nbsp;attr.InnerXml;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;attr&nbsp;=&nbsp;bookElement.GetAttributeNode(<span class="cs__string">&quot;publicationdate&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;publicationDate&nbsp;=&nbsp;attr.InnerXml;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Get&nbsp;the&nbsp;values&nbsp;of&nbsp;child&nbsp;elements&nbsp;of&nbsp;a&nbsp;book.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;title&nbsp;=&nbsp;bookElement[<span class="cs__string">&quot;title&quot;</span>].InnerText;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;price&nbsp;=&nbsp;bookElement[<span class="cs__string">&quot;price&quot;</span>].InnerText;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;Uses&nbsp;filter&nbsp;criteria&nbsp;collection&nbsp;in&nbsp;the&nbsp;UI&nbsp;to&nbsp;retreive&nbsp;specific&nbsp;elements&nbsp;and&nbsp;attributes.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;XmlNodeList&nbsp;ApplyFilters(ArrayList&nbsp;conditions,&nbsp;ArrayList&nbsp;operatorSymbols,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ArrayList&nbsp;values,&nbsp;XmlDocument&nbsp;doc,&nbsp;<span class="cs__keyword">string</span>&nbsp;matchString)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlNamespaceManager&nbsp;nsmgr&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;XmlNamespaceManager(doc.NameTable);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;nsmgr.AddNamespace(<span class="cs__string">&quot;bk&quot;</span>,&nbsp;<span class="cs__string">&quot;http://www.contoso.com/books&quot;</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;xPathQueryString&nbsp;=&nbsp;<span class="cs__string">&quot;//bk:books/bk:book[&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;xPathQueryEnding&nbsp;=&nbsp;<span class="cs__string">&quot;]&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ArrayList&nbsp;xPathQueryStrings&nbsp;=&nbsp;<span class="cs__keyword">new</span>&nbsp;ArrayList();&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;booleanOperator;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(matchString&nbsp;==&nbsp;<span class="cs__string">&quot;Any&quot;</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;booleanOperator&nbsp;=&nbsp;<span class="cs__string">&quot;or&nbsp;&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">else</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;booleanOperator&nbsp;=&nbsp;<span class="cs__string">&quot;and&nbsp;&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">int</span>&nbsp;counter&nbsp;=&nbsp;<span class="cs__number">0</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>[]&nbsp;operatorArray&nbsp;=&nbsp;(<span class="cs__keyword">string</span>[])operatorSymbols.ToArray(<span class="cs__keyword">typeof</span>(<span class="cs__keyword">string</span>));&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>[]&nbsp;valueArray&nbsp;=&nbsp;(<span class="cs__keyword">string</span>[])values.ToArray(<span class="cs__keyword">typeof</span>(<span class="cs__keyword">string</span>));&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">foreach</span>&nbsp;(<span class="cs__keyword">string</span>&nbsp;condition&nbsp;<span class="cs__keyword">in</span>&nbsp;conditions)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;xPathQueryPart&nbsp;=&nbsp;<span class="cs__string">&quot;&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;operatorSymbol&nbsp;=&nbsp;operatorArray[counter];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;<span class="cs__keyword">value</span>&nbsp;=&nbsp;valueArray[counter];&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(counter&nbsp;&gt;&nbsp;<span class="cs__number">0</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;xPathQueryString&nbsp;=&nbsp;xPathQueryString&nbsp;&#43;&nbsp;booleanOperator;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;counter&#43;&#43;;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">switch</span>&nbsp;(condition)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">case</span>&nbsp;Constants.Title:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">switch</span>&nbsp;(operatorSymbol)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">case</span>&nbsp;<span class="cs__string">&quot;Contains&quot;</span>:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;xPathQueryPart&nbsp;=&nbsp;<span class="cs__string">&quot;contains(bk:title,'&quot;</span>&nbsp;&#43;&nbsp;<span class="cs__keyword">value</span>&nbsp;&#43;&nbsp;<span class="cs__string">&quot;')&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">break</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">case</span>&nbsp;<span class="cs__string">&quot;Excludes&quot;</span>:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;xPathQueryPart&nbsp;=&nbsp;<span class="cs__string">&quot;not(contains(bk:title,'&quot;</span>&nbsp;&#43;&nbsp;<span class="cs__keyword">value</span>&nbsp;&#43;&nbsp;<span class="cs__string">&quot;'))&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">break</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">case</span>&nbsp;<span class="cs__string">&quot;=&quot;</span>:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;xPathQueryPart&nbsp;=&nbsp;<span class="cs__string">&quot;bk:title='&quot;</span>&nbsp;&#43;&nbsp;<span class="cs__keyword">value</span>&nbsp;&#43;&nbsp;<span class="cs__string">&quot;'&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">break</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">break</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">case</span>&nbsp;Constants.ISBN:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">switch</span>&nbsp;(operatorSymbol)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">case</span>&nbsp;<span class="cs__string">&quot;Contains&quot;</span>:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;xPathQueryPart&nbsp;=&nbsp;<span class="cs__string">&quot;contains(@ISBN,&nbsp;'&quot;</span>&nbsp;&#43;&nbsp;<span class="cs__keyword">value</span>&nbsp;&#43;&nbsp;<span class="cs__string">&quot;')&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">break</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">case</span>&nbsp;<span class="cs__string">&quot;Excludes&quot;</span>:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;xPathQueryPart&nbsp;=&nbsp;<span class="cs__string">&quot;not(contains(@ISBN,&nbsp;'&quot;</span>&nbsp;&#43;&nbsp;<span class="cs__keyword">value</span>&nbsp;&#43;&nbsp;<span class="cs__string">&quot;'))&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">break</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">case</span>&nbsp;<span class="cs__string">&quot;=&quot;</span>:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;xPathQueryPart&nbsp;=&nbsp;<span class="cs__string">&quot;@ISBN='&quot;</span>&nbsp;&#43;&nbsp;<span class="cs__keyword">value</span>&nbsp;&#43;&nbsp;<span class="cs__string">&quot;'&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">break</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">break</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">case</span>&nbsp;Constants.PubDate:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;xPathQueryPart&nbsp;=&nbsp;<span class="cs__string">&quot;contains(@publicationdate,&nbsp;'&quot;</span>&nbsp;&#43;&nbsp;<span class="cs__keyword">value</span>&nbsp;&#43;&nbsp;<span class="cs__string">&quot;')&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">break</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">case</span>&nbsp;Constants.Price:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">switch</span>&nbsp;(operatorSymbol)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">case</span>&nbsp;<span class="cs__string">&quot;=&quot;</span>:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;xPathQueryPart&nbsp;=&nbsp;<span class="cs__string">&quot;bk:price='&quot;</span>&nbsp;&#43;&nbsp;<span class="cs__keyword">value</span>&nbsp;&#43;&nbsp;<span class="cs__string">&quot;'&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">break</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">case</span>&nbsp;<span class="cs__string">&quot;&gt;&quot;</span>:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;xPathQueryPart&nbsp;=&nbsp;<span class="cs__string">&quot;bk:price&gt;'&quot;</span>&nbsp;&#43;&nbsp;<span class="cs__keyword">value</span>&nbsp;&#43;&nbsp;<span class="cs__string">&quot;'&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">break</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">case</span>&nbsp;<span class="cs__string">&quot;&lt;&quot;</span>:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;xPathQueryPart&nbsp;=&nbsp;<span class="cs__string">&quot;bk:price&lt;'&quot;</span>&nbsp;&#43;&nbsp;<span class="cs__keyword">value</span>&nbsp;&#43;&nbsp;<span class="cs__string">&quot;'&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">break</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">case</span>&nbsp;<span class="cs__string">&quot;&gt;=&quot;</span>:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;xPathQueryPart&nbsp;=&nbsp;<span class="cs__string">&quot;bk:price&gt;='&quot;</span>&nbsp;&#43;&nbsp;<span class="cs__keyword">value</span>&nbsp;&#43;&nbsp;<span class="cs__string">&quot;'&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">break</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">case</span>&nbsp;<span class="cs__string">&quot;&lt;=&quot;</span>:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;xPathQueryPart&nbsp;=&nbsp;<span class="cs__string">&quot;bk:price&lt;='&quot;</span>&nbsp;&#43;&nbsp;<span class="cs__keyword">value</span>&nbsp;&#43;&nbsp;<span class="cs__string">&quot;'&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">break</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">case</span>&nbsp;<span class="cs__string">&quot;&lt;&gt;&quot;</span>:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;xPathQueryPart&nbsp;=&nbsp;<span class="cs__string">&quot;bk:price!='&quot;</span>&nbsp;&#43;&nbsp;<span class="cs__keyword">value</span>&nbsp;&#43;&nbsp;<span class="cs__string">&quot;'&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">break</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">break</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">case</span>&nbsp;Constants.Genre:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;xPathQueryPart&nbsp;=&nbsp;<span class="cs__string">&quot;@genre='&quot;</span>&nbsp;&#43;&nbsp;<span class="cs__keyword">value</span>&nbsp;&#43;&nbsp;<span class="cs__string">&quot;'&quot;</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">break</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;xPathQueryString&nbsp;=&nbsp;xPathQueryString&nbsp;&#43;&nbsp;xPathQueryPart;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;xPathQueryString&nbsp;=&nbsp;xPathQueryString&nbsp;&#43;&nbsp;xPathQueryEnding;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlNodeList&nbsp;nodeList&nbsp;=&nbsp;doc.DocumentElement.SelectNodes(xPathQueryString,&nbsp;nsmgr);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;nodeList;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}<span class="cs__preproc">&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;#endregion</span><span class="cs__preproc">&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;#region&nbsp;Add&nbsp;XML&nbsp;elements&nbsp;and&nbsp;attributes</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;Add&nbsp;an&nbsp;element&nbsp;to&nbsp;the&nbsp;XML&nbsp;document.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;This&nbsp;method&nbsp;creates&nbsp;a&nbsp;new&nbsp;book&nbsp;element&nbsp;and&nbsp;saves&nbsp;that&nbsp;element&nbsp;to&nbsp;the&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;XMLDocument&nbsp;object.&nbsp;It&nbsp;addes&nbsp;attributes&nbsp;for&nbsp;the&nbsp;new&nbsp;element&nbsp;and&nbsp;introduces</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;newline&nbsp;characters&nbsp;between&nbsp;elements&nbsp;fora&nbsp;nice&nbsp;readable&nbsp;format.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;XmlElement&nbsp;AddNewBook(<span class="cs__keyword">string</span>&nbsp;genre,&nbsp;<span class="cs__keyword">string</span>&nbsp;ISBN,&nbsp;<span class="cs__keyword">string</span>&nbsp;misc,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;title,&nbsp;<span class="cs__keyword">string</span>&nbsp;price,&nbsp;XmlDocument&nbsp;doc)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Create&nbsp;a&nbsp;new&nbsp;book&nbsp;element.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlElement&nbsp;bookElement&nbsp;=&nbsp;doc.CreateElement(<span class="cs__string">&quot;book&quot;</span>,&nbsp;<span class="cs__string">&quot;http://www.contoso.com/books&quot;</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Create&nbsp;attributes&nbsp;for&nbsp;book&nbsp;and&nbsp;append&nbsp;them&nbsp;to&nbsp;the&nbsp;book&nbsp;element.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlAttribute&nbsp;attribute&nbsp;=&nbsp;doc.CreateAttribute(<span class="cs__string">&quot;genre&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;attribute.Value&nbsp;=&nbsp;genre;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bookElement.Attributes.Append(attribute);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;attribute&nbsp;=&nbsp;doc.CreateAttribute(<span class="cs__string">&quot;ISBN&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;attribute.Value&nbsp;=&nbsp;ISBN;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bookElement.Attributes.Append(attribute);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;attribute&nbsp;=&nbsp;doc.CreateAttribute(<span class="cs__string">&quot;publicationdate&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;attribute.Value&nbsp;=&nbsp;misc;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bookElement.Attributes.Append(attribute);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Create&nbsp;and&nbsp;append&nbsp;a&nbsp;child&nbsp;element&nbsp;for&nbsp;the&nbsp;title&nbsp;of&nbsp;the&nbsp;book.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlElement&nbsp;titleElement&nbsp;=&nbsp;doc.CreateElement(<span class="cs__string">&quot;title&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;titleElement.InnerText&nbsp;=&nbsp;title;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bookElement.AppendChild(titleElement);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Introduce&nbsp;a&nbsp;newline&nbsp;character&nbsp;so&nbsp;that&nbsp;XML&nbsp;is&nbsp;nicely&nbsp;formatted.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bookElement.InnerXml&nbsp;=&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bookElement.InnerXml.Replace(titleElement.OuterXml,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__string">&quot;\n&nbsp;&nbsp;&nbsp;&nbsp;&quot;</span>&nbsp;&#43;&nbsp;titleElement.OuterXml&nbsp;&#43;&nbsp;<span class="cs__string">&quot;&nbsp;\n&nbsp;&nbsp;&nbsp;&nbsp;&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Create&nbsp;and&nbsp;append&nbsp;a&nbsp;child&nbsp;element&nbsp;for&nbsp;the&nbsp;price&nbsp;of&nbsp;the&nbsp;book.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlElement&nbsp;priceElement&nbsp;=&nbsp;doc.CreateElement(<span class="cs__string">&quot;price&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;priceElement.InnerText=&nbsp;price;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bookElement.AppendChild(priceElement);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Introduce&nbsp;a&nbsp;newline&nbsp;character&nbsp;so&nbsp;that&nbsp;XML&nbsp;is&nbsp;nicely&nbsp;formatted.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bookElement.InnerXml&nbsp;=&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bookElement.InnerXml.Replace(priceElement.OuterXml,&nbsp;priceElement.OuterXml&nbsp;&#43;&nbsp;<span class="cs__string">&quot;&nbsp;&nbsp;&nbsp;\n&nbsp;&nbsp;&quot;</span>);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">return</span>&nbsp;bookElement;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;Add&nbsp;an&nbsp;element&nbsp;to&nbsp;the&nbsp;XML&nbsp;document&nbsp;at&nbsp;a&nbsp;specific&nbsp;location</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;Takes&nbsp;a&nbsp;string&nbsp;that&nbsp;describes&nbsp;where&nbsp;the&nbsp;user&nbsp;wants&nbsp;the&nbsp;new&nbsp;node</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;to&nbsp;be&nbsp;positioned.&nbsp;The&nbsp;string&nbsp;comes&nbsp;from&nbsp;a&nbsp;series&nbsp;of&nbsp;radio&nbsp;buttons&nbsp;in&nbsp;a&nbsp;UI.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;this&nbsp;method&nbsp;also&nbsp;accepts&nbsp;the&nbsp;XMLDocument&nbsp;in&nbsp;context.&nbsp;You&nbsp;have&nbsp;to&nbsp;use&nbsp;the&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;this&nbsp;instance&nbsp;because&nbsp;it&nbsp;is&nbsp;the&nbsp;object&nbsp;that&nbsp;was&nbsp;used&nbsp;to&nbsp;generate&nbsp;the&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;selectedBook&nbsp;XMLNode.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;InsertBookElement(XmlElement&nbsp;bookElement,&nbsp;<span class="cs__keyword">string</span>&nbsp;position,&nbsp;XmlNode&nbsp;selectedBook,&nbsp;<span class="cs__keyword">bool</span>&nbsp;validateNode,&nbsp;<span class="cs__keyword">bool</span>&nbsp;generateSchema)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlDocument&nbsp;doc&nbsp;=&nbsp;bookElement.OwnerDocument;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;stringThatContainsNewline&nbsp;=&nbsp;bookElement.OuterXml;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">switch</span>&nbsp;(position)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">case</span>&nbsp;Constants.positionTop:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Add&nbsp;newline&nbsp;characters&nbsp;and&nbsp;spaces&nbsp;to&nbsp;make&nbsp;XML&nbsp;more&nbsp;readable.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlSignificantWhitespace&nbsp;sigWhiteSpace&nbsp;=&nbsp;doc.CreateSignificantWhitespace(<span class="cs__string">&quot;\n&nbsp;&nbsp;&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc.DocumentElement.InsertBefore(sigWhiteSpace,&nbsp;doc.DocumentElement.FirstChild);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc.DocumentElement.InsertAfter(bookElement,&nbsp;doc.DocumentElement.FirstChild);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">break</span>;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">case</span>&nbsp;Constants.positionBottom:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Add&nbsp;newline&nbsp;characters&nbsp;to&nbsp;make&nbsp;XML&nbsp;more&nbsp;readable.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlWhitespace&nbsp;whitespace&nbsp;=&nbsp;doc.CreateWhitespace(<span class="cs__string">&quot;&nbsp;&nbsp;&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlNode&nbsp;appendedNode&nbsp;=&nbsp;doc.DocumentElement.AppendChild(bookElement);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc.DocumentElement.InsertBefore(whitespace,&nbsp;appendedNode);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sigWhiteSpace&nbsp;=&nbsp;doc.CreateSignificantWhitespace(<span class="cs__string">&quot;\n&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc.DocumentElement.InsertAfter(sigWhiteSpace,&nbsp;appendedNode);&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">break</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">case</span>&nbsp;Constants.positionAbove:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Add&nbsp;newline&nbsp;characters&nbsp;to&nbsp;make&nbsp;XML&nbsp;more&nbsp;readable.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlNode&nbsp;currNode&nbsp;=&nbsp;doc.DocumentElement.InsertBefore(bookElement,&nbsp;selectedBook);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sigWhiteSpace&nbsp;=&nbsp;doc.CreateSignificantWhitespace(<span class="cs__string">&quot;\n&nbsp;&nbsp;&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc.DocumentElement.InsertAfter(sigWhiteSpace,&nbsp;currNode);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">break</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">case</span>&nbsp;Constants.positionBelow:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Add&nbsp;newline&nbsp;characters&nbsp;to&nbsp;make&nbsp;XML&nbsp;more&nbsp;readable.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;sigWhiteSpace&nbsp;=&nbsp;doc.CreateSignificantWhitespace(<span class="cs__string">&quot;\n&nbsp;&nbsp;&quot;</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlNode&nbsp;whiteSpaceNode&nbsp;=&nbsp;doc.DocumentElement.InsertAfter(sigWhiteSpace,&nbsp;selectedBook);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc.DocumentElement.InsertAfter(bookElement,&nbsp;whiteSpaceNode);&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">break</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">default</span>:&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;doc.DocumentElement.AppendChild(bookElement);&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">break</span>;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(validateNode)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;validateXML(generateSchema,&nbsp;doc);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}<span class="cs__preproc">&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;#endregion</span><span class="cs__preproc">&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;#region&nbsp;Edit&nbsp;XML&nbsp;elements&nbsp;and&nbsp;attributes</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;Edit&nbsp;an&nbsp;XML&nbsp;element</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;editBook(<span class="cs__keyword">string</span>&nbsp;title,&nbsp;<span class="cs__keyword">string</span>&nbsp;ISBN,&nbsp;<span class="cs__keyword">string</span>&nbsp;publicationDate,&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">string</span>&nbsp;genre,&nbsp;<span class="cs__keyword">string</span>&nbsp;price,&nbsp;XmlNode&nbsp;book,&nbsp;<span class="cs__keyword">bool</span>&nbsp;validateNode,&nbsp;<span class="cs__keyword">bool</span>&nbsp;generateSchema)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlElement&nbsp;bookElement&nbsp;=&nbsp;(XmlElement)book;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Get&nbsp;the&nbsp;attributes&nbsp;of&nbsp;a&nbsp;book.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bookElement.SetAttribute(<span class="cs__string">&quot;ISBN&quot;</span>,&nbsp;ISBN);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bookElement.SetAttribute(<span class="cs__string">&quot;genre&quot;</span>,&nbsp;genre);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bookElement.SetAttribute(<span class="cs__string">&quot;publicationdate&quot;</span>,&nbsp;publicationDate);&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Get&nbsp;the&nbsp;values&nbsp;of&nbsp;child&nbsp;elements&nbsp;of&nbsp;a&nbsp;book.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bookElement[<span class="cs__string">&quot;title&quot;</span>].InnerText&nbsp;=&nbsp;title;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;bookElement[<span class="cs__string">&quot;price&quot;</span>].InnerText&nbsp;=&nbsp;price;&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(validateNode)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;validateXML(generateSchema,&nbsp;bookElement.OwnerDocument);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}<span class="cs__preproc">&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;#endregion</span><span class="cs__preproc">&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;#region&nbsp;Remove&nbsp;elements</span>&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;Summary:&nbsp;Delete&nbsp;a&nbsp;book&nbsp;node&nbsp;from&nbsp;the&nbsp;XMLDocument.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;deleteBook(XmlNode&nbsp;book)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlNode&nbsp;prevNode&nbsp;=&nbsp;book.PreviousSibling;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlNode&nbsp;nextNode&nbsp;=&nbsp;book.NextSibling;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;book.OwnerDocument.DocumentElement.RemoveChild(book);&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(prevNode.NodeType&nbsp;==&nbsp;XmlNodeType.Whitespace&nbsp;||&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;prevNode.NodeType&nbsp;==&nbsp;XmlNodeType.SignificantWhitespace)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;prevNode.OwnerDocument.DocumentElement.RemoveChild(prevNode);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}<span class="cs__preproc">&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;#endregion</span><span class="cs__preproc">&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;#region&nbsp;Position&nbsp;elements</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;Summary:&nbsp;Move&nbsp;elements&nbsp;up&nbsp;in&nbsp;the&nbsp;XML.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;MoveElementUp(XmlNode&nbsp;book)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlNode&nbsp;previousNode&nbsp;=&nbsp;book.PreviousSibling;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">while</span>&nbsp;(previousNode&nbsp;!=&nbsp;<span class="cs__keyword">null</span>&nbsp;&amp;&amp;&nbsp;(previousNode.NodeType&nbsp;!=&nbsp;XmlNodeType.Element))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;previousNode&nbsp;=&nbsp;previousNode.PreviousSibling;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(previousNode&nbsp;!=&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlNode&nbsp;newLineNode&nbsp;=&nbsp;book.NextSibling;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;book.OwnerDocument.DocumentElement.RemoveChild(book);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(newLineNode.NodeType&nbsp;==&nbsp;XmlNodeType.Whitespace&nbsp;|&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;newLineNode.NodeType&nbsp;==&nbsp;XmlNodeType.SignificantWhitespace)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;newLineNode.OwnerDocument.DocumentElement.RemoveChild(newLineNode);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;InsertBookElement((XmlElement)book,&nbsp;Constants.positionAbove,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;previousNode,&nbsp;<span class="cs__keyword">false</span>,&nbsp;<span class="cs__keyword">false</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;Summary:&nbsp;Move&nbsp;elements&nbsp;down&nbsp;in&nbsp;the&nbsp;XML.</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;&nbsp;</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//************************************************************************************</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">public</span>&nbsp;<span class="cs__keyword">void</span>&nbsp;MoveElementDown(XmlNode&nbsp;book)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__com">//&nbsp;Walk&nbsp;backwards&nbsp;until&nbsp;we&nbsp;find&nbsp;an&nbsp;element&nbsp;-&nbsp;ignore&nbsp;text&nbsp;nodes</span>&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlNode&nbsp;NextNode&nbsp;=&nbsp;book.NextSibling;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">while</span>&nbsp;(NextNode&nbsp;!=&nbsp;<span class="cs__keyword">null</span>&nbsp;&amp;&amp;&nbsp;(NextNode.NodeType&nbsp;!=&nbsp;XmlNodeType.Element))&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;NextNode&nbsp;=&nbsp;NextNode.NextSibling;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(NextNode&nbsp;!=&nbsp;<span class="cs__keyword">null</span>)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;XmlNode&nbsp;newLineNode&nbsp;=&nbsp;book.PreviousSibling;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;book.OwnerDocument.DocumentElement.RemoveChild(book);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="cs__keyword">if</span>&nbsp;(newLineNode.NodeType&nbsp;==&nbsp;XmlNodeType.Whitespace&nbsp;|&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;newLineNode.NodeType&nbsp;==&nbsp;XmlNodeType.SignificantWhitespace)&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;newLineNode.OwnerDocument.DocumentElement.RemoveChild(newLineNode);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;InsertBookElement((XmlElement)book,&nbsp;Constants.positionBelow,&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;NextNode,&nbsp;<span class="cs__keyword">false</span>,&nbsp;<span class="cs__keyword">false</span>);&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}<span class="cs__preproc">&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;#endregion</span>&nbsp;
&nbsp;
&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;}</pre>
</div>
</div>
</div>
<h1>More Information</h1>
<p>For more information, see <a href="http://msdn.microsoft.com/en-us/library/hf9hbf87(v=vs.100).aspx">
http://msdn.microsoft.com/en-us/library/hf9hbf87(v=vs.100).aspx</a>.</p>
