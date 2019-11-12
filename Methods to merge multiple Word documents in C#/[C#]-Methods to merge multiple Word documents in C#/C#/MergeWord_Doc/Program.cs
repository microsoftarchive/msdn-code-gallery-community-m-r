using Spire.Doc;

namespace MergeWord_Doc
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create two Document class instances and load two documents that you need to merge
            Document doc1 = new Document(@"C:\Users\Administrator\Desktop\TradeNegotiation.docx");
            Document doc2 = new Document(@"C:\Users\Administrator\Desktop\DisputeSettlement.docx");

            //Call InsertTextFromFile() method to merge the doc2 into the doc1
            string fileName = @"C:\Users\Administrator\Desktop\DisputeSettlement.docx";
            doc1.InsertTextFromFile(fileName, FileFormat.Docx2013);

            //Save the merged document into a new one
            doc1.SaveToFile("MergedDocument.docx", FileFormat.Docx2013);
        }
    }
}
