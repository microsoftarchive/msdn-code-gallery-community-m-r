
namespace MyShuttle.Data
{
    using Model;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Collections.Generic;
    using System;
    using Microsoft.Data.Entity;

    public class VehicleRepository : IVehicleRepository
    {
        MyShuttleContext _context;


        public VehicleRepository(MyShuttleContext dbcontext)
        {
            _context = dbcontext;
        }


        public async Task<Vehicle> GetAsync(int vehicleId)
        {
            var results = await _context.Vehicles
               .Where(v => v.VehicleId == vehicleId)
               .Select(v => new
               {
                   Vehicle = v,
                   Driver = _context.Drivers.SingleOrDefault(d => d.DriverId == v.DriverId)
               })
               .ToListAsync();

            return results.Select(d => BuildVehicle(d.Vehicle, d.Driver, null)).SingleOrDefault();
        }

        public async Task<Vehicle> GetByDeviceIdAsync(string deviceId)
        {
            var results = await _context.Vehicles
               .Where(v => v.DeviceId == deviceId)
               .Select(v => new
               {
                   Vehicle = v,
                   Driver = _context.Drivers.SingleOrDefault(d => d.DriverId == v.DriverId)
               })
               .ToListAsync();

            return results.Select(d => BuildVehicle(d.Vehicle, d.Driver, null)).SingleOrDefault();
        }

        public async Task<IEnumerable<Vehicle>> GetVehiclesAsync(int carrierId, string filter, int pageSize, int pageCount)
        {
            var results = await _context.Vehicles
                .Where(v =>
                    v.CarrierId == carrierId &&
                    (String.IsNullOrEmpty(filter) ||
                    v.LicensePlate.Contains(filter) || v.Make.Contains(filter) || v.Model.Contains(filter)))
                .Select(v => new
                {
                    Vehicle = v,
                })
                .OrderBy(v => v.Vehicle.LicensePlate)
                .Skip(pageSize * pageCount)
                .Take(pageSize)
                .ToListAsync();

            return results.Select(v => BuildVehicle(v.Vehicle, null, null));
        }

        public async Task<IEnumerable<Vehicle>> GetVehiclesFilterAsync(int carrierId)
        {
            var results = await _context.Vehicles
                .Where(v => v.CarrierId == carrierId )
                .Select(v => new
                {
                    Vehicle = v,
                })
                .OrderBy(v => v.Vehicle.LicensePlate)
                .ToListAsync();

            return results.Select(v => BuildVehicle(v.Vehicle, null, null, 0, false));
        }

        public async Task<int> GetCountAsync(int carrierId, string filter)
        {
            return await _context.Vehicles
                .Where(v =>
                    v.CarrierId == carrierId &&
                    (String.IsNullOrEmpty(filter) ||
                    v.LicensePlate.Contains(filter) || v.Make.Contains(filter) || v.Model.Contains(filter)))
                .CountAsync();
        }

        public async Task<int> AddAsync(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);

            await _context.SaveChangesAsync();

            return vehicle.VehicleId;
        }

