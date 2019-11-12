using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SingleResponsibilityDemo
{
    //1. Single Responsibility Principle

    // Data access class is only responsible for data base related operations
    class DataAccess
    {
        public static void InsertData()
        {
            Console.WriteLine("Data inserted into database successfully");
        }
    }
    // Logger class is only responsible for logging related operations
    class Logger
    {
        public static void WriteLog()
        {
            Console.WriteLine( "Logged Time:"  + DateTime.Now.ToLongTimeString() + " Log  Data insertion completed successfully");
        }
    }
}