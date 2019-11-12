using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SOLIDPrinciplesDemo
{
    class SOLIDPrinciplesDemo
    {
        static void Main(string[] args)
        {
            //1. Single Responsibility Principle
            Console.WriteLine("\n\nSingle Responsibility Principle Demo ");
            SingleResponsibilityDemo.DataAccess.InsertData();
            SingleResponsibilityDemo.Logger.WriteLog();

            //2. Open Close Principle
            OpenClosePrincipleDemo.OSPDemo();

            //3. Liskov Substitution Principle            
            LiskovSubstitutionPrincipleDemo.LSPDemo();
            //4. Interface Segregation Principle
            InterfaceSegregationPrincipleDemo.ISPDemo();

            //5. Dependency Inversion Principle
            DependencyInversionPrincipleDemo.DIPDemo();
 
            Console.ReadLine();
        }
    }
}