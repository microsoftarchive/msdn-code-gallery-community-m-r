using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JSON_CSHARP
{
    class Program
    {
        static void Main(string[] args)
        {
            RunOrdersAPI(10);
        }

        #region For JSON Methods
        private static void RunProductsAPI()
        {
            #region For Products
            var jObjectP = new JObject();
            dynamic jsonP = JValue.Parse(getProducts());
            dynamic dynJsoncP = jsonP["product"];
            List<Products> products = dynJsoncP.ToObject<List<Products>>();
            foreach (var item in products)
            {
                Console.WriteLine(item.name);
            }
            #endregion
        }

        private static void RunOrdersAPI(int r)
        {
            #region For Orders
            var jObject = new JObject();
            dynamic json = JValue.Parse(getOrders(r));
            dynamic dynJsonc = json["order"];
            List<Orders> orders = dynJsonc.ToObject<List<Orders>>();
            foreach (var item in orders)
            {
                Console.WriteLine(item.shipments[0].order_id + " " + item.shipments[0].cart_vendor + " " + item.shipments[0].status);
            }
            #endregion
        }
        public static string getProducts()
        {
            Console.WriteLine("Initializing API Call to  API Servers for Products");
            WebRequest request = WebRequest.Create("https://api.domain.com/product/");
            request.Method = "GET";
            string userName = "SandeepThakur";
            string password = "Password";
            string credentials = userName + ":" + password;
            request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(credentials));
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string serverResponse = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            Console.WriteLine("API Call to Servers for Products Completed");
            Console.WriteLine("Generating JSON from Response");
            return serverResponse;
        }
        public static string getOrders(int r)
        {
            string oDate = oDate = System.DateTime.UtcNow.Date.AddDays(-5).ToShortDateString();
            try
            {
                Console.WriteLine("Waiting for Order Date (mm/dd/yyyy): ");
                oDate = Reader.ReadLine(30000);
            }
            catch (TimeoutException)
            {
                oDate = System.DateTime.UtcNow.Date.ToShortDateString();
                Console.WriteLine("Setting Today as Order Date... ");
            }
            if (oDate.Length == 0)
            {
                oDate = System.DateTime.UtcNow.Date.AddDays(-5).ToShortDateString();
            }
            Console.WriteLine("Initializing API Call to Servers");
            WebRequest request = WebRequest.Create("https://api.domain.com/orders/");
            request.Method = "GET";
            string userName = "SandeepThakur";
            string password = "Password";
            string credentials = userName + ":" + password;
            request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(credentials));
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string serverResponse = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            Console.WriteLine("API Call to Servers Completed");
            Console.WriteLine("Generating JSON from Response");
            return serverResponse;

        }
        public static bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        #endregion
    }

}
