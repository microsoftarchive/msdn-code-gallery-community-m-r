namespace MyCompany.Vacation.Web.Controllers.OData
{
    using MyCompany.Vacation.Data.Repositories;
    using MyCompany.Vacation.Model;
    using MyCompany.Vacation.Web.Infraestructure.Security;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http.OData;

    /// <summary>
    /// Offices Odata controller.
    /// </summary>
    [MyCompanyAuthorization]
    public class OfficesODataController : EntitySetController<Office, int>
    {
        private readonly ICalendarRepository _calendarRepository;

        /// <summary>
        /// Contructor
        /// </summary>
        public OfficesODataController(ICalendarRepository calendarRepository) {
            _calendarRepository = calendarRepository;
        }
    }
}