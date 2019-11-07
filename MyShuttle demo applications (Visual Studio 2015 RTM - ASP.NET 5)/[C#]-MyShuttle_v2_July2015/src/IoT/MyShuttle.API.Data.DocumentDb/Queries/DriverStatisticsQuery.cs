using MyShuttle.API.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;

namespace MyShuttle.API.Data.DocumentDb.Queries
{
    public class DriverStatisticsQuery : BaseDocumentQuery
    {
        public DriverStatisticsQuery(DocumentDbContext ctx) : base(ctx)
        {
            GetGlobalStatisticsAlso = true;
        }

        public int DriverId { get; set; }

        public bool GetGlobalStatisticsAlso { get; set; }

        public async Task<Tuple<RidesStatisticsDto, RidesStatisticsDto>> ExecuteAsync()
        {
            var collection = await DocumentContext.GetCollectionAsync(DocumentDbContext.CollectionTrackedRides);
            var driverRidesSql = "SELECT c.Miles, c.GpsAverageSpeed, c.Breakdowns FROM c where c.DriverId = " + DriverId;
            var driverStats = GetRideStatistics(collection, driverRidesSql);
            RidesStatisticsDto allStats = null;
            if (GetGlobalStatisticsAlso)
            {
                var allRidesSql = "SELECT c.Miles, c.GpsAverageSpeed, c.Breakdowns FROM c";
                allStats = GetRideStatistics(collection, allRidesSql);
            }
            return new Tuple<RidesStatisticsDto, RidesStatisticsDto>(item1: driverStats, item2: allStats);
        }

        private RidesStatisticsDto GetRideStatistics(DocumentCollection collection, string sql)
        {
            var rides = DocumentContext.CreateDocumentQueryFromSQL(collection,
               sql).
               ToList();

            var statistics = new RidesStatisticsDto();
            var count = 0;

            foreach (dynamic rideData in rides)
            {
                statistics.Breakdowns += rideData.Breakdowns;
                statistics.AvgSpeed = ((statistics.Miles * statistics.AvgSpeed) + (rideData.Miles * rideData.GpsAverageSpeed)) / (statistics.Miles + rideData.Miles);
                statistics.Miles += rideData.Miles;
                statistics.Items = ++count;
            }


            return statistics;
        }
    }
}
