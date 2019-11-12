using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.techphernalia.windows.forms.techieUI;
using MongoDB.Bson;

namespace MongoStart
{
    [Title("BSON")]
    [Prefix("BSON")]
    public class BSONTutorial : TreeDataList
    {
        #region 1. Creating Document
        [Title("Create Empty Document")]
        [Category("1. Creating Document")]
        [Description("An empty document is an object with no key and no value { } ")]
        [Code("Console", "var _doc = { }; ")]
        [Code("C#", "BsonDocument _doc = new BsonDocument(); ")]
        public void BSON000Document()
        {
            BsonDocument _doc = new BsonDocument();
            Console.WriteLine(_doc);
        }

        [Title("Simple Key Value")]
        [Category("1. Creating Document")]
        [Description("A simple document with FirstName as key and Durgesh as its value { \"FirstName\", \"Durgesh\" } ")]
        [Code("Console", "var _doc = { \"FirstName\" : \"Durgesh\" } ;")]
        [Code("C#","BsonDocument _doc = new BsonDocument(\"FirstName\", \"Durgesh\");")]
        public void BSON010DocumentWithKeyValue()
        {
            BsonDocument _doc = new BsonDocument("FirstName", "Durgesh");
            Console.WriteLine(_doc);
        }
        
        [Title("String Valued Document")]
        [Category("1. Creating Document")]
        [Description("A simple document with value of type String ")]
        [Code("Console", "var _doc = { \"FirstName\" : \"Durgesh\" }; ")]
        [Code("C#","BsonDocument _doc = new BsonDocument(\"FirstName\", \"Durgesh\");")]
        public void BSON020DocumentString()
        {
            BsonDocument _doc = new BsonDocument("FirstName", "Durgesh");
            Console.WriteLine(_doc);
        }
        
        [Title("Numeric Valued Document")]
        [Category("1. Creating Document")]
        [Description("A simple document with value of type Number ")]
        [Code("Console", "var _doc = { \"SalaryInLac\" : 25.4 }; ")]
        [Code("C#","BsonDocument _doc = new BsonDocument(\"SalaryInLac\", 25.4);")]
        public void BSON030DocumentNumber()
        {
            BsonDocument _doc = new BsonDocument("SalaryInLac", 25.4);
            Console.WriteLine(_doc);
        }
        
        [Title("Array  Valued Document")]
        [Category("1. Creating Document")]
        [Description("A simple document with value of type Array ")]
        [Code("Console", "var _doc = { \"Technology\" : [\"Java\", \"C#\", \"SQL\"] }; ")]
        [Code("C#","BsonDocument _doc = new BsonDocument(\"Technology\", new BsonArray(new List<String>() { \"Java\", \"C#\", \"SQL\" }));")]
        public void BSON040DocumentArray()
        {
            BsonDocument _doc = new BsonDocument("Technology", new BsonArray(new List<String>() { "Java", "C#", "SQL" }));
            Console.WriteLine(_doc);
        }
        
        [Title("Document Valued Document")]
        [Category("1. Creating Document")]
        [Description("A simple document with value of type Document ie Sub Document ")]
        [Code("Console", "var _doc = { \"Address\" : { \"Home\" : \"Great Den\" } }; ")]
        [Code("C#","BsonDocument _doc = new BsonDocument(\"Address\", new BsonDocument(\"Home\", \"Great Den\"));")]
        public void BSON050DocumentDocument()
        {
            BsonDocument _doc = new BsonDocument("Address", new BsonDocument("Home", "Great Den"));
            Console.WriteLine(_doc);
        }
        
        [Title("Boolean Valued Document")]
        [Category("1. Creating Document")]
        [Description("A simple document with value of type Boolean ")]
        [Code("Console", "var _doc = { \"IsSingle\" : true }; ")]
        [Code("C#","BsonDocuent _doc = new BsonDocument(\"IsSingle\", true);")]
        public void BSON060DocumentBoolean()
        {
            BsonDocument _doc = new BsonDocument("IsSingle", true);
            Console.WriteLine(_doc);
        }
        
