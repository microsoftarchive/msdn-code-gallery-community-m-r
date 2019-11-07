

namespace MyShuttle.Data
{
    using Model;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Collections.Generic;
    using System;
    using Microsoft.Data.Entity;

    public class RidesRepository : IRidesRepository
    {
        MyShuttleContext _context;

        public RidesRepository(MyShuttleContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<Ride> GetAsync(int rideId)
        {
            var results = await _context.Rides
               .Where(r => r.RideId == rideId)
               .Select(r => new
               {
                   Ride = r,
                   DriverName = _context.Drivers.Where(d => d.DriverId == r.DriverId).Select(d => d.Name).FirstOrDefault(),
                   Vehicle = _context.Vehicles.SingleOrDefault(v => v.VehicleId == r.VehicleId),
                   Employee = _context.Employees.SingleOrDefault(e => e.EmployeeId == r.EmployeeId),
               })
               .ToListAsync();

            // ToDo: WA
            return results.Select(d => BuildRide(d.Ride, d.DriverName, d.Vehicle, d.Employee)).SingleOrDefault();
        }

        public async Task<IEnumerable<Ride>> GetRidesAsync(int carrierId, int? driverId, int? vehicleId, int pageSize, int pageCount)
        {
            int driverValue = driverId.HasValue ? driverId.Value : 0;
            int vehicleValue = vehicleId.HasValue ? vehicleId.Value : 0;

            var results = await _context.Rides
                .Where(r =>
                    r.CarrierId == carrierId &&
                    (vehicleValue == 0 || vehicleValue == r.VehicleId) &&
                    (driverValue == 0 || driverValue == r.DriverId))
                .OrderByDescending(r => r.StartDateTime)
                .ToListAsync();

            var rides = results
                .Skip(pageSize * pageCount)
                .Take(pageSize)
                .Select(r => new
                {
                    Ride = r,
                    DriverName =
                        _context.Drivers.Where(d => d.DriverId == r.DriverId).Select(d => d.Name).FirstOrDefault(),
                    Vehicle =
                        _context.Vehicles.SingleOrDefault(v => v.VehicleId == r.VehicleId)
                })
                .ToList();

            return rides.Select(r => BuildRide(r.Ride, r.DriverName, r.Vehicle, null));
        }

        public async Task<IEnumerable<Ride>> GetEmployeeAsync(int employeeId, int count)
        {
            var results = await _context.Rides
                .Where(r => r.EmployeeId == employeeId)
                .Select(r => new
                {
                    Ride = r,
                    DriverName =
                        _context.Drivers.Where(d => d.DriverId == r.DriverId).Select(d => d.Name).FirstOrDefault(),
                })
                .OrderByDescending(r => r.Ride.StartDateTime)
                .Take(count)
                .ToListAsync();

            return results.Select(v => BuildRide(v.Ride, v.DriverName, null, null));
        }

        public async Task<IEnumerable<Ride>> GetCompanyAsync(int customerId, int count)
        {
            var rides = await _context.Rides
                .Select(r => new
                {
                    Ride = r,
                    Employee = _context.Employees.SingleOrDefault(d => d.EmployeeId == r.EmployeeId),
                })
                .Where(r => r.Employee.CustomerId == customerId)
                .OrderByDescending(r => r.Ride.StartDateTime)
                .Take(count)
                .ToListAsync();

            var results = rides.Select(r => new
                    {
                        Ride = r.Ride,
                        Employee = r.Employee,
                        DriverName =
                                _context.Drivers.Where(d => d.DriverId == r.Ride.DriverId).Select(d => d.Name).FirstOrDefault()
                    })
                    .ToList();

            return results.Select(v => BuildRide(v.Ride, v.DriverName, null, v.Employee));
        }

        public async Task<int> GetCountAsync(int carrierId, int? driverId, int? vehicleId)
        {
            int driverValue = driverId.HasValue ? driverId.Value : 0;
            int vehicleValue = vehicleId.HasValue ? vehicleId.Value : 0;

            return await _context.Rides
                .Where(r =>
                    r.CarrierId == carrierId &&
                    (vehicleValue == 0 || vehicleValue == r.VehicleId) &&
                    (driverValue == 0 || driverValue == r.DriverId))
                .CountAsync();
        }

        public async Task<IEnumerable<RideResult>> GetRidesEvolutionAsync(int carrierId, DateTime from)
        {

            return await _context.Rides
                        .Where(r => r.CarrierId == carrierId)
                        .Where(r => r.StartDateTime >= from)
                        .GroupBy(r => r.StartDateTime.Date)
                        .OrderByDescending(g => g.Key)
                        .Select(g => new RideResult { Date = g.Key, Value = g.Count() })
                        .ToListAsync();
        }

        public async Task<int> LastDaysPassengersAsync(int carrierId, DateTime from)
        {
            var results = await _context.Rides
                            .Where(r => r.CarrierId == carrierId && r.StartDateTime >= from)
                            .Select(r => r.EmployeeId)
                            .ToListAsync();

            // ToDO: Distinct doesn´t work inside the EF query with this version
            return results.Distinct().Count();
        }

        public async Task<int> LastDaysRidesAsync(int carrierId, DateTime from)
        {
            return await _context.Rides
                            .Where(r => r.CarrierId == carrierId && r.StartDateTime >= from)
                            .CountAsync();
        }

        public async Task UpdateAsync(Ride ride)
        {
            _context.Rides.Update(ride);

            await _context.SaveChangesAsync();
        }

        private Ride BuildRide(Ride ride, string driverName, Vehicle vehicle, Employee employee)
        {
            var created = new Ride
            {
                CarrierId = ride.CarrierId,
                Id = ride.Id,
                Comments = ride.Comments,
                Cost = ride.Cost,
                Distance = ride.Distance,
                Duration = ride.Duration,
                EndAddress = ride.EndAddress,
                StartAddress = ride.StartAddress,
                EndDateTime = ride.EndDateTime,
                StartDateTime = ride.StartDateTime,
                StartLatitude = 10,
                EndLatitude = 10,
                StartLongitude = 10,
                EndLongitude = 10,
                Rating = ride.Rating,
                RideId = ride.RideId,
                VehicleId = ride.VehicleId,
                Vehicle = vehicle == null ? null : new Vehicle()
                {
                    LicensePlate = vehicle.LicensePlate,
                    Model = vehicle.Model,
                    Picture = vehicle.Picture,
                    Make = vehicle.Make,
                    Rate = vehicle.Rate
                },
                DriverId = ride.DriverId,
                Driver = new Driver()
                {
                    DriverId = ride.DriverId,
                    Name = driverName
                },
                EmployeeId = ride.EmployeeId,
                Employee = employee == null ? null : new Employee()
                {
                    EmployeeId = employee.EmployeeId,
                    Picture = employee.Picture,
                    Name = employee.Name
                }
            };

            return created;
        }
    }
}