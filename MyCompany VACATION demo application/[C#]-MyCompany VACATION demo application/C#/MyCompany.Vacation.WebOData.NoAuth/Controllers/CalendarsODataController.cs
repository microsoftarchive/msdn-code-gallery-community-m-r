namespace MyCompany.Vacation.Web.Controllers.OData
{
    using MyCompany.Vacation.Data;
    using MyCompany.Vacation.Data.Repositories;
    using MyCompany.Vacation.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.OData;
    using System.Web.Http.OData.Query;
    using System.Data.Entity;

    /// <summary>
    /// Vacation request odata controller.
    /// </summary>
    public class CalendarsODataController : EntitySetController<Calendar, int>
    {
        private readonly MyCompanyContext _context;
        private readonly ISecurityHelper _securityHelper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="securityHelper">context</param>
        public CalendarsODataController(MyCompanyContext context, ISecurityHelper securityHelper)
        {
            _context = context;
            _securityHelper = securityHelper;
        }

        /// <summary>
        /// Gets the vacation requests.
        /// </summary>
        /// <returns></returns>
        [Queryable(MaxExpansionDepth = 2)]
        public override IQueryable<Calendar> Get()
        {
            var queryOptions = this.QueryOptions;
            var identity = _securityHelper.GetUser();
            var employee = _context.Employees.Single(e => e.Email == identity);

            var calendars = _context.
                Calendars
                .Include(c=> c.CalendarHolidays)
                .Where(c => c.Offices.Any(o=>o.OfficeId == employee.OfficeId))
                .AsQueryable();

            queryOptions.ApplyTo(calendars);

            return calendars.AsQueryable();
        }
    }
}
