using MyShuttle.API.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.Data.Queries
{
    public class TopDriversQuery : BaseQuery
    {

        private List<int> _ids;
        public TopDriversQuery(MyShuttleDashboardContext ctx) : base(ctx)
        {
            _ids = new List<int>();
        }

        public async Task<IEnumerable<Driver>> ExecuteAsync()
        {
            var result = await DbContext.Drivers.Where(d => _ids.Contains(d.DriverId)).ToListAsync();

            return result;
        }

        public async Task<int> GetTotalTopDrivers()
        {
            return await DbContext.Drivers.CountAsync();
        }

        public void SetIds(IEnumerable<int> ids)
        {
            _ids.AddRange(ids);
        }
    }
}
