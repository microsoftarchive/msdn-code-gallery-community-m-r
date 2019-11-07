using MyShuttle.API.Data.DocumentDb.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.Extensions
{
    static class TrackedRidesByVehicleQueryExtensions
    {
        public static void SetYearAndMonth(this TrackedRidesByVehicleQuery query, int year, int month)
        {
            var firstDayOfMonth = new DateTime(year, month, 1);
            var toYear = year;
            var toMonth = month + 1;
            if (month == 12)
            {
                toYear++;
                toMonth = 1;
            }

            var toDate = new DateTime(toYear, toMonth, 1);

            query.StartDateTime = firstDayOfMonth;
            query.ExclusiveEndDateTime = toDate;
        }
    }
}