        public async Task UpdateAsync(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int vehicleId)
        {
            var Vehicle = await _context.Vehicles
                .SingleOrDefaultAsync(d => d.VehicleId == vehicleId);

            if (Vehicle != null)
            {
                _context.Vehicles
                    .Remove(Vehicle);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<Vehicle>> GetTopVehiclesAsync(int carrierId, int numOfDrivers)
        {
            return await _context.Vehicles
                .Where(d => d.CarrierId == carrierId)
                .OrderByDescending(d => d.RatingAvg)
                .Take(numOfDrivers)
                .Select(d => new Vehicle
                {
                    VehicleId = d.VehicleId,
                    LicensePlate = d.LicensePlate,
                    Make = d.Make,
                    Model = d.Model,
                    Picture = d.Picture,
                    RatingAvg = d.RatingAvg,
                    TotalRides = d.TotalRides,
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<Vehicle>> GetByPriceAsync(double latitude, double longitude, int count)
        {
            int defaultDistance = 50;
            var results = await _context.Vehicles
                .OrderBy(v => v.Rate)
                .Where(v => GetDistance(v.Latitude, v.Longitude, latitude, longitude, 'M') <= defaultDistance)
                .Select(v => new
                {
                    Vehicle = v,
                })
                .Take(count)
                .ToListAsync();

            return results.Select(v => BuildVehicle(v.Vehicle, null, null, 0));
        }

        public async Task<IEnumerable<Vehicle>> GetByDistanceAsync(double latitude, double longitude, int count)
        {
            // TODO: Review this query with beta6 to avoid N+1. Beta5 generates InvalidCastException .
            var vehicles = await _context.Vehicles
                .ToListAsync();

            var results = vehicles.Select(v => new
                {
                    Vehicle = v,
                    Carrier = _context.Carriers.SingleOrDefault(c => c.CarrierId == v.CarrierId),
                    DistanceFromGivenPosition = GetDistance(v.Latitude, v.Longitude, latitude, longitude, 'M')
                })
                .OrderBy(v => v.DistanceFromGivenPosition)
                .Take(count);
            
            return results.Select(v => BuildVehicle(v.Vehicle, null, null, v.DistanceFromGivenPosition));
        }


        private Vehicle BuildVehicle(Vehicle vehicle, Driver driver, Carrier carrier, double distanceFromGivenPosition = 0, 
            bool includePicture = true)
        {
            //the idea is remove reference for improve 
            //client work without $ref
            var created = new Vehicle
            {
                VehicleId = vehicle.VehicleId,
                CarrierId = vehicle.CarrierId,
                Picture = includePicture ? vehicle.Picture : null,
                DriverId = vehicle.DriverId,
                LicensePlate = vehicle.LicensePlate,
                DistanceFromGivenPosition = distanceFromGivenPosition,
                Make = vehicle.Make,
                Latitude = vehicle.Latitude,
                Longitude = vehicle.Longitude,
                Model = vehicle.Model,
                Seats = vehicle.Seats,
                Type = vehicle.Type,
                VehicleStatus = vehicle.VehicleStatus,
                Rate = vehicle.Rate,
                RatingAvg = vehicle.RatingAvg,
                TotalRides = vehicle.TotalRides,
                DeviceId = vehicle.DeviceId,
                Driver = driver == null ? null : new Driver()
                {
                    DriverId = driver.DriverId,
                    Name = driver.Name,
                    Phone = driver.Phone
                },
                Carrier = carrier == null ? null : new Carrier()
                {
                    Picture = carrier.Picture
                },
            };

            return created;
        }


        //::: This routine calculates the distance between two points(given the     :::
        //:::  latitude/longitude of those points). It is being used to calculate     :::
        //:::  the distance between two locations using GeoDataSource(TM) products    :::
        //:::                                                                         :::
        //:::  Definitions:                                                           :::
        //:::    South latitudes are negative, east longitudes are positive           :::
        //:::                                                                         :::
        //:::  Passed to function:                                                    :::
        //:::    lat1, lon1 = Latitude and Longitude of point 1 (in decimal degrees)  :::
        //:::    lat2, lon2 = Latitude and Longitude of point 2 (in decimal degrees)  :::
        //:::    unit = the unit you desire for results                               :::
        //:::           where: 'M' is statute miles                                   :::
        //:::                  'K' is kilometers (default)                            :::
        //:::                  'N' is nautical miles                                  :::
        //:::                                                                         :::

        private static double GetDistance(double lat1, double lon1, double lat2, double lon2, char unit)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            if (unit == 'K')
            {
                dist = dist * 1.609344;
            }
            else if (unit == 'N')
            {
                dist = dist * 0.8684;
            }
            return (dist);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts decimal degrees to radians             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private static double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts radians to decimal degrees             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        private static double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }


    }
}