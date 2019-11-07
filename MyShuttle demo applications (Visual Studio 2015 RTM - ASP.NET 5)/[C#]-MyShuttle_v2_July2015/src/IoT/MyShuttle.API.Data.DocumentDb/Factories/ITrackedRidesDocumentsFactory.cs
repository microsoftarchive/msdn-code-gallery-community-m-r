using MyShuttle.API.Data.DocumentDb.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.Data.DocumentDb.Factories
{
    public interface ITrackedRidesDocumentsFactory
    {
        DriverStatisticsQuery GetDriverStatistics();
        TrackedRidesByVehicleQuery GetTrackedRidesByVehicle();
    }
}
