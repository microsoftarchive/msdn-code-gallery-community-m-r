using MyShuttle.API.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.Data.Queries
{
    public class RidesPerMonthYearVehicleQuery : BaseQuery
    {
        public RidesPerMonthYearVehicleQuery(MyShuttleDashboardContext dbCtx) : base(dbCtx)
        {
        }

        public string DeviceId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }          // 1 based

        public async Task<IEnumerable<Ride>> ExecuteAsync()
        {
            var from = new DateTime(Year, Month, 1);
            var nextMonth = Month + 1;
            var nextYear = Year;
            if (Month == 12) {
                nextMonth = 1;
                nextYear = Year + 1;
            }
            var to = new DateTime(nextYear, nextMonth, 1);
            var rides = await DbContext.Rides.Where(r => r.Vehicle.DeviceId == DeviceId && r.StartDateTime >= from
                && r.EndDateTime < to).ToListAsync();
            return rides;
        }
    }
}
