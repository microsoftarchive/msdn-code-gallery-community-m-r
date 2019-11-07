using MyShuttle.API.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.Extensions
{
    static class TrackedRideDocumentExtensions
    {
        private const double DefaultConsumptionLitersPer100Miles = 9;
        public static double GetAvgConsumption(this TrackedRideDocument tr)
        {
            var baseConsumptionLiters = (DefaultConsumptionLitersPer100Miles * tr.Miles) / 100.0;
            var additionalPercent = tr.GpsAverageSpeed <= 60.0 ? 0.0 : (tr.GpsAverageSpeed - 60.0) / 100.0;
            var totalLiters = baseConsumptionLiters + baseConsumptionLiters * additionalPercent;
            var totalInLitersPer100Miles = (totalLiters * 100.0) / tr.Miles;
            var totalInGallonsPer100Miles = totalInLitersPer100Miles * 0.264172052;
            return totalInGallonsPer100Miles;
        }
    }
}
