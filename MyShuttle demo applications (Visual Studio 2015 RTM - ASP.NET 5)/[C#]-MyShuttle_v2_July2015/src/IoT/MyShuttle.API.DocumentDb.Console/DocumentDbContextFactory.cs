using MyShuttle.API.Data.DocumentDb;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.DocumentDb.ConsoleApp
{
    class DocumentDbContextFactory
    {
        public static DocumentDbContext New()
        {
            var uri = ConfigurationManager.AppSettings["DocumentDb:EndpointUrl"];
            var key = ConfigurationManager.AppSettings["DocumentDb:AccessKey"];
            var dbid = ConfigurationManager.AppSettings["DocumentDb:DatabaseId"];

            return new DocumentDbContext(uri, key, dbid);
        }
    }
}
