using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShuttle.API.Data.DocumentDb.Queries;

namespace MyShuttle.API.Data.DocumentDb.Factories
{
    public class DrivingStyleDocumentsFactory : IDrivingStyleDocumentsFactory
    {
        private readonly DocumentDbContext _ctx;

        public DrivingStyleDocumentsFactory(DocumentDbContext ctx)
        {
            _ctx = ctx;
        }

        public TopRatedDriversQuery GetTopRatedDrivers()
        {
            return new TopRatedDriversQuery(_ctx);
        }
        public DriverStylesQuery GetStylesForDriver()
        {
            return new DriverStylesQuery(_ctx);
        }
    }
}
