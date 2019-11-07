namespace MyCompany.Vacation.Web.Controllers.OData
{
    using MyCompany.Vacation.Data;
    using MyCompany.Vacation.Web.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Data.Entity;
    using MyCompany.Vacation.Model;

    /// <summary>
    /// Current employee controller
    /// </summary>
    public class CurrentEmployeesController : ApiController
    {
        private readonly MyCompanyContext _context;
        private readonly ISecurityHelper _securityHelper;

        /// <summary>
        /// CurrentEmployeesController constructor
        /// </summary>
        public CurrentEmployeesController(MyCompanyContext context, ISecurityHelper securityHelper)
        {
            _context = context;
            _securityHelper = securityHelper;
        }

        /// <summary>
        /// Get's the current employee
        /// </summary>
        /// <returns></returns>
        [Queryable]
        public IQueryable<CurrentEmployee> Get()
        {
            var identity = _securityHelper.GetUser();

            var currentEmployee = _context.
                Employees
                .Include(e => e.EmployeePictures)
                .Include(e => e.ManagedTeams)
                .Where(e => e.Email == identity)
                .Select(employee =>
                    new CurrentEmployee
                    {
                        EmployeeId = employee.EmployeeId,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        Email = employee.Email,
                        OfficeId = employee.OfficeId,
                        TeamId = employee.TeamId,
                        ManagedTeams = employee.ManagedTeams,
                        EmployeePictures = employee.EmployeePictures
                    }).ToList();

            return currentEmployee.AsQueryable(); 
        }
    }
}
