using MyShuttle.API.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.Data.Queries
{
    public class RidesBetweenDatesQuery : BaseQuery
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDateIncluded { get; set; }

        public RidesBetweenDatesQuery(MyShuttleDashboardContext dbCtx) : base(dbCtx)
        {
        }

        public async Task<IEnumerable<Ride>> ExecuteAsync()
        {
            var from = FromDate.Date;
            var to = ToDateIncluded.Date + new TimeSpan(23, 59, 59);
            var ridesBetween = await DbContext.Rides.Where
                (r => r.StartDateTime >= from && r.EndDateTime <= to).ToListAsync();
            return ridesBetween;
        }

        public void SetIntervalInDays(int days)
        {
            FromDate = ToDateIncluded - TimeSpan.FromDays(days - 1);
        }
    }
}
