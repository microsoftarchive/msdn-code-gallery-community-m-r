using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Aspose.Words;
using System.Collections;
using System.Text.RegularExpressions;

namespace ResumeParser
{
    public class Parser
    {
        public static string name = "";
        public static string email = "";
        public static string phone = "";
        public static string skills = "";
        public static string summary = "";
        public static string experience = "";
        public static string education = "";
        public static string interests = "";
        public static string languages = "";
        public static int rowIndex = -1;
        public void ParseData()
        {

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[9] { 
                            new DataColumn("Name", typeof(string)),
                            new DataColumn("Email", typeof(string)),
                            new DataColumn("Phone", typeof(string)),
                            new DataColumn("Summary", typeof(string)),
                            new DataColumn("Skills", typeof(string)),
                            new DataColumn("Experience", typeof(string)),
                            new DataColumn("Education", typeof(string)),
                            new DataColumn("Interests", typeof(string)),
                            new DataColumn("Languages", typeof(string))});


            string path = HttpContext.Current.Server.MapPath("Input/");
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            // Get the Excel Files
            FileInfo[] fileinfo = directoryInfo.GetFiles();

            for (int i = 0; i < fileinfo.Length; i++)
            {
                name = "";
                email = "";
                phone = "";
                skills = "";
                summary = "";
                experience = "";
                education = "";
                interests = "";
                languages = "";
                Converter(HttpContext.Current.Server.MapPath("Input/" + fileinfo[i]), fileinfo[i].Extension);

                dt.Rows.Add(name.Trim(','), email.Trim(','), phone.Trim(','), summary.Trim(','), skills.Trim(',').Trim(), experience.Trim(','), education.Trim(','), interests.Trim(','), languages.Trim(','));
            }

            dt.AcceptChanges();
            HttpContext.Current.Session["DataTable"] = dt;
        
        }

        protected void Converter(string path, string extension)
        {
            switch (extension)
            {
                case ".pdf":
                    Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(path);
                    Aspose.Pdf.DocSaveOptions opts = new Aspose.Pdf.DocSaveOptions();
                    opts.Format = Aspose.Pdf.DocSaveOptions.DocFormat.Doc;
                    opts.Mode = Aspose.Pdf.DocSaveOptions.RecognitionMode.Flow;
                    pdfDoc.Save(HttpContext.Current.Server.MapPath("Convert/input.doc"), opts);
                    // Load in the document
                    Document docpdf = new Document(HttpContext.Current.Server.MapPath("Convert/input.doc"));

                    docpdf.Range.Replace("\t", "\n", false, true);
                    docpdf.Save(HttpContext.Current.Server.MapPath("Input/input.txt"));
                    break;
                case ".doc":
                case ".docx":
                case ".txt":
                case ".rtf":
                    {
                        // Load in the document
                        Document doc = new Document(path);
                        doc.Save(HttpContext.Current.Server.MapPath("Input/input.txt"));
                        break;
                    }
            }
            ConvertDocument(HttpContext.Current.Server.MapPath("Input/input.txt"));
        }
        protected void ConvertDocument(string path)
        {



            Document doc = new Document(HttpContext.Current.Server.MapPath("Input/input.txt"));

            int total = doc.GetChildNodes(NodeType.Paragraph, true).Count;

            //doc.Save(Server.MapPath("Input/input.txt"), SaveFormat.Text);

            ArrayList list = (ArrayList)HttpContext.Current.Session["Markers"];
            ArrayList index = new ArrayList();

            Hashtable table = new Hashtable();

            int counter = 0;
            foreach (String item in list)
            {
                CheckMarkers(doc, ref index, item, ref table, counter);
                counter++;
            }

            index.Sort();
            if (index.Count > 0)
            {
                GetParsedData(doc, ref index, total, table);
            }
        }
        protected void CheckMarkers(Document doc, ref ArrayList index, string list, ref Hashtable table, int counter)
        {
            int paras = 0;
            int total = doc.GetChildNodes(NodeType.Paragraph, true).Count;
            foreach (Paragraph para in doc.GetChildNodes(NodeType.Paragraph, true))
            {
                string singlePara = para.GetText().Replace("\r", "").ToLower();


                if (list.ToLower().Split(',').Contains(singlePara))
                {
                    index.Add(paras);
                    table.Add(paras, counter);
                    break;
                }

                paras++;
            }
        }
        protected void GetParsedData(Document doc, ref ArrayList index, int total, Hashtable table)
        {

            int count = 0;

            // Gather the nodes. The GetChild method uses 0-based index
            Paragraph startPara = (Paragraph)doc.FirstSection.GetChild(NodeType.Paragraph, 0, true);
            Paragraph endPara = (Paragraph)doc.FirstSection.GetChild(NodeType.Paragraph, ((int)(index[0]) - 1), true);

            // Extract the content between these nodes in the document. Include these markers in the extraction.
            ArrayList extractedNodes = ExtractContent(startPara, endPara, true);
            ExtractContactInfo(extractedNodes);

            for (int indexer = 0; indexer < index.Count; indexer++)
            {


                // Gather the nodes. The GetChild method uses 0-based index
                startPara = (Paragraph)doc.FirstSection.GetChild(NodeType.Paragraph, (int)index[indexer], true);
                endPara = null;
                if (indexer < index.Count - 1)
                {
                    endPara = (Paragraph)doc.FirstSection.GetChild(NodeType.Paragraph, (int)(index[indexer + 1]) - 1, true);
                }
                else
                {
                    endPara = (Paragraph)doc.FirstSection.GetChild(NodeType.Paragraph, total - 1, true);
                }
                // Extract the content between these nodes in the document. Include these markers in the extraction.
                extractedNodes = ExtractContent(startPara, endPara, true);

                if ((int)table[(int)index[indexer]] == 0)
                    ExtractSummary(extractedNodes);

                else if ((int)table[(int)index[indexer]] == 2)
                    ExtractSkills(extractedNodes);
                else if ((int)table[(int)index[indexer]] == 3)
                    ExtractExperience(extractedNodes);
                else if ((int)table[(int)index[indexer]] == 4)
                    ExtractEducation(extractedNodes);
                else if ((int)table[(int)index[indexer]] == 5)
                    ExtractInterests(extractedNodes);
                else if ((int)table[(int)index[indexer]] == 6)
                    ExtractLanguages(extractedNodes);

            }
        }

