using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http;

namespace Sample.Server.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<Person> Get()
        {
            return new[]
            {
                new Person { Id = 1, Name = "たなか" },
                new Person { Id = 2, Name = "きむら" }
            };
        }

        // GET api/values/5
        public Person Get(int id)
        {
            return new Person { Id = id, Name = "name" };
        }

        // POST api/values
        public void Post([FromBody]Person value)
        {
            Debug.WriteLine("Post {0}, {1}", value.Id, value.Name);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]Person value)
        {
            Debug.WriteLine("Put {0}, {1}, {2}", id, value.Id, value.Name);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            Debug.WriteLine("Delete {0}", id);
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