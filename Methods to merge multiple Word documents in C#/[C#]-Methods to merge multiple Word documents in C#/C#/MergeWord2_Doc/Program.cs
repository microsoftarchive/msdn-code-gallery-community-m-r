using Spire.Doc;
using Spire.Doc.Documents;

namespace MergeWord2_Doc
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create two instances Document classes and load the documents
            Document doc1 = new Document(@"C:\Users\Administrator\Desktop\TradeNegotiation.docx");
            Document doc2 = new Document(@"C:\Users\Administrator\Desktop\DisputeSettlement.docx");

            //Get the last section of doc1
            Section lastSection = doc1.LastSection;

            //Traverse the section and paragraph of doc2, and clone the paragraphs to the last section of doc1
            foreach (Section section in doc2.Sections)
            {
                foreach (Paragraph paragraph in section.Paragraphs)
                {
                    lastSection.Paragraphs.Add(paragraph.Clone() as Paragraph);
                }
            }

            //Save the merged document into a new one
            doc1.SaveToFile("Merged.docx", FileFormat.Docx2013);
        }
    }
}