        private void ExtractLanguages(ArrayList extractedNodes)
        {
            string[] langs = new string[] { "Chinese" , "Czech" , "Dutch" , "English", "French","German","Greek", "Hungarian", "Italian", "Norwegian", "Portuguese", "Russian", "Spanish", "Swedish" ,"Urdu", "Hindi"};
            for (int i = 1; i < extractedNodes.Count; i++)
            {

                Paragraph p = (Paragraph)extractedNodes[i];
                if (!p.GetText().Equals("") && langs.Contains(p.GetText().Replace("\r", "")))
                    languages += p.GetText().Replace("\r", "") + ",";

            }

        }

        private void ExtractInterests(ArrayList extractedNodes)
        {
            for (int i = 1; i < extractedNodes.Count; i++)
            {

                Paragraph p = (Paragraph)extractedNodes[i];
                if (!p.GetText().Equals(""))
                    interests += p.GetText().Replace("\r", "") + ",";

            }
        }

        private void ExtractContactInfo(ArrayList extractedNodes)
        {
            name = "";
            email = "";
            phone = "";
            for (int i = 0; i < extractedNodes.Count; i++)
            {

                Paragraph p = (Paragraph)extractedNodes[i];
                string value = p.GetText().Replace("\r", "");

                if (!value.Contains("Evaluation Only. ") && !value.Equals(""))
                {
                    if (IsValidName(value) && name.Equals(""))
                    {
                        name = value;
                    }
                    if (email.Equals(""))
                    {
                        email = IsValidEmail(value);
                    }
                    if (phone.Equals(""))
                    {
                        phone = IsValidPhone(value);
                    }
                }
            }
        }
        public bool IsValidName(string isName)
        {

            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(isName, @"^[\p{L}\p{M}' \.\-]+$", RegexOptions.IgnoreCase);

        }
        public string IsValidPhone(string phoneNum)
        {
            const string MatchEmailPattern = @"(\+[0-9]{2}|\+[0-9]{2}\(0\)|\(\+[0-9]{2}\)\(0\)|00[0-9]{2}|0)([0-9]{9}|[0-9\-\s]{7,13})";
            Regex rx = new Regex(MatchEmailPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            // Find matches.
            MatchCollection matches = rx.Matches(phoneNum);

            string phone1 = "";
            foreach (Match match in matches)
            {
                phone1 += match.Value.ToString();
            }

            return phone1;

        }

        public string IsValidEmail(string strIn)
        {

            if (String.IsNullOrEmpty(strIn))
                return "";

            const string MatchEmailPattern =
          @"(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
          + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
            + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
          + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})";
            Regex rx = new Regex(MatchEmailPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            // Find matches.
            MatchCollection matches = rx.Matches(strIn);

            string emails = "";
            foreach (Match match in matches)
            {
                email += match.Value.ToString();
            }

            return email;


        }

        public void ExtractSkills(ArrayList extractedNodes)
        {
            for (int i = 1; i < extractedNodes.Count; i++)
            {

                Paragraph p = (Paragraph)extractedNodes[i];
                if (!p.GetText().Equals(""))
                {
                    string text = p.GetText().Replace("\r", "").Trim();
                    skills +=  text + ",";
                }

            }

        }

