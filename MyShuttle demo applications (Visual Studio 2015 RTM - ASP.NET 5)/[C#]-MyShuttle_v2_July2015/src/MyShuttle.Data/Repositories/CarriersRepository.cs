
namespace MyShuttle.Data
{
    using Model;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Collections.Generic;
    using System;
    using Microsoft.Data.Entity;

    public class CarrierRepository : ICarrierRepository
    {

        MyShuttleContext _context;
        static readonly int DEFAULT_PICTURE = 0;

        public CarrierRepository(MyShuttleContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<List<Carrier>> GetCarriersAsync(string filter)
        {
            var carriers = _context.Carriers.AsQueryable();
            if (!String.IsNullOrEmpty(filter))
            {
                carriers = carriers.Where(c => c.Name.ToLowerInvariant().Contains(filter.ToLowerInvariant()));
            }
            return await carriers.ToListAsync();
        }

        public async Task<Carrier> GetAsync(int carrierId)
        {
            return await _context.Carriers
                .Where(c => c.CarrierId == carrierId)
                .SingleOrDefaultAsync();
        }

        public async Task<int> AddAsync(Carrier carrier)
        {
            carrier.Picture = Convert.FromBase64String(FakeImages.Carriers[DEFAULT_PICTURE]);

            _context.Carriers.Add(carrier);

            await _context.SaveChangesAsync();

            return carrier.CarrierId;
        }

        public async Task UpdateAsync(Carrier carrier)
        {
            _context.Carriers.Update(carrier);

            await _context.SaveChangesAsync();
        }

        public async Task<SummaryAnalyticInfo> GetAnalyticSummaryInfoAsync(int carrierId)
        {
            var passengers = await _context.Rides.Where(r => r.CarrierId == carrierId).Select(r => r.EmployeeId).ToListAsync();
            var rating = _context.Rides.Where(r => r.CarrierId == carrierId).Select(r => r.Rating);
            return new SummaryAnalyticInfo()
            {
                Rating = (rating.Count() > 0) ? rating.Average() : 0,
                TotalDrivers = await _context.Drivers.Where(r => r.CarrierId == carrierId).CountAsync(),
                TotalPassengers = passengers.Distinct().Count(),
                TotalVehicles = await _context.Vehicles.Where(r => r.CarrierId == carrierId).CountAsync()
            };
        }
    }
}