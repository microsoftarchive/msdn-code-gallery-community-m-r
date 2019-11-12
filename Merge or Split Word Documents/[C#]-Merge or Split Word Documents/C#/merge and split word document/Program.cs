using Spire.Doc;
using Spire.Doc.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace merge_and_split_word_document
{
    class Program
    {
        static void Main(string[] args)
        {   //(This part is merging documents)
            //Initialize a new word document object and load the original word file "A good man.docx".
            Document document1 = new Document();
            document1.LoadFromFile(@"C:\Users\Administrator\Desktop\A good man.docx", FileFormat.Docx);
            //Merge another word file "The Kite Runner.docx" to the original one.
            document1.InsertTextFromFile(@"C:\Users\Administrator\Desktop\The Kite Runner.docx", FileFormat.Docx);
            //Save and launch the file.
            document1.SaveToFile("MergedFile.docx", FileFormat.Docx);
            System.Diagnostics.Process.Start("MergedFile.docx");


            //(This part is spliting document by section break)
            //Initialize a new word document object and load the original word document which has two sections.
            Document document2 = new Document();
            document2.LoadFromFile(@"C:\Users\Administrator\Desktop\Text.docx", FileFormat.Docx);
            //Define another new word document object.
            Document newWord;
            //Traverse through all sections of the original word document, clone each section and add it to a new word document as new section, then save the document to specific path.
            for (int i = 0; i < document2.Sections.Count; i++)
            {
                newWord = new Document();
                newWord.Sections.Add(document2.Sections[i].Clone());
                newWord.SaveToFile(String.Format(@"test\out_{0}.docx", i));

            }



            //(This part is spliting document by page break)
            //Create a word document and load the original word document.
            Document original = new Document();
            original.LoadFromFile(@"C:\Users\Administrator\Desktop\daffodil.docx", FileFormat.Docx);
            //Create a new word document and add a section to it.
            Document document3 = new Document();
            Section section = document3.AddSection();
            //Split the original word document into separate documents according to page break.
            int index = 0;
            foreach (Section sec in original.Sections)
            {
                foreach (DocumentObject obj in sec.Body.ChildObjects)
                {
                    if (obj is Paragraph)
                    {
                        Paragraph para = obj as Paragraph;
                        section.Body.ChildObjects.Add(para.Clone());

                        foreach (DocumentObject parobj in para.ChildObjects)
                        {
                            if (parobj is Break && (parobj as Break).BreakType == BreakType.PageBreak)
                            {
                                int i = para.ChildObjects.IndexOf(parobj);
                                section.Body.LastParagraph.ChildObjects.RemoveAt(i);
                                document3.SaveToFile(String.Format("result/out-{0}.docx", index), FileFormat.Docx);
                                index++;

                                document3 = new Document();
                                section = document3.AddSection();
                                section.Body.ChildObjects.Add(para.Clone());
                                if (section.Paragraphs[0].ChildObjects.Count == 0)
                                {
                                    section.Body.ChildObjects.RemoveAt(0);
                                }
                                else
                                {
                                    while (i >= 0)
                                    {
                                        section.Paragraphs[0].ChildObjects.RemoveAt(i);
                                        i--;
                                    }
                                }
                            }
                        }
                    }
                    if (obj is Table)
                    {
                        section.Body.ChildObjects.Add(obj.Clone());
                    }
                }
            }
            //save the file
            document3.SaveToFile(String.Format(@"result/out-{0}.docx", index), FileFormat.Docx);


            
        }


    }
}
