using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShuttle.API.Data.Queries;

namespace MyShuttle.API.Data.Factories
{
    public class DriverFactoryQuery : IDriverQueryFactory
    {

        private readonly MyShuttleDashboardContext _ctx;

        public DriverFactoryQuery(MyShuttleDashboardContext ctx)
        {
            _ctx = ctx;
        }

        public TopDriversQuery GetTopDrivers()
        {
            return new TopDriversQuery(_ctx);
        }

        public DriverByIdQuery GetDriverById()
        {
            return new DriverByIdQuery(_ctx);
        }
    }
}
