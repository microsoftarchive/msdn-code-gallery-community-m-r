using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwinSource
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (WebApp.Start<StartUpController>("http://localhost:5888/"))
                {
                    Console.WriteLine("we 're up and running");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                Console.Read();
            }
        }
    }
}