        [Title("Null Valued Document")]
        [Category("1. Creating Document")]
        [Description("A simple document with value null")]
        [Code("Console", "var _doc = { \"SpouseName\" : null }; ")]
        [Code("C#","BsonDocument _doc = new BsonDocument(\"SpouseName\", BsonNull.Value);")]
        public void BSON070DocumentNull()
        {
            BsonDocument _doc = new BsonDocument("SpouseName", BsonNull.Value);
            Console.WriteLine(_doc);
        }
        
        [Title("Add field to existing document")]
        [Category("1. Creating Document")]
        [Description("Adding another field to existing document")]
        [Code("Console", new string[]{"var _doc = { \"FirstName\" : \"Durgesh\" };"," _doc.LastName = \"Chaudhary\"; "})]
        [Code("C#",new string[]{ "BsonDocument _doc = new BsonDocument(\"FirstName\", \"Durgesh\");","_doc.Add(\"LastName\", \"Chaudhary\");"})]
        public void BSON080AddFieldToDocument()
        {
            BsonDocument _doc = new BsonDocument("FirstName", "Durgesh");
            _doc.Add("LastName", "Chaudhary");
            Console.WriteLine(_doc);
        }
        
        [Title("Multiple fields in document")]
        [Category("1. Creating Document")]
        [Description("Creating Document with multiple fields")]
        [Code("Console", "var _doc = { \"FirstName\" : \"Durgesh\", \"LastName\" : \"Chaudhary\" }; ")]
        [Code("C#","BsonDocument _doc = new BsonDocument { { \"FirstName\", \"Durgesh\" }, { \"LastName\", \"Chaudhary\" } };")]
        public void BSON090DocumentMultipleFields()
        {
            BsonDocument _doc = new BsonDocument { { "FirstName", "Durgesh" }, { "LastName", "Chaudhary" } };
            Console.WriteLine(_doc);
        }
        
        [Title("Creating Complex Document")]
        [Category("1. Creating Document")]
        [Description("Creating a complex document with many fields and of different type.")]
        [Code("Console", "var _doc = { \"FirstName\" : \"Durgesh\", \"LastName\" : \"Chaudhary\", \"Age\" : 25, \"Address\" : { \"Home\" : \"Cool Den\", \"Office\" : \"Hot Environment\" }, \"IsSingle\" : true, \"SalaryInLac\" : 25.4, \"DOB\" : ISODate(\"1988-09-26T18:30:00Z\"), \"TechInfo\" : [\"ASP.Net\", \"C#\", \"MongoDB\"] }; ")]
        [Code("C#", new string[]{"BsonDocument _doc = new BsonDocument ","{ ","	{ \"FirstName\", \"Durgesh\" }, ","	{ \"LastName\", \"Chaudhary\" } ,","	{\"Age\"  , 25},","	{\"Address\" , new BsonDocument","		{","			{\"Home\" , \"Cool Den\"},","			{\"Office\" , \"Hot Environment\"}","		}","	},","	{\"IsSingle\",true},","	{\"SalaryInLac\",25.4},","	{\"DOB\",new DateTime(1988,9,27)},","	{\"TechInfo\" , new BsonArray(new List<String>{ \"ASP.Net\" , \"C#\" , \"MongoDB\" })}","};"})]
        public void BSON100DocumentComplex()
        {
            BsonDocument _doc = new BsonDocument 
            { 
                { "FirstName", "Durgesh" }, 
                { "LastName", "Chaudhary" } ,
                {"Age"  , 25},
                {"Address" , new BsonDocument
                    {
                        {"Home" , "Cool Den"},
                        {"Office" , "Hot Environment"}
                    }
                },
                {"IsSingle",true},
                {"SalaryInLac",25.4},
                {"DOB",new DateTime(1988,9,27)},
                {"TechInfo" , new BsonArray(new List<String>{ "ASP.Net" , "C#" , "MongoDB" })}
            };
            Console.WriteLine(_doc);
        }
        #endregion
    }
}
