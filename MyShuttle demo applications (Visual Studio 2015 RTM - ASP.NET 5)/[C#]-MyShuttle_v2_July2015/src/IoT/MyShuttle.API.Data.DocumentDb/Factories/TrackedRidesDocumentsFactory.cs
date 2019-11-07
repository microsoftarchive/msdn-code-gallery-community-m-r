using MyShuttle.API.Data.DocumentDb.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.Data.DocumentDb.Factories
{
    public class TrackedRidesDocumentsFactory : ITrackedRidesDocumentsFactory
    {
        private readonly DocumentDbContext _ctx;

        public TrackedRidesDocumentsFactory(DocumentDbContext ctx)
        {
            _ctx = ctx;
        }

        public DriverStatisticsQuery GetDriverStatistics()
        {
            return new DriverStatisticsQuery(_ctx);
        }

        public TrackedRidesByVehicleQuery GetTrackedRidesByVehicle()
        {
            return new TrackedRidesByVehicleQuery(_ctx);
        }
    }
}
