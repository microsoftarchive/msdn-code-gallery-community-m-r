using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShuttle.API.Data.Queries;

namespace MyShuttle.API.Data.Factories
{
    public class RideFactoryQuery : IRideFactoryQuery
    {
        private readonly MyShuttleDashboardContext _ctx;

        public RideFactoryQuery(MyShuttleDashboardContext db)
        {
            _ctx = db;
        }
        public RidesBetweenDatesQuery GetRidesBetweenDates()
        {
            return new RidesBetweenDatesQuery(_ctx);
        }

        public RidesPerYearAndVehicleQuery GetRidesPerYearAndVehicle()
        {
            return new RidesPerYearAndVehicleQuery(_ctx);
        }

        public RidesPerMonthYearVehicleQuery GetRidesPerMonthYearVehicleQuery()
        {
            return new RidesPerMonthYearVehicleQuery(_ctx);
        }
    }
}
