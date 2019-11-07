using MyShuttle.API.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.Data.Queries
{
    public class DriverByIdQuery : BaseQuery
    {
        private bool _includeRides;
        public DriverByIdQuery(MyShuttleDashboardContext dbCtx) : base(dbCtx)
        {
            _includeRides = false;
        }

        public int Id { get; set; }

        public DriverByIdQuery IncludeRides()
        {
            _includeRides = true;
            return this;
        }

        public async Task<Driver> ExecuteAsync()
        {
            if (Id == 0)
            {
                return null;
            }

            IQueryable<Driver> queryable = DbContext.Drivers;
            if (_includeRides)
            {
                queryable = queryable.Include("Rides").Include("Rides.Vehicle");
           } 
            var driver = await Task.Run<Driver>(() => queryable.SingleOrDefault(d => d.DriverId == Id));
            return driver;
        }
    }
}
