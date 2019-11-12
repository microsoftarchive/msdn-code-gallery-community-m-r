namespace MyShuttle.Data
{
    using Microsoft.Data.Entity;
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class DriverRepository : IDriverRepository
    {
        MyShuttleContext _context;

        public DriverRepository(MyShuttleContext dbcontext)
	    {
            _context = dbcontext;
        }

        public async Task<Driver> GetAsync(int carrierId, int driverId)
        {
            var results = await _context.Drivers
                .Where(d => d.CarrierId == carrierId && d.DriverId == driverId)
                .Select(d => new
                {
                    Driver = d,
                    Vehicle = _context.Vehicles.SingleOrDefault(v => v.VehicleId == d.VehicleId)
                })
                .ToListAsync();

            return results.Select(d => BuildDriver(d.Driver, d.Vehicle)).SingleOrDefault();
        }

        public async Task<IEnumerable<Driver>> GetDriversAsync(int carrierId, string filter, int pageSize, int pageCount)
        {
            var results = await _context.Drivers
                .Where(d => d.CarrierId == carrierId && (String.IsNullOrEmpty(filter) || d.Name.Contains(filter)))
                .Select(d => new
                {
                    Driver = d,
                })
                .OrderByDescending(d => d.Driver.Name)
                .Skip(pageSize * pageCount)
                .Take(pageSize)
                .ToListAsync();

            var result = results.Select(v => BuildDriver(v.Driver, null));

            return result;
        }

        public async Task<IEnumerable<Driver>> GetDriversFilterAsync(int carrierId)
        {
            var results = await _context.Drivers
                .Where(d => d.CarrierId == carrierId)
                .Select(d => new
                {
                    Driver = d,
                })
                .OrderByDescending(d => d.Driver.Name)
                .ToListAsync();

            var result = results.Select(v => BuildDriver(v.Driver, null, false));

            return result;
        }

        public async Task<int> GetCountAsync(int carrierId, string filter)
        {
            return await _context.Drivers
                .Where(d => d.CarrierId == carrierId && (String.IsNullOrEmpty(filter) || d.Name.Contains(filter)))
                .CountAsync();
        }

        public async Task<int> AddAsync(Driver driver)
        {
            _context.Drivers.Add(driver);

            await _context.SaveChangesAsync();

            return driver.DriverId;
        }

        public async Task UpdateAsync(Driver driver)
        {
            _context.Drivers.Update(driver);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int driverId)
        {
            var driver = await _context.Drivers
                .SingleOrDefaultAsync(d => d.DriverId == driverId);

            if (driver != null)
            {
                _context.Drivers
                    .Remove(driver);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<Driver>> GetTopDriversAsync(int carrierId, int numOfDrivers)
        {
            return await _context.Drivers
                .Where(d => d.CarrierId == carrierId)
                .OrderByDescending(d => d.RatingAvg)
                .Take(numOfDrivers)
                .Select(d => new Driver
                {
                    DriverId = d.DriverId,
                    Name = d.Name,
                    Picture = d.Picture,
                    RatingAvg = d.RatingAvg,
                    TotalRides = d.TotalRides,
                })
                .ToListAsync();
        }

        private Driver BuildDriver(Driver driver, Vehicle vehicle, bool includePicture = true)
        {
            //the idea is remove reference for improve
            //client work without $ref
            var created = new Driver
            {
                DriverId = driver.DriverId,
                CarrierId = driver.CarrierId,
                Name = driver.Name,
                Phone = driver.Phone,
                Picture = includePicture ? driver.Picture : null,
                VehicleId = driver.VehicleId,
                RatingAvg = driver.RatingAvg,
                TotalRides = driver.TotalRides,
                Vehicle = vehicle == null ? null :  new Vehicle()
                            {
                                LicensePlate = vehicle.LicensePlate,
                                Make = vehicle.Make,
                                Model = vehicle.Model
                            }
            };

            return created;
        }

    }
}