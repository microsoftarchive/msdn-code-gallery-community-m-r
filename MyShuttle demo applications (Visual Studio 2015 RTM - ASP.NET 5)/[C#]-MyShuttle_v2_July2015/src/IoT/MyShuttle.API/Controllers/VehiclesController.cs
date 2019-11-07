using MyShuttle.API.Data.DocumentDb.Factories;
using MyShuttle.API.Data.Factories;
using MyShuttle.API.Extensions;
using MyShuttle.API.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MyShuttle.API.Controllers
{
    public class VehiclesController : ApiController
    {
        private const int DefaultAlarmsCount = 8;

        private readonly IVehicleDocumentsFactory _vehicleFactory;
        private readonly ITrackedRidesDocumentsFactory _trackedRidesFactory;
        private readonly IRideFactoryQuery _sqlRideFactory;

        public VehiclesController(IVehicleDocumentsFactory vfactory, ITrackedRidesDocumentsFactory trf, IRideFactoryQuery rf)
        {
            _vehicleFactory = vfactory;
            _trackedRidesFactory = trf;
            _sqlRideFactory = rf;
        }

        [Route("vehicles/summary")]
        [HttpGet]
        public async Task<IHttpActionResult> GetSummary()
        {
            var data = await _vehicleFactory.GetSummary().ExecuteAsync();
            return Ok(data);
        }

        [Route("vehicles/{device}/alarms")]
        public async Task<IHttpActionResult> GetAlarms(string device)
        {
            var query = _trackedRidesFactory.GetTrackedRidesByVehicle();
            query.DeviceId = device;
            query.WithField(tr => tr.Obds);
            var rides = await query.ExecuteAsync();
            var alarms = rides.SelectMany(r => r.Obds).ToList().OrderByDescending(o => o.Date).
                Take(DefaultAlarmsCount).
                Select(o => new
                {
                    o.Code,
                    o.Date,
                    Level = 1
                });
            return Ok(alarms);
        }

        [Route("vehicles/{device}/miles/{year:int}")]
        public async Task<IHttpActionResult> GetMilesPerYear(string device, int year)
        {
            var firstDayOfYear = new DateTime(year, 1, 1);
            var trackedRidesQuery = _trackedRidesFactory.GetTrackedRidesByVehicle();
            trackedRidesQuery.DeviceId = device;
            trackedRidesQuery.StartDateTime = firstDayOfYear;
            trackedRidesQuery.ExclusiveEndDateTime = new DateTime(year + 1, 1, 1);
            trackedRidesQuery.WithField(tr => tr.Miles).WithField(tr => tr.StartTime).WithField(tr => tr.DriverId);

            var sqlRidesQuery = _sqlRideFactory.GetRidesPerYearAndVehicle();
            sqlRidesQuery.Year = year;
            sqlRidesQuery.DeviceId = device;
            var trackedRides = (await trackedRidesQuery.ExecuteAsync()).ToArray();
            var sqlRides = (await sqlRidesQuery.ExecuteAsync()).ToArray();

            var trackedGroupedPerMonth = trackedRides.GroupBy(r => r.StartTime.Month).Select(g => new
            {
                Month = g.Key,
                TotalMiles = g.Sum(gi => gi.Miles)
            });

            var sqlGroupedPerMonth = sqlRides.GroupBy(r => r.StartDateTime.Month).Select(g => new
            {
                Month = g.Key,
                TotalMiles = g.Sum(gi => gi.Distance)
            });

            var combined = trackedGroupedPerMonth.Select(tg => new
            {
                tg.Month,
                tg.TotalMiles,
                InvoicedMiles = sqlGroupedPerMonth.SingleOrDefault(sr => sr.Month == tg.Month)?.TotalMiles ?? 0.0
            });

            return Ok(combined);

        }

        [Route("vehicles/{device}/miles/{year:int}/{month:int}")]
        public async Task<IHttpActionResult> GetMilesPerYearMonth(string device, int year, int month)
        {

            if (month < 1 || month > 12)
            {
                return this.BadRequest("Invalid month index " + month);
            }

            var firstDayOfYear = new DateTime(year, month, 1);
            var trackedRidesQuery = _trackedRidesFactory.GetTrackedRidesByVehicle();
            trackedRidesQuery.DeviceId = device;
            trackedRidesQuery.SetYearAndMonth(year, month);
            trackedRidesQuery.WithField(tr => tr.Miles).WithField(tr => tr.StartTime).WithField(tr => tr.DriverId);

            var sqlRidesQuery = _sqlRideFactory.GetRidesPerMonthYearVehicleQuery();
            sqlRidesQuery.Year = year;
            sqlRidesQuery.Month = month;
            sqlRidesQuery.DeviceId = device;
            var trackedRides = (await trackedRidesQuery.ExecuteAsync()).ToArray();
            var sqlRides = (await sqlRidesQuery.ExecuteAsync()).ToArray();

            var trackedGroupedPerDay = trackedRides.GroupBy(r => r.StartTime.Day).Select(g => new
            {
                Day = g.Key,
                TotalMiles = g.Sum(gi => gi.Miles)
            });

            var sqlGroupedPerDay = sqlRides.GroupBy(r => r.StartDateTime.Day).Select(g => new
            {
                Day = g.Key,
                TotalMiles = g.Sum(gi => gi.Distance)
            });

            var combined = trackedGroupedPerDay.Select(tg => new
            {
                tg.Day,
                tg.TotalMiles,
                InvoicedMiles = sqlGroupedPerDay.SingleOrDefault(sr => sr.Day == tg.Day)?.TotalMiles ?? 0.0
            });

            return Ok(combined);
        }

        [Route("vehicles/{device}/details")]
        public async Task<IHttpActionResult> GetDetails(string device)
        {
            var query = _trackedRidesFactory.GetTrackedRidesByVehicle();
            query.DeviceId = device;
            query.WithField(tr => tr.Breakdowns).
                WithField(tr => tr.GpsAverageSpeed).
                WithField(tr => tr.Miles);
            var rides = await query.ExecuteAsync();
            var details = new VehicleDetailsSummaryDto();
            var ridesCount = 0;
            foreach (var ride in rides)
            {
                details.AvgSpeed = (details.AvgSpeed * details.TotalMiles + ride.GpsAverageSpeed * ride.Miles) /
                    (details.TotalMiles + ride.Miles);

                details.AvgConsumption = (details.AvgConsumption * details.TotalMiles + ride.GetAvgConsumption() * ride.Miles) /
                    (details.TotalMiles + ride.Miles);

                details.TotalMiles += ride.Miles;
                details.Breakdowns += ride.Breakdowns;
                ridesCount++;
            }
            details.TotalRides = ridesCount;
            return Ok(details);
        }
    }
}
