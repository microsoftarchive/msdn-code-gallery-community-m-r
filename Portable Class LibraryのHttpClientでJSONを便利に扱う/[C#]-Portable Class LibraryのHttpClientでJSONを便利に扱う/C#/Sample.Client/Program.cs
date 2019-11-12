using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Codeplex.HttpClientExtensions;
using System.Runtime.Serialization;

namespace Sample.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Run().Wait();
            Console.ReadLine();
        }

        static async Task Run()
        {
            var root = "http://localhost:9454/api/";

            var c = new HttpClient();
            {
                // Get
                var r = await c.GetAsync(root + "values");
                r.EnsureSuccessStatusCode();
                var ps = await r.Content.ReadAsJsonAsync<IEnumerable<Person>>();
                foreach (var p in ps)
                {
                    Console.WriteLine("{0} {1}", p.Id, p.Name);
                }
            }
            {
                // Post
                await c.PostAsJsonAsync(root + "values", new Person { Id = -1, Name = "くらいあんとたろう" });
                // Put
                await c.PostAsJsonAsync(root + "values/10", new Person { Id = 10, Name = "くらいあんとたろう" });
            }
        }
    }

    [DataContract]
    public class Person
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }

}
