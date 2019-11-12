using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

namespace RESTbook
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu 
    //       to change the class name "RESTbookService" in code, svc and config file together.

    // NOTE: In order to launch WCF Test Client for testing this service, 
    //      please select RESTbookService.svc or RESTbookService.svc.cs 
    //      at the Solution Explorer and start debugging.


    /// <summary>
    /// Basically this code is developed for HTTP GET, PUT, POST & DELETE operation.
    /// </summary>
   
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]

    public class RESTbookService : IRESTbookService
    {
        //public void DoWork(){}

        //POST operation
        public Book CreateBook(Book book)
        {
            //try
            //{
            //    int bookNo = Repository.GetBookList().Last().BookNo;
            //    book.BookNo = bookNo + 1;
            //    Repository.GetBookList().Add(book);
            //}
            //catch (Exception ex)
            //{
            //    book.BookName = ex.Message;
            //}

            int bookNo = Repository.GetBookList().Last().BookNo;
            book.BookNo = bookNo + 1;
            Repository.GetBookList().Add(book);

            return book;
        }

        //GET Operation
        public List<Book> GetAllBooks()
        {
            return Repository.GetBookList();
        }


        //GET Operation
        public Book GetAbook(string id)
        {
            return Repository.GetBookList().FirstOrDefault(b => b.BookNo.Equals(Convert.ToInt32(id)));
        }


        //PUT Operation
        public Book UpdateBook(string id, Book book)
        {
            Book bo = Repository.GetBookList().FirstOrDefault(b => b.BookNo.Equals(Convert.ToInt32(id)));
            bo.BookName = book.BookName;
            bo.PublicationYear = book.PublicationYear;
            return bo;
        }


        //DELETE Operation
        public string DeleteBook(string id)
        {
            try
            {
                Repository.GetBookList().RemoveAll(e => e.BookNo.Equals(Convert.ToInt32(id)));
                return "1";
            }
            catch (Exception ex)
            {
                return "opps! " + ex.Message;

            }
        }


    }
}
