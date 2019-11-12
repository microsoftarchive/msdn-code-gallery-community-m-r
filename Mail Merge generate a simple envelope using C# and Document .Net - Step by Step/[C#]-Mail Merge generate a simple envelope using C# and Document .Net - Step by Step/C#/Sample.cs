using System;
using System.IO;
using SautinSoft.Document;

namespace Sample
{
    class Sample
    {
        static void Main(string[] args)
        {
            DocumentCore dc = DocumentCore.Load(@"envelope-template.docx");

            var dataSource = new[] { new { Name = "Homer", FamilyName = "Simpson" },  
                    new { Name = "Marge ", FamilyName = "Simpson" }, 
                    new { Name = "Bart", FamilyName = "Simpson" }, 
                    new { Name = "Lisa", FamilyName = "Simpson" }, 
                    new { Name = "Maggie", FamilyName = "Simpson" }};

            dc.MailMerge.Execute(dataSource);
            dc.Save(@"Simpson-family.docx");

        }
    }
}