        public void ExtractSummary(ArrayList extractedNodes)
        {
            for (int i = 1; i < extractedNodes.Count; i++)
            {

                Paragraph p = (Paragraph)extractedNodes[i];
                if (!p.GetText().Equals(""))
                    summary += p.GetText().Replace("\r", "") + ",";

            }

        }

        public void ExtractExperience(ArrayList extractedNodes)
        {
            int count = 1;
            for (int i = 1; i < extractedNodes.Count; i++)
            {
                
                Paragraph p = (Paragraph)extractedNodes[i];
                if (!p.GetText().Equals(""))
                {
                    experience += p.GetText().Replace("\r", "") + ",";
                    if (count > 2)
                        break;
                    count += 1;
                }
            }

        }

        public void ExtractEducation(ArrayList extractedNodes)
        {
            int count = 1;
            for (int i = 1; i < extractedNodes.Count; i++)
            {
                Paragraph p = (Paragraph)extractedNodes[i];
                if (!p.GetText().Equals(""))
                {
                    education += p.GetText().Replace("\r", "") + ",";
                    if (count > 3)
                        break;
                    count += 1;
                }

            }

        }
        /// <summary>
        /// Extracts a range of nodes from a document found between specified markers and returns a copy of those nodes. Content can be extracted
        /// between inline nodes, block level nodes, and also special nodes such as Comment or Boomarks. Any combination of different marker types can used.
        /// </summary>
        /// <param name="startNode">The node which defines where to start the extraction from the document. This node can be block or inline level of a body.</param>
        /// <param name="endNode">The node which defines where to stop the extraction from the document. This node can be block or inline level of body.</param>
        /// <param name="isInclusive">Should the marker nodes be included.</returns>
        public static ArrayList ExtractContent(Node startNode, Node endNode, bool isInclusive)
        {
            // First check that the nodes passed to this method are valid for use.
            VerifyParameterNodes(startNode, endNode);

            // Create a list to store the extracted nodes.
            ArrayList nodes = new ArrayList();

            // Keep a record of the original nodes passed to this method so we can split marker nodes if needed.
            Node originalStartNode = startNode;
            Node originalEndNode = endNode;

            // Extract content based on block level nodes (paragraphs and tables). Traverse through parent nodes to find them.
            // We will split the content of first and last nodes depending if the marker nodes are inline
            while (startNode.ParentNode.NodeType != NodeType.Body)
                startNode = startNode.ParentNode;

            while (endNode.ParentNode.NodeType != NodeType.Body)
                endNode = endNode.ParentNode;

            bool isExtracting = true;
            bool isStartingNode = true;
            bool isEndingNode = false;
            // The current node we are extracting from the document.
            Node currNode = startNode;

            // Begin extracting content. Process all block level nodes and specifically split the first and last nodes when needed so paragraph formatting is retained.
            // Method is little more complex than a regular extractor as we need to factor in extracting using inline nodes, fields, bookmarks etc as to make it really useful.
            while (isExtracting)
            {
                // Clone the current node and its children to obtain a copy.
                CompositeNode cloneNode = (CompositeNode)currNode.Clone(true);
                isEndingNode = currNode.Equals(endNode);

                if (isStartingNode || isEndingNode)
                {
                    // We need to process each marker separately so pass it off to a separate method instead.
                    if (isStartingNode)
                    {
                        ProcessMarker(cloneNode, nodes, originalStartNode, isInclusive, isStartingNode, isEndingNode);
                        isStartingNode = false;
                    }

                    // Conditional needs to be separate as the block level start and end markers maybe the same node.
                    if (isEndingNode)
                    {
                        ProcessMarker(cloneNode, nodes, originalEndNode, isInclusive, isStartingNode, isEndingNode);
                        isExtracting = false;
                    }
                }
                else
                    // Node is not a start or end marker, simply add the copy to the list.
                    nodes.Add(cloneNode);

                // Move to the next node and extract it. If next node is null that means the rest of the content is found in a different section.
                if (currNode.NextSibling == null && isExtracting)
                {
                    // Move to the next section.
                    Section nextSection = (Section)currNode.GetAncestor(NodeType.Section).NextSibling;
                    currNode = nextSection.Body.FirstChild;
                }
                else
                {
                    // Move to the next node in the body.
                    currNode = currNode.NextSibling;
                }
            }

            // Return the nodes between the node markers.
            return nodes;
        }

