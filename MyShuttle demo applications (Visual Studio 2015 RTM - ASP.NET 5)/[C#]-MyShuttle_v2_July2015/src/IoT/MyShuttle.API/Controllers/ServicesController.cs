using MyShuttle.API.Data.Factories;
using MyShuttle.API.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace MyShuttle.API.Controllers
{
    public class ServicesController : ApiController
    {
        const int DefaultDaysForRidePeriods = 7;
        private readonly IRideFactoryQuery _queryFactory;
        public ServicesController(IRideFactoryQuery qfactory)
        {
            _queryFactory = qfactory;
        }

        [Route("services/satisfaction")]
        [HttpGet]
        public async Task<IHttpActionResult> GetSatisfaction([FromUri]int? days = null, [FromUri] string from = null)
        {
            var dtFrom = ParseFrom(from);
            var windowSize = days ?? DefaultDaysForRidePeriods;
            var ridesQuery = _queryFactory.GetRidesBetweenDates();
            ridesQuery.ToDateIncluded = dtFrom;
            ridesQuery.SetIntervalInDays(windowSize);
            var firstSet = await ridesQuery.ExecuteAsync();
            ridesQuery.ToDateIncluded = ridesQuery.FromDate - TimeSpan.FromDays(1);
            ridesQuery.SetIntervalInDays(windowSize);
            var secondSet = await ridesQuery.ExecuteAsync();

            return Ok(new ServicesSummaryDto(firstSet, secondSet));

        }

        private DateTime ParseFrom(string from)
        {
            if (string.IsNullOrEmpty(from))
            {
                return DateTime.UtcNow;
            }
            int ifrom;
            if (int.TryParse(from, out ifrom))
            {
                return DateTime.UtcNow + TimeSpan.FromDays(ifrom);
            }
            DateTime dtFrom;
            return DateTime.TryParseExact(from, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtFrom) ?
                dtFrom : 
                DateTime.UtcNow; 
        }
    }
}
