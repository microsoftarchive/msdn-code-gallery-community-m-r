using MyShuttle.API.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.Data.Queries
{
    public class RidesPerYearAndVehicleQuery : BaseQuery
    {
        public RidesPerYearAndVehicleQuery(MyShuttleDashboardContext dbCtx) : base(dbCtx)
        {
        }

        public string DeviceId { get; set; }
        public int Year { get; set; }

        public async Task<IEnumerable<Ride>> ExecuteAsync()
        {
            var from = new DateTime(Year, 1, 1);
            var to = new DateTime(Year + 1, 1, 1);
            var rides = await DbContext.Rides.Where(r => r.Vehicle.DeviceId == DeviceId && r.StartDateTime >= from 
                && r.EndDateTime < to).ToListAsync();
            return rides;
        }
    }
}