        /// <summary>
        /// Checks the input parameters are correct and can be used. Throws an exception if there is any problem.
        /// </summary>
        private static void VerifyParameterNodes(Node startNode, Node endNode)
        {
            // The order in which these checks are done is important.
            if (startNode == null)
                throw new ArgumentException("Start node cannot be null");
            if (endNode == null)
                throw new ArgumentException("End node cannot be null");

            if (!startNode.Document.Equals(endNode.Document))
                throw new ArgumentException("Start node and end node must belong to the same document");

            if (startNode.GetAncestor(NodeType.Body) == null || endNode.GetAncestor(NodeType.Body) == null)
                throw new ArgumentException("Start node and end node must be a child or descendant of a body");

            // Check the end node is after the start node in the DOM tree
            // First check if they are in different sections, then if they're not check their position in the body of the same section they are in.
            Section startSection = (Section)startNode.GetAncestor(NodeType.Section);
            Section endSection = (Section)endNode.GetAncestor(NodeType.Section);

            int startIndex = startSection.ParentNode.IndexOf(startSection);
            int endIndex = endSection.ParentNode.IndexOf(endSection);

            if (startIndex == endIndex)
            {
                if (startSection.Body.IndexOf(startNode) > endSection.Body.IndexOf(endNode))
                    throw new ArgumentException("The end node must be after the start node in the body");
            }
            else if (startIndex > endIndex)
                throw new ArgumentException("The section of end node must be after the section start node");
        }

        /// <summary>
        /// Checks if a node passed is an inline node.
        /// </summary>
        private static bool IsInline(Node node)
        {
            // Test if the node is desendant of a Paragraph or Table node and also is not a paragraph or a table a paragraph inside a comment class which is decesant of a pararaph is possible.
            return ((node.GetAncestor(NodeType.Paragraph) != null || node.GetAncestor(NodeType.Table) != null) && !(node.NodeType == NodeType.Paragraph || node.NodeType == NodeType.Table));
        }

        /// <summary>
        /// Removes the content before or after the marker in the cloned node depending on the type of marker.
        /// </summary>
        private static void ProcessMarker(CompositeNode cloneNode, ArrayList nodes, Node node, bool isInclusive, bool isStartMarker, bool isEndMarker)
        {
            // If we are dealing with a block level node just see if it should be included and add it to the list.
            if (!IsInline(node))
            {
                // Don't add the node twice if the markers are the same node
                if (!(isStartMarker && isEndMarker))
                {
                    if (isInclusive)
                        nodes.Add(cloneNode);
                }
                return;
            }

            // If a marker is a FieldStart node check if it's to be included or not.
            // We assume for simplicity that the FieldStart and FieldEnd appear in the same paragraph.
            if (node.NodeType == NodeType.FieldStart)
            {
                // If the marker is a start node and is not be included then skip to the end of the field.
                // If the marker is an end node and it is to be included then move to the end field so the field will not be removed.
                if ((isStartMarker && !isInclusive) || (!isStartMarker && isInclusive))
                {
                    while (node.NextSibling != null && node.NodeType != NodeType.FieldEnd)
                        node = node.NextSibling;

                }
            }

            // If either marker is part of a comment then to include the comment itself we need to move the pointer forward to the Comment
            // node found after the CommentRangeEnd node.
            if (node.NodeType == NodeType.CommentRangeEnd)
            {
                while (node.NextSibling != null && node.NodeType != NodeType.Comment)
                    node = node.NextSibling;

            }

            // Find the corresponding node in our cloned node by index and return it.
            // If the start and end node are the same some child nodes might already have been removed. Subtract the
            // difference to get the right index.
            int indexDiff = node.ParentNode.ChildNodes.Count - cloneNode.ChildNodes.Count;

            // Child node count identical.
            if (indexDiff == 0)
                node = cloneNode.ChildNodes[node.ParentNode.IndexOf(node)];
            else
                node = cloneNode.ChildNodes[node.ParentNode.IndexOf(node) - indexDiff];

            // Remove the nodes up to/from the marker.
            bool isSkip = false;
            bool isProcessing = true;
            bool isRemoving = isStartMarker;
            Node nextNode = cloneNode.FirstChild;

            while (isProcessing && nextNode != null)
            {
                Node currentNode = nextNode;
                isSkip = false;

                if (currentNode.Equals(node))
                {
                    if (isStartMarker)
                    {
                        isProcessing = false;
                        if (isInclusive)
                            isRemoving = false;
                    }
                    else
                    {
                        isRemoving = true;
                        if (isInclusive)
                            isSkip = true;
                    }
                }

                nextNode = nextNode.NextSibling;
                if (isRemoving && !isSkip)
                    currentNode.Remove();
            }

            // After processing the composite node may become empty. If it has don't include it.
            if (!(isStartMarker && isEndMarker))
            {
                if (cloneNode.HasChildNodes)
                    nodes.Add(cloneNode);
            }

        }
    }
}