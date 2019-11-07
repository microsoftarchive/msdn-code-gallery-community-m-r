using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTbook
{
    public class Repository
    {

        static List<Book> BookList;

        static Repository()
        {

            BookList = new List<Book>();

            BookList.Add(new Book { BookNo = 1, BookName = "For Whom the Bell Tolls ",  PublicationYear = 1940 });
            BookList.Add(new Book { BookNo = 2, BookName = "The Grapes of Wrath",       PublicationYear = 1939 });
            BookList.Add(new Book { BookNo = 3, BookName = "The Captain's Daughter",    PublicationYear = 1836 });
            BookList.Add(new Book { BookNo = 4, BookName = "Madame Bovary",             PublicationYear = 1856 });
            BookList.Add(new Book { BookNo = 5, BookName = "Father Goriot",             PublicationYear = 1835 });

        }


        public static List<Book> GetBookList()
        {
            return BookList;
        }

    }
}