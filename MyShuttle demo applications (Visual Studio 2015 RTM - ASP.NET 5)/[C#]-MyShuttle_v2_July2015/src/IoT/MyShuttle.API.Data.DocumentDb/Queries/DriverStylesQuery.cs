using MyShuttle.API.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.Data.DocumentDb.Queries
{
    public class DriverStylesQuery : BaseDocumentQuery
    {
        public DriverStylesQuery(DocumentDbContext ctx) : base(ctx)
        {
        }

        public int DriverId { get; set; }

        public async Task<DrivingStyleDto> ExecuteAsync()
        {
            var stylesCollection = await DocumentContext.GetCollectionAsync(DocumentDbContext.CollectionDrivingStyles);
            var driverStyleDataItems = DocumentContext.CreateDocumentQueryFromSQL(stylesCollection, "select c.ClassificationAvg, c.Classifications from c where c.DriverId = " + DriverId).ToList();
            dynamic driverStyleData = driverStyleDataItems.FirstOrDefault();

            var driverStatisticsQuery = new DriverStatisticsQuery(DocumentContext) { DriverId = DriverId, GetGlobalStatisticsAlso = false };
            var statistics = await driverStatisticsQuery.ExecuteAsync();
            var driverStatistics = statistics.Item1;

            var style = new DrivingStyleDto();
            if (driverStatistics != null)
            {
                style.Breakdowns = driverStatistics.Breakdowns <= 10 ? (int)driverStatistics.Breakdowns : 10;
                style.Speed = DrivingStyleDto.SpeedFromAvgSpeed(driverStatistics.AvgSpeed);
                if (driverStatistics.Items > 0)
                {
                    style.Profiability = 10 - (int)(driverStatistics.Breakdowns / driverStatistics.Items);
                }
            }
            if (driverStyleData != null)
            {
                style.AvgStyle = driverStyleData.ClassificationAvg;
                style.Consumption = (int)driverStyleData.ClassificationAvg;
                style.Aggressiveness = 10 - (int)driverStyleData.ClassificationAvg;
                style.Brakes = (int)driverStyleData.ClassificationAvg;
            }
 
            return style;
        }
    }
}
