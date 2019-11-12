using MyShuttle.API.Data.DocumentDb.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.Data.DocumentDb.Factories
{
    public class VehicleDocumentsFactory : IVehicleDocumentsFactory
    {
        private readonly DocumentDbContext _ctx;

        public VehicleDocumentsFactory(DocumentDbContext ctx)
        {
            _ctx = ctx;
        }

        public VehicleSummaryQuery GetSummary()
        {
            return new VehicleSummaryQuery(_ctx);
        }
    }
}
