using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RESTbook
{
    // NOTE: You can use the "Rename" command 
    //       on the "Refactor" menu to change the interface name "IRESTbookService" 
    //       in both code and config file together.

    [ServiceContract]
    public interface IRESTbookService
    {
        //[OperationContract]
        //void DoWork();


        //POST operation
        [OperationContract]
        [WebInvoke(UriTemplate = "", Method = "POST")]
        Book CreateBook(Book book);

        //GET Operation
        [OperationContract]
        [WebGet(UriTemplate = "")]
        List<Book> GetAllBooks();

        //GET Operation
        [OperationContract]
        [WebGet(UriTemplate = "{id}")]
        Book GetAbook(string id);

        //PUT Operation
        [OperationContract]
        [WebInvoke(UriTemplate = "{id}", Method = "PUT")]
        Book UpdateBook(string id, Book book);

        //DELETE Operation
        [OperationContract]
        [WebInvoke(UriTemplate = "{id}", Method = "DELETE")]
        string DeleteBook(string id);

    }



    [DataContract]
    public class Book
    {
        [DataMember]
        public int BookNo;

        [DataMember]
        public string BookName;

        [DataMember]
        public int PublicationYear;
    }


}
