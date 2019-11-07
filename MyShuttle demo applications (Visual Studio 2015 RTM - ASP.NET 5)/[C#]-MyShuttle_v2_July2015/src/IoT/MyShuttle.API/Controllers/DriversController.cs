namespace MyShuttle.API.Host.Controllers
{
    using MyShuttle.API.Data.DocumentDb.Factories;
    using MyShuttle.API.Data.Factories;
    using MyShuttle.API.Model.Dtos;
    using MyShuttle.API.Results;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;

    public class DriversController : ApiController
    {
        private const int DefaultDriversCount = 10;

        private readonly IDriverQueryFactory _driverFactory;
        private readonly IDrivingStyleDocumentsFactory _drivingStylesFactory;
        private readonly ITrackedRidesDocumentsFactory _trackedRidesFactory;

        public DriversController(IDriverQueryFactory driverFactory, IDrivingStyleDocumentsFactory drivingStyleFactory, ITrackedRidesDocumentsFactory trdf)
        {
            _drivingStylesFactory = drivingStyleFactory;
            _driverFactory = driverFactory;
            _trackedRidesFactory = trdf;
        }


        [Route("drivers/top")]
        [HttpGet]
        public async Task<IHttpActionResult> TopDrivers([FromUri] bool? withPhoto = null, [FromUri] int? count = null)
        {
            var desiredDriversCount = count.HasValue ? count.Value : DefaultDriversCount;
            var topRatedQuery = _drivingStylesFactory.GetTopRatedDrivers();
            topRatedQuery.DesiredDrivers = desiredDriversCount;

            var driversDataQuery = _driverFactory.GetTopDrivers();

            var totalDriversCount = await driversDataQuery.GetTotalTopDrivers();

            var topRated = await topRatedQuery.ExecuteAsync();
            if (!topRated.Any())
            {
                return Ok(CollectionResult.Empty(desiredDriversCount));
            }

            driversDataQuery.SetIds(topRated.Select(tr => tr.DriverId));

            var driversData = await driversDataQuery.ExecuteAsync();
            var needsPhoto = withPhoto.HasValue && withPhoto.Value;
            var neededDriversData = driversData.Select(d => new { d.DriverId, d.Name, d.TotalRides, d.RatingAvg, Picture = needsPhoto ? d.Picture : null });
            var finalInfo = new List<object>();

            foreach (var topRatedDriver in topRated)
            {
                finalInfo.Add(new
                {
                    DriverId = topRatedDriver.DriverId,
                    Rate = new
                    {
                        AvgRate = topRatedDriver.ClassificationAvg,
                        NumRates = topRatedDriver.Classifications.Count()
                    },
                    DriverInfo = neededDriversData.FirstOrDefault(ndd => ndd.DriverId == topRatedDriver.DriverId)
                });
            }

            return Ok(new CollectionResult(finalInfo, desiredDriversCount, totalDriversCount));
        }

        [Route("drivers/{id:int}/card")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDriverCard(int id)
        {
            var driverQuery = _driverFactory.GetDriverById().IncludeRides();
            driverQuery.Id = id;
            var driver = await driverQuery.ExecuteAsync();
            if (driver == null)
            {
                return NotFound();
            }

            var driverCard = new DriverCardDto(driver);

            return Ok(driverCard);
        }

        [Route("drivers/{id:int}/statistics")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDriverStatistics(int id)
        {
            var query = _trackedRidesFactory.GetDriverStatistics();
            query.DriverId = id;
            var statistics = await query.ExecuteAsync();
            return Ok(new { Driver = statistics.Item1, Global = statistics.Item2 });
        }

        [Route("drivers/{id:int}/style")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDriverStyle(int id)
        {
            var query = _drivingStylesFactory.GetStylesForDriver();
            query.DriverId = id;
            var style = await query.ExecuteAsync();
            return Ok(style);
        }
    }
}
